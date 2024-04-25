using Nest;
using PIMS.Domain;
using UglyToad.PdfPig;
using Elasticsearch.Net;
using UglyToad.PdfPig.Content;

public class PdfMetadataExtractor
{
    
        private readonly IElasticClient _elasticClient;

        public PdfMetadataExtractor(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexPdfDocumentAsync(PdfDocumentData document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);
            if (!response.IsValid)
            {
                throw new Exception("Failed to index document", response.OriginalException);
            }
        }

}

