using AISchoolManagementApp.ViewModels;

namespace AISchoolManagementApp.Views;

public partial class ExamDetailView : ContentPage
{
    ExamDetailViewModel vm;

    public ExamDetailView(ExamDetailViewModel vm)
	{
		InitializeComponent();

        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await vm.LoadExamCommand.ExecuteAsync(null);
    }
}