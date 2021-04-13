using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using amazen.Models;
using Dapper;

namespace amazen.Repositorys
{
    public class ProductsRepository
    {

        public readonly IDbConnection _db;

        public ProductsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Product> GetAll()
        {
            string sql = @"SELECT
            p.*,
            prof.*
            FROM products p
            JOIN profiles prof ON p.creatorId = prof.id;";
            return _db.Query<Product, Profile, Product>(sql, (product, profile) =>
            {
                product.Creator = profile;
                return product;
            }, splitOn: "id");
        }

        internal Product GetOne(int id)
        {
            string sql = @"SELECT
            p.*,
            prof.*
            FROM products p 
            JOIN profiles prof ON p.creatorId = prof.id
            WHERE p.id = @id;";
            return _db.Query<Product, Profile, Product>(sql, (product, profile) =>
            {
                product.Creator = profile;
                return product;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal IEnumerable<Product> GetProductsByProfileId(string id)
        {
            string sql = @"SELECT 
            p.*
            prof.*
            FROM products p 
            JOIN profiles prof ON p.creatorId = id
            WHERE creatorId = id;
            ";
            return _db.Query<Product, Profile, Product>(sql, (product, profile) =>
            {
                product.Creator = profile;
                return product;
            }, new { id }, splitOn: "id");
        }

        internal Product CreateOne(Product newProduct)
        {
            string sql = @"INSERT INTO products
            (name, description, price, creatorId)
            VALUES
            (@Name, @Description, @Price, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newProduct);
            newProduct.Id = id;
            return newProduct;
        }

        internal Product EditOne(Product current)
        {
            string sql = @"UPDATE products
            SET
                name = @Name,
                description = @Description,
                price = @Price
                WHERE id = @id;
            SELECT * FROM products WHERE id = @id;";
            return _db.QueryFirstOrDefault<Product>(sql, current);
        }

        internal Product DeleteOne(int id)
        {
            string sql = "DELETE FROM products WHERE id = @id LIMIT 1";
            return _db.QueryFirstOrDefault<Product>(sql, new { id });
        }
    }
}