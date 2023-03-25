using iTextSharp.text.pdf;
using iTextSharp.text;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class PdfExportService : IPdfExport
    {
        public async Task<byte[]> PDFGenerate(List<CompanyModel> companies)
        {
                byte[] fileContents;
                int _maxColumn = 2;
                BaseFont baseFont = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                var _pdfStream = new MemoryStream();
               Document _document = new Document(PageSize.A4, 10f, 10f, 20f, 30f);

                PdfPTable _pdfTable = new PdfPTable(2);
                PdfPCell _pdfPCell;
                Font _fontStyle = new Font(baseFont, 12);
                _pdfTable.WidthPercentage = 100;
                _pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                PdfWriter.GetInstance(_document, _pdfStream);
                _document.Open();

                float[] sizes = new float[_maxColumn];
                for (int i = 0; i < sizes.Length; i++)
                {
                    if (i == 0) sizes[i] = 50;
                    else sizes[i] = 100;
                }

                _pdfTable.SetWidths(sizes);
                PdfHeader();
                PdfBody();
                _pdfTable.HeaderRows = 2;
                _document.Add(_pdfTable);
                _document.Close();


                void PdfHeader()
                {
                    _pdfPCell = new PdfPCell(new Phrase("Заказчики", _fontStyle));
                    _pdfPCell.Colspan = _maxColumn;
                    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    _pdfPCell.Border = 0;
                    _pdfPCell.ExtraParagraphSpace = 0;
                    _pdfTable.AddCell(_pdfPCell);

                    _pdfTable.CompleteRow();
                }

                void PdfBody()
                {
                    _pdfPCell = new PdfPCell(new Phrase("Наименование", _fontStyle));
                    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _pdfPCell.BackgroundColor = BaseColor.LightGray;
                    _pdfTable.AddCell(_pdfPCell);

                    _pdfPCell = new PdfPCell(new Phrase("Реквизиты", _fontStyle));
                    _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    _pdfPCell.BackgroundColor = BaseColor.LightGray;
                    _pdfTable.AddCell(_pdfPCell);

                    _pdfTable.CompleteRow();

                    foreach (var item in companies)
                    {
                        _pdfPCell = new PdfPCell(new Phrase(item.CompanyName, _fontStyle));
                        _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        _pdfPCell.BackgroundColor = BaseColor.White;
                        _pdfTable.AddCell(_pdfPCell);

                        _pdfPCell = new PdfPCell(new Phrase(item.CompanyDetails, _fontStyle));
                        _pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        _pdfPCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        _pdfPCell.BackgroundColor = BaseColor.White;
                        _pdfTable.AddCell(_pdfPCell);

                        _pdfTable.CompleteRow();
                    }
                }

                fileContents = _pdfStream.ToArray();
                return fileContents;
            
        }
    }
}
