Public Class frmPilih
    Dim ViewerFrm As New Frm_CRViewer
    Private Sub frmPilih_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Tag.ToString.Substring(0, 4) = "CCCC" Then
            cbPilih.Items.Add("print Clearance Cost with estimated Other Cost")
            cbPilih.Items.Add("print Clearance Cost without estimated Other Cost")
            cbPilih.Items.Add("print estimated Other Cost")
            cbPilih.Text = "Pilih"
        End If
        If Me.Tag.ToString.Substring(0, 4) = "CSCS" Then
            cbPilih.Items.Add("print R/M Cost Slip with estimated Other Cost")
            cbPilih.Items.Add("print R/M Cost Slip without estimated Other Cost")
            cbPilih.Items.Add("print estimated Other Cost")
            cbPilih.Text = "Pilih"
        End If
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        xto_sap = "0"
        Select Case cbPilih.Text
            Case "print Clearance Cost with estimated Other Cost"
                ViewerFrm.Tag = Me.Tag.ToString + "01"
            Case "print Clearance Cost without estimated Other Cost"
                ViewerFrm.Tag = Me.Tag.ToString + "02"
            Case "print estimated Other Cost"
                ViewerFrm.Tag = Me.Tag.ToString + "03"
            Case "print R/M Cost Slip with estimated Other Cost"
                ViewerFrm.Tag = Me.Tag.ToString + "01"
            Case "print R/M Cost Slip without estimated Other Cost"
                ViewerFrm.Tag = Me.Tag.ToString + "02"
                If xto_sap2 = "CS" Then
                    xto_sap = "1"
                End If
            Case "print estimated Other Cost"
                ViewerFrm.Tag = Me.Tag.ToString + "03"
            Case Else
                Me.Hide()
                MsgBox("Tidak ada tipe laporan yang sesuai untuk dicetak.", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
        End Select
        Me.Close()
        ViewerFrm.ShowDialog()
    End Sub
End Class