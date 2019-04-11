using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using BorrowIt.Auth.Application.Services;
using BorrowIt.Auth.Infrastructure.Repositories.Users;
using BorrowIt.Common.Infrastructure.IoC;
using BorrowIt.Common.Mongo.IoC;
using BorrowIt.Common.Mongo.Repositories;
using BorrowIt.Common.Rabbit.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
            
            var builder = new ContainerBuilder();
            builder.RegisterModule(new RawRabbitModule(Configuration));
            builder.RegisterModule(new MongoDbModule(Configuration, "mongoDb"));
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.AddRepositories<IUsersRepository>()
                .AddGenericRepository(typeof(GenericMongoRepository<,>));
            builder.AddServices<IUsersService>();
            builder.Populate(services);
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}