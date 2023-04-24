﻿namespace PortfolioApi.Models
{
    public class RecallItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? Comment { get; set; }
        public string? Secret { get; set; }


    }
    public class RecallItemDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public string? Comment { get; set; }
    }
}
