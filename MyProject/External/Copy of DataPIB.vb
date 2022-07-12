Imports System.Data.OleDb
Imports System.Management
Imports System.Text.RegularExpressions

Public Class DataPIB
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, ErrMsg2, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Synchronize PIB"
    Dim mac1 As String

    Public Function DataOK(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return False
            End While
            CloseMyReader(MyReader, UserData)
        End If

        Return True
    End Function

    Sub New()
        mac1 = GetUserMACAddress()

        InitializeComponent()
        btnDelete.Enabled = False
        dt1.Checked = True
        dt1.Value = DateAdd(DateInterval.Day, -60, Now())
    End Sub

    Function GetUserMACAddress() As String
        Dim strQuery As String = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"
        Dim query As ManagementObjectSearcher = New ManagementObjectSearcher(strQuery)
        Dim queryCollection As ManagementObjectCollection = query.Get()
        Dim mo As ManagementObject

        GetUserMACAddress = ""
        For Each mo In queryCollection
            GetUserMACAddress = mo("MacAddress").ToString()
            GetUserMACAddress = Regex.Replace(GetUserMACAddress, ":", "-")
            Exit For
        Next
    End Function

    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()
        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select folder_name as DestinationFile from tbr_folderpib where trim(mac_address)=trim('" & mac1 & "')) as a")
        DataGridView1.Columns(0).Width = 400

        btnProcess.Enabled = False
        If dt1.Checked = True And DataGridView1.RowCount > 0 Then btnProcess.Enabled = True

        btnDelete.Enabled = False
        btnSave.Enabled = False
        txtFolder_Name.Clear()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbr_folderpib " & _
                 "where trim(folder_name)=trim('" & EscapeStr(txtFolder_Name.Text) & "') and trim(mac_address)=trim('" & mac1 & "')"

        ErrMsg = "Failed when deleting " & v_idtable & " data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        teks = "Save Data"

        If baru Then
            SQLstr = "Select * from tbr_folderpib where folder_name='" & EscapeStr(txtFolder_Name.Text) & "' and trim(mac_address)=trim('" & mac1 & "')"
            If DataOK(SQLstr) = False Then
                MsgBox("Folder Data already created! ", MsgBoxStyle.Critical, "Warning")
                Exit Sub
            End If

            ErrMsg = "Failed when saving Data"
            SQLstr = "INSERT INTO tbr_folderpib (folder_name, mac_address) " & _
                     "VALUES ('" & EscapeStr(txtFolder_Name.Text) & "','" & mac1 & "')"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input " & v_idtable & "  data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
        End If
    End Sub

    Private Sub txtFolder_Name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolder_Name.TextChanged
        btnSave.Enabled = (Len(Trim(txtFolder_Name.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txtFolder_Name.Text = DataGridView1.Item(0, brs).Value.ToString
        btnDelete.Enabled = (Len(Trim(txtFolder_Name.Text)) > 0)
        btnSave.Enabled = False
    End Sub
    Private Sub f_getdata()
        SQLstr = "select * from tbr_folderpib where folder_name = '" & txtFolder_Name.Text & "' and trim(mac_address)=trim('" & mac1 & "')"
        ErrMsg = "Failed when read File Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            txtFolder_Name.Text = ""

            While MyReader.Read
                Try
                    txtFolder_Name.Text = MyReader.GetString("folder_NAME")
                Catch ex As Exception
                End Try

            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
            Else
                baru = False
                edit = True
                txtFolder_Name.Enabled = False
            End If
            btnDelete.Enabled = (Len(Trim(txtFolder_Name.Text)) > 0)

            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub ProcessFile(ByVal FileName As String)
        Dim OleConn As OleDbConnection
        Dim OleReader As OleDbDataReader
        Dim RstQA, RstQB As DataTableReader

        Dim strsql As String
        Dim BCAJU, BCPIB, BCSPPB As String
        Dim xShipNo, xBL, xSUP, xSUPNM, xAJU, xPIB, xSPPB As String
        Dim xRESKD, xKPBC, xRESKRIP, xRESDES As String
        Dim xContNo, xContTipe, xCont, xContUnit As String
        Dim DCKD, DCNO As String
        Dim BCPIB_DT, BCSPPB_DT, xRESTG, DCDT As Date
        Dim iLoop, irec, xContTot As Integer

        ' Error Handling Variables
        Dim strTmp As String

        Dim FileNm As String
        FileNm = FileName

        OleConn = CekOleConn(FileNm, OleConn)

        SQLstr = "Select t1.shipment_no, t1.bl_no, t1.received_copydoc_dt, t1.supplier_code, m2.supplier_name, " _
         & "if(t1.aju_no is null, '', t1.aju_no ) aju_no, if(t1.pib_no is null, '', t1.pib_no) pib_no, if(t1.sppb_no is null, '', t1.sppb_no) sppb_no " _
         & "From tbl_shipping t1, tbm_supplier m2 " _
         & "Where t1.supplier_code=m2.supplier_code " _
         & "and received_copydoc_dt >= '" & Format(dt1.Value, "yyyy-MM-dd") & "'"

        ErrMsg = "Failed when read data"
        ErrMsg2 = "Failed when update data"
        RstQA = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not RstQA Is Nothing Then
            While RstQA.Read

                xShipNo = RstQA.GetInt32(0)
                xBL = RstQA.GetString(1)
                xSUP = RstQA.GetString(3)
                xSUPNM = RstQA.GetString(4)
                xAJU = RstQA.GetString(5)
                xPIB = RstQA.GetString(6)
                xSPPB = RstQA.GetString(7)

                'If xSPPB = "" Then
                '---Update AJU---
                BCAJU = ""

                OleReader = DBQueryOleReader("SELECT tblPibDok.CAR From tblPibDok " & _
                                             "Where (tblPibDok.DokKd = '705') And (tblPibDok.DokNo= '" & xBL & "')", OleConn, "", FileNm)

                If Not OleReader Is Nothing Then
                    If OleReader.HasRows Then
                        OleReader.Read()
                        BCAJU = OleReader.GetString(0)
                    End If
                End If
                OleReader.Close()

                If BCAJU <> "" Then

                    SQLstr = "Update tbl_shipping set aju_no='" & BCAJU & "' " _
                            & "where bl_no='" & xBL & "' and supplier_code = '" & xSUP & "'"

                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                    '---Update PIB---
                    OleReader = DBQueryOleReader("SELECT tblPibHdr.PibNo, tblPibHdr.PibTg FROM tblPibHdr " & _
                                                 "Where tblPibHdr.CAR = '" & BCAJU & "'", OleConn, "", FileNm)

                    If Not OleReader Is Nothing Then
                        If OleReader.HasRows Then
                            OleReader.Read()
                            Try
                                BCPIB = OleReader.GetString(0)
                                BCPIB_DT = OleReader.GetDateTime(1)
                            Catch ex As Exception
                                BCPIB = ""
                                BCPIB_DT = Nothing
                            End Try
                        End If
                    End If
                    OleReader.Close()

                    If BCPIB_DT <> Nothing Then
                        SQLstr = "Update tbl_shipping set pib_no='" & BCPIB & "', pib_dt = '" & Format(BCPIB_DT, "yyyy-MM-dd") & "' " _
                                 & "where aju_no='" & BCAJU & "'"

                        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                        '---Update SPPB ---
                        OleReader = DBQueryOleReader("SELECT tblPibRes.DOKRESNO, tblPibRes.DOKRESTG From tblPibRes " & _
                                                     "WHERE tblPibRes.RESKD='300' And tblPibRes.CAR = '" & BCAJU & "'", OleConn, "", FileNm)

                        If Not OleReader Is Nothing Then
                            If OleReader.HasRows Then
                                OleReader.Read()
                                Try
                                    BCSPPB = OleReader.GetString(0)
                                    BCSPPB_DT = OleReader.GetDateTime(1)
                                Catch ex As Exception
                                    BCSPPB = ""
                                    BCSPPB_DT = Nothing
                                End Try
                            End If
                        End If
                        OleReader.Close()

                        If BCSPPB_DT <> Nothing Then
                            SQLstr = "Update tbl_shipping set sppb_no='" & BCSPPB & "', sppb_dt = '" & Format(BCSPPB_DT, "yyyy-MM-dd") & "' " _
                                     & "where aju_no='" & BCAJU & "'"

                            affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                        End If
                    End If

                    '---Update Respon History---
                    irec = 0
                    SQLstr = "Select if(Max(ord_no) is null,0, Max(ord_no)) ord_no from tbl_pib_history " _
                           & "where aju_no='" & xAJU & "'"

                    RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                    If Not RstQB Is Nothing Then
                        While RstQB.Read
                            Try
                                irec = RstQB.GetValue(0)
                            Catch ex As Exception
                                irec = 0
                            End Try
                        End While
                    End If

                    strsql = "SELECT tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK,  tblPibRes.KPBC, tblTabel.URAIAN, tblPibRes.DesKripsi " & _
                             "FROM tblPibRes, tblTabel Where tblPibRes.RESKD = tblTabel.KDREC " & _
                             "And tblPibRes.CAR = '" & xAJU & "' " & _
                             "ORDER BY tblPibRes.CAR, tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK "

                    OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
                    iLoop = 1
                    If Not OleReader Is Nothing Then
                        While OleReader.Read()
                            If iLoop > irec Then
                                Try
                                    xRESKD = OleReader.GetString(0)
                                    xRESTG = OleReader.GetDateTime(1)
                                    xKPBC = OleReader.GetString(3)
                                    xRESKRIP = OleReader.GetString(4)
                                    If Trim(xRESKRIP) <> "" Then xRESKRIP = "[NOTES]: " & xRESKRIP & ""
                                    xRESDES = OleReader.GetString(5) & " " & xRESKRIP

                                Catch ex As Exception
                                    xRESKD = ""
                                    xRESTG = Nothing
                                    xKPBC = ""
                                    xRESKRIP = ""
                                    xRESDES = ""
                                End Try
                                If xRESTG <> Nothing Then
                                    SQLstr = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description, status_dt) " _
                                             & " values ('" & xAJU & "','" & iLoop & "','" & xKPBC & "','" & xRESKD & "','" & xRESDES & "','" & Format(xRESTG, "yyyy-MM-dd") & "')"

                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                End If
                            End If
                            iLoop = iLoop + 1
                        End While
                    End If
                    OleReader.Close()

                    '---Update Container---
                    strsql = "SELECT tblPibCon.ContNo, tblPibCon.ContUkur, tblPibCon.ContTipe " & _
                             "FROM tblPibCon Where tblPibCon.CAR = '" & xAJU & "' "

                    OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
                    iLoop = 1
                    If Not OleReader Is Nothing Then
                        While OleReader.Read()
                            Try
                                xContNo = OleReader.GetString(0)
                                xContTipe = OleReader.GetString(1) & "" & OleReader.GetString(2)
                            Catch ex As Exception
                                xContNo = ""
                                xContTipe = ""
                            End Try

                            If xContNo <> "" Then

                                SQLstr = "Select * from tbl_shipping_cont " & _
                                         "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""

                                RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                                If RstQB.HasRows Then
                                    SQLstr = "update tbl_shipping_cont set container_no='" & xContNo & "', unit_code='" & xContTipe & "' " _
                                           & "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""
                                Else
                                    SQLstr = "insert into tbl_shipping_cont (shipment_no, ord_no, container_no, unit_code) " _
                                           & "values (" & xShipNo & "," & iLoop & ",'" & xContNo & "','" & xContTipe & "')"
                                End If

                                affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)

                                iLoop = iLoop + 1
                            End If
                        End While

                        SQLstr = "SELECT SUM(1) as tot, unit_code FROM tbl_shipping_cont " & _
                                 "WHERE shipment_no=" & xShipNo & " GROUP BY unit_code"

                        RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)
                        If Not RstQB Is Nothing Then
                            While RstQB.Read
                                Try
                                    xContTot = RstQB.GetValue(0)
                                    xContUnit = RstQB.GetString(1)
                                Catch ex As Exception
                                    xContTot = 0
                                End Try
                            End While
                            If xContTot > 0 Then xCont = "," & xContTot & " x " & xContUnit
                            If xCont <> "" Then
                                xCont = Mid(xCont, 2, Len(xCont) - 1)
                                SQLstr = "Update tbl_shipping set total_container='" & xCont & "' where shipment_no='" & xShipNo & "'"
                                affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                            End If
                        End If
                    End If
                    OleReader.Close()

                    '---Update Supporting Documents---
                    strsql = "SELECT tblPibDok.DokKd, tblPibDok.DokNo, tblPibDok.DokTg " _
                            & "FROM tblPibDok Where tblPibDok.CAR = '" & xAJU & "' "

                    OleReader = DBQueryOleReader(strsql, OleConn, "", FileNm)
                    iLoop = 1
                    If Not OleReader Is Nothing Then
                        While OleReader.Read()
                            Try
                                DCKD = OleReader.GetString(0)
                                DCNO = OleReader.GetString(1)
                                DCDT = OleReader.GetDateTime(2)
                            Catch ex As Exception
                                DCKD = ""
                                DCNO = ""
                                DCDT = Nothing
                            End Try

                            If DCDT <> Nothing Then
                                SQLstr = "Select doc_code from tbm_document " & _
                                         "where doc_code like 'DC%' and refer_to='" & DCKD & "' "

                                RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                                If Not RstQB Is Nothing Then
                                    While RstQB.Read
                                        Try
                                            DCKD = RstQB.GetValue(0)
                                        Catch ex As Exception
                                            DCKD = "DC999"
                                        End Try
                                    End While
                                End If

                                If DCKD <> "" Then
                                    SQLstr = "Select * from tbl_doc_custom " & _
                                             "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""

                                    RstQB = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)

                                    If RstQB.HasRows Then
                                        SQLstr = "update tbl_doc_custom set doc_code='" & DCKD & "', doc_no='" & DCNO & "', doc_dt='" & Format(DCDT, "yyyy-MM-dd") & "', doc_remark='' " & _
                                                 "where shipment_no =" & xShipNo & " and ord_no =" & iLoop & ""
                                    Else
                                        SQLstr = "Insert into tbl_doc_custom (shipment_no, ord_no, doc_code, doc_no, doc_dt, doc_remark) " & _
                                                 "values (" & xShipNo & "," & iLoop & ",'" & DCKD & "','" & DCNO & "','" & Format(DCDT, "yyyy-MM-dd") & "','')"
                                    End If

                                    affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
                                    iLoop = iLoop + 1
                                End If
                            End If
                        End While
                    End If
                    OleReader.Close()
                End If
                'End If
            End While
        End If

