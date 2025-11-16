using Microsoft.EntityFrameworkCore;
using ShopManagement.Data;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<List<string>> GetCategoriesAsync();
        Task<bool> ProductExistsAsync(string name, string category);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                Console.WriteLine($"CreateProductAsync started - Name: {product.Name}, Category: {product.Category}");
                _logger.LogInformation($"Creating product: {product.Name}");

                // بررسی تکراری نبودن محصول
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p =>
                        p.Name.ToLower() == product.Name.ToLower() &&
                        p.Category.ToLower() == product.Category.ToLower() &&
                        p.IsActive);

                if (existingProduct != null)
                {
                    Console.WriteLine($"Duplicate product found - ID: {existingProduct.Id}");
                    _logger.LogWarning($"Duplicate product found: {product.Name} in {product.Category}");
                    throw new InvalidOperationException("محصولی با این نام و دسته‌بندی قبلاً وجود دارد.");
                }

                Console.WriteLine("No duplicate found, setting dates...");
                product.CreatedDate = DateTime.Now;
                product.UpdatedDate = DateTime.Now;

                Console.WriteLine("Adding product to context...");
                _context.Products.Add(product);

                Console.WriteLine("Saving changes to database...");
                var result = await _context.SaveChangesAsync();

                Console.WriteLine($"SaveChanges completed, affected rows: {result}");
                Console.WriteLine($"Product saved with ID: {product.Id}");
                _logger.LogInformation($"Product created successfully with ID: {product.Id}");

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateProductAsync: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            try
            {
                Console.WriteLine($"UpdateProductAsync started - ID: {product.Id}");
                _logger.LogInformation($"Updating product: {product.Id}");

                // بررسی تکراری نبودن محصول (به جز خود محصول)
                var existingProduct = await _context.Products
                    .FirstOrDefaultAsync(p =>
                        p.Name.ToLower() == product.Name.ToLower() &&
                        p.Category.ToLower() == product.Category.ToLower() &&
                        p.Id != product.Id &&
                        p.IsActive);

                if (existingProduct != null)
                {
                    Console.WriteLine($"Duplicate product found during update - ID: {existingProduct.Id}");
                    throw new InvalidOperationException("محصولی با این نام و دسته‌بندی قبلاً وجود دارد.");
                }

                product.UpdatedDate = DateTime.Now;
                _context.Products.Update(product);

                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"Product updated successfully - ID: {product.Id}, affected rows: {result}");
                _logger.LogInformation($"Product updated successfully: {product.Id}");

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateProductAsync: {ex.Message}");
                _logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsActive = false;
                product.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> ProductExistsAsync(string name, string category)
        {
            return await _context.Products
                .AnyAsync(p =>
                    p.Name.ToLower() == name.ToLower() &&
                    p.Category.ToLower() == category.ToLower() &&
                    p.IsActive);
        }
    }
}