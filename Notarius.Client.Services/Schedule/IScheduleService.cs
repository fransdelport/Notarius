using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Notarius.Client.DataModel;

namespace Notarius.Client.Services.Schedule
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleUI>> GetAll(DateTime dt);
        Task<ScheduleUI> Get(int key);
        Task<ScheduleUI> Add(ScheduleUI swd);
        Task<ScheduleUI> Update(ScheduleUI swd);
        Task<bool> Delete(string key);
    }
}
