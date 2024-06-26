using AISchoolManagementApp.ViewModels;

namespace AISchoolManagementApp.Views;

public partial class DocumentsView : ContentPage
{
	DocumentsViewModel vm;

    public DocumentsView(DocumentsViewModel vm)
	{
		InitializeComponent();

        this.vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Task.Run(async () =>
            await vm.GetAllDocumentsCommand.ExecuteAsync(null));
    }
}