namespace FinalProject.Migrations
{
    using Lexicon.CSharp.InfoGenerator;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FinalProject.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            List<string> photos = new List<string>() {
         "~/Content/Players/فايدنفيلر.jpg",
"~/Content/Players/weigl_bvb_getty_485021126.jpg",
"~/Content/Players/Thomas-Muller-Photos.jpg",
"~/Content/Players/mhc1Az75.jpg",
"~/Content/Players/mats-hummels-bayern-munich_pbn6zon6nus81hkjfn0u3mp10.jpg",
"~/Content/Players/manuel-neuer-cropped_1eaym58htd5y91vwvjdf4fe037.jpg",
"~/Content/Players/lukas-podolski-germany-armenia_3154450.jpg",
"~/Content/Players/kahn.jpg",
"~/Content/Players/images.jpg",
"~/Content/Players/hi-res-7b4834524f967daffbb49c3382d4c5f9_crop_north.jpg",
"~/Content/Players/gonzalocastro-cropped_14rb2mcyqyzgx1sqfjg9qi9i66.jpg",
"~/Content/Players/fcbhippo-header.png",
"~/Content/Players/DEEF15C5E1FF426BAED912A0C29E4922.jpg",
"~/Content/Players/Arjen+Robben+Manchester+City+FC+v+FC+Bayern+Eipqdimrsqql.jpg",
"~/Content/Players/84G_Schmelzer5_bvbnachrichtenbild_regular.jpg",
"~/Content/Players/38617FAD00000578-0-image-a-4_1473931837359.jpg",
"~/Content/Players/370x257xnuri-sahin-borussia-dortmund_9noraukbc4n81gtsgzu5ii9bi.jpg.pagespeed.ic.pEgF490fdt.jpg",
"~/Content/Players/3354302_heroa.jpg",
"~/Content/Players/3236412_heroa.jpg",
"~/Content/Players/3000.jpg",
"~/Content/Players/1730335-36604926-2560-1440.jpg",
"~/Content/Players/110515-Soccer-Borussia-Dortmund-Marco-Reus-PI-SW.vresize.1200.675.high.4.jpg"
            };


            InfoGenerator rnd = new InfoGenerator();

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "Player"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Player" };

                manager.Create(role);
            }


            if (!context.Roles.Any(r => r.Name == "Fan"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Fan" };

                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "rami.su93@hotmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "rami.su93@hotmail.com", Email = "rami.su93@hotmail.com", Birthdate = rnd.NextDate(), Fullname = "Rami Sulebi" };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "firas.wira@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "firas.wira@gmail.com", Email = "firas.wira@gmail.com", Birthdate = rnd.NextDate(), Fullname = "Firas Wira" };

                manager.Create(user, "1234567");
                manager.AddToRole(user.Id, "Admin");
            }

           



            for (int j = 1; j < 23; j++)
            {
                string pos;

                if (j < 4)
                {
                    pos = "Goal Keeper";
                }
                else if (j >= 4 && j < 9)
                {
                    pos = "Middle Field";
                }
                else if (j >= 9 && j < 15)
                {
                    pos = "Attack";
                }
                else
                {
                    pos = "Defence";
                }

                string fName = rnd.NextFirstName();
                string lName = rnd.NextLastName();

                if (!context.Users.Any(u => u.UserName == (fName + "." + lName + "@gmail.com")))
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var manager = new UserManager<ApplicationUser>(store);
                    var user = new ApplicationUser
                    {
                        UserName = (fName + "." + lName + "@gmail.com"),
                        Email = (fName + "." + lName + "@gmail.com"),
                        Fullname = fName + " " + lName,
                        Birthdate = rnd.NextDate(),
                        Address = rnd.NextStreet(),
                        Position = pos,
                        PhotoUrl = photos[j - 1]
                    };

                    manager.Create(user, "7654321");
                    manager.AddToRole(user.Id, "Player");

                }
            }

            for (int i = 1; i < 31; i++)
            {
                bool eventOrNews = false;
                if (i % 2 == 0)
                    eventOrNews = true;

                context.News.AddOrUpdate(n => n.Subject,
              new News
              {
                  Subject = "Something to Post" + i,
                  Body = "Lorem ipsum dolor sit amet, cum justo quodsi te, ea illud affert consequuntur sea. Has cu primis praesent, ex nec quot indoctum vituperatoribus. An iracundia definiebas dissentias eum. Per id duis laudem. Eam tation sensibus pericula an, nam et denique salutandi qualisque. Omnis paulo doctus eu vel, vivendum scripserit vim ex.",
                  DatePosted = rnd.NextDate(),
                  EventDate = rnd.NextDate(),
                  EventOrNews = eventOrNews
              });
            }
        }
    }
}

