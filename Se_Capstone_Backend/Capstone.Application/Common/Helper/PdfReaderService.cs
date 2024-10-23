
using UglyToad.PdfPig;

namespace Capstone.Application.Common.Helper
{
    public class PdfReaderService
    {
        public string ExtractTextFromPdf(Stream pdfStream)
        {
            string text = string.Empty;

            using (var document = PdfDocument.Open(pdfStream))
            {
                foreach (var page in document.GetPages())
                {
                    text += page.Text;
                }
            }
            return text;
        }
    }
}
