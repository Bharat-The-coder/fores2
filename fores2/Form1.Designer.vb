<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        SelectProduct = New ComboBox()
        SelectDesc = New ComboBox()
        Label2 = New Label()
        Panel1 = New Panel()
        Panel2 = New Panel()
        Panel5 = New Panel()
        input = New Label()
        btn_save = New Button()
        Label6 = New Label()
        Label5 = New Label()
        Label4 = New Label()
        Label3 = New Label()
        txtMetal3Qty = New TextBox()
        txtMetal3Lot = New TextBox()
        txtMetal3Desc = New TextBox()
        txtMetal3Code = New TextBox()
        txtMetal2Qty = New TextBox()
        txtMetal2Lot = New TextBox()
        txtMetal2Desc = New TextBox()
        txtMetal2Code = New TextBox()
        txtMetal1Qty = New TextBox()
        txtMetal1Lot = New TextBox()
        txtMetal1Desc = New TextBox()
        txtMetal1Code = New TextBox()
        Panel3 = New Panel()
        Panel4 = New Panel()
        Panel6 = New Panel()
        Label9 = New Label()
        Label8 = New Label()
        Panel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel5.SuspendLayout()
        Panel3.SuspendLayout()
        Panel4.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.ForeColor = SystemColors.ActiveCaptionText
        Label1.Location = New Point(69, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(114, 23)
        Label1.TabIndex = 6
        Label1.Text = "Material Code"
        ' 
        ' SelectProduct
        ' 
        SelectProduct.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        SelectProduct.FormattingEnabled = True
        SelectProduct.Location = New Point(17, 38)
        SelectProduct.Name = "SelectProduct"
        SelectProduct.Size = New Size(203, 31)
        SelectProduct.TabIndex = 9
        SelectProduct.Text = "Select Sap Code "
        ' 
        ' SelectDesc
        ' 
        SelectDesc.AutoCompleteMode = AutoCompleteMode.Suggest
        SelectDesc.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        SelectDesc.FormattingEnabled = True
        SelectDesc.Location = New Point(256, 38)
        SelectDesc.Name = "SelectDesc"
        SelectDesc.Size = New Size(203, 31)
        SelectDesc.TabIndex = 19
        SelectDesc.Text = "Select Description"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.BackColor = Color.Transparent
        Label2.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.ForeColor = SystemColors.ActiveCaptionText
        Label2.Location = New Point(267, 12)
        Label2.Name = "Label2"
        Label2.Size = New Size(164, 23)
        Label2.TabIndex = 20
        Label2.Text = "Material Description"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = SystemColors.ActiveBorder
        Panel1.BorderStyle = BorderStyle.Fixed3D
        Panel1.Controls.Add(SelectProduct)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(SelectDesc)
        Panel1.Controls.Add(Label2)
        Panel1.Location = New Point(13, 79)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(659, 101)
        Panel1.TabIndex = 34
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = SystemColors.ActiveBorder
        Panel2.BorderStyle = BorderStyle.Fixed3D
        Panel2.Controls.Add(Panel5)
        Panel2.Controls.Add(btn_save)
        Panel2.Controls.Add(Label6)
        Panel2.Controls.Add(Label5)
        Panel2.Controls.Add(Label4)
        Panel2.Controls.Add(Label3)
        Panel2.Controls.Add(txtMetal3Qty)
        Panel2.Controls.Add(txtMetal3Lot)
        Panel2.Controls.Add(txtMetal3Desc)
        Panel2.Controls.Add(txtMetal3Code)
        Panel2.Controls.Add(txtMetal2Qty)
        Panel2.Controls.Add(txtMetal2Lot)
        Panel2.Controls.Add(txtMetal2Desc)
        Panel2.Controls.Add(txtMetal2Code)
        Panel2.Controls.Add(txtMetal1Qty)
        Panel2.Controls.Add(txtMetal1Lot)
        Panel2.Controls.Add(txtMetal1Desc)
        Panel2.Controls.Add(txtMetal1Code)
        Panel2.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Panel2.Location = New Point(12, 198)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(660, 339)
        Panel2.TabIndex = 35
        ' 
        ' Panel5
        ' 
        Panel5.BackColor = SystemColors.ActiveCaptionText
        Panel5.BorderStyle = BorderStyle.Fixed3D
        Panel5.Controls.Add(input)
        Panel5.Dock = DockStyle.Top
        Panel5.Location = New Point(0, 0)
        Panel5.Name = "Panel5"
        Panel5.Size = New Size(656, 34)
        Panel5.TabIndex = 50
        ' 
        ' input
        ' 
        input.AutoSize = True
        input.BackColor = SystemColors.ActiveCaptionText
        input.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        input.ForeColor = SystemColors.ButtonFace
        input.Location = New Point(275, 3)
        input.Margin = New Padding(3)
        input.MaximumSize = New Size(200, 0)
        input.Name = "input"
        input.Size = New Size(62, 23)
        input.TabIndex = 33
        input.Text = "Details"
        ' 
        ' btn_save
        ' 
        btn_save.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        btn_save.BackColor = Color.DarkGreen
        btn_save.Cursor = Cursors.Hand
        btn_save.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        btn_save.ForeColor = SystemColors.ButtonFace
        btn_save.Location = New Point(253, 285)
        btn_save.Name = "btn_save"
        btn_save.Size = New Size(110, 32)
        btn_save.TabIndex = 36
        btn_save.Text = "Start"
        btn_save.UseVisualStyleBackColor = False
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.BackColor = Color.Transparent
        Label6.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label6.ForeColor = SystemColors.ActiveCaptionText
        Label6.Location = New Point(498, 75)
        Label6.MaximumSize = New Size(200, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(76, 23)
        Label6.TabIndex = 49
        Label6.Text = "Quantity"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.BackColor = Color.Transparent
        Label5.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label5.ForeColor = SystemColors.ActiveCaptionText
        Label5.Location = New Point(345, 75)
        Label5.MaximumSize = New Size(200, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(114, 23)
        Label5.TabIndex = 48
        Label5.Text = "Lot/Batch No."
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.BackColor = Color.Transparent
        Label4.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label4.ForeColor = SystemColors.ActiveCaptionText
        Label4.Location = New Point(204, 75)
        Label4.MaximumSize = New Size(200, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(95, 23)
        Label4.TabIndex = 47
        Label4.Text = "Description"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.BackColor = Color.Transparent
        Label3.Font = New Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.ForeColor = SystemColors.ActiveCaptionText
        Label3.Location = New Point(40, 75)
        Label3.MaximumSize = New Size(200, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(81, 23)
        Label3.TabIndex = 46
        Label3.Text = "Sap_code"
        ' 
        ' txtMetal3Qty
        ' 
        txtMetal3Qty.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal3Qty.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal3Qty.Location = New Point(479, 233)
        txtMetal3Qty.Name = "txtMetal3Qty"
        txtMetal3Qty.Size = New Size(124, 26)
        txtMetal3Qty.TabIndex = 45
        txtMetal3Qty.Text = "0"
        ' 
        ' txtMetal3Lot
        ' 
        txtMetal3Lot.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal3Lot.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal3Lot.Location = New Point(339, 233)
        txtMetal3Lot.Name = "txtMetal3Lot"
        txtMetal3Lot.Size = New Size(120, 26)
        txtMetal3Lot.TabIndex = 44
        ' 
        ' txtMetal3Desc
        ' 
        txtMetal3Desc.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal3Desc.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal3Desc.Location = New Point(173, 233)
        txtMetal3Desc.Name = "txtMetal3Desc"
        txtMetal3Desc.Size = New Size(148, 26)
        txtMetal3Desc.TabIndex = 43
        ' 
        ' txtMetal3Code
        ' 
        txtMetal3Code.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal3Code.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal3Code.Location = New Point(17, 233)
        txtMetal3Code.Name = "txtMetal3Code"
        txtMetal3Code.Size = New Size(134, 26)
        txtMetal3Code.TabIndex = 42
        ' 
        ' txtMetal2Qty
        ' 
        txtMetal2Qty.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal2Qty.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal2Qty.Location = New Point(479, 171)
        txtMetal2Qty.Name = "txtMetal2Qty"
        txtMetal2Qty.Size = New Size(124, 26)
        txtMetal2Qty.TabIndex = 41
        txtMetal2Qty.Text = "0"
        ' 
        ' txtMetal2Lot
        ' 
        txtMetal2Lot.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal2Lot.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal2Lot.Location = New Point(339, 171)
        txtMetal2Lot.Name = "txtMetal2Lot"
        txtMetal2Lot.Size = New Size(120, 26)
        txtMetal2Lot.TabIndex = 40
        ' 
        ' txtMetal2Desc
        ' 
        txtMetal2Desc.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal2Desc.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal2Desc.Location = New Point(173, 171)
        txtMetal2Desc.Name = "txtMetal2Desc"
        txtMetal2Desc.Size = New Size(148, 26)
        txtMetal2Desc.TabIndex = 39
        ' 
        ' txtMetal2Code
        ' 
        txtMetal2Code.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal2Code.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal2Code.Location = New Point(18, 171)
        txtMetal2Code.Name = "txtMetal2Code"
        txtMetal2Code.Size = New Size(134, 26)
        txtMetal2Code.TabIndex = 38
        ' 
        ' txtMetal1Qty
        ' 
        txtMetal1Qty.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal1Qty.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal1Qty.Location = New Point(479, 116)
        txtMetal1Qty.Name = "txtMetal1Qty"
        txtMetal1Qty.Size = New Size(124, 26)
        txtMetal1Qty.TabIndex = 37
        txtMetal1Qty.Text = "0"
        ' 
        ' txtMetal1Lot
        ' 
        txtMetal1Lot.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal1Lot.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal1Lot.Location = New Point(340, 116)
        txtMetal1Lot.Name = "txtMetal1Lot"
        txtMetal1Lot.Size = New Size(120, 26)
        txtMetal1Lot.TabIndex = 36
        ' 
        ' txtMetal1Desc
        ' 
        txtMetal1Desc.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal1Desc.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal1Desc.Location = New Point(173, 116)
        txtMetal1Desc.Name = "txtMetal1Desc"
        txtMetal1Desc.Size = New Size(148, 26)
        txtMetal1Desc.TabIndex = 35
        ' 
        ' txtMetal1Code
        ' 
        txtMetal1Code.AutoCompleteMode = AutoCompleteMode.Suggest
        txtMetal1Code.Font = New Font("Comic Sans MS", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        txtMetal1Code.Location = New Point(18, 116)
        txtMetal1Code.Name = "txtMetal1Code"
        txtMetal1Code.Size = New Size(134, 26)
        txtMetal1Code.TabIndex = 34
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.LightPink
        Panel3.Controls.Add(Panel4)
        Panel3.Controls.Add(Label8)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(1261, 69)
        Panel3.TabIndex = 36
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Black
        Panel4.BackgroundImageLayout = ImageLayout.Center
        Panel4.BorderStyle = BorderStyle.FixedSingle
        Panel4.Controls.Add(Panel6)
        Panel4.Controls.Add(Label9)
        Panel4.Dock = DockStyle.Top
        Panel4.ForeColor = Color.Crimson
        Panel4.Location = New Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(1261, 69)
        Panel4.TabIndex = 37
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.Red
        Panel6.Location = New Point(0, 75)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(654, 558)
        Panel6.TabIndex = 37
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Font = New Font("Lucida Calligraphy", 18F, FontStyle.Bold, GraphicsUnit.Point)
        Label9.ForeColor = SystemColors.ButtonHighlight
        Label9.Location = New Point(558, 19)
        Label9.Name = "Label9"
        Label9.Size = New Size(253, 31)
        Label9.TabIndex = 0
        Label9.Text = "Fores Elastomech"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Lucida Calligraphy", 18F, FontStyle.Bold, GraphicsUnit.Point)
        Label8.ForeColor = SystemColors.ActiveCaptionText
        Label8.Location = New Point(559, 20)
        Label8.Name = "Label8"
        Label8.Size = New Size(253, 31)
        Label8.TabIndex = 0
        Label8.Text = "Fores Elastomech"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSizeMode = AutoSizeMode.GrowAndShrink
        BackColor = SystemColors.ControlLight
        ClientSize = New Size(1261, 570)
        Controls.Add(Panel1)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        ForeColor = SystemColors.ButtonHighlight
        Name = "Form1"
        Text = "Print Stickers"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel5.ResumeLayout(False)
        Panel5.PerformLayout()
        Panel3.ResumeLayout(False)
        Panel3.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents SelectProduct As ComboBox
    Friend WithEvents SelectDesc As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMetal3Qty As TextBox
    Friend WithEvents txtMetal3Lot As TextBox
    Friend WithEvents txtMetal3Desc As TextBox
    Friend WithEvents txtMetal3Code As TextBox
    Friend WithEvents txtMetal2Qty As TextBox
    Friend WithEvents txtMetal2Lot As TextBox
    Friend WithEvents txtMetal2Desc As TextBox
    Friend WithEvents txtMetal2Code As TextBox
    Friend WithEvents txtMetal1Qty As TextBox
    Friend WithEvents txtMetal1Lot As TextBox
    Friend WithEvents txtMetal1Desc As TextBox
    Friend WithEvents txtMetal1Code As TextBox
    Friend WithEvents input As Label
    Friend WithEvents btn_save As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel6 As Panel
End Class
