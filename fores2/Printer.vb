
Imports Spire.Pdf

Class Printer
    Public Sub print(ByVal args() As String, savepath As String)
        'Create a PdfDocument object
        Dim doc As Spire.Pdf.PdfDocument = New PdfDocument()
        'Load a PDF file

        doc.LoadFromFile(savepath)
        'Print with default printer
        doc.Print()
    End Sub
End Class
