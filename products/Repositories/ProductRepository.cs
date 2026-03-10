
using Microsoft.AspNetCore.SignalR;
using Npgsql;

public class ProductRepository
{
    private string connectionString;
    public ProductRepository(IConfiguration configuration)
    {
        var conn = configuration?.GetConnectionString("pgpool");

        if (string.IsNullOrEmpty(conn))
        {
            throw new Exception("invalid appsettings");
        }
        connectionString = conn;
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

    public async Task<int> Insert(Product.ProductEntity productEntity)
    {
        int effectedRow = 0;
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            string sql = $@"INSERT INTO products (productcode) VALUES (@productcode);";
            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@productcode", productEntity.productcode);
            effectedRow = await cmd.ExecuteNonQueryAsync();
        }
        return effectedRow;
    }
    
}