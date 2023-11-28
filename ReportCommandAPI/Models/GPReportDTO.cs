using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportQueryAPI.Models
{
    public class GPReportDTO : ReportDTO
    {
        public string Notes { get; set; }
        public GPReportDTO(Guid id, Guid patientId, Guid employeeId, string notes)
        {
            Id = id;
            PatientId = patientId;
            EmployeeId = employeeId;
            Notes = notes;
        }

        public GPReportDTO()
        {

        }
    }
}
