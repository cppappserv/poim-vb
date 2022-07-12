<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingAplikasi
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
        Me.TabCtrlSetting = New System.Windows.Forms.TabControl
        Me.TabGeneral = New System.Windows.Forms.TabPage
        Me.ChkStatProgress = New System.Windows.Forms.CheckBox
        Me.TabProses = New System.Windows.Forms.TabPage
        Me.GrpTglProses = New System.Windows.Forms.GroupBox
        Me.RdoTglLastProcess = New System.Windows.Forms.RadioButton
        Me.RdoTglCurrent = New System.Windows.Forms.RadioButton
        Me.TabAdmin = New System.Windows.Forms.TabPage
        Me.ChkTrapErr = New System.Windows.Forms.CheckBox
        Me.BtnOK = New System.Windows.Forms.Button
        Me.BtnTutup = New System.Windows.Forms.Button
        Me.TblLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ChkLogTrans = New System.Windows.Forms.CheckBox
        Me.TabCtrlSetting.SuspendLayout()
        Me.TabGeneral.SuspendLayout()
        Me.TabProses.SuspendLayout()
        Me.GrpTglProses.SuspendLayout()
        Me.TabAdmin.SuspendLayout()
        Me.TblLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabCtrlSetting
        '
        Me.TabCtrlSetting.Controls.Add(Me.TabGeneral)
        Me.TabCtrlSetting.Controls.Add(Me.TabProses)
        Me.TabCtrlSetting.Controls.Add(Me.TabAdmin)
        Me.TabCtrlSetting.Location = New System.Drawing.Point(5, 7)
        Me.TabCtrlSetting.Name = "TabCtrlSetting"
        Me.TabCtrlSetting.SelectedIndex = 0
        Me.TabCtrlSetting.Size = New System.Drawing.Size(409, 234)
        Me.TabCtrlSetting.TabIndex = 0
        '
        'TabGeneral
        '
        Me.TabGeneral.BackColor = System.Drawing.SystemColors.Control
        Me.TabGeneral.Controls.Add(Me.ChkStatProgress)
        Me.TabGeneral.Location = New System.Drawing.Point(4, 22)
        Me.TabGeneral.Name = "TabGeneral"
        Me.TabGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabGeneral.Size = New System.Drawing.Size(401, 208)
        Me.TabGeneral.TabIndex = 0
        Me.TabGeneral.Text = "Umum"
        '
        'ChkStatProgress
        '
        Me.ChkStatProgress.AutoSize = True
        Me.ChkStatProgress.Location = New System.Drawing.Point(7, 18)
        Me.ChkStatProgress.Name = "ChkStatProgress"
        Me.ChkStatProgress.Size = New System.Drawing.Size(142, 17)
        Me.ChkStatProgress.TabIndex = 0
        Me.ChkStatProgress.Text = "Aktifkan Status Progress"
        Me.ChkStatProgress.UseVisualStyleBackColor = True
        '
        'TabProses
        '
        Me.TabProses.BackColor = System.Drawing.SystemColors.Control
        Me.TabProses.Controls.Add(Me.GrpTglProses)
        Me.TabProses.Location = New System.Drawing.Point(4, 22)
        Me.TabProses.Name = "TabProses"
        Me.TabProses.Padding = New System.Windows.Forms.Padding(3)
        Me.TabProses.Size = New System.Drawing.Size(401, 208)
        Me.TabProses.TabIndex = 1
        Me.TabProses.Text = "Proses"
        '
        'GrpTglProses
        '
        Me.GrpTglProses.Controls.Add(Me.RdoTglLastProcess)
        Me.GrpTglProses.Controls.Add(Me.RdoTglCurrent)
        Me.GrpTglProses.Location = New System.Drawing.Point(7, 14)
        Me.GrpTglProses.Name = "GrpTglProses"
        Me.GrpTglProses.Size = New System.Drawing.Size(276, 52)
        Me.GrpTglProses.TabIndex = 0
        Me.GrpTglProses.TabStop = False
        Me.GrpTglProses.Text = "Default Tanggal Proses"
        '
        'RdoTglLastProcess
        '
        Me.RdoTglLastProcess.AutoSize = True
        Me.RdoTglLastProcess.Location = New System.Drawing.Point(122, 20)
        Me.RdoTglLastProcess.Name = "RdoTglLastProcess"
        Me.RdoTglLastProcess.Size = New System.Drawing.Size(141, 17)
        Me.RdoTglLastProcess.TabIndex = 1
        Me.RdoTglLastProcess.TabStop = True
        Me.RdoTglLastProcess.Text = "Tanggal Proses &Terakhir"
        Me.RdoTglLastProcess.UseVisualStyleBackColor = True
        '
        'RdoTglCurrent
        '
        Me.RdoTglCurrent.AutoSize = True
        Me.RdoTglCurrent.Location = New System.Drawing.Point(7, 20)
        Me.RdoTglCurrent.Name = "RdoTglCurrent"
        Me.RdoTglCurrent.Size = New System.Drawing.Size(100, 17)
        Me.RdoTglCurrent.TabIndex = 0
        Me.RdoTglCurrent.TabStop = True
        Me.RdoTglCurrent.Text = "Tanggal &Hari Ini"
        Me.RdoTglCurrent.UseVisualStyleBackColor = True
        '
        'TabAdmin
        '
        Me.TabAdmin.BackColor = System.Drawing.SystemColors.Control
        Me.TabAdmin.Controls.Add(Me.ChkLogTrans)
        Me.TabAdmin.Controls.Add(Me.ChkTrapErr)
        Me.TabAdmin.Location = New System.Drawing.Point(4, 22)
        Me.TabAdmin.Name = "TabAdmin"
        Me.TabAdmin.Padding = New System.Windows.Forms.Padding(3)
        Me.TabAdmin.Size = New System.Drawing.Size(401, 208)
        Me.TabAdmin.TabIndex = 2
        Me.TabAdmin.Text = "Admin"
        '
        'ChkTrapErr
        '
        Me.ChkTrapErr.AutoSize = True
        Me.ChkTrapErr.Location = New System.Drawing.Point(7, 18)
        Me.ChkTrapErr.Name = "ChkTrapErr"
        Me.ChkTrapErr.Size = New System.Drawing.Size(133, 17)
        Me.ChkTrapErr.TabIndex = 0
        Me.ChkTrapErr.Text = "&Tampilkan Setiap Error"
        Me.ChkTrapErr.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(3, 3)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 23)
        Me.BtnOK.TabIndex = 1
        Me.BtnOK.Text = "&OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnTutup
        '
        Me.BtnTutup.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnTutup.Location = New System.Drawing.Point(86, 3)
        Me.BtnTutup.Name = "BtnTutup"
        Me.BtnTutup.Size = New System.Drawing.Size(75, 23)
        Me.BtnTutup.TabIndex = 2
        Me.BtnTutup.Text = "&Tutup"
        Me.BtnTutup.UseVisualStyleBackColor = True
        '
        'TblLayoutPanel1
        '
        Me.TblLayoutPanel1.ColumnCount = 2
        Me.TblLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPanel1.Controls.Add(Me.BtnOK, 0, 0)
        Me.TblLayoutPanel1.Controls.Add(Me.BtnTutup, 1, 0)
        Me.TblLayoutPanel1.Location = New System.Drawing.Point(248, 247)
        Me.TblLayoutPanel1.Name = "TblLayoutPanel1"
        Me.TblLayoutPanel1.RowCount = 1
        Me.TblLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPanel1.Size = New System.Drawing.Size(166, 31)
        Me.TblLayoutPanel1.TabIndex = 3
        '
        'ChkLogTrans
        '
        Me.ChkLogTrans.AutoSize = True
        Me.ChkLogTrans.Location = New System.Drawing.Point(6, 41)
        Me.ChkLogTrans.Name = "ChkLogTrans"
        Me.ChkLogTrans.Size = New System.Drawing.Size(157, 17)
        Me.ChkLogTrans.TabIndex = 1
        Me.ChkLogTrans.Text = "&Update Data Log Transaksi"
        Me.ChkLogTrans.UseVisualStyleBackColor = True
        '
        'FrmSettingAplikasi
        '
        Me.AcceptButton = Me.BtnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.BtnTutup
        Me.ClientSize = New System.Drawing.Size(418, 279)
        Me.Controls.Add(Me.TblLayoutPanel1)
        Me.Controls.Add(Me.TabCtrlSetting)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSettingAplikasi"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting Aplikasi"
        Me.TabCtrlSetting.ResumeLayout(False)
        Me.TabGeneral.ResumeLayout(False)
        Me.TabGeneral.PerformLayout()
        Me.TabProses.ResumeLayout(False)
        Me.GrpTglProses.ResumeLayout(False)
        Me.GrpTglProses.PerformLayout()
        Me.TabAdmin.ResumeLayout(False)
        Me.TabAdmin.PerformLayout()
        Me.TblLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabCtrlSetting As System.Windows.Forms.TabControl
    Friend WithEvents TabProses As System.Windows.Forms.TabPage
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents BtnTutup As System.Windows.Forms.Button
    Friend WithEvents TblLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabAdmin As System.Windows.Forms.TabPage
    Friend WithEvents TabGeneral As System.Windows.Forms.TabPage
    Friend WithEvents ChkTrapErr As System.Windows.Forms.CheckBox
    Friend WithEvents ChkStatProgress As System.Windows.Forms.CheckBox
    Friend WithEvents GrpTglProses As System.Windows.Forms.GroupBox
    Friend WithEvents RdoTglLastProcess As System.Windows.Forms.RadioButton
    Friend WithEvents RdoTglCurrent As System.Windows.Forms.RadioButton
    Friend WithEvents ChkLogTrans As System.Windows.Forms.CheckBox
End Class
