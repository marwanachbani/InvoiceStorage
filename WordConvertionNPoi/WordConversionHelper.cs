using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordConvertionNPoi.Models;

namespace WordConvertionNPoi
{
    public class WordConversionHelper
    {
        public async Task<string> ConvertInputInvoiceToWordWithoutShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                // Create a new Word document using EPPlus
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;// Add a worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Invoice");

                    // Set up header row
                    worksheet.Cells[1, 1].Value = "Product Name";
                    worksheet.Cells[1, 2].Value = "Quantity";
                    worksheet.Cells[1, 3].Value = "Price";
                    worksheet.Cells[1, 4].Value = "Total";

                    // Fill data rows
                    int row = 2;
                    foreach (var item in items)
                    {
                        worksheet.Cells[row, 1].Value = item.ProductName;
                        worksheet.Cells[row, 2].Value = item.Quantity;
                        worksheet.Cells[row, 3].Value = item.Price;
                        worksheet.Cells[row, 4].Value = item.Total;
                        row++;
                    }

                    // Get the documents folder path
                    string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    // Construct the file path
                    string filePath = Path.Combine(documentsFolderPath, "invoice.xlsx");

                    // Save the Excel package to a file
                    File.WriteAllBytes(filePath, package.GetAsByteArray());

                    await Task.CompletedTask;
                    return $"Excel file created successfully at {filePath}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> ConvertInputInvoiceToWordWithShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total, string shippingMethod, string ShippingTo, decimal shippingPrice)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                // Create a new Word document using EPPlus
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Add a worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Invoice");

                    // Set up header row
                    worksheet.Cells[1, 1].Value = "Product Name";
                    worksheet.Cells[1, 2].Value = "Quantity";
                    worksheet.Cells[1, 3].Value = "Price";
                    worksheet.Cells[1, 4].Value = "Total";

                    // Fill data rows
                    int row = 2;
                    foreach (var item in items)
                    {
                        worksheet.Cells[row, 1].Value = item.ProductName;
                        worksheet.Cells[row, 2].Value = item.Quantity;
                        worksheet.Cells[row, 3].Value = item.Price;
                        worksheet.Cells[row, 4].Value = item.Total;
                        row++;
                    }

                    // Add shipping details
                    worksheet.Cells[row, 1].Value = "Shipping Method";
                    worksheet.Cells[row, 2].Value = "Shipping To";
                    worksheet.Cells[row, 3].Value = "Shipping Price";
                    worksheet.Cells[row, 4].Value = "";

                    worksheet.Cells[row + 1, 1].Value = shippingMethod;
                    worksheet.Cells[row + 1, 2].Value = ShippingTo;
                    worksheet.Cells[row + 1, 3].Value = shippingPrice;

                    // Get the documents folder path
                    string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    // Construct the file path
                    string filePath = Path.Combine(documentsFolderPath, "invoice.xlsx");

                    // Save the Excel package to a file
                    File.WriteAllBytes(filePath, package.GetAsByteArray());

                    await Task.CompletedTask;
                    return $"Excel file created successfully at {filePath}";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> ConvertInputInvoiceToExcelWithoutShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total)
        {
            try
            {
                // Create a new Excel workbook
                IWorkbook workbook = new XSSFWorkbook();

                // Create a new Excel sheet
                ISheet sheet = workbook.CreateSheet("Invoice");

                // Create cell styles
                ICellStyle headerStyle = CreateHeaderCellStyle(workbook);
                ICellStyle dataStyle = CreateDataCellStyle(workbook);

                // Set up initial row index
                int rowIndex = 0;

                // Create and set header row
                IRow headerRow = sheet.CreateRow(rowIndex++);
                headerRow.RowStyle = headerStyle;
                headerRow.CreateCell(0).SetCellValue("Invoice Number");
                headerRow.CreateCell(1).SetCellValue("Date");
                headerRow.CreateCell(2).SetCellValue("Customer");
                // Add more headers as needed

                // Create and set data rows for invoice items
                foreach (var item in items)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex++);
                    dataRow.RowStyle = dataStyle;
                    dataRow.CreateCell(0).SetCellValue(item.ProductName);
                    dataRow.CreateCell(1).SetCellValue(item.Quantity);
                    dataRow.CreateCell(2).SetCellValue(item.Price.ToString());
                    dataRow.CreateCell(3).SetCellValue(item.Total.ToString());
                    // Add more data cells as needed
                }

