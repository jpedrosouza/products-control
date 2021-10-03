using System.Collections.Generic;
using ProductsControl.Models;
using ProductsControl.Config;
using MySql.Data.MySqlClient;

namespace ProductsControl.Controllers
{
    class ProductsController
    {
       
       /// <summary>
       /// Obtem tdos os produtos da tabela products.
       /// </summary>
       /// <returns>
       /// Um array de produtos
       /// </returns>

       public List<Product> index()
        {

            MySqlConnection mySqlConnection = new DatabaseConfig().connectDatabase();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            mySqlCommand.CommandText = "SELECT * FROM products";

            MySqlDataReader mySqlReader = mySqlCommand.ExecuteReader();

            List<Product> products = new List<Product>();

            while(mySqlReader.Read())
            {
                Product product = new Product();

                product.Id = int.Parse(mySqlReader.GetString(0));
                product.Name = mySqlReader.GetString(1);
                product.Ean = mySqlReader.GetString(2);
                product.Price = double.Parse(mySqlReader.GetString(3));
                product.StockQuantity = int.Parse(mySqlReader.GetString(4));

                products.Add(product);
            }


            return products;
        }

        /// <summary>
        /// Armazena um produto na tabela products
        /// </summary>
        /// <param name="product"></param>

        public void store(Product product)
        {
            MySqlConnection mySqlConnection = new DatabaseConfig().connectDatabase();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            mySqlCommand.CommandText = $"INSERT INTO products (name, ean, price, stock_quantity) VALUES('{product.Name}', '{product.Ean}', {product.Price}, {product.StockQuantity})";
            mySqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Atualiza um produto na tabela products
        /// </summary>
        /// <param name="product"></param>

        public void update(Product product)
        {
            MySqlConnection mySqlConnection = new DatabaseConfig().connectDatabase();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            mySqlCommand.CommandText = $"UPDATE products SET name = '{product.Name}', ean = '{product.Ean}', price = {product.Price}, stock_quantity = {product.StockQuantity} WHERE id = {product.Id}";
            mySqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Deleta um produto da tabela products
        /// </summary>
        /// <param name="productId"></param>

        public void delete(int productId)
        {
            MySqlConnection mySqlConnection = new DatabaseConfig().connectDatabase();
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            mySqlCommand.CommandText = $"DELETE FROM products WHERE id = {productId}";
            mySqlCommand.ExecuteNonQuery();
        }
    }
}
