using Contracts.Domains;
using Infrastructure.Extensions;
using MongoDB.Bson.Serialization.Attributes;

namespace User.Api.Entities
{
    [BsonCollectionAttribute("UserEntries")]
    public class UserEntry : MongoEntity
    {

        public UserEntry(string id) => (Id) = id;

        public UserEntry()
        {
        }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("subjectId")]
        public string SubjectId { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
