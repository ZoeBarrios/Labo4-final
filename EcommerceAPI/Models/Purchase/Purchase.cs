using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models.Purchase
{
    public class Purchase
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        public DateTime PurchaseDate = DateTime.Now;

        [Required]
        public int UserId { get; set; }


        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;

        [Required]
        public int PublicationId { get; set; }

        [ForeignKey("PublicationId")]
        public Publication.Publication Publication { get; set; } = null!;


    }
}
