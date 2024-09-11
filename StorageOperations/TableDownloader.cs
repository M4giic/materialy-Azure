using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using StorageOperations;
using System.Collections.Concurrent;

public static class TableDownloader
{
    public static void DownloadTableData()
    {
        var connctionString = "connctionString";
        var tableName = "Kamil";
        var tableClient = new TableClient(connctionString, tableName);
        var partitionKey = "PartitionKey";

        Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

        // Iterate the <see cref="Pageable"> to access all queried entities.
        foreach (TableEntity qEntity in queryResultsFilter)
        {
            Console.WriteLine($"{qEntity.GetString("UserName")}: {qEntity.GetString("City")}");
        }

    }
}