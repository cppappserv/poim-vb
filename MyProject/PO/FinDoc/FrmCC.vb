'Title                         : Transaksi CL
'Form                          : FrmCL
'Created By                    : Hanny
'Created Date                  : 09 Feb 2009
'Table Used                    : tbl_shipping_doc, tbl_shipping_doc_detail, 
'                                tbl_docsupl, tbm_expedition
'Modify                        : 04 Aug 2009 : btnCalc_Click
'                                - mengganti relasi  inner join pada query
'                                - mengganti cara menampilkan hasil dalam grid
'                                - mengganti logika mandapatkan rate 

'Stored Procedure Used (MySQL) : SaveCC
Public Class FrmCC
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim Ship, NONum, Plant, Group As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num, jmlCC, jmlCont As Integer
    Dim arrCNo(), arrCSz() As String
    Dim strCSz, strCNo As String
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String
    Dim MODULE_NAME As String
    Dim ModCode As String
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim v_doccode, V_DOCNAME, BLStatus, BLNo As String
    Dim CCNo, ShipOrdNo, xFirst As Integer
    Dim FinalApp, MultiCont As Boolean

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
    Sub New(ByVal No As Integer, ByVal ShipNo As Integer, ByVal ShipOrd As Integer, ByVal BLStat As String, ByVal BLNum As String)
        Dim strSQL, errMSg As String
        Dim lv_CTApp, idx As Integer

        MODULE_CODE = "CC"
        MODULE_NAME = "Inklaring "

        InitializeComponent()

        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        financeappby.Text = ""
        CTApp.Text = ""
        CTFin.Text = ""
        AppDt.Value = GetServerDate()
        DTRequest.Value = GetServerDate()
        AppDt.Checked = False 'GetServerDate()
        FinalApp = False

        CCNo = No
        Ship = ShipNo
        ShipOrdNo = ShipOrd
        BLStatus = BLStat
        BLNo = BLNum

        'Call GetButtonAccess()

        fillcbFor()

        If Trim(No) <> 0 Then
            Call DisplayData()
            refreshGrid(Ship, ShipOrdNo)
            If (btnPrint.Enabled) And (PunyaAkses("CC-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If

            lv_CTApp = IIf(CTApp.Text = "", 0, CTApp.Text)
            If (Status.Text = "Approved") And (lv_CTApp = UserData.UserCT) Then
                btnApprove.Enabled = True
            Else
                btnApprove.Enabled = False
            End If
        Else
            btnReject.Enabled = False
            btnApprove.Enabled = False
            btnPrint.Enabled = False
            NONum = "0"
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            crtdt.Text = GetServerDate.ToString
            Me.Text = MODULE_NAME & "- New"

            strSQL = "SELECT SUM(1) unit_tot, t1.unit_code, m1.unit_name, m2.type_name FROM tbl_shipping_cont t1, tbm_unit m1, tbm_unit_type m2 " & _
                     "WHERE t1.unit_code=m1.unit_code AND m1.type_code=m2.type_code AND t1.shipment_no='" & Ship & "' GROUP BY t1.unit_code, m2.type_name "

            errMSg = "Failed when read user authorization data"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

            cbCont.Items.Clear()
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        txtCont.Text = MyReader.GetString("type_name")
                        txtContNum.Text = MyReader.GetString("unit_tot")
                        cbCont.Items.Add(Trim(MyReader.GetString("unit_code")))
                        cbCont.Text = Trim(MyReader.GetString("unit_code"))
                        strCNo = strCNo & "," & txtContNum.Text
                        strCSz = strCSz & "," & cbCont.Text
                        jmlCont = jmlCont + 1
                    Catch ex As Exception
                        txtCont.Text = ""
                        txtContNum.Text = ""
                        strCNo = ""
                        strCSz = ""
                        cbCont.Items.Add("")
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            End If
            If (strCNo <> "" And strCSz <> "") Then
                strCNo = Mid(strCNo, 2, Len(strCNo) - 1)
                strCSz = Mid(strCSz, 2, Len(strCSz) - 1)
                arrCNo = Split(strCNo, ",")
                arrCSz = Split(strCSz, ",")

                If jmlCont > 1 Then
                    txtContNum.Text = strCNo
                    cbCont.Items.Add(strCSz)
                    cbCont.Text = strCSz
                End If
            End If

            If txtContNum.Text = "" Then
                txtCont.Text = "Curah"
                txtContNum.Visible = False
                cbCont.Visible = False
            Else
                If jmlCC = 1 Then ' lock item container jika hanya ada 1 perhitungan Inklaring
                    txtContNum.ReadOnly = True
                    cbCont.Enabled = False
                Else
                    MultiCont = True
                End If
            End If
        End If

        Call GetButtonAccess()
    End Sub

    Private Sub GetButtonAccess()
        Dim SQLStr, ModCode As String
        If MODULE_CODE = "CC" Then
            ModCode = "CC-C"
        End If

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        btnSave.Enabled = (DataExist(SQLStr) = True)
        If CTCrt.Text <> "" Then btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT)

        btnReject.Enabled = btnSave.Enabled

        If MODULE_CODE = "CC" Then
            ModCode = "CC-L"
        End If

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        btnPrint.Enabled = (DataExist(SQLStr) = True)
    End Sub

    Private Sub DisplayData()
        Dim pjg As Integer
        Dim strSQL, errMSg As String
        Dim TEMP As String = ""
        Dim tempExp As String = ""

        Me.Text = "CC #" & CCNo
        errMSg = "CC data view failed"

        NONum = num.ToString
        strSQL = "SELECT t1.*, m2.plant_name, m1.group_name, m3.kurs_pajak " & _
                 "FROM TBL_SHIPPING_DOC t1 " & _
                 "LEFT JOIN tbm_plant m2 ON t1.findoc_no=m2.plant_code " & _
                 "LEFT JOIN tbm_material_group m1 ON t1.findoc_reff=m1.group_code " & _
                 "LEFT JOIN tbl_shipping t2 ON t1.shipment_no=t2.shipment_no " & _
                 "LEFT JOIN tbm_kurs m3 ON m3.currency_code='USD' AND m3.effective_date='" & DTPrinted.Text & "' " & _
                 "WHERE t1.SHIPMENT_NO = '" & Ship & "' AND t1.ORD_NO = '" & CCNo & "' " & _
                 "AND t1.FINDOC_TYPE = '" & MODULE_CODE & "' and t1.FINDOC_GROUPTO = 'FIN'"

        '"LEFT JOIN tbm_kurs m3 ON m3.currency_code='USD' AND m3.effective_date=t2.sppb_dt " & _

        approvedby.ReadOnly = True
        crt.ReadOnly = True
        AppDt.Enabled = False
        crtdt.ReadOnly = True
        Button3.Visible = False
        Button4.Visible = False

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
                    remark.Text = MyReader.GetString("FINDOC_NOTE")
                Catch ex As Exception
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
                Try
                    CTFin.Text = MyReader.GetString("FINDOC_FINAPPBY")
                Catch ex As Exception
                    CTFin.Text = ""
                End Try

                DTRequest.Value = MyReader.GetString("FINDOC_FINAPPDT")
                Status.Text = MyReader.GetString("FINDOC_STATUS")

                If Trim(MyReader.GetString("findoc_no")) <> "" Then
                    cbFor.Text = "[" & Trim(MyReader.GetString("findoc_no")) & ":" & Trim(MyReader.GetString("findoc_reff")) & "] " & Trim(MyReader.GetString("plant_name")) & " ... " & Trim(MyReader.GetString("group_name"))
                Else
                    cbFor.Text = ""
                End If

                txtContNum.Text = MyReader.GetString("FINDOC_VALSIZE")
                cbCont.Text = Trim(MyReader.GetString("FINDOC_VALUNIT"))


            End While
            CloseMyReader(MyReader, UserData)
            btnSave.Text = "Update"
            'btnSave.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            btnSave.Enabled = (btnSave.Enabled And Not (Status.Text = "Rejected"))
            If btnReject.Enabled = True Then
                btnReject.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            End If
            ''untuk aktif jika sudah siap Final Approved
            ''If btnPrint.Enabled = True Then
            ''btnPrint.Enabled = (Status.Text <> "Open" And Status.Text <> "Rejected")
            ''End If
            btnCalc.Enabled = False
            cbFor.Enabled = False
            txtContNum.ReadOnly = True
            cbCont.Enabled = False

            dgvcost.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")

            DTPrinted.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            remark.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")
            approvedby.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")
            financeappby.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved" And Status.Text <> "Final Approved")
            AppDt.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            Button3.Visible = (Status.Text = "Open" Or Status.Text = "Approved")
            'Button4.Visible = (Status.Text = "Open" Or Status.Text = "Approved")
            'Button4.Visible = Not (Status.Text = "Rejected")
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            If CTFin.Text <> "" Then
                financeappby.Text = GetName2(CTFin.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)

            Me.Text = MODULE_NAME & "# " & CCNo
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

    Private Sub FrmCL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(55, 200)

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

        If sender = "App" Then
            ModCode = "CC-A"
        Else
            ModCode = "FI-C"
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
            ElseIf sender Is "Fin" Then
                financeappby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
            Call Name_Data(sender)
        End If
    End Sub

    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String

        If sender = "App" Then
            ModCode = "CC-A"
        Else
            ModCode = "FI-C"
        End If

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

        If approvedby.Text <> "" Then
            STRsql = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & MODULE_CODE & "-A' and tu.name='" & approvedby.Text & "'"

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
        Dim AppDt1, FinDt1, SQLStr, DTPrinted1 As String
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""
        Dim insertStr As String = ""
        Dim lv_cost_ord, lv_cost_code, lv_cost_desc, lv_cost_amount, lv_cost_unit, lv_cost_vat As String
        Dim lv_docdt, lv_no, lv_nourut, lv_curr, counter As String
        Dim FillStr, PlantCode, GroupCode, TotAmount As String
        Dim num1 As Decimal
        Dim ContTot, ContUnit As String

        If CekData() = False Then Exit Sub
        Try
            'OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")
            AppDt1 = Format(AppDt.Value, "yyyy-MM-dd")
            FinDt1 = Format(DTRequest.Value, "yyyy-MM-dd")

            FillStr = cbFor.Text
            If FillStr <> "" Then
                PlantCode = Mid(FillStr, 2, InStr(FillStr, ":") - 2)
                GroupCode = Mid(FillStr, InStr(FillStr, ":") + 1, InStr(FillStr, "]") - InStr(FillStr, ":") - 1)
            End If

            TotAmount = GetNum(ttotal.Text)
            TotAmount = GetNum2(TotAmount)

            ContTot = txtContNum.Text
            If ContTot = "" Then ContTot = "1"

            ContUnit = cbCont.Text

            dgvcost.CommitEdit(DataGridViewDataErrorContexts.Commit)
            counter = 0
            For i = 0 To dgvcost.RowCount - 1
                ErrMsg = "Failed to update CC detail data."
                If dgvcost.Rows(i).Cells("No").Value Is Nothing Then
                Else
                    lv_cost_ord = dgvcost.Rows(i).Cells("No").Value.ToString
                    lv_cost_code = dgvcost.Rows(i).Cells("ItemCost").Value.ToString
                    lv_cost_desc = dgvcost.Rows(i).Cells("Description").Value.ToString
                    lv_cost_amount = GetNum(dgvcost.Rows(i).Cells("Amount").Value.ToString)
                    lv_cost_amount = GetNum2(lv_cost_amount)
                    lv_curr = dgvcost.Rows(i).Cells("Currency").Value.ToString
                    lv_cost_unit = GetNum(dgvcost.Rows(i).Cells("Rate").Value.ToString)
                    lv_cost_unit = GetNum2(lv_cost_unit)
                    lv_cost_vat = GetNum(dgvcost.Rows(i).Cells("Vat").Value.ToString)
                    lv_cost_vat = GetNum2(lv_cost_vat)
                    lv_cost_ord = Mid(lv_cost_ord & "           ", 1, 11)
                    lv_cost_code = Mid(lv_cost_code & "     ", 1, 5)
                    lv_cost_desc = Mid(lv_cost_desc & Space(40), 1, 40)
                    lv_cost_amount = Mid(lv_cost_amount & "                 ", 1, 17)
                    lv_curr = Mid(lv_curr & "     ", 1, 5)
                    lv_cost_unit = Mid(lv_cost_unit & "                 ", 1, 17)
                    lv_cost_vat = Mid(lv_cost_vat & "                 ", 1, 17)

                    insertStr &= lv_cost_ord & lv_cost_code & lv_cost_desc & lv_cost_amount & lv_curr & lv_cost_unit & lv_cost_vat & ";"
                End If
            Next
            If MODULE_CODE = "CC" Then
                If btnSave.Text = "Save" Then
                    SQLStr = "Run Stored Procedure SaveCC (Save," & Ship & ",'CC'," & PlantCode & "," & GroupCode & "," & TotAmount & "," & ContTot & "," & ContUnit & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & crt.Text & "," & crtdt.Text & "," & insertStr & ")"
                    keyprocess = "Save"
                ElseIf btnSave.Text = "Update" Then
                    SQLStr = "Run Stored Procedure SaveCC (Updt," & Ship & ",'CC'," & PlantCode & "," & GroupCode & "," & TotAmount & "," & ContTot & "," & ContUnit & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & crt.Text & "," & crtdt.Text & "," & insertStr & ")"
                    keyprocess = "Updt"
                End If
            End If
            MyComm.CommandText = "SaveCC"

            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("PlantCode", PlantCode)
            MyComm.Parameters.AddWithValue("GroupCode", GroupCode)
            MyComm.Parameters.AddWithValue("TotAmount", TotAmount)
            MyComm.Parameters.AddWithValue("ContTot", ContTot)
            MyComm.Parameters.AddWithValue("ContUnit", ContUnit)
            MyComm.Parameters.AddWithValue("DTPrint", DTPrinted1)
            If CTApp.Text = "" Then
                MyComm.Parameters.AddWithValue("AppBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("AppDt", DBNull.Value)
                MyComm.Parameters.AddWithValue("Stat", "Open")
            Else
                MyComm.Parameters.AddWithValue("AppBy", CTApp.Text)
                MyComm.Parameters.AddWithValue("AppDt", AppDt1)
                If (FinalApp Or Status.Text = "Final Approved") Then
                    MyComm.Parameters.AddWithValue("Stat", "Final Approved")
                Else
                    MyComm.Parameters.AddWithValue("Stat", "Approved")
                End If
            End If
            'item ini digunakan untuk Request By
            'If CTFin.Text = "" Then
            'MyComm.Parameters.AddWithValue("FinBy", DBNull.Value)
            'MyComm.Parameters.AddWithValue("FinDt", DBNull.Value)
            'Else
            'MyComm.Parameters.AddWithValue("FinBy", CTFin.Text)
            'MyComm.Parameters.AddWithValue("FinDt", FinDt1)
            'End If

            MyComm.Parameters.AddWithValue("FinDt", FinDt1)
            MyComm.Parameters.AddWithValue("FinBy", UserData.UserCT)

            MyComm.Parameters.AddWithValue("Remark", remark.Text)
            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("MODUL", MODULE_CODE)
            If btnSave.Text = "Save" Then
                MyComm.Parameters.AddWithValue("vord", "")
            ElseIf btnSave.Text = "Update" Then
                MyComm.Parameters.AddWithValue("vord", ShipOrdNo)
            End If
            MyComm.Parameters.AddWithValue("InsertStr", insertStr)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " " & MODULE_NAME)
                btnClose_Click(sender, e)
            Else
                MsgBox(btnSave.Text & " " & MODULE_NAME & " failed'")
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
                     " and ord_no=" & CCNo & "" & _
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

        'v_PlantCode = txtPlantCode.ToString
        If Len(CStr(CCNo)) = 1 Then
            v_num = " " & CCNo
        Else
            v_num = CCNo.ToString
        End If
        If MODULE_CODE = "CC" Then
            ViewerFrm.Tag = "CCCC" & v_num & Ship
        End If

        Dim objek As New frmPilih
        objek.Tag() = "CCCC" & v_num & Ship
        objek.ShowDialog()


        ' ViewerFrm.ShowDialog()
    End Sub

    Private Sub refreshGrid(ByVal v_no As String, ByVal v_ord As String)
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts As DataTable
        Dim sumAmount, sumVAT, sumTotal As Double

        in_field = "cost_ord_no as No, cost_code as ItemCost, cost_description as Description, currency_code as currency, cost_amount as Amount, cost_unit as Rate, cost_vat as Vat"
        in_tbl = "tbl_cost"
        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where shipment_no = '" & v_no & "' and ship_ord_no = '" & v_ord & "' and type_code = 'CC'"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        dgvcost.DataSource = dts
        'If dts. > 0 Then
        'Show_Grid_JoinTable(DGVDetail, in_field, in_tbl)
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
            dgvcost.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvcost.Columns(3).DefaultCellStyle.Format = "N2"
            'get total
            sumAmount = AmbilData("sum(cost_amount)", "tbl_cost", "shipment_no = '" & v_no & "' and ship_ord_no = '" & v_ord & "' and type_code = 'CC'")
            sumVAT = AmbilData("sum(cost_vat)", "tbl_cost", "shipment_no = '" & v_no & "' and ship_ord_no = '" & v_ord & "' and type_code = 'CC'")
            sumTotal = sumAmount + sumVAT

            total.Text = FormatNumber(sumAmount, 2)
            tVAT.Text = FormatNumber(sumVAT, 2)
            ttotal.Text = FormatNumber(sumTotal, 2)

        End If
    End Sub

    Private Sub dgvCL_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvcost.CellClick
        If dgvcost.Columns(e.ColumnIndex).Name = "btnDoc" Then
            dgvcost.Rows(e.RowIndex).Cells("no").Value = e.RowIndex + 1
            dgvcost.Rows(e.RowIndex).Cells("DocCode").Value = v_doccode
            dgvcost.Rows(e.RowIndex).Cells("DocName").Value = V_DOCNAME
        End If
    End Sub


    Private Sub btnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalc.Click
        Dim strSQL, strSQL_6a, strSQL_6b, strSQL_7, strSQL_A, errMSg As String
        Dim V_PLANT, V_PORT, V_SUPPLIER, V_EXPEDITION, V_FREETIME, V_MATGRP, V_QTY, V_QTYUNIT, V_UNITTYPE As String
        Dim V_DOCDT, V_SPPBDT As Date
        Dim V_UNITCODE, V_UNITCODE2, V_CURR, V_CURRRATE As String
        Dim arrUNITCODE(), arrSIZE() As String
        Dim V_DAY, V_DAY2, V_DAY2_FTD, V_LEADTIME, V_TOTEQ As Integer
        Dim V_SIZE, V_RATE, V_RATE_MIN, V_RATEORG, V_RATEUNIT, V_KURS, V_RATEBYCOST, V_RATE_MINBYCOST, v_total, v_vat, v_vattotal, v_vatamount As Double
        Dim V_NOURUT, v_index As Integer
        Dim RstQ6a, RstQ6b, RstQ7, RstQA As DataTableReader
        Dim tempVar As String()
        Dim xCek, xTot As Integer
        Dim FillStr, PlantCode, GroupCode As String
        Dim DTPrinted1 As String

        If cbFor.Text = "" Then
            MsgBox("Please select Plant and Material Group first! ", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        Else
            FillStr = cbFor.Text
            PlantCode = Mid(FillStr, 2, InStr(FillStr, ":") - 2)
            GroupCode = Mid(FillStr, InStr(FillStr, ":") + 1, InStr(FillStr, "]") - InStr(FillStr, ":") - 1)
        End If
        DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")

        dgvcost.AllowUserToAddRows = True

        'Kosongkan Grid
        xTot = 0
        xCek = dgvcost.Rows.Count()
        If xCek > 0 Then
            Do Until xTot = xCek - 1
                dgvcost.Rows.Remove(dgvcost.Rows(0))
                xTot += 1
            Loop
        End If

        'Get variable from Query 1, 2, 3, 4, 5
        strSQL = _
            "select distinct t1.*, tu.type_code, tsdoc.findoc_to from " & _
                "(select ts.shipment_no, tsp.plant_code, ts.port_code, ts.supplier_code, ts.lead_time, ts.total_equipment, tm.group_code, " & _
                "sum(tsd.quantity) quantity , " & _
                "(select max(unit_code) unit_code from tbl_po_detail tpo where tpo.po_no=tsd.po_no and tpo.po_item=tsd.po_item) unit_code, " & _
                "if(ts.received_doc_dt is null or ts.received_doc_dt='', ts.received_copydoc_dt , ts.received_doc_dt ) docdt, ts.sppb_dt, (ts.free_time + ts.fte) free_time, " & _
                "DATEDIFF(if(ts.CLR_DT is null or ts.CLR_DT='', ts.EST_CLR_DT ,ts.CLR_DT), if(ts.NOTICE_ARRIVAL_DT is null or ts.NOTICE_ARRIVAL_DT='', EST_ARRIVAL_DT, NOTICE_ARRIVAL_DT)) AS V_DAY, " & _
                "DATEDIFF(if(ts.BAPB_DT is null or ts.BAPB_DT='', ts.EST_BAPB_DT ,ts.BAPB_DT), if(ts.NOTICE_ARRIVAL_DT is null or ts.NOTICE_ARRIVAL_DT='', EST_ARRIVAL_DT, NOTICE_ARRIVAL_DT)) AS V_DAY2 " & _
                "from tbl_shipping  as ts, tbl_shipping_detail as tsd, tbl_shipping_plant AS tsp, tbm_material as tm " & _
                "where ts.shipment_no = '" & Ship & "' AND tsp.plant_code='" & PlantCode & "' AND tm.group_code='" & GroupCode & "' " & _
                "and ts.shipment_no = tsd.shipment_no " & _
                "AND tsd.shipment_no = tsp.shipment_no AND tsd.po_no = tsp.po_no " & _
                "and tsd.material_code = tm.material_code " & _
                "group by tm.group_code) t1 " & _
            "left join tbl_shipping_cont AS tsc on t1.shipment_no = tsc.shipment_no " & _
            "left join tbm_unit AS tu on tsc.unit_code = tu.unit_code " & _
            "left join tbl_shipping_doc as tsdoc on t1.shipment_no = tsdoc.shipment_no " & _
            "and tsdoc.findoc_type = 'KO' and tsdoc.findoc_groupto = 'EMKL' and tsdoc.findoc_status <> 'Rejected' "

        '"and ts.shipment_no = tsd.shipment_no and tsd.material_code = tm.material_code and not isnull(ts.sppb_dt) " & _

        errMSg = "Failed when read data"
        If DBQueryGetTotalRows(strSQL, MyConn, errMSg, True, UserData) = 1 Then

            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
            xCek = 0
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        V_PLANT = MyReader.GetString("PLANT_CODE")
                    Catch ex As Exception
                        V_PLANT = ""
                    End Try
                    Try
                        V_PORT = MyReader.GetString("port_code")
                    Catch ex As Exception
                        V_PORT = ""
                    End Try
                    Try
                        V_SUPPLIER = MyReader.GetString("supplier_code")
                    Catch ex As Exception
                        V_SUPPLIER = ""
                    End Try
                    Try
                        V_EXPEDITION = MyReader.GetString("findoc_to")
                    Catch ex As Exception
                        V_EXPEDITION = ""
                    End Try
                    Try
                        V_DOCDT = MyReader.GetString("docdt")
                    Catch ex As Exception
                        V_DOCDT = ""
                    End Try
                    'Try
                    'V_SPPBDT = MyReader.GetString("sppb_dt")
                    'Catch ex As Exception
                    'V_SPPBDT = ""
                    'End Try
                    V_SPPBDT = DTPrinted.Text
                    Try
                        V_FREETIME = MyReader.GetString("free_time")
                    Catch ex As Exception
                        V_FREETIME = 0
                    End Try
                    Try
                        V_DAY = MyReader.GetString("V_DAY") + 1
                        If V_DAY = 0 Then V_DAY = 1
                    Catch ex As Exception
                        V_DAY = 0
                    End Try
                    Try
                        V_DAY2 = MyReader.GetString("V_DAY2") + 1
                        If V_DAY2 = 0 Then V_DAY2 = 1
                    Catch ex As Exception
                        V_DAY2 = 0
                    End Try
                    Try
                        V_LEADTIME = MyReader.GetString("lead_time")
                    Catch ex As Exception
                        V_LEADTIME = 0
                    End Try
                    Try
                        V_TOTEQ = MyReader.GetString("total_equipment")
                    Catch ex As Exception
                        V_TOTEQ = 0
                    End Try
                    Try
                        V_MATGRP = MyReader.GetString("group_code")
                    Catch ex As Exception
                        V_MATGRP = ""
                    End Try
                    Try
                        V_QTY = MyReader.GetString("quantity")
                    Catch ex As Exception
                        V_QTY = 0
                    End Try
                    Try
                        V_QTYUNIT = MyReader.GetString("unit_code")
                    Catch ex As Exception
                        V_QTYUNIT = ""
                    End Try
                    Try
                        V_UNITTYPE = MyReader.GetString("type_code")
                    Catch ex As Exception
                        V_UNITTYPE = ""
                    End Try

                    xCek = xCek + 1
                End While
                CloseMyReader(MyReader, UserData)

                If (xCek = 1 And ((V_DAY > 0 And V_UNITTYPE <> "4") Or (V_UNITTYPE = "4"))) Then

                    If (V_DAY > 0 And V_UNITTYPE <> "4") Then
                        'get all cost category sesuai type unitnya for non curah
                        strSQL_7 = _
                        "Select t1.costcat_code, t1.costcat_name, t1.subgroup_code, t1.collected, " & _
                        "IF(t1.reference_field IS NULL,'',t1.reference_field) AS reference_field, t1.vat, t1.inc_freetime From tbm_costcategory t1, tbm_costcategory_subgroup t2 " & _
                        "Where t1.subgroup_code= t2.subgroup_code and t2.group_code='00001' and t1.active=1 " & _
                        "AND (t1.costcat_code IN  " & _
                        "    (SELECT DISTINCT t1.costcat_code FROM tbm_costrates t1, tbm_unit t2 " & _
                        "     WHERE t1.unit_code=t2.unit_code AND (t2.type_code='" & V_UNITTYPE & "' OR t2.type_code='1')) OR t1.collected = '-') " & _
                        "order by t1.costcat_name"
                    Else
                        ' for curah
                        strSQL_7 = _
                        "Select t1.costcat_code, t1.costcat_name, t1.subgroup_code, t1.collected, " & _
                        "IF(t1.reference_field IS NULL,'',t1.reference_field) AS reference_field, t1.vat, t1.inc_freetime From tbm_costcategory t1, tbm_costcategory_subgroup t2 " & _
                        "Where t1.subgroup_code= t2.subgroup_code and t2.group_code='00001' and t1.active=1 " & _
                        "AND (t1.costcat_code IN  " & _
                        "    (SELECT DISTINCT t1.costcat_code FROM tbm_costrates_curah t1, tbm_unit t2 " & _
                        "     WHERE t1.unit_code=t2.unit_code AND (t2.type_code='" & V_UNITTYPE & "' OR t2.type_code='1')) OR t1.collected = '-') " & _
                        "order by t1.costcat_name"
                    End If

                    errMSg = "Failed when read data"
                    RstQ7 = DBQueryDataReader(strSQL_7, MyConn, errMSg, UserData)

                    Dim v_costcat_code, v_costcat_name, v_costcat_collect, v_costcat_ref, V_REFSTR As String
                    Dim V_DAYCEK, V_DAYRATE, V_DAYRATE2, V_LEN As Integer
                    Dim V_REFVAL As Decimal
                    Dim V_REFTYPE, v_incf, v_last_costcat_name, v_tmp_costcat_name As String
                    Dim xPos, z As Integer

                    'Free Time bisa mempengaruhi days jika dalam tabe master nilai (DAYS_INC_FTD) nya "Y"
                    'V_DAYCEK = V_DAY
                    V_DAY2_FTD = 0
                    If V_FREETIME < V_DAY2 Then V_DAY2_FTD = V_DAY2 - V_FREETIME

                    xFirst = 0
                    v_last_costcat_name = ""
                    listError.Items.Clear()

                    While RstQ7.Read

                        v_costcat_code = RstQ7.GetString(0)
                        v_costcat_name = RstQ7.GetString(1)
                        v_costcat_collect = RstQ7.GetString(3)
                        v_costcat_ref = RstQ7.GetString(4)
                        v_vat = RstQ7.GetDecimal(5)
                        v_incf = RstQ7.GetString(6)

                        'Free Time bisa mempengaruhi days jika dalam tabe master nilai (DAYS_INC_FTD) nya "Y"
                        'Hitung pengurangan DayCek untuk category yang sama (asumsi penamaan cost category yang sama hanya di bedakan dgn keterangan dalam "( )"
                        v_tmp_costcat_name = "-"
                        V_LEN = InStr(v_costcat_name, "(")
                        If V_LEN > 0 Then v_tmp_costcat_name = Mid(v_costcat_name, 1, InStr(v_costcat_name, "(") - 1)

                        If (v_tmp_costcat_name <> v_last_costcat_name) Then
                            If v_incf = "N" Then
                                V_DAYCEK = V_DAY
                            Else
                                V_DAYCEK = V_DAY2_FTD
                            End If
                            If V_LEN > 0 Then v_last_costcat_name = v_tmp_costcat_name
                        End If


                        v_vatamount = 0
                        V_REFSTR = "Y"
                        V_REFVAL = 0

                        If v_costcat_ref <> "" Then
                            xPos = InStr(v_costcat_ref, ".")
                            If xPos = 1 Then
                                v_costcat_ref = Mid(v_costcat_ref, 2, Len(v_costcat_ref) - 1)
                                v_costcat_ref = Replace(v_costcat_ref, ":shipment_no", Ship)
                                strSQL = v_costcat_ref
                            Else
                                xPos = InStr(v_costcat_ref, ":")
                                If xPos = 1 Then v_costcat_ref = Mid(v_costcat_ref, 2, Len(v_costcat_ref) - 1)

                                strSQL = "Select " & v_costcat_ref & " From tbl_shipping where shipment_no='" & Ship & "'"
                            End If

                            RstQ6b = DBQueryDataReader(strSQL, MyConn, errMSg, UserData)
                            While RstQ6b.Read
                                If RstQ6b.IsDBNull(0) Then
                                    V_REFSTR = "N"
                                Else
                                    V_REFTYPE = RstQ6b.Item(0).GetType.ToString
                                    If V_REFTYPE = "System.DateTime" Then
                                        'V_REF = RstQ6b.GetDateTime(0) --- jika ada tgl berarti active
                                        V_REFSTR = "Y"
                                    ElseIf V_REFTYPE = "System.String" Then
                                        V_REFSTR = RstQ6b.GetString(0)
                                    ElseIf V_REFTYPE = "System.Double" Then
                                        V_REFVAL = RstQ6b.GetDouble(0)
                                        V_REFSTR = ""
                                    ElseIf V_REFTYPE = "System.Decimal" Then
                                        V_REFVAL = RstQ6b.GetDecimal(0)
                                        V_REFSTR = ""
                                    Else
                                        V_REFVAL = RstQ6b.GetValue(0)
                                        V_REFSTR = ""
                                    End If
                                End If
                            End While
                        End If

                        V_RATEBYCOST = 0
                        V_RATE = 0
                        V_RATEORG = 0
                        xCek = 0
                        'get size base on V_UNITTYPE
                        If V_UNITTYPE = "4" Then
                            'MsgBox("Curah")

                            V_SIZE = V_QTY

                            strSQL_6a = _
                                    "SELECT DISTINCT t1.unit_code FROM tbl_po_detail t1, tbl_shipping_detail t2 " & _
                                    "WHERE t1.po_no=t2.po_no AND t2.shipment_no='" & Ship & "'"

                            RstQ6a = DBQueryDataReader(strSQL_6a, MyConn, errMSg, UserData)
                            While RstQ6a.Read
                                V_UNITCODE = RstQ6a.GetString(0)
                                xCek = xCek + 1
                            End While

                            If xCek = 1 Then
                                If xFirst = 0 Then
                                    errMSg = ". . . Packed Curah - " & V_SIZE & " " & V_UNITCODE & " / Effective Date : " & V_SPPBDT
                                    listError.Items.Add(errMSg)
                                    xFirst = 1
                                End If

                                strSQL_A = _
                                    "Select tz.currency_code, tz.rate, tk.kurs_pajak, if(tz.days=0,0,(tz.days2-tz.days+1)) days, tz.unit_code, if(ue.rate IS NULL, 0, ue.rate) rate, if(tz.totrate_min IS NULL, 0, tz.totrate_min) totrate_min, tz.effective_dt From " & _
                                    "(Select * From " & _
                                    "(Select * From " & _
                                    "(Select * From " & _
                                    "(Select * From " & _
                                    "(Select * From " & _
                                    "(Select * From tbm_costrates_curah Where costcat_code='" & v_costcat_code & "' and effective_dt<='" & DTPrinted1 & "') t1 " & _
                                    "Where plant_code='" & V_PLANT & "' or plant_code='00000') t1 " & _
                                    "Where port_code='" & V_PORT & "' or port_code='00000') t1 " & _
                                    "Where supplier_code='" & V_SUPPLIER & "' or supplier_code='00000') t1 " & _
                                    "Where expedition_code='" & V_EXPEDITION & "' or expedition_code='00000') t1 " & _
                                    "Where material_group='" & V_MATGRP & "' or material_group='00000') tz " & _
                                    "left join tbm_kurs as tk on tz.currency_code = tk.currency_code and " & _
                                    "tk.effective_date = '" & Format(V_SPPBDT, "yyyy-MM-dd") & "' " & _
                                    "left join tbm_unit_equivalent as ue on ue.unit_code = '" & V_QTYUNIT & "' AND ue.unit_code_to = tz.unit_code " & _
                                    "Where (trim(tz.unit_code) = 'DOC' OR trim(tz.unit_code) = trim('" & V_UNITCODE & "') " & _
                                    "       OR TRIM(tz.unit_code) IN (SELECT unit_code FROM tbm_unit_equivalent WHERE unit_code_to = '" & V_QTYUNIT & "')) " & _
                                    "order by tz.plant_code desc, tz.port_code desc, tz.supplier_code desc, tz.expedition_code desc, tz.material_group desc, tz.effective_dt desc limit 1 "

                                RstQA = DBQueryDataReader(strSQL_A, MyConn, errMSg, UserData)

                                While RstQA.Read

                                    V_CURR = RstQA.GetString(0)
                                    V_RATE = RstQA.GetDecimal(1)
                                    V_DAYRATE = RstQA.GetInt64(3)
                                    V_UNITCODE2 = RstQA.GetString(4)
                                    V_RATEUNIT = RstQA.GetDecimal(5)
                                    V_RATE_MIN = RstQA.GetDecimal(6)

                                    'untuk yg memperhitungan rate per dokumen
                                    If (V_UNITCODE2 = "Y") Or (V_UNITCODE2 = "DOC") Then
                                        V_SIZE = 1

                                        'untuk yg memperhitungan quantity/weight barang bkn dari jumlah containernya 
                                    Else
                                        If V_UNITCODE2 <> V_UNITCODE Then
                                            If V_RATEUNIT = 0 Then
                                                errMSg = ". . . No Conversion Matrix for " & V_UNITCODE2 & " to " & V_UNITCODE
                                                listError.Items.Add(errMSg)
                                            End If
                                            V_SIZE = V_QTY * V_RATEUNIT
                                        End If
                                    End If

                                    V_RATEORG = V_RATE
                                    If V_CURR <> "IDR" And V_RATE > 0 Then
                                        Try
                                            V_KURS = RstQA.GetDecimal(2)
                                        Catch ex As Exception
                                            V_KURS = 1
                                            errMSg = ". . . No Rate for " & v_costcat_name & " (" & V_SPPBDT & ")"
                                            listError.Items.Add(errMSg)
                                        End Try
                                        If V_KURS = 0 Then
                                            V_KURS = 1
                                            errMSg = ". . . No Rate  for " & v_costcat_name & " (" & V_SPPBDT & ")"
                                            listError.Items.Add(errMSg)
                                        End If

                                        V_RATE = V_RATE * V_KURS
                                        V_RATE_MINBYCOST = V_RATE_MIN * V_KURS
                                    End If
                                    If V_REFSTR <> "N" Then
                                        ' untuk kondisi logical (Yes/No)
                                        If (V_REFSTR = "Y") Or (V_REFSTR = "NotCumulative") Then
                                            If v_costcat_collect = "N" Then 'N = cost di hitung per quantity
                                                If V_DAYRATE = 0 Then
                                                    V_RATEBYCOST = (V_RATE * V_SIZE)
                                                    'cek nilai minimum dari rate jika ada
                                                    If (V_RATEBYCOST < V_RATE_MINBYCOST And V_RATE_MIN > 0) Then
                                                        V_RATEBYCOST = V_RATE_MINBYCOST
                                                        errMSg = ". . . Minimum Rate for " & v_costcat_name & " = " & FormatNumber(V_RATE_MIN, 2)
                                                        listError.Items.Add(errMSg)
                                                    End If
                                                Else
                                                    If V_DAYCEK > 0 Then
                                                        If V_DAYCEK > V_DAYRATE Then
                                                            V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE * V_DAYRATE)
                                                            V_DAYCEK = V_DAYCEK - V_DAYRATE
                                                        Else
                                                            V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE * V_DAYCEK)
                                                            V_DAYCEK = 0
                                                        End If
                                                    End If
                                                End If

                                            Else 'Y = cost di hitung per BL atau dgn kata lain per dokumen (selain Y/N ada - = cost di input manual -> di proses curah)
                                                V_RATEBYCOST = V_RATE
                                            End If

                                        Else
                                            'untuk kondisi dari nilai tertentu
                                            V_RATEBYCOST = V_RATE * V_REFVAL
                                        End If

                                        v_vatamount = 0
                                        If v_vat > 0 Then
                                            v_vatamount = (V_RATEBYCOST * (v_vat / 100))
                                            v_vattotal = v_vattotal + v_vatamount
                                        End If
                                    End If
                                End While
                            Else
                                MsgBox("Different Unit Type. Please check unit of quantity on your document", MsgBoxStyle.Critical, "Error")
                            End If

                        Else
                            If Trim(cbCont.Text) <> "" And Trim(txtContNum.Text) <> "" Then
                                strSQL_6a = _
                                        "Select tsc.unit_code, sum(1) totunit, tu.type_code " & _
                                        "From tbl_shipping_cont as tsc, tbm_unit as tu " & _
                                        "Where tsc.shipment_no='" & Ship & "' " & _
                                        "and tsc.unit_code = tu.unit_code " & _
                                        "Group by tsc.unit_code"

                                If V_UNITTYPE = "2" Then
                                    'MsgBox("Stripping")

                                    '--- jumlah dan type stripping bisa di edit jadi bagain ini di tutup
                                    'V_SIZE = V_QTY
                                    'RstQ6a = DBQueryDataReader(strSQL_6a, MyConn, errMSg, UserData)
                                    'While RstQ6a.Read
                                    'V_UNITCODE = RstQ6a.GetString(0)
                                    'End While
                                    '-------------------------------------------------------------------

                                    arrUNITCODE = Split(Trim(cbCont.Text), ",")
                                    arrSIZE = Split(txtContNum.Text, ",")
                                    If UBound(arrUNITCODE) <> UBound(arrSIZE) Then
                                        MsgBox("Invalid Stripping Number and Size! ", MsgBoxStyle.Critical, "Warning")
                                        Exit Sub
                                    End If

                                    For z = 0 To UBound(arrUNITCODE)

                                        V_UNITCODE = arrUNITCODE(z)
                                        V_SIZE = arrSIZE(z)

                                        If xFirst = 0 Then
                                            'errMSg = ". . . Quantity " & V_UNITCODE & " - " & V_SIZE & " " & V_QTYUNIT & " / Effective Date : " & Format(V_SPPBDT, "dd-MM-yyyy")
                                            errMSg = ". . . Quantity " & V_QTY & " " & V_QTYUNIT & " in " & txtContNum.Text & " x " & Trim(cbCont.Text) & " / Effective Date : " & Format(V_SPPBDT, "dd-MM-yyyy")
                                            listError.Items.Add(errMSg)
                                            xFirst = 1
                                        End If

                                        'get rate from query A
                                        strSQL_A = _
                                        "Select tz.currency_code, tz.rate, tk.kurs_pajak, if(tz.days=0,0,(tz.days2-tz.days+1)) days, tz.unit_code, if(ue.rate IS NULL, 0, ue.rate) rate, if(tz.totrate_min IS NULL, 0, tz.totrate_min) totrate_min, tz.effective_dt From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From tbm_costrates Where costcat_code='" & v_costcat_code & "' and effective_dt<='" & DTPrinted1 & "') t1 " & _
                                        "Where plant_code='" & V_PLANT & "' or plant_code='00000') t1 " & _
                                        "Where port_code='" & V_PORT & "' or port_code='00000') t1 " & _
                                        "Where supplier_code='" & V_SUPPLIER & "' or supplier_code='00000') t1 " & _
                                        "Where expedition_code='" & V_EXPEDITION & "' or expedition_code='00000') t1 " & _
                                        "Where material_group='" & V_MATGRP & "' or material_group='00000') tz " & _
                                        "left join tbm_kurs as tk on tz.currency_code = tk.currency_code and " & _
                                        "tk.effective_date = '" & Format(V_SPPBDT, "yyyy-MM-dd") & "' " & _
                                        "left join tbm_unit_equivalent as ue on ue.unit_code = '" & V_QTYUNIT & "' AND ue.unit_code_to = tz.unit_code " & _
                                        "Where (trim(tz.unit_code) = 'DOC' OR trim(tz.unit_code) = trim('" & V_UNITCODE & "') " & _
                                        "       OR TRIM(tz.unit_code) IN (SELECT unit_code FROM tbm_unit_equivalent WHERE unit_code_to = '" & V_QTYUNIT & "')) " & _
                                        "and (size_min=0 or (" & GetNum(V_SIZE.ToString) & " between size_min and size_max)) " & _
                                        "order by tz.plant_code desc, tz.port_code desc, tz.supplier_code desc, tz.expedition_code desc, tz.material_group desc, tz.effective_dt desc limit 1 "

                                        RstQA = DBQueryDataReader(strSQL_A, MyConn, errMSg, UserData)

                                        While RstQA.Read

                                            V_CURR = RstQA.GetString(0)
                                            V_RATE = RstQA.GetDecimal(1)
                                            V_DAYRATE = RstQA.GetInt64(3)
                                            V_UNITCODE2 = RstQA.GetString(4)
                                            V_RATEUNIT = RstQA.GetDecimal(5)
                                            V_RATE_MIN = RstQA.GetDecimal(6)

                                            'untuk yg memperhitungan rate per dokumen
                                            If (V_UNITCODE2 = "Y") Or (V_UNITCODE2 = "DOC") Then
                                                V_SIZE = 1

                                                'untuk yg memperhitungan quantity/weight barang bkn dari jumlah containernya 
                                            Else
                                                If V_UNITCODE2 <> V_UNITCODE Then
                                                    If V_RATEUNIT = 0 Then
                                                        errMSg = ". . . No Conversion Matrix for " & V_UNITCODE2 & " to " & V_UNITCODE
                                                        listError.Items.Add(errMSg)
                                                    End If
                                                    V_SIZE = V_QTY * V_RATEUNIT
                                                End If
                                            End If

                                            V_RATEORG = V_RATE
                                            If V_CURR <> "IDR" And V_RATE > 0 Then
                                                Try
                                                    V_KURS = RstQA.GetDecimal(2)
                                                Catch ex As Exception
                                                    V_KURS = 1
                                                    errMSg = ". . . No Rate for " & v_costcat_name & " (" & V_SPPBDT & ")"
                                                    listError.Items.Add(errMSg)
                                                End Try
                                                If V_KURS = 0 Then
                                                    V_KURS = 1
                                                    errMSg = ". . . No Rate  for " & v_costcat_name & " (" & V_SPPBDT & ")"
                                                    listError.Items.Add(errMSg)
                                                End If

                                                V_RATE = V_RATE * V_KURS
                                                V_RATE_MINBYCOST = V_RATE_MIN * V_KURS
                                            End If
                                            'If V_REFSTR = "Y" Then
                                            If V_REFSTR <> "N" Then
                                                If v_costcat_collect = "N" Then 'N = cost di hitung per quantity
                                                    If V_DAYRATE = 0 Then
                                                        V_RATEBYCOST = (V_RATE * V_SIZE)
                                                        'cek nilai minimum dari rate jika ada
                                                        If (V_RATEBYCOST < V_RATE_MINBYCOST And V_RATE_MIN > 0) Then
                                                            V_RATEBYCOST = V_RATE_MINBYCOST
                                                            errMSg = ". . . Minimum Rate for " & v_costcat_name & " = " & FormatNumber(V_RATE_MIN, 2)
                                                            listError.Items.Add(errMSg)
                                                        End If
                                                    Else
                                                        If V_DAYCEK > 0 Then
                                                            If V_DAYCEK > V_DAYRATE Then
                                                                V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE * V_DAYRATE)
                                                                V_DAYCEK = V_DAYCEK - V_DAYRATE
                                                            Else
                                                                V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE * V_DAYCEK)
                                                                V_DAYCEK = 0
                                                            End If
                                                        End If
                                                    End If

                                                    If V_REFVAL > 0 Then
                                                        V_RATEBYCOST = V_RATEBYCOST * V_REFVAL
                                                    End If

                                                Else 'Y = cost di hitung per BL atau dgn kata lain per dokumen (selain Y/N ada - = cost di input manual -> di proses curah)
                                                    V_RATEBYCOST = V_RATE
                                                End If

                                                v_vatamount = 0
                                                If v_vat > 0 Then
                                                    v_vatamount = (V_RATEBYCOST * (v_vat / 100))
                                                    v_vattotal = v_vattotal + v_vatamount
                                                End If
                                            End If
                                        End While
                                    Next
                                Else
                                    'MsgBox("CONTAINER")
                                    '--- di ganti loop di bawah ' While z <= jmlCont
                                    'RstQ6a = DBQueryDataReader(strSQL_6a, MyConn, errMSg, UserData)
                                    'While RstQ6a.Read
                                    '-------------------------------------------------------------------

                                    '--- jumlah dan type container bisa di edit jadi bagain ini di tutup
                                    'V_UNITCODE = RstQ6a.GetString(0)
                                    'V_SIZE = CDbl(RstQ6a.GetDecimal(1))
                                    '-------------------------------------------------------------------

                                    arrUNITCODE = Split(Trim(cbCont.Text), ",")
                                    arrSIZE = Split(txtContNum.Text, ",")
                                    If UBound(arrUNITCODE) <> UBound(arrSIZE) Then
                                        MsgBox("Invalid Container Number and Size! ", MsgBoxStyle.Critical, "Warning")
                                        Exit Sub
                                    End If

                                    For z = 0 To UBound(arrUNITCODE)


                                        V_UNITCODE = arrUNITCODE(z)
                                        V_SIZE = arrSIZE(z)

                                        If xFirst = 0 Then
                                            errMSg = ". . . Quantity " & V_QTY & " " & V_QTYUNIT & " in " & txtContNum.Text & " x " & Trim(cbCont.Text) & " / Effective Date : " & Format(V_SPPBDT, "dd-MM-yyyy")
                                            listError.Items.Add(errMSg)
                                            xFirst = 1
                                        End If

                                        'get rate from query A
                                        strSQL_A = _
                                        "Select tz.currency_code, tz.rate, tk.kurs_pajak, if(tz.days=0,0,(tz.days2-tz.days+1)) days, tz.unit_code, if(ue.rate IS NULL, 0, ue.rate) rate, if(tz.totrate_min IS NULL, 0, tz.totrate_min) totrate_min, tz.effective_dt From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From " & _
                                        "(Select * From tbm_costrates Where costcat_code='" & v_costcat_code & "' and effective_dt<='" & DTPrinted1 & "') t1 " & _
                                        "Where plant_code='" & V_PLANT & "' or plant_code='00000') t1 " & _
                                        "Where port_code='" & V_PORT & "' or port_code='00000') t1 " & _
                                        "Where supplier_code='" & V_SUPPLIER & "' or supplier_code='00000') t1 " & _
                                        "Where expedition_code='" & V_EXPEDITION & "' or expedition_code='00000') t1 " & _
                                        "Where material_group='" & V_MATGRP & "' or material_group='00000') tz " & _
                                        "left join tbm_kurs as tk on tz.currency_code = tk.currency_code and " & _
                                        "tk.effective_date = '" & Format(V_SPPBDT, "yyyy-MM-dd") & "' " & _
                                        "left join tbm_unit_equivalent as ue on ue.unit_code = '" & V_QTYUNIT & "' AND ue.unit_code_to = tz.unit_code " & _
                                        "Where (trim(tz.unit_code) = 'DOC' OR trim(tz.unit_code) = trim('" & V_UNITCODE & "') " & _
                                        "OR TRIM(tz.unit_code) IN (SELECT unit_code FROM tbm_unit_equivalent WHERE unit_code_to = '" & V_QTYUNIT & "')) " & _
                                        "and (size_min=0 or (" & GetNum(V_SIZE.ToString) & " between size_min and size_max)) " & _
                                        "order by tz.plant_code desc, tz.port_code desc, tz.supplier_code desc, tz.expedition_code desc, tz.material_group desc, tz.effective_dt desc limit 1 "

                                        ''"and (size_min=0 or (" & Replace(V_SIZE.ToString, ",", ".") & " between size_min and size_max)) " & _
                                        'dalam query ambil konversi rate, cek jika perlu konversi V_UNITCODE <> tz.unit_code maka V_Size di ambil dari rate ini
                                        RstQA = DBQueryDataReader(strSQL_A, MyConn, errMSg, UserData)

                                        While RstQA.Read
                                            V_CURR = RstQA.GetString(0)
                                            V_RATE = RstQA.GetDecimal(1)
                                            V_DAYRATE = RstQA.GetInt64(3)
                                            V_UNITCODE2 = RstQA.GetString(4)
                                            V_RATEUNIT = RstQA.GetDecimal(5)
                                            V_RATE_MIN = RstQA.GetDecimal(6)

                                            'untuk yg memperhitungan rate per dokumen
                                            If (V_UNITCODE2 = "Y") Or (V_UNITCODE2 = "DOC") Then
                                                V_SIZE = 1

                                                'untuk yg memperhitungan quantity/weight barang bkn dari jumlah containernya 
                                            Else
                                                If (V_UNITCODE2 <> V_UNITCODE) Then
                                                    If V_RATEUNIT = 0 Then
                                                        errMSg = ". . . No Conversion Matrix for " & V_UNITCODE2 & " to " & V_UNITCODE
                                                        listError.Items.Add(errMSg)
                                                    End If
                                                    V_SIZE = V_QTY * V_RATEUNIT
                                                End If
                                            End If

                                            V_RATEORG = V_RATE
                                            If V_CURR <> "IDR" And V_RATE > 0 Then
                                                Try
                                                    V_KURS = RstQA.GetDecimal(2)
                                                Catch ex As Exception
                                                    V_KURS = 1
                                                    errMSg = ". . . No Rate for " & v_costcat_name & " (" & V_SPPBDT & ")"
                                                    listError.Items.Add(errMSg)
                                                End Try

                                                If V_KURS = 0 Then
                                                    V_KURS = 1
                                                    errMSg = ". . . No rate for " & v_costcat_name & " (" & V_SPPBDT & ")"
                                                    listError.Items.Add(errMSg)
                                                End If

                                                V_RATE = V_RATE * V_KURS
                                                V_RATE_MINBYCOST = V_RATE_MIN * V_KURS
                                            End If

                                            'If V_REFSTR = "Y" Then
                                            If V_REFSTR <> "N" Then
                                                If v_costcat_collect = "N" Then 'N = cost di hitung per quantity
                                                    If V_DAYRATE = 0 Then
                                                        If V_REFSTR = "NotCumulative" Then
                                                            V_RATEBYCOST = V_RATE * V_SIZE
                                                        Else
                                                            V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE)
                                                        End If

                                                        'cek nilai minimum dari rate jika ada
                                                        If (V_RATEBYCOST < V_RATE_MINBYCOST And V_RATE_MIN > 0) Then
                                                            V_RATEBYCOST = V_RATE_MINBYCOST
                                                            errMSg = ". . . Minimum Rate for " & v_costcat_name & " = " & FormatNumber(V_RATE_MIN, 2)
                                                            listError.Items.Add(errMSg)
                                                        End If
                                                    Else
                                                        If V_DAYCEK > 0 Then
                                                            If V_DAYCEK > V_DAYRATE Then
                                                                V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE * V_DAYRATE)
                                                                V_DAYCEK = V_DAYCEK - V_DAYRATE
                                                            Else
                                                                V_RATEBYCOST = V_RATEBYCOST + (V_RATE * V_SIZE * V_DAYCEK)
                                                                V_DAYCEK = 0
                                                            End If
                                                        End If
                                                    End If
                                                    If V_REFVAL > 0 Then
                                                        V_RATEBYCOST = V_RATEBYCOST * V_REFVAL
                                                    End If

                                                Else 'Y = cost di hitung per BL atau dgn kata lain per dokumen (selain Y/N ada - = cost di input manual -> di proses curah)

                                                    'V_RATEBYCOST = V_RATEBYCOST + V_RATE
                                                    V_RATEBYCOST = V_RATE
                                                End If
                                            End If
                                            v_vatamount = 0
                                            If v_vat > 0 Then
                                                v_vatamount = (V_RATEBYCOST * (v_vat / 100))
                                                v_vattotal = v_vattotal + v_vatamount
                                            End If
                                        End While
                                    Next
                                    'End While
                                End If
                            End If
                        End If
                        '-------------
                        'STORE KE GRID
                        V_NOURUT = V_NOURUT + 1
                        tempVar = New String() {V_NOURUT, v_costcat_code, v_costcat_name, V_CURR, FormatNumber(V_RATEORG, 2), FormatNumber(V_RATEBYCOST, 2), FormatNumber(v_vatamount, 2)}
                        dgvcost.Rows.Add(tempVar)
                        v_total = v_total + V_RATEBYCOST

                    End While
                    dgvcost.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    dgvcost.Columns(3).DefaultCellStyle.Format = "N2"

                    total.Text = FormatNumber(v_total, 2)
                    tVAT.Text = FormatNumber(v_vattotal, 2)
                    ttotal.Text = v_total + v_vattotal
                    ttotal.Text = FormatNumber(ttotal.Text, 2)

                    If V_KURS > 1 Then
                        errMSg = ". . . Tax Rate " & FormatNumber(V_KURS, 2)
                        listError.Items.Add(errMSg)
                        'listError.Visible = True
                        'Me.Height = 518
                    End If
                    If V_DAY > 0 Then
                        errMSg = ". . . Penumpukan " & V_DAY & " days / Free Time " & V_FREETIME
                        listError.Items.Add(errMSg)
                    End If
                Else
                    If V_UNITTYPE <> "4" Then
                        MsgBox("Data not completed. Please check ETA and Delivery date  on your document", MsgBoxStyle.Critical, "Warning")
                    Else
                        MsgBox("Data not completed. Please check Lead Time and Total Equipment on your document", MsgBoxStyle.Critical, "Warning")
                    End If
                End If
            End If
        Else
            MsgBox("Creating Clerance Cost is canceled. Data not completed, please check plant, port, supplier, expedition, date, material, quantity, container and unit group", MsgBoxStyle.Critical, "Warning")
            'If V_UNITTYPE <> "4" Then
            'MsgBox("Creating Clerance Cost is canceled. Please check ETA and Delivery date on your document", MsgBoxStyle.Critical, "Warning")
            'Else
            'MsgBox("Creating Clerance Cost is canceled. Please check Lead Time and Total Equipment on your document", MsgBoxStyle.Critical, "Warning")
            'End If
        End If
    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("Fin")
    End Sub

    Private Sub VAT_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        If IsNumeric(tVAT.Text) = False Then
            MsgBox("Invalid VAT, input numeric value")
            tVAT.Text = 0
        End If
    End Sub

    Private Sub fillcbFor()
        Dim strSQL, errMSg, temp As String

        strSQL = "SELECT distinct t3.plant_code, m3.plant_name, m1.group_code, m2.group_name " & _
                 "FROM tbl_shipping t0, tbl_shipping_detail t1, tbl_po t2, tbl_shipping_plant t3, tbm_material m1, tbm_material_group m2, tbm_plant m3 " & _
                 "WHERE t0.shipment_no=t1.shipment_no AND t1.po_no = t2.po_no " & _
                 "AND t1.shipment_no=t3.shipment_no AND t1.po_no = t3.po_no " & _
                 "AND t3.plant_code = m3.plant_code AND t1.material_code=m1.material_code " & _
                 "AND m1.group_code=m2.group_code AND t1.shipment_no='" & Ship & "' " & _
                 "AND CONCAT(TRIM(t3.plant_code), TRIM(m1.group_code)) NOT IN " & _
                 " (SELECT CONCAT(TRIM(findoc_no),TRIM(findoc_reff)) FROM tbl_shipping_doc sd " & _
                 "  WHERE findoc_type='CC' AND TRIM(findoc_no) <> '' AND TRIM(findoc_reff) <> '' AND findoc_status <> 'Rejected' AND sd.shipment_no='" & Ship & "') "

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        cbFor.Refresh()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = "[" & Trim(MyReader.GetString("plant_code")) & ":" & Trim(MyReader.GetString("group_code")) & "] " & Trim(MyReader.GetString("plant_name")) & " ... " & Trim(MyReader.GetString("group_name"))
                    cbFor.Items.Add(temp)
                    jmlCC = jmlCC + 1
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub dgvcost_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvcost.CellEndEdit
        Dim v_sub, v_vat, v_total As Double

        dgvcost.CommitEdit(DataGridViewDataErrorContexts.Commit)
        For i = 0 To dgvcost.RowCount - 1
            If dgvcost.Rows(i).Cells("ItemCost").Value Is Nothing Then
            Else
                v_sub = v_sub + CDbl(dgvcost.Rows(i).Cells("Amount").Value.ToString)
                v_vat = v_vat + CDbl(dgvcost.Rows(i).Cells("VAT").Value.ToString)
            End If
        Next
        total.Text = FormatNumber(v_sub, 2)
        tVAT.Text = FormatNumber(v_vat, 2)

        ttotal.Text = v_sub + v_vat
        ttotal.Text = FormatNumber(ttotal.Text, 2)

    End Sub
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        FinalApp = True
        Call btnSave_Click(sender, e)
    End Sub

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

    Private Sub cbFor_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFor.SelectedIndexChanged

    End Sub

    Private Sub DTPrinted_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTPrinted.ValueChanged

    End Sub

    Private Sub cbCont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCont.SelectedIndexChanged
        Dim a As Integer

        If MultiCont Then
            If cbCont.SelectedIndex > (arrCNo.Length - 1) Then
                txtContNum.Text = strCNo
            Else
                txtContNum.Text = arrCNo(cbCont.SelectedIndex)
            End If
        End If
    End Sub

    Private Sub dgvcost_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvcost.CellContentClick

    End Sub
End Class