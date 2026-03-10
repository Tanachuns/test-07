using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text.RegularExpressions;
using Serilog;

namespace products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ProductRepository repository = new(configuration);


    [HttpGet(Name = "Get All Products")]
    public IActionResult Get()
    {
        Log.Information("Get All Products");
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            var products = repository.GetAll();
            response.IsSuccess = true;
            response.data = products;
            
            Log.Information($"return {products.Count} Products");
            return Ok(response);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            response.IsSuccess= false;
            response.Message = "Internal Error.";
            return StatusCode(500,response);
        }
    }

    [HttpPost(Name = "Insert Product")]
    public async Task<IActionResult> Post(Product.Request request)
    {
        Log.Information("Insert Product");
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            Log.Information($"product code: {request.ProductCode}");
            if (!request.IsValid())
            {
                Log.Warning("Invalid Request.");
                response.IsSuccess = false;
                response.Message = "Invalid Request.";
                return BadRequest(response);
            }
            
            Product.ProductEntity entity = new Product.ProductEntity(request.ProductCode);
            List<Product.ProductEntity>  productEntities = repository.GetSingle(entity);
            if (productEntities.Count > 0)
            {
                Log.Warning("Product Already Exists");
                response.IsSuccess = false;
                response.Message = "Product Already Exists";
                return BadRequest(response);
            }
            
            int effectedRow = await repository.Insert(entity);
            response.IsSuccess = true;
            response.Message = $"{effectedRow} Rows Effected;";
            Log.Information(response.Message);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            response.IsSuccess= false;
            response.Message = "Internal Error.";
            return StatusCode(500,response);
        }
 
    }
    
    [HttpDelete(Name = "Delete Product")]
    public async Task<IActionResult> Delete(Product.Request request)
    {
        Log.Information("Delete Product");
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            Log.Information($"product code: {request.ProductCode}");
            if (!request.IsValid())
            {
                Log.Warning("Invalid Request.");
                response.IsSuccess = false;
                response.Message = "Invalid Request.";
                return BadRequest(response);
            }
            Product.ProductEntity entity = new Product.ProductEntity(request.ProductCode);
            List<Product.ProductEntity>  productEntities = repository.GetSingle(entity);
            if (productEntities.Count == 0)
            {
                Log.Warning("Product Not Found.");
                response.IsSuccess = false;
                response.Message = "Product Not Found.";
                return BadRequest(response);
            }
            int effectedRow = await repository.Delete(entity);
            response.IsSuccess = true;
            response.Message = $"{effectedRow} Rows Effected;";
            Log.Information(response.Message);
            return Ok(response);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            response.IsSuccess= false;
            response.Message = "Internal Error.";
            return StatusCode(500,response);
        }
    }
}
