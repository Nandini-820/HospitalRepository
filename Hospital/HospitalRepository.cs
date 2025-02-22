using Hospital.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        #region deletes all surgery details on Surgery table
        public bool RemoveSurgeryDetails(DateTime surgeryDate)
        {
            bool flag;
            try
            {
             
              Surgery s1= (from s in context.Surgeries
                           where s.SurgeryDate ==surgeryDate
                           select s).FirstOrDefault();
                if (s1 != null)
                { 
                   context.Surgeries.Remove(s1);
                    context.SaveChanges();
                    flag = true;
                }
                else 
                  flag = false;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                flag = false;

            }


            return flag;
        }
        #endregion


        #region AddSurgeryDetials on surgery table
        public int AddSurgeryDetails(int doctorD,DateTime surgeryDate,decimal startTime,decimal endTime,string SurgeryCategory,out int surgeryID)
        {
            int resultValue = 0;
            int result = 0;
            surgeryID = 0;
            try
            {
                SqlParameter prmDoctorId = new SqlParameter("@DoctorId", doctorD);
                SqlParameter prmsurgeryDate = new SqlParameter("@SurgeryDate", surgeryDate);
                SqlParameter prmsqlstartTime = new SqlParameter("@StartTime", startTime);
                SqlParameter prmsqlendTime = new SqlParameter("@EndTime",endTime);
                SqlParameter prmSyrgeryCategory = new SqlParameter("@SurgeryCategory", SurgeryCategory);
                SqlParameter prmsurgeryId = new SqlParameter("@SurgeryId", SqlDbType.Int);
                prmsurgeryId.Direction = ParameterDirection.Output;
                SqlParameter prmReturnValue = new SqlParameter("@ReturnValue", SqlDbType.Int);
                prmReturnValue.Direction = ParameterDirection.Output;
                result = context.Database.ExecuteSqlRaw("Exec @ReturnValue=usp_AddSurgeryDetails @DoctorID,@SurgeryDate,@StartTime,@EndTime,@SurgeryCategory,@SurgeryID out",
                     new[] { prmDoctorId , prmsurgeryDate , prmsqlstartTime , prmsqlendTime, prmSyrgeryCategory, prmsurgeryId });

                resultValue = Convert.ToInt32(prmReturnValue.Value);
                if (resultValue> 0)
                {
                    surgeryID = Convert.ToInt32(prmsurgeryId.Value);
                } 

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                surgeryID = 0;
                resultValue = -99;
            }

            return resultValue;
        }

        #endregion








    }
}
