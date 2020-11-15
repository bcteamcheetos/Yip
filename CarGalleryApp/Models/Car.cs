using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using CarGalleryApp.CustomAttributes;

namespace CarGalleryApp.Models
{
    public class Car
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Brand")]
        [Display(Name = "Restaurant")]
        [Required]
        public string Brand { get; set; }

        [BsonElement("Model")]
        [Display(Name = "Date")]
        [Required]
       
        public DateTime Model { get; set; }//formatted for date
        [Range(1, 5)]
        [Display(Name = "Rating(1-5)")]
        [BsonElement("Year")]
        [Required]
        //[YearRange]
        public int Year { get; set; }

        [BsonElement("Price")]
        [Display(Name = "Comment")]
        [Required]
        
        public string Price { get; set; } //changed to string

        [BsonElement("ImageUrl")]
        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]//Deleted Required
        
        public string ImageUrl { get; set; }
    }
}
