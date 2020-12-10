using System;
using System.ComponentModel.DataAnnotations;

namespace Notarius.Client.DataModel
{
    public class PatientUI:IDataModelUI
    {
        [Required]
        [StringLength(30, ErrorMessage = "Medical record number cannot exceeds 30 characters")]
        public string MRN { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Patient first name cannot exceeds 30 characters")]
        public string Firstname { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Patient last name cannot exceeds 30 characters")]
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string  State { get; set; }
        public string Zip { get; set; }
        public bool IsDirty { get ; set; }
        public bool ShowDetails { get; set; } = false;

        public bool HasDetails { get; set; } = true;
    }
}
