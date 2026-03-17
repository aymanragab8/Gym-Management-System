using FluentValidation;
using FluentValidation.AspNetCore;
using GymSystem.Application.Interfaces;
using GymSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymSystem.Application
{
    public static class ModuleApplicationDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IExerciseService, ExerciseService>();
            services.AddScoped<IWorkoutPlanService, WorkoutPlanService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ModuleApplicationDependencies).Assembly));

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(typeof(ModuleApplicationDependencies).Assembly);
            return services;
        }
    }
}
