﻿using Microsoft.EntityFrameworkCore;
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

            if (!string.IsNullOrEmpty(searchParams.Title))
                query = query.Where(doc => EF.Functions.FreeText(doc.Title, searchParams.Title));
            if (!string.IsNullOrEmpty(searchParams.Author))
                query = query.Where(doc => EF.Functions.FreeText(doc.Author, searchParams.Author));
            if (!string.IsNullOrEmpty(searchParams.Publisher))
                query = query.Where(doc => EF.Functions.FreeText(doc.Publisher, searchParams.Publisher));
            if (searchParams.Year.HasValue)
                query = query.Where(doc => doc.Year == searchParams.Year.Value);
            if (!string.IsNullOrEmpty(searchParams.Keywords))
                query = query.Where(doc => EF.Functions.FreeText(doc.Content, searchParams.Keywords));

            return await _context.PdfDocuments
               .Where(d => d.Content.Contains(searchParams.Query) ||
                           d.Title.Contains(searchParams.Query) ||
                           d.Author.Contains(searchParams.Query) ||
                           d.Publisher.Contains(searchParams.Query))
               .ToListAsync();
        }
    }
}
