using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain
{
    public interface ISearchEngine
    {
        List<PageEntry> Search(string word);
    }
}
