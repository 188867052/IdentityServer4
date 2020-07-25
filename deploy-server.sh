#!/bin/bash

sudo docker pull 542153354/identityserver4:v1.0 

containerId="`sudo docker ps | grep "8088->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

# imageId="`sudo docker images | grep "IdentityServer4          v1.0" | awk  '{print $3}'`"
# echo "imageId:$imageId"
# if [ -n "$imageId" ]
# then
# 	sudo docker rmi -f $imageId
# fi

sudo docker run -d -p 8088:80 --restart=always 542153354/identityserver4:v1.0 /bin/sh 

exit
