using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text.RegularExpressions;

namespace products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ProductRepository repository;
    public ProductController(IConfiguration configuration)
    {
        _configuration = configuration;
        repository = new ProductRepository(configuration);
    }


    [HttpGet(Name = "Products")]
    public IActionResult Get()
    {
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            var products = repository.GetAll();
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
    public IActionResult Post(Product.Request request)
    {
        BaseResponseModel response = new BaseResponseModel();
        try
        {
            string regexpattern = "^[A-Za-z0-9]{5}(-[A-Za-z0-9]{5}){4}$";
            if (string.IsNullOrEmpty(request.ProductCode)|| !Regex.Match(request.ProductCode,regexpattern).Success)
            {
                response.IsSuccess = false;
                response.Message = "Invalid Request.";
                return BadRequest(response);
            }

            
            

            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccess= false;
            response.Message = "Internal Error.";
            return StatusCode(500,response);
        }
 
    }
}
