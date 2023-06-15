Imports System.Drawing.Printing
Imports System.IO

Public Class Printer_Class
    Private printDocument As PrintDocument

    Public Sub New()
        printDocument = New PrintDocument()
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
    End Sub

    Public Sub PrintPDF(pdfFilePath As String)
        ' Set the PDF file path to the print document
        printDocument.DocumentName = pdfFilePath
        ' Set the printer name (e.g., "Microsoft Print to PDF" or the name of your physical printer)
        printDocument.PrinterSettings.PrinterName = "Your Printer Name"

        ' Optionally, set other printer settings such as page orientation, paper size, etc.
        ' printDocument.PrinterSettings.Orientation = PrinterOrientation.Landscape
        ' printDocument.PrinterSettings.DefaultPageSettings.PaperSize = New PaperSize("Custom", 500, 700)

        ' Start the printing process
        printDocument.Print()
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Dim pdfFilePath As String = printDocument.DocumentName

        ' Read the content of the file
        Dim fileContent As String = File.ReadAllText(pdfFilePath)

        ' Print the content to the page
        Dim font As New Font("Arial", 12)
        Dim brush As New SolidBrush(Color.Black)
        Dim xPos As Single = e.MarginBounds.Left
        Dim yPos As Single = e.MarginBounds.Top
        Dim lineOffset As Single = font.GetHeight(e.Graphics) + 5
        e.Graphics.DrawString(fileContent, font, brush, xPos, yPos)
        yPos += lineOffset
    End Sub
End Class
