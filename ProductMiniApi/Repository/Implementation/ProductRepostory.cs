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
    }
}
