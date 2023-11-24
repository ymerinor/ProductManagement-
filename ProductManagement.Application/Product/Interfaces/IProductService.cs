﻿using ProductManagement.Domain.Product;

namespace ProductManagement.Application.Product.Interfaces
{
    public interface IProductService
    {
        Task<Products> GetByIdAsync(int v);
    }
}