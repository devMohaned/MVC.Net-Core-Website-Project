namespace Final_Project.Services.ProductsServiceFolder
{
    public interface IAdService : IGeneralDataService<Ad>, ISingleDataService<Ad>
    {
        //      public Product GetProductWithRatings(int id);
        public List<Ad> GetRandomAds(int adSize, int multiple);
        public List<Ad> GetUserAd(string UserId);
        public List<Ad> GetUserAdWithReview(string UserId, bool isProductReviewed);

        public Ad GetAd(int id);
        public List<AdImage> GetAdImageList(int AdId);
        public List<Ad> getYouAlsoMayLike(int? catId, int id);
        public void AdViewd(int Adid);
        public void AdPhoneRequested(int Adid);
        public void AdAddedToFavourit(int Adid);
        public List<Ad> searchforstr(string str);



    }
}
