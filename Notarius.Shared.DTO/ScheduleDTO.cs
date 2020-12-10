using System;
using System.Collections.Generic;
using System.Text;

namespace Notarius.Shared.DTO
{
    public class ScheduleDTO
    {
        public bool IsDirty { get; set; }
        public int Key { get; set; }
        public string MRN { get; set; }
        public string ProviderId { get; set; }

        public PatientDTO Patient { get; set; }
        public string ScheduleTime { get; set; }

    }
}
