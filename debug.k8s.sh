namespace=production
pod=$(kubectl get pods \
--selector='app=votingapp' \
-o jsonpath='{.items[0].metadata.name}' \
-n $namespace)

kubectl exec $pod -i \
--namespace $namespace \
-- sh -c \
"apt-get update && 
apt-get install -y unzip && 
curl -sSL https://aka.ms/getvsdbgsh -o '/root/getvsdbg.sh' &&
bash /root/getvsdbg.sh -v latest -l /vsdbg"

prid=$(kubectl exec $pod -i --namespace $namespace -- pidof -s dotnet)
echo 'Pod '$pod' dotnet running at '$prid

DEBUG_POD_NAME=$pod code .
