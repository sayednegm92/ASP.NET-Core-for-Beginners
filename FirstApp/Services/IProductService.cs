using FirstApp.DTO;
using FirstApp.Model;


namespace FirstApp.Services
{
    public interface IProductService
    {
        Task<ProductReturn> AddProduct(Product product);
        Task<ProductReturn> EditProduct(Product product);
        Task<ProductReturn> RemoveProduct(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
    }
}
