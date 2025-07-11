﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Learnava.DataAccess.RepositoryContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Learnava.web.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInstructorService _insService;
        private readonly IStudentService _studentService;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager,
            IInstructorService insService,
            IStudentService studentService)
        {
            _userManager = userManager;
            _insService = insService;
            _studentService = studentService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if(!result.Succeeded)
            {
                StatusMessage = "Error confirming your email.";
                return Page();
            }

            StatusMessage = "Thank you for confirming your email.";
            
            if(await _userManager.IsInRoleAsync(user, SD.Role_Instructor))
            {
                await _insService.Create(userId);
            }
            else if(await _userManager.IsInRoleAsync(user, SD.Role_Student))
            {
                await _studentService.Create(userId);
            }

            return Page();
        }
    }
}
