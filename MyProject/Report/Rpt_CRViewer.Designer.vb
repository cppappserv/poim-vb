<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rpt_CRViewer
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
        Me.CRV_Viewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'CRV_Viewer
        '
        Me.CRV_Viewer.ActiveViewIndex = -1
        Me.CRV_Viewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRV_Viewer.DisplayGroupTree = False
        Me.CRV_Viewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRV_Viewer.Location = New System.Drawing.Point(0, 0)
        Me.CRV_Viewer.Name = "CRV_Viewer"
        Me.CRV_Viewer.SelectionFormula = ""
        Me.CRV_Viewer.Size = New System.Drawing.Size(719, 527)
        Me.CRV_Viewer.TabIndex = 0
        Me.CRV_Viewer.ViewTimeSelectionFormula = ""
        '
        'Rpt_CRViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 527)
        Me.Controls.Add(Me.CRV_Viewer)
        Me.Name = "Rpt_CRViewer"
        Me.Text = "Report using Crystal Report"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRV_Viewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
