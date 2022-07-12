<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmKOSK
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmKOSK))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.AccountName = New System.Windows.Forms.MaskedTextBox
        Me.Bank = New System.Windows.Forms.TextBox
        Me.AccountNo = New System.Windows.Forms.MaskedTextBox
        Me.txtPos = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtLap = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtBCNo = New System.Windows.Forms.TextBox
        Me.BCDt = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.refund2 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblDG = New System.Windows.Forms.Label
        Me.cbDG = New System.Windows.Forms.ComboBox
        Me.print2 = New System.Windows.Forms.RadioButton
        Me.print1 = New System.Windows.Forms.RadioButton
        Me.BankBranch = New System.Windows.Forms.MaskedTextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.AppDt = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAuthorized = New System.Windows.Forms.TextBox
        Me.lblExp = New System.Windows.Forms.Label
        Me.btnSearchExp = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtExp = New System.Windows.Forms.TextBox
        Me.CTFin = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.financeappby = New System.Windows.Forms.TextBox
        Me.findt = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.CTApp = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.approvedby = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.crtdttext = New System.Windows.Forms.Label
        Me.CTCrt = New System.Windows.Forms.TextBox
        Me.crtdt = New System.Windows.Forms.TextBox
        Me.crtt = New System.Windows.Forms.Label
        Me.crt = New System.Windows.Forms.TextBox
        Me.lblCityName = New System.Windows.Forms.Label
        Me.btnSearchCity = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCity_Code = New System.Windows.Forms.TextBox
        Me.DTPrinted = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(767, 25)
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.AccountName)
        Me.GroupBox1.Controls.Add(Me.Bank)
        Me.GroupBox1.Controls.Add(Me.AccountNo)
        Me.GroupBox1.Controls.Add(Me.txtPos)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtLap)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtBCNo)
        Me.GroupBox1.Controls.Add(Me.BCDt)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.refund2)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblDG)
        Me.GroupBox1.Controls.Add(Me.cbDG)
        Me.GroupBox1.Controls.Add(Me.print2)
        Me.GroupBox1.Controls.Add(Me.print1)
        Me.GroupBox1.Controls.Add(Me.BankBranch)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.AppDt)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtTitle)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtAuthorized)
        Me.GroupBox1.Controls.Add(Me.lblExp)
        Me.GroupBox1.Controls.Add(Me.btnSearchExp)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtExp)
        Me.GroupBox1.Controls.Add(Me.CTFin)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.financeappby)
        Me.GroupBox1.Controls.Add(Me.findt)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.CTApp)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.approvedby)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.crtdttext)
        Me.GroupBox1.Controls.Add(Me.CTCrt)
        Me.GroupBox1.Controls.Add(Me.crtdt)
        Me.GroupBox1.Controls.Add(Me.crtt)
        Me.GroupBox1.Controls.Add(Me.crt)
        Me.GroupBox1.Controls.Add(Me.lblCityName)
        Me.GroupBox1.Controls.Add(Me.btnSearchCity)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtCity_Code)
        Me.GroupBox1.Controls.Add(Me.DTPrinted)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(743, 339)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(421, 174)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(41, 13)
        Me.Label18.TabIndex = 2248
        Me.Label18.Text = "- Name"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(421, 130)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(47, 13)
        Me.Label17.TabIndex = 2247
        Me.Label17.Text = "- Branch"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(421, 153)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(70, 13)
        Me.Label16.TabIndex = 2246
        Me.Label16.Text = "- Account No"
        '
        'AccountName
        '
        Me.AccountName.BackColor = System.Drawing.SystemColors.Control
        Me.AccountName.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.AccountName.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.AccountName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountName.HidePromptOnLeave = True
        Me.AccountName.Location = New System.Drawing.Point(531, 170)
        Me.AccountName.Name = "AccountName"
        Me.AccountName.ReadOnly = True
        Me.AccountName.RejectInputOnFirstFailure = True
        Me.AccountName.Size = New System.Drawing.Size(194, 20)
        Me.AccountName.TabIndex = 2245
        Me.AccountName.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'Bank
        '
        Me.Bank.Enabled = False
        Me.Bank.Location = New System.Drawing.Point(531, 103)
        Me.Bank.MaxLength = 5
        Me.Bank.Name = "Bank"
        Me.Bank.Size = New System.Drawing.Size(122, 20)
        Me.Bank.TabIndex = 2244
        '
        'AccountNo
        '
        Me.AccountNo.BackColor = System.Drawing.SystemColors.Control
        Me.AccountNo.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.AccountNo.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.AccountNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AccountNo.HidePromptOnLeave = True
        Me.AccountNo.Location = New System.Drawing.Point(531, 148)
        Me.AccountNo.Name = "AccountNo"
        Me.AccountNo.ReadOnly = True
        Me.AccountNo.RejectInputOnFirstFailure = True
        Me.AccountNo.Size = New System.Drawing.Size(122, 20)
        Me.AccountNo.TabIndex = 2243
        Me.AccountNo.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'txtPos
        '
        Me.txtPos.Location = New System.Drawing.Point(531, 59)
        Me.txtPos.MaxLength = 40
        Me.txtPos.Name = "txtPos"
        Me.txtPos.Size = New System.Drawing.Size(122, 20)
        Me.txtPos.TabIndex = 2242
        Me.txtPos.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(421, 62)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(25, 13)
        Me.Label11.TabIndex = 2241
        Me.Label11.Text = "Pos"
        '
        'txtLap
        '
        Me.txtLap.Location = New System.Drawing.Point(531, 36)
        Me.txtLap.MaxLength = 40
        Me.txtLap.Name = "txtLap"
        Me.txtLap.Size = New System.Drawing.Size(122, 20)
        Me.txtLap.TabIndex = 2239
        Me.txtLap.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(421, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 2238
        Me.Label9.Text = "TPS Lap."
        '
        'txtBCNo
        '
        Me.txtBCNo.Location = New System.Drawing.Point(124, 58)
        Me.txtBCNo.MaxLength = 40
        Me.txtBCNo.Name = "txtBCNo"
        Me.txtBCNo.Size = New System.Drawing.Size(91, 20)
        Me.txtBCNo.TabIndex = 2237
        Me.txtBCNo.Visible = False
        '
        'BCDt
        '
        Me.BCDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.BCDt.Location = New System.Drawing.Point(217, 58)
        Me.BCDt.Name = "BCDt"
        Me.BCDt.ShowCheckBox = True
        Me.BCDt.Size = New System.Drawing.Size(97, 20)
        Me.BCDt.TabIndex = 2236
        Me.BCDt.Value = New Date(2009, 1, 27, 0, 0, 0, 0)
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 61)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 13)
        Me.Label8.TabIndex = 2234
        Me.Label8.Text = "No BC 1.1"
        '
        'refund2
        '
        Me.refund2.Location = New System.Drawing.Point(531, 192)
        Me.refund2.MaxLength = 255
        Me.refund2.Name = "refund2"
        Me.refund2.Size = New System.Drawing.Size(194, 20)
        Me.refund2.TabIndex = 2233
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(421, 197)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 2232
        Me.Label7.Text = "Refund Amount"
        '
        'lblDG
        '
        Me.lblDG.AutoSize = True
        Me.lblDG.Location = New System.Drawing.Point(18, 193)
        Me.lblDG.Name = "lblDG"
        Me.lblDG.Size = New System.Drawing.Size(88, 13)
        Me.lblDG.TabIndex = 2197
        Me.lblDG.Text = "Document Group"
        '
        'cbDG
        '
        Me.cbDG.FormattingEnabled = True
        Me.cbDG.Location = New System.Drawing.Point(124, 190)
        Me.cbDG.Name = "cbDG"
        Me.cbDG.Size = New System.Drawing.Size(192, 21)
        Me.cbDG.TabIndex = 2231
        '
        'print2
        '
        Me.print2.AutoSize = True
        Me.print2.Location = New System.Drawing.Point(18, 313)
        Me.print2.Name = "print2"
        Me.print2.Size = New System.Drawing.Size(91, 17)
        Me.print2.TabIndex = 2230
        Me.print2.Text = "Print by Name"
        Me.print2.UseVisualStyleBackColor = True
        Me.print2.Visible = False
        '
        'print1
        '
        Me.print1.AutoSize = True
        Me.print1.Checked = True
        Me.print1.Location = New System.Drawing.Point(18, 297)
        Me.print1.Name = "print1"
        Me.print1.Size = New System.Drawing.Size(107, 17)
        Me.print1.TabIndex = 2229
        Me.print1.TabStop = True
        Me.print1.Text = "Print by Company"
        Me.print1.UseVisualStyleBackColor = True
        Me.print1.Visible = False
        '
        'BankBranch
        '
        Me.BankBranch.BackColor = System.Drawing.SystemColors.Control
        Me.BankBranch.Culture = New System.Globalization.CultureInfo("en-GB")
        Me.BankBranch.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.BankBranch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BankBranch.HidePromptOnLeave = True
        Me.BankBranch.Location = New System.Drawing.Point(531, 126)
        Me.BankBranch.Name = "BankBranch"
        Me.BankBranch.ReadOnly = True
        Me.BankBranch.RejectInputOnFirstFailure = True
        Me.BankBranch.Size = New System.Drawing.Size(194, 20)
        Me.BankBranch.TabIndex = 6
        Me.BankBranch.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(653, 103)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(22, 20)
        Me.btnSearch.TabIndex = 9
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(421, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 2224
        Me.Label6.Text = "Bank "
        '
        'AppDt
        '
        Me.AppDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AppDt.Location = New System.Drawing.Point(531, 236)
        Me.AppDt.Name = "AppDt"
        Me.AppDt.ShowCheckBox = True
        Me.AppDt.Size = New System.Drawing.Size(97, 20)
        Me.AppDt.TabIndex = 15
        Me.AppDt.Value = New Date(2009, 1, 27, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 2221
        Me.Label2.Text = "Authorized Title"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(124, 125)
        Me.txtTitle.MaxLength = 5
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(192, 20)
        Me.txtTitle.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 107)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 2219
        Me.Label4.Text = "Authorized Person"
        '
        'txtAuthorized
        '
        Me.txtAuthorized.Location = New System.Drawing.Point(124, 103)
        Me.txtAuthorized.MaxLength = 5
        Me.txtAuthorized.Name = "txtAuthorized"
        Me.txtAuthorized.ReadOnly = True
        Me.txtAuthorized.Size = New System.Drawing.Size(192, 20)
        Me.txtAuthorized.TabIndex = 7
        '
        'lblExp
        '
        Me.lblExp.AutoSize = True
        Me.lblExp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExp.Location = New System.Drawing.Point(249, 85)
        Me.lblExp.Name = "lblExp"
        Me.lblExp.Size = New System.Drawing.Size(58, 13)
        Me.lblExp.TabIndex = 2217
        Me.lblExp.Text = "Company"
        '
        'btnSearchExp
        '
        Me.btnSearchExp.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearchExp.Location = New System.Drawing.Point(215, 80)
        Me.btnSearchExp.Name = "btnSearchExp"
        Me.btnSearchExp.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchExp.TabIndex = 5
        Me.btnSearchExp.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 2216
        Me.Label3.Text = "Expedition"
        '
        'txtExp
        '
        Me.txtExp.Enabled = False
        Me.txtExp.Location = New System.Drawing.Point(124, 80)
        Me.txtExp.MaxLength = 5
        Me.txtExp.Name = "txtExp"
        Me.txtExp.Size = New System.Drawing.Size(91, 20)
        Me.txtExp.TabIndex = 4
        '
        'CTFin
        '
        Me.CTFin.Location = New System.Drawing.Point(297, 258)
        Me.CTFin.MaxLength = 1
        Me.CTFin.Name = "CTFin"
        Me.CTFin.ReadOnly = True
        Me.CTFin.Size = New System.Drawing.Size(41, 20)
        Me.CTFin.TabIndex = 18
        Me.CTFin.Visible = False
        '
        'Button4
        '
        Me.Button4.Image = Global.POIM.My.Resources.Resources.search
        Me.Button4.Location = New System.Drawing.Point(264, 260)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 18)
        Me.Button4.TabIndex = 17
        Me.Button4.TabStop = False
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(18, 263)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 2212
        Me.Label14.Text = "Finance App By"
        Me.Label14.Visible = False
        '
        'financeappby
        '
        Me.financeappby.Location = New System.Drawing.Point(124, 258)
        Me.financeappby.MaxLength = 5
        Me.financeappby.Name = "financeappby"
        Me.financeappby.ReadOnly = True
        Me.financeappby.Size = New System.Drawing.Size(139, 20)
        Me.financeappby.TabIndex = 16
        Me.financeappby.Visible = False
        '
        'findt
        '
        Me.findt.Location = New System.Drawing.Point(531, 258)
        Me.findt.MaxLength = 5
        Me.findt.Name = "findt"
        Me.findt.ReadOnly = True
        Me.findt.Size = New System.Drawing.Size(68, 20)
        Me.findt.TabIndex = 19
        Me.findt.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(421, 263)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 13)
        Me.Label13.TabIndex = 2210
        Me.Label13.Text = "Finance App Date"
        Me.Label13.Visible = False
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(531, 14)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(84, 20)
        Me.Status.TabIndex = 2
        Me.Status.Text = "Open"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(421, 18)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2207
        Me.Label15.Text = "Status"
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(297, 236)
        Me.CTApp.MaxLength = 1
        Me.CTApp.Name = "CTApp"
        Me.CTApp.ReadOnly = True
        Me.CTApp.Size = New System.Drawing.Size(41, 20)
        Me.CTApp.TabIndex = 2205
        Me.CTApp.Visible = False
        '
        'Button3
        '
        Me.Button3.Image = Global.POIM.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(264, 236)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 14
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(18, 240)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2204
        Me.Label12.Text = "Approved By"
        '
        'approvedby
        '
        Me.approvedby.Location = New System.Drawing.Point(124, 236)
        Me.approvedby.MaxLength = 5
        Me.approvedby.Name = "approvedby"
        Me.approvedby.ReadOnly = True
        Me.approvedby.Size = New System.Drawing.Size(139, 20)
        Me.approvedby.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(421, 240)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2202
        Me.Label5.Text = "Approved Date"
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(421, 218)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2199
        Me.crtdttext.Text = "Created Date"
        '
        'CTCrt
        '
        Me.CTCrt.Location = New System.Drawing.Point(297, 214)
        Me.CTCrt.MaxLength = 1
        Me.CTCrt.Name = "CTCrt"
        Me.CTCrt.ReadOnly = True
        Me.CTCrt.Size = New System.Drawing.Size(41, 20)
        Me.CTCrt.TabIndex = 2198
        Me.CTCrt.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(531, 214)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(68, 20)
        Me.crtdt.TabIndex = 12
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(18, 218)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2196
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(124, 214)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 11
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = True
        Me.lblCityName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCityName.Location = New System.Drawing.Point(249, 18)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(28, 13)
        Me.lblCityName.TabIndex = 2194
        Me.lblCityName.Text = "City"
        '
        'btnSearchCity
        '
        Me.btnSearchCity.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearchCity.Location = New System.Drawing.Point(215, 13)
        Me.btnSearchCity.Name = "btnSearchCity"
        Me.btnSearchCity.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCity.TabIndex = 1
        Me.btnSearchCity.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(18, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 13)
        Me.Label10.TabIndex = 2193
        Me.Label10.Text = "Printed On"
        '
        'txtCity_Code
        '
        Me.txtCity_Code.Enabled = False
        Me.txtCity_Code.Location = New System.Drawing.Point(124, 14)
        Me.txtCity_Code.MaxLength = 5
        Me.txtCity_Code.Name = "txtCity_Code"
        Me.txtCity_Code.Size = New System.Drawing.Size(91, 20)
        Me.txtCity_Code.TabIndex = 0
        '
        'DTPrinted
        '
        Me.DTPrinted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPrinted.Location = New System.Drawing.Point(124, 36)
        Me.DTPrinted.Name = "DTPrinted"
        Me.DTPrinted.Size = New System.Drawing.Size(91, 20)
        Me.DTPrinted.TabIndex = 3
        Me.DTPrinted.Value = New Date(2009, 1, 27, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 2184
        Me.Label1.Text = "Printed Date"
        '
        'FrmKOSK
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(767, 382)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmKOSK"
        Me.Text = "FrmKOSK"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DTPrinted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCityName As System.Windows.Forms.Label
    Friend WithEvents btnSearchCity As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCity_Code As System.Windows.Forms.TextBox
    Friend WithEvents CTCrt As System.Windows.Forms.TextBox
    Friend WithEvents crtdt As System.Windows.Forms.TextBox
    Friend WithEvents crtt As System.Windows.Forms.Label
    Friend WithEvents crt As System.Windows.Forms.TextBox
    Friend WithEvents crtdttext As System.Windows.Forms.Label
    Friend WithEvents CTApp As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents approvedby As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents CTFin As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents financeappby As System.Windows.Forms.TextBox
    Friend WithEvents findt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAuthorized As System.Windows.Forms.TextBox
    Friend WithEvents lblExp As System.Windows.Forms.Label
    Friend WithEvents btnSearchExp As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtExp As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents AppDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents BankBranch As System.Windows.Forms.MaskedTextBox
    Friend WithEvents print2 As System.Windows.Forms.RadioButton
    Friend WithEvents print1 As System.Windows.Forms.RadioButton
    Friend WithEvents cbDG As System.Windows.Forms.ComboBox
    Friend WithEvents lblDG As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents refund2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLap As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBCNo As System.Windows.Forms.TextBox
    Friend WithEvents BCDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPos As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents AccountNo As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Bank As System.Windows.Forms.TextBox
    Friend WithEvents AccountName As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
End Class
