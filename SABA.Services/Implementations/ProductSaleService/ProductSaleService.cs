﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using SABA.Core.Abstractions;
using SABA.Core.Models.ProductModel;
using SABA.Core.Models.SaleModel;
using SABA.Services.Abstractiuons.ProductSaleService;
using SABA.Services.Models.RequestModels.Products;
using SABA.Services.Models.RequestModels.Sales;
using SABA.Services.Models.ResponseModels.AddProductRes;
using SABA.Services.Models.ResponseModels.ProductsAndSales;
using SABA.Services.Models.ResponseModels.Sales;

namespace SABA.Services.Implementations.ProductSaleService
{
    public class ProductSaleService : IProductSaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductSaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UpdateProductResponse> UpdateProduct(ProductDto entity, int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if (product != null)
            {
                var productToUpdate = _mapper.Map(entity, product);
                _unitOfWork.Products.Update(productToUpdate);
                await _unitOfWork.SaveAsync();
                return new UpdateProductResponse { Message = "პროდუქტი წარმატებით დააბდეითდა", StatusCode = StatusCodes.Status200OK };
            }
            return new UpdateProductResponse { Message = $"პროდუქტი აიდით{product.ProductId} ვერ მოიძებნა ", StatusCode = StatusCodes.Status200OK };
        }

        public async Task<GetProductsResponse> GetById(int productId)
        {
            var product = await _unitOfWork.Products.GetById(productId);

            if (product == null)
            {
                return new GetProductsResponse { Message = $"პროდუქტი აიდით {productId} ვერ მოიძებნა ", StatusCode = StatusCodes.Status404NotFound };
            }
            else
            {
                var productDto = _mapper.Map<ProductResponseDto>(product);

                var productList = new List<ProductResponseDto> { productDto };

                return new GetProductsResponse
                {
                    Products = productList,
                    Message = "პროდუქტი წარმატებით მოიძებნა",
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public async Task<AddProductResponse> AddProduct(ProductDto entity)
        {
            Product productToInsert = _mapper.Map<Product>(entity);
            if (entity.Images != null)
            {
                foreach (var formFile in entity.Images)
                {
                    using var memoryStream = new MemoryStream();
                    await formFile.CopyToAsync(memoryStream);

                    productToInsert.Images.Add(new ProductImage
                    {
                        ImageData = memoryStream.ToArray(),
                        ContentType = formFile.ContentType,
                        FileName = formFile.FileName
                    });
                }
            }

            await _unitOfWork.Products.Add(productToInsert);
            await _unitOfWork.SaveAsync();
            return new AddProductResponse
            {
                Message = "Product Inserted successfully!",
                StatusCode = StatusCodes.Status200OK
            };
        }
        public async Task<GetProductsResponse> GetProducts()
        {
            var result = await _unitOfWork.Products.GetAllAsync();
            var productToReturn = _mapper.Map<List<ProductResponseDto>>(result);
            if (result != null)
            {
                return new GetProductsResponse
                {
                    Products = productToReturn,
                    Message = "Products Returned successfully!",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            return new GetProductsResponse
            {
                Products = productToReturn,
                Message = $"{nameof(productToReturn)}",
                StatusCode = StatusCodes.Status404NotFound
            };
        }
        public async Task<CreateSaleResponse> AddSale(SaleDto entity)
        {
            var saleToCreate = _mapper.Map<Sale>(entity);
            if (saleToCreate.SoldProducts == null)
            {
                saleToCreate.SoldProducts = new List<Product>();
            }
            await _unitOfWork.Sales.Add(saleToCreate);
            await _unitOfWork.SaveAsync();
            return new CreateSaleResponse
            {
                Message = "Sale Created successfully!",
                StatusCode = StatusCodes.Status200OK,
            };
        }
        public async Task<GetSaleResponse> GetSalesByUserId(int userId, DateTime startDate, DateTime endDate)
        {
            var result = await _unitOfWork.Sales
                .FindAsync(s => s.UserId == userId && s.SaleDate >= startDate && s.SaleDate <= endDate);
            var salesToReturn = _mapper.Map<List<SaleDto>>(result);
            if (result != null)
            {
                return new GetSaleResponse
                {
                    Message = "Sales Returned Successfully!",
                    StatusCode = StatusCodes.Status200OK,
                    Sales = salesToReturn
                };
            }

            return new GetSaleResponse
            {
                Message = "Sales Returned Successfully!",
                StatusCode = StatusCodes.Status404NotFound,
                Sales = salesToReturn
            };

        }
    }
}
