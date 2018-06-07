﻿namespace Marvin.AbstractionLayer
{
    /// <summary>
    /// Base class that allows to assign a value to <see cref="IPersistentObject.Id"/>
    /// </summary>
    public abstract class ProductPartLink : IProductPartLink
    {
        /// <summary>
        /// Default constructor for a new part link
        /// </summary>
        internal ProductPartLink()
        {
        }

        /// <summary>
        /// Constructor for parts links that are read from database
        /// </summary>
        /// <param name="id"></param>
        internal ProductPartLink(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Unique id of this link object
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Parent product for this part link
        /// </summary>
        public IProduct Parent { get; set; }

        /// <summary>
        /// Generic product reference of this link
        /// </summary>
        public IProduct Product { get; set; }

        /// <summary>
        /// Create single article instance for this part
        /// </summary>
        public virtual Article Instantiate()
        {
            var article = Product.CreateInstance();
            ((IArticleParts)article).PartLinkId = Id;
            return article;
        }
    }

    /// <summary>
    /// Class to create generic part structure
    /// </summary>
    public class ProductPartLink<TProduct> : ProductPartLink, IProductPartLink<TProduct>
        where TProduct : class, IProduct
    {
        /// <summary>
        /// Default constructor for a new part link
        /// </summary>
        public ProductPartLink()
        {
        }

        /// <summary>
        /// Constructor for parts links that are read from database
        /// </summary>
        /// <param name="id"></param>
        public ProductPartLink(long id) : base(id)
        {
        }

        /// <summary>
        /// Reference to the generic product
        /// </summary>
        public new TProduct Product
        {
            get { return (TProduct)base.Product; }
            set { base.Product = value; }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Product.Type;
        }
    }
}