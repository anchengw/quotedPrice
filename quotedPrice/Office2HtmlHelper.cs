using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;

namespace quotedPrice
{
    public class Office2HtmlHelper
    {
        #region Index页面
        /// <summary>
        /// Index页面
        /// </summary>
        /// <param name="url">例：/uploads/......XXX.xls</param>
        public static string ActionResult(string physicalPath)
        {
            string extension = Path.GetExtension(physicalPath);
            string htmlUrl = "";
            switch (extension.ToLower())
            {
                case ".xls":
                case ".xlsx":
                    htmlUrl = PreviewExcel(physicalPath);
                    break;
                case ".doc":
                case ".docx":
                    htmlUrl = PreviewWord(physicalPath);
                    break;
                case ".txt":
                    //htmlUrl = PreviewTxt(physicalPath, url);
                    break;
                case ".pdf":
                    //htmlUrl = PreviewPdf(physicalPath, url);
                    break;
            }
            return  htmlUrl;
        }
        #endregion
        #region 预览Excel
        /// <summary>
        /// 预览Excel
        /// </summary>
        public static string PreviewExcel(string physicalPath)
        {
            Microsoft.Office.Interop.Excel.Application application = null;
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            application = new Microsoft.Office.Interop.Excel.Application();
            object missing = Type.Missing;
            object trueObject = true;
            application.Visible = false;
            application.DisplayAlerts = false;
            workbook = application.Workbooks.Open(physicalPath, missing, trueObject, missing, missing, missing,
              missing, missing, missing, missing, missing, missing, missing, missing, missing);
            //Save Excel to Html
            object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml;
            string htmlName = Path.GetFileNameWithoutExtension(physicalPath) + ".html";
            String outputFile = Path.GetDirectoryName(physicalPath) + "\\" + htmlName;
            try
            {
                workbook.SaveAs(outputFile, format, missing, missing, missing,  //文件夹权限问题 COMException (0x800A03EC)
                         missing, XlSaveAsAccessMode.xlNoChange, missing,
                         missing, missing, missing, missing);
            }
            finally
            {
                workbook.Close();
                application.Quit();
                Kill(application);
            }
            return outputFile;
        }
        #endregion
        #region 预览Word
        /// <summary>
        /// 预览Word
        /// </summary>
        public static string PreviewWord(string physicalPath)
        {
            Microsoft.Office.Interop.Word._Application application = null;
            Microsoft.Office.Interop.Word._Document doc = null;
            application = new Microsoft.Office.Interop.Word.Application();
            object missing = Type.Missing;
            object trueObject = true;
            application.Visible = false;
            application.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            doc = application.Documents.Open(physicalPath, missing, trueObject, missing, missing, missing,
              missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            //Save Excel to Html
            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatHTML;
            string htmlName = Path.GetFileNameWithoutExtension(physicalPath) + ".html";
            String outputFile = Path.GetDirectoryName(physicalPath) + "\\" + htmlName;
            doc.SaveAs(outputFile, format, missing, missing, missing,
                     missing, XlSaveAsAccessMode.xlNoChange, missing,
                     missing, missing, missing, missing);
            doc.Close();
            application.Quit();
            return outputFile;
        }
        #endregion
        
        #region 只有安装了SaveAsPDFandXPS.exe，程序操作office文档，才有office文档另存为pdf文件。office2010不需要安装了，内置有这个功能。
        /// <summary>
        /// Word转换成pdf
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>true=转换成功</returns>
        public static bool DOCConvertToPDF(string sourcePath, string targetPath)
        {
            bool result = false;
            Microsoft.Office.Interop.Word.WdExportFormat exportFormat = Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF;
            object paramMissing = Type.Missing;
            Microsoft.Office.Interop.Word.ApplicationClass wordApplication = new Microsoft.Office.Interop.Word.ApplicationClass();
            Microsoft.Office.Interop.Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;
                Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = exportFormat;
                bool paramOpenAfterExport = false;
                Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor = Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks = Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;
                wordDocument = wordApplication.Documents.Open(
                    ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing, ref paramMissing, ref paramMissing,
                    ref paramMissing);
                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                        paramExportFormat, paramOpenAfterExport,
                        paramExportOptimizeFor, paramExportRange, paramStartPage,
                        paramEndPage, paramExportItem, paramIncludeDocProps,
                        paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                        paramBitmapMissingFonts, paramUseISO19005_1,
                        ref paramMissing);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (wordDocument != null)
                {
                    wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// 把Excel文件转换成PDF格式文件  
        /// </summary>
        /// <param name="sourcePath">源文件路径</param>
        /// <param name="targetPath">目标文件路径</param>
        /// <returns>true=转换成功</returns>
        public static bool XLSConvertToPDF(string sourcePath, string targetPath)
        {
            bool result = false;
            Microsoft.Office.Interop.Excel.XlFixedFormatType targetType = Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF;
            object missing = Type.Missing;
            Microsoft.Office.Interop.Excel.ApplicationClass application = null;
            Microsoft.Office.Interop.Excel.Workbook workBook = null;
            try
            {
                application = new Microsoft.Office.Interop.Excel.ApplicationClass();
                object target = targetPath;
                object type = targetType;
                workBook = application.Workbooks.Open(sourcePath, missing, missing, missing, missing, missing,
                    missing, missing, missing, missing, missing, missing, missing, missing, missing);
                workBook.ExportAsFixedFormat(targetType, target, Microsoft.Office.Interop.Excel.XlFixedFormatQuality.xlQualityStandard, true, false, missing, missing, missing, missing);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close(true, missing, missing);
                    workBook = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }
        ///<summary>        
        /// 把PowerPoint文件转换成PDF格式文件       
        ///</summary>        
        ///<param name="sourcePath">源文件路径</param>     
        ///<param name="targetPath">目标文件路径</param> 
        ///<returns>true=转换成功</returns> 
        public static bool PPTConvertToPDF(string sourcePath, string targetPath)
        {
            bool result;
            Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType targetFileType =  Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsPDF;
            object missing = Type.Missing;
             Microsoft.Office.Interop.PowerPoint.ApplicationClass application = null;
             Microsoft.Office.Interop.PowerPoint.Presentation persentation = null;
            try
            {
                application = new  Microsoft.Office.Interop.PowerPoint.ApplicationClass();
                persentation = application.Presentations.Open(sourcePath, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse); persentation.SaveAs(targetPath, targetFileType, Microsoft.Office.Core.MsoTriState.msoTrue);
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (persentation != null)
                {
                    persentation.Close();
                    persentation = null;
                }
                if (application != null)
                {
                    application.Quit();
                    application = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }
        #endregion
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        /// <summary>
        /// 结束EXCEL进程 最好的方法
        /// </summary>
        /// <param name="excel"></param>
        public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
        {
            try
            {
                IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口 
                int k = 0;
                GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用
                p.Kill();     //关闭进程k
            }
            catch
            { }
        }
    }
}
