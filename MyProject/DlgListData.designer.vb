<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DlgListData
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
        Me.BtnOK = New System.Windows.Forms.Button
        Me.DgvResult = New System.Windows.Forms.DataGridView
        CType(Me.DgvResult, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnOK.Location = New System.Drawing.Point(150, 287)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(67, 23)
        Me.BtnOK.TabIndex = 0
        Me.BtnOK.Text = "OK"
        '
        'DgvResult
        '
        Me.DgvResult.AllowUserToAddRows = False
        Me.DgvResult.AllowUserToDeleteRows = False
        Me.DgvResult.AllowUserToOrderColumns = True
        Me.DgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvResult.Location = New System.Drawing.Point(13, 13)
        Me.DgvResult.Name = "DgvResult"
        Me.DgvResult.ReadOnly = True
        Me.DgvResult.Size = New System.Drawing.Size(345, 265)
        Me.DgvResult.TabIndex = 1
        '
        'DlgListData
        '
        Me.AcceptButton = Me.BtnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(370, 315)
        Me.Controls.Add(Me.DgvResult)
        Me.Controls.Add(Me.BtnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DlgListData"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "DlgListData"
        CType(Me.DgvResult, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents DgvResult As System.Windows.Forms.DataGridView

End Class
