using Azure.Storage.Blobs;

public static class BlobUploader
{
    public static string UploadBlob()
    {
        //dane dostępowe do Azure Blob Storage
        string connectionString = "connctionString";
        string containerName = "containername";

        //nazwa pliku, który zostanie zapisany w Azure Blob Storage
        string blobName = "blobName.jpg";

        //scieżka do pliku, który chcemy przesłać
        string filePath = "./photo.jpg";

        //pobieranie kontenera
        BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

        //tworzenie referencji do bloba
        BlobClient blob = container.GetBlobClient(blobName);

        //wysłanie pliku
        var response = blob.Upload(filePath);

        return response.ToString();
    }
}