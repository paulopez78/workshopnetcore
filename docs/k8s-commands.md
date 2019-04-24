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
    *  kubernetes dashboard

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
            kubectl get pod -l run=votingapp
            kubectl get pod -l run=votingapp,env=test
            kubectl get pod -l 'run in (votingapp)'
            kubectl get pod -l 'run in (votingapp)'
            kubectl get pod -l '!run'
    *  kubectl get svc votingapp -o yaml > svc.yaml 
    *  health checks: readiness and liveliness proof
    *  k patch svc votingapp -p '{"spec":{"selector":{"run":"votingapp2"}}}'

*  Replication Controller and ReplicaSets
    *  kubectl run --generator=run/v1
    *  label selectors
    *  --watch
    *  Manual rolling update with rc

*  Services ,endpoints, DNS, and networking
    * kubectl run dnsutils --image=paulopez/dnsutils --generator=run-pod/v1 --command -- sleep infinity
    * kubedns server, /etc/resolv.conf, environment variables
    * headless services (without clusterIP)
    * Ingress

*  ConfigMaps and Secrets
    * k create configmap votingapp-staging --from-file=src/VotingApp.Api/appsettings.json
    * mount appsettings.secrets.json as a secret

*  Deployments and Rolling Updates
    * kubectl set image deployment/votingapp votingapp=localhost:30400/votingapp:latest
    * kubectl rollout history deployments votingapp
    * kubectl rollout undo deployment/votingapp --to-revision=1

* Kubernetes API    
    * kubectl proxy
    * curl http://localhost:8001/api/v1/namespaces/votingapp/pods/votingapp  == kubectl get pod votingapp -o json -n votingapp
    * use tools pod for get access to the k8s api from inside a container
    * cd /var/run/secrets/kubernetes.io/serviceaccount
    * curl -H "Authorization: Bearer ${TOKEN}" --cacert ca.crt   https://kubernetes/app
    * kubectl create clusterrolebinding permissive-binding --clusterrole=cluster-admin --group=system:serviceaccounts

*  Persistent Volumes
    * Votingapp with redis database

* RBAC

*  Stateful
    *  helm rabbit cluster

* Helm package manager
