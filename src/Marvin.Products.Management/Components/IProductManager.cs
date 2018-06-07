﻿using System;
using System.Collections.Generic;
using Marvin.AbstractionLayer;
using Marvin.Modules;
using Marvin.Products.Management.Importers;
using Marvin.Products.Management.Modification;

namespace Marvin.Products.Management
{
    /// <summary>
    /// Management component
    /// </summary>
    internal interface IProductManager : IPlugin
    {
        /// <summary>
        /// Returns all available product importers
        /// </summary>
        IProductImporter[] Importers { get; }

        /// <summary>
        /// Returns all products on this machine
        /// </summary>
        IEnumerable<IProduct> GetAll();

        /// <summary>
        /// Load product instance by id
        /// </summary>
        IProduct GetProduct(long id);

        /// <summary>
        /// Load product by identity
        /// </summary>
        IProduct GetProduct(ProductIdentity identity);

        /// <summary>
        /// Event raised when a product changed
        /// </summary>
        event EventHandler<IProduct> ProductChanged;

        /// <summary>
        /// Save a product to the database
        /// </summary>
        long Save(IProduct modifiedInstance);

        /// <summary>
        /// Release this product to be available for production
        /// </summary>
        IProduct Release(long id);

        /// <summary>
        /// Create revision of this product with provided revision number
        /// </summary>
        IProduct CreateRevision(long productId, short revisionNo, string comment);

        /// <summary>
        /// Import the given file as a product to the database
        /// </summary>
        IReadOnlyList<IProduct> ImportProducts(string importer, IImportParameters parameters);

        /// <summary>
        /// Try to delete a product. If it is still used as a part in other products, the product
        /// is not deleted but instead all affacted products returned.
        /// </summary>
        /// <param name="deprecatedProduct">Product that is depracted and should be deleted.</param>
        /// <returns>The collection of all products that would be affacted by the removal. If the collection is empty the product was removed.</returns>
        IReadOnlyList<IProduct> DeleteProduct(IProduct deprecatedProduct);

        /// <summary>
        /// Create an article instance of given product
        /// </summary>
        /// <param name="product">Product to instanciate</param>
        /// <param name="save">Flag if new instance should already be saved</param>
        /// <returns>New instance</returns>
        Article CreateInstance(IProduct product, bool save);

        /// <summary>
        /// Get an article with the given id.
        /// </summary>
        /// <param name="id">The id for the article which should be searched for.</param>
        /// <returns>The article with the id when it exists.</returns>
        Article GetArticle(long id);

        /// <summary>
        /// Gets a list of articles by a given state
        /// </summary>
        IEnumerable<Article> GetArticles(ArticleState state);

        /// <summary>
        /// Load articles using combined bit flags
        /// </summary>
        IEnumerable<Article> GetArticles(int state);

        /// <summary>
        /// Updates the database from the article instance
        /// </summary>
        void SaveArticles(params Article[] articles);

        /// <summary>
        /// Export the full tree
        /// </summary>
        ProductStructureEntry[] ExportTree();

        /// <summary>
        /// Load full revision graph
        /// </summary>
        ProductRevisionEntry[] Revisions(string identifier);
    }
}