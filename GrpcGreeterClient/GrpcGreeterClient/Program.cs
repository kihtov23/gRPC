using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeterClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7111/");
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                     new HelloRequest { Name = "GreeterClient" });
Console.WriteLine("Greeting: " + reply.Message);


var serverStreamReply = client.StreamingFromServer(new HelloRequest { Name = "GreeterClient" });

Console.WriteLine("Replies from server streaming");
await foreach (var response in serverStreamReply.ResponseStream.ReadAllAsync())
{
    Console.WriteLine("Greeting: " + response);
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();