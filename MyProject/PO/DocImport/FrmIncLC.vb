'Title                         : Transaksi Document Import - IncLC
'Form                          : FrmIncLC
'Table Used                    : tbm_bank, tbm_bank_Reference, tbl_docimpr, tbl_budget
'Stored Procedure Used (MySQL) : SaveIncLC
'Created By               : Hanny, Mei 2009


Public Class FrmIncLC
    Dim PO, BOLCNum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim v_tot, f_get_tol_del As Double
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
    Sub New(ByVal pono As String, ByVal Currency As String, ByVal CurrName As String, ByVal TotalAmt As String, ByVal kurs As String, ByVal BOLC As String)
        'Dim ttl As Decimal
        Dim v_tgl As String

        InitializeComponent()
        'TotalAmount.Text = TotalAmt
        curr.Text = Currency
        Curr_Name.Text = CurrName
        'tgl.Value = Now
        'tgl.Value = GetServerDate()

        'txtrate.Text = kurs
        Call GetKurs()
        bank_name.Text = ""
        'DT2.Value = Now
        'DT3.Value = Now
        DT2.Value = GetServerDate()
        DT3.Value = GetServerDate()
        DT2.Checked = False
        DT3.Checked = False

        'tgl2.Text = Mid(tgl.Value.ToString, 1, 10)
        'tgl3.Text = Mid(tgl.Value.ToString, 1, 10)
        PO = pono
        Call GetButtonAccess()

        If Trim(BOLC) <> "" Then
            'original amount
            oriLC.Text = FormatNumber(AmbilAngka("amount", "tbl_budget", "po_no='" & pono & "' and type_code = 'BOLC' and status <> 'Rejected'"), 2)
            Call DisplayData(BOLC)
        Else
            btnReject.Enabled = False
            btnPrint.Enabled = False
            BOLCNum = "0"
            'opening dt
            v_tgl = AmbilData("openingdt", "tbl_budget", "po_no='" & pono & "'")
            If v_tgl = "" Then
                tgl.Value = GetServerDate()
            Else
                tgl.Value = CDate(v_tgl)
            End If
            'bank name
            bank.Text = AmbilData("bank_code", "tbl_budget", "po_no='" & pono & "' and type_code = 'BOLC' and status <> 'Rejected'")
            bank_data()
            'lc no
            lcno.Text = AmbilData("lc_no", "tbl_budget", "po_no='" & pono & "' and type_code = 'BOLC' and status <> 'Rejected'")
            'original amount
            oriLC.Text = FormatNumber(AmbilAngka("amount", "tbl_budget", "po_no='" & pono & "' and type_code = 'BOLC' and status <> 'Rejected'"), 2)
            TotalAmount.Text = 0
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            crtdt.Text = GetServerDate.ToString
            Me.Text = "Increasing LC - New"
        End If
    End Sub

    Private Sub DisplayData(ByVal BOLC As String)
        Dim pjg As Integer
        Dim strSQL, errMSg As String
        Dim TEMP As String = ""
        Dim tgl1 As Date

        pjg = Len(RTrim(BOLC)) - 6
        num = CInt(Mid(BOLC, 7, pjg))
        BOLCNum = num.ToString

        strSQL = " select a.*,b.* from tbl_budget as a" & _
                 " inner join tbm_bank as b on a.bank_code=b.bank_code " & _
                 " where a.po_no = '" & PO & "' and a.ord_no=" & num & "" & _
                 " AND a.TYPE_CODE = 'ICLC'"


        errMSg = "Failed when read ICLC data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = MyReader.GetString("bank_code")
                    tgl1 = MyReader.GetString("OpeningDt")
                    remark.Text = MyReader.GetString("Remark")
                    'DT2.Text = MyReader.GetString("APPROVEDDT")
                    'DT3.Text = MyReader.GetString("FINANCEAPPDT")                    
                    'CTApp.Text = MyReader.GetString("ApprovedBy")
                    'CTFun.Text = MyReader.GetString("FinanceAppBy")
                    Try
                        CTApp.Text = MyReader.GetString("ApprovedBy")
                    Catch ex As Exception
                        CTApp.Text = ""
                        DT2.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        DT2.Value = MyReader.GetString("APPROVEDDT")
                        DT2.Checked = True
                    End If
                    Try
                        CTFun.Text = MyReader.GetString("FinanceAppBy")
                    Catch ex As Exception
                        CTFun.Text = ""
                        DT3.Checked = False
                    End Try
                    If CTFun.Text <> "" Then
                        DT3.Value = MyReader.GetString("FINANCEAPPDT")
                        DT3.Checked = True
                    End If

                    Status.Text = MyReader.GetString("status")
                    crt.Text = MyReader.GetString("CreatedBy")
                    crtdt.Text = MyReader.GetString("CreatedDt")
                    lcno.Text = MyReader.GetString("LC_NO")
                    acno.Text = MyReader.GetString("BL_NO")
                    TotalAmount.Text = FormatNumber(MyReader.GetString("AMOUNT"), 3)
                    If btnReject.Enabled = True Then
                        btnReject.Enabled = f_getenable(Status.Text)
                    End If
                    If btnPrint.Enabled = True Then
                        btnPrint.Enabled = f_getenable(Status.Text)
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)

            bank.ReadOnly = f_getread(Status.Text)
            remark.ReadOnly = f_getread(Status.Text)
            'approvedby.ReadOnly = f_getread(Status.Text)
            'financeappby.ReadOnly = f_getread(Status.Text)
            btnSave.Enabled = f_getenable(Status.Text)
            btnSearch.Visible = f_getenable(Status.Text)
            Button3.Visible = f_getenable(Status.Text)
            Button4.Visible = f_getenable(Status.Text)

            tgl.Text = tgl1
            'posisi di akhir supaya tidak dead lock
            tgl.Enabled = f_getenable(Status.Text)
            DT2.Enabled = f_getenable(Status.Text)
            DT3.Enabled = f_getenable(Status.Text)
            btnSave.Text = "Update"
            bank.Text = TEMP
            bank_data()
            'v_tot = (CDec(deposit.Text) / 100 * CDec(oriLC.Text)) + (CDec(commision.Text) / 100 * CDec(TotalAmount.Text)) + CDec(charge.Text)
            'v_tot = (CDec(deposit.Text) / 100 * CDec(TotalAmount.Text)) + (CDec(commision.Text) / 100 * CDec(TotalAmount.Text)) + CDec(charge.Text)
            v_tot = ((CDec(deposit.Text) / 100) * CDec(TotalAmount.Text) * (100 + f_get_tol_del) / 100) + ((CDec(commision.Text) / 100) * CDec(TotalAmount.Text)) + CDec(charge.Text)
            tot.Text = FormatNumber(v_tot, 3)

            approvedby.Text = GetName2(CTApp.Text)
            financeappby.Text = GetName2(CTFun.Text)
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & crt.Text & "'")
            Me.Text = "Increasing LC # " & BOLCNum

            'cek enable lcno.
            If cekLC() = "0" Then
                lcno.ReadOnly = False
            Else
                lcno.ReadOnly = True
            End If
            If Status.Text = "Rejected" Then
                lcno.ReadOnly = True
            End If


        End If
    End Sub
    Private Function cekLC() As String
        cekLC = AmbilData("count(LC_No)", "tbl_remitance", "status <> 'Rejected' and LC_No='" & lcno.Text & "'")
    End Function
    Private Sub FrmIncLC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 360)

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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strSQL As String
        Dim strSQLF As String
        Dim PilihanDlg As New DlgPilihan

        strSQL = " select a.bank_code as BankCode, a.currency_code as CurrCode, a.bank_name as BankName, a.city_code as CityCode, " & _
                 " a.account_no as AccountNo, a.margin_deposit as MarginDeposit, a.commision as Commision, a.POSTAGE_CHARGES as PostageCharges " & _
                 " from tbm_bank as a" & _
                 " inner join tbm_bank_reference as b on a.bank_code=b.bank_code" & _
                 " inner join tbl_po as c on b.ref_code=c.company_code and c.currency_code = a.currency_code" & _
                 " where c.po_no = '" & PO & "'"
        strSQLF = strSQL & _
                  " and a.bank_code like 'FilterData1%' and a.bank_name like 'FilterData2%'"
        PilihanDlg.Text = "Select Bank code"
        PilihanDlg.LblKey1.Text = "Bank Code"
        PilihanDlg.LblKey2.Text = "Bank Name"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            bank.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            bank_data()
        End If
    End Sub
    Private Sub GetName(ByVal sender As String, ByVal modl As String)
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = '" & modl & "'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = '" & modl & "'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If sender Is "App" Then
                approvedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            ElseIf sender Is "Fun" Then
                financeappby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            End If
            Call Name_Data(sender)
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GetName("App", "IC-A")
        DT2.Checked = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("Fun", "FI-C")
        DT3.Checked = True
    End Sub
    Private Sub GetButtonAccess()
        Dim SQLStr, ModCode As String

        ModCode = "IC-C"
        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        btnSave.Enabled = (DataExist(SQLStr) = True)
        btnReject.Enabled = btnSave.Enabled

        ModCode = "IC-P"
        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        btnPrint.Enabled = (DataExist(SQLStr) = True)
    End Sub
    Private Function CekData() As Boolean
        If bank_name.Text = "" Then
            MsgBox("Bank code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            bank.Focus()
            Exit Function
        End If
        'If CTApp.Text = "" Then
        '    'MsgBox("Approved By name not found! ", MsgBoxStyle.Critical, "Warning")
        '    CekData = True
        '    'approvedby.Focus()
        '    Exit Function
        'Else
        '    If DT2.Checked = False Then
        '        MsgBox("Approved By name not found! ", MsgBoxStyle.Critical, "Warning")
        '        CekData = False
        '    Else
        '        CekData = True
        '    End If
        '    Exit Function
        'End If
        'If CTFun.Text = "" Then
        '    MsgBox("Finance App By name not found! ", MsgBoxStyle.Critical, "Warning")
        '    CekData = False
        '    financeappby.Focus()
        '    Exit Function
        'End If
        CekData = True
    End Function

    Private Sub bank_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles bank.PreviewKeyDown
        If e.KeyValue = 13 Then Call bank_data()
    End Sub

    Private Sub approvedby_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles approvedby.PreviewKeyDown
        Call Name_Data("App")
    End Sub

    Private Sub financeappby_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles financeappby.PreviewKeyDown
        Call Name_Data("Fun")
    End Sub
    Private Sub bank_data()
        Dim strSQL, errMSg As String


        'Dim ttl As Integer

        If BOLCNum > 0 Then
            'BOLC exist
            errMSg = "Failed when read ICLC data"
            strSQL = " select a.*,b.bank_name,p.tolerable_delivery from tbl_budget as a " & _
                     " inner join tbm_bank as b on a.bank_Code = b.bank_code " & _
                     " inner join tbl_po as p on a.po_no = p.po_no " & _
                     " where a.po_no = '" & PO & "' and a.ord_no=" & BOLCNum & " AND a.TYPE_CODE = 'ICLC' "
        Else
            'Create new
            errMSg = "Failed when read bank data"
            strSQL = " select a.*, c.TOLERABLE_DELIVERY from tbm_bank as a" & _
                     " inner join tbm_bank_reference as b on a.bank_code=b.bank_code" & _
                     " inner join tbl_po as c on b.ref_code=c.company_code and c.currency_code = a.currency_code" & _
                     " where c.po_no = '" & PO & "' and a.bank_code='" & bank.Text & "'"
        End If

        deposit.Text = ""
        commision.Text = ""
        charge.Text = ""
        tot.Text = ""
        bank_name.Text = ""


        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    bank_name.Text = MyReader2.GetString("bank_name")
                    acno.Text = MyReader2.GetString("account_no")
                    deposit.Text = FormatNumber(MyReader2.GetString("margin_deposit"), 3, , , TriState.True)
                    commision.Text = FormatNumber(MyReader2.GetString("commision"), 3, , , TriState.True)
                    charge.Text = FormatNumber(MyReader2.GetString("postage_charges"), 3, , , TriState.True)
                    'lcno.Text = MyReader2.GetString("lc_no")
                    'oriLC.Text = FormatNumber(MyReader2.GetString("amount"), 3)
                    'v_tot = ((CDec(deposit.Text) * CDec(TotalAmount.Text)) + (CDec(commision.Text) * CDec(TotalAmount.Text)) + (CDec(charge.Text) * CDec(TotalAmount.Text)))
                    'rumus baru
                    Try
                        f_get_tol_del = MyReader2.GetString("TOLERABLE_DELIVERY")
                    Catch
                        f_get_tol_del = 1
                    End Try
                    'v_tot = ((CDec(TotalAmount.Text) * ((100 + f_get_tol_del) / 100)) * (CDec(deposit.Text) / 100 + CDec(commision.Text) / 100)) + CDec(charge.Text)

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Sub
    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String

        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'BO-A' and tu.name='" & approvedby.Text & "'"
        ElseIf sender = "Fun" Then
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
                    ElseIf sender = "Fun" Then
                        CTFun.Text = MyReader.GetString("user_ct")
                        financeappby.Text = MyReader.GetString("name")
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Function GetNum(ByVal strnum As String) As Decimal
        Dim temp As String

        temp = Replace(strnum, ".", "")
        GetNum = CDec(temp)
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        'Dim transaction As MySqlTransaction
        Dim OpeningDt, AppDt, FinAppDt, SQLStr, str1, str2, str3, str4 As String
        Dim num1, num2, num3, num4 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, DT2, "Approved Date") Then Exit Sub
        If Not CekInputTgl(CTFun, DT3, "FundApp Date") Then Exit Sub


        Try
            OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            AppDt = Format(DT2.Value, "yyyy-MM-dd")
            FinAppDt = Format(DT3.Value, "yyyy-MM-dd")

            'num1 = GetNum(TotalAmount.Text)
            'num2 = GetNum(deposit.Text)
            'num3 = GetNum(commision.Text)
            'num4 = GetNum(charge.Text)

            str1 = Replace(num1, ",", ".")
            str2 = Replace(num2, ",", ".")
            str3 = Replace(num3, ",", ".")
            str4 = Replace(num4, ",", ".")

            num1 = CDec(TotalAmount.Text)
            num2 = CDec(deposit.Text)
            num3 = CDec(commision.Text)
            num4 = CDec(charge.Text)

            If btnSave.Text = "Save" Then
                SQLStr = "Run Stored Procedure SaveIncLC (Save," & PO & "," & OpeningDt & "," & bank.Text & "," & acno.Text & "," & str1 & "," _
                         & str2 & "," & str3 & "," & str4 & "," & remark.Text & "," & CTApp.Text & "," & AppDt & "," & CTFun.Text & "," & FinAppDt & ")"
                keyprocess = "Save"
            Else
                SQLStr = "Run Stored Procedure SaveIncLC (Updt," & PO & "," & OpeningDt & "," & bank.Text & "," & acno.Text & "," & str1 & "," _
                                         & str2 & "," & str3 & "," & str4 & "," & remark.Text & "," & CTApp.Text & "," & AppDt & "," & CTFun.Text & "," & FinAppDt & ")"
                keyprocess = "Updt"
            End If

            MyComm.CommandText = "SaveIncLC"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("PO", PO)
            MyComm.Parameters.AddWithValue("OpeningDt", OpeningDt)
            MyComm.Parameters.AddWithValue("LcNo", lcno.Text)
            MyComm.Parameters.AddWithValue("BankCode", bank.Text)
            MyComm.Parameters.AddWithValue("AccountNo", acno.Text)

            MyComm.Parameters.AddWithValue("Amount", num1)
            MyComm.Parameters.AddWithValue("Deposit", num2)
            MyComm.Parameters.AddWithValue("Commision", num3)
            MyComm.Parameters.AddWithValue("Charge", num4)
            MyComm.Parameters.AddWithValue("Remark", remark.Text)
            'MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
            'MyComm.Parameters.AddWithValue("AppDt", AppDt)
            'MyComm.Parameters.AddWithValue("FinAppBy", CTFun.Text)
            'MyComm.Parameters.AddWithValue("FinAppDt", FinAppDt)
            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDt)
                MyComm.Parameters.AddWithValue("Stat", "Approved")
            End If
            If CTFun.Text = "" Then
                MyComm.Parameters.AddWithValue("FinAppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("FinAppDt", DBNull.Value)
            Else
                MyComm.Parameters.AddWithValue("FinAppBy", CTFun.Text)
                MyComm.Parameters.AddWithValue("FinAppDt", FinAppDt)
            End If
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
                f_msgbox_successful("Save ICLC")
                btnClose_Click(sender, e)
            Else
                MsgBox("Save ICLC failed'")
            End If
            'transaction.Commit()
        Catch ex As Exception
            MsgBox(ex.Message)
            'transaction.Rollback()
        End Try
    End Sub

    Private Sub bank_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bank.TextChanged
        bank_data()
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject Increasing LC #" & BOLCNum & " of " & PO & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_budget " & _
                     "SET status='Rejected'" & _
                     " where PO_No='" & PO & "' " & _
                     " and ord_no=" & BOLCNum & "" & _
                     " AND TYPE_CODE = 'ICLC' "

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "Increasing LC #" & BOLCNum & " of " & PO & " has been rejected"
                MsgBox(msg)
            End If

        End If
        btnClose_Click(sender, e)
    End Sub
    Private Function GetName2(ByVal code As String) As String
        Dim strSQL, errMsg As String
        If code <> "" Then
            strSQL = "Select tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tu.user_Ct=" & code & ""

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
        End If
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        'v_pono = txtpono.ToString
        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num.ToString
        End If
        ViewerFrm.Tag = "ICLC" & v_num & PO
        ViewerFrm.ShowDialog()
    End Sub
    Function GetServerDate() As Date
        'Dim temp As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Sub GetKurs()
        Dim temp As String

        temp = Format(tgl.Value, "yyyy-MM-dd")
        txtrate.Text = AmbilData("EFFECTIVE_KURS", "tbm_kurs", "currency_code='" & curr.Text & "' and effective_Date='" & temp & "'")
        If txtrate.Text = "" Then txtrate.Text = "0"
        txtrate.Text = FormatNumber(txtrate.Text, 2, , , TriState.True)
    End Sub
    Private Sub tgl_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tgl.ValueChanged
        Call GetKurs()
    End Sub

    Private Sub TotalAmount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TotalAmount.Validated
        If IsNumeric(TotalAmount.Text) = True Then
            'v_tot = (CDec(deposit.Text) / 100 * CDec(TotalAmount.Text)) + (CDec(commision.Text) / 100 * CDec(TotalAmount.Text)) + CDec(charge.Text)
            v_tot = ((CDec(deposit.Text) / 100) * CDec(TotalAmount.Text) * (100 + f_get_tol_del) / 100) + ((CDec(commision.Text) / 100) * CDec(TotalAmount.Text)) + CDec(charge.Text)
            tot.Text = FormatNumber(v_tot, 3)
            TotalAmount.Text = FormatNumber(TotalAmount.Text, 3)
        Else
            MsgBox("Total Amount should be numeric! ", MsgBoxStyle.Critical, "Error")
        End If
    End Sub
End Class