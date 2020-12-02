using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using YipRestaurantApp.Models;

namespace YipRestaurantApp.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Review> cars;

        public CarService(IConfiguration config)
        {
            MongoClient client = new MongoClient(config.GetConnectionString("CarGalleryDb"));//MongoClient reads instance to preforM DB operations
            IMongoDatabase database = client.GetDatabase("CarGalleryDb");//IMongoDatabase: Represents the Mongo database for performing operations
            cars = database.GetCollection<Review>("Cars");//GetCollection<Review>("Cars"): generic  method used to gain access to data in a specific collection. 
                                                           //CRUD operations can be preformed against the collection after this method is called
        }

        public List<Review> Get()
        {
            return cars.Find(car => true).ToList();//Returns Documents in the collection matching the provided search critera
        }

        public Review Get(string id)
        {
            return cars.Find(car => car.Id == id).FirstOrDefault();//Returns Documents in the collection matching the provided search critera
        }

        public Review Create(Review car)
        {
            cars.InsertOne(car);//InsertOne: inserts the provided object as a new document in the collection
            return car;
        }

        public void Update(string id, Review carIn)
        {
            cars.ReplaceOne(car => car.Id == id, carIn);//ReplaceOne: Replaces the single document matching the provided search criteria with the provided object
        }

        public void Remove(Review carIn)
        {
            cars.DeleteOne(car => car.Id == carIn.Id);//Deletes a single document matching the provided search criteria
        }

        public void Remove(string id)
        {
            cars.DeleteOne(car => car.Id == id);//Deletes a single document matching the provided search criteria
        }
    }
}
