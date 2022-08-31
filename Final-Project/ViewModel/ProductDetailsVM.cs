using Final_Project.Models;

namespace Final_Project.ViewModel
{
    public class ProductDetailsVM
    {
        public Ad MainProduct { get; set; }
        public List<Ad> RelatedProducts { get; set; }
    }
}
