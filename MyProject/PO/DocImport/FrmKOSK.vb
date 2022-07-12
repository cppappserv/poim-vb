'Title                         : Transaksi KO / SK
'Form                          : FrmKOSK
'Created By                    : Hanny
'Created Date                  : 24 NOV 2008
'Table Used                    : tbm_bank, tbm_bank_Reference, 
'                                tbl_docimpr, tbl_SHIPPING_doc
'Stored Procedure Used (MySQL) : SaveKO / SaveSK
Public Class FrmKOSK
    Dim Ship, NONum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE, module_code2 As String
    Dim MODULE_NAME As String
    Dim ModCode As String
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String


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
    Sub New(ByVal ShipNo As String, ByVal Currency As String, ByVal CurrName As String, ByVal TotalAmt As String, ByVal kurs As String, ByVal NO As String, ByVal modul As String)
        If modul = "KO" Then
            MODULE_CODE = "KO"
            module_code2 = "Surat Kuasa Pengambilan DO"
        ElseIf modul = "SK" Then
            MODULE_CODE = "SK"
            module_code2 = "Surat Kuasa Karantina"
        ElseIf modul = "JC" Then
            MODULE_CODE = "JC"
            module_code2 = "Surat Kuasa Penarikan Jaminan Container"
        ElseIf modul = "BC" Then
            MODULE_CODE = "BC"
            module_code2 = "Surat Kuasa Bea Cukai"
        ElseIf modul = "PC" Then
            MODULE_CODE = "PC"
            module_code2 = "Surat Kuasa Peminjaman Container"
        ElseIf modul = "BS" Then
            MODULE_CODE = "BS"
            module_code2 = "Surat Pemberitahuan Beda Shipper"
        ElseIf modul = "SZ" Then
            MODULE_CODE = "SZ"
            module_code2 = "Surat Permohonan Izin"
        ElseIf modul = "SB" Then
            MODULE_CODE = "SB"
            module_code2 = "Surat Pembatalan BCF 1.5"
        ElseIf modul = "NP" Then
            MODULE_CODE = "NP"
            module_code2 = "Deklarasi Nilai Pabean"
        End If

        MODULE_NAME = module_code2
        InitializeComponent()
        txtCity_Code.Text = ""
        txtExp.Text = ""
        Bank.Text = ""
        AccountNo.Text = ""
        BankBranch.Text = ""
        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        CTApp.Text = ""
        AppDt.Value = GetServerDate()
        AppDt.Checked = False

        BCDt.Value = GetServerDate()
        BCDt.Checked = False

        Ship = ShipNo

        If Trim(NO) <> "" Then
            Call DisplayData(NO)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("KO-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        Else
            btnReject.Enabled = False
            btnPrint.Enabled = False
            NONum = "0"
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            crtdt.Text = GetServerDate.ToString
            Me.Text = module_code2 & " - New"

            txtExp.Text = AmbilData("emkl_code", "TBL_SHIPPING", "shipment_no='" & Ship & "'")
        End If
    End Sub
    Private Sub btnSearchExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchExp.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"

        If MODULE_CODE = "BS" Then
            PilihanDlg.SQLGrid = "SELECT produsen_code AS ProdusenCompanyCode, produsen_name AS ProduseCompanyName, address AS Address,  rendring_no AS RendringNo FROM tbm_produsen " & _
                                 "WHERE produsen_code in (SELECT DISTINCT produsen_code FROM tbl_shipping_detail t1, tbl_po t2 " & _
                                 "                        WHERE t1.po_no=t2.po_no AND t1.shipment_no='" & Ship & "' AND produsen_code<>'00000') order by produsen_code "

            PilihanDlg.SQLFilter = "SELECT produsen_code AS ProdusenCompanyCode, produsen_name AS ProdusenCompanyName, address AS Address,  rendring_no AS RendringNo FROM tbm_produsen " & _
                                   "WHERE produsen_code LIKE 'FilterData1%' and produsen_name LIKE 'FilterData2%' " & _
                                   "AND produsen_code in (SELECT DISTINCT produsen_code FROM tbl_shipping_detail t1, tbl_po t2 " & _
                                   "                      WHERE t1.po_no=t2.po_no AND t1.shipment_no='" & Ship & "' AND produsen_code<>'00000') order by produsen_code "

            PilihanDlg.Tables = "tbm_produsen"
        Else
            PilihanDlg.SQLGrid = "SELECT company_code as ExpeditionCompanyCode, company_name as ExpeditionCompanyName, Title as Title, AUTHORIZE_PERSON as AuthorizedPerson FROM tbm_expedition order by company_code "
            PilihanDlg.SQLFilter = "SELECT company_code as ExpeditionCompanyCode, company_name as ExpeditionCompanyName, Title as Title, AUTHORIZE_PERSON as AuthorizedPerson FROM tbm_expedition " & _
                                   "WHERE PPJK_STAT = '1' and company_code LIKE 'FilterData1%' and company_name LIKE 'FilterData2%' order by company_code "

            PilihanDlg.Tables = "tbm_expedition"
        End If

        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtExp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblExp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            txtTitle.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            txtAuthorized.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString

            Bank.Text = ""
            AccountNo.Text = ""
            BankBranch.Text = ""
            AccountName.Text = ""
        End If

    End Sub

    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city Where country_code='ID'"
        PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' and city_name LIKE 'FilterData2%' and country_code='ID' "
        PilihanDlg.Tables = "tbm_city"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCity_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If

    End Sub
    Private Sub DisplayData(ByVal NO As String)
        Dim pjg As Integer
        Dim strSQL, errMSg As String
        Dim TEMP As String = ""
        Dim tempExp As String = ""
        Dim tempBank As String = ""
        Dim tempArr() As String

        'pjg = Len(RTrim(NO)) - 7
        pjg = 3
        num = CInt(Mid(NO, 8, pjg))
        NONum = num.ToString
        strSQL = "SELECT * FROM TBL_SHIPPING_DOC " & _
                 "WHERE SHIPMENT_NO = '" & Ship & "' AND " & _
                 "ORD_NO = '" & NONum & "' AND FINDOC_TYPE = '" & MODULE_CODE & "' "

        'and (FINDOC_GROUPTO = 'EMKL' OR FINDOC_GROUPTO = 'CUSTOM')"

        DTPrinted.Enabled = False
        txtCity_Code.ReadOnly = True
        txtExp.ReadOnly = True
        Bank.ReadOnly = True
        txtAuthorized.ReadOnly = True
        crtdt.ReadOnly = True
        btnSearchCity.Visible = False
        btnSearchExp.Visible = False
        Button3.Visible = False

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    TEMP = MyReader.GetString("FINDOC_PRINTEDON")
                    DTPrinted.Text = MyReader.GetString("FINDOC_PRINTEDDT")
                    tempExp = MyReader.GetString("FINDOC_TO")
                    CTCrt.Text = MyReader.GetString("FINDOC_CREATEDBY")
                    crtdt.Text = MyReader.GetString("FINDOC_CREATEDDT")

                    Try
                        txtBCNo.Text = MyReader.GetString("FINDOC_NO")
                    Catch ex As Exception
                        txtBCNo.Text = ""
                        BCDt.Checked = False
                    End Try

                    If txtBCNo.Text <> "" Then
                        BCDt.Value = MyReader.GetString("FINDOC_FINAPPDT")
                        BCDt.Checked = True
                    End If

                    Try
                        txtPos.Text = MyReader.GetString("FINDOC_REFF")
                    Catch ex As Exception
                        txtPos.Text = ""
                    End Try

                    Try
                        tempBank = MyReader.GetString("FINDOC_REFF")
                    Catch ex As Exception
                        tempBank = ""
                    End Try

                    Try
                        txtLap.Text = MyReader.GetString("FINDOC_NOTE")
                    Catch ex As Exception
                        txtLap.Text = ""
                    End Try

                    Try
                        refund2.Text = MyReader.GetString("FINDOC_NOTE")
                    Catch ex As Exception
                        refund2.Text = ""
                    End Try

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
                    txtExp.ReadOnly = f_getread(Status.Text)
                    Bank.ReadOnly = f_getread(Status.Text)
                    txtAuthorized.ReadOnly = f_getread(Status.Text)
                    crtdt.ReadOnly = f_getread(Status.Text)
                    btnSearchCity.Visible = f_getenable(Status.Text)
                    btnSearchExp.Visible = f_getenable(Status.Text)
                    Button3.Visible = f_getenable(Status.Text)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            btnSave.Text = "Update"
            'posisi di akhir supaya tidak dead lock
            txtCity_Code.Text = temp
            txtExp.Text = tempExp

            'untuk modul Jaminan Container
            If MODULE_CODE = "JC" Then
                If tempBank = "" Then
                    Bank.Text = tempBank
                    BankBranch.Text = tempBank
                    AccountNo.Text = tempBank
                    AccountName.Text = tempBank
                Else
                    tempArr = Split(tempBank, ";")
                    Try
                        Bank.Text = tempArr(0)
                        BankBranch.Text = tempArr(1)
                        AccountNo.Text = tempArr(2)
                        AccountName.Text = tempArr(3)
                    Catch ex As Exception
                    End Try
                End If
            End If
            '-------------------------------

            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            Me.Text = module_code2 & " # " & Trim(NONum) & " - Update"
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
    Private Sub FrmKOSK_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 380)

        Me.Location = value
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","
        print1.Visible = (MODULE_CODE = "KO")
        print2.Visible = (MODULE_CODE = "KO")

        'hide optional item
        Label6.Visible = (MODULE_CODE = "JC")
        Label7.Visible = (MODULE_CODE = "JC")
        Label16.Visible = (MODULE_CODE = "JC")
        Label17.Visible = (MODULE_CODE = "JC")
        Label18.Visible = (MODULE_CODE = "JC")
        BankBranch.Visible = (MODULE_CODE = "JC")
        Bank.Visible = (MODULE_CODE = "JC")
        AccountNo.Visible = (MODULE_CODE = "JC")
        AccountName.Visible = (MODULE_CODE = "JC")
        btnSearch.Visible = (MODULE_CODE = "JC")
        refund2.Visible = (MODULE_CODE = "JC")

        Label8.Visible = (MODULE_CODE = "SB")
        Label9.Visible = (MODULE_CODE = "SB")
        Label11.Visible = (MODULE_CODE = "SB")
        txtBCNo.Visible = (MODULE_CODE = "SB")
        BCDt.Visible = (MODULE_CODE = "SB")
        txtPos.Visible = (MODULE_CODE = "SB")
        txtLap.Visible = (MODULE_CODE = "SB")

        lblDG.Visible = (MODULE_CODE = "SZ") Or (MODULE_CODE = "SB")
        cbDG.Visible = (MODULE_CODE = "SZ") Or (MODULE_CODE = "SB")

        If (MODULE_CODE = "SZ" Or MODULE_CODE = "SB") Then fillcbDG()

        If MODULE_CODE = "PC" Or MODULE_CODE = "SZ" Or MODULE_CODE = "SB" Or MODULE_CODE = "NP" Then
            Label3.Visible = False
            txtExp.Visible = False
            btnSearchExp.Visible = False
            Label4.Visible = False
            txtAuthorized.Visible = False
            Label2.Visible = False
            txtTitle.Visible = False
            lblExp.Visible = False
        ElseIf MODULE_CODE = "BS" Then

            Label3.Text = "Produsen"
            Label4.Text = "                                                                          (* as shipper)"
            txtAuthorized.Visible = False
            Label2.Visible = False
            txtTitle.Visible = False
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

        ModCode = MODULE_CODE & "-A"
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "'"
        PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "'" & _
                               "and tu.name LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If sender Is "App" Then
                approvedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
            Call Name_Data(sender)
        End If
    End Sub
    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String

        ModCode = MODULE_CODE & "-A"
        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' and tu.name='" & approvedby.Text & "'"
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

        ModCode = MODULE_CODE & "-A"
        If approvedby.Text <> "" Then
            STRsql = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' and tu.name='" & approvedby.Text & "'"

            If FM02_MaterialGroup.DataOK(STRsql) = True Then
                MsgBox("Name does not exist! ", MsgBoxStyle.Critical, "Warning")
                CekData = False
                approvedby.Focus()
                Exit Function
            End If
        End If
    End Function
    Private Function GetNum(ByVal strnum As String) As Decimal
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum = CDec(temp)
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        Dim AppDt1, SQLStr, DTPrinted1, DocDt1 As String
        Dim num As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""
        Dim DocTo As String
        Dim DocRef As String

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDt, "Approved Date") Then Exit Sub

        If cbDG.Visible = True Then
            If cbDG.Text = "" Then
                MsgBox("Select Document Group first! ", MsgBoxStyle.Critical, "Warning")
                cbDG.Focus()
                Exit Sub
            End If
        End If

        Try
            AppDt1 = Format(AppDt.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")
            DocDt1 = Format(BCDt.Value, "yyyy-MM-dd")

            keyprocess = If(btnSave.Text = "Save", "Save", "Updt")
            SQLStr = "Run Stored Procedure SaveKOSK (Save," & keyprocess & "," & Ship & "," & txtCity_Code.Text & "," & DTPrinted1

            MyComm.CommandText = "SaveKOSK"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)

            If txtBCNo.Visible Then
                MyComm.Parameters.AddWithValue("Nomor", txtBCNo.Text)
            Else
                MyComm.Parameters.AddWithValue("Nomor", "")
            End If

            If MODULE_CODE = "JC" Then
                DocRef = Bank.Text & ";" & BankBranch.Text & ";" & AccountNo.Text & ";" & AccountName.Text
                MyComm.Parameters.AddWithValue("Reff", DocRef)
            Else
                If txtPos.Visible Then
                    MyComm.Parameters.AddWithValue("Reff", txtPos.Text)
                Else
                    MyComm.Parameters.AddWithValue("Reff", "")
                End If
            End If
            MyComm.Parameters.AddWithValue("CityCode", txtCity_Code.Text)
            MyComm.Parameters.AddWithValue("DTPrint", DTPrinted1)
            MyComm.Parameters.AddWithValue("Expd", If(MODULE_CODE = "PC" Or MODULE_CODE = "SZ" Or MODULE_CODE = "SB", "", txtExp.Text))
            MyComm.Parameters.AddWithValue("DocTo", If(MODULE_CODE = "BS" Or MODULE_CODE = "NP", "CUSTOM", If(MODULE_CODE = "SZ" Or MODULE_CODE = "SB", cbDG.Text, "EMKL")))
            SQLStr = SQLStr & "," & If(MODULE_CODE = "PC" Or MODULE_CODE = "SZ" Or MODULE_CODE = "SB", "NULL", txtExp.Text)
            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
                SQLStr = SQLStr & "," & "NULL" & "," & "NULL" & "," & "Open"
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDt1)
                MyComm.Parameters.AddWithValue("Stat", "Approved")
                SQLStr = SQLStr & "," & CTApp.Text & "," & AppDt1 & "," & "Approved"
            End If

            If txtBCNo.Visible Then
                MyComm.Parameters.AddWithValue("FinBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("FinDt", DocDt1)
            Else
                If CTFin.Text = "" Then
                    MyComm.Parameters.AddWithValue("FinBy", DBNull.Value)
                    MyComm.Parameters.AddWithValue("FinDt", DBNull.Value)
                Else
                    MyComm.Parameters.AddWithValue("FinBy", CTFin.Text)
                    MyComm.Parameters.AddWithValue("FinDt", findt)
                End If
            End If

            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("MODUL", MODULE_CODE)
            SQLStr = SQLStr & "," & UserData.UserCT & "," & MODULE_CODE

            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
                SQLStr = SQLStr & "," & ""
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", NONum)
                SQLStr = SQLStr & "," & NONum
            End If
            If refund2.Visible Then
                MyComm.Parameters.AddWithValue("Refund", DBNull.Value)
                MyComm.Parameters.AddWithValue("Currency", DBNull.Value)
                MyComm.Parameters.AddWithValue("Note", refund2.Text)
                SQLStr = SQLStr & "," & "NULL" & "," & "NULL" & "," & refund2.Text
            Else
                MyComm.Parameters.AddWithValue("Refund", DBNull.Value)
                MyComm.Parameters.AddWithValue("Currency", DBNull.Value)
                If MODULE_CODE <> "SB" Then
                    MyComm.Parameters.AddWithValue("Note", DBNull.Value)
                    SQLStr = SQLStr & "," & "NULL" & "," & "NULL" & "," & "NULL"
                Else
                    MyComm.Parameters.AddWithValue("Note", txtLap.Text)
                    SQLStr = SQLStr & "," & "NULL" & "," & "NULL" & "," & txtLap.Text
                End If
            End If
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " " & module_code2)
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " " & module_code2 & " failed'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject " & MODULE_NAME & "#" & NONum & " of " & Ship & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_shipping_doc " & _
                     "SET FINDOC_STATUS='Rejected'" & _
                     " where SHIPMENT_NO='" & Ship & "' " & _
                     " and ord_no=" & NONum & "" & _
                     " AND FINDOC_TYPE = '" & MODULE_CODE & "'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
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
        Dim v_num, sel, v_dg As String

        If cbDG.Visible = True Then
            If cbDG.Text = "" Then
                MsgBox("Select Document Group first! ", MsgBoxStyle.Critical, "Warning")
                cbDG.Focus()
                Exit Sub
            Else
                v_dg = cbDG.Text
            End If
        End If

        If MODULE_CODE = "KO" Then
            If print1.Checked Then
                sel = 1
            Else
                sel = 2
            End If
        Else
            sel = ""
        End If
        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num.ToString
        End If
        If MODULE_CODE = "KO" Then
            ViewerFrm.Tag = "KOOO" & v_num & sel & Ship
        ElseIf MODULE_CODE = "SK" Then
            ViewerFrm.Tag = "SKKK" & v_num & sel & Ship
        ElseIf MODULE_CODE = "JC" Then
            ViewerFrm.Tag = "JCCC" & v_num & sel & Ship
        ElseIf MODULE_CODE = "BC" Then
            ViewerFrm.Tag = "BCCC" & v_num & sel & Ship
        ElseIf MODULE_CODE = "PC" Then
            ViewerFrm.Tag = "PCCC" & v_num & sel & Ship
        ElseIf MODULE_CODE = "BS" Then
            ViewerFrm.Tag = "BSCC" & v_num & sel & Ship
        ElseIf MODULE_CODE = "SZ" Then
            ViewerFrm.Tag = "SZSZ" & v_num & sel & Ship & ";" & v_dg
        ElseIf MODULE_CODE = "SB" Then
            ViewerFrm.Tag = "SBSB" & v_num & sel & Ship & ";" & v_dg
        ElseIf MODULE_CODE = "NP" Then
            ViewerFrm.Tag = "DNPC" & v_num & sel & Ship
        End If

        ViewerFrm.ShowDialog()

    End Sub


    Private Sub txtExp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExp.TextChanged
        If txtExp.Text = "" Then
            lblExp.Text = ""
            txtAuthorized.Text = ""
            txtTitle.Text = ""
        ElseIf txtExp.Text <> "" Then
            If MODULE_CODE = "BS" Then
                lblExp.Text = AmbilData("PRODUSEN_NAME", "TBM_PRODUSEN", "PRODUSEN_CODE='" & txtExp.Text & "'")
            Else
                lblExp.Text = AmbilData("COMPANY_NAME", "TBM_EXPEDITION", "COMPANY_CODE='" & txtExp.Text & "'")
            End If
            txtAuthorized.Text = AmbilData("AUTHORIZE_PERSON", "TBM_EXPEDITION", "COMPANY_CODE='" & txtExp.Text & "'")
            txtTitle.Text = AmbilData("TITLE", "TBM_EXPEDITION", "COMPANY_CODE='" & txtExp.Text & "'")
        End If
    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        lblCityName.Text = AmbilData("CITY_NAME", "TBM_CITY", "CITY_CODE='" & txtCity_Code.Text & "'")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strsql As String

        If txtExp.Text = "" Then
            MsgBox("Please select Expedition first! ", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        Else
            strsql = "SELECT bank_name Bank, bank_branch Branch, account_no AccountNo, account_name AccountName " & _
                 "FROM tbm_expedition WHERE company_code='" & txtExp.Text & "' AND bank_name <> '' " & _
                 "UNION " & _
                 "SELECT bank_name2 Bank, bank_branch2 Branch, account_no2 AccountNo, account_name2 AccountName " & _
                 "FROM tbm_expedition WHERE company_code='" & txtExp.Text & "' AND bank_name2 <> '' " & _
                 "UNION " & _
                 "SELECT bank_name3 Bank, bank_branch3 Branch, account_no3 AccountNo, account_name3 AccountName " & _
                 "FROM tbm_expedition WHERE company_code='" & txtExp.Text & "' AND bank_name3 <> '' "

            PilihanDlg.Text = "Select Account Bank"
            PilihanDlg.LblKey1.Text = "Bank"
            PilihanDlg.LblKey2.Text = "Account"
            PilihanDlg.SQLGrid = strsql

            PilihanDlg.SQLFilter = "select t1.* from (" & strsql & ") t1 " & _
                                   "WHERE t1.Bank LIKE 'FilterData1%' " & _
                                   " and t1.AccountNo  LIKE 'FilterData2%' "
            PilihanDlg.Tables = "tbm_expedition"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Bank.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                BankBranch.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
                AccountNo.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
                AccountName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString

            End If
        End If

    End Sub

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function


    Private Sub fillcbDG()
        Dim strSQL, errMSg, temp As String

        strSQL = "SELECT FINDOC_GROUPTO group_code FROM TBL_SHIPPING_DOC " & _
                 "WHERE SHIPMENT_NO = '" & Ship & "' AND ORD_NO = '" & NONum & "' AND FINDOC_TYPE = '" & MODULE_CODE & "' " & _
                 "UNION " & _
                 "Select group_code from tbm_cr_template where type_code='" & MODULE_CODE & "' " & _
                 "and group_code not in " & _
                 "  (SELECT FINDOC_GROUPTO FROM TBL_SHIPPING_DOC " & _
                 "   WHERE SHIPMENT_NO = '" & Ship & "' AND FINDOC_TYPE = '" & MODULE_CODE & "' AND FINDOC_STATUS <> 'Rejected')"


        errMSg = "Failed when read template data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        cbDG.Refresh()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = MyReader.GetString("group_code")
                    cbDG.Items.Add(temp)
                    cbDG.SelectedIndex = 0
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub txtBCNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBCNo.TextChanged
        If Trim(txtBCNo.Text) = "" Then
            BCDt.Checked = False
        Else
            BCDt.Checked = True
        End If

    End Sub
End Class