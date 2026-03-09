
using Microsoft.AspNetCore.SignalR;
using Npgsql;

public class ProductRepository
{
    private string connectionString;
    public ProductRepository(IConfiguration configuration)
    {
           connectionString = configuration.GetConnectionString("pgpool");
    }

    public List<Product.ProductEntity> GetAll()
    {
        List<Product.ProductEntity> entities = new ();
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            string sql = "SELECT * from products";

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);

            conn.Open();
            NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                entities.Add(new Product.ProductEntity(reader));
            }
        }

        return entities;
    }

    
}