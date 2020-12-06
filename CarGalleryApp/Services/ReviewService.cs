using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using YipRestaurantApp.Models;

namespace YipRestaurantApp.Services
{
    public class ReviewService
    {
        private readonly IMongoCollection<Review> reviews;

        public ReviewService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("CarGalleryDb"));//MongoClient reads instance to preforM DB operations
            IMongoDatabase database = client.GetDatabase("CarGalleryDb");//IMongoDatabase: Represents the Mongo database for performing operations
            reviews = database.GetCollection<Review>("Cars");//GetCollection<Review>("Cars"): generic  method used to gain access to data in a specific collection. 
                                                          //CRUD operations can be preformed against the collection after this method is called
        }

        public List<Review> Get()
        {
            return reviews.Find(review => true).ToList();//Returns Documents in the collection matching the provided search critera
        }

        public Review Get(string id)
        {
            return reviews.Find(review => review.Id == id).FirstOrDefault();//Returns Documents in the collection matching the provided search critera
        }

        public Review Create(Review review)
        {
            reviews.InsertOne(review);//InsertOne: inserts the provided object as a new document in the collection
            return review;
        }

        public void Update(string id, Review reviewIn)
        {
            reviews.ReplaceOne(review => review.Id == id, reviewIn);//ReplaceOne: Replaces the single document matching the provided search criteria with the provided object
        }

        public void Remove(Review reviewIn)
        {
            reviews.DeleteOne(review => review.Id == reviewIn.Id);//Deletes a single document matching the provided search criteria
        }

        public void Remove(string id)
        {
            reviews.DeleteOne(review => review.Id == id);//Deletes a single document matching the provided search criteria
        }

        public bool Get(string person, string password) //Queries Mongo to determine whether document exists and if it matches both
        {
            var myVar = reviews.Find(car => car.Person.Password == password).FirstOrDefault();
            if (myVar != null && myVar.Person.FirstName == person) return true;
            else
            {
                return false; //Failed one of the two checks, not a valid user
            }
      
        }
    }
}
