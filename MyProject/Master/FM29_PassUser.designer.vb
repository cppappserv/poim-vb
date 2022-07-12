<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FM29_PassUser
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LblUserName = New System.Windows.Forms.Label
        Me.LblUserId = New System.Windows.Forms.Label
        Me.BttnOK = New System.Windows.Forms.Button
        Me.BttnCancel = New System.Windows.Forms.Button
        Me.TxtKonfPass = New System.Windows.Forms.TextBox
        Me.TxtNewPass = New System.Windows.Forms.TextBox
        Me.TxtOldPass = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(490, 25)
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "User Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "User Id"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblUserName)
        Me.GroupBox1.Controls.Add(Me.LblUserId)
        Me.GroupBox1.Controls.Add(Me.BttnOK)
        Me.GroupBox1.Controls.Add(Me.BttnCancel)
        Me.GroupBox1.Controls.Add(Me.TxtKonfPass)
        Me.GroupBox1.Controls.Add(Me.TxtNewPass)
        Me.GroupBox1.Controls.Add(Me.TxtOldPass)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(466, 217)
        Me.GroupBox1.TabIndex = 55
        Me.GroupBox1.TabStop = False
        '
        'LblUserName
        '
        Me.LblUserName.AutoSize = True
        Me.LblUserName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblUserName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserName.Location = New System.Drawing.Point(137, 45)
        Me.LblUserName.Name = "LblUserName"
        Me.LblUserName.Size = New System.Drawing.Size(58, 15)
        Me.LblUserName.TabIndex = 59
        Me.LblUserName.Text = "UserName"
        '
        'LblUserId
        '
        Me.LblUserId.AutoSize = True
        Me.LblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblUserId.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserId.Location = New System.Drawing.Point(137, 22)
        Me.LblUserId.Name = "LblUserId"
        Me.LblUserId.Size = New System.Drawing.Size(41, 15)
        Me.LblUserId.TabIndex = 58
        Me.LblUserId.Text = "UserId"
        '
        'BttnOK
        '
        Me.BttnOK.Location = New System.Drawing.Point(218, 182)
        Me.BttnOK.Name = "BttnOK"
        Me.BttnOK.Size = New System.Drawing.Size(75, 23)
        Me.BttnOK.TabIndex = 5
        Me.BttnOK.Text = "Save"
        Me.BttnOK.UseVisualStyleBackColor = True
        '
        'BttnCancel
        '
        Me.BttnCancel.Location = New System.Drawing.Point(137, 182)
        Me.BttnCancel.Name = "BttnCancel"
        Me.BttnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BttnCancel.TabIndex = 6
        Me.BttnCancel.Text = "Cancel"
        Me.BttnCancel.UseVisualStyleBackColor = True
        '
        'TxtKonfPass
        '
        Me.TxtKonfPass.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtKonfPass.Location = New System.Drawing.Point(137, 121)
        Me.TxtKonfPass.MaxLength = 40
        Me.TxtKonfPass.Name = "TxtKonfPass"
        Me.TxtKonfPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtKonfPass.Size = New System.Drawing.Size(156, 21)
        Me.TxtKonfPass.TabIndex = 4
        '
        'TxtNewPass
        '
        Me.TxtNewPass.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNewPass.Location = New System.Drawing.Point(137, 98)
        Me.TxtNewPass.MaxLength = 40
        Me.TxtNewPass.Name = "TxtNewPass"
        Me.TxtNewPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNewPass.Size = New System.Drawing.Size(156, 21)
        Me.TxtNewPass.TabIndex = 3
        '
        'TxtOldPass
        '
        Me.TxtOldPass.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOldPass.Location = New System.Drawing.Point(137, 75)
        Me.TxtOldPass.MaxLength = 40
        Me.TxtOldPass.Name = "TxtOldPass"
        Me.TxtOldPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtOldPass.Size = New System.Drawing.Size(156, 21)
        Me.TxtOldPass.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(67, 13)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "Konfirmation"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "New Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Old Password"
        '
        'FM29_PassUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 269)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FM29_PassUser"
        Me.Text = "FM29_PassUser"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtKonfPass As System.Windows.Forms.TextBox
    Friend WithEvents TxtNewPass As System.Windows.Forms.TextBox
    Friend WithEvents TxtOldPass As System.Windows.Forms.TextBox
    Friend WithEvents BttnOK As System.Windows.Forms.Button
    Friend WithEvents BttnCancel As System.Windows.Forms.Button
    Friend WithEvents LblUserName As System.Windows.Forms.Label
    Friend WithEvents LblUserId As System.Windows.Forms.Label
End Class
