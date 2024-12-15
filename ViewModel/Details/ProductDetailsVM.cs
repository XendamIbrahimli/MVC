using UniqloMVC.Models;

namespace UniqloMVC.ViewModel.Details
{
    public class ProductDetailsVM
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal SellPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public string CoverImage { get; set; } = null!;
        public int? CategoryId { get; set; }
        public string Username { get; set; }
        public ICollection<string>? ImagesUrl { get; set; }
        public ICollection<int>? Ratings {  get; set; }
        public ICollection<ProductComment> Comments { get; set; }
    }
}
