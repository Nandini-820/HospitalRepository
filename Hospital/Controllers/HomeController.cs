using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
//using HospitalDalCrossPlatform;

namespace Hospital.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {


        HospitalRepository repo;
        public HomeController()
        {
            repo = new HospitalRepository();
          
            
        }

        #region fetch doctorids
        [HttpGet]

        //https://localhost:7274/api/Home/FetchDoctorIds?specializationCode=ANE
        public JsonResult FetchDoctorIds(string specializationCode)
        {
            List<int> result = new List<int>();
            //specializationCode = "CAR";
            try
            {
                result = repo.FetchDoctorIDs(specializationCode);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = null;
            }

            return Json(result);
        }
        #endregion

        #region AddDoctorSpecialization
        [HttpPost]
        public bool AddDoctorSpecialization(int doctorId, string specializationCode, DateTime specializationDate)
        {
            bool result;
            try
            {
                result = repo.AddDoctorSpecialization(doctorId, specializationCode, specializationDate);

            }
            catch (Exception ex)
            {
                result = false;

            }

            return result;
        }

        #endregion


        #region update surgery time
        [HttpPut]
        public int UpdateSurgeryTime(Surgery surgery)
        {
            int result = 0;
            try
            {
                result = repo.UpdateSurgeryTime(surgery.SurgeryId, surgery.EndTime);
            }
            catch (Exception ex)
            {
                result = 0;

            }

            return result;
        }


        #endregion

        #region Remove surgery details
        [HttpDelete]
        public JsonResult RemoveSurgeryDetails(DateTime surgeryDate)
        {
            bool result = false;
            try
            {
                result = repo.RemoveSurgeryDetails(surgeryDate);

            }

            catch (Exception e) 
            {
                Console.WriteLine("exception is "+e.Message);
                result = false;

            }

            return Json(result);

        }

        #endregion








    }
}
