﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceAPI.Models.Comment
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]

        public string Text { get; set; } = null!;

        [Required]
        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User.User User { get; set; } = null!;

        [Required]
        public int PublicationId { get; set; }

        [ForeignKey("PublicationId")]
        public Publication.Publication Publication { get; set;} = null!;
    }
}