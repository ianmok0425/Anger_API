using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace Anger_API.Service.Admin.RunningText
{
    using Database.RunningTexts;
    public class RunningTextService : IRunningTextService
    {
        public IRunningTextRepository RunningTextRepo { get; }
        public RunningTextService(IRunningTextRepository runningTextRepo)
        {
            RunningTextRepo = runningTextRepo ?? throw new ArgumentNullException(nameof(RunningTextRepo));
        }
        public async Task<ExcelPackage> GenerateList(DateTime? createdOn)
        {
            ExcelPackage excel = new ExcelPackage();
            string fileName = "Running Text List";
            SetExcelProperties(excel, fileName);
            List<RunningText> rts = await RunningTextRepo.RetrieveAll();
            ExcelWorksheet worksheet = CreateWorkSheet(excel, "List", rts);
            return excel;
        }
        private ExcelWorksheet CreateWorkSheet(ExcelPackage excel, string sheetName, List<RunningText> rts)
        {
            excel.Workbook.Worksheets.Add(sheetName);

            ExcelWorksheet workSheet = excel.Workbook.Worksheets[sheetName];
            workSheet.Name = sheetName;
            workSheet.Cells.Style.Font.Size = 11;
            workSheet.Cells.Style.Font.Name = "Calibri";
            workSheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

            workSheet = ConstructHeader(workSheet);
            int rowNo = 2;
            foreach(var rt in rts)
            {
                workSheet = ConstructRow(workSheet, rowNo, rt);
                rowNo += 1;
            }
            workSheet.Cells.AutoFitColumns();
            return workSheet;
        }
        private ExcelWorksheet ConstructRow(ExcelWorksheet worksheet, int rn, RunningText rt)
        {
            worksheet.Cells[rn, 1].Value = rt.ID;
            worksheet.Cells[rn, 2].Value = rt.Content;
            worksheet.Cells[rn, 3].Value = rt.MemberID;
            worksheet.Cells[rn, 4].Value = rt.PostAt;
            worksheet.Cells[rn, 5].Value = rt.Approved;
            worksheet.Cells[rn, 6].Value = rt.ApprovedAt;
            worksheet.Cells[rn, 7].Value = rt.Rejected;
            worksheet.Cells[rn, 8].Value = rt.RejectedAt;
            worksheet.Cells[rn, 9].Value = rt.RejectReasonID;
            worksheet.Cells[rn, 10].Value = rt.UpdatedAt.HasValue ? rt.UpdatedAt.Value.AddHours(8).ToString() : null;
            worksheet.Cells[rn, 11].Value = rt.CreatedAt.AddHours(8).ToString();
            return worksheet;
        }
        private ExcelWorksheet ConstructHeader(ExcelWorksheet worksheet)
        {
            worksheet.Cells[1, 1].Value = nameof(RunningText.ID);
            worksheet.Cells[1, 2].Value = nameof(RunningText.Content);
            worksheet.Cells[1, 3].Value = nameof(RunningText.MemberID);
            worksheet.Cells[1, 4].Value = nameof(RunningText.PostAt);
            worksheet.Cells[1, 5].Value = nameof(RunningText.Approved);
            worksheet.Cells[1, 6].Value = nameof(RunningText.ApprovedAt);
            worksheet.Cells[1, 7].Value = nameof(RunningText.Rejected);
            worksheet.Cells[1, 8].Value = nameof(RunningText.RejectedAt);
            worksheet.Cells[1, 9].Value = nameof(RunningText.RejectReasonID);
            worksheet.Cells[1, 10].Value = nameof(RunningText.UpdatedAt);
            worksheet.Cells[1, 11].Value = nameof(RunningText.CreatedAt);
            worksheet.Row(1).Style.Font.Bold = true;
            return worksheet;
        }
        private void SetExcelProperties(ExcelPackage excel, string title)
        {
            excel.Workbook.Properties.Author = "HSPM CRM";
            excel.Workbook.Properties.Title = title;
        }
    }
}