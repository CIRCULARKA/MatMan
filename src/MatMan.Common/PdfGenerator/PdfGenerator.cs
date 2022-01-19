using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace MatMan.Common.PdfGeneration
{
    public class PdfGenerator
    {
        /// <summary>
        /// Creates pdf document at specified path. Stores pdf file in user local data by default
        /// </summary>
        public void CreatePdfDocument(string path)
        {
            var pdfDocument = new PdfDocument(
                new PdfReader(path)
            );

            var document = new Document(pdfDocument);

            // var centerAlignedText = new Paragraph();
            // centerAlignedText.Add("");
            // centerAlignedText.SetTextAlignment(TextAlignment.CENTER);

            // document.Add(centerAlignedText);

            document.Close();
            pdfDocument.Close();
        }
    }
}
