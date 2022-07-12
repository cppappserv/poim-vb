<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenu
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMenu))
        Me.Button1 = New System.Windows.Forms.Button
        Me.txtNO = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.PrintForm1 = New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm(Me.components)
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.no_doc = New System.Windows.Forms.TextBox
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button10 = New System.Windows.Forms.Button
        Me.Button11 = New System.Windows.Forms.Button
        Me.Button12 = New System.Windows.Forms.Button
        Me.txtnpwp = New System.Windows.Forms.TextBox
        Me.Button13 = New System.Windows.Forms.Button
        Me.lblNPWP = New System.Windows.Forms.Label
        Me.Button14 = New System.Windows.Forms.Button
        Me.Button15 = New System.Windows.Forms.Button
        Me.Button16 = New System.Windows.Forms.Button
        Me.Button17 = New System.Windows.Forms.Button
        Me.Button18 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(28, 253)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(178, 24)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "FrmSSPCP"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtNO
        '
        Me.txtNO.Location = New System.Drawing.Point(102, 37)
        Me.txtNO.Name = "txtNO"
        Me.txtNO.Size = New System.Drawing.Size(104, 20)
        Me.txtNO.TabIndex = 1
        Me.txtNO.Text = "1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Shipment No."
        '
        'PrintForm1
        '
        Me.PrintForm1.DocumentName = "document"
        Me.PrintForm1.Form = Me
        Me.PrintForm1.PrintAction = System.Drawing.Printing.PrintAction.PrintToPrinter
        Me.PrintForm1.PrinterSettings = CType(resources.GetObject("PrintForm1.PrinterSettings"), System.Drawing.Printing.PrinterSettings)
        Me.PrintForm1.PrintFileName = Nothing
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(28, 91)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(178, 26)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "FrmBR"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(28, 123)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(178, 27)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "FrmDI"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(28, 156)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(178, 27)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "FrmPV"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(28, 189)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(178, 27)
        Me.Button5.TabIndex = 6
        Me.Button5.Text = "FrmKO"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(28, 222)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(178, 27)
        Me.Button6.TabIndex = 7
        Me.Button6.Text = "FrmSK"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'no_doc
        '
        Me.no_doc.Location = New System.Drawing.Point(268, 37)
        Me.no_doc.Name = "no_doc"
        Me.no_doc.Size = New System.Drawing.Size(180, 20)
        Me.no_doc.TabIndex = 8
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(270, 222)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(178, 27)
        Me.Button7.TabIndex = 14
        Me.Button7.Text = "View doc FrmSK"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(270, 189)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(178, 27)
        Me.Button8.TabIndex = 13
        Me.Button8.Text = "View doc FrmKO"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(270, 156)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(178, 27)
        Me.Button9.TabIndex = 12
        Me.Button9.Text = "View doc FrmPV"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(270, 123)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(178, 27)
        Me.Button10.TabIndex = 11
        Me.Button10.Text = "View doc FrmDI"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(270, 91)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(178, 26)
        Me.Button11.TabIndex = 10
        Me.Button11.Text = "View doc FrmBR"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(270, 253)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(178, 24)
        Me.Button12.TabIndex = 9
        Me.Button12.Text = "View doc FrmSSPCP"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'txtnpwp
        '
        Me.txtnpwp.Location = New System.Drawing.Point(28, 357)
        Me.txtnpwp.Name = "txtnpwp"
        Me.txtnpwp.Size = New System.Drawing.Size(261, 20)
        Me.txtnpwp.TabIndex = 15
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(295, 357)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(47, 23)
        Me.Button13.TabIndex = 16
        Me.Button13.Text = "Button13"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'lblNPWP
        '
        Me.lblNPWP.AutoSize = True
        Me.lblNPWP.Location = New System.Drawing.Point(25, 380)
        Me.lblNPWP.Name = "lblNPWP"
        Me.lblNPWP.Size = New System.Drawing.Size(39, 13)
        Me.lblNPWP.TabIndex = 17
        Me.lblNPWP.Text = "Label2"
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(28, 283)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(178, 24)
        Me.Button14.TabIndex = 18
        Me.Button14.Text = "FrmCL"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(268, 283)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(178, 24)
        Me.Button15.TabIndex = 19
        Me.Button15.Text = "View doc FrmCL"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'Button16
        '
        Me.Button16.Location = New System.Drawing.Point(268, 313)
        Me.Button16.Name = "Button16"
        Me.Button16.Size = New System.Drawing.Size(178, 24)
        Me.Button16.TabIndex = 21
        Me.Button16.Text = "View doc FrmTT"
        Me.Button16.UseVisualStyleBackColor = True
        '
        'Button17
        '
        Me.Button17.Location = New System.Drawing.Point(28, 313)
        Me.Button17.Name = "Button17"
        Me.Button17.Size = New System.Drawing.Size(178, 24)
        Me.Button17.TabIndex = 20
        Me.Button17.Text = "FrmTT"
        Me.Button17.UseVisualStyleBackColor = True
        '
        'Button18
        '
        Me.Button18.Location = New System.Drawing.Point(360, 357)
        Me.Button18.Name = "Button18"
        Me.Button18.Size = New System.Drawing.Size(86, 24)
        Me.Button18.TabIndex = 22
        Me.Button18.Text = "ContractDoc"
        Me.Button18.UseVisualStyleBackColor = True
        '
        'FrmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 406)
        Me.Controls.Add(Me.Button18)
        Me.Controls.Add(Me.Button16)
        Me.Controls.Add(Me.Button17)
        Me.Controls.Add(Me.Button15)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.lblNPWP)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.txtnpwp)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button8)
        Me.Controls.Add(Me.Button9)
        Me.Controls.Add(Me.Button10)
        Me.Controls.Add(Me.Button11)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.no_doc)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNO)
        Me.Controls.Add(Me.Button1)
        Me.Name = "FrmMenu"
        Me.Text = "FrmMenu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtNO As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PrintForm1 As Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents no_doc As System.Windows.Forms.TextBox
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents lblNPWP As System.Windows.Forms.Label
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents txtnpwp As System.Windows.Forms.TextBox
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Button15 As System.Windows.Forms.Button
    Friend WithEvents Button16 As System.Windows.Forms.Button
    Friend WithEvents Button17 As System.Windows.Forms.Button
    Friend WithEvents Button18 As System.Windows.Forms.Button
End Class
