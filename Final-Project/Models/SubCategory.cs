using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }
        //[Required,Display(Name ="Category")]
        //public int CategoryId { get; set; }
        [Required,StringLength(50,MinimumLength =3)]
        public string Name { get; set; }

        #region Relations
       // [ForeignKey(nameof(CategoryId))]
        //public virtual Category? _Category { get; set; }
        public  List<Ad>? _Ads { get; set; }
        #endregion
    }
}