Done:

        Exit Sub
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        For i = 0 To DataGridView1.RowCount - 1
            Call ProcessFile(DataGridView1.Item(0, i).Value.ToString)
        Next
        MsgBox("End Process ...", MsgBoxStyle.Information, v_idtable)
    End Sub

    Friend Function CekOleConn(ByVal FileName As String, ByVal OleConn As OleDbConnection) As OleDbConnection
        If OleConn Is Nothing Then
            Try
                OleConn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & "; Jet OLEDB:Database Password=MumtazFarisHana;")
                OleConn.Open()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
            End Try
        Else
            If OleConn.State = ConnectionState.Closed Then OleConn.Open()
        End If
        Return OleConn
    End Function

    Friend Sub CloseOleConn(ByVal OleConn As OleDbConnection)
        Try
            If Not OleConn Is Nothing Then
                If OleConn.State = ConnectionState.Open Then
                    OleConn.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
        End Try
    End Sub

    Friend Function DBQueryOleReader(ByVal SQLStr As String, ByVal OleConn As OleDbConnection, _
                                    ByVal ErrMsg As String, ByVal FileName As String) As OleDbDataReader

        OleConn = CekOleConn(FileName, OleConn)
        If OleConn Is Nothing Then Return Nothing

        Dim OleCmd As New OleDbCommand(SQLStr, OleConn)
        Try
            Dim OleReader As OleDbDataReader = OleCmd.ExecuteReader()
            Return OleReader
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & "SQL error " & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")

            Return Nothing
        End Try
    End Function

    Private Sub dt1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dt1.ValueChanged
        btnProcess.Enabled = False
        If dt1.Checked = True And DataGridView1.RowCount > 0 Then btnProcess.Enabled = True
    End Sub
End Class