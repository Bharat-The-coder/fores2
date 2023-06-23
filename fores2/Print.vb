Imports System.IO
Imports QRCoder
Imports MySql.Data.MySqlClient
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Public Class print
    Dim conn As New MySqlConnection("server=localhost;database=fores;user=root;password=Root@123;")
    Dim printed As Integer
    Dim shift = ""
    Dim batch_no = ""
    Dim batch_desc = ""
    Dim Exit_loop As Boolean
    Dim printDialog As New PrintDialog()
    Dim savepath As String
    Dim is1_empty As Boolean
    Dim is2_empty As Boolean
    Dim is3_empty As Boolean
    'On form load event
    Private Sub print_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Load_sap_code()
        Panel2.Visible = False
    End Sub
    Private Sub Load_sap_code()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
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
        txtMetal1Lot.Text = ""
        txtMetal1Qty.Text = "0"
        txtMetal2Lot.Text = ""
        txtMetal2Qty.Text = "0"
        txtMetal3Lot.Text = ""
        txtMetal3Qty.Text = "0"
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        Panel2.Visible = True
        btn_save.BackColor = Color.Green
        btn_save.Text = "Start"
        conn.Open()
        printed = 0
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
                    If String.IsNullOrEmpty(txtMetal1Code.Text) Or txtMetal1Code.Text = "-" Then
                        txtMetal1Desc.Text = ""
                        txtMetal1Lot.Text = ""
                        txtMetal1Lot.Enabled = False
                        txtMetal1Qty.Text = "0"
                        txtMetal1Qty.Enabled = False
                    Else

                        txtMetal1Lot.Enabled = True
                        txtMetal1Qty.Enabled = True
                    End If

                    If String.IsNullOrEmpty(txtMetal2Code.Text) Or txtMetal2Code.Text = "-" Then
                        txtMetal2Desc.Text = ""
                        txtMetal2Lot.Text = ""
                        txtMetal2Lot.Enabled = False
                        txtMetal2Qty.Text = "0"
                        txtMetal2Qty.Enabled = False
                    Else
                        txtMetal2Lot.Enabled = True
                        txtMetal2Qty.Enabled = True
                    End If
                    If String.IsNullOrEmpty(txtMetal3Code.Text) Or txtMetal3Code.Text = "-" Then
                        txtMetal3Desc.Text = ""
                        txtMetal3Lot.Text = ""
                        txtMetal3Lot.Enabled = False
                        txtMetal3Qty.Text = "0"
                        txtMetal3Qty.Enabled = False
                    Else
                        txtMetal3Lot.Enabled = True
                        txtMetal3Qty.Enabled = True
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

    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        btn_save.Enabled = False
        'Update_Available()
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
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
                Exit_loop = False
                For printed = printed + 1 To runCavity
                    If (String.IsNullOrEmpty(txtMetal3Code.Text) Or txtMetal3Code.Text = "-") And (String.IsNullOrEmpty(txtMetal2Code.Text) Or txtMetal2Code.Text = "-") And (String.IsNullOrEmpty(txtMetal1Code.Text) Or txtMetal1Code.Text = "-") Then
                        If printed = runCavity Then
                            Func_Barcode()
                            print(Nothing, savepath)
                            printed = 0
                            MessageBox.Show("Success: " & "All stickers printed")
                            SelectProduct.Enabled = True
                            SelectDesc.Enabled = True
                            btn_save.Text = "Print Stickers"
                            btn_save.Enabled = True
                            Exit For
                        ElseIf printed < runCavity Then
                            Func_Barcode()
                            print(Nothing, savepath)
                        End If
                    ElseIf ((Double.Parse(txtMetal1Qty.Text) <= 0 Or txtMetal1Lot.Text = "") And (Not String.IsNullOrEmpty(txtMetal1Code.Text) Or txtMetal1Code.Text = "-")) Or ((Double.Parse(txtMetal2Qty.Text) <= 0 Or txtMetal2Lot.Text = "") And (Not String.IsNullOrEmpty(txtMetal2Code.Text) Or txtMetal2Code.Text = "-")) Or ((Double.Parse(txtMetal3Qty.Text) <= 0 Or txtMetal3Lot.Text = "") And (Not String.IsNullOrEmpty(txtMetal3Code.Text) Or txtMetal3Code.Text = "-")) Then
                        btn_save.Text = "Restart"
                        btn_save.BackColor = Color.Yellow
                        btn_save.ForeColor = Color.Black
                        printed -= 1
                        btn_save.Enabled = True
                        If Not printed = runCavity Then
                            SelectProduct.Enabled = False
                            SelectDesc.Enabled = False
                        End If
                        MessageBox.Show("Error: Please Fill LOT/Batch No. and Quantity")
                        Exit For

                    ElseIf printed = runCavity Then

                        Func_Barcode()
                        Func_update_qty()
                        printed = 0
                        MessageBox.Show("Success: " & "All stickers printed")
                        SelectProduct.Enabled = True
                        SelectDesc.Enabled = True

                        btn_save.Text = "Print Stickers"
                        btn_save.Enabled = True
                        Exit For
                    ElseIf printed < runCavity Then
                        If String.IsNullOrEmpty(txtMetal1Code.Text) Or txtMetal3Code.Text = "-" Then
                            txtMetal1Lot.Enabled = False
                            txtMetal1Qty.Enabled = False
                        End If
                        If String.IsNullOrEmpty(txtMetal2Code.Text) Or txtMetal3Code.Text = "-" Then
                            txtMetal2Lot.Enabled = False
                            txtMetal2Qty.Enabled = False
                        End If
                        If String.IsNullOrEmpty(txtMetal3Code.Text) Or txtMetal3Code.Text = "-" Then
                            txtMetal3Lot.Enabled = False
                            txtMetal3Qty.Enabled = False
                        End If
                        btn_save.Enabled = False
                        Func_Barcode()
                        Func_update_qty()
                    End If
                    If Exit_loop Then
                        Exit For
                    End If
                Next
                If Not String.IsNullOrEmpty(txtMetal1Code.Text) Or txtMetal3Code.Text = "-" Then
                    txtMetal1Lot.Enabled = True
                    txtMetal1Qty.Enabled = True
                End If
                If Not String.IsNullOrEmpty(txtMetal2Code.Text) Or txtMetal3Code.Text = "-" Then
                    txtMetal2Lot.Enabled = True
                    txtMetal2Qty.Enabled = True
                End If
                If Not String.IsNullOrEmpty(txtMetal3Code.Text) Or txtMetal3Code.Text = "-" Then
                    txtMetal3Lot.Enabled = True
                    txtMetal3Qty.Enabled = True
                End If
                reader.Close()
            End If
            btn_save.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
            btn_save.Enabled = True
        End Try
    End Sub
    Private Sub Func_update_qty()
        If conn.State = ConnectionState.Open Then
            conn.Close()
        End If
        conn.Open()
        Dim Metal1_qty As Double = Double.Parse(txtMetal1Qty.Text)
        Dim Metal2_qty As Double = Double.Parse(txtMetal2Qty.Text)
        Dim Metal3_qty As Double = Double.Parse(txtMetal3Qty.Text)

        Dim query As String = $"SELECT * FROM bom_material_master WHERE material_sap_code = '{SelectProduct.SelectedItem.ToString}'"
        Dim command As New MySqlCommand(query, conn)
        Dim reader As MySqlDataReader = command.ExecuteReader()
        If reader.HasRows Then
            reader.Read()
            'Get Metal Quantity as Double  
            Dim qty1 As Double? = reader.GetDouble("metal1_qty")
            Dim qty2 As Double? = reader.GetDouble("metal2_qty")
            Dim qty3 As Double? = reader.GetDouble("metal3_qty")
            reader.Close()
            Try

                'Check if there are metal3 available
                If (Not String.IsNullOrEmpty(txtMetal1Code.Text)) Or txtMetal1Code.Text = "-" Then
                    If qty1 <= Metal1_qty Then
                        'query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty1}, Date = NOW() WHERE metal_code = '{txtMetal1Code.Text}'"
                        'command = New MySqlCommand(query, conn)
                        'command.ExecuteNonQuery()
                        'txtMetal1Qty.Text = (Metal1_qty - qty1).ToString()
                        is1_empty = False
                    Else
                        is1_empty = True
                        Exit_loop = True
                    End If

                End If
                If (Not String.IsNullOrEmpty(txtMetal2Code.Text)) Or txtMetal2Code.Text = "-" Then
                    If qty2 <= Metal2_qty Then
                        'query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty2}, Date = NOW() WHERE metal_code = '{txtMetal2Code.Text}'"
                        'command = New MySqlCommand(query, conn)
                        'command.ExecuteNonQuery()
                        'txtMetal2Qty.Text = (Metal2_qty - qty2).ToString()
                        is2_empty = False
                    Else
                        is2_empty = True
                        'MessageBox.Show("Error: Please Fill LOT/Batch No. and Quantity")
                        Exit_loop = True
                    End If
                End If
                If (Not String.IsNullOrEmpty(txtMetal3Code.Text)) Or txtMetal3Code.Text = "-" Then
                    If qty3 <= Metal3_qty Then
                        'query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty3}, Date = NOW() WHERE metal_code = '{txtMetal3Code.Text}'"
                        'command = New MySqlCommand(query, conn)
                        'command.ExecuteNonQuery()
                        'txtMetal3Qty.Text = (Metal3_qty - qty3).ToString()
                        is3_empty = False
                    Else
                        is3_empty = True

                        'MessageBox.Show("Error: Please Fill LOT/Batch No. and Quantity")
                        Exit_loop = True
                    End If
                End If
                If is1_empty Or is2_empty Or is3_empty Then
                    btn_save.Text = "Restart"
                    btn_save.BackColor = Color.Yellow
                    btn_save.ForeColor = Color.Black
                    btn_save.Enabled = True
                    MessageBox.Show("Error: No Material In Table to update or Less than size")
                Else
                    If is1_empty = False Then
                        query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty1}, Date = NOW() WHERE metal_code = '{txtMetal1Code.Text}'"
                        command = New MySqlCommand(query, conn)
                        command.ExecuteNonQuery()
                        txtMetal1Qty.Text = (Metal1_qty - qty1).ToString()
                    End If
                    If is2_empty = False Then
                        query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty2}, Date = NOW() WHERE metal_code = '{txtMetal2Code.Text}'"
                        command = New MySqlCommand(query, conn)
                        command.ExecuteNonQuery()
                        txtMetal2Qty.Text = (Metal2_qty - qty2).ToString()
                    End If
                    If is3_empty = False Then
                        query = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty3}, Date = NOW() WHERE metal_code = '{txtMetal3Code.Text}'"
                        command = New MySqlCommand(query, conn)
                        command.ExecuteNonQuery()
                        txtMetal3Qty.Text = (Metal3_qty - qty3).ToString()
                    End If
                    print(Nothing, savepath)
                End If
            Catch ex As Exception
                MessageBox.Show("Error: While Updating the database " & ex.Message)
                Exit_loop = True
            Finally
                conn.Close()
            End Try
        Else
            MessageBox.Show("Error: Material Sap Code Not Found ")

        End If
    End Sub

    Private Sub Func_Barcode()
        Dim gen As New QRCodeGenerator
        'Create a table format string based on the input
        If txtMetal1Code.Text Is Nothing Then
            txtMetal1Code.Text = ""
            txtMetal1Lot.Text = ""
        ElseIf txtMetal2Code.Text Is Nothing Then
            txtMetal2Code.Text = ""
            txtMetal2Lot.Text = ""
        ElseIf txtMetal3Code.Text Is Nothing Then
            txtMetal3Code.Text = ""
            txtMetal3Lot.Text = ""
        End If
        'Data in barcode encode
        Dim tableInput As String = shift.ToString + (DateTime.Today).ToString("yyyyMMdd") + "/" + SelectProduct.Text + "/" + SelectDesc.Text + "/" + batch_no.ToString + batch_desc.ToString & vbCrLf &
                                   txtMetal1Code.Text & "/" & txtMetal1Lot.Text + "/" + txtMetal2Code.Text & "/" & txtMetal2Lot.Text + "/" + txtMetal3Code.Text & "/" & txtMetal3Lot.Text & vbCrLf
        Dim data = gen.CreateQrCode(tableInput, QRCodeGenerator.ECCLevel.Q)
        Dim code As New QRCode(data)
        'Path of pdf file
        Dim saveDirectory As String = "\fores india\data" ' Change the save directory as per your requirement
        Dim fileName As String = SelectProduct.SelectedItem + ".pdf"
        savepath = Path.Combine(saveDirectory, fileName)
        If Not Directory.Exists(saveDirectory) Then
            Directory.CreateDirectory(saveDirectory)
        End If
        Using fs As New FileStream(savepath, FileMode.Create)
            Try
                Using doc As New Document()
                    doc.SetPageSize(New iTextSharp.text.Rectangle(220, 150))
                    Using writer As PdfWriter = PdfWriter.GetInstance(doc, fs)
                        doc.Open()
                        Dim table As New PdfPTable(2)
                        table.SetWidths({1, 2}) ' Adjust the values as needed
                        table.WidthPercentage = 80
                        'table.HorizontalAlignment = Element.ALIGN_LEFT ' Align the table to the left
                        ' Set cell border style
                        Dim cellBorder As Integer = Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER
                        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(code.GetGraphic(6), System.Drawing.Imaging.ImageFormat.Png)
                        image.ScalePercent(12)
                        'Create a PdfPCell for the image
                        Dim cell1 As New PdfPCell()
                        cell1.Border = cellBorder
                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER   ' Align the content horizontally to the center
                        cell1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE     ' Align the content vertically to the middle
                        cell1.AddElement(image)
                        cell1.BackgroundColor = New BaseColor(255, 255, 0)  ' Red fill color (adjust the RGB values as needed)
                        ' Add the image cell to the table
                        table.AddCell(cell1)
                        table.DefaultCell.Border = cellBorder
                        Dim text1 As New PdfPCell(New Phrase(New Phrase(shift.ToString + DateAndTime.Today.ToString("yyyyMMdd"))))
                        text1.Border = cellBorder
                        text1.Phrase.Font.Size = 10 ' Set the font size
                        text1.MinimumHeight = 8 ' Set the minimum cell height
                        text1.HorizontalAlignment = PdfPCell.ALIGN_CENTER   ' Align the content horizontally to the center
                        text1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE     ' Align the content vertically to the middle
                        text1.BackgroundColor = New BaseColor(255, 255, 0)  ' Red fill color (adjust the RGB values as needed)
                        table.AddCell(text1)
                        doc.Add(table)
                        doc.Close()
                    End Using
                End Using
            Catch ex As Exception
                ' Handle other exceptions or display a generic error message
                MessageBox.Show("Error: An unexpected error occurred while creating the PDF." & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Using

    End Sub
    Public Sub print(ByVal args() As String, savepath As String)
        Try
            'Create a PdfDocument object
            Dim doc As Spire.Pdf.PdfDocument = New Spire.Pdf.PdfDocument()
            'Load a PDF file
            doc.LoadFromFile(savepath)
            If printed = 1 Then
                If printDialog.ShowDialog() = DialogResult.OK Then
                    doc.PrintSettings.PrinterName = printDialog.PrinterSettings.PrinterName
                End If
            End If
            doc.Print()
            doc.Close()
        Catch ex As Exception
            MessageBox.Show("Error: Printing Stickers " & ex.Message)
            Exit_loop = True
        End Try
    End Sub

    Private Sub SelectProduct_DoubleClick(sender As Object, e As EventArgs) Handles SelectProduct.DoubleClick
        If Not SelectDesc.Enabled Then
            SelectProduct.Enabled = True
        End If
    End Sub
    Private Sub txtMetal1Qty_TextChanged(sender As Object, e As EventArgs) Handles txtMetal1Qty.TextChanged
        Dim enteredText As String = txtMetal1Qty.Text
        ' Check if the entered text is numeric
        Dim isNumeric As Boolean = Double.TryParse(enteredText, 0)
        If Not isNumeric Then
            ' Clear the textbox or display an error message
            txtMetal1Qty.Text = ""
            ' You can also display an error message
            'MessageBox.Show("Only numeric input is allowed.")
        End If
    End Sub
    Private Sub txtMetal2Qty_TextChanged(sender As Object, e As EventArgs) Handles txtMetal2Qty.TextChanged
        Dim enteredText As String = txtMetal2Qty.Text
        ' Check if the entered text is numeric
        Dim isNumeric As Boolean = Double.TryParse(enteredText, 0)
        If Not isNumeric Then
            ' Clear the textbox or display an error message
            txtMetal2Qty.Text = ""
            ' You can also display an error message
            'MessageBox.Show("Only numeric input is allowed.")
        End If
    End Sub
    Private Sub txtMetal3Qty_TextChanged(sender As Object, e As EventArgs) Handles txtMetal3Qty.TextChanged
        Dim enteredText As String = txtMetal3Qty.Text
        ' Check if the entered text is numeric
        Dim isNumeric As Boolean = Double.TryParse(enteredText, 0)
        If Not isNumeric Then
            ' Clear the textbox or display an error message
            txtMetal3Qty.Text = ""
            ' You can also display an error message
            'MessageBox.Show("Only numeric input is allowed.")
        End If
    End Sub

    Private Sub SelectProduct_MouseEnter(sender As Object, e As EventArgs) Handles SelectProduct.MouseEnter
        ToolTip1.SetToolTip(SelectProduct, "Select Sap Code of Material")
        ' Display the tooltip                                                          
        ToolTip1.Active = True
    End Sub
    Private Sub SelectProduct_MouseLeave(sender As Object, e As EventArgs) Handles SelectProduct.MouseLeave
        'ToolTip1.SetToolTip(SelectProduct, "Select Sap Code of Material") 
        ' Display the tooltip
        ToolTip1.Active = False
    End Sub
    Private Sub SelectDesc_MouseEnter(sender As Object, e As EventArgs) Handles SelectDesc.MouseEnter
        ToolTip2.SetToolTip(SelectDesc, "Select Description Code of Material")

        ' Display the tooltip
        ToolTip2.Active = True
    End Sub
    Private Sub SelectDesc_MouseLeave(sender As Object, e As EventArgs) Handles SelectDesc.MouseLeave
        'ToolTip1.SetToolTip(SelectProduct, "Select Sap Code of Material")
        ' Display the tooltip
        ToolTip2.Active = False
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.DoubleClick
        SelectProduct.Enabled = True
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.DoubleClick
        SelectDesc.Enabled = True
    End Sub

    Private Sub txtMetalLot_TextChanged(sender As Object, e As EventArgs) Handles txtMetal1Lot.TextChanged
        Dim input As String = txtMetal1Lot.Text
        Dim isValid As Boolean = True
        For Each c As Char In input
            If Not (Char.IsLetterOrDigit(c) Or c = "_"c Or c = "-"c) Then
                isValid = False
                Exit For
            End If
        Next
        If Not isValid Then
            MessageBox.Show("Invalid input! Only characters, numbers, '_', and '-' are allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMetal1Lot.Text = String.Empty ' Clear the text box
        End If
    End Sub
    Private Sub txtMetal2Lot_TextChanged(sender As Object, e As EventArgs) Handles txtMetal2Lot.TextChanged
        Dim input As String = txtMetal2Lot.Text
        Dim isValid As Boolean = True

        For Each c As Char In input
            If Not (Char.IsLetterOrDigit(c) Or c = "_"c Or c = "-"c) Then
                isValid = False
                Exit For
            End If
        Next

        If Not isValid Then
            MessageBox.Show("Invalid input! Only characters, numbers, ' ', and '-' are allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMetal2Lot.Text = String.Empty ' Clear the text box
        End If
    End Sub
    Private Sub txtMetal3Lot_TextChanged(sender As Object, e As EventArgs) Handles txtMetal3Lot.TextChanged
        Dim input As String = txtMetal3Lot.Text
        Dim isValid As Boolean = True
        For Each c As Char In input
            If Not (Char.IsLetterOrDigit(c) Or c = "_"c Or c = "-"c) Then
                isValid = False
                Exit For
            End If
        Next
        If Not isValid Then
            MessageBox.Show("Invalid input! Only characters, numbers, ' ', and '-' are allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMetal3Lot.Text = String.Empty ' Clear the text box
        End If
    End Sub

    Private Sub txtMetalQty_Enter(sender As Object, e As KeyEventArgs) Handles txtMetal1Qty.KeyDown, txtMetal2Qty.KeyDown, txtMetal3Qty.KeyDown
        If e.KeyCode = Keys.Enter Then
            btn_save_Click(sender, e)
        End If
    End Sub

    Private Sub SelectProduct_Click(sender As Object, e As EventArgs) Handles SelectProduct.Click
        If SelectProduct.SelectedItem Is Nothing Then
            SelectProduct.Text = ""
        End If

    End Sub
    Private Sub SelectDesc_Click(sender As Object, e As EventArgs) Handles SelectDesc.Click
        If SelectDesc.SelectedItem Is Nothing Then
            SelectDesc.Text = ""
        End If
    End Sub

    Private Sub SelectDesc_TextChanged(sender As Object, e As EventArgs) Handles SelectDesc.TextChanged
        For Each item In SelectDesc.Items

            If item.Contains(SelectDesc.Text) And item.Length >= 4 Then
                SelectDesc.Text = item
            End If
        Next
    End Sub
End Class