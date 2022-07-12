''' <summary>
''' Title                         : Funds & Finance - BPjUM
''' SubForm                       : FrmBPjUM
''' Table Used                    : 
''' Stored Procedure Used (MySQL) : SaveBPJUM, RejectBPJUM
''' Created By                    : Yanti, 07.07.2009
''' 
''' 
''' </summary>
''' <remarks></remarks>


Public Class FrmBPJUM
    Public ClientDecimalSeparator, ClientGroupSeparator As String
    Public RegionalSetting As System.Globalization.CultureInfo
    Public ServerDecimal, ServerSeparator As String
    Dim strSQL, strSQLA, errMSg, BLStatus, BLNo As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim DataError, Edit, Baru As Boolean
    Dim BPJUMNo, ShipNo, ShipOrdNo As Integer
    Dim arrBank() As String
    Dim BankBranch As String

    Sub New(ByVal No As Integer, ByVal Ship As Integer, ByVal ShipOrd As Integer, ByVal BLStat As String, ByVal BLNum As String)
        Dim tg As Date

        BPJUMNo = No
        ShipNo = Ship
        ShipOrdNo = ShipOrd
        BLStatus = BLStat
        BLNo = BLNum
        InitializeComponent()
        tg = GetServerDate()
        dt2.Value = tg
        dt3.Value = tg

        If Trim(BPJUMNo) <> 0 Then
            Edit = True
            Baru = False
            Call DisplayData()
            btnSave.Enabled = (btnSave.Enabled) And (CRTCODE.Text = UserData.UserCT)
            btnReject.Enabled = (btnReject.Enabled) And (CRTCODE.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("PP-P")) Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
        Else
            btnReject.Enabled = False
            btnPrint.Enabled = False
            dt1.Value = tg
            dt2.Value = tg
            dt3.Value = tg
            crtdt.Text = Microsoft.VisualBasic.Left(tg.ToString, 10)
            totalBPUM.Text = FormatNumber(0, 2, , , TriState.True)
            totalBPJUM.Text = FormatNumber(0, 2, , , TriState.True)
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            Me.Text = "BPJUM - New"
            Edit = False
            Baru = True
        End If
    End Sub
    Private Function GetBPUMOrdNo() As String
        Dim temp As String = ""
        Dim cnt, a As Integer
        Dim strBPUMNo() As String

        ' change
        'cnt = BPUMOrdNo.Lines.Count - 1
        'For a = 0 To cnt
        'If temp <> "" Then temp = temp & ","
        'temp = temp & Chr(39) & BPUMOrdNo.Lines(a) & Chr(39)
        'Next

        strBPUMNo = BPUMOrdNo.Text.Split(",")
        For a = LBound(strBPUMNo) To UBound(strBPUMNo)
            If temp <> "" Then temp = temp & ","
            temp = temp & Chr(39) & strBPUMNo(a) & Chr(39)
        Next
        ' -----

        GetBPUMOrdNo = temp
        If GetBPUMOrdNo <> "" Then GetBPUMOrdNo = "(" & GetBPUMOrdNo & ")"
    End Function
    Private Sub GetCostData(ByVal StatData As Integer)
        Dim tot As Decimal
        Dim dts, dts2 As DataTable
        Dim DT As New System.Data.DataTable
        Dim cbn As New DataGridViewComboBoxColumn
        Dim strBPUMOrdNo As String
        Dim sdata As String

        grid.DataSource = Nothing
        grid.Columns.Clear()
        errMSg = "Cost data view failed"

        If BPUMDocNo.Lines.Count = 0 Then Exit Sub

        If StatData = 0 Then 'existing data dari BPJUM
            strSQLA = "SELECT b.findoc_note FROM tbl_shipping_doc AS b " & _
                     "WHERE b.shipment_no = " & ShipNo & " AND b.ord_no = " & BPJUMNo & " AND b.findoc_TYPE = 'PP'"

            'strSQL = "SELECT t1.cost_code, t1.cost_description, " & _
            '         "IF(t1.bpum_amount IS NULL, 0, t1.bpum_amount) AS bpum_amount, t1.bpjum_amount " & _
            '         "FROM (" & _
            '         "  SELECT a.cost_code, trim(a.cost_description) cost_description, b.findoc_reff, a.cost_amount bpjum_amount, " & _
            '         "      (" & _
            '         "          SELECT SUM(coalescce(st1.cost_amount,0)) " & _
            '         "          FROM tbl_cost st1 " & _
            '         "          WHERE st1.type_code='DP' AND st1.cost_code = a.cost_code " & _
            '         "              AND b.findoc_reff LIKE CONCAT('%',st1.shipment_no,';',st1.ship_ord_no,'%')" & _
            '         "      ) bpum_amount " & _
            '         "  FROM tbl_Cost as a, tbl_shipping_doc as b " & _
            '         "  WHERE a.shipment_no=b.shipment_no AND a.ship_ord_no=b.ord_no AND a.type_code=b.findoc_type " & _
            '         "      and a.shipment_no = " & ShipNo & " AND b.ord_no = " & BPJUMNo & " AND b.findoc_TYPE = 'PP' " & _
            '         ") t1 "

            'strSQL = "" & _
            '    " SELECT a.cost_code, a.cost_description, SUM(coalesce(c.bpum_amount,0)) bpum_amount, a.bpjum_amount, id" & _
            '    " FROM (" & _
            '    "   SELECT a.cost_code, a.cost_description, a.bpjum_amount, @rownum:=@rownum + 1 AS id, b.findoc_reff" & _
            '    "   FROM (" & _
            '    " 	    SELECT cost_code, cost_description, cost_amount bpjum_amount, shipment_no, ship_ord_no, type_code " & _
            '    " 	    FROM tbl_Cost " & _
            '    " 	    WHERE shipment_no = " & ShipNo & _
            '    "   ) a" & _
            '    "   INNER JOIN tbl_shipping_doc AS b ON a.shipment_no = b.shipment_no " & _
            '    " 	    AND a.ship_ord_no = b.ord_no " & _
            '    " 	    AND a.type_code = b.findoc_type " & _
            '    " 	    AND b.ord_no = " & BPJUMNo & _
            '    " 	    AND b.findoc_TYPE = 'PP' " & _
            '    "   CROSS JOIN (SELECT @rownum:=0) z" & _
            '    " ) a" & _
            '    " LEFT OUTER JOIN (" & _
            '    " 	SELECT cost_code, shipment_no, ship_ord_no, cost_amount bpum_amount, CONCAT(shipment_no,';',ship_ord_no) AS id2 " & _
            '    " 	FROM tbl_Cost " & _
            '    " 	WHERE type_code = 'DP'" & _
            '    " ) AS c ON c.cost_code = a.cost_code  AND a.findoc_reff = c.id2 AND a.findoc_reff = c.shipment_no" & _
            '    " GROUP BY id, c.shipment_no, c.ship_ord_no, a.cost_code, a.cost_description"
            strSQL = "call getResultSet(" & ShipNo & "," & BPJUMNo & ")"
        Else 'tambahan BPUM
            strBPUMOrdNo = GetBPUMOrdNo()

            strSQLA = "SELECT b.findoc_note FROM tbl_shipping_doc AS b " & _
                     "WHERE CONCAT(b.shipment_no,';',b.ord_no) IN " & strBPUMOrdNo & " AND b.findoc_TYPE = 'DP'"

            strSQL = "SELECT a.cost_code, GROUP_CONCAT(DISTINCT trim(a.cost_description) SEPARATOR ' ') cost_description, SUM(a.cost_amount) bpum_amount, SUM(a.cost_amount) bpjum_amount " & _
                     "FROM (SELECT a.cost_code, a.cost_description, a.cost_amount " & _
                     "      FROM tbl_Cost as a, tbl_shipping_doc as b " & _
                     "      WHERE a.shipment_no=b.shipment_no AND a.ship_ord_no=b.ord_no AND a.type_code=b.findoc_type " & _
                     "      AND concat(a.shipment_no,';',b.ord_no) IN " & strBPUMOrdNo & " AND b.findoc_TYPE = 'DP' " & _
                     ") a GROUP BY a.cost_code "
        End If

        MyReader = DBQueryMyReader(strSQLA, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                remark.Text = MyReader.GetString("findoc_note")
            End While
        End If
        CloseMyReader(MyReader, UserData)

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        grid.DataSource = dts

        'Combo Box Document
        errMSg = "Tbm_CostCategory data view failed"
        strSQL = "select costcat_code,costcat_name from tbm_costcategory where costcat_code like '3%' order by costcat_name"
        dts2 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn
            .DataSource = dts2
            .DisplayMember = "COSTCAT_NAME"
            .ValueMember = "COSTCAT_CODE"
        End With

        grid.Columns.Insert(1, cbn)
        grid.Columns(0).Visible = False
        grid.Columns(1).Width = 150
        grid.Columns(1).HeaderText = "Item Cost"
        grid.Columns(2).HeaderText = "Description"
        grid.Columns(2).Width = 250
        grid.Columns(3).Width = 100
        grid.Columns(4).Width = 100
        grid.Columns(3).HeaderText = "BPUM Amount"
        grid.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
        grid.Columns(3).DefaultCellStyle.Format = "N2"
        grid.Columns(4).HeaderText = "BPJUM Amount"
        grid.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grid.Columns(4).DefaultCellStyle.Format = "N2"

        UpdateCombo()

        tot = GetTotal()
        totalBPJUM.Text = FormatNumber(tot, 2, , , TriState.True)
        tot = GetTotalBPUM()
        totalBPUM.Text = FormatNumber(tot, 2, , , TriState.True)
        grid.Columns(0).ReadOnly = True
        grid.Columns(1).ReadOnly = False
        grid.Columns(2).ReadOnly = False
        grid.Columns(3).ReadOnly = True
        grid.Columns(4).ReadOnly = False
        grid.Columns(0).DefaultCellStyle.BackColor = Color.Gray
        grid.Columns(3).DefaultCellStyle.BackColor = Color.Gray
        '''grid.AllowUserToAddRows = False
    End Sub
    Private Function GetValue(ByVal field As String) As String
        Try
            GetValue = MyReader.GetString(field)
        Catch ex As Exception
            GetValue = ""
        End Try
    End Function
    Private Sub DisplayData()
        Dim temp1, temp2, temp3, temp4, temp5, temp6 As String
        Dim tot, totRefund As Decimal

        Me.Text = "BPJUM #" & Trim(BPJUMNo) & " - Update"
        btnSave.Text = "Update"
        errMSg = "BPJUM data view failed"
        strSQL = "select * from tbl_shipping_doc where shipment_no=" & ShipNo & " and ord_no=" & BPJUMNo & " and findoc_type='PP'"
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
                    temp4 = MyReader.GetString("FinDoc_CreatedBy")
                    crtdt.Text = MyReader.GetString("FinDoc_CreatedDt")
                    status.Text = MyReader.GetString("FinDoc_Status")
                    temp5 = GetValue("FinDoc_Reff")
                    totRefund = GetValue("FinDoc_ValAmt2")

                    temp6 = MyReader.GetString("FinDoc_GroupTo")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            expcode.Text = temp1
            appcode.Text = temp2
            fincode.Text = temp3
            CRTCODE.Text = temp4
            BPUMOrdNo.Text = temp5    '==> otomatis call getcostData

            If (temp6 = "" Or IsDBNull(temp6)) Then
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
        End If
        DisplayBPUMDocNo()

        GetCostData(0)
    End Sub
    Private Sub DisplayBPUMDocNo()
        Dim cnt, brs As Integer
        Dim Dat As String
        Dim temp As String = ""
        Dim temp1 As String = ""
        Dim strBPUMNo() As String
        Dim shipord, xshipno, xord As String

        'cnt = BPUMOrdNo.Lines.Count - 1
        'strBPUMNo = BPUMOrdNo.Text.Split()
        'For a = 0 To cnt
        strBPUMNo = BPUMOrdNo.Text.Split(",")
        For a = LBound(strBPUMNo) To UBound(strBPUMNo)
            shipord = strBPUMNo(a)
            xshipno = Mid(shipord, 1, InStr(shipord, ";") - 1)
            xord = Mid(shipord, InStr(shipord, ";") + 1, Len(shipord) - InStr(shipord, ";"))
            If temp <> "" Then temp = temp & vbCrLf ' Chr(13) & Chr(10)
            If temp1 <> "" Then temp1 = temp1 & ";"
            Dat = AmbilData("b.findoc_no", _
                   "tbl_Cost as a inner join tbl_shipping_doc as b on a.shipment_no=b.shipment_no and a.ship_ord_no=b.ord_no and a.type_code=b.findoc_type", _
                   "a.shipment_no=" & xshipno & " and b.ord_no = " & xord & " and b.findoc_TYPE = 'DP'")

            temp = temp & Dat
            temp1 = temp1 & Dat
        Next
        BPUMDocNo.Text = temp
        FillBPUMDocNo.Text = temp1 & ";"
    End Sub
    Private Sub FrmBPJUM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        Dim ee As System.EventArgs
        BPUMDocNo_TextChanged(BPUMDocNo, ee)   'supaya grid combo keupdate
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientGroupSeparator, ServerSeparator)
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
        Dim str As String = ""

        GridOK = True
        Try
            str = grid.Item(0, baris).Value
        Catch ex As Exception
        End Try
        If str <> "" Then
            val = grid.Item(4, baris).Value
            If val < 0 Then
                grid.Focus()
                grid.CurrentCell = grid(4, baris)
                MsgBox("Amount should be >= 0")
                GridOK = False
            End If
        End If
    End Function
    Private Function CekDataHeader() As Boolean
        Dim num As Decimal

        CekDataHeader = True

        If expname.Text = "" Then
            MsgBox("Expedition not found")
            expname.Focus()
            CekDataHeader = False
            Exit Function
        End If
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim tgl1, tgl2, tgl3, str, DetailCost, DetailBPUMOrdNo, SQLStr, RetMess, No, Desc, Amt, Pers, tot, totRefund As String
        Dim hasil As Boolean = False
        Dim cnt, brs As Integer
        Dim DT As New System.Data.DataTable
        Dim num0, num1 As Decimal
        Dim ee As System.EventArgs
        Dim strBPUMNo() As String
        Dim BankStr As String
        'Dim tot As Decimal

        If Edit And BLStatus = "Closed" Then
            MsgBox("BPJUM has been closed, can't save data")
            Exit Sub
        End If
        'If Baru = True Then                         'klo dah di save grid gak boleh di edit
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
        'End If
        If CekDataHeader() = False Then Exit Sub
        DetailCost = ""
        DetailBPUMOrdNo = ""
        tgl1 = Format(dt1.Value, "yyyy-MM-dd")
        tgl2 = Format(dt2.Value, "yyyy-MM-dd")
        tgl3 = Format(dt3.Value, "yyyy-MM-dd")
        tot = GetNum2(totalBPJUM.Text)

        BankStr = bankname.Text & ";" & BankBranch & ";" & accno.Text & ";" & accname.Text

        'If Baru Then
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
            Try
                No = Microsoft.VisualBasic.Mid(No & "     ", 1, 5)
                Desc = DT.Rows(cnt - 1).Item(1).ToString
                Desc = Microsoft.VisualBasic.Mid(Desc & "                                        ", 1, 40)
                num0 = CDec(DT.Rows(cnt - 1).Item(2).ToString)
                num1 = CDec(DT.Rows(cnt - 1).Item(3).ToString)
                num0 = num0 + num1
                str = GetNum2(num1)
                Amt = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                If (No <> "" And num0 > 0) Then DetailCost = DetailCost & No & Desc & Amt & ";"
            Catch ex As Exception
            End Try
        Next
        'End If

        ' change
        'cnt = BPUMOrdNo.Lines.Count - 1
        'For brs = 0 To cnt
        'Desc = Trim(BPUMOrdNo.Lines(brs))
        'Desc = Microsoft.VisualBasic.Mid(Desc & Space(11), 1, 11)
        'DetailBPUMOrdNo = DetailBPUMOrdNo & Desc & ";"
        'Next

        strBPUMNo = BPUMOrdNo.Text.Split(",")
        For a = LBound(strBPUMNo) To UBound(strBPUMNo)
            Desc = Trim(strBPUMNo(a))
            Desc = Microsoft.VisualBasic.Mid(Desc & Space(11), 1, 11)
            DetailBPUMOrdNo = DetailBPUMOrdNo & Desc & ";"
        Next
        ' ------

        If Baru Then
            SQLStr = "Run Stored Procedure SaveBPJUM (Save" & "," & ShipNo & "," & "0"
        Else
            SQLStr = "Run Stored Procedure SaveBPJUM (Update" & "," & ShipNo & "," & BPJUMNo
        End If
        SQLStr = SQLStr & "," & docno.Text & "," & tgl1 & "," & expcode.Text & "," & BankStr & "," & remark.Text & "," & appcode.Text & "," & If(dt2.Checked, tgl2, "") _
                 & "," & fincode.Text & "," & If(dt3.Checked, tgl3, "") & "," & tot & "," & If(appcode.Text = "", "Open", "Approved") & "," & UserData.UserCT & "," & DetailBPUMOrdNo & ")"
        '& "," & fincode.Text & "," & If(dt3.Checked, tgl3, "") & "," & tot & "," & totRefund & "," & If(appcode.Text = "", "Open", "Approved") & "," & UserData.UserCT & "," & DetailBPUMOrdNo & ")"
        'If Baru Then
        SQLStr = SQLStr & "," & DetailCost
        'End If

        With MyComm
            .CommandText = "SaveBPJUM"
            .CommandType = CommandType.StoredProcedure

            With .Parameters
                .Clear()
                .AddWithValue("jenis", If(Baru, "Save", "Update"))
                .AddWithValue("ShipNo", ShipNo)
                .AddWithValue("UpdateOrdNo", If(Baru, 0, ShipOrdNo))
                .AddWithValue("DocNo", docno.Text)
                .AddWithValue("DocReff", BPUMOrdNo.Text)
                .AddWithValue("PrintDt", tgl1)
                .AddWithValue("DocTo", expcode.Text)
                .AddWithValue("DocGroupTo", BankStr)
                .AddWithValue("DocNote", remark.Text)
                .AddWithValue("AppBy", appcode.Text)
                .AddWithValue("AppDt", If(dt2.Checked, tgl2, ""))
                .AddWithValue("FinBy", fincode.Text)
                .AddWithValue("FinDt", If(dt3.Checked, tgl3, ""))
                .AddWithValue("FinTotal", CDec(totalBPJUM.Text))
                .AddWithValue("stat", If(appcode.Text = "", "Open", "Approved"))
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("DetailBPUMOrdNo", DetailBPUMOrdNo)
                .AddWithValue("CostDetail", DetailCost)
                .AddWithValue("AuditStr", SQLStr)
                .AddWithValue("Hasil", hasil)
                .AddWithValue("Message", RetMess)
            End With
            '.AddWithValue("FinTotal2", CDec(ExpRefund.Text))
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
                RetMess = .Parameters("message").Value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End With

        If hasil = True Then
            f_msgbox_successful(btnSave.Text & " BPJUM")
            Me.Close()
            Me.Dispose()
        Else
            MsgBox(btnSave.Text & " BPJUM failed'")
        End If
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, DetailBPUMOrdNo, desc As String
        Dim cnt As Integer
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If Edit And status.Text = "Rejected" Then
            MsgBox("Payment Voucher has been rejected")
            Exit Sub
        End If
        If Edit And BLStatus = "Closed" Then
            MsgBox("BPJUM has been closed, can't reject")
            Exit Sub
        End If
        DetailBPUMOrdNo = ""
        cnt = BPUMOrdNo.Lines.Count - 1
        For brs = 0 To cnt
            Desc = Trim(BPUMOrdNo.Lines(brs))
            Desc = Microsoft.VisualBasic.Mid(Desc & Space(11), 1, 11)
            DetailBPUMOrdNo = DetailBPUMOrdNo & Desc & ";"
        Next

        msg = "Reject BPJUM #" & BPJUMNo & " of BL " & BLNo & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            MyComm.CommandText = "RejectBPJUM"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("ShipNo", ShipNo)
            MyComm.Parameters.AddWithValue("OrdNo", ShipOrdNo)
            MyComm.Parameters.AddWithValue("DetailBPUMOrdNo", DetailBPUMOrdNo)
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
        Dim ee As System.EventArgs

        btnClearD_Click(sender, ee)

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
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'PP-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'PP-A'" & _
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

        BPUMDocNo.Text = ""
        grid.DataSource = Nothing
        grid.Columns.Clear()
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

    Private Sub grid_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grid.CellBeginEdit
        Dim brs As Integer

        If BPUMDocNo.Text = "" Then
            MsgBox("Fill in Reff No.")
            BPUMDocNo.Focus()
            grid.CancelEdit()
        Else
            Try
                brs = grid.CurrentCell.RowIndex
                If grid.Item(4, brs).Value.ToString = "" Then grid.Item(4, brs).Value = 0
                If grid.Item(3, brs).Value.ToString = "" Then grid.Item(3, brs).Value = 0
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub grid_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid.CellEndEdit
        Dim tot As Decimal
        Dim brs As Integer

        Try
            brs = grid.CurrentCell.RowIndex
            grid.Item(0, brs).Value = grid.Item(1, brs).Value
        Catch ex As Exception
            grid.Item(1, brs).Value = ""
        End Try

        tot = GetTotal()
        totalBPJUM.Text = FormatNumber(tot, 2, , , TriState.True)
        tot = GetTotalBPUM()
        totalBPUM.Text = FormatNumber(tot, 2, , , TriState.True)
    End Sub

    Private Sub grid_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.CurrentCellDirtyStateChanged
        If DataError = True And grid.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid.DataError
        MsgBox("Invalid amount, input numeric value")
        DataError = True
    End Sub
    Private Function GetTotal() As Decimal
        Dim DT As System.Data.DataTable
        Dim brs, cnt As Integer
        Dim tot, temp As Decimal

        DT = grid.DataSource
        brs = DT.Rows.Count - 1
        tot = 0

        For cnt = 0 To brs
            If IsDBNull(grid.Rows(cnt).Cells(4).Value) Then grid.Rows(cnt).Cells(4).Value = 0
            temp = grid.Rows(cnt).Cells(4).Value
            tot = tot + temp
        Next
        GetTotal = tot
    End Function

    Private Function GetTotalBPUM() As Decimal
        Dim DT As System.Data.DataTable
        Dim brs, cnt As Integer
        Dim tot, temp As Decimal

        DT = grid.DataSource
        brs = DT.Rows.Count - 1
        tot = 0

        For cnt = 0 To brs
            If IsDBNull(grid.Rows(cnt).Cells(3).Value) Then grid.Rows(cnt).Cells(3).Value = 0
            temp = grid.Rows(cnt).Cells(3).Value
            tot = tot + temp
        Next
        GetTotalBPUM = tot
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        If Len(CStr(BPJUMNo)) = 1 Then
            v_num = " " & BPJUMNo
        Else
            v_num = BPJUMNo.ToString
        End If
        ViewerFrm.Tag = "BPJU" & v_num & ShipNo
        ViewerFrm.ShowDialog()
    End Sub
    Private Sub CRTCODE_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRTCODE.TextChanged
        crt.Text = AmbilData("NAME", "tbm_users", "user_ct=" & CRTCODE.Text)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim strSQL, strSQLF, temp, strtemp, DPOrd, mess As String
        Dim a As Integer

        If expcode.Text = "" Then
            MsgBox("expedition should be filled")
            Exit Sub
        End If
        If Baru Then
            'strSQL = "SELECT t2.bl_no AS BLNo, t1.findoc_no AS BPUMNo, t1.shipment_no AS ShipmentNo, t1.ord_no AS NO FROM tbl_shipping_Doc t1, tbl_shipping t2 WHERE t1.shipment_no=t2.shipment_no AND t1.findoc_type='DP' AND (t1.findoc_reff='' OR t1.findoc_reff IS NULL) AND t1.findoc_status<>'Rejected' AND t1.findoc_to='" & expcode.Text & "' AND t1.shipment_no = " & ShipNo & ""
            strSQL = "SELECT t2.bl_no AS BLNo, t1.findoc_no AS BPUMNo, format(t1.findoc_valamt,2) AS Amount, t1.shipment_no AS ShipmentNo, t1.ord_no AS No FROM tbl_shipping_Doc t1, tbl_shipping t2 WHERE t1.shipment_no=t2.shipment_no AND t1.findoc_type='DP' AND (t1.findoc_reff='' OR t1.findoc_reff IS NULL) AND t1.findoc_status<>'Rejected' AND t1.findoc_to='" & expcode.Text & "' AND t1.shipment_no = " & ShipNo & ""
        Else
            strSQL = "SELECT t2.bl_no AS BLNo, t1.findoc_no AS BPUMNo, format(t1.findoc_valamt,2) AS Amount, t1.shipment_no AS ShipmentNo, t1.ord_no AS No FROM tbl_shipping_Doc t1, tbl_shipping t2 WHERE t1.shipment_no=t2.shipment_no AND findoc_type='DP' AND t1.findoc_status<>'Rejected' and t1.findoc_to='" & expcode.Text & "' AND t1.shipment_no = " & ShipNo & ""
        End If

        strSQLF = strSQL & "AND t2.bl_no like 'FilterData1%' and t1.findoc_no like 'FilterData2%'"
        PilihanDlg.Text = "Select BPUM"
        PilihanDlg.LblKey1.Text = "BL No"
        PilihanDlg.LblKey2.Text = "BPUM No"
        PilihanDlg.SQLGrid = strSQL & " ORDER BY t2.bl_no, t1.findoc_no, t1.ord_no"
        PilihanDlg.SQLFilter = strSQLF & " ORDER BY t2.bl_no, t1.findoc_no, t1.ord_no"
        PilihanDlg.Tables = "tbl_shipping_doc"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            DPOrd = PilihanDlg.DgvResult.CurrentRow.Cells.Item(3).Value.ToString & ";" & PilihanDlg.DgvResult.CurrentRow.Cells.Item(4).Value.ToString
            temp = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            If FillBPUMDocNo.Find(temp & ";") >= 0 Then
                mess = "BPUM No. " & temp & " already choosed"
                MsgBox(mess)
            Else
                If BPUMDocNo.Lines.Length >= 1 Then
                    BPUMDocNo.Text = BPUMDocNo.Text & vbCrLf ' Chr(13) & Chr(10)
                End If

                If BPUMOrdNo.Lines.Length >= 1 Then
                    'BPUMOrdNo.Text = BPUMOrdNo.Text & vbCrLf & DPOrd ' Chr(13) & Chr(10)
                    BPUMOrdNo.Text = BPUMOrdNo.Text & "," & DPOrd
                Else
                    BPUMOrdNo.Text = DPOrd
                End If

                If docno.Lines.Length >= 1 Then docno.Text = docno.Text & ","

                ''BPUMOrdNo.Text = BPUMOrdNo.Text & DPOrd
                BPUMDocNo.Text = BPUMDocNo.Text & temp
                docno.Text = docno.Text & temp

                If Len(FillBPUMDocNo.Text) > 0 Then
                    If Mid(FillBPUMDocNo.Text, Len(FillBPUMDocNo.Text), 1) <> ";" Then FillBPUMDocNo.Text = FillBPUMDocNo.Text & ";"
                End If
                FillBPUMDocNo.Text = FillBPUMDocNo.Text & temp & ";"
            End If

            GetCostData(1)
        End If
    End Sub

    Private Sub btnClearD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearD.Click
        BPUMDocNo.Text = ""
        BPUMOrdNo.Text = ""
        FillBPUMDocNo.Text = ""
        docno.Text = ""
        remark.Text = ""
        totalBPJUM.Text = FormatNumber(0, 2, , , TriState.True)
        totalBPUM.Text = FormatNumber(0, 2, , , TriState.True)
        grid.DataSource = Nothing
        grid.Columns.Clear()
    End Sub

    Private Sub BPUMDocNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BPUMDocNo.TextChanged
        GetCostData(0)
    End Sub

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub totalBPUM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles totalBPUM.TextChanged
        Dim tot As Decimal
        tot = GetNum2(totalBPUM.Text) - GetNum2(totalBPJUM.Text)
        Refund.Text = FormatNumber(tot, 2, , , TriState.True)
    End Sub

    Private Sub totalBPJUM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles totalBPJUM.TextChanged
        Dim tot As Decimal
        tot = GetNum2(totalBPUM.Text) - GetNum2(totalBPJUM.Text)
        Refund.Text = FormatNumber(tot, 2, , , TriState.True)
    End Sub

    Private Sub grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid.CellContentClick

    End Sub

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

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub
End Class