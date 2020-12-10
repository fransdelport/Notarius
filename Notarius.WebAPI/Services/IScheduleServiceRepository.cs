using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notarius.Shared.DTO;

namespace Notarius.WebAPI.Services
{
    public interface IScheduleServiceRepository
    {

        Task<IEnumerable<ScheduleDTO>> GetAll();
        ScheduleDTO GetScheduleByKey(int MRN);
        Task<bool> Add(ScheduleDTO sched);
        Task<bool> Update(ScheduleDTO sched);
        void Delete(int key);
    }
}
