using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using VotingApp.Domain;

namespace VotingApp.Api
{
    public class MongoDbVotingService : IVotingService
    {
        private readonly IMongoCollection<VotingDocument> _collection;

        public MongoDbVotingService(string connectionString)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("voting");
            _collection = db.GetCollection<VotingDocument>("votes");
        }

        public async Task<Voting> Get()
        {
            var result = await _collection.Find(_ => true).FirstOrDefaultAsync();
            return result == null
                ? new Voting()
                : new Voting(result.Votes, result.Winner);
        }

        public async Task Save(Voting voting)
        {
            await _collection.ReplaceOneAsync(_ => true, new VotingDocument
            {
                Votes = voting.Votes,
                Winner = voting.Winner
            },
            new UpdateOptions
            {
                IsUpsert = true
            });
        }
    }

    public class VotingDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Votes")]
        public Dictionary<string, int> Votes { get; set; }

        [BsonElement("Winner")]
        public string Winner { get; set; }
    }
}