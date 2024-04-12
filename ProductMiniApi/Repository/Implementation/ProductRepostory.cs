using Microsoft.EntityFrameworkCore;
using ProductMiniApi.Models.Domain;
using ProductMiniApi.Repository.Abastract;

namespace ProductMiniApi.Repository.Implementation
{
	public class ProductRepostory : IProductRepository
	{
		private readonly DatabaseContext _context;
		public ProductRepostory(DatabaseContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<Product>> GetProducts()
		{
			return await _context.Product.AsNoTracking().ToListAsync();
		}

		public bool Add(Product model)
		{
			try
			{
				_context.Product.Add(model);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}

		public async Task DeleteAsync(Product product)
		{

			_context.Product.Remove(product);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Product product)
		{
			_context.Product.Update(product);
			await _context.SaveChangesAsync();
		}

		public async Task<Product?> FindByIdAsync(int id)
		{
			// var product = await _context.Product.FirstOrDefaultAsync(a => a.Id == id);
			var product = await _context.Product.FindAsync(id);
			return product;
		}
	}
}
