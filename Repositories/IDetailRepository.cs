using HelloApi.Models;
using HelloApi.Models.DTOs;

namespace MessageApi.Repositories
{
    public interface IDetailRepository
    {
        Task<Detail> AddDetailAsync(DetailCreateDto dto);
        Task<IEnumerable<Detail>> GetAllDetailsAsync();
        Task<Detail?> GetDetailByIdAsync(int id);
        Task<Detail?> UpdateDetailAsync(Detail detail);
        Task<bool> DeleteDetailAsync(int id);
    }
}
