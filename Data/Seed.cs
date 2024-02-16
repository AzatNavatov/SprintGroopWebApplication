using Microsoft.AspNetCore.Identity;
using SprintGroopWebApplication.Data.Enums;
using SprintGroopWebApplication.Models;

namespace SprintGroopWebApplication.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "BishkekRun",
                            Image = "https://www.shutterstock.com/shutterstock/photos/1513909058/display_1500/stock-photo-athlete-running-track-with-number-on-the-start-in-a-stadium-evening-scene-d-rendering-1513909058.jpg",
                            
                            ClubCategory = ClubCategory.BigClub,
                            Address = new Address()
                            {
                                Street = "Turusbekova 22",
                                City = "Bishkek",
                                Country = "Kyrgyzstan"
                            }
                         },
                        new Club()
                        {
                            Title = "2chiDem",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            
                            ClubCategory = ClubCategory.SmallClub,
                            Address = new Address()
                            {
                                Street = "Sovetskaya 99",
                                City = "Bishkek",
                                Country = "Kyrgyzstan"
                            }
                        },
                        new Club()
                        {
                            Title = "OshSprinters",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",

                            ClubCategory = ClubCategory.MediumClub,
                            Address = new Address()
                            {
                                Street = "Isanova 35",
                                City = "Osh",
                                Country = "Kyrgyzstan"
                            }
                        },
                        new Club()
                        {
                            Title = "Er-RiyadRun",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",

                            ClubCategory = ClubCategory.MediumClub,
                            Address = new Address()
                            {
                                Street = "Cental Street 1",
                                City = "Er-Riyad",
                                Country = "Saud Arabia"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Race>()
                    {
                        new Race()
                        {
                            Title = "Pro Sprint",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            
                            RaceCategory = RaceCategory.Maraphon40,
                            Address = new Address()
                            {
                                Street = "Naberjnaya",
                                City = "Bishkek",
                                Country = "Kyrgyzstan"
                            }
                        },
                        new Race()
                        {
                            Title = "Annual Race",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            RaceCategory = RaceCategory.Maraphon21,
                            AddressId = 5,
                            Address = new Address()
                            {
                                Street = "Magistral",
                               City = "Bishkek",
                                Country = "Kyrgyzstan"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "AzatNavatov",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            Country = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            Country = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
