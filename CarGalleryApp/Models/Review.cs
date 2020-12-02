using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using YipRestaurantApp.CustomAttributes;

namespace YipRestaurantApp.Models
{
    public class Review    ///Using Mapping class, difine properties that we expect the data we want to use
    {
        [BsonId]//Documents as primary key
        [BsonRepresentation(BsonType.ObjectId)]// allow passign the parameter as a type string instead of ObjectId(Mongo handles)
        public string Id { get; set; }//properties for ID DB

        [BsonElement("Brand")] //bson is specific to mongo, it repersents the name of property in the mongoDB
        [Display(Name = "Restaurant")]//allows the display attribute to display "Restaurant"
        [Required]//this is required by the system from the user
        public string Brand { get; set; }//Property is for restaraunt

        [BsonElement("Model")] //bson is specific to mongo, it repersents the name of property in the mongoDB
        [Display(Name = "Date")]//Display attribute allows to display "Date"
        [Required]//this value is required by the user... They cannot sumbit with out it

        public DateTime Model { get; set; }//formatted for date
        [Range(1, 5)]//sets the constraint for user to only put the numbers 1 - 5 into the text box
        [Display(Name = "Rating(1-5)")]//Display attribute allows to display "Rating(1-5)"

        [BsonElement("Year")]//////bson is specific to mongo, it repersents the name of property in the mongoDB
        [Required]//this value is required by the user... They cannot sumbit with out it
        //[YearRange]
        public int Year { get; set; }//property is for the date

        [BsonElement("Price")]//bson is specific to mongo, it repersents the name of property in the mongoDB
        [Display(Name = "Comment")]//Display attribute allows to display "Comment"
        [Required]//this value is required by the user... They cannot sumbit with out it

        public string Price { get; set; } //changed to string//from an int

        [BsonElement("Person")]//bson is specific to mongo, it repersents the name of property in the mongoDB
        [Display(Name = "Person")]////Display attribute allows to display "Person"


        public UserModel Person { get; set; }//setting person object to usermodel for login properties
    }
}
