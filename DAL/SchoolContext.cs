using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=SchoolProjectDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Account)
                .WithMany() // Si la relation est Many-to-One, sinon utiliser WithOne() pour One-to-One
                .HasForeignKey(s => s.AccountID);
        }


        // Method to seed data into the database
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
                // Ajoutez les étudiants avec des AccountID existants
                var accountIds = Accounts.Select(a => a.AccountID).ToList();
                var student1 = new Student { FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(2000, 1, 1), ClasseID = 1, AccountID = accountIds[0], Email = "john.doe@example.com" };
                var student2 = new Student { FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(2001, 2, 2), ClasseID = 1, AccountID = accountIds[1], Email = "alice.smith@example.com" };
                var student3 = new Student { FirstName = "Bob", LastName = "Johnson", DateOfBirth = new DateTime(2002, 3, 3), ClasseID = 2, AccountID = accountIds[2], Email = "bob.johnson@example.com" };
                Students.AddRange(student1, student2, student3);
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
