using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using Anger_API.Library;
using Anger_API.API.Models.Admins;

namespace Anger_API.Service.Admin.RunningText
{
    using Database.RunningTexts;
    public class RunningTextService : IRunningTextService
    {
        private string[] CharArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        public IRunningTextRepository RunningTextRepo { get; }
        public RunningTextService(IRunningTextRepository runningTextRepo)
        {
            RunningTextRepo = runningTextRepo ?? throw new ArgumentNullException(nameof(RunningTextRepo));
        }
        public async Task<ExcelPackage> GenerateList(ActionType actionType, DateTime? createdOn)
        {
            ExcelPackage excel = new ExcelPackage();
            string fileName = "Running Text List";
            SetExcelProperties(excel, fileName);
            List<RunningText> rts = new List<RunningText>();
            switch (actionType)
            {
                case ActionType.All:
                    rts = await RunningTextRepo.RetrieveAll(createdOn);
                    break;
            }
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
            worksheet.Cells[rn, 4].Value = rt.PostAt.HasValue ? rt.PostAt.Value.ToHKTime().ToString() : null;
            SetBoolean(rn, 5, rt.Approved, ref worksheet);
            worksheet.Cells[rn, 6].Value = rt.ApprovedAt.HasValue ? rt.ApprovedAt.Value.ToHKTime().ToString() : null;
            SetBoolean(rn, 7, rt.Rejected, ref worksheet);
            worksheet.Cells[rn, 8].Value = rt.RejectedAt.HasValue ? rt.RejectedAt.Value.ToHKTime().ToString() : null;
            worksheet.Cells[rn, 9].Value = rt.RejectReason;
            worksheet.Cells[rn, 10].Value = rt.UpdatedAt.HasValue ? rt.UpdatedAt.Value.ToHKTime().ToString() : null;
            worksheet.Cells[rn, 11].Value = rt.CreatedAt.ToHKTime().ToString();
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
            worksheet.Cells[1, 9].Value = nameof(RunningText.RejectReason);
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
        private void SetBoolean(int rowNo, int colNo, bool? value, ref ExcelWorksheet ws)
        {
            ws.Cells[rowNo, colNo].Value = value;
            var booleanValidation = ws.DataValidations.AddListValidation($"{CharArray[colNo - 1]}{rowNo}");
            booleanValidation.Formula.Values.Add("true");
            booleanValidation.Formula.Values.Add("false");
            booleanValidation.AllowBlank = true;
            booleanValidation.ShowErrorMessage = true;
        }
    }
}