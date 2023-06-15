Imports System.IO
Imports QRCoder
Imports MySql.Data.MySqlClient
Imports System.Timers
Imports iTextSharp.text.pdf
Imports iTextSharp.text


Public Class Form1
    Private timer As Timer
    Dim conn As New MySqlConnection("server=localhost;database=fores;user=root;password=Root@123;")
    Private total_time, intervalCount As Integer
    Dim printed As Integer = 0
    Dim shift = ""
    Dim batch_no = ""
    Private buttonClicked As Boolean = True
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateComboBoxes()
        Panel2.Visible = False

    End Sub


    Private Sub PopulateComboBoxes()
        conn.Open()
        Dim query As String = "SELECT material_sap_code, material_description FROM bom_material_master"
        Dim command As New MySqlCommand(query, conn)
        Try
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.HasRows Then
                While reader.Read()
                    SelectProduct.Items.Add(reader("material_sap_code").ToString())
                    SelectDesc.Items.Add(reader("material_description").ToString())
                End While
                SelectProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                SelectProduct.AutoCompleteSource = AutoCompleteSource.ListItems
                SelectDesc.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                SelectDesc.AutoCompleteSource = AutoCompleteSource.ListItems
            End If

            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally


            conn.Close()
        End Try
    End Sub

    Private Sub SelectProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectProduct.SelectedIndexChanged
        Show_value(SelectProduct.SelectedItem.ToString())

    End Sub

    Private Sub SelectDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectDesc.SelectedIndexChanged
        Show_value(SelectDesc.SelectedItem.ToString())

    End Sub

    Private Sub Show_value(selectedItem As String)
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        Dim columnName As String = ""
        If SelectProduct.SelectedItem IsNot Nothing AndAlso selectedItem = SelectProduct.SelectedItem.ToString() Then
            columnName = "material_sap_code"
        ElseIf SelectDesc.SelectedItem IsNot Nothing AndAlso selectedItem = SelectDesc.SelectedItem.ToString() Then
            columnName = "material_description"
        End If

        If columnName <> "" Then
            Dim query As String = $"SELECT * FROM bom_material_master WHERE {columnName} = @selectedItem"
            Dim command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@selectedItem", selectedItem)
            Dim reader As MySqlDataReader = command.ExecuteReader()
            Try
                If reader.HasRows Then
                    reader.Read()
                    txtMetal1Code.Text = reader("metal1_sap_code").ToString()
                    txtMetal1Desc.Text = reader("metal1_description").ToString()
                    txtMetal2Code.Text = reader("metal2_sap_code").ToString()
                    txtMetal2Desc.Text = reader("metal2_description").ToString()
                    txtMetal3Code.Text = reader("metal3_sap_code").ToString()
                    txtMetal3Desc.Text = reader("metal3_description").ToString()
                    txtMetal1Lot.Text = ""
                    txtMetal2Lot.Text = ""
                    txtMetal3Lot.Text = ""
                    txtMetal1Qty.Text = ""
                    txtMetal2Qty.Text = ""
                    txtMetal3Qty.Text = ""
                    'buttonClicked = True
                    If String.IsNullOrEmpty(txtMetal1Code.Text) Then
                        txtMetal1Desc.Text = ""
                        txtMetal1Lot.Text = ""
                        txtMetal1Qty.Text = "0"
                    End If

                    If String.IsNullOrEmpty(txtMetal2Code.Text) Then
                        txtMetal2Desc.Text = ""
                        txtMetal2Lot.Text = ""
                        txtMetal2Qty.Text = "0"
                    End If

                    If String.IsNullOrEmpty(txtMetal3Code.Text) Then
                        txtMetal3Desc.Text = ""
                        txtMetal3Lot.Text = ""
                        txtMetal3Qty.Text = "0"
                    End If


                    If columnName = "material_sap_code" Then
                        SelectDesc.SelectedItem = reader("material_description").ToString()
                        SelectDesc.Enabled = False
                        SelectProduct.Enabled = True
                    Else
                        SelectProduct.SelectedItem = reader("material_sap_code").ToString()
                        SelectDesc.Enabled = True
                        SelectProduct.Enabled = False
                    End If

                    Panel2.Visible = True
                    txtMetal1Code.Enabled = False
                    txtMetal2Code.Enabled = False
                    txtMetal3Code.Enabled = False
                    txtMetal1Desc.Enabled = False
                    txtMetal2Desc.Enabled = False
                    txtMetal3Desc.Enabled = False
                Else
                    MessageBox.Show("Error: No Data Found")
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)

            Finally
                reader.Close()
            End Try
        End If
        conn.Close()
    End Sub

    'Private Sub Update_Available()

    '    Dim qty1 As Double = txtMetal1Qty.Text
    '    Dim qty2 As Double = txtMetal2Qty.Text
    '    Dim qty3 As Double = txtMetal3Qty.Text
    '    conn.Open()
    '    If txtMetal1Code.Text IsNot Nothing Then
    '        Dim query As String = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty1}, Date = NOW() WHERE metal_code = '{txtMetal1Code.Text.ToString}'"
    '        Dim command As New MySqlCommand(query, conn)
    '        command.ExecuteNonQuery()
    '    End If
    '    If txtMetal2Code.Text IsNot Nothing Then
    '        Dim query As String = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty2}, Date = NOW() WHERE metal_code = '{txtMetal2Code.Text.ToString}'"
    '        Dim command As New MySqlCommand(query, conn)
    '        command.ExecuteNonQuery()
    '    End If
    '    If txtMetal3Code.Text IsNot Nothing Then
    '        Dim query As String = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty3}, Date = NOW() WHERE metal_code = '{txtMetal3Code.Text.ToString}'"
    '        Dim command As New MySqlCommand(query, conn)
    '        command.ExecuteNonQuery()
    '    End If
    '    conn.Close()
    'End Sub


    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        btn_save.Enabled = False
        'Update_Available()
        Try
            conn.Open()
            Dim query As String = $"SELECT run_cavity,shift,batch_no FROM {SelectProduct.SelectedItem.ToString()} "
            Dim command As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.HasRows Then
                reader.Read()
                'Dim cycleTime As Integer = reader.GetInt32("Cycle_time")
                Dim runCavity As Integer = reader.GetInt32("run_cavity")
                shift = reader.GetChar("shift")
                batch_no = reader.GetString("batch_no")
                reader.Close()
                conn.Close()

                For printed = printed + 1 To runCavity
                    If (Integer.Parse(txtMetal1Qty.Text) <= 0 And txtMetal1Code IsNot Nothing And Integer.Parse(txtMetal1Qty.Text) <= 0 And txtMetal1Code IsNot Nothing And Integer.Parse(txtMetal1Qty.Text) <= 0) And txtMetal1Code IsNot Nothing Then
                        MessageBox.Show("Error: Please Fill LOT/Batch No. and Quantity")
                        btn_save.Text = "Restart"
                        printed -= 1

                        Exit For
                    End If
                    If printed = runCavity Then
                        'Func_update_qty()
                        Func_Barcode()
                        printed = 0
                        MessageBox.Show("Success: " & "All stickers printed")
                        Exit For
                    Else
                        txtMetal1Qty.Enabled = False
                        txtMetal2Qty.Enabled = False
                        txtMetal3Qty.Enabled = False
                        txtMetal1Lot.Enabled = False
                        txtMetal2Lot.Enabled = False
                        txtMetal3Lot.Enabled = False

                        Func_Barcode()

                    End If
                Next
                reader.Close()
            End If
            Me.Invoke(Sub() btn_save.Enabled = True)
            txtMetal1Qty.Enabled = True
            txtMetal2Qty.Enabled = True
            txtMetal3Qty.Enabled = True
            txtMetal1Lot.Enabled = True
            txtMetal2Lot.Enabled = True
            txtMetal3Lot.Enabled = True

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            btn_save.Enabled = True
        End Try
    End Sub

    Private Sub Func_update_qty()
        conn.Open()
        Dim Metal1_qty As Integer = Integer.Parse(txtMetal1Qty.Text)
        Dim Metal2_qty As Integer = Integer.Parse(txtMetal2Qty.Text)
        Dim Metal3_qty As Integer = Integer.Parse(txtMetal3Qty.Text)

        Try
            Dim query As String = $"SELECT * FROM bom_material_master WHERE material_sap_code = '{SelectProduct.SelectedItem}' or material_description='{SelectDesc.SelectedItem}'"
            Dim command As New MySqlCommand(query, conn)
            Dim reader As MySqlDataReader = command.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                Dim qty1 As Double = reader.GetDouble("metal1_qty")
                Dim qty2 As Double = reader.GetDouble("metal2_qty")
                Dim qty3 As Double = reader.GetDouble("metal3_qty")
                If Not String.IsNullOrEmpty(txtMetal1Code.Text) And reader.GetInt32("metal1_qty") <= (Metal1_qty) Or Not String.IsNullOrEmpty(txtMetal2Code.Text) And reader.GetInt32("metal2_qty") <= (Metal2_qty) Or Not String.IsNullOrEmpty(txtMetal3Code.Text) And reader.GetInt32("metal3_qty") <= (Metal3_qty) Then
                    txtMetal1Qty.Text = (Metal1_qty - reader.GetInt32("metal1_qty")).ToString()
                    txtMetal2Qty.Text = (Metal2_qty - reader.GetInt32("metal2_qty")).ToString()
                    txtMetal3Qty.Text = (Metal3_qty - reader.GetInt32("metal3_qty")).ToString()
                    reader.Close()
                    query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty1}, Date = Now() WHERE metal_code = '{txtMetal1Code.Text.ToString}'"
                    command = New MySqlCommand(query, conn)
                    command.ExecuteNonQuery()
                    query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty2}, Date = Now() WHERE metal_code = '{txtMetal2Code.Text.ToString}'"
                    command = New MySqlCommand(query, conn)
                    command.ExecuteNonQuery()
                    query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty3}, Date = Now() WHERE metal_code = '{txtMetal3Code.Text.ToString}'"
                    command = New MySqlCommand(query, conn)
                    command.ExecuteNonQuery()
                End If

            End If

        Catch ex As Exception
            MessageBox.Show("Error: While Updating to database " & ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub Func_Barcode()
        Dim gen As New QRCodeGenerator
        'Create a table format string based on the input
        Dim tableInput As String = SelectProduct.Text & "|" & shift + (DateTime.Today).ToString("yyddMM") + batch_no.ToString & vbCrLf &
                                   txtMetal1Code.Text & "|" & txtMetal1Lot.Text & vbCrLf &
                                   txtMetal2Code.Text & "|" & txtMetal2Lot.Text & vbCrLf &
                                   txtMetal3Code.Text & "|" & txtMetal3Lot.Text & vbCrLf
        Dim data = gen.CreateQrCode(tableInput, QRCodeGenerator.ECCLevel.Q)
        Dim code As New QRCode(data)
        Dim saveDirectory As String = "C:\Users\Bharat\source\repos\fores2\data" ' Change the save directory as per your requirement
        Dim fileName As String = SelectProduct.SelectedItem + ".pdf"
        Dim savePath As String = Path.Combine(saveDirectory, fileName)
        If Not Directory.Exists(saveDirectory) Then
            Directory.CreateDirectory(saveDirectory)
        End If
        Using fs As New FileStream(savePath, FileMode.Create)
            Try
                Using doc As New Document()
                    doc.SetPageSize(New iTextSharp.text.Rectangle(450, 280))

                    Using writer As PdfWriter = PdfWriter.GetInstance(doc, fs)
                        doc.Open()
                        Dim table As New PdfPTable(2)
                        table.WidthPercentage = 65
                        ' Set cell border style
                        Dim cellBorder As Integer = Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER

                        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(code.GetGraphic(6), System.Drawing.Imaging.ImageFormat.Png)
                        image.ScalePercent(35)
                        ' Create a PdfPCell for the image
                        Dim cell1 As New PdfPCell()
                        cell1.Border = cellBorder
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER ' Align the content horizontally to the center
                        cell1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE ' Align the content vertically to the middle
                        cell1.AddElement(image)

                        ' Add the image cell to the table
                        table.AddCell(cell1)
                        Dim nestedTable As New PdfPTable(1)
                        nestedTable.DefaultCell.Border = cellBorder

                        Dim text1 As New PdfPCell(New Phrase(SelectProduct.SelectedItem.ToString))
                        text1.Border = cellBorder
                        text1.Phrase.Font.Size = 12 ' Set the font size

                        text1.MinimumHeight = 30 ' Set the minimum cell height
                        nestedTable.AddCell(text1)

                        Dim text2 As New PdfPCell(New Phrase(SelectDesc.SelectedItem.ToString))
                        text2.Border = cellBorder
                        text2.MinimumHeight = 30 ' Set the minimum cell height
                        text2.Phrase.Font.Size = 12 ' Set the font size

                        nestedTable.AddCell(text2)

                        Dim text3 As New PdfPCell(New Phrase(shift.ToString + DateAndTime.Today.ToString("yyyyddMM") + batch_no.ToString))
                        text3.Border = cellBorder
                        text1.Phrase.Font.Size = 12 ' Set the font size
                        text3.MinimumHeight = 30 ' Set the minimum cell height

                        nestedTable.AddCell(text3)

                        Dim cell2 As New PdfPCell(nestedTable)
                        cell2.Border = cellBorder
                        table.AddCell(cell2)

                        Dim text4 As New PdfPCell(New Phrase("Batch No.:" + batch_no.ToString))
                        text4.Border = cellBorder
                        text4.Phrase.Font.Size = 12 ' Set the font size
                        text4.MinimumHeight = 30 ' Set the minimum cell height

                        Dim text5 As New PdfPCell(New Phrase(DateTime.Now().ToString("yyMMdd|hhmmss")))
                        text5.Border = cellBorder
                        text5.Phrase.Font.Size = 12 ' Set the font size
                        text5.MinimumHeight = 30 ' Set the minimum cell height
                        table.AddCell(text4)
                        table.AddCell(text5)
                        doc.Add(table)
                        doc.Close()
                        print(Nothing, savePath)
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error: While creating Pdf " & ex.Message)
            End Try
        End Using
    End Sub
    Private Sub SelectProduct_Click(sender As Object, e As EventArgs) Handles SelectProduct.Click
        If SelectProduct.SelectedIndex Then
            SelectProduct.Text = ""
        End If
    End Sub
    Private Sub SelectDesc_Click(sender As Object, e As EventArgs) Handles SelectDesc.Click
        If SelectDesc.SelectedItem Then
            SelectDesc.Text = ""
        End If
    End Sub
    ' Rest of the code...
    Public Sub print(ByVal args() As String, savepath As String)
        'Create a PdfDocument object
        Dim doc As Spire.Pdf.PdfDocument = New Spire.Pdf.PdfDocument()
        'Load a PDF file

        doc.LoadFromFile(savepath)
        'Print with default printer

        doc.Print()
        Func_update_qty()
    End Sub
End Class

