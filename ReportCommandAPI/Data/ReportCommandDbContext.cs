using Microsoft.EntityFrameworkCore;
using ReportCommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportCommandAPI.Data
{
    public class ReportCommandDbContext : DbContext
    {
        public ReportCommandDbContext(DbContextOptions<ReportCommandDbContext> options) : base(options)
        {

        }
        public DbSet<GPReportDTO> GPReports => Set<GPReportDTO>();
    }
}
