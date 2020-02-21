using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortLinks.Models
{

    public class LinkstoreDatabaseSettings : ILinkstoreDatabaseSettings
    {
        public string LinksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ILinkstoreDatabaseSettings
    {
        string LinksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
