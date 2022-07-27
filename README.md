# gRPC-Demo

## Prerequisites
* dotnet 6 installed
* grpcurl installed (or any other tool to make grpc "requests")

## Run 
* `dotnet run`

## Endpoints
* `grpcurl -plaintext -d '{"name": "noah"}' localhost:5226 Greeter/UnaryCall`

## Additional resources
* [grpcurl](https://github.com/fullstorydev/grpcurl)