                // Get the documents folder path
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Construct the file path
                string filePath = Path.Combine(documentsFolderPath, "invoice.xlsx");

                // Write the workbook content to a file
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream);
                }
                await Task.CompletedTask;
                return $"Excel file created successfully at {filePath}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Helper method to create cell style for header row
        private ICellStyle CreateHeaderCellStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.FillForegroundColor = IndexedColors.LightBlue.Index;
            style.FillPattern = FillPattern.SolidForeground;
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;

            // Add more styling as needed

            return style;
        }

        // Helper method to create cell style for data rows
        private ICellStyle CreateDataCellStyle(IWorkbook workbook)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;
            style.VerticalAlignment = VerticalAlignment.Center;

            // Add more styling as needed

            return style;
        }

        [Obsolete]
        public async Task<string> ConvertInputInvoiceToExcelWithShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total, string shippingMethod, string ShippingTo, decimal shippingPrice)
        {
            try
            {
                // Create a new Excel workbook
                IWorkbook workbook = new XSSFWorkbook();

                // Create a worksheet
                ISheet sheet = workbook.CreateSheet("Invoice");

                // Set up cell styles for headers and data
                ICellStyle headerStyle = workbook.CreateCellStyle();
                IFont headerFont = workbook.CreateFont();
                headerFont.Boldweight = (short)FontBoldWeight.Bold;
                headerStyle.SetFont(headerFont);

                // Create header row
                IRow headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("Product Name");
                headerRow.CreateCell(1).SetCellValue("Quantity");
                headerRow.CreateCell(2).SetCellValue("Price");
                headerRow.CreateCell(3).SetCellValue("Total");

                // Fill data rows
                int rowNum = 1;
                foreach (var item in items)
                {
                    IRow row = sheet.CreateRow(rowNum++);
                    row.CreateCell(0).SetCellValue(item.ProductName);
                    row.CreateCell(1).SetCellValue(item.Quantity);
                    row.CreateCell(2).SetCellValue(item.Price.ToString());
                    row.CreateCell(3).SetCellValue(item.Total.ToString());
                }

                // Add shipping details
                IRow shippingRow = sheet.CreateRow(rowNum++);
                shippingRow.CreateCell(0).SetCellValue("Shipping Method");
                shippingRow.CreateCell(1).SetCellValue("Shipping To");
                shippingRow.CreateCell(2).SetCellValue("Shipping Price");
                shippingRow.CreateCell(3).SetCellValue("");
                shippingRow.CreateCell(0).CellStyle = headerStyle;
                shippingRow.CreateCell(1).CellStyle = headerStyle;
                shippingRow.CreateCell(2).CellStyle = headerStyle;

                IRow shippingData = sheet.CreateRow(rowNum++);
                shippingData.CreateCell(0).SetCellValue(shippingMethod);
                shippingData.CreateCell(1).SetCellValue(ShippingTo);
                shippingData.CreateCell(2).SetCellValue(shippingPrice.ToString());

                // Add tax, reduction, and total
                IRow taxRow = sheet.CreateRow(rowNum++);
                taxRow.CreateCell(0).SetCellValue($"Tax: {taxpercent}%");
                taxRow.CreateCell(1).SetCellValue($"{tax}$");

                IRow redcRow = sheet.CreateRow(rowNum++);
                redcRow.CreateCell(0).SetCellValue($"Reduction: {redcpercent}%");
                redcRow.CreateCell(1).SetCellValue($"{redc}$");

                IRow totalRow = sheet.CreateRow(rowNum++);
                totalRow.CreateCell(0).SetCellValue("Total");
                totalRow.CreateCell(1).SetCellValue($"{total}$");

                // Auto-size columns
                for (int i = 0; i < 4; i++)
                {
                    sheet.AutoSizeColumn(i);
                }

                // Get the documents folder path
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Construct the file path
                string filePath = Path.Combine(documentsFolderPath, "invoice.xlsx");

                // Save the workbook to the file
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    workbook.Write(fileStream);
                }
                await Task.CompletedTask;
                return $"Excel file created successfully at {filePath}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
       
    }
}
