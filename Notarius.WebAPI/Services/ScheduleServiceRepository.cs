using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notarius.API;
using Notarius.Shared.DTO;

namespace Notarius.WebAPI.Services
{
    public class ScheduleServiceRepository : IScheduleServiceRepository
    {
        public async Task<bool> Add(ScheduleDTO sched)
        {
            return await ScheduleAPI.SaveAsync(sched);
        }

        public async void Delete(int key)
        {
            await ScheduleAPI.DeleteAsync(key);
        }

        public async Task<IEnumerable<ScheduleDTO>> GetAll()
        {
            return await ScheduleAPI.GetAllAsync();
        }


        public ScheduleDTO GetScheduleByKey(int key)
        {
            return ScheduleAPI.GetAsync(key);
        }

        public Task<bool> Update(ScheduleDTO sched)
        {
            throw new NotImplementedException();
        }
    }
}
