using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MrClean.Core.Application;
using MrClean.Infrastructure;
using MrClean.Presentation.WebApi.Common.Errors;
//using MrClean.Presentation.WebApi.Filters;
//using MrClean.Presentation.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

    // Add Project base dependencies:
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());    // Apply ErrorHandlingFilterAttribute globally instead of one by one thru controller

    builder.Services.AddControllers();
    builder.Services.AddSingleton<ProblemDetailsFactory, MrCleanProblemDetailsFactory>();

    //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    //builder.Services.AddSwaggerGen(c => {
    //    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    //    c.IgnoreObsoleteActions();
    //    c.IgnoreObsoleteProperties();
    //    c.CustomSchemaIds(type => type.FullName);
    //});
}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    //app.UseExceptionHandler(a => a.Run(async context =>
    //{
    //    var error = context.Features.Get<IExceptionHandlerFeature>().Error;
    //    var problem = new ProblemDetails { Title = "Critical Error" };
    //    if (error != null)
    //    {
    //        //if (env.IsDevelopment())
    //        //{
    //        //    problem.Title = error.Message;
    //        //    problem.Detail = error.StackTrace;
    //        //}
    //        //else
    //            problem.Detail = error.Message;
    //    }
    //    await context.Response.WriteAsJsonAsync(problem);
    //}));


    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");  // Error, couldn't start Swagger!
    //app.Map("/error", (HttpContext httpContent) =>
    //{
    //    Exception? exception = httpContent.Features.Get<IExceptionHandlerFeature>()?.Error;
    //    return Results.Problem();
    //});

    app.UseHttpsRedirection();
    //app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
