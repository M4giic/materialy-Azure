

//1. Przykład zapisania pliku do BlobStorage
using StorageOperations;

var response = BlobUploader.UploadBlob();
Console.WriteLine(response);

//2. Przykład pobrania pliku z BlobStorage
//var response = BlobDownloader.DownloadBlob();
//Console.WriteLine(response);

//3. Generowanie sasa
var sas = GenerateSasToken.Generate();
Console.WriteLine(sas);

//3. Przykład zapisania danych do TableStorage
//var response = TableUploader.UploadTableData();
//Console.WriteLine(response);

//4. Przykład pobrania danych z TableStorage
//TableDownloader.DownloadTableData();
