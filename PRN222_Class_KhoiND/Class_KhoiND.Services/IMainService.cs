using Class_KhoiND.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_KhoiND.Services
{
    public interface IMainService
    {
        // Item là tên entity  bảng chính

        Task<List<Item>> GetAllAsync();

        Task<Item> GetByIdAsync(int id);

        Task<List<Item>> SearchAsync(int x1, string? x2, string x3);

        Task<int> CreateAsync(Item bc);

        Task<int> UpdateAsync(Item bc);

        Task<bool> DeleteAsync(int id);
    }
}
