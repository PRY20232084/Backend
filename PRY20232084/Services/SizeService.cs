using PRY20232084.Data.Interfaces;
using PRY20232084.Models.Entities;

namespace PRY20232084.Services
{
    public class SizeService
    {
        public readonly ISizeDA sizeDA;

        public SizeService(ISizeDA sizeDA)
        {
            this.sizeDA = sizeDA;
        }

        public async Task<List<ProductSize>> FetchSizes(string userId)
        {
            try
            {
                var productSizes = new List<ProductSize>();

                productSizes = await sizeDA.GetAllSizes(userId);

                return productSizes;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to fetch sizes", ex);
            }
        }
    }
}
