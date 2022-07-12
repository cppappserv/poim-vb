<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingAksesUser
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
        Me.LblKodeUser = New System.Windows.Forms.Label
        Me.TxtKodeUser = New System.Windows.Forms.TextBox
        Me.btnPilihKodeUser = New System.Windows.Forms.Button
        Me.LblNamaUser = New System.Windows.Forms.Label
        Me.LblIsiNamaUser = New System.Windows.Forms.Label
        Me.dgvAkses = New System.Windows.Forms.DataGridView
        Me.CbSemuaAkses = New System.Windows.Forms.CheckBox
        Me.BtnSimpan = New System.Windows.Forms.Button
        Me.access = New System.Windows.Forms.DataGridViewCheckBoxColumn
        CType(Me.dgvAkses, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblKodeUser
        '
        Me.LblKodeUser.AutoSize = True
        Me.LblKodeUser.Location = New System.Drawing.Point(12, 17)
        Me.LblKodeUser.Name = "LblKodeUser"
        Me.LblKodeUser.Size = New System.Drawing.Size(57, 13)
        Me.LblKodeUser.TabIndex = 0
        Me.LblKodeUser.Text = "&Kode User"
        '
        'TxtKodeUser
        '
        Me.TxtKodeUser.Location = New System.Drawing.Point(81, 14)
        Me.TxtKodeUser.MaxLength = 10
        Me.TxtKodeUser.Name = "TxtKodeUser"
        Me.TxtKodeUser.Size = New System.Drawing.Size(100, 20)
        Me.TxtKodeUser.TabIndex = 1
        '
        'btnPilihKodeUser
        '
        Me.btnPilihKodeUser.Image = Global.POIM.My.Resources.Resources.search
        Me.btnPilihKodeUser.Location = New System.Drawing.Point(187, 12)
        Me.btnPilihKodeUser.Name = "btnPilihKodeUser"
        Me.btnPilihKodeUser.Size = New System.Drawing.Size(30, 23)
        Me.btnPilihKodeUser.TabIndex = 2
        Me.btnPilihKodeUser.UseVisualStyleBackColor = True
        '
        'LblNamaUser
        '
        Me.LblNamaUser.AutoSize = True
        Me.LblNamaUser.Location = New System.Drawing.Point(12, 44)
        Me.LblNamaUser.Name = "LblNamaUser"
        Me.LblNamaUser.Size = New System.Drawing.Size(60, 13)
        Me.LblNamaUser.TabIndex = 7
        Me.LblNamaUser.Text = "Nama User"
        '
        'LblIsiNamaUser
        '
        Me.LblIsiNamaUser.AutoSize = True
        Me.LblIsiNamaUser.Location = New System.Drawing.Point(78, 44)
        Me.LblIsiNamaUser.Name = "LblIsiNamaUser"
        Me.LblIsiNamaUser.Size = New System.Drawing.Size(0, 13)
        Me.LblIsiNamaUser.TabIndex = 8
        '
        'dgvAkses
        '
        Me.dgvAkses.AllowUserToAddRows = False
        Me.dgvAkses.AllowUserToDeleteRows = False
        Me.dgvAkses.AllowUserToResizeRows = False
        Me.dgvAkses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAkses.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.access})
        Me.dgvAkses.Location = New System.Drawing.Point(15, 67)
        Me.dgvAkses.MultiSelect = False
        Me.dgvAkses.Name = "dgvAkses"
        Me.dgvAkses.Size = New System.Drawing.Size(513, 392)
        Me.dgvAkses.TabIndex = 3
        '
        'CbSemuaAkses
        '
        Me.CbSemuaAkses.AutoSize = True
        Me.CbSemuaAkses.Location = New System.Drawing.Point(30, 70)
        Me.CbSemuaAkses.Name = "CbSemuaAkses"
        Me.CbSemuaAkses.Size = New System.Drawing.Size(15, 14)
        Me.CbSemuaAkses.TabIndex = 9
        Me.CbSemuaAkses.ThreeState = True
        Me.CbSemuaAkses.UseVisualStyleBackColor = True
        '
        'BtnSimpan
        '
        Me.BtnSimpan.Location = New System.Drawing.Point(350, 12)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(75, 23)
        Me.BtnSimpan.TabIndex = 10
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = True
        '
        'access
        '
        Me.access.HeaderText = "Akses"
        Me.access.Name = "access"
        Me.access.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.access.Width = 40
        '
        'FrmSettingAksesUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 471)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.CbSemuaAkses)
        Me.Controls.Add(Me.dgvAkses)
        Me.Controls.Add(Me.LblIsiNamaUser)
        Me.Controls.Add(Me.LblNamaUser)
        Me.Controls.Add(Me.btnPilihKodeUser)
        Me.Controls.Add(Me.TxtKodeUser)
        Me.Controls.Add(Me.LblKodeUser)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSettingAksesUser"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting Akses User"
        CType(Me.dgvAkses, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblKodeUser As System.Windows.Forms.Label
    Friend WithEvents TxtKodeUser As System.Windows.Forms.TextBox
    Friend WithEvents btnPilihKodeUser As System.Windows.Forms.Button
    Friend WithEvents LblNamaUser As System.Windows.Forms.Label
    Friend WithEvents LblIsiNamaUser As System.Windows.Forms.Label
    Friend WithEvents dgvAkses As System.Windows.Forms.DataGridView
    Friend WithEvents CbSemuaAkses As System.Windows.Forms.CheckBox
    Friend WithEvents BtnSimpan As System.Windows.Forms.Button
    Friend WithEvents access As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
