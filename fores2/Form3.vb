'Imports System.IO
'Imports QRCoder
'Imports MySql.Data.MySqlClient
'Imports System.Timers
'Imports iTextSharp.text.pdf
'Imports iTextSharp.text
'Public Class Form3
'    Private timer As Timer
'    Dim conn As New MySqlConnection("server=localhost;database=fores;user=root;password=Root@123;")
'    Private total_time, intervalCount As Integer
'    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        PopulateComboBoxes()
'        Panel2.Visible = False
'    End Sub


'    Private Sub PopulateComboBoxes()
'        conn.Open()
'        Dim query As String = "SELECT material_sap_code, material_description FROM bom_material_master"
'        Dim command As New MySqlCommand(query, conn)
'        Try
'            Dim reader As MySqlDataReader = command.ExecuteReader()
'            If reader.HasRows Then
'                While reader.Read()
'                    SelectProduct.Items.Add(reader("material_sap_code").ToString())
'                    SelectDesc.Items.Add(reader("material_description").ToString())
'                End While
'                SelectProduct.AutoCompleteMode = AutoCompleteMode.SuggestAppend
'                SelectProduct.AutoCompleteSource = AutoCompleteSource.ListItems
'                SelectDesc.AutoCompleteMode = AutoCompleteMode.SuggestAppend
'                SelectDesc.AutoCompleteSource = AutoCompleteSource.ListItems
'            End If

'            reader.Close()
'        Catch ex As Exception
'            MessageBox.Show("Error: " & ex.Message)
'        Finally


'            conn.Close()
'        End Try
'    End Sub

'    Private Sub SelectProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectProduct.SelectedIndexChanged
'        Show_value(SelectProduct.SelectedItem.ToString())

'    End Sub

'    Private Sub SelectDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SelectDesc.SelectedIndexChanged
'        Show_value(SelectDesc.SelectedItem.ToString())

'    End Sub

'    Private Sub Show_value(selectedItem As String)
'        If conn.State = ConnectionState.Open Then
'            conn.Close()
'        End If
'        conn.Open()
'        Dim columnName As String = ""
'        If SelectProduct.SelectedItem IsNot Nothing AndAlso selectedItem = SelectProduct.SelectedItem.ToString() Then
'            columnName = "material_sap_code"


'        ElseIf SelectDesc.SelectedItem IsNot Nothing AndAlso selectedItem = SelectDesc.SelectedItem.ToString() Then
'            columnName = "material_description"

'        End If

'        If columnName <> "" Then
'            Dim query As String = $"SELECT * FROM bom_material_master WHERE {columnName} = @selectedItem"
'            Dim command As New MySqlCommand(query, conn)
'            command.Parameters.AddWithValue("@selectedItem", selectedItem)
'            Dim reader As MySqlDataReader = command.ExecuteReader()
'            Try
'                If reader.HasRows Then
'                    reader.Read()
'                    txtMetal1Code.Text = reader("metal1_sap_code").ToString()
'                    txtMetal1Desc.Text = reader("metal1_description").ToString()
'                    txtMetal2Code.Text = reader("metal2_sap_code").ToString()
'                    txtMetal2Desc.Text = reader("metal2_description").ToString()
'                    txtMetal3Code.Text = reader("metal3_sap_code").ToString()
'                    txtMetal3Desc.Text = reader("metal3_description").ToString()
'                    txtCavity.Text = reader("run_cavity").ToString()
'                    If columnName = "material_sap_code" Then
'                        SelectDesc.SelectedItem = reader("material_description").ToString()
'                        SelectDesc.Enabled = False
'                        SelectProduct.Enabled = True
'                    Else
'                        SelectProduct.SelectedItem = reader("material_sap_code").ToString()
'                        SelectDesc.Enabled = True
'                        SelectProduct.Enabled = False
'                    End If

'                    txtCavity.Enabled = False
'                    Panel2.Visible = True
'                    txtMetal1Code.Enabled = False
'                    txtMetal2Code.Enabled = False
'                    txtMetal3Code.Enabled = False
'                    txtMetal1Desc.Enabled = False
'                    txtMetal2Desc.Enabled = False
'                    txtMetal3Desc.Enabled = False

'                Else
'                    MessageBox.Show("Error: No Data Found")
'                End If
'            Catch ex As Exception
'                MessageBox.Show("Error: " & ex.Message)

