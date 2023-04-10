﻿using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Models;

public class WorksContext : DbContext
{
    public WorksContext(DbContextOptions<WorksContext> options)

        : base(options)
    {
    }

    public DbSet<PostItem> PostItems { get; set; } = null!;

    public DbSet<PortfolioApi.Models.WorksItem> WorksItem { get; set; } = default!;

}