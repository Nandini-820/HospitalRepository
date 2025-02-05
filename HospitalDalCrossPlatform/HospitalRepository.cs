using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDalCrossPlatform
{
    public class HospitalRepository
    {
        CureHospitalDbContext context;
        public HospitalRepository()
        {
            context = new CureHospitalDbContext();
        }

        #region FetchIDs
        public List<int> FetchDoctorIDs(string specializationCode)
        {
            List<int> doctorIdList = new List<int>();
            try
            {
                doctorIdList = (from d1 in context.DoctorSpecializations
                                where d1.SpecializationCode == specializationCode
                                select d1.DoctorId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                doctorIdList = null;
            }
            return doctorIdList;

        }

        #endregion

    }
}
