using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Notarius.DataAccess;
using Notarius.Shared.DataModel;
using Notarius.Shared.DTO;

namespace Notarius.API
{
    public static class ScheduleAPI
    {
        public static async Task<ObservableCollection<ScheduleDTO>> GetAllAsync(bool deleted = false)
        {
            ObservableCollection<Schedule> Scheds = await ScheduleData.GetSchedulesAsync();

            ObservableCollection<ScheduleDTO> returnList = new ObservableCollection<ScheduleDTO>();

            foreach (Schedule sched in Scheds)
            {
                ScheduleDTO pdto = TransformToDTO(sched);
                returnList.Add(pdto);
            }
            return returnList;
        }

        public static ScheduleDTO GetAsync(int key)
        {
            Schedule sched = ScheduleData.Get(key);
            if (sched == null)
                return null;
            ScheduleDTO dto = TransformToDTO(sched);

            return dto;
        }
        private static ScheduleDTO TransformToDTO(Schedule sched)
        {
            ScheduleDTO sdto = new ScheduleDTO();
            sdto.MRN = sched.MRN;
            sdto.ProviderId = sched.ProviderId;
            sdto.Patient = PatientsAPI.TransformToDTO(sched.Patient);
            sdto.ScheduleTime = sched.ScheduleTime;

            return sdto;
        }

        public static async Task<bool> SaveAsync(ScheduleDTO entity)
        {
            Schedule sched = TransformFromDTO(entity);
            return await ScheduleData.SaveAsync(sched);

        }

        private static Schedule TransformFromDTO(ScheduleDTO dto)
        {
            Schedule sched = new Schedule();
            sched.Key = dto.Key;
            sched.MRN = dto.MRN;
            sched.ProviderId = dto.ProviderId;
            sched.ScheduleTime = dto.ScheduleTime;
            return sched;
        }
        public static async Task<bool> DeleteAsync(int key)
        {
            return await ScheduleData.DeleteAsync(key);

        }
    }
}
