using API;
using API.ExceptionHandling;
using API.Mapping;
using API.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterMapsterConfiguration();
builder.Services.ConfigureVersioning();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    )
);
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    //c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API");
    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        c.RoutePrefix = "";
    }
});

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
