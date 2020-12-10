using System;
using System.Collections.Generic;
using System.Text;

namespace Notarius.Shared.DataModel
{
    public class Schedule : IDataModel
    {
        public bool IsDirty { get; set; }
        public int Key { get; set; }
        public string MRN { get; set; }
        public string ProviderId { get; set; }

        public string ScheduleTime { get; set; }

        public Patient Patient { get; set; }

    }
}
