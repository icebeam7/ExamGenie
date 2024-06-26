using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using AISchoolManagementApp.Models;
using AISchoolManagementApp.Services;

namespace AISchoolManagementApp.ViewModels
{
    [QueryProperty(nameof(Exam), nameof(Exam))]
    public partial class ExamDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Exam exam;

        [ObservableProperty]
        string topic;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotNew))]
        bool isNew;

        public bool IsNotNew => !IsNew;

        [ObservableProperty]
        bool showCorrectAnswer;

        public ObservableCollection<Question> Questions { get; }

        IExamService examService;

        public ExamDetailViewModel(IExamService examService)
        {
            this.examService = examService;
            this.Title = "Exam Details";
            this.Topic = string.Empty;
            //this.IsNew = true;
            this.ShowCorrectAnswer = false;
            this.Questions = new();
        }

        [RelayCommand]
        async Task LoadExamAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                IsNew = Exam.Id == 0;

                if (!IsNew)
                {
                    Exam = await examService.GetExamWithDetailsAsync(Exam.Id);
                    Questions.Clear();

                    Topic = Exam.Topic;

                    foreach (var question in Exam.Questions)
                        Questions.Add(question);
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
        async Task GenerateExamAsync()
        {
            if (IsBusy || !IsNew)
                return;

            try
            {
                IsBusy = true;
                Exam = await examService.GenerateNewExamAsync(Topic);

                if (Questions.Count != 0)
                    Questions.Clear();

                var citations = Exam.Citations.ToList();

                foreach (var question in Exam.Questions)
                {
                    var references = examService.FindCitation(question.Reference, citations);
                    question.Citation = references.First();

                    Questions.Add(question);
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
        async Task SaveExamAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var op = true;

                if (IsNew)
                {
                    await examService.SaveExamAsync(Exam);
                    await Shell.Current.DisplayAlert("Success!", "Exam saved!", "OK");
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
        async Task EvaluateAnswersAsync()
        {
            var numberQuestions = Questions.Count;
            var correctAnswers = 0;

            foreach (var question in Questions)
            {
                if (question.SelectedOption == question.CorrectOption)
                    correctAnswers++;
            }

            await Shell.Current.DisplayAlert("Score",
                $"You got {correctAnswers}/{numberQuestions} correct answers!",
                "OK");
        }

        [RelayCommand]
        async Task ViewPDFAsync()
        {
            await examService.ViewPDFAsync(Exam);
        }

        [RelayCommand]
        async Task BrowseUrl(string url)
        {
            await Launcher.OpenAsync(url);
        }

        [RelayCommand]
        async Task DeleteExamAsync()
        {
            if (IsBusy || IsNew)
                return;

            try
            {
                IsBusy = true;

                var confirm =
                    await Shell.Current.DisplayAlert(
                        "Confirm operation",
                        "Do you really want to delete this exam?",
                        "Yes",
                        "No");

                if (confirm)
                {
                    await examService.DeleteExamAsync(Exam);
                    await Shell.Current.DisplayAlert("Success!", "Exam removed!", "OK");

                    IsBusy = false;
                    await Shell.Current.Navigation.PopAsync();
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
    }
}
