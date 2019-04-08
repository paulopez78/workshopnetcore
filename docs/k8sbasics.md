## Kubernetes Workshop 

### Kubernetes local setup: docker for desktop vs minikube
*  kubernetes building blocks

*  kubectl command line tool basics
    *  kubectl cluster-info
    *  kubectl config get-contexts
    *  kubectl get/decribe nodes

*  Pod resource
    *  kubectl run --generator=run-pod/v1
    *  kubectl logs votingapp
    *  kubectl exec -it votingapp -- sh
    *  kubectl port-forward votingapp 8080:80
    *  curl http://localhost:8080/api/voting
    *  kubectl expose pod votingapp --type=NodePort
    *  curl http://localhost:30834/api/voting
    *  kubectl get pod votingapp -o yaml > pod.yaml 
    *  labels
        *  kgp --show-labels
        *  kgp -L app,env -l app,env=pro --all-namespaces

*  Kubernetes dashboard

*  Replication Controller
    *  kubectl run --generator=run/v1
    *  label selectors
    *  --watch

*  Services ,endpoints and networking
    *  kubectl run --generator=run/v1

*  CI/CD with Deployments