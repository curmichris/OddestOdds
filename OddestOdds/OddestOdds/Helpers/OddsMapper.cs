using OddestOdds.Service.DTOs;
using OddestOdds.Web.ViewModels.Admin;
using OddestOdds.Web.ViewModels.Punter;

namespace OddestOdds.Web.Helpers
{
    public static class OddsMapper
    {
        public static OddDto Map(AdminOddsViewModel viewModel)
        {
            return new OddDto()
            {
                Id = viewModel.Id,
                OddName = $"{viewModel.HomeTeamName} vs {viewModel.AwayTeamName}",
                HomeTeamName = viewModel.HomeTeamName,
                AwayTeamName = viewModel.AwayTeamName,
                OddValues = new OddValueDto()
                {
                    Id = viewModel.OddValues.Id,
                    HomeOddValue = viewModel.OddValues.HomeOddValue,
                    DrawOddValue = viewModel.OddValues.DrawOddValue,
                    AwayOddValue = viewModel.OddValues.AwayOddValue,
                }
            };
        }

        public static AdminOddsViewModel InverseMap(OddDto dto)
        {
            return new AdminOddsViewModel()
            {
                Id = dto.Id,
                OddsName = $"{dto.HomeTeamName} vs {dto.AwayTeamName}",
                HomeTeamName = dto.HomeTeamName,
                AwayTeamName = dto.AwayTeamName,
                OddValues = new AdminOddValuesViewModel()
                {
                    Id = dto.OddValues.Id,
                    HomeOddValue = dto.OddValues.HomeOddValue,
                    DrawOddValue = dto.OddValues.DrawOddValue,
                    AwayOddValue = dto.OddValues.AwayOddValue,
                }
            };
        }

        public static OddsViewModel InverseOddsViewModelMap(OddDto dto)
        {
            return new OddsViewModel()
            {
                OddsName = $"{dto.HomeTeamName} vs {dto.AwayTeamName}",
                HomeTeamName = dto.HomeTeamName,
                AwayTeamName = dto.AwayTeamName,
                OddValues = new OddValuesViewModel()
                {
                    HomeOddValue = dto.OddValues.HomeOddValue,
                    DrawOddValue = dto.OddValues.DrawOddValue,
                    AwayOddValue = dto.OddValues.AwayOddValue,
                }
            };
        }

    }
}