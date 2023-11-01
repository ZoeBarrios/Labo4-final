using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Publication;

namespace EcommerceAPI.Models.Purchase.Dto
{
    public class PurchaseDto
    {

        public int Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<Publication.Publication> Publications = null!;

    }
}
