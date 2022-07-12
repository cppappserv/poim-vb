<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMCI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMCI))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnReject = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.docaddress = New System.Windows.Forms.RichTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.footer = New System.Windows.Forms.RichTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Button6 = New System.Windows.Forms.Button
        Me.Status = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtIssued = New System.Windows.Forms.DateTimePicker
        Me.Label9 = New System.Windows.Forms.Label
        Me.reffNo = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbDG = New System.Windows.Forms.ComboBox
        Me.crtdate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.crtby = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.crtname = New System.Windows.Forms.TextBox
        Me.appdate = New System.Windows.Forms.DateTimePicker
        Me.btnClearD = New System.Windows.Forms.Button
        Me.req = New System.Windows.Forms.RichTextBox
        Me.appcode = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.te = New System.Windows.Forms.Label
        Me.appname = New System.Windows.Forms.TextBox
        Me.tex = New System.Windows.Forms.Label
        Me.tgl = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
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
        Me.ToolStrip1.Size = New System.Drawing.Size(870, 25)
        Me.ToolStrip1.TabIndex = 6
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
        Me.GroupBox1.Controls.Add(Me.Button5)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.docaddress)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.footer)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Button6)
        Me.GroupBox1.Controls.Add(Me.Status)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.dtIssued)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.reffNo)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbDG)
        Me.GroupBox1.Controls.Add(Me.crtdate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.crtby)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.crtname)
        Me.GroupBox1.Controls.Add(Me.appdate)
        Me.GroupBox1.Controls.Add(Me.btnClearD)
        Me.GroupBox1.Controls.Add(Me.req)
        Me.GroupBox1.Controls.Add(Me.appcode)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.te)
        Me.GroupBox1.Controls.Add(Me.appname)
        Me.GroupBox1.Controls.Add(Me.tex)
        Me.GroupBox1.Controls.Add(Me.tgl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(851, 229)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'Button5
        '
        Me.Button5.Image = Global.POIM.My.Resources.Resources.deleteS1
        Me.Button5.Location = New System.Drawing.Point(825, 64)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(22, 22)
        Me.Button5.TabIndex = 14
        Me.Button5.TabStop = False
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = Global.POIM.My.Resources.Resources.history
        Me.Button1.Location = New System.Drawing.Point(825, 42)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 22)
        Me.Button1.TabIndex = 13
        Me.Button1.TabStop = False
        Me.Button1.UseVisualStyleBackColor = True
        '
        'docaddress
        '
        Me.docaddress.Location = New System.Drawing.Point(539, 44)
        Me.docaddress.Name = "docaddress"
        Me.docaddress.Size = New System.Drawing.Size(281, 58)
        Me.docaddress.TabIndex = 12
        Me.docaddress.Text = ""
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(428, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(97, 13)
        Me.Label10.TabIndex = 2210
        Me.Label10.Text = "Document Address"
        '
        'Button2
        '
        Me.Button2.Image = Global.POIM.My.Resources.Resources.deleteS1
        Me.Button2.Location = New System.Drawing.Point(389, 132)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 22)
        Me.Button2.TabIndex = 7
        Me.Button2.TabStop = False
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Image = Global.POIM.My.Resources.Resources.history
        Me.Button4.Location = New System.Drawing.Point(389, 110)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(22, 22)
        Me.Button4.TabIndex = 6
        Me.Button4.TabStop = False
        Me.Button4.UseVisualStyleBackColor = True
        '
        'footer
        '
        Me.footer.Location = New System.Drawing.Point(103, 110)
        Me.footer.Name = "footer"
        Me.footer.Size = New System.Drawing.Size(281, 56)
        Me.footer.TabIndex = 5
        Me.footer.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 2206
        Me.Label3.Text = "Footer Note"
        '
        'Button6
        '
        Me.Button6.Image = Global.POIM.My.Resources.Resources.history
        Me.Button6.Location = New System.Drawing.Point(389, 44)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(22, 22)
        Me.Button6.TabIndex = 3
        Me.Button6.TabStop = False
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Status
        '
        Me.Status.Location = New System.Drawing.Point(539, 19)
        Me.Status.MaxLength = 5
        Me.Status.Name = "Status"
        Me.Status.ReadOnly = True
        Me.Status.Size = New System.Drawing.Size(91, 20)
        Me.Status.TabIndex = 11
        Me.Status.Text = "Open"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(426, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 2202
        Me.Label7.Text = "Status"
        '
        'dtIssued
        '
        Me.dtIssued.Checked = False
        Me.dtIssued.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtIssued.Location = New System.Drawing.Point(539, 152)
        Me.dtIssued.Name = "dtIssued"
        Me.dtIssued.ShowCheckBox = True
        Me.dtIssued.Size = New System.Drawing.Size(100, 20)
        Me.dtIssued.TabIndex = 17
        Me.dtIssued.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(426, 154)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 2200
        Me.Label9.Text = "Issued Date"
        '
        'reffNo
        '
        Me.reffNo.Location = New System.Drawing.Point(539, 130)
        Me.reffNo.MaxLength = 40
        Me.reffNo.Name = "reffNo"
        Me.reffNo.Size = New System.Drawing.Size(158, 20)
        Me.reffNo.TabIndex = 16
        Me.reffNo.Tag = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(426, 132)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 2198
        Me.Label8.Text = "Reference No."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(426, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 2196
        Me.Label6.Text = "Print Doc.Group"
        '
        'cbDG
        '
        Me.cbDG.FormattingEnabled = True
        Me.cbDG.Items.AddRange(New Object() {"MCI Charter", "MCI Container"})
        Me.cbDG.Location = New System.Drawing.Point(539, 106)
        Me.cbDG.Name = "cbDG"
        Me.cbDG.Size = New System.Drawing.Size(158, 21)
        Me.cbDG.TabIndex = 15
        Me.cbDG.Text = "MCI Charter"
        '
        'crtdate
        '
        Me.crtdate.Enabled = False
        Me.crtdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.crtdate.Location = New System.Drawing.Point(539, 175)
        Me.crtdate.Name = "crtdate"
        Me.crtdate.Size = New System.Drawing.Size(100, 20)
        Me.crtdate.TabIndex = 18
        Me.crtdate.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(426, 178)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 2194
        Me.Label5.Text = "Created Date"
        '
        'crtby
        '
        Me.crtby.Location = New System.Drawing.Point(278, 175)
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
        Me.Label4.Location = New System.Drawing.Point(13, 178)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 2192
        Me.Label4.Text = "Created By"
        '
        'crtname
        '
        Me.crtname.Location = New System.Drawing.Point(103, 175)
        Me.crtname.MaxLength = 5
        Me.crtname.Name = "crtname"
        Me.crtname.ReadOnly = True
        Me.crtname.Size = New System.Drawing.Size(139, 20)
        Me.crtname.TabIndex = 8
        '
        'appdate
        '
        Me.appdate.Checked = False
        Me.appdate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.appdate.Location = New System.Drawing.Point(539, 201)
        Me.appdate.Name = "appdate"
        Me.appdate.ShowCheckBox = True
        Me.appdate.Size = New System.Drawing.Size(100, 20)
        Me.appdate.TabIndex = 19
        Me.appdate.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'btnClearD
        '
        Me.btnClearD.Image = Global.POIM.My.Resources.Resources.deleteS1
        Me.btnClearD.Location = New System.Drawing.Point(389, 66)
        Me.btnClearD.Name = "btnClearD"
        Me.btnClearD.Size = New System.Drawing.Size(22, 22)
        Me.btnClearD.TabIndex = 4
        Me.btnClearD.TabStop = False
        Me.btnClearD.UseVisualStyleBackColor = True
        '
        'req
        '
        Me.req.CausesValidation = False
        Me.req.Location = New System.Drawing.Point(103, 46)
        Me.req.Name = "req"
        Me.req.Size = New System.Drawing.Size(281, 58)
        Me.req.TabIndex = 2
        Me.req.Text = ""
        '
        'appcode
        '
        Me.appcode.Location = New System.Drawing.Point(278, 198)
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
        Me.Button3.Location = New System.Drawing.Point(250, 198)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 18)
        Me.Button3.TabIndex = 10
        Me.Button3.TabStop = False
        Me.Button3.UseVisualStyleBackColor = True
        '
        'te
        '
        Me.te.AutoSize = True
        Me.te.Location = New System.Drawing.Point(13, 201)
        Me.te.Name = "te"
        Me.te.Size = New System.Drawing.Size(68, 13)
        Me.te.TabIndex = 2166
        Me.te.Text = "Approved By"
        '
        'appname
        '
        Me.appname.Location = New System.Drawing.Point(103, 198)
        Me.appname.MaxLength = 5
        Me.appname.Name = "appname"
        Me.appname.ReadOnly = True
        Me.appname.Size = New System.Drawing.Size(139, 20)
        Me.appname.TabIndex = 9
        '
        'tex
        '
        Me.tex.AutoSize = True
        Me.tex.Location = New System.Drawing.Point(426, 201)
        Me.tex.Name = "tex"
        Me.tex.Size = New System.Drawing.Size(79, 13)
        Me.tex.TabIndex = 2151
        Me.tex.Text = "Approved Date"
        '
        'tgl
        '
        Me.tgl.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.tgl.Location = New System.Drawing.Point(103, 22)
        Me.tgl.Name = "tgl"
        Me.tgl.Size = New System.Drawing.Size(91, 20)
        Me.tgl.TabIndex = 1
        Me.tgl.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Printed Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Survey Req."
        '
        'FrmMCI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 266)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.HelpButton = True
        Me.Name = "FrmMCI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MCI"
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
    Friend WithEvents tgl As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tex As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents te As System.Windows.Forms.Label
    Friend WithEvents appname As System.Windows.Forms.TextBox
    Friend WithEvents appcode As System.Windows.Forms.TextBox
    Friend WithEvents req As System.Windows.Forms.RichTextBox
    Friend WithEvents btnClearD As System.Windows.Forms.Button
    Friend WithEvents appdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents crtdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents crtby As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents crtname As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbDG As System.Windows.Forms.ComboBox
    Friend WithEvents dtIssued As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents reffNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents footer As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents docaddress As System.Windows.Forms.RichTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
