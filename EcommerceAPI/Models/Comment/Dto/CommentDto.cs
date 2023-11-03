using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models.Comment.Dto
{
    public class CommentDto
    {
       
        public int CommentId { get; set; }

       
        public string Text { get; set; } = null!;

     
        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

       
        public int UserId { get; set; }


        public bool isEliminated { get; set; }
     
        public int PublicationId { get; set; }

   
    }
}
