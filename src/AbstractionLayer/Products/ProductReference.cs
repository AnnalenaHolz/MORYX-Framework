using System;
using Marvin.AbstractionLayer.Identity;

namespace Marvin.AbstractionLayer
{
    /// <summary>
    /// Temporary product reference to be replaced by the product management
    /// </summary>
    public class ProductReference : IProduct
    {
        ///
        public const string TypeName = nameof(ProductReference);

        /// <summary>
        /// Unique type name of this instance
        /// </summary>
        public string Type => TypeName;

        /// <summary>
        /// Create a reference product by giving an id
        /// </summary>
        public ProductReference(long id)
        {
            Id = id;
        }

        /// <summary>
        /// Create a refernce product by giving an identity
        /// </summary>
        /// <param name="identity">Identity information of this ProductReference</param>
        public ProductReference(IIdentity identity)
        {
            Identity = identity;
        }

        /// <summary>
        /// Unique id of this product
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Display name of this product
        /// </summary>
        public string Name { get; set; }

        /// <inheritdoc />
        public ProductTypeState State { get; set; }

        /// <summary>
        /// Reference does not have a parent
        /// </summary>
        public IProduct Parent { get; }

        /// <summary>
        /// Reference does not have a parent
        /// </summary>
        public IProductPartLink ParentLink { get; }

        /// <summary>
        /// Identity of this product
        /// </summary>
        public IIdentity Identity { get; set; }

        /// <summary>
        /// Create article instance of this type
        /// </summary>
        public Article CreateInstance()
        {
            throw new InvalidOperationException("Reference products can not be instantiated. Please replace with a real product first!");
        }
    }
}
