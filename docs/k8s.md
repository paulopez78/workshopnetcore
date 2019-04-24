## Kubernetes Workshop 

### Kubernetes Basics

*  kubernetes building blocks and local setup
    * Kubernetes cluster explanation
    * Local kubernetes: Docker-for-desktop or minikube
    * Cloud kubernetes: GKE, EKS, AKS
    * Install WSL (for windows users), [oh-my-zsh, kubectl plugin](https://github.com/robbyrussell/oh-my-zsh)
    * [kubectx and kubens](https://github.com/ahmetb/kubectx)
    * kubectl config command
    * kubectl cluster-info

*  Pods and Services Basics
    * Create votignapp with kubectl run, explain the pod concept.
        * k port-forward
        * k logs
        * k exec 
        * k proxy, curl http://localhost:8001/api/v1/namespaces/default/pods/votingapp/proxy/
        * k proxy, curl http://localhost:8001/api/v1/namespaces/default/services/votingapp/proxy/
    * Expose votignapp with kubectl expose, explain the service concept. 
    * Expose votingapp using NodePort
    * Use dnstools explaining DNS networking.
    * Upgrading votingapp to version2:
        * Set image downtime.
        * Create votingapp v2 (rolling update)
        * blue-green and rollback
    * Multicontainer pod with curl and nslookup tools and introduction to a Volume emptyDir.

*  Services, endpoints, DNS, and networking
    * kubectl run dnsutils --image=paulopez/dnsutils --generator=run-pod/v1 --command -- sleep infinity
    * kubedns server, /etc/resolv.conf, environment variables

*  Legacy Replication Controller 
    *  kubectl run --generator=run/v1
    *  label selectors
    *  --watch
    *  rolling update with rc

*  ConfigMaps and Secrets
    * Add nginx as sidecar container of votingapp
    * mount appsettings.json as configmap and appsettings.secrets.json as a secret

* Ingress Controller
    * kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/mandatory.yaml
    * kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/provider/cloud-generic.yaml

*  Deployments and Rolling Updates
    * kubectl set image deployment/votingapp votingapp=localhost:30400/votingapp:latest
    * kubectl rollout history deployments votingapp
    * kubectl rollout undo deployment/votingapp --to-revision=1

*  Stateful and Persistent Volumes
    * Votingapp with redis database

### Helm Package Manager

* Helm package manager

### Kubernetes advanced topics

* Kubernetes API (optional)
    * kubectl proxy
    * curl http://localhost:8001/api/v1/namespaces/votingapp/pods/votingapp  == kubectl get pod votingapp -o json -n votingapp
    * use tools pod for get access to the k8s api from inside a container
    * cd /var/run/secrets/kubernetes.io/serviceaccount
    * curl -H "Authorization: Bearer ${TOKEN}" --cacert ca.crt   https://kubernetes/app
    * kubectl create clusterrolebinding permissive-binding --clusterrole=cluster-admin --group=system:serviceaccounts

* RBAC (optional)
