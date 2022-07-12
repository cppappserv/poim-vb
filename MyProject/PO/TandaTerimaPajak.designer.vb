<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TandaTerimaPajak
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TandaTerimaPajak))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAll = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnView = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TxtPOByName = New System.Windows.Forms.TextBox
        Me.btnPOBy = New System.Windows.Forms.Button
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPOBy = New System.Windows.Forms.TextBox
        Me.txtAJU = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnCompany = New System.Windows.Forms.Button
        Me.tgl = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCompany = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCompanyCode = New System.Windows.Forms.TextBox
        Me.txtByName = New System.Windows.Forms.TextBox
        Me.txtShipNo = New System.Windows.Forms.TextBox
        Me.txtPIB = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtNewBy = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtBL = New System.Windows.Forms.TextBox
        Me.txtPPH = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPIUD = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBM = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtVAT = New System.Windows.Forms.TextBox
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Curr_Name = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBy = New System.Windows.Forms.TextBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Location = New System.Drawing.Point(12, 202)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(983, 373)
        Me.DataGridView1.TabIndex = 4
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator3, Me.btnAll, Me.ToolStripSeparator4, Me.btnView, Me.ToolStripSeparator2, Me.btnPrint, Me.ToolStripSeparator5})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1007, 25)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.AutoSize = False
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 22)
        Me.btnClose.Text = "Close"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnAll
        '
        Me.btnAll.AutoSize = False
        Me.btnAll.Image = CType(resources.GetObject("btnAll.Image"), System.Drawing.Image)
        Me.btnAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAll.Name = "btnAll"
        Me.btnAll.Size = New System.Drawing.Size(100, 22)
        Me.btnAll.Text = "View All"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnView
        '
        Me.btnView.AutoSize = False
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(100, 22)
        Me.btnView.Text = "View by Filter"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.AutoSize = False
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 22)
        Me.btnPrint.Text = "Print"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtPOByName)
        Me.GroupBox1.Controls.Add(Me.btnPOBy)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtPOBy)
        Me.GroupBox1.Controls.Add(Me.txtAJU)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.btnCompany)
        Me.GroupBox1.Controls.Add(Me.tgl)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCompany)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtCompanyCode)
        Me.GroupBox1.Controls.Add(Me.txtByName)
        Me.GroupBox1.Controls.Add(Me.txtShipNo)
        Me.GroupBox1.Controls.Add(Me.txtPIB)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtNewBy)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtBL)
        Me.GroupBox1.Controls.Add(Me.txtPPH)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPIUD)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtBM)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtVAT)
        Me.GroupBox1.Controls.Add(Me.txtRate)
        Me.GroupBox1.Controls.Add(Me.Curr_Name)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtBy)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(983, 166)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'TxtPOByName
        '
        Me.TxtPOByName.Location = New System.Drawing.Point(137, 87)
        Me.TxtPOByName.MaxLength = 5
        Me.TxtPOByName.Name = "TxtPOByName"
        Me.TxtPOByName.ReadOnly = True
        Me.TxtPOByName.Size = New System.Drawing.Size(302, 20)
        Me.TxtPOByName.TabIndex = 2281
        '
        'btnPOBy
        '
        Me.btnPOBy.Image = CType(resources.GetObject("btnPOBy.Image"), System.Drawing.Image)
        Me.btnPOBy.Location = New System.Drawing.Point(442, 88)
        Me.btnPOBy.Name = "btnPOBy"
        Me.btnPOBy.Size = New System.Drawing.Size(22, 18)
        Me.btnPOBy.TabIndex = 2279
        Me.btnPOBy.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(12, 90)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(82, 13)
        Me.Label13.TabIndex = 2280
        Me.Label13.Text = "BL Created By *"
        '
        'txtPOBy
        '
        Me.txtPOBy.Location = New System.Drawing.Point(121, 87)
        Me.txtPOBy.MaxLength = 5
        Me.txtPOBy.Name = "txtPOBy"
        Me.txtPOBy.Size = New System.Drawing.Size(13, 20)
        Me.txtPOBy.TabIndex = 2278
        Me.txtPOBy.Visible = False
        '
        'txtAJU
        '
        Me.txtAJU.Location = New System.Drawing.Point(137, 135)
        Me.txtAJU.MaxLength = 5
        Me.txtAJU.Name = "txtAJU"
        Me.txtAJU.ReadOnly = True
        Me.txtAJU.Size = New System.Drawing.Size(158, 20)
        Me.txtAJU.TabIndex = 2267
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(302, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 2266
        Me.Label9.Text = "PIB No."
        '
        'btnCompany
        '
        Me.btnCompany.Image = CType(resources.GetObject("btnCompany.Image"), System.Drawing.Image)
        Me.btnCompany.Location = New System.Drawing.Point(442, 66)
        Me.btnCompany.Name = "btnCompany"
        Me.btnCompany.Size = New System.Drawing.Size(22, 18)
        Me.btnCompany.TabIndex = 2265
        Me.btnCompany.UseVisualStyleBackColor = True
        '
        'tgl
        '
        Me.tgl.Checked = False
        Me.tgl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl.Location = New System.Drawing.Point(137, 17)
        Me.tgl.Name = "tgl"
        Me.tgl.ShowCheckBox = True
        Me.tgl.Size = New System.Drawing.Size(100, 20)
        Me.tgl.TabIndex = 2264
        Me.tgl.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 12)
        Me.Label8.TabIndex = 2243
        Me.Label8.Text = "* Filter Criteria"
        '
        'txtCompany
        '
        Me.txtCompany.Location = New System.Drawing.Point(137, 64)
        Me.txtCompany.MaxLength = 5
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.ReadOnly = True
        Me.txtCompany.Size = New System.Drawing.Size(302, 20)
        Me.txtCompany.TabIndex = 2242
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(12, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(58, 13)
        Me.Label12.TabIndex = 2241
        Me.Label12.Text = "Company *"
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.Location = New System.Drawing.Point(121, 64)
        Me.txtCompanyCode.MaxLength = 5
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.Size = New System.Drawing.Size(13, 20)
        Me.txtCompanyCode.TabIndex = 2239
        Me.txtCompanyCode.Visible = False
        '
        'txtByName
        '
        Me.txtByName.Enabled = False
        Me.txtByName.Location = New System.Drawing.Point(137, 41)
        Me.txtByName.MaxLength = 5
        Me.txtByName.Name = "txtByName"
        Me.txtByName.Size = New System.Drawing.Size(302, 20)
        Me.txtByName.TabIndex = 2238
        '
        'txtShipNo
        '
        Me.txtShipNo.Location = New System.Drawing.Point(121, 111)
        Me.txtShipNo.MaxLength = 5
        Me.txtShipNo.Name = "txtShipNo"
        Me.txtShipNo.Size = New System.Drawing.Size(13, 20)
        Me.txtShipNo.TabIndex = 2237
        Me.txtShipNo.Visible = False
        '
        'txtPIB
        '
        Me.txtPIB.Location = New System.Drawing.Point(346, 135)
        Me.txtPIB.MaxLength = 5
        Me.txtPIB.Name = "txtPIB"
        Me.txtPIB.ReadOnly = True
        Me.txtPIB.Size = New System.Drawing.Size(93, 20)
        Me.txtPIB.TabIndex = 2236
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 138)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 13)
        Me.Label11.TabIndex = 2235
        Me.Label11.Text = "AJU No."
        '
        'txtNewBy
        '
        Me.txtNewBy.Location = New System.Drawing.Point(104, 41)
        Me.txtNewBy.MaxLength = 5
        Me.txtNewBy.Name = "txtNewBy"
        Me.txtNewBy.Size = New System.Drawing.Size(14, 20)
        Me.txtNewBy.TabIndex = 2234
        Me.txtNewBy.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 114)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 13)
        Me.Label10.TabIndex = 2232
        Me.Label10.Text = "B/L or AWB No."
        '
        'txtBL
        '
        Me.txtBL.Location = New System.Drawing.Point(137, 111)
        Me.txtBL.MaxLength = 5
        Me.txtBL.Name = "txtBL"
        Me.txtBL.ReadOnly = True
        Me.txtBL.Size = New System.Drawing.Size(302, 20)
        Me.txtBL.TabIndex = 2231
        '
        'txtPPH
        '
        Me.txtPPH.Location = New System.Drawing.Point(872, 111)
        Me.txtPPH.MaxLength = 13
        Me.txtPPH.Name = "txtPPH"
        Me.txtPPH.ReadOnly = True
        Me.txtPPH.Size = New System.Drawing.Size(91, 20)
        Me.txtPPH.TabIndex = 2224
        Me.txtPPH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(747, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 2223
        Me.Label6.Text = "PPh Pasal 22"
        '
        'txtPIUD
        '
        Me.txtPIUD.Location = New System.Drawing.Point(872, 135)
        Me.txtPIUD.MaxLength = 13
        Me.txtPIUD.Name = "txtPIUD"
        Me.txtPIUD.ReadOnly = True
        Me.txtPIUD.Size = New System.Drawing.Size(91, 20)
        Me.txtPIUD.TabIndex = 2222
        Me.txtPIUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(747, 138)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 2221
        Me.Label7.Text = "PIUD/TR"
        '
        'txtBM
        '
        Me.txtBM.Location = New System.Drawing.Point(872, 64)
        Me.txtBM.MaxLength = 13
        Me.txtBM.Name = "txtBM"
        Me.txtBM.ReadOnly = True
        Me.txtBM.Size = New System.Drawing.Size(91, 20)
        Me.txtBM.TabIndex = 2220
        Me.txtBM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(747, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 13)
        Me.Label3.TabIndex = 2219
        Me.Label3.Text = "Bea Masuk"
        '
        'txtVAT
        '
        Me.txtVAT.Location = New System.Drawing.Point(872, 88)
        Me.txtVAT.MaxLength = 13
        Me.txtVAT.Name = "txtVAT"
        Me.txtVAT.ReadOnly = True
        Me.txtVAT.Size = New System.Drawing.Size(91, 20)
        Me.txtVAT.TabIndex = 2218
        Me.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(872, 41)
        Me.txtRate.MaxLength = 13
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReadOnly = True
        Me.txtRate.Size = New System.Drawing.Size(91, 20)
        Me.txtRate.TabIndex = 2217
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Curr_Name
        '
        Me.Curr_Name.AutoSize = True
        Me.Curr_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Curr_Name.Location = New System.Drawing.Point(214, 39)
        Me.Curr_Name.Name = "Curr_Name"
        Me.Curr_Name.Size = New System.Drawing.Size(0, 13)
        Me.Curr_Name.TabIndex = 2216
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(747, 91)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "VAT (PPn Masuk)"
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(442, 42)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(22, 18)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(747, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Tax Rate"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Received Date *"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Received By *"
        '
        'txtBy
        '
        Me.txtBy.Location = New System.Drawing.Point(121, 41)
        Me.txtBy.MaxLength = 5
        Me.txtBy.Name = "txtBy"
        Me.txtBy.Size = New System.Drawing.Size(13, 20)
        Me.txtBy.TabIndex = 1
        Me.txtBy.Visible = False
        '
        'TandaTerimaPajak
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1007, 587)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "TandaTerimaPajak"
        Me.Text = "Tanda Terima Pajak"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnView As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBy As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Curr_Name As System.Windows.Forms.Label
    Friend WithEvents txtVAT As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents txtBM As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPPH As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPIUD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtBL As System.Windows.Forms.TextBox
    Friend WithEvents txtNewBy As System.Windows.Forms.TextBox
    Friend WithEvents txtPIB As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtShipNo As System.Windows.Forms.TextBox
    Friend WithEvents txtByName As System.Windows.Forms.TextBox
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyCode As System.Windows.Forms.TextBox
    Friend WithEvents btnAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tgl As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnCompany As System.Windows.Forms.Button
    Friend WithEvents txtAJU As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtPOByName As System.Windows.Forms.TextBox
    Friend WithEvents btnPOBy As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPOBy As System.Windows.Forms.TextBox
End Class
