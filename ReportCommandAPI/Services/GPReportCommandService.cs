using Grpc.Core;
using ReportCommandAPI;

namespace ReportCommandAPI.Services
{
    public class GPReportCommandService : GPReportCommandProto.GPReportCommandProtoBase
    {
        private readonly ILogger<GPReportCommandService> _logger;
        public GPReportCommandService(ILogger<GPReportCommandService> logger)
        {
            _logger = logger;
        }

        public override Task<CreateGPReportResponse> CreateGPReport(CreateGPReportRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CreateGPReportResponse
            {
                Id = "Hello " + request
            });
        }
    }
}