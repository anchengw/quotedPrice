/*
 类：Export
 描述：导出
 编 码 人：韩兆新 日期：2015年01月17日
 修改记录：

*/

using NPOI.SS.UserModel;
using NPOI.Extend;
using System.IO;

namespace ExcelReport
{
    public static class Export
    {
        /// <summary>
        ///     导出格式化处理后的文件到二进制文件流
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="sheetFormatters"></param>
        /// <returns></returns>
        public static byte[] ExportToBuffer(string templateFile, params SheetFormatter[] sheetFormatters)
        {
            IWorkbook workbook = NPOIHelper.LoadWorkbook(templateFile);
            foreach (SheetFormatter sheetFormatter in sheetFormatters)
            {
                sheetFormatter.Format(workbook);
            }
            return workbook.SaveToBuffer();
        }
        /// <summary>
        ///     导出格式化处理后的文件到内存流
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="sheetFormatters"></param>
        /// <returns></returns>
        public static Stream ExportToStream(string templateFile, params SheetFormatter[] sheetFormatters)
        {
            IWorkbook workbook = NPOIHelper.LoadWorkbook(templateFile);
            foreach (SheetFormatter sheetFormatter in sheetFormatters)
            {
                sheetFormatter.Format(workbook);
            }
            var ms = new MemoryStream();
            workbook.Write(ms);
            return ms;
        }
        /// <summary>
        ///     导出格式化处理后的文件到内存流
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="sheetFormatters"></param>
        /// <returns></returns>
        public static byte[] ExportToBuffer(Stream ExcelStream, params SheetFormatter[] sheetFormatters)
        {
            IWorkbook workbook = WorkbookFactory.Create(ExcelStream);
            foreach (SheetFormatter sheetFormatter in sheetFormatters)
            {
                sheetFormatter.Format(workbook);
            }
            return workbook.SaveToBuffer();
        }
    }
}