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
        private NpgsqlConnection _connection;
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=test_db;Username=postgres;Password=qwerty1";

        //Сразу открываем подключение
        public ViewModel()
        {
            _connection =
                new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        public async Task Create(Product product) //Добавление продукта
        {
            var command = new NpgsqlCommand("INSERT INTO product (name, cost) VALUES (@name, @cost)", _connection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("cost", product.Cost);
            await command.ExecuteNonQueryAsync();
        }
        public async Task Update(Product product) //Обновление продукта (его изменение)
        {
            var command = new NpgsqlCommand("UPDATE product SET name = @name, cost = @cost WHERE id =@id", _connection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("cost", product.Cost);
            command.Parameters.AddWithValue("id", product.Id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task Delete(Product product) // Удаление продукта
        {
            var command = new NpgsqlCommand("DELETE FROM product WHERE id = @id", _connection);
            command.Parameters.AddWithValue("id", product.Id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task<ObservableCollection<Product>> Read()  //Чтение таблицы в коллекцию
        {
            var products = new ObservableCollection<Product>();
            var command = new NpgsqlCommand("SELECT * FROM product", _connection);
            var reader = await command.ExecuteReaderAsync();

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

            await reader.CloseAsync();

            return products;
        }
    }
}
