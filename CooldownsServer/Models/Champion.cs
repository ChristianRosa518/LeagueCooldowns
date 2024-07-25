using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace CooldownsServer.Models
{
    public class Champion

    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string ChampionName { get; set; } = null!;
        public string[] AbilityQ { get; set; } = null!;
        public string[] AbilityW { get; set; } = null!;
        public string[] AbilityE { get; set; } = null!;
        public string[] AbilityR { get; set; } = null!;

    }                                  
}                                      
                                       