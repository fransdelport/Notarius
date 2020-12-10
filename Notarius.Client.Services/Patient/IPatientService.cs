using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notarius.Client.DataModel;

namespace Notarius.Client.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientUI>> GetAllPatients();
        Task<PatientUI> GetPatient(string mrn);
        Task<PatientUI> AddPatient(PatientUI swd);
        Task UpdatePatient(PatientUI swd);
        Task<bool> DeletePatient(string key);
    }
}
