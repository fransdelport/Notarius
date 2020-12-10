using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notarius.API;
using Notarius.Shared.DTO;

namespace Notarius.WebAPI.Services
{
    public class PatientServiceRepository : IPatientServiceRepository
    {
        public async Task<bool> AddPatient(PatientDTO patient)
        {
            return await PatientsAPI.SaveAsync(patient);
        }

        public async void DeleteAccount(string MRN)
        {
            await PatientsAPI.DeletePatientAsync(MRN);
        }

        public async Task<IEnumerable<PatientDTO>> GetAllPatients()
        {

            return await PatientsAPI.GetPatientsAsync();
        }

        public PatientDTO GetPatientByMRN(string MRN)
        {
            return PatientsAPI.GetPatientAsync(MRN);
        }

        public Task<bool> UpdateAccount(PatientDTO account)
        {
            throw new NotImplementedException();
        }
    }
}
