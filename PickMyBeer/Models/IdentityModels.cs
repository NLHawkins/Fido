using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace PickMyBeer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserRole { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("UserRole", this.UserRole));
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<PatronClient> PatronClients { get; set; }
        public DbSet<BarClient> BarClients { get; set; }
        public DbSet<PrevSearchedBeer> PrevSearchedBeers { get; set; }
        public DbSet<FaveBeer> FaveBeers { get; set; }
        public DbSet<BeerOnTap> BeerOnTaps { get; set; }
        public DbSet<BeerArchive> BeerArchives { get; set; }
        public DbSet<IngredientLog> IngredientLogs { get; set; }
        public DbSet<PopBeer> PopBeers { get; set; }
        public DbSet<Match> Matches { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}