'            Finally
'                reader.Close()
'            End Try
'        End If
'        conn.Close()
'    End Sub

'    Private Sub txtMetalQty_Enter(sender As Object, e As EventArgs) Handles txtMetal1Qty.Enter, txtMetal2Qty.Enter, txtMetal3Qty.Enter
'        Dim textBox As TextBox = DirectCast(sender, TextBox)
'        Dim metalCodeTextBox As TextBox
'        Dim availableQtyTextBox As TextBox
'        If textBox.Name = "txtMetal1Qty" Then
'            metalCodeTextBox = txtMetal1Code
'            availableQtyTextBox = txtMetal1Qty
'        ElseIf textBox.Name = "txtMetal2Qty" Then
'            metalCodeTextBox = txtMetal2Code
'            availableQtyTextBox = txtMetal2Qty
'        ElseIf textBox.Name = "txtMetal3Qty" Then
'            metalCodeTextBox = txtMetal3Code
'            availableQtyTextBox = txtMetal3Qty
'        Else
'            Return
'        End If
'        Dim metalCode As String = metalCodeTextBox.Text
'        Dim qty As Integer

'        If Integer.TryParse(textBox.Text, qty) Then
'            conn.Open()
'            Dim query As String = $"UPDATE available_metal SET metal_Qty = metal_Qty - {qty}, Date = NOW() WHERE metal_code = '{metalCode}'"
'            Dim command As New MySqlCommand(query, conn)
'            Try
'                command.ExecuteNonQuery()
'                Dim newQty As Integer = Integer.Parse(availableQtyTextBox.Text) - qty
'            Catch ex As Exception
'                MessageBox.Show("Error: " & ex.Message)
'            End Try
'            conn.Close()
'        End If

'    End Sub

'    Private Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click

'        conn.Open()
'        Dim query As String = $"SELECT Cycle_time, run_cavity FROM bom_material_master WHERE material_sap_code = '{SelectProduct.SelectedItem}' OR material_description = '{SelectDesc.SelectedItem}'"
'        Dim command As New MySqlCommand(query, conn)
'        btn_save.Enabled = False
'        Try
'            Dim reader As MySqlDataReader = command.ExecuteReader()

'            If reader.HasRows Then
'                reader.Read()
'                Dim cycleTime As Integer = reader.GetInt32("Cycle_time")
'                Dim runCavity As Integer = reader.GetInt32("run_cavity")
'                reader.Close()

'                timer = New Timer()
'                total_time = cycleTime * 60000        ' Convert minutes to milliseconds
'                timer.Interval = total_time / runCavity
'                Dim numberOfIntervals As Integer = total_time / timer.Interval
'                Dim intervalCount As Integer = 0
'                AddHandler timer.Elapsed, AddressOf TimerElapsed
'                timer.Enabled = True
'                timer.Start()
'                txtMetal1Qty.Enabled = False
'                txtMetal2Qty.Enabled = False
'                txtMetal3Qty.Enabled = False
'                txtMetal1Lot.Enabled = False
'                txtMetal2Lot.Enabled = False
'                txtMetal3Lot.Enabled = False
'                reader.Close()
'            End If
'            conn.Close()
'        Catch ex As Exception
'            MessageBox.Show("Error: " & ex.Message)
'        End Try

'    End Sub

'    Private Sub TimerElapsed(sender As Object, e As ElapsedEventArgs)

'        intervalCount += 1
'        If intervalCount <= Integer.Parse(txtCavity.Text) Then
'            ' Continue with the desired functionality
'            If (Integer.Parse(txtMetal1Qty.Text) <= 0 And txtMetal1Code IsNot Nothing Or Integer.Parse(txtMetal1Qty.Text) <= 0 And txtMetal1Code IsNot Nothing Or Integer.Parse(txtMetal1Qty.Text) <= 0) And txtMetal1Code IsNot Nothing Then
'                ' Timer has completed the desired number of intervals
'                ' Perform any necessary cleanup or actions
'                timer.Stop()
'                timer.Enabled = False
'                btn_save.Enabled = True
'                btn_save.Text = "Restart"
'                txtMetal1Qty.Enabled = True
'                txtMetal2Qty.Enabled = True
'                txtMetal3Qty.Enabled = True
'                txtMetal1Lot.Enabled = True
'                txtMetal2Lot.Enabled = True
'                txtMetal3Lot.Enabled = True
'                MessageBox.Show("Error: Please Fill Metal Quantity")
'            Else
'                Func_update_qty()
'                Func_Barcode()
'            End If
'        Else
'            txtMetal1Qty.Enabled = True
'            txtMetal2Qty.Enabled = True
'            txtMetal3Qty.Enabled = True
'            txtMetal1Lot.Enabled = True
'            txtMetal2Lot.Enabled = True
'            txtMetal3Lot.Enabled = True
'            timer.Stop()
'            timer.Enabled = False
'            btn_save.Enabled = True
'            btn_save.Text = "Start Again"
'            MessageBox.Show("End Of all products", "Success")
'        End If

