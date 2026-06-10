using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektCmentarz.Data;
using ProjektCmentarz.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektCmentarz.Controllers
{
    public class ReportController : Controller
    {
        private readonly GraveyardContext _context;

        public ReportController(GraveyardContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DownloadMonthlyReport(int month, int year)
        {
            var deceasedList = await _context.Deceaseds
                .Where(d => d.DeathDate.Month == month && d.DeathDate.Year == year)
                .Select(d => new
                {
                    d.Id,
                    d.FirstName,
                    d.Surname,
                    d.DeathDate,
                    Age = d.DeathDate.Year - d.BirthDate.Year,
                    HasFuneral = d.FuneralId != null
                })
                .ToListAsync();

            int totalDeceased = deceasedList.Count;
            int totalFunerals = deceasedList.Count(d => d.HasFuneral);
            double averageAge = deceasedList.Any() ? deceasedList.Average(d => d.Age) : 0;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Raport Miesięczny");


                // Tytuł
                worksheet.Cell("A1").Value = $"Raport Statystyczny - Okres: {month:02d}/{year}";
                worksheet.Cell("A1").Style.Font.SetBold();
                worksheet.Cell("A1").Style.Font.SetFontColor(XLColor.FromHtml("#2A4B7C"));
                worksheet.Cell("A1").Style.Font.SetFontSize(16);

                // Podsumowanie
                worksheet.Cell("A3").Value = "Liczba zmarłych:";
                worksheet.Cell("B3").Value = totalDeceased;
                worksheet.Cell("C3").Value = "Liczba pogrzebów:";
                worksheet.Cell("D3").Value = totalFunerals;
                worksheet.Cell("E3").Value = "Średni wiek zmarłych:";
                worksheet.Cell("F3").Value = Math.Round(averageAge, 1);

                var summaryRange = worksheet.Range("A3:F3");
                summaryRange.Style.Font.SetBold();
                summaryRange.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#E2E9F3"));

                // Tabela
                string[] headers = { "ID", "Imię", "Nazwisko", "Data Zgonu", "Wiek", "Czy był pogrzeb?" };
                int startRow = 5;

                
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(startRow, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.SetBold();
                    cell.Style.Font.SetFontColor(XLColor.White);
                    cell.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#2A4B7C"));
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                int currentRow = startRow + 1;
                foreach (var item in deceasedList)
                {
                    worksheet.Cell(currentRow, 1).Value = item.Id;
                    worksheet.Cell(currentRow, 2).Value = item.FirstName;
                    worksheet.Cell(currentRow, 3).Value = item.Surname;
                    worksheet.Cell(currentRow, 4).Value = item.DeathDate;
                    worksheet.Cell(currentRow, 5).Value = item.Age;
                    worksheet.Cell(currentRow, 6).Value = item.HasFuneral ? "Tak" : "Nie";

                    worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "yyyy-mm-dd";
                    worksheet.Cell(currentRow, 5).Style.NumberFormat.Format = "#,##0";
                    worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(currentRow, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    if (currentRow % 2 == 0)
                    {
                        worksheet.Row(currentRow).Cells(1, 6).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#F4F7FA"));
                    }

                    currentRow++;
                }

                worksheet.Columns(1, 6).AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    string excelName = $"Raport_Cmentarz_{year}_{month:02d}.xlsx";

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        excelName
                    );
                }
            }
        }
    }
}