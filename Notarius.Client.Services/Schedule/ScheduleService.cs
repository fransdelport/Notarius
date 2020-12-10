using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Notarius.Client.DataModel;
using Notarius.Shared.DTO;

namespace Notarius.Client.Services.Schedule
{
    public class ScheduleService : IScheduleService
    {

        private readonly HttpClient _httpClient;
        public ScheduleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ScheduleUI> Add(ScheduleUI sched)
        {
            return await Save(sched);
        }
        private async Task<ScheduleUI> Save(ScheduleUI sched)
        {
            ScheduleDTO dto = new ScheduleDTO();

            dto.Key = sched.Key;
            dto.MRN = sched.MRN;
            dto.ProviderId = sched.ProvideId;
            dto.ScheduleTime = sched.ScheduleTime;;
          


            var scheduleJson = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync("api/Schedule", scheduleJson);

                if (response.IsSuccessStatusCode)
                {
                    ScheduleDTO s = JsonSerializer.Deserialize<ScheduleDTO>(await response.Content.ReadAsStringAsync());
                    ScheduleUI p = new ScheduleUI();
                    p.Key = s.Key;
                    p.MRN = s.MRN;
                    p.ProvideId = s.ProviderId;
                    p.ScheduleTime = s.ScheduleTime;
                   
                    return p;
                }

            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }

            return sched;
        }
        public async Task<bool> Delete(string key)
        {
            await _httpClient.DeleteAsync($"/api/schedule/{key}");
            return true;
        }
        public async Task<IEnumerable<ScheduleUI>> GetAll(DateTime date)
           {
                try
                {

                    var pats = await JsonSerializer.DeserializeAsync<IEnumerable<ScheduleDTO>>
                   (await _httpClient.GetStreamAsync($"api/schedule"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });


                    List<ScheduleUI> returnList = new List<ScheduleUI>();
                    foreach (ScheduleDTO p in pats)
                    {
                        ScheduleUI pa = new ScheduleUI();
                        pa.MRN = p.MRN;
                        pa.Key = p.Key;
                        pa.ProvideId = p.ProviderId;
                        pa.ScheduleTime = p.ScheduleTime;
                   
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
        public async Task<ScheduleUI> Get(int key)
        {
            throw new NotImplementedException();
        }
        public async Task<ScheduleUI> Update(ScheduleUI sched)
        {
            return await Save(sched);
        }

    }
}
