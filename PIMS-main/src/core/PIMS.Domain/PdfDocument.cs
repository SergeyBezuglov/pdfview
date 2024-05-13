using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Domain
{
    public class PdfDocument
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher {  get; set; }
        public int? Year {  get; set; }
        public string FilePath { get; set; }
        public string Content { get; set; } // Здесь будет храниться текст PDF
    }
}
