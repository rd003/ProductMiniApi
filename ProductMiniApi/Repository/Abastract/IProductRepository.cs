using ProductMiniApi.Models.Domain;

namespace ProductMiniApi.Repository.Abastract
{
    public interface IProductRepository
    {
        // Sorry, I did not created it as a async method. It should have async method. 
        bool Add(Product model);

        Task<Product?> FindByIdAsync(int id);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
