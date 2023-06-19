namespace SklStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product p);
        void CreateProduct(Product p);
        void DeleteProduct(Product p);

        Product GetProductById(long id);
        List<Product> GetProducts();
    }
}
