using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class Favorite
    {
        [Required]
        public string CustomerId { get; set; }


        [Required]
        public int AdtId { get; set; }



        #region Relations

        [ForeignKey(nameof(AdtId))]
        public  Ad? _Ad { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public  User? _Customer { get; set; }


        #endregion


    }
}
