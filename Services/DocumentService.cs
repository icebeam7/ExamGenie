using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;
using PdfSharpCore.Pdf.IO.enums;
using AISchoolManagementApp.Helpers;

namespace AISchoolManagementApp.Services
{
    public class DocumentService : IDocumentService
    {
        BlobContainerClient containerClient;

        public DocumentService()
        {
            containerClient = new BlobContainerClient(
                Constants.AzureStorageConnectionString,
                Constants.AzureStorageDocsContainer);
        }

        public async Task<bool> UploadDocumentAsync(FileResult file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var fileNameExtension = Path.GetExtension(fileName).ToLower();

            var fileUploaded = false;

            if (fileNameExtension is ".png" or ".jpg" or ".jpeg")
            {
                var blobClient = containerClient
                    .GetBlobClient(fileName);

                if (await blobClient.ExistsAsync())
                    return false;

                using var fileStream = await file.OpenReadAsync();

                await blobClient.UploadAsync(fileStream, new BlobHttpHeaders
                {
                    ContentType = "image"
                });

                fileUploaded = (await blobClient.GetPropertiesAsync())
                    .Value.ContentLength > 0;
            }
            else if (fileNameExtension is ".pdf")
            {
                using var fileStream = await file.OpenReadAsync();

                using var documents = PdfReader.Open(fileStream,
                    PdfDocumentOpenMode.Import, PdfReadAccuracy.Moderate);

                var fileNameOnly = Path.GetFileNameWithoutExtension(fileName);

                for (int i = 0; i < documents.PageCount; i++)
                {
                    var documentName = $"{fileNameOnly}-{i:D2}.pdf";
                    var blobClient = containerClient.GetBlobClient(documentName);

                    if (await blobClient.ExistsAsync())
                        continue;

                    var tempFileName = Path.GetTempFileName();

                    try
                    {
                        var document = new PdfDocument();
                        document.AddPage(documents.Pages[i]);
                        document.Save(tempFileName);

                        await using var tempStream =
                            File.OpenRead(tempFileName);

                        await blobClient.UploadAsync(
                            tempStream,
                            new BlobHttpHeaders
                            {
                                ContentType = "application/pdf"
                            });

                        fileUploaded |= (
                            await blobClient.GetPropertiesAsync())
                            .Value.ContentLength > 0;
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        File.Delete(tempFileName);
                    }
                }
            }

            return fileUploaded;
        }

        public async IAsyncEnumerable<string> ListAllDocumentsAsync(int pageSize = 50)
        {
            var blobList = containerClient
                .GetBlobsAsync()
                .AsPages(default, pageSize);

            await foreach (var blobPage in blobList)
            {
                foreach (var blobItem in blobPage.Values)
                {
                    yield return blobItem.Name;
                }
            }
        }
    }
}
