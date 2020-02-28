using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using ShortLinks.Models;

namespace ShortLinks.Services
{
    public class ShortLinksService : IShortLinksService
    {
        private readonly IMongoCollection<Link> _links;

        public ShortLinksService(ILinkstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _links = database.GetCollection<Link>(settings.LinksCollectionName);
        }

        public async Task<string> CreateShortLinkAsync(string link, string sessionId)
        {
            if (!Utils.CheckUrlValid(link)) throw new Exceptions.ApiException("введите ссылку в правильном формате");
            var newLink = new Link
            {
                OriginalLink = Utils.GetValidLink(link),
                ShortLink = Utils.RandomString(7),
                ViewCounter = 0,
                SessionId = sessionId
            };
            await _links.InsertOneAsync(newLink);
            return newLink.ShortLink;
        }

        public async Task<IEnumerable<ShortLink>> GetAllShortLinkAsync(string sessionId = null)
        {
            var builder = Builders<Link>.Filter;
            var filter = sessionId == null ? builder.Empty :  builder.Eq("SessionId", sessionId);
            var linkResult = await _links.Find(filter).ToListAsync();
            return linkResult.Select(i => new ShortLink {
                Link = Utils.GetLinkWithDomain(i.ShortLink),
                ViewCounter = i.ViewCounter
            });
        }

        public async Task<string> GetLinkAsync(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink)) throw new ArgumentNullException("shortLink");

            var link = await _links.FindOneAndUpdateAsync(Builders<Link>.Filter.Eq("ShortLink", shortLink),
                                Builders<Link>.Update.Inc("ViewCounter", 1),
                                new FindOneAndUpdateOptions<Link, Link>
                                {
                                    IsUpsert = false,
                                    ReturnDocument = ReturnDocument.After
                                });
            return link?.OriginalLink;
        }
    }
}
