using System;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Windows.Forms;
using NPOI.HPSF;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Collections.Generic;

namespace quotedPrice
{
    public class NPOIExcelHelp
    {
        /// <summary>
        /// 获取要保存的文件名称（含完整路径）
        /// </summary>
        /// <returns></returns>
        private static string GetSaveFilePath()
        {
            SaveFileDialog saveFileDig = new SaveFileDialog();
            saveFileDig.Filter = "Excel Office97-2003(*.xls)|.xls|Excel Office2007及以上(*.xlsx)|*.xlsx";
            saveFileDig.FilterIndex = 0;
            saveFileDig.OverwritePrompt = true;
            saveFileDig.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = null;
            if (saveFileDig.ShowDialog() == DialogResult.OK)
            {
                filePath = saveFileDig.FileName;
            }

            return filePath;
        }
        //只支持2003以下版本
        public static void setFileInfo(HSSFWorkbook workbook)
        {
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "NPOI";
            workbook.DocumentSummaryInformation = dsi;

            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Author = "文件作者信息";
            si.ApplicationName = "创建程序信息";
            si.LastAuthor = "最后保存者信息";
            si.Comments = "作者信息";
            si.Title = "标题信息";
            si.Subject = "主题信息";
            si.CreateDateTime = DateTime.Now;
            workbook.SummaryInformation = si;
        }
        //设置列宽 列号从0开始
        public static void setColWidth(ISheet sheet, int col, int width)
        {
            sheet.SetColumnWidth(col, width * 256);
        }
        //设置行高
        public static void setRowHeight(IRow row, short height)
        {
            row.Height = height;
        }
        //设置单元格样式
        public static void setCellStyle(IWorkbook workbook, ICell cell)
        {
            ICellStyle style = workbook.CreateCellStyle();
            //设置单元格的样式：水平对齐居中
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //垂直居中
            style.VerticalAlignment = VerticalAlignment.Center;
            //设置边框
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Dotted;//NPOI.SS.UserModel.BorderStyle.Thin; 
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Dotted;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Dotted;
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Dotted;
            //新建一个字体样式对象
            IFont font = workbook.CreateFont();
            //设置字体加粗样式
            font.Boldweight = short.MaxValue;
            //使用SetFont方法将字体样式添加到单元格样式中 
            style.SetFont(font);
            //将新的样式赋给单元格
            cell.CellStyle = style;
        }
        //合并单元格
        public static void mergeCell(ISheet sheet,int sRow,int eRow,int sCol,int eCol)
        {
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet.AddMergedRegion(new CellRangeAddress(sRow, eRow, sCol, eCol));
        }
        //设置合并单元格边框
        public static void setBorderForMergeCell(int border, CellRangeAddress region,ISheet sheet,IWorkbook workBook)
        {
            sheet.AddMergedRegion(region);
            RegionUtil.SetBorderBottom(1, region, sheet, workBook);
            RegionUtil.SetBorderLeft(1, region, sheet, workBook);
            RegionUtil.SetBorderRight(1, region, sheet, workBook);
            RegionUtil.SetBorderTop(1, region, sheet, workBook);
        }
        //设置单元格公式
        public static void setCellFormula(ICell cell,string formula)
        {
            //cell.CellFormula = "HYPERLINK(\"测试图片.jpg\",\"测试图片.jpg\")";
            cell.CellFormula = formula;
        }
        //插入行
        public static void insertRow(ISheet sheet,int startRow, int endRow, int rowNum)
        {
            sheet.ShiftRows(startRow, endRow, rowNum);
        }
        //插入列
        public static void insertCol()
        {

        }
        //复制单元格样式
        public static void CopyCellStyle(IWorkbook wb, ICellStyle fromStyle, ICellStyle toStyle)
        {
            toStyle.Alignment = fromStyle.Alignment;
            //边框和边框颜色
            toStyle.BorderBottom = fromStyle.BorderBottom;
            toStyle.BorderLeft = fromStyle.BorderLeft;
            toStyle.BorderRight = fromStyle.BorderRight;
            toStyle.BorderTop = fromStyle.BorderTop;
            toStyle.TopBorderColor = fromStyle.TopBorderColor;
            toStyle.BottomBorderColor = fromStyle.BottomBorderColor;
            toStyle.RightBorderColor = fromStyle.RightBorderColor;
            toStyle.LeftBorderColor = fromStyle.LeftBorderColor;
            //背景和前景
            toStyle.FillBackgroundColor = fromStyle.FillBackgroundColor;
            toStyle.FillForegroundColor = fromStyle.FillForegroundColor;
            toStyle.DataFormat = fromStyle.DataFormat;
            toStyle.FillPattern = fromStyle.FillPattern;
            //toStyle.Hidden=fromStyle.Hidden;
            toStyle.IsHidden = fromStyle.IsHidden;
            toStyle.Indention = fromStyle.Indention;//首行缩进
            toStyle.IsLocked = fromStyle.IsLocked;
            toStyle.Rotation = fromStyle.Rotation;//旋转
            toStyle.VerticalAlignment = fromStyle.VerticalAlignment;
            toStyle.WrapText = fromStyle.WrapText;
            toStyle.SetFont(fromStyle.GetFont(wb));
        }
        /// <summary>
        /// 复制原有sheet的合并单元格到新创建的sheet
        /// </summary>
        /// <param name="fromSheet"></param>
        /// <param name="toSheet"></param>
        public static void MergerRegion(ISheet fromSheet, ISheet toSheet)
        {
            int sheetMergerCount = fromSheet.NumMergedRegions;
            for (int i = 0; i < sheetMergerCount; i++)
            {
                //Region mergedRegionAt = fromSheet.GetMergedRegion(i); //.MergedRegionAt(i);
                //CellRangeAddress[] cra = new CellRangeAddress[1];
                //cra[0] = fromSheet.GetMergedRegion(i);
                //Region[] rg = Region.ConvertCellRangesToRegions(cra);
                toSheet.AddMergedRegion(fromSheet.GetMergedRegion(i));
            }
        }
        /// <summary>
        /// 复制表
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fromSheet"></param>
        /// <param name="toSheet"></param>
        /// <param name="copyValueFlag"></param>
        public static void CopySheet(IWorkbook wb, ISheet fromSheet, ISheet toSheet, bool copyValueFlag)
        {
            //合并区域处理
            MergerRegion(fromSheet, toSheet);
            System.Collections.IEnumerator rows = fromSheet.GetRowEnumerator();
            while (rows.MoveNext())
            {
                IRow row = null;
                if (wb is HSSFWorkbook)
                    row = rows.Current as HSSFRow;
                else
                    row = rows.Current as NPOI.XSSF.UserModel.XSSFRow;
                IRow newRow = toSheet.CreateRow(row.RowNum);
                CopyRow(wb, row, newRow, copyValueFlag);
            }
        }
        /// <summary>
        /// 复制行
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fromRow"></param>
        /// <param name="toRow"></param>
        /// <param name="copyValueFlag"></param>
        public static void CopyRow(IWorkbook wb, IRow fromRow, IRow toRow, bool copyValueFlag)
        {
            System.Collections.IEnumerator cells = fromRow.GetEnumerator();//.GetRowEnumerator();
            toRow.Height = fromRow.Height;
            while (cells.MoveNext())
            {
                ICell cell = null;
                //ICell cell = (wb is HSSFWorkbook) ? cells.Current as HSSFCell : cells.Current as NPOI.XSSF.UserModel.XSSFCell;
                if (wb is HSSFWorkbook)
                    cell = cells.Current as HSSFCell;
                else
                    cell = cells.Current as NPOI.XSSF.UserModel.XSSFCell;
                ICell newCell = toRow.CreateCell(cell.ColumnIndex);
                CopyCell(wb, cell, newCell, copyValueFlag);
            }
        }
        /// <summary>
        /// 复制单元格
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="srcCell"></param>
        /// <param name="distCell"></param>
        /// <param name="copyValueFlag"></param>
        public static void CopyCell(IWorkbook wb, ICell srcCell, ICell distCell, bool copyValueFlag)
        {
            ICellStyle newstyle = wb.CreateCellStyle();
            CopyCellStyle(wb, srcCell.CellStyle, newstyle);
            //样式
            distCell.CellStyle = newstyle;
            //评论
            if (srcCell.CellComment != null)
            {
                distCell.CellComment = srcCell.CellComment;
            }
            // 不同数据类型处理
            CellType srcCellType = srcCell.CellType;
            distCell.SetCellType(srcCellType);
            if (copyValueFlag)
            {
                if (srcCellType == CellType.Numeric)
                {
                    if (HSSFDateUtil.IsCellDateFormatted(srcCell))
                    {
                        distCell.SetCellValue(srcCell.DateCellValue);
                    }
                    else
                    {
                        distCell.SetCellValue(srcCell.NumericCellValue);
                    }
                }
                else if (srcCellType == CellType.String)
                {
                    distCell.SetCellValue(srcCell.RichStringCellValue);
                }
                else if (srcCellType == CellType.Blank)
                {
                    // nothing21
                }
                else if (srcCellType == CellType.Boolean)
                {
                    distCell.SetCellValue(srcCell.BooleanCellValue);
                }
                else if (srcCellType == CellType.Error)
                {
                    distCell.SetCellErrorValue(srcCell.ErrorCellValue);
                }
                else if (srcCellType == CellType.Formula)
                {
                    distCell.SetCellFormula(srcCell.CellFormula);
                }
                else
                { // nothing29
                }
            }
        }
    
