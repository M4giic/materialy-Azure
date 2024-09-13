using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageOperations;
public static class GenerateSasToken
{
    public static string Generate()
    {
        //dane dostępowe do Azure Blob Storage
        string connectionString = "";
        string containerName = "blob-na-zdjecia";

        //nazwa pliku, który zostanie zapisany w Azure Blob Storage
        string blobName = "plik-uploadowany-z-visuala-1.jpg";

        //scieżka do pliku, który chcemy przesłać
        string filePath = "./photo.jpg";

        //pobieranie kontenera
        BlobClient container = new BlobContainerClient(connectionString, containerName).GetBlobClient(blobName);

        // Check if BlobContainerClient object has been authorized with Shared Key
        if (container.CanGenerateSasUri)
        {

            // Create a SAS token that's valid for one day
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "c"
            };

            sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddDays(1);
            sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
    

            Uri sasURI = container.GenerateSasUri(sasBuilder);

            return sasURI.ToString();
        }
     

        return string.Empty;
    }
}
