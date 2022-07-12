''' <summary>
''' Title                         : Customs Facilities ==> menjadi Customs Clearance
''' Form                          : FrmCustom
''' Form linked                   : FrmBL (get Psot Import Document data)
''' Table Used                    :
''' Stored Procedure Used (MySQL) : 
''' Created By                    : Yanti 12.02.2009
''' Catatan                       : Jangan create file excel memakai sintaks WORKBOOK or WORKSHEET karena bila user memakai excel versi lain, file excel tidak bisa dibuka
'''                                 Create file teks (isinya code HTML), kemudian disimpan dengan extention xml, jadi file bisa dibuka di excel versi apa aja
''' 
''' </summary>
''' <remarks></remarks>


Imports FrmBL
Public Class FrmCustom
    Dim errMsg, strSQL, Arr(0) As String
    Dim DataError As Boolean
    Dim PilihanDlg As New DlgPilihan
    Dim MyReader As MySqlDataReader
    Dim ShipNo As Integer
    Private ServerDecimal, ServerSeparator, ClientDecimalSeparator, ClientGroupSeparator As String
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Function CekData() As Boolean
        CekData = True
        If recdt1.Value > recdt2.Value Then
            MsgBox("Second Receive Doc. Date should be >= first Receive Doc. Date", MsgBoxStyle.Critical, "Warning")
            recdt1.Focus()
            CekData = False
            Exit Function
        End If
        If pibdt1.Visible Then
            If pibdt1.Value > pibdt2.Value Then
                MsgBox("Second PIB Date should be >= first PIB Date", MsgBoxStyle.Critical, "Warning")
                pibdt1.Focus()
                CekData = False
                Exit Function
            End If
        End If
        If suppname.Text = "" And suppcode.Text <> "" Then
            MsgBox("Supplier code does not exist! ", MsgBoxStyle.Critical, "Warning")
            suppcode.Focus()
            CekData = False
            Exit Function
        End If
    End Function

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim file As System.IO.StreamWriter
        Dim idx, a As Integer
        Dim txt, FileName As String
        Dim DT As New System.Data.DataTable

        If GridSearch.RowCount = 0 Then
            MsgBox("Data not found")
            Exit Sub
        End If
        TabControl1.SelectedIndex = 1
        DT = grid3.DataSource
        If (DT Is Nothing) Or (DT.Rows.Count = 0) Then
            TabControl1.SelectedIndex = 1
            grid3.Focus()
            MsgBox("Customs Doc detail data not filled")
            Exit Sub
        End If
        TabControl1.SelectedIndex = 2
        DT = grid4.DataSource
        If (DT Is Nothing) Or (DT.Rows.Count = 0) Then
            TabControl1.SelectedIndex = 2
            grid4.Focus()
            MsgBox("Container detail data not filled")
            Exit Sub
        End If
        FileName = GetFileName()
        If FileName = "" Then Exit Sub
        ReDim Arr(0)
        SaveArrayXML()
        idx = Arr.Length - 1
        Try
            Try
                My.Computer.FileSystem.DeleteFile(FileName)
            Catch ex As Exception
            End Try
            file = My.Computer.FileSystem.OpenTextFileWriter(FileName, True)
            With file
                For a = 1 To idx
                    .WriteLine(Arr(a))
                Next
                .Close()
            End With
            txt = "File Excel Created" & Chr(13) & Chr(10) & "File name : " & Chr(34) & FileName & Chr(34)
            MsgBox(txt)
            If ajuNo.ReadOnly = False Then
                UpdateAJU()
                ajuNo.ReadOnly = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub UpdateAJU()
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        strSQL = "update tbl_Shipping set aju_no='" & ajuNo.Text & "' where shipment_no=" & ShipNo
        errMsg = "Failed when update AJU No."

        Try
            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", strSQL)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = False Then MsgBox(errMsg, MsgBoxStyle.Information, "Update AJU No.")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub SaveArr(ByVal teks As String)
        Dim idx As Integer

        idx = Arr.Length
        ReDim Preserve Arr(idx)
        Arr(idx) = teks
    End Sub
    Private Sub SaveHeaderXML()
        SaveArr("<?xml version=" & Chr(34) & "1.0" & Chr(34) & "?>")
        SaveArr("<?mso-application progid=" & Chr(34) & "Excel.Sheet" & Chr(34) & "?>")
        SaveArr("<Workbook")
        SaveArr("xmlns:x=" & Chr(34) & "urn:schemas-microsoft-com:office:excel" & Chr(34))
        SaveArr("xmlns=" & Chr(34) & "urn:schemas-microsoft-com:office:spreadsheet" & Chr(34))
        SaveArr("xmlns:ss=" & Chr(34) & "urn:schemas-microsoft-com:office:spreadsheet" & Chr(34) & ">")
        SaveArr("<Styles>")
        SaveArr("<Style ss:ID=" & Chr(34) & "Default" & Chr(34) & " ss:Name=" & Chr(34) & "Normal" & Chr(34) & ">")
        SaveArr("<Alignment ss:Vertical=" & Chr(34) & "Bottom" & Chr(34) & "/>")
        SaveArr("<Borders/>")
        SaveArr("<Font/>")
        SaveArr("<Interior/>")
        SaveArr("<NumberFormat/>")
        SaveArr("<Protection/>")
        SaveArr("</Style>")
        SaveArr("<Style ss:ID=" & Chr(34) & "s27" & Chr(34) & ">")
        SaveArr("<Font x:Family=" & Chr(34) & "Swiss" & Chr(34) & " ss:Color=" & Chr(34) & "#0000FF" & Chr(34) & " ss:Bold=" & Chr(34) & "1" & Chr(34) & "/>")
        SaveArr("</Style>")
        SaveArr("<Style ss:ID=" & Chr(34) & "s21" & Chr(34) & ">")
        SaveArr("<NumberFormat ss:Format=" & Chr(34) & "yyyy\-mm\-dd" & Chr(34) & "/>")
        SaveArr("</Style>")
        SaveArr("<Style ss:ID=" & Chr(34) & "s22" & Chr(34) & ">")
        SaveArr("<NumberFormat ss:Format=" & Chr(34) & "yyyy\-mm\-dd\ hh:mm:ss" & Chr(34) & "/>")
        SaveArr("</Style>")
        SaveArr("<Style ss:ID=" & Chr(34) & "s23" & Chr(34) & ">")
        SaveArr("<NumberFormat ss:Format=" & Chr(34) & "hh:mm:ss" & Chr(34) & "/>")
        SaveArr("</Style>")
        SaveArr("</Styles>")
    End Sub
    Private Sub SaveDetilXML(ByVal Name As String)
        SaveArr("<Worksheet ss:Name=" & Chr(34) & Name & Chr(34) & ">")
        SaveArr("<ss:Table>")
    End Sub
    Private Sub SaveDetilXMLFooter()
        SaveArr("</ss:Table>")
        SaveArr("</Worksheet>")
    End Sub
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function
    Private Sub SaveDetilXML_Sheet1()
        Dim cnt, nomor As Integer
        Dim DT As New System.Data.DataTable

        'Daftar Barang
        SaveDetilXML("Daftar Barang")
        SaveArr("<ss:Row>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">No.</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">PO No.</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Material Description</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Origin</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Unit</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Actual Quantity</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Package</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Package Quantity</Data></ss:Cell>")
        SaveArr("</ss:Row>")

        nomor = 0
        For a = 1 To List.Items.Count
            DT = FrmBL.Array(a - 1).DSPO
            For cnt = 1 To DT.Rows.Count
                nomor = nomor + 1
                SaveArr("<ss:Row>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "Number" & Chr(34) & ">" & nomor & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(List.Items(a - 1).ToString) & "(" & Trim(cnt.ToString) & ")" & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(DT.Rows(cnt - 1).Item(2).ToString) & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(DT.Rows(cnt - 1).Item(3).ToString) & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(DT.Rows(cnt - 1).Item(4).ToString) & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "Number" & Chr(34) & ">" & GetNum2(Trim(DT.Rows(cnt - 1).Item(7).ToString)) & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(DT.Rows(cnt - 1).Item(9).ToString) & "</Data></ss:Cell>")
                SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "Number" & Chr(34) & ">" & GetNum2(Trim(DT.Rows(cnt - 1).Item(10).ToString)) & "</Data></ss:Cell>")
                SaveArr("</ss:Row>")
            Next
        Next
        SaveDetilXMLFooter()
    End Sub
    Private Sub SaveDetilXML_Sheet2()
        Dim brs, kol, cnt As Integer

        'Container
        SaveDetilXML("Container")
        SaveArr("<ss:Row>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Container No.</Data></ss:Cell>")
        SaveArr("<ss:Cell  ss:StyleID=" & Chr(34) & "s27" & Chr(34) & "><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">Size</Data></ss:Cell>")
        SaveArr("</ss:Row>")

        TabControl1.SelectedIndex = 2
        brs = grid4.RowCount
        For cnt = 1 To brs
            SaveArr("<ss:Row>")
            SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(grid4.Rows(cnt - 1).Cells(0).Value) & "</Data></ss:Cell>")
            SaveArr("<ss:Cell><Data ss:Type=" & Chr(34) & "String" & Chr(34) & ">" & Trim(grid4.Rows(cnt - 1).Cells(1).Value) & "</Data></ss:Cell>")
            SaveArr("</ss:Row>")
        Next
        SaveDetilXMLFooter()
    End Sub
    Private Sub SaveHeaderXMLFooter()
        SaveArr("</Workbook>")
    End Sub
    Private Sub SaveArrayXML()
        SaveHeaderXML()
        SaveDetilXML_Sheet1()
        SaveDetilXML_Sheet2()
        SaveHeaderXMLFooter()
    End Sub

    Function GetServerDate() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Function GetFileName() As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim cdrive As System.IO.DriveInfo
        Dim str, dir As String
        Dim temp As Date
        Dim pos1, pos2 As Integer

        If ajuNo.Text = "" Then
            TabControl1.SelectedIndex = 1
            ajuNo.ReadOnly = False
            ajuNo.Focus()
            MsgBox("AJU No. should be filled")
            GetFileName = ""
            Exit Function
        End If
        MyComm.CommandText = "select now()"
        MyComm.CommandType = CommandType.Text
        temp = MyComm.ExecuteScalar()
        MyComm.Dispose()
        dir = Application.ExecutablePath
        pos1 = InStr(dir, "\")
        pos2 = InStr(Microsoft.VisualBasic.Right(dir, Len(dir) - pos1), "\")
        dir = Microsoft.VisualBasic.Left(dir, pos1 + pos2 - 1)
        GetFileName = dir & "\AJU" & ajuNo.Text & "_" & Format(temp, "yyyyMMdd") & "_" & Format(temp, "HHmmss") & ".xml"
    End Function
    Function GetDate3MonthAgo() As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "SELECT DATE_ADD(curdate(),INTERVAL -3 MONTH)"
        MyComm.CommandType = CommandType.Text
        GetDate3MonthAgo = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function
    Private Function DataExist(ByVal str As String) As Boolean
        Dim MyReader As MySqlDataReader

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
    Private Sub FrmCustom_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tg As Date
        Dim value = New System.Drawing.Size(1011, 600)
        Dim loc = New System.Drawing.Point(0, 0)

        Me.Location = loc
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","
        tg = GetServerDate()
        recdt1.Value = GetDate3MonthAgo()
        recdt2.Value = tg
        suppname.Text = ""
        Me.Size = value
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        PilihanDlg.Text = "Select Supplier Code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        PilihanDlg.SQLGrid = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier"
        PilihanDlg.SQLFilter = "SELECT supplier_code as SupplierCode, supplier_name as SupplierName FROM tbm_supplier " & _
                               "WHERE supplier_code LIKE 'FilterData1%' AND supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then suppcode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
    End Sub

    Private Sub suppcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles suppcode.TextChanged
        suppname.Text = AmbilData("supplier_name", "tbm_supplier", "supplier_code='" & suppcode.Text & "'")
    End Sub
    Private Sub ClearScreenData()
        Dim qt0, qt2 As String
        Dim tg As Date

        List.Items.Clear()
        grid.DataSource = Nothing
        grid.Columns.Clear()
        GridInv.DataSource = Nothing
        GridInv.Columns.Clear()
        grid2.DataSource = Nothing
        grid2.Columns.Clear()
        grid3.DataSource = Nothing
        grid3.Columns.Clear()
        grid4.DataSource = Nothing
        grid4.Columns.Clear()
        tg = GetServerDate()
        qt0 = FormatNumber(0, 0, , , TriState.True)
        qt2 = FormatNumber(0, 2, , , TriState.True)
        InsNo.Text = ""
        InsAmt.Text = qt2
        free.Text = qt0
        dt5.Value = GetServerDate()
        KursPajak.Text = qt2
        BeaMasuk.Text = qt0
        vat.Text = qt0
        pph.Text = qt0
        piud.Text = qt0
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
        Currency.Text = ""
        ajuNo.Text = ""
        SPPBNo.Text = ""
        PIBNo.Text = ""
        BeaMasuk.Text = qt0
        vat.Text = qt0
        pph.Text = qt0
        piud.Text = qt0
        Status2.Text = ""
        crtcode.Text = UserData.UserCT.ToString
        crtname.Text = ""
        dt4.Value = tg
        dt5.Value = tg
        dt8.Value = tg
        dt9.Value = tg
        dt10.Value = tg
        dt11.Value = tg
        dt12.Value = tg
        DT13.Value = tg
        DT14.Value = tg
        DT15.Value = tg
        DT16.Value = tg
        DT17.Value = tg
        DT18.Value = tg
        sdem.Text = Microsoft.VisualBasic.Left(tg.ToString, 10)
    End Sub
    Private Sub GetStartDemurrage()
        Dim angka As Integer
        Dim temp As Date

        If DataError Then Exit Sub
        If free.Text = "" Then Exit Sub
        Try
            angka = CInt(free.Text)
            temp = DateAdd(DateInterval.Day, angka, DT13.Value)
            sdem.Text = Microsoft.VisualBasic.Left(temp.ToString, 10)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GetSelisihTgl()
        Dim selisih As Integer
        Dim tg1, tg2 As Date

        If sdem.Text = "" Then Exit Sub
        tg1 = CDate(sdem.Text)
        tg2 = DT16.Value
        selisih = DateDiff(DateInterval.Day, tg1, tg2)
        dem.Text = FormatNumber(selisih, 0, , , TriState.True)
    End Sub
    Private Sub GetTgl(ByVal fld As String, ByVal obj As DateTimePicker)
        Dim temp As Object
        Try
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then obj.Value = temp
        Catch ex As Exception
        End Try
        obj.Checked = Not (temp Is Nothing)
    End Sub
    Private Function GetData(ByVal fld As String) As String
        Dim temp As Object
        GetData = ""
        Try
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then GetData = temp
        Catch ex As Exception
        End Try
    End Function
    Private Sub DisplayBLHeader()
        Dim temp5 As String
        Dim temp7 As String = ""
        Dim temp8 As String = ""
        Dim temp9 As String
        Dim temp10 As String

        strSQL = "select * from tbl_shipping where SHIPMENT_NO = " & ShipNo
        errMsg = "Bill of Lading data view failed"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                temp5 = GetData("Received_By")
                temp7 = MyReader.GetString("CreatedBy")
                temp8 = MyReader.GetString("Currency_Code")
                temp9 = GetData("Verified1By")
                temp10 = GetData("Verified2By")
                GetTgl("Received_Doc_Dt", dt5)
                GetTgl("Verified1Dt", dt8)
                GetTgl("Verified2Dt", dt9)
                GetTgl("PIB_Dt", dt10)
                GetTgl("SPPB_Dt", dt11)
                GetTgl("Est_SPPB_Dt", dt12)
                GetTgl("Notice_Arrival_Dt", DT13)
                GetTgl("OB_Dt", DT14)
                GetTgl("Est_Clr_Dt", DT15)
                GetTgl("Clr_Dt", DT16)
                GetTgl("Est_BAPB_Dt", DT17)
                GetTgl("BAPB_Dt", DT18)
                GetTgl("Forward_Doc_Dt", dt19)
                GetTgl("TT_Dt", dt21)
                GetTgl("Due_Dt", dt20)

                InsNo.Text = MyReader.GetString("Insurance_No")
                InsAmt.Text = FormatNumber(MyReader.GetString("Insurance_Amount"), 2, , TriState.True)
                KursPajak.Text = FormatNumber(MyReader.GetString("Kurs_Pajak"), 2, , , TriState.True)
                bankname.Text = MyReader.GetString("Bank_Name")
                accno.Text = MyReader.GetString("Account_No")
                BeaMasuk.Text = FormatNumber(MyReader.GetString("Bea_Masuk"), 0, , , TriState.True)
                vat.Text = FormatNumber(MyReader.GetString("VAT"), 0, , , TriState.True)
                pph.Text = FormatNumber(MyReader.GetString("Pph21"), 0, , , TriState.True)
                piud.Text = FormatNumber(MyReader.GetString("PIUD"), 0, , , TriState.True)
                Try
                    finalty.Text = FormatNumber(MyReader.GetString("finalty"), 2, , , TriState.True)
                Catch ex As Exception
                    finalty.Text = FormatNumber(0, 2, , , TriState.True)
                End Try
                dt4.Text = MyReader.GetString("CreatedDt")
                Status2.Text = MyReader.GetString("Status")
                free.Text = FormatNumber(MyReader.GetString("Free_Time"), 0, , , TriState.True)

                free_ext.Text = FormatNumber(MyReader.GetString("FTE"), 0, , , TriState.True)

                GetTgl("FTE_PROPOSED_DT", free_ext_prosdt)
                GetTgl("FTE_APPROVED_DT", free_ext_appdt)

                free_ext_note.Text = MyReader.GetString("FTE_Note")

                If MyReader.GetString("RED_LINE") = "Y" Then ChkRedLn.Checked = 1 Else ChkRedLn.Checked = 0
                If MyReader.GetString("MCI") = "Y" Then ChkMCI.Checked = 1 Else ChkMCI.Checked = 0

                Cur_Work.Text = FormatNumber(MyReader.GetString("LEAD_TIME"), 0, , , TriState.True)
                Cur_Eq.Text = FormatNumber(MyReader.GetString("TOTAL_EQUIPMENT"), 0, , , TriState.True)

                ajuNo.Text = GetData("AJU_No")
                PIBNo.Text = GetData("PIB_No")
                SPPBNo.Text = GetData("SPPB_No")
            End While
            CloseMyReader(MyReader, UserData)
            receivecode.Text = temp5
            crtcode.Text = temp7
            Currency.Text = temp8
            vercode1.Text = temp9
            vercode2.Text = temp10
        End If
    End Sub

    Private Sub vercode1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles vercode1.TextChanged
        vername1.Text = AmbilData("name", "tbm_users", "user_ct='" & vercode1.Text & "'")
    End Sub

    Private Sub vercode2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles vercode2.TextChanged
        vername2.Text = AmbilData("name", "tbm_users", "user_ct='" & vercode2.Text & "'")
    End Sub

    Private Sub DT13_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DT13.ValueChanged
        GetStartDemurrage()
    End Sub

    Private Sub free_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles free.TextChanged
        GetStartDemurrage()
    End Sub

    Private Sub DT16_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DT16.ValueChanged
        GetSelisihTgl()
    End Sub

    Private Sub sdem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sdem.TextChanged
        GetSelisihTgl()
    End Sub

    Private Sub receivecode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles receivecode.TextChanged
        If receivecode.Text <> "" Then
            receivedname.Text = AmbilData("name", "tbm_users", "user_ct=" & receivecode.Text)
        Else
            receivedname.Text = ""
        End If
    End Sub

    Private Sub crtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crtcode.TextChanged
        crtname.Text = AmbilData("name", "tbm_users", "user_ct='" & crtcode.Text & "'")
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim ee As DataGridViewCellEventArgs

        If TabControl1.SelectedIndex = 1 And grid3.DataSource Is Nothing Then
            FrmBL.GetDataCustomDoc(grid3, ShipNo, True)
            FrmBL.GetDataPIBStatus(GridPIB, ajuNo.Text, True)
            GridPIB_CellClick(sender, ee)
        End If
        If TabControl1.SelectedIndex = 2 And grid4.DataSource Is Nothing Then FrmBL.GetDataContainer(grid4, ShipNo, True)
        grid3.ReadOnly = True
        grid3.AllowUserToAddRows = False
        grid4.ReadOnly = True
        grid4.AllowUserToAddRows = False
    End Sub

    Private Sub List_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles List.Click
        DisplayPOInvDetil()
    End Sub
    Private Sub DisplayBLDetil()
        Dim ee As System.EventArgs
        Dim brs, cnt As Integer

        strSQL = "select po_no from tbl_shipping_detail where SHIPMENT_NO = " & ShipNo & " group by po_no"
        errMsg = "Bill of Lading data view failed"
        List.Items.Clear()
        MyReader = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    List.Items.Add(MyReader.GetString("PO_No"))
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            brs = List.Items.Count
            ReDim FrmBL.Array(brs - 1)

            For cnt = 1 To brs
                List.SelectedIndex = cnt - 1
                DisplayPOInvDetil()
            Next
            List.SelectedIndex = 0
            List_Click(List, ee)
        End If
    End Sub
    Private Sub DisplayPOInvDetil()
        Dim SelBLNo As String
        Dim brs As Integer

        brs = GridSearch.CurrentCell.RowIndex
        SelBLNo = GridSearch.Item(1, brs).Value
        FrmBL.DisplayPODetil(Trim(List.Items(List.SelectedIndex).ToString), SelBLNo, grid, GridInv, List, GridSearch.Item(0, brs).Value)
        FrmBL.DisplayInvDetil(Trim(List.Items(List.SelectedIndex).ToString), SelBLNo, grid, GridInv, List, GridSearch.Item(0, brs).Value)
        GridInv.AllowUserToAddRows = False
        'grid.Columns(1).Frozen = True
        grid.ScrollBars = ScrollBars.Horizontal
        grid.Columns(1).Frozen = True
        GridInv.Columns(1).Frozen = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dts As DataTable
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs

        ClearScreenData()
        If CekData() = False Then Exit Sub
        GridSearch.DataSource = Nothing
        errMsg = "Search data failed"
        strSQL = GetStrSQL()
        dts = DBQueryDataTable(strSQL, MyConn, "", errMsg, UserData)
        GridSearch.DataSource = dts
        'GridSearch.Columns(0).Visible = False
        GridSearch.Columns(0).HeaderText = "ShipNo."
        GridSearch.Columns(1).HeaderText = "BL No."
        GridSearch.Columns(2).HeaderText = "Supplier"
        GridSearch.Columns(3).HeaderText = "Pack.List No."
        GridSearch.Columns(4).HeaderText = "Rec.Doc.Dt"
        GridSearch.Columns(5).HeaderText = "SPPB Date"
        GridSearch.Columns(6).HeaderText = "PIB Date"
        GridSearch.Columns(0).Width = 50
        GridSearch.Columns(1).Width = 71
        GridSearch.Columns(4).Width = 71
        GridSearch.Columns(5).Width = 71
        GridSearch.Columns(6).Width = 71
        GridSearch.Columns(0).Frozen = True
        ReDim FrmBL.Array(0)
        If dts.Rows.Count = 0 Then
            MsgBox("Data not found")
        Else
            GridSearch.CurrentCell = GridSearch(1, 0)
            GridSearch_CellClick(sender, ee)
        End If
        btnCreate.Enabled = (GridSearch.RowCount > 0)
        btnUpdate.Enabled = (GridSearch.RowCount > 0)
        btnko.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("KO-C")
        btnSK.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("SK-C")
        btnJC.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("JC-C")
        btnJC.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("JC-C")
        btnBC.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("BC-C")
        btnPC.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("PC-C")
        btnDS.Enabled = (GridSearch.RowCount > 0) And FrmBL.PunyaAkses("DS-C")
    End Sub
    Private Function GetStrSQL() As String
        Dim temp1, temp2, temp3, tgl1, tgl2 As String
        'received_doc_dt,pib_dt
        temp1 = "select shipment_no,bl_no,supplier_code,packinglist_no," & _
                "if(received_doc_dt is null," & Chr(34) & Chr(34) & ",cast(received_doc_dt as char(10))), " & _
                "if(sppb_dt is null," & Chr(34) & Chr(34) & ",cast(sppb_dt as char(10))), " & _
                "if(pib_dt is null," & Chr(34) & Chr(34) & ",cast(pib_dt as char(10))) " & _
                "from tbl_shipping "
        temp2 = ""
        If Trim(blno.Text) <> "" Then temp2 = temp2 & "bl_no like'" & blno.Text & "%'"
        If Trim(suppcode.Text) <> "" Then
            If temp2 <> "" Then temp2 = temp2 & " and "
            temp2 = temp2 & "supplier_code='" & suppcode.Text & "'"
        End If
        If Trim(packlist.Text) <> "" Then
            If temp2 <> "" Then temp2 = temp2 & " and "
            temp2 = temp2 & "PackingList_No like '" & packlist.Text & "%'"
        End If
        tgl1 = Format(recdt1.Value, "yyyy-MM-dd")
        tgl2 = Format(recdt2.Value, "yyyy-MM-dd")
        If temp2 <> "" Then temp2 = temp2 & " and "
        temp2 = temp2 & "((received_doc_dt>=" & Chr(34) & tgl1 & Chr(34) & " and received_doc_dt<=" & Chr(34) & tgl2 & Chr(34) & ") or (received_Doc_Dt is null))"
        temp3 = GetFilterStatus()
        If temp2 <> "" Then
            GetStrSQL = temp1 & " where " & temp2
            If temp3 <> "" Then GetStrSQL = GetStrSQL & " and " & temp3
        Else
            GetStrSQL = temp1
            If temp3 <> "" Then GetStrSQL = GetStrSQL & " where " & temp3
        End If
    End Function
    Private Function GetFilterStatus() As String
        Dim tgl1, tgl2 As String

        If cb1.Checked Then GetFilterStatus = ""
        If cb2.Checked Then GetFilterStatus = "SPPB_No=''"
        If cb3.Checked Then GetFilterStatus = "SPPB_No<>''"
        If cb4.Checked Then
            If GetFilterStatus <> "" Then GetFilterStatus = GetFilterStatus & " and "
            GetFilterStatus = GetFilterStatus & "PIB_Dt is null"
        End If
        If cb5.Checked Then
            tgl1 = Format(pibdt1.Value, "yyyy-MM-dd")
            tgl2 = Format(pibdt2.Value, "yyyy-MM-dd")
            If GetFilterStatus <> "" Then GetFilterStatus = GetFilterStatus & " and "
            GetFilterStatus = GetFilterStatus & "PIB_dt>=" & Chr(34) & tgl1 & Chr(34) & " and PIB_dt<=" & Chr(34) & tgl2 & Chr(34)
        End If
    End Function

    Private Sub GridSearch_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridSearch.CellClick
        Dim brs As Integer

        If (cb1.Checked Or cb2.Checked Or cb3.Checked Or cb4.Checked Or cb5.Checked) = False Then cb1.Checked = True
        ClearScreenData()
        brs = GridSearch.CurrentCell.RowIndex
        GridSearch.Rows(brs).Selected = True
        ShipNo = GridSearch.Item(0, brs).Value
        TabControl1.SelectedIndex = 0
        POTab.SelectedIndex = 0
        FrmBL.GetDataSuppDoc(grid2, ShipNo, True)
        DisplayBLHeader()
        DisplayBLDetil()
        grid.ReadOnly = True
        GridInv.ReadOnly = True
        grid2.ReadOnly = True
        grid2.AllowUserToAddRows = False
        DisplayDocument()
    End Sub
    Private Sub DisplayDocument()
        Dim teks, str2, stat, num As String

        strSQL = "select concat('SK-DO',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & ShipNo.ToString & " and findoc_type='KO' " & _
                 " union " & _
                 "select concat('SK-KR',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & ShipNo.ToString & " and findoc_type='SK' " & _
                 " union " & _
                 "select concat('SK-JC',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & ShipNo.ToString & " and findoc_type='JC' " & _
                 " union " & _
                 "select concat('SK-BC',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & ShipNo.ToString & " and findoc_type='BC' " & _
                 " union " & _
                 "select concat('SK-PC',' #',ord_no) as data,findoc_status  as status from tbl_shipping_doc " & _
                 "where shipment_no=" & ShipNo.ToString & " and findoc_type='PC' "

        errMsg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        ListBox1.Items.Clear()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    stat = MyReader.GetString("status")
                    teks = Trim(MyReader.GetString("data"))
                    str2 = Microsoft.VisualBasic.Left(teks, 2)
                    teks = Mid(teks & Space(29), 1, 29) & " - " & Microsoft.VisualBasic.Left(stat & Space(10), 10)
                    ListBox1.Items.Add(teks)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub cb1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb1.CheckedChanged
        If Not (cb1.Checked) Then Exit Sub
        cb2.Checked = Not (cb1.Checked)
        cb3.Checked = Not (cb1.Checked)
        cb4.Checked = Not (cb1.Checked)
        cb5.Checked = Not (cb1.Checked)
        Label9.Visible = (cb4.Checked)
        Label10.Visible = (cb4.Checked)
        pibdt1.Visible = (cb4.Checked)
        pibdt2.Visible = (cb4.Checked)
    End Sub

    Private Sub cb2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb2.CheckedChanged
        If Not (cb2.Checked) Then Exit Sub
        cb1.Checked = False
        cb3.Checked = Not (cb2.Checked)
    End Sub

    Private Sub cb4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb4.CheckedChanged
        If Not cb4.Checked Then Exit Sub
        cb1.Checked = False
        cb5.Checked = Not (cb4.Checked)
        Label9.Visible = (cb5.Checked)
        Label10.Visible = (cb5.Checked)
        pibdt1.Visible = (cb5.Checked)
        pibdt2.Visible = (cb5.Checked)
    End Sub

    Private Sub cb3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb3.CheckedChanged
        If Not cb3.Checked Then Exit Sub
        cb1.Checked = False
        cb2.Checked = False
    End Sub

    Private Sub cb5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb5.CheckedChanged
        Dim tg As Date

        If Not cb5.Checked Then Exit Sub
        cb1.Checked = False
        cb4.Checked = False
        Label9.Visible = (cb5.Checked)
        Label10.Visible = (cb5.Checked)
        pibdt1.Visible = (cb5.Checked)
        pibdt2.Visible = (cb5.Checked)
        tg = GetServerDate()
        pibdt1.Value = tg
        pibdt2.Value = tg
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim ee As DataGridViewCellEventArgs
        Dim f As New FrmUpdatePIB(ajuNo.Text, blno.Text, PIBNo.Text, SPPBNo.Text, GridSearch.DataSource)
        f.ShowDialog()
        GridSearch_CellClick(sender, ee)
    End Sub

    Private Sub btnko_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnko.Click
        OpenFrm("KO")
    End Sub

    Private Sub btnSK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSK.Click
        OpenFrm("SK")
    End Sub
    Private Sub OpenFrm(ByVal jns As String)
        If GridSearch.RowCount = 0 Then
            MsgBox("Data not found")
            Exit Sub
        End If
        Dim f As New FrmKOSK(ShipNo.ToString, "", "", "", "", "", jns)
        If DataOK(jns) = True Then
            f.Label6.Visible = (jns = "JC")
            f.refund.Visible = (jns = "JC")
            f.Label7.Visible = (jns = "JC")
            f.Curr.Visible = (jns = "JC")
            f.btnSearch.Visible = (jns = "JC")
            If jns = "PC" Then
                f.Label3.Visible = False
                f.txtExp.Visible = False
                f.btnSearchExp.Visible = False
                f.Label4.Visible = False
                f.txtAuthorized.Visible = False
                f.Label2.Visible = False
                f.txtTitle.Visible = False
                f.lblExp.Visible = False
                f.crtt.Top = f.Label3.Top
                f.crt.Top = f.txtExp.Top
                f.crtdt.Top = f.txtExp.Top
                f.crtdttext.Top = f.Label3.Top
                f.Label12.Top = f.Label4.Top
                f.approvedby.Top = f.txtAuthorized.Top
                f.Button3.Top = f.txtAuthorized.Top
                f.Label5.Top = f.Label4.Top
                f.AppDt.Top = f.txtAuthorized.Top
            End If
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub

    Private Function DataOK(ByVal jns As String) As Boolean
        Dim mess1, mess2 As String
        Dim status As String = ""
        Dim V_MODULE_NAME As String


        If jns = "KO" Then
            V_MODULE_NAME = "SK-DO"
        ElseIf jns = "SK" Then
            V_MODULE_NAME = "SK-KR"
        ElseIf jns = "JC" Then
            V_MODULE_NAME = "SK-JC"
        ElseIf jns = "BC" Then
            V_MODULE_NAME = "SK-BC"
        ElseIf jns = "PC" Then
            V_MODULE_NAME = "SK-PC"
        End If

        errMsg = "Failed when read " & jns & " data"
        mess1 = V_MODULE_NAME & " has been closed"
        mess2 = V_MODULE_NAME & " has been created"

        strSQL = "select FINDOC_STATUS from tbl_shipping_doc " & _
                 "where SHIPMENT_NO='" & ShipNo.ToString & "'" & " and ORD_NO=(select max(ord_no) from tbl_shipping_doc where " & _
                 "SHIPMENT_NO='" & ShipNo.ToString & "' and FINDOC_TYPE='" & jns & "') " & _
                 "and FINDOC_TYPE='" & jns & "'"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

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
    End Function
    Private Sub OpenFrmDoubleClick(ByVal kdAkses1 As String, ByVal module_code As String, ByVal chosen As String, ByVal jns As String)
        If FrmBL.PeriksaAkses(kdAkses1, module_code) Then
            Dim f As New FrmKOSK(ShipNo.ToString, "", "", "", "", chosen, jns)
            f.Label6.Visible = (jns = "JC")
            f.refund.Visible = (jns = "JC")
            f.Label7.Visible = (jns = "JC")
            f.Curr.Visible = (jns = "JC")
            f.btnSearch.Visible = (jns = "JC")
            If jns = "PC" Then
                f.Label3.Visible = False
                f.txtExp.Visible = False
                f.btnSearchExp.Visible = False
                f.Label4.Visible = False
                f.txtAuthorized.Visible = False
                f.Label2.Visible = False
                f.txtTitle.Visible = False
                f.lblExp.Visible = False
                f.crtt.Top = f.Label3.Top
                f.crt.Top = f.txtExp.Top
                f.crtdt.Top = f.txtExp.Top
                f.crtdttext.Top = f.Label3.Top
                f.Label12.Top = f.Label4.Top
                f.approvedby.Top = f.txtAuthorized.Top
                f.Button3.Top = f.txtAuthorized.Top
                f.Label5.Top = f.Label4.Top
                f.AppDt.Top = f.txtAuthorized.Top
            End If
            f.ShowDialog()
            DisplayDocument()
        End If
    End Sub
    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim chosen, PO, InPO, num, str5 As String
        Dim pos, pjg As Integer

        If ListBox1.SelectedIndex < 0 Or ListBox1.SelectedIndex < 0 Then Exit Sub
        chosen = Microsoft.VisualBasic.Left(ListBox1.Items(ListBox1.SelectedIndex).ToString, 29)
        str5 = Microsoft.VisualBasic.Left(chosen, 5)

        Select Case str5
            Case "SK-DO"
                OpenFrmDoubleClick("KO-L", str5, chosen, "KO")
            Case "SK-KR"
                OpenFrmDoubleClick("SK-L", str5, chosen, "SK")
            Case "SK-JC"
                OpenFrmDoubleClick("JC-L", str5, chosen, "JC")
            Case "SK-BC"
                OpenFrmDoubleClick("BC-L", str5, chosen, "BC")
            Case "SK-PC"
                OpenFrmDoubleClick("PC-L", str5, chosen, "PC")
            Case "DS"
                OpenFrmDoubleClick("DS-L", str5, chosen, "DS")
        End Select
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

    Private Sub btnDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDS.Click
        OpenFrm("DS")
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
End Class