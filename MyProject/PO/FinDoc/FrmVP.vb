﻿''' <summary>
''' Title                         : Financial Documents - VP  ==> menjadi Funds & Finance - VP
''' SubForm                       : FrmVP
''' MainForm                      : FrmBL
''' Table Used                    : 
''' Stored Procedure Used (MySQL) : SaveVP
''' Created By                    : Yanti, 10.02.2009
''' 
''' 
''' </summary>
''' <remarks></remarks>


Public Class FrmVP
    Public ClientDecimalSeparator, ClientGroupSeparator As String
    Public RegionalSetting As System.Globalization.CultureInfo
    Public ServerDecimal, ServerSeparator As String
    Dim strSQL, errMSg, BLStatus, BLNo As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim DataError, Edit, Baru As Boolean
    Dim VPNo, ShipNo, ShipOrdNo As Integer
    Dim arrBank() As String
    Dim BankBranch As String

    Sub New(ByVal No As Integer, ByVal Ship As Integer, ByVal ShipOrd As Integer, ByVal BLStat As String, ByVal BLNum As String)
        Dim tg As Date

        VPNo = No
        ShipNo = Ship
        ShipOrdNo = ShipOrd
        BLStatus = BLStat
        BLNo = BLNum
        InitializeComponent()
        ' Call GetButtonAccess()
        tg = GetServerDate()
        dt2.Value = tg
        dt3.Value = tg

        If Trim(VPNo) <> 0 Then
            Call DisplayData()
            Edit = True
            Baru = False
            btnSave.Enabled = (btnSave.Enabled) And (CRTCODE.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CRTCODE.Text = UserData.UserCT)

            If (btnPrint.Enabled) And (PunyaAkses("VP-P")) Then
                btnPrint.Enabled = True
                btnPrintLamp.Enabled = True
            Else
                btnPrint.Enabled = False
                btnPrintLamp.Enabled = False
            End If
        Else
            btnReject.Enabled = False
            btnPrint.Enabled = False
            btnPrintLamp.Enabled = False
            dt1.Value = tg
            currname.Text = ""
            crtdt.Text = Microsoft.VisualBasic.Left(tg.ToString, 10)
            total.Text = FormatNumber(0, 2, , , TriState.True)
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            Me.Text = "PV - New"
            Edit = False
            Baru = True
        End If
    End Sub
    Private Sub GetCostData()
        Dim dts, dts2 As DataTable
        Dim DT As New System.Data.DataTable
        Dim cbn As New DataGridViewComboBoxColumn

        grid.DataSource = Nothing
        grid.Columns.Clear()
        errMSg = "Cost data view failed"
        strSQL = "select cost_code,cost_description,cost_amount from tbl_Cost where shipment_no=" & ShipNo & " and ship_ord_no=" & ShipOrdNo & " and type_code = 'VP'"
        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        grid.DataSource = dts

        'Combo Box Document
        errMSg = "Tbm_CostCategory data view failed"
        strSQL = "select costcat_code,costcat_name from tbm_costcategory"
        dts2 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn
            .DataSource = dts2
            .DisplayMember = "COSTCAT_NAME"
            .ValueMember = "COSTCAT_CODE"
        End With
        grid.Columns.Insert(1, cbn)
        grid.Columns(0).Visible = False
        grid.Columns(1).Width = 250
        grid.Columns(1).HeaderText = "Item Cost"
        grid.Columns(2).HeaderText = "Description"
        grid.Columns(2).Width = 250
        grid.Columns(3).HeaderText = "Amount"
        grid.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns(3).DefaultCellStyle.Format = "N2"
    End Sub
    Private Function GetValue(ByVal field As String) As String
        Try
            GetValue = MyReader.GetString(field)
        Catch ex As Exception
            GetValue = ""
        End Try
    End Function
    Private Sub DisplayData()
        Dim temp1, temp2, temp3, temp4, temp5 As String

        Me.Text = "PV #" & Trim(VPNo) & " - Update"
        errMSg = "PV data view failed"
        strSQL = "select * from tbl_shipping_doc where shipment_no=" & ShipNo & " and ord_no=" & VPNo & " and findoc_type='VP'"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    docno.Text = MyReader.GetString("FinDoc_No")
                    dt1.Text = MyReader.GetString("FinDoc_PrintedDt")
                    temp1 = GetValue("FinDoc_To")
                    remark.Text = GetValue("FinDoc_Note")
                    temp2 = GetValue("FinDoc_AppBy")
                    If temp2 <> "" Then dt2.Text = GetValue("FinDoc_AppDt")
                    dt2.Checked = (temp2 <> "")
                    temp3 = GetValue("FinDoc_FinAppBy")
                    If temp3 <> "" Then dt3.Text = GetValue("FinDoc_FinAppDt")
                    dt3.Checked = (temp3 <> "")
                    crt.Text = MyReader.GetString("FinDoc_CreatedBy")
                    CRTCODE.Text = MyReader.GetString("FinDoc_CreatedBy")
                    crtdt.Text = Microsoft.VisualBasic.Left(MyReader.GetString("FinDoc_CreatedDt"), 10)
                    status.Text = MyReader.GetString("FinDoc_Status")
                    temp4 = MyReader.GetString("FinDoc_ValCur")
                    temp5 = MyReader.GetString("FinDoc_GroupTo")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            expcode.Text = temp1
            appcode.Text = temp2
            fincode.Text = temp3
            CurrCode.Text = temp4

            If (temp5 = "" Or IsDBNull(temp5)) Then
                bankname.Text = AmbilData("concat(bank_name,' ',bank_branch)", "tbm_expedition", "company_code='" & expcode.Text & "'")
                BankBranch = AmbilData("bank_branch", "tbm_expedition", "company_code='" & expcode.Text & "'")
                accname.Text = AmbilData("account_name", "tbm_expedition", "company_code='" & expcode.Text & "'")
                accno.Text = AmbilData("account_no", "tbm_expedition", "company_code='" & expcode.Text & "'")
            Else
                arrBank = Split(temp5, ";")
                bankname.Text = arrBank(0)
                BankBranch = arrBank(1)
                accno.Text = arrBank(2)
                accname.Text = arrBank(3)
            End If

            btnSave.Enabled = f_getenable(status.Text)
            btnReject.Enabled = btnSave.Enabled
            btnPrint.Enabled = btnSave.Enabled
            btnPrintLamp.Enabled = btnSave.Enabled
        End If
        GetCostData()
    End Sub
    Private Sub FrmVP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(400, 100)

        Me.Location = value
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        RegionalSetting = Global.System.Globalization.CultureInfo.CurrentCulture
        ServerDecimal = "."
        ServerSeparator = ","
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function
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
    Private Function CekGrid() As Boolean
        Dim brs, cnt As Integer

        brs = grid.RowCount
        For cnt = 1 To brs
            CekGrid = GridOK(cnt - 1)
            If CekGrid = False Then Exit For
        Next
    End Function
    Private Function GridOK(ByVal baris As Integer) As Boolean
        Dim val As Integer
        Dim str As String

        GridOK = True
        str = grid.Item(0, baris).Value
        If str <> "" Then
            val = grid.Item(3, baris).Value
            If val <= 0 Then
                grid.Focus()
                grid.CurrentCell = grid(3, baris)
                MsgBox("Amount should be > 0")
                GridOK = False
            End If
        End If
    End Function
    Private Function CekDataHeader() As Boolean

        CekDataHeader = True
        If docno.Text = "" Then
            MsgBox("Document No. should be filled")
            docno.Focus()
            CekDataHeader = False
            Exit Function
        End If
        If expname.Text = "" Then
            MsgBox("Expedition not found")
            expname.Focus()
            CekDataHeader = False
            Exit Function
        End If
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim tgl1, tgl2, tgl3, str, DetailCost, SQLStr, RetMess, No, Desc, Amt As String
        Dim hasil As Boolean = False
        Dim cnt, brs As Integer
        Dim DT As New System.Data.DataTable
        Dim num1 As Decimal
        Dim tot As Decimal
        Dim BankStr As String

        If Edit And BLStatus = "Closed" Then
            MsgBox("PV has been closed, can't save data")
            Exit Sub
        End If
        If Baru = True Then                         'klo dah di save grid gak boleh di edit
            If grid.IsCurrentCellDirty Then         'data di grid sudah di input tapi blom tekan enter, data yg diinput tidak terbaca 
                cnt = grid.CurrentCell.ColumnIndex
                brs = grid.CurrentCell.RowIndex
                docno.Focus()                       'pindahkan focus supaya data di grid bisa dibaca
                grid.Focus()
                grid.CurrentCell = grid(cnt, brs)   'kembalikan ke kolom awal
            End If
            If DataError = False Then
                If CekGrid() = False Then Exit Sub
            End If
            If DataError = True Then Exit Sub
        End If
        If CekDataHeader() = False Then Exit Sub

        DetailCost = ""
        tgl1 = Format(dt1.Value, "yyyy-MM-dd")
        tgl2 = Format(dt2.Value, "yyyy-MM-dd")
        tgl3 = Format(dt3.Value, "yyyy-MM-dd")
        tot = GetNum2(total.Text)
        BankStr = bankname.Text & ";" & BankBranch & ";" & accno.Text & ";" & accname.Text

        If Baru Then
            Try
                DT = grid.DataSource
                brs = DT.Rows.Count
            Catch ex As Exception
                brs = 0
            End Try
            If brs = 0 Then
                grid.Focus()
                MsgBox("Cost Data should be filled")
                Exit Sub
            End If
            For cnt = 1 To brs
                No = DT.Rows(cnt - 1).Item(0).ToString
                No = Microsoft.VisualBasic.Mid(No & "     ", 1, 5)
                Desc = DT.Rows(cnt - 1).Item(1).ToString
                Desc = Microsoft.VisualBasic.Mid(Desc & "                                        ", 1, 40)
                num1 = CDec(DT.Rows(cnt - 1).Item(2).ToString)
                str = GetNum2(num1)
                Amt = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                If No <> "" Then DetailCost = DetailCost & No & Desc & Amt & ";"
            Next
        End If

        If Baru Then
            SQLStr = "Run Stored Procedure SaveVP (Save" & "," & ShipNo & "," & "0"
        Else
            SQLStr = "Run Stored Procedure SaveVP (Update" & "," & ShipNo & "," & VPNo
        End If
        SQLStr = SQLStr & "," & docno.Text & "," & tgl1 & "," & expcode.Text & "," & BankStr & "," & remark.Text & "," & appcode.Text & "," & If(dt2.Checked, tgl2, "") _
                 & "," & fincode.Text & "," & If(dt3.Checked, tgl3, "") & "," & tot & "," & CurrCode.Text & "," & If(appcode.Text = "", "Open", "Approved") & "," & UserData.UserCT & ")"
        If Baru Then SQLStr = SQLStr & "," & DetailCost
        With MyComm
            .CommandText = "SaveVP"
            .CommandType = CommandType.StoredProcedure

            With .Parameters
                .Clear()
                .AddWithValue("jenis", If(Baru, "Save", "Update"))
                .AddWithValue("ShipNo", ShipNo)
                .AddWithValue("UpdateOrdNo", If(Baru, 0, ShipOrdNo))
                .AddWithValue("DocNo", docno.Text)
                .AddWithValue("PrintDt", tgl1)
                .AddWithValue("DocTo", expcode.Text)
                .AddWithValue("DocGroupTo", BankStr)
                .AddWithValue("DocNote", remark.Text)
                .AddWithValue("AppBy", appcode.Text)
                .AddWithValue("AppDt", If(dt2.Checked, tgl2, ""))
                .AddWithValue("FinBy", fincode.Text)
                .AddWithValue("FinDt", If(dt3.Checked, tgl3, ""))
                .AddWithValue("FinTotal", CDec(total.Text))
                .AddWithValue("FinCurr", CurrCode.Text)
                .AddWithValue("stat", If(appcode.Text = "", "Open", "Approved"))
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("CostDetail", DetailCost)
                .AddWithValue("AuditStr", SQLStr)
                .AddWithValue("Hasil", hasil)
                .AddWithValue("Message", RetMess)
            End With
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
                RetMess = .Parameters("message").Value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End With

        If hasil = True Then
            f_msgbox_successful(btnSave.Text & " PV")
            Me.Close()
            Me.Dispose()
        Else
            MsgBox(btnSave.Text & " PV failed'")
        End If
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, SQLstr As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If Edit And status.Text = "Rejected" Then
            MsgBox("Payment Voucher has been rejected")
            Exit Sub
        End If
        If Edit And BLStatus = "Closed" Then
            MsgBox("PV has been closed, can't reject")
            Exit Sub
        End If
        msg = "Reject Payment Voucher #" & VPNo & " of BL " & BLNo & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            errMSg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_shipping_doc SET findoc_status='Rejected' " & _
                     "where shipment_no=" & ShipNo & " and ord_no=" & VPNo & " and findoc_type='VP'"

            errMSg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_shipping_doc SET findoc_status='Rejected' " & _
                     "where shipment_no=" & ShipNo & " and ord_no=" & VPNo & " and findoc_type='VP'"

            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", SQLstr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful("Reject")
                Me.Close()
                Me.Dispose()
            Else
                MsgBox("Reject failed...", MsgBoxStyle.Information, "Reject User data")
            End If
        End If
    End Sub

    Function GetServerDate() As Date
        Dim temp As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim strSQL, strSQLF As String

        strSQL = "select company_code CompanyCode, company_name CompanyName, Address, Phone, Fax from tbm_expedition where ppjk_stat = 1 "
        strSQLF = strSQL & "and company_code like 'FilterData1%' and city_code like 'FilterData2%'"
        PilihanDlg.Text = "Select Expedition"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "City Code"
        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strSQLF
        PilihanDlg.Tables = "tbm_expedition"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then expcode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'VP-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'VP-A'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            appcode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            dt2.Checked = True
        End If
    End Sub

    Private Sub expcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles expcode.TextChanged
        expname.Text = AmbilData("COMPANY_NAME", "tbm_expedition", "company_code='" & expcode.Text & "'")

        '--- bank di pilih manual untuk mendukung 1 Expedition bisa punya lebih dari 1 account bank ---
        'bankname.Text = AmbilData("concat(bank_name,' ',bank_branch)", "tbm_expedition", "company_code='" & expcode.Text & "'")
        'accname.Text = AmbilData("account_name", "tbm_expedition", "company_code='" & expcode.Text & "'")
        'accno.Text = AmbilData("account_no", "tbm_expedition", "company_code='" & expcode.Text & "'")
        bankname.Text = ""
        BankBranch = ""
        accname.Text = ""
        accno.Text = ""
    End Sub

    Private Sub appcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles appcode.TextChanged
        appby.Text = AmbilData("NAME", "tbm_users", "user_ct=" & appcode.Text)
    End Sub
    Private Sub UpdateCombo()
        Dim brs, cnt As Integer
        Dim str As String

        brs = grid.RowCount - 1
        For cnt = 0 To brs
            str = grid.Item(0, cnt).Value
            grid.Rows(cnt).Cells(1).Value = str
        Next
    End Sub

    Private Sub grid_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid.CellEndEdit
        Dim brs As Integer
        Dim str As String
        Dim tot As Decimal

        Try
            brs = grid.CurrentCell.RowIndex
            grid.Item(0, brs).Value = grid.Item(1, brs).Value
        Catch ex As Exception
            grid.Item(1, brs).Value = ""
        End Try

        Try
            brs = grid.CurrentCell.RowIndex
            str = grid.Item(2, brs).Value
        Catch ex As Exception
            grid.Item(2, brs).Value = ""
        End Try

        Try
            brs = grid.CurrentCell.RowIndex
            str = grid.Item(3, brs).Value
        Catch ex As Exception
            grid.Item(3, brs).Value = 0
        End Try

        tot = GetTotal()
        total.Text = FormatNumber(tot, 2, , , TriState.True)
    End Sub

    Private Sub grid_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.CurrentCellDirtyStateChanged
        If DataError = True And grid.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid.DataError
        MsgBox("Invalid amount, input numeric value")
        DataError = True
    End Sub

    Private Sub grid_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grid.RowsAdded
        Dim brs As Integer

        If Edit Then Exit Sub
        Try
            brs = grid.CurrentCell.RowIndex
            grid.Item(0, brs).Value = ""
            grid.Item(2, brs).Value = ""
            grid.Item(3, brs).Value = 0
        Catch ex As Exception
        End Try
    End Sub
    Private Function GetTotal() As Decimal
        Dim DT As System.Data.DataTable
        Dim brs, cnt As Integer
        Dim tot, temp As Decimal

        DT = grid.DataSource
        brs = DT.Rows.Count
        tot = 0

        For cnt = 0 To brs
            temp = grid.Rows(cnt).Cells(3).Value
            tot = tot + temp
        Next
        GetTotal = tot
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            fincode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            dt3.Checked = True
        End If
    End Sub

    Private Sub fincode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fincode.TextChanged
        finby.Text = AmbilData("NAME", "tbm_users", "user_ct=" & fincode.Text)
    End Sub

    Private Sub FrmVP_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim tot As Decimal

        GetCostData()
        If Edit Then
            UpdateCombo()
            tot = GetTotal()
            total.Text = FormatNumber(tot, 2, , , TriState.True)
            grid.ReadOnly = True
            grid.AllowUserToAddRows = False
        End If
    End Sub

    Private Sub btnSearchCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCurrency.Click
        PilihanDlg.Text = "Select Currency Code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        PilihanDlg.SQLGrid = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency"
        PilihanDlg.SQLFilter = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' AND currency_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then CurrCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub CurrCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrCode.TextChanged
        currname.Text = AmbilData("CURRENCY_NAME", "tbm_currency", "currency_code='" & CurrCode.Text & "'")
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        'v_pono = txtpono.ToString
        If Len(CStr(VPNo)) = 1 Then
            v_num = " " & VPNo
        Else
            v_num = VPNo.ToString
        End If
        ViewerFrm.Tag = "VPPP" & v_num & ShipNo

        ViewerFrm.ShowDialog()
    End Sub
    Private Sub btnPrintLamp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintLamp.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        'v_pono = txtpono.ToString
        If Len(CStr(VPNo)) = 1 Then
            v_num = " " & VPNo
        Else
            v_num = VPNo.ToString
        End If
        ViewerFrm.Tag = "VPLL" & v_num & ShipNo

        ViewerFrm.ShowDialog()
    End Sub
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub btnBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBank.Click
        Dim strSQL, strSQLF As String

        If expcode.Text = "" Then
            MsgBox("Please select Expedition first! ", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        Else
            PilihanDlg.LblKey1.Text = "Bank Name"
            PilihanDlg.LblKey2.Text = "Bank Branch"
            strSQL = "SELECT * FROM " & _
                     "(     SELECT bank_name Bank, bank_branch Branch, account_no AccountNo, account_name AccountName FROM tbm_expedition WHERE company_code='" & expcode.Text & "' " & _
                     "UNION SELECT bank_name2 Bank, bank_branch2 Branch, account_no2 AccountNo, account_name2 AccountName FROM tbm_expedition WHERE company_code='" & expcode.Text & "' " & _
                     "UNION SELECT bank_name3 Bank, bank_branch3 Branch, account_no3 AccountNo, account_name3 AccountName FROM tbm_expedition WHERE company_code='" & expcode.Text & "') t1 "
            strSQLF = strSQL & " "
            PilihanDlg.Text = "Select Account " & expname.Text

            PilihanDlg.SQLGrid = strSQL
            PilihanDlg.SQLFilter = strSQL & " WHERE t1.Bank LIKE '%FilterData1%' and t1.Branch LIKE '%FilterData2%'"
            PilihanDlg.Tables = "tbm_expedition"

            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                bankname.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                BankBranch = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
                accname.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString
                accno.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            End If
        End If
    End Sub
End Class