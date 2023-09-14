<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCC))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCalc = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.btnApprove = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DTRequest = New System.Windows.Forms.DateTimePicker
        Me.cbCont = New System.Windows.Forms.ComboBox
        Me.txtCont = New System.Windows.Forms.TextBox
        Me.txtContNum = New System.Windows.Forms.TextBox
        Me.cbFor = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.tVAT = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.ttotal = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.total = New System.Windows.Forms.TextBox
        Me.remark = New System.Windows.Forms.RichTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.CTFin = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.financeappby = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.dgvcost = New System.Windows.Forms.DataGridView
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ItemCost = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Currency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Rate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.vat = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AppDt = New System.Windows.Forms.DateTimePicker
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
        Me.DTPrinted = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.listError = New System.Windows.Forms.ListBox
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvcost, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint, Me.ToolStripSeparator1, Me.btnCalc, Me.ToolStripSeparator5, Me.btnApprove})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(767, 25)
        Me.ToolStrip1.TabIndex = 7
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnCalc
        '
        Me.btnCalc.Image = Global.poim.My.Resources.Resources.Chart
        Me.btnCalc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCalc.Name = "btnCalc"
        Me.btnCalc.Size = New System.Drawing.Size(71, 22)
        Me.btnCalc.Text = "Calculate"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'btnApprove
        '
        Me.btnApprove.Image = CType(resources.GetObject("btnApprove.Image"), System.Drawing.Image)
        Me.btnApprove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(68, 22)
        Me.btnApprove.Text = "Approve"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DTRequest)
        Me.GroupBox1.Controls.Add(Me.cbCont)
        Me.GroupBox1.Controls.Add(Me.txtCont)
        Me.GroupBox1.Controls.Add(Me.txtContNum)
        Me.GroupBox1.Controls.Add(Me.cbFor)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.tVAT)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ttotal)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.total)
        Me.GroupBox1.Controls.Add(Me.remark)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CTFin)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.financeappby)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.dgvcost)
        Me.GroupBox1.Controls.Add(Me.AppDt)
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
        Me.GroupBox1.Controls.Add(Me.DTPrinted)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(740, 385)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'DTRequest
        '
        Me.DTRequest.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTRequest.Location = New System.Drawing.Point(438, 12)
        Me.DTRequest.Name = "DTRequest"
        Me.DTRequest.Size = New System.Drawing.Size(91, 20)
        Me.DTRequest.TabIndex = 2246
        Me.DTRequest.Value = New Date(2009, 2, 20, 0, 0, 0, 0)
        '
        'cbCont
        '
        Me.cbCont.FormattingEnabled = True
        Me.cbCont.Location = New System.Drawing.Point(640, 36)
        Me.cbCont.Name = "cbCont"
        Me.cbCont.Size = New System.Drawing.Size(88, 21)
        Me.cbCont.TabIndex = 2245
        '
        'txtCont
        '
        Me.txtCont.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCont.Location = New System.Drawing.Point(535, 41)
        Me.txtCont.MaxLength = 5
        Me.txtCont.Name = "txtCont"
        Me.txtCont.ReadOnly = True
        Me.txtCont.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtCont.Size = New System.Drawing.Size(60, 13)
        Me.txtCont.TabIndex = 2244
        Me.txtCont.Text = "Container"
        Me.txtCont.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtContNum
        '
        Me.txtContNum.Location = New System.Drawing.Point(606, 37)
        Me.txtContNum.MaxLength = 5
        Me.txtContNum.Name = "txtContNum"
        Me.txtContNum.Size = New System.Drawing.Size(28, 20)
        Me.txtContNum.TabIndex = 2242
        '
        'cbFor
        '
        Me.cbFor.FormattingEnabled = True
        Me.cbFor.Location = New System.Drawing.Point(91, 37)
        Me.cbFor.Name = "cbFor"
        Me.cbFor.Size = New System.Drawing.Size(438, 21)
        Me.cbFor.TabIndex = 2240
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 41)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(22, 13)
        Me.Label7.TabIndex = 2239
        Me.Label7.Text = "For"
        '
        'tVAT
        '
        Me.tVAT.Location = New System.Drawing.Point(548, 260)
        Me.tVAT.MaxLength = 5
        Me.tVAT.Name = "tVAT"
        Me.tVAT.ReadOnly = True
        Me.tVAT.Size = New System.Drawing.Size(106, 20)
        Me.tVAT.TabIndex = 2238
        Me.tVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(478, 288)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 2237
        Me.Label4.Text = "Total"
        '
        'ttotal
        '
        Me.ttotal.Location = New System.Drawing.Point(548, 284)
        Me.ttotal.MaxLength = 5
        Me.ttotal.Name = "ttotal"
        Me.ttotal.ReadOnly = True
        Me.ttotal.Size = New System.Drawing.Size(106, 20)
        Me.ttotal.TabIndex = 2236
        Me.ttotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(478, 263)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 2235
        Me.Label2.Text = "VAT"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(478, 239)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 13)
        Me.Label6.TabIndex = 2233
        Me.Label6.Text = "Sub Total"
        '
        'total
        '
        Me.total.Location = New System.Drawing.Point(548, 235)
        Me.total.MaxLength = 5
        Me.total.Name = "total"
        Me.total.ReadOnly = True
        Me.total.Size = New System.Drawing.Size(106, 20)
        Me.total.TabIndex = 2232
        Me.total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'remark
        '
        Me.remark.Location = New System.Drawing.Point(112, 235)
        Me.remark.Name = "remark"
        Me.remark.Size = New System.Drawing.Size(326, 65)
        Me.remark.TabIndex = 54
        Me.remark.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 238)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Remark"
        '
        'CTFin
        '
        Me.CTFin.Location = New System.Drawing.Point(287, 361)
        Me.CTFin.MaxLength = 1
        Me.CTFin.Name = "CTFin"
        Me.CTFin.ReadOnly = True
        Me.CTFin.Size = New System.Drawing.Size(41, 20)
        Me.CTFin.TabIndex = 2230
        Me.CTFin.Visible = False
        '
        'Button4
        '
        Me.Button4.Image = Global.poim.My.Resources.Resources.search
        Me.Button4.Location = New System.Drawing.Point(259, 364)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 18)
        Me.Button4.TabIndex = 2228
        Me.Button4.TabStop = False
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(16, 367)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 13)
        Me.Label14.TabIndex = 2229
        Me.Label14.Text = "*tidak di pakai"
        Me.Label14.Visible = False
        '
        'financeappby
        '
        Me.financeappby.Location = New System.Drawing.Point(112, 362)
        Me.financeappby.MaxLength = 5
        Me.financeappby.Name = "financeappby"
        Me.financeappby.ReadOnly = True
        Me.financeappby.Size = New System.Drawing.Size(139, 20)
        Me.financeappby.TabIndex = 2226
        Me.financeappby.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(347, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(73, 13)
        Me.Label13.TabIndex = 2227
        Me.Label13.Text = "Request Date"
        '
        'dgvcost
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvcost.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvcost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvcost.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.ItemCost, Me.Description, Me.Currency, Me.Rate, Me.Amount, Me.vat})
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvcost.DefaultCellStyle = DataGridViewCellStyle7
        Me.dgvcost.Location = New System.Drawing.Point(19, 64)
        Me.dgvcost.Name = "dgvcost"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvcost.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvcost.Size = New System.Drawing.Size(709, 165)
        Me.dgvcost.TabIndex = 2223
        '
        'No
        '
        Me.No.DataPropertyName = "no"
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        Me.No.Width = 30
        '
        'ItemCost
        '
        Me.ItemCost.DataPropertyName = "ItemCost"
        Me.ItemCost.HeaderText = "ItemCost"
        Me.ItemCost.Name = "ItemCost"
        Me.ItemCost.ReadOnly = True
        Me.ItemCost.Visible = False
        Me.ItemCost.Width = 60
        '
        'Description
        '
        Me.Description.DataPropertyName = "Description"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.Description.DefaultCellStyle = DataGridViewCellStyle2
        Me.Description.HeaderText = "Description"
        Me.Description.MaxInputLength = 40
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.Width = 300
        '
        'Currency
        '
        Me.Currency.DataPropertyName = "Currency"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Currency.DefaultCellStyle = DataGridViewCellStyle3
        Me.Currency.HeaderText = "Currency"
        Me.Currency.Name = "Currency"
        Me.Currency.ReadOnly = True
        Me.Currency.Width = 60
        '
        'Rate
        '
        Me.Rate.DataPropertyName = "Rate"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.Rate.DefaultCellStyle = DataGridViewCellStyle4
        Me.Rate.HeaderText = "Unit Amount*"
        Me.Rate.Name = "Rate"
        Me.Rate.Width = 80
        '
        'Amount
        '
        Me.Amount.DataPropertyName = "Amount"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = Nothing
        Me.Amount.DefaultCellStyle = DataGridViewCellStyle5
        Me.Amount.HeaderText = "Final Amount*"
        Me.Amount.Name = "Amount"
        '
        'vat
        '
        Me.vat.DataPropertyName = "Vat"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle6.Format = "N2"
        Me.vat.DefaultCellStyle = DataGridViewCellStyle6
        Me.vat.HeaderText = "VAT*"
        Me.vat.Name = "vat"
        Me.vat.Width = 80
        '
        'AppDt
        '
        Me.AppDt.Checked = False
        Me.AppDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AppDt.Location = New System.Drawing.Point(548, 333)
        Me.AppDt.Name = "AppDt"
        Me.AppDt.ShowCheckBox = True
        Me.AppDt.Size = New System.Drawing.Size(106, 20)
        Me.AppDt.TabIndex = 2222
        Me.AppDt.Value = New Date(2009, 2, 20, 0, 0, 0, 0)
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(606, 11)
        Me.Status.MaxLength = 15
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(122, 20)
        Me.Status.TabIndex = 2206
        Me.Status.Text = "Open"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(562, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2207
        Me.Label15.Text = "Status"
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(287, 337)
        Me.CTApp.MaxLength = 1
        Me.CTApp.Name = "CTApp"
        Me.CTApp.ReadOnly = True
        Me.CTApp.Size = New System.Drawing.Size(41, 20)
        Me.CTApp.TabIndex = 2205
        Me.CTApp.Visible = False
        '
        'Button3
        '
        Me.Button3.Image = Global.poim.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(259, 337)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 2203
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 341)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2204
        Me.Label12.Text = "Approved By"
        '
        'approvedby
        '
        Me.approvedby.Location = New System.Drawing.Point(112, 336)
        Me.approvedby.MaxLength = 5
        Me.approvedby.Name = "approvedby"
        Me.approvedby.ReadOnly = True
        Me.approvedby.Size = New System.Drawing.Size(139, 20)
        Me.approvedby.TabIndex = 2200
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(436, 340)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2202
        Me.Label5.Text = "Approved Date"
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(436, 316)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2199
        Me.crtdttext.Text = "Created Date"
        '
        'CTCrt
        '
        Me.CTCrt.Location = New System.Drawing.Point(287, 311)
        Me.CTCrt.MaxLength = 1
        Me.CTCrt.Name = "CTCrt"
        Me.CTCrt.ReadOnly = True
        Me.CTCrt.Size = New System.Drawing.Size(41, 20)
        Me.CTCrt.TabIndex = 2198
        Me.CTCrt.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(548, 309)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(68, 20)
        Me.crtdt.TabIndex = 2197
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(16, 317)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2196
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(112, 311)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 2195
        '
        'DTPrinted
        '
        Me.DTPrinted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPrinted.Location = New System.Drawing.Point(91, 11)
        Me.DTPrinted.Name = "DTPrinted"
        Me.DTPrinted.Size = New System.Drawing.Size(91, 20)
        Me.DTPrinted.TabIndex = 2183
        Me.DTPrinted.Value = New Date(2009, 2, 20, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 2184
        Me.Label1.Text = "Opening Date"
        '
        'listError
        '
        Me.listError.ForeColor = System.Drawing.Color.Red
        Me.listError.FormattingEnabled = True
        Me.listError.HorizontalScrollbar = True
        Me.listError.Location = New System.Drawing.Point(12, 419)
        Me.listError.Name = "listError"
        Me.listError.Size = New System.Drawing.Size(740, 56)
        Me.listError.TabIndex = 2239
        '
        'FrmCC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(767, 483)
        Me.Controls.Add(Me.listError)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmCC"
        Me.Text = "Inklaring"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvcost, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents AppDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvcost As System.Windows.Forms.DataGridView
    Friend WithEvents CTFin As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents financeappby As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCalc As System.Windows.Forms.ToolStripButton
    Friend WithEvents remark As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents total As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents listError As System.Windows.Forms.ListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ttotal As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tVAT As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cbFor As System.Windows.Forms.ComboBox
    Friend WithEvents txtContNum As System.Windows.Forms.TextBox
    Friend WithEvents txtCont As System.Windows.Forms.TextBox
    Friend WithEvents cbCont As System.Windows.Forms.ComboBox
    Friend WithEvents No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ItemCost As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Currency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents vat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnApprove As System.Windows.Forms.ToolStripButton
    Friend WithEvents DTRequest As System.Windows.Forms.DateTimePicker
End Class
