﻿using API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                 .HasOne(e => e.Account)
                 .WithOne(a => a.Employee)
                 .HasForeignKey<Account>(a => a.NIK);

            modelBuilder.Entity<Account>()
               .HasOne(a => a.Profiling)
               .WithOne(p => p.Account)
               .HasForeignKey<Profiling>(p => p.NIK);

            modelBuilder.Entity<Education>()
                .HasMany(p => p.Profilings)
                .WithOne(edc => edc.Education);

            modelBuilder.Entity<Education>()
               .HasOne(edc => edc.University)
               .WithMany(u => u.Educations);

            modelBuilder.Entity<AccountRole>()
                .HasKey(bc => new { bc.NIK, bc.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Account)
                .WithMany(b => b.AccountRoles)
                .HasForeignKey(bc => bc.NIK);
            modelBuilder.Entity<AccountRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.AccountRoles)
                .HasForeignKey(bc => bc.RoleId);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies();
        //}
    }
}
