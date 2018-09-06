using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB;
using MongoDB.Bson;

namespace SimpleBot
{
    public class SimpleBotUser
    {
        public static string Reply(Message message)
        {

            /*var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela1");

            var doc = new BsonDocument()
            {
                {"id", message.Id },
                {"texto",message.Text }

            };

            col.InsertOne(doc);*/

            var profile = GetProfile(message.Id);

            if (profile != null)
            {
                profile.Visitas += 1;

            }

            SetProfile(message.Id, profile);

            return $"{message.User} disse '{message.Text}'";
        }

        public static UserProfile GetProfile(string id)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela1");
            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);
            var retorno = col.Find(filtro).ToList().First<BsonDocument>() ;

            /*var docFiltro = new BsonDocument()
            {
                {"id", id }
            };

            var retorno = col.Find(docFiltro);*/
            if (retorno != null)
            {
                var profile = new UserProfile();
                profile.Id = retorno.GetElement("id").Value.ToString();
                
                profile.Visitas = retorno.GetElement("visitas").Value.ToInt32();

                return profile;
            }
            else
            {
                return null;
            }
           

           
        }

        public static void SetProfile(string id, UserProfile profile)
        {

            var client = new MongoClient();
            var db = client.GetDatabase("db01");
            var col = db.GetCollection<BsonDocument>("tabela1");
            var filtro = Builders<BsonDocument>.Filter.Eq("id", id);

            var doc = new BsonDocument()
            {
                {"id", id },
                {"visitas", profile.Visitas }

            };

            //col.ReplaceOne(filtro, doc,  { upsert: true });
            col.ReplaceOne(filtro, doc);

        }
    }
}