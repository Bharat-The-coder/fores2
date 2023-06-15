Imports System.IO
Imports QRCoder

Public Class Form2
    Private Sub txtInput_TextChanged(sender As Object, e As EventArgs)
        Dim gen As New QRCodeGenerator
        Dim data = gen.CreateQrCode(txtInput.Text, QRCodeGenerator.ECCLevel.Q)
        Dim code As New QRCode(data)
        pic.Image = code.GetGraphic(6)
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs)
        If pic.Image IsNot Nothing Then
            If pic.Image IsNot Nothing Then
                Dim saveDirectory As String = "C:\compiler" ' Change the save directory as per your requirement
                Dim fileName As String = "Bharat.png"
                Dim savePath As String = Path.Combine(saveDirectory, fileName)
                If Not Directory.Exists(saveDirectory) Then
                    Directory.CreateDirectory(saveDirectory)
                End If
                Dim bitmap As New Bitmap(pic.Image)
                bitmap.Save(savePath, System.Drawing.Imaging.ImageFormat.Png)
            End If
        End If
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class