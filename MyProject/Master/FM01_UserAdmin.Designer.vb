<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FM01_UserAdmin
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtUser_ID = New System.Windows.Forms.TextBox
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.txtPhone = New System.Windows.Forms.TextBox
        Me.txtFax = New System.Windows.Forms.TextBox
        Me.cbCompAll = New System.Windows.Forms.CheckBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.cbBlock = New System.Windows.Forms.CheckBox
        Me.dgvComp = New System.Windows.Forms.DataGridView
        Me.access = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.cbModulAll = New System.Windows.Forms.CheckBox
        Me.dgvModul = New System.Windows.Forms.DataGridView
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.dgvReport = New System.Windows.Forms.DataGridView
        Me.cekbox = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.cbReportAll = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbGroup = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvComp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvModul, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnNew, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnDelete})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(950, 25)
        Me.ToolStrip1.TabIndex = 15
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.AutoSize = False
        Me.btnClose.Image = Global.poim.My.Resources.Resources.Critical
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
        Me.btnDelete.Image = Global.poim.My.Resources.Resources.delete
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "User Id"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 31
        Me.Label3.Text = "Username"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Title"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Email"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Phone"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 198)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Fax"
        '
        'txtUser_ID
        '
        Me.txtUser_ID.Location = New System.Drawing.Point(117, 38)
        Me.txtUser_ID.MaxLength = 20
        Me.txtUser_ID.Name = "txtUser_ID"
        Me.txtUser_ID.Size = New System.Drawing.Size(161, 20)
        Me.txtUser_ID.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(117, 63)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(161, 20)
        Me.txtPassword.TabIndex = 3
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(117, 88)
        Me.txtName.MaxLength = 80
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(161, 20)
        Me.txtName.TabIndex = 4
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(117, 114)
        Me.txtTitle.MaxLength = 80
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(161, 20)
        Me.txtTitle.TabIndex = 5
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(117, 139)
        Me.txtEmail.MaxLength = 80
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(161, 20)
        Me.txtEmail.TabIndex = 6
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(117, 165)
        Me.txtPhone.MaxLength = 80
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(161, 20)
        Me.txtPhone.TabIndex = 7
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(117, 191)
        Me.txtFax.MaxLength = 80
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(161, 20)
        Me.txtFax.TabIndex = 8
        '
        'cbCompAll
        '
        Me.cbCompAll.AutoSize = True
        Me.cbCompAll.Location = New System.Drawing.Point(512, 275)
        Me.cbCompAll.Name = "cbCompAll"
        Me.cbCompAll.Size = New System.Drawing.Size(15, 14)
        Me.cbCompAll.TabIndex = 10
        Me.cbCompAll.UseVisualStyleBackColor = True
        Me.cbCompAll.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.poim.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(284, 38)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(22, 20)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cbBlock
        '
        Me.cbBlock.AutoSize = True
        Me.cbBlock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbBlock.ForeColor = System.Drawing.Color.Crimson
        Me.cbBlock.Location = New System.Drawing.Point(352, 220)
        Me.cbBlock.Name = "cbBlock"
        Me.cbBlock.Size = New System.Drawing.Size(110, 17)
        Me.cbBlock.TabIndex = 2
        Me.cbBlock.Text = "Block this user"
        Me.cbBlock.UseVisualStyleBackColor = True
        '
        'dgvComp
        '
        Me.dgvComp.AllowUserToAddRows = False
        Me.dgvComp.AllowUserToDeleteRows = False
        Me.dgvComp.AllowUserToResizeRows = False
        Me.dgvComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvComp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.access})
        Me.dgvComp.Location = New System.Drawing.Point(495, 272)
        Me.dgvComp.MultiSelect = False
        Me.dgvComp.Name = "dgvComp"
        Me.dgvComp.Size = New System.Drawing.Size(443, 350)
        Me.dgvComp.TabIndex = 9
        '
        'access
        '
        Me.access.HeaderText = "Active"
        Me.access.Name = "access"
        Me.access.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.access.Width = 45
        '
        'cbModulAll
        '
        Me.cbModulAll.AutoSize = True
        Me.cbModulAll.Location = New System.Drawing.Point(33, 275)
        Me.cbModulAll.Name = "cbModulAll"
        Me.cbModulAll.Size = New System.Drawing.Size(15, 14)
        Me.cbModulAll.TabIndex = 12
        Me.cbModulAll.UseVisualStyleBackColor = True
        Me.cbModulAll.Visible = False
        '
        'dgvModul
        '
        Me.dgvModul.AllowUserToAddRows = False
        Me.dgvModul.AllowUserToDeleteRows = False
        Me.dgvModul.AllowUserToResizeRows = False
        Me.dgvModul.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvModul.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1})
        Me.dgvModul.Location = New System.Drawing.Point(19, 272)
        Me.dgvModul.MultiSelect = False
        Me.dgvModul.Name = "dgvModul"
        Me.dgvModul.Size = New System.Drawing.Size(443, 350)
        Me.dgvModul.TabIndex = 11
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Active"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn1.Width = 45
        '
        'dgvReport
        '
        Me.dgvReport.AllowUserToAddRows = False
        Me.dgvReport.AllowUserToDeleteRows = False
        Me.dgvReport.AllowUserToResizeRows = False
        Me.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvReport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cekbox})
        Me.dgvReport.Location = New System.Drawing.Point(495, 70)
        Me.dgvReport.MultiSelect = False
        Me.dgvReport.Name = "dgvReport"
        Me.dgvReport.Size = New System.Drawing.Size(443, 174)
        Me.dgvReport.TabIndex = 13
        Me.dgvReport.Visible = False
        '
        'cekbox
        '
        Me.cekbox.HeaderText = "Active"
        Me.cekbox.Name = "cekbox"
        Me.cekbox.Width = 45
        '
        'cbReportAll
        '
        Me.cbReportAll.AutoSize = True
        Me.cbReportAll.Location = New System.Drawing.Point(511, 73)
        Me.cbReportAll.Name = "cbReportAll"
        Me.cbReportAll.Size = New System.Drawing.Size(15, 14)
        Me.cbReportAll.TabIndex = 14
        Me.cbReportAll.UseVisualStyleBackColor = True
        Me.cbReportAll.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Group User"
        '
        'cbGroup
        '
        Me.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGroup.FormattingEnabled = True
        Me.cbGroup.Items.AddRange(New Object() {"0  - External Users", "1  - Import Dept."})
        Me.cbGroup.Location = New System.Drawing.Point(117, 216)
        Me.cbGroup.Name = "cbGroup"
        Me.cbGroup.Size = New System.Drawing.Size(161, 21)
        Me.cbGroup.TabIndex = 37
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(492, 251)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(122, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Authorized to Company :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 251)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(112, 13)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Authorized to Moduls :"
        '
        'FM01_UserAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 626)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cbGroup)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cbReportAll)
        Me.Controls.Add(Me.dgvReport)
        Me.Controls.Add(Me.cbModulAll)
        Me.Controls.Add(Me.dgvModul)
        Me.Controls.Add(Me.cbCompAll)
        Me.Controls.Add(Me.dgvComp)
        Me.Controls.Add(Me.cbBlock)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.txtPhone)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtTitle)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUser_ID)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FM01_UserAdmin"
        Me.Text = "FM01_UserAdmin"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvComp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvModul, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvReport, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtUser_ID As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents txtPhone As System.Windows.Forms.TextBox
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents cbCompAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cbBlock As System.Windows.Forms.CheckBox
    Friend WithEvents dgvComp As System.Windows.Forms.DataGridView
    Friend WithEvents cbModulAll As System.Windows.Forms.CheckBox
    Friend WithEvents dgvModul As System.Windows.Forms.DataGridView
    Friend WithEvents dgvReport As System.Windows.Forms.DataGridView
    Friend WithEvents cbReportAll As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents access As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents cekbox As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbGroup As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
