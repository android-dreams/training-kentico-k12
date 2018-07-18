﻿using CMS.DocumentEngine;

namespace Kentico.Services.Query
{
    public interface IDocumentQueryService
    {
        DocumentQuery<TDocument> GetDocument<TDocument>(int nodeId) where TDocument : TreeNode, new();
        DocumentQuery<TDocument> GetDocuments<TDocument>() where TDocument : TreeNode, new();
    }
}