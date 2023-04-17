﻿using Microsoft.EntityFrameworkCore;
using Rocky.Models;

namespace Rocky.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<ApplicationType> ApplicationTypes { get; set; } = null!;
}
