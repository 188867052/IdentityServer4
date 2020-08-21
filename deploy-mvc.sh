#!/bin/bash

sudo docker pull 542153354/mvcclient:v1.0 

containerId="`sudo docker ps | grep "8089->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8089:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 
#-------------------------------------------------------------------------------
containerId="`sudo docker ps | grep "8090->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8090:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 
#-------------------------------------------------------------------------------
containerId="`sudo docker ps | grep "8091->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8091:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 
#-------------------------------------------------------------------------------
containerId="`sudo docker ps | grep "8092->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8092:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 
#-------------------------------------------------------------------------------
containerId="`sudo docker ps | grep "8093->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8093:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 
#-------------------------------------------------------------------------------
containerId="`sudo docker ps | grep "8094->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8094:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 
#-------------------------------------------------------------------------------
containerId="`sudo docker ps | grep "8095->80" | awk  '{print $1}'`"
echo "containerId:$containerId"
if [ -n "$containerId" ]
then
	sudo docker stop $containerId
	sudo docker rm $containerId
fi

sudo docker run -d -p 8095:80 --restart=always 542153354/mvcclient:v1.0 /bin/sh 

exit
