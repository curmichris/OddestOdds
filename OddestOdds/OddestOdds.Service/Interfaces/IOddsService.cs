using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OddestOdds.Core.Shared;
using OddestOdds.Service.DTOs;

namespace OddestOdds.Service.Interfaces
{
    public interface IOddsService
    {
        Task<OddDto> GetByIdAsync(Guid id);
        Task<OddDto> GetByIdAsync(Guid id, string[] includes);
        Task<List<OddDto>> ListAsync();
        Task<List<OddDto>> ListAsync(string[] includes);
        Task<OddDto> AddAsync(OddDto dto);
        Task<OddDto> UpdateAsync(OddDto dto);
        Task<OddValueDto> UpdateAsync(OddValueDto dto);
        Task<OddDto> DeleteAsync(OddDto dto);
        Task<Guid> DeleteAsync(Guid id);
    }
}
