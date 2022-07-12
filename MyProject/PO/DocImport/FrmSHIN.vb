'Title                         : Transaksi Document Import - SHIN
'Form                          : FrmSHIN
'Table Used                    : tbm_document, tbm_attachment_doc, tbm_lines, tbl_si, tbl_si_doc, tbl_si_line
'Stored Procedure Used (MySQL) : SaveSHIN
'Created By                    : Yanti, Oktober 2008

Public Class FrmSHIN
    Dim PO, SHINNum, ShipNo As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim FinalApp As Boolean

    Private Function DataExist(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)

        If MyReader Is Nothing Then
            Return False
        Else
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return True
            End While
        End If
        CloseMyReader(MyReader, UserData)
    End Function
    Sub New(ByVal pono As String, ByVal SHIN As String)
        Dim strSQL, errMSg As String
        Dim xCek As Integer
        Dim lv_CTApp As Integer

        InitializeComponent()
        ShipNo = ""
        PO = pono
        xCek = InStr(PO, ";")
        If xCek > 0 Then
            ShipNo = Mid(PO, 1, xCek - 1)
            PO = Mid(PO, xCek + 1, Len(PO) - xCek)
        End If

        tgl.Value = GetServerDate()
        AppDt.Value = GetServerDate()
        AppDt.Checked = False
        FinalApp = False

        If Trim(SHIN) <> "" Then
            'existing data
            Call DisplayData(SHIN)
            btnSave.Enabled = (btnSave.Enabled) And (CRTCODE.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CRTCODE.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("SI-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
            lv_CTApp = IIf(CTApp.Text = "", 0, CTApp.Text)
            If (Status.Text = "Approved") And (lv_CTApp = UserData.UserCT) Then
                btnApprove.Enabled = True
            Else
                btnApprove.Enabled = False
            End If
            'added by estrika 20101105
            If (Status.Text = "Final Approved") And (lv_CTApp = UserData.UserCT) Then
                btnReject.Enabled = True
            Else
                btnReject.Enabled = False
            End If
        Else
            'create new

            strSQL = "SELECT remark FROM tbl_si " & _
                     "WHERE createdby='" & UserData.UserCT & "' ORDER BY createddt DESC LIMIT 1"

            errMSg = "Failed when read SHIN data"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        remark.Text = MyReader.GetString("Remark")
                    Catch ex As Exception
                        remark.Text = ""
                    End Try
                End While
            End If
            CloseMyReader(MyReader, UserData)

            SHINNum = "0"
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            crtdt.Text = GetServerDate.ToString
            footer.Text = AmbilData("footer_note", "tbl_si", "createdby=" & UserData.UserCT & " and trim(footer_note) <> '' order by createddt desc,po_no,ord_no  limit 1")
            Note.Text = AmbilData("note", "tbl_si", "createdby=" & UserData.UserCT & " and trim(note) <> '' order by createddt desc,po_no,ord_no  limit 1")
            Me.Text = "Shipping Instruction - New"
            btnReject.Enabled = False
            btnPrint.Enabled = False
            btnApprove.Enabled = False
        End If
    End Sub

    Private Sub DisplayData(ByVal SHIN As String)
        Dim pjg As Integer
        Dim strSQL, errMSg As String

        pjg = Len(RTrim(SHIN)) - 4
        num = CInt(Mid(SHIN, 5, pjg))
        SHINNum = num.ToString

        strSQL = " select a.*,b.doc_no, b.doc_name,c.line_code from tbl_si as a " & _
                 " inner join tbl_si_doc as b on a.po_no=b.po_no and a.ord_no=b.ord_no " & _
                 " inner join tbl_si_line as c on a.po_no=c.po_no and a.ord_no = c.ord_no " & _
                 " where ((a.shipment_no is null and a.po_no = '" & PO & "') or ('" & ShipNo & "' <> '' and a.shipment_no = '" & ShipNo & "')) and a.ord_no=" & num

        errMSg = "Failed when read SHIN data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    tgl.Text = MyReader.GetString("OpeningDt")
                    remark.Text = MyReader.GetString("Remark")
                    Try
                        CTApp.Text = MyReader.GetString("PreparedBy")
                    Catch ex As Exception
                        CTApp.Text = ""
                        AppDt.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        AppDt.Value = MyReader.GetString("PreparedDt")
                        AppDt.Checked = True
                    End If
                    footer.Text = MyReader.GetString("Footer_Note")
                    Note.Text = MyReader.GetString("Note")
                    '                    txtNPWP.Text = MyReader.GetString("NPWP_SI")
                    Status.Text = MyReader.GetString("status")
                    doc_no.Text = MyReader.GetString("Doc_No")
                    line_code.Text = MyReader.GetString("Line_Code")
                    docaddress.Text = MyReader.GetString("Doc_Address")
                    crt.Text = MyReader.GetString("CreatedBy")
                    CRTCODE.Text = MyReader.GetString("CreatedBy")
                    crtdt.Text = MyReader.GetString("CreatedDt")
                    If btnReject.Enabled = True Then
                        btnReject.Enabled = f_getenable(Status.Text)
                    End If
                    If btnPrint.Enabled = True Then
                        btnPrint.Enabled = (Status.Text <> "Rejected")
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            strSQL = "Select b.npwp FROM tbl_po AS a INNER JOIN tbm_company AS b ON a.company_code = b.company_code WHERE po_no = '" & PO & "'"
            errMSg = "Failed when read SHIN data"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        txtNPWP.Text = MyReader.GetString("npwp")
                    Catch ex As Exception
                        txtNPWP.Text = ""
                    End Try
                End While
            End If
            CloseMyReader(MyReader, UserData)


            btnSave.Text = "Update"
            btnSave.Enabled = f_getenable(Status.Text)
            btnReject.Enabled = btnSave.Enabled
            tgl.Enabled = f_getenable(Status.Text)
            txtNPWP.ReadOnly = True
            remark.ReadOnly = f_getread(Status.Text)
            footer.ReadOnly = f_getread(Status.Text)
            Note.ReadOnly = f_getread(Status.Text)
            docaddress.ReadOnly = f_getread(Status.Text)
            btnSearchD.Visible = f_getenable(Status.Text)
            'btnSearchL.Visible = f_getenable(Status.Text)
            Button3.Visible = f_getenable(Status.Text)


            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & crt.Text & "'")
            Me.Text = "Shipping Instruction # " & Trim(SHINNum) & " - Update"
        End If
        RefreshDoc()
        RefreshLine()
    End Sub
    Private Sub RefreshDoc()
        Dim cnt, ulang As Integer

        cnt = doc_no.Lines.Length - 1
        ulang = 0

        While ulang <= cnt
            Call doc_no_TextChanged(doc_no.Lines(ulang).ToString)
            ulang = ulang + 1
        End While
    End Sub
    Private Sub RefreshLine()
        Dim cnt, ulang As Integer

        cnt = line_code.Lines.Length - 1
        ulang = 0

        While ulang <= cnt
            Call line_code_TextChanged(line_code.Lines(ulang).ToString)
            ulang = ulang + 1
        End While
    End Sub

    Private Sub FrmSHIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 330)

        Me.Location = value
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True
    End Function
    Private Sub btnSearchD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchD.Click
        Dim strSQL, strSQLF As String
        Dim PilihanDlg As New DlgPilihan
        Dim temp As Integer

        strSQL = "select a.doc_code as DocumentCode, a.doc_name as DocumentName from tbm_document as a " & _
                 "inner join tbm_attachment_doc as b on a.doc_code=b.doc_code " & _
                 "where b.doc_code LIKE 'SI%'"
        strSQLF = strSQL & " and a.doc_code like 'FilterData1%' and a.doc_name like 'FilterData2%' group by a.doc_code"
        strSQL = strSQL & " group by a.doc_code"

        PilihanDlg.Text = "Select Document Code"
        PilihanDlg.LblKey1.Text = "Document Code"
        PilihanDlg.LblKey2.Text = "Document Name"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        PilihanDlg.Tables = "tbm_attachment_doc"

        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If doc_no.Lines.Length >= 1 Then doc_no.Text = doc_no.Text & Chr(13) & Chr(10)
            doc_no.Text = doc_no.Text & PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            temp = doc_no.Lines.Length - 1
            doc_no_TextChanged(doc_no.Lines(temp).ToString)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'SI-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'SI-A'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"


        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            CTApp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            approvedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
        End If
        AppDt.Checked = True
    End Sub

    Private Sub btnSearchL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchL.Click
        Dim strSQL, strSQLF As String
        Dim PilihanDlg As New DlgPilihan
        Dim temp As Integer

        strSQL = "select line_code as LineCode, line_name as LineName, city_code as CityCode from tbm_lines"
        strSQLF = strSQL & " where line_code LIKE 'FilterData1%' and line_name LIKE 'FilterData2%'"
        PilihanDlg.Text = "Select Line Code"
        PilihanDlg.LblKey1.Text = "Line Code"
        PilihanDlg.LblKey2.Text = "Line Name"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If line_code.Lines.Length >= 1 Then line_code.Text = line_code.Text & Chr(13) & Chr(10)
            line_code.Text = line_code.Text & PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            temp = line_code.Lines.Length - 1
            line_code_TextChanged(line_code.Lines(temp).ToString)
        End If
    End Sub
    Private Function GetName2(ByVal code As String) As String
        Dim strSQL, errMsg As String

        strSQL = "Select * from tbm_users " & _
                 "where user_ct = '" & code & "'"

        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    GetName2 = MyReader2.GetString("name")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Function

    Private Sub doc_no_TextChanged(ByVal doccode As String)
        Dim strSQL, errMsg As String
        Dim nomor As Integer

        strSQL = "select doc_name from tbm_document where doc_code='" & doccode & "'"
        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    nomor = doc_text.Lines.Length + 1
                    If doc_text.Text <> "" Then doc_text.Text = doc_text.Text & Chr(13) & Chr(10)
                    doc_text.Text = doc_text.Text & nomor.ToString & ". " & MyReader2.GetString("doc_name")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Sub
    Private Sub line_code_TextChanged(ByVal linecode As String)
        Dim strSQL, errMsg As String

        strSQL = "select * from tbm_lines where line_code='" & linecode & "'"

        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    If line_text.Lines.Length >= 1 Then line_text.Text = line_text.Text & Chr(13) & Chr(10)
                    line_text.Text = line_text.Text & MyReader2.GetString("line_name")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Sub

    Private Sub btnClearD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearD.Click
        doc_no.Text = ""
        doc_text.Text = ""
    End Sub

    Private Sub btnClearL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearL.Click
        line_code.Text = ""
        line_text.Text = ""
    End Sub
    Function GetServerDate() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function

    Private Sub btnDelFooter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelFooter.Click
        footer.Text = ""
    End Sub

    Private Sub btnFooter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFooter.Click
        Dim strSQL, strSQLF As String
        Dim PilihanDlg As New DlgPilihan
        Dim temp As Integer

        strSQL = "select distinct(footer_note) as Footer from tbl_si " & _
                 "where createdby=" & UserData.UserCT & " and trim(footer_note) <> '' order by createddt desc,po_no,ord_no "
        strSQLF = "select distinct(footer_note) as footer from tbl_si " & _
                  "where createdby=" & UserData.UserCT & " and trim(footer_note) <> '' " & " and footer_note like 'FilterData1%'" & _
                  "order by createddt desc,po_no,ord_no "

        PilihanDlg.Text = "Select Footer Note History"
        PilihanDlg.LblKey1.Text = "Footer"
        PilihanDlg.LblKey2.Visible = False
        PilihanDlg.TxtKey2.Visible = False

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        PilihanDlg.Tables = "tbl_si"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then footer.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub btnAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddress.Click
        Dim strSQL, strSQLF As String
        Dim PilihanDlgAddress As New DlgPilihanAddress("SI")

        strSQL = "select Item_no as No from tbm_item_history where item_code LIKE 'SI%' order by item_no"
        strSQLF = "select Item_no as No from tbm_item_history where item_code LIKE 'SI%' and item_no like 'FilterData1%' order by item_no"

        PilihanDlgAddress.Text = "Select Document Address History"
        PilihanDlgAddress.LblKey1.Text = "No."
        PilihanDlgAddress.LblKey2.Visible = False
        PilihanDlgAddress.TxtKey2.Visible = False

        PilihanDlgAddress.SQLGrid = strSQL
        PilihanDlgAddress.SQLFilter = strSQLF
        PilihanDlgAddress.Tables = "tbm_item_history"

        If PilihanDlgAddress.ShowDialog() = Windows.Forms.DialogResult.OK Then docaddress.Text = PilihanDlgAddress.address.Text
    End Sub

    Private Sub btnDelAddress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelAddress.Click
        docaddress.Text = ""
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
        Dispose()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim OpeningDt, PrepDt1, SQLStr As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(approvedby, AppDt, "Approved Date") Then Exit Sub

        Try
            OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            PrepDt1 = Format(AppDt.Value, "yyyy-MM-dd")

            If btnSave.Text = "Save" Then
                SQLStr = "Run Stored Procedure SaveSHIN (Save," & ShipNo & "," & PO & "," & OpeningDt & "," & doc_text.Text & "," & remark.Text & "," _
                         & docaddress.Text & "," & CTApp.Text & "," & PrepDt1 & "," & footer.Text & "," & Note.Text & "," & line_text.Text & "," & line_text.Text & ")"
                
                keyprocess = "Save"
            Else
                SQLStr = "Run Stored Procedure SaveSHIN (Updt," & ShipNo & "," & PO & "," & OpeningDt & "," & doc_text.Text & "," & remark.Text & "," _
                             & docaddress.Text & "," & CTApp.Text & "," & PrepDt1 & "," & footer.Text & "," & Note.Text & "," & line_text.Text & ")"
                keyprocess = "Updt"
            End If
            MyComm.CommandText = "SaveSHIN"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", ShipNo)
            MyComm.Parameters.AddWithValue("PO", PO)
            MyComm.Parameters.AddWithValue("OpeningDt", OpeningDt)
            MyComm.Parameters.AddWithValue("DocNo", doc_no.Text)
            MyComm.Parameters.AddWithValue("DocName", doc_text.Text)
            MyComm.Parameters.AddWithValue("Remark", remark.Text)
            MyComm.Parameters.AddWithValue("DocAddress", docaddress.Text)

            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("PrepBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("PrepDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
            Else
                MyComm.Parameters.AddWithValue("PrepBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("PrepDt", PrepDt1)
                If (FinalApp Or Status.Text = "Final Approved") Then
                    MyComm.Parameters.AddWithValue("Stat", "Final Approved")
                Else
                    MyComm.Parameters.AddWithValue("Stat", "Approved")
                End If
            End If
            MyComm.Parameters.AddWithValue("Footer", footer.Text)
            MyComm.Parameters.AddWithValue("Note", Note.Text)
            MyComm.Parameters.AddWithValue("LineCode", line_code.Text)
            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", num)
            End If
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " SI")
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " SI failed'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer
        Dim xStatus As String = ""

        msg = "Reject Shipment Instruction #" & SHINNum & " of " & PO & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            If ShipNo = "" Then
                SQLstr = "SELECT shipment_no,status FROM tbl_si " & _
                         " where shipment_no is null and PO_No='" & PO & "' " & _
                         " and ord_no=" & SHINNum & ""
                Errmsg = "Failed when read user data"
                MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            xStatus = MyReader.GetString("Status")
                        Catch ex As Exception
                            xStatus = ""
                        End Try
                    End While
                    CloseMyReader(MyReader, UserData)
                End If
                If xStatus = "Open" Then
                    SQLstr = "UPDATE tbl_si " & _
                             "SET status='Rejected'" & _
                             " where shipment_no is null and PO_No='" & PO & "' " & _
                             " and ord_no=" & SHINNum & ""
                ElseIf xStatus = "Final Approved" Then
                    SQLstr = "UPDATE tbl_si " & _
                             "SET status='Approved'" & _
                             " where shipment_no is null and PO_No='" & PO & "' " & _
                             " and ord_no=" & SHINNum & ""
                End If
                
            Else
                SQLstr = "SELECT shipment_no,status FROM tbl_si " & _
                         "where shipment_No=" & ShipNo & " and ord_no=" & SHINNum & ""
                Errmsg = "Failed when read user data"
                MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            xStatus = MyReader.GetString("Status")
                        Catch ex As Exception
                            xStatus = ""
                        End Try
                    End While
                    CloseMyReader(MyReader, UserData)
                End If

                If xStatus = "Open" Then
                    SQLstr = "UPDATE tbl_si " & _
                             "SET status='Rejected' " & _
                             " where shipment_No=" & ShipNo & " and ord_no=" & SHINNum & ""
                ElseIf xStatus = "Final Approved" Then
                    SQLstr = "UPDATE tbl_si " & _
                             "SET status='Approved' " & _
                             "where shipment_No=" & ShipNo & " and ord_no=" & SHINNum & ""
                End If
            End If

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "Shipment Instruction #" & SHINNum & " of " & PO & " has been rejected"
                MsgBox(msg)
            End If

        End If
        btnClose_Click(sender, e)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        If cbPrintGroup.Items.IndexOf(cbPrintGroup.Text) < 0 Then
            MsgBox("Invalid Print Doc. Group")
            cbPrintGroup.Focus()
        End If
        If Len(CStr(SHINNum)) = 1 Then
            v_num = " " & SHINNum
        Else
            v_num = SHINNum
        End If
        ViewerFrm.Tag = "SHIN" & v_num & cbPrintGroup.Items.IndexOf(cbPrintGroup.Text) & ShipNo & ";" & PO
        ViewerFrm.ShowDialog()
    End Sub

    Private Sub btnD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnD.Click
        Dim strSQL, strSQLF As String
        Dim PilihanDlgAddress As New DlgPilihanAddress("DR")

        strSQL = "select Item_no as No from tbm_item_history where item_code='DR' order by item_no"
        strSQLF = "select Item_no as No from tbm_item_history where item_code='DR' and item_no like 'FilterData1%' order by item_no"

        PilihanDlgAddress.Text = "Select Document Req. History"
        PilihanDlgAddress.LblKey1.Text = "No."
        PilihanDlgAddress.LblKey2.Visible = False
        PilihanDlgAddress.TxtKey2.Visible = False

        PilihanDlgAddress.SQLGrid = strSQL
        PilihanDlgAddress.SQLFilter = strSQLF
        PilihanDlgAddress.Tables = "tbm_item_history"

        If PilihanDlgAddress.ShowDialog() = Windows.Forms.DialogResult.OK Then
            doc_text.Text = PilihanDlgAddress.address.Text
            doc_no.Text = PilihanDlgAddress.temp.Text
        End If
    End Sub
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub btnNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNote.Click
        Dim strSQL, strSQLF As String
        Dim PilihanDlg As New DlgPilihan
        Dim temp As Integer

        strSQL = "select distinct(note) as Note from tbl_si " & _
                 "where createdby=" & UserData.UserCT & " and trim(note) <> '' order by createddt desc,po_no,ord_no "
        strSQLF = "select distinct(note) as Note from tbl_si " & _
                  "where createdby=" & UserData.UserCT & " and trim(note) <> '' " & " and note like 'FilterData1%'" & _
                  "order by createddt desc,po_no,ord_no "

        PilihanDlg.Text = "Select Note History"
        PilihanDlg.LblKey1.Text = "Note"
        PilihanDlg.LblKey2.Visible = False
        PilihanDlg.TxtKey2.Visible = False

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        PilihanDlg.Tables = "tbl_si"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then Note.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString

    End Sub

    Private Sub btnDelNote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelNote.Click
        Note.Text = ""
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        FinalApp = True
        Call btnSave_Click(sender, e)
    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label9.Click

    End Sub

    Private Sub ToolStrip2_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip2.ItemClicked

    End Sub
End Class