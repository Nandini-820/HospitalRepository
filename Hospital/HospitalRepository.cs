using Hospital.Models;

namespace Hospital
{
    public class HospitalRepository
    {
        CureHospitalDbContext context;
        public HospitalRepository()
        {
            context = new CureHospitalDbContext();
        }

        #region FetchDoctorIDs
        public List<int> FetchDoctorIDs(string specializationCode ) 
        { 
           List<int > doctorIDsList = new List<int>();
            try
            {
                doctorIDsList = (from d1 in context.DoctorSpecializations
                                 where d1.SpecializationCode == specializationCode
                                 select d1.DoctorId ).ToList();

            }

            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                doctorIDsList = null;

            }

            return doctorIDsList;  
        
        }
        #endregion

        #region Add details to DoctorSpecialization method

        public bool AddDoctorSpecialization(int doctorID,string specializationCode , DateTime specializationDate)
        {
            bool flag ;
           
            try
            {
                DoctorSpecialization ds = new DoctorSpecialization();
                ds.DoctorId = doctorID;
                ds.SpecializationCode = specializationCode;
                ds.SpecializationDate = specializationDate;
                context.DoctorSpecializations.Add(ds);
                context.SaveChanges();

                flag = true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                flag = false;
            }
            return flag;
        }
        #endregion

        #region UpdateSurgeryTime on Surgery Table
        public int UpdateSurgeryTime(int SurgeryId,decimal newEndTime)
        {
            int flag;
            try
            {
                Surgery surgery = new Surgery();
                surgery = context.Surgeries.Find(SurgeryId);
                surgery.EndTime = newEndTime;
                context.Surgeries.Add(surgery);
                context.SaveChanges();
                flag = 1;


            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                flag = 0;
            }


            return flag;
        }
        #endregion

        #region
        #endregion




    }
}
