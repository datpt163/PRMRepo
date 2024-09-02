﻿using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Query;
using Capstone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.QueryHandle
{
    public class ForgotPasswordQueryHandle : IRequestHandler<ForgotPasswordQuery, ResponseMediator>
    {
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public ForgotPasswordQueryHandle(IEmailService emailService, IJwtService jwtService, UserManager<User> userManager)
        {
            _emailService = emailService;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<ResponseMediator> Handle(ForgotPasswordQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
                return new ResponseMediator("Email is empty", null);

            if (!request.Email.Contains("@") || !request.Email.Contains("."))
                return new ResponseMediator("Invalid email format.", null);

            var ac = await _userManager.FindByEmailAsync(request.Email);

            if (ac == null)
                return new ResponseMediator("It seems like you are trying to reset your password, please follow the instruction below", null);

            var code = await _jwtService.GenerateJwtTokenAsync(ac, DateTime.Now.AddMinutes(3), "da-32-character-ultra-secure-and-ultra-long-secret");

            string body = "<!DOCTYPE html>\r\n<html lang=\"vi\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Reset Password</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            display: flex;\r\n            justify-content: center;\r\n            align-items: center;\r\n            height: 100vh;\r\n            margin: 0;\r\n            background-color: #f5f5f5;\r\n        }\r\n        .container {\r\n            text-align: center;\r\n            border: 2px solid black;\r\n            border-radius: 10px;\r\n            padding: 20px;\r\n            background-color: #ffffff;\r\n        }\r\n        .container h1 {\r\n            margin-top: 0;\r\n            font-size: 24px;\r\n        }\r\n        .container p {\r\n            font-size: 16px;\r\n            margin: 10px 0;\r\n        }\r\n        .reset-button {\r\n            display: inline-block;\r\n            padding: 10px 20px;\r\n            font-size: 16px;\r\n            color: #ffffff;\r\n            background-color: #007bff;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            border: 1px solid #007bff;\r\n        }\r\n        .reset-button:hover {\r\n            background-color: #0056b3;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h1>Reset your password</h1>\r\n        <p>We heard that you lost your password. Sorry about that!</p>\r\n        <p>But don’t worry! You can use the following button to reset your password:</p>\r\n        <a style=\"color: white;\" href=\"https://tradeplatform.netlify.app/renew-password?code=" + code + " \" class=\"reset-button\">Reset your password</a> \r  <a href=\"http://localhost:3600/renew-password?code=" + code + " \" class=\"reset-button\">Reset your password</a>    \r\n    </div>\r\n</body>\r\n</html>\r\n";
            (bool isSuccess, string errorMessage) = await _emailService.SendEmailAsync(request.Email, "Forgot Password", body);

            if (!isSuccess)
                return new ResponseMediator(errorMessage, null);
            return new ResponseMediator("", null);
        }
    }
}