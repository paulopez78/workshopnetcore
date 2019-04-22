kubectl create ns votingapp
kubectl delete cm votingapp
kubectl create cm votingapp --from-file=nginx.conf
kubectl apply -f votingapp.yaml

# kubectl exec tools -it -c curl -- curl http://votingapp/