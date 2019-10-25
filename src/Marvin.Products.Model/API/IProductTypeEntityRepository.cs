﻿using Marvin.Model;

namespace Marvin.Products.Model
{
    /// <summary>
    /// The public API of the ProductEntity repository.
    /// </summary>
    public interface IProductTypeEntityRepository : IRepository<ProductTypeEntity>
    {
        /// <summary>
        /// Creates instance with all not nullable properties prefilled
        /// </summary>
        ProductTypeEntity Create(string identifier, short revision, string name, string typeName);

        /// <summary>
        /// This method returns the first matching ProductEntity for given parameters
        /// </summary>
        /// <param name="identifier">Value for MaterialNumber the ProductEntity has to match</param>
        /// <param name="revision">Value for Revision the ProductEntity has to match</param>
        ProductTypeEntity GetByIdentity(string identifier, short revision);
    }
}
