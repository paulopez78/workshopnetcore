new=$1
old=$2

rolling_update() {
    kubectl run votingapp$new --generator=run-pod/v1 --image localhost:30400/votingapp:$new
    kubectl label pod votingapp$new run=votingapp --overwrite
    kubectl patch svc votingapp -p '{"spec":{"selector":{"run":"votingapp"}}}'

    sleep 20
    kubectl label pod votingapp$old run=votingapp$old --overwrite
}

replace(){
    kubectl set image pods/votingapp$old votingapp$old=localhost:30400/votingapp:$new
}

blue_green(){
    kubectl run votingapp$new --generator=run-pod/v1 --image localhost:30400/votingapp:$new
    k patch svc votingapp -p '{"spec":{"selector":{"run":"votingapp'$new'"}}}' 
}


arguments="$*"

if [[ $arguments == *"--replace"* ]]; then
    replace
fi

if [[ $arguments == *"--rolling-update"* ]]; then
    rolling_update
fi