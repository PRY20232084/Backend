using PRY20232084.Models.Entities;
using System.Drawing;

namespace PRY20232084.Data.Interfaces
{
    public interface ISizeDA
    {
        Task<List<ProductSize>> GetAllSizes(string userId);

        Task<ProductSize> GetSizeById(int id);

        Task<int> Insert(ProductSize productSize);

        Task<int> Update(ProductSize productSize);
    }
}
