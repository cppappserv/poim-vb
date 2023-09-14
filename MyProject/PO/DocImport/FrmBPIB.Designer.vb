<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBPIB
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBPIB))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.FinDT = New System.Windows.Forms.DateTimePicker
        Me.AppDT = New System.Windows.Forms.DateTimePicker
        Me.CTFin = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.financeappby = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
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
        Me.remark = New System.Windows.Forms.RichTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblCityName = New System.Windows.Forms.Label
        Me.btnSearchCity = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtCity_Code = New System.Windows.Forms.TextBox
        Me.DTPrinted = New System.Windows.Forms.DateTimePicker
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.blno = New System.Windows.Forms.TextBox
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.acno = New System.Windows.Forms.TextBox
        Me.bank_name = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.tgl = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.bank = New System.Windows.Forms.TextBox
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(844, 25)
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
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "Print"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.FinDT)
        Me.GroupBox1.Controls.Add(Me.AppDT)
        Me.GroupBox1.Controls.Add(Me.CTFin)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.financeappby)
        Me.GroupBox1.Controls.Add(Me.Label13)
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
        Me.GroupBox1.Controls.Add(Me.remark)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblCityName)
        Me.GroupBox1.Controls.Add(Me.btnSearchCity)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.txtCity_Code)
        Me.GroupBox1.Controls.Add(Me.DTPrinted)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.blno)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.acno)
        Me.GroupBox1.Controls.Add(Me.bank_name)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.tgl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.bank)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(825, 244)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'FinDT
        '
        Me.FinDT.Checked = False
        Me.FinDT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.FinDT.Location = New System.Drawing.Point(618, 205)
        Me.FinDT.Name = "FinDT"
        Me.FinDT.ShowCheckBox = True
        Me.FinDT.Size = New System.Drawing.Size(102, 20)
        Me.FinDT.TabIndex = 2242
        Me.FinDT.Value = New Date(2009, 2, 11, 0, 0, 0, 0)
        '
        'AppDT
        '
        Me.AppDT.Checked = False
        Me.AppDT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AppDT.Location = New System.Drawing.Point(618, 180)
        Me.AppDT.Name = "AppDT"
        Me.AppDT.ShowCheckBox = True
        Me.AppDT.Size = New System.Drawing.Size(102, 20)
        Me.AppDT.TabIndex = 2241
        Me.AppDT.Value = New Date(2009, 2, 11, 0, 0, 0, 0)
        '
        'CTFin
        '
        Me.CTFin.Location = New System.Drawing.Point(277, 203)
        Me.CTFin.MaxLength = 1
        Me.CTFin.Name = "CTFin"
        Me.CTFin.ReadOnly = True
        Me.CTFin.Size = New System.Drawing.Size(41, 20)
        Me.CTFin.TabIndex = 2240
        Me.CTFin.Visible = False
        '
        'Button4
        '
        Me.Button4.Image = Global.poim.My.Resources.Resources.search
        Me.Button4.Location = New System.Drawing.Point(249, 206)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 18)
        Me.Button4.TabIndex = 2238
        Me.Button4.TabStop = False
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(18, 209)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 2239
        Me.Label14.Text = "Finance App By"
        '
        'financeappby
        '
        Me.financeappby.Location = New System.Drawing.Point(102, 204)
        Me.financeappby.MaxLength = 5
        Me.financeappby.Name = "financeappby"
        Me.financeappby.ReadOnly = True
        Me.financeappby.Size = New System.Drawing.Size(139, 20)
        Me.financeappby.TabIndex = 2236
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(479, 209)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 13)
        Me.Label13.TabIndex = 2237
        Me.Label13.Text = "Finance App Date"
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(275, 180)
        Me.CTApp.MaxLength = 1
        Me.CTApp.Name = "CTApp"
        Me.CTApp.ReadOnly = True
        Me.CTApp.Size = New System.Drawing.Size(41, 20)
        Me.CTApp.TabIndex = 2235
        Me.CTApp.Visible = False
        '
        'Button3
        '
        Me.Button3.Image = Global.poim.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(247, 180)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 2233
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(18, 184)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2234
        Me.Label12.Text = "Approved By"
        '
        'approvedby
        '
        Me.approvedby.Location = New System.Drawing.Point(102, 178)
        Me.approvedby.MaxLength = 5
        Me.approvedby.Name = "approvedby"
        Me.approvedby.ReadOnly = True
        Me.approvedby.Size = New System.Drawing.Size(139, 20)
        Me.approvedby.TabIndex = 2231
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(479, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2232
        Me.Label5.Text = "Approved Date"
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(479, 160)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2230
        Me.crtdttext.Text = "Created Date"
        '
        'CTCrt
        '
        Me.CTCrt.Location = New System.Drawing.Point(275, 154)
        Me.CTCrt.MaxLength = 1
        Me.CTCrt.Name = "CTCrt"
        Me.CTCrt.ReadOnly = True
        Me.CTCrt.Size = New System.Drawing.Size(41, 20)
        Me.CTCrt.TabIndex = 2229
        Me.CTCrt.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(618, 153)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(68, 20)
        Me.crtdt.TabIndex = 2228
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(18, 160)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2227
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(102, 153)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 2226
        '
        'remark
        '
        Me.remark.Location = New System.Drawing.Point(102, 89)
        Me.remark.Name = "remark"
        Me.remark.Size = New System.Drawing.Size(360, 58)
        Me.remark.TabIndex = 2201
        Me.remark.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 2202
        Me.Label3.Text = "Remark"
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = True
        Me.lblCityName.Location = New System.Drawing.Point(179, 23)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(77, 13)
        Me.lblCityName.TabIndex = 2200
        Me.lblCityName.Text = "city description"
        '
        'btnSearchCity
        '
        Me.btnSearchCity.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearchCity.Location = New System.Drawing.Point(151, 19)
        Me.btnSearchCity.Name = "btnSearchCity"
        Me.btnSearchCity.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCity.TabIndex = 2198
        Me.btnSearchCity.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(18, 26)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(57, 13)
        Me.Label17.TabIndex = 2199
        Me.Label17.Text = "Printed On"
        '
        'txtCity_Code
        '
        Me.txtCity_Code.Location = New System.Drawing.Point(102, 18)
        Me.txtCity_Code.MaxLength = 5
        Me.txtCity_Code.Name = "txtCity_Code"
        Me.txtCity_Code.Size = New System.Drawing.Size(43, 20)
        Me.txtCity_Code.TabIndex = 2197
        '
        'DTPrinted
        '
        Me.DTPrinted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPrinted.Location = New System.Drawing.Point(102, 43)
        Me.DTPrinted.Name = "DTPrinted"
        Me.DTPrinted.Size = New System.Drawing.Size(91, 20)
        Me.DTPrinted.TabIndex = 2195
        Me.DTPrinted.Value = New Date(2009, 2, 11, 0, 0, 0, 0)
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(18, 50)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(66, 13)
        Me.Label18.TabIndex = 2196
        Me.Label18.Text = "Printed Date"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(479, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(40, 13)
        Me.Label16.TabIndex = 2181
        Me.Label16.Text = "BL No."
        '
        'blno
        '
        Me.blno.Location = New System.Drawing.Point(619, 43)
        Me.blno.MaxLength = 60
        Me.blno.Name = "blno"
        Me.blno.Size = New System.Drawing.Size(175, 20)
        Me.blno.TabIndex = 2180
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(619, 18)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(84, 20)
        Me.Status.TabIndex = 9
        Me.Status.Text = "Open"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(479, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2173
        Me.Label15.Text = "Status"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(479, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 2154
        Me.Label7.Text = "Account No."
        '
        'acno
        '
        Me.acno.Location = New System.Drawing.Point(618, 87)
        Me.acno.MaxLength = 5
        Me.acno.Name = "acno"
        Me.acno.ReadOnly = True
        Me.acno.Size = New System.Drawing.Size(176, 20)
        Me.acno.TabIndex = 10
        '
        'bank_name
        '
        Me.bank_name.AutoSize = True
        Me.bank_name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bank_name.Location = New System.Drawing.Point(694, 68)
        Me.bank_name.Name = "bank_name"
        Me.bank_name.Size = New System.Drawing.Size(35, 13)
        Me.bank_name.TabIndex = 166
        Me.bank_name.Text = "bank"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(666, 65)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(22, 18)
        Me.btnSearch.TabIndex = 2
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'tgl
        '
        Me.tgl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl.Location = New System.Drawing.Point(102, 66)
        Me.tgl.Name = "tgl"
        Me.tgl.Size = New System.Drawing.Size(91, 20)
        Me.tgl.TabIndex = 0
        Me.tgl.Value = New Date(2009, 3, 11, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Opening Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(479, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Bank"
        '
        'bank
        '
        Me.bank.Location = New System.Drawing.Point(619, 65)
        Me.bank.MaxLength = 5
        Me.bank.Name = "bank"
        Me.bank.Size = New System.Drawing.Size(43, 20)
        Me.bank.TabIndex = 1
        '
        'FrmBPIB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(844, 281)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.HelpButton = True
        Me.Name = "FrmBPIB"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "B - PIB"
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
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents tgl As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents bank As System.Windows.Forms.TextBox
    Friend WithEvents bank_name As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents acno As System.Windows.Forms.TextBox
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents blno As System.Windows.Forms.TextBox
    Friend WithEvents remark As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCityName As System.Windows.Forms.Label
    Friend WithEvents btnSearchCity As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCity_Code As System.Windows.Forms.TextBox
    Friend WithEvents DTPrinted As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents FinDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents AppDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents CTFin As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents financeappby As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents CTApp As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents approvedby As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents crtdttext As System.Windows.Forms.Label
    Friend WithEvents CTCrt As System.Windows.Forms.TextBox
    Friend WithEvents crtdt As System.Windows.Forms.TextBox
    Friend WithEvents crtt As System.Windows.Forms.Label
    Friend WithEvents crt As System.Windows.Forms.TextBox
End Class
