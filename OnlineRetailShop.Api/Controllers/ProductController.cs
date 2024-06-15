using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Domain.CustomValidation;
using OnlineRetailShop.Domain.DTO;
using OnlineRetailShop.Domain.Interfaces;
using OnlineRetailShop.Domain.Models;

namespace OnlineRetailShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }



        //GET : Get all Product 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();

            return Ok(products);
            //return Ok(_mapper.Map<ProductDto>products);
        }

        //GET : Get Product by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Product Details from Database
            var productDetail = await _productService.GetProductByIdAsync(id);

            if (productDetail == null)
            {
                return NotFound();
            }
            var productDTO = new ProductDto()
            {
                Id = productDetail.Id,
                Name = productDetail.Name,
                Price = productDetail.Price,
                Quantity = productDetail.Quantity

            };

            //var dto = _mapper.Map < ProductDto > productDetail;

            return Ok(productDTO);
            //return Ok(productDetail);
        }

        //POST: Add Product to the database
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productRequestDto)
        {
            //Map requested product dto to the domain model
            var createdProduct = _mapper.Map<Product>(productRequestDto);

            //Add product to the database
            createdProduct = await _productService.AddProductAsync(createdProduct);

            //Map Product to ProductDto
            var productDto = _mapper.Map<ProductDto>(createdProduct);


            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);           
        }

        

        //POST: Update Product Details by Id
        [HttpPost]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductRequestDto productRequestDto)
        {
            var product = _mapper.Map<Product>(productRequestDto);

            var updatedProduct = await _productService.UpdateProductAsync(id,product);
            if (updatedProduct == null)
                return NotFound();

            //updatedProduct = _mapper.Map < ProductDto > updatedProduct;

            return Ok(updatedProduct);
        }

        //DELETE : Delete product by Id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var product = await _productService.DeleteProductAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
    }
}
