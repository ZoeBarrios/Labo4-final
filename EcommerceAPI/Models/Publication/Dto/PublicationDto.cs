﻿namespace EcommerceAPI.Models.Publication.Dto
{
    public class PublicationDto
    {
        
        public int PublicationId { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; } 

        public DateTime CreatedAt { get; set; }

        public bool IsPaused { get; }

        public string IMageUrl { get; set; } = null!;
 
        public int UserId { get; set; }


        public int CategoryId { get; set; }

    }
}
