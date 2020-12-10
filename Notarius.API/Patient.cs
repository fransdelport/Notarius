using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Notarius.DataAccess;
using Notarius.Shared.DataModel;
using Notarius.Shared.DTO;

namespace Notarius.API
{
    public static class PatientsAPI
    {

        public static async Task<ObservableCollection<PatientDTO>> GetPatientsAsync(bool deleted = false)
        {
            ObservableCollection<Patient> pats = await PatientData.GetPatientsAsync();

            ObservableCollection<PatientDTO> returnList = new ObservableCollection<PatientDTO>();

            foreach(Patient pat in pats)
            {
                PatientDTO pdto = TransformToDTO(pat);
                returnList.Add(pdto);
            }
            return returnList;
        }

        public static PatientDTO TransformToDTO(Patient pat)
        {
            PatientDTO pdto = new PatientDTO();
            pdto.MRN = pat.MRN;
            pdto.Firstname = pat.Firstname;
            pdto.Lastname = pat.Lastname;
            pdto.Address = pat.Address;
            pdto.City = pat.City;
            pdto.State = pat.State;
            pdto.Zip = pat.Zip;
            return pdto;
        }

        public static PatientDTO GetPatientAsync(string mrn)
        {
            Patient pat = PatientData.GetPatientAsync(mrn);
            if (pat == null)
                return null;
            PatientDTO pdto = TransformToDTO(pat);

            return pdto;
        }

        public static async Task<bool> SaveAsync(PatientDTO entity)
        {
            Patient pat = TransformFromDTO(entity);
            return await PatientData.SavePatientAsync(pat);

        }

        private static Patient TransformFromDTO(PatientDTO entity)
        {
            Patient pat = new Patient();
            pat.MRN = entity.MRN;
            pat.Firstname = entity.Firstname;
            pat.Lastname = entity.Lastname;
            pat.Address = entity.Address;
            pat.City = entity.City;
            pat.State = entity.State;
            pat.Zip = entity.Zip;
            return pat;
        }

        public static async Task<bool> DeletePatientAsync(string MRN)
        {
           return  await PatientData.DeletePatientAsync(MRN);

        }
    }
}
