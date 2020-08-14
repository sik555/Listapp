using Listapp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listapp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _Users;

        public UserService(IListappDatabaseSettings settings) 
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() => _Users.Find(c => true).ToList();

        public User Get(string id) => _Users.Find(c => c.Id == id).FirstOrDefault();

        public User Create(User user)
        {
            _Users.InsertOne(user);

            return user;
        }

        public void Update(string id, User userIn)
        {
            _Users.ReplaceOne(c => c.Id == id, userIn);
        }

        public void Remove (string id)
        {
            _Users.DeleteOne(c => c.Id == id);
        }
    }
}
