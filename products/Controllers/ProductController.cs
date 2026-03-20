using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text.RegularExpressions;
using products.Interfaces;
using products.Services;
using Serilog;

namespace products.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{

    [HttpGet(Name = "Get All Products")]
    public IActionResult Get()
    {
        Log.Information("Get All Products");
        BaseResponseModel result = new  BaseResponseModel();
        try
        {
            result  =  productService.GetAll();
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            result.IsSuccess= false;
            result.Message = "Internal Error.";
            return StatusCode(500,result);
        }
    }

    [HttpPost(Name = "Insert Product")]
    public async Task<IActionResult> Post(Product.Request request)
    {
        Log.Information("Insert Product");
        BaseResponseModel result = new  BaseResponseModel();
        try
        {
            result  = await productService.Create(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            result.IsSuccess= false;
            result.Message = "Internal Error.";
            return StatusCode(500,result);
        }
 
    }
    
    [HttpDelete(Name = "Delete Product")]
    public async Task<IActionResult> Delete(Product.Request request)
    {
        Log.Information("Delete Product");
        BaseResponseModel result = new  BaseResponseModel();
        try
        {
            result  = await productService.DeleteSingle(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            result.IsSuccess= false;
            result.Message = "Internal Error.";
            return StatusCode(500,result);
        }
    }
}
