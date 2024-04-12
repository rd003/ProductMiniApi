using Microsoft.AspNetCore.Mvc;
using ProductMiniApi.Models.Domain;
using ProductMiniApi.Models.DTO;
using ProductMiniApi.Repository.Abastract;

namespace ProductMiniApi.Controllers
{
    // [Route("api/[controller]/{action}")]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IProductRepository _productRepo;
        public ProductController(IFileService fs, IProductRepository productRepo)
        {
            this._fileService = fs;
            this._productRepo = productRepo;
        }

        [HttpPost]
        public IActionResult Add([FromForm] Product model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    model.ProductImage = fileResult.Item2; // getting name of image
                }
                var productResult = _productRepo.Add(model);
                if (productResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Added successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding product";

                }
            }
            return Ok(status);

        }

        // eg. PUT: api/product/3 
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateModel productToUpdate)
        {
            try
            {
                if (id != productToUpdate.Id)
                    return StatusCode(StatusCodes.Status400BadRequest,
                    new Status
                    {
                        StatusCode = 400,
                        Message = "Id in url and form body does not match."
                    });
                var product = await _productRepo.FindByIdAsync(id);
                if (product == null)
                    return StatusCode(StatusCodes.Status404NotFound,
                     new Status
                     {
                         StatusCode = 404,
                         Message = $"product with id: {id} does not exists"
                     }
                     );

                if (productToUpdate.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(productToUpdate.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        productToUpdate.ProductImage = fileResult.Item2; // getting name of image
                    }
                }
                // convert ProductUpdateModel to ProductUpdate , you can use automapper here
                string oldImage = product.ProductImage;
                product.Id = productToUpdate.Id;
                product.ProductImage = productToUpdate.ProductImage;
                product.ProductName = productToUpdate.ProductName;
                await _productRepo.UpdateAsync(product);

                // if user is updating the image, then delete the old image.
                if (productToUpdate.ImageFile != null)
                {
                    await _fileService.DeleteImage(oldImage);
                }
                return Ok(new Status
                {
                    StatusCode = 200,
                    Message = "Updated successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Status
                {
                    StatusCode = 500,
                    Message = ex.Message
                });
            }

        }

        // eg. DELETE: api/product/3 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _productRepo.FindByIdAsync(id);
                if (product == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Status
                    {
                        StatusCode = 404,
                        Message = $"product with id: {id} does not exists"
                    });
                }
                await _productRepo.DeleteAsync(product);
                await _fileService.DeleteImage(product.ProductImage);

                return Ok(new Status
                {
                    StatusCode = 200,
                    Message = $"Product with id: {id} is deleted successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Status
                {
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        // GET: api/products
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productRepo.GetProducts();
                return Ok(products);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Status
                {
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

    }
}
