﻿using System.Collections.Generic;
using System.Linq;
using MaterialCMS.Services.ImportExport.DTOs;
using MaterialCMS.Entities.Documents.Web;

namespace MaterialCMS.Services.ImportExport.Rules
{
    public class DocumentParentIsValid : IDocumentImportValidationRule
    {
        private readonly IDocumentService _documentService;

        public DocumentParentIsValid(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public IEnumerable<string> GetErrors(DocumentImportDTO item, IList<DocumentImportDTO> allItems)
        {
            if (!string.IsNullOrWhiteSpace(item.ParentUrl) && _documentService.GetDocumentByUrl<Webpage>(item.ParentUrl) == null && !allItems.Any(x => x.UrlSegment == item.ParentUrl))
                yield return "The parent url specified is not present within the system.";
        }
    }
}