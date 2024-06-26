using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

using AISchoolManagementApp.Views;
using AISchoolManagementApp.Models;
using AISchoolManagementApp.Services;

namespace AISchoolManagementApp.ViewModels
{
    public partial class ExamsViewModel : BaseViewModel
    {
        public ObservableCollection<Exam> Exams { get; }

        IExamService examService;

        [ObservableProperty]
        Exam selectedExam;

        public ExamsViewModel(IExamService examService)
        {
            Title = "Exam List";
            this.examService = examService;
            Exams = new();
        }

        [RelayCommand]
        async Task GetExamsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var exams = await examService.GetExamsAsync();

                Exams.Clear();

                foreach (var exam in exams)
                    Exams.Add(exam);
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
        async Task GoToDetailsAsync()
        {
            Exam exam = SelectedExam ?? new Exam();

            var data = new Dictionary<string, object>
            {
                {nameof(Exam), exam }
            };

            await Shell.Current.GoToAsync(
                nameof(ExamDetailView), true, data);

            SelectedExam = null;
        }
    }
}
