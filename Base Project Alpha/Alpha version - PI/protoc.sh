#!/bin/sh
./protoc/bin/protoc --proto_path=./Assets --csharp_out=./Assets --csharp_opt=file_extension=.g.cs,base_namespace=Protocol ./Assets/messages.proto