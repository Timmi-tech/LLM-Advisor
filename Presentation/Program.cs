using Infrastructure.Extensions;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities.Models;
using Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using Presentation.ActionFilters;
using Domain.Entities.ConfigurationsModels;
using Application.Services;
using Application.Services.Contracts;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.ConfigurePostGressContext(builder.Configuration);
builder.Services.ConfigureProgramRepository();
builder.Services.ConfigureRecommendationService();
builder.Services.ConfigureHttpClient();
builder.Services.ConfigureConversationService();
builder.Services.AddControllers();
builder.Services.ConfigureAuthenticationService();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Host.ConfigureSerilogService();
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.ConfigureFeedbackService();
builder.Services.ConfigureFeedbackRepository();
builder.Services.ConfigureProgramService();
builder.Services.ConfigureUserService();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(); 
builder.Services.Configure<GeminiSettings>(
    builder.Configuration.GetSection("GeminiSettings"));
builder.Services.AddHttpClient<GeminiApiClient>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.ConfigureCors();


var app = builder.Build();

// Seed JSON data
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<RepositoryContext>();

//     string jsonFilePath = "Program.json";

//     if (File.Exists(jsonFilePath))
//     {
//         string json = File.ReadAllText(jsonFilePath);
//         var response = JsonSerializer.Deserialize<PostgraduateProgramsResponse>(json);

//         if (response != null)
//         {
//             foreach (var program in response.PostgraduatePrograms)
//             {
//                 // Avoid duplicates by checking Id
//                 var exists = context.PostgraduatePrograms.Any(p => p.Id == program.Id);
//                 if (!exists)
//                 {
//                     context.PostgraduatePrograms.Add(program);
//                 }
//             }
//             context.SaveChanges();
//             Console.WriteLine("Data inserted successfully!");
//         }
//         else
//         {
//             Console.WriteLine("Failed to deserialize JSON.");
//         }
//     }
//     else
//     {
//         Console.WriteLine("JSON file not found!");
//     }
// }


if (app.Environment.IsProduction())
{
    app.UseHsts();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LLM-Advisor v1"));
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.Run();