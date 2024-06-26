using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using AISchoolManagementApp.Services;
using System.Collections.ObjectModel;

namespace AISchoolManagementApp.ViewModels
{
    public partial class DocumentsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool uploadStatus;

        public ObservableCollection<string> Documents { get; }

        IDocumentService documentService;

        public DocumentsViewModel(IDocumentService documentService)
        {
            Title = "Document List";
            this.documentService = documentService;
            Documents = new();
        }

        [RelayCommand]
        async Task PickAndUploadDocumentAsync()
        {
            var fileResult = await pickFileAsync();

            if (fileResult != null)
            {
                //FilePath = fileResult.FullPath;
                //var fileCopy = await copyFile(fileResult);
                await uploadDocumentAsync(fileResult);
            }
        }

        async Task<FileResult> pickFileAsync()
        {
            try
            {
                return await FilePicker.Default.PickAsync();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }

            return default;
        }

        async Task<FileResult> copyFileAsync(FileResult sourceFile)
        {
            var filePath = Path.Combine(FileSystem.CacheDirectory, sourceFile.FileName);

            using var sourceStream = await sourceFile.OpenReadAsync();
            using var copyStream = File.OpenWrite(filePath);

            await sourceStream.CopyToAsync(copyStream);
            return new(filePath);
        }

        async Task uploadDocumentAsync(FileResult file)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                UploadStatus = await documentService.UploadDocumentAsync(file);

                if (UploadStatus)
                {
                    await Task.Delay(3000);
                    await Shell.Current.DisplayAlert("Success!", "Document added!", "OK");

                    IsBusy = false;
                    await GetAllDocumentsAsync();
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GetAllDocumentsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var documents = documentService.ListAllDocumentsAsync();

                if (Documents.Count != 0)
                    Documents.Clear();

                await foreach (var document in documents)
                    Documents.Add(document);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
