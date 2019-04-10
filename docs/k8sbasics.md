## Kubernetes Workshop 

### Kubernetes local setup: docker for desktop vs minikube
*  kubernetes building blocks

*  kubectl command line tool basics
    *  kubectl cluster-info
    *  kubectl config get-contexts
        * kubectx and kubens
        * https://github.com/ahmetb/kubectx
    *  kubectl get/decribe nodes
    *  kubectl explain nodes
* Kubernetes dashboard

*  Pod resource
    *  kubectl run --generator=run-pod/v1
    *  kubectl logs votingapp
    *  kubectl exec -it votingapp -- sh
    *  kubectl port-forward votingapp 8080:80
    *  curl http://localhost:8080/api/voting
    *  kubectl expose pod votingapp --type=NodePort
    *  curl http://localhost:30834/api/voting
    *  kubectl get pod votingapp -o yaml > pod.yaml 
    *  add nginx container to votingapp pod
        * hostname -I
    *  kubectl exec -it votingapp -c gateway -- sh
    *  labels
        *  kgp --show-labels
        *  kgp -L app,env -l app,env=pro --all-namespaces
        *  k label po votingapp app=votingapp --overwrite
    *  kubectl get svc votingapp -o yaml > svc.yaml 

*  Replication Controller
    *  kubectl run --generator=run/v1
    *  label selectors
    *  --watch
    *  Manual rolling update with rc

*  Services ,endpoints, DNS, and networking
    * https://hub.docker.com/r/tutum/dnsutils
    * kubectl run dnsutils --image=tutum/dnsutils --generator=run-pod/v1 --command -- sleep infinity
    * kubedns server, /etc/resolv.conf, environment variables
    * headless services (without clusterIP)

*  ConfigMaps and Secrets
    * k create configmap votingapp-staging --from-file=src/VotingApp.Api/appsettings.json
    

*  ReplicaSets and Deployments
*  Persistent Volumes
*  Stateful