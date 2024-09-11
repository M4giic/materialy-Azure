using Azure;
using Azure.Data.Tables;

namespace StorageOperations
{
    public class UserData : ITableEntity
    {
        public string PartitionKey { get; set; } = null!;
        public string RowKey { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
