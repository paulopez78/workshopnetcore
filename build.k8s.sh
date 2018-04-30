function deploy()
{
    kubectl create namespace $1
    sed 's/${TAG}/'${TAG}'/g' k8s/votingapp.yml | kubectl apply -f - -n $1
    kubectl rollout status deployment/votingapp -n $1
}

function deploy_registry()
{
    kubectl apply -f k8s/registry.yml
    kubectl rollout status deployment/registry
}

function integration_tests()
{
    kubectl run integration-tests \
        --attach \
        --restart=Never \
        --namespace $TAG \
        --image=$REGISTRY/integration-tests:$TAG \
        --rm \
        -- votingapp \
    && kubectl delete namespace $TAG
}

set -e
export REGISTRY=localhost:30400
export TAG=$RANDOM

deploy_registry 
docker-compose build
docker-compose push

deploy $TAG
integration_tests

set +e
deploy production