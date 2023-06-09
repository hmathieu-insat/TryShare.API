﻿using INSAT._4I4U.TryShare.Core.Models;
using INSAT._4I4U.TryShare.Core.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace INSAT._4I4U.TryShare.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Tricycle> Tricycles { get; set; }
    }
}
