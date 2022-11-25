using inzynierka.Data.Roles;
using inzynierka.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pracainz.Data
{
    public class DbData
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                /*
                if (!context.Nibyusers.Any())
                {
                    context.Nibyusers.AddRange(new List<Nibyuser>()
                    {
                        new Nibyuser()
                        {
                            Name = "User1",
                            ImageURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",

                        },
                        new Nibyuser()
                        {
                            Name = "User2",
                            ImageURL = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",

                        },
                  });
                    context.SaveChanges();
                }
                */
                /*
            if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Name = "Drama",


                        },
                        new Category()
                        {
                            Name = "Comedy",


                        },
                  });
                    context.SaveChanges();
                }
                if (!context.MovieTypes.Any())
                {
                    context.MovieTypes.AddRange(new List<MovieType>()
                    {
                        new MovieType()
                        {
                            Name = "series",


                        },
                        new MovieType()
                        {
                            Name = "film",


                        },
                  });
                    context.SaveChanges();
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "FirstMovie",
                            Description = "This is description",
                            Premier = DateTime.Now.AddDays(-10),
                          //  StartDate = DateTime.Now.AddDays(-10),
                           // MovieCategory = MovieCategory.Action,
                       //     NibyuserId = 1,
                            MovieTypeId = 2,
                            ImageURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",

                        },
                        new Movie()
                        {
                             Name = "FirstSeries",
                            Description = "This is description",
                            Premier = DateTime.Now.AddDays(-10),
                         //   StartDate = DateTime.Now.AddDays(-12),
                          //  MovieCategory = MovieCategory.Action,
                        //    NibyuserId = 2,
                            MovieTypeId = 1,
                            ImageURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",


                        },
                         new Movie()
                        {
                            Name = "SecondMovie",
                            Description = "This is description",
                            Premier = DateTime.Now.AddDays(-10),
                           // StartDate = DateTime.Now.AddDays(-10),
                          //  MovieCategory = MovieCategory.Action,
                        //    NibyuserId = 1,
                            MovieTypeId = 1,
                            ImageURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",

                        },
                          new Movie()
                        {
                            Name = "Third",
                            Description = "This is description",
                            Premier = DateTime.Now.AddDays(-10),
                         //   StartDate = DateTime.Now.AddDays(-15),
                       //     MovieCategory = MovieCategory.Action,
                    //        NibyuserId = 1,
                            MovieTypeId = 1,
                            ImageURL = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",

                        },
                  }); ;
                    context.SaveChanges();
                }

                if (!context.Movie_Categories.Any())
                {
                    context.Movie_Categories.AddRange(new List<Movie_Category>()
                    {
                        new Movie_Category()
                        {
                            CategoryId = 1,
                            MovieId = 1,


                        },
                        new Movie_Category()
                        {
                           CategoryId = 2,
                            MovieId = 1,


                        },
                          new Movie_Category()
                        {
                            CategoryId = 1,
                            MovieId = 2,


                        },
                        new Movie_Category()
                        {
                           CategoryId = 2,
                            MovieId = 3,


                        },
                        new Movie_Category()
                        {
                           CategoryId = 1,
                            MovieId = 4,


                        },
                        new Movie_Category()
                        {
                           CategoryId = 2,
                            MovieId = 4,


                        },
                  });
                    context.SaveChanges();
                } */
            }
        }

        public static async Task SeedUsersWithRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));





                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<WebUser>>();

                string adminUserEmail = "admin@mail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null)
                {
                    var newAdmin = new WebUser()
                    {
                        FullName = "Admin Admin",
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdmin, "Admin@1234");
                    await userManager.AddToRoleAsync(newAdmin, UserRoles.Admin);
                }


                string appUserEmail = "user@mail.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);

                if (appUser == null)
                {
                    var newAppUser = new WebUser()
                    {
                        FullName = "User User",
                        UserName = "User",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

            }
        }

    }
}
