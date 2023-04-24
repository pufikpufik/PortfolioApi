﻿using Microsoft.EntityFrameworkCore;
using PortfolioApi.Models;

namespace PortfolioApi.Models;

public class WorksContext : DbContext
{
    public WorksContext(DbContextOptions<WorksContext> options)

        : base(options)
    {
    }

    public DbSet<WorksItem> WorksItems { get; set; } = null!;


}