using GraphQlTest.Data;
using GraphQlTest.Exeptions;
using GraphQlTest.Schema.Mutations;
using GraphQlTest.Schema.Querries;
using GraphQlTest.Schema.Subscriptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//services and config 
builder.Services
    .AddGraphQLServer()
    .AddInMemorySubscriptions()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>();

builder.Services.AddSingleton<IErrorFilter, NotFoundErrorFilter>();
builder.Services.AddPooledDbContextFactory<PizzaToppingsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

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
