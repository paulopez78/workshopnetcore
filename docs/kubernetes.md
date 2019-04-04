## Kubernetes Workshop 

As usual, we will use the votinapp application as a playground context for understanding all the different kubernetes resources.

### Docker and containers essentials 
*   Process Models: Virtual Machines vs Containers
*   Image and containers internals.
*   Building images and running containers with docker.

### Kubernetes local setup
*   Enable kubernetes in Docker for windows or mac
*   Basic kubectl commands to connect to the cluster (understanding the configuration file) 
*   Setup kubernetes dashboard.
*   Get familiar with the most basic building blocks of a kubernetes cluster (control plane, api server, etcd, kubelet)

### Pods 
*   Understanding why is the most fundamental resource of kubernetes.
*   Running the votingapp in a Pod using the kubectl run command.
*   Creating pods in a declarative way with yaml files
*   Labeling pods and querying them using labels.
*   Using ReplicationControllers and ReplicaSets for managing pods with label selectors.

### Services
*   Understanding the kubernetes networking (kube-proxy and kube-dns)
*   Exposing the votingapp using a service.
*   Internal and External Services (NodePort)
*   LoadBalancer services (only available with cloud setup)
*   Using a simplified model with the ingress controller (nginx controller)

### Kubernetes cloud setup
*   Comparing the 3 major managed kubernetes versions: GKE vs AKS vs EKS 
*   Setup a 3 node cluster in GKE (or choose one of the other 2 based on the subscription availability)

### Deployments
*   Understanding the connection between deployments, replicasets and pods.
*   Deploying new versions of the votingapp using Rolling upgrades.

### Configurations and Secrets
*   Difference between configuration and secrets resources
*   Using configuration and secrets with pods.

### Stateful workloads
*   Understanding the differences between the deployments and the stateful resources in kubernetes.
*   Understanding Volumes and VolumeClaims.
*   Deploying rabbitmq and postgresql as an stateful resource.

### Authentication and Authorization: RBAC
*   Kubernetes security model (authentication and authorization) using RBAC. 

### Helm (the kubernetes package manager)
*   Basic helm commands for creating and installing helm charts.
*   Understanding the templates render engine
*   Create a chart for the votingapp.
*   Deploy infrastructure with helm charts: rabbitmq and postgresql charts
