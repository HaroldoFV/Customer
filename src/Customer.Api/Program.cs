using Customer.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConnections(builder.Configuration)
    .AddUseCases()
    .AddHttpContextAccessor()
    .AddAndConfigureControllers()
    .AddCors(p =>
        p.AddPolicy("CORS", c
            =>
        {
            c.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        })
    );


var app = builder.Build();

app.UseDocumentation();
app.UseRequestResponseLogging();
app.UseCors("CORS");
app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/healthchecks-ui");
app.MapControllers();

app.Run();