<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingUser
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
        Me.LblUser = New System.Windows.Forms.Label
        Me.TxtKodeUser = New System.Windows.Forms.TextBox
        Me.TxtNamaUser = New System.Windows.Forms.TextBox
        Me.TxtPassword = New System.Windows.Forms.TextBox
        Me.ChkAccAdmin = New System.Windows.Forms.CheckBox
        Me.GrpSetting = New System.Windows.Forms.GroupBox
        Me.ChkBlock = New System.Windows.Forms.CheckBox
        Me.ChkDbgMsg = New System.Windows.Forms.CheckBox
        Me.LblNama = New System.Windows.Forms.Label
        Me.LblPassword = New System.Windows.Forms.Label
        Me.BtnSimpan = New System.Windows.Forms.Button
        Me.StsStrip = New System.Windows.Forms.StatusStrip
        Me.StsKeterangan = New System.Windows.Forms.ToolStripStatusLabel
        Me.BtnCari = New System.Windows.Forms.Button
        Me.BtnSetPassword = New System.Windows.Forms.Button
        Me.LblPassBaru1 = New System.Windows.Forms.Label
        Me.TxtPassBaru1 = New System.Windows.Forms.TextBox
        Me.BtnUserBaru = New System.Windows.Forms.Button
        Me.BtnModifikasi = New System.Windows.Forms.Button
        Me.LblPassBaru2 = New System.Windows.Forms.Label
        Me.TxtPassBaru2 = New System.Windows.Forms.TextBox
        Me.BtnBatal = New System.Windows.Forms.Button
        Me.GrpSetting.SuspendLayout()
        Me.StsStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblUser
        '
        Me.LblUser.AutoSize = True
        Me.LblUser.Location = New System.Drawing.Point(19, 31)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(57, 13)
        Me.LblUser.TabIndex = 1
        Me.LblUser.Text = "Kode User"
        '
        'TxtKodeUser
        '
        Me.TxtKodeUser.Location = New System.Drawing.Point(121, 24)
        Me.TxtKodeUser.MaxLength = 10
        Me.TxtKodeUser.Name = "TxtKodeUser"
        Me.TxtKodeUser.Size = New System.Drawing.Size(162, 20)
        Me.TxtKodeUser.TabIndex = 2
        '
        'TxtNamaUser
        '
        Me.TxtNamaUser.Location = New System.Drawing.Point(121, 51)
        Me.TxtNamaUser.MaxLength = 30
        Me.TxtNamaUser.Name = "TxtNamaUser"
        Me.TxtNamaUser.Size = New System.Drawing.Size(251, 20)
        Me.TxtNamaUser.TabIndex = 3
        '
        'TxtPassword
        '
        Me.TxtPassword.Location = New System.Drawing.Point(121, 76)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(162, 20)
        Me.TxtPassword.TabIndex = 4
        '
        'ChkAccAdmin
        '
        Me.ChkAccAdmin.AutoSize = True
        Me.ChkAccAdmin.Location = New System.Drawing.Point(6, 19)
        Me.ChkAccAdmin.Name = "ChkAccAdmin"
        Me.ChkAccAdmin.Size = New System.Drawing.Size(93, 17)
        Me.ChkAccAdmin.TabIndex = 5
        Me.ChkAccAdmin.Text = "Access Admin"
        Me.ChkAccAdmin.UseVisualStyleBackColor = True
        '
        'GrpSetting
        '
        Me.GrpSetting.Controls.Add(Me.ChkBlock)
        Me.GrpSetting.Controls.Add(Me.ChkDbgMsg)
        Me.GrpSetting.Controls.Add(Me.ChkAccAdmin)
        Me.GrpSetting.Location = New System.Drawing.Point(22, 161)
        Me.GrpSetting.Name = "GrpSetting"
        Me.GrpSetting.Size = New System.Drawing.Size(350, 68)
        Me.GrpSetting.TabIndex = 6
        Me.GrpSetting.TabStop = False
        Me.GrpSetting.Text = "Other Setting"
        '
        'ChkBlock
        '
        Me.ChkBlock.AutoSize = True
        Me.ChkBlock.Location = New System.Drawing.Point(193, 19)
        Me.ChkBlock.Name = "ChkBlock"
        Me.ChkBlock.Size = New System.Drawing.Size(91, 17)
        Me.ChkBlock.TabIndex = 7
        Me.ChkBlock.Text = "Block Access"
        Me.ChkBlock.UseVisualStyleBackColor = True
        '
        'ChkDbgMsg
        '
        Me.ChkDbgMsg.AutoSize = True
        Me.ChkDbgMsg.Location = New System.Drawing.Point(6, 42)
        Me.ChkDbgMsg.Name = "ChkDbgMsg"
        Me.ChkDbgMsg.Size = New System.Drawing.Size(120, 17)
        Me.ChkDbgMsg.TabIndex = 6
        Me.ChkDbgMsg.Text = "Display Debug Error"
        Me.ChkDbgMsg.UseVisualStyleBackColor = True
        '
        'LblNama
        '
        Me.LblNama.AutoSize = True
        Me.LblNama.Location = New System.Drawing.Point(19, 57)
        Me.LblNama.Name = "LblNama"
        Me.LblNama.Size = New System.Drawing.Size(60, 13)
        Me.LblNama.TabIndex = 7
        Me.LblNama.Text = "Nama User"
        '
        'LblPassword
        '
        Me.LblPassword.AutoSize = True
        Me.LblPassword.Location = New System.Drawing.Point(19, 83)
        Me.LblPassword.Name = "LblPassword"
        Me.LblPassword.Size = New System.Drawing.Size(53, 13)
        Me.LblPassword.TabIndex = 8
        Me.LblPassword.Text = "Password"
        '
        'BtnSimpan
        '
        Me.BtnSimpan.Location = New System.Drawing.Point(113, 244)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(61, 23)
        Me.BtnSimpan.TabIndex = 9
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = True
        '
        'StsStrip
        '
        Me.StsStrip.AutoSize = False
        Me.StsStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StsKeterangan})
        Me.StsStrip.Location = New System.Drawing.Point(0, 279)
        Me.StsStrip.Name = "StsStrip"
        Me.StsStrip.Size = New System.Drawing.Size(398, 22)
        Me.StsStrip.SizingGrip = False
        Me.StsStrip.Stretch = False
        Me.StsStrip.TabIndex = 10
        '
        'StsKeterangan
        '
        Me.StsKeterangan.Name = "StsKeterangan"
        Me.StsKeterangan.Size = New System.Drawing.Size(0, 17)
        '
        'BtnCari
        '
        Me.BtnCari.Image = Global.poim.My.Resources.Resources.search
        Me.BtnCari.Location = New System.Drawing.Point(289, 22)
        Me.BtnCari.Name = "BtnCari"
        Me.BtnCari.Size = New System.Drawing.Size(25, 23)
        Me.BtnCari.TabIndex = 11
        Me.BtnCari.UseVisualStyleBackColor = True
        '
        'BtnSetPassword
        '
        Me.BtnSetPassword.Location = New System.Drawing.Point(9, 244)
        Me.BtnSetPassword.Name = "BtnSetPassword"
        Me.BtnSetPassword.Size = New System.Drawing.Size(98, 23)
        Me.BtnSetPassword.TabIndex = 12
        Me.BtnSetPassword.Text = "&Rubah Password"
        Me.BtnSetPassword.UseVisualStyleBackColor = True
        '
        'LblPassBaru1
        '
        Me.LblPassBaru1.AutoSize = True
        Me.LblPassBaru1.Location = New System.Drawing.Point(19, 109)
        Me.LblPassBaru1.Name = "LblPassBaru1"
        Me.LblPassBaru1.Size = New System.Drawing.Size(87, 13)
        Me.LblPassBaru1.TabIndex = 14
        Me.LblPassBaru1.Text = "Password Baru 1"
        '
        'TxtPassBaru1
        '
        Me.TxtPassBaru1.Enabled = False
        Me.TxtPassBaru1.Location = New System.Drawing.Point(121, 102)
        Me.TxtPassBaru1.Name = "TxtPassBaru1"
        Me.TxtPassBaru1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassBaru1.Size = New System.Drawing.Size(162, 20)
        Me.TxtPassBaru1.TabIndex = 13
        '
        'BtnUserBaru
        '
        Me.BtnUserBaru.Location = New System.Drawing.Point(180, 244)
        Me.BtnUserBaru.Name = "BtnUserBaru"
        Me.BtnUserBaru.Size = New System.Drawing.Size(63, 23)
        Me.BtnUserBaru.TabIndex = 15
        Me.BtnUserBaru.Text = "&User Baru"
        Me.BtnUserBaru.UseVisualStyleBackColor = True
        '
        'BtnModifikasi
        '
        Me.BtnModifikasi.Location = New System.Drawing.Point(249, 244)
        Me.BtnModifikasi.Name = "BtnModifikasi"
        Me.BtnModifikasi.Size = New System.Drawing.Size(66, 23)
        Me.BtnModifikasi.TabIndex = 16
        Me.BtnModifikasi.Text = "&Edit User"
        Me.BtnModifikasi.UseVisualStyleBackColor = True
        '
        'LblPassBaru2
        '
        Me.LblPassBaru2.AutoSize = True
        Me.LblPassBaru2.Location = New System.Drawing.Point(19, 135)
        Me.LblPassBaru2.Name = "LblPassBaru2"
        Me.LblPassBaru2.Size = New System.Drawing.Size(87, 13)
        Me.LblPassBaru2.TabIndex = 18
        Me.LblPassBaru2.Text = "Password Baru 2"
        '
        'TxtPassBaru2
        '
        Me.TxtPassBaru2.Enabled = False
        Me.TxtPassBaru2.Location = New System.Drawing.Point(121, 128)
        Me.TxtPassBaru2.Name = "TxtPassBaru2"
        Me.TxtPassBaru2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassBaru2.Size = New System.Drawing.Size(162, 20)
        Me.TxtPassBaru2.TabIndex = 17
        '
        'BtnBatal
        '
        Me.BtnBatal.Location = New System.Drawing.Point(321, 244)
        Me.BtnBatal.Name = "BtnBatal"
        Me.BtnBatal.Size = New System.Drawing.Size(67, 23)
        Me.BtnBatal.TabIndex = 19
        Me.BtnBatal.Text = "&Batal"
        Me.BtnBatal.UseVisualStyleBackColor = True
        '
        'FrmSettingUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 301)
        Me.Controls.Add(Me.BtnBatal)
        Me.Controls.Add(Me.LblPassBaru2)
        Me.Controls.Add(Me.TxtPassBaru2)
        Me.Controls.Add(Me.BtnModifikasi)
        Me.Controls.Add(Me.BtnUserBaru)
        Me.Controls.Add(Me.LblPassBaru1)
        Me.Controls.Add(Me.TxtPassBaru1)
        Me.Controls.Add(Me.BtnSetPassword)
        Me.Controls.Add(Me.BtnCari)
        Me.Controls.Add(Me.StsStrip)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.LblPassword)
        Me.Controls.Add(Me.LblNama)
        Me.Controls.Add(Me.GrpSetting)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtNamaUser)
        Me.Controls.Add(Me.TxtKodeUser)
        Me.Controls.Add(Me.LblUser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSettingUser"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting User"
        Me.GrpSetting.ResumeLayout(False)
        Me.GrpSetting.PerformLayout()
        Me.StsStrip.ResumeLayout(False)
        Me.StsStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblUser As System.Windows.Forms.Label
    Friend WithEvents TxtKodeUser As System.Windows.Forms.TextBox
    Friend WithEvents TxtNamaUser As System.Windows.Forms.TextBox
    Friend WithEvents TxtPassword As System.Windows.Forms.TextBox
    Friend WithEvents ChkAccAdmin As System.Windows.Forms.CheckBox
    Friend WithEvents GrpSetting As System.Windows.Forms.GroupBox
    Friend WithEvents ChkBlock As System.Windows.Forms.CheckBox
    Friend WithEvents ChkDbgMsg As System.Windows.Forms.CheckBox
    Friend WithEvents LblNama As System.Windows.Forms.Label
    Friend WithEvents LblPassword As System.Windows.Forms.Label
    Friend WithEvents BtnSimpan As System.Windows.Forms.Button
    Friend WithEvents StsStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents StsKeterangan As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BtnCari As System.Windows.Forms.Button
    Friend WithEvents BtnSetPassword As System.Windows.Forms.Button
    Friend WithEvents LblPassBaru1 As System.Windows.Forms.Label
    Friend WithEvents TxtPassBaru1 As System.Windows.Forms.TextBox
    Friend WithEvents BtnUserBaru As System.Windows.Forms.Button
    Friend WithEvents BtnModifikasi As System.Windows.Forms.Button
    Friend WithEvents LblPassBaru2 As System.Windows.Forms.Label
    Friend WithEvents TxtPassBaru2 As System.Windows.Forms.TextBox
    Friend WithEvents BtnBatal As System.Windows.Forms.Button
End Class
