#!/bin/sh
testapi() {
    appjson="Content-Type:application/json"
    url="http://"${1:-localhost:5000}"/api/voting"

    curl --silent --request POST --header $appjson --data "['c#','f#']" $url
    curl --silent --request PUT --header $appjson --data '"c#"' $url
    winner=$(curl --silent --request DELETE --header $appjson $url | jq -r '.winner')

    if [ "$winner" == "c#" ]
    then
        echo "Test passing! The winner is "$winner
        return 0
    else
        echo "Test failed!"
        return 1 
    fi
}

retry=0
maxRetries=3
retryInterval=2
until [ ${retry} -ge ${maxRetries} ]
do
	testapi $1 && break
	retry=$((retry+1))
	echo "Retrying ["${retry}/${maxRetries}"] in "${retryInterval}"(s)"
	sleep ${retryInterval}
done

if [ ${retry} -ge ${maxRetries} ]; then
  echo "Failed after ${maxRetries} attempts!"
  exit 1
fi
