using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Data.Interfaces;
using PRY20232084.Models.Entities;
using System.Drawing;
using System.Text.RegularExpressions;

namespace PRY20232084.Data.DataAcess
{
    public class SizeDA : ISizeDA
    {
        public readonly ApplicationDbContext Db;

        public SizeDA(ApplicationDbContext Db)
        {
            this.Db = Db;
        }

        public async Task<List<ProductSize>> GetAllSizes(string userId)
        {
            try
            {
                var sizes = await Db.Sizes.Where(x => x.CreatedBy == userId).ToListAsync();

                return sizes;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to list process sizes", ex);
            }
        }

        public async Task<ProductSize> GetSizeById(int id)
        {
            try
            {
                return await Db.Sizes.Where(x => x.ID == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get size", ex);
            }
        }

        public async Task<int> Insert(ProductSize productSize)
        {
            try
            {
                Db.Sizes.Add(productSize);
                Db.SaveChanges();
                return productSize.ID;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create size", ex);
            }
        }

        public async Task<int> Update(ProductSize productSize)
        {
            try
            {
                var oldProductSize = Db.Sizes.FirstOrDefault(x => x.ID == productSize.ID);
                oldProductSize.Name = productSize.Name;
                Db.SaveChanges();
                return oldProductSize.ID;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update size", ex);
            }
        }
    }
}