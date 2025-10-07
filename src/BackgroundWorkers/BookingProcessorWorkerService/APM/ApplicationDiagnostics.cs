using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace BookingProcessorWorkerService.APM
{
    internal class ApplicationDiagnostics
    {
        //best practices
        public const string ServiceName="BookingProcessorWorkerService";
        public static readonly ActivitySource ActivitySource= new ActivitySource(ServiceName);
        public static readonly Meter Meter = new Meter(ServiceName);
    }
    internal class TagNames
    {
        //use prefix for tags
    }
    //record exceptions
    //set activity status
    //use extention methods
}
