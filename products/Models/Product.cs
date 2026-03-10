using Npgsql;

public class Product
{
    public class Request 
    {
        public string? ProductCode {get;set;}
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