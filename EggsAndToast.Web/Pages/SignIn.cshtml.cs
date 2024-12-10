using EggsAndToast.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EggsAndToast.Web.Pages;

[AllowAnonymous]
public class SignInModel(SignInManager<User> signInManager) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? ReturnUrl { get; set; }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {

        return Page();
    }
}