'    End Sub
'    Private Sub Func_update_qty()
'        conn.Open()
'        Dim Metal1_qty As Integer = Integer.Parse(txtMetal1Qty.Text)
'        Dim Metal2_qty As Integer = Integer.Parse(txtMetal2Qty.Text)
'        Dim Metal3_qty As Integer = Integer.Parse(txtMetal3Qty.Text)

'        Try
'            Dim query As String = $"SELECT * FROM bom_material_master WHERE material_sap_code = '{SelectProduct.SelectedItem}' or material_description='{SelectDesc.SelectedItem}'"
'            Dim command As New MySqlCommand(query, conn)
'            Dim reader As MySqlDataReader = command.ExecuteReader()
'            If reader.HasRows Then
'                reader.Read()
'                If txtMetal1Code IsNot Nothing Then
'                    txtMetal1Qty.Text = (Metal1_qty - reader.GetInt32("metal1_qty")).ToString()
'                End If
'                If txtMetal2Code IsNot Nothing Then
'                    txtMetal2Qty.Text = (Metal2_qty - reader.GetInt32("metal2_qty")).ToString()
'                End If
'                If txtMetal3Code IsNot Nothing Then
'                    txtMetal3Qty.Text = (Metal3_qty - reader.GetInt32("metal3_qty")).ToString()
'                End If
'            End If
'            reader.Close()
'        Catch ex As Exception
'            MessageBox.Show("Error: While Updating to database " & ex.Message)
'        End Try
'        conn.Close()
'    End Sub

'    Private Sub Func_Barcode()
'        Dim gen As New QRCodeGenerator
'        'Create a table format string based on the input
'        Dim tableInput As String = SelectProduct.Text & "|" & (DateTime.Today).ToString("yyyyddMM") & vbCrLf &
'                                   txtMetal1Code.Text & "|" & txtMetal1Lot.Text & vbCrLf &
'                                   txtMetal2Code.Text & "|" & txtMetal2Lot.Text & vbCrLf &
'                                   txtMetal3Code.Text & "|" & txtMetal3Lot.Text & vbCrLf

'        Dim data = gen.CreateQrCode(tableInput, QRCodeGenerator.ECCLevel.Q)
'        Dim code As New QRCode(data)
'        Dim saveDirectory As String = "C:\Users\Bharat\source\repos\fores2\data" ' Change the save directory as per your requirement
'        Dim fileName As String = SelectProduct.SelectedItem + ".pdf"
'        Dim savePath As String = Path.Combine(saveDirectory, fileName)
'        If Not Directory.Exists(saveDirectory) Then
'            Directory.CreateDirectory(saveDirectory)
'        End If
'        Using fs As New FileStream(savePath, FileMode.Create)
'            Try
'                Using doc As New Document()
'                    doc.SetPageSize(PageSize.LETTER.Rotate()) ' Change the page size to LETTER with rotated orientation

'                    Using writer As PdfWriter = PdfWriter.GetInstance(doc, fs)
'                        doc.Open()
'                        Dim table As New PdfPTable(2)
'                        table.WidthPercentage = 50
'                        ' Set cell border style
'                        Dim cellBorder As Integer = Rectangle.TOP_BORDER Or Rectangle.BOTTOM_BORDER Or Rectangle.LEFT_BORDER Or Rectangle.RIGHT_BORDER

'                        Dim image As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(code.GetGraphic(6), System.Drawing.Imaging.ImageFormat.Png)
'                        image.ScalePercent(35)
'                        ' Create a PdfPCell for the image
'                        Dim cell1 As New PdfPCell()
'                        cell1.Border = cellBorder
'                        cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER ' Align the content horizontally to the center
'                        cell1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE ' Align the content vertically to the middle
'                        cell1.AddElement(image)

