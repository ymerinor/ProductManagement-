﻿using ProductManagement.Application.Common.Exeptions;
using ProductManagement.Application.Product.Dto;
using ProductManagement.Application.Product.Interfaces;
using ProductManagement.Domain.Core;
using ProductManagement.Domain.ExternalServices;
using ProductManagement.Domain.Product;
using ProductManagement.Domain.Repository.Interface;

namespace ProductManagement.Application.Product.Services
{
    /// <inheritdoc/>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Dependencia de IProductRepository
        /// </summary>
        public readonly IProductRepository _productRepository;

        /// <summary>
        /// Dependencia de IProductApiClient
        /// </summary>
        private readonly IProductApiClient _productApiClient;

        /// <summary>
        ///  Dependencia de IProductStatusCache
        /// </summary>
        private readonly IProductStatusCache _productStatusCache;

        /// <summary>
        /// inicializador de clase <class>ProductService</class>
        /// </summary>
        /// <param name="productRepository">IProductRepository</param>
        /// <param name="productApiClient">IProductApiClient</param>
        /// <param name="productStatusCache">IProductStatusCache</param>
        public ProductService(IProductRepository productRepository, IProductApiClient productApiClient, IProductStatusCache productStatusCache)
        {
            _productRepository = productRepository;
            _productApiClient = productApiClient;
            _productStatusCache = productStatusCache;
        }
        /// <inheritdoc/>
        public async Task<Products> CreateAsync(ProductsRequestDto productsRequestDto)
        {
            var product = (Products)productsRequestDto;
            product.StatusName = GetStatusName(productsRequestDto.Status);
            var productRegister = await _productRepository.CreateAsync(product);
            var discount = await _productApiClient.GetDataItemAsync(productRegister.ProductId);
            product.FinalPrice = productsRequestDto.Price * (100 - discount.Discount) / 100;
            product.Discount = discount.Discount;
            await _productRepository.Commit();
            return productRegister;
        }
        /// <inheritdoc/>
        public async Task<ProductsDto> GetByIdAsync(int productId)
        {
            var productInfomation = await ProductExists(productId);
            return (ProductsDto)productInfomation;
        }
        /// <inheritdoc/>
        public async Task<Products> UpdateAsync(int productId, ProductsRequestDto productsRequestDto)
        {
            await ProductExists(productId);
            var discount = await _productApiClient.GetDataItemAsync(productId);
            var productUpdate = (Products)productsRequestDto;
            productUpdate.StatusName = GetStatusName(productsRequestDto.Status);
            productUpdate.FinalPrice = productsRequestDto.Price * (100 - discount.Discount) / 100;
            productUpdate.Discount = discount.Discount;
            return await _productRepository.UpdateAsync(productId, productUpdate);
        }
        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(int productId)
        {
            await ProductExists(productId);
            return await _productRepository.RemoveAsync(productId);
        }
        /// <summary>
        /// Realiza la validacion si un producto consulta existe registrado o no
        /// </summary>
        /// <param name="productId">id de producto a consultar</param>
        /// <returns></returns>
        /// <exception cref="NoContentException">se lanza exepcion en caso de no existir registro creado</exception>
        protected async Task<Products> ProductExists(int productId)
        {
            var productInfomation = await _productRepository.GetByIdAsync(productId);
            if (productInfomation is null)
            {
                throw new NoContentException("No existe informacion relacionados con el producto");
            }
            else
            {
                return productInfomation;
            }
        }

        /// <summary>
        /// Recupera el valor del estado seleccinado segun su KEY 
        /// </summary>
        /// <param name="status">valor de estado a consultar</param>
        /// <returns>nombre de estado</returns>
        protected string GetStatusName(int status)
        {
            // Obtener la información del caché
            var cacheInformation = _productStatusCache.GetProductStatus();

            // Asignar el valor de StatusName desde la información del caché
            if (cacheInformation.TryGetValue(status, out var statusName))
            {
                return statusName;
            }
            return string.Empty;
        }
    }
}
