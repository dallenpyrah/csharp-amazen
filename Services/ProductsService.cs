using System;
using System.Collections.Generic;
using amazen.Models;
using amazen.Repositorys;

namespace amazen.Services
{
    public class ProductsService
    {

        private readonly ProductsRepository _repo;

        public ProductsService(ProductsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Product> GetAll()
        {
            return _repo.GetAll();
        }

        internal Product GetOne(int id)
        {
            Product product = _repo.GetOne(id);
            return product;
        }

        internal IEnumerable<Product> GetProductsByProfileId(string id)
        {
            return _repo.GetProductsByProfileId(id);
        }

        internal Product CreateOne(Product newProduct)
        {
            return _repo.CreateOne(newProduct);
        }

        internal Product EditOne(Product editProduct)
        {
            Product current = GetOne(editProduct.Id);
            if (current.CreatorId != editProduct.CreatorId)
            {
                throw new SystemException("Invalid edit permission");
            }
            else
            {
                current.Description = editProduct.Description != null ? editProduct.Description : current.Description;
                current.Name = editProduct.Name != null ? editProduct.Name : current.Name;
                current.Price = editProduct.Price != null ? editProduct.Price : current.Price;
                return _repo.EditOne(current);
            }
        }

        internal Product DeleteOne(int id, string userId)
        {
            Product prodToDelete = GetOne(id);
            if (prodToDelete.CreatorId != userId)
            {
                throw new SystemException("Invalid delete permission");
            }
            else
            {
                return _repo.DeleteOne(id);
            }
        }
    }
}