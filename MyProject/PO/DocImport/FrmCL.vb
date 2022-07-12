'Title                         : Transaksi CL
'Form                          : FrmCL
'Created By                    : Hanny
'Created Date                  : 09 Feb 2009
'Table Used                    : tbl_shipping_doc, tbl_shipping_doc_detail, 
'                                tbl_docsupl, tbm_expedition

'Stored Procedure Used (MySQL) : SaveCL
Public Class FrmCL
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
        'Dim ttl As Decimal
        'Dim lcno As String

        MODULE_CODE = "CL"
        MODULE_NAME = "Cover Letter "

        InitializeComponent()
        'DIno.Text = ""
        txtCity_Code.Text = ""
        txtExp.Text = ""
        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        CTApp.Text = ""
        AppDt.Value = GetServerDate()
        AppDt.Checked = False
        'findt.Text = Mid(Now.ToString, 1, 10)


        Ship = ShipNo
        'DIno.Text = ""

        'Call GetButtonAccess()

        If Trim(NO) <> "" Then
            Call DisplayData(NO)
            refreshGrid(Ship, NONum)
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
            Call GetExistedDocument()
        End If
    End Sub
    Private Sub GetExistedDocument()
        Dim dts As DataTable
        Dim DocMaxShipNo, DocMaxOrdNo As String

        DocMaxShipNo = AmbilData("Shipment_No", "tbl_shipping_doc", "findoc_type='CL' and findoc_createdby='" & UserData.UserCT & "'")
        If DocMaxShipNo = "" Then DocMaxShipNo = "0"
        DocMaxOrdNo = AmbilData("Ord_No", "tbl_shipping_doc", "shipment_no=" & DocMaxShipNo & " and findoc_type='CL' and findoc_createdby='" & UserData.UserCT & "'")
        If DocMaxOrdNo = "" Then DocMaxOrdNo = "0"
        SQLstr = "Select t1.finddoc_no No,t1.doc_code DocCode, t2.doc_name DocName,t3.refer_val DocNo," & _
                 "'' DocDate, t1.doc_remark Remark " & _
                 "from tbl_shipping_doc_detail t1, vw_doc_value t2, vw_doc_value t3 " & _
                 "where t1.shipment_no=t2.shipment_no and t1.doc_code=t2.doc_code and t1.shipment_no= " & DocMaxShipNo & _
                 " and t1.ord_no=" & DocMaxOrdNo & " and t3.shipment_no=" & Ship & " and t3.doc_code=t1.doc_code"

        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        dgvCL.DataSource = dts
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
    Private Sub btnSearchDoc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDoc.Click
        Dim brs As Integer

        PilihanDlg.Text = "Select Document Code"
        PilihanDlg.LblKey1.Text = "Document Code"
        PilihanDlg.LblKey2.Text = "Document Name"
        PilihanDlg.SQLGrid = "SELECT DOC_CODE as DocumentCode, DOC_NAME as DocumentName, REFER_VAL as DocumentNo FROM vw_doc_value WHERE DOC_CODE LIKE 'CL%' and SHIPMENT_NO = '" & Ship & "'"
        PilihanDlg.SQLFilter = "SELECT DOC_CODE as DocumentCode, DOC_NAME as DocumentName, REFER_VAL as DocumentNo FROM vw_doc_value " & _
                               "WHERE DOC_CODE LIKE 'CL%' and SHIPMENT_NO = '" & Ship & "' and DOC_CODE LIKE 'FilterData1%' and DOC_NAME LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_document"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            v_doccode = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            V_DOCNAME = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            V_DOCNO = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString

            brs = dgvCL.CurrentCell.RowIndex
            dgvCL.Rows(brs).Cells("DocCode").Value = v_doccode
            dgvCL.Rows(brs).Cells("DocName").Value = V_DOCNAME
            dgvCL.Rows(brs).Cells("DocNo").Value = V_DOCNO
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
        'strSQL = " select a.*,b.* from tbl_budget as a" & _
        '         " inner join tbm_bank as b on a.bank_code=b.bank_code " & _
        '         " where a.po_no = '" & PO & "' and a.ord_no='" & num & "' " & _
        '         " and a.type_code = 'BP' "
        DTPrinted.Enabled = False
        txtCity_Code.ReadOnly = True
        txtExp.ReadOnly = True
        txtAuthorized.ReadOnly = True
        'approvedby.ReadOnly = True
        'crt.ReadOnly = True
        AppDt.Enabled = False
        crtdt.ReadOnly = True
        'btnSearchCity.Visible = False
        'btnSearchExp.Visible = False
        'Button3.Visible = False

        errMSg = "Failed when read data"
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
                    tempExp = MyReader.GetString("FINDOC_TO")
                    CTCrt.Text = MyReader.GetString("FINDOC_CREATEDBY")
                    crtdt.Text = MyReader.GetString("FINDOC_CREATEDDT")
                    'CTApp.Text = MyReader.GetString("")
                    'AppDt.Value = MyReader.GetString("")

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


                    'CTFin.Text = MyReader.GetString("FINDOC_FINAPPBY")
                    Status.Text = MyReader.GetString("FINDOC_STATUS")
                    txtCity_Code.ReadOnly = f_getread(Status.Text)
                    txtExp.ReadOnly = f_getread(Status.Text)
                    txtAuthorized.ReadOnly = f_getread(Status.Text)
                    'approvedby.ReadOnly = f_getread(Status.Text)
                    'approvedby.Enabled = f_getenable(Status.Text)
                    'crt.ReadOnly = f_getread(Status.Text)
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
            'financeappby.Text = GetName2(CTFin.Text)
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
    Private Sub FrmCL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            'Else
            'ModCode = "SK-A"
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
                'ElseIf sender Is "Fun" Then
                '    financeappby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
            Call Name_Data(sender)
        End If
    End Sub
    Private Sub Name_Data(ByVal sender As String)
        Dim strSQL, errMSg As String

        If MODULE_CODE = "CL" Then
            ModCode = "CL-A"
            'Else
            'ModCode = "SK-A"
        End If

        If sender = "App" Then
            strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                     "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' and tu.name='" & approvedby.Text & "'"
        Else
            'strSQL = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
            '         "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C' and tu.name='" & financeappby.Text & "'"
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
                        '    CTFin.Text = MyReader.GetString("user_ct")
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
        If MODULE_CODE = "CL" Then
            ModCode = "CL-A"
            'Else
            'ModCode = "SK-A"
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
        'Dim transaction As MySqlTransaction
        'Dim AppDt,  SQLStr, FinDt,str1, str2, str3, str4 As String
        Dim AppDt1, SQLStr, DTPrinted1 As String
        'Dim num1, num2, num3, num4 As Decimal
        Dim hasil As Boolean = False
        Dim keyprocess As String = ""
        Dim insertStr As String = ""
        Dim lv_doccode, lv_docno, lv_remark As String
        Dim lv_docdt As String
        Dim lv_no As String
        Dim lv_nourut As String
        Dim counter As String

        If CekData() = False Then Exit Sub
        If Not CekInputTgl(CTApp, AppDt, "Approved Date") Then Exit Sub        

        Try
            'OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
            AppDt1 = Format(AppDt.Value, "yyyy-MM-dd")
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")
            'findt = Format(findt.ToString, "yyyy-MM-dd")
            'num1 = GetNum(TotalAmount.Text)
            'num2 = GetNum(deposit.Text)
            'num3 = GetNum(commision.Text)
            'num4 = GetNum(charge.Text)

            'str1 = Replace(num1, ",", ".")
            'str2 = Replace(num2, ",", ".")
            'str3 = Replace(num3, ",", ".")
            'str4 = Replace(num4, ",", ".")
            If MODULE_CODE = "CL" Then
                SQLStr = "Run Stored Procedure SaveCL (" & btnSave.Text & ","
                If btnSave.Text = "Save" Then
                    keyprocess = "Save"
                ElseIf btnSave.Text = "Update" Then
                    keyprocess = "Updt"
                End If
            End If
            dgvCL.CommitEdit(DataGridViewDataErrorContexts.Commit)
            counter = 0
            For i = 0 To dgvCL.RowCount - 1
                ErrMsg = "Failed to update CL detail data."
                If dgvCL.Rows(i).Cells("DocCode").Value Is Nothing Then
                Else
                    LV_NO = dgvCL.Rows(i).Cells("No").Value.ToString
                    Try
                        'lv_poitem = DGVDetail.Rows(i).Cells(4).Value.ToString
                        lv_doccode = dgvCL.Rows(i).Cells("DocCode").Value.ToString
                    Catch ex As Exception
                        lv_doccode = ""
                    End Try
                    If lv_doccode <> "CL000" Then
                        counter = counter + 1
                        lv_nourut = counter & "."
                    Else
                        lv_nourut = ""
                    End If
                    Try
                        'lv_matcode = DGVDetail.Rows(i).Cells(5).Value.ToString
                        lv_docno = dgvCL.Rows(i).Cells("DocNo").Value.ToString
                    Catch ex As Exception
                        lv_docno = ""
                    End Try
                    Try
                        'lv_origin = DGVDetail.Rows(i).Cells(7).Value.ToString

                        lv_docdt = Format(CDate(dgvCL.Rows(i).Cells("DocDate").Value.ToString), "yyyy-MM-dd")
                    Catch ex As Exception
                        lv_docdt = ""
                    End Try
                    Try
                        'lv_hsno = DGVDetail.Rows(i).Cells(9).Value.ToString
                        lv_remark = dgvCL.Rows(i).Cells("Remark").Value.ToString
                    Catch ex As Exception
                        lv_remark = ""
                    End Try
                    lv_no = Mid(lv_no & "  ", 1, 2)
                    lv_doccode = Mid(lv_doccode & "     ", 1, 5)
                    lv_docno = Mid(lv_docno & "                                        ", 1, 40)
                    lv_docdt = Mid(lv_docdt & "          ", 1, 10)
                    lv_remark = Mid(lv_remark & Space(255), 1, 255)
                    lv_nourut = Mid(lv_nourut & "   ", 1, 3)
                    insertStr &= lv_no & lv_doccode & lv_docno & lv_docdt & lv_remark & lv_nourut & ";"
                End If
            Next

            MyComm.CommandText = "SaveCL"
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
            SQLStr = SQLStr & "," & insertStr & ")"
            MyComm.Parameters.AddWithValue("InsertStr", insertStr)
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

        'v_pono = txtpono.ToString
        If Len(CStr(num)) = 1 Then
            v_num = " " & num
        Else
            v_num = num.ToString
        End If
        If MODULE_CODE = "CL" Then
            ViewerFrm.Tag = "CLLL" & v_num & Ship
            'Else
            '    ViewerFrm.Tag = "SKKK" & v_num & Ship
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
    Private Sub refreshGrid(ByVal v_no As String, ByVal v_ord As String)
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts As DataTable

        in_field = "tsdd.finddoc_no as No,tsdd.Doc_Code as DocCode,td.Doc_Name as DocName,trim(tsdd.Doc_No) as DocNo,date_format(tsdd.Doc_dt,'%d-%m-%Y') AS DocDate,trim(tsdd.doc_remark) as Remark"
        in_tbl = "tbl_shipping_doc_detail as tsdd inner join tbm_document as td on tsdd.doc_code = td.doc_code"
        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tsdd.shipment_no = '" & v_no & "' and tsdd.ord_no = '" & v_ord & "'"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        dgvCL.DataSource = dts
        'If dts. > 0 Then
        'Show_Grid_JoinTable(DGVDetail, in_field, in_tbl)
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then

            'dgvCL.Rows(0).Cells("doc_date").Value = 1
            'DGVDetail.Columns("po_no").Visible = True
            'DGVDetail.Columns("shipment_no").Visible = True
        End If
    End Sub

    Private Sub dgvCL_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvCL.CellClick
        If dgvCL.Columns(e.ColumnIndex).Name = "btnDoc" Then
            dgvCL.Rows(e.RowIndex).Cells("no").Value = e.RowIndex + 1
            Call btnSearchDoc_Click("", e)
            'dgvCL.Rows(e.RowIndex).Cells("DocCode").Value = v_doccode
            'dgvCL.Rows(e.RowIndex).Cells("DocName").Value = V_DOCNAME
            'dgvCL.Rows(e.RowIndex).Cells("DocNo").Value = V_DOCNO
        End If
        'If dgvCL.Columns(e.ColumnIndex).Name = "Doc_Date" Then
        '    Call btnGetDt_Click("", e)
        '    dgvCL.Rows(e.RowIndex).Cells("Doc_Code").Value = v_doccode
        '    dgvCL.Rows(e.RowIndex).Cells("Doc_Name").Value = V_DOCNAME
        'End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Test"
        PilihanDlg.LblKey1.Text = "Test"
        PilihanDlg.SQLGrid = "call cetakDocBL('1','1')"
        'PilihanDlg.SQLFilter = "SELECT city_code as CityCode, city_name as CityName, country_code as CountryCode FROM tbm_city " & _
        '                       "WHERE city_code LIKE '%FilterData1%' "
        PilihanDlg.Tables = "call cetakDocBL('1','1')"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'txtCity_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function
End Class