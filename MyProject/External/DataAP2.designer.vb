<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataAP2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataAP2))
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtFolder_Name1 = New System.Windows.Forms.TextBox
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnProcess = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.dt2_3 = New System.Windows.Forms.DateTimePicker
        Me.txtFolder_Name2_4 = New System.Windows.Forms.TextBox
        Me.txtFolder_Name2_3 = New System.Windows.Forms.TextBox
        Me.listError = New System.Windows.Forms.ListBox
        Me.txtFolder_Name2_2 = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnPlant = New System.Windows.Forms.Button
        Me.TxtPlantCode = New System.Windows.Forms.TextBox
        Me.TxtPlantName = New System.Windows.Forms.TextBox
        Me.dt2_1 = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.dt2_2 = New System.Windows.Forms.DateTimePicker
        Me.txtFolder_Name2_1 = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnPlant2 = New System.Windows.Forms.Button
        Me.TxtPlantCode2 = New System.Windows.Forms.TextBox
        Me.TxtPlantName2 = New System.Windows.Forms.TextBox
        Me.dt1 = New System.Windows.Forms.DateTimePicker
        Me.txtID = New System.Windows.Forms.TextBox
        Me.lblProcess = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Destination File"
        '
        'txtFolder_Name1
        '
        Me.txtFolder_Name1.ForeColor = System.Drawing.Color.Red
        Me.txtFolder_Name1.Location = New System.Drawing.Point(146, 65)
        Me.txtFolder_Name1.MaxLength = 215
        Me.txtFolder_Name1.Name = "txtFolder_Name1"
        Me.txtFolder_Name1.ReadOnly = True
        Me.txtFolder_Name1.Size = New System.Drawing.Size(294, 20)
        Me.txtFolder_Name1.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnProcess})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(491, 25)
        Me.ToolStrip1.TabIndex = 1
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnProcess
        '
        Me.btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), System.Drawing.Image)
        Me.btnProcess.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(64, 22)
        Me.btnProcess.Text = "Process"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 49
        Me.Label1.Text = "Process Date"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 62)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(494, 289)
        Me.TabControl1.TabIndex = 50
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage1.Controls.Add(Me.dt2_3)
        Me.TabPage1.Controls.Add(Me.txtFolder_Name2_4)
        Me.TabPage1.Controls.Add(Me.txtFolder_Name2_3)
        Me.TabPage1.Controls.Add(Me.listError)
        Me.TabPage1.Controls.Add(Me.txtFolder_Name2_2)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.btnPlant)
        Me.TabPage1.Controls.Add(Me.TxtPlantCode)
        Me.TabPage1.Controls.Add(Me.TxtPlantName)
        Me.TabPage1.Controls.Add(Me.dt2_1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.dt2_2)
        Me.TabPage1.Controls.Add(Me.txtFolder_Name2_1)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(486, 263)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Transaction"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dt2_3
        '
        Me.dt2_3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt2_3.Location = New System.Drawing.Point(146, 0)
        Me.dt2_3.Name = "dt2_3"
        Me.dt2_3.Size = New System.Drawing.Size(91, 20)
        Me.dt2_3.TabIndex = 2264
        Me.dt2_3.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        Me.dt2_3.Visible = False
        '
        'txtFolder_Name2_4
        '
        Me.txtFolder_Name2_4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFolder_Name2_4.ForeColor = System.Drawing.Color.Red
        Me.txtFolder_Name2_4.Location = New System.Drawing.Point(146, 131)
        Me.txtFolder_Name2_4.MaxLength = 215
        Me.txtFolder_Name2_4.Name = "txtFolder_Name2_4"
        Me.txtFolder_Name2_4.ReadOnly = True
        Me.txtFolder_Name2_4.Size = New System.Drawing.Size(294, 13)
        Me.txtFolder_Name2_4.TabIndex = 2263
        '
        'txtFolder_Name2_3
        '
        Me.txtFolder_Name2_3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFolder_Name2_3.ForeColor = System.Drawing.Color.Red
        Me.txtFolder_Name2_3.Location = New System.Drawing.Point(146, 89)
        Me.txtFolder_Name2_3.MaxLength = 215
        Me.txtFolder_Name2_3.Name = "txtFolder_Name2_3"
        Me.txtFolder_Name2_3.ReadOnly = True
        Me.txtFolder_Name2_3.Size = New System.Drawing.Size(294, 13)
        Me.txtFolder_Name2_3.TabIndex = 2262
        '
        'listError
        '
        Me.listError.ForeColor = System.Drawing.Color.Red
        Me.listError.FormattingEnabled = True
        Me.listError.HorizontalScrollbar = True
        Me.listError.Location = New System.Drawing.Point(23, 156)
        Me.listError.Name = "listError"
        Me.listError.Size = New System.Drawing.Size(440, 95)
        Me.listError.TabIndex = 2240
        Me.listError.Visible = False
        '
        'txtFolder_Name2_2
        '
        Me.txtFolder_Name2_2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFolder_Name2_2.ForeColor = System.Drawing.Color.Red
        Me.txtFolder_Name2_2.Location = New System.Drawing.Point(146, 110)
        Me.txtFolder_Name2_2.MaxLength = 215
        Me.txtFolder_Name2_2.Name = "txtFolder_Name2_2"
        Me.txtFolder_Name2_2.ReadOnly = True
        Me.txtFolder_Name2_2.Size = New System.Drawing.Size(294, 13)
        Me.txtFolder_Name2_2.TabIndex = 2261
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 2260
        Me.Label6.Text = "Plant"
        '
        'btnPlant
        '
        Me.btnPlant.Image = Global.POIM.My.Resources.Resources.search
        Me.btnPlant.Location = New System.Drawing.Point(441, 44)
        Me.btnPlant.Name = "btnPlant"
        Me.btnPlant.Size = New System.Drawing.Size(22, 18)
        Me.btnPlant.TabIndex = 2258
        Me.btnPlant.UseVisualStyleBackColor = True
        '
        'TxtPlantCode
        '
        Me.TxtPlantCode.Enabled = False
        Me.TxtPlantCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlantCode.Location = New System.Drawing.Point(91, 43)
        Me.TxtPlantCode.MaxLength = 5
        Me.TxtPlantCode.Name = "TxtPlantCode"
        Me.TxtPlantCode.Size = New System.Drawing.Size(53, 20)
        Me.TxtPlantCode.TabIndex = 2257
        Me.TxtPlantCode.Visible = False
        '
        'TxtPlantName
        '
        Me.TxtPlantName.Enabled = False
        Me.TxtPlantName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlantName.Location = New System.Drawing.Point(146, 43)
        Me.TxtPlantName.MaxLength = 80
        Me.TxtPlantName.Name = "TxtPlantName"
        Me.TxtPlantName.Size = New System.Drawing.Size(294, 20)
        Me.TxtPlantName.TabIndex = 2259
        '
        'dt2_1
        '
        Me.dt2_1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt2_1.Location = New System.Drawing.Point(146, 21)
        Me.dt2_1.Name = "dt2_1"
        Me.dt2_1.Size = New System.Drawing.Size(91, 20)
        Me.dt2_1.TabIndex = 2256
        Me.dt2_1.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(240, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 13)
        Me.Label5.TabIndex = 2255
        Me.Label5.Text = "-"
        '
        'dt2_2
        '
        Me.dt2_2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt2_2.Location = New System.Drawing.Point(257, 21)
        Me.dt2_2.Name = "dt2_2"
        Me.dt2_2.Size = New System.Drawing.Size(91, 20)
        Me.dt2_2.TabIndex = 2254
        Me.dt2_2.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'txtFolder_Name2_1
        '
        Me.txtFolder_Name2_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFolder_Name2_1.ForeColor = System.Drawing.Color.Red
        Me.txtFolder_Name2_1.Location = New System.Drawing.Point(146, 68)
        Me.txtFolder_Name2_1.MaxLength = 215
        Me.txtFolder_Name2_1.Name = "txtFolder_Name2_1"
        Me.txtFolder_Name2_1.ReadOnly = True
        Me.txtFolder_Name2_1.Size = New System.Drawing.Size(294, 13)
        Me.txtFolder_Name2_1.TabIndex = 51
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Destination File"
        Me.Label3.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Invoice Date"
        '
        'TabPage2
        '
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.btnPlant2)
        Me.TabPage2.Controls.Add(Me.TxtPlantCode2)
        Me.TabPage2.Controls.Add(Me.TxtPlantName2)
        Me.TabPage2.Controls.Add(Me.dt1)
        Me.TabPage2.Controls.Add(Me.txtFolder_Name1)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(486, 263)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Master Supplier"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 46)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 2264
        Me.Label7.Text = "Plant"
        '
        'btnPlant2
        '
        Me.btnPlant2.Image = Global.POIM.My.Resources.Resources.search
        Me.btnPlant2.Location = New System.Drawing.Point(441, 44)
        Me.btnPlant2.Name = "btnPlant2"
        Me.btnPlant2.Size = New System.Drawing.Size(22, 18)
        Me.btnPlant2.TabIndex = 2262
        Me.btnPlant2.UseVisualStyleBackColor = True
        '
        'TxtPlantCode2
        '
        Me.TxtPlantCode2.Enabled = False
        Me.TxtPlantCode2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlantCode2.Location = New System.Drawing.Point(91, 43)
        Me.TxtPlantCode2.MaxLength = 5
        Me.TxtPlantCode2.Name = "TxtPlantCode2"
        Me.TxtPlantCode2.Size = New System.Drawing.Size(53, 20)
        Me.TxtPlantCode2.TabIndex = 2261
        Me.TxtPlantCode2.Visible = False
        '
        'TxtPlantName2
        '
        Me.TxtPlantName2.Enabled = False
        Me.TxtPlantName2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPlantName2.Location = New System.Drawing.Point(146, 43)
        Me.TxtPlantName2.MaxLength = 80
        Me.TxtPlantName2.Name = "TxtPlantName2"
        Me.TxtPlantName2.Size = New System.Drawing.Size(294, 20)
        Me.TxtPlantName2.TabIndex = 2263
        '
        'dt1
        '
        Me.dt1.Enabled = False
        Me.dt1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dt1.Location = New System.Drawing.Point(146, 21)
        Me.dt1.Name = "dt1"
        Me.dt1.Size = New System.Drawing.Size(91, 20)
        Me.dt1.TabIndex = 2255
        Me.dt1.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(447, 39)
        Me.txtID.MaxLength = 215
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(18, 20)
        Me.txtID.TabIndex = 52
        Me.txtID.Visible = False
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.Location = New System.Drawing.Point(5, 39)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(84, 13)
        Me.lblProcess.TabIndex = 54
        Me.lblProcess.Text = "Start Processing"
        '
        'DataAP2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(491, 354)
        Me.Controls.Add(Me.lblProcess)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "DataAP2"
        Me.Text = "Synchronize AP2"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFolder_Name1 As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents txtFolder_Name2_1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dt2_2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dt2_1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dt1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents btnPlant As System.Windows.Forms.Button
    Friend WithEvents TxtPlantCode As System.Windows.Forms.TextBox
    Friend WithEvents TxtPlantName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFolder_Name2_2 As System.Windows.Forms.TextBox
    Friend WithEvents listError As System.Windows.Forms.ListBox
    Friend WithEvents txtFolder_Name2_4 As System.Windows.Forms.TextBox
    Friend WithEvents txtFolder_Name2_3 As System.Windows.Forms.TextBox
    Friend WithEvents dt2_3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPlant2 As System.Windows.Forms.Button
    Friend WithEvents TxtPlantCode2 As System.Windows.Forms.TextBox
    Friend WithEvents TxtPlantName2 As System.Windows.Forms.TextBox
End Class
