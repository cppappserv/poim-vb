'Title                         : Transaksi Document Import - BR
'Form                          : FrmBR
'Created By                    : Hanny
'Created Date                  : 18 NOV 2008
'Table Used                    : tbm_bank, tbm_bank_Reference, 
'                                tbl_docimpr, tbl_remitance
'Stored Procedure Used (MySQL) : SaveBR

Public Class FrmBR
    Dim Ship, BRNum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num, startdata As Integer
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
    Sub New(ByVal ShipNo As String, ByVal Currency As String, ByVal CurrName As String, ByVal TotalAmt As String, ByVal kurs As String, ByVal BR As String)
        Dim ttl As Decimal
        Dim v_tgl As String
        'Dim lcno As String

        InitializeComponent()

        TotalInvoice.Text = AmbilData("sum(invoice_amount-invoice_penalty)", "tbl_shipping_invoice", "shipment_no='" & ShipNo & "'")
        TotalInvoice.Text = FormatNumber(TotalInvoice.Text, 2)

        kurs = AmbilData("A.EFFECTIVE_KURS", "TBM_KURS AS A INNER JOIN TBL_SHIPPING AS B ON B.RECEIVED_DOC_DT = A.EFFECTIVE_DATE AND B.CURRENCY_CODE = A.CURRENCY_CODE", "B.SHIPMENT_NO='" & ShipNo & "'")
        Currency = AmbilData("CURRENCY_CODE", "TBL_SHIPPING", "SHIPMENT_NO='" & ShipNo & "'")
        curr.Text = Currency
        CurrName = AmbilData("CURRENCY_NAME", "TBM_CURRENCY", "CURRENCY_CODE='" & Currency & "'")
        Curr_Name.Text = CurrName
        txtrate.Text = IIf(kurs = "", 0, kurs)
        txtrate.Text = FormatNumber(txtrate.Text, 2)
        If txtrate.Text = "0" Then
            ttl = CDec(TotalInvoice.Text)
        Else
            ttl = CDec(TotalInvoice.Text) * CDec(txtrate.Text)
        End If
        'tot.Text = ttl.ToString
        'If Len(tot.Text) > 3 Then
        '    tot.Text = FM11_Kurs.GetMaskText(Mid(tot.Text, 1, Len(tot.Text) - 2))
        'End If
        'tgl.Value = GetServerDate()
        v_tgl = AmbilData("tt_dt", "tbl_shipping", "SHIPMENT_NO='" & ShipNo & "'")
        If v_tgl = "" Then
            tgl.Value = GetServerDate()
        Else
            tgl.Value = CDate(v_tgl)
        End If

        tgl2.Value = GetServerDate()
        tgl3.Value = GetServerDate()
        tgl2.Checked = False
        tgl3.Checked = False

        Ship = ShipNo
        BLno.Text = ""
        'get default bank code
        bank_name.Text = ""
        bank.Text = AmbilData("distinct a.bank_code", "tbm_bank as a inner join tbl_budget as b on b.bank_code = a.bank_code inner join tbl_shipping_detail as c on c.po_no = b.po_no", "c.shipment_no='" & ShipNo & "'")

        'Call GetButtonAccess()
        startdata = 0
        If Trim(BR) <> "" Then
            fillcbLC()
            Call DisplayData(BR)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("BR-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If

        Else
            startdata = 1

            TotalAmount.Text = FormatNumber(0, 2)
            CutMargin.Text = FormatNumber(0, 2)
            deposit.Text = FormatNumber(0, 3)
            commision.Text = FormatNumber(0, 3)
            charge.Text = FormatNumber(0, 3)
            tot.Text = FormatNumber(0, 3)
            crtdt.Text = GetServerDate.ToString
            CTCrt.Text = UserData.UserCT.ToString
            crt.Text = UserData.UserName ' GetName2(CTCrt.Text)
            btnReject.Enabled = False
            btnPrint.Enabled = False
            BRNum = "0"
            BLno.Text = AmbilData("BL_NO", "tbl_shipping", "shipment_no='" & ShipNo & "'")
            Me.Text = "Budget Remitance - New"
            'fill cbLC
            fillcbLC()
        End If
    End Sub
    Private Sub fillcbLC()
        Dim strSQL, errMSg, temp As String
        strSQL = "select distinct B.LC_NO from tbl_budget AS B INNER JOIN TBL_SHIPPING_DETAIL AS S " & _
                 "ON B.PO_NO = S.PO_NO where S.SHIPMENT_NO = '" & Ship & "' and B.STATUS <> 'Rejected'"

        'Button4.Visible = False

        errMSg = "Failed when read BR data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        cbLC.Refresh()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = MyReader.GetString("LC_no")
                    cbLC.Items.Add(temp)

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub DisplayData(ByVal BR As String)
        Dim pjg As Integer
        Dim strSQL, errMSg As String
        Dim temp As String = ""
        Dim tempLC As String = ""
        Dim tempDep As Double = 0
        Dim tempCom As Double = 0
        Dim tempChrg As Double = 0
        Dim v_pono As String
        Dim v_tot, f_get_tol_del As Double

        pjg = Len(RTrim(BR)) - 4
        num = CInt(Mid(BR, 5, pjg))
        BRNum = num.ToString

        strSQL = " select a.*,b.* from tbl_remitance as a" & _
                 " left join tbm_bank as b on a.bank_code=b.bank_code " & _
                 " where a.shipment_no = '" & Ship & "' and a.ord_no='" & BRNum & "' " & _
                 " and a.type_code = 'BR' "
        'tgl.Enabled = False
        'bank.ReadOnly = True
        'remark.ReadOnly = True
        'approvedby.ReadOnly = True
        'financeappby.ReadOnly = True
        'crt.ReadOnly = True

        errMSg = "Failed when read BR data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try

                    temp = MyReader.GetString("bank_code")
                    tgl.Value = MyReader.GetString("OpeningDt")

                    TotalAmount.Text = MyReader.GetString("Amount")
                    TotalAmount.Text = FormatNumber(TotalAmount.Text, 2)
                    remark.Text = MyReader.GetString("Remark")

                    CTCrt.Text = MyReader.GetString("CreatedBy")
                    crtdt.Text = MyReader.GetString("CREATEDDT")

                    'CTApp.Text = MyReader.GetString("ApprovedBy")
                    'CTFun.Text = MyReader.GetString("FinanceAppBy")
                    'tgl2.Value = MyReader.GetString("APPROVEDDT")
                    'tgl3.Value = MyReader.GetString("FINANCEAPPDT")
                    Try
                        CTApp.Text = MyReader.GetString("ApprovedBy")
                    Catch ex As Exception
                        CTApp.Text = ""
                        tgl2.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        tgl2.Value = MyReader.GetString("APPROVEDDT")
                        tgl2.Checked = True
                    End If
                    Try
                        CTFun.Text = MyReader.GetString("FinanceAppBy")
                    Catch ex As Exception
                        CTFun.Text = ""
                        tgl3.Checked = False
                    End Try
                    If CTFun.Text <> "" Then
                        tgl3.Value = MyReader.GetString("FINANCEAPPDT")
                        tgl3.Checked = True
                    End If


                    Status.Text = MyReader.GetString("status")
                    CutMargin.Text = MyReader.GetString("cutmargin")
                    CutMargin.Text = FormatNumber(CutMargin.Text, 2)
                    BLno.Text = MyReader.GetString("BL_NO")
                    tempLC = MyReader.GetString("LC_NO")
                    tempDep = CDbl(MyReader.GetString("MARGIN_DEPOSIT"))
                    tempCom = CDbl(MyReader.GetString("COMMISION"))
                    tempChrg = CDbl(MyReader.GetString("POSTAGE_CHARGES"))
                    btnReject.Enabled = f_getenable(Status.Text)
                    btnPrint.Enabled = f_getenable(Status.Text)
                    If btnSave.Enabled = True Then
                        btnSave.Enabled = f_getenable(Status.Text)
                        btnSearch.Visible = f_getenable(Status.Text) 'False
                        Button3.Visible = f_getenable(Status.Text) ' False
                        Button4.Visible = f_getenable(Status.Text) ' False
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)

            'posisi di akhir supaya tidak dead lock
            bank.Text = temp
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            If CTFun.Text <> "" Then
                financeappby.Text = GetName2(CTFun.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            cbLC.Text = tempLC
            'but, dont get the LC detail from table master.
            'get from tbl_remitance aja.
            deposit.Text = CStr(FormatNumber(tempDep, 3))
            commision.Text = CStr(FormatNumber(tempCom, 3))
            charge.Text = CStr(FormatNumber(tempChrg, 3))
            'tot.Text = CStr(FormatNumber((tempDep + tempCom + tempChrg), 3))

            strSQL = "SELECT DISTINCT PO_NO, ACCOUNT_NO, MARGIN_DEPOSIT, COMMISION, POSTAGE_CHARGES " _
                   & "FROM TBL_BUDGET WHERE LC_NO ='" & cbLC.Text & "' and STATUS <> 'Rejected' " _
                   & "AND PO_NO IN (SELECT PO_NO FROM TBL_SHIPPING_DETAIL WHERE SHIPMENT_NO='" & Ship & "')"

            errMSg = "Failed when read user data"
            MyReader2 = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

            If Not MyReader2 Is Nothing Then
                While MyReader2.Read
                    Try
                        v_pono = MyReader2.GetString("PO_NO")
                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader2, UserData)
                'rumus baru

                strSQL = "select TOLERABLE_DELIVERY FROM tbl_po WHERE po_no ='" & v_pono & "'"

                errMSg = "Failed when read user data"
                MyReader2 = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
                f_get_tol_del = 1
                If Not MyReader2 Is Nothing Then
                    While MyReader2.Read
                        Try
                            f_get_tol_del = MyReader2.GetString("TOLERABLE_DELIVERY")
                        Catch
                            f_get_tol_del = 1
                        End Try
                    End While
                    CloseMyReader(MyReader2, UserData)
                End If
                'v_tot = ((CDec(TotalInvoice.Text) * ((100 + f_get_tol_del) / 100)) * (CDec(deposit.Text) / 100 + CDec(commision.Text) / 100)) + CDec(charge.Text)
                v_tot = ((CDec(deposit.Text) / 100) * CDec(TotalAmount.Text) * (100 + f_get_tol_del) / 100) + ((CDec(commision.Text) / 100) * CDec(TotalAmount.Text)) + CDec(charge.Text)
                'tot.Text = FormatNumber(v_tot, 3)
                tot.Text = FormatNumber(1000, 3)
            Else
                tot.Text = 0
            End If

            'save button = update button
            btnSave.Text = "Update"
            'Dim ship_status As String
            'ship_status = AmbilData("STATUS", "tbl_shipping", "SHIPMENT_NO='" & Ship & "'")
            'If ship_status <> "CLOSED" Then
            '    btnSave.Enabled = True
            'Else
            '    btnSave.Enabled = False
            'End If
            Me.Text = "Budget Remitance # " & Trim(BRNum) & " - Update"
        End If
    End Sub
    Private Sub FrmBR_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strSQL As String
        Dim PilihanDlg As New DlgPilihan

        'strSQL = " select a.* from tbm_bank as a" & _
        '         " inner join tbm_bank_reference as b on a.bank_code=b.bank_code" & _
        '         " inner join tbl_po as c on b.ref_code=c.company_code and c.currency_code = a.currency_code" & _
        '         " where c.po_no = '" & PO & "'"
        strSQL = " select Bank_code as BankCode, Bank_name as BankName from tbm_bank"

        PilihanDlg.Text = "Select Bank Code"
        PilihanDlg.LblKey1.Text = "Bank Code"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQL
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            bank.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            bank_data()
        End If
    End Sub
    Private Sub GetName(ByVal sender As String)
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'PO-A'" & _
                               "and tu.name LIKE 'FilterData1%' "
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
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        GetName("App")
        tgl2.Checked = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("Fun")
        tgl3.Checked = True
    End Sub
    Private Function CekData() As Boolean
        Dim STRsql As String

        CekData = True

        If Trim(cbLC.Text) = "" Then
            MsgBox("Select LC NO first! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            cbLC.Focus()
            Exit Function
        End If

        'Foreign Key
        STRsql = " select * from tbm_bank  WHERE bank_code='" & bank.Text & "'"

        If FM02_MaterialGroup.DataOK(STRsql) = True Then
            MsgBox("Bank code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            bank.Focus()
            Exit Function
        End If

        'Foreign Key
        If approvedby.Text <> "" Then
            STRsql = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'BR-A' and tu.name='" & approvedby.Text & "'"

            If FM02_MaterialGroup.DataOK(STRsql) = True Then
                MsgBox("Name does not exist! ", MsgBoxStyle.Critical, "Warning")
                CekData = False
                approvedby.Focus()
                Exit Function
            End If
        End If

        'Foreign Key
        If financeappby.Text <> "" Then
            STRsql = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C' and tu.name='" & financeappby.Text & "'"

            If FM02_MaterialGroup.DataOK(STRsql) = True Then
                MsgBox("Name does not exist! ", MsgBoxStyle.Critical, "Warning")
                CekData = False
                financeappby.Focus()
                Exit Function
            End If
        End If
    End Function

    Private Sub bank_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles bank.PreviewKeyDown
        If e.KeyValue = 13 Then
            Call bank_data()
        End If
    End Sub

    Private Sub approvedby_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles approvedby.PreviewKeyDown
        Call Name_Data("App")
    End Sub

    Private Sub financeappby_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles financeappby.PreviewKeyDown
        Call Name_Data("Fun")
    End Sub
    Private Sub bank_data()
        Dim strSQL, errMSg As String

        ''If BRNum > "0" Then
        '''BR exist
        ''errMSg = "Failed when read BR data"
        ''strSQL = " select a.*,b.* from tbl_remitance as a" & _
        ''          " left join tbm_bank as b on a.bank_code=b.bank_code " & _
        ''          " where a.shipment_no = '" & Ship & "' and a.ord_no='" & BRNum & "' " & _
        ''          " and a.type_code = 'BR' "
        ''Else
        'Create new
        errMSg = "Failed when read bank data"
        strSQL = " select * from tbm_bank " & _
                 " where bank_code='" & bank.Text & "'"
        ''End If

        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    bank_name.Text = MyReader2.GetString("bank_name")
                    acno.Text = MyReader2.GetString("account_no")
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
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'BR-A' and tu.name='" & approvedby.Text & "'"
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
        Dim createDt, OpeningDt, AppDt, FinAppDt, SQLStr, str1, str2, str3, str4 As String
        Dim num1, num2, num3, num4, num5 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, tgl2, "Approved Date") Then Exit Sub
        If Not CekInputTgl(CTFun, tgl3, "Finance App Date") Then Exit Sub
        'transaction = MyConn.BeginTransaction(IsolationLevel.ReadCommitted)
        'MyComm.Transaction = transaction
        Try
            OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            AppDt = Format(CDate(tgl2.Value), "yyyy-MM-dd")
            FinAppDt = Format(CDate(tgl3.Value), "yyyy-MM-dd")
            createDt = Format(Now, "yyyy-MM-dd")
            'num1 = GetNum(TotalAmount.Text)
            'num2 = GetNum(deposit.Text)
            'num3 = GetNum(commision.Text)
            'num4 = GetNum(charge.Text)

            'str1 = Replace(TotalAmount.Text, ",", ".")
            'str2 = Replace(deposit.Text, ",", ".")
            'str3 = Replace(commision.Text, ",", ".")
            'str4 = Replace(charge.Text, ",", ".")

            Try
                num1 = CDec(TotalAmount.Text)
            Catch ex As Exception
                num1 = 0
            End Try
            Try
                num2 = CDec(deposit.Text)
            Catch ex As Exception
                num2 = 0
            End Try
            Try
                num3 = CDec(commision.Text)
            Catch ex As Exception
                num3 = 0
            End Try
            Try
                num4 = CDec(charge.Text)
            Catch ex As Exception
                num4 = 0
            End Try
            Try
                num5 = CDec(CutMargin.Text)
            Catch ex As Exception
                num5 = 0
            End Try

            If btnSave.Text = "Save" Then
                SQLStr = "Run Stored Procedure SaveBR (Save," & Ship & "," & OpeningDt & "," & cbLC.Text & "," & BLno.Text & "," & bank.Text & "," & acno.Text & "," & num1 & "," _
                         & num2 & "," & num3 & "," & num4 & "," & num5 & "," & remark.Text & "," & CTApp.Text & "," & AppDt & "," & CTFun.Text & "," & FinAppDt & "," & crt.Text & "," & createDt & ")"
                keyprocess = "Save"
            ElseIf btnSave.Text = "Update" Then
                SQLStr = "Run Stored Procedure SaveBR (Updt," & Ship & "," & OpeningDt & "," & cbLC.Text & "," & BLno.Text & "," & bank.Text & "," & acno.Text & "," & num1 & "," _
                         & num2 & "," & num3 & "," & num4 & "," & num5 & "," & remark.Text & "," & CTApp.Text & "," & AppDt & "," & CTFun.Text & "," & FinAppDt & "," & crt.Text & "," & createDt & ")"
                keyprocess = "Updt"
            End If

            MyComm.CommandText = "SaveBR"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            'MyComm.Parameters.AddWithValue("Ord", Ship)
            MyComm.Parameters.AddWithValue("OpeningDt", OpeningDt)
            MyComm.Parameters.AddWithValue("LCNO", cbLC.Text)
            MyComm.Parameters.AddWithValue("BLNO", BLno.Text)


            MyComm.Parameters.AddWithValue("BankCode", bank.Text)
            MyComm.Parameters.AddWithValue("AccountNo", acno.Text)

            MyComm.Parameters.AddWithValue("Amount", num1)
            MyComm.Parameters.AddWithValue("Deposit", num2)
            MyComm.Parameters.AddWithValue("Commision", num3)
            MyComm.Parameters.AddWithValue("Charge", num4)
            MyComm.Parameters.AddWithValue("CMargin", num5)
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
                MyComm.Parameters.AddWithValue("vord", BRNum)
            End If
            MyComm.Parameters.AddWithValue("Hasil", hasil)


            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " BR")
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " BR failed'")
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

        msg = "Reject Budget Remitance #" & BRNum & " of " & Ship & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_remitance " & _
                     "SET status='Rejected'" & _
                     " where Shipment_No='" & Ship & "' " & _
                     " and ord_no=" & BRNum & "" & _
                     " AND TYPE_CODE = 'BR'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "Budget Remitance #" & BRNum & " of " & Ship & " has been rejected"
                MsgBox(msg)
            End If

        End If
        btnClose_Click(sender, e)
    End Sub
    Private Function GetName2(ByVal code As String) As String
        Dim strSQL, errMsg As String

        strSQL = "Select distinct tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
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
    End Function

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String
        Dim strSQL, errMsg, GetBL As String
        Dim OpeningDt As String

        OpeningDt = Format(tgl.Value, "yyyy-MM-dd")

        strSQL = "SELECT GROUP_CONCAT(bl_no SEPARATOR ', ') bl_no FROM tbl_remitance WHERE lc_no='" & cbLC.Text & "' and openingdt = '" & tgl.Text & "'"
        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    GetBL = MyReader2.GetString("bl_no")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
            MsgBox("Creating Budget Remitance LC " & cbLC.Text & " of BL " & GetBL, MsgBoxStyle.Information, "Information")
        End If

        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num.ToString
        End If
        ViewerFrm.Tag = "BRRR" & v_num & Ship
        ViewerFrm.ShowDialog()
    End Sub

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub Calc_Amount()
        Dim strSQL, errMsg As String
        Dim f_lcamount, f_lcamount_tot, f_lcmargin, f_lcamount_up, f_lcamount_more, f_brcutmargin_ot, f_cutmargin, f_moreless As Double
        Dim f_amount As Double
        Dim lv_bank As String

        If Trim(cbLC.Text) <> "" And startdata = 1 Then
            strSQL = "SELECT t1.lc_no, t1.bank_code, t1.account_no, t1.margin_deposit, t1.commision, t1.postage_charges, t1.amount, IF(m1.more_less=1,t2.TOLERABLE_DELIVERY,0) moreless " _
                       & "FROM tbl_budget t1, tbl_po t2, tbm_bank m1 " _
                       & "WHERE t1.po_no=t2.po_no AND t1.bank_code = m1.bank_code AND t1.lc_no = '" & cbLC.Text & "' AND t1.STATUS<> 'Rejected'"

            errMsg = "Failed when read user data"
            MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

            If Not MyReader2 Is Nothing Then
                While MyReader2.Read
                    Try
                        lv_bank = MyReader2.GetString("BANK_CODE")
                        acno.Text = MyReader2.GetString("ACCOUNT_NO")
                        deposit.Text = FormatNumber(MyReader2.GetString("MARGIN_DEPOSIT"), 3, , , TriState.True)
                        commision.Text = FormatNumber(MyReader2.GetString("COMMISION"), 3, , , TriState.True)
                        charge.Text = FormatNumber(MyReader2.GetString("POSTAGE_CHARGES"), 3, , , TriState.True)

                        f_lcmargin = MyReader2.GetString("MARGIN_DEPOSIT")
                        f_lcamount = MyReader2.GetString("AMOUNT")
                        f_moreless = MyReader2.GetString("MORELESS")
                        f_lcamount_tot = f_lcamount_tot + f_lcamount

                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader2, UserData)

                bank.Text = lv_bank

                If f_lcmargin > 0 Then
                    f_lcamount_up = f_lcamount_tot * (f_lcmargin / 100)
                    f_lcamount_more = ((f_lcamount_tot * (100 + f_moreless) / 100) * (f_lcmargin / 100))
                    f_cutmargin = TotalInvoice.Text * (f_lcmargin / 100)
                End If

                strSQL = "SELECT lc_no, SUM(cutmargin) cutmargin FROM tbl_remitance " _
                        & "WHERE lc_no = '" & cbLC.Text & "' AND STATUS<> 'Rejected' AND shipment_no <> '" & Ship & "' " _
                        & "GROUP BY lc_no"

                errMsg = "Failed when read user data"
                MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

                If Not MyReader2 Is Nothing Then
                    While MyReader2.Read
                        Try
                            f_brcutmargin_ot = MyReader2.GetString("CUTMARGIN")
                        Catch
                            f_brcutmargin_ot = 0
                        End Try
                    End While
                    CloseMyReader(MyReader2, UserData)
                End If

                If Not (ChkFinal.Checked) Then
                    f_amount = TotalInvoice.Text - f_cutmargin
                Else
                    f_cutmargin = f_lcamount_more - f_brcutmargin_ot
                    If f_cutmargin < 0 Then f_cutmargin = 0
                    f_amount = TotalInvoice.Text - f_cutmargin
                End If
                ''---

                TotalAmount.Text = f_amount
                TotalAmount.Text = FormatNumber(TotalAmount.Text, 2)
                CutMargin.Text = f_cutmargin
                CutMargin.Text = FormatNumber(CutMargin.Text, 2)
            Else
                acno.Text = ""
                deposit.Text = ""
                commision.Text = ""
                charge.Text = ""
                TotalAmount.Text = ""
                CutMargin.Text = ""
            End If
        End If
    End Sub

    Private Sub cbLC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLC.SelectedIndexChanged
        Calc_Amount()
        startdata = 1
    End Sub

    Private Sub ChkFinal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkFinal.CheckedChanged
        startdata = 1
        Calc_Amount()
    End Sub
End Class