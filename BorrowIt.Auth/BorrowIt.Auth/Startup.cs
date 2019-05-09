using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Auth.Application.Queries;
using BorrowIt.Auth.Application.Services;
using BorrowIt.Auth.Domain.Users.Factories;
using BorrowIt.Auth.Infrastructure.Mappings.Users;
using BorrowIt.Auth.Infrastructure.Repositories.Users;
using BorrowIt.Common.Infrastructure.Abstraction;
using BorrowIt.Common.Infrastructure.Implementations;
using BorrowIt.Common.Infrastructure.IoC;
using BorrowIt.Common.Mongo.IoC;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.Common.Rabbit.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace BorrowIt.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddSwaggerGen(ctx =>
            {
                
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                ctx.SwaggerDoc("v1", new Info() {Title = "BorrowIt.Auth", Version = "v1"});
                
                ctx.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                
                ctx.AddSecurityRequirement(security);
            });
            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RawRabbitModule(Configuration));
            builder.RegisterModule(new MongoDbModule(Configuration, "mongoDb"));
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRepositories<IUsersRepository>()
                .AddGenericRepository(typeof(GenericMongoRepository<,>));
            builder.AddServices<IUsersService>();
            builder.RegisterAssemblyTypes(typeof(CreateUserCommand).Assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>));
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().InstancePerLifetimeScope();
            builder.Register(ctx =>
            {
                var assemblies = new List<Assembly> {typeof(UsersMappingProfile).Assembly, typeof(CreateUserCommand).Assembly};
                
                var mapperConfig = new MapperConfiguration(x =>
                    x.AddProfiles(assemblies));

                return mapperConfig.CreateMapper();
            }).As<IMapper>().InstancePerLifetimeScope();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(SignInQuery).Assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>));
            builder.Populate(services);
            builder.RegisterAssemblyTypes(typeof(IUserFactory).Assembly).Where(x => x.Name.EndsWith("Factory"))
                .AsImplementedInterfaces();
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}