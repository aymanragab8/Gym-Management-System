using GymSystem.API.Middlewares;
using GymSystem.Application;
using GymSystem.Infrastructure;
using GymSystem.Infrastructure.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Models.Data;

namespace Gym_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            builder.Services.AddControllers();

            builder.Services.AddScoped<ApplicationDbContext>();



            builder.Services.AddInfrastructureDependencies(builder.Configuration)
                            .AddApplicationDependencies();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseExceptionHandler();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                IdentitySeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
