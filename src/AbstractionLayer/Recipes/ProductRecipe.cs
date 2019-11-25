namespace Marvin.AbstractionLayer
{
    /// <summary>
    /// Recipe to create instances of a product
    /// </summary>
    public class ProductRecipe : Recipe, IProductRecipe
    {
        /// <summary>
        /// Create a new product recipe
        /// </summary>
        public ProductRecipe()
        {
        }

        /// <summary>
        /// Clone a product recipe
        /// </summary>
        protected ProductRecipe(ProductRecipe source)
            : base(source)
        {
            Product = source.Product;
        }

        /// <inheritdoc />
        public IProductType Product { get; set; }

        /// <inheritdoc />
        public virtual IProductType Target => Product;

        /// <inheritdoc />
        public override IRecipe Clone()
        {
            return new ProductRecipe(this);
        }
    }
}