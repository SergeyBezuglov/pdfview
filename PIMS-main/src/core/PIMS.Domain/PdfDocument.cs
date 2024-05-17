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
        public string Keywords {  get; set; }
        public string DocumentType { get; set; }
        public byte[] Content { get; set; }  // Изменено на массив байтов
        public string Extension { get; set; }  // Добавлено поле для расширения файла 
    }
}
