
deploy_rc(){
    kubectl run votingapp \
        --namespace $TAG \
        --image=$REGISTRY/votingapp:$TAG \
        --generator=run/v1
}

scale_rc(){
    kubectl scale rc votingapp --replicas=3
}

kubectl run --generator=run-pod/v1
kubectl port-forward pod-name 8080:80
kubectl get pods --show-labels
kubectl get pods -L run, app
kubectl label po pod-name env=dev --overwrite

# label selectors
kubectl get pod -l run=votingapp
kubectl get pod -l run=votingapp,env=test
kubectl get pod -l 'run in (votingapp)'
kubectl get pod -l 'run in (votingapp)'
kubectl get pod -l '!run'