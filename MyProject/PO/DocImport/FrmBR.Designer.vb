<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBR))
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.CTFun = New System.Windows.Forms.TextBox
        Me.CTApp = New System.Windows.Forms.TextBox
        Me.charge = New System.Windows.Forms.MaskedTextBox
        Me.commision = New System.Windows.Forms.MaskedTextBox
        Me.deposit = New System.Windows.Forms.MaskedTextBox
        Me.txtrate = New System.Windows.Forms.MaskedTextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.financeappby = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.approvedby = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.acno = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.tot = New System.Windows.Forms.MaskedTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TotalInvoice = New System.Windows.Forms.MaskedTextBox
        Me.Curr_Name = New System.Windows.Forms.Label
        Me.curr = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.bank_name = New System.Windows.Forms.Label
        Me.remark = New System.Windows.Forms.RichTextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.tgl = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.bank = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.ChkFinal = New System.Windows.Forms.CheckBox
        Me.CutMargin = New System.Windows.Forms.TextBox
        Me.TotalAmount = New System.Windows.Forms.MaskedTextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.tgl3 = New System.Windows.Forms.DateTimePicker
        Me.tgl2 = New System.Windows.Forms.DateTimePicker
        Me.CTCrt = New System.Windows.Forms.TextBox
        Me.crtdt = New System.Windows.Forms.TextBox
        Me.crtt = New System.Windows.Forms.Label
        Me.crt = New System.Windows.Forms.TextBox
        Me.crtdttext = New System.Windows.Forms.Label
        Me.cbLC = New System.Windows.Forms.ComboBox
        Me.BLno = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(619, 17)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(84, 20)
        Me.Status.TabIndex = 8
        Me.Status.Text = "Open"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(480, 20)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2173
        Me.Label15.Text = "Status"
        '
        'CTFun
        '
        Me.CTFun.Location = New System.Drawing.Point(278, 270)
        Me.CTFun.MaxLength = 1
        Me.CTFun.Name = "CTFun"
        Me.CTFun.ReadOnly = True
        Me.CTFun.Size = New System.Drawing.Size(41, 20)
        Me.CTFun.TabIndex = 2171
        Me.CTFun.Visible = False
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(278, 250)
        Me.CTApp.MaxLength = 1
        Me.CTApp.Name = "CTApp"
        Me.CTApp.ReadOnly = True
        Me.CTApp.Size = New System.Drawing.Size(41, 20)
        Me.CTApp.TabIndex = 2170
        Me.CTApp.Visible = False
        '
        'charge
        '
        Me.charge.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.charge.Location = New System.Drawing.Point(619, 174)
        Me.charge.Name = "charge"
        Me.charge.ReadOnly = True
        Me.charge.Size = New System.Drawing.Size(91, 20)
        Me.charge.TabIndex = 12
        Me.charge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'commision
        '
        Me.commision.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.commision.Location = New System.Drawing.Point(619, 152)
        Me.commision.Name = "commision"
        Me.commision.ReadOnly = True
        Me.commision.Size = New System.Drawing.Size(91, 20)
        Me.commision.TabIndex = 11
        Me.commision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'deposit
        '
        Me.deposit.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.deposit.Location = New System.Drawing.Point(619, 130)
        Me.deposit.Name = "deposit"
        Me.deposit.ReadOnly = True
        Me.deposit.Size = New System.Drawing.Size(91, 20)
        Me.deposit.TabIndex = 10
        Me.deposit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtrate
        '
        Me.txtrate.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.txtrate.Location = New System.Drawing.Point(372, 195)
        Me.txtrate.Name = "txtrate"
        Me.txtrate.Size = New System.Drawing.Size(91, 20)
        Me.txtrate.TabIndex = 5
        Me.txtrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button4
        '
        Me.Button4.Image = Global.POIM.My.Resources.Resources.search
        Me.Button4.Location = New System.Drawing.Point(250, 273)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 18)
        Me.Button4.TabIndex = 2168
        Me.Button4.TabStop = False
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(20, 271)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 2169
        Me.Label14.Text = "Finance App By"
        '
        'financeappby
        '
        Me.financeappby.Location = New System.Drawing.Point(103, 271)
        Me.financeappby.MaxLength = 5
        Me.financeappby.Name = "financeappby"
        Me.financeappby.ReadOnly = True
        Me.financeappby.Size = New System.Drawing.Size(139, 20)
        Me.financeappby.TabIndex = 7
        '
        'Button3
        '
        Me.Button3.Image = Global.POIM.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(250, 250)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 2165
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(20, 249)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2166
        Me.Label12.Text = "Approved By"
        '
        'approvedby
        '
        Me.approvedby.Location = New System.Drawing.Point(103, 249)
        Me.approvedby.MaxLength = 5
        Me.approvedby.Name = "approvedby"
        Me.approvedby.ReadOnly = True
        Me.approvedby.Size = New System.Drawing.Size(139, 20)
        Me.approvedby.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(324, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 2163
        Me.Label6.Text = "Rate"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(480, 174)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 13)
        Me.Label11.TabIndex = 2161
        Me.Label11.Text = "Cable/Postage Charges"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(480, 152)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 13)
        Me.Label10.TabIndex = 2159
        Me.Label10.Text = "Bank Commision"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(480, 130)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 13)
        Me.Label9.TabIndex = 2157
        Me.Label9.Text = "Margin Deposit"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(480, 111)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 2154
        Me.Label7.Text = "A/C No."
        '
        'acno
        '
        Me.acno.Location = New System.Drawing.Point(619, 108)
        Me.acno.MaxLength = 5
        Me.acno.Name = "acno"
        Me.acno.ReadOnly = True
        Me.acno.Size = New System.Drawing.Size(218, 20)
        Me.acno.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(480, 250)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2151
        Me.Label5.Text = "Approved Date"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(480, 271)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 13)
        Me.Label13.TabIndex = 2149
        Me.Label13.Text = "Finance App Date"
        '
        'tot
        '
        Me.tot.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.tot.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tot.Location = New System.Drawing.Point(764, 196)
        Me.tot.Name = "tot"
        Me.tot.ReadOnly = True
        Me.tot.Size = New System.Drawing.Size(73, 20)
        Me.tot.TabIndex = 13
        Me.tot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.tot.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(480, 198)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 2146
        Me.Label4.Text = "Cut Margin"
        '
        'TotalInvoice
        '
        Me.TotalInvoice.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.TotalInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalInvoice.Location = New System.Drawing.Point(102, 173)
        Me.TotalInvoice.Name = "TotalInvoice"
        Me.TotalInvoice.Size = New System.Drawing.Size(139, 20)
        Me.TotalInvoice.TabIndex = 3
        Me.TotalInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Curr_Name
        '
        Me.Curr_Name.AutoSize = True
        Me.Curr_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Curr_Name.Location = New System.Drawing.Point(167, 198)
        Me.Curr_Name.Name = "Curr_Name"
        Me.Curr_Name.Size = New System.Drawing.Size(57, 13)
        Me.Curr_Name.TabIndex = 217
        Me.Curr_Name.Text = "Currency"
        '
        'curr
        '
        Me.curr.Location = New System.Drawing.Point(103, 195)
        Me.curr.MaxLength = 1
        Me.curr.Name = "curr"
        Me.curr.ReadOnly = True
        Me.curr.Size = New System.Drawing.Size(58, 20)
        Me.curr.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(20, 196)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 216
        Me.Label8.Text = "Currency"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(19, 176)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(69, 13)
        Me.Label27.TabIndex = 215
        Me.Label27.Text = "Total Invoice"
        '
        'bank_name
        '
        Me.bank_name.AutoSize = True
        Me.bank_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bank_name.Location = New System.Drawing.Point(180, 45)
        Me.bank_name.Name = "bank_name"
        Me.bank_name.Size = New System.Drawing.Size(35, 13)
        Me.bank_name.TabIndex = 166
        Me.bank_name.Text = "bank"
        '
        'remark
        '
        Me.remark.Location = New System.Drawing.Point(103, 65)
        Me.remark.Name = "remark"
        Me.remark.Size = New System.Drawing.Size(360, 102)
        Me.remark.TabIndex = 2
        Me.remark.Text = ""
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(152, 42)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(22, 18)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        Me.btnSearch.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Remark"
        '
        'tgl
        '
        Me.tgl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl.Location = New System.Drawing.Point(103, 18)
        Me.tgl.Name = "tgl"
        Me.tgl.Size = New System.Drawing.Size(91, 20)
        Me.tgl.TabIndex = 0
        Me.tgl.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Opening Date"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(883, 25)
        Me.ToolStrip1.TabIndex = 8
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
        Me.btnPrint.Enabled = False
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(49, 22)
        Me.btnPrint.Text = "Print"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Bank"
        '
        'bank
        '
        Me.bank.Location = New System.Drawing.Point(103, 42)
        Me.bank.MaxLength = 5
        Me.bank.Name = "bank"
        Me.bank.ReadOnly = True
        Me.bank.Size = New System.Drawing.Size(43, 20)
        Me.bank.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.ChkFinal)
        Me.GroupBox1.Controls.Add(Me.CutMargin)
        Me.GroupBox1.Controls.Add(Me.TotalAmount)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.tgl3)
        Me.GroupBox1.Controls.Add(Me.tgl2)
        Me.GroupBox1.Controls.Add(Me.CTCrt)
        Me.GroupBox1.Controls.Add(Me.crtdt)
        Me.GroupBox1.Controls.Add(Me.crtt)
        Me.GroupBox1.Controls.Add(Me.crt)
        Me.GroupBox1.Controls.Add(Me.crtdttext)
        Me.GroupBox1.Controls.Add(Me.cbLC)
        Me.GroupBox1.Controls.Add(Me.BLno)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.CTFun)
        Me.GroupBox1.Controls.Add(Me.CTApp)
        Me.GroupBox1.Controls.Add(Me.charge)
        Me.GroupBox1.Controls.Add(Me.commision)
        Me.GroupBox1.Controls.Add(Me.deposit)
        Me.GroupBox1.Controls.Add(Me.txtrate)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.financeappby)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.approvedby)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.acno)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.tot)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TotalInvoice)
        Me.GroupBox1.Controls.Add(Me.Curr_Name)
        Me.GroupBox1.Controls.Add(Me.curr)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label27)
        Me.GroupBox1.Controls.Add(Me.bank_name)
        Me.GroupBox1.Controls.Add(Me.remark)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.tgl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.bank)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(857, 295)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(712, 177)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(15, 13)
        Me.Label22.TabIndex = 2291
        Me.Label22.Text = "%"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(712, 155)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(15, 13)
        Me.Label21.TabIndex = 2290
        Me.Label21.Text = "%"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(711, 132)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(15, 13)
        Me.Label20.TabIndex = 2289
        Me.Label20.Text = "%"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(633, 65)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(133, 13)
        Me.Label19.TabIndex = 2288
        Me.Label19.Text = "Calculate for final shipment"
        '
        'ChkFinal
        '
        Me.ChkFinal.AutoSize = True
        Me.ChkFinal.Location = New System.Drawing.Point(619, 65)
        Me.ChkFinal.Name = "ChkFinal"
        Me.ChkFinal.Size = New System.Drawing.Size(15, 14)
        Me.ChkFinal.TabIndex = 2287
        Me.ChkFinal.UseVisualStyleBackColor = True
        '
        'CutMargin
        '
        Me.CutMargin.Location = New System.Drawing.Point(619, 197)
        Me.CutMargin.MaxLength = 15
        Me.CutMargin.Name = "CutMargin"
        Me.CutMargin.Size = New System.Drawing.Size(138, 20)
        Me.CutMargin.TabIndex = 2190
        Me.CutMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TotalAmount
        '
        Me.TotalAmount.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.TotalAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalAmount.Location = New System.Drawing.Point(324, 173)
        Me.TotalAmount.Name = "TotalAmount"
        Me.TotalAmount.Size = New System.Drawing.Size(139, 20)
        Me.TotalAmount.TabIndex = 2189
        Me.TotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(258, 177)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 13)
        Me.Label18.TabIndex = 2188
        Me.Label18.Text = "Budget Amt."
        '
        'tgl3
        '
        Me.tgl3.Checked = False
        Me.tgl3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl3.Location = New System.Drawing.Point(619, 269)
        Me.tgl3.Name = "tgl3"
        Me.tgl3.ShowCheckBox = True
        Me.tgl3.Size = New System.Drawing.Size(101, 20)
        Me.tgl3.TabIndex = 2187
        Me.tgl3.Value = New Date(2009, 2, 2, 0, 0, 0, 0)
        '
        'tgl2
        '
        Me.tgl2.Checked = False
        Me.tgl2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl2.Location = New System.Drawing.Point(619, 248)
        Me.tgl2.Name = "tgl2"
        Me.tgl2.ShowCheckBox = True
        Me.tgl2.Size = New System.Drawing.Size(101, 20)
        Me.tgl2.TabIndex = 2186
        Me.tgl2.Value = New Date(2009, 2, 2, 0, 0, 0, 0)
        '
        'CTCrt
        '
        Me.CTCrt.Location = New System.Drawing.Point(278, 226)
        Me.CTCrt.MaxLength = 1
        Me.CTCrt.Name = "CTCrt"
        Me.CTCrt.ReadOnly = True
        Me.CTCrt.Size = New System.Drawing.Size(41, 20)
        Me.CTCrt.TabIndex = 2185
        Me.CTCrt.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(619, 226)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(68, 20)
        Me.crtdt.TabIndex = 2184
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(20, 228)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2183
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(103, 226)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 2181
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(480, 229)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2182
        Me.crtdttext.Text = "Created Date"
        '
        'cbLC
        '
        Me.cbLC.FormattingEnabled = True
        Me.cbLC.Location = New System.Drawing.Point(619, 85)
        Me.cbLC.Name = "cbLC"
        Me.cbLC.Size = New System.Drawing.Size(218, 21)
        Me.cbLC.TabIndex = 2180
        '
        'BLno
        '
        Me.BLno.Enabled = False
        Me.BLno.Location = New System.Drawing.Point(619, 39)
        Me.BLno.MaxLength = 5
        Me.BLno.Name = "BLno"
        Me.BLno.Size = New System.Drawing.Size(218, 20)
        Me.BLno.TabIndex = 2178
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(480, 88)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(40, 13)
        Me.Label16.TabIndex = 2177
        Me.Label16.Text = "LC No."
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(480, 44)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 13)
        Me.Label17.TabIndex = 2176
        Me.Label17.Text = "B/L No."
        '
        'FrmBR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 330)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmBR"
        Me.Text = "FormBR"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents CTFun As System.Windows.Forms.TextBox
    Friend WithEvents CTApp As System.Windows.Forms.TextBox
    Friend WithEvents charge As System.Windows.Forms.MaskedTextBox
    Friend WithEvents commision As System.Windows.Forms.MaskedTextBox
    Friend WithEvents deposit As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtrate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents financeappby As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents approvedby As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents acno As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents tot As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TotalInvoice As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Curr_Name As System.Windows.Forms.Label
    Friend WithEvents curr As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents bank_name As System.Windows.Forms.Label
    Friend WithEvents remark As System.Windows.Forms.RichTextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tgl As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnReject As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bank As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents BLno As System.Windows.Forms.TextBox
    Friend WithEvents cbLC As System.Windows.Forms.ComboBox
    Friend WithEvents crtdt As System.Windows.Forms.TextBox
    Friend WithEvents crtt As System.Windows.Forms.Label
    Friend WithEvents crt As System.Windows.Forms.TextBox
    Friend WithEvents crtdttext As System.Windows.Forms.Label
    Friend WithEvents CTCrt As System.Windows.Forms.TextBox
    Friend WithEvents tgl3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents tgl2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TotalAmount As System.Windows.Forms.MaskedTextBox
    Friend WithEvents CutMargin As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ChkFinal As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
End Class
