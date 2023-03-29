using AutoMapper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class WordExportService : IWordExport
    {
        public async Task<byte[]> WordGenerate(List<CompanyModel> companies)
        {
            byte[] fileContents;
            var stream = new MemoryStream();
            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();

                new Document(new Body()).Save(mainPart);

                Body body = mainPart.Document.Body;

                body.Append(new Paragraph(new Run(new Text($"Список заказчиков"))));

                Table table = new Table();

                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new BottomBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new LeftBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new RightBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new InsideHorizontalBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        },
                        new InsideVerticalBorder()
                        {
                            Val =
                            new EnumValue<BorderValues>(BorderValues.Seattle),
                            Size = 10
                        }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);

                foreach (var item in companies)
                {
                    TableRow tr = new TableRow();

                    TableCell tc1 = new TableCell();


                    tc1.Append(new TableCellProperties(
                        new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));

                    tc1.Append(new Paragraph(new Run(new Text(item.CompanyName))));

                    tr.Append(tc1);

                    TableCell tc2 = new TableCell();

                    tc2.Append(new Paragraph(new Run(new Text(item.CompanyDetails))));

                    tr.Append(tc2);

                    table.Append(tr);
                }

                doc.MainDocumentPart.Document.Body.Append(table);

                mainPart.Document.Save();
            }
            fileContents = stream.ToArray();

            return  fileContents;
        }

    }
}
