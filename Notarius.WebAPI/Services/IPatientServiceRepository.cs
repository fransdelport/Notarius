using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notarius.Shared.DTO;

namespace Notarius.WebAPI.Services
{
    public interface IPatientServiceRepository
    {

        Task<IEnumerable<PatientDTO>> GetAllPatients();
        PatientDTO GetPatientByMRN(string MRN);
        Task<bool> AddPatient(PatientDTO account);
        Task<bool> UpdateAccount(PatientDTO account);
        void DeleteAccount(string accountId);
    }
}
