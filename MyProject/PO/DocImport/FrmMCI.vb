''' <summary>
''' Title                         : Transaksi Bill of Lading - MCI
''' Form                          : FrmMCI
''' Table Used                    : tbl_mci, tbl_item_history, tbl_audittrail
''' Stored Procedure Used (MySQL) : SaveMCI
''' Created By                    : Yanti, 30.09.2009
''' 
''' </summary>
''' <remarks></remarks>



Public Class FrmMCI
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim MCNum, strSQL, errMSg, ShipNo, BLNum As String
    Sub New(ByVal Ship As String, ByVal BL As String, ByVal MC As String)
        Dim tg As Date

        InitializeComponent()
        ShipNo = Ship
        MCNum = Trim(MC)
        BLNum = Trim(BL)
        tg = GetServerDate()
        tgl.Value = tg
        appdate.Value = tg
        dtIssued.Value = tg

        If Trim(MCNum) <> "" Then
            Call DisplayData()
            btnSave.Enabled = (btnSave.Enabled) And (crtby.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (crtby.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("MC-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
         Else
            btnReject.Enabled = False
            btnPrint.Enabled = False
            crtby.Text = UserData.UserCT
            crtdate.Text = tg.ToString
            Me.Text = "MCI - New"
        End If
    End Sub
    Private Sub DisplayData()
        Dim temp1, temp2, cekCont As String
        strSQL = "select t1.*, TRIM(t2.total_container) cekContainer from tbl_mci t1, tbl_shipping t2 " & _
                 "where t1.shipment_no=t2.shipment_no and t1.shipment_no=" & ShipNo & " and t1.ord_no=" & MCNum
        errMSg = "Failed when read MCI data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    tgl.Text = MyReader.GetString("OpeningDt")
                    req.Text = GetData("Survey_Req")
                    footer.Text = GetData("Footer_Note")
                    temp1 = GetData("CreatedBy")
                    temp2 = GetData("ApprovedBy")
                    Status.Text = GetData("Status")
                    docaddress.Text = GetData("Doc_Address")
                    reffNo.Text = GetData("Reference_No")
                    GetTgl("IssuedDt", dtIssued)
                    crtdate.Text = MyReader.GetString("CreatedDt")
                    GetTgl("ApprovedDt", appdate)
                    cekCont = MyReader.GetString("cekContainer")

                    If btnReject.Enabled = True Then
                        btnReject.Enabled = (Status.Text = "Approved")
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            crtby.Text = temp1
            appcode.Text = temp2
            btnSave.Text = "Update"
            btnSave.Enabled = f_getenable(Status.Text)
            btnReject.Enabled = btnSave.Enabled
            btnPrint.Enabled = btnSave.Enabled
            Me.Text = "MCI # " & Trim(MCNum) & " - Update"

            If cekCont <> "" Then cbDG.Text = "MCI Container"
        End If
    End Sub
    Private Sub GetTgl(ByVal fld As String, ByVal obj As DateTimePicker)
        Dim temp As Object
        Try
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then obj.Value = temp
        Catch ex As Exception
        End Try
        obj.Checked = Not (temp Is Nothing)
    End Sub
    Private Function GetData(ByVal fld As String) As String
        Dim temp As Object

        GetData = ""
        Try
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then GetData = temp
        Catch ex As Exception
        End Try
    End Function
    Private Sub FrmMCI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dts As DataTable
        Dim loc = New System.Drawing.Point(253, 250)
        Dim DT As New System.Data.DataTable

        Me.Location = loc
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Function CekData() As Boolean
        CekData = True
        If appcode.Text <> "" And appdate.Checked = False Then
            MsgBox("Approved Date should be filled! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            appdate.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        Dim OpeningDt, AppDt, dtIssued1, SQLStr, MaterialCode, Qty, PONo, POItem, str, RetMess As String
        Dim hasil As Boolean
        Dim cnt As Integer
        Dim num As Decimal

        If CekData() = False Then Exit Sub
        OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
        AppDt = IIf(appdate.Checked, Format(appdate.Value, "yyyy-MM-dd"), "")
        dtIssued1 = IIf(dtIssued.Checked, Format(dtIssued.Value, "yyyy-MM-dd"), "")

        SQLStr = "Run Stored Procedure SaveMCI (" & If(btnSave.Text = "Save", "Save", "Updt") & "," & ShipNo & "," & OpeningDt & "," & req.Text & "," _
                 & footer.Text & "," & docaddress.Text & "," & reffNo.Text & "," & dtIssued1 & "," _
                 & UserData.UserCT & "," & appcode.Text & "," & AppDt & "," & If(appcode.Text = "", "Open", "Approved") & ")"

        With MyComm
            .CommandText = "SaveMCI"
            .CommandType = CommandType.StoredProcedure
            With .Parameters
                .Clear()
                .AddWithValue("keyprocess", If(btnSave.Text = "Save", "Save", "Updt"))
                .AddWithValue("MCNum", If(btnSave.Text = "Save", "0", MCNum))
                .AddWithValue("ShipNo", ShipNo)
                .AddWithValue("OpenDt", OpeningDt)
                .AddWithValue("SurveyReq", req.Text)
                .AddWithValue("Footer", footer.Text)
                .AddWithValue("DocAddress", docaddress.Text)
                .AddWithValue("ReffNo", reffNo.Text)
                .AddWithValue("IssuedDt", dtIssued1)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("AppBy", appcode.Text)
                .AddWithValue("AppDt", If(appcode.Text = "", "", AppDt))
                .AddWithValue("AuditStr", SQLStr)
                .AddWithValue("Stat", If(appcode.Text = "", "Open", "Approved"))
                .AddWithValue("Hasil", hasil)
            End With            
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End With

        If hasil = True Then
            f_msgbox_successful(btnSave.Text & " MCI")
            Close()
            Dispose()
        Else
            MsgBox(btnSave.Text & " MCI failed")
        End If
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject MCI #" & MCNum & " of " & BLNum & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_mci SET status='Rejected' " & _
                     "where Shipment_No=" & ShipNo & " and shipment_no=" & ShipNo & " and ord_no=" & MCNum

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "MCI #" & MCNum & " of " & BLNum & " has been rejected"
                MsgBox(msg)
            End If
        End If
        Close()
        Dispose()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'MC-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'MC-A'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then appcode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        appdate.Checked = True
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        If cbDG.Text = "" Then
            MsgBox("Print Doc. Group harus diisi")
            cbDG.Focus()
            Exit Sub
        End If
        v_num = Mid(MCNum & Space(40), 1, 40)
        ViewerFrm.Tag = If(cbDG.Text = "MCI Charter", "MCI1", "MCI2") & v_num & ShipNo
        ViewerFrm.ShowDialog()
    End Sub

    Private Sub btnClearD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearD.Click
        req.Text = ""
    End Sub

    Function GetServerDate() As Date
        Dim temp As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function

    Private Sub appcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles appcode.TextChanged
        appname.Text = AmbilData("name", "tbm_users", "user_ct=" & appcode.Text)
    End Sub

    Private Sub crtby_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crtby.TextChanged
        crtname.Text = AmbilData("name", "tbm_users", "user_ct=" & crtby.Text)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim strSQLF As String
        Dim PilihanDlgAddress As New DlgPilihanAddress("SR")

        strSQL = "select Item_no as No from tbm_item_history where item_code LIKE 'SR%' order by item_no"
        strSQLF = "select Item_no as No from tbm_item_history where item_code LIKE 'SR%' and item_no like 'FilterData1%' order by item_no"

        PilihanDlgAddress.Text = "Select Survey Req. History"
        PilihanDlgAddress.LblKey1.Text = "No."
        PilihanDlgAddress.LblKey2.Visible = False
        PilihanDlgAddress.TxtKey2.Visible = False

        PilihanDlgAddress.SQLGrid = strSQL
        PilihanDlgAddress.SQLFilter = strSQLF
        PilihanDlgAddress.Tables = "tbm_item_history"

        If PilihanDlgAddress.ShowDialog() = Windows.Forms.DialogResult.OK Then req.Text = PilihanDlgAddress.address.Text
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim strSQLF As String
        Dim PilihanDlgAddress As New DlgPilihanAddress("")

        strSQL = "select " & _
                 "(select count(*) from tbl_mci where tbl_mci.shipment_no<=t1.shipment_no and tbl_mci.ord_no<=t1.ord_no and tbl_mci.createdby=" & UserData.UserCT & _
                 "  order by tbl_mci.shipment_no) AS Footer,t1.footer_note " & _
                 "from tbl_mci t1 "
        strSQLF = "select * from (" & strSQL & ") as  a where a.footer like 'FilterData1%'"

        PilihanDlgAddress.Text = "Select Footer Note History"
        PilihanDlgAddress.LblKey1.Text = "Footer"
        PilihanDlgAddress.LblKey2.Visible = False
        PilihanDlgAddress.TxtKey2.Visible = False

        PilihanDlgAddress.SQLGrid = strSQL
        PilihanDlgAddress.SQLFilter = strSQLF
        PilihanDlgAddress.Tables = "tbl_mci"

        If PilihanDlgAddress.ShowDialog() = Windows.Forms.DialogResult.OK Then footer.Text = PilihanDlgAddress.address.Text
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        footer.Text = ""
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strSQLF As String
        Dim PilihanDlgAddress As New DlgPilihanAddress("MC")

        strSQL = "select Item_no as No from tbm_item_history where item_code LIKE 'MC%' order by item_no"
        strSQLF = "select Item_no as No from tbm_item_history where item_code LIKE 'MC%' and item_no like 'FilterData1%' order by item_no"

        PilihanDlgAddress.Text = "Select Document Address History"
        PilihanDlgAddress.LblKey1.Text = "No."
        PilihanDlgAddress.LblKey2.Visible = False
        PilihanDlgAddress.TxtKey2.Visible = False

        PilihanDlgAddress.SQLGrid = strSQL
        PilihanDlgAddress.SQLFilter = strSQLF
        PilihanDlgAddress.Tables = "tbm_item_history"

        If PilihanDlgAddress.ShowDialog() = Windows.Forms.DialogResult.OK Then docaddress.Text = PilihanDlgAddress.address.Text
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        docaddress.Text = ""
    End Sub
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
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function
End Class