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
        private  NpgsqlConnection connection;
        
        //Откроем подключение
        public ViewModel()
        {
            connection =
                new NpgsqlConnection("Host=localhost;Port=5432;Database=test_db;Username=postgres;Password=C4_Nyb5LD");
            connection.Open();

        }

        public void Create(Product product)
        {
            var command = new NpgsqlCommand("INSERT INTO product (name, cost) VALUES (@name, @cost)",
                connection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("cost", product.Cost);
            command.ExecuteNonQuery();
        }

       /* public Product Read(int id)
        {
            var command = new NpgsqlCommand("SELECT * FROM product WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", @id);
            var reader = command.ExecuteReader();
            reader.Read();
            return new Product()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Cost = reader.GetDouble(2),
            };

        }*/

        public void Update(Product product)
        {
            var command = new NpgsqlCommand("UPDATE product SET name = @name, cost = @cost WHERE id =@id", connection);
            command.Parameters.AddWithValue("name", product.Name);
            command.Parameters.AddWithValue("cost", product.Cost);
            command.Parameters.AddWithValue("id", product.Id);
            command.ExecuteNonQuery();
        }

        public void Delete(Product product)
        {
            var command = new NpgsqlCommand("DELETE FROM product WHERE id = @id", connection);
            command.Parameters.AddWithValue("id", product.Id);
            command.ExecuteNonQuery();
        }

        public ObservableCollection<Product> Select() //Возвращает лист всех продуктов
        {
            // Обсервбал круто, потому что получаем нотификейшн сразу
            ObservableCollection<Product> products = new ObservableCollection<Product>();

            var command = new NpgsqlCommand("SELECT * FROM product", connection);
            var reader = command.ExecuteReader();
            //Таблицу в лист
            while (reader.Read())
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
