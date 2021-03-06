﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Services.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        public UserManager<ApplicationUser> UserManager { get; private set; }

        private string GenerateActivationCode(int numberOfChars = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, numberOfChars)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            return result;
        }

        private void SendEmail(ApplicationUser user)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("3D.Cartesius@gmail.com", "I4PRJGruppe4"),
                EnableSsl = true
            };
            string body = "Hello " + user.FName +
                "\n\nThanks for your reg. on 3D-Printer. To succesfully " +
                "activate your account copy the activation code below and " +
                "paste it in the application. \n\nApplication code: " +
                user.ActivationCode + "\n\nReplies to this email will not be replied.";
            client.Send(user.UserName, user.UserName, "Activation code for your 3D Printer account", body);
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string message = null)
        {
            if (message != null)
                ViewBag.Message = message;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    if (user.Activated == 1)
                    {
                        await SignInAsync(user, model.RememberMe);
                        
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        if (user.Activated == 2)
                        {
                            return RedirectToAction("Login", new {message = "User has been deleted. Contact an admin."});
                        }
                        await SignInAsync(user, model.RememberMe);
                        return RedirectToAction("Activation");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    FName = model.FName,
                    LName = model.LName,
                    Phone = model.Phone,
                    Activated = 0,
                    AdminRights = 0,
                    ActivationCode = GenerateActivationCode()
                    
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    SendEmail(user);
                        
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Activation", "Account");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        
        // GET: /Account/Activation
        public ActionResult Activation(MessageId? message)
        {
            ViewBag.ActivationStatus =
                message == MessageId.ActivationWrong ? "Activation is wrong, try again!"
                 : "";
                ViewBag.ResendStatus = message == MessageId.ActivationResendSuccess ? "New Activation code has been resend."
            : "";
       
            return View();
        }

        //
        // POST: /Account/Activation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Activation(ActivationViewModel model)
        {
            if (ModelState.IsValid)
            {
                    var user = await UserManager.FindByNameAsync(User.Identity.GetUserName());
                    if (user.ActivationCode == model.ActivationCode)
                    {
                        user.Activated = 1;
                        var result = await UserManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            result = await UserManager.AddToRoleAsync(user.Id, "User");
                            if (result.Succeeded)
                            {
                                LogOff();
                                await SignInAsync(user, false);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                AddErrors(result);
                            }
                        }
                        else
                        {
                            AddErrors(result);
                        }

                    }
                    else
                    {
                        return RedirectToAction("Activation", new {message = MessageId.ActivationWrong});
                    }
                
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Activation");
        }

        public async Task<ActionResult> ResendActivationCode()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            user.ActivationCode = GenerateActivationCode();

            await UserManager.UpdateAsync(user);

            SendEmail(user);


            return RedirectToAction("Activation", new   { Message = MessageId.ActivationResendSuccess} );
        }
        

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            MessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = MessageId.RemoveLoginSuccess;
            }
            else
            {
                message = MessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        
        // GET: /Account/Manage

        /// <summary>
        /// GET: Funktion der opretter messeges til succes eller error i profil opdatering.
        /// Bruger data hentes fra database via Usermanager der er nedarvet fra IdentityUser
        /// Der oprettes en instans af ManageUserViewModel for at sende kun nødvendige data til View
        /// </summary>
        /// <param name="message">Besked modtaget fra Enum MessageId</param>
        /// <returns>Manage View samt ManageUserViewModel</returns>
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "Activation")]
        public ActionResult Manage(MessageId? message)
        {
            ViewBag.StatusMessage =
                message == MessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == MessageId.SetPasswordSuccess ? "Your password has been set."
                : message == MessageId.EditProfileSuccess ? "Your profile has been updated."
                : message == MessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
                ViewBag.Failed = message == MessageId.Error ? "An error has occurred." 
                : "";
            
                ViewBag.HasLocalPassword = HasPassword();
                ViewBag.ReturnUrl = Url.Action("Manage");

             var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
             var currentUser = manager.FindById(User.Identity.GetUserId());
             ManageUserViewModel model = new ManageUserViewModel();
                model.FirstName = currentUser.FName;
                model.SurName = currentUser.LName;
                model.PhoneNumber = currentUser.Phone;

              return View(model);
        }

        //
        // POST: /Account/Manage

        /// <summary>
        /// POST funktion til opdatering af profil
        /// Oprettelse af to IdentityResult for at tjekke to new password er enes
        /// Ny indtastet data der modtages som input gemmes i database  
        /// </summary>
        /// <param name="model">ViewModel modtages med ny indtastet data </param>
        /// <returns>Manage View samt ManageUserViewModel</returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "Activation")]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            
            bool hasPassword = HasPassword();
            bool hasFName = HasFName();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            IdentityResult result = new IdentityResult();
            IdentityResult result2= new IdentityResult();

            if (hasFName)
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindAsync(User.Identity.GetUserName(), model.OldPassword);
                    if (user == null)
                        return RedirectToAction("Manage" , new { Message = MessageId.Error} );
                    user.FName = model.FirstName;
                    user.LName = model.SurName;
                    user.Phone = model.PhoneNumber;   
        
                    result = await UserManager.UpdateAsync(user);
                }             

            if (model.NewPassword != null)
            {
                if (hasPassword)
                {

                    if (ModelState.IsValid)
                    {
                        result2 =
                            await
                                UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                                    model.NewPassword);
                        
                    }
                }
                
               
            }

            if (result.Succeeded || result2.Succeeded)
            {
                if (result.Succeeded)
                    AddErrors(result2);

                if (result2.Succeeded)
                    AddErrors(result);

                return RedirectToAction("Manage", new { Message = MessageId.EditProfileSuccess });
            }
            else
            {
                AddErrors(result);
                AddErrors(result2);
            }
            
          }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = MessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = MessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }
        
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin", NotifyUrl = "../Home/Index")]
        public async Task<ActionResult> Index()
        {

                    var model = new IndexUsersViewModel();
                    model.UserTuples = new List<Tuple<ApplicationUser, string>>();
                    
                    var banned = new List<Tuple<ApplicationUser, string>>();
                    var active = new List<Tuple<ApplicationUser, string>>();
                    var inactive = new List<Tuple<ApplicationUser, string>>();

                    string status = "";
         		   List<ApplicationUser> tmpUsers = await db.Users.ToListAsync();
                    foreach (ApplicationUser u in tmpUsers)
                    {
                        status = ActivationToString(u.Activated);
                        var tmpTuple = new Tuple<ApplicationUser, string>(u, status);
                        switch (u.Activated)
                        {
                            case 0:
                                inactive.Add(new Tuple<ApplicationUser, string>(u, status));
                                break;
                            case 1:
                                active.Add(new Tuple<ApplicationUser, string>(u, status));
                                break;
                            case 2:
                                banned.Add(new Tuple<ApplicationUser, string>(u, status));
                                break;
                        }

                    }
                    model.UserTuples.AddRange(active);
                    model.UserTuples.AddRange(banned);
                    model.UserTuples.AddRange(inactive);
                    return View(model);

           
        }

        // GET: /account/Details/5
         [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            var model = new DetailsUserViewModel();
            var activeStatus = "";
            model.Email = user.UserName;
            model.PhoneNumber = user.Phone;
            model.UserRole = db.Users.Find(id).Roles.Last().Role.Name;
            model.FName = user.FName;
            model.LName = user.LName;
            model.Id = user.Id;
            model.Activated = ActivationToString(user.Activated);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Account/Edit/5
         [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string id)
        {

            EditUsersViewModel model = new EditUsersViewModel();
            model.UserRole = db.Users.Find(id).Roles.Last().Role.Name;
            model.UserId = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: /Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]

        public async Task<ActionResult> Edit(EditUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> rolesList = (List<string>)await UserManager.GetRolesAsync(model.UserId);


                await UserManager.RemoveFromRoleAsync(model.UserId, rolesList.Last());
                var result = await UserManager.AddToRoleAsync(model.UserId, model.UserRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");   
                }
                else AddErrors(result);
            }
           
            return View(model);
        }

        // GET: /Account/Delete/5
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            user.Activated = 2;
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AddErrors(result);
                return RedirectToAction("Delete");
            }

        }
        // GET: /Account/Reinsert/5
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Reinsert(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /Account/Reinsert/5
        [HttpPost, ActionName("Reinsert")]
        [ValidateAntiForgeryToken]
        [NewAuthorize(Roles = "Admin, User", NotifyUrl = "../Account/Activation")]
        [NewAuthorize(Roles = "Admin")]
        public async Task<ActionResult> ReinsertConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            user.Activated = 1;
            var result = await UserManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                AddErrors(result);
                return RedirectToAction("Reinsert");
            }

        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasFName()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.FName != null;
            }
            return false;
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
                
            }
            return false;
        }

        /// <summary>
        /// De forskellige Messeges der for validering 
        /// </summary>
        public enum MessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            EditProfileSuccess,
            RemoveLoginSuccess,
            Error,
            ActivationWrong,
            ActivationResendSuccess
        }
        public string ActivationToString(int activated)
        {
            string activeStatus = "-";
            switch (activated)
            {
                case 0:
                    activeStatus = "Inactive";
                    break;
                case 1:
                    activeStatus = "Activated";
                    break;
                case 2:
                    activeStatus = "Banned";
                    break;
            }
            return activeStatus;
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
    #region NewAuthorize

    /// <summary>
    /// Selvoprettet Authorize der nedarver AuthorizeAttribute
    /// Bruges til at linke til "../Home/Index" 
    /// </summary>
    public class NewAuthorize : AuthorizeAttribute
    {
        // Set default Unauthorized Page Url here
        private string _notifyUrl = "../Home/Index";

        public string NotifyUrl
        {
            get { return _notifyUrl; }
            set { _notifyUrl = value; }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (AuthorizeCore(filterContext.HttpContext))
            {
                HttpCachePolicyBase cachePolicy =
                    filterContext.HttpContext.Response.Cache;
                cachePolicy.SetProxyMaxAge(new TimeSpan(0));
                //cachePolicy.AddValidationCallback(CacheValidateHandler, null);
            }

            // This code added to support custom Unauthorized pages.
            else if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (NotifyUrl != null)
                    filterContext.Result = new RedirectResult(NotifyUrl);
                else
                    // Redirect to Login page.
                    HandleUnauthorizedRequest(filterContext);
            }
            // End of additional code
            else
            {
                // Redirect to Login page.
                HandleUnauthorizedRequest(filterContext);
            }
        }
    }
#endregion
}