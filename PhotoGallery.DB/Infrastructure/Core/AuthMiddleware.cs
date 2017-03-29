﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using PhotoGallery.DB.Infrastructure.Repositories.Abstract;
using PhotoGallery.DB.Infrastructure.Services.Abstract;
using PhotoGallery.DB.Model;

namespace PhotoGallery.DB.Infrastructure.Core
{
    public class AuthMiddleware
    {
        //private readonly RequestDelegate _next;
        private readonly ILoggingRepository _loggingRepository;
        private readonly IMembershipService _membershipService;

        //public AuthMiddleware(RequestDelegate next,
        //    IMembershipService membershipService,
        //    ILoggingRepository loggingRepository)
        //{
        //    _next = next;
        //    _membershipService = membershipService;
        //    _loggingRepository = loggingRepository;
        //}

        //public async Task Invoke(HttpContext context)
        //{
        //    var request = context.Request;

        //    try
        //    {
        //        if (!context.User.Identities.Any(identity => identity.IsAuthenticated))
        //        {
        //            Claim _claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, "chsakell");
        //            await context.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
        //                new ClaimsPrincipal(new ClaimsIdentity(new[] { _claim }, CookieAuthenticationDefaults.AuthenticationScheme)));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
        //        _loggingRepository.Commit();
        //    }

        //    await _next.Invoke(context);
        //}
    }
}
