using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;

using iText.Layout;

using AISchoolManagementApp.Models;

namespace AISchoolManagementApp.Services
{
    public class PDFService : IPDFService
    {

        public async Task GeneratePDFAsync(Exam exam)
        {
            string fileName = $"{exam.Topic ?? "exam"}.pdf";

#if ANDROID
            Permissions.RequestAsync<Permissions.StorageWrite>();
            var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);
            var filePath = Path.Combine(docsDirectory.AbsoluteFile.Path, fileName);
#else
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);
#endif

            using (PdfWriter writer = new PdfWriter(filePath))
            {
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                var leftAlignment = iText.Layout.Properties.TextAlignment.LEFT;
                var centerAlignment = iText.Layout.Properties.TextAlignment.CENTER;

                var header = new Paragraph($"Topic Exam: '{exam.Topic}'")
                    .SetTextAlignment(centerAlignment)
                    .SetFontSize(20);
                document.Add(header);

                var subheader = new Paragraph("Choose the correct answer:")
                    .SetTextAlignment(leftAlignment)
                    .SetFontSize(15);
                document.Add(subheader);

                foreach (var question in exam.Questions)
                {
                    var content = new Paragraph($"{question.Number}. {question.Content}")
                        .SetTextAlignment(leftAlignment)
                        .SetBold()
                        .SetMargins(5, 0, 5, 0)
                        .SetFontSize(12);
                    document.Add(content);

                    List options = new List(ListNumberingType.ENGLISH_LOWER)
                        .SetSymbolIndent(12)
                        .SetFontSize(11);

                    options.Add(new ListItem(question.OptionA))
                        .Add(new ListItem(question.OptionB))
                        .Add(new ListItem(question.OptionC))
                        .Add(new ListItem(question.OptionD));

                    document.Add(options);

                    var ls = new LineSeparator(new SolidLine())
                        .SetMargins(5, 0, 5, 0);
                    document.Add(ls);
                }

                document.Close();
            }

            await Launcher.Default.OpenAsync(
                new OpenFileRequest("View PDF Exam",
                new ReadOnlyFile(filePath)));
        }
    }
}
