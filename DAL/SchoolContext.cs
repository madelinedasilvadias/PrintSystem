using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL
{
    public class SchoolContext : DbContext
    {
        //Permet d'intéragir avec les tables de la BD
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }
        // Configuration de la connexion à la BD
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SchoolProjectDB");
        }
        // Configuration des relations entre les entités 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Account)
                .WithMany() 
                .HasForeignKey(s => s.AccountID);
        }


        // Méthode pour initialiser la BD avec des données 
        public void Seed()
        {
            if (!Accounts.Any())
            {
                var account1 = new Account { StudentID = 1, StudentBalance = 500 };
                var account2 = new Account { StudentID = 2, StudentBalance = 700 };
                var account3 = new Account { StudentID = 3, StudentBalance = 1000 };
                Accounts.AddRange(account1, account2, account3);
                SaveChanges();
            }

            if (!Classes.Any())
            {
                var class1 = new Classe { Name = "Class A" };
                var class2 = new Classe { Name = "Class B" };
                Classes.AddRange(class1, class2);
                SaveChanges();
            }

            if (!Students.Any())
            {
                // Ajoutez les étudiants
                var accountIds = Accounts.Select(a => a.AccountID).ToList();
                var student1 = new Student { FirstName = "Leonardo", LastName = "DiCaprio", DateOfBirth = new DateTime(1974, 11, 11), ClasseID = 1, AccountID = accountIds[0], Email = "leonardo.dicaprio@hollywood.com" };
                var student2 = new Student { FirstName = "Scarlett", LastName = "Johansson", DateOfBirth = new DateTime(1984, 11, 22), ClasseID = 2, AccountID = accountIds[1], Email = "scarlett.johansson@hollywood.com" };
                var student3 = new Student { FirstName = "Robert", LastName = "Downey Jr.", DateOfBirth = new DateTime(1965, 4, 4), ClasseID = 2, AccountID = accountIds[2], Email = "robert.downeyjr@hollywood.com" };
                var student4 = new Student { FirstName = "Nicole", LastName = "Kidman", DateOfBirth = new DateTime(1967, 6, 20), ClasseID = 1, AccountID = accountIds[0], Email = "nicole.kidman@hollywood.com" };
                var student5 = new Student { FirstName = "Brad", LastName = "Pitt", DateOfBirth = new DateTime(1963, 12, 18), ClasseID = 1, AccountID = accountIds[1], Email = "brad.pitt@hollywood.com" };
                var student6 = new Student { FirstName = "Tom", LastName = "Cruise", DateOfBirth = new DateTime(1962, 7, 3), ClasseID = 2, AccountID = accountIds[2], Email = "tom.cruise@hollywood.com" };
                Students.AddRange(student1, student2, student3, student4, student5, student6);
                SaveChanges();
            }

            if (!Transactions.Any())
            {
                var transaction1 = new Transaction { AccountID = 1, Date = DateTime.Now, Amount = 50, StudentBalance = 550 };
                var transaction2 = new Transaction { AccountID = 2, Date = DateTime.Now, Amount = 70, StudentBalance = 770 };
                Transactions.AddRange(transaction1, transaction2);
                SaveChanges();
            }
        }

    }
}
