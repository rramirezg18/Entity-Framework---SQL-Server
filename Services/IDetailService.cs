using HelloApi.Models.DTOs;

namespace HelloApi.Services
{
    public interface IDetailService
    {
        Task<DetailReadDto> CreateDetailAsync(DetailCreateDto dto);
        Task<IEnumerable<DetailReadDto>> GetAllDetailsAsync();
        Task<DetailReadDto?> GetDetailByIdAsync(int id);
        Task<DetailReadDto?> UpdateDetailAsync(int id, DetailUpdateDto dto);
        Task<bool> DeleteDetailAsync(int id);
    }
}
