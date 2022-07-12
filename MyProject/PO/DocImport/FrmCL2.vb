'Title                         : Transaksi CL
'Form                          : FrmCL2
'Created By                    : Hanny
'Created Date                  : 09 Feb 2009
'Table Used                    : tbl_shipping_doc, tbl_shipping_doc_detail, 
'                                tbl_docsupl, tbm_expedition

'Stored Procedure Used (MySQL) : SaveCL2
Public Class FrmCL2
    Dim Ship, NONum As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String
    Dim MODULE_NAME As String
    Dim ModCode As String
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim v_doccode, V_DOCNAME, V_DOCNO As String

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
        MODULE_CODE = "CL"
        MODULE_NAME = "Cover Letter "

        InitializeComponent()
        txtCity_Code.Text = ""
        txtExp.Text = ""
        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        CTApp.Text = ""
        AppDt.Value = GetServerDate()
        AppDt.Checked = False

        Ship = ShipNo
        If Trim(NO) <> "" Then
            Call DisplayData(NO)
            btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CTCrt.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("CL-P")) Then
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
            Me.Text = MODULE_NAME & "- New"
        End If
    End Sub

    Private Sub btnSearchExp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchExp.Click
        PilihanDlg.Text = "Select Expedition Company Code"
        PilihanDlg.LblKey1.Text = "Expedition Company Code"
        PilihanDlg.SQLGrid = "SELECT company_code as ExpeditionCompanyCode, company_name as ExpeditionCompanyName, Title as Title, AUTHORIZE_PERSON as AuthorizedPerson FROM tbm_expedition"
        PilihanDlg.SQLFilter = "SELECT company_code as ExpeditionCompanyCode, company_name as ExpeditionCompanyName, Title as Title, AUTHORIZE_PERSON as AuthorizedPerson FROM tbm_expedition " & _
                               "WHERE PPJK_STAT = '1' and company_code LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_expedition"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtExp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblExp.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            txtTitle.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            txtAuthorized.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString
        End If
    End Sub

    Private Sub btnSearchCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCity.Click
        PilihanDlg.Text = "Select City Code"
        PilihanDlg.LblKey1.Text = "City Code"
        PilihanDlg.LblKey2.Text = "City Name"
        PilihanDlg.SQLGrid = "SELECT city_code as CityCode, city_name as CityName, country_code as CountryCode FROM tbm_city where country_code='ID'"
        PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName, country_code as CountryCode FROM tbm_city " & _
                               "WHERE city_code LIKE 'FilterData1%' and city_name LIKE 'FilterData2%' and country_code='ID'"
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
        pjg = Len(RTrim(NO)) - 4
        num = CInt(Mid(NO, 5, pjg))
        NONum = num.ToString
        strSQL = "SELECT * FROM TBL_SHIPPING_DOC " & _
                 "WHERE SHIPMENT_NO = '" & Ship & "' AND " & _
                 "ORD_NO = '" & NONum & "' AND FINDOC_TYPE = '" & MODULE_CODE & "' and FINDOC_GROUPTO = 'EMKL'"

        DTPrinted.Enabled = False
        txtCity_Code.ReadOnly = True
        txtExp.ReadOnly = True
        txtAuthorized.ReadOnly = True

        AppDt.Enabled = False
        crtdt.ReadOnly = True

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
                    txtCity_Code.ReadOnly = f_getread(Status.Text)
                    txtExp.ReadOnly = f_getread(Status.Text)
                    txtAuthorized.ReadOnly = f_getread(Status.Text)
                    AppDt.Enabled = f_getenable(Status.Text)
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
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
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
    Private Sub FrmCL2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

        If MODULE_CODE = "CL" Then
            ModCode = "CL-A"
        End If
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

        If MODULE_CODE = "CL" Then
            ModCode = "CL-A"
        End If

        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' and tu.name='" & approvedby.Text & "'"
        End If

        errMSg = "Failed when read user authorization data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If sender = "App" Then
                        CTApp.Text = MyReader.GetString("user_ct")
                        approvedby.Text = MyReader.GetString("name")
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

        STRsql = " select * from tbm_CITY where city_code='" & txtCity_Code.Text & "'"

        If FM02_MaterialGroup.DataOK(STRsql) = True Then
            MsgBox("City code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txtCity_Code.Focus()
            Exit Function
        End If
        If MODULE_CODE = "CL" Then
            ModCode = "CL-A"
        End If
        'Foreign Key
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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader

        Dim AppDt1, SQLStr, DTPrinted1 As String
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDt, "Approved Date") Then Exit Sub        

        Try
            AppDt1 = Format(AppDt.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")

            If MODULE_CODE = "CL" Then
                SQLStr = "Run Stored Procedure SaveCL2 (" & btnSave.Text & ","
                If btnSave.Text = "Save" Then
                    keyprocess = "Save"
                ElseIf btnSave.Text = "Update" Then
                    keyprocess = "Updt"
                End If
            End If

            MyComm.CommandText = "SaveCL2"
            MyComm.CommandType = CommandType.StoredProcedure
            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("CityCode", txtCity_Code.Text)
            MyComm.Parameters.AddWithValue("DTPrint", DTPrinted1)
            MyComm.Parameters.AddWithValue("Expd", txtExp.Text)

            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
                SQLStr = SQLStr & Ship & "," & txtCity_Code.Text & "," & DTPrinted1 & "," & txtExp.Text & "," & "NULL" & "," & "NULL" & "," & "Open" & UserData.UserCT & "," & MODULE_CODE & ","
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDt1)
                MyComm.Parameters.AddWithValue("Stat", "Approved")
                SQLStr = SQLStr & Ship & "," & txtCity_Code.Text & "," & DTPrinted1 & "," & txtExp.Text & "," & CTApp.Text & "," & AppDt1 & "," & "Approved" & UserData.UserCT & "," & MODULE_CODE & ","
            End If

            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("MODUL", MODULE_CODE)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
                SQLStr = SQLStr & ""
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", NONum)
                SQLStr = SQLStr & NONum
            End If
            SQLStr = SQLStr & ")"
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " " & MODULE_CODE)
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " " & MODULE_CODE & " failed'")
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
        Dim v_num As String

        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num.ToString
        End If
        If MODULE_CODE = "CL" Then
            ViewerFrm.Tag = "CLLL" & v_num & Ship
        End If

        ViewerFrm.ShowDialog()
    End Sub


    Private Sub txtExp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExp.TextChanged
        If txtExp.Text = "" Then
            lblExp.Text = ""
            txtAuthorized.Text = ""
            txtTitle.Text = ""
        ElseIf txtExp.Text <> "" Then
            lblExp.Text = AmbilData("COMPANY_NAME", "TBM_EXPEDITION", "COMPANY_CODE='" & txtExp.Text & "'")
            txtAuthorized.Text = AmbilData("AUTHORIZE_PERSON", "TBM_EXPEDITION", "COMPANY_CODE='" & txtExp.Text & "'")
            txtTitle.Text = AmbilData("TITLE", "TBM_EXPEDITION", "COMPANY_CODE='" & txtExp.Text & "'")
        End If
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
End Class