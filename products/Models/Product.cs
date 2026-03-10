using System.Text.RegularExpressions;
using Npgsql;

public class Product
{
    public class Request 
    {
        public string ProductCode {get;set;}
        public bool IsValid()
        {
            string regexPattern = "^[A-Z0-9]{5}(-[A-Z0-9]{5}){5}$";
            return (!string.IsNullOrEmpty(this.ProductCode) || Regex.Match(this.ProductCode, regexPattern).Success);
        }
    }

    public class ProductEntity
    {
        public string productcode {get;set;}
        public DateTime create_date {get;set;}

        public ProductEntity(string _productcode)
        {
            productcode = _productcode;
        }

        public ProductEntity(NpgsqlDataReader reader)
        {
            productcode = reader.GetString(0);
            create_date = reader.GetDateTime(1);
        }

        
    }
   
}