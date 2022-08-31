using Final_Project.Models;

namespace Final_Project.Services.BrandServiceFolder
{
    public interface IBrandService : IGeneralDataService<Brand>, ISingleDataService<Brand>
    {
        public List<Brand> GetCategoryBrands(int id);
    }
}
