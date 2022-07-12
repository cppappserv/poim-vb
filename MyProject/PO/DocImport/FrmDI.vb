'Title                         : Transaksi Document Import - DI
'Form                          : FrmDI
'Created By                    : Hanny
'Created Date                  : 24 NOV 2008
'Table Used                    : tbm_bank, tbm_bank_Reference, 
'                                tbl_docimpr, tbl_SHIPPING
'Stored Procedure Used (MySQL) : SaveDI
Public Class FrmDI
    Dim Ship, DINum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
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
    Sub New(ByVal ShipNo As String, ByVal Currency As String, ByVal CurrName As String, ByVal TotalAmt As String, ByVal kurs As String, ByVal DI As String)
        Dim ttl As Decimal
        Dim lcno As String

        InitializeComponent()
        'DIno.Text = ""
        txtCity_Code.Text = ""
        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        CTApp.Text = ""
        appdt.Value = GetServerDate()
        AppDt.Checked = False

        Ship = ShipNo
        'DIno.Text = ""

        'Call GetButtonAccess()

        If Trim(DI) <> "" Then
            Call DisplayData(DI)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("DI-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        Else
            crtdt.Text = GetServerDate.ToString
            crt.Text = UserData.UserName
            CTCrt.Text = UserData.UserCT.ToString
            btnReject.Enabled = False
            btnPrint.Enabled = False
            DINum = 0
            Me.Text = "Debit Instruction - New"
        End If
    End Sub

    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT city_code as CityCode, city_name as CityName, country_code as CountryCode FROM tbm_city Where country_code='ID'"
        PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName, country_code as CountryCode FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' and city_name LIKE 'FilterData2%' and country_code='ID'"
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCity_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCityName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub
    Private Sub DisplayData(ByVal DI As String)
        Dim pjg, ttl As Integer
        Dim strSQL, errMSg, temp, temp2 As String

        pjg = Len(RTrim(DI)) - 4
        num = Mid(DI, 5, pjg)
        DINum = num

        strSQL = "SELECT * FROM TBL_SHIPPING_DOC " & _
                 "WHERE SHIPMENT_NO = '" & Ship & "' AND " & _
                 "ORD_NO = '" & DINum & "' AND FINDOC_TYPE = 'DI'"
        'strSQL = " select a.*,b.* from tbl_budget as a" & _
        '         " inner join tbm_bank as b on a.bank_code=b.bank_code " & _
        '         " where a.po_no = '" & PO & "' and a.ord_no='" & num & "' " & _
        '         " and a.type_code = 'BP' "


        errMSg = "Failed when read DI data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    'temp = MyReader.GetString("bank_code")
                    'tgl.Text = MyReader.GetString("OpeningDt")
                    'remark.Text = MyReader.GetString("Remark")
                    'CTApp.Text = MyReader.GetString("ApprovedBy")
                    'CTFun.Text = MyReader.GetString("FinanceAppBy")
                    'Status.Text = MyReader.GetString("status")
                    'BLno.Text = MyReader.GetString("BL_NO")
                    'cbLC.Text = MyReader.GetString("LC_NO")
                    temp = MyReader.GetString("FINDOC_PRINTEDON")
                    DTPrinted.Text = MyReader.GetString("FINDOC_PRINTEDDT")
                    crtdt.Text = MyReader.GetString("FINDOC_CREATEDDT")
                    CTCrt.Text = MyReader.GetString("FINDOC_CREATEDBY")
                    'CTApp.Text = MyReader.GetString("FINDOC_APPBY")
                    'AppDt.Value = MyReader.GetString("FINDOC_APPDT")
                    Try
                        CTApp.Text = MyReader.GetString("FINDOC_APPBY")
                    Catch ex As Exception
                        CTApp.Text = ""
                        AppDt.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        AppDt.Value = MyReader.GetString("FINDOC_APPDT")
                        AppDt.Checked = True
                    End If

                    Status.Text = MyReader.GetString("FINDOC_STATUS")
                    If btnReject.Enabled = True Then
                        btnReject.Enabled = f_getenable(Status.Text)
                    End If
                    If btnPrint.Enabled = True Then
                        btnPrint.Enabled = f_getenable(Status.Text)
                    End If
                    DTPrinted.Enabled = f_getenable(Status.Text)
                    txtCity_Code.ReadOnly = f_getread(Status.Text)
                    'approvedby.ReadOnly = (Status.Text = "Approved")
                    'crt.ReadOnly = (Status.Text = "Approved")
                    'AppDt.Enabled = (Status.Text = "Approved")
                    crtdt.ReadOnly = f_getread(Status.Text)
                    btnSearchCity.Visible = f_getenable(Status.Text)
                    btnSearchCity.Visible = f_getenable(Status.Text)
                    Button3.Visible = f_getenable(Status.Text)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            btnSave.Text = "Update"
            btnSave.Enabled = f_getenable(Status.Text)
            btnReject.Enabled = btnSave.Enabled
            btnPrint.Enabled = btnSave.Enabled
            'posisi di akhir supaya tidak dead lock
            txtCity_Code.Text = temp
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            Me.Text = "Debit Instruction # " & Trim(DINum) & " - Update"
        End If
    End Sub
    Private Function GetName2(ByVal code As String) As String
        Dim strSQL, errMsg As String

        strSQL = "Select Name from tbm_users " & _
                 "where user_Ct=" & code & ""

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
    Private Sub FrmDI_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 380)

        Me.Location = value
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
        Dispose()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GetName("App")
        AppDt.Checked = True
    End Sub
    Private Sub GetName(ByVal sender As String)
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'DI-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'DI-A'" & _
                               "and tu.name LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If sender Is "App" Then
                approvedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
                'ElseIf sender Is "Fun" Then
                '    financeappby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
            Call Name_Data(sender)
        End If
    End Sub
    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String

        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'DI-A' and tu.name='" & approvedby.Text & "'"
            'Else
            '    strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
            '             "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-A' and tu.name='" & financeappby.Text & "'"
        End If

        errMSg = "Failed when read user authorization data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If sender = "App" Then
                        CTApp.Text = MyReader.GetString("user_ct")
                        approvedby.Text = MyReader.GetString("name")
                        'Else
                        '    CTFun.Text = MyReader.GetString("user_ct")
                        '    financeappby.Text = MyReader.GetString("name")
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Function CekData() As Boolean
        Dim STRsql As String

        CekData = True

        'Foreign Key, CITY DAN APPROVED BY
        STRsql = " select * from tbm_CITY where city_code='" & txtCity_Code.Text & "'"

        If FM02_MaterialGroup.DataOK(STRsql) = True Then
            MsgBox("City code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtCity_Code.Focus()
            Exit Function
        End If

        'Foreign Key
        If approvedby.Text <> "" Then
            STRsql = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'DI-A' and tu.name='" & approvedby.Text & "'"

            If FM02_MaterialGroup.DataOK(STRsql) = True Then
                MsgBox("Name does not exist! ", MsgBoxStyle.Critical, "Warning")
                CekData = False
                approvedby.Focus()
                Exit Function
            End If
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        'Dim transaction As MySqlTransaction
        Dim AppDt1, dtprinted1, SQLStr, str1, str2, str3, str4 As String
        Dim num1, num2, num3, num4 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""


        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDt, "Approved Date") Then Exit Sub

        Try
            AppDt1 = Format(AppDt.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")

            If btnSave.Text = "Save" Then
                SQLStr = "Run Stored Procedure SaveDI (Save," & Ship & "," & txtCity_Code.Text & "," & dtprinted1 & "," & CTApp.Text & "," & AppDt1 & "," & crt.Text & "," & crtdt.Text & ")"
                keyprocess = "Save"
            ElseIf btnSave.Text = "Update" Then
                SQLStr = "Run Stored Procedure SaveDI (Updt," & Ship & "," & txtCity_Code.Text & "," & dtprinted1 & "," & CTApp.Text & "," & AppDt1 & "," & crt.Text & "," & crtdt.Text & ")"
                keyprocess = "Updt"
            End If


            MyComm.CommandText = "SaveDI"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("CityCode", txtCity_Code.Text)
            MyComm.Parameters.AddWithValue("DTPrint", dtprinted1)
            'MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
            'MyComm.Parameters.AddWithValue("AppDt", AppDt1)
            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDt1)
                MyComm.Parameters.AddWithValue("Stat", "Approved")
            End If

            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", DINum)
            End If
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " DI")
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " DI failed'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            'transaction.Rollback()
        End Try
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject Debit Instruction #" & DINum & " of " & Ship & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_shipping_doc " & _
                     "SET FINDOC_STATUS='Rejected'" & _
                     " where SHIPMENT_NO='" & Ship & "' " & _
                     " and ord_no=" & DINum & "" & _
                     " AND FINDOC_TYPE = 'DI'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "Debit Instruction #" & DINum & " of " & Ship & " has been rejected"
                MsgBox(msg)
            End If

        End If
        btnClose_Click(sender, e)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        'v_pono = txtpono.ToString
        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num
        End If
        ViewerFrm.Tag = "DIII" & v_num & Ship
        ViewerFrm.ShowDialog()
    End Sub

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

End Class