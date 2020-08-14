using Listapp.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listapp.Services
{
    public class ListService
    {
        private readonly IMongoCollection<List> _Lists;

        public ListService(IListappDatabaseSettings settings) 
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Lists = database.GetCollection<List>(settings.ListsCollectionName);
        }

        public List<List> Get() => _Lists.Find(c => true).ToList();

        public List GetList(string id) => _Lists.Find(c => c.Id == id).FirstOrDefault();

        public List<List> GetVotedList(string userId)
        {
            List<List> result = new List<List>();
            try
            {
                result = _Lists.Find(x => x.Items == x.Items.Where(c => c.Votes == c.Votes.Where(y => y.Id == userId)).ToList()).ToList(); 
            }
            catch(Exception e)
            {
               
            }

            return result;
        }

        public List Create(List list)
        {
            _Lists.InsertOne(list);

            return list;
        }

        public void Update(string id, List listIn)
        {

            _Lists.ReplaceOne(x => x.Id == id, listIn);

        }

        public void Remove (string id)
        {
            _Lists.DeleteOne(c => c.Id == id);
        }
    }
}
