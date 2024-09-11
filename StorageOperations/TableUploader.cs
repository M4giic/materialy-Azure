using Azure.Data.Tables;
using Azure.Storage.Blobs;
using StorageOperations;

public static class TableUploader
{
    public static string UploadTableData()
    {
        var connctionString = "connctionString";
        var tableName = "Kamil";
        var tableClient = new TableClient(connctionString, tableName);

        // Create the table in the service.
        var response = tableClient.UpsertEntity(new UserData { PartitionKey = "PartitionKey", RowKey = "RowKey", UserName = "Kamil", City = "Kraków"});
        return response.ToString();
    }
}