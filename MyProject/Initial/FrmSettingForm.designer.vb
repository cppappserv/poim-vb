<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingForm
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
        Me.LblKodeForm = New System.Windows.Forms.Label
        Me.TxtKodeForm = New System.Windows.Forms.TextBox
        Me.LblNamaForm = New System.Windows.Forms.Label
        Me.TxtNamaForm = New System.Windows.Forms.TextBox
        Me.btnPilihKodeForm = New System.Windows.Forms.Button
        Me.grpPilihanAksi = New System.Windows.Forms.GroupBox
        Me.btnBatal = New System.Windows.Forms.Button
        Me.btnSimpan = New System.Windows.Forms.Button
        Me.btnFormBaru = New System.Windows.Forms.Button
        Me.btnEditForm = New System.Windows.Forms.Button
        Me.LblKategori = New System.Windows.Forms.Label
        Me.CbKategori = New System.Windows.Forms.ComboBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StsKeterangan = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblNamaKategori = New System.Windows.Forms.Label
        Me.grpPilihanAksi.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblKodeForm
        '
        Me.LblKodeForm.AutoSize = True
        Me.LblKodeForm.Location = New System.Drawing.Point(12, 70)
        Me.LblKodeForm.Name = "LblKodeForm"
        Me.LblKodeForm.Size = New System.Drawing.Size(58, 13)
        Me.LblKodeForm.TabIndex = 2
        Me.LblKodeForm.Text = "&Kode Form"
        '
        'TxtKodeForm
        '
        Me.TxtKodeForm.Enabled = False
        Me.TxtKodeForm.Location = New System.Drawing.Point(98, 67)
        Me.TxtKodeForm.MaxLength = 20
        Me.TxtKodeForm.Name = "TxtKodeForm"
        Me.TxtKodeForm.Size = New System.Drawing.Size(190, 20)
        Me.TxtKodeForm.TabIndex = 3
        '
        'LblNamaForm
        '
        Me.LblNamaForm.AutoSize = True
        Me.LblNamaForm.Location = New System.Drawing.Point(12, 97)
        Me.LblNamaForm.Name = "LblNamaForm"
        Me.LblNamaForm.Size = New System.Drawing.Size(61, 13)
        Me.LblNamaForm.TabIndex = 5
        Me.LblNamaForm.Text = "&Nama Form"
        '
        'TxtNamaForm
        '
        Me.TxtNamaForm.Enabled = False
        Me.TxtNamaForm.Location = New System.Drawing.Point(98, 94)
        Me.TxtNamaForm.Name = "TxtNamaForm"
        Me.TxtNamaForm.Size = New System.Drawing.Size(190, 20)
        Me.TxtNamaForm.TabIndex = 6
        '
        'btnPilihKodeForm
        '
        Me.btnPilihKodeForm.Image = Global.POIM.My.Resources.Resources.search
        Me.btnPilihKodeForm.Location = New System.Drawing.Point(258, 65)
        Me.btnPilihKodeForm.Name = "btnPilihKodeForm"
        Me.btnPilihKodeForm.Size = New System.Drawing.Size(30, 23)
        Me.btnPilihKodeForm.TabIndex = 4
        Me.btnPilihKodeForm.UseVisualStyleBackColor = True
        Me.btnPilihKodeForm.Visible = False
        '
        'grpPilihanAksi
        '
        Me.grpPilihanAksi.Controls.Add(Me.btnBatal)
        Me.grpPilihanAksi.Controls.Add(Me.btnSimpan)
        Me.grpPilihanAksi.Controls.Add(Me.btnFormBaru)
        Me.grpPilihanAksi.Controls.Add(Me.btnEditForm)
        Me.grpPilihanAksi.Location = New System.Drawing.Point(15, 120)
        Me.grpPilihanAksi.Name = "grpPilihanAksi"
        Me.grpPilihanAksi.Size = New System.Drawing.Size(273, 80)
        Me.grpPilihanAksi.TabIndex = 22
        Me.grpPilihanAksi.TabStop = False
        Me.grpPilihanAksi.Text = "Pilihan"
        '
        'btnBatal
        '
        Me.btnBatal.Enabled = False
        Me.btnBatal.Location = New System.Drawing.Point(141, 48)
        Me.btnBatal.Name = "btnBatal"
        Me.btnBatal.Size = New System.Drawing.Size(75, 23)
        Me.btnBatal.TabIndex = 10
        Me.btnBatal.Text = "Ba&tal"
        Me.btnBatal.UseVisualStyleBackColor = True
        '
        'btnSimpan
        '
        Me.btnSimpan.Enabled = False
        Me.btnSimpan.Location = New System.Drawing.Point(60, 48)
        Me.btnSimpan.Name = "btnSimpan"
        Me.btnSimpan.Size = New System.Drawing.Size(75, 23)
        Me.btnSimpan.TabIndex = 9
        Me.btnSimpan.Text = "&Simpan"
        Me.btnSimpan.UseVisualStyleBackColor = True
        '
        'btnFormBaru
        '
        Me.btnFormBaru.Location = New System.Drawing.Point(60, 19)
        Me.btnFormBaru.Name = "btnFormBaru"
        Me.btnFormBaru.Size = New System.Drawing.Size(75, 23)
        Me.btnFormBaru.TabIndex = 7
        Me.btnFormBaru.Text = "Form &Baru"
        Me.btnFormBaru.UseVisualStyleBackColor = True
        '
        'btnEditForm
        '
        Me.btnEditForm.Location = New System.Drawing.Point(141, 19)
        Me.btnEditForm.Name = "btnEditForm"
        Me.btnEditForm.Size = New System.Drawing.Size(75, 23)
        Me.btnEditForm.TabIndex = 8
        Me.btnEditForm.Text = "&Edit Form"
        Me.btnEditForm.UseVisualStyleBackColor = True
        '
        'LblKategori
        '
        Me.LblKategori.AutoSize = True
        Me.LblKategori.Location = New System.Drawing.Point(12, 15)
        Me.LblKategori.Name = "LblKategori"
        Me.LblKategori.Size = New System.Drawing.Size(74, 13)
        Me.LblKategori.TabIndex = 0
        Me.LblKategori.Text = "Kode Kate&gori"
        '
        'CbKategori
        '
        Me.CbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbKategori.Enabled = False
        Me.CbKategori.FormattingEnabled = True
        Me.CbKategori.Items.AddRange(New Object() {""})
        Me.CbKategori.Location = New System.Drawing.Point(98, 12)
        Me.CbKategori.Name = "CbKategori"
        Me.CbKategori.Size = New System.Drawing.Size(190, 21)
        Me.CbKategori.TabIndex = 1
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StsKeterangan})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 219)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(300, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 23
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'StsKeterangan
        '
        Me.StsKeterangan.Name = "StsKeterangan"
        Me.StsKeterangan.Size = New System.Drawing.Size(0, 17)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Nama Kategori"
        '
        'LblNamaKategori
        '
        Me.LblNamaKategori.AutoSize = True
        Me.LblNamaKategori.Location = New System.Drawing.Point(95, 42)
        Me.LblNamaKategori.Name = "LblNamaKategori"
        Me.LblNamaKategori.Size = New System.Drawing.Size(0, 13)
        Me.LblNamaKategori.TabIndex = 25
        '
        'FrmSettingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 241)
        Me.Controls.Add(Me.LblNamaKategori)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.CbKategori)
        Me.Controls.Add(Me.LblKategori)
        Me.Controls.Add(Me.grpPilihanAksi)
        Me.Controls.Add(Me.btnPilihKodeForm)
        Me.Controls.Add(Me.TxtNamaForm)
        Me.Controls.Add(Me.LblNamaForm)
        Me.Controls.Add(Me.TxtKodeForm)
        Me.Controls.Add(Me.LblKodeForm)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSettingForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting Form"
        Me.grpPilihanAksi.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblKodeForm As System.Windows.Forms.Label
    Friend WithEvents TxtKodeForm As System.Windows.Forms.TextBox
    Friend WithEvents LblNamaForm As System.Windows.Forms.Label
    Friend WithEvents TxtNamaForm As System.Windows.Forms.TextBox
    Friend WithEvents btnPilihKodeForm As System.Windows.Forms.Button
    Friend WithEvents grpPilihanAksi As System.Windows.Forms.GroupBox
    Friend WithEvents btnBatal As System.Windows.Forms.Button
    Friend WithEvents btnSimpan As System.Windows.Forms.Button
    Friend WithEvents btnFormBaru As System.Windows.Forms.Button
    Friend WithEvents btnEditForm As System.Windows.Forms.Button
    Friend WithEvents LblKategori As System.Windows.Forms.Label
    Friend WithEvents CbKategori As System.Windows.Forms.ComboBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents StsKeterangan As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblNamaKategori As System.Windows.Forms.Label
End Class
