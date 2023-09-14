<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVP
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmVP))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrintLamp = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.CRTCODE = New System.Windows.Forms.TextBox
        Me.currname = New System.Windows.Forms.Label
        Me.btnSearchCurrency = New System.Windows.Forms.Button
        Me.CurrCode = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.statustxt = New System.Windows.Forms.Label
        Me.status = New System.Windows.Forms.TextBox
        Me.dt3 = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.fincode = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label8 = New System.Windows.Forms.Label
        Me.finby = New System.Windows.Forms.TextBox
        Me.docno = New System.Windows.Forms.TextBox
        Me.expcode = New System.Windows.Forms.TextBox
        Me.dt2 = New System.Windows.Forms.DateTimePicker
        Me.Label11 = New System.Windows.Forms.Label
        Me.appcode = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.appby = New System.Windows.Forms.TextBox
        Me.crtdt = New System.Windows.Forms.TextBox
        Me.crtt = New System.Windows.Forms.Label
        Me.crt = New System.Windows.Forms.TextBox
        Me.crtdttext = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.remark = New System.Windows.Forms.RichTextBox
        Me.total = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.grid = New System.Windows.Forms.DataGridView
        Me.accno = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.accname = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.bankname = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.te = New System.Windows.Forms.Label
        Me.expname = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.dt1 = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnBank = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint, Me.ToolStripSeparator1, Me.btnPrintLamp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(691, 25)
        Me.ToolStrip1.TabIndex = 6
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
        'btnSave
        '
        Me.btnSave.AutoSize = False
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
        Me.btnReject.Image = Global.poim.My.Resources.Resources.delete
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
        Me.btnPrint.Size = New System.Drawing.Size(136, 22)
        Me.btnPrint.Text = "Print Payment Voucher"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrintLamp
        '
        Me.btnPrintLamp.Image = CType(resources.GetObject("btnPrintLamp.Image"), System.Drawing.Image)
        Me.btnPrintLamp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintLamp.Name = "btnPrintLamp"
        Me.btnPrintLamp.Size = New System.Drawing.Size(110, 22)
        Me.btnPrintLamp.Text = "Print Lampiran PV"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnBank)
        Me.GroupBox1.Controls.Add(Me.CRTCODE)
        Me.GroupBox1.Controls.Add(Me.currname)
        Me.GroupBox1.Controls.Add(Me.btnSearchCurrency)
        Me.GroupBox1.Controls.Add(Me.CurrCode)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.statustxt)
        Me.GroupBox1.Controls.Add(Me.status)
        Me.GroupBox1.Controls.Add(Me.dt3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.fincode)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.finby)
        Me.GroupBox1.Controls.Add(Me.docno)
        Me.GroupBox1.Controls.Add(Me.expcode)
        Me.GroupBox1.Controls.Add(Me.dt2)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.appcode)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.appby)
        Me.GroupBox1.Controls.Add(Me.crtdt)
        Me.GroupBox1.Controls.Add(Me.crtt)
        Me.GroupBox1.Controls.Add(Me.crt)
        Me.GroupBox1.Controls.Add(Me.crtdttext)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.remark)
        Me.GroupBox1.Controls.Add(Me.total)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.grid)
        Me.GroupBox1.Controls.Add(Me.accno)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.accname)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.bankname)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.te)
        Me.GroupBox1.Controls.Add(Me.expname)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dt1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(690, 538)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'CRTCODE
        '
        Me.CRTCODE.Location = New System.Drawing.Point(278, 450)
        Me.CRTCODE.MaxLength = 1
        Me.CRTCODE.Name = "CRTCODE"
        Me.CRTCODE.ReadOnly = True
        Me.CRTCODE.Size = New System.Drawing.Size(41, 20)
        Me.CRTCODE.TabIndex = 2230
        Me.CRTCODE.Visible = False
        '
        'currname
        '
        Me.currname.AutoSize = True
        Me.currname.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currname.Location = New System.Drawing.Point(195, 367)
        Me.currname.Name = "currname"
        Me.currname.Size = New System.Drawing.Size(57, 13)
        Me.currname.TabIndex = 2229
        Me.currname.Text = "Currency"
        '
        'btnSearchCurrency
        '
        Me.btnSearchCurrency.Image = CType(resources.GetObject("btnSearchCurrency.Image"), System.Drawing.Image)
        Me.btnSearchCurrency.Location = New System.Drawing.Point(167, 363)
        Me.btnSearchCurrency.Name = "btnSearchCurrency"
        Me.btnSearchCurrency.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCurrency.TabIndex = 11
        Me.btnSearchCurrency.UseVisualStyleBackColor = True
        '
        'CurrCode
        '
        Me.CurrCode.Location = New System.Drawing.Point(103, 364)
        Me.CurrCode.MaxLength = 1
        Me.CurrCode.Name = "CurrCode"
        Me.CurrCode.ReadOnly = True
        Me.CurrCode.Size = New System.Drawing.Size(58, 20)
        Me.CurrCode.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(14, 367)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 2228
        Me.Label9.Text = "Currency"
        '
        'statustxt
        '
        Me.statustxt.AutoSize = True
        Me.statustxt.Location = New System.Drawing.Point(543, 22)
        Me.statustxt.Name = "statustxt"
        Me.statustxt.Size = New System.Drawing.Size(37, 13)
        Me.statustxt.TabIndex = 2227
        Me.statustxt.Text = "Status"
        '
        'status
        '
        Me.status.Location = New System.Drawing.Point(599, 19)
        Me.status.MaxLength = 5
        Me.status.Name = "status"
        Me.status.ReadOnly = True
        Me.status.Size = New System.Drawing.Size(56, 20)
        Me.status.TabIndex = 2226
        Me.status.Text = "Open"
        '
        'dt3
        '
        Me.dt3.Checked = False
        Me.dt3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt3.Location = New System.Drawing.Point(574, 498)
        Me.dt3.Name = "dt3"
        Me.dt3.ShowCheckBox = True
        Me.dt3.Size = New System.Drawing.Size(96, 20)
        Me.dt3.TabIndex = 22
        Me.dt3.Value = New Date(2008, 12, 15, 0, 0, 0, 0)
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(489, 502)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(75, 13)
        Me.Label7.TabIndex = 2225
        Me.Label7.Text = "Fin. App. Date"
        '
        'fincode
        '
        Me.fincode.Location = New System.Drawing.Point(278, 499)
        Me.fincode.MaxLength = 1
        Me.fincode.Name = "fincode"
        Me.fincode.ReadOnly = True
        Me.fincode.Size = New System.Drawing.Size(41, 20)
        Me.fincode.TabIndex = 21
        Me.fincode.Visible = False
        '
        'Button2
        '
        Me.Button2.Image = Global.poim.My.Resources.Resources.search
        Me.Button2.Location = New System.Drawing.Point(250, 499)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 18)
        Me.Button2.TabIndex = 20
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 502)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(82, 13)
        Me.Label8.TabIndex = 2224
        Me.Label8.Text = "Finance App By"
        '
        'finby
        '
        Me.finby.Location = New System.Drawing.Point(103, 498)
        Me.finby.MaxLength = 5
        Me.finby.Name = "finby"
        Me.finby.ReadOnly = True
        Me.finby.Size = New System.Drawing.Size(139, 20)
        Me.finby.TabIndex = 19
        '
        'docno
        '
        Me.docno.Location = New System.Drawing.Point(103, 19)
        Me.docno.MaxLength = 40
        Me.docno.Name = "docno"
        Me.docno.Size = New System.Drawing.Size(252, 20)
        Me.docno.TabIndex = 0
        '
        'expcode
        '
        Me.expcode.Location = New System.Drawing.Point(390, 81)
        Me.expcode.MaxLength = 1
        Me.expcode.Name = "expcode"
        Me.expcode.ReadOnly = True
        Me.expcode.Size = New System.Drawing.Size(41, 20)
        Me.expcode.TabIndex = 2219
        Me.expcode.Visible = False
        '
        'dt2
        '
        Me.dt2.Checked = False
        Me.dt2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt2.Location = New System.Drawing.Point(574, 472)
        Me.dt2.Name = "dt2"
        Me.dt2.ShowCheckBox = True
        Me.dt2.Size = New System.Drawing.Size(96, 20)
        Me.dt2.TabIndex = 18
        Me.dt2.Value = New Date(2008, 12, 15, 0, 0, 0, 0)
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(489, 476)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(79, 13)
        Me.Label11.TabIndex = 2218
        Me.Label11.Text = "Approved Date"
        '
        'appcode
        '
        Me.appcode.Location = New System.Drawing.Point(278, 473)
        Me.appcode.MaxLength = 1
        Me.appcode.Name = "appcode"
        Me.appcode.ReadOnly = True
        Me.appcode.Size = New System.Drawing.Size(41, 20)
        Me.appcode.TabIndex = 17
        Me.appcode.Visible = False
        '
        'Button1
        '
        Me.Button1.Image = Global.poim.My.Resources.Resources.search
        Me.Button1.Location = New System.Drawing.Point(250, 473)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 18)
        Me.Button1.TabIndex = 16
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(14, 476)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2215
        Me.Label12.Text = "Approved By"
        '
        'appby
        '
        Me.appby.Location = New System.Drawing.Point(103, 472)
        Me.appby.MaxLength = 5
        Me.appby.Name = "appby"
        Me.appby.ReadOnly = True
        Me.appby.Size = New System.Drawing.Size(139, 20)
        Me.appby.TabIndex = 15
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(574, 450)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(69, 20)
        Me.crtdt.TabIndex = 14
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(14, 451)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2212
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(103, 450)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 13
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(489, 453)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2211
        Me.crtdttext.Text = "Created Date"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(13, 389)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 2208
        Me.Label10.Text = "Remark"
        '
        'remark
        '
        Me.remark.Location = New System.Drawing.Point(103, 389)
        Me.remark.Name = "remark"
        Me.remark.Size = New System.Drawing.Size(575, 58)
        Me.remark.TabIndex = 12
        Me.remark.Text = ""
        '
        'total
        '
        Me.total.Location = New System.Drawing.Point(556, 319)
        Me.total.MaxLength = 5
        Me.total.Name = "total"
        Me.total.ReadOnly = True
        Me.total.Size = New System.Drawing.Size(99, 20)
        Me.total.TabIndex = 9
        Me.total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(495, 322)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 2202
        Me.Label6.Text = "Total"
        '
        'grid
        '
        Me.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grid.Location = New System.Drawing.Point(16, 189)
        Me.grid.Name = "grid"
        Me.grid.Size = New System.Drawing.Size(662, 130)
        Me.grid.TabIndex = 8
        '
        'accno
        '
        Me.accno.Location = New System.Drawing.Point(103, 147)
        Me.accno.MaxLength = 5
        Me.accno.Name = "accno"
        Me.accno.ReadOnly = True
        Me.accno.Size = New System.Drawing.Size(252, 20)
        Me.accno.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 150)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 2199
        Me.Label5.Text = "Account No."
        '
        'accname
        '
        Me.accname.Location = New System.Drawing.Point(103, 125)
        Me.accname.MaxLength = 5
        Me.accname.Name = "accname"
        Me.accname.ReadOnly = True
        Me.accname.Size = New System.Drawing.Size(252, 20)
        Me.accname.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 2197
        Me.Label4.Text = "Account Name"
        '
        'bankname
        '
        Me.bankname.Location = New System.Drawing.Point(103, 103)
        Me.bankname.MaxLength = 5
        Me.bankname.Name = "bankname"
        Me.bankname.ReadOnly = True
        Me.bankname.Size = New System.Drawing.Size(252, 20)
        Me.bankname.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2195
        Me.Label1.Text = "Bank Name"
        '
        'Button3
        '
        Me.Button3.Image = Global.poim.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(356, 81)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 4
        Me.Button3.UseVisualStyleBackColor = True
        '
        'te
        '
        Me.te.AutoSize = True
        Me.te.Location = New System.Drawing.Point(13, 83)
        Me.te.Name = "te"
        Me.te.Size = New System.Drawing.Size(56, 13)
        Me.te.TabIndex = 2193
        Me.te.Text = "Expedition"
        '
        'expname
        '
        Me.expname.Location = New System.Drawing.Point(103, 81)
        Me.expname.MaxLength = 5
        Me.expname.Name = "expname"
        Me.expname.ReadOnly = True
        Me.expname.Size = New System.Drawing.Size(252, 20)
        Me.expname.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 2190
        Me.Label3.Text = "Doc. No."
        '
        'dt1
        '
        Me.dt1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt1.Location = New System.Drawing.Point(103, 41)
        Me.dt1.Name = "dt1"
        Me.dt1.Size = New System.Drawing.Size(91, 20)
        Me.dt1.TabIndex = 1
        Me.dt1.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Printed Date"
        '
        'btnBank
        '
        Me.btnBank.Image = Global.poim.My.Resources.Resources.search
        Me.btnBank.Location = New System.Drawing.Point(356, 103)
        Me.btnBank.Name = "btnBank"
        Me.btnBank.Size = New System.Drawing.Size(22, 18)
        Me.btnBank.TabIndex = 2231
        Me.btnBank.UseVisualStyleBackColor = True
        '
        'FrmVP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 564)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.HelpButton = True
        Me.Name = "FrmVP"
        Me.ShowInTaskbar = False
        Me.Text = "VP"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnReject As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dt1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents accno As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents accname As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents bankname As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents te As System.Windows.Forms.Label
    Friend WithEvents expname As System.Windows.Forms.TextBox
    Friend WithEvents total As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grid As System.Windows.Forms.DataGridView
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents remark As System.Windows.Forms.RichTextBox
    Friend WithEvents crtdt As System.Windows.Forms.TextBox
    Friend WithEvents crtt As System.Windows.Forms.Label
    Friend WithEvents crt As System.Windows.Forms.TextBox
    Friend WithEvents crtdttext As System.Windows.Forms.Label
    Friend WithEvents appcode As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents appby As System.Windows.Forms.TextBox
    Friend WithEvents dt2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents expcode As System.Windows.Forms.TextBox
    Friend WithEvents docno As System.Windows.Forms.TextBox
    Friend WithEvents dt3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents fincode As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents finby As System.Windows.Forms.TextBox
    Friend WithEvents statustxt As System.Windows.Forms.Label
    Friend WithEvents status As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchCurrency As System.Windows.Forms.Button
    Friend WithEvents CurrCode As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents currname As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrintLamp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CRTCODE As System.Windows.Forms.TextBox
    Friend WithEvents btnBank As System.Windows.Forms.Button
End Class
