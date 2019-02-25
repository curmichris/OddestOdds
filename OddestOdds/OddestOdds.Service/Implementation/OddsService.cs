using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OddestOdds.Core.Entities;
using OddestOdds.Core.Interfaces;
using OddestOdds.Infrastructure.Entities;
using OddestOdds.Service.DTOs;
using OddestOdds.Service.Interfaces;

namespace OddestOdds.Service.Implementation
{
    public class OddsService : IOddsService
    {
        private readonly IOddsRepository _oddsRepository;

        public OddsService(IOddsRepository oddsRepository)
        {
            _oddsRepository = oddsRepository;
        }

        public async Task<OddDto> GetByIdAsync(Guid id)
        {
            return Map(await _oddsRepository.GetByIdAsync<Odd>(id));
        }

        public async Task<OddDto> GetByIdAsync(Guid id, string[] includes)
        {
            return Map(await _oddsRepository.GetByIdAsync<Odd>(id, includes));
        }

        public async Task<List<OddDto>> ListAsync()
        {
            return (await _oddsRepository.ListAsync<Odd>()).Select(Map).ToList();
        }

        public async Task<List<OddDto>> ListAsync(string[] includes)
        {
            return (await _oddsRepository.ListAsync<Odd>(includes)).Select(Map).ToList();
        }

        public async Task<OddDto> AddAsync(OddDto dto)
        {
            return Map(await _oddsRepository.AddAsync(InverseMap(dto)));
        }

        public async Task<OddDto> UpdateAsync(OddDto dto)
        {
            var entity = InverseMap(dto);
            await _oddsRepository.UpdateAsync(entity);
            await _oddsRepository.UpdateAsync(entity.OddValues);
            return Map(entity);
        }

        public async Task<OddValueDto> UpdateAsync(OddValueDto dto)
        {
            await _oddsRepository.UpdateAsync(InverseMapOddValues(dto));
            return dto;
        }

        public async Task<OddDto> DeleteAsync(OddDto dto)
        {
            await DeleteAsync(dto.Id);
            return dto;
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _oddsRepository.DeleteAsync<Odd>(id);
            return id;
        }

        private OddDto Map(Odd entity)
        {
            return new OddDto()
            {
                Id = entity.Id,
                OddName = $"{entity.HomeTeamName} vs {entity.AwayTeamName}",
                HomeTeamName = entity.HomeTeamName,
                AwayTeamName = entity.AwayTeamName,
                OddValues = new OddValueDto()
                {
                    Id = entity.OddValues.Id,
                    HomeOddValue = entity.OddValues.HomeOddValue,
                    DrawOddValue = entity.OddValues.DrawOddValue,
                    AwayOddValue = entity.OddValues.AwayOddValue,
                }
            };
        }

        private Odd InverseMap(OddDto dto)
        {
            return new Odd()
            {
                Id = dto.Id,
                OddName = $"{dto.HomeTeamName} vs {dto.AwayTeamName}",
                HomeTeamName = dto.HomeTeamName,
                AwayTeamName = dto.AwayTeamName,
                OddValues = new OddValue()
                {
                    Id = dto.OddValues.Id,
                    HomeOddValue = dto.OddValues.HomeOddValue,
                    DrawOddValue = dto.OddValues.DrawOddValue,
                    AwayOddValue = dto.OddValues.AwayOddValue,
                }
            };
        }

        private OddValue InverseMapOddValues(OddValueDto dto)
        {
            return new OddValue()
            {
                Id = dto.Id,
                HomeOddValue = dto.HomeOddValue,
                DrawOddValue = dto.DrawOddValue,
                AwayOddValue = dto.AwayOddValue,
            };
        }
    }
}