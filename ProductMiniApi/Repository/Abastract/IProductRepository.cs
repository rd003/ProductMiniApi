using ProductMiniApi.Models.Domain;

namespace ProductMiniApi.Repository.Abastract
{
    public interface IProductRepository
    {
        bool Add(Product model);
    }
}
