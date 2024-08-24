using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AirlinesApi.Telemetry
{
    public class TelemetryConstants
    {
        public const string ServiceName = "AirlinesApi";
        public static readonly ActivitySource MainAppActivitySource = new ActivitySource(ServiceName);
        public static readonly Meter MainAppMeter = new Meter(ServiceName);
        public static readonly Counter<long> ConsumerLag = MainAppMeter.CreateCounter<long>("consumer_lag");
        public static readonly Counter<long> ConsumerError = MainAppMeter.CreateCounter<long>("consumer_error");
        public static readonly Counter<long> ConsumerSuccess = MainAppMeter.CreateCounter<long>("consumer_success");
        public static readonly Counter<long> ConsumerTimeout = MainAppMeter.CreateCounter<long>("consumer_timeout");
        public static readonly Counter<long> ConsumerRetry = MainAppMeter.CreateCounter<long>("consumer_retry");
        public static readonly Counter<long> ConsumerRetrySuccess = MainAppMeter.CreateCounter<long>("consumer_retry_success");
        public static readonly Counter<long> ConsumerRetryError = MainAppMeter.CreateCounter<long>("consumer_retry_error");
        public static readonly Counter<long> ConsumerRetryTimeout = MainAppMeter.CreateCounter<long>("consumer_retry_timeout");
        public static readonly Counter<long> ConsumerRetryExhausted = MainAppMeter.CreateCounter<long>("consumer_retry_exhausted");
        public static readonly Counter<long> RecordsLag = MainAppMeter.CreateCounter<long>("records_lag");
        // New counters based on provided metrics
        public static readonly Counter<long> RecordsConsumedRate = MainAppMeter.CreateCounter<long>("records_consumed_rate");
        public static readonly Counter<long> CommitRate = MainAppMeter.CreateCounter<long>("commit_rate");
        public static readonly Histogram<double> CommitLatencyAvg = MainAppMeter.CreateHistogram<double>("commit_latency_avg");
        public static readonly Histogram<double> CommitLatencyMax = MainAppMeter.CreateHistogram<double>("commit_latency_max");
        public static readonly Histogram<double> FetchLatencyAvg = MainAppMeter.CreateHistogram<double>("fetch_latency_avg");
        public static readonly Histogram<double> FetchLatencyMax = MainAppMeter.CreateHistogram<double>("fetch_latency_max");
        public static readonly Counter<long> FetchRate = MainAppMeter.CreateCounter<long>("fetch_rate");
        public static readonly Histogram<double> FetchSizeAvg = MainAppMeter.CreateHistogram<double>("fetch_size_avg");
        public static readonly Histogram<double> FetchSizeMax = MainAppMeter.CreateHistogram<double>("fetch_size_max");
        public static readonly Counter<long> PollRate = MainAppMeter.CreateCounter<long>("poll_rate");
        public static readonly Histogram<double> PollAvgTime = MainAppMeter.CreateHistogram<double>("poll_avg_time");
        public static readonly Histogram<double> PollMaxTime = MainAppMeter.CreateHistogram<double>("poll_max_time");
        public static readonly Counter<long> RebalanceRate = MainAppMeter.CreateCounter<long>("rebalance_rate");
        public static readonly Histogram<double> RebalanceLatencyAvg = MainAppMeter.CreateHistogram<double>("rebalance_latency_avg");
        public static readonly Histogram<double> RebalanceLatencyMax = MainAppMeter.CreateHistogram<double>("rebalance_latency_max");
        public static readonly Counter<long> BytesConsumedRate = MainAppMeter.CreateCounter<long>("bytes_consumed_rate");
        public static readonly Counter<long> BytesConsumedTotal = MainAppMeter.CreateCounter<long>("bytes_consumed_total");
        public static readonly Histogram<double> IoWaitTimeNsAvg = MainAppMeter.CreateHistogram<double>("io_wait_time_ns_avg");
        public static readonly Histogram<double> IoWaitTimeNsMax = MainAppMeter.CreateHistogram<double>("io_wait_time_ns_max");
        public static readonly Counter<long> ConnectionCount = MainAppMeter.CreateCounter<long>("connection_count");
        public static readonly Counter<long> ConnectionCreationRate = MainAppMeter.CreateCounter<long>("connection_creation_rate");
        public static readonly Counter<long> FailedFetchRate = MainAppMeter.CreateCounter<long>("failed_fetch_rate");
        public static readonly Counter<long> FailedFetchTotal = MainAppMeter.CreateCounter<long>("failed_fetch_total");
        public static readonly Counter<long> FailedDeserializationRate = MainAppMeter.CreateCounter<long>("failed_deserialization_rate");
        public static readonly Counter<long> FailedDeserializationTotal = MainAppMeter.CreateCounter<long>("failed_deserialization_total");
        public static readonly Counter<long> AssignedPartitions = MainAppMeter.CreateCounter<long>("assigned_partitions");
        public static readonly Counter<long> HeartbeatRate = MainAppMeter.CreateCounter<long>("heartbeat_rate");
        public static readonly Histogram<double> HeartbeatResponseTimeMax = MainAppMeter.CreateHistogram<double>("heartbeat_response_time_max");
    }
}
