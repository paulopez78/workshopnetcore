function deploy_votingapp()
{
    postgrePassword=$(kubectl get secret --namespace ${NAMESPACE} postgresql-${NAMESPACE} -o jsonpath="{.data.postgres-password}" | base64 --decode)
    rabbitPassword=$(kubectl get secret --namespace ${NAMESPACE} rabbit-${NAMESPACE}-rabbitmq -o jsonpath="{.data.rabbitmq-password}" | base64 --decode)
    dbconnection='Username=postgres;Password='${postgrePassword}';Host=postgresql-'${NAMESPACE}''
    messagebroker='amqp://user:'${rabbitPassword}'@rabbit-'${NAMESPACE}'-rabbitmq'

    kubectl create secret generic votingapp-secrets \
        --from-literal=dbconnection=$dbconnection \
        --from-literal=messagebroker=$messagebroker \
        --namespace $NAMESPACE

    for deployment in commands queries ui
    do
        component=votingapp-$deployment
        sed 's/${TAG}/'${TAG}'/g' k8s/$component.yml | kubectl apply -f - -n $NAMESPACE
        kubectl rollout status deployment/$component -n $NAMESPACE
    done
}

function deploy_registry()
{
    kubectl apply -f k8s/registry.yml
    kubectl rollout status deployment/registry
}

function install_helm()
{
    kubectl create serviceaccount --namespace kube-system tiller
    kubectl create clusterrolebinding tiller-cluster-rule --clusterrole=cluster-admin --serviceaccount=kube-system:tiller
    helm init --upgrade --service-account tiller --wait
}

function deploy_infra()
{
    helm install stable/rabbitmq --name 'rabbit-'${NAMESPACE} --namespace $NAMESPACE --wait
    helm install stable/postgresql --name 'postgresql-'${NAMESPACE} --namespace $NAMESPACE --wait
}

function destroy_infra()
{
    helm delete 'rabbit-'${NAMESPACE} --purge
    helm delete 'postgresql-'${NAMESPACE} --purge
    kubectl delete namespace $NAMESPACE
}

function integration_tests()
{
    kubectl run integration-tests \
        --attach \
        --restart=Never \
        --namespace $NAMESPACE \
        --image=$REGISTRY/integration-tests:$NAMESPACE \
        --rm \
        -- votingapp-commands
}

install_helm

set -e
export REGISTRY=localhost:30400
export TAG=$RANDOM
export NAMESPACE=$TAG

# BUILD AND PUSH
deploy_registry 
docker-compose build && docker-compose push

# DEPLOY BUILD AND RUN INTEGRATION TESTS
deploy_infra
deploy_votingapp
integration_tests
destroy_infra

# DEPLOY TO PRODUCTION
set +e
export NAMESPACE=production

kubectl create namespace production
deploy_infra
deploy_votingapp

