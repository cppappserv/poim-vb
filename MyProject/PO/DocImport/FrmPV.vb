'Title                         : Transaksi Document Import - DI
'Form                          : FrmPV
'Created By                    : Hanny
'Created Date                  : 24 NOV 2008
'Table Used                    : tbm_bank, tbm_bank_Reference, 
'                                tbl_docimpr, tbl_SHIPPING_doc
'Stored Procedure Used (MySQL) : SavePV
Public Class FrmPV
    Dim Ship, PVNum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String = "PV"
    Dim MODULE_NAME As String = "Payment Voucher "

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
    Sub New(ByVal ShipNo As String, ByVal Currency As String, ByVal CurrName As String, ByVal TotalAmt As String, ByVal kurs As String, ByVal PV As String)
        Dim ttl As Decimal
        Dim lcno As String
        Dim v_tgl As String

        InitializeComponent()
        txtCity_Code.Text = ""
        approvedby.Text = ""
        CTApp.Text = ""
        CTFin.Text = ""
        AppDT.Value = GetServerDate()
        FinDT.Value = GetServerDate()
        AppDT.Checked = False
        FinDT.Checked = False
        Ship = ShipNo

        v_tgl = AmbilData("tt_dt", "tbl_shipping", "SHIPMENT_NO='" & Ship & "'")
        If v_tgl = "" Then
            DTPrinted.Value = GetServerDate()
        Else
            DTPrinted.Value = CDate(v_tgl)
        End If

        If Trim(PV) <> "" Then
            Call DisplayData(PV)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("PV-P")) Then
                btnPrint.Enabled = True
                btnTTd.Enabled = True
            Else
                btnPrint.Enabled = False
                btnTTd.Enabled = False
            End If
        Else
            crtdt.Text = GetServerDate.ToString
            crt.Text = UserData.UserName
            CTCrt.Text = UserData.UserCT.ToString
            btnReject.Enabled = False
            btnPrint.Enabled = False
            btnTTd.Enabled = False
            PVNum = 0
            txtCity_Code.Text = "JKT"
            Me.Text = MODULE_NAME & "- New"
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

    Private Sub DisplayData(ByVal PV As String)
        Dim pjg, ttl As Integer
        Dim strSQL, errMSg, temp, temp2 As String

        pjg = Len(RTrim(PV)) - 4
        num = Mid(PV, 5, pjg)
        PVNum = num
        strSQL = "SELECT t1.*, m1.company_code FROM TBL_SHIPPING_DOC t1, TBL_SHIPPING t2, TBM_PLANT m1 " & _
                 "WHERE t1.shipment_no=t2.shipment_no AND t2.plant_code=m1.plant_code " & _
                 "AND t1.SHIPMENT_NO = '" & Ship & "' AND t1.ORD_NO = '" & PVNum & "' AND t1.FINDOC_TYPE = 'PV'"

        DTPrinted.Enabled = False
        txtCity_Code.ReadOnly = True
        'DIno.ReadOnly = True
        approvedby.ReadOnly = True
        financeappby.ReadOnly = True
        crt.ReadOnly = True
        AppDT.Enabled = True
        FinDT.Enabled = True
        crtdt.ReadOnly = True
        'btnSave.Enabled = False
        btnSearchCity.Visible = False
        Button3.Visible = False

        errMSg = "Failed when read PV data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtCompanyCode.Text = MyReader.GetString("company_code")
                    temp = MyReader.GetString("FINDOC_PRINTEDON")
                    DTPrinted.Text = MyReader.GetString("FINDOC_PRINTEDDT")
                    CTCrt.Text = MyReader.GetString("FINDOC_CREATEDBY")
                    Status.Text = MyReader.GetString("FINDOC_STATUS")
                    crtdt.Text = MyReader.GetString("FINDOC_CREATEDdt")
                    txtRemark.Text = MyReader.GetString("FINDOC_NOTE")
                    Try
                        CTApp.Text = MyReader.GetString("FINDOC_APPBY")
                    Catch ex As Exception
                        CTApp.Text = ""
                        AppDT.Checked = False
                    End Try
                    If CTApp.Text <> "" Then
                        AppDT.Value = MyReader.GetString("FINDOC_APPDT")
                        AppDT.Checked = True
                    End If
                    Try
                        CTFin.Text = MyReader.GetString("FINDOC_FINAPPBY")
                    Catch ex As Exception
                        CTFin.Text = ""
                        FinDT.Checked = False
                    End Try
                    If CTFin.Text <> "" Then
                        FinDT.Value = MyReader.GetString("FINDOC_FINAPPDT")
                        FinDT.Checked = True
                    End If

                    If btnReject.Enabled = True Then
                        btnReject.Enabled = f_getenable(Status.Text)
                    End If
                    If btnPrint.Enabled = True Then
                        btnPrint.Enabled = f_getenable(Status.Text)
                        btnTTd.Enabled = f_getenable(Status.Text)
                    End If
                    DTPrinted.Enabled = f_getenable(Status.Text)
                    txtCity_Code.ReadOnly = f_getread(Status.Text)
                    crt.ReadOnly = f_getread(Status.Text)
                    crtdt.ReadOnly = f_getread(Status.Text)
                    btnSearchCity.Visible = f_getenable(Status.Text)
                    Button3.Visible = f_getenable(Status.Text)
                    Button4.Visible = f_getenable(Status.Text)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            btnSave.Text = "Update"
            'posisi di akhir supaya tidak dead lock
            txtCity_Code.Text = temp
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            If CTFin.Text <> "" Then
                financeappby.Text = GetName2(CTFin.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            Me.Text = MODULE_NAME & "# " & Trim(PVNum) & " - Update"
            btnSave.Enabled = f_getenable(Status.Text)
            btnReject.Enabled = btnSave.Enabled
            btnPrint.Enabled = btnSave.Enabled
            btnTTd.Enabled = btnSave.Enabled
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
    Private Sub FrmPV_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        AppDT.Checked = True

    End Sub
    Private Sub GetName(ByVal sender As String)
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        If sender Is "App" Then
            PilihanDlg.SQLGrid = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                 "tu.user_ct = tum.user_ct where tum.modul_code = 'PV-A'"
            PilihanDlg.SQLFilter = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                                   "tu.user_ct = tum.user_ct where tum.modul_code = 'PV-A'" & _
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
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'PV-A' and tu.name='" & approvedby.Text & "'"
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
                     "tu.user_ct = tum.user_ct where tum.modul_code = 'PV-A' and tu.name='" & approvedby.Text & "'"

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
        Dim AppDt1, FinDt1, DTPrinted1, SQLStr, str1, str2, str3, str4 As String
        Dim num1, num2, num3, num4 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDT, "Approved Date") Then Exit Sub
        If Not CekInputTgl(CTFin, FinDT, "Finance App Date") Then Exit Sub
        Try
            'OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            AppDt1 = Format(AppDT.Value, "yyyy-MM-dd")
            FinDt1 = Format(FinDT.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")

            'num1 = GetNum(TotalAmount.Text)
            'num2 = GetNum(deposit.Text)
            'num3 = GetNum(commision.Text)
            'num4 = GetNum(charge.Text)

            'str1 = Replace(num1, ",", ".")
            'str2 = Replace(num2, ",", ".")
            'str3 = Replace(num3, ",", ".")
            'str4 = Replace(num4, ",", ".")

            If btnSave.Text = "Save" Then                
                SQLStr = "Run Stored Procedure SavePV (Save," & Ship & "," & txtCity_Code.Text & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & CTFin.Text & "," & FinDt1 & "," & crt.Text & "," & crtdt.Text & "," & txtRemark.Text & ")"
                keyprocess = "Save"
            ElseIf btnSave.Text = "Update" Then
                SQLStr = "Run Stored Procedure SavePV (Updt," & Ship & "," & txtCity_Code.Text & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & CTFin.Text & "," & FinDt1 & "," & crt.Text & "," & crtdt.Text & "," & txtRemark.Text & ")"
                keyprocess = "Updt"
            End If

            MyComm.CommandText = "SavePV"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("CityCode", txtCity_Code.Text)
            MyComm.Parameters.AddWithValue("DTPrint", DTPrinted1)
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
            MyComm.Parameters.AddWithValue("Note", txtRemark.Text)
            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", PVNum)
            End If
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " PV")
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " PV failed")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            'transaction.Rollback()
        End Try
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject " & MODULE_NAME & "#" & PVNum & " of " & Ship & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_shipping_doc " & _
                     "SET FINDOC_STATUS='Rejected'" & _
                     " where SHIPMENT_NO='" & Ship & "' " & _
                     " and ord_no=" & PVNum & "" & _
                     " AND FINDOC_TYPE = 'PV'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = MODULE_NAME & "#" & PVNum & " of " & Ship & " has been rejected"
                MsgBox(msg)
            End If

        End If
        btnClose_Click(sender, e)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num
        End If
        ViewerFrm.Tag = "PVVV" & v_num & Ship
        ViewerFrm.ShowDialog()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("Fun")
        FinDT.Checked = True

    End Sub

    Private Sub txtCity_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCity_Code.TextChanged
        lblCityName.Text = AmbilData("CITY_NAME", "TBM_CITY", "CITY_CODE='" & txtCity_Code.Text & "'")
    End Sub
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub btnTTd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTTd.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num, tptgl As String

        tptgl = Format(DTPrinted.Value, "yyyy-MM-dd")

        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num
        End If
        ViewerFrm.Tag = "TTDPV;" & tptgl & "." & CTFin.Text & "." & txtCompanyCode.Text & "." & CTCrt.Text
        ViewerFrm.ShowDialog()
    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class