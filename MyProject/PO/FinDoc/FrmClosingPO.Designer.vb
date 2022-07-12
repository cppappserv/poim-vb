<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmClosingPO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmClosingPO))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnView = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblPort_Name = New System.Windows.Forms.Label
        Me.btnSearchPort = New System.Windows.Forms.Button
        Me.txtPort_Code = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblPlant_Name = New System.Windows.Forms.Label
        Me.btnSearchPlant = New System.Windows.Forms.Button
        Me.txtPlant_Code = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnSearchLine = New System.Windows.Forms.Button
        Me.txtLine_Code = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.userct = New System.Windows.Forms.TextBox
        Me.btnUserPur = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtCreatedby = New System.Windows.Forms.TextBox
        Me.lblMatGrp = New System.Windows.Forms.Label
        Me.btnMatgrp = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtMatGrp = New System.Windows.Forms.TextBox
        Me.lblSuppName = New System.Windows.Forms.Label
        Me.btnSup = New System.Windows.Forms.Button
        Me.txtSuppCode = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblCompany_Name = New System.Windows.Forms.Label
        Me.btnSearchCompany = New System.Windows.Forms.Button
        Me.txtCompany_Code = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.CmbPeriode = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbStatus = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.DGVClosing = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGVClosing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnView, Me.ToolStripSeparator2, Me.btnSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(976, 25)
        Me.ToolStrip1.TabIndex = 0
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
        'btnView
        '
        Me.btnView.AutoSize = False
        Me.btnView.Image = CType(resources.GetObject("btnView.Image"), System.Drawing.Image)
        Me.btnView.ImageTransparentColor = System.Drawing.Color.Transparent
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(60, 22)
        Me.btnView.Text = "View"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(60, 22)
        Me.btnSave.Text = "Save"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblPort_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchPort)
        Me.GroupBox1.Controls.Add(Me.txtPort_Code)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblPlant_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchPlant)
        Me.GroupBox1.Controls.Add(Me.txtPlant_Code)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.btnSearchLine)
        Me.GroupBox1.Controls.Add(Me.txtLine_Code)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.userct)
        Me.GroupBox1.Controls.Add(Me.btnUserPur)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtCreatedby)
        Me.GroupBox1.Controls.Add(Me.lblMatGrp)
        Me.GroupBox1.Controls.Add(Me.btnMatgrp)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtMatGrp)
        Me.GroupBox1.Controls.Add(Me.lblSuppName)
        Me.GroupBox1.Controls.Add(Me.btnSup)
        Me.GroupBox1.Controls.Add(Me.txtSuppCode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblCompany_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchCompany)
        Me.GroupBox1.Controls.Add(Me.txtCompany_Code)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.CmbPeriode)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbStatus)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(954, 176)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        '
        'lblPort_Name
        '
        Me.lblPort_Name.AutoSize = True
        Me.lblPort_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPort_Name.Location = New System.Drawing.Point(638, 69)
        Me.lblPort_Name.Name = "lblPort_Name"
        Me.lblPort_Name.Size = New System.Drawing.Size(30, 13)
        Me.lblPort_Name.TabIndex = 2264
        Me.lblPort_Name.Text = "Port"
        '
        'btnSearchPort
        '
        Me.btnSearchPort.Image = CType(resources.GetObject("btnSearchPort.Image"), System.Drawing.Image)
        Me.btnSearchPort.Location = New System.Drawing.Point(611, 62)
        Me.btnSearchPort.Name = "btnSearchPort"
        Me.btnSearchPort.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchPort.TabIndex = 2263
        Me.btnSearchPort.UseVisualStyleBackColor = True
        '
        'txtPort_Code
        '
        Me.txtPort_Code.Location = New System.Drawing.Point(546, 62)
        Me.txtPort_Code.MaxLength = 5
        Me.txtPort_Code.Name = "txtPort_Code"
        Me.txtPort_Code.Size = New System.Drawing.Size(60, 20)
        Me.txtPort_Code.TabIndex = 2262
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(421, 67)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 13)
        Me.Label12.TabIndex = 2261
        Me.Label12.Text = "Port"
        '
        'lblPlant_Name
        '
        Me.lblPlant_Name.AutoSize = True
        Me.lblPlant_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlant_Name.Location = New System.Drawing.Point(638, 46)
        Me.lblPlant_Name.Name = "lblPlant_Name"
        Me.lblPlant_Name.Size = New System.Drawing.Size(36, 13)
        Me.lblPlant_Name.TabIndex = 2260
        Me.lblPlant_Name.Text = "Plant"
        '
        'btnSearchPlant
        '
        Me.btnSearchPlant.Image = CType(resources.GetObject("btnSearchPlant.Image"), System.Drawing.Image)
        Me.btnSearchPlant.Location = New System.Drawing.Point(611, 39)
        Me.btnSearchPlant.Name = "btnSearchPlant"
        Me.btnSearchPlant.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchPlant.TabIndex = 2259
        Me.btnSearchPlant.UseVisualStyleBackColor = True
        '
        'txtPlant_Code
        '
        Me.txtPlant_Code.Location = New System.Drawing.Point(546, 39)
        Me.txtPlant_Code.MaxLength = 5
        Me.txtPlant_Code.Name = "txtPlant_Code"
        Me.txtPlant_Code.Size = New System.Drawing.Size(60, 20)
        Me.txtPlant_Code.TabIndex = 2258
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(421, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 2257
        Me.Label10.Text = "Plant"
        '
        'btnSearchLine
        '
        Me.btnSearchLine.Image = CType(resources.GetObject("btnSearchLine.Image"), System.Drawing.Image)
        Me.btnSearchLine.Location = New System.Drawing.Point(611, 17)
        Me.btnSearchLine.Name = "btnSearchLine"
        Me.btnSearchLine.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchLine.TabIndex = 2255
        Me.btnSearchLine.UseVisualStyleBackColor = True
        '
        'txtLine_Code
        '
        Me.txtLine_Code.Location = New System.Drawing.Point(546, 17)
        Me.txtLine_Code.MaxLength = 5
        Me.txtLine_Code.Name = "txtLine_Code"
        Me.txtLine_Code.Size = New System.Drawing.Size(60, 20)
        Me.txtLine_Code.TabIndex = 2254
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(421, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 13)
        Me.Label8.TabIndex = 2253
        Me.Label8.Text = "Line Bussiness"
        '
        'userct
        '
        Me.userct.Location = New System.Drawing.Point(97, 63)
        Me.userct.Name = "userct"
        Me.userct.Size = New System.Drawing.Size(38, 20)
        Me.userct.TabIndex = 2252
        Me.userct.Visible = False
        '
        'btnUserPur
        '
        Me.btnUserPur.Image = CType(resources.GetObject("btnUserPur.Image"), System.Drawing.Image)
        Me.btnUserPur.Location = New System.Drawing.Point(338, 63)
        Me.btnUserPur.Name = "btnUserPur"
        Me.btnUserPur.Size = New System.Drawing.Size(22, 20)
        Me.btnUserPur.TabIndex = 2251
        Me.btnUserPur.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 2250
        Me.Label2.Text = "Create By"
        '
        'txtCreatedby
        '
        Me.txtCreatedby.Location = New System.Drawing.Point(137, 63)
        Me.txtCreatedby.MaxLength = 40
        Me.txtCreatedby.Name = "txtCreatedby"
        Me.txtCreatedby.ReadOnly = True
        Me.txtCreatedby.Size = New System.Drawing.Size(197, 20)
        Me.txtCreatedby.TabIndex = 2249
        '
        'lblMatGrp
        '
        Me.lblMatGrp.AutoSize = True
        Me.lblMatGrp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMatGrp.Location = New System.Drawing.Point(638, 135)
        Me.lblMatGrp.Name = "lblMatGrp"
        Me.lblMatGrp.Size = New System.Drawing.Size(126, 13)
        Me.lblMatGrp.TabIndex = 2248
        Me.lblMatGrp.Text = "Material Group Name"
        '
        'btnMatgrp
        '
        Me.btnMatgrp.Image = Global.POIM.My.Resources.Resources.search
        Me.btnMatgrp.Location = New System.Drawing.Point(611, 132)
        Me.btnMatgrp.Name = "btnMatgrp"
        Me.btnMatgrp.Size = New System.Drawing.Size(22, 18)
        Me.btnMatgrp.TabIndex = 2246
        Me.btnMatgrp.TabStop = False
        Me.btnMatgrp.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(421, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 2247
        Me.Label7.Text = "Material Group"
        '
        'txtMatGrp
        '
        Me.txtMatGrp.Location = New System.Drawing.Point(546, 131)
        Me.txtMatGrp.MaxLength = 5
        Me.txtMatGrp.Name = "txtMatGrp"
        Me.txtMatGrp.Size = New System.Drawing.Size(60, 20)
        Me.txtMatGrp.TabIndex = 2245
        '
        'lblSuppName
        '
        Me.lblSuppName.AutoSize = True
        Me.lblSuppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuppName.Location = New System.Drawing.Point(638, 114)
        Me.lblSuppName.Name = "lblSuppName"
        Me.lblSuppName.Size = New System.Drawing.Size(89, 13)
        Me.lblSuppName.TabIndex = 2244
        Me.lblSuppName.Text = "Supplier Name"
        '
        'btnSup
        '
        Me.btnSup.Image = CType(resources.GetObject("btnSup.Image"), System.Drawing.Image)
        Me.btnSup.Location = New System.Drawing.Point(611, 108)
        Me.btnSup.Name = "btnSup"
        Me.btnSup.Size = New System.Drawing.Size(22, 20)
        Me.btnSup.TabIndex = 2243
        Me.btnSup.UseVisualStyleBackColor = True
        '
        'txtSuppCode
        '
        Me.txtSuppCode.Location = New System.Drawing.Point(546, 108)
        Me.txtSuppCode.MaxLength = 1
        Me.txtSuppCode.Name = "txtSuppCode"
        Me.txtSuppCode.Size = New System.Drawing.Size(60, 20)
        Me.txtSuppCode.TabIndex = 2242
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(421, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 2241
        Me.Label6.Text = "Supplier"
        '
        'lblCompany_Name
        '
        Me.lblCompany_Name.AutoSize = True
        Me.lblCompany_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany_Name.Location = New System.Drawing.Point(638, 92)
        Me.lblCompany_Name.Name = "lblCompany_Name"
        Me.lblCompany_Name.Size = New System.Drawing.Size(58, 13)
        Me.lblCompany_Name.TabIndex = 2240
        Me.lblCompany_Name.Text = "Company"
        '
        'btnSearchCompany
        '
        Me.btnSearchCompany.Image = CType(resources.GetObject("btnSearchCompany.Image"), System.Drawing.Image)
        Me.btnSearchCompany.Location = New System.Drawing.Point(611, 85)
        Me.btnSearchCompany.Name = "btnSearchCompany"
        Me.btnSearchCompany.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCompany.TabIndex = 2239
        Me.btnSearchCompany.UseVisualStyleBackColor = True
        '
        'txtCompany_Code
        '
        Me.txtCompany_Code.Location = New System.Drawing.Point(546, 85)
        Me.txtCompany_Code.MaxLength = 5
        Me.txtCompany_Code.Name = "txtCompany_Code"
        Me.txtCompany_Code.Size = New System.Drawing.Size(60, 20)
        Me.txtCompany_Code.TabIndex = 2238
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(421, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 2237
        Me.Label4.Text = "Company"
        '
        'CmbPeriode
        '
        Me.CmbPeriode.FormattingEnabled = True
        Me.CmbPeriode.Location = New System.Drawing.Point(137, 40)
        Me.CmbPeriode.Name = "CmbPeriode"
        Me.CmbPeriode.Size = New System.Drawing.Size(69, 21)
        Me.CmbPeriode.TabIndex = 2236
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 2235
        Me.Label3.Text = "Status"
        '
        'cbStatus
        '
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"PurchaseDate Not Completed", "Open (All)", "Open (Diff<Tolerable)", "Closed"})
        Me.cbStatus.Location = New System.Drawing.Point(137, 17)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(184, 21)
        Me.cbStatus.TabIndex = 2234
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Purchase Periode"
        '
        'DGVClosing
        '
        Me.DGVClosing.AllowUserToAddRows = False
        Me.DGVClosing.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVClosing.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVClosing.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVClosing.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGVClosing.Location = New System.Drawing.Point(10, 231)
        Me.DGVClosing.Name = "DGVClosing"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVClosing.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVClosing.Size = New System.Drawing.Size(954, 341)
        Me.DGVClosing.TabIndex = 62
        '
        'FrmClosingPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(976, 587)
        Me.Controls.Add(Me.DGVClosing)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "FrmClosingPO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Closing PO"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DGVClosing, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnView As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbPeriode As System.Windows.Forms.ComboBox
    Friend WithEvents DGVClosing As System.Windows.Forms.DataGridView
    Friend WithEvents lblMatGrp As System.Windows.Forms.Label
    Friend WithEvents btnMatgrp As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMatGrp As System.Windows.Forms.TextBox
    Friend WithEvents lblSuppName As System.Windows.Forms.Label
    Friend WithEvents btnSup As System.Windows.Forms.Button
    Friend WithEvents txtSuppCode As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblCompany_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchCompany As System.Windows.Forms.Button
    Friend WithEvents txtCompany_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents userct As System.Windows.Forms.TextBox
    Friend WithEvents btnUserPur As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCreatedby As System.Windows.Forms.TextBox
    Friend WithEvents lblPlant_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchPlant As System.Windows.Forms.Button
    Friend WithEvents txtPlant_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnSearchLine As System.Windows.Forms.Button
    Friend WithEvents txtLine_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblPort_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchPort As System.Windows.Forms.Button
    Friend WithEvents txtPort_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
