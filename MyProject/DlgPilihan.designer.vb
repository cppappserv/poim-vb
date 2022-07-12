<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DlgPilihan
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
        Me.DgvResult = New System.Windows.Forms.DataGridView
        Me.TxtKey1 = New System.Windows.Forms.TextBox
        Me.LblKey1 = New System.Windows.Forms.Label
        Me.BtnBatal = New System.Windows.Forms.Button
        Me.BtnOK = New System.Windows.Forms.Button
        Me.TblLayoutPanel = New System.Windows.Forms.TableLayoutPanel
        Me.LblKey2 = New System.Windows.Forms.Label
        Me.TxtKey2 = New System.Windows.Forms.TextBox
        CType(Me.DgvResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TblLayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'DgvResult
        '
        Me.DgvResult.AllowUserToAddRows = False
        Me.DgvResult.AllowUserToDeleteRows = False
        Me.DgvResult.AllowUserToOrderColumns = True
        Me.DgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvResult.Location = New System.Drawing.Point(12, 64)
        Me.DgvResult.Name = "DgvResult"
        Me.DgvResult.ReadOnly = True
        Me.DgvResult.Size = New System.Drawing.Size(370, 167)
        Me.DgvResult.TabIndex = 1
        '
        'TxtKey1
        '
        Me.TxtKey1.Location = New System.Drawing.Point(105, 12)
        Me.TxtKey1.Name = "TxtKey1"
        Me.TxtKey1.Size = New System.Drawing.Size(277, 20)
        Me.TxtKey1.TabIndex = 2
        '
        'LblKey1
        '
        Me.LblKey1.AutoSize = True
        Me.LblKey1.Location = New System.Drawing.Point(12, 15)
        Me.LblKey1.Name = "LblKey1"
        Me.LblKey1.Size = New System.Drawing.Size(40, 13)
        Me.LblKey1.TabIndex = 3
        Me.LblKey1.Text = "Kunci :"
        '
        'BtnBatal
        '
        Me.BtnBatal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnBatal.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnBatal.Location = New System.Drawing.Point(76, 3)
        Me.BtnBatal.Name = "BtnBatal"
        Me.BtnBatal.Size = New System.Drawing.Size(67, 23)
        Me.BtnBatal.TabIndex = 1
        Me.BtnBatal.Text = "Cancel"
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnOK.Location = New System.Drawing.Point(3, 3)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(67, 23)
        Me.BtnOK.TabIndex = 0
        Me.BtnOK.Text = "OK"
        '
        'TblLayoutPanel
        '
        Me.TblLayoutPanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TblLayoutPanel.ColumnCount = 2
        Me.TblLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPanel.Controls.Add(Me.BtnOK, 0, 0)
        Me.TblLayoutPanel.Controls.Add(Me.BtnBatal, 1, 0)
        Me.TblLayoutPanel.Location = New System.Drawing.Point(236, 236)
        Me.TblLayoutPanel.Name = "TblLayoutPanel"
        Me.TblLayoutPanel.RowCount = 1
        Me.TblLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPanel.Size = New System.Drawing.Size(146, 29)
        Me.TblLayoutPanel.TabIndex = 0
        '
        'LblKey2
        '
        Me.LblKey2.AutoSize = True
        Me.LblKey2.Location = New System.Drawing.Point(12, 41)
        Me.LblKey2.Name = "LblKey2"
        Me.LblKey2.Size = New System.Drawing.Size(40, 13)
        Me.LblKey2.TabIndex = 5
        Me.LblKey2.Text = "Kunci :"
        '
        'TxtKey2
        '
        Me.TxtKey2.Location = New System.Drawing.Point(105, 38)
        Me.TxtKey2.Name = "TxtKey2"
        Me.TxtKey2.Size = New System.Drawing.Size(277, 20)
        Me.TxtKey2.TabIndex = 4
        '
        'DlgPilihan
        '
        Me.AcceptButton = Me.BtnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnBatal
        Me.ClientSize = New System.Drawing.Size(394, 277)
        Me.Controls.Add(Me.LblKey2)
        Me.Controls.Add(Me.TxtKey2)
        Me.Controls.Add(Me.LblKey1)
        Me.Controls.Add(Me.TxtKey1)
        Me.Controls.Add(Me.DgvResult)
        Me.Controls.Add(Me.TblLayoutPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DlgPilihan"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Dialog "
        CType(Me.DgvResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TblLayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DgvResult As System.Windows.Forms.DataGridView
    Friend WithEvents TxtKey1 As System.Windows.Forms.TextBox
    Friend WithEvents LblKey1 As System.Windows.Forms.Label
    Friend WithEvents BtnBatal As System.Windows.Forms.Button
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents TblLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LblKey2 As System.Windows.Forms.Label
    Friend WithEvents TxtKey2 As System.Windows.Forms.TextBox

End Class
