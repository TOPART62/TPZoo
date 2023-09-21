using ZooCore.Models;
using ZooCore.Datas;
using Microsoft.EntityFrameworkCore;

namespace ZooAPI.Datas
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Animal> Animals{ get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().HasData(InitialAnimal.animals);

            var adminRoot = new User()
            {
                Id = 1,
                FirstName = "Root",
                LastName = "ROOT",
                PhoneNumber = "01 01 01 01 01",
                Address = "2 rue tartempion 55555 Bidule",
                Email = "root@pokecorp.com",
                PassWord = "UEFzczAwKytsYSBjbMOpIHN1cGVyIHNlY3LDqHRlIGRlIGxhIHBva2Vtb24gYXBp",
                IsAdmin = true
            };
            modelBuilder.Entity<User>().HasData(adminRoot);
            var currentUser = new User()
            {
                Id = 2,
                FirstName = "Current",
                LastName = "User",
                PhoneNumber = "02 02 02 02 02",
                Address = "10 rue tartempion 55555 Turlututu",
                Email = "curentuser@pokecorp.com",
                PassWord = "UEFzczAwKytsYSBjbMOpIHN1cGVyIHNlY3LDqHRlIGRlIGxhIHBva2Vtb24gYXBp",
                IsAdmin = false
            };
            modelBuilder.Entity<User>().HasData(currentUser);
        }


    }
}
