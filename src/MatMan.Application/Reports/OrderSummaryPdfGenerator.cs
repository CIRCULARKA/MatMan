using System;
using System.IO;
using System.Collections.Generic;
using MatMan.Domain.Models;
using Gehtsoft.PDFFlow;
using Gehtsoft.PDFFlow.Models.Shared;
using Gehtsoft.PDFFlow.Models.Exceptions;
using Gehtsoft.PDFFlow.Builder;
using Gehtsoft.PDFFlow.Utils;
using Gehtsoft.PDFFlow.UserUtils;
using Gehtsoft.PDFFlow.Models.Enumerations;

namespace MatMan.Application.Reports
{
    public class OrderSummaryPdfGenerator
    {
        /// <summary>
        /// Creates pdf document at specified path. Stores pdf file in user local data by default
        /// </summary>
        public byte[] CreateReport(IEnumerable<WareMaterial> wareMaterials)
        {
            var documentBuilder = DocumentBuilder.New();

            var sectionBuilder = SectionBuilder.New();

            sectionBuilder.AddParagraph(builder => {
                builder.AddText("Привет");
                var font = Fonts.Times(18);
                builder.SetFontName(font.Name);
                builder.SetFontEncodingName(EncodingNames.KOI8_R);
            });

            documentBuilder.AddSection(sectionBuilder);

            var outputStream = new MemoryStream();
            documentBuilder.Build(outputStream);

            return outputStream.ToArray();
        }
    }
}
