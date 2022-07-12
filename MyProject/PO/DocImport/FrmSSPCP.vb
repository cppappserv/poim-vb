'Title                         : Transaksi SSPCP
'Form                          : FrmSSPCP
'Created By                    : Hanny
'Created Date                  : 24 NOV 2008
'Table Used                    : tbm_bank, tbm_bank_Reference, 
'                                tbl_sspcp
'Stored Procedure Used (MySQL) : SaveSSPCP
Public Class FrmSSPCP
    Dim Ship, NONum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String
    Dim MODULE_NAME As String
    Dim ModCode As String = "SP"
    Dim txtItem1 As String = "Bea Masuk"
    Dim txtItem2 As String = "Bea Masuk Ditanggung Pemerintah atas Hibah (SPM) Nihil"
    Dim txtItem3 As String = "Denda Administrasi Pabean"
    Dim txtItem4 As String = "Penerimaan Pabean lainnya"
    Dim txtItem5 As String = "Cukai Etil Alkohol"
    Dim txtItem6 As String = "Penerimaan Cukai lainnya"
    Dim txtItem7 As String = "Denda Administrasi Cukai"
    Dim txtItem8 As String = "PNBP/Pendapatan DJBC"
    Dim txtItem9 As String = "Pajak Pertambahan Nilai (PPN) Import"
    Dim txtItem10 As String = "Pajak Penjualan Atas Barang Mewah (PPnBM) Import"
    Dim txtItem11 As String = "Pajak Penghasilan Pasal 22 (PPh Pasal 22) Import"




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
        'Dim ttl As Decimal
        'Dim lcno As String



        MODULE_CODE = "SP"
        MODULE_NAME = "SSPCP "


        InitializeComponent()

        txtBC.Text = ""
        txtBank.Text = ""
        approvedby.Text = ""
        CTApp.Text = ""

        AppDt.Value = GetServerDate()
        recDt.Value = GetServerDate()
        AppDt.Checked = False
        recDt.Checked = False
        crtdt.Text = GetServerDate.ToString
        txtThn.Text = Format(GetServerDate(), "yyyy")
        Ship = ShipNo
        'DIno.Text = ""

        'item detail
        TextBox1.Text = txtItem1
        TextBox2.Text = txtItem2
        TextBox3.Text = txtItem3
        TextBox4.Text = txtItem4
        TextBox5.Text = txtItem5
        TextBox6.Text = txtItem6
        TextBox7.Text = txtItem7
        TextBox8.Text = txtItem8
        TextBox9.Text = txtItem9
        TextBox10.Text = txtItem10
        TextBox11.Text = txtItem11


        'Call GetButtonAccess()

        If Trim(NO) <> "" Then
            Call DisplayData(NO)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("SP-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        Else
            btnReject.Enabled = False
            btnPrint.Enabled = False
            NONum = "0"
            txtCompCode.Text = AmbilData("Distinct tp.company_code", "tbl_po as tp inner join tbl_shipping_detail as tsd on tsd.po_no = tp.po_no", "tsd.shipment_no='" & ShipNo & "'")
            If txtCompCode.Text <> "" Then
                txtBank.Text = AmbilData("distinct a.bank_code", "tbm_bank as a inner join tbm_bank_reference as b on a.bank_code=b.bank_code", "b.ref_code ='" & txtCompCode.Text & "'")
            Else
                txtBank.Text = ""
            End If
            crtdt.Text = GetServerDate.ToString
            crt.Text = UserData.UserName
            CTCrt.Text = UserData.UserCT.ToString
            Me.Text = MODULE_NAME & "- New"

            'txtAmount1.Text = "0"
            txtAmount2.Text = "0"
            txtAmount3.Text = "0"
            txtAmount4.Text = "0"
            txtAmount5.Text = "0"
            txtAmount6.Text = "0"
            txtAmount7.Text = "0"
            txtAmount8.Text = "0"
            'txtAmount9.Text = "0"
            txtAmount10.Text = "0"
            'txtAmount11.Text = "0"
            txtAmountT.Text = "0"

            'txtamount1 / bea masuk
            Try
                txtAmount1.Text = CDbl(AmbilData("BEA_MASUK", "tbl_shipping", "SHIPMENT_NO='" & ShipNo & "'"))
                txtAmount1.Text = FormatNumber(txtAmount1.Text, 0)
            Catch ex As Exception
                txtAmount1.Text = FormatNumber(0, 0)
            End Try
            'txtamount9 / vat
            Try
                txtAmount9.Text = CDbl(AmbilData("VAT", "tbl_shipping", "SHIPMENT_NO='" & ShipNo & "'"))
                txtAmount9.Text = FormatNumber(txtAmount9.Text, 0)
            Catch ex As Exception
                txtAmount9.Text = FormatNumber(0, 0)
            End Try
            'PPH22
            Try
                txtAmount11.Text = CDbl(AmbilData("PPH21", "tbl_shipping", "SHIPMENT_NO='" & ShipNo & "'"))
                txtAmount11.Text = FormatNumber(txtAmount11.Text, 0)
            Catch ex As Exception
                txtAmount11.Text = FormatNumber(0, 0)
            End Try
            'total
            txtAmountT.Text = CDbl(txtAmount1.Text) + CDbl(txtAmount9.Text) + +CDbl(txtAmount11.Text)
            txtAmountT.Text = FormatNumber(txtAmountT.Text, 0)
        End If
    End Sub
    Private Sub btnSearchBank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchBank.Click
        Dim strSQL As String

        PilihanDlg.Text = "Select Bank Code"
        PilihanDlg.LblKey1.Text = "Bank Code"
        'PilihanDlg.SQLGrid = "SELECT bank_code as BankCode, bank_name as BankName FROM tbm_bank"
        strSQL = "SELECT TB.BANK_CODE AS BankCode,TB.BANK_NAME AS BankName, " & _
                 "TB.ACCOUNT_NO AS AccountNo, TB.CURRENCY_CODE AS Currency, " & _
                 "TB.MARGIN_DEPOSIT AS MarginDeposit, TB.COMMISION AS Commision, TB.POSTAGE_CHARGES AS PostageCharges " & _
                 "FROM TBM_BANK AS TB, TBM_BANK_REFERENCE AS TBR " & _
                 "WHERE TB.BANK_CODE = TBR.BANK_CODE And TBR.REF_CODE='" & txtCompCode.Text & "' " & _
                 "ORDER BY TB.BANK_CODE"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = "SELECT bank_code as BankCode, bank_name as BankName FROM tbm_bank " & _
                               "WHERE bank_code LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_bank"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBank.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblBank.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub
    Private Sub DisplayData(ByVal NO As String)
        Dim pjg As Integer
        Dim strSQL, errMSg, tempPeriode As String
        Dim tempBC As String = ""
        Dim tempBank As String = ""
        Dim tempComp As String = ""


        pjg = Len(RTrim(NO)) - 4
        num = CInt(Mid(NO, 8, pjg))
        NONum = num.ToString
        strSQL = "SELECT * FROM tbl_sspcp " & _
                 "WHERE SHIPMENT_NO = '" & Ship & "' AND " & _
                 "ORD_NO = '" & NONum & "' "
        'strSQL = " select a.*,b.* from tbl_budget as a" & _
        '         " inner join tbm_bank as b on a.bank_code=b.bank_code " & _
        '         " where a.po_no = '" & PO & "' and a.ord_no='" & num & "' " & _
        '         " and a.type_code = 'BP' "
        txtBC.ReadOnly = True
        txtBank.ReadOnly = True


        'crt.ReadOnly = True
        approvedby.ReadOnly = True
        recby.ReadOnly = True

        'crtdt.ReadOnly = True
        AppDt.Enabled = False
        recDt.Enabled = False

        btnSearchPort.Visible = False
        btnSearchBank.Visible = False
        Button3.Visible = False

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    tempPeriode = MyReader.GetString("SSPCP_PERIOD")
                    If tempPeriode <> "" Then
                        txtBln.SelectedIndex = CInt(Mid(tempPeriode, 5, 2)) - 1
                    End If
                    tempBC = MyReader.GetString("SSPCP_OFFICE_CODE")
                    tempBank = MyReader.GetString("bank_code")
                    tempComp = MyReader.GetString("COMPANY_CODE")
                    ctcrt.Text = MyReader.GetString("CREATEDBY")
                    crtdt.Text = MyReader.GetString("CREATEDDT")
                    'CTApp.Text = MyReader.GetString("APPROVEDBY")
                    'AppDt.Text = MyReader.GetString("APPROVEDDT")
                    'ctrec.Text = MyReader.GetString("RECEIVEDBY")
                    'recDt.Text = MyReader.GetString("RECEIVEDDT")
                    Try
                        CTApp.Text = MyReader.GetString("APPROVEDBY")
                    Catch ex As Exception
                        CTApp.Text = ""
                        AppDt.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        AppDt.Value = MyReader.GetString("APPROVEDDT")
                        AppDt.Checked = True
                    End If
                    Try
                        ctrec.Text = MyReader.GetString("RECEIVEDBY")
                    Catch ex As Exception
                        ctrec.Text = ""
                        recDt.Checked = False
                    End Try
                    If ctrec.Text <> "" Then
                        recDt.Value = MyReader.GetString("RECEIVEDDT")
                        recDt.Checked = True
                    End If


                    Status.Text = MyReader.GetString("STATUS")

                    'detail
                    txtAmount1.Text = FormatNumber(MyReader.GetString("BEA_MASUK"), 0)
                    txtAmount2.Text = FormatNumber(MyReader.GetString("SPM"), 0)
                    txtAmount3.Text = FormatNumber(MyReader.GetString("DENDA_PABEAN"), 0)
                    txtAmount4.Text = FormatNumber(MyReader.GetString("PEN_PABEAN_LAIN"), 0)
                    txtAmount5.Text = FormatNumber(MyReader.GetString("CUKAI_ETIL"), 0)
                    txtAmount6.Text = FormatNumber(MyReader.GetString("DENDA_CUKAI"), 0)
                    txtAmount7.Text = FormatNumber(MyReader.GetString("PEN_CUKAI_LAIN"), 0)
                    txtAmount8.Text = FormatNumber(MyReader.GetString("PNBP"), 0)
                    txtAmount9.Text = FormatNumber(MyReader.GetString("VAT"), 0)
                    txtAmount10.Text = FormatNumber(MyReader.GetString("PPNBM"), 0)
                    txtAmount11.Text = FormatNumber(MyReader.GetString("PPH22"), 0)
                    'enddetai
                    If btnReject.Enabled = True Then
                        btnReject.Enabled = f_getenable(Status.Text)
                    End If
                    If btnPrint.Enabled = True Then
                        btnPrint.Enabled = f_getenable(Status.Text)
                    End If
                    txtBC.ReadOnly = f_getread(Status.Text)
                    txtBank.ReadOnly = f_getread(Status.Text)

                    'crt.ReadOnly = f_getread(Status.Text)
                    'approvedby.ReadOnly = f_getread(Status.Text)
                    'recby.ReadOnly = f_getread(Status.Text)

                    txtBln.Enabled = f_getenable(Status.Text)
                    txtThn.Enabled = f_getenable(Status.Text)
                    'crtdt.ReadOnly = f_getread(Status.Text)
                    AppDt.Enabled = f_getenable(Status.Text)
                    recDt.Enabled = f_getenable(Status.Text)

                    btnSearchPort.Visible = f_getenable(Status.Text)
                    btnSearchBank.Visible = f_getenable(Status.Text)
                    Button3.Visible = f_getenable(Status.Text)
                    Button4.Visible = f_getenable(Status.Text)
                    btnSave.Enabled = f_getenable(Status.Text)
                    btnReject.Enabled = btnSave.Enabled
                    btnPrint.Enabled = btnSave.Enabled

                    txtAmount1.ReadOnly = f_getread(Status.Text)
                    txtAmount2.ReadOnly = f_getread(Status.Text)
                    txtAmount3.ReadOnly = f_getread(Status.Text)
                    txtAmount4.ReadOnly = f_getread(Status.Text)
                    txtAmount5.ReadOnly = f_getread(Status.Text)
                    txtAmount6.ReadOnly = f_getread(Status.Text)
                    txtAmount7.ReadOnly = f_getread(Status.Text)
                    txtAmount8.ReadOnly = f_getread(Status.Text)
                    txtAmount9.ReadOnly = f_getread(Status.Text)
                    txtAmount10.ReadOnly = f_getread(Status.Text)
                    txtAmount11.ReadOnly = f_getread(Status.Text)
                Catch ex As Exception
                    'tempBC = ""
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            btnSave.Text = "Update"
            'posisi di akhir supaya tidak dead lock
            GetAmountT()
            txtBC.Text = tempBC
            txtBank.Text = tempBank
            txtCompCode.Text = tempComp
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            If ctrec.Text <> "" Then
                recby.Text = GetName2(ctrec.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            'financeappby.Text = GetName2(CTFin.Text)
            Me.Text = MODULE_NAME & "# " & Trim(NONum) & " - Update"
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
    Private Sub FrmSSPCP_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 200)

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
        Dim modcode_a As String

        If sender Is "App" Then
            modcode_a = "SP-A"
            PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.Name as Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & modcode_a & "'"
            PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.Name as Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                   "tu.user_ct = tum.user_ct where tum.modul_code = '" & modcode_a & "'" & _
                                   "and tu.name LIKE 'FilterData1%' "
        Else
            'modcode_a = "*"
            PilihanDlg.SQLGrid = "Select distinct tu.user_ct as UserCT,tu.Name as Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                 "tu.user_ct = tum.user_ct "
            PilihanDlg.SQLFilter = "Select distinct tu.user_ct as UserCT,tu.Name as Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                   "tu.user_ct = tum.user_ct where " & _
                                   "and tu.name LIKE 'FilterData1%' "

        End If
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If sender Is "App" Then
                approvedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
                Call Name_Data(sender)
            ElseIf sender Is "" Then
                recby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
                ctrec.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            End If
        End If
    End Sub
    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String
        Dim modcode_a As String
        modcode_a = "SP-A"

        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & modcode_a & "' and tu.name='" & approvedby.Text & "'"
        Else
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tu.name='" & recby.Text & "'"
            '                     "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C' and tu.name='" & recby.Text & "'"
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
                        ctrec.Text = MyReader.GetString("user_ct")
                        recby.Text = MyReader.GetString("name")
                    End If
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Function CekData() As Boolean
        Dim STRsql, MODCode_a As String

        CekData = True

        'Foreign Key, CITY DAN APPROVED BY
        STRsql = " select * from tbm_port where port_code='" & txtBC.Text & "'"

        If FM02_MaterialGroup.DataOK(STRsql) = True Then
            MsgBox("Port code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtBC.Focus()
            Exit Function
        End If

        ModCode_A = "SP-A"

        'Foreign Key
        If approvedby.Text <> "" Then
            STRsql = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & MODCode_a & "' and tu.name='" & approvedby.Text & "'"

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
        Dim AppDte, RecDte, SQLStr, str1 As String
        Dim tempbln As String
        'Dim num1, num2, num3, num4 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDt, "Approved Date") Then Exit Sub
        If Not CekInputTgl(ctrec, recDt, "Received Date") Then Exit Sub
        Try
            'OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            AppDte = Format(AppDt.Value, "yyyy-MM-dd")
            RecDte = Format(recDt.Value, "yyyy-MM-dd")
            'findt = Format(findt.ToString, "yyyy-MM-dd")
            'num1 = CDec(txtAmount1.Text)
            'num2 = GetNum(deposit.Text)
            'num3 = GetNum(commision.Text)
            'num4 = GetNum(charge.Text)

            'str1 = Replace(num1, ",", ".")
            'str2 = Replace(num2, ",", ".")
            'str3 = Replace(num3, ",", ".")
            'str4 = Replace(num4, ",", ".")
            If btnSave.Text = "Save" Then
                SQLStr = "Run Stored Procedure SaveSSPCP(Save," & Ship & "," & txtBC.Text & "," & txtBank.Text & "," & CTApp.Text & "," & AppDte & "," & ctrec.Text & "," & RecDte & "," & crt.Text & "," & crtdt.Text & ")"
                keyprocess = "Save"
            ElseIf btnSave.Text = "Update" Then
                SQLStr = "Run Stored Procedure SaveSSPCP(Updt," & Ship & "," & txtBC.Text & "," & txtBank.Text & "," & CTApp.Text & "," & AppDte & "," & ctrec.Text & "," & RecDte & "," & crt.Text & "," & crtdt.Text & ")"
                keyprocess = "Updt"
            End If
            MyComm.CommandText = "SaveSSPCP"

            'periode
            If Len(txtBln.SelectedIndex.ToString) = 1 Then
                tempbln = "0" & (txtBln.SelectedIndex + 1)
            Else
                tempbln = CStr(txtBln.SelectedIndex + 1)
            End If
            str1 = txtThn.Text & Microsoft.VisualBasic.Right(tempbln, 2)


            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("Periode", str1)
            MyComm.Parameters.AddWithValue("BC", txtBC.Text)
            MyComm.Parameters.AddWithValue("Company", txtCompCode.Text)
            MyComm.Parameters.AddWithValue("Bank", txtBank.Text)
            'detail
            MyComm.Parameters.AddWithValue("BEA_MASUK", CDbl(txtAmount1.Text))
            MyComm.Parameters.AddWithValue("SPM", CDbl(txtAmount2.Text))
            MyComm.Parameters.AddWithValue("DENDA_PABEAN", CDbl(txtAmount3.Text))
            MyComm.Parameters.AddWithValue("PEN_PABEAN_LAIN", CDbl(txtAmount4.Text))
            MyComm.Parameters.AddWithValue("CUKAI_ETIL", CDbl(txtAmount5.Text))
            MyComm.Parameters.AddWithValue("DENDA_CUKAI", CDbl(txtAmount6.Text))
            MyComm.Parameters.AddWithValue("PEN_CUKAI_LAIN", CDbl(txtAmount7.Text))
            MyComm.Parameters.AddWithValue("PNBP", CDbl(txtAmount8.Text))
            MyComm.Parameters.AddWithValue("VAT", CDbl(txtAmount9.Text))
            MyComm.Parameters.AddWithValue("PPNBM", CDbl(txtAmount10.Text))
            MyComm.Parameters.AddWithValue("PPH22", CDbl(txtAmount11.Text))
            'foot
            'MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
            'MyComm.Parameters.AddWithValue("AppDt", AppDte)
            'MyComm.Parameters.AddWithValue("RecBy", ctrec.Text)
            'MyComm.Parameters.AddWithValue("RecDt", RecDte)
            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDte)
                MyComm.Parameters.AddWithValue("Stat", "Approved")
            End If
            If ctrec.Text = "" Then
                MyComm.Parameters.AddWithValue("RecBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("RecDt", DBNull.Value)                
            Else
                MyComm.Parameters.AddWithValue("RecBy", ctrec.Text)
                MyComm.Parameters.AddWithValue("RecDt", RecDte)
            End If

            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
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
                f_msgbox_successful(btnSave.Text & " SSPCP")
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " SSPCP  failed")
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
            SQLstr = "UPDATE tbl_sspcp " & _
                     "SET STATUS='Rejected'" & _
                     " where SHIPMENT_NO='" & Ship & "' " & _
                     " and ord_no= '" & NONum & "'"
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
        Dim v_num As String

        'v_pono = txtpono.ToString
        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num.ToString
        End If
        ViewerFrm.Tag = "SPPP" & v_num & Ship


        ViewerFrm.ShowDialog()
    End Sub


    Private Sub txtBank_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBank.TextChanged
        If txtBank.Text = "" Then
            lblBank.Text = ""
        ElseIf txtBank.Text <> "" Then
            lblBank.Text = AmbilData("BANK_NAME", "TBM_BANK", "BANK_CODE='" & txtBank.Text & "'")
        End If
    End Sub

    Private Sub txtBC_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBC.TextChanged
        lblBCOfficeName.Text = AmbilData("PORT_NAME", "TBM_PORT", "PORT_CODE='" & txtBC.Text & "'")
    End Sub

    Private Sub btnSearchPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPort.Click
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.SQLGrid = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port"
        PilihanDlg.SQLFilter = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                               "WHERE port_code LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBC.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblBCOfficeName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub txtCompCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompCode.TextChanged
        lblComp.Text = AmbilData("COMPANY_NAME", "TBM_COMPANY", "COMPANY_CODE='" & txtCompCode.Text & "'")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("")
        recDt.Checked = True
    End Sub


    Private Sub txtAmount1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount1.Validated
        If txtAmount1.Text = "" Then
            txtAmount1.Text = "0"
        Else
            txtAmount1.Text = FormatNumber(txtAmount1.Text, 0)
        End If
        GetAmountT()
    End Sub
    Private Sub txtAmount2_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount2.Validated
        If txtAmount2.Text = "" Then
            txtAmount2.Text = "0"
        Else
            txtAmount2.Text = FormatNumber(txtAmount2.Text, 0)
        End If
        GetAmountT()
    End Sub
    Private Sub txtAmount3_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount3.Validated
        If txtAmount3.Text = "" Then
            txtAmount3.Text = "0"
        Else
            txtAmount3.Text = FormatNumber(txtAmount3.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount4_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount4.Validated
        If txtAmount4.Text = "" Then
            txtAmount4.Text = "0"
        Else
            txtAmount4.Text = FormatNumber(txtAmount4.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount5_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount5.Validated
        If txtAmount5.Text = "" Then
            txtAmount5.Text = "0"
        Else
            txtAmount5.Text = FormatNumber(txtAmount5.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount6_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount6.Validated
        If txtAmount6.Text = "" Then
            txtAmount6.Text = "0"
        Else
            txtAmount6.Text = FormatNumber(txtAmount6.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount7_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount7.Validated
        If txtAmount7.Text = "" Then
            txtAmount7.Text = "0"
        Else
            txtAmount7.Text = FormatNumber(txtAmount7.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount8_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount8.Validated
        If txtAmount8.Text = "" Then
            txtAmount8.Text = "0"
        Else
            txtAmount8.Text = FormatNumber(txtAmount8.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount9_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount9.Validated
        If txtAmount9.Text = "" Then
            txtAmount9.Text = "0"
        Else
            txtAmount9.Text = FormatNumber(txtAmount9.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount10_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount10.Validated
        If txtAmount10.Text = "" Then
            txtAmount10.Text = "0"
        Else
            txtAmount10.Text = FormatNumber(txtAmount10.Text, 0)
        End If
        GetAmountT()
    End Sub

    Private Sub txtAmount11_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount11.Validated
        If txtAmount11.Text = "" Then
            txtAmount11.Text = "0"
        Else
            txtAmount11.Text = FormatNumber(txtAmount11.Text, 0)
        End If
        GetAmountT()
    End Sub
    Private Sub GetAmountT()
        'If txtAmount1.Text <> "" And txtAmount2.Text <> "" And txtAmount3.Text <> "" & _
        '    txtAmount4.Text <> "" And txtAmount5.Text <> "" And txtAmount6.Text <> "" & _
        '    txtAmount7.Text <> "" And txtAmount8.Text <> "" And txtAmount9.Text <> "" & _
        '    txtAmount10.Text <> "" And txtAmount11.Text <> "" Then

        If cek_amount() = "" Then
            txtAmountT.Text = CStr(CDbl(txtAmount1.Text) + CDbl(txtAmount2.Text) + _
                              CDbl(txtAmount3.Text) + CDbl(txtAmount4.Text) + _
                              CDbl(txtAmount5.Text) + CDbl(txtAmount6.Text) + _
                              CDbl(txtAmount7.Text) + CDbl(txtAmount8.Text) + _
                              CDbl(txtAmount9.Text) + CDbl(txtAmount10.Text) + _
                              CDbl(txtAmount11.Text))
            txtAmountT.Text = FormatNumber(txtAmountT.Text, 0)
        End If
        'End If

    End Sub
    Private Function cek_amount() As String
        If IsNumeric(txtAmount1.Text) Then
            If IsNumeric(txtAmount2.Text) Then
                If IsNumeric(txtAmount3.Text) Then
                    If IsNumeric(txtAmount4.Text) Then
                        If IsNumeric(txtAmount5.Text) Then
                            If IsNumeric(txtAmount6.Text) Then
                                If IsNumeric(txtAmount7.Text) Then
                                    If IsNumeric(txtAmount8.Text) Then
                                        If IsNumeric(txtAmount9.Text) Then
                                            If IsNumeric(txtAmount10.Text) Then
                                                If IsNumeric(txtAmount11.Text) Then
                                                Else
                                                    cek_amount = "err11"
                                                    'f_msgbox_custom(txtItem11 & " - wrong input")
                                                End If
                                            Else
                                                cek_amount = "err10"
                                                'f_msgbox_custom(txtItem10 & " - wrong input")
                                            End If
                                        Else
                                            cek_amount = "err9"
                                            'f_msgbox_custom(txtItem9 & " - wrong input")
                                        End If
                                    Else
                                        cek_amount = "err8"
                                        'f_msgbox_custom(txtItem8 & " - wrong input")
                                    End If
                                Else
                                    cek_amount = "err7"
                                    'f_msgbox_custom(txtItem7 & " - wrong input")
                                End If
                            Else
                                cek_amount = "err6"
                                'f_msgbox_custom(txtItem6 & " - wrong input")
                            End If
                        Else
                            cek_amount = "err5"
                            'f_msgbox_custom(txtItem5 & " - wrong input")
                        End If
                    Else
                        cek_amount = "err4"
                        'f_msgbox_custom(txtItem4 & " - wrong input")
                    End If
                Else
                    cek_amount = "err3"
                    'f_msgbox_custom(txtItem3 & " - wrong input")
                End If
            Else
                cek_amount = "err2"
                'f_msgbox_custom(txtItem2 & " - wrong input")
            End If
        Else
            cek_amount = "err1"
            'f_msgbox_custom(txtItem1 & " - wrong input")
        End If
    End Function
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

End Class