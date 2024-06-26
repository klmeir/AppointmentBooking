
using AppointmentBooking.Api.ApiHandlers;
using AppointmentBooking.Api.Converters;
using AppointmentBooking.Api.Filters;
using AppointmentBooking.Api.Middleware;
using AppointmentBooking.Infrastructure.DataSource;
using AppointmentBooking.Infrastructure.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

namespace AppointmentBooking.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var config = builder.Configuration;

                builder.Host.UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.WriteTo.Console();
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
                });

                builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);

                builder.Services.AddDbContext<DataContext>(opts =>
                {
                    opts.UseSqlServer(config.GetConnectionString("db"));
                });            

                // Add services to the container.
                builder.Services.AddAuthorization();
                builder.Services.AddDomainServices();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                builder.Services.AddMediatR(Assembly.Load("AppointmentBooking.Application"), typeof(Program).Assembly);

                var dateFormat = "dd/MM/yyyy";
                builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.Converters.Add(new JsonDateTimeConverterWithFormat(dateFormat)));

                var app = builder.Build();

                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }            

                app.UseAuthorization();

                app.UseMiddleware<AppExceptionHandlerMiddleware>();

                app.MapGroup("/api/turns").MapTurn().AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

                app.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "server terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
