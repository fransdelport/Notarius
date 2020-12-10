using System;

namespace Notarius.Shared.DataModel
{
    public class Patient:IDataModel
    {
        public string MRN { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string  State { get; set; }
        public string Zip { get; set; }
        public bool IsDirty { get ; set; }
    }
}
