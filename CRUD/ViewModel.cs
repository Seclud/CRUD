using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace CRUD
{
    public class ViewModel
    {
        private NpgsqlConnection connection;

        //Откроем подключение
        public ViewModel()
        {
            connection =
                new NpgsqlConnection("Host=localhost;Port=5432;Database=test_db;Username=postgres;Password=C4_Nyb5LD");
        }

        public async Task Create(Product product)
        {
            try
            { await connection.OpenAsync(); }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var command = new NpgsqlCommand("INSERT INTO product (name, cost) VALUES (@name, @cost)", connection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("cost", product.Cost);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }
        public async Task Update(Product product)
        {
            try
            { await connection.OpenAsync(); }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var command = new NpgsqlCommand("UPDATE product SET name = @name, cost = @cost WHERE id =@id", connection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("cost", product.Cost);
            command.Parameters.AddWithValue("id", product.Id);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task Delete(Product product)
        {
            try
            { await connection.OpenAsync(); }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var command = new NpgsqlCommand("DELETE FROM product WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", product.Id);
            await command.ExecuteNonQueryAsync();
            await connection.CloseAsync();
        }

        public async Task<ObservableCollection<Product>> Select() //Возвращает лист всех продуктов
        {
            // Обсервбал круто, потому что получаем нотификейшн сразу
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            try
            { await connection.OpenAsync(); }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            var command = new NpgsqlCommand("SELECT * FROM product", connection);
            var reader = await command.ExecuteReaderAsync();
            //Таблицу в лист
            while (await reader.ReadAsync())
            {
                var product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Cost = reader.GetDouble(2),
                };
                products.Add(product);
            }

            reader.Close();

            return products;
        }
    }
}
