Imports poim.FrmDOC_Import
Public Class FRMRILQuota
    Dim v_type As String
    Dim NomUrut As Integer
    Dim MyConn2 As MySqlConnection

    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim MyDataReader As DataTableReader
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim v_matcode, v_matdesc, v_hscode, v_origin_code, v_origin_name As String
    Dim x As Integer
    Dim currentdate As Date
    Private Sub FrmAdmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        edit = False : baru = False
        _Clear()
        fillcbDG()
    End Sub
    Private Sub refreshGrid(ByVal vpo_no As String)
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts As DataTable
        Dim strSQLu, strSQLp As String
        Dim cbu As New DataGridViewComboBoxColumn
        Dim cbp As New DataGridViewComboBoxColumn
        Dim cbtm, cbto As New DataGridViewButtonColumn
        Dim dtsu, dtsp As DataTable

        DGVDetail.DataSource = Nothing
        DGVDetail.Columns.Clear()
        DGVDetail.Rows.Clear()
        in_field = "dpo.material_code,dpo.material_code as btnMat, tmat.MATERIAL_name, dpo.country_code,dpo.country_code as btnOrigin,tcou.COUNTRY_NAME, " & _
                   "dpo.quantity,dpo.unit_code, dpo.package_code, dpo.price"

        in_tbl = "tbl_ril_quota as tpo inner join tbl_ril_quota_detail as dpo on " & _
                 "tpo.ril_no = dpo.ril_no inner join tbm_material as tmat on dpo.material_code = tmat.material_code " & _
                 "inner join tbm_country as tcou on dpo.country_code = tcou.country_code "

        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tpo.ril_no = '" & vpo_no & "'"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DGVDetail.DataSource = dts

        ErrMsg = "tbm_unit data view failed"
        strSQLu = "select UNIT_CODE from tbm_unit"
        dtsu = DBQueryDataTable(strSQLu, MyConn, "", ErrMsg, UserData)
        With cbu
            .DataSource = dtsu
            .HeaderText = "Unit Code"
            .DataPropertyName = "unit_code"
            .DisplayMember = "UNIT_CODE"
            .ValueMember = "UNIT_CODE"
            .Width = 70
        End With
        'Combo Box Document
        ErrMsg = "tbm_packing data view failed"
        strSQLp = "select PACK_CODE from tbm_packing"
        dtsp = DBQueryDataTable(strSQLp, MyConn, "", ErrMsg, UserData)
        With cbp
            .HeaderText = "Pack Code"
            .DataSource = dtsp
            .DataPropertyName = "package_code"
            .DisplayMember = "PACK_CODE"
            .ValueMember = "PACK_CODE"
            .Width = 70
        End With
        With cbtm
            .DataPropertyName = "btnMatz"
            .HeaderText = "SrchMat"
            .Width = 15
            .DefaultCellStyle.BackColor = Color.LightGray
            .Text = "..."
        End With
        With cbto
            .DataPropertyName = "btnOriginz"
            .HeaderText = "SrchOrigin"
            .Width = 15
            .DefaultCellStyle.BackColor = Color.LightGray
            .Text = "..."
        End With


        DGVDetail.Columns.Insert(1, cbtm)
        DGVDetail.Columns.Insert(5, cbto)
        DGVDetail.Columns.Insert(9, cbu)
        DGVDetail.Columns.Insert(11, cbp)

        DGVDetail.Columns(2).Visible = False
        DGVDetail.Columns(6).Visible = False
        DGVDetail.Columns(8).Visible = True
        DGVDetail.Columns(10).Visible = False
        DGVDetail.Columns(12).Visible = False

        DGVDetail.Columns(0).HeaderText = "Material Code"
        DGVDetail.Columns(0).Width = 100
        DGVDetail.Columns(3).HeaderText = "Material Name"
        DGVDetail.Columns(3).Width = 150
        DGVDetail.Columns(3).ReadOnly = True
        DGVDetail.Columns(4).HeaderText = "Country Code"
        DGVDetail.Columns(4).Width = 100
        DGVDetail.Columns(7).HeaderText = "Country Name"
        DGVDetail.Columns(7).ReadOnly = True
        DGVDetail.Columns(8).HeaderText = "Quantity"
        DGVDetail.Columns(13).HeaderText = "Unit Price"

        DGVDetail.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(8).DefaultCellStyle.Format = "N5"

        DGVDetail.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGVDetail.Columns(13).DefaultCellStyle.Format = "N5"
    End Sub
    Private Sub LeadTime()
        Dim selisih As Long
        Dim tg1, tg2 As Date

        If dtSubmited.Checked = False Or dtIssued.Checked = False Then txtLeadtime.Text = 0 : Exit Sub

        tg1 = CDate(dtSubmited.Text)
        tg2 = CDate(dtIssued.Text)

        selisih = DateDiff(DateInterval.Day, tg1, tg2)
        txtLeadtime.Text = FormatNumber(selisih, 0, , , TriState.True)
        If Val(txtLeadtime.Text) > 0 Then
            txtLeadtime.Text = (Val(txtLeadtime.Text) + 1).ToString
        Else
            txtLeadtime.Text = "0"
        End If
    End Sub
    Private Sub DGVDetail_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDetail.CellClick
        If txtRILno.Text <> "" Then
            If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
                If DGVDetail.Columns(e.ColumnIndex).HeaderText = "SrchMat" Then
                    Call btnSearchMat_Click("", e)
                    DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value = v_matcode '0
                    DGVDetail.Rows(e.RowIndex).Cells("Material_Name").Value = v_matdesc '3
                    DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value = ""
                    DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value = ""
                    DGVDetail.Rows(e.RowIndex).Cells("Quantity").Value = 0
                    DGVDetail.Rows(e.RowIndex).Cells("Price").Value = 0
                End If
                If DGVDetail.Columns(e.ColumnIndex).HeaderText = "SrchOrigin" Then
                    'cek initial
                    If DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value.ToString <> "" Then
                        v_origin_code = DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value.ToString   '4
                        v_origin_name = DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value.ToString   '7
                    End If
                    If DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value.ToString <> "" Then
                        Call btnSearchOri_Click(DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value, e)
                        DGVDetail.Rows(e.RowIndex).Cells("Country_Code").Value = v_origin_code  '4
                        DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value = v_origin_name  '7
                    Else
                        MsgBox("Please input Material Code", MsgBoxStyle.Information, "Error")
                    End If
                End If
            End If
            If e.ColumnIndex = 8 Or e.ColumnIndex = 9 Or e.ColumnIndex = 10 Or e.ColumnIndex = 11 Or e.ColumnIndex = 12 Or e.ColumnIndex = 13 Then
                If DGVDetail.Rows(e.RowIndex).Cells("Material_Code").Value.ToString = "" Or DGVDetail.Rows(e.RowIndex).Cells("Country_Name").Value.ToString = "" Then
                    MsgBox("Please input Material Code/Country Code", MsgBoxStyle.Information, "Error")
                End If
            End If
        Else
            MsgBox("Please fill in RIL number", MsgBoxStyle.Information, "Error") : txtRILno.Focus()
        End If
    End Sub
    Private Sub btnSearchMat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchMat.Click
        PilihanDlg.Text = "Select Material Code"
        PilihanDlg.Width = 600
        PilihanDlg.Height = 402
        PilihanDlg.DgvResult.Width = 570
        PilihanDlg.DgvResult.Height = 267
        PilihanDlg.LblKey1.Text = "Material Code"
        PilihanDlg.LblKey2.Text = "Material Name"
        PilihanDlg.SQLGrid = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                             "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                             "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
        PilihanDlg.SQLFilter = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                               "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                               "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code " & _
                               "WHERE tm.material_code LIKE 'FilterData1%' AND " & _
                               "tm.Material_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            v_matcode = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            v_matdesc = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub btnSearchOri_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchOri.Click
        PilihanDlg.Text = "Select Country Code"
        PilihanDlg.LblKey1.Text = "Country Code"
        PilihanDlg.LblKey2.Text = "Country Name"
        PilihanDlg.SQLGrid = "select tc.country_code as CountryCode, tc.country_name as CountryName from tbm_country as tc " & _
                             "inner join tbm_material_origin as tm on tm.country_code = tc.country_code " & _
                             "where tm.material_code = '" & sender.ToString & "'"
        PilihanDlg.SQLFilter = "select tc.country_code as CountryCode, tc.country_name as CountryName from tbm_country as tc " & _
                             "inner join tbm_material_origin as tm on tm.country_code = tc.country_code " & _
                             "where tm.material_code = '" & sender.ToString & "' and tm.country_code LIKE 'FilterData1%' and tc.country_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_country as tc inner join tbm_material_origin as tm on tm.country_code = tc.country_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            v_origin_code = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            v_origin_name = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim brs As Integer
        Dim totqty, dqty, totprc, dprc As Decimal
        Dim dtSubmited1, dtIssued1, appdate1, s_tolerable As String
        Dim mcode, ccode, ucode, pcode, doc_add As String
        Dim qty, prc As Double
        Dim v_tolerable As Decimal

        DGVDetail.CommitEdit(DataGridViewDataErrorContexts.Commit)
        dtSubmited1 = IIf(dtSubmited.Checked, Format(dtSubmited.Value, "yyyy-MM-dd"), "")
        dtIssued1 = IIf(dtIssued.Checked, Format(dtIssued.Value, "yyyy-MM-dd"), "")
        appdate1 = IIf(appdate.Checked, Format(appdate.Value, "yyyy-MM-dd"), "")

        Status.Text = IIf((appdate.Checked And Status.Text = "Open"), "Approved", IIf((Not appdate.Checked And Status.Text = "Approved"), "Open", Status.Text))

        '----------------Validasi Inputan----------------------------------------------------------------------
        If txtCompany_Code.Text = "" Then
            MsgBox("Company should be filled! ", MsgBoxStyle.Critical, "Warning")
            txtCompany_Code.Focus()
            Exit Sub
        End If
        If txtPort_Code.Text = "" Then
            MsgBox("Port should be filled! ", MsgBoxStyle.Critical, "Warning")
            txtPort_Code.Focus()
            Exit Sub
        End If
        If txtSupplier_Code.Text = "" Then
            MsgBox("Supplier should be filled! ", MsgBoxStyle.Critical, "Warning")
            txtSupplier_Code.Focus()
            Exit Sub
        End If
        If txtLoadPort_Code.Text = "" Then
            MsgBox("Load Port should be filled! ", MsgBoxStyle.Critical, "Warning")
            txtLoadPort_Code.Focus()
            Exit Sub
        End If
        Try
            s_tolerable = Replace(txtTolerable.Text, ",", ".")
            v_tolerable = CDec(s_tolerable)
        Catch ex As Exception
            MsgBox("Please enter numeric value for Tolerable! ", MsgBoxStyle.Critical, "Warning")
            txtTolerable.Focus()
            Exit Sub
        End Try
        If txtRILno.Text = "" Then
            MsgBox("No. RIL be filled! ", MsgBoxStyle.Critical, "Warning")
            txtRILno.Focus()
            Exit Sub
        End If
        If checkDetail() = False Then Exit Sub

        brs = DGVDetail.RowCount : totqty = 0
        For cnt = 1 To brs - 1
            dqty = CDec(DGVDetail.Item(8, cnt - 1).Value)
            If dqty = 0 Then
                MsgBox("At least one of RIL Quantity should be filled! ", MsgBoxStyle.Critical, "Warning")
                DGVDetail.Focus()
                DGVDetail.CurrentCell = DGVDetail(8, cnt - 1)
                Exit Sub
            End If
        Next

        If appcode.Text = "" Then appcode.Text = "0"
        SQLstr = "SELECT ril_no FROM tbl_ril_quota WHERE ril_no='" & txtRILno.Text & "'"
        ErrMsg = "Fail to check existing tbl_ril_quota."

        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
            SQLstr = "delete from tbl_ril_quota_detail where ril_no='" & txtRILno.Text & "'"
            ErrMsg = "Failed to Delete tbl_ril_quota_detail Quota Header data."
            If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
            SQLstr = "delete from tbl_ril_quota where ril_no='" & txtRILno.Text & "'"
            ErrMsg = "Failed to Delete tbl_ril_quota Quota Header data."
            If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        End If
        doc_add = GetDocAddress()
        SQLstr = "INSERT INTO tbl_ril_quota (ril_no, company_code, supplier_code, port_code,openingdt, " & _
                    "doc_address, createdby, createddt, approvedby, " & _
                    "approveddt, group_code,deptan_no,issueddt,submiteddt,remark,tolerable, status,loadport_code) " & _
                    "VALUES ('" & txtRILno.Text & "', '" & txtCompany_Code.Text & "', '" & txtSupplier_Code.Text & "', '" & txtPort_Code.Text & "', '" & Format(tgl.Value, "yyyy-MM-dd") & "', " & _
                    "'" & doc_add & "', '" & UserData.UserCT & "', '" & Format(currentdate, "yyyy-MM-dd") & "',IF('" & appdate1 & "'='',NULL,'" & appcode.Text & "'), IF('" & appdate1 & "'='',NULL,'" & appdate1 & "'), " & _
                    "'" & cbDG.Text & "','" & txtDeptanNo.Text & "',IF('" & dtIssued1 & "'='',NULL,'" & dtIssued1 & "'),IF('" & dtSubmited1 & "'='',NULL,'" & dtSubmited1 & "'), '" & txtRemark.Text & "','" & v_tolerable & "','" & Status.Text & "','" & txtLoadPort_Code.Text & "')"

        ErrMsg = "Failed to Add RIL Quota Header data."
        If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
        For cnt = 1 To brs - 1
            mcode = "" : ccode = "" : ucode = "" : qty = 0
            mcode = DGVDetail.Rows(cnt - 1).Cells("material_code").Value.ToString
            ccode = DGVDetail.Rows(cnt - 1).Cells("country_code").Value.ToString
            If (mcode <> "" And ccode <> "") Then
                qty = DGVDetail.Rows(cnt - 1).Cells("quantity").Value
                prc = DGVDetail.Rows(cnt - 1).Cells("price").Value
                ucode = DGVDetail.Rows(cnt - 1).Cells("unit_code").Value.ToString
                pcode = DGVDetail.Rows(cnt - 1).Cells("package_code").Value.ToString
                SQLstr = "INSERT INTO tbl_ril_quota_detail " & _
                         "(ril_no,ril_item,material_code,country_code,quantity,unit_code, package_code, price) " & _
                         "values ('" & txtRILno.Text & "','" & Format(cnt, "00") & "','" & mcode & "','" & ccode & "'," & qty & ",'" & ucode & "','" & pcode & "'," & prc & ")"
                ErrMsg = "Failed to Add RIL Quota Detail data."
                If DBQueryUpdate(SQLstr, MyConn, True, ErrMsg, UserData) < 0 Then Exit Sub
            End If
        Next
        MsgBox("Data saved!", MsgBoxStyle.Information, "Success") '_Clear()
    End Sub
    Private Function checkDetail() As Boolean
        For i As Integer = DGVDetail.Rows.GetFirstRow(DataGridViewElementStates.Visible) To DGVDetail.Rows.GetLastRow(DataGridViewElementStates.Visible) - 1
            If DGVDetail.Rows.Item(i).Cells.Item("Quantity").Value.ToString = "" Then
                MsgBox("Quantity should be filled", MsgBoxStyle.Information, "Warning")
                Return False
            End If
        Next
        Return True
    End Function
    Private Function GetDocAddress() As String
        SQLstr = "select line_text from tbm_gov_address where doc_type='RL' and group_code in ('" & cbDG.Text & "') group by line_no,line_text"
        ErrMsg = "Failed when read tbm_gov_address"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        GetDocAddress = ""
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If GetDocAddress <> "" Then GetDocAddress = GetDocAddress & Chr(13) & Chr(10)
                    GetDocAddress = GetDocAddress & MyReader.GetString("line_text")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Function
    Private Sub fillcbDG()
        Dim strSQL, errMSg, temp As String
        strSQL = "select group_code from tbm_cr_template where type_code='RLQ'"

        errMSg = "Failed when read template data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        cbDG.Refresh()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = MyReader.GetString("group_code")
                    cbDG.Items.Add(temp)

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'RL-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'RL-A'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then appcode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        appdate.Checked = True
    End Sub
    Private Sub appcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles appcode.TextChanged
        If appcode.Text <> "" Then appname.Text = AmbilData("name", "tbm_users", "user_ct=" & appcode.Text)
    End Sub
    Private Sub crtby_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crtby.TextChanged
        crtname.Text = AmbilData("name", "tbm_users", "user_ct=" & crtby.Text)
    End Sub
    Private Sub dtSubmited_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtSubmited.ValueChanged
        Call LeadTime()
    End Sub
    Private Sub dtIssued_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtIssued.ValueChanged
        Call LeadTime()
    End Sub

    Private Sub btnSearchCompany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCompany.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"
        PilihanDlg.SQLGrid = "select tc.company_code as CompanyCode, tc.company_name as CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                             "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "'"
        PilihanDlg.SQLFilter = "select tc.company_code as CompanyCode, tc.company_name as CompanyName from tbm_company as tc inner join tbm_users_company as tuc " & _
                               "on tc.COMPANY_CODE = tuc.COMPANY_CODE where tuc.USER_CT = '" & UserData.UserCT & "' " & _
                               "and tc.company_code LIKE 'FilterData1%' AND " & _
                               "tc.company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company as tc inner join tbm_users_company as tuc on tc.COMPANY_CODE = tuc.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub txtCompany_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany_Code.TextChanged
        If txtCompany_Code.Text = "" Then
            btnSearchPort.Enabled = False
        Else
            btnSearchPort.Enabled = True
        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        _Clear()
    End Sub
    Private Sub _Clear()
        clearscreen()
        RefreshScreen()
        refreshGrid("") : txtRILno.Focus()
    End Sub
    Private Sub clearscreen()
        txtRILno.Text = "" : Status.Text = "Open"
        txtCompany_Code.Text = "" : txtSupplier_Code.Text = "" : txtPort_Code.Text = "" : txtDeptanNo.Text = "" : txtLoadPort_Code.Text = ""
        lblCompany_Name.Text = "Company" : lblSupplier_Name.Text = "Supplier" : lblPort_Name.Text = "Port" : lblloadport.Text = "Load Port"
        dtSubmited.Checked = False : dtIssued.Checked = False : txtLeadtime.Text = "0" : txtRemark.Text = ""
        cbDG.Text = ""
        appname.Text = "" : appdate.Checked = False : crtby.Text = UserData.UserCT
        Status.Text = "Open"
        txtTolerable.Text = FormatNumber(0, 2, , , TriState.True)
    End Sub
    Private Sub RefreshScreen()
        btnSave.Enabled = True
        btnReject.Enabled = False
        txtRILno.Enabled = True
        txtRILno.Clear()
        btnSearchPort.Enabled = False
        baru = True
        edit = False
        txtRILno.ReadOnly = False
        txtRILno.Focus()
        currentdate = GetServerDate()
        tgl.Text = GetServerDate()
        crtdate.Text = GetServerDate()
        appdate.Value = GetServerDate()
        appdate.Checked = False
    End Sub

    Private Sub btnSearchPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPort.Click
        'Dim lv_cityplant As String
        Dim lv_countrycomp As String

        'lv_cityplant = AmbilData("city_code", "tbm_plant", "plant_code='" & txtPlant_Code.Text & "'")
        lv_countrycomp = AmbilData("t2.country_code", "tbm_city as t2 inner join tbm_company as t1", "t1.city_code = t2.city_code and t1.company_code ='" & txtCompany_Code.Text & "'")
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                             "where country_code = '" & lv_countrycomp & "'"
        PilihanDlg.SQLFilter = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                               "WHERE port_code LIKE 'FilterData1%' AND " & _
                                    "port_name LIKE 'FilterData2%' AND " & _
                                    "country_code = '" & lv_countrycomp & "'"
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPort_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub btnListRIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListRIL.Click
        PilihanDlg.Text = "Select RIL No"
        PilihanDlg.LblKey1.Text = "RIL No"
        PilihanDlg.LblKey2.Text = "Deptan No"

        PilihanDlg.SQLGrid = "select t1.ril_no as RilNo, t1.deptan_no as DeptanNo, t1.company_code as CompanyCode, t2.company_name as Company, t1.supplier_code as SupplierCode, t3.supplier_name as Supplier, t1.openingdt as DocDate, " & _
                             "t1.issueddt IssuedDate,t1.submiteddt SubmitedDate,t1.Remark,t1.Status " & _
                             "from tbl_ril_quota as t1, tbm_company as t2, tbm_supplier as t3 " & _
                             "where t1.company_code=t2.company_code AND t1.supplier_code=t3.supplier_code "
        PilihanDlg.SQLFilter = "select t1.ril_no as RilNo, t1.deptan_no as DeptanNo, t1.company_code as CompanyCode, t2.company_name as Company, t1.supplier_code as SupplierCode, t3.supplier_name as Supplier, t1.openingdt as DocDate, " & _
                               "t1.issueddt IssuedDate,t1.submiteddt SubmitedDate,t1.Remark,t1.Status " & _
                               "from tbl_ril_quota as t1, tbm_company as t2, tbm_supplier as t3 " & _
                               "where t1.company_code=t2.company_code AND t1.supplier_code=t3.supplier_code AND " & _
                               "t1.ril_no LIKE 'FilterData1%' AND " & _
                               "t1.deptan_no LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbl_ril_quota as t1, tbm_company as t2 where t1.COMPANY_CODE = t2.COMPANY_CODE"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            clearscreen()
            txtRILno.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txtCompany_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            lblCompany_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString
            txtSupplier_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(4).Value.ToString
            lblSupplier_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(5).Value.ToString
            tgl.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(6).Value.ToString
            refreshGrid(txtRILno.Text)
            edit = True : baru = False : btnReject.Enabled = True
            Display_Data()
        End If
    End Sub
    Private Sub Display_Data()
        Dim mcnt As Integer = 0
        SQLstr = "select t1.ril_no, t1.company_code, t1.supplier_code, t1.port_code,t1.openingdt, " & _
                "t1.doc_address, t1.createdby, t1.createddt, t1.approvedby, " & _
                "DATE_FORMAT(t1.approveddt,'%Y-%m-%d'), t1.group_code,t1.deptan_no,DATE_FORMAT(t1.issueddt,'%Y-%m-%d')," & _
                "DATE_FORMAT(t1.submiteddt,'%Y-%m-%d'),t1.remark,t1.status,t1.loadport_code,m1.name,t1.tolerable " & _
                "from tbl_ril_quota AS t1 " & _
                "LEFT JOIN tbm_users AS m1 ON m1.user_ct = t1.approvedby " & _
                "where ril_no='" & txtRILno.Text & "'"
        ErrMsg = "Failed when read RIL"
        MyDataReader = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)
        'MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyDataReader Is Nothing Then
            If MyDataReader.HasRows = True Then
                While MyDataReader.Read

                    Try
                        txtPort_Code.Text = MyDataReader.GetString(3)
                    Catch ex As Exception
                        txtPort_Code.Text = ""
                    End Try
                    Try
                        cbDG.Text = MyDataReader.GetString(10)
                    Catch ex As Exception
                        cbDG.Text = ""
                    End Try
                    Try
                        txtDeptanNo.Text = MyDataReader.GetString(11)
                    Catch ex As Exception
                        txtDeptanNo.Text = ""
                    End Try
                    Try
                        If MyDataReader.GetValue(13).ToString = "" Then
                            dtSubmited.Checked = False
                        Else
                            dtSubmited.Checked = True : dtSubmited.Text = MyDataReader.GetValue(13).ToString
                        End If
                    Catch ex As Exception
                        dtSubmited.Checked = False
                    End Try
                    Try
                        If MyDataReader.GetValue(12).ToString = "" Then
                            dtIssued.Checked = False
                        Else
                            dtIssued.Checked = True : dtIssued.Text = MyDataReader.GetValue(12).ToString
                        End If
                    Catch ex As Exception
                        dtIssued.Checked = False
                    End Try
                    Try
                        txtRemark.Text = MyDataReader.GetString(14)
                    Catch ex As Exception
                        txtRemark.Text = ""
                    End Try
                    Try
                        txtTolerable.Text = FormatNumber(MyDataReader.GetDecimal(18), 2)
                    Catch ex As Exception
                        txtTolerable.Text = FormatNumber(0, 2)
                    End Try
                    Try
                        Status.Text = MyDataReader.GetString(15)
                    Catch ex As Exception
                        Status.Text = ""
                    End Try
                    Try
                        crtby.Text = MyDataReader.GetValue(6)
                    Catch ex As Exception
                        crtby.Text = ""
                    End Try
                    Try
                        appcode.Text = MyDataReader.GetValue(8)
                    Catch ex As Exception
                        appcode.Text = ""
                    End Try
                    Try
                        appname.Text = MyDataReader.GetString(17)
                    Catch ex As Exception
                        appname.Text = ""
                    End Try
                    Try
                        crtdate.Text = MyDataReader.GetString(7)
                    Catch ex As Exception
                        crtdate.Text = ""
                    End Try
                    Try
                        If MyDataReader.GetValue(9).ToString = "" Then
                            appdate.Checked = False
                        Else
                            appdate.Checked = True : appdate.Text = MyDataReader.GetValue(9).ToString
                        End If
                    Catch ex As Exception
                        appdate.Checked = False
                    End Try
                    Try
                        txtLoadPort_Code.Text = MyDataReader.GetString(16)
                    Catch ex As Exception
                        txtLoadPort_Code.Text = ""
                    End Try
                End While
                txtRILno.ReadOnly = True
            End If
        End If
        btnSave.Enabled = (UserData.UserCT = crtby.Text) And (Status.Text <> "Rejected")
        btnReject.Enabled = btnSave.Enabled
    End Sub
    Private Sub txtRILno_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRILno.Validated
        SQLstr = "select ril_no from tbl_ril_quota " & _
                "where ril_no='" & txtRILno.Text & "'"
        ErrMsg = "Failed when read RIL"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If MyReader.HasRows Then
            baru = False : edit = True
            CloseMyReader(MyReader, UserData)
            refreshGrid(txtRILno.Text)
            Display_Data() : btnReject.Enabled = True
        Else
            CloseMyReader(MyReader, UserData)
            baru = True : edit = False
        End If
    End Sub
    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject Request Import Licence #" & txtRILno.Text & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_ril_quota " & _
                     "SET status='Rejected'" & _
                     " where ril_no='" & txtRILno.Text & "'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "Request Import Licence #" & txtRILno.Text & " has been rejected"
                MsgBox(msg)
            End If
            _Clear()
        End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        ViewerFrm.Tag = "RILQ" & Mid(txtRILno.Text & Space(40), 1, 40) & cbDG.Text
        ViewerFrm.ShowDialog()
    End Sub
    Private Sub btnSearchLoadPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchLoadPort.Click
        'Dim lv_cityplant As String
        Dim lv_countrycomp As String

        'lv_cityplant = AmbilData("city_code", "tbm_plant", "plant_code='" & txtPlant_Code.Text & "'")
        lv_countrycomp = AmbilData("t2.country_code", "tbm_city as t2 inner join tbm_company as t1", "t1.city_code = t2.city_code and t1.company_code ='" & txtCompany_Code.Text & "'")
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port "
        PilihanDlg.SQLFilter = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port " & _
                               "WHERE port_code LIKE 'FilterData1%' AND " & _
                                    "port_name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtLoadPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblloadport.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub txtPort_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPort_Code.TextChanged
        If txtPort_Code.Text = "" Then Return
        SQLstr = "select port_name from tbm_port " & _
            "where port_code='" & txtPort_Code.Text & "'"
        ErrMsg = ""
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If MyReader.HasRows Then
            MyReader.Read()
            lblPort_Name.Text = MyReader.GetString(0)
        End If
        CloseMyReader(MyReader, UserData)
    End Sub
    Private Sub TbLoadPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLoadPort_Code.TextChanged
        If txtLoadPort_Code.Text = "" Then Return
        SQLstr = "select port_name from tbm_port " & _
           "where port_code='" & txtLoadPort_Code.Text & "'"
        ErrMsg = ""
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If MyReader.HasRows Then
            MyReader.Read()
            lblloadport.Text = MyReader.GetString(0)
        End If
        CloseMyReader(MyReader, UserData)
    End Sub
    Private Sub btnAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.Click
        'f_caller = txtRILno.Text 'Added by Prie 04.11.2010
        'FrmRepot1.Show()

        Dim ViewerFrm As New Frm_CRViewer
        Dim SQLStr As String = ""
        Dim ErrMsg As String = ""

        Dim QuotaRilNo, compName, compIzin, CompAddress, compAutorize As String
        Dim matName, matZatActive, matKelOHewan, matReg, detUnit, detPack, quotaAppdt, userName, userTitle As String
        Dim detQty, detPrice, detCNF, headTolerable, DetQtyImp As Decimal

        NomUrut = 0
        v_type = "RIL"
        ViewerFrm.Text = "Display Report - " & Me.Text
        ViewerFrm.Tag = "RIL"

        TempTableName = "temp_" & GetIPAddressDash() & "_RIL"
        CreateTempTableName("ot_report_column", TempTableName)
        SQLStr = "SELECT a.*,b.name,b.title FROM " & _
                 "(SELECT a.ril_no,approveddt,approvedby,a.company_code,c.company_name,c.izin_deptan_no,c.address,c.authorize_person, " & _
                         "d.material_code,d.quantity,d.unit_code,e.material_name,e.zat_active,e.kelompok_obat_hewan,e.register_no, f.pack_name, d.price, a.tolerable " & _
                 " FROM tbl_ril_quota a, tbm_company c, tbl_ril_quota_detail d,tbm_material e, tbm_packing f " & _
                 " WHERE a.company_code=c.company_code AND a.ril_no = d.ril_no " & _
                       "AND d.material_code=e.material_code AND d.package_code=f.pack_code AND a.ril_no='" & txtRILno.Text & "') a " & _
                 "LEFT JOIN tbm_users b ON a.approvedby=b.user_ct "

        If DBQueryGetTotalRows(SQLStr, MyConn, ErrMsg, False, UserData) <= 0 Then
            Me.Hide()
            MsgBox("No data matches with your selection.", MsgBoxStyle.Information)
            Me.Close()
            Exit Sub
        End If
        MyDataReader = DBQueryDataReader(SQLStr, MyConn, ErrMsg, UserData)
        If Not MyDataReader Is Nothing Then
            While MyDataReader.Read
                Application.DoEvents()
                NomUrut = NomUrut + 1
                Try
                    QuotaRilNo = MyDataReader.GetString(0)
                Catch ex As Exception
                    QuotaRilNo = ""
                End Try
                Try
                    quotaAppdt = Format(MyDataReader.GetDateTime(1), "MMMM dd, yyyy")
                Catch ex As Exception
                    quotaAppdt = ""
                End Try
                Try
                    userName = EscapeStr(MyDataReader.GetString(17))
                Catch ex As Exception
                    userName = ""
                End Try
                Try
                    userTitle = MyDataReader.GetString(18)
                Catch ex As Exception
                    userTitle = ""
                End Try
                Try
                    compName = MyDataReader.GetString(4)
                Catch ex As Exception
                    compName = ""
                End Try
                Try
                    compIzin = MyDataReader.GetString(5)
                Catch ex As Exception
                    compIzin = ""
                End Try
                Try
                    CompAddress = MyDataReader.GetString(6)
                Catch ex As Exception
                    CompAddress = ""
                End Try
                Try
                    compAutorize = MyDataReader.GetString(7)
                Catch ex As Exception
                    compAutorize = ""
                End Try
                Try
                    detQty = CDec(Val(MyDataReader.GetValue(9)))
                Catch ex As Exception
                    detQty = 0
                End Try
                Try
                    detUnit = MyDataReader.GetString(10)
                Catch ex As Exception
                    detUnit = ""
                End Try
                Try
                    matName = MyDataReader.GetString(11)
                Catch ex As Exception
                    matName = ""
                End Try
                Try
                    matZatActive = MyDataReader.GetString(12)
                Catch ex As Exception
                    matZatActive = ""
                End Try
                Try
                    matKelOHewan = MyDataReader.GetString(13)
                Catch ex As Exception
                    matKelOHewan = ""
                End Try
                Try
                    matReg = MyDataReader.GetString(14)
                Catch ex As Exception
                    matReg = ""
                End Try
                Try
                    detPack = MyDataReader.GetString(15)
                Catch ex As Exception
                    detPack = ""
                End Try
                Try
                    detPrice = CDec(Val(MyDataReader.GetValue(16)))
                Catch ex As Exception
                    detPrice = 0
                End Try
                Try
                    headTolerable = MyDataReader.GetValue(17)
                Catch ex As Exception
                    headTolerable = 0
                End Try
                detCNF = detPrice * detQty
                DetQtyImp = detQty * ((100 + headTolerable) / 100)

                SQLStr = "INSERT into " & TempTableName & " (ClmInt1,ClmInt2,ClmInt3," & _
                             "ClmDec1,ClmDec2,ClmDec3,ClmDec4,ClmDec5,ClmDec6,ClmDec7,ClmDec8,ClmDec9,ClmDec10," & _
                             "ClmDec11,ClmDec12,ClmDec13,ClmDec14, " & _
                             "ClmStr1,ClmStr2,ClmStr3,ClmStr4,ClmStr5,ClmStr6,ClmStr7,ClmStr8,ClmStr9,ClmStr10," & _
                             "ClmStr11,ClmStr12,ClmStr13,ClmStr14,ClmStr15,ClmStr16,ClmStr17,ClmStr18," & _
                             "ClmDate1,ClmDate2,ClmDate3,ClmDate4,ClmDate5,ClmDate6,ClmDate7,ClmDate8) " & _
                             "VALUES (" & NomUrut & ",0,0," & _
                                     "" & detQty & "," & detPrice & "," & detCNF & "," & headTolerable & "," & DetQtyImp & ",0,0,0,0,0, " & _
                                     "0,0,0,0, " & _
                                     "'" & QuotaRilNo & "','" & quotaAppdt & "','" & userName & "','" & userTitle & "','" & compName & "'," & _
                                     "'" & CompAddress & "','" & compIzin & "','" & compAutorize & " ','" & detUnit & "','" & matName & "'," & _
                                     "'" & matZatActive & "','" & matKelOHewan & "','" & matReg & "','" & detPack & "','','','',''," & _
                                     "null,null,null,null,null,null,null,null)"
                ErrMsg = "Simpan tabel ot_report_column gagal..."
                If DBQueryUpdate(SQLStr, MyConn2, True, ErrMsg, UserData) < 0 Then
                    Exit Sub
                End If
            End While
        End If
        ViewerFrm.ShowDialog()

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Function GetIPAddress() As String
        Try
            Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
            Return h.AddressList.GetValue(0).ToString
        Catch ex As Exception
            Return "127.0.0.1"
        End Try
    End Function

    Private Function GetIPAddressDash() As String
        Dim IPStr As String = GetIPAddress()
        IPStr = IPStr.Replace(".", "_")
        IPStr = IPStr.Replace(":", "_")
        IPStr = IPStr.Replace("%", "_")
        Return IPStr
    End Function
    Private Function CreateTempTableName(ByVal nama As String, ByVal mTempTableName As String) As Boolean
        If DBCreateTableFromTable(nama, mTempTableName, MyConn, UserData, True, True) > 0 Then
            MsgBox("Gagal membuat table temporary", MsgBoxStyle.Information, "Informasi")
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub btnSearchSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupplier.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName, Bank_Name as BankName, Account_No as AccountNo FROM tbm_supplier WHERE active='1' "
        PilihanDlg.SQLFilter = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier " & _
                               "WHERE active='1' AND supplier_code LIKE 'FilterData1%' AND " & _
                                    "supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtSupplier_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblSupplier_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub txtSupplier_Code_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSupplier_Code.TextChanged
        If txtSupplier_Code.Text = "" Then
            btnSearchLoadPort.Enabled = False
        Else
            btnSearchLoadPort.Enabled = True
        End If
    End Sub
End Class