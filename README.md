# gRPC-Demo

## Prerequisites
* dotnet 6 installed
* grpcurl installed (or any other tool to make grpc "requests")

## Run 

As there are different scenarios when using grpc, there are separate projects that can be run. 

### Unary 

Client sends request, server sends response

* `dotnet run --project Server`
* `grpcurl -plaintext -d '{"name": "noah"}' localhost:5226 Greeter/UnaryCall`

### Server Stream

Clients sends request, server streams responses

* `dotnet run --project Server`
* `dotnet run --project TempClient`

### Client Stream 

Client sends requests through stream, server responds

* `dotnet run --project Server`
* `dotnet run --project GreetingsClient`


## Additional resources
* [grpcurl](https://github.com/fullstorydev/grpcurl)
* https://github.com/ckadluba/GrpcDemos 
* https://github.com/stevejgordon/gRPC-Demos