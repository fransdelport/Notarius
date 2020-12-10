using System;

namespace Notarius.Shared.DTO
{
    
    public class PatientDTO
    {
        public string MRN { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsDirty { get; set; }
    }
}
