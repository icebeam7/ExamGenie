using AISchoolManagementApp.DbContexts;
using AISchoolManagementApp.Helpers;
using AISchoolManagementApp.Services;
using AISchoolManagementApp.ViewModels;
using AISchoolManagementApp.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace AISchoolManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, Constants.DbName);
            builder.Services.AddSqlite<SmartSchoolContext>($"FileName={dbPath}");

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IDocumentService, DocumentService>()
                .AddSingleton<IDatabaseService, DatabaseService>()
                .AddSingleton<IExamService, ExamService>()
                .AddSingleton<IPDFService, PDFService>();

            builder.Services.AddScoped<DocumentsViewModel>()
                            .AddScoped<ExamsViewModel>()
                            .AddScoped<ExamDetailViewModel>();

            builder.Services.AddScoped<DocumentsView>()
                .AddScoped<ExamsView>()
                .AddScoped<ExamDetailView>();

            return builder.Build();
        }
    }
}
