<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPV))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnTTd = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.txtCompanyCode = New System.Windows.Forms.TextBox
        Me.FinDT = New System.Windows.Forms.DateTimePicker
        Me.AppDT = New System.Windows.Forms.DateTimePicker
        Me.CTFin = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.financeappby = New System.Windows.Forms.TextBox
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
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint, Me.ToolStripSeparator1, Me.btnTTd})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(656, 25)
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnTTd
        '
        Me.btnTTd.Image = CType(resources.GetObject("btnTTd.Image"), System.Drawing.Image)
        Me.btnTTd.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTTd.Name = "btnTTd"
        Me.btnTTd.Size = New System.Drawing.Size(98, 22)
        Me.btnTTd.Text = "Tanda Terima"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtRemark)
        Me.GroupBox1.Controls.Add(Me.txtCompanyCode)
        Me.GroupBox1.Controls.Add(Me.FinDT)
        Me.GroupBox1.Controls.Add(Me.AppDT)
        Me.GroupBox1.Controls.Add(Me.CTFin)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.financeappby)
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
        Me.GroupBox1.Size = New System.Drawing.Size(632, 218)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 2228
        Me.Label2.Text = "Remark"
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(101, 70)
        Me.txtRemark.MaxLength = 2147483647
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(252, 55)
        Me.txtRemark.TabIndex = 2227
        Me.txtRemark.Tag = ""
        '
        'txtCompanyCode
        '
        Me.txtCompanyCode.Location = New System.Drawing.Point(275, 44)
        Me.txtCompanyCode.MaxLength = 1
        Me.txtCompanyCode.Name = "txtCompanyCode"
        Me.txtCompanyCode.ReadOnly = True
        Me.txtCompanyCode.Size = New System.Drawing.Size(41, 20)
        Me.txtCompanyCode.TabIndex = 2226
        Me.txtCompanyCode.Visible = False
        '
        'FinDT
        '
        Me.FinDT.Checked = False
        Me.FinDT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.FinDT.Location = New System.Drawing.Point(505, 182)
        Me.FinDT.Name = "FinDT"
        Me.FinDT.ShowCheckBox = True
        Me.FinDT.Size = New System.Drawing.Size(100, 20)
        Me.FinDT.TabIndex = 2225
        Me.FinDT.Value = New Date(2009, 1, 28, 0, 0, 0, 0)
        '
        'AppDT
        '
        Me.AppDT.Checked = False
        Me.AppDT.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AppDT.Location = New System.Drawing.Point(505, 157)
        Me.AppDT.Name = "AppDT"
        Me.AppDT.ShowCheckBox = True
        Me.AppDT.Size = New System.Drawing.Size(100, 20)
        Me.AppDT.TabIndex = 2224
        Me.AppDT.Value = New Date(2009, 1, 28, 0, 0, 0, 0)
        '
        'CTFin
        '
        Me.CTFin.Location = New System.Drawing.Point(277, 180)
        Me.CTFin.MaxLength = 1
        Me.CTFin.Name = "CTFin"
        Me.CTFin.ReadOnly = True
        Me.CTFin.Size = New System.Drawing.Size(41, 20)
        Me.CTFin.TabIndex = 2213
        Me.CTFin.Visible = False
        '
        'Button4
        '
        Me.Button4.Image = Global.poim.My.Resources.Resources.search
        Me.Button4.Location = New System.Drawing.Point(249, 183)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 18)
        Me.Button4.TabIndex = 2211
        Me.Button4.TabStop = False
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(18, 186)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(82, 13)
        Me.Label14.TabIndex = 2212
        Me.Label14.Text = "Finance App By"
        '
        'financeappby
        '
        Me.financeappby.Location = New System.Drawing.Point(102, 181)
        Me.financeappby.MaxLength = 5
        Me.financeappby.Name = "financeappby"
        Me.financeappby.ReadOnly = True
        Me.financeappby.Size = New System.Drawing.Size(139, 20)
        Me.financeappby.TabIndex = 2208
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(395, 186)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(93, 13)
        Me.Label13.TabIndex = 2210
        Me.Label13.Text = "Finance App Date"
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(505, 19)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(84, 20)
        Me.Status.TabIndex = 2206
        Me.Status.Text = "Open"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(395, 27)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2207
        Me.Label15.Text = "Status"
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(276, 155)
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
        Me.Button3.Location = New System.Drawing.Point(249, 157)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 2203
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(18, 161)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2204
        Me.Label12.Text = "Approved By"
        '
        'approvedby
        '
        Me.approvedby.Location = New System.Drawing.Point(102, 155)
        Me.approvedby.MaxLength = 5
        Me.approvedby.Name = "approvedby"
        Me.approvedby.ReadOnly = True
        Me.approvedby.Size = New System.Drawing.Size(139, 20)
        Me.approvedby.TabIndex = 2200
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(395, 161)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2202
        Me.Label5.Text = "Approved Date"
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(395, 137)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2199
        Me.crtdttext.Text = "Created Date"
        '
        'CTCrt
        '
        Me.CTCrt.Location = New System.Drawing.Point(276, 131)
        Me.CTCrt.MaxLength = 1
        Me.CTCrt.Name = "CTCrt"
        Me.CTCrt.ReadOnly = True
        Me.CTCrt.Size = New System.Drawing.Size(41, 20)
        Me.CTCrt.TabIndex = 2198
        Me.CTCrt.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(505, 130)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(68, 20)
        Me.crtdt.TabIndex = 2197
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(18, 137)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2196
        Me.crtt.Text = "Created By"
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(102, 130)
        Me.crt.MaxLength = 5
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 2195
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = True
        Me.lblCityName.Location = New System.Drawing.Point(179, 24)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(0, 13)
        Me.lblCityName.TabIndex = 2194
        '
        'btnSearchCity
        '
        Me.btnSearchCity.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearchCity.Location = New System.Drawing.Point(151, 20)
        Me.btnSearchCity.Name = "btnSearchCity"
        Me.btnSearchCity.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCity.TabIndex = 2192
        Me.btnSearchCity.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(18, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(57, 13)
        Me.Label10.TabIndex = 2193
        Me.Label10.Text = "Printed On"
        '
        'txtCity_Code
        '
        Me.txtCity_Code.Location = New System.Drawing.Point(102, 19)
        Me.txtCity_Code.MaxLength = 5
        Me.txtCity_Code.Name = "txtCity_Code"
        Me.txtCity_Code.Size = New System.Drawing.Size(43, 20)
        Me.txtCity_Code.TabIndex = 2191
        '
        'DTPrinted
        '
        Me.DTPrinted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPrinted.Location = New System.Drawing.Point(102, 44)
        Me.DTPrinted.Name = "DTPrinted"
        Me.DTPrinted.Size = New System.Drawing.Size(91, 20)
        Me.DTPrinted.TabIndex = 2183
        Me.DTPrinted.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 2184
        Me.Label1.Text = "Printed Date"
        '
        'FrmPV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(656, 254)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmPV"
        Me.Text = "FrmPV"
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
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents FinDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents AppDT As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnTTd As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtCompanyCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
End Class
