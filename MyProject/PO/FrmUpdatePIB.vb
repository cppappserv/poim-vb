''' <summary>
''' Title                         : Update PIB
''' Form                          : FrmUpdatePIB
''' Master Form                   : FrmCustom
''' Table MySQL Used              :
''' Stored Procedure Used (MySQL) : 
''' Created By                    : Yanti 25.02.2009
''' Catatan                       : Get data from database Access (dari Bea Cukai), trus disimpan di POIM
'''                                 Password access : MumtazFarisHana
'''                                 Data dari access kemudian di update ke MySQL
''' Table Access used             : TblPibRes, TblPibKms, TblPibHdr, TblPibDok
''' </summary>
''' <remarks></remarks>

'Sekali update bisa BANYAK Shipment_No
Public Class FrmUpdatePIB
    Dim ship, strQuery1, strQuery2, strQuery3, strQuery4, strQuery5 As String
    Dim MyReader As MySqlDataReader
    Dim strSQL, temp, ErrMsg, fmtTime As String
    Private Sub GetShipNoIn(ByVal DT As System.Data.DataTable)
        Dim cnt, max As Integer

        max = DT.Rows.Count - 1
        ship = "("
        For cnt = 0 To max
            If ship <> "(" Then ship = ship & ","
            ship = ship & Trim(DT.Rows(cnt).Item(0).ToString())
        Next
        ship = ship & ")"
    End Sub
    Private Sub GetStrQuery1()
        strSQL = "select po_no from tbl_shipping_Detail where shipment_no in " & ship & " group by po_no"
        strQuery1 = "("
        ErrMsg = "failed get PO No."
        MyReader = DBQueryMyReader(strSQL, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If strQuery1 <> "(" Then strQuery1 = strQuery1 & ","
                    strQuery1 = strQuery1 & "'" & Trim(MyReader.GetString("PO_No")) & "'"
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        strQuery1 = strQuery1 & ")"
    End Sub
    Private Sub GetStrQuery2()
        strSQL = "select bl_no from tbl_shipping where shipment_no in " & ship & " and aju_no='' and bl_no<>''"
        strQuery2 = "("
        ErrMsg = "failed get BL No."
        MyReader = DBQueryMyReader(strSQL, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If strQuery2 <> "(" Then strQuery2 = strQuery2 & ","
                    strQuery2 = strQuery2 & "'" & Trim(MyReader.GetString("bl_No")) & "'"
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        strQuery2 = strQuery2 & ")"
    End Sub
    Private Sub GetStrQuery3()
        strSQL = "select aju_no from tbl_shipping where shipment_no in " & ship & " and pib_no='' and aju_no<>''"
        strQuery3 = "("
        ErrMsg = "failed get AJU No."
        MyReader = DBQueryMyReader(strSQL, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If strQuery3 <> "(" Then strQuery3 = strQuery3 & ","
                    strQuery3 = strQuery3 & "'" & Trim(MyReader.GetString("aju_No")) & "'"
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        strQuery3 = strQuery3 & ")"
    End Sub
    Private Sub GetStrQuery4()
        strSQL = "select aju_no from tbl_shipping where shipment_no in " & ship & " and sppb_no='' and aju_no<>''"
        strQuery4 = "("
        ErrMsg = "failed get AJU No."
        MyReader = DBQueryMyReader(strSQL, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If strQuery4 <> "(" Then strQuery4 = strQuery4 & ","
                    strQuery4 = strQuery4 & "'" & Trim(MyReader.GetString("aju_No")) & "'"
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        strQuery4 = strQuery4 & ")"
    End Sub
    Private Sub GetStrQuery5()
        strSQL = "select aju_no from tbl_shipping where shipment_no in " & ship & " AND aju_no<>''"
        strQuery5 = "("
        ErrMsg = "failed get AJU No."
        MyReader = DBQueryMyReader(strSQL, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    If strQuery5 <> "(" Then strQuery5 = strQuery5 & ","
                    strQuery5 = strQuery5 & "'" & Trim(MyReader.GetString("aju_No")) & "'"
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        strQuery5 = strQuery5 & ")"
    End Sub
    Private Sub GetStrQuery()
        GetStrQuery1()
        GetStrQuery2()
        GetStrQuery3()
        GetStrQuery4()
        GetStrQuery5()
    End Sub
    Sub New(ByVal AJU As String, ByVal BL As String, ByVal PIB As String, ByVal SPPB As String, ByVal DT As System.Data.DataTable)
        InitializeComponent()
        Label2.Text = ""
        'Label3.Text = ""
        Label4.Text = ""
        Label5.Text = ""
        Label6.Text = ""
        GetShipNoIn(DT)
        GetStrQuery()
    End Sub
    Private Sub FrmUpdatePIB_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim value = New System.Drawing.Point(355, 250)
        Dim size = New System.Drawing.Point(779, 332)
        Dim sep As String

        Me.Location = value
        Me.Size = size
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        sep = Global.System.Globalization.DateTimeFormatInfo.CurrentInfo.TimeSeparator
        'fmtTime = "%H " & sep & "%I " & sep & "%s"
        fmtTime = "&'" & sep & "'"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        If (result = DialogResult.OK) Then ds.Text = OpenFileDialog1.FileName
    End Sub
    Private Function AccessDBQueryMyReader(ByVal SQLStr As String, ByVal AccessConn As OleDb.OleDbConnection) As OleDb.OleDbDataReader
        Dim Reader As OleDb.OleDbDataReader = Nothing
        Dim MyCmd As New OleDb.OleDbCommand(SQLStr, AccessConn)

        Try
            Reader = MyCmd.ExecuteReader()
            Return Reader
        Catch ex As OleDb.OleDbException
            MsgBox(ex.Message)
            Return Nothing
        End Try

        'AccessReader = AccessDBQueryMyReader(strSQL, AccessConn)
        'If Not AccessReader Is Nothing Then
        '    While AccessReader.Read
        '        Try
        '            test= AccessReader.Item(0).ToString
        '            MsgBox(test)
        '            test = AccessReader.Item(1).ToString
        '            MsgBox(test)
        '        Catch ex As Exception
        '        End Try
        '    End While
        '    AccessConn.Close()
        'End If
        'AccessConn = Nothing
    End Function
    'Private Function GetMaxStatusDate() As String
    '    Dim MyComm As MySqlCommand = MyConn.CreateCommand()
    '    Dim tg As Date

    '    'MyComm.CommandText = "SELECT max(status_dt) FROM tbl_pib_history where AJU_No='" & AJUNo & "'"
    '    MyComm.CommandText = "SELECT max(status_dt) FROM tbl_pib_history where AJU_No IN " & strQuery5
    '    MyComm.CommandType = CommandType.Text
    '    Try
    '        tg = MyComm.ExecuteScalar()
    '        GetMaxStatusDate = "#" & Format(tg, "MM-dd-yyyy") & "#"
    '    Catch ex As Exception
    '        GetMaxStatusDate = ""
    '    End Try
    '    MyComm.Dispose()
    'End Function
    Private Function GetRecord() As Integer
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim tg As Date

        'MyComm.CommandText = "SELECT count(*) FROM tbl_pib_history where AJU_No='" & AJUNo & "'"
        MyComm.CommandText = "SELECT count(*) FROM tbl_pib_history where AJU_No IN " & strQuery5
        MyComm.CommandType = CommandType.Text
        GetRecord = CInt(MyComm.ExecuteScalar())
        MyComm.Dispose()
    End Function
    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If ds.Text = "" Then
            MsgBox("Data Source should be filled")
            Button1.Focus()
            Exit Sub
        End If
        GetAccessData()
        If grid2.DataSource Is Nothing And Grid3.DataSource Is Nothing _
           And Grid4.DataSource Is Nothing And grid5.DataSource Is Nothing Then
            MsgBox("No data from Data Source match with PO Import data")
            Exit Sub
        End If
        ProsesUpdate()
        MsgBox("Finish update PIB")
    End Sub
    Private Sub GetAccessData()
        Dim AccessConn As New OleDb.OleDbConnection
        Dim PO, MaxStatDt, strSQL As String

        AccessConn.ConnectionString = "Jet OLEDB:Database Password=MumtazFarisHana;Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & ds.Text & ";Persist Security Info=True"
        AccessConn.Open()

        'Query 1
        'strSQL = "SELECT TblPibRes.PibTg, TblPibRes.PibNo,TblPibRes.CAR,TblPibRes.DokResTg, TblPibRes.DokResNo,TblPibKms.MerkKemas,'' as hasilupdate " & _
        '         "FROM TblPibRes, TblPibKms WHERE TblPibRes.CAR = TblPibKms.CAR AND TblPibKms.MerkKemas IN " & strQuery1 & _
        '         " and TblPibRes.dibaca=True and TblPibRes.PibNo<>'' and TblPibRes.DokResNo <> ''"

        'Dim MyAdapter1 As New OleDb.OleDbDataAdapter(strSQL, AccessConn)
        'Dim MyDataSet1 As New DataSet
        'Try
        '    MyAdapter1.Fill(MyDataSet1)
        '    grid1.DataSource = MyDataSet1.Tables(0)
        '    grid1.Columns(0).HeaderText = "PIB Date"
        '    grid1.Columns(1).HeaderText = "PIB No."
        '    grid1.Columns(2).HeaderText = "AJU No."
        '    grid1.Columns(3).HeaderText = "SPPB Date"
        '    grid1.Columns(4).HeaderText = "SPPB No."
        '    grid1.Columns(5).Visible = False
        '    grid1.Columns(6).HeaderText = "Update Status"
        'Catch ex As Exception
        'Finally
        '    If grid1.DataSource Is Nothing Or grid1.Rows.Count = 0 Then
        '        grid1.DataSource = Nothing
        '    End If
        '    Label3.Text = FormatNumber(grid1.RowCount, 0, , , TriState.True) & " record(s)"
        'End Try

        'Query 2
        'strSQL = "SELECT DokNo,car FROM tblPibDok WHERE DokKd = '705' AND DokNo='" & BLNo & "'"
        strSQL = "SELECT DokNo,car,'' as hasilupdate FROM tblPibDok WHERE DokKd = '705' AND DokNo IN " & strQuery2
        Dim MyAdapter2 As New OleDb.OleDbDataAdapter(strSQL, AccessConn)
        Dim MyDataSet2 As New DataSet
        Try
            MyAdapter2.Fill(MyDataSet2)
            grid2.DataSource = MyDataSet2.Tables(0)
            grid2.Columns(0).HeaderText = "BL No."
            grid2.Columns(0).Width = 132
            grid2.Columns(1).HeaderText = "AJU No."
            grid2.Columns(1).Width = 164
            grid2.Columns(2).HeaderText = "Update Status"
        Catch ex As Exception
        Finally
            If grid2.DataSource Is Nothing Or grid2.Rows.Count = 0 Then
                grid2.DataSource = Nothing
            End If
        End Try
        Label2.Text = FormatNumber(grid2.RowCount, 0, , , TriState.True) & " record(s)"

        'Query 3
        'strSQL = "SELECT car,PibNo,PibTg FROM tblPibHdr WHERE car='" & AJUNo & "'"
        strSQL = "SELECT car,PibNo,PibTg,'' as hasilupdate FROM tblPibHdr WHERE car IN " & strQuery3
        Dim MyAdapter3 As New OleDb.OleDbDataAdapter(strSQL, AccessConn)
        Dim MyDataSet3 As New DataSet
        Try
            MyAdapter3.Fill(MyDataSet3)
            Grid3.DataSource = MyDataSet3.Tables(0)
            Grid3.Columns(0).HeaderText = "AJU No."
            Grid3.Columns(1).HeaderText = "PIB No."
            Grid3.Columns(2).HeaderText = "PIB Date"
            Grid3.Columns(3).HeaderText = "Update Status"
        Catch ex As Exception
        Finally
            If Grid3.DataSource Is Nothing Or Grid3.Rows.Count = 0 Then
                Grid3.DataSource = Nothing
            End If
        End Try
        Label4.Text = FormatNumber(Grid3.RowCount, 0, , , TriState.True) & " record(s)"

        'Query 4
        'strSQL = "SELECT car,DokResNo, DokResTg FROM tblPibRes WHERE ResKd = '300' AND car ='" & AJUNo & "'"
        strSQL = "SELECT car,DokResNo, DokResTg,'' as hasilupdate FROM tblPibRes WHERE ResKd = '300' AND car IN " & strQuery4
        Dim MyAdapter4 As New OleDb.OleDbDataAdapter(strSQL, AccessConn)
        Dim MyDataSet4 As New DataSet
        Try
            MyAdapter4.Fill(MyDataSet4)
            Grid4.DataSource = MyDataSet4.Tables(0)
            Grid4.Columns(0).HeaderText = "AJU No."
            Grid4.Columns(1).HeaderText = "SPPB No."
            Grid4.Columns(2).HeaderText = "SPPB Date"
            Grid4.Columns(3).HeaderText = "Update Status"
        Catch ex As Exception
        Finally
            If Grid4.DataSource Is Nothing Or Grid4.Rows.Count = 0 Then
                Grid4.DataSource = Nothing
            End If
        End Try
        Label5.Text = FormatNumber(Grid4.RowCount, 0, , , TriState.True) & " record(s)"

        'Query 5
        'MaxStatDt = GetMaxStatusDate()
        ''strSQL = "SELECT car, kpbc, reskd, restg, deskripsi FROM tblPibRes WHERE restg >" & MaxStatDt & " AND car='" & AJUNo & "'"
        'strSQL = "SELECT car, kpbc, reskd, restg, deskripsi,'' as hasilupdate FROM tblPibRes WHERE restg >" & MaxStatDt & " AND car IN " & strQuery5
        strSQL = "SELECT t1.car,t1.restg&' '&" & _
                 "Left(t1.reswk,2)" & fmtTime & "&Mid(t1.reswk,3,2)" & fmtTime & "&Mid(t1.reswk,5,2), " & _
                 "t2.uraian,'' as hasilupdate FROM tblPibRes t1, tblTabel t2 WHERE t1.ResKd=t2.KdRec AND ResKd <> '300' AND car IN " & strQuery5

        Dim MyAdapter5 As New OleDb.OleDbDataAdapter(strSQL, AccessConn)
        Dim MyDataSet5 As New DataSet
        Try
            MyAdapter5.Fill(MyDataSet5)
            grid5.DataSource = MyDataSet5.Tables(0)
            grid5.Columns(0).HeaderText = "AJU No."
            grid5.Columns(1).HeaderText = "Respon Date Time"
            grid5.Columns(2).HeaderText = "Respon Note"
            grid5.Columns(3).HeaderText = "Update Status"
            grid5.Columns(0).Width = 164
            grid5.Columns(1).Width = 90
        Catch ex As Exception
        Finally
            If grid5.DataSource Is Nothing Or grid5.Rows.Count = 0 Then
                grid5.DataSource = Nothing
            End If
        End Try
        Label6.Text = FormatNumber(grid5.RowCount, 0, , , TriState.True) & " record(s)"
    End Sub
    Private Sub ProsesUpdate()
        Dim brs, cnt, JumRec As Integer
        Dim PIBNo, AJUNo, SPPBNo, BLNo, PIBDt, SPPBDt, Status, Desc, RespDt, RespDt2, RespNote As String
        Dim SQLstr, SQLStr2, Detail, SpNo, hasil As String
        Dim dt As DateTime

        'If Not grid1.DataSource Is Nothing Then
        '    brs = grid1.RowCount
        '    For cnt = 1 To brs
        '        PIBDt = Format(grid1.Item(0, cnt - 1).Value, "yyyy-MM-dd")
        '        PIBNo = Trim(grid1.Item(1, cnt - 1).Value)
        '        AJUNo = Trim(grid1.Item(2, cnt - 1).Value)
        '        SPPBDt = Format(grid1.Item(3, cnt - 1).Value, "yyyy-MM-dd")
        '        SPPBNo = Trim(grid1.Item(4, cnt - 1).Value)
        '        PONo = Trim(grid1.Item(5, cnt - 1).Value)
        '        'SQLstr = "Update tbl_shipping set PIB_Dt='" & PIBDt & ",PIB_No='" & PIBNo & "',AJU_No='" & AJUNo & "',SPPB_Dt='" & SPPBDt & "',SPPB_No='" & SPPBNo & "' " & _
        '        '         "where Shipment_No IN " & ship & " and po_no='" & POno

        '        SQLstr = "Update tbl_shipping set PIB_Dt='" & PIBDt & "',PIB_No='" & PIBNo & "',AJU_No='" & AJUNo & "',SPPB_Dt='" & SPPBDt & "',SPPB_No='" & SPPBNo & "' " & _
        '                 "where shipment_no in " & _
        '                 "(select shipment_no from " & _
        '                                           "(select b.shipment_no,b.po_no from tbl_shipping as a " & _
        '                                           "inner join tbl_shipping_Detail as b on a.shipment_no=b.shipment_no " & _
        '                                           "where b.shipment_no in " & ship & _
        '                                           ") as x where po_no='" & PONo & "'" & _
        '                 ")"
        '        grid1.Item(6, cnt - 1).Value = Update(SQLstr)
        '    Next
        'End If

        If Not grid2.DataSource Is Nothing Then
            brs = grid2.RowCount
            For cnt = 1 To brs
                BLNo = grid2.Item(0, cnt - 1).Value
                AJUNo = grid2.Item(1, cnt - 1).Value
                'SQLstr = "Update tbl_shipping  set AJU_No='" & AJUNo & "' where Shipment_No in " & ship & " and bl_no='" & BLNo & "'"

                'SQLstr = "Update tbl_shipping  set AJU_No='" & AJUNo & "'" & _
                '         "where shipment_no in " & _
                '         "(select shipment_no from " & _
                '                                   "(select b.shipment_no,b.po_no from tbl_shipping as a " & _
                '                                   "inner join tbl_shipping_Detail as b on a.shipment_no=b.shipment_no " & _
                '                                   "where b.shipment_no in " & ship & _
                '                                   ") as x where bl_no='" & BLNo & "'" & _

                SQLstr = "Update tbl_shipping  set AJU_No='" & AJUNo & "'" & _
                         "where shipment_no in " & _
                         "(select shipment_no from " & _
                                                   "(select shipment_no,bl_no from tbl_shipping where shipment_no in " & ship & _
                                                   ") as x where bl_no='" & BLNo & "'" & _
                         ")"
                '         ")"
                grid2.Item(2, cnt - 1).Value = Update(SQLstr)
            Next
        End If

        If Not Grid3.DataSource Is Nothing Then
            brs = Grid3.RowCount
            For cnt = 1 To brs
                AJUNo = Grid3.Item(0, cnt - 1).Value
                PIBNo = Grid3.Item(1, cnt - 1).Value
                Try
                    PIBDt = Format(Grid3.Item(2, cnt - 1).Value, "yyyy-MM-dd")
                Catch ex As Exception
                    PIBDt = ""
                End Try
                'SQLstr = "Update tbl_shipping set pib_No='" & PIBNo & "',PIB_Dt='" & PIBDt & "' where Shipment_No in " & ship & " and aju_no='" & AJUNo & "'"
                'SQLstr = "Update tbl_shipping set pib_No='" & PIBNo & "',PIB_Dt='" & PIBDt & "' " & _
                '         "where shipment_no in " & _
                '         "(select shipment_no from " & _
                '                                   "(select b.shipment_no,b.po_no from tbl_shipping as a " & _
                '                                   "inner join tbl_shipping_Detail as b on a.shipment_no=b.shipment_no " & _
                '                                   "where b.shipment_no in " & ship & _
                '                                   ") as x where AJU_no='" & AJUNo & "'" & _

                SQLstr = "Update tbl_shipping set pib_No='" & PIBNo & "'"
                If PIBDt <> "" Then SQLstr = SQLstr & ",PIB_Dt='" & PIBDt & "' "
                SQLstr = SQLstr & "where shipment_no in " & _
                                  "(select shipment_no from " & _
                                                           "(select shipment_no,aju_no from tbl_shipping where shipment_no in " & ship & _
                                                           ") as x where AJU_no='" & AJUNo & "'" & _
                                  ")"

                Grid3.Item(3, cnt - 1).Value = Update(SQLstr)
            Next
        End If

        If Not Grid4.DataSource Is Nothing Then
            brs = Grid4.RowCount
            For cnt = 1 To brs
                AJUNo = Grid4.Item(0, cnt - 1).Value
                SPPBNo = Grid4.Item(1, cnt - 1).Value
                Try
                    SPPBDt = Format(Grid4.Item(2, cnt - 1).Value, "yyyy-MM-dd")
                Catch ex As Exception
                    SPPBDt = ""
                End Try
                'SQLstr = "Update tbl_shipping set SPPB_No='" & SPPBNo & "',SPPB_Dt='" & SPPBDt & "' where Shipment_No in " & ship & " and aju_no='" & AJUNo & "'"

                'SQLstr = "Update tbl_shipping set SPPB_No='" & SPPBNo & "',SPPB_Dt='" & SPPBDt & "'" & _
                '         "where shipment_no in " & _
                '         "(select shipment_no from " & _
                '                                   "(select b.shipment_no,b.po_no from tbl_shipping as a " & _
                '                                   "inner join tbl_shipping_Detail as b on a.shipment_no=b.shipment_no " & _
                '                                   "where b.shipment_no in " & ship & _
                '                                   ") as x where AJU_no='" & AJUNo & "'" & _
                '         ")"

                SQLstr = "Update tbl_shipping set SPPB_No='" & SPPBNo
                If SPPBDt <> "" Then SQLstr = SQLstr & "',SPPB_Dt='" & SPPBDt & "'"
                SQLstr = SQLstr & "where shipment_no in " & _
                                  "(select shipment_no from " & _
                                                            "(select shipment_no,aju_no from tbl_shipping where shipment_no in " & ship & _
                                                            ") as x where AJU_no='" & AJUNo & "'" & _
                                  ")"
                Grid4.Item(3, cnt - 1).Value = Update(SQLstr)
            Next
        End If

        If Not grid5.DataSource Is Nothing Then
            brs = grid5.RowCount
            JumRec = GetRecord()
            Detail = ""
            For cnt = 1 To brs
                JumRec = JumRec + 1
                AJUNo = grid5.Item(0, cnt - 1).Value
                Try
                    RespDt = grid5.Item(1, cnt - 1).Value
                    dt = RespDt
                    RespDt2 = Format(dt, "yyyy-MM-dd HH:mm:ss")
                Catch ex As Exception
                    RespDt2 = ""
                End Try

                RespNote = grid5.Item(2, cnt - 1).Value
                'SQLGetShipNo = "(select shipment_no as from " & _
                '                                       "(select shipment_no,aju_no from tbl_shipping where shipment_no in " & ship & _
                '                                       ") as x where AJU_no='" & AJUNo & "')"
                'SQLstr = "Insert tbl_pib_respon(Shipment_No,Respon_No,Respon_Dt,Respon_Note) values(" & _
                '         SQLGetShipNo & _
                '         "," & JumRec & "," & Chr(34) & RespDt2 & Chr(34) & "," & Chr(34) & RespNote & Chr(34) & ")"

                SpNo = AmbilData("shipment_no", "(select shipment_no,aju_no from tbl_shipping where shipment_no in " & ship & ") as x", "AJU_no='" & AJUNo & "'")
                RespNote = LSet(RespNote, 255)
                Detail = Detail & RespDt2 & RespNote & ";"
                SQLstr = "Run Stored Procedure SavePIB (" & SpNo & "," & UserData.UserCT & "," & Trim(Detail) & ")"
            Next

            If Detail <> "" Then
                hasil = Insert_Pib_Respon(SpNo, Detail, SQLstr)
                For cnt = 0 To grid5.RowCount - 1
                    grid5.Item(3, cnt).Value = hasil
                Next
            End If
        End If
    End Sub
    Private Function Insert_Pib_Respon(ByVal sp As Integer, ByVal Detail As String, ByVal SQLStr As String) As String
        Dim hasil As Boolean
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        With MyComm
            .CommandText = "SavePIB"
            .CommandType = CommandType.StoredProcedure
            With .Parameters
                .Clear()
                .AddWithValue("ShipNo", sp)
                .AddWithValue("UserCT", UserData.UserCT)
                .AddWithValue("Detail", Detail)
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

        If hasil Then
            Insert_Pib_Respon = "Success"
        Else
            Insert_Pib_Respon = "Failed"
        End If
    End Function
    Private Function Update(ByVal str As String) As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        Dim hasil As Integer

        MyComm.CommandText = "RunSQL"
        MyComm.CommandType = CommandType.StoredProcedure

        MyComm.Parameters.Clear()
        MyComm.Parameters.AddWithValue("SQLStr", str)
        MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
        MyComm.Parameters.AddWithValue("Hasil", hasil)

        Try
            dr = MyComm.ExecuteReader()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        hasil = dr.FieldCount
        CloseMyReader(dr, UserData)

        If hasil Then
            Update = "Success"
        Else
            Update = "Failed"
        End If
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MsgBox(Global.System.Globalization.DateTimeFormatInfo.CurrentInfo.LongTimePattern)
        '%H %I %s'
    End Sub
End Class