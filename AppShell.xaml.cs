using AISchoolManagementApp.Views;

namespace AISchoolManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
                nameof(ExamDetailView),
                typeof(ExamDetailView));
        }
    }
}
