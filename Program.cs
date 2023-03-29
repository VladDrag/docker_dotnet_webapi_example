//Azure Active Directory Requirements
// using Microsoft.AspNetCore.Authentication.AzureAD.UI;

//Azure Key Vault Requirements
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
// using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Azure AD authentication
// builder.Services.AddAuthentication();


var app = builder.Build();
// app.UseAuthentication();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/secret", async () => 
{

    //configure secret defaults
    const string secretName = "MySecret";
    var keyVaultName = "vlad-id-dev-kv";
    var kvUri = $"https://{keyVaultName}.vault.azure.net";

    //set client to connect to the Azure Service
    var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());


    var secret = await client.GetSecretAsync(secretName);
    Console.WriteLine($"Your secret is '{secret.Value.Value}'.");


    return secret;
})
.WithName("GetAzureSecret")
.WithOpenApi();

app.Run();


