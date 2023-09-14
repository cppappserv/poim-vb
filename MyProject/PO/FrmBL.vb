''' <summary>
''' Title                         : Transaksi Bill of Lading ==> menjadi Port Import Document
''' Form                          : FrmBL
''' Table Used                    :
''' Stored Procedure Used (MySQL) : SaveBL, UpdateBL, DelBL
''' Created By                    : Yanti 12.01.2009
''' Note                          : Form ini dipakai oleh menu :
'''                                 1. Transaksi Bill of Lading 
'''                                 2. Logistic Documents   => menjadi Post Import Document
'''                                 3. Financial Documents  => menjadi Funds & Finance
'''                                 Source Code public dipakai oleh menu :
'''                                 1. Customs Documents
''' </summary>
''' <remarks></remarks>

Public Class FrmBL
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim strSQL, errMSg, PrevPO As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Dim DataError, POBaru, Baru, Edit, Display As Boolean
    Dim PilihBL As New FrmListBL
    Dim DT As New System.Data.DataTable
    Dim DT_i As New System.Data.DataTable
    Dim MaxReadOnly As SByte
    Dim Kurs As Decimal = 0
    Dim isiPlant As Boolean
    Dim purchase_sts As Boolean
    Dim xblno As String
    Dim chosen As String
    Dim Chosen2 As String
    Dim RstQ0 As DataTableReader

    Structure MyStruc
        Dim DSPO As System.Data.DataTable
        Dim DSInv As System.Data.DataTable
        Dim DSPlant As System.Data.DataTable
    End Structure
    Public Array(0) As MyStruc
    Private Sub SaveData(ByVal brs As Integer, ByVal grd As DataGridView, ByVal grdInv As DataGridView, ByVal grdPlant As DataGridView)
        Array(brs).DSPO = grd.DataSource
        Array(brs).DSInv = grdInv.DataSource
        Array(brs).DSPlant = grdPlant.DataSource
    End Sub
    Structure MyStruc2
        Dim DSSuppDoc As System.Data.DataTable
        Dim DSCustomDoc As System.Data.DataTable
        Dim DSContainer As System.Data.DataTable
        Dim DSExpeditionDoc As System.Data.DataTable
    End Structure
    Public Array2(0) As MyStruc2
    Private Sub SaveData2()
        Array2(0).DSSuppDoc = grid2.DataSource
        Array2(0).DSCustomDoc = grid3.DataSource
        Array2(0).DSContainer = grid4.DataSource
        Array2(0).DSExpeditionDoc = grid5.DataSource
    End Sub
    Structure MyStruc3
        Dim Inv As Decimal
        Dim MaxDate As String
        Dim Bea As Decimal
        Dim PPN As Decimal
        Dim PPH As Decimal
        Dim PIUD As Decimal
    End Structure
    Public Array3(0) As MyStruc3
    Public Array4(0) As MyStruc3
    Private Sub ClearArray3(ByVal ListIdx As Integer)
        Array3(ListIdx).MaxDate = ""
        Array3(ListIdx).Bea = 0
        Array3(ListIdx).PPN = 0
        Array3(ListIdx).Inv = 0
        Array3(ListIdx).PPH = 0
        Array3(ListIdx).PIUD = 0
    End Sub
    Private Sub SaveData3(ByVal ListIdx As Integer, ByVal KursPjk As Decimal)
        Dim mat, countryCode, no As String
        Dim xcek, angka, BeaMat, PPNMat, PPHMat, PIUDMat, BeaPO, PPNPO, PPHPO, PIUDPO, InvPO As Decimal
        Dim brs, jml As SByte

        If ListIdx < 0 Then Exit Sub
        If ListIdx > Array3.Length - 1 Then ReDim Preserve Array3(ListIdx)

        BeaPO = 0
        PPNPO = 0
        PPHPO = 0
        PIUDPO = 0
        InvPO = 0

        DT = Array(ListIdx).DSPO
        jml = DT.Rows.Count

        mat = Trim(DT.Rows(0).Item(1).ToString)
        countryCode = Trim(DT.Rows(0).Item(11).ToString)
        PIUDMat = GetNilai("PIUD_TR", "tbm_material_origin", "material_code='" & mat & "' and country_code='" & countryCode & "'")

        If IsDBNull(PIUDMat) Then
            MsgBox("Please check Material Origin Master of this data! ", MsgBoxStyle.Critical, "Warning")
            PIUDPO = 0
        Else
            PIUDPO = PIUDMat

            For brs = 0 To jml - 1
                angka = DT.Rows(brs).Item(8).ToString
                If angka > 0 Then

                    DT_i = Array(ListIdx).DSInv
                    InvPO = DT_i.Rows(brs).Item(3).ToString

                    mat = Trim(DT.Rows(brs).Item(1).ToString)
                    countryCode = Trim(DT.Rows(brs).Item(11).ToString)

                    If cbExclude.Checked = False Then
                        BeaMat = GetNilai("bea_masuk", "tbm_material_origin", "material_code='" & mat & "' and country_code='" & countryCode & "'")
                        BeaMat = (BeaMat / 100) * InvPO
                    Else
                        BeaMat = 0
                    End If
                    PPNMat = GetNilai("PPN", "tbm_material_origin", "material_code='" & mat & "' and country_code='" & countryCode & "'")
                    PPHMat = GetNilai("PPH_21", "tbm_material_origin", "material_code='" & mat & "' and country_code='" & countryCode & "'")

                    BeaPO = BeaPO + BeaMat

                    PPNPO = PPNPO + ((PPNMat / 100) * (InvPO + BeaMat))
                    PPHPO = PPHPO + ((PPHMat / 100) * (InvPO + BeaMat))
                End If
            Next
        End If
        BeaPO = BeaPO * KursPjk
        PPNPO = PPNPO * KursPjk
        PPHPO = PPHPO * KursPjk
        Array3(ListIdx).Bea = GetRounded(BeaPO, 1)
        Array3(ListIdx).PPN = GetRounded(PPNPO, 1)
        Array3(ListIdx).PPH = GetRounded(PPHPO, 1)
        Array3(ListIdx).PIUD = GetRounded(PIUDPO, 1)
    End Sub

    Private Function GetNilai(ByVal field As String, ByVal tabel As String, ByVal where As String) As Decimal
        Try
            GetNilai = CDec(AmbilData(field, tabel, where))
        Catch ex As Exception
            GetNilai = 0
        End Try
    End Function

    Private Sub UpdateDisplayBea()
        Dim a, ListIdx As SByte
        Dim TotBea, TotPPH, TotPPN, TotPIUD As Decimal

        ListIdx = List.Items.Count - 1
        TotBea = 0
        TotPPH = 0
        TotPPN = 0
        TotPIUD = 0
        For a = 0 To ListIdx
            TotBea = TotBea + Array3(a).Bea
            TotPPH = TotPPH + Array3(a).PPH
            TotPPN = TotPPN + Array3(a).PPN
            'piud di hitung per shipment
            'TotPIUD = TotPIUD + Array3(a).PIUD
            TotPIUD = Array3(a).PIUD
        Next
        BeaMasuk.Text = FormatNumber(Math.Floor(TotBea), 0, , , TriState.True)
        pph.Text = FormatNumber(Math.Floor(TotPPH), 0, , , TriState.True)
        vat.Text = FormatNumber(Math.Floor(TotPPN), 0, , , TriState.True)
        piud.Text = FormatNumber(Math.Floor(TotPIUD), 0, , , TriState.True)
    End Sub
    Private Function GetMaxDateOnePO(ByVal ListIdx As Integer) As String
        Dim jml As SByte
        Dim no, tgl, tempTgl As String
        Dim angka As Decimal
        Dim tg1, tg2 As Date

        If GridInv.DataSource Is Nothing Then
            GetMaxDateOnePO = ""
            Exit Function
        End If
        tempTgl = ""
        If ListIdx > Array3.Length - 1 Then ReDim Preserve Array3(ListIdx)

        DT = Array(ListIdx).DSInv
        jml = DT.Rows.Count
        For brs = 0 To jml - 1
            no = DT.Rows(brs).Item(0).ToString
            tgl = DT.Rows(brs).Item(1).ToString
            tgl = FormatDateTime(tgl, DateFormat.ShortDate)
            angka = DT.Rows(brs).Item(3).ToString
            If no <> "" And angka > 0 Then
                If (tempTgl = "") Then tempTgl = tgl
                Try
                    If (CDate(tgl) > CDate(tempTgl)) Then tempTgl = tgl
                Catch ex As Exception
                End Try
            End If
        Next
        GetMaxDateOnePO = tempTgl
    End Function
    Private Function GetMaxDateAllPO(ByVal excl As SByte) As String
        Dim idx, cnt As SByte
        Dim str, tgl1, tgl2 As String

        tgl1 = Nothing
        idx = List.Items.Count - 1

        For cnt = 0 To idx
            If cnt <> excl Then
                tgl2 = Array3(cnt).MaxDate
                If tgl2 <> "" Then
                    If tgl1 Is Nothing Then tgl1 = tgl2
                    If CDate(tgl2) > CDate(tgl1) Then tgl1 = tgl2
                End If
            End If
        Next

        If tgl1 Is Nothing Then tgl1 = ""
        GetMaxDateAllPO = tgl1
    End Function

    Private Function GetKursPajak(ByVal Excl As SByte) As Decimal
        Dim MaxDt As String

        'MaxDt = GetMaxDateAllPO(Excl)
        'Max Date di ambil langsung dari Ship On Board
        MaxDt = dtETD.Text
        If MaxDt <> "" Then
            GetKursPajak = GetNilai("Kurs_Pajak", "tbm_kurs", "currency_code='" & Currency.Text & "' and effective_date='" & Format(CDate(MaxDt), "yyyy-MM-dd") & "'")
        Else
            GetKursPajak = 0
        End If
    End Function

    Private Sub FrmBL_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        ''btnSave.Enabled = False
    End Sub

    Private Sub FrmBL_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub

    Private Sub FrmBLDoc_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ee As System.EventArgs
        'Dim value = New System.Drawing.Size(1019, 582)
        Dim value = New System.Drawing.Size(1023, 616)
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
    End Sub
    Function GetServerDate() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Function GetServerDate2() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select DATE_ADD(curdate(),INTERVAL +1 DAY)"
        MyComm.CommandType = CommandType.Text
        GetServerDate2 = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Private Sub RefreshBea()
        Dim qt0, qt2 As String

        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)
        BeaMasuk.Text = qt0
        vat.Text = qt0
        pph.Text = qt0
        piud.Text = qt0
        KursPajak.Text = qt2
        finalty.Text = qt2
    End Sub
    Private Sub ClearTab()
        Dim qt0, qt2 As String
        Dim tg As Date

        ReDim Array2(0)
        grid2.DataSource = Nothing
        grid2.Columns.Clear()
        grid3.DataSource = Nothing
        grid3.Columns.Clear()
        grid4.DataSource = Nothing
        grid4.Columns.Clear()
        grid5.DataSource = Nothing
        grid5.Columns.Clear()
        TotCont.Text = ""

        tg = GetServerDate()
        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)
        InsNo.Text = ""
        InsAmt.Text = qt2
        free.Text = qt0
        free_ext.Text = qt0
        CTPro.Text = ""
        dem.Text = qt0
        dtRec.Value = GetServerDate()
        free_ext_note.Text = ""
        exp_note.Text = ""
        Cur_Work.Text = qt0
        Cur_Eq.Text = qt0
        '''ChkTahanOB.Checked = False
        ChkRedLn.Checked = False
        ChkMCI.Checked = False
        ChkTrsIn.Checked = False
        ChkTrsEx.Checked = False
        ChkTreatment.Checked = False
        ChkEigen.Checked = False
        ChkCMA.Checked = False

        RefreshBea()
        receivedname.Text = ""
        receivecode.Text = ""
        vercode1.Text = ""
        vername1.Text = ""
        vercode2.Text = ""
        vername2.Text = ""
        bankname.Text = ""
        crtcode.Text = ""
        crtname.Text = ""
        accno.Text = ""
        BeaMasuk.Text = qt0
        vat.Text = qt0
        pph.Text = qt0
        piud.Text = qt0
        finalty.Text = qt2
        crtcode.Text = UserData.UserCT.ToString
        dtRec.Value = tg
        dtAreaDeptan.Value = tg
        dtAreaRec.Value = tg
        dtRecExp.Value = tg
        dtForwExp.Value = tg
        dtVerifySuppDoc.Value = tg
        dtRecBeaByAcc.Value = tg
        dtPIB.Value = tg
        dtSPPB.Value = tg
        dtAJU.Value = tg
        dtArrival.Value = tg
        dtOB.Value = tg
        dtEstDelivery.Value = tg
        dtDelivery.Value = tg
        dtEstClearance.Value = tg
        dtClearance.Value = tg
        sdem.Text = tg.ToString
        GetSelisihTgl()
    End Sub
    Private Function QtyAndPackageOK(ByVal baris As Integer, ByVal Qty1 As Decimal, ByVal Qty2 As Decimal, ByVal qty3 As Decimal) As Boolean
        Dim str As String

        'qty1 => available qty
        'qty2 => actual qty (input)
        'qty3 => package size

        QtyAndPackageOK = True
        Try
            str = grid.Item(9, baris).Value
        Catch ex As Exception
        End Try
        If Qty2 < 0 Then
            List.SelectedIndex = List.Items.IndexOf(PrevPO)
            POTab.SelectedIndex = 0
            grid.Focus()
            grid.CurrentCell = grid(8, baris)
            MsgBox("Actual Quantity should be > 0")
            QtyAndPackageOK = False
            Exit Function
        End If
        'package unit boleh kosong
        If qty3 <= 0 Then
            List.SelectedIndex = List.Items.IndexOf(PrevPO)
            POTab.SelectedIndex = 0
            grid.Focus()
            grid.CurrentCell = grid(11, baris)
            MsgBox("Package Size should be > 0")
            QtyAndPackageOK = False
            Exit Function
        End If
        If Qty2 <= 0 Or qty3 <= 0 Then Exit Function
        If (Qty2 > Qty1) Then
            Try
                grid.Focus()
                grid.CurrentCell = grid(8, baris)
            Catch ex As Exception
            End Try
            str = FormatNumber(Qty1, 2, , , TriState.True)
            List.SelectedIndex = List.Items.IndexOf(PrevPO)
            POTab.SelectedIndex = 0
            grid.Focus()
            grid.CurrentCell = grid(8, baris)
            MsgBox("Maximum Tolerable Actual Quantity " & str)
            QtyAndPackageOK = False
        End If
    End Function
    Function CekGrid() As Boolean
        CekGrid = CekCurrentPOInvGridData()
        If CekGrid = True Then CekGrid = CekGridHeader()
    End Function
    Function CekGridHeader() As Boolean
        Dim brs, cnt As Integer
        Dim str, str2, str3 As String

        CekGridHeader = True
        txtShipNo.Focus()   'invoke grid event, jika data terakhir yg diinput adalah grid
        If DataError = True Then Exit Function

        'Supplier Doc TAB
        Try
            brs = Array2(0).DSSuppDoc.Rows.Count
        Catch ex As Exception
            brs = 0
        End Try

        For cnt = 1 To brs
            Try
                str = Trim(grid2.Item(0, cnt - 1).Value.ToString)
                If (str <> "" And str <> "SD000") Then
                    Try
                        str2 = grid2.Item(1, cnt - 1).Value.ToString
                        str3 = grid2.Item(2, cnt - 1).Value.ToString
                    Catch ex As Exception
                    End Try
                    If str2 = "" Or str3 = "" Then
                        TabControl1.SelectedIndex = 0
                        grid2.Focus()
                        If Trim(str2) = "" Then
                            grid2.CurrentCell = grid2(1, cnt - 1)
                        Else
                            grid2.CurrentCell = grid2(2, cnt - 1)
                        End If
                        MsgBox("Supplier Doc Data should be filled")
                        CekGridHeader = False
                        Exit Function
                    End If
                End If
            Catch ex As Exception
            End Try
            If CekGridHeader = False Then Exit For
        Next
        If CekGridHeader = False Then Exit Function
        '=========end cek Supplier Doc

        'Customs Doc TAB
        'di tutup karena jika ada baris kosong selain 1 baris terakhir akan error
        'Try
        'brs = Array2(0).DSCustomDoc.Rows.Count
        'Catch ex As Exception
        'brs = 0
        'End Try

        'For cnt = 1 To brs
        'str = grid3.Item(0, cnt - 1).Value
        'If str <> "" Then
        'str2 = grid3.Item(1, cnt - 1).Value
        'Try
        'str3 = grid3.Item(2, cnt - 1).Value
        'Catch ex As Exception
        'End Try
        'If str2 = "" Or str3 = "" Then
        'TabControl1.SelectedIndex = 1
        'grid3.Focus()
        'If Trim(str2) = "" Then
        'grid3.CurrentCell = grid3(1, cnt - 1)
        'Else
        'grid3.CurrentCell = grid3(2, cnt - 1)
        'End If
        'MsgBox("Customs Doc Data should be filled")
        'CekGridHeader = False
        'Exit Function
        'End If
        'End If
        'If CekGridHeader = False Then Exit For
        'Next
        'if CekGridHeader = False Then Exit Function
        '=========end cek customs Doc

        'Container TAB
        Try
            brs = Array2(0).DSContainer.Rows.Count
        Catch ex As Exception
            brs = 0
        End Try

        For cnt = 1 To brs
            str = grid4.Item(0, cnt - 1).Value
            If str <> "" Then
                str2 = grid4.Item(1, cnt - 1).Value
                If str2 = "" Then
                    TabControl1.SelectedIndex = 2
                    grid4.Focus()
                    grid4.CurrentCell = grid4(2, cnt - 1)
                    MsgBox("Container Data should be filled")
                    CekGridHeader = False
                    Exit Function
                End If
            End If
            If CekGridHeader = False Then Exit For
        Next
        If CekGridHeader = False Then Exit Function
        '=========end cek Container

        'Expedition Info TAB
        '=========end Expedition Info 
        
    End Function
    Function CekCurrentPOInvGridData() As Boolean
        Dim brs, cnt As Integer
        Dim totqty, qty1, qty2, qty3 As Decimal
        Dim str As String
        Dim chkPlant As Boolean

        CekCurrentPOInvGridData = True

        If List.Items.Count = 0 Then
            CekCurrentPOInvGridData = True
            Exit Function
        End If

        txtShipNo.Focus()   'invoke grid event, jika data terakhir yg diinput adalah grid
        If DataError = True Then Exit Function

        brs = grid.RowCount
        totqty = 0
        For cnt = 1 To brs
            If List.Items.IndexOf(PrevPO) <= MaxReadOnly Then Continue For
            totqty = totqty + CDec(grid.Item(8, cnt - 1).Value)
            qty1 = grid.Item(7, cnt - 1).Value   'available qty
            qty2 = grid.Item(8, cnt - 1).Value   'actual qty (input)
            qty3 = grid.Item(11, cnt - 1).Value  'package size
            CekCurrentPOInvGridData = QtyAndPackageOK(cnt - 1, qty1, qty2, qty3)
            If CekCurrentPOInvGridData = False Then Exit For
        Next
        If totqty <= 0 And CekCurrentPOInvGridData = True And DataError = False And List.Items.IndexOf(PrevPO) > MaxReadOnly Then
            POTab.SelectedIndex = 0
            List.SelectedIndex = List.Items.IndexOf(PrevPO)
            grid.Focus()
            grid.CurrentCell = grid(8, 0)
            MsgBox("At least one of actual quantity should be filled! ", MsgBoxStyle.Critical, "Warning")
            CekCurrentPOInvGridData = False
            Exit Function
        End If

        If CekCurrentPOInvGridData = False Then Exit Function
        '=========end cek PO Detail


        '=========cek Invoice 
        brs = GridInv.RowCount
        totqty = 0
        For cnt = 1 To brs
            CekCurrentPOInvGridData = InvoiceOK(cnt - 1)
            If CekCurrentPOInvGridData = False Then
                Exit For
            End If
        Next

        If Not isiPlant Then 'focus di pindahkan di sini agar Tab Destination Detail di kenal, hanya untuk pertama form ini di load
            isiPlant = True
            POTab.SelectedIndex = 2
            POTab.SelectedIndex = 0
        End If
        '=========end cek Plant

    End Function
    Private Function CekDataHeader() As Boolean
        Dim brs, angka, cnt As Integer
        Dim str, xshipno As String

        CekDataHeader = True
        If POTab.Focused Then
            blno.Focus() 'invoke grid event, jika data terakhir yg diinput adalah grid
            POTab.Focus()
        Else
            blno.Focus()
        End If
        If DataError = True Then Exit Function

        xshipno = "0"
        If Baru Then
            strSQL = "SELECT shipment_no FROM tbl_shipping WHERE bl_no='" & blno.Text & "'"
            errMSg = "Bill of Lading data view failed"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
            If Not MyReader Is Nothing Then
                Try
                    xshipno = MyReader.GetString("shipment_no")
                Catch ex As Exception
                    xshipno = "0"
                End Try
            End If
            CloseMyReader(MyReader, UserData)
            If xshipno <> "0" Then
                MsgBox("" & blno.Text & " already has BL document. Can't created BL No anymore")
                blno.Focus()
                CekDataHeader = False
                Exit Function
            End If
        End If

        If blno.Text = "" Then
            MsgBox("BL No. should be filled")
            blno.Focus()
            CekDataHeader = False
            Exit Function
        End If
        If supplname.Text = "" Then
            MsgBox("Supplier not found")
            suppl.Focus()
            CekDataHeader = False
            Exit Function
        End If
        If DestPlantName.Text = "" Then
            MsgBox("Destination Plant not found")
            DestPlant.Focus()
            CekDataHeader = False
            Exit Function
        End If
        If DestPortName.Text = "" Then
            MsgBox("Destination Port not found")
            DestPort.Focus()
            CekDataHeader = False
            Exit Function
        End If
        If LoadPortName.Text = "" Then
            MsgBox("Load Port not found")
            LoadPort.Focus()
            CekDataHeader = False
            Exit Function
        End If
        If ShipLineName.Text = "" Then
            MsgBox("Shipping Line not found")
            ShipLine.Focus()
            CekDataHeader = False
            Exit Function
        End If

        angka = CInt(free.Text)
        If angka < 0 And dtArrival.Checked Then
            TabControl1.SelectedIndex = 4
            MsgBox("Free time should be >= 0")
            free.Focus()
            CekDataHeader = False
            Exit Function
        End If
        angka = CInt(free_ext.Text)
        If angka < 0 And dtArrival.Checked Then
            TabControl1.SelectedIndex = 4
            MsgBox("Free time (extended) should be >= 0")
            free_ext.Focus()
            CekDataHeader = False
            Exit Function
        End If

        If dtSPPB.Value > dtClearance.Value And dtSPPB.Checked And dtClearance.Checked Then
            TabControl1.SelectedIndex = 4
            MsgBox("Clearance date should be >= SPPB date")
            dtClearance.Focus()
            CekDataHeader = False
            Exit Function
        End If

        If dtDelivery.Value < dtArrival.Value And dtDelivery.Checked Then
            TabControl1.SelectedIndex = 4
            MsgBox("Clearance date should be >= Notice Arrival Date")
            dtDelivery.Focus()
            CekDataHeader = False
            Exit Function
        End If

        If (dtEstDelivery.Value > dtEstClearance.Value And dtEstDelivery.Checked And dtEstClearance.Checked) Then
            TabControl1.SelectedIndex = 4
            MsgBox("Est.Clearance date should be >= Est.Delivery Date")
            dtEstDelivery.Focus()
            CekDataHeader = False
            Exit Function
        End If

        If (dtDelivery.Value > dtClearance.Value And dtDelivery.Checked And dtClearance.Checked) Then
            TabControl1.SelectedIndex = 4
            MsgBox("Clearance date should be >= Delivery Date")
            dtDelivery.Focus()
            CekDataHeader = False
            Exit Function
        End If

        If List.Items.Count = 0 Then
            MsgBox("PO No. should be filled")
            Button5.Focus()
            CekDataHeader = False
            Exit Function
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
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"

        PilihanDlg.SQLGrid = "SELECT a.port_code AS PortCode, a.Port_Name AS PortName " & _
                             "FROM tbm_port a, tbm_city b WHERE a.city_code = b.city_code AND b.country_code = " & _
                             "(SELECT DISTINCT b.country_code FROM tbm_plant a, tbm_city b " & _
                             " WHERE a.city_code = b.city_code AND a.plant_code='" & DestPlant.Text & "')"

        PilihanDlg.SQLFilter = "SELECT a.port_code AS PortCode, a.Port_Name AS PortName " & _
                             "FROM tbm_port a, tbm_city b WHERE a.city_code = b.city_code AND b.country_code = " & _
                             "(SELECT DISTINCT b.country_code FROM tbm_plant a, tbm_city b " & _
                             " WHERE a.city_code = b.city_code AND a.plant_code='" & DestPlant.Text & "') " & _
                             "and b.port_code LIKE 'FilterData1%' AND b.port_name LIKE 'FilterData2%' "

        PilihanDlg.Tables = "tbm_port b, tbm_plant a WHERE a.city_code=b.city_code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then DestPort.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "SELECT port_code as PortCode, port_name as PortName FROM tbm_port"
        PilihanDlg.SQLFilter = "SELECT port_code, port_name FROM tbm_port " & _
                               "WHERE port_code LIKE 'FilterData1%' AND " & _
                                    "port_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then LoadPort.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PilihanDlg.Text = "Select Shipping Line"
        PilihanDlg.LblKey1.Text = "Shipping Line"
        PilihanDlg.LblKey2.Text = "Shipping Name"
        PilihanDlg.SQLGrid = "SELECT line_code as LineCode, line_name as LineName,address as Address,city_name as CityName,phone as Phone,fax as Fax FROM tbm_lines AS a INNER JOIN tbm_city AS b on a.city_code=b.city_Code"
        PilihanDlg.SQLFilter = "SELECT line_code, line_name,address,city_name,phone,fax FROM tbm_lines AS a INNER JOIN tbm_city AS b on a.city_code=b.city_Code " & _
                               "WHERE line_code LIKE 'FilterData1%' AND " & _
                               "line_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_lines"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then ShipLine.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub
    Private Sub DestPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DestPort.TextChanged
        DestPortName.Text = AmbilData("port_name", "tbm_port", "port_code='" & DestPort.Text & "'")
    End Sub

    Private Sub LoadPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadPort.TextChanged
        LoadPortName.Text = AmbilData("port_name", "tbm_port", "port_code='" & LoadPort.Text & "'")
    End Sub

    Private Sub ShipLine_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShipLine.TextChanged
        ShipLineName.Text = AmbilData("line_name", "tbm_lines", "line_code='" & ShipLine.Text & "'")
    End Sub
    Private Sub btnSearchSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupplier.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName, Bank_Name as BankName, Account_No as AccountNo FROM tbm_supplier WHERE active='1' "
        PilihanDlg.SQLFilter = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier " & _
                               "WHERE active='1' AND supplier_code LIKE 'FilterData1%' AND supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then suppl.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub suppl_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles suppl.TextChanged
        Dim temp As String
        Dim ee As System.EventArgs

        If Baru Then
            bankname.Text = ""
            bankname.Text = ""
            accno.Text = ""
        End If
        temp = AmbilData("supplier_name", "tbm_supplier", "supplier_code='" & suppl.Text & "'")
        If temp <> supplname.Text Then
            ClearAllPO()
            DestPort.Text = ""
        End If
        supplname.Text = temp
        Currency.Text = AmbilData("currency_code", "tbl_po", "supplier_code='" & suppl.Text & "'")
        curr2.Text = Currency.Text
        currname3.Text = Currency.Text
        crtname.Text = UserData.UserName
        dtValue.Enabled = (supplname.Text <> "") And (Me.Text = "Bill of Lading")
        If supplname.Text = "" Then dtValue.Checked = False
        dtValue_ValueChanged(sender, ee)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim data, mess As String
        Dim sisa As Decimal

        If supplname.Text = "" Then
            If Trim(suppl.Text) <> "" Then
                MsgBox("Supplier not found")
            Else
                MsgBox("Supplier should be filled")
            End If
            suppl.Focus()
            Exit Sub
        End If

        data = AmbilData("count(*)", "tbl_po", "supplier_code='" & suppl.Text & "'")
        If data = "0" Then
            MsgBox("PO not found for supplier " & suppl.Text)
            Exit Sub
        End If

        If CekGrid() = False Then Exit Sub
        strSQL = "SELECT a.PO_NO as 'PO No.',a.CURRENCY_CODE as CurrencyCode,a.COMPANY_CODE as CompanyCode,b.Company_Name as CompanyName " & _
                 "from tbl_po as a " & _
                 "inner join tbm_company as b on a.company_code=b.company_code " & _
                 "WHERE supplier_code='" & suppl.Text & "'"

        PilihanDlg.Text = "Select PO No."
        PilihanDlg.LblKey1.Text = "PO No."
        PilihanDlg.LblKey2.Text = "Currency Code"
        PilihanDlg.SQLGrid = strSQL & " order by a.createddt"
        PilihanDlg.SQLFilter = strSQL & " and po_no LIKE 'FilterData1%' and currency_code LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbl_po"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If List.Items.IndexOf(PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString) < 0 Then
                data = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                sisa = GetSisaQty(data)
                If sisa <= 0 Then
                    mess = "All material of PO " & data & " already has BL document," & Chr(13) & Chr(10) & "Can't create BL anymore"
                    MsgBox(mess)
                Else
                    GetDataPurchase(data) 'Add by Prie 10.11.2010
                    If purchase_sts Then 'Add by Prie 10.11.2010
                        mess = "PO. Date Purchased is empty ..."
                        MsgBox(mess)
                        Exit Sub
                    Else
                        PrevPO = data
                        List.Items.Add(data)
                        ReDim Preserve Array(List.Items.Count - 1)
                        ReDim Preserve Array3(List.Items.Count - 1)
                        GetDataPO(data)
                        GetDataInvBlank(data)
                        GetDataPlant(data)
                        List.SelectedIndex = List.Items.Count - 1
                        POBaru = True
                        List_Click(sender, e)
                        POBaru = False
                        POTab.SelectedIndex = 0
                        btnClearD.Enabled = True And Me.Text = "Bill of Lading"
                        ClearAll.Enabled = True And Me.Text = "Bill of Lading"

                        If DestPlant.Text = "" Then DestPlant.Text = GetData("SELECT plant_code FROM tbl_po WHERE po_no='" & data & "'")
                        If DestPort.Text = "" Then DestPort.Text = GetData("SELECT port_code FROM tbl_po WHERE po_no='" & data & "'")
                        If LoadPort.Text = "" Then LoadPort.Text = GetData("SELECT loadport_code FROM tbl_po WHERE po_no='" & data & "'")

                        'Added by Prie 05.11.2010
                        If grid.RowCount > 0 Then
                            btnSave.Enabled = True
                        Else
                            btnSave.Enabled = False
                        End If
                    End If
                End If
            Else
                MsgBox("PO no. " & PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString & " already choosed")
            End If
        End If
    End Sub
    Private Sub ClearAllPO()
        List.Items.Clear()
        grid.DataSource = Nothing
        grid.Columns.Clear()
        GridInv.DataSource = Nothing
        GridPlant.DataSource = Nothing
        GridPlant.Columns.Clear()
        ReDim Array(0)
        ReDim Array3(0)
        Kurs = 0
        PrevPO = ""
        RefreshBea()
        btnClearD.Enabled = False
        ClearAll.Enabled = False
        MaxReadOnly = -1
    End Sub
    Private Sub btnClearD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearD.Click
        Dim sel, msg As String
        Dim idx, max As SByte
        Dim ee As System.EventArgs
        Dim del As Boolean = False
        Dim num1, num2, num3, num4 As Decimal

        If List.Items.Count = 0 Then Exit Sub
        max = List.Items.Count - 1
        idx = List.SelectedIndex
        sel = Trim(List.Items(idx).ToString)
        del = False
        If Baru Or idx > MaxReadOnly Then del = True

        If idx <= MaxReadOnly Then
            Msg = List.Items(idx).ToString & " detail will be deleted PERMANENTLY!!!" & Chr(13) & Chr(10) & "Are you sure to delete it?"
            If (MsgBox(msg, MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then
                Exit Sub
            Else
                If DelPODetilAndUpdateBea(idx) = False Then
                    ReDim Array3(Array4.Length - 1)
                    Array.Copy(Array4, Array3, Array4.Length)
                    ReDim Array4(0)
                    Exit Sub
                Else
                    del = True
                End If
            End If
        End If

        If del Then
            List.Items.Remove(sel)
            RearrangeArray(idx, max)
            Try
                List.SelectedIndex = idx
            Catch ex As Exception
                List.SelectedIndex = List.Items.Count - 1
            End Try
            idx = List.SelectedIndex
            If idx >= 0 Then HitungBea(idx, -1)
            UpdateDisplayBea()
            List_Click(sender, ee)


            If List.Items.Count = 0 Then
                btnSave.Enabled = False 'Add by prie 09.11.2010
                ClearAllPO()
            End If
            MaxReadOnly = List.Items.Count - 1
        End If
    End Sub
    Private Sub RearrangeArray(ByVal DelIdx As SByte, ByVal MaxIdx As SByte)
        Dim cnt As SByte

        For cnt = DelIdx To MaxIdx - 1
            Array(cnt) = Array(cnt + 1)
            Array3(cnt) = Array3(cnt + 1)
        Next
        ReDim Preserve Array(MaxIdx - 1)
        ReDim Preserve Array3(MaxIdx - 1)
    End Sub
    Private Sub FormatGridPO(ByVal grd As DataGridView)
        Dim cbn As New DataGridViewComboBoxColumn

        'PO
        grd.Columns(0).Visible = False
        grd.Columns(9).Visible = False
        grd.Columns(1).DefaultCellStyle.BackColor = Color.Gray
        grd.Columns(2).DefaultCellStyle.BackColor = Color.Gray
        grd.Columns(3).DefaultCellStyle.BackColor = Color.Gray
        grd.Columns(4).DefaultCellStyle.BackColor = Color.Gray
        grd.Columns(5).DefaultCellStyle.BackColor = Color.Gray
        grd.Columns(6).DefaultCellStyle.BackColor = Color.Gray
        grd.Columns(7).DefaultCellStyle.BackColor = Color.Gray

        With cbn
            .DataSource = Show_Grid(grid, "(select trim(pack_code) pack_code, trim(pack_name) pack_name from tbm_packing) as a")
            .DisplayMember = "PACK_CODE"
            .ValueMember = "PACK_CODE"
        End With

        grd.Columns.Insert(10, cbn)
        grd.Columns(7).HeaderText = "Available Quantity"
        grd.Columns(10).HeaderText = "Package Unit"

        grd.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(4).DefaultCellStyle.Format = "N5"
        grd.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(7).DefaultCellStyle.Format = "N5"
        grd.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(8).DefaultCellStyle.Format = "N5"
        grd.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(11).DefaultCellStyle.Format = "N5"
        grd.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        grd.Columns(13).DefaultCellStyle.Format = "N2"

        grd.Columns(1).ReadOnly = True
        grd.Columns(2).ReadOnly = True
        grd.Columns(3).ReadOnly = True
        grd.Columns(4).ReadOnly = True
        grd.Columns(5).ReadOnly = True
        grd.Columns(6).ReadOnly = True
        grd.Columns(7).ReadOnly = True
        grd.Columns(12).Visible = False
        grd.Columns(13).Visible = False

        grd.Columns(1).Width = 60
        grd.Columns(2).Width = 125
        grd.Columns(3).Width = 60
        grd.Columns(4).Width = 69
        grd.Columns(5).Width = 69
        grd.Columns(6).Width = 60
        grd.Columns(7).Width = 69
        grd.Columns(8).Width = 69
        grd.Columns(10).Width = 69
        grd.Columns(11).Width = 48
        grd.ColumnHeadersHeight = 35
        grd.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Try
            grd.Focus()
            grd.CurrentCell = grid(8, 0)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FormatGridInv(ByVal GrdInv As DataGridView)
        GrdInv.Columns(1).DefaultCellStyle.Format = "d"
        GrdInv.Columns(2).DefaultCellStyle.Format = "N2"
        GrdInv.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        GrdInv.Columns(3).DefaultCellStyle.Format = "N2"
        GrdInv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        GrdInv.Columns(4).DefaultCellStyle.Format = "N2"
        GrdInv.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        GrdInv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Try
            GrdInv.Focus()
            GrdInv.CurrentCell = GrdInv(0, 0)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FormatGridPlant(ByVal GrdPlant As DataGridView)
        Dim cbn As New DataGridViewComboBoxColumn
        Dim str As String

        GrdPlant.Columns(0).Visible = False
       
        With cbn
            .DataSource = Show_Grid(grid, "(select plant_code, plant_name from tbm_plant order by plant_name) as a")
            .DisplayMember = "PLANT_NAME"
            .ValueMember = "PLANT_CODE"
        End With

        GrdPlant.Columns.Insert(1, cbn)
        GrdPlant.Columns(1).HeaderText = "DestinationPlant"
        GrdPlant.Columns(1).Width = 150

        GrdPlant.Columns(2).Width = 500
        GrdPlant.ColumnHeadersHeight = 35

        GrdPlant.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Try
            GrdPlant.Focus()
            GrdPlant.CurrentCell = GrdPlant(1, 0)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetDataPO(ByVal PO As String)
        Dim dts As DataTable
        Dim brsList As Integer

        Dim purchasedt As String
        purchase_sts = False
        'PO Detail TAB
        grid.DataSource = Nothing
        grid.Columns.Clear()

        errMSg = "PO detail data view failed"
        strSQL = "select `Item`, `Mat.Code`, `Material Name`, `Origin`, `PO Quantity`, `Unit`, `PO Package`, `sisa`, `Actual Quantity`, `Package Unit`, `Package Size`, `country_code`, `price`, `Specification` from (" & _
                 "select a.PO_Item as 'Item',a.material_code as 'Mat.Code',b.material_name as 'Material Name',e.country_name as 'Origin'," & _
                 "a.quantity as 'PO Quantity',a.unit_code as 'Unit',a.Package_code as 'PO Package'," & _
                 "IF (d.quantity>0,a.quantity*((100+c.tolerable_delivery)/100)-sum(d.quantity),a.quantity*((100+c.tolerable_delivery)/100)) as 'sisa'," & _
                 "0.00 as 'Actual Quantity','' as 'Package Unit',1.00 as 'Package Size',e.country_code,a.price,a.Specification " & _
                 " ,COALESCE(d.quantity,0) AS d_quantity " & _
                 "from tbl_po_Detail as a " & _
                 "inner join tbm_material as b on a.material_Code=b.material_code " & _
                 "inner join tbl_po as c on a.po_no=c.po_no " & _
                 "left outer join tbl_shipping_Detail as d on d.po_no=a.po_no and d.po_item = a.po_item " & _
                 "inner join tbm_country as e on e.country_code=a.country_code " & _
                 "where a.po_no='" & PO & "' " & _
                 "group by a.po_item) as x where Sisa>0 and d_quantity = 0"

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        grid.DataSource = dts
        brsList = List.Items.Count - 1
        FormatGridPO(grid)
        grid.Columns(7).Frozen = True
        SaveData(brsList, grid, GridInv, GridPlant)
    End Sub
    Private Sub GetDataPurchase(ByVal PO As String)
        Dim dts As DataTable
        Dim brsList As Integer
        Dim purchasedt As String
        purchase_sts = False
        'PO Detail TAB
        grid.DataSource = Nothing
        grid.Columns.Clear()

        'Edited by Prie 10.11.2010

        errMSg = "PO detail data view failed"
        strSQL = "select * from (" & _
                 "select a.PO_Item as 'Item',a.material_code as 'Mat.Code',b.material_name as 'Material Name',e.country_name as 'Origin'," & _
                 "a.quantity as 'PO Quantity',a.unit_code as 'Unit',a.Package_code as 'PO Package'," & _
                 "IF (d.quantity>0,a.quantity*((100+c.tolerable_delivery)/100)-sum(d.quantity),a.quantity*((100+c.tolerable_delivery)/100)) as 'sisa'," & _
                 "0.00 as 'Actual Quantity','' as 'Package Unit',1.00 as 'Package Size',e.country_code,a.price,a.Specification,c.purchaseddt " & _
                 "from tbl_po_Detail as a " & _
                 "inner join tbm_material as b on a.material_Code=b.material_code " & _
                 "inner join tbl_po as c on a.po_no=c.po_no " & _
                 "left outer join tbl_shipping_Detail as d on d.po_no=a.po_no and d.po_item = a.po_item " & _
                 "inner join tbm_country as e on e.country_code=a.country_code " & _
                 "where a.po_no='" & PO & "' " & _
                 "group by a.po_item) as x where Sisa>0"

        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    purchasedt = Format(MyReader.GetDateTime(14), "yyyy-MM-dd")
                Catch ex As Exception
                    purchasedt = ""
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        If purchasedt = "" Then
            purchase_sts = True
        End If
    End Sub
    Private Sub GetDataInvBlank(ByVal PO As String)
        Dim dts As DataTable
        Dim brs As Integer

        GridInv.DataSource = Nothing
        errMSg = "Invoice data view failed"
        'strSQL = "select '' as 'No.',Invoice_Dt as 'Date',0.00 as 'Amount' from tbl_shipping_invoice " & _
        '         "where invoice_no='' and Invoice_Dt='' and invoice_amount=0"

        'strSQL = "select '' as 'No.',curdate() as 'Date',0.00 as 'Original Amount',0.00 as 'Amount',0.00 as 'Penalty Inv.' " & _
        '         "from tbl_po_Detail as a " & _
        '         "inner join tbm_material as b on a.material_Code=b.material_code " & _
        '         "inner join tbl_po as c on a.po_no=c.po_no " & _
        '         "left outer join tbl_shipping_Detail as d on d.po_no=a.po_no and d.po_item = a.po_item " & _
        '         "inner join tbm_country as e on e.country_code=a.country_code " & _
        '         "where a.po_no='" & PO & "' and a.quantity>0 " & _
        '         "group by a.po_item"
        ' REMARK BY SUPRAM : 14-09-2022

        strSQL = "select '' as 'No.',curdate() as 'Date',0.00 as 'Original Amount',0.00 as 'Amount',0.00 as 'Penalty Inv.', a.material_code " & _
                 "from tbl_po_Detail as a " & _
                 "inner join tbm_material as b on a.material_Code=b.material_code " & _
                 "inner join tbl_po as c on a.po_no=c.po_no " & _
                 "left outer join tbl_shipping_Detail as d on d.po_no=a.po_no and d.po_item = a.po_item " & _
                 "inner join tbm_country as e on e.country_code=a.country_code " & _
                 "where a.po_no='" & PO & "' and a.quantity>0 and d.quantity IS NULL "
        '& _
        '                 "group by a.material_code"

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        GridInv.DataSource = dts
        brs = List.Items.Count - 1
        FormatGridInv(GridInv)
        SaveData(brs, grid, GridInv, GridPlant)
        GridInv.ReadOnly = (Me.Text <> "Bill of Lading")
        GridInv.AllowUserToAddRows = False
    End Sub

    Private Sub GetDataPlant(ByVal PO As String)
        Dim dts As DataTable
        Dim brs As Integer

        GridPlant.DataSource = Nothing
        GridPlant.Columns.Clear()

        errMSg = "Plant data view failed"
        'add by estrika 131010
        If DestPlant.Text = "" Then DestPlant.Text = GetData("SELECT plant_code FROM tbl_po WHERE po_no='" & PO & "'")
        '***
        strSQL = "select '" & DestPlant.Text & "' DestinationPlantCode, '' as Note " & _
                 "from dual " 

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        GridPlant.DataSource = dts
        brs = List.Items.Count - 1
        FormatGridPlant(GridPlant)
        UpdateDisplayPlant(GridPlant, List)
        SaveData(brs, grid, GridInv, GridPlant)
        GridPlant.ReadOnly = (Me.Text <> "Bill of Lading")
        GridPlant.AllowUserToAddRows = False
    End Sub

    Private Sub UpdateDisplayPackage(ByVal grd As DataGridView, ByVal Lst As ListBox)
        Dim brs, cnt, idx As Integer
        Dim str As String

        brs = grd.RowCount
        idx = Lst.SelectedIndex
        For cnt = 1 To brs
            If grd.Item(9, cnt - 1).Value Is DBNull.Value Then
            Else
                str = grd.Item(9, cnt - 1).Value
                grd.Rows(cnt - 1).Cells(10).Value = str
            End If
        Next
    End Sub

    Private Sub UpdateDisplayPlant(ByVal grd As DataGridView, ByVal Lst As ListBox)
        Dim brs, cnt, idx As Integer
        Dim str As String

        brs = grd.RowCount
        idx = Lst.SelectedIndex
        For cnt = 1 To brs
            If grd.Item(1, cnt - 1).Value Is DBNull.Value Then
                str = grd.Item(0, cnt - 1).Value
            Else
                str = grd.Item(1, cnt - 1).Value
                If str = "" Then str = grd.Item(0, cnt - 1).Value
            End If
            grd.Rows(cnt - 1).Cells(0).Value = str
            grd.Rows(cnt - 1).Cells(1).Value = str

            If grd.Item(2, cnt - 1).Value Is DBNull.Value Then grd.Rows(cnt - 1).Cells(2).Value = ""
        Next
    End Sub

    Private Sub curr2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles curr2.TextChanged
        currname2.Text = AmbilData("currency_name", "tbm_currency", "currency_code='" & curr2.Text & "'")
    End Sub
    Private Function InvoiceOK(ByVal baris As Integer) As Boolean
        Dim val, valt As Decimal
        Dim str, str3 As String
        Dim qty2 As Decimal

        InvoiceOK = True
        qty2 = grid.Item(8, baris).Value

        Try
            str = GridInv.Item(0, baris).Value
        Catch ex As Exception
            str = ""
        End Try

        If str <> "" Then
            'invoice original amount
            Try
                str3 = GridInv.Item(1, baris).Value
                val = GridInv.Item(2, baris).Value
            Catch ex As Exception
            End Try

            If ((str3 = "" Or val <= 0) And qty2 <> 0) Then
                List.SelectedIndex = List.Items.IndexOf(PrevPO)
                POTab.SelectedIndex = 1
                GridInv.Focus()

                If Trim(str3) = "" Then
                    GridInv.CurrentCell = GridInv(1, baris)
                Else
                    GridInv.CurrentCell = GridInv(2, baris)
                End If
                If str3 <> "" And val < 0 Then
                    MsgBox("Invoice Original Amount should be > 0")
                ElseIf qty2 <> 0 Then
                    MsgBox("Invoice Data should be filled")
                End If
                InvoiceOK = False
                Exit Function
            End If

            'invoice amount
            Try
                str3 = GridInv.Item(1, baris).Value
                val = GridInv.Item(3, baris).Value
            Catch ex As Exception
            End Try

            If ((str3 = "" Or val <= 0) And qty2 <> 0) Then
                List.SelectedIndex = List.Items.IndexOf(PrevPO)
                POTab.SelectedIndex = 1
                GridInv.Focus()

                If Trim(str3) = "" Then
                    GridInv.CurrentCell = GridInv(1, baris)
                Else
                    GridInv.CurrentCell = GridInv(3, baris)
                End If
                If str3 <> "" And val < 0 Then
                    MsgBox("Invoice Amount should be > 0")
                Else
                    MsgBox("Invoice Data should be filled")
                End If
                InvoiceOK = False
                Exit Function
            End If

            'penalty total dan penalty by Inv. tidak boleh di isi kedua2nya
            Try
                val = GridInv.Item(4, baris).Value
                valt = GetNum(finalty.Text)
            Catch ex As Exception
            End Try

            If ((val < 0 Or (val > 0 And valt > 0)) And qty2 <> 0) Then
                List.SelectedIndex = List.Items.IndexOf(PrevPO)
                POTab.SelectedIndex = 1
                GridInv.Focus()

                GridInv.CurrentCell = GridInv(4, baris)

                If (val > 0 And valt > 0) Then
                    MsgBox("Penalty Amount harus di input di salah satu item Penalty BL (Total) atau Penalty Inv. saja, tidak boleh di keduanya")
                ElseIf val < 0 Then
                    MsgBox("Penalty Amount should be >= 0")
                End If

                InvoiceOK = False
                Exit Function
            End If
        ElseIf qty2 <> 0 Then
            MsgBox("Invoice Data should be filled")
            InvoiceOK = False
        End If
    End Function

    Private Sub grid_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grid.CellBeginEdit
        Dim kol, brs As Integer

        kol = grid.CurrentCell.ColumnIndex
        brs = grid.CurrentCell.RowIndex
    End Sub

    Private Sub grid_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid.CellEndEdit
        Dim brs, brs2, kol, brsPO As SByte

        brs = grid.CurrentCell.RowIndex
        brs2 = List.Items.Count - 1
        kol = grid.CurrentCell.ColumnIndex
        brsPO = List.SelectedIndex
        If grid.Item(8, brs).Value Is DBNull.Value Then grid.Item(8, brs).Value = 0
        If grid.Item(11, brs).Value Is DBNull.Value Then grid.Item(11, brs).Value = 1
        grid.Item(9, brs).Value = grid.Item(10, brs).Value
        SaveData(brsPO, grid, GridInv, GridPlant)
        If kol = 8 Then
            UpdateInvoice()
            HitungBea(brsPO, -1)
        End If
    End Sub

    Private Sub grid_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid.CurrentCellDirtyStateChanged
        If DataError = True And grid.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid.DataError
        If grid.CurrentCell.ColumnIndex = 8 Then
            MsgBox("Invalid Actual Quantity, input numeric value")
            DataError = True
        End If
        If grid.CurrentCell.ColumnIndex = 11 Then
            MsgBox("Invalid Package Quantity, input numeric value")
            DataError = True
        End If
    End Sub

    Private Sub GridInv_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridInv.CurrentCellDirtyStateChanged
        If DataError = True And GridInv.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub GridPlant_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridPlant.CellEndEdit
        Dim brs, brsPO As SByte

        brs = GridPlant.CurrentCell.RowIndex
        brsPO = List.SelectedIndex
        GridPlant.Item(0, brs).Value = GridPlant.Item(1, brs).Value

        SaveData(brsPO, grid, GridInv, GridPlant)
    End Sub

    Private Sub gridPlant_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridPlant.CurrentCellDirtyStateChanged
        If DataError = True And GridPlant.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub GridInv_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles GridInv.DataError
        If GridInv.CurrentCell.ColumnIndex = 1 Then
            MsgBox("Invalid date")
            DataError = True
            Exit Sub
        End If
        If GridInv.CurrentCell.ColumnIndex = 2 Then
            MsgBox("Invalid invoice original amount, input numeric value")
            DataError = True
            Exit Sub
        End If
        If GridInv.CurrentCell.ColumnIndex = 3 Then
            MsgBox("Invalid invoice amount, input numeric value")
            DataError = True
            Exit Sub
        End If
        If GridInv.CurrentCell.ColumnIndex = 4 Then
            MsgBox("Invalid penalty, input numeric value")
            DataError = True
            Exit Sub
        End If
    End Sub

    Private Sub GridInv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridInv.CellEndEdit
        Dim brsPO, brs, kol As SByte
        Dim str As String
        Dim tg As Date

        brs = GridInv.CurrentCell.RowIndex
        brsPO = List.SelectedIndex
        kol = GridInv.CurrentCell.ColumnIndex
        If GridInv.Item(0, brs).Value Is DBNull.Value Then GridInv.Item(0, brs).Value = ""
        If GridInv.Item(1, brs).Value Is DBNull.Value Then GridInv.Item(1, brs).Value = GetServerDate()
        If GridInv.Item(3, brs).Value Is DBNull.Value Then GridInv.Item(3, brs).Value = 0
        If GridInv.Item(4, brs).Value Is DBNull.Value Then GridInv.Item(4, brs).Value = 0
        SaveData(brsPO, grid, GridInv, GridPlant)
        HitungBea(brsPO, -1)
    End Sub

    Private Sub HitungBea(ByVal idxPO As SByte, ByVal excl As SByte)
        Array3(idxPO).MaxDate = GetMaxDateOnePO(idxPO)
        If Edit Then
            Kurs = CDec(KursPajak.Text)
        Else
            Kurs = GetKursPajak(excl)
            KursPajak.Text = FormatNumber(Kurs, 2, , , TriState.True)
        End If
        If Kurs = 0 Then
            Kurs = GetKursPajak(excl)
            KursPajak.Text = FormatNumber(Kurs, 2, , , TriState.True)
        End If
        HitungUlangBea(excl, Kurs)
        UpdateDisplayBea()
    End Sub
    Private Sub HitungUlangBea(ByVal excl As SByte, ByVal KursPjk As Decimal)
        Dim cnt, jml As SByte

        jml = List.Items.Count - 1
        For cnt = 0 To jml
            If cnt <> excl Then SaveData3(cnt, KursPjk)
        Next
    End Sub

    Private Function CheckPlant(ByVal brs As Integer) As Boolean
        Dim str As String
        Dim cnt, cnt2 As Integer

        'harus ada yg sama dengan Plant di Header BL, jika tidak -> data tidak konsisten !
        For cnt = 1 To brs
            DT = Array(cnt - 1).DSPlant
            For cnt2 = 1 To DT.Rows.Count
                If DT.Rows(cnt2 - 1).Item(0).ToString = DestPlant.Text Then CheckPlant = True
            Next
        Next

        If Not CheckPlant Then
            MsgBox("Different Plant. Please check items of Destination Plant and Destination Detail! ", MsgBoxStyle.Critical, "Warning")
            ''btnSave.Enabled = False 'Add by Prie 09.11.2010
            DestPlant.Focus()
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim SQLStr, DetailPO, DetailInvoice, DetailPlant, MaterialCode, POItem, QtyOrigin, Qty2, Qty3, PackQty, Spec, str As String
        Dim PO, no, tgl, pack, plant, DocCode, DocNo, status, remark, ContNo, Unit, RecBy, ProBy As String
        Dim OldRemark, OldDate, OldStatus, HistRemark As String
        Dim DetailSuppDoc, DetailCustomDoc, DetailExpeditionDoc, DetailContainer As String
        Dim hasil As Boolean = False
        Dim cnt, a, brs, brsOK, Copy, Orig, jCont As Integer
        Dim num, num1, num2, num3, num4, num5, num6, num7, num8, num9, num10 As Decimal
        Dim ee As System.EventArgs
        Dim tgl1, tgl2, tgl3, tgl4, tgl5, tgl6, tgl7, tgl8, tgl9 As String
        Dim tgl10, tgl11, tgl12, tgl13, tgl14, tgl15, tgl16, tgl17, tgl18, tgl19, tgl20, tgl21, tgl22, tgl23, tgl24, tgl25, tgl26, tgl27 As String
        Dim CekTOB, CekRL, CekMCI, CekCont, sCont As String
        Dim CekTrsIn, CekTrsEx, CekTreatment, CekEigen, CekCMA As String

        If Status2.Text = "Closed" Then
            MsgBox("Bill of Lading has been closed, can't save")
            Exit Sub
        End If
        blno.Focus()

        If grid.IsCurrentCellDirty And Baru Then         'data di grid sudah di input tapi blom tekan enter, data yg diinput tidak terbaca 
            cnt = grid.CurrentCell.ColumnIndex
            brs = grid.CurrentCell.RowIndex
            blno.Focus()                                 'pindahkan focus supaya data di grid bisa dibaca
            grid.Focus()
            grid.CurrentCell = grid(cnt, brs)            'kembalikan ke kolom awal
        End If
        If GridPlant.IsCurrentCellDirty Then
            cnt = GridPlant.CurrentCell.ColumnIndex
            brs = GridPlant.CurrentCell.RowIndex
            blno.Focus()
            If DataError = False Then
                GridPlant.Focus()
                GridPlant.CurrentCell = GridPlant(cnt, brs)
            End If
        End If

        If grid2.IsCurrentCellDirty Then
            cnt = grid2.CurrentCell.ColumnIndex
            brs = grid2.CurrentCell.RowIndex
            blno.Focus()
            If DataError = False Then
                grid.Focus()
                grid2.CurrentCell = grid2(cnt, brs)
            End If
        End If
        If grid3.IsCurrentCellDirty Then
            cnt = grid3.CurrentCell.ColumnIndex
            brs = grid3.CurrentCell.RowIndex
            blno.Focus()
            If DataError = False Then
                grid.Focus()
                grid3.CurrentCell = grid3(cnt, brs)
            End If
        End If
        If grid4.IsCurrentCellDirty Then
            cnt = grid4.CurrentCell.ColumnIndex
            brs = grid4.CurrentCell.RowIndex
            blno.Focus()
            If DataError = False Then
                grid.Focus()
                grid4.CurrentCell = grid4(cnt, brs)
            End If
        End If
        If grid5.IsCurrentCellDirty Then
            cnt = grid5.CurrentCell.ColumnIndex
            brs = grid5.CurrentCell.RowIndex
            blno.Focus()
            If DataError = False Then
                grid.Focus()
                grid5.CurrentCell = grid5(cnt, brs)
            End If
        End If
        If DataError = False Then
            If CekGrid() = False Then Exit Sub
            If CheckPlant(List.Items.Count) = False Then Exit Sub
        End If
        If DataError = True Then
            'If InsAmt.Focused Or KursPajak.Focused Or BeaMasuk0.Focused Or vat.Focused Or _
            '   pph.Focused Or piud.Focused Or free.Focused Then DataError = False
            'Exit Sub
            If InsAmt.Focused Or free.Focused Or free_ext.Focused Then DataError = False
            Exit Sub
        End If

        If CekDataHeader() = False Then Exit Sub
        If DataError Then Exit Sub

        DetailPO = ""
        DetailInvoice = ""
        DetailPlant = ""
        DetailSuppDoc = ""
        DetailCustomDoc = ""
        DetailExpeditionDoc = ""
        DetailContainer = ""

        tgl1 = IIf(dtCopy.Checked, Format(dtCopy.Value, "yyyy-MM-dd"), "")
        tgl2 = IIf(dtETD.Checked, Format(dtETD.Value, "yyyy-MM-dd"), "")
        tgl3 = IIf(dtETA.Checked, Format(dtETA.Value, "yyyy-MM-dd"), "")
        tgl4 = IIf(dtCreated.Checked, Format(dtCreated.Value, "yyyy-MM-dd"), "")

        'supplier doc tab
        tgl8 = IIf(dtVerifySuppDoc.Checked, Format(dtVerifySuppDoc.Value, "yyyy-MM-dd"), "")
        tgl9 = IIf(dtRecBeaByAcc.Checked, Format(dtRecBeaByAcc.Value, "yyyy-MM-dd"), "")

        'customs doc tab
        tgl10 = IIf(dtPIB.Checked, Format(dtPIB.Value, "yyyy-MM-dd"), "")
        tgl11 = IIf(dtSPPB.Checked, Format(dtSPPB.Value, "yyyy-MM-dd"), "")
        tgl12 = IIf(dtAJU.Checked, Format(dtAJU.Value, "yyyy-MM-dd"), "")

        'expedition info tab
        tgl26 = IIf(dtRecExp.Checked, Format(dtRecExp.Value, "yyyy-MM-dd"), "")
        tgl27 = IIf(dtForwExp.Checked, Format(dtForwExp.Value, "yyyy-MM-dd"), "")

        Try
            DT = Array2(0).DSExpeditionDoc
            brs = DT.Rows.Count
        Catch ex As Exception
            brs = 0
        End Try
        For cnt = 1 To brs
            DocCode = DT.Rows(cnt - 1).Item(0).ToString
            DocCode = Microsoft.VisualBasic.Mid(DocCode & "     ", 1, 5)
            If Trim(DocCode) <> "" Then
                DocNo = DT.Rows(cnt - 1).Item(1).ToString
                DocNo = Microsoft.VisualBasic.Mid(DocNo & "                                        ", 1, 40)
                tgl = DT.Rows(cnt - 1).Item(2).ToString
                tgl = Format(CDate(tgl), "yyyy-MM-dd")
                remark = DT.Rows(cnt - 1).Item(3).ToString
                DetailExpeditionDoc = DetailExpeditionDoc & DocCode & DocNo & tgl & remark & ";"
            End If
        Next

        'clearance analyst tab
        tgl5 = IIf(dtRec.Checked, Format(dtRec.Value, "yyyy-MM-dd"), "")
        tgl13 = IIf(dtArrival.Checked, Format(dtArrival.Value, "yyyy-MM-dd"), "")
        tgl14 = IIf(dtOB.Checked, Format(dtOB.Value, "yyyy-MM-dd"), "")
        tgl15 = IIf(dtEstDelivery.Checked, Format(dtEstDelivery.Value, "yyyy-MM-dd"), "")
        tgl16 = IIf(dtDelivery.Checked, Format(dtDelivery.Value, "yyyy-MM-dd"), "")
        tgl17 = IIf(dtEstClearance.Checked, Format(dtEstClearance.Value, "yyyy-MM-dd"), "")
        tgl18 = IIf(dtClearance.Checked, Format(dtClearance.Value, "yyyy-MM-dd"), "")
        tgl19 = IIf(dtForward.Checked, Format(dtForward.Value, "yyyy-MM-dd"), "")

        If CTPro.Text = "" Then
            ProBy = 0
        Else
            ProBy = CTPro.Text
        End If

        tgl22 = IIf(free_ext_prosdt.Checked, Format(free_ext_prosdt.Value, "yyyy-MM-dd"), "")
        tgl23 = IIf(free_ext_appdt.Checked, Format(free_ext_appdt.Value, "yyyy-MM-dd"), "")
        tgl24 = IIf(dtAreaRec.Checked, Format(dtAreaRec.Value, "yyyy-MM-dd"), "")
        tgl25 = IIf(dtAreaDeptan.Checked, Format(dtAreaDeptan.Value, "yyyy-MM-dd"), "")

        '''CekTOB = IIf(ChkTahanOB.Checked, "Y", "N")
        CekTOB = IIf(cbOB.SelectedIndex = 0, "N", IIf(cbOB.SelectedIndex = 1, "O", "Y"))

        CekRL = IIf(ChkRedLn.Checked, "Y", "N")
        CekMCI = IIf(ChkMCI.Checked, "Y", "N")
        CekTrsIn = IIf(ChkTrsIn.Checked, "Y", "N")
        CekTrsEx = IIf(ChkTrsEx.Checked, "Y", "N")
        CekTreatment = IIf(ChkTreatment.Checked, "Y", "N")
        CekEigen = IIf(ChkEigen.Checked, "Y", "N")
        CekCMA = IIf(ChkCMA.Checked, "Y", "N")

        'For Finance Purpose tab
        tgl20 = IIf(dtValue.Checked, Format(dtValue.Value, "yyyy-MM-dd"), "")
        tgl21 = IIf(dtTT.Checked, Format(dtTT.Value, "yyyy-MM-dd"), "")
        If receivecode.Text = "" Then
            RecBy = 0
        Else
            RecBy = receivecode.Text
        End If

        num2 = GetNum(InsAmt.Text)
        num4 = GetNum(KursPajak.Text)
        num5 = GetNum(BeaMasuk.Text)
        num6 = GetNum(vat.Text)
        num7 = GetNum(pph.Text)
        num8 = GetNum(piud.Text)
        num10 = GetNum(finalty.Text)

        '========HEADER
        'Supplier Document tab
        Try
            DT = Array2(0).DSSuppDoc
            brs = DT.Rows.Count
        Catch ex As Exception
            brs = 0
        End Try
        For cnt = 1 To brs
            DocCode = DT.Rows(cnt - 1).Item(0).ToString
            DocCode = Microsoft.VisualBasic.Mid(DocCode & "     ", 1, 5)
            If (Trim(DocCode) <> "" And Trim(DocCode) <> "SD000") Then
                DocNo = DT.Rows(cnt - 1).Item(1).ToString
                DocNo = Microsoft.VisualBasic.Mid(DocNo & "                                        ", 1, 40)
                tgl = DT.Rows(cnt - 1).Item(2).ToString
                If tgl <> "" Then
                    tgl = Format(CDate(tgl), "yyyy-MM-dd")
                End If
                status = DT.Rows(cnt - 1).Item(3).ToString
                Copy = IIf(status = "OK", 1, 0)
                Orig = IIf(status = "Not OK", 1, 0)
                remark = DT.Rows(cnt - 1).Item(4).ToString

                OldRemark = DT.Rows(cnt - 1).Item(5).ToString
                OldDate = DT.Rows(cnt - 1).Item(6).ToString
                If OldDate <> "" Then
                    OldDate = Format(CDate(OldDate), "yyyy-MM-dd")
                End If
                OldStatus = DT.Rows(cnt - 1).Item(7).ToString
                HistRemark = DT.Rows(cnt - 1).Item(8).ToString
                If OldRemark <> remark Then
                    HistRemark = HistRemark & Chr(13) & Chr(10) & OldDate & ":" & OldStatus & " - " & OldRemark
                End If
                remark = remark & "#" & HistRemark

                DetailSuppDoc = DetailSuppDoc & DocCode & DocNo & tgl & Copy & Orig & remark & ";"
            End If
        Next

        'Customs Doc tab
        Try
            DT = Array2(0).DSCustomDoc
            brs = DT.Rows.Count
        Catch ex As Exception
            brs = 0
        End Try
        For cnt = 1 To brs
            DocCode = DT.Rows(cnt - 1).Item(0).ToString
            DocCode = Microsoft.VisualBasic.Mid(DocCode & "     ", 1, 5)
            If Trim(DocCode) <> "" Then
                DocNo = DT.Rows(cnt - 1).Item(1).ToString
                DocNo = Microsoft.VisualBasic.Mid(DocNo & "                                        ", 1, 40)
                tgl = DT.Rows(cnt - 1).Item(2).ToString
                tgl = Format(CDate(tgl), "yyyy-MM-dd")
                remark = DT.Rows(cnt - 1).Item(3).ToString
                DetailCustomDoc = DetailCustomDoc & DocCode & DocNo & tgl & remark & ";"
            End If
        Next

        'Container tab
        CekCont = ""
        Try
            DT = Array2(0).DSContainer
            brs = DT.Rows.Count
        Catch ex As Exception
            brs = 0
        End Try

        For cnt = 1 To brs
            ContNo = DT.Rows(cnt - 1).Item(0).ToString
            ContNo = Microsoft.VisualBasic.Mid(ContNo & "                                        ", 1, 40)
            If Trim(ContNo) <> "" Then
                Unit = DT.Rows(cnt - 1).Item(1).ToString
                Unit = Microsoft.VisualBasic.Mid(Unit & "     ", 1, 5)
                DetailContainer = DetailContainer & ContNo & Unit & ";"
                brsOK = brsOK + 1

                If sCont <> Unit And brsOK <> 1 Then
                    CekCont = CekCont & ", " & jCont & " x " & sCont
                    jCont = 0
                    sCont = Unit
                ElseIf brsOK = 1 Then
                    sCont = Unit
                End If
                jCont = jCont + 1
            End If
        Next

        If brsOK > 0 Then
            CekCont = CekCont & ", " & jCont & " x " & sCont
            CekCont = Mid(CekCont, 3, Len(CekCont) - 1)
        End If
        '========DETIL
        'PO Detail
        'Diambil dari array bukan dari screen Grid
        'karena PO bisa > 1
        'PO detil kalo sudah di save tidak bisa diedit, karena susah validasi qty bila setelah BL itu ada BL baru
        'If Baru Then
        For a = 1 To List.Items.Count
            DT = Array(a - 1).DSPO
            For cnt = 1 To DT.Rows.Count
                PO = List.Items(a - 1).ToString
                PO = Microsoft.VisualBasic.Mid(PO & "                    ", 1, 20)
                MaterialCode = DT.Rows(cnt - 1).Item(1).ToString
                MaterialCode = Microsoft.VisualBasic.Mid(MaterialCode & "          ", 1, 10)
                POItem = DT.Rows(cnt - 1).Item(0).ToString
                POItem = Microsoft.VisualBasic.Mid(POItem & "     ", 1, 5)
                num = CDec(DT.Rows(cnt - 1).Item(8).ToString)
                num9 = CDec(DT.Rows(cnt - 1).Item(10).ToString)    'grid kolom 10 tidak di simpan di array
                pack = DT.Rows(cnt - 1).Item(9).ToString
                pack = Microsoft.VisualBasic.Mid(pack & "     ", 1, 5)
                Spec = DT.Rows(cnt - 1).Item(13).ToString
                Spec = Microsoft.VisualBasic.Mid(Spec & "                                        ", 1, 40)
                If num > 0 Then
                    str = GetNum2(num)
                    Qty2 = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                    str = GetNum2(num9)
                    PackQty = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                    DetailPO = DetailPO & PO & MaterialCode & Qty2 & POItem & pack & PackQty & Spec & ";"
                End If
            Next
        Next
        'End If

        'Invoice Detail
        'Diambil dari array bukan dari screen Grid
        'karena PO bisa > 1
        For a = 1 To List.Items.Count
            DT = Array(a - 1).DSInv
            For cnt = 1 To DT.Rows.Count
                PO = List.Items(a - 1).ToString
                PO = Microsoft.VisualBasic.Mid(PO & "                    ", 1, 20)
                no = Trim(DT.Rows(cnt - 1).Item(0).ToString)
                If no <> "" Then
                    tgl = DT.Rows(cnt - 1).Item(1).ToString
                    tgl = Format(CDate(tgl), "yyyy-MM-dd")
                    num = CDec(DT.Rows(cnt - 1).Item(2).ToString)
                    str = GetNum2(num)
                    QtyOrigin = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                    num = CDec(DT.Rows(cnt - 1).Item(3).ToString)
                    str = GetNum2(num)
                    Qty2 = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                    num = CDec(DT.Rows(cnt - 1).Item(4).ToString)
                    str = GetNum2(num)
                    Qty3 = Microsoft.VisualBasic.Mid(str & "               ", 1, 15)
                    DetailInvoice = DetailInvoice & PO & tgl & QtyOrigin & Qty2 & Qty3 & no & ";"
                End If
            Next
        Next

        'Destination Detail
        For a = 1 To List.Items.Count
            DT = Array(a - 1).DSPlant
            For cnt = 1 To DT.Rows.Count
                PO = List.Items(a - 1).ToString
                PO = Microsoft.VisualBasic.Mid(PO & "                    ", 1, 20)
                plant = DT.Rows(cnt - 1).Item(0).ToString
                plant = Microsoft.VisualBasic.Mid(plant & "          ", 1, 5)
                remark = Trim(DT.Rows(cnt - 1).Item(1).ToString)
                DetailPlant = DetailPlant & PO & plant & remark & ";"
            Next
        Next
        'update jumlah container sesuai isian detil nya
        If CekCont <> "" Then TotCont.Text = CekCont

        SQLStr = "Run Stored Procedure SaveBL (" & txtShipNo.Text & "," & blno.Text & "," & hostbl.Text & "," & suppl.Text & "," & packlist.Text & "," _
                 & tgl1 & "," & tgl2 & "," & tgl3 & "," & DestPlant.Text & "," & DestPort.Text & "," & LoadPort.Text & "," _
                 & ShipLine.Text & "," & vessel.Text & "," & tgl26 & "," & Expedition.Text & "," & tgl27 & "," & exp_note.Text & "," & vercode1.Text & "," & tgl8 & "," & vercode2.Text & "," & tgl9 & "," _
                 & Currency.Text & "," & GetNum2(num5) & "," & GetNum2(num6) & "," & GetNum2(num7) & "," & GetNum2(num8) & "," _
                 & GetNum2(num4) & "," & GetNum2(num10) & "," & ajuNo.Text & "," & tgl12 & "," & PIBNo.Text & "," & tgl10 & "," & SPPBNo.Text & "," _
                 & tgl11 & "," & InsNo.Text & "," & GetNum2(num2) & "," & tgl5 & "," & tgl19 & "," & tgl24 & "," & tgl25 & "," & tgl13 & "," & free.Text & "," & free_ext.Text & "," & ProBy & "," _
                 & tgl22 & "," & tgl23 & "," & free_ext_note.Text & "," & Cur_Work.Text & "," & Cur_Eq.Text & "," & TotCont.Text & "," _
                 & CekTOB & "," & CekRL & "," & CekMCI & "," & CekTrsIn & "," & CekTrsEx & "," & CekTreatment & "," & CekEigen & "," & CekCMA & "," _
                 & tgl14 & "," & tgl15 & "," & tgl16 & "," & tgl17 & "," & tgl18 & "," & GetNum2(num3) & "," & RecBy & "," & bankname.Text & "," _
                 & accno.Text & "," & tgl21 & "," & tgl20 & "," & UserData.UserCT & "," _
                 & DetailSuppDoc & "," & DetailCustomDoc & "," & DetailContainer & "," & DetailPO & "," & DetailInvoice & "," & DetailPlant & "," & DetailExpeditionDoc & ")"

        With MyComm
            .CommandText = "SaveBL"
            .CommandType = CommandType.StoredProcedure
            With .Parameters
                .Clear()
                .AddWithValue("UpdateShipNo", txtShipNo.Text)
                .AddWithValue("HostB", hostbl.Text)
                .AddWithValue("BLNo", blno.Text)
                .AddWithValue("Supplier", suppl.Text)
                .AddWithValue("PackList", packlist.Text)
                .AddWithValue("RecCopyDocDt", tgl1)
                .AddWithValue("DelDt", tgl2)
                .AddWithValue("ArrDt", tgl3)
                .AddWithValue("DestPlant", DestPlant.Text)
                .AddWithValue("DestPort", DestPort.Text)
                .AddWithValue("LoadPort", LoadPort.Text)
                .AddWithValue("ShipLine", ShipLine.Text)
                .AddWithValue("Vessel", vessel.Text)
                .AddWithValue("RecExpDt", tgl26)
                .AddWithValue("Expedition", Expedition.Text)
                .AddWithValue("ForwExpDt", tgl27)
                .AddWithValue("Exp_Note", exp_note.Text)
                .AddWithValue("Ver1By", vercode1.Text)
                .AddWithValue("Ver1Dt", tgl8)
                .AddWithValue("Ver2By", vercode2.Text)
                .AddWithValue("Ver2Dt", tgl9)
                .AddWithValue("Currency", Currency.Text)
                .AddWithValue("BeaMsk", num5)
                .AddWithValue("VAT", num6)
                .AddWithValue("Pph21", num7)
                .AddWithValue("PIUD", num8)
                .AddWithValue("KursPajak", num4)
                .AddWithValue("Finalty", num10)
                .AddWithValue("AJUNo", ajuNo.Text)
                .AddWithValue("EstSPPBDt", tgl12)
                .AddWithValue("PIBNo", PIBNo.Text)
                .AddWithValue("PIBDt", tgl10)
                .AddWithValue("SPPBNo", SPPBNo.Text)
                .AddWithValue("SPPBDt", tgl11)
                .AddWithValue("InsNo", InsNo.Text)
                .AddWithValue("InsAmt", num2)
                .AddWithValue("RecDocDt", tgl5)
                .AddWithValue("ForwardDt", tgl19)
                .AddWithValue("AreaRecDt", tgl24)
                .AddWithValue("AreaDeptanDt", tgl25)
                .AddWithValue("NoticeArrDt", tgl13)
                .AddWithValue("FreeTime", CInt(free.Text))
                .AddWithValue("FTE", CInt(free_ext.Text))
                .AddWithValue("FTE_PROPOSED_BY", ProBy)
                .AddWithValue("FTE_PROPOSED_DT", tgl22)
                .AddWithValue("FTE_APPROVED_DT", tgl23)
                .AddWithValue("FTE_Note", free_ext_note.Text)
                .AddWithValue("LEAD_TIME", CInt(Cur_Work.Text))
                .AddWithValue("TOTAL_EQUIPMENT", CInt(Cur_Eq.Text))
                .AddWithValue("TOTAL_CONTAINER", TotCont.Text)
                .AddWithValue("TAHAN_OB", CekTOB)
                .AddWithValue("RED_LINE", CekRL)
                .AddWithValue("MCI", CekMCI)
                .AddWithValue("TRANS_IN", CekTrsIn)
                .AddWithValue("TRANS_EX", CekTrsEx)
                .AddWithValue("TREATMENT", CekTreatment)
                .AddWithValue("EIGEN_LOSS", CekEigen)
                .AddWithValue("CMA", CekCMA)
                .AddWithValue("OBDt", tgl14)
                .AddWithValue("EstClrDt", tgl15)
                .AddWithValue("ClrDt", tgl16)
                .AddWithValue("EstBAPBDt", tgl17)
                .AddWithValue("BAPBDt", tgl18)
                .AddWithValue("KursPIB", num3)
                .AddWithValue("RecBy", RecBy)
                .AddWithValue("BankName", bankname.Text)
                .AddWithValue("AccNo", accno.Text)
                .AddWithValue("TTDt", tgl21)
                .AddWithValue("DueDt", tgl20)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("SuppDocDetail", DetailSuppDoc)
                .AddWithValue("CustomDocDetail", DetailCustomDoc)
                .AddWithValue("ContainerDetail", DetailContainer)
                .AddWithValue("InvoiceDetail", DetailInvoice)
                .AddWithValue("PlantDetail", DetailPlant)
                .AddWithValue("PODetail", DetailPO)
                .AddWithValue("ExpeditionDocDetail", DetailExpeditionDoc)
                .AddWithValue("AuditStr", SQLStr)
                .AddWithValue("Hasil", hasil)
            End With
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End With

        If hasil = True Then
            f_msgbox_successful("Save Bill of Lading")
            'di tutup agar data tidak di kosongkan setelah save
            'btnNew_Click(sender, ee)
        Else
            MsgBox("Save Bill of Lading failed")
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub POTab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles POTab.SelectedIndexChanged
        If POTab.SelectedIndex = 0 Then
            grid.Focus()
        Else
            If Not GridInv Is Nothing Then GridInv.Focus()
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Dim tg As Date
        Dim ee As System.EventArgs

        btnSave.Enabled = False 'add by Prie 09.11.2010
        Display = True
        Kurs = 0
        MaxReadOnly = -1
        DataError = False
        suppl.ReadOnly = False
        Baru = True
        Edit = False
        blno.Focus()
        tg = GetServerDate()
        dtCopy.Value = tg
        dtCopy.Checked = False
        dtETD.Value = tg
        dtETD.Checked = False
        dtETA.Value = tg
        dtETA.Checked = False
        dtCreated.Value = tg
        dtCreated.Checked = False
        dtRec.Value = tg
        dtRec.Checked = False
        dtVerifySuppDoc.Value = tg
        dtVerifySuppDoc.Checked = False
        dtRecBeaByAcc.Value = tg
        dtRecBeaByAcc.Checked = False
        dtPIB.Value = tg
        dtPIB.Checked = False
        dtSPPB.Value = tg
        dtSPPB.Checked = False
        dtAJU.Value = tg
        dtAJU.Checked = False
        dtArrival.Value = tg
        dtArrival.Checked = False
        cbOB.SelectedIndex = 0
        dtOB.Value = tg
        dtOB.Checked = False
        dtOB.Enabled = False
        dtEstDelivery.Value = tg
        dtEstDelivery.Checked = False
        dtDelivery.Value = tg
        dtDelivery.Checked = False
        dtEstClearance.Value = tg
        dtEstClearance.Checked = False
        dtClearance.Value = tg
        dtClearance.Checked = False
        dtForward.Value = tg
        dtForward.Checked = False
        dtAreaRec.Value = tg
        dtAreaRec.Checked = False
        dtAreaDeptan.Value = tg
        dtAreaDeptan.Checked = False
        dtValue.Value = tg
        dtValue.Checked = False
        dtTT.Value = tg
        dtTT.Checked = False
        dtTT.Enabled = False
        sdem.Checked = False

        free_ext_prosdt.Value = tg
        free_ext_prosdt.Checked = False
        free_ext_appdt.Value = tg
        free_ext_appdt.Checked = False

        ClearTab()
        ReDim Array(0)
        ReDim Array2(0)
        ReDim Array3(0)
        ReDim Array4(0)
        blno.Text = ""
        hostbl.Text = ""
        supplname.Text = ""
        DestPort.Text = ""
        DestPlant.Text = ""
        DestPlantName.Text = ""
        DestPortName.Text = ""
        LoadPort.Text = ""
        LoadPortName.Text = ""
        ShipLine.Text = ""
        ShipLineName.Text = ""
        Expedition.Text = ""
        ExpeditionName.Text = ""
        exp_note.Text = ""

        dtRecExp.Value = tg
        dtRecExp.Checked = False
        dtForwExp.Value = tg
        dtForwExp.Checked = False

        bankname.Text = ""
        bankname.Text = ""
        Currency.Text = ""
        currname.Text = ""
        curr2.Text = ""
        currname2.Text = ""
        currname3.Text = ""
        packlist.Text = ""
        vessel.Text = ""
        'btnClearD_Click(sender, ee)
        ClearAllPO()
        suppl.Text = ""
        vercode1.Text = ""
        vercode2.Text = ""
        ajuNo.Text = ""
        PIBNo.Text = ""
        SPPBNo.Text = ""
        ListBox1.Items.Clear()
        txtShipNo.Text = "0"
        POTab.SelectedIndex = 0
        TabControl1.SelectedIndex = 0
        btnClearD.Enabled = False
        ClearAll.Enabled = False
        If Me.Text = "Bill of Lading" Then GetTTDate()
        dtTT.Enabled = True And (Me.Text = "Bill of Lading")
        If Me.Text = "Bill of Lading" And grid2.DataSource Is Nothing And TabPage9.Visible Then GetDataSuppDoc(grid2, txtShipNo.Text, Edit)
        btnDelete.Enabled = False
        blno.Focus()
        Display = False   'supaya event finalty_textchanged tidak dijalankan

        UpdateAksesInput()
    End Sub
    Private Sub DisplayData()
        DisplayBLHeader()
        DisplayBLDetil()
        DisplayDocument()
    End Sub
    Private Sub DisplayBLHeader()
        Dim temp1 As String = ""
        Dim temp2 As String = ""
        Dim temp3 As String = ""
        Dim temp4 As String = ""
        Dim temp5 As String = ""
        Dim temp6 As String = ""
        Dim temp7 As String = ""
        Dim temp8 As String = ""
        Dim temp9 As String = ""
        Dim temp10 As String = ""
        Dim temp11 As String = ""
        Dim temp12 As String = ""
        Dim temp13 As String = ""
        Dim bb1 As String = ""
        Dim bb2 As String = ""
        Dim bb3 As String = ""
        Dim bb4 As String = ""
        Dim bb5 As String = ""
        Dim bb6 As String = ""
        Dim tgl1, tgl2, tgl3, tgl4, tgl5, tgl6, tgl7, tgl8, tgl9 As Object
        Dim tgl10, tgl11, tgl12, tgl13, tgl14, tgl15, tgl16, tgl17, tgl18 As Object

        'strSQL = "select * from tbl_shipping where SHIPMENT_NO = '" & txtShipNo.Text & "'"

        strSQL = "SELECT *, IF(bapb_dt IS NOT NULL, DATEDIFF(bapb_dt,sdem),IF(est_bapb_dt IS NOT NULL, DATEDIFF(est_bapb_dt,sdem),IF(clr_dt IS NOT NULL, DATEDIFF(clr_dt,sdem),DATEDIFF(est_clr_dt,sdem)))) dem FROM " & _
                 "(SELECT *, DATE_ADD(notice_arrival_dt, INTERVAL (free_time+fte) DAY) sdem " & _
                 "FROM tbl_shipping WHERE SHIPMENT_NO = '" & txtShipNo.Text & "') t1 "

        errMSg = "Bill of Lading data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp1 = MyReader.GetString("Supplier_Code")
                    temp2 = MyReader.GetString("Port_Code")
                    temp3 = MyReader.GetString("LoadPort_Code")
                    temp4 = MyReader.GetString("Shipping_Line")
                    Try
                        temp5 = MyReader.GetString("Received_By")
                    Catch ex As Exception
                        temp5 = ""
                    End Try
                    temp7 = MyReader.GetString("CreatedBy")
                    temp8 = MyReader.GetString("Currency_Code")
                    Try
                        temp9 = MyReader.GetString("Verified1By")
                    Catch ex As Exception
                        temp9 = ""
                    End Try
                    Try
                        temp10 = MyReader.GetString("Verified2By")
                    Catch ex As Exception
                        temp10 = ""
                    End Try
                    packlist.Text = MyReader.GetString("PackingList_No")

                    GetTgl("Received_CopyDoc_Dt", dtCopy)
                    GetTgl("Est_Delivery_Dt", dtETD)
                    GetTgl("Est_Arrival_Dt", dtETA)
                    GetTgl("Received_Doc_Dt", dtRec)
                    GetTgl("Verified1Dt", dtVerifySuppDoc)
                    GetTgl("Verified2Dt", dtRecBeaByAcc)
                    GetTgl("PIB_Dt", dtPIB)
                    GetTgl("SPPB_Dt", dtSPPB)
                    GetTgl("Est_SPPB_Dt", dtAJU)
                    GetTgl("Notice_Arrival_Dt", dtArrival)
                    GetTgl("OB_Dt", dtOB)
                    GetTgl("Est_Clr_Dt", dtEstDelivery)
                    GetTgl("Clr_Dt", dtDelivery)
                    GetTgl("Est_BAPB_Dt", dtEstClearance)
                    GetTgl("BAPB_Dt", dtClearance)
                    GetTgl("Forward_Doc_Dt", dtForward)
                    GetTgl("Area_Rcv_Doc_Dt", dtAreaRec)
                    GetTgl("Area_Rcv_Ril_Dt", dtAreaDeptan)
                    GetTgl("TT_Dt", dtTT)
                    GetTgl("Due_Dt", dtValue)

                    vessel.Text = MyReader.GetString("Vessel")
                    InsNo.Text = MyReader.GetString("Insurance_No")
                    InsAmt.Text = FormatNumber(MyReader.GetString("Insurance_Amount"), 2, , TriState.True)
                    bankname.Text = MyReader.GetString("Bank_Name")
                    accno.Text = MyReader.GetString("Account_No")

                    bb1 = FormatNumber(MyReader.GetString("Bea_Masuk"), 0, , , TriState.True)
                    bb2 = FormatNumber(MyReader.GetString("VAT"), 0, , , TriState.True)
                    bb3 = FormatNumber(MyReader.GetString("Pph21"), 0, , , TriState.True)
                    bb4 = FormatNumber(MyReader.GetString("PIUD"), 0, , , TriState.True)
                    bb5 = FormatNumber(MyReader.GetString("Kurs_Pajak"), 2, , , TriState.True)

                    Try
                        bb6 = FormatNumber(MyReader.GetString("Finalty"), 2, , , TriState.True)
                    Catch ex As Exception
                        bb6 = FormatNumber(0, 2, , , TriState.True)
                    End Try

                    dtCreated.Text = MyReader.GetString("CreatedDt")
                    Status2.Text = MyReader.GetString("Status")
                    temp11 = MyReader.GetString("Plant_Code")

                    Try
                        temp12 = MyReader.GetString("EMKL_Code")
                    Catch ex As Exception
                        temp12 = ""
                    End Try

                    GetTgl("RECEIVED_COPYDOC_DT_2", dtRecExp)
                    GetTgl("FORWARD_COPYDOC_DT_2", dtForwExp)
                    exp_note.Text = MyReader.GetString("Exp_Note")

                    free.Text = FormatNumber(MyReader.GetString("Free_Time"), 0, , , TriState.True)
                    free_ext.Text = FormatNumber(MyReader.GetString("FTE"), 0, , , TriState.True)

                    Try
                        temp13 = MyReader.GetString("FTE_PROPOSED_BY")
                    Catch ex As Exception
                        temp13 = ""
                    End Try

                    GetTgl("FTE_PROPOSED_DT", free_ext_prosdt)
                    GetTgl("FTE_APPROVED_DT", free_ext_appdt)


                    free_ext_note.Text = MyReader.GetString("FTE_Note")

                    '''If MyReader.GetString("TAHAN_OB") = "Y" Then ChkTahanOB.Checked = 1 Else ChkTahanOB.Checked = 0
                    If MyReader.GetString("TAHAN_OB") = "N" Then
                        cbOB.SelectedIndex = 0
                    Else
                        If MyReader.GetString("TAHAN_OB") = "O" Then
                            cbOB.SelectedIndex = 1
                        Else
                            cbOB.SelectedIndex = 2
                        End If
                    End If

                    If MyReader.GetString("RED_LINE") = "Y" Then ChkRedLn.Checked = 1 Else ChkRedLn.Checked = 0
                    If MyReader.GetString("MCI") = "Y" Then ChkMCI.Checked = 1 Else ChkMCI.Checked = 0
                    If MyReader.GetString("TRANS_IN") = "Y" Then ChkTrsIn.Checked = 1 Else ChkTrsIn.Checked = 0
                    If MyReader.GetString("TRANS_EX") = "Y" Then ChkTrsEx.Checked = 1 Else ChkTrsEx.Checked = 0
                    If MyReader.GetString("TREATMENT") = "Y" Then ChkTreatment.Checked = 1 Else ChkTreatment.Checked = 0
                    If MyReader.GetString("EIGEN_LOSS") = "Y" Then ChkEigen.Checked = 1 Else ChkEigen.Checked = 0
                    If MyReader.GetString("CMA") = "Y" Then ChkCMA.Checked = 1 Else ChkCMA.Checked = 0

                    Cur_Work.Text = FormatNumber(MyReader.GetString("LEAD_TIME"), 0, , , TriState.True)
                    Cur_Eq.Text = FormatNumber(MyReader.GetString("TOTAL_EQUIPMENT"), 0, , , TriState.True)

                    ajuNo.Text = MyReader.GetString("AJU_No")
                    xaju_no = MyReader.GetString("AJU_No")
                    PIBNo.Text = MyReader.GetString("PIB_No")
                    SPPBNo.Text = MyReader.GetString("SPPB_No")
                    hostbl.Text = MyReader.GetString("HostBL")

                    TotCont.Text = MyReader.GetString("TOTAL_CONTAINER")
                    GetTgl("sdem", sdem)
                    Try
                        dem.Text = MyReader.GetString("dem")
                        If dem.Text >= 0 Then
                            dem.Text = dem.Text + 1
                        Else
                            dem.Text = 0
                        End If
                    Catch ex As Exception
                        dem.Text = 0
                    End Try

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)

            


            suppl.Text = temp1
            DestPort.Text = temp2
            LoadPort.Text = temp3
            ShipLine.Text = temp4
            receivecode.Text = temp5
            crtcode.Text = temp7
            Currency.Text = temp8
            vercode1.Text = temp9
            vercode2.Text = temp10
            DestPlant.Text = temp11
            Expedition.Text = temp12
            CTPro.Text = temp13
            'jangan di pindahkan ke atas

            BeaMasuk.Text = bb1
            If bb1 = 0 Then cbExclude.Checked = True

            vat.Text = bb2
            pph.Text = bb3
            piud.Text = bb4
            KursPajak.Text = bb5
            Display = True
            finalty.Text = bb6
            Display = False     'supaya event finalty_textchanged tidak dijalankan
            If dtTT.Checked = False And Me.Text = "Bill of Lading" Then GetTTDate()
            dtTT.Enabled = True And (Me.Text = "Bill of Lading")
            'TT date waktu input pertama kali disabled
            'setelah save, bisa di edit
        End If
    End Sub
    Private Sub GetTTDate()
        Dim temp As String

        temp = AmbilData("if(tt_dt is null,DATE_ADD(curdate(),INTERVAL +1 DAY),tt_dt)", "vw_tt_date", "shipment_no=" & txtShipNo.Text & " group by shipment_no")
        If temp = "" Then
            dtTT.Text = GetServerDate2.ToString
            dtTT.Checked = False
        ElseIf temp = "0" Then
            dtTT.Text = GetServerDate2.ToString
            dtTT.Checked = False
        Else
            dtTT.Text = CDate(temp)
            dtTT.Checked = True
        End If

    End Sub
    Private Sub DisplayBLDetil()
        Dim ee As System.EventArgs
        Dim brs, cnt As SByte

        strSQL = "select po_no from tbl_shipping_detail where SHIPMENT_NO = '" & txtShipNo.Text & "' group by po_no"
        errMSg = "Bill of Lading data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    List.Items.Add(Trim(MyReader.GetString("PO_No")))
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        brs = List.Items.Count - 1
        MaxReadOnly = brs
        ReDim Preserve Array(brs)
        ReDim Preserve Array3(brs)
        FillMaxDateAndInv()
        
        'Kurs = GetKursPajak(-1)
        Kurs = CDec(KursPajak.Text)
        KursPajak.Text = FormatNumber(Kurs, 2, , , TriState.True)

        For cnt = 0 To brs
            List.SelectedIndex = cnt
            List_Click(List, ee)
        Next
        btnClearD.Enabled = (List.Items.Count > 0) And Me.Text = "Bill of Lading"
        ClearAll.Enabled = (List.Items.Count > 0) And Me.Text = "Bill of Lading"
    End Sub

    Private Sub FillMaxDateAndInv()
        Dim cnt, jml As SByte
        Dim tt As Date
        Dim inv As Decimal

        jml = List.Items.Count - 1
        For cnt = 0 To jml
            Try
                tt = CDate(AmbilData("max(invoice_dt)", "tbl_shipping_invoice", "po_no='" & Trim(List.Items(cnt)) & "' and shipment_no=" & txtShipNo.Text & " and invoice_no<>'' and invoice_amount>0"))
                Array3(cnt).MaxDate = tt
            Catch ex As Exception
                Array3(cnt).MaxDate = ""
            End Try
            Try
                inv = CDec(AmbilData("sum(invoice_amount)", "tbl_shipping_invoice", "po_no='" & Trim(List.Items(cnt)) & "' and shipment_no=" & txtShipNo.Text & " and invoice_no<>'' and invoice_amount>0"))
                Array3(cnt).Inv = inv
            Catch ex As Exception
            End Try
        Next
    End Sub
    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        If PilihBL.ShowDialog() = Windows.Forms.DialogResult.OK Then DisplaySelectedData()
    End Sub


    Private Sub blno_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles blno.Validating
        Dim xshipmentno, SQLStr As String
        Dim ee As System.EventArgs

        If (xblno = blno.Text) Then Exit Sub

        xshipmentno = "0"

        If blno.Text <> "" And PilihBL.v_shipno.Text = "" Then

            SQLStr = "SELECT shipment_no FROM tbl_shipping WHERE bl_no='" & blno.Text & "'"

            MyReader = DBQueryMyReader(SQLStr, MyConn, errMSg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        xshipmentno = MyReader.GetString("shipment_no")
                    Catch ex As Exception
                        xshipmentno = "0"
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            Else
                CloseMyReader(MyReader, UserData)
            End If
            If xshipmentno <> "0" Then
                PilihBL.v_shipno.Text = xshipmentno
                PilihBL.v_blno.Text = blno.Text
                DisplaySelectedData()
            End If
        End If
    End Sub

    Private Sub DisplaySelectedData()
        Dim ee As System.EventArgs

        If Not f_GetAuthorized(PilihBL.v_shipno.Text) Then
            PilihBL.v_shipno.Text = ""
            PilihBL.v_blno.Text = ""
            blno.Text = xblno
            Exit Sub
        End If

        xblno = blno.Text

        btnNew_Click(Button8, ee)
        ClearAllPO()
        txtShipNo.Text = PilihBL.v_shipno.Text
        blno.Text = PilihBL.v_blno.Text

        PilihBL.v_shipno.Text = ""
        PilihBL.v_blno.Text = ""

        RefreshDisplay(Button8, ee)
        'Hanya user yg sesuai dengan createdby di dokumen ini saja yg bisa mengupdate data BL 
        'kecuali untuk Tab Supplier Doc, Customs Doc dan Clearance Analyst
        If Me.Text = "Bill of Lading" Then UpdateAksesInput()
        '        grid.Columns(7).Frozen = True
        btnDelete.Enabled = True
    End Sub
    Private Sub EnabledPackUnitAndSpec()
        grid.ReadOnly = False
        grid.Columns(0).ReadOnly = True
        grid.Columns(1).ReadOnly = True
        grid.Columns(2).ReadOnly = True
        grid.Columns(3).ReadOnly = True
        grid.Columns(4).ReadOnly = True
        grid.Columns(5).ReadOnly = True
        grid.Columns(6).ReadOnly = True
        grid.Columns(7).ReadOnly = True
        grid.Columns(8).ReadOnly = True
        grid.Columns(9).ReadOnly = True
        grid.Columns(10).ReadOnly = True
        grid.Columns(11).ReadOnly = True
        grid.Columns(12).ReadOnly = True
        grid.Columns(13).ReadOnly = True
        grid.Columns(14).ReadOnly = False
    End Sub
    Private Sub UpdateAksesInput()

        btnDelete.Enabled = (UserData.UserCT = crtcode.Text)

        blno.ReadOnly = (UserData.UserCT <> crtcode.Text)
        hostbl.ReadOnly = blno.ReadOnly
        suppl.ReadOnly = blno.ReadOnly
        btnSearchSupplier.Enabled = Not (blno.ReadOnly)
        packlist.ReadOnly = blno.ReadOnly
        dtCopy.Enabled = Not (blno.ReadOnly)
        dtETD.Enabled = Not (blno.ReadOnly)
        dtETA.Enabled = Not (blno.ReadOnly)
        DestPlant.ReadOnly = blno.ReadOnly
        Button9.Enabled = Not (blno.ReadOnly)
        DestPort.ReadOnly = blno.ReadOnly
        btnSearch.Enabled = Not (blno.ReadOnly)
        LoadPort.ReadOnly = blno.ReadOnly
        Button1.Enabled = Not (blno.ReadOnly)
        ShipLine.ReadOnly = blno.ReadOnly
        Button2.Enabled = Not (blno.ReadOnly)
        vessel.ReadOnly = blno.ReadOnly

        'Tab Bea
        BeaMasuk.ReadOnly = blno.ReadOnly
        vat.ReadOnly = blno.ReadOnly
        pph.ReadOnly = blno.ReadOnly
        piud.ReadOnly = blno.ReadOnly
        KursPajak.ReadOnly = blno.ReadOnly
        If CostSlipStatus(txtShipNo.Text) Then
            Button5.Enabled = False
            btnClearD.Enabled = False
            ClearAll.Enabled = False
            grid.ReadOnly = True
            GridInv.ReadOnly = True
            finalty.ReadOnly = True

            Status2.Text = "Cost Slip Final Approved"
        Else
            Button5.Enabled = Not (blno.ReadOnly)
            btnClearD.Enabled = Not (blno.ReadOnly)
            ClearAll.Enabled = Not (blno.ReadOnly)
            grid.ReadOnly = blno.ReadOnly
            GridInv.ReadOnly = blno.ReadOnly
            GridPlant.ReadOnly = blno.ReadOnly
            finalty.ReadOnly = blno.ReadOnly
        End If
        If Me.Text = "Bill of Lading" And grid.ReadOnly Then EnabledPackUnitAndSpec()

        cbExclude.Enabled = Not (blno.ReadOnly)
        btnCalculate.Enabled = Not (blno.ReadOnly)

        Button7.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("DS-P"))
        dtRecBeaByAcc.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("DS-P"))


        'Tab Suppler Doc
        If PunyaAkses("DS-P") Then
            grid2.AllowUserToAddRows = True
            grid2.ReadOnly = False
            Button6.Enabled = True
            dtVerifySuppDoc.Enabled = True
        Else
            grid2.AllowUserToAddRows = Not (blno.ReadOnly)
            grid2.ReadOnly = blno.ReadOnly
            Button6.Enabled = Not (blno.ReadOnly)
            dtVerifySuppDoc.Enabled = Not (blno.ReadOnly)
        End If

        'Tab container
        If PunyaAkses("CF-A") Then
            'untuk user khusus / daerah
            grid4.AllowUserToAddRows = True
            grid4.ReadOnly = False
            TotCont.ReadOnly = False
        Else
            grid4.AllowUserToAddRows = Not (blno.ReadOnly)
            grid4.ReadOnly = blno.ReadOnly
            TotCont.ReadOnly = blno.ReadOnly
        End If

        'Tab For Finance
        InsNo.ReadOnly = blno.ReadOnly
        InsAmt.ReadOnly = blno.ReadOnly
        Button3.Enabled = Not (blno.ReadOnly)
        bankname.ReadOnly = blno.ReadOnly
        accno.ReadOnly = blno.ReadOnly
        dtValue.Enabled = Not (blno.ReadOnly)
        dtTT.Enabled = Not (blno.ReadOnly)

        'Tab Customer
        dtAJU.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtPIB.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtSPPB.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        If PunyaAkses("CF-A") Then
            grid3.AllowUserToAddRows = True
            grid3.ReadOnly = False
            ajuNo.ReadOnly = False
            PIBNo.ReadOnly = False
            SPPBNo.ReadOnly = False
        Else
            grid3.AllowUserToAddRows = Not (blno.ReadOnly)
            grid3.ReadOnly = blno.ReadOnly
            ajuNo.ReadOnly = blno.ReadOnly
            PIBNo.ReadOnly = blno.ReadOnly
            SPPBNo.ReadOnly = blno.ReadOnly
        End If

        'Tab Clearence
        dtArrival.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtRec.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtForward.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtAreaRec.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtAreaDeptan.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtOB.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A")) And (cbOB.SelectedIndex <> 0)
        dtDelivery.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtEstDelivery.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtEstClearance.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))
        dtClearance.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-A"))

        If PunyaAkses("CF-A") Then
            free.ReadOnly = False
            free_ext.ReadOnly = False
            free_ext_prosdt.Enabled = True
            free_ext_appdt.Enabled = True
            free_ext_note.ReadOnly = False
            '''ChkTahanOB.Enabled = True
            ChkRedLn.Enabled = True
            ChkMCI.Enabled = True
            ChkTrsIn.Enabled = True
            ChkTrsEx.Enabled = True
            ChkTreatment.Enabled = True
            ChkEigen.Enabled = True
            ChkCMA.Enabled = True

            Cur_Work.ReadOnly = False
            Cur_Eq.ReadOnly = False
        Else
            free.ReadOnly = blno.ReadOnly
            free_ext.ReadOnly = blno.ReadOnly
            free_ext_prosdt.Enabled = Not (blno.ReadOnly)
            free_ext_appdt.Enabled = Not (blno.ReadOnly)
            free_ext_note.ReadOnly = blno.ReadOnly
            '''ChkTahanOB.Enabled = Not (blno.ReadOnly)
            ChkRedLn.Enabled = Not (blno.ReadOnly)
            ChkMCI.Enabled = Not (blno.ReadOnly)
            ChkTrsIn.Enabled = Not (blno.ReadOnly)
            ChkTrsEx.Enabled = Not (blno.ReadOnly)
            ChkTreatment.Enabled = Not (blno.ReadOnly)
            ChkEigen.Enabled = Not (blno.ReadOnly)
            ChkCMA.Enabled = Not (blno.ReadOnly)

            Cur_Work.ReadOnly = blno.ReadOnly
            Cur_Eq.ReadOnly = blno.ReadOnly
        End If

        'Tab Expedition Info
        If PunyaAkses("CF-E") Then
            Expedition.ReadOnly = False
            btnExpedition.Enabled = True
            exp_note.ReadOnly = False
            grid5.AllowUserToAddRows = True
            grid5.ReadOnly = False
        Else
            Expedition.ReadOnly = blno.ReadOnly
            btnExpedition.Enabled = Not (blno.ReadOnly)
            exp_note.ReadOnly = blno.ReadOnly
            grid5.AllowUserToAddRows = Not (blno.ReadOnly)
            grid5.ReadOnly = blno.ReadOnly
        End If

        dtRecExp.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-E"))
        dtForwExp.Enabled = (Not (blno.ReadOnly) Or PunyaAkses("CF-E"))
    End Sub
    Private Sub SaveDisplayData()
        Dim brs, cnt As Integer

        brs = List.Items.Count - 1
        For cnt = 0 To brs
            SaveData(cnt, grid, GridInv, GridPlant)
        Next
        SaveData2()
    End Sub
    Private Sub DisplayDocument()
        Dim teks, str2, stat, num As String
        Dim pos, pjg As Integer
        Dim inPO As String

        ListBox1.Items.Clear()
        'If Me.Text = "Logistic Documents" Then
        If Me.Text = "Post Import Document" Then
            strSQL = "select concat('SI',' #',ord_no) as data, status from tbl_si where shipment_no=" & txtShipNo.Text & _
                     " union " & _
                     "select concat('RIL',' #',ril_no) as data,status from tbl_ril " & _
                     "where ril_no not in (select ril_no from tbl_ril_quota) and " & _
                     "shipment_no=" & txtShipNo.Text & " group by ril_no" & _
                     " union " & _
                     "select concat('RQL',' #',ril_no) as data,status from tbl_ril " & _
                     "where ril_no in (select ril_no from tbl_ril_quota) and " & _
                     "shipment_no=" & txtShipNo.Text & " group by ril_no" & _
                     " union " & _
                     "select concat('MC',' #',ord_no) as data, status from tbl_mci where shipment_no=" & txtShipNo.Text & _
                     " union " & _
                     "select concat(type_code,' #',ord_no) as data, status from tbl_remitance where shipment_no=" & txtShipNo.Text & _
                     " union " & _
                     "select concat(findoc_type,' #',ord_no) as data,findoc_status as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='DI' " & _
                     " union " & _
                     "select concat(type_code,' #',ord_no) as data,status from tbl_budgets " & _
                     "where shipment_no=" & txtShipNo.Text & " and type_code='TT' " & _
                     " union " & _
                     "select concat(type_code,' #',ord_no) as data,status from tbl_budgets " & _
                     "where shipment_no=" & txtShipNo.Text & " and type_code='CA' " & _
                     " union " & _
                     "select concat(findoc_type,' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='PV' " & _
                      " union " & _
                     "select concat(findoc_type,' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='VG' " & _
                     " union " & _
                     "select concat('SSPCP',' #',ord_no) as data,status from tbl_sspcp  " & _
                     "where shipment_no=" & txtShipNo.Text & _
                     " union " & _
                     "select concat(findoc_type,' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='CL' " & _
                     " union " & _
                     "select concat(type_code,' #',ord_no) as data,status from tbl_budgets " & _
                     "where shipment_no=" & txtShipNo.Text & " and type_code='BP' "
        End If
        'If Me.Text = "Financial Documents" Then
        If Me.Text = "Funds & Finance" Then
            strSQL = "select concat('BPUM',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='DP' " & _
                     " union " & _
                     "select concat('BI',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='BI' " & _
                     " union " & _
                     "select concat('BPJUM',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='PP' " & _
                     " union " & _
                     "select concat('PV',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='VP' " & _
                     " union " & _
                     "select concat(FinDoc_type,' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='CC' " & _
                     " union " & _
                     "select concat(FinDoc_type,' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                     "where shipment_no=" & txtShipNo.Text & " and findoc_type='CS' "
        End If

        If Me.Text = "Customs Clearance" Then
            strSQL = "select concat('SK-BC',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='BC' " & _
                 " union " & _
                 "select concat('SK-DO',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='KO' " & _
                 " union " & _
                 "select concat('SK-KR',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='SK' " & _
                 " union " & _
                 "select concat('SK-PC',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='PC' " & _
                 " union " & _
                 "select concat('SK-JC',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='JC' " & _
                 " union " & _
                 "select concat('SP-I ',' #',ord_no,'    (',findoc_groupto,')') as data,findoc_status as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='SZ' " & _
                 " union " & _
                 "select concat('SP-B ',' #',ord_no,'    (',findoc_groupto,')') as data,findoc_status as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='SB' " & _
                 " union " & _
                 "select concat('SP-BS',' #',ord_no) as data,findoc_status as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='BS' " & _
                 " union " & _
                 "select concat('DNP  ',' #',ord_no) as data,findoc_status as status from tbl_shipping_doc " & _
                 "where shipment_no=" & txtShipNo.Text & " and findoc_type='NP' "
        End If

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then



            While MyReader.Read
                Try
                    stat = MyReader.GetString("status")
                    teks = Trim(MyReader.GetString("data"))
                    str2 = Microsoft.VisualBasic.Left(teks, 2)
                    teks = Mid(teks & Space(45), 1, 45) & " - " & Microsoft.VisualBasic.Left(stat & Space(15), 15)
                    ListBox1.Items.Add(teks)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Function GetQty(ByVal str As String) As Decimal
        errMSg = "GetQty failed"
        MyReader = DBQueryMyReader(str, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    GetQty = MyReader.GetString("qty")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Function
    Private Function GetSisaQty(ByVal PO As String) As Decimal
        Dim strSQL As String
        Dim tQty, POQty As Decimal

        strSQL = "select sum(quantity) as 'qty' from tbl_shipping_Detail where po_no='" & PO & "' "
        tQty = GetQty(strSQL)

        strSQL = "select sum(a.quantity*((100+b.tolerable_delivery)/100)) as 'qty' " & _
                 "from tbl_po_Detail as a " & _
                 "inner join tbl_po as b on a.po_no=b.po_no " & _
                 "where a.po_no='" & PO & "' " & _
                 "group by a.po_no"
        POQty = GetQty(strSQL)
        GetSisaQty = POQty - tQty
    End Function
    Private Sub GetTgl(ByVal fld As String, ByVal obj As DateTimePicker)
        Dim temp As Object
        Try
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then obj.Value = temp
        Catch ex As Exception
        End Try
        obj.Checked = Not (temp Is Nothing)
    End Sub
    Private Sub IsiDS(ByVal idx As SByte)
        'PO Grid tab
        grid.DataSource = Nothing
        grid.Columns.Clear()
        grid.DataSource = Array(idx).DSPO
        FormatGridPO(grid)
        UpdateDisplayPackage(grid, List)

        'Invoice Grid tab
        GridInv.DataSource = Nothing
        GridInv.DataSource = Array(idx).DSInv
        FormatGridInv(GridInv)

        ''Plant Grid tab ->  bagian ini tidak perlu
        GridPlant.DataSource = Nothing
        GridPlant.Columns.Clear()
        GridPlant.DataSource = Array(idx).DSPlant
        FormatGridPlant(GridPlant)
        UpdateDisplayPlant(GridPlant, List)
    End Sub
    Private Sub List_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles List.Click
        Dim idx As SByte
        Dim ee As System.EventArgs

        If (List.Items.Count = 0) Then Exit Sub
        If POBaru = True Then Exit Sub
        If CekGrid() = False Then Exit Sub
        idx = List.SelectedIndex

        If Edit And Array(idx).DSPO Is Nothing Then
            DisplayPODetil(Trim(List.Items(List.SelectedIndex).ToString), blno.Text, grid, GridInv, GridPlant, List, txtShipNo.Text)
            DisplayInvDetil(Trim(List.Items(List.SelectedIndex).ToString), blno.Text, grid, GridInv, GridPlant, List, txtShipNo.Text)
            DisplayPlantDetil(Trim(List.Items(List.SelectedIndex).ToString), blno.Text, grid, GridInv, GridPlant, List, txtShipNo.Text)
            GetMatOriginData(Trim(List.Items(List.SelectedIndex).ToString), idx)
        End If
        
        IsiDS(idx)
        grid.ReadOnly = (idx <= MaxReadOnly)
        grid.Columns(7).Visible = (idx > MaxReadOnly)
        POTab_SelectedIndexChanged(sender, ee)
        PrevPO = List.Items(List.SelectedIndex).ToString
        'If Me.Text = "Bill of Lading" And grid.ReadOnly Then EnabledPackUnitAndSpec()
    End Sub
    Private Function GetTotalInv() As Decimal
        Dim cnt, jml As SByte
        Dim temp As Decimal

        jml = List.Items.Count - 1
        temp = 0
        For cnt = 0 To jml
            temp = temp + Array3(cnt).Inv
        Next
        GetTotalInv = temp
    End Function
    Private Sub GetMatOriginData(ByVal PO As String, ByVal idx As SByte)
        Dim strKurs, strInv As String
        Dim inv As Decimal

        strKurs = Kurs
        strKurs = GetNum2(strKurs)
        strInv = GetTotalInv()
        strInv = GetNum2(strInv)
        errMSg = "Error read Bea"
        strSQL = "select sum(c.Bea_Masuk*" & strInv & ")*" & strKurs & " as Bea," & _
                 "sum(c.ppn*(" & strInv & "+c.bea_masuk))*" & strKurs & " as PPN," & _
                 "sum(c.PPH_21)*" & strKurs & "  as PPH," & _
                 "sum(c.PIUD_TR)*" & strKurs & " as PIUD " & _
                 "from tbl_shipping_detail as a " & _
                 "inner join tbl_po_Detail as b on a.po_no=b.po_no and a.po_item=b.po_item " & _
                 "inner join tbm_material_origin as c on c.material_code=b.material_code and b.country_code=c.country_code " & _
                 "where a.po_no='" & PO & "' " & _
                 "and a.shipment_no=" & txtShipNo.Text & _
                 " group by  a.shipment_no,a.po_no"

        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    Array3(idx).Bea = CDec(MyReader.GetString("Bea"))
                    Array3(idx).PPN = CDec(MyReader.GetString("PPN"))
                    Array3(idx).PPH = CDec(MyReader.GetString("PPH"))
                    Array3(idx).PIUD = CDec(MyReader.GetString("PIUD"))
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    'Dipake juga oleh FrmCustom
    Public Sub DisplayPODetil(ByVal PO As String, ByVal blno As String, ByVal Grd As DataGridView, ByVal GrdInv As DataGridView, ByVal GrdPlant As DataGridView, ByVal Lst As ListBox, ByVal ShipNo As Integer)
        Dim dts As DataTable

        'PO Detail
        Grd.DataSource = Nothing
        Grd.Columns.Clear()

        errMSg = "Bill of Lading detail data view failed"
        strSQL = "select a.PO_Item as 'Item',a.material_code as 'Mat.Code',d.material_name as 'Material Name',e.country_name as 'Origin'," & _
                 "b.quantity as 'PO Quantity',b.unit_code as 'Unit',b.Package_code as 'PO Package'," & _
                 "0 as 'Available Quantity'," & _
                 "a.Quantity as 'Actual Quantity',f.Pack_Code as 'Package Unit',a.Pack_Quantity as 'Package Size',e.country_code,b.price,trim(a.Specification) as Specification " & _
                 "from tbl_shipping_detail as a " & _
                 "inner join tbl_po_Detail as b on a.po_no=b.po_no and a.po_item=b.po_item " & _
                 "inner join tbl_shipping as c on a.shipment_no=c.shipment_no " & _
                 "inner join tbm_material as d on a.material_Code=d.material_code " & _
                 "inner join tbm_country as e on e.country_code=b.country_code " & _
                 "inner join tbm_packing as f on a.pack_code=f.pack_code " & _
                 "where c.bl_no='" & blno & "' " & _
                 "and a.po_no='" & PO & "' " & _
                 "and c.shipment_no=" & ShipNo & _
                 " order by a.PO_Item"

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        Grd.DataSource = dts
        FormatGridPO(Grd)
        Grd.Columns(7).Visible = False
        UpdateDisplayPackage(Grd, Lst)
        SaveData(Lst.SelectedIndex, Grd, GrdInv, GridPlant)
    End Sub

    'Dipake juga oleh FrmCustom
    Public Sub DisplayInvDetil(ByVal PO As String, ByVal blno As String, ByVal Grd As DataGridView, ByVal GrdInv As DataGridView, ByVal GrdPlant As DataGridView, ByVal Lst As ListBox, ByVal ShipNo As Integer)
        Dim dts As DataTable

        'Invoice Detail
        GrdInv.DataSource = Nothing
        GrdInv.Columns.Clear()

        errMSg = "Bill of Lading detail data view failed"
        strSQL = "select b.invoice_no as 'No.',b.Invoice_dt as 'Date',if(Invoice_Origin is null,0,invoice_origin) as 'Original Amount',b.Invoice_amount as 'Amount',b.Invoice_penalty as 'Penalty Inv.' " & _
                 "from tbl_Shipping as a " & _
                 "inner join tbl_shipping_invoice as b on a.shipment_no=b.shipment_no " & _
                 "where a.bl_no='" & blno & "' " & _
                 "and b.po_no='" & PO & "' " & _
                 "and a.shipment_no=" & ShipNo & _
                 " order by b.ord_no"

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        GrdInv.DataSource = dts
        FormatGridInv(GrdInv)
        SaveData(Lst.SelectedIndex, Grd, GrdInv, GridPlant)
        GrdInv.AllowUserToAddRows = False
        GrdInv.ReadOnly = (Me.Text <> "Bill of Lading")
    End Sub

    Public Sub DisplayPlantDetil(ByVal PO As String, ByVal blno As String, ByVal Grd As DataGridView, ByVal GrdInv As DataGridView, ByVal GrdPlant As DataGridView, ByVal Lst As ListBox, ByVal ShipNo As Integer)
        Dim dts As DataTable

        'Plant Detail
        GrdPlant.DataSource = Nothing
        GrdPlant.Columns.Clear()

        errMSg = "Bill of Lading detail data view failed"
        strSQL = "select t1.plant_code as 'DestinationPlantCode', t1.note as Note " & _
                 "from tbl_shipping_plant t1 " & _
                 "where t1.shipment_no=" & ShipNo & " and t1.po_no='" & PO & "' "

        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        GrdPlant.DataSource = dts

        FormatGridPlant(GrdPlant)
        UpdateDisplayPlant(GrdPlant, Lst)
        SaveData(Lst.SelectedIndex, Grd, GrdInv, GrdPlant)
    End Sub

    Private Sub btnBR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBR.Click
        Dim f As New FrmBR(txtShipNo.Text, "", "", "0", "0", "")
        f.ShowDialog()
        DisplayDocument()
    End Sub

    Private Sub btnDI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDI.Click
        Dim v_br, v_di As String
        v_br = AmbilData("count(ord_no)", "tbl_remitance", "shipment_no = '" & txtShipNo.Text & "'")
        v_di = AmbilData("count(ord_no)", "tbl_shipping_doc", "shipment_no = '" & txtShipNo.Text & "' and findoc_type = 'DI'")
        If (v_br - 1) <> v_di Then
            MsgBox("BR no available")
        Else
            Dim f As New FrmDI(txtShipNo.Text, "", "", "", "", "")
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnPV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPV.Click
        Dim f As New FrmPV(txtShipNo.Text, "", "", "", "", "")
        If DataOK("PV") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnVG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVG.Click
        Dim f As New FrmVG(txtShipNo.Text, "", "", "", "", "")
        If DataOK("VG") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnSSPCP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSSPCP.Click
        Dim f As New FrmSSPCP(txtShipNo.Text, "", "", "", "", "")
        If DataOK("SP") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then receivecode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub receivecode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles receivecode.TextChanged
        If receivecode.Text <> "" Then
            receivedname.Text = AmbilData("name", "tbm_users", "user_ct=" & receivecode.Text)
        Else
            receivedname.Text = ""
        End If
    End Sub

    Private Sub InsAmt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsAmt.Enter
        InsAmt.SelectAll()
    End Sub

    Private Sub InsAmt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles InsAmt.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then InsAmt_Leave(sender, ee)
    End Sub

    Private Sub InsAmt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles InsAmt.Leave
        Try
            InsAmt.Text = FormatNumber(InsAmt.Text, 2, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsAmt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles InsAmt.Validating
        If IsNumeric(InsAmt.Text) = False Then
            MsgBox("Invalid insurance amount, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub


    Private Sub Currency_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Currency.TextChanged
        currname.Text = AmbilData("currency_name", "tbm_currency", "currency_code='" & Currency.Text & "'")
    End Sub

    Private Sub crtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crtcode.TextChanged
        crtname.Text = AmbilData("name", "tbm_users", "user_ct='" & crtcode.Text & "'")
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim ee As DataGridViewCellEventArgs

        If grid2.DataSource Is Nothing And TabPage9.Visible Then GetDataSuppDoc(grid2, txtShipNo.Text, Edit)
        If grid3.DataSource Is Nothing And TabPage10.Visible Then
            GetDataCustomDoc(grid3, txtShipNo.Text, Edit)
            GetDataPIBStatus(GridPIB, ajuNo.Text, Edit)
            GridPIB_CellClick(sender, ee)
            If ajuNo.Text <> "" Then
                strSQL = "SELECT pibno, pibtg , dokresno, dokrestg" & _
                        " FROM pib_tblpibres a " & _
                        " INNER JOIN pib_tblpibhdr b ON a.car = b.car" & _
                        " WHERE a.car = '" & ajuNo.Text & "'"
                ' supram
                errMSg = "Data add"
                MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        '                    If PIBNo.Text = "" Then
                        Try
                            PIBNo.Text = MyReader.GetString(0)
                            dtPIB.Text = MyReader.GetString(1)
                        Catch ex As Exception
                            PIBNo.Text = Nothing
                            dtPIB.Text = Nothing
                        End Try
                        '                    End If
                        '                    If SPPBNo.Text = "" Then
                        Try
                            SPPBNo.Text = MyReader.GetString(2)
                            dtSPPB.Text = MyReader.GetString(3)
                        Catch ex As Exception
                            SPPBNo.Text = Nothing
                            dtSPPB.Text = Nothing
                        End Try
                        '                    End If
                    End While
                End If
                MyReader.Close()


            End If
        End If
        If grid4.DataSource Is Nothing And TabPage11.Visible Then GetDataContainer(grid4, txtShipNo.Text, Edit)
        If grid5.DataSource Is Nothing And TabPage12.Visible Then GetDataExpeditionDoc(grid5, txtShipNo.Text, Edit)

        'Hanya user yg sesuai dengan createdby di dokumen ini saja yg bisa mengupdate data BL 
        'kecuali untuk Tab Supplier Doc, Customs Doc dan Clearance Analyst
        If Me.Text = "Bill of Lading" Then UpdateAksesInput()


        


    End Sub

    Private Sub grid2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid2.CellEndEdit
        Dim brs As Integer

        brs = grid2.CurrentCell.RowIndex
        If grid2.Item(2, brs).Value Is DBNull.Value Then grid2.Item(2, brs).Value = ""
        If grid2.Item(3, brs).Value Is DBNull.Value Then grid2.Item(3, brs).Value = GetServerDate()
        If grid2.Item(4, brs).Value Is DBNull.Value Then grid2.Item(4, brs).Value = ""
        If grid2.Item(5, brs).Value Is DBNull.Value Then grid2.Item(5, brs).Value = ""
        grid2.Item(0, brs).Value = grid2.Item(1, brs).Value
        grid2.Item(4, brs).Value = grid2.Item(5, brs).Value
        SaveData2()
    End Sub

    Private Sub grid2_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid2.CurrentCellDirtyStateChanged
        If DataError = True And grid2.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid2_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid2.DataError
        If grid2.CurrentCell.ColumnIndex = 3 Then
            MsgBox("Invalid Date")
            DataError = True
        End If
    End Sub
    Private Sub grid2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grid2.RowsAdded
        Dim brs As Integer
        Dim tg As Date

        If Edit Then Exit Sub
        Try
            brs = grid2.CurrentCell.RowIndex
            tg = GetServerDate()
            grid2.Item(2, brs).Value = ""
            grid2.Item(3, brs).Value = tg
            grid2.Item(4, brs).Value = ""
            grid2.Rows(brs).Cells(5).Value = "OK"
            grid2.Item(6, brs).Value = ""
            Button6.Visible = True
            Button7.Visible = True
            dtVerifySuppDoc.Enabled = True
            dtRecBeaByAcc.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'DS-P'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'DS-P'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then vercode1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'FI-C'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then vercode2.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub vercode1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles vercode1.TextChanged
        vername1.Text = AmbilData("name", "tbm_users", "user_ct='" & vercode1.Text & "'")
    End Sub

    Private Sub vercode2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles vercode2.TextChanged
        vername2.Text = AmbilData("name", "tbm_users", "user_ct='" & vercode2.Text & "'")
    End Sub

    Private Sub grid3_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid3.CellEndEdit
        Dim brs As Integer

        brs = grid3.CurrentCell.RowIndex
        If grid3.Item(1, brs).Value Is DBNull.Value Then grid3.Item(1, brs).Value = ""
        If grid3.Item(2, brs).Value Is DBNull.Value Then grid3.Item(2, brs).Value = ""
        If grid3.Item(3, brs).Value Is DBNull.Value Then grid3.Item(3, brs).Value = GetServerDate()
        If grid3.Item(4, brs).Value Is DBNull.Value Then grid3.Item(4, brs).Value = ""
        grid3.Item(0, brs).Value = grid3.Item(1, brs).Value
        SaveData2()
    End Sub

    Private Sub grid5_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid5.CellEndEdit
        Dim brs As Integer

        brs = grid5.CurrentCell.RowIndex
        If grid5.Item(1, brs).Value Is DBNull.Value Then grid5.Item(1, brs).Value = ""
        If grid5.Item(2, brs).Value Is DBNull.Value Then grid5.Item(2, brs).Value = ""
        If grid5.Item(3, brs).Value Is DBNull.Value Then grid5.Item(3, brs).Value = GetServerDate()
        If grid5.Item(4, brs).Value Is DBNull.Value Then grid5.Item(4, brs).Value = ""
        grid5.Item(0, brs).Value = grid5.Item(1, brs).Value
        SaveData2()
    End Sub

    Private Sub grid3_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid3.CurrentCellDirtyStateChanged
        If DataError = True And grid3.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid5_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid5.CurrentCellDirtyStateChanged
        If DataError = True And grid5.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid3_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid3.DataError
        If grid3.CurrentCell.ColumnIndex = 3 Then
            MsgBox("Invalid Date")
            DataError = True
        End If
    End Sub

    Private Sub grid5_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid5.DataError
        If grid5.CurrentCell.ColumnIndex = 3 Then
            MsgBox("Invalid Date")
            DataError = True
        End If
    End Sub

    Private Sub grid4_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid4.CellEndEdit
        Dim brs As Integer

        brs = grid4.CurrentCell.RowIndex
        If grid4.Item(0, brs).Value Is DBNull.Value Then grid4.Item(0, brs).Value = ""
        grid4.Item(1, brs).Value = grid4.Item(2, brs).Value
        SaveData2()
    End Sub

    Private Sub grid4_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid4.CurrentCellDirtyStateChanged
        If DataError = True And grid4.IsCurrentCellDirty = False Then DataError = False
    End Sub

    Private Sub grid4_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grid4.DataError
        If grid4.CurrentCell.ColumnIndex = 2 Then
            MsgBox("Invalid Size, input numeric value")
            DataError = True
        End If
    End Sub

    Private Sub grid3_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grid3.RowsAdded
        Dim brs As Integer
        Dim tg As Date

        If Edit Then Exit Sub
        Try
            brs = grid3.CurrentCell.RowIndex
            tg = GetServerDate()
            grid3.Item(1, brs).Value = ""
            grid3.Item(3, brs).Value = tg
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grid5_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grid5.RowsAdded
        Dim brs As Integer
        Dim tg As Date

        If Edit Then Exit Sub
        Try
            brs = grid5.CurrentCell.RowIndex
            tg = GetServerDate()
            grid5.Item(1, brs).Value = ""
            grid5.Item(3, brs).Value = tg
        Catch ex As Exception
        End Try
    End Sub

    Private Sub grid4_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grid4.RowsAdded
        Dim brs As Integer

        If Edit Then Exit Sub
        Try
            brs = grid4.CurrentCell.RowIndex
            grid4.Item(0, brs).Value = ""
            grid4.Item(2, brs).Value = ""
            grid4.Item(3, brs).Value = 0
        Catch ex As Exception
        End Try
    End Sub
    Private Function DataOK(ByVal jns As String) As Boolean
        Dim mess1, mess2, mess3 As String
        Dim status As String = ""
        Dim SQLstr As String = ""
        Dim V_MODULE_NAME As String = ""


        If jns = "PV" Then
            V_MODULE_NAME = "Payment Voucher"
        ElseIf jns = "CL" Then
            V_MODULE_NAME = "Cover Letter"
        ElseIf jns = "BP" Then
            V_MODULE_NAME = "B-PIB"
        ElseIf jns = "SP" Then
            V_MODULE_NAME = "SSPCP"
        ElseIf jns = "CC" Then
            V_MODULE_NAME = "Inklaring"
        ElseIf jns = "CS" Then
            V_MODULE_NAME = "Cost Slip"
        ElseIf jns = "BPUM" Then
            V_MODULE_NAME = "BPUM"
        ElseIf jns = "BI" Then
            V_MODULE_NAME = "BI"
        ElseIf jns = "BPJUM" Then
            V_MODULE_NAME = "BPJUM"
        ElseIf jns = "MC" Then
            V_MODULE_NAME = "MCI"
        ElseIf jns = "VG" Then
            V_MODULE_NAME = "Voucher Giro"
        ElseIf jns = "KO" Then
            V_MODULE_NAME = "SK-DO"
        ElseIf jns = "SK" Then
            V_MODULE_NAME = "SK-KR"
        ElseIf jns = "JC" Then
            V_MODULE_NAME = "SK-JC"
        ElseIf jns = "BC" Then
            V_MODULE_NAME = "SK-BC"
        ElseIf jns = "PC" Then
            V_MODULE_NAME = "SK-PC"
        ElseIf jns = "BS" Then
            V_MODULE_NAME = "SP-BS"
        ElseIf jns = "SZ" Then
            V_MODULE_NAME = "SP-I "
        ElseIf jns = "SB" Then
            V_MODULE_NAME = "SP-B "
		ElseIf jns = "NP" Then
            V_MODULE_NAME = "DNP"
        End If

        If ((jns = "PV" Or jns = "SP" Or jns = "CL" Or jns = "MC" Or jns = "VG") And (Me.Text = "Post Import Document")) Or ((jns = "CC" Or jns = "CS" Or jns = "VP") And (Me.Text = "Funds & Finance")) Then
            errMSg = "Failed when read " & jns & " data"
            mess1 = V_MODULE_NAME & " has been closed"
            mess2 = V_MODULE_NAME & " has been created"
            If jns = "PV" Or jns = "CL" Then
                SQLstr = "select FINDOC_STATUS from tbl_shipping_doc " & _
                         "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_shipping_doc where " & _
                         "SHIPMENT_NO='" & txtShipNo.Text & "' and FINDOC_TYPE='" & jns & "') " & _
                         "and FINDOC_TYPE='" & jns & "'"
            ElseIf jns = "SP" Then
                SQLstr = "select STATUS from tbl_sspcp " & _
                         "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_sspcp where " & _
                         "SHIPMENT_NO='" & txtShipNo.Text & "' ) "
            ElseIf jns = "MC" Then
                SQLstr = "select STATUS from tbl_mci " & _
                         "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_mci where " & _
                         "SHIPMENT_NO='" & txtShipNo.Text & "' ) "
            ElseIf jns = "CC" Then
                'dokumen bisa di buat lagi selama masih ada PO belum ada CCnya.

                SQLstr = "SELECT t1.findoc_status FROM tbl_shipping_doc t1 " & _
                         "WHERE t1.findoc_status<>'Rejected' AND " & _
                         "t1.SHIPMENT_NO='" & txtShipNo.Text & "' AND t1.FINDOC_TYPE='" & jns & "' AND " & _
                         "CONCAT(TRIM(t1.findoc_no),TRIM(t1.findoc_reff)) IN " & _
                         " (SELECT CONCAT(TRIM(t2.po_no), TRIM(t2.po_item)) FROM tbl_shipping_detail t2 " & _
                         "  WHERE t2.shipment_no=t1.shipment_no) "

            ElseIf jns = "CS" Then
                'dokumen bisa di buat lagi selama masih ada PO belum ada CSnya.

                ''SQLstr = "SELECT t1.findoc_status FROM tbl_shipping_doc t1 " & _
                SQLstr = "SELECT '' findoc_status FROM tbl_shipping_doc t1 " & _
                       "WHERE t1.findoc_status<>'Rejected' AND " & _
                       "t1.SHIPMENT_NO='" & txtShipNo.Text & "' AND t1.FINDOC_TYPE='" & jns & "' AND " & _
                       "CONCAT(TRIM(t1.findoc_no),TRIM(t1.findoc_reff)) IN " & _
                       " (SELECT CONCAT(TRIM(t2.po_no), TRIM(t2.po_item)) FROM tbl_shipping_detail t2 " & _
                       "  WHERE t2.shipment_no=t1.shipment_no) "

            ElseIf jns = "VG" Then
                SQLstr = "select FINDOC_STATUS from tbl_shipping_doc " & _
                         "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_shipping_doc where " & _
                         "SHIPMENT_NO='" & txtShipNo.Text & "' and FINDOC_TYPE='" & jns & "') " & _
                         "and FINDOC_TYPE='" & jns & "'"

            End If
            MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        If jns = "SP" Or jns = "MC" Then
                            status = MyReader.GetString("STATUS")
                        Else
                            status = MyReader.GetString("FINDOC_STATUS")
                        End If
                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            End If

            If (status = "Approved" Or status = "Open" Or status = "Closed") Then
                If status = "Closed" Then
                    MsgBox(mess1)
                    DataOK = False
                Else
                    MsgBox(mess2)
                    DataOK = False
                End If
            Else
                DataOK = True
            End If
        End If
        If jns = "BP" Then
            errMSg = "Failed when read " & jns & " data"
            mess1 = V_MODULE_NAME & " has been closed"
            mess2 = V_MODULE_NAME & " has been created"
            mess3 = "There's no Approved SSPCP document"
            SQLstr = "select STATUS from tbl_budgets " & _
                     "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_budgets where " & _
                     "SHIPMENT_NO='" & txtShipNo.Text & "' and type_code = '" & jns & "') and type_code = '" & jns & "'"
            MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        status = MyReader.GetString("STATUS")
                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            End If

            If (status = "Approved" Or status = "Closed") Then
                If status = "Closed" Then
                    MsgBox(mess1)
                    DataOK = False
                Else
                    MsgBox(mess2)
                    DataOK = False
                End If
            Else
                DataOK = True
            End If
            status = ""

            If DataOK = True Then
                'cek ada sspcp nya ga ?
                SQLstr = "select STATUS from tbl_sspcp " & _
                         "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_sspcp where " & _
                         "SHIPMENT_NO='" & txtShipNo.Text & "' ) "
                MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            status = MyReader.GetString("STATUS")
                        Catch ex As Exception
                        End Try
                    End While
                    CloseMyReader(MyReader, UserData)
                End If
                If (status = "Approved" Or status = "Closed" Or status = "Rejected") Then
                    If status = "Closed" Or status = "Rejected" Then
                        MsgBox(mess3)
                        DataOK = False
                    Else
                        DataOK = True
                    End If
                End If
            End If
        End If
        ''''''''''
        If ((jns = "PV") Or (jns = "BPUM") Or (jns = "BI") Or (jns = "BPJUM")) And (Me.Text = "Funds & Finance") Then
            errMSg = "Failed when read Bill of Lading data"
            mess1 = "Bill of Lading has been closed"
            SQLstr = "select STATUS from tbl_shipping where SHIPMENT_NO=" & txtShipNo.Text
            MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        status = MyReader.GetString("STATUS")
                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            End If
            If status = "Closed" Then
                MsgBox(mess1)
                DataOK = False
            Else
                DataOK = True
            End If
        End If

        If ((jns = "KO") Or (jns = "SK") Or (jns = "JC") Or (jns = "BC") Or (jns = "PC") Or (jns = "BS")) Or (jns = "NP") Then
            errMSg = "Failed when read " & jns & " data"
            mess1 = V_MODULE_NAME & " has been closed"
            mess2 = V_MODULE_NAME & " has been created"

            strSQL = "select FINDOC_STATUS from tbl_shipping_doc " & _
                     "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_shipping_doc where " & _
                     "SHIPMENT_NO='" & txtShipNo.Text & "' and FINDOC_TYPE='" & jns & "') " & _
                     "and FINDOC_TYPE='" & jns & "'"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        status = MyReader.GetString("FINDOC_STATUS")
                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            End If

            If (status = "Approved" Or status = "Open" Or status = "Closed") Then
                If status = "Closed" Then
                    MsgBox(mess1)
                    DataOK = False
                Else
                    MsgBox(mess2)
                    DataOK = False
                End If
            Else
                DataOK = True
            End If
        End If

        If ((jns = "SZ") Or (jns = "SB")) Then
            errMSg = "Failed when read " & jns & " data"
            mess1 = V_MODULE_NAME & " has been closed"

            strSQL = "select FINDOC_STATUS from tbl_shipping_doc " & _
                     "where SHIPMENT_NO='" & txtShipNo.Text & "'" & " and ORD_NO=(select max(ord_no) from tbl_shipping_doc where " & _
                     "SHIPMENT_NO='" & txtShipNo.Text & "' and FINDOC_TYPE='" & jns & "') " & _
                     "and FINDOC_TYPE='" & jns & "'"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        status = MyReader.GetString("FINDOC_STATUS")
                    Catch ex As Exception
                    End Try
                End While
                CloseMyReader(MyReader, UserData)
            End If

            If (status = "Approved" Or status = "Open" Or status = "Closed") Then
                If status = "Closed" Then
                    MsgBox(mess1)
                    DataOK = False
                Else
                    DataOK = True
                End If
            Else
                DataOK = True
            End If
        End If

    End Function

    Private Function CekBolehDeleteGak(ByVal shipno As String) As Boolean
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "SELECT BolehDelBL(" & txtShipNo.Text & ")"
        MyComm.CommandType = CommandType.Text
        CekBolehDeleteGak = MyComm.ExecuteScalar()
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim hasil, bolehDelete As Boolean
        Dim EE As System.EventArgs

        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub
        ''langsung di cek di prosedure DelBL
        ''bolehDelete = CekBolehDeleteGak(txtShipNo.Text)
        ''If bolehDelete = False Then
        ''MsgBox("Can't delete BL because not all document rejected")
        ''Exit Sub
        ''End If
        strSQL = "Run Stored Procedure DelBL (" & txtShipNo.Text & "," & UserData.UserCT & ")"
        With MyComm
            .CommandText = "DelBL"
            .CommandType = CommandType.StoredProcedure

            With .Parameters
                .Clear()
                .AddWithValue("ShipNo", txtShipNo.Text)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("AuditStr", strSQL)
                .AddWithValue("Hasil", hasil)
            End With
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End With

        If hasil = True Then
            f_msgbox_successful("Delete Bill of Lading")
            btnNew_Click(sender, EE)
        Else
            MsgBox("Delete Bill of Lading failed")
        End If
    End Sub
    Private Sub btnVP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVP.Click
        Dim f As New FrmVP(0, txtShipNo.Text, 0, Status2.Text, blno.Text)
        If DataOK("PV") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub free_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles free.Enter
        free.SelectAll()
    End Sub

    Private Sub free_ext_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles free_ext.Enter
        free_ext.SelectAll()
    End Sub

    Private Sub free_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles free.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then free_Leave(sender, ee)
    End Sub

    Private Sub free_ext_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles free_ext.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then free_ext_Leave(sender, ee)
    End Sub

    Private Sub free_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles free.Leave
        Try
            free.Text = FormatNumber(free.Text, 0, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub free_ext_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles free_ext.Leave
        Try
            free_ext.Text = FormatNumber(free_ext.Text, 0, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub free_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles free.Validating
        If IsNumeric(free.Text) = False Then
            MsgBox("Invalid free time, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub free_ext_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles free_ext.Validating
        If IsNumeric(free_ext.Text) = False Then
            MsgBox("Invalid free time (extended), input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub GetStartDemurrage()
        Dim angka As Integer
        Dim temp As Date

        If DataError Then Exit Sub
        If free.Text = "" Then Exit Sub
        Try
            angka = CInt(free.Text)
            If (free_ext.Text <> "" And free_ext_appdt.Checked) Then angka = angka + CInt(free_ext.Text)
            temp = DateAdd(DateInterval.Day, angka, dtArrival.Value)
            sdem.Text = temp.ToString
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dtArrival_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtArrival.ValueChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub

    Private Sub free_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles free.TextChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub

    Private Sub dtDelivery_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtDelivery.ValueChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub

    Private Sub GetSelisihTgl()
        Dim selisih As Integer
        Dim tg1, tg2 As Date

        If sdem.Text = "" Then Exit Sub
        tg1 = CDate(sdem.Text)

        If dtClearance.Checked Then
            tg2 = dtClearance.Value
        ElseIf dtEstClearance.Checked Then
            tg2 = dtEstClearance.Value
        ElseIf dtDelivery.Checked Then
            tg2 = dtDelivery.Value
        ElseIf dtEstDelivery.Checked Then
            tg2 = dtEstDelivery.Value
        Else
            selisih = -1
        End If

        If selisih = -1 Then
            dem.Text = 0
        Else
            selisih = DateDiff(DateInterval.Day, tg1, tg2)
            dem.Text = FormatNumber(selisih, 0, , , TriState.True)
            If dem.Text >= 0 Then
                dem.Text = dem.Text + 1
            Else
                dem.Text = 0
            End If
        End If
    End Sub
    Private Sub FrmBL_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Me.Text = "Bill of Lading" Then GetDataSuppDoc(grid2, txtShipNo.Text, Edit)
        If (Me.Text = "Bill of Lading" And blno.Text = "") Then 'untuk pertama muncul inisialisasi tab2 berisi list box
            POTab.SelectedIndex = 2
            POTab.SelectedIndex = 0
        End If
        blno.Focus()
    End Sub
    'Function2 PUBLIC 
    'Used by FrmCustom
    Public Sub GetDataSuppDoc(ByVal Grd As DataGridView, ByVal sno As String, ByVal stat As Boolean)
        Dim brs, cnt As Integer
        Dim str As String
        Dim dts, dtscb1, dtscb2 As DataTable
        Dim cbn1, cbn2 As New DataGridViewComboBoxColumn

        'Supplier Doc
        Grd.DataSource = Nothing
        Grd.Columns.Clear()

        'Grid selain combobox
        errMSg = "Tbl_Doc_Supplier data view failed"
        strSQL = "select Doc_Code as 'DocCode',Doc_No as 'Doc. No',Doc_Dt as 'Date',if(Doc_Copy=1,'OK','Not OK') as Status,Doc_Remark as 'Remark', Doc_Remark as 'OldRemark', Doc_Dt as 'OldDate', if(Doc_Copy=1,'OK','Not OK') as OldStatus, Doc_History as 'HistRemark'  from tbl_doc_supplier " & _
                 "where shipment_no=" & sno
        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        Grd.DataSource = dts

        'Combo Box Document
        errMSg = "Tbm_Document data view failed"
        strSQL = "select * from tbm_document where doc_code LIKE 'SD%' or doc_code=''"
        dtscb1 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn1
            .DataSource = dtscb1
            .DisplayMember = "DOC_NAME"
            .ValueMember = "DOC_CODE"
        End With
        Grd.Columns.Insert(1, cbn1)
        Grd.Columns(1).HeaderText = "Document"

        'Combo Box
        errMSg = ""
        strSQL = "select 'OK' as str union select 'Not OK' as str"
        dtscb2 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn2
            .DataSource = dtscb2
            .DisplayMember = "str"
            .ValueMember = "str"
        End With
        Grd.Columns.Insert(5, cbn2)
        Grd.Columns(5).HeaderText = "Status"

        Grd.Columns(0).Visible = False
        Grd.Columns(4).Visible = False
        Grd.Columns(1).Width = 180
        Grd.Columns(2).Width = 100
        Grd.Columns(3).Width = 70
        Grd.Columns(4).Width = 50
        Grd.Columns(5).Width = 70
        Grd.Columns(6).Width = 250
        Grd.Columns(7).Visible = False
        Grd.Columns(8).Visible = False
        Grd.Columns(9).Visible = False
        Grd.Columns(10).Visible = False
        Grd.ReadOnly = (Me.Text <> "Bill of Lading")
        Grd.AllowUserToAddRows = (Me.Text = "Bill of Lading")

        brs = Grd.RowCount
        For cnt = 1 To brs
            str = Grd.Item(0, cnt - 1).Value
            Grd.Rows(cnt - 1).Cells(1).Value = str
            str = Grd.Item(4, cnt - 1).Value
            Grd.Rows(cnt - 1).Cells(5).Value = str
        Next
        Try
            Grd.Focus()
            Grd.CurrentCell = Grd(0, 0)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub GetDataCustomDoc(ByVal Grd As DataGridView, ByVal sno As String, ByVal stat As Boolean)
        Dim str As String
        Dim brs, cnt As Integer
        Dim dts, dtscb1 As DataTable
        Dim cbn1 As New DataGridViewComboBoxColumn

        'Customs Doc
        Grd.DataSource = Nothing
        Grd.Columns.Clear()

        'Grid selain combobox
        errMSg = "Tbl_Doc_Custom data view failed"
        strSQL = "select Doc_Code as DocCode,Doc_no as 'Doc. No',Doc_Dt as 'Date',Doc_Remark as 'Remark' from tbl_doc_custom " & _
                 "where shipment_no=" & sno
        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)

        If dts.Rows.Count = 0 Then
            strSQL = "" & _
            "SELECT DISTINCT b.DOC_CODE AS DocCode,  a.DOKNO AS 'Doc. No', DOKTG AS 'Date', '' AS 'Remark'" & _
            "FROM (" & _
            "SELECT * FROM `pib_tblpibdokall` WHERE car = '" & ajuNo.Text & "' " & _
            ") a " & _
            "INNER JOIN tbm_document b ON a.dokkd = b.refer_to" & _
            ""
            dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        End If
        Grd.DataSource = dts

        'Combo Box Document
        errMSg = ""
        strSQL = "select * from tbm_document where doc_code LIKE 'DC%' or doc_code=''"
        dtscb1 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn1
            .DataSource = dtscb1
            .DisplayMember = "DOC_NAME"
            .ValueMember = "DOC_CODE"
        End With
        Grd.Columns.Insert(1, cbn1)
        Grd.Columns(1).HeaderText = "Document"
        Grd.Columns(0).Visible = False

        If stat Then
            brs = Grd.RowCount
            For cnt = 1 To brs
                str = Grd.Item(0, cnt - 1).Value
                Grd.Rows(cnt - 1).Cells(1).Value = str
            Next
        End If

        Grd.Columns(1).Width = 200
        Grd.Columns(2).Width = 140
        Grd.Columns(3).Width = 70
        Grd.Columns(4).Width = 130
        Grd.ReadOnly = (Me.Text <> "Bill of Lading")
        Grd.AllowUserToAddRows = (Me.Text = "Bill of Lading")

        Try
            Grd.Focus()
            Grd.CurrentCell = Grd(0, 0)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetDataExpeditionDoc(ByVal Grd As DataGridView, ByVal sno As String, ByVal stat As Boolean)
        Dim str As String
        Dim brs, cnt As Integer
        Dim dts, dtscb1 As DataTable
        Dim cbn1 As New DataGridViewComboBoxColumn

        'Expedition Doc
        Grd.DataSource = Nothing
        Grd.Columns.Clear()

        'Grid selain combobox
        errMSg = "Tbl_Doc_Expedition data view failed"
        strSQL = "select Doc_Code as DocCode,Doc_no as 'Doc. No',Doc_Dt as 'Receive Date',Doc_Remark as 'Remark' from tbl_doc_expedition " & _
                 "where shipment_no=" & sno
        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        Grd.DataSource = dts

        'Combo Box Document
        errMSg = ""
        strSQL = "select * from tbm_document where doc_code LIKE 'DC%' or doc_code=''"
        dtscb1 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn1
            .DataSource = dtscb1
            .DisplayMember = "DOC_NAME"
            .ValueMember = "DOC_CODE"
        End With
        Grd.Columns.Insert(1, cbn1)
        Grd.Columns(1).HeaderText = "Document"
        Grd.Columns(0).Visible = False

        If stat Then
            brs = Grd.RowCount
            For cnt = 1 To brs
                str = Grd.Item(0, cnt - 1).Value
                Grd.Rows(cnt - 1).Cells(1).Value = str
            Next
        End If

        Grd.Columns(1).Width = 200
        'Grd.Columns(2).Width = 140
        Grd.Columns(2).Visible = False
        Grd.Columns(3).Width = 80
        Grd.Columns(4).Width = 275
        Grd.ReadOnly = (Me.Text <> "Bill of Lading")
        Grd.AllowUserToAddRows = (Me.Text = "Bill of Lading")

        Try
            Grd.Focus()
            Grd.CurrentCell = Grd(0, 0)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub GetDataPIBStatus(ByVal Grd As DataGridView, ByVal sno As String, ByVal stat As Boolean)
        Dim str As String
        Dim brs, cnt As Integer
        Dim dts, dtscb1 As DataTable
        Dim cbn1 As New DataGridViewComboBoxColumn


        errMSg = "Tbl_pib_history data view failed"
        strSQL = "select ord_no as No, status_Dt as Date from tbl_pib_history " & _
                 "where aju_no='" & sno & "' order by ord_no desc"
        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        Grd.DataSource = dts
        Grd.Columns(0).Visible = False
        Grd.Columns(1).Width = 120
        Try
            Grd.CurrentCell = Grd(1, 0)
        Catch ex As Exception
            Grd.DataSource = Nothing
        End Try
    End Sub

    Public Sub GetDataContainer(ByVal Grd As DataGridView, ByVal sno As String, ByVal stat As Boolean)
        Dim brs, cnt As Integer
        Dim str As String
        Dim dts, dtscb1 As DataTable
        Dim cbn1 As New DataGridViewComboBoxColumn

        'Container
        Grd.DataSource = Nothing
        Grd.Columns.Clear()

        'Grid selain combobox
        errMSg = "Tbl_Shippping_Cont data view failed"
        strSQL = "select Container_No as 'Container No.',Unit_Code as 'Size' from tbl_shipping_cont " & _
                 "where shipment_no=" & sno
        dts = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        Grd.DataSource = dts

        'Combo Box Document
        errMSg = "Tbm_unit data view failed"
        strSQL = "select * from tbm_unit"
        dtscb1 = DBQueryDataTable(strSQL, MyConn, "", errMSg, UserData)
        With cbn1
            .DataSource = dtscb1
            .DisplayMember = "UNIT_NAME"
            .ValueMember = "UNIT_CODE"
        End With
        Grd.Columns.Insert(2, cbn1)
        Grd.Columns(2).HeaderText = "Size"
        Grd.Columns(1).Visible = False
        Grd.Columns(0).Width = 140
        Grd.ReadOnly = (Me.Text <> "Bill of Lading")
        Grd.AllowUserToAddRows = (Me.Text = "Bill of Lading")

        brs = Grd.RowCount
        For cnt = 1 To brs
            str = Grd.Item(1, cnt - 1).Value
            Grd.Rows(cnt - 1).Cells(2).Value = str
        Next
        Try
            Grd.Focus()
            Grd.CurrentCell = Grd(0, 0)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCC.Click
        Dim f As New FrmCC(0, txtShipNo.Text, 0, Status2.Text, blno.Text)
        If DataOK("CC") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnCS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCS.Click
        Dim f As New FrmCS(0, txtShipNo.Text, 0, Status2.Text, blno.Text)
        If DataOK("CS") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnCL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCL.Click
        Dim f As New FrmCL(txtShipNo.Text, "", "", "", "", "", "CL")
        If DataOK("CL") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnBPIB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPIB.Click
        Dim f As New FrmBPIB(txtShipNo.Text, "", "", "", "", "")
        If DataOK("BP") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        PilihanDlg.Text = "Select Plant"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "SELECT PLANT_CODE as PlantCode, PLANT_NAME  as PlantName FROM tbm_plant " & _
                             "WHERE COMPANY_CODE IN (SELECT company_code FROM tbm_users_company WHERE user_ct='" & UserData.UserCT & "')"
        PilihanDlg.SQLFilter = "SELECT PLANT_CODE, PLANT_NAME FROM tbm_plant " & _
                               "WHERE PLANT_CODE LIKE 'FilterData1%' AND " & _
                               "PLANT_NAME LIKE 'FilterData2%' AND " & _
                               "COMPANY_CODE IN (SELECT company_code FROM tbm_users_company WHERE user_ct='" & UserData.UserCT & "')"
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then DestPlant.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub DestPlant_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DestPlant.TextChanged
        DestPlantName.Text = AmbilData("plant_name", "tbm_plant", "plant_code='" & DestPlant.Text & "'")
    End Sub
    Public Sub RefreshDisplay(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim str As String

        Edit = True
        Baru = False
        DisplayData()
        If TabPage9.Visible Then GetDataSuppDoc(grid2, txtShipNo.Text, Edit)
        suppl.ReadOnly = True
        If List.Items.Count > 0 Then
            btnSave.Enabled = True  'Add by Prie 09.11.2010
            List.SelectedIndex = 0
            List_Click(sender, e)
        End If
        If Me.Text = "Post Import Document" Then
            btnRIL.Enabled = True And (PunyaAkses("RL-C"))
			BtnRILQ.Enabled = True And (PunyaAkses("RL-C")) ' Add by AK 9/11/2010
            btnSI.Enabled = True And (PunyaAkses("SI-C"))
            btnMCI.Enabled = True And (PunyaAkses("MC-C"))
            btnCL.Enabled = True And (PunyaAkses("CL-C"))
            btnBR.Enabled = True And (PunyaAkses("BR-C"))
            btnDI.Enabled = True And (PunyaAkses("DI-C"))
            btnTT.Enabled = True And (PunyaAkses("TT-C"))
            btnCAD.Enabled = True And (PunyaAkses("CA-C"))
            btnPV.Enabled = True And (PunyaAkses("PV-C"))
            btnBPIB.Enabled = True And (PunyaAkses("BP-C"))
            btnVG.Enabled = True And (PunyaAkses("VG-C"))
            btnSSPCP.Enabled = True And (PunyaAkses("SP-C"))
        End If
        If Me.Text = "Funds & Finance" Then
            btnBPUM.Enabled = True And (PunyaAkses("DP-C"))
            btnBI.Enabled = True And (PunyaAkses("BI-C"))
            btnBPJUM.Enabled = True And (PunyaAkses("PP-C"))
            btnVP.Enabled = True And (PunyaAkses("VP-C"))
            btnCC.Enabled = True And (PunyaAkses("CC-C"))
            btnCS.Enabled = True And (PunyaAkses("CS-C"))
        End If
        If Me.Text = "Customs Clearance" Then
            btnBC.Enabled = True And (PunyaAkses("BC-C"))
            btnko.Enabled = True And (PunyaAkses("KO-C"))
            btnSK.Enabled = True And (PunyaAkses("SK-C"))
            btnPC.Enabled = True And (PunyaAkses("PC-C"))
            btnJC.Enabled = True And (PunyaAkses("JC-C"))
            btnBS.Enabled = True And (PunyaAkses("BS-C"))
            btnSZ.Enabled = True And (PunyaAkses("SZ-C"))
            btnSB.Enabled = True And (PunyaAkses("SB-C"))
			BtnNP.Enabled = True And (PunyaAkses("NP-C"))
        End If
    End Sub

    Private Sub ListBox1_DoubleClick1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim PO, InPO, POInBL, num, str2 As String
        Dim pos, pos2, pjg As Integer
        If ListBox1.SelectedIndex < 0 Or ListBox1.SelectedIndex < 0 Then Exit Sub
        chosen = Microsoft.VisualBasic.Left(ListBox1.Items(ListBox1.SelectedIndex).ToString, 45)
        Chosen2 = Trim(Microsoft.VisualBasic.Mid(ListBox1.Items(ListBox1.SelectedIndex).ToString, 49, 10))
        pos = InStr(chosen, "SK-")
        pos2 = InStr(chosen, "SP-")
        If (pos > 0 Or pos2 > 0) Then
            str2 = Microsoft.VisualBasic.Left(chosen, 5)
        Else
            str2 = Microsoft.VisualBasic.Left(chosen, 2)
        End If

        Select Case str2
            Case "RI"
                PO = GetPO()
                pos = InStr(chosen, "#")
                pjg = Len(chosen) - pos
                num = Mid(chosen, pos + 1, pjg)
                If PeriksaAkses("RL-L", "RIL") Then
                    Dim f As New FrmBLRIL(txtShipNo.Text, PO, num, blno.Text, If(ListBox1.Items.Count > 0, True, False))
                    f.ShowDialog()
                End If
			Case "RQ"
                PO = GetPO()
                pos = InStr(chosen, "#")
                pjg = Len(chosen) - pos
                num = Mid(chosen, pos + 1, pjg)
                ' Add by AK 9/11/2010
                If PeriksaAkses("RL-L", "RIL") Then
                    Dim f As New FrmBLRILQ(txtShipNo.Text, PO, num, blno.Text, If(ListBox1.Items.Count > 0, True, False))
                    f.ShowDialog()
                End If
            Case "SI"
                POInBL = txtShipNo.Text & ";" & GetPOInBL()
                chosen = Trim(chosen)
                If PeriksaAkses("SI-L", "SHIN") Then
                    Dim f As New FrmSHIN(POInBL, chosen)
                    f.ShowDialog()
                End If
            Case "MC"
                pos = InStr(chosen, "#")
                pjg = Len(chosen) - pos
                num = Mid(chosen, pos + 1, pjg)
                If PeriksaAkses("MC-L", "MCI") Then
                    Dim f As New FrmMCI(txtShipNo.Text, blno.Text, num)
                    f.ShowDialog()
                End If
            Case "BR"
                If PeriksaAkses("BR-L", "BR") Then
                    Dim f As New FrmBR(txtShipNo.Text, "", "", "0", "0", chosen)
                    f.ShowDialog()
                End If
            Case "DI"
                If PeriksaAkses("DI-L", "DI") Then
                    Dim f As New FrmDI(txtShipNo.Text, "", "", "0", "0", chosen)
                    f.ShowDialog()
                End If
            Case "PV"
                If Me.Text = "Post Import Document" Then
                    If PeriksaAkses("PV-L", "PV") Then
                        Dim f As New FrmPV(txtShipNo.Text, "", "", "0", "0", chosen)
                        f.ShowDialog()
                    End If
                End If
                If Me.Text = "Funds & Finance" Then
                    pos = InStr(chosen, "#")
                    pjg = Len(chosen) - pos
                    num = Mid(chosen, pos + 1, pjg)
                    If PeriksaAkses("VP-L", "PV") Then
                        Dim f As New FrmVP(num, txtShipNo.Text, num, Status2.Text, blno.Text)
                        f.ShowDialog()
                    End If
                End If
            Case "VG"
                If PeriksaAkses("VG-L", "VG") Then
                    Dim f As New FrmVG(txtShipNo.Text, "", "", "0", "0", chosen)
                    f.ShowDialog()
                End If
            Case "SS"
                If PeriksaAkses("SP-L", "SSPCP") Then
                    Dim f As New FrmSSPCP(txtShipNo.Text, "", "", "", "", chosen)
                    f.ShowDialog()
                End If
            Case "TT"
                If PeriksaAkses("TT-L", "B-TT") Then
                    Dim f As New FrmTT(txtShipNo.Text, "", "", "0", "0", chosen, "TT")
                    f.ShowDialog()
                End If
            Case "CA"
                If PeriksaAkses("CA-L", "B-CAD") Then
                    Dim f As New FrmTT(txtShipNo.Text, "", "", "0", "0", chosen, "CA")
                    f.ShowDialog()
                End If
            Case "BI"
                'BI
                If (Me.Text = "Funds & Finance") And (Microsoft.VisualBasic.Left(chosen, 2) = "BI") And PeriksaAkses("BI-L", "BI") Then
                    pos = InStr(chosen, "#")
                    pjg = Len(chosen) - pos
                    num = Mid(chosen, pos + 1, pjg)
                    Dim f As New FrmBI(num, txtShipNo.Text, num, Status2.Text, blno.Text)
                    f.ShowDialog()
                End If
            Case "BP"
                'B-PIB
                If (Me.Text = "Post Import Document") And (Microsoft.VisualBasic.Left(chosen, 3) = "BP ") And PeriksaAkses("BP-L", "B-PIB") Then
                    Dim f As New FrmBPIB(txtShipNo.Text, "", "", "0", "0", chosen)
                    Dim EE As System.EventArgs
                    f.ShowDialog()
                End If
                'BPUM
                If (Me.Text = "Funds & Finance") And (Microsoft.VisualBasic.Left(chosen, 4) = "BPUM") And PeriksaAkses("DP-L", "BPUM") Then
                    pos = InStr(chosen, "#")
                    pjg = Len(chosen) - pos
                    num = Mid(chosen, pos + 1, pjg)
                    Dim f As New FrmBPUM(num, txtShipNo.Text, num, Status2.Text, blno.Text)
                    f.ShowDialog()
                End If
                'BPJUM
                If (Me.Text = "Funds & Finance") And (Microsoft.VisualBasic.Left(chosen, 5) = "BPJUM") And PeriksaAkses("PP-L", "BPJUM") Then
                    pos = InStr(chosen, "#")
                    pjg = Len(chosen) - pos
                    num = Mid(chosen, pos + 1, pjg)
                    Dim f As New FrmBPJUM(num, txtShipNo.Text, num, Status2.Text, blno.Text)
                    f.ShowDialog()
                End If
            Case "CC"
                pos = InStr(chosen, "#")
                pjg = Len(chosen) - pos
                num = Mid(chosen, pos + 1, pjg)
                If PeriksaAkses("CC-L", "CC") Then
                    Dim f As New FrmCC(num, txtShipNo.Text, num, Status2.Text, blno.Text)
                    f.ShowDialog()
                End If
            Case "CS"
                pos = InStr(chosen, "#")
                pjg = Len(chosen) - pos
                num = Mid(chosen, pos + 1, pjg)
                If PeriksaAkses("CS-L", "CS") Then
                    Dim f As New FrmCS(num, txtShipNo.Text, num, Status2.Text, blno.Text)
                    f.ShowDialog()
                End If
            Case "CL"
                If PeriksaAkses("CL-L", "CL") Then
                    Dim f As New FrmCL(txtShipNo.Text, "", "", "0", "0", chosen, "CL")
                    f.ShowDialog()
                End If
            Case "SK-BC"
                If PeriksaAkses("BC-L", "BC") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "BC")
                    f.ShowDialog()
                End If
            Case "SK-DO"
                If PeriksaAkses("KO-L", "KO") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "KO")
                    f.ShowDialog()
                End If
            Case "SK-KR"
                If PeriksaAkses("SK-L", "SK") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "SK")
                    f.ShowDialog()
                End If
            Case "SK-PC"
                If PeriksaAkses("PC-L", "PC") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "PC")
                    f.ShowDialog()
                End If
            Case "SK-JC"
                If PeriksaAkses("JC-L", "JC") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "JC")
                    f.ShowDialog()
                End If
            Case "SP-BS"
                If PeriksaAkses("BS-L", "BS") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "BS")
                    f.ShowDialog()
                End If
            Case "SP-I "
                If PeriksaAkses("SZ-L", "SZ") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "SZ")
                    f.ShowDialog()
                End If
            Case "SP-B "
                If PeriksaAkses("SB-L", "SB") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "SB")
                    f.ShowDialog()
                End If
			Case "DN"
                If PeriksaAkses("NP-L", "NP") Then
                    Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", chosen, "NP")
                    f.ShowDialog()
                End If
        End Select
        DisplayDocument()
    End Sub

    Private Sub dtValue_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtValue.ValueChanged
        Try
            If dtValue.Checked = True And supplname.Text <> "" Then
                bankname.Text = AmbilData("bank_name", "tbm_supplier", "supplier_code='" & suppl.Text & "'")
                ''accno.Text = AmbilData("account_no", "tbm_supplier", "supplier_code='" & suppl.Text & "'")
            End If
            If dtValue.Checked = False Or supplname.Text = "" Then
                bankname.Text = ""
                bankname.Text = ""
                ''accno.Text = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateInvoice()
        Dim brs As Integer
        Dim angka, angka1, angka2 As Decimal

        brs = grid.CurrentCell.RowIndex
        angka = GridInv.Item(3, brs).Value
        angka1 = grid.Item(8, brs).Value
        angka2 = grid.Item(13, brs).Value
        GridInv.Item(2, brs).Value = angka1 * angka2
        GridInv.Item(3, brs).Value = angka1 * angka2
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearAll.Click
        Dim ms, sel, msg As String
        Dim pil As MsgBoxResult

        If Edit Then
            msg = "All PO detail will be deleted PERMANENTLY!!!" & Chr(13) & Chr(10) & "Are you sure to delete it?"
            pil = MsgBox(msg, MsgBoxStyle.YesNo, "Confirmation")
            If pil = vbNo Then Exit Sub
        End If

        If DelAllPODetilAndUpdateBea() Then ClearAllPO()
    End Sub
    Private Function DelPODetilAndUpdateBea(ByVal idx As SByte) As Boolean
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim hasil As Boolean
        Dim a, ListIdx As SByte
        Dim TotBea, TotPPH, TotPPN, TotPIUD, newkurs As Decimal

        'display di screen belum di update
        'klo delete sukses baru display di screen di update
        'tetapi pada waktu delete nilai bea harus yg uptodate

        ReDim Array4(Array3.Length - 1)
        Array.Copy(Array3, Array4, Array3.Length)
        newkurs = GetKursPajak(idx)
        HitungUlangBea(idx, newkurs)

        ListIdx = List.Items.Count - 1
        TotBea = 0
        TotPPH = 0
        TotPPN = 0
        TotPIUD = 0
        For a = 0 To ListIdx
            If a <> idx Then
                TotBea = TotBea + Array3(a).Bea
                TotPPN = TotPPN + Array3(a).PPN
                TotPPH = TotPPH + Array3(a).PPH
                'piud di hitung per shipment
                'TotPIUD = TotPIUD + Array3(a).PIUD
                TotPIUD = Array3(a).PIUD
            End If
        Next
        TotBea = Math.Floor(TotBea)
        TotPPN = Math.Floor(TotPPN)
        TotPPH = Math.Floor(TotPPH)
        TotPIUD = Math.Floor(TotPIUD)

        strSQL = "Run Stored Procedure DelBL2 (" & txtShipNo.Text & "," & GetNum2(TotBea) & "," & GetNum2(TotPPN) & "," & GetNum2(TotPPH) & "," & GetNum2(TotPIUD) & "," & GetNum2(newkurs) & "," & UserData.UserCT & ")"
        With MyComm
            .CommandText = "DelBL2"
            .CommandType = CommandType.StoredProcedure
            With .Parameters
                .Clear()
                .AddWithValue("UpdateShipNo", txtShipNo.Text)
                .AddWithValue("PONo", Trim(List.Items(idx).ToString))
                .AddWithValue("BeaMsk", TotBea)
                .AddWithValue("VAT", TotPPN)
                .AddWithValue("Pph21", TotPPH)
                .AddWithValue("PIUD", TotPIUD)
                .AddWithValue("KursPajak", newkurs)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("AuditStr", strSQL)
                .AddWithValue("Hasil", hasil)
            End With
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
            Catch ex As Exception
            End Try
        End With
        DelPODetilAndUpdateBea = hasil
        If hasil = False Then MsgBox("Failed delete PO " & Trim(List.Items(idx).ToString))
    End Function
    Private Function DelAllPODetilAndUpdateBea() As Boolean
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim hasil As Boolean
        Dim TotBea, TotPPH, TotPPN, TotPIUD, newkurs As Decimal

        TotBea = 0
        TotPPN = 0
        TotPPH = 0
        TotPIUD = 0
        strSQL = "Run Stored Procedure DelBL2 (" & txtShipNo.Text & "," & GetNum2(TotBea) & "," & GetNum2(TotPPN) & "," & GetNum2(TotPPH) & "," & GetNum2(TotPIUD) & "," & GetNum2(newkurs) & "," & UserData.UserCT & ")"
        With MyComm
            .CommandText = "DelBL2"
            .CommandType = CommandType.StoredProcedure
            With .Parameters
                .Clear()
                .AddWithValue("UpdateShipNo", txtShipNo.Text)
                .AddWithValue("PONo", "")
                .AddWithValue("BeaMsk", TotBea)
                .AddWithValue("VAT", TotPPN)
                .AddWithValue("Pph21", TotPPH)
                .AddWithValue("PIUD", TotPIUD)
                .AddWithValue("KursPajak", newkurs)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("AuditStr", strSQL)
                .AddWithValue("Hasil", hasil)
            End With
            Try
                .ExecuteNonQuery()
                hasil = .Parameters("hasil").Value
            Catch ex As Exception
            End Try
        End With
        DelAllPODetilAndUpdateBea = hasil


        If hasil = False Then
            MsgBox("Failed delete All PO")
        Else
            btnSave.Enabled = False 'Add by prie 09.11.2010
        End If
    End Function
    Private Function GetPO() As String
        Dim cnt, jml As SByte
        Dim temp As String
        GetPO = ""

        jml = List.Items.Count - 1
        For cnt = 0 To jml
            temp = Trim(List.Items(cnt).ToString)
            temp = Microsoft.VisualBasic.Mid(temp & "                    ", 1, 20)
            GetPO = GetPO & temp & ";"
        Next
    End Function
    Private Function GetInPO() As String
        Dim cnt, jml As SByte
        Dim temp As String
        GetInPO = "("

        jml = List.Items.Count - 1
        For cnt = 0 To jml
            temp = Trim(List.Items(cnt).ToString)
            If GetInPO <> "(" Then GetInPO = GetInPO & ","
            GetInPO = GetInPO & "'" & temp & "'"
        Next
        GetInPO = GetInPO & ")"
    End Function
    Private Function GetPOInBL() As String
        Dim cnt, jml As SByte
        Dim temp As String
        GetPOInBL = ""

        jml = List.Items.Count - 1
        For cnt = 0 To jml
            temp = Trim(List.Items(cnt).ToString)
            If GetPOInBL <> "" Then GetPOInBL = GetPOInBL & ","
            GetPOInBL = GetPOInBL & temp
        Next
    End Function
    Private Function CekExistingPORIL(ByVal InPO As String) As Boolean
        Dim temp As String

        'temp = AmbilData("doc_no", "tbl_docimpr as a " _
        '                           & "inner join tbl_ril_Detail as b on a.po_no=b.po_no and a.doc_no=b.ril_no " _
        '                           & "inner join tbl_ril as c on b.po_no=c.po_no and b.ril_no=c.ril_no ", _
        '                           "a.po_no in" & InPO & " and a.doc_type='RL' and c.status<>'Rejected' and b.shipment_no=" & txtShipNo.Text)

        temp = AmbilData("po_no", "tbl_ril as a", _
                                   "a.po_no in" & InPO & " and a.status<>'Rejected' and shipment_no is null")
        CekExistingPORIL = (Trim(temp) <> "")
    End Function
    Private Function CekExistingPOSI(ByVal InPO As String) As Boolean
        Dim temp As String

        temp = AmbilData("po_no", "tbl_si as a", _
                                   "a.po_no in" & InPO & " and a.status<>'Rejected' and shipment_no is null")
        CekExistingPOSI = (Trim(temp) <> "")
    End Function
    Private Function CekExistingBLRIL(ByVal InPO As String) As Boolean
        Dim temp As String

        temp = AmbilData("po_no", "tbl_ril as a", _
                                   "a.po_no in" & InPO & " and a.status<>'Rejected' and shipment_no = '" & txtShipNo.Text & "'")
        CekExistingBLRIL = (Trim(temp) <> "")
    End Function
    Private Function CekExistingBLSI(ByVal InPO As String) As Boolean
        Dim temp As String

        temp = AmbilData("po_no", "tbl_si as a", _
                                   "a.po_no in" & InPO & " and a.status<>'Rejected' and shipment_no is not null")
        CekExistingBLSI = (Trim(temp) <> "")
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

    Public Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Public Function CostSlipStatus(ByVal kd As Integer) As Boolean
        Dim SQLStr As String

        SQLStr = "SELECT * FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status <> 'Rejected' AND shipment_no=" & kd & ""
        CostSlipStatus = (DataExist(SQLStr) = True)
    End Function



    Public Function PeriksaAkses(ByVal kdAkses1 As String, ByVal tombol As String) As Boolean
        If Not PunyaAkses(kdAkses1) Then
            MsgBox("You are not authorized to view " & tombol)
            PeriksaAkses = False
            Exit Function
        End If
        PeriksaAkses = True
    End Function

    Private Sub BeaMasuk_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles BeaMasuk.Enter
        BeaMasuk.SelectAll()
    End Sub

    Private Sub BeaMasuk_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles BeaMasuk.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then BeaMasuk_Leave(sender, ee)
    End Sub

    Private Sub BeaMasuk_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BeaMasuk.Leave
        Try
            BeaMasuk.Text = FormatNumber(BeaMasuk.Text, 0, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BeaMasuk_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles BeaMasuk.Validating
        If IsNumeric(BeaMasuk.Text) = False Then
            MsgBox("Invalid Bea Masuk, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub vat_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles vat.Enter
        vat.SelectAll()
    End Sub

    Private Sub vat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles vat.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then vat_Leave(sender, ee)
    End Sub

    Private Sub vat_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles vat.Leave
        Try
            vat.Text = FormatNumber(vat.Text, 0, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub vat_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles vat.Validating
        If IsNumeric(vat.Text) = False Then
            MsgBox("Invalid VAT, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub pph_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles pph.Enter
        pph.SelectAll()
    End Sub

    Private Sub pph_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles pph.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then pph_Leave(sender, ee)
    End Sub

    Private Sub pph_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles pph.Leave
        Try
            pph.Text = FormatNumber(pph.Text, 0, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub pph_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles pph.Validating
        If IsNumeric(pph.Text) = False Then
            MsgBox("Invalid PPh, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub piud_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles piud.Enter
        piud.SelectAll()
    End Sub

    Private Sub piud_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles piud.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then piud_Leave(sender, ee)
    End Sub

    Private Sub piud_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles piud.Leave
        Try
            piud.Text = FormatNumber(piud.Text, 0, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub finalty_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles finalty.Enter
        finalty.SelectAll()
    End Sub

    Private Sub finalty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles finalty.KeyDown
        Dim ee As System.EventArgs
        If e.KeyValue = 13 Then finalty_Leave(sender, ee)
    End Sub

    Private Sub finalty_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles finalty.Leave
        Try
            finalty.Text = FormatNumber(finalty.Text, 2, , , TriState.True)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub piud_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles piud.Validating
        If IsNumeric(piud.Text) = False Then
            MsgBox("Invalid PIUD, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub finalty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles finalty.TextChanged
        If Display Then Exit Sub
        HitungUlangBea(-1, Kurs)
        UpdateDisplayBea()
    End Sub

    Private Sub finalty_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles finalty.Validating
        If IsNumeric(finalty.Text) = False Then
            MsgBox("Invalid Finalty, input numeric value")
            e.Cancel = True
            DataError = True
        End If
    End Sub

    Private Sub btnRIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRIL.Click
        Dim PO, InPO As String
        Dim dahAda As Boolean

        'Klo sudah ada PO RIL tidak boleh buat BL RIL dan sebaliknya
        PO = GetPO()
        InPO = GetInPO()

        dahAda = CekExistingBLRIL(InPO)
        If dahAda = False Then
            dahAda = CekExistingPORIL(InPO)
            If dahAda = False Then
                Dim f As New FrmBLRIL(txtShipNo.Text, PO, "", blno.Text, If(ListBox1.Items.Count > 0, True, False))
                f.ShowDialog()
                DisplayDocument()
            Else
                MsgBox("BL " & blno.Text & " already has PO RIL," & Chr(13) & Chr(10) & "Can't create BL RIL")
            End If
        Else
            MsgBox("RIL has been created")
        End If
    End Sub

    Private Sub btnBPUM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPUM.Click
        Dim f As New FrmBPUM(0, txtShipNo.Text, 0, Status2.Text, blno.Text)
        If DataOK("BPUM") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnBPJUM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBPJUM.Click
        Dim f As New FrmBPJUM(0, txtShipNo.Text, 0, Status2.Text, blno.Text)
        If DataOK("BPJUM") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub
    Private Sub btnTT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTT.Click
        Dim f As New FrmTT(txtShipNo.Text, "", "", "", "", "", "TT")
        f.ShowDialog()
        DisplayDocument()
    End Sub

    Private Sub btnCAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCAD.Click
        Dim f As New FrmTT(txtShipNo.Text, "", "", "", "", "", "CAD")
        f.ShowDialog()
        DisplayDocument()
    End Sub

    Private Sub btnMCI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMCI.Click
        Dim f As New FrmMCI(txtShipNo.Text, blno.Text, "")
        If DataOK("MC") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnko_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnko.Click
        OpenFrm("KO")
    End Sub

    Private Sub btnSK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSK.Click
        OpenFrm("SK")
    End Sub

    Private Sub btnJC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJC.Click
        OpenFrm("JC")
    End Sub

    Private Sub btnBC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBC.Click
        OpenFrm("BC")
    End Sub

    Private Sub btnPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPC.Click
        OpenFrm("PC")
    End Sub

    Private Sub btnBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBS.Click
        OpenFrm("BS")
    End Sub

	Private Sub btnNP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNP.Click
        OpenFrm("NP")
    End Sub

    Private Sub OpenFrm(ByVal jns As String)
        Dim f As New FrmKOSK(txtShipNo.Text, "", "", "", "", "", jns)
        If DataOK(jns) = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Function f_GetAuthorized(ByVal shipno As String) As Boolean

        Dim bAuthorized As Boolean = False

        strSQL = "SELECT bl_no FROM tbl_shipping WHERE shipment_no='" & shipno & "' " & _
                 "AND plant_code IN (SELECT plant_code FROM tbm_users_company t1, tbm_plant t2 WHERE t2.company_code=t1.company_code AND user_ct='" & UserData.UserCT & "') "

        errMSg = "Failed when read Bill of Lading"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            If MyReader.HasRows = True Then
                bAuthorized = True
            End If
        End If
        CloseMyReader(MyReader, UserData)

        If Not bAuthorized Then
            MsgBox("You do not have permission to access BL from this company!", vbInformation, "Warning")
        End If

        Return bAuthorized
    End Function

    Private Sub btnCalculate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculate.Click
        Dim brsPO As SByte

        brsPO = List.SelectedIndex
        HitungBea(brsPO, -1)
    End Sub

    Private Function GetRounded(ByVal strnum As String, ByVal stat As String) As String
        Dim last3dig As Decimal
        Dim xPos As Integer

        If strnum = 0 Then
            GetRounded = 0
        Else
            'rounded ribuan dibulatkan ke atas
            'contoh 74.575.559 menjadi 74.576.000
            'contoh 74.575.359 menjadi 74.576.000
            'xPos = InStr(strnum, ".")
            'If xPos > 0 Then
            'strnum = Mid(strnum, 1, xPos - 1)
            'End If
            strnum = FormatNumber(strnum, 0)
            If stat = "1" Then   'rounded
                If Len(strnum) <= 3 Then
                    last3dig = strnum
                Else
                    last3dig = Microsoft.VisualBasic.Right(Trim(strnum), 3)
                End If
                strnum = strnum - CDec(last3dig)
                If CDec(last3dig) > 0 Then
                    strnum += 1000
                End If
            End If
            GetRounded = FormatNumber(strnum, 0)
        End If
    End Function

    Private Sub dtEstDelivery_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtEstDelivery.ValueChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub
    Function GetData(ByVal strSQL As String) As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = strSQL
        MyComm.CommandType = CommandType.Text
        Try
            GetData = MyComm.ExecuteScalar()
        Catch ex As Exception
            GetData = Nothing
        End Try
        MyComm.Dispose()
    End Function

    Private Sub btnSI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSI.Click
        Dim PO, InPO, POInBL As String
        Dim dahAda As Boolean

        'Klo sudah ada PO SI tidak boleh buat BL SI dan sebaliknya
        PO = GetPO()
        InPO = GetInPO()
        dahAda = CekExistingBLSI(InPO)
        ''di tutup untuk SI Deptan
        ''If dahAda = False Then
        dahAda = CekExistingPOSI(InPO)

        If dahAda = False Then
            POInBL = txtShipNo.Text & ";" & GetPOInBL()
            Dim f As New FrmSHIN(POInBL, "")
            f.ShowDialog()
            DisplayDocument()
        Else
            'MsgBox("BL " & blno.Text & " already has PO SI," & Chr(13) & Chr(10) & "Can't create BL SI")
            'Pengganti karena di mungkinkan membuat SI setelah SI PO untuk keperluan lampiran ke Deptan. Bedanya Quantity di SI ini sesuai actual.
            POInBL = txtShipNo.Text & ";" & GetPOInBL()
            Dim f As New FrmSHIN(POInBL, "")
            f.ShowDialog()
            DisplayDocument()
        End If
        ''Else
        ''MsgBox("Shipment Instruction has been created")
        ''End If
    End Sub
    Private Sub grid2_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid2.CellDoubleClick
        Dim ls_remark As String

        If grid2.Columns(e.ColumnIndex).Name = "Remark" Then
            If grid2.Rows(e.RowIndex).Cells("Remark").Value.ToString <> "" Then
                ls_remark = grid2.Rows(e.RowIndex).Cells("HistRemark").Value.ToString
                MsgBox(ls_remark, MsgBoxStyle.Information, "History of Supplier Doc. Status")
            End If
        End If
    End Sub

    Private Sub dtEstClearance_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtEstClearance.ValueChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub

    Private Sub dtClearance_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtClearance.ValueChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub

    Private Sub free_ext_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles free_ext.TextChanged
        GetStartDemurrage()
        GetSelisihTgl()
    End Sub

    Private Sub btnSZ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSZ.Click
        OpenFrm("SZ")
    End Sub

    Private Sub btnSB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSB.Click
        OpenFrm("SB")
    End Sub


    Private Sub btnCL2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New FrmCL2(txtShipNo.Text, "", "", "", "", "", "CL")
        If DataOK("CL") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub btnExpedition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpedition.Click
        PilihanDlg.Text = "Select Company Code"
        PilihanDlg.LblKey1.Text = "Company Code"
        PilihanDlg.LblKey2.Text = "Company Name"

        PilihanDlg.SQLGrid = "SELECT company_code as ExpeditionCompanyCode, company_name as ExpeditionCompanyName, Title as Title, AUTHORIZE_PERSON as AuthorizedPerson FROM tbm_expedition order by company_code "
        PilihanDlg.SQLFilter = "SELECT company_code as ExpeditionCompanyCode, company_name as ExpeditionCompanyName, Title as Title, AUTHORIZE_PERSON as AuthorizedPerson FROM tbm_expedition " & _
                               "WHERE company_code LIKE 'FilterData1%' and company_name LIKE 'FilterData2%' order by company_code "

        PilihanDlg.Tables = "tbm_expedition"

        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Expedition.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            ExpeditionName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub Expedition_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Expedition.TextChanged
        If Expedition.Text <> "" Then
            ExpeditionName.Text = AmbilData("company_name", "tbm_expedition", "company_code='" & Expedition.Text & "'")
            If ExpeditionName.Text = "" Then
                MsgBox("Expedition not found")
                Expedition.Text = ""
                ExpeditionName.Text = ""
                Expedition.Focus()
            End If
        End If
    End Sub

    Private Sub btnBI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBI.Click
        Dim f As New FrmBI(0, txtShipNo.Text, 0, Status2.Text, blno.Text)
        If DataOK("BI") = True Then
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Sub List_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles List.SelectedIndexChanged

    End Sub

    Private Sub grid_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid.CellContentClick

    End Sub

    Private Sub BtnRILQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRILQ.Click
        Dim PO, InPO As String
        Dim dahAda As Boolean

        'Klo sudah ada PO RIL tidak boleh buat BL RIL dan sebaliknya
        PO = GetPO()
        InPO = GetInPO()

        dahAda = CekExistingBLRIL(InPO)
        If dahAda = False Then
            dahAda = CekExistingPORIL(InPO)
            If dahAda = False Then
                Dim f As New FrmBLRILQ(txtShipNo.Text, PO, "", blno.Text, If(ListBox1.Items.Count > 0, True, False))
                f.ShowDialog()
                DisplayDocument()
            Else
                MsgBox("BL " & blno.Text & " already has PO RIL," & Chr(13) & Chr(10) & "Can't create BL RILQ")
            End If
        Else
            MsgBox("BL RIL has been created")
        End If
    End Sub

    Private Sub GridPIB_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridPIB.CellClick
        Dim brs As Integer
        Dim dts As DataTable
        Dim ord As String

        desc.Clear()
        If GridPIB.DataSource Is Nothing Then Exit Sub
        brs = GridPIB.CurrentCell.RowIndex
        ord = GridPIB.Item(0, brs).Value
        tglPIB.Text = GridPIB.Item(1, brs).Value
        desc.Text = AmbilData("status_Description as Description", "Tbl_pib_history", "aju_no='" & ajuNo.Text & "' and ord_no=" & ord)
    End Sub

    Private Sub cbShipTerm_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbOB.SelectedIndexChanged
        If cbOB.SelectedIndex = 1 Then
            dtOB.Enabled = True
            'If cbOB.SelectedIndex = 2 Then
            'ChkTahanOB.Checked = True
            'Else
            'ChkTahanOB.Checked = False
            'End If
        Else
            dtOB.Checked = False
            dtOB.Enabled = False
        End If
    End Sub

    Private Sub btnPro_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPro.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'SC-P' "
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT, tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'SC-P' " & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name like 'FilterData2%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtProposedBy.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(2).Value.ToString
            CTPro.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub CTPro_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CTPro.TextChanged
        If CTPro.Text <> "" Then
            txtProposedBy.Text = AmbilData("name", "tbm_users", "user_ct=" & CTPro.Text)
        Else
            txtProposedBy.Text = ""
        End If
    End Sub

    Private Sub grid2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grid2.CellContentClick

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub TabPage8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage8.Click

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub desc_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles desc.TextChanged

    End Sub

    Private Sub ajuNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ajuNo.TextChanged

    End Sub
End Class