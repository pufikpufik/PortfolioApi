﻿namespace PortfolioApi.Models
{
    public class PostItem
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Secret { get; set; }

    }

    public class PostItemDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } = string.Empty;
      }
}