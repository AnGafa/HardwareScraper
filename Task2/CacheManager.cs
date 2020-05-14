using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HardwareScraper
{
    class CacheManager
    {
        List<Search> searches;
        const string fileName = "Data.bin";

        public CacheManager()
        {
            searches = new List<Search>();
            if (!File.Exists(fileName)) // first time only, file does not exist
                Serialize();
            Deserialize();
        }

        public string Search(string searchTerm)
        {
            string result = null;
            foreach (Search search in searches)
                if (search.SearchTerm.ToLower() == searchTerm.ToLower())
                {
                    result = search.ArticleText;
                    break;
                }
            return result;
        }

        public void Save(Search search)
        {
            searches.Add(search);
            Serialize();
        }

        void Serialize()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, searches);
            }
        }

        void Deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                searches = (List<Search>)formatter.Deserialize(stream);
            }
        }
    }
}
