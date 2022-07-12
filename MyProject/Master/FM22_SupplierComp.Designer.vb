<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FM22_SupplierComp
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCompany_Code = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnNew = New System.Windows.Forms.ToolStripButton
        Me.txtSupplier_Code = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LblCompany = New System.Windows.Forms.Label
        Me.BttnCompanyHelp = New System.Windows.Forms.Button
        Me.lblSupplierName = New System.Windows.Forms.Label
        Me.btnSearchSupp = New System.Windows.Forms.Button
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 105)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(571, 306)
        Me.DataGridView1.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.AutoSize = False
        Me.btnDelete.Enabled = False
        Me.btnDelete.Image = Global.POIM.My.Resources.Resources.delete
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Company Code"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Supplier Code"
        '
        'txtCompany_Code
        '
        Me.txtCompany_Code.Location = New System.Drawing.Point(137, 37)
        Me.txtCompany_Code.MaxLength = 5
        Me.txtCompany_Code.Name = "txtCompany_Code"
        Me.txtCompany_Code.Size = New System.Drawing.Size(43, 20)
        Me.txtCompany_Code.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Enabled = False
        Me.btnSave.Image = Global.POIM.My.Resources.Resources.SaveHL
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(60, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnNew, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnDelete})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(590, 25)
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
        'btnNew
        '
        Me.btnNew.AutoSize = False
        Me.btnNew.Image = Global.POIM.My.Resources.Resources.NewDocumentHS
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(60, 22)
        Me.btnNew.Text = "New"
        '
        'txtSupplier_Code
        '
        Me.txtSupplier_Code.Location = New System.Drawing.Point(137, 15)
        Me.txtSupplier_Code.MaxLength = 5
        Me.txtSupplier_Code.Name = "txtSupplier_Code"
        Me.txtSupplier_Code.Size = New System.Drawing.Size(43, 20)
        Me.txtSupplier_Code.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblCompany)
        Me.GroupBox1.Controls.Add(Me.BttnCompanyHelp)
        Me.GroupBox1.Controls.Add(Me.lblSupplierName)
        Me.GroupBox1.Controls.Add(Me.btnSearchSupp)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCompany_Code)
        Me.GroupBox1.Controls.Add(Me.txtSupplier_Code)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 32)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(571, 67)
        Me.GroupBox1.TabIndex = 49
        Me.GroupBox1.TabStop = False
        '
        'LblCompany
        '
        Me.LblCompany.AutoSize = True
        Me.LblCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCompany.Location = New System.Drawing.Point(214, 42)
        Me.LblCompany.Name = "LblCompany"
        Me.LblCompany.Size = New System.Drawing.Size(0, 13)
        Me.LblCompany.TabIndex = 133
        '
        'BttnCompanyHelp
        '
        Me.BttnCompanyHelp.Image = Global.POIM.My.Resources.Resources.search
        Me.BttnCompanyHelp.Location = New System.Drawing.Point(186, 38)
        Me.BttnCompanyHelp.Name = "BttnCompanyHelp"
        Me.BttnCompanyHelp.Size = New System.Drawing.Size(22, 18)
        Me.BttnCompanyHelp.TabIndex = 132
        Me.BttnCompanyHelp.UseVisualStyleBackColor = True
        '
        'lblSupplierName
        '
        Me.lblSupplierName.AutoSize = True
        Me.lblSupplierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.Location = New System.Drawing.Point(214, 20)
        Me.lblSupplierName.Name = "lblSupplierName"
        Me.lblSupplierName.Size = New System.Drawing.Size(0, 13)
        Me.lblSupplierName.TabIndex = 131
        '
        'btnSearchSupp
        '
        Me.btnSearchSupp.Image = Global.POIM.My.Resources.Resources.search
        Me.btnSearchSupp.Location = New System.Drawing.Point(186, 15)
        Me.btnSearchSupp.Name = "btnSearchSupp"
        Me.btnSearchSupp.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchSupp.TabIndex = 130
        Me.btnSearchSupp.UseVisualStyleBackColor = True
        '
        'FM22_SupplierComp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 423)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "FM22_SupplierComp"
        Me.Text = "FM22_Supplier Reference"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCompany_Code As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSupplier_Code As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblSupplierName As System.Windows.Forms.Label
    Friend WithEvents btnSearchSupp As System.Windows.Forms.Button
    Friend WithEvents LblCompany As System.Windows.Forms.Label
    Friend WithEvents BttnCompanyHelp As System.Windows.Forms.Button
End Class
