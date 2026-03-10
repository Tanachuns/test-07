using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text.RegularExpressions;

namespace products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ProductRepository repository = new(configuration);


    [HttpGet(Name = "Products")]
    public IActionResult Get()
    {
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            var products = repository.GetAll();
            response.IsSuccess = true;
            response.data = products;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess= false;
            response.Message = "Internal Error.";
            return StatusCode(500,response);
        }
    }

    [HttpPost(Name = "Product")]
    public async Task<IActionResult> Post(Product.Request request)
    {
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            string regexPattern = "^[A-Z0-9]{5}(-[A-Z0-9]{5}){5}$";
            if (string.IsNullOrEmpty(request.ProductCode)|| !Regex.Match(request.ProductCode,regexPattern).Success)
            {
                response.IsSuccess = false;
                response.Message = "Invalid Request.";
                return BadRequest(response);
            }

            Product.ProductEntity entity = new Product.ProductEntity(request.ProductCode);

            int effectedRow = await repository.Insert(entity);
            response.IsSuccess = true;
            response.Message = $"{effectedRow} Rows Effected;";
            return Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            response.IsSuccess= false;
            response.Message = "Internal Error.";
            return StatusCode(500,response);
        }
 
    }
}
