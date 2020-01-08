﻿using Blog.Persistence.Configuration;
using Blog.Persistence.Repositories;
using Guyabano.DepenencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Service.Configuration
{
    public class ServiceModule : IIocModule
    {
        public void AddServices(IFunctionsHostBuilder builder)
        {
            builder.AddModule(new PersistenceModule("blogAppV1"));

            builder.Services.AddSingleton<IAuthenticationService>(p => new AuthenticationService(p.GetService<ILoginRepository>()));
            builder.Services.AddSingleton<IEmailService>(p => new EmailService(p.GetService<IEmailRepository>()));


            builder.Services.AddSingleton<IUserService>(p =>
                new UserService(
                    p.GetService<IDbSetup>(),
                    p.GetService<IUserRepository>(),
                    p.GetService<IUserIdRepository>(),
                    p.GetService<IUserNameRepository>(),
                    p.GetService<IEmailService>(),
                    p.GetService<IAuthenticationService>()));
        }
    }
}