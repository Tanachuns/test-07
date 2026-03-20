namespace products.Interfaces;

public interface IProductRepository
{
    public List<Product.ProductEntity> GetAll();
    public List<Product.ProductEntity> GetSingle(Product.ProductEntity productEntity);
    public Task<int> Insert(Product.ProductEntity productEntity);
    public Task<int> Delete(Product.ProductEntity productEntity);
}