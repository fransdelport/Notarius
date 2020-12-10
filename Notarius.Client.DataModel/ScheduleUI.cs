using System;
using System.Collections.Generic;
using System.Text;

namespace Notarius.Client.DataModel
{
    public class ScheduleUI : IDataModelUI
    {
        
        public bool IsDirty { get; set; }
        public int Key { get; set; }
        public string MRN { get; set; }
        public PatientUI patient { get; set; } = new PatientUI {Firstname = "Available", Lastname = string.Empty };

        public string ScheduleTime { get; set;}

        public string ProvideId { get; set; }

        public override string ToString()
        {
            return patient.Firstname + " " + patient.Lastname;
        }
    }
}
