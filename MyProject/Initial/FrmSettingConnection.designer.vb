<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSettingConnection
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSettingConnection))
        Me.LblServer = New System.Windows.Forms.Label
        Me.LblDB = New System.Windows.Forms.Label
        Me.LblUser = New System.Windows.Forms.Label
        Me.LblPasword = New System.Windows.Forms.Label
        Me.BtnSimpan = New System.Windows.Forms.Button
        Me.TxtServer = New System.Windows.Forms.TextBox
        Me.TxtDB = New System.Windows.Forms.TextBox
        Me.TxtUser = New System.Windows.Forms.TextBox
        Me.TxtPassword = New System.Windows.Forms.TextBox
        Me.BtnTest = New System.Windows.Forms.Button
        Me.LblServerSample = New System.Windows.Forms.Label
        Me.LblDBSample = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GrpBoxSample = New System.Windows.Forms.GroupBox
        Me.GrpBoxSample.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblServer
        '
        Me.LblServer.AutoSize = True
        Me.LblServer.Location = New System.Drawing.Point(16, 42)
        Me.LblServer.Name = "LblServer"
        Me.LblServer.Size = New System.Drawing.Size(90, 13)
        Me.LblServer.TabIndex = 7
        Me.LblServer.Text = "Nama Server / IP"
        '
        'LblDB
        '
        Me.LblDB.AutoSize = True
        Me.LblDB.Location = New System.Drawing.Point(16, 70)
        Me.LblDB.Name = "LblDB"
        Me.LblDB.Size = New System.Drawing.Size(53, 13)
        Me.LblDB.TabIndex = 8
        Me.LblDB.Text = "Nama DB"
        '
        'LblUser
        '
        Me.LblUser.AutoSize = True
        Me.LblUser.Location = New System.Drawing.Point(16, 98)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(60, 13)
        Me.LblUser.TabIndex = 9
        Me.LblUser.Text = "Nama User"
        '
        'LblPasword
        '
        Me.LblPasword.AutoSize = True
        Me.LblPasword.Location = New System.Drawing.Point(16, 124)
        Me.LblPasword.Name = "LblPasword"
        Me.LblPasword.Size = New System.Drawing.Size(53, 13)
        Me.LblPasword.TabIndex = 10
        Me.LblPasword.Text = "Password"
        '
        'BtnSimpan
        '
        Me.BtnSimpan.Location = New System.Drawing.Point(200, 161)
        Me.BtnSimpan.Name = "BtnSimpan"
        Me.BtnSimpan.Size = New System.Drawing.Size(75, 23)
        Me.BtnSimpan.TabIndex = 5
        Me.BtnSimpan.Text = "&Simpan"
        Me.BtnSimpan.UseVisualStyleBackColor = True
        '
        'TxtServer
        '
        Me.TxtServer.Location = New System.Drawing.Point(122, 35)
        Me.TxtServer.MaxLength = 20
        Me.TxtServer.Name = "TxtServer"
        Me.TxtServer.Size = New System.Drawing.Size(163, 20)
        Me.TxtServer.TabIndex = 0
        '
        'TxtDB
        '
        Me.TxtDB.Location = New System.Drawing.Point(122, 63)
        Me.TxtDB.MaxLength = 20
        Me.TxtDB.Name = "TxtDB"
        Me.TxtDB.Size = New System.Drawing.Size(163, 20)
        Me.TxtDB.TabIndex = 1
        '
        'TxtUser
        '
        Me.TxtUser.Location = New System.Drawing.Point(122, 91)
        Me.TxtUser.MaxLength = 20
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.Size = New System.Drawing.Size(163, 20)
        Me.TxtUser.TabIndex = 2
        '
        'TxtPassword
        '
        Me.TxtPassword.Location = New System.Drawing.Point(122, 117)
        Me.TxtPassword.MaxLength = 20
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(163, 20)
        Me.TxtPassword.TabIndex = 3
        '
        'BtnTest
        '
        Me.BtnTest.Location = New System.Drawing.Point(99, 161)
        Me.BtnTest.Name = "BtnTest"
        Me.BtnTest.Size = New System.Drawing.Size(79, 23)
        Me.BtnTest.TabIndex = 4
        Me.BtnTest.Text = "&Test Koneksi"
        Me.BtnTest.UseVisualStyleBackColor = True
        '
        'LblServerSample
        '
        Me.LblServerSample.AutoSize = True
        Me.LblServerSample.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.LblServerSample.Location = New System.Drawing.Point(7, 25)
        Me.LblServerSample.Name = "LblServerSample"
        Me.LblServerSample.Size = New System.Drawing.Size(49, 13)
        Me.LblServerSample.TabIndex = 12
        Me.LblServerSample.Text = "10.108.5.122"
        '
        'LblDBSample
        '
        Me.LblDBSample.AutoSize = True
        Me.LblDBSample.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.LblDBSample.Location = New System.Drawing.Point(7, 53)
        Me.LblDBSample.Name = "LblDBSample"
        Me.LblDBSample.Size = New System.Drawing.Size(66, 13)
        Me.LblDBSample.TabIndex = 13
        Me.LblDBSample.Text = "impr"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(7, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "user"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(6, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "password"
        '
        'GrpBoxSample
        '
        Me.GrpBoxSample.Controls.Add(Me.LblServerSample)
        Me.GrpBoxSample.Controls.Add(Me.Label3)
        Me.GrpBoxSample.Controls.Add(Me.LblDBSample)
        Me.GrpBoxSample.Controls.Add(Me.Label2)
        Me.GrpBoxSample.Location = New System.Drawing.Point(304, 17)
        Me.GrpBoxSample.Name = "GrpBoxSample"
        Me.GrpBoxSample.Size = New System.Drawing.Size(111, 138)
        Me.GrpBoxSample.TabIndex = 16
        Me.GrpBoxSample.TabStop = False
        Me.GrpBoxSample.Text = "Sample Koneksi"
        '
        'FrmSetConnection
        '
        Me.AcceptButton = Me.BtnSimpan
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 209)
        Me.Controls.Add(Me.GrpBoxSample)
        Me.Controls.Add(Me.BtnTest)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.TxtUser)
        Me.Controls.Add(Me.TxtDB)
        Me.Controls.Add(Me.TxtServer)
        Me.Controls.Add(Me.BtnSimpan)
        Me.Controls.Add(Me.LblPasword)
        Me.Controls.Add(Me.LblUser)
        Me.Controls.Add(Me.LblDB)
        Me.Controls.Add(Me.LblServer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSetConnection"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Setting Koneksi Mysql"
        Me.GrpBoxSample.ResumeLayout(False)
        Me.GrpBoxSample.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblServer As System.Windows.Forms.Label
    Friend WithEvents LblDB As System.Windows.Forms.Label
    Friend WithEvents LblUser As System.Windows.Forms.Label
    Friend WithEvents LblPasword As System.Windows.Forms.Label
    Friend WithEvents BtnSimpan As System.Windows.Forms.Button
    Friend WithEvents TxtServer As System.Windows.Forms.TextBox
    Friend WithEvents TxtDB As System.Windows.Forms.TextBox
    Friend WithEvents TxtUser As System.Windows.Forms.TextBox
    Friend WithEvents TxtPassword As System.Windows.Forms.TextBox
    Friend WithEvents BtnTest As System.Windows.Forms.Button
    Friend WithEvents LblServerSample As System.Windows.Forms.Label
    Friend WithEvents LblDBSample As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GrpBoxSample As System.Windows.Forms.GroupBox
End Class
