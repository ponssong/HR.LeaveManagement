﻿using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HR.LeaveManagement.Identity.Configurations;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity
{
    public class LeaveTypeManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public LeaveTypeManagementIdentityDbContext(DbContextOptions<LeaveTypeManagementIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}