﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Yumify.API.Helper;
using Yumify.API.Helper.Mapping;
using Yumify.API.Middlewares;
using Yumify.Core.Entities.IdentityEntities;
using Yumify.Core.IRepository;
using Yumify.Core.IServices;
using Yumify.Repository.Data;
using Yumify.Repository.Data.DataSeeding;
using Yumify.Repository.IDentity;
using Yumify.Repository.IDentity.DataSeeding;
using Yumify.Repository.Repositories;

namespace Yumify.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var DefaultCs = builder.Configuration.GetConnectionString("DefaultCs");
            var IdentityCs = builder.Configuration.GetConnectionString("IdentityCs");

            builder.Services.AddControllers();
            //.ConfigureApiBehaviorOptions(option => { option.SuppressModelStateInvalidFilter = true; } );

            builder.Services.AddAutoMapper(typeof(Mapping));
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (options =>
                {
                    var errors = options.ModelState.Where(M => M.Value?.Errors.Count > 0)
                      .SelectMany(M => M.Value!.Errors)
                      .Select(M => M.ErrorMessage)
                      .ToList();

                    var Response = new GeneralResponseValidation(400, errors, "Bad Request");

                    return new BadRequestObjectResult(Response);
                });

            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //for lazy loading we use this .UseLazyLoadingProxies()
            builder.Services.AddDbContext<YumifyDbContext>(options => options.UseSqlServer(DefaultCs));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(ICart), typeof(CartRepository));
            builder.Services.AddScoped(typeof(IAuth), typeof(AuthenticationService));
            builder.Services.AddScoped<IConnectionMultiplexer>
                ((serviceProvider) =>
                    {
                        return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis")!);
                    }
                );
            builder.Services.AddDbContext<IdentityYumifyDbContext>
                (options =>
                {
                    options.UseSqlServer(IdentityCs);
                });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityYumifyDbContext>();

            var app = builder.Build();

            var Scope = app.Services.CreateScope();   // هنا كريت سكوب فيه كل السيرفيس اللي حصلها ريجستر
            var service = Scope.ServiceProvider; // هنا علشان اقدر اطلب اوبجيكت من الموجودين في الاسكوب 
            var YumifyContext = service.GetRequiredService<YumifyDbContext>(); // كدة معايا اوبجيكت منه 
            var IdentityYumifyContext = service.GetRequiredService<IdentityYumifyDbContext>();

            var UserMangerIDentity=service.GetRequiredService<UserManager<ApplicationUser>>();    

            var loggerFact = service.GetRequiredService<ILoggerFactory>();

            try
            {
                await YumifyContext.Database.MigrateAsync();
                await DataSeed.SeedAsync(YumifyContext);

                await IdentityYumifyContext.Database.MigrateAsync();
                await YumifyIdentitySeeding.IdentitySeeding(UserMangerIDentity,IdentityYumifyContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFact.CreateLogger<Program>();
                logger.LogError(ex.Message, "an error occured during migrate");
            }
            finally
            {
                Scope.Dispose();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
