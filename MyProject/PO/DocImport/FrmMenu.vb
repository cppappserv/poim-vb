Public Class FrmMenu
    Dim ErrMsg, SQLstr As String
    Dim PilihanDlg As New DlgPilihan
    Dim MyReader, MyReader2 As MySqlDataReader
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim ViewerFrm As New Frm_CRViewer
    '    Dim v_pono As String

    '    v_pono = txtpono.ToString

    '    ViewerFrm.Tag = "BOLC" & v_pono
    '    ViewerFrm.ShowDialog()
    'End Sub

    'Private Sub FrmMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Dim var1 As String
    '    Dim stradd() As String
    '    Dim z As Integer
    '    var1 = AmbilData("doc_no", "tbl_si_doc", "po_no='123450001'")

    '    stradd = Split(var1, vbCrLf, -1, CompareMethod.Text)
    '    'Label1.Text = ""
    '    'For z = LBound(stradd) To UBound(stradd)
    '    '    Label1.Text = Label1.Text & " - " & stradd(z)
    '    'Next z
    '    Dim v_docname, v_gabsidoc As String
    '    Dim no As Integer = 1
    '    v_gabsidoc = ""
    '    For z = LBound(stradd) To UBound(stradd)
    '        'no = z + 1
    '        v_docname = ""
    '        v_docname = AmbilData("doc_name", "tbm_document", "doc_code = '" & stradd(z) & "'")
    '        v_gabsidoc = v_gabsidoc & no & ". " & v_docname & vbCrLf
    '        no = no + 1
    '    Next z
    '    Label1.Text = v_gabsidoc
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim f As New FrmSSPCP(txtNO.Text, "", "", "", "", "")
        'If DataOK("SP") = True Then
        f.ShowDialog()
        'End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim f As New FrmBR(txtNO.Text, "USD", "USD", "0", "0", "")
        If dataOK("BR") = True Then
            f.ShowDialog()
        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'cek dulu jumlah BR - 1 = jumlah DI
        Dim v_br, v_di As String
        v_br = AmbilData("count(ord_no)", "tbl_remitance", "shipment_no = '" & txtNO.Text & "'")
        v_di = AmbilData("count(ord_no)", "tbl_shipping_doc", "shipment_no = '" & txtNO.Text & "' and findoc_type = 'DI'")
        If (v_br - 1) <> v_di Then
            MsgBox("BR no available")
        Else
            Dim f As New FrmDI(txtNO.Text, "", "", "", "", "")
            f.ShowDialog()
        End If
        
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim f As New FrmPV(txtNO.Text, "", "", "", "", "")
        If DataOK("PV") = True Then
            f.ShowDialog()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim f As New FrmKOSK(txtNO.Text, "", "", "", "", "", "KO")
        If DataOK("KO") = True Then
            f.ShowDialog()
        End If

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim f As New FrmKOSK(txtNO.Text, "", "", "", "", "", "SK")
        If DataOK("SK") = True Then
            f.ShowDialog()
        End If
    End Sub



    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim chosen As String
        chosen = no_doc.Text()
        If Mid(chosen, 1, 2) = "BR" Then
            'Dim f As New FrmBR(txtNO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, chosen)
            Dim f As New FrmBR(txtNO.Text, "", "", "0", "0", chosen)
            f.ShowDialog()
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim chosen As String
        chosen = no_doc.Text()
        If Mid(chosen, 1, 2) = "DI" Then
            Dim f As New FrmDI(txtNO.Text, "", "", "0", "0", chosen)
            f.ShowDialog()
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim chosen As String
        chosen = no_doc.Text()
        If Mid(chosen, 1, 2) = "PV" Then
            Dim f As New FrmPV(txtNO.Text, "", "", "0", "0", chosen)
            f.ShowDialog()
        End If
    End Sub
    Private Function DataOK(ByVal jns As String) As Boolean
        Dim mess1, mess2 As String
        Dim status As String = ""
        Dim sisaQty As Decimal

        If jns = "PV" Then
            ErrMsg = "Failed when read PV data"
            mess1 = "Payment Voucher has been closed"
            mess2 = "Payment Voucher has been created"

            SQLstr = "select FINDOC_STATUS from tbl_shipping_doc " & _
                     "where SHIPMENT_NO='" & txtNO.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_shipping_doc where " & _
                     "SHIPMENT_NO='" & txtNO.Text & "' and FINDOC_TYPE='" & jns & "') " & _
                     "and FINDOC_TYPE='" & jns & "'"
        End If
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    status = MyReader.GetString("FINDOC_STATUS")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        If (status = "Approved" Or status = "Closed") Then
            If status = "Closed" Then
                MsgBox(mess1)
            Else
                'If jns <> "RIL" Then
                MsgBox(mess2)
                DataOK = False
                'Else
                '    sisaQty = GetSisaQty()
                '    If sisaQty > 0 Then
                '        DataOK = True
                '    Else
                '        MsgBox(mess2)
                '        DataOK = False
                '    End If
                'End If
            End If
        Else
            DataOK = True
        End If
    End Function

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click

    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click

    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        lblNPWP.Text = f_SPLITNPWP(txtnpwp.Text, 3)
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim f As New FrmCL(txtNO.Text, "", "", "", "", "", "CL")
        'If DataOK("CL") = True Then
        f.ShowDialog()
        'End If
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim chosen As String
        chosen = no_doc.Text()
        If Mid(chosen, 1, 2) = "CL" Then
            'Dim f As New FrmBR(txtNO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, chosen)
            Dim f As New FrmCL(txtNO.Text, "", "", "0", "0", chosen, "CL")
            f.ShowDialog()
        End If
    End Sub

    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim f As New FrmTT(txtNO.Text, "", "", "", "", "", "TT")
        'If DataOK("CL") = True Then
        f.ShowDialog()
    End Sub

    Private Sub Button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button16.Click
        Dim chosen As String
        chosen = no_doc.Text()
        If Mid(chosen, 1, 2) = "TT" Then
            'Dim f As New FrmBR(txtNO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, chosen)
            Dim f As New FrmTT(txtNO.Text, "", "", "0", "0", chosen, "TT")
            f.ShowDialog()
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        Dim f As New FrmCD
        f.ShowDialog()

    End Sub
End Class