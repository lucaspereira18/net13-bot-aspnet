using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBot
{
    public class UserProfile
    {
        public string Id { get; set; }
        public int Visitas { get; set; }

        public UserProfile GetProfile()
        {

            var client = new MongoClient();
            var db = client.GetDatabase("local");
            var collection = db.GetCollection<BsonDocument>("UserProfile");

            return null;
        }

        public void SetProfile(string Id, UserProfile profile)
        {

            var client = new MongoClient();
            var db = client.GetDatabase("local");
          

            var documentos = from i in Enumerable.Range(1, 10)
                             select new BsonDocument() {
                                { "id", i }
                                };
            






        }

    }
}