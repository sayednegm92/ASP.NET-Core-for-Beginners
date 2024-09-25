using FirstApp.Data;
using FirstApp.DTO;
using FirstApp.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Services
{
    public class ProductService: IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var records = await _context.Set<Product>().ToListAsync();
            return records;
        }
        public async Task<Product> GetProductById(int id)
        {
            var record = await _context.Set<Product>().FindAsync(id);
            return record;
        }
        public async Task<ProductReturn> AddProduct(Product product)
        {
            product.Id = 0;
             _context.Set<Product>().Add(product);
            await _context.SaveChangesAsync();
            return new ProductReturn { 
                Id=product.Id,
                Message= "Product Created Successfully"
            };
        }

        public async Task<ProductReturn> EditProduct(Product product)
        {
          var existingProduct=_context.Set<Product>().Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Sku = product.Sku;
                _context.Set<Product>().Update(existingProduct);
                await _context.SaveChangesAsync();
                return new ProductReturn { Message = "Product Updated Successfully" };
            }
            else
            {
                return new ProductReturn
                {
                    Message = "Sorry Something Wrong, Product not Updated"
                };
            }
        }

        public async Task<ProductReturn> RemoveProduct(int id)
        {
            var existingProduct = _context.Set<Product>().Find(id);
            if (existingProduct != null)
            {
                _context.Set<Product>().Remove(existingProduct);
                await _context.SaveChangesAsync();
                return new ProductReturn { 
                    Id=existingProduct.Id,  
                    Message = "Product Deleted  Successfully" 
                };
            }
            else
            {
                return new ProductReturn
                {
                    Message = "Sorry Something Wrong, Product not Delete"
                };
            }
        }
    }
}
