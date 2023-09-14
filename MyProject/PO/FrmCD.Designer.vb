<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCD))
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.crtt = New System.Windows.Forms.Label
        Me.crt = New System.Windows.Forms.TextBox
        Me.crtdttext = New System.Windows.Forms.Label
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.remark = New System.Windows.Forms.RichTextBox
        Me.btnSearchCompany = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.btnMat = New System.Windows.Forms.Button
        Me.btnListCD = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.total = New System.Windows.Forms.TextBox
        Me.lblCurrency_Name = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtMM = New System.Windows.Forms.TextBox
        Me.txtDD = New System.Windows.Forms.TextBox
        Me.dtDel = New System.Windows.Forms.DateTimePicker
        Me.rdMM = New System.Windows.Forms.RadioButton
        Me.rdDD = New System.Windows.Forms.RadioButton
        Me.rdDel = New System.Windows.Forms.RadioButton
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnSearchCurrency = New System.Windows.Forms.Button
        Me.txtCurrency_Code = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtCDNo = New System.Windows.Forms.TextBox
        Me.lblSupplierName = New System.Windows.Forms.Label
        Me.btnSearchSupplier = New System.Windows.Forms.Button
        Me.txtSupplier_Code = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblCompany_Name = New System.Windows.Forms.Label
        Me.txtCompany_Code = New System.Windows.Forms.TextBox
        Me.dtPer2 = New System.Windows.Forms.DateTimePicker
        Me.dtPer1 = New System.Windows.Forms.DateTimePicker
        Me.dtCT = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.grid1 = New System.Windows.Forms.DataGridView
        Me.grid2 = New System.Windows.Forms.DataGridView
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.crtcode = New System.Windows.Forms.TextBox
        Me.crtdt = New System.Windows.Forms.DateTimePicker
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(539, 47)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2178
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(678, 40)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(215, 20)
        Me.crt.TabIndex = 6
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(539, 24)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2177
        Me.crtdttext.Text = "Created Date"
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(678, 64)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(215, 20)
        Me.Status.TabIndex = 9
        Me.Status.Text = "Open"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(539, 71)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2173
        Me.Label15.Text = "Status"
        '
        'remark
        '
        Me.remark.Location = New System.Drawing.Point(129, 160)
        Me.remark.Name = "remark"
        Me.remark.Size = New System.Drawing.Size(324, 89)
        Me.remark.TabIndex = 2
        Me.remark.Text = ""
        '
        'btnSearchCompany
        '
        Me.btnSearchCompany.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearchCompany.Location = New System.Drawing.Point(191, 110)
        Me.btnSearchCompany.Name = "btnSearchCompany"
        Me.btnSearchCompany.Size = New System.Drawing.Size(22, 18)
        Me.btnSearchCompany.TabIndex = 2
        Me.btnSearchCompany.TabStop = False
        Me.btnSearchCompany.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 163)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Remark"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Contract Date"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnNew, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1008, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.AutoSize = False
        Me.btnClose.Image = Global.poim.My.Resources.Resources.CLOSE
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(60, 22)
        Me.btnClose.Text = "Close"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnNew
        '
        Me.btnNew.Image = Global.poim.My.Resources.Resources.NewDocumentHS
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 22)
        Me.btnNew.Text = "New"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Enabled = False
        Me.btnSave.Image = Global.poim.My.Resources.Resources.SaveHL
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(60, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnReject
        '
        Me.btnReject.AutoSize = False
        Me.btnReject.Enabled = False
        Me.btnReject.Image = Global.poim.My.Resources.Resources.delete
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(60, 22)
        Me.btnReject.Text = "Delete"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Company"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.crtdt)
        Me.GroupBox1.Controls.Add(Me.crtcode)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.btnMat)
        Me.GroupBox1.Controls.Add(Me.btnListCD)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.total)
        Me.GroupBox1.Controls.Add(Me.lblCurrency_Name)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.btnSearchCurrency)
        Me.GroupBox1.Controls.Add(Me.txtCurrency_Code)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtCDNo)
        Me.GroupBox1.Controls.Add(Me.lblSupplierName)
        Me.GroupBox1.Controls.Add(Me.btnSearchSupplier)
        Me.GroupBox1.Controls.Add(Me.txtSupplier_Code)
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.lblCompany_Name)
        Me.GroupBox1.Controls.Add(Me.txtCompany_Code)
        Me.GroupBox1.Controls.Add(Me.dtPer2)
        Me.GroupBox1.Controls.Add(Me.dtPer1)
        Me.GroupBox1.Controls.Add(Me.dtCT)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.crtt)
        Me.GroupBox1.Controls.Add(Me.crt)
        Me.GroupBox1.Controls.Add(Me.crtdttext)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.remark)
        Me.GroupBox1.Controls.Add(Me.btnSearchCompany)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 37)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(982, 267)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(220, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(10, 13)
        Me.Label11.TabIndex = 2250
        Me.Label11.Text = "-"
        '
        'btnMat
        '
        Me.btnMat.Image = CType(resources.GetObject("btnMat.Image"), System.Drawing.Image)
        Me.btnMat.Location = New System.Drawing.Point(417, 124)
        Me.btnMat.Name = "btnMat"
        Me.btnMat.Size = New System.Drawing.Size(22, 18)
        Me.btnMat.TabIndex = 2249
        Me.btnMat.UseVisualStyleBackColor = True
        Me.btnMat.Visible = False
        '
        'btnListCD
        '
        Me.btnListCD.Image = CType(resources.GetObject("btnListCD.Image"), System.Drawing.Image)
        Me.btnListCD.Location = New System.Drawing.Point(380, 16)
        Me.btnListCD.Name = "btnListCD"
        Me.btnListCD.Size = New System.Drawing.Size(22, 20)
        Me.btnListCD.TabIndex = 2247
        Me.btnListCD.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(539, 233)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 2246
        Me.Label7.Text = "Total Amount"
        '
        'total
        '
        Me.total.Location = New System.Drawing.Point(678, 226)
        Me.total.MaxLength = 5
        Me.total.Name = "total"
        Me.total.ReadOnly = True
        Me.total.Size = New System.Drawing.Size(139, 20)
        Me.total.TabIndex = 2245
        Me.total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCurrency_Name
        '
        Me.lblCurrency_Name.AutoSize = True
        Me.lblCurrency_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency_Name.Location = New System.Drawing.Point(768, 204)
        Me.lblCurrency_Name.Name = "lblCurrency_Name"
        Me.lblCurrency_Name.Size = New System.Drawing.Size(93, 13)
        Me.lblCurrency_Name.TabIndex = 2244
        Me.lblCurrency_Name.Text = "Currency Name"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtMM)
        Me.GroupBox2.Controls.Add(Me.txtDD)
        Me.GroupBox2.Controls.Add(Me.dtDel)
        Me.GroupBox2.Controls.Add(Me.rdMM)
        Me.GroupBox2.Controls.Add(Me.rdDD)
        Me.GroupBox2.Controls.Add(Me.rdDel)
        Me.GroupBox2.Location = New System.Drawing.Point(678, 107)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 87)
        Me.GroupBox2.TabIndex = 2243
        Me.GroupBox2.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(85, 60)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(41, 13)
        Me.Label13.TabIndex = 2252
        Me.Label13.Text = "months"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(85, 38)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 2251
        Me.Label12.Text = "days"
        '
        'txtMM
        '
        Me.txtMM.Location = New System.Drawing.Point(27, 53)
        Me.txtMM.Name = "txtMM"
        Me.txtMM.Size = New System.Drawing.Size(46, 20)
        Me.txtMM.TabIndex = 2250
        '
        'txtDD
        '
        Me.txtDD.Location = New System.Drawing.Point(27, 31)
        Me.txtDD.Name = "txtDD"
        Me.txtDD.Size = New System.Drawing.Size(46, 20)
        Me.txtDD.TabIndex = 2249
        '
        'dtDel
        '
        Me.dtDel.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDel.Location = New System.Drawing.Point(27, 8)
        Me.dtDel.Name = "dtDel"
        Me.dtDel.ShowCheckBox = True
        Me.dtDel.Size = New System.Drawing.Size(106, 20)
        Me.dtDel.TabIndex = 2240
        Me.dtDel.Value = New Date(2009, 2, 24, 0, 0, 0, 0)
        '
        'rdMM
        '
        Me.rdMM.AutoSize = True
        Me.rdMM.Location = New System.Drawing.Point(7, 56)
        Me.rdMM.Name = "rdMM"
        Me.rdMM.Size = New System.Drawing.Size(14, 13)
        Me.rdMM.TabIndex = 2239
        Me.rdMM.TabStop = True
        Me.rdMM.UseVisualStyleBackColor = True
        '
        'rdDD
        '
        Me.rdDD.AutoSize = True
        Me.rdDD.Location = New System.Drawing.Point(7, 34)
        Me.rdDD.Name = "rdDD"
        Me.rdDD.Size = New System.Drawing.Size(14, 13)
        Me.rdDD.TabIndex = 2238
        Me.rdDD.TabStop = True
        Me.rdDD.UseVisualStyleBackColor = True
        '
        'rdDel
        '
        Me.rdDel.AutoSize = True
        Me.rdDel.Checked = True
        Me.rdDel.Location = New System.Drawing.Point(7, 12)
        Me.rdDel.Name = "rdDel"
        Me.rdDel.Size = New System.Drawing.Size(14, 13)
        Me.rdDel.TabIndex = 2237
        Me.rdDel.TabStop = True
        Me.rdDel.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(539, 114)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 2236
        Me.Label6.Text = "Delivery"
        '
        'btnSearchCurrency
        '
        Me.btnSearchCurrency.Image = CType(resources.GetObject("btnSearchCurrency.Image"), System.Drawing.Image)
        Me.btnSearchCurrency.Location = New System.Drawing.Point(740, 200)
        Me.btnSearchCurrency.Name = "btnSearchCurrency"
        Me.btnSearchCurrency.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCurrency.TabIndex = 2235
        Me.btnSearchCurrency.UseVisualStyleBackColor = True
        '
        'txtCurrency_Code
        '
        Me.txtCurrency_Code.Location = New System.Drawing.Point(678, 200)
        Me.txtCurrency_Code.MaxLength = 3
        Me.txtCurrency_Code.Name = "txtCurrency_Code"
        Me.txtCurrency_Code.ReadOnly = True
        Me.txtCurrency_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtCurrency_Code.TabIndex = 2234
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(539, 207)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 2233
        Me.Label8.Text = "Currency"
        '
        'txtCDNo
        '
        Me.txtCDNo.Location = New System.Drawing.Point(129, 17)
        Me.txtCDNo.MaxLength = 40
        Me.txtCDNo.Name = "txtCDNo"
        Me.txtCDNo.Size = New System.Drawing.Size(248, 20)
        Me.txtCDNo.TabIndex = 2232
        '
        'lblSupplierName
        '
        Me.lblSupplierName.AutoSize = True
        Me.lblSupplierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.Location = New System.Drawing.Point(221, 137)
        Me.lblSupplierName.Name = "lblSupplierName"
        Me.lblSupplierName.Size = New System.Drawing.Size(89, 13)
        Me.lblSupplierName.TabIndex = 2231
        Me.lblSupplierName.Text = "Supplier Name"
        '
        'btnSearchSupplier
        '
        Me.btnSearchSupplier.Image = CType(resources.GetObject("btnSearchSupplier.Image"), System.Drawing.Image)
        Me.btnSearchSupplier.Location = New System.Drawing.Point(191, 133)
        Me.btnSearchSupplier.Name = "btnSearchSupplier"
        Me.btnSearchSupplier.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchSupplier.TabIndex = 2230
        Me.btnSearchSupplier.UseVisualStyleBackColor = True
        '
        'txtSupplier_Code
        '
        Me.txtSupplier_Code.Location = New System.Drawing.Point(129, 134)
        Me.txtSupplier_Code.MaxLength = 5
        Me.txtSupplier_Code.Name = "txtSupplier_Code"
        Me.txtSupplier_Code.ReadOnly = True
        Me.txtSupplier_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtSupplier_Code.TabIndex = 2229
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(22, 140)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(45, 13)
        Me.Label26.TabIndex = 2228
        Me.Label26.Text = "Supplier"
        '
        'lblCompany_Name
        '
        Me.lblCompany_Name.AutoSize = True
        Me.lblCompany_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany_Name.Location = New System.Drawing.Point(221, 113)
        Me.lblCompany_Name.Name = "lblCompany_Name"
        Me.lblCompany_Name.Size = New System.Drawing.Size(58, 13)
        Me.lblCompany_Name.TabIndex = 2227
        Me.lblCompany_Name.Text = "Company"
        '
        'txtCompany_Code
        '
        Me.txtCompany_Code.Location = New System.Drawing.Point(129, 108)
        Me.txtCompany_Code.MaxLength = 5
        Me.txtCompany_Code.Name = "txtCompany_Code"
        Me.txtCompany_Code.ReadOnly = True
        Me.txtCompany_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtCompany_Code.TabIndex = 2226
        '
        'dtPer2
        '
        Me.dtPer2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPer2.Location = New System.Drawing.Point(237, 69)
        Me.dtPer2.Name = "dtPer2"
        Me.dtPer2.Size = New System.Drawing.Size(86, 20)
        Me.dtPer2.TabIndex = 2225
        Me.dtPer2.Value = New Date(2009, 2, 24, 0, 0, 0, 0)
        '
        'dtPer1
        '
        Me.dtPer1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtPer1.Location = New System.Drawing.Point(129, 69)
        Me.dtPer1.Name = "dtPer1"
        Me.dtPer1.Size = New System.Drawing.Size(84, 20)
        Me.dtPer1.TabIndex = 2224
        Me.dtPer1.Value = New Date(2009, 2, 24, 0, 0, 0, 0)
        '
        'dtCT
        '
        Me.dtCT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtCT.Location = New System.Drawing.Point(129, 43)
        Me.dtCT.Name = "dtCT"
        Me.dtCT.Size = New System.Drawing.Size(84, 20)
        Me.dtCT.TabIndex = 2223
        Me.dtCT.Value = New Date(2009, 2, 24, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 13)
        Me.Label5.TabIndex = 2182
        Me.Label5.Text = "Contract Periode"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 2180
        Me.Label4.Text = "Contract No"
        '
        'grid1
        '
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.grid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grid1.DefaultCellStyle = DataGridViewCellStyle20
        Me.grid1.Location = New System.Drawing.Point(12, 333)
        Me.grid1.Name = "grid1"
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid1.RowHeadersDefaultCellStyle = DataGridViewCellStyle21
        Me.grid1.Size = New System.Drawing.Size(982, 128)
        Me.grid1.TabIndex = 10
        '
        'grid2
        '
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid2.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle22
        Me.grid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grid2.DefaultCellStyle = DataGridViewCellStyle23
        Me.grid2.Location = New System.Drawing.Point(12, 495)
        Me.grid2.Name = "grid2"
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid2.RowHeadersDefaultCellStyle = DataGridViewCellStyle24
        Me.grid2.Size = New System.Drawing.Size(981, 154)
        Me.grid2.TabIndex = 11
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(12, 315)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 13)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Payment Term"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(11, 477)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Items"
        '
        'crtcode
        '
        Me.crtcode.Location = New System.Drawing.Point(633, 40)
        Me.crtcode.MaxLength = 1
        Me.crtcode.Name = "crtcode"
        Me.crtcode.ReadOnly = True
        Me.crtcode.Size = New System.Drawing.Size(41, 20)
        Me.crtcode.TabIndex = 2251
        Me.crtcode.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Enabled = False
        Me.crtdt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.crtdt.Location = New System.Drawing.Point(678, 16)
        Me.crtdt.Name = "crtdt"
        Me.crtdt.Size = New System.Drawing.Size(86, 20)
        Me.crtdt.TabIndex = 2252
        Me.crtdt.Value = New Date(2009, 2, 24, 0, 0, 0, 0)
        '
        'FrmCD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 660)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.grid2)
        Me.Controls.Add(Me.grid1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmCD"
        Me.ShowIcon = False
        Me.Text = "Contract Document"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grid2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents crtt As System.Windows.Forms.Label
    Friend WithEvents crt As System.Windows.Forms.TextBox
    Friend WithEvents crtdttext As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents remark As System.Windows.Forms.RichTextBox
    Friend WithEvents btnSearchCompany As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnReject As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtPer2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtPer1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtCT As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCompany_Code As System.Windows.Forms.TextBox
    Friend WithEvents lblCompany_Name As System.Windows.Forms.Label
    Friend WithEvents lblSupplierName As System.Windows.Forms.Label
    Friend WithEvents btnSearchSupplier As System.Windows.Forms.Button
    Friend WithEvents txtSupplier_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCDNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents dtDel As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdMM As System.Windows.Forms.RadioButton
    Friend WithEvents rdDD As System.Windows.Forms.RadioButton
    Friend WithEvents rdDel As System.Windows.Forms.RadioButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSearchCurrency As System.Windows.Forms.Button
    Friend WithEvents txtCurrency_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents total As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrency_Name As System.Windows.Forms.Label
    Friend WithEvents grid1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnListCD As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents grid2 As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnMat As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtMM As System.Windows.Forms.TextBox
    Friend WithEvents txtDD As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents crtcode As System.Windows.Forms.TextBox
    Friend WithEvents crtdt As System.Windows.Forms.DateTimePicker
End Class
