using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
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
            try
            {
                // Create a new Word document
                XWPFDocument document = new XWPFDocument();

                // Create a paragraph for the document
                XWPFParagraph paragraph = document.CreateParagraph();

                // Set up fonts
                XWPFRun run = paragraph.CreateRun();
                run.FontFamily = "Segoe";
                run.FontSize = 14;
                run.IsBold = true;

                // Generate invoice number as hexadecimal GUID
                string invoiceNumber = Guid.NewGuid().ToString("N").ToUpper(); // Convert to upper case

                // Write invoice details to the document
                run.SetText($"Invoice N°: {invoiceNumber}Date: {date.ToShortDateString()}Customer: {customer}");

                // Add a table for invoice items
                XWPFTable table = document.CreateTable(items.Count + 1, 4);

                // Set table width
                table.Width = 5000;

                // Set table headers
                string[] headers = { "Product Name", "Quantity", "Price", "Total" };
                for (int i = 0; i < headers.Length; i++)
                {
                    table.GetRow(0).GetCell(i).SetText(headers[i]);
                }

                // Fill table with invoice items
                for (int i = 0; i < items.Count; i++)
                {
                    table.GetRow(i + 1).GetCell(0).SetText(items[i].ProductName);
                    table.GetRow(i + 1).GetCell(1).SetText(items[i].Quantity.ToString());
                    table.GetRow(i + 1).GetCell(2).SetText($"{items[i].Price}$");
                    table.GetRow(i + 1).GetCell(3).SetText($"{items[i].Total}$");
                }

                // Add tax, reduction, and total to the document
                paragraph = document.CreateParagraph();
                run = paragraph.CreateRun();
                run.FontFamily = "Segoe";
                run.FontSize = 14;
                run.IsBold = true;
                run.SetText($"Total of items: {total}$ Tax: {taxpercent}% - {tax}$ Reduction: {redcpercent}% - {redc}$ Total: {total}$");

                // Save the document to a file
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsFolderPath, "invoice.docx");
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    document.Write(stream);
                }

                await Task.CompletedTask;
                return $"Word document created successfully at {filePath}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> ConvertInputInvoiceToWordWithShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total, string shippingMethod, string ShippingTo, decimal shippingPrice)
        {
            try
            {
                // Create a new Word document
                XWPFDocument document = new XWPFDocument();

                // Create a paragraph for the invoice details
                XWPFParagraph paragraph = document.CreateParagraph();

                // Set up fonts and styling
                XWPFRun run = paragraph.CreateRun();
                run.FontFamily = "Segoe UI";
                run.FontSize = 14;
                run.IsBold = true;

                // Generate invoice number as hexadecimal GUID
                string invoiceNumber = Guid.NewGuid().ToString("N").ToUpper(); // Convert to upper case

                // Write invoice header
                run.SetText("Invoice");
                run.AddBreak();
                run.SetText($"N°: {invoiceNumber}");
                run.AddBreak();
                run.SetText($"Date: {date.ToShortDateString()}");
                run.AddBreak();
                run.SetText($"Customer: {customer}");
                run.AddBreak();
                run.AddBreak(); // Add extra line for spacing

                // Create a table for items list
                XWPFTable table = document.CreateTable(items.Count + 1, 4); // Add 1 row for headers

                // Set table width
                table.Width = 10000; // 100% width

                // Write table headers
                table.GetRow(0).GetCell(0).SetText("Product Name");
                table.GetRow(0).GetCell(1).SetText("Quantity");
                table.GetRow(0).GetCell(2).SetText("Price");
                table.GetRow(0).GetCell(3).SetText("Total");

                // Write invoice items to the table
                for (int i = 0; i < items.Count; i++)
                {
                    table.GetRow(i + 1).GetCell(0).SetText(items[i].ProductName);
                    table.GetRow(i + 1).GetCell(1).SetText(items[i].Quantity.ToString());
                    table.GetRow(i + 1).GetCell(2).SetText($"{items[i].Price.ToString()}$");
                    table.GetRow(i + 1).GetCell(3).SetText($"{items[i].Total.ToString()}$");
                }

                // Create a new paragraph for calculations and shipping details
                XWPFParagraph calcParagraph = document.CreateParagraph();

                // Set up fonts and styling for calculations
                XWPFRun calcRun = calcParagraph.CreateRun();
                calcRun.FontFamily = "Segoe UI";
                calcRun.FontSize = 14;
                calcRun.IsBold = true;

                // Write subtotal, tax, reduction, and total
                calcRun.SetText($"Total of items: {total.ToString()}$");
                calcRun.AddBreak();
                calcRun.SetText($"Tax: {taxpercent}% - {tax.ToString()}$");
                calcRun.AddBreak();
                calcRun.SetText($"Reduction: {redcpercent}% - {redc.ToString()}$");
                calcRun.AddBreak();
                calcRun.SetText($"Total: {total.ToString()}$");
                calcRun.AddBreak();
                calcRun.AddBreak(); // Add extra line for spacing

                // Write shipping details
                calcRun.SetText("Shipping Details:");
                calcRun.AddBreak();
                calcRun.SetText($"Method: {shippingMethod}");
                calcRun.AddBreak();
                calcRun.SetText($"Shipping To: {ShippingTo}");
                calcRun.AddBreak();
                calcRun.SetText($"Shipping Price: {shippingPrice.ToString()}$");
                calcRun.AddBreak();

                // Save the document to a file
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(documentsFolderPath, "invoice.docx");
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    document.Write(stream);
                }

                await Task.CompletedTask;
                return $"Word document created successfully at {filePath}";
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
