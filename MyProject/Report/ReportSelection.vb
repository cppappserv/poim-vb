Public Class ReportSelection
    Dim PilihanDlg As New DlgPilihan
    Dim v_type As String


    Sub New(ByVal caller As String, ByVal namef As String)
        InitializeComponent()
        Me.Text = namef
        gb1.Visible = True
        v_type = caller

        Select Case caller
            Case "RP01"
                lbl05.Visible = True
                DT05a.Visible = True
                DT05b.Visible = True
                DT05b.Value = GetServerDate()
                DT05a.Value = DateAdd(DateInterval.Month, -1, DT05b.Value)
                Call f_std01()
                lbl14.Visible = True
                cb140.Visible = True

                Call f_displayqty()
            Case "RP02"
                Call f_std02()
                lbl18.Visible = False
                lbl18.Location = lbl14.Location
                cb18.Visible = False
                cb18.Location = cb140.Location
                lbl19.Visible = False
                lbl19.Location = lbl17.Location
                cb19.Visible = True
                cb19.Location = cb17.Location
                cb18.Checked = False
                cb19.Checked = False
            Case "RP03"
                Call f_std02()
                lbl15.Visible = True
                lbl15.Location = lbl14.Location
                txt15.Visible = True
                txt15.Location = cb140.Location
                cb15a.Visible = True
                cb15b.Visible = True
                cb15c.Visible = True
                cb15a.Location = cb14a.Location
                cb15b.Location = cb14b.Location
                cb15c.Location = cb14c.Location
                cb15c.Checked = True
                lbl18.Visible = True
                lbl18.Location = lbl17.Location
                cb18.Visible = True
                cb18.Location = cb17.Location
            Case "RP04"
                Call f_std02()
                lbl15.Visible = True
                lbl15.Location = lbl14.Location
                txt15.Visible = True
                txt15.Location = cb140.Location
                cb15a.Visible = True
                cb15b.Visible = True
                cb15c.Visible = True
                cb15a.Location = cb14a.Location
                cb15b.Location = cb14b.Location
                cb15c.Location = cb14c.Location
                cb15c.Checked = True
            Case "RP05"
                lbl03.Visible = True
                lbl03.Location = lbl05.Location
                DT03a.Visible = True
                DT03b.Visible = True
                DT03a.Location = DT05a.Location
                DT03b.Location = DT05b.Location
                DT03b.Value = GetServerDate()
                DT03a.Value = DateAdd(DateInterval.Month, -1, DT03b.Value)
                Call f_std01()
                Call f_displayqty()
                lbl16.Visible = False
                txt16.Visible = False
                lbl16.Location = lbl14.Location
                txt16.Location = cb140.Location
                rb16a.Visible = True
                rb16b.Visible = True
                rb16a.Location = cb14a.Location
                rb16b.Location = cb14b.Location
            Case "RP06"
                lbl02.Visible = True
                DT02a.Visible = True
                DT02b.Visible = True
                DT02b.Value = GetServerDate()
                DT02a.Value = DateAdd(DateInterval.Month, -1, DT02b.Value)
                lbl02.Location = lbl05.Location
                DT02a.Location = DT05a.Location
                DT02b.Location = DT05b.Location
                Call f_std01()
                lbl11.Visible = True
                txi11.Visible = True
                btn11.Visible = True
                txt11.Visible = True
                lbl11.Location = lbl13.Location
                txi11.Location = txi13.Location
                btn11.Location = btn13.Location
                txt11.Location = txt13.Location

                lbl12.Visible = True
                txi12.Visible = True
                btn12.Visible = True
                txt12.Visible = True
            Case "RP07"
                lbl03.Visible = True
                DT03a.Visible = True
                DT03b.Visible = True
                lbl03.Location = lbl05.Location
                DT03a.Location = DT05a.Location
                DT03b.Location = DT05b.Location
                DT03b.Value = GetServerDate()
                DT03a.Value = DateAdd(DateInterval.Month, -1, DT03b.Value)
                Call f_std01()
                Call f_displayqty()
                lbl17.Visible = True
                lbl17.Location = lbl14.Location
                cb17.Visible = True
                cb17.Location = cb140.Location
            Case "RP08"
                lbl01.Visible = True
                DT01a.Visible = True
                DT01b.Visible = True
                lbl01.Location = lbl05.Location
                DT01a.Location = DT05a.Location
                DT01b.Location = DT05b.Location
                DT01b.Value = GetServerDate()
                DT01a.Value = DateAdd(DateInterval.Month, -1, DT01b.Value)

                lbl06.Visible = True
                txi06.Visible = True
                btn06.Visible = True
                txt06.Visible = True

                lbl07.Visible = True
                txi07.Visible = True
                btn07.Visible = True
                txt07.Visible = True

                lbl10.Visible = True
                txi10.Visible = True
                btn10.Visible = True
                txt10.Visible = True
                lbl10.Location = lbl09.Location
                txi10.Location = txi09.Location
                btn10.Location = btn09.Location
                txt10.Location = txt09.Location

                lbl09.Visible = True
                txi09.Visible = True
                btn09.Visible = True
                txt09.Visible = True
                lbl09.Location = lbl08.Location
                txi09.Location = txi08.Location
                btn09.Location = btn08.Location
                txt09.Location = txt08.Location
                Call f_displayqty()
            Case Else
                'RP09 dan RP10
                lbl01.Visible = True
                DT01a.Visible = True
                DT01b.Visible = True
                lbl01.Location = lbl05.Location
                DT01a.Location = DT05a.Location
                DT01b.Location = DT05b.Location
                DT01b.Value = GetServerDate()
                DT01a.Value = DateAdd(DateInterval.Month, -1, DT01b.Value)
                lbl01.Text = "Notice on Arrival"

                lbl06.Visible = True
                txi06.Visible = True
                btn06.Visible = True
                txt06.Visible = True

                lbl27.Visible = True
                txi27.Visible = True
                btn27.Visible = True
                txt27.Visible = True
                lbl27.Location = lbl13.Location
                txi27.Location = txi13.Location
                btn27.Location = btn13.Location
                txt27.Location = txt13.Location

                lbl26.Visible = True
                txi26.Visible = True
                btn26.Visible = True
                txt26.Visible = True
                lbl26.Location = lbl10.Location
                txi26.Location = txi10.Location
                btn26.Location = btn10.Location
                txt26.Location = txt10.Location

                lbl10.Visible = True
                txi10.Visible = True
                btn10.Visible = True
                txt10.Visible = True
                lbl10.Location = lbl09.Location
                txi10.Location = txi09.Location
                btn10.Location = btn09.Location
                txt10.Location = txt09.Location

                lbl09.Visible = True
                txi09.Visible = True
                btn09.Visible = True
                txt09.Visible = True
                lbl09.Location = lbl08.Location
                txi09.Location = txi08.Location
                btn09.Location = btn08.Location
                txt09.Location = txt08.Location

                lbl08.Visible = True
                txi08.Visible = True
                btn08.Visible = True
                txt08.Visible = True
                lbl08.Location = lbl07.Location
                txi08.Location = txi07.Location
                btn08.Location = btn07.Location
                txt08.Location = txt07.Location


                If caller = "RP09" Then
                    lbl18.Visible = True
                    lbl18.Location = lbl14.Location
                    cb18.Visible = True
                    cb18.Location = cb140.Location
                    lbl28.Visible = True
                    lbl28.Location = lbl17.Location
                    cb28.Visible = True
                    cb28.Location = cb17.Location
                    cb28.Checked = True
                Else
                    lbl29.Visible = True
                    lbl29.Location = lbl14.Location
                    cb29.Visible = True
                    cb29.Location = cb140.Location
                End If

        End Select

    End Sub
    Private Sub f_displayqty()
        lbl20.Visible = True
        txi20.Visible = True
        btn20.Visible = True
        txt20.Visible = True
        lbl20.Location = lbl12.Location
        txi20.Location = txi12.Location
        btn20.Location = btn12.Location
        txt20.Location = txt12.Location
    End Sub
    Private Sub f_std01()
        lbl06.Visible = True
        txi06.Visible = True
        btn06.Visible = True
        txt06.Visible = True

        lbl07.Visible = True
        txi07.Visible = True
        btn07.Visible = True
        txt07.Visible = True

        lbl08.Visible = True
        txi08.Visible = True
        btn08.Visible = True
        txt08.Visible = True

        lbl09.Visible = True
        txi09.Visible = True
        btn09.Visible = True
        txt09.Visible = True

        lbl10.Visible = True
        txi10.Visible = True
        btn10.Visible = True
        txt10.Visible = True
    End Sub
    Private Sub f_std02()

        lbl04.Location = lbl05.Location
        DT04a.Location = DT05a.Location
        DT04b.Location = DT05b.Location
        lbl04.Visible = True
        DT04a.Visible = True
        DT04b.Visible = True
        DT04b.Value = GetServerDate()
        DT04a.Value = DateAdd(DateInterval.Month, -1, DT04b.Value)

        lbl06.Visible = True
        txi06.Visible = True
        btn06.Visible = True
        txt06.Visible = True

        lbl11.Location = lbl09.Location
        txi11.Location = txi09.Location
        btn11.Location = btn09.Location
        txt11.Location = txt09.Location
        lbl11.Visible = True
        txi11.Visible = True
        btn11.Visible = True
        txt11.Visible = True

        lbl12.Location = lbl10.Location
        txi12.Location = txi10.Location
        btn12.Location = btn10.Location
        txt12.Location = txt10.Location
        lbl12.Visible = True
        txi12.Visible = True
        btn12.Visible = True
        txt12.Visible = True

        lbl09.Location = lbl07.Location
        txi09.Location = txi07.Location
        btn09.Location = btn07.Location
        txt09.Location = txt07.Location
        lbl09.Visible = True
        txi09.Visible = True
        btn09.Visible = True
        txt09.Visible = True

        lbl10.Location = lbl08.Location
        txi10.Location = txi08.Location
        btn10.Location = btn08.Location
        txt10.Location = txt08.Location
        lbl10.Visible = True
        txi10.Visible = True
        btn10.Visible = True
        txt10.Visible = True

        lbl13.Visible = True
        txi13.Visible = True
        btn13.Visible = True
        txt13.Visible = True

    End Sub
    Private Sub ReportSelection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.Tag = ""
        Dim value = New System.Drawing.Point(5, 5)
        Me.Location = value
        If v_type = "RP08" Then
            Me.Size = New System.Drawing.Size(690, 308)
            gb1.Size = New System.Drawing.Size(655, 218)
            'TblLayoutPanel.Location = New System.Drawing.Point(524, 219)
        Else
            Dim size = New System.Drawing.Size(690, 388)
            Me.Size = size
        End If

        'f_assign_all_date()

        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
    End Sub


    Private Sub btn06_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn06.Click
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
            txi06.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt06.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btn07_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn07.Click
        If txi06.Text <> "" Then
            PilihanDlg.Text = "Select Plant"
            PilihanDlg.LblKey1.Text = "Plant Code"
            PilihanDlg.LblKey2.Text = "Plant Name"
            PilihanDlg.SQLGrid = "SELECT PLANT_CODE as PlantCode, PLANT_NAME as PlantName, CITY_CODE as CityCode FROM tbm_plant "
            '"WHERE COMPANY_CODE = '" & txi06.Text & "'"
            PilihanDlg.SQLFilter = "SELECT PLANT_CODE AS PlantCode, PLANT_NAME as PlantName, CITY_CODE as CityCode FROM tbm_plant " & _
                                   "WHERE PLANT_CODE LIKE 'FilterData1%' AND " & _
                                        "PLANT_NAME LIKE 'FilterData2%' and "
            '"COMPANY_CODE = '" & txi06.Text & "'"
            PilihanDlg.Tables = "tbm_plant"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txi07.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                txt07.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub btn08_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn08.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier"
        PilihanDlg.SQLFilter = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier " & _
                               "WHERE supplier_code LIKE 'FilterData1%' AND " & _
                                    "supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi08.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt08.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btn09_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn09.Click
        PilihanDlg.Text = "Select Group Code"
        PilihanDlg.LblKey1.Text = "Group Code"
        PilihanDlg.LblKey2.Text = "Group Name"
        PilihanDlg.SQLGrid = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group"
        PilihanDlg.SQLFilter = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' " & _
                               " and group_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi09.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt09.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString()
        End If
    End Sub

    Private Sub btn10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn10.Click
        If txi09.Text <> "" Then
            PilihanDlg.Text = "Select Material Code"
            PilihanDlg.Width = 600
            PilihanDlg.Height = 402
            PilihanDlg.DgvResult.Width = 570
            PilihanDlg.DgvResult.Height = 267
            PilihanDlg.LblKey1.Text = "Material Code"
            PilihanDlg.LblKey2.Text = "Material Group"
            PilihanDlg.SQLGrid = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                                 "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                                 "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code where " & _
                                "tm.group_code = '" & txi09.Text & "'"
            PilihanDlg.SQLFilter = "SELECT tm.MATERIAL_CODE as MaterialCode, tm.Material_name as MaterialName, tm.Group_code as GroupCode, tmg.group_name as GroupName, tm.HS_CODE as HSCode, " & _
                                   "tm.Material_Shortname as MaterialShortName,tm.Register_No as RegisterNo, tm.Zat_Active as ZatActive, tm.Kelompok_Obat_Hewan as KelompokObatHewan " & _
                                   "FROM tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code " & _
                                   "WHERE tm.material_code LIKE 'FilterData1%' AND " & _
                                   "tm.Group_code LIKE 'FilterData2%' and " & _
                                   "tm.group_code = '" & txi09.Text & "'"
            PilihanDlg.Tables = "tbm_material as tm left join tbm_material_group as tmg on tm.Group_code = tmg.Group_code"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txi10.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                txt10.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub btn13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn13.Click
        PilihanDlg.Text = "Select Currency Code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        PilihanDlg.SQLGrid = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency"
        PilihanDlg.SQLFilter = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' AND " & _
                                    "currency_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi13.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt13.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btn12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn12.Click
        If txi11.Text <> "" Then
            PilihanDlg.Text = "Select Payment Term"
            PilihanDlg.LblKey1.Text = "Payment Term Code"
            PilihanDlg.LblKey2.Text = "Payment Type Name"
            PilihanDlg.SQLGrid = "SELECT payment_code as PaymentCode, payment_name as PaymentName FROM tbm_payment_term where " & _
                                 "class_code = '" & txi11.Text & "'"
            PilihanDlg.SQLFilter = "SELECT payment_code as PaymentCode, payment_name as PaymentName FROM tbm_payment_term " & _
                                   "WHERE payment_code LIKE 'FilterData1%' AND " & _
                                        "payment_name LIKE 'FilterData2%' AND " & _
                                        "class_code = '" & txi11.Text & "'"
            PilihanDlg.Tables = "tbm_payment_term"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txi12.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                txt12.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub btn11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn11.Click
        PilihanDlg.Text = "Select Payment Type"
        PilihanDlg.LblKey1.Text = "Payment Type Code"
        PilihanDlg.LblKey2.Text = "Payment Type Name"
        PilihanDlg.SQLGrid = "SELECT class_code as TypeCode, class_name as TypeName FROM tbm_payment_class"
        PilihanDlg.SQLFilter = "SELECT class_code as TypeCode, class_name as TypeName FROM tbm_payment_class " & _
                               "WHERE class_code LIKE 'FilterData1%' AND " & _
                                    "class_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_payment_class"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi11.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt11.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub btn26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn26.Click
        PilihanDlg.Text = "Select Shipping Line"
        PilihanDlg.LblKey1.Text = "Shipping Line"
        PilihanDlg.LblKey2.Text = "Shipping Name"
        PilihanDlg.SQLGrid = "SELECT line_code as LineCode, line_name as LineName FROM tbm_lines"
        PilihanDlg.SQLFilter = "SELECT line_code as LineCode, line_name as LineName FROM tbm_lines " & _
                               "WHERE line_code LIKE 'FilterData1%' AND " & _
                                    "line_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_lines"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi26.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt26.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub btn27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn27.Click
        PilihanDlg.Text = "Select EMKL"
        PilihanDlg.LblKey1.Text = "Expedition Code"
        PilihanDlg.LblKey2.Text = "Expedition Name"
        PilihanDlg.SQLGrid = "SELECT company_code as ExpeditionCode, company_name as ExpeditionName FROM tbm_expedition"
        PilihanDlg.SQLFilter = "SELECT company_code as ExpeditionCode, company_name as ExpeditionName FROM tbm_expedition " & _
                               "WHERE company_code LIKE 'FilterData1%' AND " & _
                                    "company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_expedition"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi27.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt27.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btn20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn20.Click
        PilihanDlg.Text = "Select Unit "
        PilihanDlg.LblKey1.Text = "Unit Code"
        PilihanDlg.LblKey2.Text = "Unit Name"
        PilihanDlg.SQLGrid = "Select unit_code UnitCode, unit_name UnitName From tbm_unit where type_code=1 "
        PilihanDlg.SQLFilter = "Select unit_code UnitCode, unit_name UnitName From tbm_unit where type_code=1 " & _
                               "and unit_code LIKE 'FilterData1%' AND " & _
                                    "unit_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_unit"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txi20.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            txt20.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim ViewerFrm As New Rpt_CRViewer
        'Dim Viewerxl As New Rpt_Excel 
        Dim v_num As String

        ViewerFrm.Text = "Display Report - " & Me.Text
        ViewerFrm.v_type = v_type
        Select Case v_type
            Case "RP01"
                ViewerFrm.date1 = DT05a.Value
                ViewerFrm.date2 = DT05b.Value
                ViewerFrm.field1 = txi06.Text
                ViewerFrm.field2 = txi07.Text
                ViewerFrm.field3 = txi08.Text
                ViewerFrm.field4 = txi09.Text
                ViewerFrm.field5 = txi10.Text
                ViewerFrm.field6 = txi20.Text
                ViewerFrm.con1 = cb140.Checked
                ViewerFrm.ShowDialog()
            Case "RP02"
                'If cb19.Checked = False And cb18.Checked = False Then
                '    MsgBox("Pilih minimal salah satu jenis Display", MsgBoxStyle.Exclamation, "Error")
                'Else
                ViewerFrm.date1 = DT04a.Value
                ViewerFrm.date2 = DT04b.Value
                ViewerFrm.field1 = txi06.Text
                ViewerFrm.field2 = txi09.Text
                ViewerFrm.field3 = txi10.Text
                ViewerFrm.field4 = txi11.Text
                ViewerFrm.field5 = txi12.Text
                ViewerFrm.field6 = txi13.Text
                ViewerFrm.con1 = cb18.Checked
                ViewerFrm.con2 = cb19.Checked
                ViewerFrm.ShowDialog()
                'End If
            Case "RP03"
                If cb15a.Checked = False And cb15b.Checked = False And cb15c.Checked = False Then
                    MsgBox("Pilih minimal salah satu Status", MsgBoxStyle.Exclamation, "Error")
                Else
                    Dim Viewerxl As New Rpt_Excel
                    Viewerxl.Text = "Display Report - " & Me.Text
                    Viewerxl.v_type = v_type
                    Viewerxl.date1 = DT04a.Value
                    Viewerxl.date2 = DT04b.Value
                    Viewerxl.field1 = txi06.Text
                    Viewerxl.field2 = txi09.Text
                    Viewerxl.field3 = txi10.Text
                    Viewerxl.field4 = txi11.Text
                    Viewerxl.field5 = txi12.Text
                    Viewerxl.field6 = txi13.Text
                    Viewerxl.con1 = cb15a.Checked
                    Viewerxl.con2 = cb15b.Checked
                    Viewerxl.con3 = cb15c.Checked
                    Viewerxl.conb = cb19.Checked
                    Viewerxl.ShowDialog()
                End If
            Case "RP04"
                If cb15a.Checked = False And cb15b.Checked = False And cb15c.Checked = False Then
                    MsgBox("Pilih minimal salah satu Status", MsgBoxStyle.Exclamation, "Error")
                Else
                    Dim Viewerxl As New Rpt_Excel
                    Viewerxl.Text = "Display Report - " & Me.Text
                    Viewerxl.v_type = v_type
                    Viewerxl.date1 = DT04a.Value
                    Viewerxl.date2 = DT04b.Value
                    Viewerxl.field1 = txi06.Text
                    Viewerxl.field2 = txi09.Text
                    Viewerxl.field3 = txi10.Text
                    Viewerxl.field4 = txi11.Text
                    Viewerxl.field5 = txi12.Text
                    Viewerxl.field6 = txi13.Text
                    Viewerxl.con1 = cb15a.Checked
                    Viewerxl.con2 = cb15b.Checked
                    Viewerxl.con3 = cb15c.Checked
                    Viewerxl.ShowDialog()
                End If
            Case "RP05"
                ViewerFrm.date1 = DT03a.Value
                ViewerFrm.date2 = DT03b.Value
                ViewerFrm.field1 = txi06.Text
                ViewerFrm.field2 = txi07.Text
                ViewerFrm.field3 = txi08.Text
                ViewerFrm.field4 = txi09.Text
                ViewerFrm.field5 = txi10.Text
                ViewerFrm.field6 = txi20.Text
                ViewerFrm.con1 = rb16a.Checked
                ViewerFrm.con2 = rb16b.Checked
                ViewerFrm.ShowDialog()
            Case "RP06"
                ViewerFrm.date1 = DT02a.Value
                ViewerFrm.date2 = DT02b.Value
                ViewerFrm.field1 = txi06.Text
                ViewerFrm.field2 = txi07.Text
                ViewerFrm.field3 = txi08.Text
                ViewerFrm.field4 = txi09.Text
                ViewerFrm.field5 = txi10.Text
                ViewerFrm.field6 = txi11.Text
                ViewerFrm.field7 = txi12.Text
                ViewerFrm.ShowDialog()
            Case "RP07"
                ViewerFrm.date1 = DT03a.Value
                ViewerFrm.date2 = DT03b.Value
                ViewerFrm.field1 = txi06.Text
                ViewerFrm.field2 = txi07.Text
                ViewerFrm.field3 = txi08.Text
                ViewerFrm.field4 = txi09.Text
                ViewerFrm.field5 = txi10.Text
                ViewerFrm.field6 = txi20.Text
                ViewerFrm.con1 = cb17.Checked
                ViewerFrm.ShowDialog()
            Case "RP08"
                ViewerFrm.date1 = DT01a.Value
                ViewerFrm.date2 = DT01b.Value
                ViewerFrm.field1 = txi06.Text
                ViewerFrm.field2 = txi07.Text
                ViewerFrm.field3 = txi09.Text
                ViewerFrm.field4 = txi10.Text
                ViewerFrm.field5 = txi20.Text
                ViewerFrm.ShowDialog()
            Case "RP09"
                Dim Viewerxl As New Rpt_Excel
                Viewerxl.Text = "Display Report - " & Me.Text
                Viewerxl.v_type = v_type
                Viewerxl.date1 = DT01a.Value
                Viewerxl.date2 = DT01b.Value
                Viewerxl.field1 = txi06.Text
                Viewerxl.field2 = txi08.Text
                Viewerxl.field3 = txi09.Text
                Viewerxl.field4 = txi10.Text
                Viewerxl.field5 = txi26.Text
                Viewerxl.field6 = txi27.Text
                Viewerxl.con1 = cb18.Checked
                Viewerxl.con2 = cb28.Checked
                'ViewerFrm.ShowDialog()
                Viewerxl.ShowDialog()
            Case "RP10"
                Dim Viewerxl As New Rpt_Excel
                Viewerxl.Text = "Display Report - " & Me.Text
                Viewerxl.v_type = v_type
                Viewerxl.date1 = DT01a.Value
                Viewerxl.date2 = DT01b.Value
                Viewerxl.field1 = txi06.Text
                Viewerxl.field2 = txi08.Text
                Viewerxl.field3 = txi09.Text
                Viewerxl.field4 = txi10.Text
                Viewerxl.field5 = txi26.Text
                Viewerxl.field6 = txi27.Text
                Viewerxl.con1 = cb29.Checked
                'ViewerFrm.ShowDialog()
                Viewerxl.ShowDialog()
        End Select
    End Sub

    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Form1.ShowDialog()
    End Sub
    'Function f_assign_all_date()
    '    DT01b.Value = Now()
    '    DT02b.Value = Now()
    '    DT03b.Value = Now()
    '    DT04b.Value = Now()
    '    DT05b.Value = Now()
    '    DT01a.Value = DateAdd(DateInterval.Month, -1, DT01b.Value)
    '    DT02a.Value = DateAdd(DateInterval.Month, -1, DT02b.Value)
    '    DT03a.Value = DateAdd(DateInterval.Month, -1, DT03b.Value)
    '    DT04a.Value = DateAdd(DateInterval.Month, -1, DT04b.Value)
    '    DT05a.Value = DateAdd(DateInterval.Month, -1, DT05b.Value)
    'End Function
End Class