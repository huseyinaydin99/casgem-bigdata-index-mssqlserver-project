using Casgem.BigData.Index.MSSQLServer.Project.DAL.DTOs;

namespace Casgem.BigData.Index.MSSQLServer.Project.Models
{
    public class ShowQueryResultView
    {
        public List<MaximumBrandAndModelDto> MaximumBrandAndModels { get; set; }
        public List<MinimumColorDto> MinimumColors { get; set; }
        public List<LicenceDateBrandModelDto> LicenceDateBrandModels { get; set; }
        public List<CityBrandDto> CityBrands { get; set; }
        public List<MaximumMotorVolumeDto> MaximumMotorVolumes { get; set; }
        public List<CityOfShiftTypeDto> CityOfAutomaticShiftTypes { get; set; }
        public List<CityOfShiftTypeDto> CityOfManuelShiftTypes { get; set; }
    }
}