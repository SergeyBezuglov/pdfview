using Microsoft.EntityFrameworkCore;
using PIMS.Application;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain;
using PIMS.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Common
{
    public class PdfDocumentRepository : IPdfDocumentRepository
    {
        private readonly PIMSDbContext _context;

        public PdfDocumentRepository(PIMSDbContext context)
        {
            _context = context;
        }

        public async Task<PdfDocument> GetByIdAsync(int id)
        {
            return await _context.PdfDocuments.FindAsync(id);
        }

        public async Task<List<PdfDocument>> GetAllAsync()
        {
            return await _context.PdfDocuments.ToListAsync();
        }

        public async Task AddAsync(PdfDocument pdfDocument)
        {
            _context.PdfDocuments.Add(pdfDocument);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PdfDocument pdfDocument)
        {
            _context.PdfDocuments.Update(pdfDocument);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var document = await _context.PdfDocuments.FindAsync(id);
            if (document != null)
            {
                _context.PdfDocuments.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<PdfDocument>> SearchByContentAsync(string query)
        {
            return await _context.PdfDocuments
                .Where(d => EF.Functions.FreeText(d.Content, query))
                .ToListAsync();
        }
        public async Task<List<PdfDocument>> SearchByParamsAsync(SearchParams searchParams)
        {
            var query = _context.PdfDocuments.AsQueryable();

            // Фильтрация по ID, если указан
            if (searchParams.Id.HasValue)
                query = query.Where(doc => doc.Id == searchParams.Id.Value);

            // Фильтрация по названию, автору, издателю и ключевым словам с использованием FullText Search, если доступно
            if (!string.IsNullOrEmpty(searchParams.Title))
                query = query.Where(doc => EF.Functions.FreeText(doc.Title, searchParams.Title));
            if (!string.IsNullOrEmpty(searchParams.Author))
                query = query.Where(doc => EF.Functions.FreeText(doc.Author, searchParams.Author));
            if (!string.IsNullOrEmpty(searchParams.Publisher))
                query = query.Where(doc => EF.Functions.FreeText(doc.Publisher, searchParams.Publisher));

            // Фильтрация по году, если указан
            if (searchParams.Year.HasValue)
                query = query.Where(doc => doc.Year == searchParams.Year.Value);

            // Поиск по ключевым словам, если они указаны
            if (!string.IsNullOrEmpty(searchParams.Keywords))
            {
                var keywordQuery = query.Where(doc => EF.Functions.FreeText(doc.Keywords, searchParams.Keywords));
                // Объединяем результаты поиска по ключевым словам и по типу документа
                query = query.Concat(keywordQuery);
            }

            // Поиск по типу документа, если он указан
            if (!string.IsNullOrEmpty(searchParams.DocumentType))
            {
                var docTypeQuery = _context.PdfDocuments.Where(doc => doc.DocumentType == searchParams.DocumentType);
                // Объединяем результаты поиска по ключевым словам и по типу документа
                query = query.Concat(docTypeQuery);
            }

            // Удаляем дубликаты документов, которые могли возникнуть при объединении результатов двух запросов
            query = query.Distinct();


            // Возврат результатов поиска
            return await query.ToListAsync();
        }
    }
}
