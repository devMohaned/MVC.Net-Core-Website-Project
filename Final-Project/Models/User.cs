using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string FullName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public string? Address { get; set; }
        [StringLength(11, MinimumLength = 11)]
        public string? Phone { get; set; }
        #region Relations

        public  List<Ad>? _UserAds { get; set; }
        public  List<Favorite>? _UserFavoriteItems { get; set; }
        #endregion
    }
}