    //带格式复制区域
    public static void CopyRange(ISheet sheet, int fromRowIndex, int fromColIndex, int toRowIndex, int toColIndex, bool onlyData)
        {
            IRow sourceRow = sheet.GetRow(fromRowIndex);
            ICell sourceCell = sourceRow.GetCell(fromColIndex);
            if (sourceRow != null && sourceCell != null)
            {
                IRow changingRow = null;
                ICell changingCell = null;
                changingRow = sheet.GetRow(toRowIndex);
                if (changingRow == null)
                    changingRow = sheet.CreateRow(toRowIndex);
                changingCell = changingRow.GetCell(toColIndex);
                if (changingCell == null)
                    changingCell = changingRow.CreateCell(toColIndex);

                if (onlyData)//仅数据
                {
                    //对单元格的值赋值
                    changingCell.SetCellValue(sourceCell.StringCellValue);
                }
                else         //非仅数据
                {
                    //单元格的编码
                    //changingCell.Encoding = sourceCell.Encoding;
                    //单元格的格式
                    changingCell.CellStyle = sourceCell.CellStyle;
                    //单元格的公式
                    //if (sourceCell.CellFormula == "")
                    changingCell.SetCellValue(sourceCell.StringCellValue);
                    //else
                    //    changingCell.SetCellFormula(sourceCell.CellFormula);
                }
            }
        }
        /// <summary>
        /// 判断是否为兼容模式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool GetIsCompatible(string filePath)
        {
            return filePath.EndsWith(".xls", StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// 创建工作薄(依据文件扩展名)
        /// </summary>
        /// <param name="ExcelPath"></param>
        /// <returns></returns>

        private static IWorkbook CreateWorkbook(string ExcelPath)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(ExcelPath).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            return workbook;
        }
        /// <summary>
        /// 读取2007Excel[.xlsx]或读取2003Excel[.xls](返回DataTable)
        /// </summary>
        /// <param name="path">Excel路径</param>
        /// <param name="sheetName">表名</param>
        /// <param name="isheader">第一行是否列名，否表默认列名为column1，column2等</param>
        /// <returns>表</returns>
        public static DataTable ReadExcel(string path,string sheetName, bool isheader)
        {
            try
            {
                DataTable dt = new DataTable();
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    //IWorkbook workbook = new XSSFWorkbook(fs);//2007
                    //IWorkbook workbook = new HSSFWorkbook(fs);//2003
                    IWorkbook workbook = WorkbookFactory.Create(fs);//工厂模式
                    ISheet sheet;
                    if (string.IsNullOrEmpty(sheetName))
                       sheet = workbook.GetSheetAt(0); //取第一个工作表
                    else
                       sheet = workbook.GetSheet(sheetName);
                    IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(workbook);
                    int rfirst = sheet.FirstRowNum;//工作表第一行
                    int rlast = sheet.LastRowNum; //工作表最后一行
                    IRow row = sheet.GetRow(rfirst);
                    int cfirst = row.FirstCellNum;//工作表第一列
                    int clast = row.LastCellNum;//工作表最后一列
                    //构建表列
                    if (isheader)
                    {
                        for (int i = cfirst; i < clast; i++)
                        {
                            if (row.GetCell(i) != null)
                                dt.Columns.Add(row.GetCell(i).StringCellValue, System.Type.GetType("System.String"));
                        }
                        rfirst = rfirst + 1;
                        row = null;
                    }
                    else
                    {
                        for (int i = cfirst; i < clast; i++)
                        {
                            //DataColumn column = new DataColumn("column" + (i + 1));
                            dt.Columns.Add("column" + (i + 1), System.Type.GetType("System.String"));
                        }
                    }
                    for (int i = rfirst; i <= rlast; i++)
                    {
                        DataRow r = dt.NewRow();
                        IRow ir = sheet.GetRow(i);
                        for (int j = cfirst; j < clast; j++)
                        {
                            ICell cell = evaluator.EvaluateInCell(ir.GetCell(j));
                            if (cell != null)
                            {
                                //if (cell.CellType == CellType.Formula)
                                //{
                                //    r[j] = cell.NumericCellValue.ToString();//cell.NumericCellValue取包括公式的值 IFormulaEvaluator evaluator = WorkbookFactory.CreateFormulaEvaluator(workbook); cell = e.EvaluateInCell(cell);
                                // }
                                //else
                                r[j] = ir.GetCell(j).ToString();
                            }
                        }
                        dt.Rows.Add(r);
                        ir = null;
                        r = null;
                    }
                    sheet = null;
                    workbook = null;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw (ex);//System.Windows.Forms.MessageBox.Show("Excel格式错误或者Excel正由另一进程在访问");
            }
        }
        /// <summary>
        /// 取单元格地址
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string GetCellPosition(int row, int col)
        {
            col = Convert.ToInt32('A') + col;
            row = row + 1;
            return ((char)col) + row.ToString();
        }
        /// <summary>
        /// DataTable转成Excel文件
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <returns></returns>
        public static void DataTableToExcel(DataTable dt,string ExcelPath)
        {
            IWorkbook workbook = CreateWorkbook(ExcelPath);           
            if (workbook == null)
                return;
            ISheet sheet = workbook.CreateSheet(string.IsNullOrEmpty(dt.TableName) ? "Sheet1" : dt.TableName);
            //建立Eexcel表头行
            IRow headerRow = sheet.CreateRow(0);
            foreach (DataColumn column in dt.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            //数据
            int rowIndex = 1;
            foreach (DataRow row in dt.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);

                foreach (DataColumn column in dt.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            if (!Directory.Exists(Path.GetDirectoryName(ExcelPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(ExcelPath));
            using (FileStream file = new FileStream(ExcelPath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(file);
                file.Flush();
            }
        }
        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                default:
                    return "=" + cell.CellFormula;
            }
        }
        /// <summary>
        /// 由List导出Excel
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="data">在导出的List</param>
        /// <param name="sheetName">sheet名称</param>
        /// <returns></returns>
        public static string ExportToExcel<T>(List<T> data, IList<KeyValuePair<string, string>> headerNameList, string sheetName = "result", string filePath = null) where T : class
        {
            if (data.Count <= 0) return null;
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }
            if (string.IsNullOrEmpty(filePath)) return null;
            IWorkbook workbook = CreateWorkbook(filePath);
            if (workbook == null) return null;
            ICellStyle cellStyle = workbook.CreateCellStyle();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow headerRow = sheet.CreateRow(0);

            for (int i = 0; i < headerNameList.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(headerNameList[i].Value);
                cell.CellStyle = cellStyle;
            }

            Type t = typeof(T);
            int rowIndex = 1;
            foreach (T item in data)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int n = 0; n < headerNameList.Count; n++)
                {
                    object pValue = t.GetProperty(headerNameList[n].Key).GetValue(item, null);
                    dataRow.CreateCell(n).SetCellValue((pValue ?? "").ToString());
                }
                rowIndex++;
            }
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();

            sheet = null;
            headerRow = null;
            workbook = null;

            return filePath;
        }
        /// <summary>
        /// 由DataGridView导出
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="sheetName"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ExportToExcel(DataGridView grid, string sheetName = "result", string filePath = null)
        {
            if (grid.Rows.Count <= 0) return null;

            if (string.IsNullOrEmpty(filePath))
            {
                filePath = GetSaveFilePath();
            }

            if (string.IsNullOrEmpty(filePath)) return null;

            bool isCompatible = GetIsCompatible(filePath);

            IWorkbook workbook = CreateWorkbook(filePath);
            if (workbook == null) return null;
            ICellStyle cellStyle = workbook.CreateCellStyle();
            ISheet sheet = workbook.CreateSheet(sheetName);

            IRow headerRow = sheet.CreateRow(0);

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                ICell cell = headerRow.CreateCell(i);
                cell.SetCellValue(grid.Columns[i].Name);
                cell.CellStyle = cellStyle;
            }

            int rowIndex = 1;
            foreach (DataGridViewRow row in grid.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                for (int n = 0; n < grid.Columns.Count; n++)
                {
                    dataRow.CreateCell(n).SetCellValue((row.Cells[n].Value ?? "").ToString());
                }
                rowIndex++;
            }

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            workbook.Write(fs);
            fs.Dispose();

            sheet = null;
            headerRow = null;
            workbook = null;

            return filePath;
        }
        public static IWorkbook LoadWorkbook(string Excelfile)
        {
            if (!File.Exists(Excelfile))
                return null;
            IWorkbook workbook = null;
            using (FileStream fs = new FileStream(Excelfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                workbook = WorkbookFactory.Create(fs);//工厂模式
            }
            return workbook;
        }
        public bool DataTableToExcel(DataTable dt, string mobanfile, string saveExcel)
        {
            #region 声明变量
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            FileStream file = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            #endregion

            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    //打开Excel模板文件 , 我这里有一个模板Excel文件 , 数据全部写入模板文件
                    file = new FileStream(mobanfile, FileMode.Open, FileAccess.Read);
                    //这里可能会报错 , 是因为系统Excel版本不对应的原因 , 就像Ajax在IE和其他浏览器的声明 , 但我一直找不到好的解决方案 , 如果你有更好的 , 也可以分享一下 , 我这里仅作参考代码
                    workbook = new XSSFWorkbook(file);
                    sheet = workbook.GetSheetAt(0);//获取Excel 中 的sheet  
                    int rowCount = dt.Rows.Count;//查询出数据的row count
                    int columnCount = dt.Columns.Count;//查询出数据的cell count
                    
                    #region 设置Cell边框样式
                    ICellStyle style = workbook.CreateCellStyle();

                    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BottomBorderColor = HSSFColor.Black.Index;
                    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.LeftBorderColor = HSSFColor.Black.Index;
                    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.RightBorderColor = HSSFColor.Black.Index;
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.TopBorderColor = HSSFColor.Black.Index;
                    #endregion

                    //我这里声明3是因为我的模板前两行都有标头数据
                    int result_count = 3;
                    //设置每行每列的单元格  
                    for (int i = 0; i < rowCount; i++)
                    {
                        ////我这里+2是因为我的模板前两行都有标头数据
                        row = sheet.CreateRow(i + 2);

                        for (int j = 0; j <= columnCount; j++)
                        {
                            //这里的判断主要为数据加上序号 , 1,2,3,4...
                            if (j == 0)
                            {
                                cell = row.CreateCell(j);//excel第一行第一格开始写入数据
                                cell.CellStyle = style;//设置边框
                                cell.SetCellValue(result_count + "");//设置Cell的值
                            }
                            else
                            {
                                cell = row.CreateCell(j);//excel第一行第二格开始写入数据  
                                cell.CellStyle = style;//设置边框
                                cell.SetCellValue(dt.Rows[i][j].ToString());//设置Cell的值
                            }

                        }
                        result_count++;
                    }
                    using (fs = File.OpenWrite(saveExcel))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                file.Close();
                return false;
            }
        }
    }
}
