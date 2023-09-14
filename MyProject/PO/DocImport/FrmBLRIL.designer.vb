<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBLRIL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBLRIL))
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
        Me.txtbea0 = New System.Windows.Forms.GroupBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtLeadtime = New System.Windows.Forms.TextBox
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtDeptanNo = New System.Windows.Forms.TextBox
        Me.dtSubmited = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.dtIssued = New System.Windows.Forms.DateTimePicker
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbDG = New System.Windows.Forms.ComboBox
        Me.crtdate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.crtby = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.crtname = New System.Windows.Forms.TextBox
        Me.no = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.appdate = New System.Windows.Forms.DateTimePicker
        Me.btnClearD = New System.Windows.Forms.Button
        Me.detail = New System.Windows.Forms.RichTextBox
        Me.doc_no = New System.Windows.Forms.RichTextBox
        Me.doc_text = New System.Windows.Forms.RichTextBox
        Me.appcode = New System.Windows.Forms.TextBox
        Me.Grid = New System.Windows.Forms.DataGridView
        Me.Button3 = New System.Windows.Forms.Button
        Me.te = New System.Windows.Forms.Label
        Me.appname = New System.Windows.Forms.TextBox
        Me.tex = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.tgl = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.txtbea0.SuspendLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnReject, Me.ToolStripSeparator4, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(746, 25)
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
        Me.btnPrint.Size = New System.Drawing.Size(49, 22)
        Me.btnPrint.Text = "Print"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtbea0)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbDG)
        Me.GroupBox1.Controls.Add(Me.crtdate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.crtby)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.crtname)
        Me.GroupBox1.Controls.Add(Me.no)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.appdate)
        Me.GroupBox1.Controls.Add(Me.btnClearD)
        Me.GroupBox1.Controls.Add(Me.detail)
        Me.GroupBox1.Controls.Add(Me.doc_no)
        Me.GroupBox1.Controls.Add(Me.doc_text)
        Me.GroupBox1.Controls.Add(Me.appcode)
        Me.GroupBox1.Controls.Add(Me.Grid)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.te)
        Me.GroupBox1.Controls.Add(Me.appname)
        Me.GroupBox1.Controls.Add(Me.tex)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.tgl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(839, 387)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'txtbea0
        '
        Me.txtbea0.Controls.Add(Me.Label14)
        Me.txtbea0.Controls.Add(Me.Label12)
        Me.txtbea0.Controls.Add(Me.Label13)
        Me.txtbea0.Controls.Add(Me.txtLeadtime)
        Me.txtbea0.Controls.Add(Me.txtRemark)
        Me.txtbea0.Controls.Add(Me.Label11)
        Me.txtbea0.Controls.Add(Me.txtDeptanNo)
        Me.txtbea0.Controls.Add(Me.dtSubmited)
        Me.txtbea0.Controls.Add(Me.Label1)
        Me.txtbea0.Controls.Add(Me.Label10)
        Me.txtbea0.Controls.Add(Me.Label15)
        Me.txtbea0.Controls.Add(Me.dtIssued)
        Me.txtbea0.Location = New System.Drawing.Point(16, 92)
        Me.txtbea0.Name = "txtbea0"
        Me.txtbea0.Size = New System.Drawing.Size(705, 122)
        Me.txtbea0.TabIndex = 2293
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
        Me.Label13.Location = New System.Drawing.Point(391, 20)
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
        Me.txtRemark.Location = New System.Drawing.Point(484, 18)
        Me.txtRemark.MaxLength = 2147483647
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(215, 90)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 2198
        Me.Label1.Text = "Deptan No."
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
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(13, 67)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 13)
        Me.Label15.TabIndex = 2200
        Me.Label15.Text = "Issued Date"
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
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(597, 19)
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
        Me.Label7.Location = New System.Drawing.Point(503, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 2202
        Me.Label7.Text = "Status"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 13)
        Me.Label6.TabIndex = 2196
        Me.Label6.Text = "Document Group"
        '
        'cbDG
        '
        Me.cbDG.FormattingEnabled = True
        Me.cbDG.Location = New System.Drawing.Point(122, 69)
        Me.cbDG.Name = "cbDG"
        Me.cbDG.Size = New System.Drawing.Size(250, 21)
        Me.cbDG.TabIndex = 2195
        '
        'crtdate
        '
        Me.crtdate.Enabled = False
        Me.crtdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.crtdate.Location = New System.Drawing.Point(597, 222)
        Me.crtdate.Name = "crtdate"
        Me.crtdate.Size = New System.Drawing.Size(100, 20)
        Me.crtdate.TabIndex = 8
        Me.crtdate.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(503, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 2194
        Me.Label5.Text = "Created Date"
        '
        'crtby
        '
        Me.crtby.Location = New System.Drawing.Point(297, 222)
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
        Me.Label4.Location = New System.Drawing.Point(13, 224)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 2192
        Me.Label4.Text = "Created By"
        '
        'crtname
        '
        Me.crtname.Location = New System.Drawing.Point(122, 222)
        Me.crtname.MaxLength = 5
        Me.crtname.Name = "crtname"
        Me.crtname.ReadOnly = True
        Me.crtname.Size = New System.Drawing.Size(139, 20)
        Me.crtname.TabIndex = 6
        '
        'no
        '
        Me.no.Location = New System.Drawing.Point(122, 19)
        Me.no.MaxLength = 40
        Me.no.Name = "no"
        Me.no.Size = New System.Drawing.Size(250, 20)
        Me.no.TabIndex = 0
        Me.no.Tag = ""
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
        Me.appdate.Location = New System.Drawing.Point(597, 245)
        Me.appdate.Name = "appdate"
        Me.appdate.ShowCheckBox = True
        Me.appdate.Size = New System.Drawing.Size(100, 20)
        Me.appdate.TabIndex = 12
        Me.appdate.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'btnClearD
        '
        Me.btnClearD.Image = Global.poim.My.Resources.Resources.deleteS1
        Me.btnClearD.Location = New System.Drawing.Point(357, 301)
        Me.btnClearD.Name = "btnClearD"
        Me.btnClearD.Size = New System.Drawing.Size(22, 22)
        Me.btnClearD.TabIndex = 5
        Me.btnClearD.TabStop = False
        Me.btnClearD.UseVisualStyleBackColor = True
        Me.btnClearD.Visible = False
        '
        'detail
        '
        Me.detail.Location = New System.Drawing.Point(409, 274)
        Me.detail.Name = "detail"
        Me.detail.Size = New System.Drawing.Size(81, 38)
        Me.detail.TabIndex = 2186
        Me.detail.Text = ""
        Me.detail.Visible = False
        '
        'doc_no
        '
        Me.doc_no.Location = New System.Drawing.Point(16, 310)
        Me.doc_no.Name = "doc_no"
        Me.doc_no.Size = New System.Drawing.Size(56, 28)
        Me.doc_no.TabIndex = 2185
        Me.doc_no.Text = "RL000"
        Me.doc_no.Visible = False
        '
        'doc_text
        '
        Me.doc_text.Location = New System.Drawing.Point(103, 283)
        Me.doc_text.Name = "doc_text"
        Me.doc_text.ReadOnly = True
        Me.doc_text.Size = New System.Drawing.Size(250, 55)
        Me.doc_text.TabIndex = 3
        Me.doc_text.Text = ""
        Me.doc_text.Visible = False
        '
        'appcode
        '
        Me.appcode.Location = New System.Drawing.Point(297, 245)
        Me.appcode.MaxLength = 1
        Me.appcode.Name = "appcode"
        Me.appcode.ReadOnly = True
        Me.appcode.Size = New System.Drawing.Size(41, 20)
        Me.appcode.TabIndex = 11
        Me.appcode.Visible = False
        '
        'Grid
        '
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToDeleteRows = False
        Me.Grid.AllowUserToResizeRows = False
        Me.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.NullValue = Nothing
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grid.DefaultCellStyle = DataGridViewCellStyle1
        Me.Grid.Location = New System.Drawing.Point(16, 274)
        Me.Grid.Name = "Grid"
        Me.Grid.ShowCellErrors = False
        Me.Grid.Size = New System.Drawing.Size(705, 105)
        Me.Grid.TabIndex = 13
        '
        'Button3
        '
        Me.Button3.Image = Global.poim.My.Resources.Resources.search
        Me.Button3.Location = New System.Drawing.Point(265, 245)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 10
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'te
        '
        Me.te.AutoSize = True
        Me.te.Location = New System.Drawing.Point(13, 247)
        Me.te.Name = "te"
        Me.te.Size = New System.Drawing.Size(68, 13)
        Me.te.TabIndex = 2166
        Me.te.Text = "Approved By"
        '
        'appname
        '
        Me.appname.Location = New System.Drawing.Point(122, 245)
        Me.appname.MaxLength = 5
        Me.appname.Name = "appname"
        Me.appname.ReadOnly = True
        Me.appname.Size = New System.Drawing.Size(139, 20)
        Me.appname.TabIndex = 9
        '
        'tex
        '
        Me.tex.AutoSize = True
        Me.tex.Location = New System.Drawing.Point(503, 247)
        Me.tex.Name = "tex"
        Me.tex.Size = New System.Drawing.Size(79, 13)
        Me.tex.TabIndex = 2151
        Me.tex.Text = "Approved Date"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(357, 277)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(22, 18)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.TabStop = False
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'tgl
        '
        Me.tgl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl.Location = New System.Drawing.Point(122, 43)
        Me.tgl.Name = "tgl"
        Me.tgl.Size = New System.Drawing.Size(91, 20)
        Me.tgl.TabIndex = 1
        Me.tgl.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Date"
        '
        'FrmBLRIL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 429)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.HelpButton = True
        Me.Name = "FrmBLRIL"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "BL Request Import Licence"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.txtbea0.ResumeLayout(False)
        Me.txtbea0.PerformLayout()
        CType(Me.Grid, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tex As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents te As System.Windows.Forms.Label
    Friend WithEvents appname As System.Windows.Forms.TextBox
    Friend WithEvents appcode As System.Windows.Forms.TextBox
    Public WithEvents Grid As System.Windows.Forms.DataGridView
    Friend WithEvents doc_no As System.Windows.Forms.RichTextBox
    Friend WithEvents doc_text As System.Windows.Forms.RichTextBox
    Friend WithEvents detail As System.Windows.Forms.RichTextBox
    Friend WithEvents btnClearD As System.Windows.Forms.Button
    Friend WithEvents appdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents no As System.Windows.Forms.TextBox
    Friend WithEvents crtdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents crtby As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents crtname As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbDG As System.Windows.Forms.ComboBox
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtbea0 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtLeadtime As System.Windows.Forms.TextBox
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDeptanNo As System.Windows.Forms.TextBox
    Friend WithEvents dtSubmited As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents dtIssued As System.Windows.Forms.DateTimePicker
End Class
