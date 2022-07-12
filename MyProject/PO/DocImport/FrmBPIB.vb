'Title                         : Transaksi BPIB
'Form                          : FrmBPIB
'Table Used                    : tbm_bank, tbm_bank_Reference, tbl_budgets
'Stored Procedure Used (MySQL) : SaveBPIB
'Created By               : Hanny, 11 Feb 09

Public Class FrmBPIB
    Dim Ship, NONum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String = "BP"
    Dim MODULE_NAME As String = "B-PIB "

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
    Sub New(ByVal ShipNo As String, ByVal Currency As String, ByVal CurrName As String, ByVal TotalAmt As String, ByVal kurs As String, ByVal NO As String)
        Dim v_tgl As String

        InitializeComponent()
        txtCity_Code.Text = ""
        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        CTApp.Text = ""
        CTFin.Text = ""
        AppDT.Value = GetServerDate()
        FinDT.Value = GetServerDate()
        crtdt.Text = GetServerDate.ToString
        Ship = ShipNo

        v_tgl = AmbilData("tt_dt", "tbl_shipping", "SHIPMENT_NO='" & Ship & "'")
        If v_tgl = "" Then
            tgl.Value = GetServerDate()
        Else
            tgl.Value = CDate(v_tgl)
        End If

        blno.Text = AmbilData("BL_NO", "TBL_SHIPPING", "SHIPMENT_NO = '" & Ship & "'")

        If Trim(NO) <> "" Then
            Call DisplayData(NO)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("BP-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        Else
            crt.Text = UserData.UserName
            CTCrt.Text = UserData.UserCT.ToString
            btnReject.Enabled = False
            btnPrint.Enabled = False
            NONum = 0
            Me.Text = MODULE_NAME & "- New"
            txtCity_Code.Text = "JKT"
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
        End If
    End Sub
    Private Sub DisplayData(ByVal NO As String)
        Dim pjg, ttl As Integer
        Dim strSQL, errMSg, temp_BANK, temp_CITY As String

        pjg = Len(RTrim(NO)) - 4
        num = Mid(NO, 5, pjg)
        NONum = num
        strSQL = "SELECT * FROM TBL_BUDGETS " & _
                 "WHERE SHIPMENT_NO = '" & Ship & "' AND " & _
                 "ORD_NO = '" & NONum & "' AND TYPE_CODE = '" & MODULE_CODE & "'"

        DTPrinted.Enabled = False
        tgl.Enabled = False
        AppDT.Enabled = False
        FinDT.Enabled = False
        txtCity_Code.ReadOnly = True

        'approvedby.ReadOnly = True
        'financeappby.ReadOnly = True
        crt.ReadOnly = True
        'AppDT.Enabled = True
        'FinDT.Enabled = True
        crtdt.ReadOnly = True
        'btnSave.Enabled = False
        btnSearchCity.Visible = False
        btnSearch.Visible = False
        Button3.Visible = False

        errMSg = "Failed when read " & MODULE_CODE & " data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try

                    tgl.Text = MyReader.GetString("OpeningDt")
                    temp_BANK = MyReader.GetString("bank_code")
                    'TotalAmount.Text = FormatNumber(MyReader.GetString("AMOUNT"), 2)
                    remark.Text = MyReader.GetString("Remark")
                    CTCrt.Text = MyReader.GetString("CREATEDBY")
                    'CTApp.Text = MyReader.GetString("ApprovedBy")
                    'CTFin.Text = MyReader.GetString("FinanceAppBy")
                    crtdt.Text = MyReader.GetString("CREATEDDT")
                    'AppDT.Value = MyReader.GetString("APPROVEDDT")
                    'FinDT.Value = MyReader.GetString("FINANCEAPPDT")

                    Try
                        CTApp.Text = MyReader.GetString("ApprovedBy")
                    Catch ex As Exception
                        CTApp.Text = ""
                        AppDT.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        AppDT.Value = MyReader.GetString("APPROVEDDT")
                        AppDT.Checked = True
                    End If
                    Try
                        CTFin.Text = MyReader.GetString("FinanceAppBy")
                    Catch ex As Exception
                        CTFin.Text = ""
                        FinDT.Checked = False
                    End Try
                    If CTFin.Text <> "" Then
                        FinDT.Value = MyReader.GetString("FINANCEAPPDT")
                        FinDT.Checked = True
                    End If

                    DTPrinted.Value = MyReader.GetString("PRINTEDDT")
                    temp_CITY = MyReader.GetString("PRINTEDON")
                    Status.Text = MyReader.GetString("status")

                    'temp = MyReader.GetString("FINDOC_PRINTEDON")
                    'DTPrinted.Text = MyReader.GetString("FINDOC_PRINTEDDT")
                    'CTCrt.Text = MyReader.GetString("FINDOC_CREATEDBY")
                    'CTApp.Text = MyReader.GetString("FINDOC_APPBY")
                    'CTFin.Text = MyReader.GetString("FINDOC_FINAPPBY")
                    'Status.Text = MyReader.GetString("FINDOC_STATUS")
                    '
                    'AppDT.Value = MyReader.GetString("FINDOC_APPDT")
                    'FinDT.Value = MyReader.GetString("FINDOC_FINAPPDT")
                    If btnReject.Enabled = True Then
                        btnReject.Enabled = f_getenable(Status.Text)
                    End If
                    If btnPrint.Enabled = True Then
                        btnPrint.Enabled = f_getenable(Status.Text)
                    End If
                    DTPrinted.Enabled = f_getenable(Status.Text)
                    tgl.Enabled = f_getenable(Status.Text)
                    AppDT.Enabled = f_getenable(Status.Text)
                    FinDT.Enabled = f_getenable(Status.Text)

                    txtCity_Code.ReadOnly = f_getread(Status.Text)
                    bank.ReadOnly = (Status.Text <> "Approved")
                    'approvedby.ReadOnly = f_getread(Status.Text)
                    'tgl.Enabled = f_getenable(Status.Text)
                    'crtdt.Enabled = f_getenable(Status.Text)
                    'crt.ReadOnly = f_getread(Status.Text)
                    'AppDT.Enabled = f_getenable(Status.Text)
                    'FinDT.Enabled = f_getenable(Status.Text)
                    'crtdt.ReadOnly = f_getread(Status.Text)
                    remark.ReadOnly = f_getread(Status.Text)
                    blno.ReadOnly = f_getread(Status.Text)
                    btnSearchCity.Visible = f_getenable(Status.Text)
                    btnSearch.Visible = f_getenable(Status.Text)
                    Button3.Visible = f_getenable(Status.Text)
                    Button4.Visible = f_getenable(Status.Text)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            btnSave.Text = "Update"
            'posisi di akhir supaya tidak dead lock
            bank.Text = temp_BANK
            txtCity_Code.Text = temp_CITY
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            If CTFin.Text <> "" Then
                financeappby.Text = GetName2(CTFin.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            Me.Text = MODULE_NAME & "# " & Trim(NONum) & " - Update"
            btnSave.Enabled = f_getenable(Status.Text)
            btnReject.Enabled = btnSave.Enabled
            btnPrint.Enabled = btnSave.Enabled
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
    Private Sub FrmBPIB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 350)

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
        AppDT.Checked = True
    End Sub
    Private Sub GetName(ByVal sender As String)
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        If sender Is "App" Then
            PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                 "tu.user_ct = tum.user_ct where tum.modul_code = 'BP-A'"
            PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                   "tu.user_ct = tum.user_ct where tum.modul_code = 'BP-A'" & _
                                   "and tu.name LIKE 'FilterData1%' "
        ElseIf sender Is "Fun" Then
            PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                             "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'"
            PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                   "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'" & _
                                   "and tu.name LIKE 'FilterData1%' "
        End If
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If sender Is "App" Then
                approvedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            ElseIf sender Is "Fun" Then
                financeappby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
            Call Name_Data(sender)
        End If
    End Sub
    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String

        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & MODULE_CODE & "-A' and tu.name='" & approvedby.Text & "'"
        Else
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C' and tu.name='" & financeappby.Text & "'"
        End If

        errMSg = "Failed when read user authorization data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If sender = "App" Then
                        CTApp.Text = MyReader.GetString("user_ct")
                        approvedby.Text = MyReader.GetString("name")
                    Else
                        CTFin.Text = MyReader.GetString("user_ct")
                        financeappby.Text = MyReader.GetString("name")
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
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'BP-A' and tu.name='" & approvedby.Text & "'"

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
        Dim TGL1, AppDt1, FinDt1, DTPrinted1, SQLStr, str1, str2, str3, str4 As String
        Dim num1, num2, num3, num4 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDT, "Approved Date") Then Exit Sub
        If Not CekInputTgl(CTFin, FinDT, "Finance App Date") Then Exit Sub

        Try
            TGL1 = Format(tgl.Value, "yyyy-MM-dd")
            AppDt1 = Format(AppDT.Value, "yyyy-MM-dd")
            FinDt1 = Format(FinDT.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")

            If btnSave.Text = "Save" Then
                SQLStr = "Run Stored Procedure SaveBPIB (Save," & Ship & "," & txtCity_Code.Text & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & CTFin.Text & "," & FinDt1 & "," & crt.Text & "," & crtdt.Text & ")"
                keyprocess = "Save"
            ElseIf btnSave.Text = "Update" Then
                SQLStr = "Run Stored Procedure SaveBPIB (Updt," & Ship & "," & txtCity_Code.Text & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & CTFin.Text & "," & FinDt1 & "," & crt.Text & "," & crtdt.Text & ")"
                keyprocess = "Updt"
            End If



            MyComm.CommandText = "SaveBPIB"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("OpeningDt", TGL1)
            MyComm.Parameters.AddWithValue("BankCode", bank.Text)
            'MyComm.Parameters.AddWithValue("Amount", CDbl(TotalAmount.Text))
            MyComm.Parameters.AddWithValue("Remark", remark.Text)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            'MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
            'MyComm.Parameters.AddWithValue("AppDt", AppDt1)
            'MyComm.Parameters.AddWithValue("FinBy", CTFin.Text)
            'MyComm.Parameters.AddWithValue("FinDt", FinDt1)
            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDt1)
                MyComm.Parameters.AddWithValue("Stat", "Approved")
            End If
            If CTFin.Text = "" Then
                MyComm.Parameters.AddWithValue("FinBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("FinDt", DBNull.Value)
            Else
                MyComm.Parameters.AddWithValue("FinBy", CTFin.Text)
                MyComm.Parameters.AddWithValue("FinDt", FinDt1)
            End If
            MyComm.Parameters.AddWithValue("CityCode", txtCity_Code.Text)
            MyComm.Parameters.AddWithValue("DTPrint", DTPrinted1)
            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", NONum)
            End If
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " B-PIB")
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " B-PIB failed")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            'transaction.Rollback()
        End Try
    End Sub
    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject " & MODULE_NAME & "#" & NONum & " of " & Ship & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_budgets " & _
                     "SET STATUS='Rejected'" & _
                     " where SHIPMENT_NO='" & Ship & "' " & _
                     " and ord_no=" & NONum & "" & _
                     " AND TYPE_CODE = '" & MODULE_CODE & "'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update B-TT")
                Exit Sub
            Else
                msg = MODULE_NAME & "#" & NONum & " of " & Ship & " has been rejected"
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
        ViewerFrm.Tag = "BPIB" & v_num & Ship
        ViewerFrm.ShowDialog()
    End Sub



    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("Fun")
        FinDT.Checked = True
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strSQL As String
        Dim strSQLF As String
        Dim PilihanDlg As New DlgPilihan

        'strSQL = " SELECT DISTINCT TB.BANK_CODE AS BankCode,TB.BANK_NAME as BankName, " & _
        '"TB.ACCOUNT_NO as AccountNo, TB.CURRENCY_CODE as Currency, TB.MARGIN_DEPOSIT as MarginDeposit, TB.COMMISION as Commision, TB.POSTAGE_CHARGES as PostageCharges " & _
        '"FROM TBL_SHIPPING AS TS " & _
        '"LEFT JOIN TBM_PLANT AS TP ON TS.PLANT_CODE = TP.PLANT_CODE " & _
        '"LEFT JOIN TBM_BANK_REFERENCE AS TBR ON TP.COMPANY_CODE = TBR.REF_CODE " & _
        '"LEFT JOIN TBM_BANK AS TB ON TBR.BANK_CODE = TB.BANK_CODE " & _
        '"WHERE TS.SHIPMENT_NO = '" & Ship & "'"

        strSQL = "SELECT TB.BANK_CODE AS BankCode,TB.BANK_NAME AS BankName, " & _
        "TB.ACCOUNT_NO AS AccountNo, TB.CURRENCY_CODE AS Currency, " & _
        "TB.MARGIN_DEPOSIT AS MarginDeposit, TB.COMMISION AS Commision, TB.POSTAGE_CHARGES AS PostageCharges " & _
        "FROM TBM_BANK AS TB, TBM_BANK_REFERENCE AS TBR, TBM_PLANT AS TP, TBL_SHIPPING AS TS " & _
        "WHERE(TB.BANK_CODE = TBR.BANK_CODE And TP.COMPANY_CODE = TBR.REF_CODE) " & _
        "And TS.PLANT_CODE = TP.PLANT_CODE " & _
        "And TS.SHIPMENT_NO = '" & Ship & "' ORDER BY TB.BANK_CODE"

        strSQLF = strSQL & _
                  "and tb.bank_code like 'FilterData1%' and tb.bank_name like 'FilterData2%'"
        PilihanDlg.Text = "Select Bank code"
        PilihanDlg.LblKey1.Text = "Bank Code"
        PilihanDlg.LblKey2.Text = "Bank Name"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            bank.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        lblCityName.Text = AmbilData("CITY_NAME", "TBM_CITY", "CITY_CODE='" & txtCity_Code.Text & "'")
    End Sub

    Private Sub bank_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bank.TextChanged
        bank_name.Text = AmbilData("BANK_NAME", "TBM_BANK", "BANK_CODE='" & bank.Text & "'")
        acno.Text = AmbilData("ACCOUNT_NO", "TBM_BANK", "BANK_CODE='" & bank.Text & "'")
    End Sub

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function
End Class

