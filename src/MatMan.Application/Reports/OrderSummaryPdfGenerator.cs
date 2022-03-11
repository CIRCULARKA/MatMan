using System;
using System.IO;
using System.Collections.Generic;
using MatMan.Domain.Models;
using Gehtsoft.PDFFlow.Models.Shared;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Utils;
using Gehtsoft.PDFFlow.Models.Enumerations;

namespace MatMan.Application.Reports
{
    public class OrderSummaryPdfGenerator
    {
        private readonly DocumentBuilder _documentBuilder;

        private readonly SectionBuilder _documentSectionBuilder;

        public OrderSummaryPdfGenerator()
        {
            _documentBuilder = DocumentBuilder.New();
            _documentSectionBuilder = SectionBuilder.New();
            _documentSectionBuilder.SetOrientation(PageOrientation.Portrait);

            var documentStyle = StyleBuilder.New();
            documentStyle.SetFontName(Fonts.Courier(14).Name);
            documentStyle.SetFontSize(14);
            documentStyle.SetFontEncodingName(EncodingNames.KOI8_R);

            _documentBuilder.ApplyStyle(documentStyle);

            _documentBuilder.AddSection(_documentSectionBuilder);
        }

        /// <summary>
        /// Outputs generated PDF document as the sequence of bytes
        /// </summary>
        public byte[] GenerateReport(string title, IEnumerable<OrderComponent<Material>> wareMaterials)
        {
            _documentSectionBuilder.AddParagraph(builder => {
                builder.SetFontSize(24);
                builder.SetBold();
                builder.AddText(title);
                builder.SetAlignment(HorizontalAlignment.Center);
                builder.SetMarginBottom(30);
            });

            _documentSectionBuilder.AddTable(tableBuilder => {
                tableBuilder.AddColumn("Материал");
                tableBuilder.AddColumn("Количество");

                foreach (var wareMaterial in wareMaterials)
                {
                    tableBuilder.AddRow(rowBuilder => {
                        rowBuilder.AddCell(wareMaterial.Component.Name);
                        rowBuilder.AddCell(wareMaterial.ComponentAmount.ToString());
                    });
                }
            });

            return ConvertDocumentToBytes();
        }

        private byte[] ConvertDocumentToBytes()
        {
            var outputStream = new MemoryStream();
            _documentBuilder.Build(outputStream);
            return outputStream.ToArray();
        }
    }
}
