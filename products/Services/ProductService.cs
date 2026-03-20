using products.Interfaces;
using Serilog;

namespace products.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<BaseResponseModel> Create(Product.Request request)
    {
        BaseResponseModel response = new BaseResponseModel();
        Log.Information($"product code: {request.ProductCode}");
        if (!request.IsValid())
        {
            Log.Warning("Invalid Request.");
            response.IsSuccess = false;
            response.Message = "Invalid Request.";
            return response;
        }
            
        Product.ProductEntity entity = new Product.ProductEntity(request.ProductCode);
        List<Product.ProductEntity>  productEntities = repository.GetSingle(entity);
        if (productEntities.Count > 0)
        {
            Log.Warning("Product Already Exists");
            response.IsSuccess = false;
            response.Message = "Product Already Exists";
            return response;
        }
            
        int effectedRow = await repository.Insert(entity);
        response.IsSuccess = true;
        response.Message = $"{effectedRow} Rows Effected;";
        Log.Information(response.Message);
        return response;
    }

    public BaseResponseModel GetAll()
    {
        BaseResponseModel response = new BaseResponseModel();
        var products = repository.GetAll();
        response.IsSuccess = true;
        response.Data = products;
        Log.Information($"return {products.Count} Products");
        return response;
    }

    public async Task<BaseResponseModel> DeleteSingle(Product.Request request)
    {
        BaseResponseModel response = new BaseResponseModel();
        Log.Information($"product code: {request.ProductCode}");
        if (!request.IsValid())
        {
            Log.Warning("Invalid Request.");
            response.IsSuccess = false;
            response.Message = "Invalid Request.";
            return response;
        }
        Product.ProductEntity entity = new Product.ProductEntity(request.ProductCode);
        List<Product.ProductEntity>  productEntities = repository.GetSingle(entity);
        if (productEntities.Count == 0)
        {
            Log.Warning("Product Not Found.");
            response.IsSuccess = false;
            response.Message = "Product Not Found.";
            return response;
        }
        int effectedRow = await repository.Delete(entity);
        response.IsSuccess = true;
        response.Message = $"{effectedRow} Rows Effected;";
        Log.Information(response.Message);
        return response;
    }
}
