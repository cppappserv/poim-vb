<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCL))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.crt = New System.Windows.Forms.TextBox
        Me.approvedby = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnSearchDoc = New System.Windows.Forms.Button
        Me.dgvCL = New System.Windows.Forms.DataGridView
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DocCode = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnDoc = New System.Windows.Forms.DataGridViewButtonColumn
        Me.DocName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DocNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DocDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Remark = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AppDt = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtAuthorized = New System.Windows.Forms.TextBox
        Me.lblExp = New System.Windows.Forms.Label
        Me.btnSearchExp = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtExp = New System.Windows.Forms.TextBox
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.CTApp = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.crtdttext = New System.Windows.Forms.Label
        Me.CTCrt = New System.Windows.Forms.TextBox
        Me.crtdt = New System.Windows.Forms.TextBox
        Me.crtt = New System.Windows.Forms.Label
        Me.lblCityName = New System.Windows.Forms.Label
        Me.btnSearchCity = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCity_Code = New System.Windows.Forms.TextBox
        Me.DTPrinted = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvCL, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(779, 25)
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
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "Print"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.crt)
        Me.GroupBox1.Controls.Add(Me.approvedby)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.btnSearchDoc)
        Me.GroupBox1.Controls.Add(Me.dgvCL)
        Me.GroupBox1.Controls.Add(Me.AppDt)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtTitle)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtAuthorized)
        Me.GroupBox1.Controls.Add(Me.lblExp)
        Me.GroupBox1.Controls.Add(Me.btnSearchExp)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtExp)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.CTApp)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.crtdttext)
        Me.GroupBox1.Controls.Add(Me.CTCrt)
        Me.GroupBox1.Controls.Add(Me.crtdt)
        Me.GroupBox1.Controls.Add(Me.crtt)
        Me.GroupBox1.Controls.Add(Me.lblCityName)
        Me.GroupBox1.Controls.Add(Me.btnSearchCity)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtCity_Code)
        Me.GroupBox1.Controls.Add(Me.DTPrinted)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(755, 445)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'crt
        '
        Me.crt.Location = New System.Drawing.Point(114, 388)
        Me.crt.MaxLength = 0
        Me.crt.Name = "crt"
        Me.crt.ReadOnly = True
        Me.crt.Size = New System.Drawing.Size(139, 20)
        Me.crt.TabIndex = 2228
        '
        'approvedby
        '
        Me.approvedby.Location = New System.Drawing.Point(114, 414)
        Me.approvedby.MaxLength = 0
        Me.approvedby.Name = "approvedby"
        Me.approvedby.ReadOnly = True
        Me.approvedby.Size = New System.Drawing.Size(139, 20)
        Me.approvedby.TabIndex = 2227
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(336, 126)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 30)
        Me.Button1.TabIndex = 2225
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'btnSearchDoc
        '
        Me.btnSearchDoc.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearchDoc.Location = New System.Drawing.Point(430, 78)
        Me.btnSearchDoc.Name = "btnSearchDoc"
        Me.btnSearchDoc.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchDoc.TabIndex = 2224
        Me.btnSearchDoc.UseVisualStyleBackColor = True
        Me.btnSearchDoc.Visible = False
        '
        'dgvCL
        '
        Me.dgvCL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCL.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.DocCode, Me.btnDoc, Me.DocName, Me.DocNo, Me.DocDate, Me.Remark})
        Me.dgvCL.Location = New System.Drawing.Point(21, 156)
        Me.dgvCL.Name = "dgvCL"
        Me.dgvCL.Size = New System.Drawing.Size(714, 216)
        Me.dgvCL.TabIndex = 2223
        '
        'No
        '
        Me.No.DataPropertyName = "no"
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.Width = 30
        '
        'DocCode
        '
        Me.DocCode.DataPropertyName = "DocCode"
        Me.DocCode.HeaderText = "DocCode"
        Me.DocCode.MaxInputLength = 5
        Me.DocCode.Name = "DocCode"
        Me.DocCode.Width = 70
        '
        'btnDoc
        '
        Me.btnDoc.HeaderText = "Doc"
        Me.btnDoc.Name = "btnDoc"
        Me.btnDoc.Width = 30
        '
        'DocName
        '
        Me.DocName.DataPropertyName = "DocName"
        Me.DocName.HeaderText = "DocName"
        Me.DocName.Name = "DocName"
        Me.DocName.ReadOnly = True
        Me.DocName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DocName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DocName.Width = 140
        '
        'DocNo
        '
        Me.DocNo.DataPropertyName = "DocNo"
        Me.DocNo.HeaderText = "DocNo"
        Me.DocNo.MaxInputLength = 40
        Me.DocNo.Name = "DocNo"
        '
        'DocDate
        '
        Me.DocDate.DataPropertyName = "DocDate"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.DocDate.DefaultCellStyle = DataGridViewCellStyle1
        Me.DocDate.HeaderText = "DocDate"
        Me.DocDate.MaxInputLength = 10
        Me.DocDate.Name = "DocDate"
        Me.DocDate.Width = 80
        '
        'Remark
        '
        Me.Remark.DataPropertyName = "Remark"
        Me.Remark.HeaderText = "Remark"
        Me.Remark.MaxInputLength = 200
        Me.Remark.Name = "Remark"
        Me.Remark.Width = 200
        '
        'AppDt
        '
        Me.AppDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AppDt.Location = New System.Drawing.Point(546, 410)
        Me.AppDt.Name = "AppDt"
        Me.AppDt.ShowCheckBox = True
        Me.AppDt.Size = New System.Drawing.Size(103, 20)
        Me.AppDt.TabIndex = 2222
        Me.AppDt.Value = New Date(2009, 1, 27, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 2221
        Me.Label2.Text = "Authorized Title"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(114, 119)
        Me.txtTitle.MaxLength = 0
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.ReadOnly = True
        Me.txtTitle.Size = New System.Drawing.Size(139, 20)
        Me.txtTitle.TabIndex = 2220
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 2219
        Me.Label4.Text = "Authorized Person"
        '
        'txtAuthorized
        '
        Me.txtAuthorized.Location = New System.Drawing.Point(114, 95)
        Me.txtAuthorized.MaxLength = 5
        Me.txtAuthorized.Name = "txtAuthorized"
        Me.txtAuthorized.ReadOnly = True
        Me.txtAuthorized.Size = New System.Drawing.Size(139, 20)
        Me.txtAuthorized.TabIndex = 2218
        '
        'lblExp
        '
        Me.lblExp.AutoSize = True
        Me.lblExp.Location = New System.Drawing.Point(239, 77)
        Me.lblExp.Name = "lblExp"
        Me.lblExp.Size = New System.Drawing.Size(110, 13)
        Me.lblExp.TabIndex = 2217
        Me.lblExp.Text = "Company Descrtiption"
        '
        'btnSearchExp
        '
        Me.btnSearchExp.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearchExp.Location = New System.Drawing.Point(211, 70)
        Me.btnSearchExp.Name = "btnSearchExp"
        Me.btnSearchExp.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchExp.TabIndex = 2215
        Me.btnSearchExp.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 2216
        Me.Label3.Text = "Expedition"
        '
        'txtExp
        '
        Me.txtExp.Location = New System.Drawing.Point(114, 70)
        Me.txtExp.MaxLength = 5
        Me.txtExp.Name = "txtExp"
        Me.txtExp.Size = New System.Drawing.Size(91, 20)
        Me.txtExp.TabIndex = 2214
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(546, 18)
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
        Me.Label15.Location = New System.Drawing.Point(436, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 2207
        Me.Label15.Text = "Status"
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(287, 414)
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
        Me.Button3.Location = New System.Drawing.Point(259, 414)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 2203
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(18, 418)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(68, 13)
        Me.Label12.TabIndex = 2204
        Me.Label12.Text = "Approved By"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(436, 417)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 2202
        Me.Label5.Text = "Approved Date"
        '
        'crtdttext
        '
        Me.crtdttext.AutoSize = True
        Me.crtdttext.Location = New System.Drawing.Point(436, 393)
        Me.crtdttext.Name = "crtdttext"
        Me.crtdttext.Size = New System.Drawing.Size(70, 13)
        Me.crtdttext.TabIndex = 2199
        Me.crtdttext.Text = "Created Date"
        '
        'CTCrt
        '
        Me.CTCrt.Location = New System.Drawing.Point(287, 388)
        Me.CTCrt.MaxLength = 1
        Me.CTCrt.Name = "CTCrt"
        Me.CTCrt.ReadOnly = True
        Me.CTCrt.Size = New System.Drawing.Size(41, 20)
        Me.CTCrt.TabIndex = 2198
        Me.CTCrt.Visible = False
        '
        'crtdt
        '
        Me.crtdt.Location = New System.Drawing.Point(546, 386)
        Me.crtdt.MaxLength = 5
        Me.crtdt.Name = "crtdt"
        Me.crtdt.ReadOnly = True
        Me.crtdt.Size = New System.Drawing.Size(69, 20)
        Me.crtdt.TabIndex = 2197
        '
        'crtt
        '
        Me.crtt.AutoSize = True
        Me.crtt.Location = New System.Drawing.Point(18, 394)
        Me.crtt.Name = "crtt"
        Me.crtt.Size = New System.Drawing.Size(59, 13)
        Me.crtt.TabIndex = 2196
        Me.crtt.Text = "Created By"
        '
        'lblCityName
        '
        Me.lblCityName.AutoSize = True
        Me.lblCityName.Location = New System.Drawing.Point(239, 22)
        Me.lblCityName.Name = "lblCityName"
        Me.lblCityName.Size = New System.Drawing.Size(80, 13)
        Me.lblCityName.TabIndex = 2194
        Me.lblCityName.Text = "City Description"
        '
        'btnSearchCity
        '
        Me.btnSearchCity.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearchCity.Location = New System.Drawing.Point(211, 18)
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
        Me.txtCity_Code.Location = New System.Drawing.Point(114, 19)
        Me.txtCity_Code.MaxLength = 5
        Me.txtCity_Code.Name = "txtCity_Code"
        Me.txtCity_Code.Size = New System.Drawing.Size(91, 20)
        Me.txtCity_Code.TabIndex = 2191
        '
        'DTPrinted
        '
        Me.DTPrinted.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPrinted.Location = New System.Drawing.Point(114, 44)
        Me.DTPrinted.Name = "DTPrinted"
        Me.DTPrinted.Size = New System.Drawing.Size(91, 20)
        Me.DTPrinted.TabIndex = 2183
        Me.DTPrinted.Value = New Date(2009, 1, 27, 0, 0, 0, 0)
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
        'FrmCL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(779, 485)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmCL"
        Me.Text = "FrmCL"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvCL, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents crtdttext As System.Windows.Forms.Label
    Friend WithEvents CTApp As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtAuthorized As System.Windows.Forms.TextBox
    Friend WithEvents lblExp As System.Windows.Forms.Label
    Friend WithEvents btnSearchExp As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtExp As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents AppDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgvCL As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearchDoc As System.Windows.Forms.Button
    Friend WithEvents No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnDoc As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents DocName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents approvedby As System.Windows.Forms.TextBox
    Friend WithEvents crt As System.Windows.Forms.TextBox
End Class
