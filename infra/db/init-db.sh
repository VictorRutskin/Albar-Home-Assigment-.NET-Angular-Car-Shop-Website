#!/bin/bash

docker build . -t db
docker run --name db -p 1433:1433 -d db
