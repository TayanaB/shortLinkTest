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

        public Task<string> CreateShortLink(string link, string sessionId)
        {
            if (!Utils.CheckUrlValid(link)) throw new Exceptions.ApiException("введите ссылку в правильном формате");
            var newLink = new Link
            {
                OriginalLink = Utils.GetValidLink(link),
                ShortLink = Utils.RandomString(7),
                ViewCounter = 0,
                SessionId = sessionId
            };
            _links.InsertOne(newLink);
            return Task.FromResult( Utils.GetLinkWithDomain(newLink.ShortLink));
        }

        public Task<IEnumerable<ShortLink>> GetAllShortLink(string sessionId = null)
        {
            var linkResult = sessionId == null ? _links.Find(i => true).ToEnumerable() 
                                                : _links.Find(i => i.SessionId == sessionId).ToEnumerable();
            return Task.FromResult(linkResult.Select(i => new ShortLink {
                Link = Utils.GetLinkWithDomain(i.ShortLink),
                ViewCounter = i.ViewCounter
            }));
        }

        public Task<string> GetLink(string shortLink)
        {
            var link = _links.Find(i => i.ShortLink == shortLink).FirstOrDefault();
            if (link == null) throw new Exceptions.NotFoundException("ссылка не найдена");
            link.ViewCounter++;
            _links.ReplaceOne(i => i.Id == link.Id, link);
            return Task.FromResult(link?.OriginalLink);
        }
    }
}
