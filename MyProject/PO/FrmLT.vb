''' <summary>
''' Title                         : Laytime Calculation
''' Form                          : FrmLT
''' Table Used                    :
''' Stored Procedure Used (MySQL) : SaveLT
''' Created By                    : 
''' Note                          : 
''' </summary>
''' <remarks></remarks>

Public Class FrmLT
    Dim strSQL, errMSg As String
    Dim MyReader As MySqlDataReader

    Dim PilihanDlg As New DlgPilihan
    Dim PilihBL As New FrmListBL
    Dim Baru, Edit As Boolean
    Dim DataError As Boolean

    Dim qt0, qt2 As String

    Structure MyStruc
        Dim DSDateTime As System.Data.DataTable
        Dim DSDelay As System.Data.DataTable
        Dim DSDetail As System.Data.DataTable
    End Structure

    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Private Sub FrmLT_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'btnSave.Enabled = False
    End Sub

    Private Sub FrmLTDoc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ee As System.EventArgs
        Dim value = New System.Drawing.Size(1023, 700)
        Dim loc = New System.Drawing.Point(0, 30)

        Me.Location = loc
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If

        btnNew_Click(sender, ee)
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        Me.Size = value
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","

        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub btnCurrency_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCurrency.Click
        PilihanDlg.Text = "Select Currency Code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        PilihanDlg.SQLGrid = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency"
        PilihanDlg.SQLFilter = "SELECT currency_code as CurrencyCode, currency_name as CurrencyName FROM tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' AND " & _
                                    "currency_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Currency.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub btnBL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBL.Click
        If PilihBL.ShowDialog() = Windows.Forms.DialogResult.OK Then DisplaySelectedData()
    End Sub

    Private Sub DisplaySelectedData()
        Dim ee As System.EventArgs
        Dim TypeC As String

        btnNew_Click(btnBL, ee)

        txtShipNo.Text = PilihBL.v_shipno.Text
        blno.Text = PilihBL.v_blno.Text

        PilihBL.v_shipno.Text = ""
        PilihBL.v_blno.Text = ""

        TypeC = ""
        strSQL = "SELECT upper(unit_code) unit_code FROM tbl_shipping_cont WHERE shipment_no='" & txtShipNo.Text & "'"
        errMSg = "Bill of Lading data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                TypeC = MyReader.GetString("unit_code")
            End While
            CloseMyReader(MyReader, UserData)
        End If
        If TypeC <> "CURAH" Then
            MsgBox("Only calculate bulk cargo. Process can't be continued  ", MsgBoxStyle.Critical, "Warning")

            txtShipNo.Text = ""
            blno.Text = ""
            blno.Focus()
        Else
            RefreshDisplay(btnBL, ee)
        End If
    End Sub

    Public Sub RefreshDisplay(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Edit = True
        Baru = False
        DisplayLTHeader()
        GetDataDateTime(txtShipNo.Text, 1, Edit)
        GetDataDelay(txtShipNo.Text, 1, Edit)
    End Sub

    Private Sub DisplayLTHeader()
        Dim temp1 As String = ""
        Dim temp2 As String = ""
        Dim temp3 As String = ""
        Dim temp4 As String = ""
        Dim temp5 As String = ""

        strSQL = "SELECT t1.* " & _
                 "FROM tbl_shipping t1 WHERE t1.SHIPMENT_NO = '" & txtShipNo.Text & "' "

        errMSg = "Bill of Lading data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    hostbl.Text = MyReader.GetString("HostBL")
                    packlist.Text = MyReader.GetString("PackingList_No")

                    temp1 = MyReader.GetString("Supplier_Code")
                    temp2 = MyReader.GetString("Port_Code")
                    temp3 = MyReader.GetString("LoadPort_Code")
                    temp4 = MyReader.GetString("Shipping_Line")
                    temp5 = MyReader.GetString("Plant_Code")

                    vessel.Text = MyReader.GetString("Vessel")

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        suppl.Text = temp1
        supplname.Text = AmbilData("supplier_name", "tbm_supplier", "supplier_code='" & suppl.Text & "'")
        Company.Text = AmbilData("company_code", "tbm_plant", "plant_code='" & temp5 & "'")
        CompanyName.Text = AmbilData("company_name", "tbm_company", "company_code='" & Company.Text & "'")
        DestPlant.Text = temp5
        DestPlantName.Text = AmbilData("plant_name", "tbm_plant", "plant_code='" & DestPlant.Text & "'")
        DestPort.Text = temp2
        DestPortName.Text = AmbilData("port_name", "tbm_port", "port_code='" & DestPort.Text & "'")
        LoadPort.Text = temp3
        LoadPortName.Text = AmbilData("port_name", "tbm_port", "port_code='" & LoadPort.Text & "'")
        ShipLine.Text = temp4
        ShipLineName.Text = AmbilData("line_name", "tbm_lines", "line_code='" & ShipLine.Text & "'")

        strSQL = "SELECT t1.material_code, t1.quantity, t2.unit_code FROM tbl_shipping_detail t1, tbl_po_detail t2 " & _
                 "WHERE t1.po_no=t2.po_no AND t1.shipment_no='" & txtShipNo.Text & "' "

        errMSg = "Bill of Lading Detail data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp1 = MyReader.GetString("material_code")
                    temp2 = MyReader.GetString("quantity")
                    temp3 = MyReader.GetString("unit_code")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        Material.Text = temp1
        MaterialName.Text = AmbilData("material_name", "tbm_material", "material_code='" & Material.Text & "'")
        ''------------------------------------------------------
        ''FormatNumber(Kurs, 2, , , TriState.True)
        ''FormatNumber(Math.Floor(TotBea), 0, , , TriState.True)
        ''------------------------------------------------------
        Quantity.Text = FormatNumber(temp2, 2, , , TriState.True)
        QuantityUnit.Text = temp3

        Cargo.Text = FormatNumber(temp2, 2, , , TriState.True)
        Unit2.Text = temp3

        lblUnitAllowance.Text = temp3 & "/day"

        lblSharePT.Text = "Share of " & CompanyName.Text
        lblFinal.Text = "Cargo quantity " & Quantity.Text & " of " & Cargo.Text & " = 100%"
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim tg As Date
        Dim ee As System.EventArgs
        Dim Cnt As Integer
        Dim Val As String

        btnSave.Enabled = False

        Baru = True
        Edit = False

        tg = GetServerDate()

        blno.Text = ""
        hostbl.Text = ""
        suppl.Text = ""
        supplname.Text = ""
        packlist.Text = ""
        Company.Text = ""
        CompanyName.Text = ""
        DestPlant.Text = ""
        DestPlantName.Text = ""
        DestPort.Text = ""
        DestPortName.Text = ""
        LoadPort.Text = ""
        LoadPortName.Text = ""
        ShipLine.Text = ""
        ShipLineName.Text = ""
        vessel.Text = ""
        Material.Text = ""
        MaterialName.Text = ""
        Quantity.Text = ""
        QuantityUnit.Text = ""

        cbDiscTerm.Items.Clear()
        strSQL = "SELECT CONCAT(term_code,'-',term_name) term_code FROM tbm_discharge_term"
        errMSg = "Discharge Term data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Val = MyReader.GetString("term_code")
                cbDiscTerm.Items.Insert(Cnt, Val)
                Cnt = Cnt + 1
            End While
            CloseMyReader(MyReader, UserData)
        End If

        Cargo.Text = qt2
        Unit2.Text = ""
        Currency.Text = ""
        Allowance.Text = qt2
        lblUnitAllowance.Text = "-/day"
        DemRate.Text = qt2
        DispacthRate.Text = qt2
        txtNote.Text = ""

        'gridDateTime Clear
        DGDateTime.DataSource = Nothing
        DGDateTime.Columns.Clear()

        'gridDelay Clear
        DGDelay.DataSource = Nothing
        DGDelay.Columns.Clear()

        day1.Text = ""
        date1.Checked = False
        hours1.Text = "08"
        minute1.Text = "00"
        remark1.Text = ""
        day2.Text = ""
        date2.Checked = False
        hours2.Text = "08"
        minute2.Text = "00"
        remark2.Text = ""

        LTAllowed_1.Text = ""
        TimeUsed_1.Text = ""
        lblTimeLostSaved.Text = "Time Lost/Saved"
        TimeLostSaved_1.Text = ""
        lblDemDesp.Text = "Demurrage/Despatch due"
        DemDespAmt.Text = qt2
        lblSharePT.Text = "Share of PT."
        lblFinal.Text = "Cargo quantity"
        DemDispAmtFinal.Text = qt2

        h.Value = tg
        crtcode.Text = UserData.UserCT.ToString
        crtname.Text = UserData.UserName
        Status.Text = "Open"

        blno.Focus()
        GetDataDateTime(1, 1, True)
        GetDataDelay(1, 1, True)
    End Sub


    Private Sub Cargo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Cargo.Validating
        Dim prc As Decimal
        Dim prc_str As String

        lblFinal.Text = "Cargo quantity "
        If IsNumeric(Cargo.Text) = False Then
            MsgBox("Invalid Cargo value, input numeric value! ", MsgBoxStyle.Critical, "Warning")
            Cargo.Focus()
            DataError = True
        Else
            Cargo.Text = FormatNumber(Cargo.Text, 2, , , TriState.True)
            If Cargo.Text <> "" And Cargo.Text <> "0" Then
                Try
                    prc = (Quantity.Text / Cargo.Text) * 100
                Catch ex As Exception
                    prc = 0
                End Try
                prc_str = FormatNumber(prc, 2, , , TriState.True)
                lblFinal.Text = "Cargo quantity " & Quantity.Text & " of " & Cargo.Text & " = " & prc_str & "%"
            End If
        End If
    End Sub

    Public Array(0) As MyStruc
    Private Sub SaveData()
        Array(0).DSDateTime = DGDateTime.DataSource
        Array(0).DSDelay = DGDelay.DataSource
    End Sub

    Private Sub cbDiscTerm_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbDiscTerm.SelectedValueChanged
        txtDiscTerm.Text = AmbilData("term_description", "tbm_discharge_term", "term_code='" & mid(cbDiscTerm.Text,1,2) & "'")
    End Sub

    Private Sub Allowance_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Allowance.Validating
        If IsNumeric(Allowance.Text) = False Then
            MsgBox("Invalid Allowance value, input numeric value! ", MsgBoxStyle.Critical, "Warning")
            Allowance.Focus()
            DataError = True
        Else
            Allowance.Text = FormatNumber(Allowance.Text, 2, , , TriState.True)
        End If
    End Sub

    Private Sub DemRate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DemRate.Validating
        If IsNumeric(DemRate.Text) = False Then
            MsgBox("Invalid Rate value, input numeric value! ", MsgBoxStyle.Critical, "Warning")
            DemRate.Focus()
            DataError = True
        Else
            DemRate.Text = FormatNumber(DemRate.Text, 2, , , TriState.True)
        End If
    End Sub

    Private Sub DispacthRate_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DispacthRate.Validating
        If IsNumeric(DispacthRate.Text) = False Then
            MsgBox("Invalid Rate value, input numeric value! ", MsgBoxStyle.Critical, "Warning")
            DispacthRate.Focus()
            DataError = True
        Else
            DispacthRate.Text = FormatNumber(DispacthRate.Text, 2, , , TriState.True)
        End If
    End Sub

    Private Sub hours1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles hours1.Validating
        Dim icek As Integer

        If IsNumeric(hours1.Text) = False Then
            icek = -1
        Else
            If CInt((hours1.Text)) > 60 Then icek = -1
        End If

        If icek = -1 Then
            MsgBox("Hours out of range (0-24)! ", MsgBoxStyle.Critical, "Warning")
            hours1.Focus()
            DataError = True
        End If
    End Sub

    Private Sub date1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles date1.ValueChanged
        If date1.Checked Then
            day1.Text = Mid(Format(CDate(date1.Value), "dddd"), 1, 3)
        Else
            day1.Text = ""
            hours1.Text = "08"
            minute1.Text = "00"
            remark1.Text = ""
        End If
    End Sub

    Private Sub date2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles date2.ValueChanged
        If date2.Checked Then
            day2.Text = Mid(Format(CDate(date2.Value), "dddd"), 1, 3)
        Else
            day2.Text = ""
            hours2.Text = "08"
            minute2.Text = "00"
            remark2.Text = ""
        End If
    End Sub

    Public Sub GetDataDateTime(ByVal sno As String, ByVal ono As String, ByVal stat As Boolean)
        Dim dts1, dtscb1 As DataTable
        Dim cbn1 As New DataGridViewComboBoxColumn

        DGDateTime.DataSource = Nothing
        DGDateTime.Columns.Clear()

        'Grid selain combobox
        errMSg = "Tbl_Laytime_DDate data view failed"

        strSQL = "SELECT DDATE_CODE as 'DescCode', FORMAT(DDATE,'dddd') as 'Day', DDATE as 'Date', DHOURS as 'Hours', DMINUTE as 'Minute', REMARK as 'Remark' " & _
                 "FROM tbl_laytime_ddate WHERE SHIPMENT_NO='" & sno & "' AND ORD_NO = '" & ono & "'"

        dts1 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        DGDateTime.DataSource = dts1

            'Combo Box DateTime
            errMSg = "Tbm_DDate data view failed"
            strSQL = "select '' DDate_Code, '' DDate_Name from DUAL  " & _
                     " UNION " & _
                     "select DDate_Code, DDate_Name from Tbm_DDate Where DDate_flag='+' "
            dtscb1 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
            With cbn1
                .DataSource = dtscb1
                .DisplayMember = "DDate_Name"
                .ValueMember = "DDate_Code"
            End With
            DGDateTime.Columns.Insert(1, cbn1)
            DGDateTime.Columns(1).HeaderText = "Description"

            DGDateTime.Columns(0).Visible = False
            DGDateTime.Columns(1).Width = 140
            DGDateTime.Columns(2).Width = 40
            DGDateTime.Columns(2).ReadOnly = True
            DGDateTime.Columns(2).DefaultCellStyle.BackColor = Color.LightGray
            DGDateTime.Columns(3).Width = 100
            DGDateTime.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            DGDateTime.Columns(4).Width = 43
            DGDateTime.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGDateTime.Columns(5).Width = 43
            DGDateTime.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGDateTime.Columns(6).Width = 190
    End Sub

    Public Sub GetDataDelay(ByVal sno As String, ByVal ono As String, ByVal stat As Boolean)
        Dim dts2, dtscb2 As DataTable
        Dim cbn2 As New DataGridViewComboBoxColumn

        DGDelay.DataSource = Nothing
        DGDelay.Columns.Clear()

        'Grid selain combobox
        errMSg = "Tbl_Laytime_DDate data view failed"

        strSQL = "SELECT DDATE_CODE as 'DescCode', FORMAT(DDATE,'dddd') as 'Day', DDATE as 'Date', " & _
                 "DHOURS_FR as 'Fr_hh', DMINUTE_Fr as 'Fr_mm', DHOURS_TO as 'To_hh', DMINUTE_To as 'To_mm', " & _
                 "REMARK as 'Remark', DHOURS_USED as 'Used_hh', DMINUTE_USED as 'Used_mm', " & _
                 "'' as 'TotalDay', '' as Total_hh, '' as Total_mm " & _
                 "FROM tbl_laytime_detail WHERE SHIPMENT_NO='" & sno & "' AND ORD_NO = '" & ono & "'"

        dts2 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        DGDelay.DataSource = dts2

        'Combo Box DateTime
        errMSg = "Tbm_DDate data view failed"
        strSQL = "select '' DDate_Code, '' DDate_Name from DUAL  " & _
                 " UNION " & _
                 "select CONCAT(DDATE_FLAG,' ',DDate_Code) DDate_Code, DDate_Name from Tbm_DDate Where DDate_flag<>'+' "
        dtscb2 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn2
            .DataSource = dtscb2
            .DisplayMember = "DDate_Name"
            .ValueMember = "DDate_Code"
        End With
        DGDelay.Columns.Insert(1, cbn2)

        DGDelay.ColumnHeadersHeight = 40
        DGDelay.Columns(7).Frozen = True
        DGDelay.Columns(0).Visible = False
        DGDelay.Columns(1).HeaderText = "Description"
        DGDelay.Columns(1).Width = 150
        DGDelay.Columns(2).Width = 40
        DGDelay.Columns(2).ReadOnly = True
        DGDelay.Columns(2).DefaultCellStyle.BackColor = Color.LightGray
        DGDelay.Columns(3).Width = 80
        DGDelay.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DGDelay.Columns(4).Width = 45
        DGDelay.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(5).Width = 45
        DGDelay.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(6).Width = 45
        DGDelay.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(7).Width = 45
        DGDelay.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(8).Width = 200
        DGDelay.Columns(9).Width = 58
        DGDelay.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(9).ReadOnly = True
        DGDelay.Columns(9).DefaultCellStyle.BackColor = Color.LightGray
        DGDelay.Columns(10).Width = 58
        DGDelay.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(10).ReadOnly = True
        DGDelay.Columns(10).DefaultCellStyle.BackColor = Color.LightGray
        DGDelay.Columns(11).Width = 55
        DGDelay.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(11).ReadOnly = True
        DGDelay.Columns(11).DefaultCellStyle.BackColor = Color.LightGray
        DGDelay.Columns(12).Width = 55
        DGDelay.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(12).ReadOnly = True
        DGDelay.Columns(12).DefaultCellStyle.BackColor = Color.LightGray
        DGDelay.Columns(13).Width = 55
        DGDelay.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGDelay.Columns(13).ReadOnly = True
        DGDelay.Columns(13).DefaultCellStyle.BackColor = Color.LightGray
    End Sub

    Private Sub DGDateTime_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDateTime.CellEndEdit
        Dim brs, icek As Integer
        Dim tg As Date

        brs = DGDateTime.CurrentCell.RowIndex
        tg = GetServerDate()

        If DGDateTime.Columns(e.ColumnIndex).Name = "Date" Then    
            Try
                DGDateTime.Item(2, brs).Value = Mid(Format(CDate(DGDateTime.Item(3, brs).Value), "dddd"), 1, 3)
            Catch ex As Exception
                MsgBox("Invalid Date! ", MsgBoxStyle.Critical, "Warning")
                DGDateTime.Item(2, brs).Value = Mid(Format(CDate(tg), "dddd"), 1, 3)
                DGDateTime.Item(3, brs).Value = tg
            End Try
        End If

        If DGDateTime.Columns(e.ColumnIndex).Name = "Hours" Then
            Try
                icek = CInt(DGDateTime.Item(4, brs).Value)
                If (icek < 0 Or icek > 24) Then icek = -1
            Catch ex As Exception
                icek = -1
            End Try
            If icek = -1 Then
                MsgBox("Hours out of range (0-24)! ", MsgBoxStyle.Critical, "Warning")
                DGDateTime.Item(4, brs).Value = "08"
            End If
        End If
        If DGDateTime.Columns(e.ColumnIndex).Name = "Minute" Then
            Try
                icek = CInt(DGDateTime.Item(5, brs).Value)
                If (icek < 0 Or icek > 60) Then icek = -1
            Catch ex As Exception
                icek = -1
            End Try
            If icek = -1 Then
                MsgBox("Minute out of range (0-60)! ", MsgBoxStyle.Critical, "Warning")
                DGDateTime.Item(5, brs).Value = "00"
            End If
        End If

        SaveData()
    End Sub

    Private Sub DGDateTime_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGDateTime.RowsAdded
        Dim brs As Integer
        Dim tg As Date

        Try
            brs = DGDateTime.CurrentCell.RowIndex
            tg = GetServerDate()
            If DGDateTime.Item(3, brs).Value Is DBNull.Value Then
                DGDateTime.Item(2, brs).Value = Mid(Format(CDate(tg), "dddd"), 1, 3)
                DGDateTime.Item(3, brs).Value = tg
            End If

            If DGDateTime.Item(4, brs).Value Is DBNull.Value Then DGDateTime.Item(4, brs).Value = "08"
            If DGDateTime.Item(5, brs).Value Is DBNull.Value Then DGDateTime.Item(5, brs).Value = "00"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub hours2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles hours2.Validating
        Dim icek As Integer

        If IsNumeric(hours2.Text) = False Then
            icek = -1
        Else
            If CInt((hours2.Text)) > 60 Then icek = -1
        End If

        If icek = -1 Then
            MsgBox("Hours out of range (0-24)! ", MsgBoxStyle.Critical, "Warning")
            hours2.Focus()
            DataError = True
        End If
    End Sub


    Private Sub minute1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles minute1.Validating
        Dim icek As Integer

        If IsNumeric(minute1.Text) = False Then
            icek = -1
        Else
            If CInt((minute1.Text)) > 60 Then icek = -1
        End If

        If icek = -1 Then
            MsgBox("Minute out of range (0-60)! ", MsgBoxStyle.Critical, "Warning")
            minute1.Focus()
            DataError = True
        End If
    End Sub

    Private Sub minute2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minute2.TextChanged

    End Sub

    Private Sub minute2_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles minute2.Validating
        Dim icek As Integer

        If IsNumeric(minute2.Text) = False Then
            icek = -1
        Else
            If CInt((minute2.Text)) > 60 Then icek = -1
        End If

        If icek = -1 Then
            MsgBox("Minute out of range (0-60)! ", MsgBoxStyle.Critical, "Warning")
            minute2.Focus()
            DataError = True
        End If
    End Sub

    Private Sub DGDelay_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDelay.CellEndEdit
        Dim brs, iCek As Integer
        Dim tg As Date
        Dim strCek As String

        brs = DGDelay.CurrentCell.RowIndex
        tg = GetServerDate()

        strCek = DGDelay.Columns(e.ColumnIndex).Name

        If strCek = "Date" Then
            Try
                DGDelay.Item(2, brs).Value = Mid(Format(CDate(DGDelay.Item(3, brs).Value), "dddd"), 1, 3)
            Catch ex As Exception
                MsgBox("Invalid Date! ", MsgBoxStyle.Critical, "Warning")
                DGDelay.Item(2, brs).Value = ""
                DGDelay.Item(3, brs).Value = ""
            End Try
        End If

        If strCek = "Fr_hh" Then
            Try
                iCek = CInt(DGDelay.Item(4, brs).Value)
                If (iCek < 0 Or iCek > 24) Then iCek = -1
            Catch ex As Exception
                iCek = -1
            End Try
            If iCek = -1 Then
                MsgBox("Hours out of range (0-24)! ", MsgBoxStyle.Critical, "Warning")
                DGDelay.Item(4, brs).Value = "08"
            End If
        End If
        If strCek = "Fr_mm" Then
            Try
                iCek = CInt(DGDelay.Item(5, brs).Value)
                If (iCek < 0 Or iCek > 60) Then iCek = -1
            Catch ex As Exception
                iCek = -1
            End Try
            If iCek = -1 Then
                MsgBox("Minute out of range (0-60)! ", MsgBoxStyle.Critical, "Warning")
                DGDelay.Item(5, brs).Value = "00"
            End If
        End If

        If strCek = "To_hh" Then
            Try
                iCek = CInt(DGDelay.Item(6, brs).Value)
                If (iCek < 0 Or iCek > 24) Then iCek = -1
            Catch ex As Exception
                iCek = -1
            End Try
            If iCek = -1 Then
                MsgBox("Hours out of range (0-24)! ", MsgBoxStyle.Critical, "Warning")
                DGDelay.Item(6, brs).Value = "24"
            End If
        End If
        If strCek = "To_mm" Then
            Try
                iCek = CInt(DGDelay.Item(7, brs).Value)
                If (iCek < 0 Or iCek > 60) Then iCek = -1
            Catch ex As Exception
                iCek = -1
            End Try
            If iCek = -1 Then
                MsgBox("Minute out of range (0-60)! ", MsgBoxStyle.Critical, "Warning")
                DGDelay.Item(7, brs).Value = "00"
            End If
        End If
        SaveData()
    End Sub

    Private Sub DGDelay_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGDelay.RowsAdded
        Dim brs As Integer
        Dim tg As Date

        Try
            brs = DGDelay.CurrentCell.RowIndex
            tg = GetServerDate()
            If DGDelay.Item(3, brs).Value Is DBNull.Value Then
                DGDelay.Item(2, brs).Value = Mid(Format(CDate(tg), "dddd"), 1, 3)
                DGDelay.Item(3, brs).Value = tg
            End If

            If DGDelay.Item(4, brs).Value Is DBNull.Value Then DGDelay.Item(4, brs).Value = "08"
            If DGDelay.Item(5, brs).Value Is DBNull.Value Then DGDelay.Item(5, brs).Value = "00"
            If DGDelay.Item(6, brs).Value Is DBNull.Value Then DGDelay.Item(6, brs).Value = "24"
            If DGDelay.Item(7, brs).Value Is DBNull.Value Then DGDelay.Item(7, brs).Value = "00"
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGDelay_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGDelay.TabIndexChanged

    End Sub

    Private Sub DGDelay_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGDelay.Validating
    End Sub

    Private Sub DGDateTime_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDateTime.CellContentClick

    End Sub
End Class