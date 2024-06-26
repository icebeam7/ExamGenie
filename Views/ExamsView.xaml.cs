using AISchoolManagementApp.ViewModels;

namespace AISchoolManagementApp.Views;

public partial class ExamsView : ContentPage
{
    ExamsViewModel vm;

    public ExamsView(ExamsViewModel vm)
	{
		InitializeComponent();

        this.vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await vm.GetExamsCommand.ExecuteAsync(null);
    }
}