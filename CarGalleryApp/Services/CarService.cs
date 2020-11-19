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
            MongoClient client = new MongoClient(config.GetConnectionString("CarGalleryDb"));
            IMongoDatabase database = client.GetDatabase("CarGalleryDb");
            cars = database.GetCollection<Review>("Cars");
        }

        public List<Review> Get()
        {
            return cars.Find(car => true).ToList();
        }

        public Review Get(string id)
        {
            return cars.Find(car => car.Id == id).FirstOrDefault();
        }

        public Review Create(Review car)
        {
            cars.InsertOne(car);
            return car;
        }

        public void Update(string id, Review carIn)
        {
            cars.ReplaceOne(car => car.Id == id, carIn);
        }

        public void Remove(Review carIn)
        {
            cars.DeleteOne(car => car.Id == carIn.Id);
        }

        public void Remove(string id)
        {
            cars.DeleteOne(car => car.Id == id);
        }
    }
}
