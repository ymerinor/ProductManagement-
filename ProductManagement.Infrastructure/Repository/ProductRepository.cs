﻿using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;
using ProductManagement.Infrastructure.Exceptions;

namespace ProductManagement.Infrastructure.Repository
{
    /// <inheritdoc/>
    public class ProductRepository : IProductRepository
    {
        private readonly ProductManagementDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProductRepository(ProductManagementDbContext context)
        {
            _context = context;
        }
        /// <inheritdoc/>
        public async Task<Products> CreateAsync(Products products)
        {
            await _context.Products.AddAsync(products);
            await Commit();
            return products;
        }

        /// <inheritdoc/>
        public async Task<Products> GetByIdAsync(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(t => t.ProductId == productId);
        }
        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(int productId)
        {
            var productRemove = await _context.Products.Where(t => t.ProductId == productId).FirstOrDefaultAsync();
            if (productRemove != null)
            {
                _context.Products.Remove(productRemove);
                await Commit();
                return true;
            }
            return false;
        }

        /// <inheritdoc/>
        public async Task<Products> UpdateAsync(int productId, Products productCreateTest)
        {
            var itemProduct = await _context.Products.Where(t => t.ProductId == productId).FirstOrDefaultAsync();
            if (itemProduct is null)
            {
                throw new NoContentException("No existe informacion relacionada con el producto");
            }
            else
            {
                itemProduct.Name = productCreateTest.Name;
                itemProduct.Price = productCreateTest.Price;
                itemProduct.Discount = productCreateTest.Discount;
                _context.Entry(itemProduct).State = EntityState.Modified;
                await Commit();
            }
            return itemProduct;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
