using System;

namespace HardwareScraper
{
    [Serializable]
    class Search
    {
        public string SearchTerm {get;set;}
        public string ArticleText { get; set; }

        public Search(string searchTerm, string articleText)
        {
            SearchTerm = searchTerm;
            ArticleText = articleText;
        }
    }
}
