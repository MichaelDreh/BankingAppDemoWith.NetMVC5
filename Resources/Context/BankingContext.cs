using BankingSystem.Resources.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankingSystem.Resources.Context
{
    public class BankingContext : DbContext
    {
        public BankingContext() : base("conString")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Role> Roles { get;set ;}
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<RecoverPassword> RecoverPasswords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasRequired(a => a.User)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .HasRequired(a => a.Customer)
                .WithMany()
                .WillCascadeOnDelete(false);
        }
    }

}