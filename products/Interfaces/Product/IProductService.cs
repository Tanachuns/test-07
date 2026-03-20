namespace products.Interfaces;

public interface IProductService
{
    public BaseResponseModel GetAll();
    public Task<BaseResponseModel> Create(Product.Request request);
    public Task<BaseResponseModel> DeleteSingle(Product.Request request);
}