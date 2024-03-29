using PDFCHARPConversion.Models;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace PDFCHARPConversion
{
    public class PdfConversionHelper
    {
        public async Task<string> ConvertInputInvoiceToPdfWithoutShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total)
        {
            try
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Add a page to the document
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Set up fonts
                XFont fontRegular = new XFont("Segoe", 14, XFontStyle.Regular);
                XFont fontSpecial = new XFont("Segoe", 15, XFontStyle.Bold);
                XFont fontTitle = new XFont("Segoe", 72, XFontStyle.Bold);
                XFont fontCalculation = new XFont("Segoe", 16, XFontStyle.Bold);

                // Set up initial position for invoice number and date
                XPoint currentPosition = new XPoint(30, 30);

                // Generate invoice number as hexadecimal GUID
                string invoiceNumber = Guid.NewGuid().ToString("N").ToUpper(); // Convert to upper case

                // Draw gray background for invoice number and date
                XRect backgroundRect = new XRect(0, 0, page.Width, 125);
                XRect backgroundCompany = new XRect(0, 0, 300, 125);
                
                XRect bigLine = new XRect(0, 0, page.Width, 40);// Start from top of the page (Y = 0)
                gfx.DrawRectangle(XBrushes.MediumPurple, backgroundRect);
                gfx.DrawRectangle(XBrushes.Purple, backgroundCompany);

                // Draw invoice, number, and date with special font and styling on the gray background
                gfx.DrawString("Invoice", fontTitle, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 70);
                

                currentPosition.X += 280;
                currentPosition.Y = 30;
                gfx.DrawString($"N°:", fontSpecial, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 30);
                gfx.DrawString($"{invoiceNumber}", fontSpecial, XBrushes.White, currentPosition.X + 30, currentPosition.Y + 30);
                gfx.DrawString($"Date: {date.ToShortDateString()}", fontSpecial, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 50);
                gfx.DrawString($"Customer: {customer}", fontSpecial, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 70);

                // Update current position for the next element
                currentPosition.Y += 120;
                currentPosition.X = 30;
                gfx.DrawString("Items List", fontTitle, XBrushes.Black, currentPosition.X+10, currentPosition.Y + 10);
                currentPosition.Y += 60;
                currentPosition.X += 10;
                // Draw table headers
                DrawTableHeader(gfx, currentPosition);
                XRect smallLine = new XRect(10, currentPosition.Y+= 5, page.Width-20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine);
                // Update current position for the next element
                currentPosition.Y += 30;
                currentPosition.X += 0;
                // Write invoice items
                foreach (var item in items)
                {
                    DrawTableRow(gfx, currentPosition, item.ProductName, item.Quantity.ToString(), item.Price.ToString()+"$", item.Total.ToString()+"$");
                    currentPosition.Y += 20;
                }
                currentPosition.Y += 5;
                XRect smallLine2 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine2);

                // Draw subtotal
                currentPosition.Y += 20;
                currentPosition.X += 300;
                gfx.DrawString($"Total of items", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", total)}$", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.Y += 5;
                XRect smallLine3 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine3);
                // Draw tax
                currentPosition.Y += 20;
                currentPosition.X -= 100;
                gfx.DrawString($"Tax: {taxpercent}%", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", tax)}$", fontCalculation, XBrushes.Black, currentPosition);
                // Draw reduction
                currentPosition.Y += 20;
                currentPosition.X -= 100;
                gfx.DrawString($"Tax: {redcpercent}%", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", redc)}$", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.Y += 5;
                XRect smallLine4 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine4);
                // Draw total
                currentPosition.X -= 100;
                currentPosition.Y += 20;
                gfx.DrawString($"Total", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}",total)}$", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.Y += 5; XRect smallLine5 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine5);
                currentPosition.Y += 5; XRect smallLine6 = new XRect(10, page.Height-50, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine6);
                // Get the documents folder path
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Construct the file path
                string filePath = Path.Combine(documentsFolderPath, "invoice.pdf");

                // Save the document to the file
                document.Save(filePath);

                await Task.CompletedTask;
                return $"PDF created successfully at {filePath}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> ConvertInputInvoiceToPdfWithShipping(string customer, DateTime date, ObservableCollection<InvoiceItem> items, decimal taxpercent, decimal tax, decimal redcpercent, decimal redc, decimal total,string shippingMethod , string ShippingTo , decimal shippingPrice)
        {
            try
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Add a page to the document
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Set up fonts
                XFont fontRegular = new XFont("Segoe", 14, XFontStyle.Regular);
                XFont fontSpecial = new XFont("Segoe", 15, XFontStyle.Bold);
                XFont fontTitle = new XFont("Segoe", 72, XFontStyle.Bold);
                XFont fontCalculation = new XFont("Segoe", 16, XFontStyle.Bold);

                // Set up initial position for invoice number and date
                XPoint currentPosition = new XPoint(30, 30);

                // Generate invoice number as hexadecimal GUID
                string invoiceNumber = Guid.NewGuid().ToString("N").ToUpper(); // Convert to upper case

                // Draw gray background for invoice number and date
                XRect backgroundRect = new XRect(0, 0, page.Width, 125);
                XRect backgroundCompany = new XRect(0, 0, 300, 125);

                XRect bigLine = new XRect(0, 0, page.Width, 40); // Start from top of the page (Y = 0)
                gfx.DrawRectangle(XBrushes.MediumPurple, backgroundRect);
                gfx.DrawRectangle(XBrushes.Purple, backgroundCompany);

                // Draw invoice, number, and date with special font and styling on the gray background
                gfx.DrawString("Invoice", fontTitle, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 70);

                currentPosition.X += 280;
                currentPosition.Y = 30;
                gfx.DrawString($"N°:", fontSpecial, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 30);
                gfx.DrawString($"{invoiceNumber}", fontSpecial, XBrushes.White, currentPosition.X + 30, currentPosition.Y + 30);
                gfx.DrawString($"Date: {date.ToShortDateString()}", fontSpecial, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 50);
                gfx.DrawString($"Customer: {customer}", fontSpecial, XBrushes.White, currentPosition.X + 10, currentPosition.Y + 70);

                // Update current position for the next element
                currentPosition.Y += 120;
                currentPosition.X = 30;
                gfx.DrawString("Items List", fontTitle, XBrushes.Black, currentPosition.X + 10, currentPosition.Y + 10);
                currentPosition.Y += 60;
                currentPosition.X += 10;
                // Draw table headers
                DrawTableHeader(gfx, currentPosition);
                XRect smallLine = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine);
                // Update current position for the next element
                currentPosition.Y += 30;
                currentPosition.X += 0;
                // Write invoice items
                foreach (var item in items)
                {
                    DrawTableRow(gfx, currentPosition, item.ProductName, item.Quantity.ToString(), item.Price.ToString() + "$", item.Total.ToString() + "$");
                    currentPosition.Y += 20;
                }
                currentPosition.Y += 5;
                XRect smallLine2 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine2);

                // Draw subtotal
                currentPosition.Y += 20;
                currentPosition.X += 300;
                gfx.DrawString($"Total of items", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", total)}$", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.Y += 5;
                XRect smallLine3 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine3);
                // Draw tax
                currentPosition.Y += 20;
                currentPosition.X -= 100;
                gfx.DrawString($"Tax: {taxpercent}%", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", tax)}$", fontCalculation, XBrushes.Black, currentPosition);
                // Draw reduction
                currentPosition.Y += 20;
                currentPosition.X -= 100;
                gfx.DrawString($"Tax: {redcpercent}%", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", redc)}$", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.Y += 5;
                XRect smallLine4 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine4);
                currentPosition.Y += 20;
                currentPosition.X = 30;

                // Draw table headers for shipping details
                gfx.DrawString("Shipping Method", fontSpecial, XBrushes.Black, currentPosition.X, currentPosition.Y);
                gfx.DrawString("Shipping To", fontSpecial, XBrushes.Black, currentPosition.X + 210, currentPosition.Y);
                gfx.DrawString("Shipping Price", fontSpecial, XBrushes.Black, currentPosition.X + 410, currentPosition.Y);
                currentPosition.Y += 20;
                
                // Draw shipping details
                gfx.DrawString(shippingMethod, fontSpecial, XBrushes.Black, currentPosition.X, currentPosition.Y);
                gfx.DrawString(ShippingTo, fontSpecial, XBrushes.Black, currentPosition.X + 210, currentPosition.Y);
                gfx.DrawString($"{shippingPrice.ToString()}$", fontSpecial, XBrushes.Black, currentPosition.X + 410, currentPosition.Y);
                XRect smallLine7 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine7);
                currentPosition.X += 310;
                currentPosition.Y += 20;
                gfx.DrawString($"Total", fontCalculation, XBrushes.Black, currentPosition);
                currentPosition.X += 100;
                gfx.DrawString($"{String.Format("{0:0.00}", total)}$", fontCalculation, XBrushes.Black, currentPosition);
                
                currentPosition.Y += 5;
                XRect smallLine5 = new XRect(10, currentPosition.Y += 5, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine5);
                currentPosition.Y += 5;
                
                XRect smallLine6 = new XRect(10, page.Height - 50, page.Width - 20, 1);
                gfx.DrawRectangle(XBrushes.Purple, smallLine6);

                // Draw shipping details table
                

                // Get the documents folder path
                string documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Construct the file path
                string filePath = Path.Combine(documentsFolderPath, "jhkdjshfk.pdf");

                // Save the document to the file
                document.Save(filePath);

                await Task.CompletedTask;
                return $"PDF created successfully at {filePath}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void DrawTableHeader(XGraphics gfx, XPoint currentPosition)
        {
            // Draw table header
            gfx.DrawString("Product Name", new XFont("Futura", 16, XFontStyle.Bold), XBrushes.Black, currentPosition.X, currentPosition.Y);
            gfx.DrawString("Quantity", new XFont("Futura", 16, XFontStyle.Bold), XBrushes.Black, currentPosition.X + 200, currentPosition.Y);
            gfx.DrawString("Price", new XFont("Futura", 16, XFontStyle.Bold), XBrushes.Black, currentPosition.X + 300, currentPosition.Y);
            gfx.DrawString("Total", new XFont("Futura", 16, XFontStyle.Bold), XBrushes.Black, currentPosition.X + 400, currentPosition.Y);
        }

        private void DrawTableRow(XGraphics gfx, XPoint currentPosition, string productName, string quantity, string price, string total)
        {
            // Draw table row
            gfx.DrawString(productName, new XFont("Futura", 14), XBrushes.Black, currentPosition.X, currentPosition.Y);
            gfx.DrawString(quantity, new XFont("Futura", 14), XBrushes.Black, currentPosition.X + 200, currentPosition.Y);
            gfx.DrawString(price, new XFont("Futura", 14), XBrushes.Black, currentPosition.X + 300, currentPosition.Y);
            gfx.DrawString(total, new XFont("Futura", 14), XBrushes.Black, currentPosition.X + 400, currentPosition.Y);
        }
       
    }
}