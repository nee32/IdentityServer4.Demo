using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _03.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        public AccountController(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 登录页面
        /// </summary>
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// 登录post回发处理
        /// </summary>
        [HttpPost]
        public IActionResult Login(string userName, string password, string returnUrl = null)
        {
            //ViewData["returnUrl"] = returnUrl;
            //Admin user = await _adminService.GetByStr(userName, password);
            //if (user != null)
            //{
            //    AuthenticationProperties props = new AuthenticationProperties
            //    {
            //        IsPersistent = true,
            //        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(1))
            //    };
            //    await HttpContext.SignInAsync(user.Id.ToString(), user.UserName, props);
            //    if (returnUrl != null)
            //    {
            //        return Redirect(returnUrl);
            //    }

            //    return View();
            //}
            //else
            //{
            //    return View();
            //}
            return Redirect(returnUrl);
            //return View();
        }
    }
}