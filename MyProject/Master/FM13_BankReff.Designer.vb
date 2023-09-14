<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FM13_BankReff
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.txtRef_Code = New System.Windows.Forms.TextBox
        Me.txtBank_Code = New System.Windows.Forms.TextBox
        Me.btnSearchBank = New System.Windows.Forms.Button
        Me.btnSearchCompany = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.acc = New System.Windows.Forms.TextBox
        Me.lblBankName = New System.Windows.Forms.Label
        Me.lblCompName = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnNew, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnDelete})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(584, 25)
        Me.ToolStrip1.TabIndex = 1
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnNew
        '
        Me.btnNew.AutoSize = False
        Me.btnNew.Image = Global.poim.My.Resources.Resources.NewDocumentHS
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(60, 22)
        Me.btnNew.Text = "New"
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
        'btnDelete
        '
        Me.btnDelete.AutoSize = False
        Me.btnDelete.Enabled = False
        Me.btnDelete.Image = Global.poim.My.Resources.Resources.delete
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 111)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(560, 300)
        Me.DataGridView1.TabIndex = 2
        '
        'txtRef_Code
        '
        Me.txtRef_Code.Location = New System.Drawing.Point(137, 15)
        Me.txtRef_Code.Name = "txtRef_Code"
        Me.txtRef_Code.Size = New System.Drawing.Size(100, 20)
        Me.txtRef_Code.TabIndex = 3
        '
        'txtBank_Code
        '
        Me.txtBank_Code.Location = New System.Drawing.Point(137, 38)
        Me.txtBank_Code.Name = "txtBank_Code"
        Me.txtBank_Code.Size = New System.Drawing.Size(100, 20)
        Me.txtBank_Code.TabIndex = 4
        '
        'btnSearchBank
        '
        Me.btnSearchBank.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearchBank.Location = New System.Drawing.Point(243, 38)
        Me.btnSearchBank.Name = "btnSearchBank"
        Me.btnSearchBank.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchBank.TabIndex = 127
        Me.btnSearchBank.UseVisualStyleBackColor = True
        '
        'btnSearchCompany
        '
        Me.btnSearchCompany.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearchCompany.Location = New System.Drawing.Point(243, 15)
        Me.btnSearchCompany.Name = "btnSearchCompany"
        Me.btnSearchCompany.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCompany.TabIndex = 126
        Me.btnSearchCompany.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 128
        Me.Label1.Text = "Company"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 129
        Me.Label2.Text = "Bank"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.acc)
        Me.GroupBox1.Controls.Add(Me.lblBankName)
        Me.GroupBox1.Controls.Add(Me.lblCompName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSearchBank)
        Me.GroupBox1.Controls.Add(Me.btnSearchCompany)
        Me.GroupBox1.Controls.Add(Me.txtBank_Code)
        Me.GroupBox1.Controls.Add(Me.txtRef_Code)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 32)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(560, 73)
        Me.GroupBox1.TabIndex = 130
        Me.GroupBox1.TabStop = False
        '
        'acc
        '
        Me.acc.Location = New System.Drawing.Point(415, 19)
        Me.acc.Name = "acc"
        Me.acc.Size = New System.Drawing.Size(100, 20)
        Me.acc.TabIndex = 132
        Me.acc.Visible = False
        '
        'lblBankName
        '
        Me.lblBankName.AutoSize = True
        Me.lblBankName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankName.Location = New System.Drawing.Point(271, 43)
        Me.lblBankName.Name = "lblBankName"
        Me.lblBankName.Size = New System.Drawing.Size(0, 13)
        Me.lblBankName.TabIndex = 131
        '
        'lblCompName
        '
        Me.lblCompName.AutoSize = True
        Me.lblCompName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompName.Location = New System.Drawing.Point(271, 21)
        Me.lblCompName.Name = "lblCompName"
        Me.lblCompName.Size = New System.Drawing.Size(0, 13)
        Me.lblCompName.TabIndex = 130
        '
        'FM13_BankReff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(584, 423)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "FM13_BankReff"
        Me.Text = "FM13_Bank Reference"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtRef_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtBank_Code As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchBank As System.Windows.Forms.Button
    Friend WithEvents btnSearchCompany As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblBankName As System.Windows.Forms.Label
    Friend WithEvents lblCompName As System.Windows.Forms.Label
    Friend WithEvents acc As System.Windows.Forms.TextBox
End Class