'                        ' Add the image cell to the table
'                        table.AddCell(cell1)
'                        Dim nestedTable As New PdfPTable(1)
'                        nestedTable.DefaultCell.Border = cellBorder


'                        Dim text1 As New PdfPCell(New Phrase(SelectProduct.SelectedItem.ToString))
'                        text1.Border = cellBorder
'                        text1.Phrase.Font.Size = 12 ' Set the font size

'                        text1.MinimumHeight = 30 ' Set the minimum cell height
'                        nestedTable.AddCell(text1)

'                        Dim text2 As New PdfPCell(New Phrase(DateAndTime.Today.ToString("yyyyddMM") + (txtBatchCode.Text.ToString)))
'                        text2.Border = cellBorder
'                        text2.MinimumHeight = 30 ' Set the minimum cell height
'                        text2.Phrase.Font.Size = 12 ' Set the font size

'                        nestedTable.AddCell(text2)

'                        Dim text3 As New PdfPCell(New Phrase(txtMetal1Code.Text.ToString))
'                        text3.Border = cellBorder
'                        text1.Phrase.Font.Size = 12 ' Set the font size
'                        text3.MinimumHeight = 30 ' Set the minimum cell height

'                        nestedTable.AddCell(text3)

'                        Dim cell2 As New PdfPCell(nestedTable)
'                        cell2.Border = cellBorder
'                        table.AddCell(cell2)

'                        Dim emptyCell As New PdfPCell()
'                        emptyCell.Border = cellBorder
'                        emptyCell.FixedHeight = 20 ' Set the height of the empty cell

'                        table.AddCell(emptyCell)
'                        table.AddCell(emptyCell)
'                        doc.Add(table)
'                        doc.Close()
'                    End Using
'                End Using
'            Catch ex As Exception
'                ' Handle any exceptions
'            End Try
'        End Using
'        'Dim printerExample As New Printer_Class()
'        'printerExample.PrintPDF(savePath)

'    End Sub

'    Private Sub SelectProduct_Click(sender As Object, e As EventArgs) Handles SelectProduct.Click
'        If SelectProduct.SelectedIndex Then
'            SelectProduct.Text = ""
'        End If
'    End Sub
'    Private Sub SelectDesc_Click(sender As Object, e As EventArgs) Handles SelectDesc.Click
'        If SelectDesc.SelectedItem Then
'            SelectDesc.Text = ""
'        End If
'    End Sub

'    ' Rest of the code...
'End Class

'Private Sub TimerElapsed(sender As Object, e As ElapsedEventArgs)

'    intervalCount += 1
'    If intervalCount <= Integer.Parse(txtCavity.Text) Then
'        ' Continue with the desired functionality
'        If (Integer.Parse(txtMetal1Qty.Text) <= 0 And txtMetal1Code IsNot Nothing Or Integer.Parse(txtMetal1Qty.Text) <= 0 And txtMetal1Code IsNot Nothing Or Integer.Parse(txtMetal1Qty.Text) <= 0) And txtMetal1Code IsNot Nothing Then
'            ' Timer has completed the desired number of intervals
'            ' Perform any necessary cleanup or actions
'            timer.Stop()
'            timer.Enabled = False
'            btn_save.Enabled = True
'            btn_save.Text = "Restart"
'            txtMetal1Qty.Enabled = True
'            txtMetal2Qty.Enabled = True
'            txtMetal3Qty.Enabled = True
'            txtMetal1Lot.Enabled = True
'            txtMetal2Lot.Enabled = True
'            txtMetal3Lot.Enabled = True
'            MessageBox.Show("Error: Please Fill Metal Quantity")
'        Else
'            Func_update_qty()
'            Func_Barcode()
'        End If
'    Else
'        txtMetal1Qty.Enabled = True
'        txtMetal2Qty.Enabled = True
'        txtMetal3Qty.Enabled = True
'        txtMetal1Lot.Enabled = True
'        txtMetal2Lot.Enabled = True
'        txtMetal3Lot.Enabled = True
'        timer.Stop()
'        timer.Enabled = False
'        btn_save.Enabled = True
'        btn_save.Text = "Start Again"
'        MessageBox.Show("End Of all products", "Success")
'    End If

'End Sub