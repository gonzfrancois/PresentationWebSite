using System.Configuration;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PresentationWebSite.UI.WebMvc.Models;

[assembly: OwinStartupAttribute(typeof(PresentationWebSite.UI.WebMvc.Startup))]
namespace PresentationWebSite.UI.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Administrator Role and creating a default Administrator User     
            if (!roleManager.RoleExists("Administrator"))
            {

                // first we create Administrator rool    
                var role = new IdentityRole {Name = "Administrator"};
                roleManager.Create(role);

                //HACK If UserName and Email aren't the same, we could'nt log in. This is a issue from Identity 2.0. See http://stackoverflow.com/a/24252833/2961285
                var adminEmail = ConfigurationManager.AppSettings["AdministratorEmail"];
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var userPwd = ConfigurationManager.AppSettings["AdministratorBasePwd"]; 

                var chkUser = userManager.Create(user, userPwd);

                //Add default User to Role Administrator    
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrator");

                }
            }
        }
    }
}
