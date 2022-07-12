<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRMRILQuota
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRMRILQuota))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.btnNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.btnAttachment = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblSupplier_Name = New System.Windows.Forms.Label
        Me.btnSearchSupplier = New System.Windows.Forms.Button
        Me.txtSupplier_Code = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblloadport = New System.Windows.Forms.Label
        Me.btnSearchLoadPort = New System.Windows.Forms.Button
        Me.txtLoadPort_Code = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.btnListRIL = New System.Windows.Forms.Button
        Me.Label23 = New System.Windows.Forms.Label
        Me.lblPort_Name = New System.Windows.Forms.Label
        Me.btnSearchPort = New System.Windows.Forms.Button
        Me.txtPort_Code = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.lblCompany_Name = New System.Windows.Forms.Label
        Me.btnSearchCompany = New System.Windows.Forms.Button
        Me.txtCompany_Code = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSearchOri = New System.Windows.Forms.Button
        Me.DGVDetail = New System.Windows.Forms.DataGridView
        Me.TextBox = New System.Windows.Forms.TextBox
        Me.btnSearchMat = New System.Windows.Forms.Button
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbDG = New System.Windows.Forms.ComboBox
        Me.crtdate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.crtby = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.crtname = New System.Windows.Forms.TextBox
        Me.txtRILno = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.appdate = New System.Windows.Forms.DateTimePicker
        Me.appcode = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.te = New System.Windows.Forms.Label
        Me.appname = New System.Windows.Forms.TextBox
        Me.tex = New System.Windows.Forms.Label
        Me.tgl = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtbea0 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtLeadtime = New System.Windows.Forms.TextBox
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtDeptanNo = New System.Windows.Forms.TextBox
        Me.dtSubmited = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtIssued = New System.Windows.Forms.DateTimePicker
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtTolerable = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGVDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.txtbea0.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.btnNew, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint, Me.ToolStripSeparator5, Me.ToolStripSeparator6, Me.btnAttachment})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(945, 25)
        Me.ToolStrip1.TabIndex = 7
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.AutoSize = False
        Me.btnClose.Image = Global.POIM.My.Resources.Resources.CLOSE
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(60, 22)
        Me.btnClose.Text = "Close"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.POIM.My.Resources.Resources.NewDocumentHS
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(48, 22)
        Me.btnNew.Text = "New"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Image = Global.POIM.My.Resources.Resources.SaveHL
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
        Me.btnReject.Image = Global.POIM.My.Resources.Resources.delete
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(60, 22)
        Me.btnReject.Text = "Reject"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(49, 22)
        Me.btnPrint.Text = "Print"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'btnAttachment
        '
        Me.btnAttachment.Image = CType(resources.GetObject("btnAttachment.Image"), System.Drawing.Image)
        Me.btnAttachment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAttachment.Name = "btnAttachment"
        Me.btnAttachment.Size = New System.Drawing.Size(83, 22)
        Me.btnAttachment.Text = "Attachment"
        Me.btnAttachment.ToolTipText = "Attachment"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblSupplier_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchSupplier)
        Me.GroupBox1.Controls.Add(Me.txtSupplier_Code)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.lblloadport)
        Me.GroupBox1.Controls.Add(Me.btnSearchLoadPort)
        Me.GroupBox1.Controls.Add(Me.txtLoadPort_Code)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.btnListRIL)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.lblPort_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchPort)
        Me.GroupBox1.Controls.Add(Me.txtPort_Code)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.lblCompany_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchCompany)
        Me.GroupBox1.Controls.Add(Me.txtCompany_Code)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSearchOri)
        Me.GroupBox1.Controls.Add(Me.DGVDetail)
        Me.GroupBox1.Controls.Add(Me.TextBox)
        Me.GroupBox1.Controls.Add(Me.btnSearchMat)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbDG)
        Me.GroupBox1.Controls.Add(Me.crtdate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.crtby)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.crtname)
        Me.GroupBox1.Controls.Add(Me.txtRILno)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.appdate)
        Me.GroupBox1.Controls.Add(Me.appcode)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.te)
        Me.GroupBox1.Controls.Add(Me.appname)
        Me.GroupBox1.Controls.Add(Me.tex)
        Me.GroupBox1.Controls.Add(Me.tgl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtbea0)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(910, 511)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'lblSupplier_Name
        '
        Me.lblSupplier_Name.AutoSize = True
        Me.lblSupplier_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplier_Name.Location = New System.Drawing.Point(215, 93)
        Me.lblSupplier_Name.Name = "lblSupplier_Name"
        Me.lblSupplier_Name.Size = New System.Drawing.Size(53, 13)
        Me.lblSupplier_Name.TabIndex = 2312
        Me.lblSupplier_Name.Text = "Supplier"
        '
        'btnSearchSupplier
        '
        Me.btnSearchSupplier.Image = CType(resources.GetObject("btnSearchSupplier.Image"), System.Drawing.Image)
        Me.btnSearchSupplier.Location = New System.Drawing.Point(183, 88)
        Me.btnSearchSupplier.Name = "btnSearchSupplier"
        Me.btnSearchSupplier.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchSupplier.TabIndex = 2309
        Me.btnSearchSupplier.UseVisualStyleBackColor = True
        '
        'txtSupplier_Code
        '
        Me.txtSupplier_Code.Location = New System.Drawing.Point(123, 89)
        Me.txtSupplier_Code.MaxLength = 5
        Me.txtSupplier_Code.Name = "txtSupplier_Code"
        Me.txtSupplier_Code.ReadOnly = True
        Me.txtSupplier_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtSupplier_Code.TabIndex = 2311
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(15, 93)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 13)
        Me.Label18.TabIndex = 2310
        Me.Label18.Text = "Supplier"
        '
        'lblloadport
        '
        Me.lblloadport.AutoSize = True
        Me.lblloadport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblloadport.Location = New System.Drawing.Point(215, 140)
        Me.lblloadport.Name = "lblloadport"
        Me.lblloadport.Size = New System.Drawing.Size(62, 13)
        Me.lblloadport.TabIndex = 2308
        Me.lblloadport.Text = "Load Port"
        '
        'btnSearchLoadPort
        '
        Me.btnSearchLoadPort.Image = CType(resources.GetObject("btnSearchLoadPort.Image"), System.Drawing.Image)
        Me.btnSearchLoadPort.Location = New System.Drawing.Point(183, 135)
        Me.btnSearchLoadPort.Name = "btnSearchLoadPort"
        Me.btnSearchLoadPort.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchLoadPort.TabIndex = 2305
        Me.btnSearchLoadPort.UseVisualStyleBackColor = True
        '
        'txtLoadPort_Code
        '
        Me.txtLoadPort_Code.Location = New System.Drawing.Point(123, 136)
        Me.txtLoadPort_Code.MaxLength = 5
        Me.txtLoadPort_Code.Name = "txtLoadPort_Code"
        Me.txtLoadPort_Code.ReadOnly = True
        Me.txtLoadPort_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtLoadPort_Code.TabIndex = 2307
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(15, 140)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 13)
        Me.Label17.TabIndex = 2306
        Me.Label17.Text = "Load Port"
        '
        'btnListRIL
        '
        Me.btnListRIL.Image = CType(resources.GetObject("btnListRIL.Image"), System.Drawing.Image)
        Me.btnListRIL.Location = New System.Drawing.Point(354, 19)
        Me.btnListRIL.Name = "btnListRIL"
        Me.btnListRIL.Size = New System.Drawing.Size(22, 20)
        Me.btnListRIL.TabIndex = 2304
        Me.btnListRIL.UseVisualStyleBackColor = True
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(33, 18)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(14, 17)
        Me.Label23.TabIndex = 2303
        Me.Label23.Text = "*"
        '
        'lblPort_Name
        '
        Me.lblPort_Name.AutoSize = True
        Me.lblPort_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPort_Name.Location = New System.Drawing.Point(215, 116)
        Me.lblPort_Name.Name = "lblPort_Name"
        Me.lblPort_Name.Size = New System.Drawing.Size(30, 13)
        Me.lblPort_Name.TabIndex = 2302
        Me.lblPort_Name.Text = "Port"
        '
        'btnSearchPort
        '
        Me.btnSearchPort.Image = CType(resources.GetObject("btnSearchPort.Image"), System.Drawing.Image)
        Me.btnSearchPort.Location = New System.Drawing.Point(183, 111)
        Me.btnSearchPort.Name = "btnSearchPort"
        Me.btnSearchPort.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchPort.TabIndex = 2299
        Me.btnSearchPort.UseVisualStyleBackColor = True
        '
        'txtPort_Code
        '
        Me.txtPort_Code.Location = New System.Drawing.Point(123, 112)
        Me.txtPort_Code.MaxLength = 5
        Me.txtPort_Code.Name = "txtPort_Code"
        Me.txtPort_Code.ReadOnly = True
        Me.txtPort_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtPort_Code.TabIndex = 2301
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(14, 116)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(82, 13)
        Me.Label15.TabIndex = 2300
        Me.Label15.Text = "Destination Port"
        '
        'lblCompany_Name
        '
        Me.lblCompany_Name.AutoSize = True
        Me.lblCompany_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany_Name.Location = New System.Drawing.Point(215, 70)
        Me.lblCompany_Name.Name = "lblCompany_Name"
        Me.lblCompany_Name.Size = New System.Drawing.Size(58, 13)
        Me.lblCompany_Name.TabIndex = 2297
        Me.lblCompany_Name.Text = "Company"
        '
        'btnSearchCompany
        '
        Me.btnSearchCompany.Image = CType(resources.GetObject("btnSearchCompany.Image"), System.Drawing.Image)
        Me.btnSearchCompany.Location = New System.Drawing.Point(183, 65)
        Me.btnSearchCompany.Name = "btnSearchCompany"
        Me.btnSearchCompany.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCompany.TabIndex = 2295
        Me.btnSearchCompany.UseVisualStyleBackColor = True
        '
        'txtCompany_Code
        '
        Me.txtCompany_Code.Location = New System.Drawing.Point(123, 66)
        Me.txtCompany_Code.MaxLength = 5
        Me.txtCompany_Code.Name = "txtCompany_Code"
        Me.txtCompany_Code.ReadOnly = True
        Me.txtCompany_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtCompany_Code.TabIndex = 2294
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 2296
        Me.Label1.Text = "Company"
        '
        'btnSearchOri
        '
        Me.btnSearchOri.Image = CType(resources.GetObject("btnSearchOri.Image"), System.Drawing.Image)
        Me.btnSearchOri.Location = New System.Drawing.Point(671, 45)
        Me.btnSearchOri.Name = "btnSearchOri"
        Me.btnSearchOri.Size = New System.Drawing.Size(22, 18)
        Me.btnSearchOri.TabIndex = 2211
        Me.btnSearchOri.UseVisualStyleBackColor = True
        Me.btnSearchOri.Visible = False
        '
        'DGVDetail
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVDetail.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGVDetail.Location = New System.Drawing.Point(16, 343)
        Me.DGVDetail.Name = "DGVDetail"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVDetail.Size = New System.Drawing.Size(877, 162)
        Me.DGVDetail.TabIndex = 2293
        '
        'TextBox
        '
        Me.TextBox.Location = New System.Drawing.Point(602, 43)
        Me.TextBox.Name = "TextBox"
        Me.TextBox.Size = New System.Drawing.Size(46, 20)
        Me.TextBox.TabIndex = 2210
        Me.TextBox.Visible = False
        '
        'btnSearchMat
        '
        Me.btnSearchMat.Image = CType(resources.GetObject("btnSearchMat.Image"), System.Drawing.Image)
        Me.btnSearchMat.Location = New System.Drawing.Point(649, 45)
        Me.btnSearchMat.Name = "btnSearchMat"
        Me.btnSearchMat.Size = New System.Drawing.Size(22, 18)
        Me.btnSearchMat.TabIndex = 2209
        Me.btnSearchMat.UseVisualStyleBackColor = True
        Me.btnSearchMat.Visible = False
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(602, 19)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(91, 20)
        Me.Status.TabIndex = 2201
        Me.Status.Text = "Open"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(477, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 2202
        Me.Label7.Text = "Status"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(477, 137)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 13)
        Me.Label6.TabIndex = 2196
        Me.Label6.Text = "Document  Group"
        '
        'cbDG
        '
        Me.cbDG.FormattingEnabled = True
        Me.cbDG.Location = New System.Drawing.Point(602, 133)
        Me.cbDG.Name = "cbDG"
        Me.cbDG.Size = New System.Drawing.Size(217, 21)
        Me.cbDG.TabIndex = 2195
        '
        'crtdate
        '
        Me.crtdate.Enabled = False
        Me.crtdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.crtdate.Location = New System.Drawing.Point(602, 290)
        Me.crtdate.Name = "crtdate"
        Me.crtdate.Size = New System.Drawing.Size(100, 20)
        Me.crtdate.TabIndex = 8
        Me.crtdate.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(476, 292)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 2194
        Me.Label5.Text = "Created Date"
        '
        'crtby
        '
        Me.crtby.Location = New System.Drawing.Point(298, 290)
        Me.crtby.MaxLength = 1
        Me.crtby.Name = "crtby"
        Me.crtby.ReadOnly = True
        Me.crtby.Size = New System.Drawing.Size(41, 20)
        Me.crtby.TabIndex = 7
        Me.crtby.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 2192
        Me.Label4.Text = "Created By"
        '
        'crtname
        '
        Me.crtname.Location = New System.Drawing.Point(123, 290)
        Me.crtname.MaxLength = 5
        Me.crtname.Name = "crtname"
        Me.crtname.ReadOnly = True
        Me.crtname.Size = New System.Drawing.Size(139, 20)
        Me.crtname.TabIndex = 6
        '
        'txtRILno
        '
        Me.txtRILno.Location = New System.Drawing.Point(123, 19)
        Me.txtRILno.MaxLength = 40
        Me.txtRILno.Name = "txtRILno"
        Me.txtRILno.Size = New System.Drawing.Size(230, 20)
        Me.txtRILno.TabIndex = 0
        Me.txtRILno.Tag = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 13)
        Me.Label3.TabIndex = 2190
        Me.Label3.Text = "No."
        '
        'appdate
        '
        Me.appdate.Checked = False
        Me.appdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.appdate.Location = New System.Drawing.Point(602, 313)
        Me.appdate.Name = "appdate"
        Me.appdate.ShowCheckBox = True
        Me.appdate.Size = New System.Drawing.Size(100, 20)
        Me.appdate.TabIndex = 12
        Me.appdate.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'appcode
        '
        Me.appcode.Location = New System.Drawing.Point(298, 313)
        Me.appcode.MaxLength = 1
        Me.appcode.Name = "appcode"
        Me.appcode.ReadOnly = True
        Me.appcode.Size = New System.Drawing.Size(41, 20)
        Me.appcode.TabIndex = 11
        Me.appcode.Visible = False
        '
        'Button3
        '
        Me.Button3.Image = Global.POIM.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(270, 313)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 10
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'te
        '
        Me.te.AutoSize = True
        Me.te.Location = New System.Drawing.Point(13, 315)
        Me.te.Name = "te"
        Me.te.Size = New System.Drawing.Size(68, 13)
        Me.te.TabIndex = 2166
        Me.te.Text = "Approved By"
        '
        'appname
        '
        Me.appname.Location = New System.Drawing.Point(123, 313)
        Me.appname.MaxLength = 5
        Me.appname.Name = "appname"
        Me.appname.ReadOnly = True
        Me.appname.Size = New System.Drawing.Size(139, 20)
        Me.appname.TabIndex = 9
        '
        'tex
        '
        Me.tex.AutoSize = True
        Me.tex.Location = New System.Drawing.Point(476, 315)
        Me.tex.Name = "tex"
        Me.tex.Size = New System.Drawing.Size(79, 13)
        Me.tex.TabIndex = 2151
        Me.tex.Text = "Approved Date"
        '
        'tgl
        '
        Me.tgl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl.Location = New System.Drawing.Point(123, 42)
        Me.tgl.Name = "tgl"
        Me.tgl.Size = New System.Drawing.Size(91, 20)
        Me.tgl.TabIndex = 1
        Me.tgl.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Date"
        '
        'txtbea0
        '
        Me.txtbea0.Controls.Add(Me.Label19)
        Me.txtbea0.Controls.Add(Me.Label16)
        Me.txtbea0.Controls.Add(Me.txtTolerable)
        Me.txtbea0.Controls.Add(Me.Label14)
        Me.txtbea0.Controls.Add(Me.Label12)
        Me.txtbea0.Controls.Add(Me.Label13)
        Me.txtbea0.Controls.Add(Me.txtLeadtime)
        Me.txtbea0.Controls.Add(Me.txtRemark)
        Me.txtbea0.Controls.Add(Me.Label11)
        Me.txtbea0.Controls.Add(Me.txtDeptanNo)
        Me.txtbea0.Controls.Add(Me.dtSubmited)
        Me.txtbea0.Controls.Add(Me.Label8)
        Me.txtbea0.Controls.Add(Me.Label10)
        Me.txtbea0.Controls.Add(Me.Label9)
        Me.txtbea0.Controls.Add(Me.dtIssued)
        Me.txtbea0.Location = New System.Drawing.Point(16, 162)
        Me.txtbea0.Name = "txtbea0"
        Me.txtbea0.Size = New System.Drawing.Size(877, 122)
        Me.txtbea0.TabIndex = 2292
        Me.txtbea0.TabStop = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(145, 90)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(29, 13)
        Me.Label14.TabIndex = 2208
        Me.Label14.Text = "days"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(149, 155)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 13)
        Me.Label12.TabIndex = 2207
        Me.Label12.Text = "days"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(461, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 13)
        Me.Label13.TabIndex = 2199
        Me.Label13.Text = "Remark"
        '
        'txtLeadtime
        '
        Me.txtLeadtime.Location = New System.Drawing.Point(107, 88)
        Me.txtLeadtime.MaxLength = 40
        Me.txtLeadtime.Name = "txtLeadtime"
        Me.txtLeadtime.ReadOnly = True
        Me.txtLeadtime.Size = New System.Drawing.Size(32, 20)
        Me.txtLeadtime.TabIndex = 2205
        Me.txtLeadtime.Tag = ""
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(586, 18)
        Me.txtRemark.MaxLength = 2147483647
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(215, 67)
        Me.txtRemark.TabIndex = 2198
        Me.txtRemark.Tag = ""
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(13, 90)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 13)
        Me.Label11.TabIndex = 2206
        Me.Label11.Text = "Leadtime"
        '
        'txtDeptanNo
        '
        Me.txtDeptanNo.Location = New System.Drawing.Point(107, 19)
        Me.txtDeptanNo.MaxLength = 40
        Me.txtDeptanNo.Name = "txtDeptanNo"
        Me.txtDeptanNo.Size = New System.Drawing.Size(230, 20)
        Me.txtDeptanNo.TabIndex = 2197
        Me.txtDeptanNo.Tag = ""
        '
        'dtSubmited
        '
        Me.dtSubmited.Checked = False
        Me.dtSubmited.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtSubmited.Location = New System.Drawing.Point(107, 42)
        Me.dtSubmited.Name = "dtSubmited"
        Me.dtSubmited.ShowCheckBox = True
        Me.dtSubmited.Size = New System.Drawing.Size(100, 20)
        Me.dtSubmited.TabIndex = 2203
        Me.dtSubmited.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(62, 13)
        Me.Label8.TabIndex = 2198
        Me.Label8.Text = "Deptan No."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 13)
        Me.Label10.TabIndex = 2204
        Me.Label10.Text = "Submited Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(13, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 2200
        Me.Label9.Text = "Issued Date"
        '
        'dtIssued
        '
        Me.dtIssued.Checked = False
        Me.dtIssued.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtIssued.Location = New System.Drawing.Point(107, 65)
        Me.dtIssued.Name = "dtIssued"
        Me.dtIssued.ShowCheckBox = True
        Me.dtIssued.Size = New System.Drawing.Size(100, 20)
        Me.dtIssued.TabIndex = 2199
        Me.dtIssued.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(656, 90)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(16, 13)
        Me.Label16.TabIndex = 2210
        Me.Label16.Text = "%"
        '
        'txtTolerable
        '
        Me.txtTolerable.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTolerable.Location = New System.Drawing.Point(586, 88)
        Me.txtTolerable.MaxLength = 5
        Me.txtTolerable.Name = "txtTolerable"
        Me.txtTolerable.Size = New System.Drawing.Size(69, 20)
        Me.txtTolerable.TabIndex = 2209
        Me.txtTolerable.Text = "0"
        Me.txtTolerable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(461, 90)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(51, 13)
        Me.Label19.TabIndex = 2211
        Me.Label19.Text = "Tolerable"
        '
        'FRMRILQuota
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(945, 551)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "FRMRILQuota"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Request Import Lisense - Quota"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DGVDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.txtbea0.ResumeLayout(False)
        Me.txtbea0.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnReject As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbDG As System.Windows.Forms.ComboBox
    Friend WithEvents crtdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents crtby As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents crtname As System.Windows.Forms.TextBox
    Friend WithEvents txtRILno As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents appdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents appcode As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents te As System.Windows.Forms.Label
    Friend WithEvents appname As System.Windows.Forms.TextBox
    Friend WithEvents tex As System.Windows.Forms.Label
    Friend WithEvents tgl As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtbea0 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtLeadtime As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDeptanNo As System.Windows.Forms.TextBox
    Friend WithEvents dtSubmited As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtIssued As System.Windows.Forms.DateTimePicker
    Friend WithEvents DGVDetail As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchOri As System.Windows.Forms.Button
    Friend WithEvents TextBox As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchMat As System.Windows.Forms.Button
    Friend WithEvents lblCompany_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchCompany As System.Windows.Forms.Button
    Friend WithEvents txtCompany_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblPort_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchPort As System.Windows.Forms.Button
    Friend WithEvents txtPort_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnListRIL As System.Windows.Forms.Button
    Friend WithEvents btnAttachment As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblloadport As System.Windows.Forms.Label
    Friend WithEvents btnSearchLoadPort As System.Windows.Forms.Button
    Friend WithEvents txtLoadPort_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblSupplier_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchSupplier As System.Windows.Forms.Button
    Friend WithEvents txtSupplier_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtTolerable As System.Windows.Forms.TextBox
End Class
