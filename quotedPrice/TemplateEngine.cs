using System;
using System.IO;
using System.Text.RegularExpressions;
using ExcelReport;
using NPOI.SS.UserModel;
using NPOI.Extend;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace quotedPrice
{
    public class TemplateEngine
    {
        private static string sheetName { set; get; }
        public static WorkbookParameterContainer Parse(string templatePath)
        {
            var workbookParameterContainer = new WorkbookParameterContainer();
            string templateConfig = Path.ChangeExtension(templatePath, ".xml");
            if (File.Exists(templateConfig))
            {
                workbookParameterContainer.Load(templateConfig);
                return workbookParameterContainer;
            }
            IWorkbook workbook = NPOIHelper.LoadWorkbook(templatePath);
            ISheet sheet = workbook.GetSheetAt(0);
            sheetName = sheet.SheetName;
            workbookParameterContainer[sheetName] = new SheetParameterContainer
            {
                SheetName = sheetName
            };
            foreach (IRow row in sheet)
            {
                foreach (ICell cell in row.Cells)
                {
                    if (cell.CellType.Equals(CellType.String))
                    {
                        MatchCollection matches = new Regex(@"(?<=\$\[)([\w]*)(?=\])").Matches(cell.StringCellValue);
                        foreach (Match match in matches)
                        {
                            workbookParameterContainer[sheet.SheetName][match.Value] = new Parameter
                            {
                                Name = match.Value,
                                RowIndex = cell.RowIndex,
                                ColumnIndex = cell.ColumnIndex
                            };
                        }
                    }
                }
            }
            workbookParameterContainer.Save(templateConfig);
            return workbookParameterContainer;
        }
        public static void resolveExcel(string temlateFile,string saveExcel, List<chestInfo> list)
        {
            sheetName = "Sheet1";
            var workbookParameterContainer = Parse(temlateFile);
            SheetParameterContainer sheetParameterContainer = workbookParameterContainer[sheetName];

            Stream excelStrem = Export.ExportToStream(temlateFile,
                    new SheetFormatter(sheetName,
                        new RepeaterFormatter<chestInfo>(sheetParameterContainer["repeat_start"],
                            sheetParameterContainer["repeat_end"], list,
                            new CellFormatter<chestInfo>(sheetParameterContainer["Group"], t => t.Group),
                            new CellFormatter<chestInfo>(sheetParameterContainer["total"], t => t.total),
                            new CellFormatter<chestInfo>(sheetParameterContainer["ChestTitle"], t => t.ChestTitle)                            
                           )
                        )
                    );
            ExportHelper.ExportToLocal(temlateFile, saveExcel,
                    new SheetFormatter(sheetName,
                        new RepeaterFormatter<chestInfo>(sheetParameterContainer["repeat1_start"],
                            sheetParameterContainer["repeat1_end"], list,
                                 new CellFormatter<chestInfo>(sheetParameterContainer["IncNum"], t => t.IncNum),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Parts"], t => t.Parts),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Materials"], t => t.Materials),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Color"], t => t.Color),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Brand"], t => t.Brand),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["H"], t => t.H),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["W"], t => t.W),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["D"], t => t.D),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Count"], t => t.Count),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Unit"], t => t.Unit),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Area"], t => t.Area),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Price"], t => t.Price),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Amount"], t => t.Amount),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Remark"], t => t.Remark),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Rprice"], t => t.Rprice),
                                 new CellFormatter<chestInfo>(sheetParameterContainer["Ramount"], t => t.Ramount)
                            )
                        )
                    );
        }
    }
}
