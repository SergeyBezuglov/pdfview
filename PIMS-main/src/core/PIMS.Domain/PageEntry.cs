using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain
{
    public class PageEntry : IComparable<PageEntry>
    {
        public string PdfName { get; }
        public int Page { get; }
        public int Count { get; }

        public PageEntry(string pdfName, int page, int count)
        {
            PdfName = pdfName;
            Page = page;
            Count = count;
        }

        public int CompareTo(PageEntry other)
        {
            return other.Count.CompareTo(this.Count);
        }

        public override string ToString()
        {
            return $"{{pdfName: {PdfName}, page: {Page}, count: {Count}}}";
        }
    }
}
