﻿namespace PortfolioApi.Models
{
    public class WorksItem

    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? HyperText { get; set; }

    }
}
