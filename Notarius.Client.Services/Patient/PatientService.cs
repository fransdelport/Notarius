using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Notarius.Client.DataModel;
using Notarius.Shared.DTO;

namespace Notarius.Client.Services
{
    public class PatientService : IPatientService
    {

        private readonly HttpClient _httpClient;
        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PatientUI> AddPatient(PatientUI pt)
        {
            return await SavePatient(pt);
        }

        private async Task<PatientUI> SavePatient(PatientUI pt)
        {
            PatientDTO pat = new PatientDTO();

            pat.MRN = pt.MRN;
            pat.Firstname = pt.Firstname;
            pat.Lastname = pt.Lastname;
            pat.Address = pt.Address;
            pat.City = pt.City;
            pat.State = pt.State;
            pat.Zip = pt.Zip;


            var patientJson = new StringContent(JsonSerializer.Serialize(pt), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("api/Patient", patientJson);

                if (response.IsSuccessStatusCode)
                {
                    PatientDTO s = JsonSerializer.Deserialize<PatientDTO>(await response.Content.ReadAsStringAsync());
                    PatientUI p = new PatientUI();
                    p.MRN = s.MRN;
                    p.Firstname = s.Firstname;
                    p.Lastname = s.Lastname;
                    p.Address = s.Address;
                    p.City = s.City;
                    p.State = s.State;
                    p.Zip = s.Zip;

                    return p;
                }

            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }

            return pt;
        }

        public async Task<bool> DeletePatient(string key)
        {
             await _httpClient.DeleteAsync($"/api/patient/{key}");
            return true;

        }

        public async Task<IEnumerable<PatientUI>> GetAllPatients()
        {
            try
            {

                var pats = await JsonSerializer.DeserializeAsync<IEnumerable<PatientDTO>>
               (await _httpClient.GetStreamAsync($"api/patient"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


                List<PatientUI> returnList = new List<PatientUI>();
                foreach(PatientDTO p in pats)
                {
                    PatientUI pa = new PatientUI();
                    pa.MRN = p.MRN;
                    pa.Firstname = p.Firstname;
                    pa.Lastname = p.Lastname;
                    pa.Address = p.Address;
                    pa.City = p.City;
                    pa.State = p.State;
                    pa.Zip = p.Zip;
                    returnList.Add(pa);
                }
                return returnList;
            }
            catch (Exception ex)
            {
                string err = ex.ToString();
                throw;
            }
        }

        public Task<PatientUI> GetPatient(string mrn)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePatient(PatientUI swd)
        {
            var pat =  SavePatient(swd);
            return pat;

        }
    }
}