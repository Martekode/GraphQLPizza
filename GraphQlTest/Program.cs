using GraphQlTest.Schema.Mutations;
using GraphQlTest.Schema.Querries;
using GraphQlTest.Schema.Subscriptions;

var builder = WebApplication.CreateBuilder(args);


//services and config 
builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

// app building and USES
var app = builder.Build();
app.UseRouting();
app.UseWebSockets();

// endpoint
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.Run();
