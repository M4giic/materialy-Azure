using Azure.Storage.Blobs;

public static class BlobDownloader
{
    public static string DownloadBlob()
    {
        var response = new BlobClient(new Uri("sasUrl")).DownloadTo("Url");
        return response.ToString(); 
    }
}