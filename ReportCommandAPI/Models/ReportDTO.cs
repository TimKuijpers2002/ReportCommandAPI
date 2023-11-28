using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportQueryAPI.Models
{
    public abstract class ReportDTO 
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime InitialCreation { get; } = DateTime.Now;

        public ReportDTO(Guid id, Guid patientId, Guid employeeId) 
        {
            Id = id;
            PatientId = patientId;
            EmployeeId = employeeId;
        }

        public ReportDTO() { }
    }
}
