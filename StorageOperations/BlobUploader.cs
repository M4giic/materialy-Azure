using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage.Blob;

public static class BlobUploader
{
    public static string UploadBlob()
    {
        //dane dostępowe do Azure Blob Storage
        string connectionString = "";
        string containerName = "blob-na-zdjecia";

        //nazwa pliku, który zostanie zapisany w Azure Blob Storage
        string blobName = "plik-uploadowany-z-visuala-1.jpg";

        //scieżka do pliku, który chcemy przesłać
        string filePath = "./photo.jpg";

        //pobieranie kontenera
        BlobContainerClient container = new BlobContainerClient(connectionString, containerName);


        //tworzenie referencji do bloba
        BlobClient blob = container.GetBlobClient(blobName);
        var uploadOptions = new BlobUploadOptions(); 
        uploadOptions.HttpHeaders = new BlobHttpHeaders(); 
        uploadOptions.HttpHeaders.ContentType = "image/jpg";

        //wysłanie pliku
        var response = blob.Upload(filePath, uploadOptions);

        return response.ToString();
    }
}