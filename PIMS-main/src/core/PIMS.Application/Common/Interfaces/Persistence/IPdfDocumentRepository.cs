using PIMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Persistence
{
    public interface IPdfDocumentRepository
    {
        Task<PdfDocument> GetByIdAsync(int id);
        Task<List<PdfDocument>> GetAllAsync();
        Task AddAsync(PdfDocument pdfDocument);
        Task UpdateAsync(PdfDocument pdfDocument);
        Task DeleteAsync(int id);
        Task<List<PdfDocument>> SearchByParamsAsync(SearchParams searchParams);
    }
}
