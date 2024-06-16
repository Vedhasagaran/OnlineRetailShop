using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineRetailShop.Application.Interfaces;
using OnlineRetailShop.Utilities.CustomValidation;
using OnlineRetailShop.Domain.DTO;

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
            
            return Ok(productDetail);
        }

        //POST: Add Product to the database
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productRequestDto)
        { 
            //Add product to the database
            var productDto = await _productService.AddProductAsync(productRequestDto);           

            return CreatedAtAction(nameof(GetById), new { id = productDto.Id }, productDto);           
        }        

        //POST: Update Product Details by Id
        [HttpPost]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductRequestDto productRequestDto)
        {   
            var updatedProduct = await _productService.UpdateProductAsync(id, productRequestDto);

            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }

        //DELETE : Delete product by Id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _productService.DeleteProductAsync(id);          
            return NoContent();
        }
    }
}
