using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ShortLinks.Models
{
    public class Link
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("originalLink")]
        public string OriginalLink { get; set; }

        [JsonProperty("shortLink")]
        public string ShortLink { get; set; }

        [JsonProperty("viewCounter")]
        public long ViewCounter { get; set; }

        [JsonProperty("SessionId")]
        public string SessionId { get; set; }
    }

    public class ShortLink
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("viewCounter")]
        public long ViewCounter { get; set; }
    }

}
