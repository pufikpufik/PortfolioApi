namespace PortfolioApi.Models
{
    public class AboutMeItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set;}
        public string? Secret { get; set; }

    }
  
    public class AboutMeItemDTO
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set;}
    }
}
