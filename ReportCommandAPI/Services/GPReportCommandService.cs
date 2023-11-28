using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ReportCommandAPI;
using ReportCommandAPI.Data;
using ReportCommandAPI.Models;

namespace ReportCommandAPI.Services
{
    public class GPReportCommandService : GPReportCommandProto.GPReportCommandProtoBase
    {
        private readonly ReportCommandDbContext _dbContext;
        public GPReportCommandService(ReportCommandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CreateGPReportResponse> CreateGPReport(CreateGPReportRequest request, ServerCallContext context)
        {
            if (request.Notes == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must provide a valid input"));
            }

            var gpreport = new GPReportDTO
            {
                PatientId = Guid.Parse(request.PatientId),
                EmployeeId = Guid.Parse(request.EmployeeId),
                Notes = request.Notes,
            };

            await _dbContext.AddAsync(gpreport);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreateGPReportResponse
            {
                Id = gpreport.Id.ToString()
            });
        }

        public override async Task<UpdateGPReportResponse> UpdateGPReport(UpdateGPReportRequest request, ServerCallContext context)
        {
            if (request.Id == string.Empty || request.PatientId == string.Empty || request.EmployeeId == string.Empty || request.Notes == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must provide a valid input"));
            }
            var gpreport = await _dbContext.GPReports.FirstOrDefaultAsync(r => r.Id.ToString() == request.Id);

            if (gpreport == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No gpreport with id {request.Id}"));
            }

            gpreport.Id = Guid.Parse(request.Id);
            gpreport.PatientId = Guid.Parse(request.PatientId);
            gpreport.EmployeeId = Guid.Parse(request.EmployeeId);
            gpreport.Notes = request.Notes;

            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new UpdateGPReportResponse
            {
                Id = gpreport.Id.ToString()
            });
        }

        public override async Task<DeleteGPReportResponse> DeleteGPReport(DeleteGPReportRequest request, ServerCallContext context)
        {
            if (request.Id == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must provide a valid input"));
            }
            var gpreport = await _dbContext.GPReports.FirstOrDefaultAsync(r => r.Id.ToString() == request.Id);

            if (gpreport == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No gpreport with id {request.Id}"));
            }

            _dbContext.GPReports.Remove(gpreport);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new DeleteGPReportResponse
            {
                Id = gpreport.Id.ToString()
            });
        }
    }
}