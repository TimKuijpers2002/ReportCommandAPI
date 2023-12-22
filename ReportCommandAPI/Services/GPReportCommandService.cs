using Cassandra;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ReportCommandAPI;
using ReportCommandAPI.Models;

namespace ReportCommandAPI.Services
{
    public class GPReportCommandService : GPReportCommandProto.GPReportCommandProtoBase
    {
        private readonly Cassandra.ISession _cassandraSession;

        public GPReportCommandService(Cassandra.ISession cassandraSession)
        {
            _cassandraSession = cassandraSession;
        }

        public override async Task<CreateGPReportResponse> CreateGPReport(CreateGPReportRequest request, ServerCallContext context)
        {
            if (request.Notes == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must provide a valid input"));
            }

            var gpreport = new GPReportDTO
            (
                Guid.NewGuid(),
                Guid.Parse(request.PatientId),
                Guid.Parse(request.EmployeeId),
                request.Notes
            );

            // Prepare the statement
            var statement = new SimpleStatement(
                "INSERT INTO gpreport (Id, PatientId, EmployeeId, InitialCreation Notes) VALUES (?, ?, ?, ?, ?)",
                gpreport.Id, gpreport.PatientId, gpreport.EmployeeId, gpreport.Notes, gpreport.InitialCreation);

            // Execute the statement
            await _cassandraSession.ExecuteAsync(statement);

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

            var gpreportId = Guid.Parse(request.Id);

            // Prepare the statement to update the existing report
            var updateStatement = new SimpleStatement(
                "UPDATE gpreport SET PatientId = ?, EmployeeId = ?, Notes = ? WHERE Id = ?",
                Guid.Parse(request.PatientId), Guid.Parse(request.EmployeeId), request.Notes, gpreportId);

            // Execute the update statement
            await _cassandraSession.ExecuteAsync(updateStatement);

            return await Task.FromResult(new UpdateGPReportResponse
            {
                Id = gpreportId.ToString()
            });
        }

        public override async Task<DeleteGPReportResponse> DeleteGPReport(DeleteGPReportRequest request, ServerCallContext context)
        {
            if (request.Id == string.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must provide a valid input"));
            }

            var gpreportId = Guid.Parse(request.Id);

            // Prepare the statement to delete the existing report
            var deleteStatement = new SimpleStatement("DELETE FROM gpreport WHERE Id = ?", gpreportId);

            // Execute the delete statement
            await _cassandraSession.ExecuteAsync(deleteStatement);

            return await Task.FromResult(new DeleteGPReportResponse
            {
                Id = gpreportId.ToString()
            });
        }
    }
}