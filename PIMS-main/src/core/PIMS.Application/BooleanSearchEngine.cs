using PIMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application
{
    public class BooleanSearchEngine : ISearchEngine
    {
        private Dictionary<string, List<PageEntry>> wordList = new Dictionary<string, List<PageEntry>>();

        public BooleanSearchEngine(string pdfsDirPath)
        {
            // Implement the logic to populate wordList here
        }

        public List<PageEntry> Search(string word)
        {
            var wordToLowerCase = word.ToLower();
            if (wordList.ContainsKey(wordToLowerCase))
            {
                return new List<PageEntry>(wordList[wordToLowerCase]);
            }
            return new List<PageEntry>();
        }
    }
}
