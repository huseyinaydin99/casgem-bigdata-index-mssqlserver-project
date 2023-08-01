using Casgem.BigData.Index.MSSQLServer.Project.DAL.Constant;
using Casgem.BigData.Index.MSSQLServer.Project.DAL.DTOs;
using Casgem.BigData.Index.MSSQLServer.Project.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Casgem.BigData.Index.MSSQLServer.Project.Controllers
{
	public class CarPlatesController : Controller
	{
		private static SqlConnection CONNECTION;

		public CarPlatesController()
		{
			CONNECTION = Constans.GetConnection();
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Index_2()
		{
			/*IEnumerable<MaximumBrandAndModelDto> val1 = new List<MaximumBrandAndModelDto>();
            Thread thread1 = new Thread(() => { val1 = GetMaximumBrandAndModel(); });
            thread1.Start();
            thread1.Join();*/

			var query1 = "SELECT TOP(1) COUNT(BRAND) AS MARKA_SAYISI, BRAND, MODEL \r\nFROM PLATES\r\nGROUP BY BRAND, MODEL\r\nORDER BY COUNT(BRAND) DESC, COUNT(MODEL) DESC;";
			var result1 = (List<MaximumBrandAndModelDto>)CONNECTION.Query<MaximumBrandAndModelDto>(query1);  //liste olmayacak!

			/*IEnumerable<MinimumColorDto> val2 = new List<MinimumColorDto>();
            Thread thread2 = new Thread(() => { val2 = GetMinimumColor(); });
            thread2.Start();
            thread2.Join();*/

			var query2 = "SELECT TOP(1) COUNT(COLOR) AS RENK_ADETI, COLOR AS RENK\r\nFROM PLATES\r\nGROUP BY COLOR\r\nHAVING COLOR IS NOT NULL AND COLOR <> '-'\r\nORDER BY COUNT(COLOR) ASC;";
			var result2 = (List<MinimumColorDto>)CONNECTION.Query<MinimumColorDto>(query2); //liste olmayacak!

			/*IEnumerable<LicenceDateBrandModelDto> val3 = new List<LicenceDateBrandModelDto>();
            Thread thread3 = new Thread(() => { val3 = GetLicenceDateBrandModel(); });
            thread3.Start();
            thread2.Join();*/

			var query3 = "SELECT TOP(1)  LICENCEDATE, COUNT(BRAND), COUNT(MODEL)\r\nFROM PLATES\r\nGROUP BY LICENCEDATE\r\nHAVING DATEPART(YEAR, LICENCEDATE) > 2008\r\nORDER BY LICENCEDATE;";
			var result3 = (List<LicenceDateBrandModelDto>)CONNECTION.Query<LicenceDateBrandModelDto>(query3);

			/*IEnumerable<CityBrandDto> val4 = new List<CityBrandDto>();
            Thread thread4 = new Thread(() => { val4 = GetCityBrand(); });
            thread4.Start();
            thread4.Join();*/

			var query4 = "SELECT TOP(1) CITYNR, BRAND AS MARKA, COUNT(BRAND) AS MARKA_SAYISI\r\nFROM PLATES\r\nGROUP BY CITYNR, BRAND\r\nORDER BY CITYNR, BRAND;";
			var result4 = (List<CityBrandDto>)CONNECTION.Query<CityBrandDto>(query4);

			/*IEnumerable<MaximumMotorVolumeDto> val5 = new List<MaximumMotorVolumeDto>();
            Thread thread5 = new Thread(() => { val5 = GetMaximumMotorVolume(); });
            thread5.Start();
            thread5.Join();*/

			var query5 = "SELECT TOP(1) MOTORVOLUME AS EN_COK_TERCIH_EDILEN_MOTOR_HACMI, COUNT(MOTORVOLUME) AS TOPLAM_SAYISI\r\nFROM PLATES\r\nGROUP BY MOTORVOLUME\r\nHAVING MOTORVOLUME IS NOT NULL AND MOTORVOLUME <> '-' AND MOTORVOLUME <> ''\r\nORDER BY COUNT(MOTORVOLUME) DESC;";
			var result5 = (List<MaximumMotorVolumeDto>)CONNECTION.Query<MaximumMotorVolumeDto>(query5); //liste olmayacak!

			/*IEnumerable<CityOfShiftTypeDto> val6 = new List<CityOfShiftTypeDto>();
            Thread thread6 = new Thread(() => { val6 = GetCityOfShiftType(); });
            thread6.Start();
            thread6.Join();*/

			var query6 = "SELECT TOP(1) CITYNR, SHIFTTYPE AS EN_COK_TERCIH_EDILEN_VITES_TIPI, COUNT(SHIFTTYPE) AS TOPLAM_SAYISI\r\nFROM PLATES\r\nGROUP BY CITYNR, SHIFTTYPE\r\nHAVING SHIFTTYPE IS NOT NULL AND SHIFTTYPE <> '-' AND SHIFTTYPE <> '' AND SHIFTTYPE = 'Düz Vites' AND CITYNR = 36\r\nORDER BY COUNT(SHIFTTYPE) DESC;";
			var result6 = (List<CityOfShiftTypeDto>)CONNECTION.Query<CityOfShiftTypeDto>(query6);

			/*IEnumerable<CityOfShiftTypeDto> val7 = new List<CityOfShiftTypeDto>();
            Thread thread7 = new Thread(() => { val7 = GetCityOfAutomaticShiftType(); });
            thread7.Start();
            thread7.Join();*/

			var query7 = "SELECT TOP(1) CITYNR, SHIFTTYPE AS EN_COK_TERCIH_EDILEN_VITES_TIPI, COUNT(SHIFTTYPE) AS TOPLAM_SAYISI\r\nFROM PLATES\r\nGROUP BY CITYNR, SHIFTTYPE\r\nHAVING SHIFTTYPE IS NOT NULL AND SHIFTTYPE <> '-' AND SHIFTTYPE <> '' AND SHIFTTYPE = 'Otomatik Vites' AND CITYNR = 36\r\nORDER BY TOPLAM_SAYISI DESC;";
			var result7 = (List<CityOfShiftTypeDto>)CONNECTION.Query<CityOfShiftTypeDto>(query7);

			/*thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            thread5.Join();
            thread6.Join();
            thread7.Join();*/

			ShowQueryResultView model = new ShowQueryResultView()
			{
				MaximumBrandAndModels = result1,
				MinimumColors = result2,
				LicenceDateBrandModels = result3,
				CityBrands = result4,
				MaximumMotorVolumes = result5,
				CityOfAutomaticShiftTypes = result6,
				CityOfManuelShiftTypes = result7
			};

			return View(model);
			//return View();
		}

		/*
        public IEnumerable<MaximumBrandAndModelDto> GetMaximumBrandAndModel()
        {
            var query1 = "SELECT TOP(1) COUNT(BRAND) AS MARKA_SAYISI, BRAND, MODEL \r\nFROM PLATES\r\nGROUP BY BRAND, MODEL\r\nORDER BY COUNT(BRAND) DESC, COUNT(MODEL) DESC;";
            var result1 = CONNECTION.Query<MaximumBrandAndModelDto>(query1);
            return result1;
        }

        public IEnumerable<MinimumColorDto> GetMinimumColor()
        {
            var query2 = "SELECT TOP(1) COUNT(COLOR) AS RENK_ADETI, COLOR AS RENK\r\nFROM PLATES\r\nGROUP BY COLOR\r\nHAVING COLOR IS NOT NULL AND COLOR <> '-'\r\nORDER BY COUNT(COLOR) ASC;";
            var result2 = CONNECTION.Query<MinimumColorDto>(query2);
            return result2;
        }

        public IEnumerable<LicenceDateBrandModelDto> GetLicenceDateBrandModel()
        {
            var query3 = "SELECT LICENCEDATE, COUNT(BRAND), COUNT(MODEL)\r\nFROM PLATES\r\nGROUP BY LICENCEDATE\r\nHAVING DATEPART(YEAR, LICENCEDATE) > 2008\r\nORDER BY LICENCEDATE;";
            var result3 = CONNECTION.Query<LicenceDateBrandModelDto>(query3);
            return result3;
        }

        public IEnumerable<CityBrandDto> GetCityBrand()
        {
            var query4 = "SELECT CITYNR, BRAND AS MARKA, COUNT(BRAND) AS MARKA_SAYISI\r\nFROM PLATES\r\nGROUP BY CITYNR, BRAND\r\nORDER BY CITYNR, BRAND;";
            var result4 = CONNECTION.Query<CityBrandDto>(query4);
            return result4;
        }

        public IEnumerable<MaximumMotorVolumeDto> GetMaximumMotorVolume()
        {
            var query5 = "SELECT TOP(1) MOTORVOLUME AS EN_COK_TERCIH_EDILEN_MOTOR_HACMI, COUNT(MOTORVOLUME) AS TOPLAM_SAYISI\r\nFROM PLATES\r\nGROUP BY MOTORVOLUME\r\nHAVING MOTORVOLUME IS NOT NULL AND MOTORVOLUME <> '-' AND MOTORVOLUME <> ''\r\nORDER BY COUNT(MOTORVOLUME) DESC;";
            var result5 = CONNECTION.Query<MaximumMotorVolumeDto>(query5);
            return result5;
        }

        public IEnumerable<CityOfShiftTypeDto> GetCityOfShiftType()
        {
            var query6 = "SELECT CITYNR, SHIFTTYPE AS EN_COK_TERCIH_EDILEN_VITES_TIPI, COUNT(SHIFTTYPE) AS TOPLAM_SAYISI\r\nFROM PLATES\r\nGROUP BY CITYNR, SHIFTTYPE\r\nHAVING SHIFTTYPE IS NOT NULL AND SHIFTTYPE <> '-' AND SHIFTTYPE <> '' AND SHIFTTYPE = 'Düz Vites'\r\nORDER BY TOPLAM_SAYISI DESC;";
            var result6 = CONNECTION.Query<CityOfShiftTypeDto>(query6);
            return result6;
        }
        public IEnumerable<CityOfShiftTypeDto> GetCityOfAutomaticShiftType()
        {
            var query7 = "SELECT CITYNR, SHIFTTYPE AS EN_COK_TERCIH_EDILEN_VITES_TIPI, COUNT(SHIFTTYPE) AS TOPLAM_SAYISI\r\nFROM PLATES\r\nGROUP BY CITYNR, SHIFTTYPE\r\nHAVING SHIFTTYPE IS NOT NULL AND SHIFTTYPE <> '-' AND SHIFTTYPE <> '' AND SHIFTTYPE = 'Otomatik Vites'\r\nORDER BY TOPLAM_SAYISI DESC;";
            var result7 = CONNECTION.Query<CityOfShiftTypeDto>(query7);
            return result7;
        }
        */
	}
}