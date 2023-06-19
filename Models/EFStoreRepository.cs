namespace SklStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }
        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }
        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }

        public Product GetProductById(long id)
        {
            return context.Products.FirstOrDefault(p => p.ProductID == id);
        }

        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }
    }
}
