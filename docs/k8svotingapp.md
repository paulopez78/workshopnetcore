## Kubernetes Workshop 

### Kubernetes local setup: docker for desktop vs minikube
*  kubernetes building blocks
    * Kubernetes cluster explanation
    * Docker-for-desktop or minikube
    * GKE

*  Pods and Services Basics
    * https://kubernetes.io/docs/reference/kubectl/conventions/
    * Create votignapp with kubectl run, explain the pod concept.
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
    * k create configmap votingapp-staging --from-file=src/VotingApp.Api/appsettings.json
    * mount appsettings.secrets.json as a secret

* Ingress Controller
    * kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/mandatory.yaml
    * kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/provider/cloud-generic.yaml

*  Deployments and Rolling Updates
    * kubectl set image deployment/votingapp votingapp=localhost:30400/votingapp:latest
    * kubectl rollout history deployments votingapp
    * kubectl rollout undo deployment/votingapp --to-revision=1

*  Stateful and Persistent Volumes
    * Votingapp with redis database

* Helm package manager

* Kubernetes API (optional)
    * kubectl proxy
    * curl http://localhost:8001/api/v1/namespaces/votingapp/pods/votingapp  == kubectl get pod votingapp -o json -n votingapp
    * use tools pod for get access to the k8s api from inside a container
    * cd /var/run/secrets/kubernetes.io/serviceaccount
    * curl -H "Authorization: Bearer ${TOKEN}" --cacert ca.crt   https://kubernetes/app
    * kubectl create clusterrolebinding permissive-binding --clusterrole=cluster-admin --group=system:serviceaccounts

* RBAC (optional)
