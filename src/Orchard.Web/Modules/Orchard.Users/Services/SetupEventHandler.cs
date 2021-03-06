﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Orchard.Events;
using Orchard.Users.Models;

namespace Orchard.Users.Services
{
    public interface ISetupEventHandler : IEventHandler
    {
        Task Setup(string userName, string email, string password);
    }

    /// <summary>
    /// During setup, creates the admin user account.
    /// </summary>
    public class SetupEventHandler : ISetupEventHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public SetupEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Setup(string userName, string email, string password)
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<User>>();
            var superUser = new User
            {
                UserName = userName,
                Email = email,
                RoleNames = { "Administrator" }
            };

            return userManager.CreateAsync(superUser, password);

        }
    }
}
