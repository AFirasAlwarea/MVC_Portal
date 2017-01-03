using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Fullname { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Position { get; set; }
        public string PhotoUrl { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<RecievedMessages> RecievMessages { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<FinalProject.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.News> News { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.Post> Posts { get; set; }

        public System.Data.Entity.DbSet<FinalProject.Models.RecievedMessages> RecievedMessages { get; set; }
    }
}