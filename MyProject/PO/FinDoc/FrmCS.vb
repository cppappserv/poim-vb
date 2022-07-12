'Title                         : Transaksi CS
'Form                          : FrmCS
'Created By                    :  

'Stored Procedure Used (MySQL) : SaveCS

Imports System.Data.OleDb
Imports System.Management
Imports System.Text.RegularExpressions
Public Class FrmCS
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String
    Dim Ship, NONum, v_Rate, vuserid As String
    Dim MyReader, MyReader2 As MySqlDataReader
    Dim num As Integer
    Dim PilihanDlg As New DlgPilihan
    Dim MODULE_CODE As String
    Dim MODULE_NAME As String
    Dim ModCode As String
    Dim ErrMsg, SQLstr As String
    Dim affrow, spo As Integer
    Dim v_doccode, V_DOCNAME, BLStatus, BLNo As String
    Dim CSNo, ShipOrdNo As Integer
    Dim FinalApp As Boolean
    Dim StsAprovePo As String
    Dim txtsincronize, txtemkl, ypo As String
    Dim xpoitem As String
    Dim tanggal_sob As Date


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

    Private Function PIBSyncronize(ByVal str As String) As Boolean
        Dim MyConnT1, MyConnT2 As MySqlConnection
        Dim msg, val, teks, Errmsg, SQLstr, SQLstr2 As String
        Dim xshipment_no, affrow As Integer
        Dim xbl_no, xsupplier_code, xsupplier_name, xaju_no, xpib_no, xsppb_no, xhostbl, xvessel As String
        Dim xdokno, xreceived_copydoc_dt, xest_delivery_dt As String
        Dim xada, xord_no, iLoop As Integer
        Dim KeyBL, xCAR, xPibNo, xPibTg, xDOKRESNO, xDOKRESTG As String
        Dim xRESKD, xRESTG, xRESWK, xKPBC, xURAIAN, xDesKripsi As String
        Dim xContNo, xContUkur, xContTipe As String
        Dim xproses As Boolean

        SQLstr = "" & _
            " SELECT " & _
            "    t1.shipment_no, " & _
            "    t1.bl_no, " & _
            "    DATE_FORMAT(t1.received_copydoc_dt,'%Y-%m-%d') received_copydoc_dt, " & _
            "    t1.supplier_code, " & _
            "    m2.supplier_name, " & _
            "    COALESCE(t1.aju_no, '') aju_no, " & _
            "    COALESCE(t1.pib_no, '') pib_no, " & _
            "    COALESCE(t1.sppb_no, '') sppb_no, " & _
            "    t1.hostbl, " & _
            "    t1.vessel, " & _
            "    DATE_FORMAT(est_delivery_dt,'%Y-%m-%d') est_delivery_dt " & _
            " FROM tbl_shipping t1 " & _
            " INNER JOIN tbm_supplier m2 ON t1.supplier_code = m2.supplier_code " & _
            " WHERE t1.bl_no = '" & BLNo & "' " & _
            ""
        Errmsg = "Failed when read data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
        SQLstr2 = Nothing
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    xshipment_no = MyReader.GetDecimal(0)
                    xbl_no = ReplStr(MyReader.GetString(1))
                    xsupplier_code = ReplStr(MyReader.GetString(3))
                    xsupplier_name = ReplStr(MyReader.GetString(4))
                    xaju_no = ReplStr(MyReader.GetString(5))
                    xpib_no = ReplStr(MyReader.GetString(6))
                    xsppb_no = ReplStr(MyReader.GetString(7))
                    xhostbl = ReplStr(MyReader.GetString(8))
                    xvessel = ReplStr(MyReader.GetString(9))
                Catch ex As Exception
                    xshipment_no = 0
                    xbl_no = ""
                    xsupplier_code = ""
                    xsupplier_name = ""
                    xaju_no = ""
                    xpib_no = ""
                    xsppb_no = ""
                    xhostbl = ""
                    xvessel = ""
                End Try
                Try
                    xreceived_copydoc_dt = ReplStr(MyReader.GetString(2))

                Catch ex As Exception
                    xreceived_copydoc_dt = ""
                End Try
                Try
                    xest_delivery_dt = ReplStr(MyReader.GetString(10))
                Catch ex As Exception
                    xest_delivery_dt = ""
                End Try
            End While
        End If
        xada = 0
        MyReader.Close()
        SQLstr = "" & _
            " SELECT COUNT(*) ada, MAX(CAR) CAR, dokno " & _
            " FROM pib_tblPibDok WHERE cdokno = '" & BLNo & "' " & _
            " group by dokno "
        MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    xada = MyReader.GetDecimal(0)
                Catch ex As Exception
                    xada = 0
                End Try
                Try
                    xCAR = ReplStr(MyReader.GetString(1))
                Catch ex As Exception
                    xCAR = ""
                End Try
                Try
                    xdokno = ReplStr(MyReader.GetString(2))
                Catch ex As Exception
                    xdokno = ""
                End Try
            End While
        End If
        MyReader.Close()
        If xada = 1 Then SQLstr2 = "update pib_tblPibDok set flag = '1' where cdokno = '" & BLNo & "'"
        If xada > 1 Then
            SQLstr = "" & _
                " SELECT COALESCE(CAR, '') " & _
                " FROM pib_tblPibDok WHERE dokno = '" & xdokno & "' and DATE_FORMAT(doktg,'%Y-%m-%d') = '" & xest_delivery_dt & "' " & _
                ""
            MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
            If Not MyReader Is Nothing Then
                SQLstr2 = "update pib_tblPibDok set flag = '1' WHERE dokno = '" & xdokno & "' and DATE_FORMAT(doktg,'%Y-%m-%d') = '" & xest_delivery_dt & "' "
                While MyReader.Read
                    Try
                        xCAR = ReplStr(MyReader.GetString(0))
                    Catch ex As Exception
                        xCAR = 0
                    End Try
                End While
            End If
            MyReader.Close()
        End If
        KeyBL = "MASTER"
        If xCAR = "" Then
            KeyBL = "HOST"
            SQLstr = "" & _
                " SELECT COUNT(*) ada, MAX(CAR) CAR, max(dokno) dokno " & _
                " FROM pib_tblPibDok " & _
                " WHERE cDokNo = '" & xhostbl & "'" & _
                " group by dokno "

            MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        xada = MyReader.GetDecimal(0)
                    Catch ex As Exception
                        xada = 0
                    End Try
                    Try
                        xCAR = ReplStr(MyReader.GetString(1))
                    Catch ex As Exception
                        xCAR = ""
                    End Try
                    Try
                        xdokno = ReplStr(MyReader.GetString(2))
                    Catch ex As Exception
                        xdokno = ""
                    End Try
                End While
            End If
            MyReader.Close()
            If xada = 1 Then SQLstr2 = "update pib_tblPibDok set flag = '1' WHERE cDokNo = '" & xhostbl & "'"
            If xada > 1 Then
                SQLstr = "" & _
                    " SELECT CAR INTO xCAR " & _
                    " FROM pib_tblPibDok " & _
                    " WHERE DokNo = '" & xdokno & "' AND DATE_FORMAT(doktg,'%Y-%m-%d') = '" & xest_delivery_dt & "'" & _
                    ""
                MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
                If Not MyReader Is Nothing Then
                    SQLstr2 = "update pib_tblPibDok set flag = '1' WHERE DokNo = '" & xdokno & "' AND DATE_FORMAT(doktg,'%Y-%m-%d') = '" & xest_delivery_dt & "'"
                    While MyReader.Read
                        Try
                            xada = MyReader.GetDecimal(0)
                        Catch ex As Exception
                            xada = 0
                        End Try
                        Try
                            xCAR = ReplStr(MyReader.GetString(1))
                        Catch ex As Exception
                            xCAR = ""
                        End Try
                    End While
                End If
                MyReader.Close()
            End If
        End If
        If SQLstr2 = Nothing Then
            MsgBox("Data PIB belum terekam pada server ..., Jalankan proses Sincronize...", MsgBoxStyle.Information, "Record Data PIB tidak di temukan")
            Exit Function
        End If
        SQLstr = "" & _
            " UPDATE tbl_shipping SET aju_no = '" & xCAR & "' " & _
            " WHERE bl_no = '" & BLNo & "' AND supplier_code = "
        If KeyBL = "MASTER" Then
            SQLstr = SQLstr & "'" & xsupplier_code & "'"
        Else
            SQLstr = SQLstr & "'" & xhostbl & "'"
        End If
        affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)

        SQLstr = "" & _
            " SELECT PibNo, DATE_FORMAT(PibTg,'%Y-%m-%d') " & _
            " FROM pib_tblPibHdr WHERE CAR = '" & xCAR & "'"
        MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    xPibNo = ReplStr(MyReader.GetString(0))
                Catch ex As Exception
                    xPibNo = ""
                End Try
                Try
                    xPibTg = ReplStr(MyReader.GetString(1))
                Catch ex As Exception
                    xPibTg = ""
                End Try
            End While
        End If
        MyReader.Close()
        If xPibNo <> "" Then
            SQLstr = "" & _
            " UPDATE tbl_shipping SET pib_no = '" & xPibNo & "', pib_dt = '" & xPibTg & "'" & _
            " WHERE bl_no = "
            If KeyBL = "MASTER" Then
                SQLstr = SQLstr & "'" & xbl_no & "'"
            Else
                SQLstr = SQLstr & "'" & xhostbl & "'"
            End If
            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
        End If

        SQLstr = "" & _
            " SELECT DOKRESNO, DATE_FORMAT(DOKRESTG,'%Y-%m-%d') DOKRESTG " & _
            " FROM pib_tblPibRes " & _
            " WHERE RESKD='300' AND CAR = '" & xCAR & "'"
        MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    xDOKRESNO = ReplStr(MyReader.GetString(0))
                Catch ex As Exception
                    xDOKRESNO = ""
                End Try
                Try
                    xDOKRESTG = ReplStr(MyReader.GetString(1))
                Catch ex As Exception
                    xDOKRESTG = ""
                End Try
            End While
        End If
        MyReader.Close()
        If xDOKRESNO <> "" Then
            SQLstr = "" & _
                " UPDATE tbl_shipping SET sppb_no= '" & xDOKRESNO & "', sppb_dt = '" & xDOKRESTG & "'" & _
                " WHERE bl_no = "
            If KeyBL = "MASTER" Then
                SQLstr = SQLstr & "'" & xbl_no & "'"
            Else
                SQLstr = SQLstr & "'" & xhostbl & "'"
            End If
            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
        End If

        If xaju_no = "" Then xaju_no = xCAR
        If xaju_no <> "" Then
            SQLstr = "" & _
                " SELECT count(*) ada, MAX(ord_no) ord_no " & _
                " FROM tbl_pib_history" & _
                " WHERE aju_no = '" & xaju_no & "' "
            MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        xada = MyReader.GetDecimal(0)
                    Catch ex As Exception
                        xada = 0
                    End Try
                    Try
                        xord_no = MyReader.GetDecimal(1)
                    Catch ex As Exception
                        xord_no = 0
                    End Try
                End While
            End If
            MyReader.Close()

            SQLstr = "" & _
                " SELECT RESKD, DATE_FORMAT(RESTG,'%Y-%m-%d') RESTG, RESWK, KPBC, URAIAN, DesKripsi " & _
                " FROM pib_tblPibRes f1 " & _
                " inner join PIB_tblTabel f2 on f1.RESKD = f2.KDREC " & _
                " Where f1.CAR = '" & xaju_no & "' " & _
                " ORDER BY f1.CAR, f1.RESKD, f1.RESTG, f1.RESWK " & _
                ""
            MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
            If Not MyReader Is Nothing Then
                iLoop = 0
                While MyReader.Read
                    iLoop = iLoop + 1
                    If iLoop > xord_no Then
                        Try
                            xRESKD = ReplStr(MyReader.GetString(0))
                            xRESWK = ReplStr(MyReader.GetString(2))
                            xKPBC = ReplStr(MyReader.GetString(3))
                            xURAIAN = ReplStr(MyReader.GetString(4))
                            xDesKripsi = ReplStr(MyReader.GetString(5))
                        Catch ex As Exception
                            xRESKD = ""
                            xRESWK = ""
                            xKPBC = ""
                            xURAIAN = ""
                            xDesKripsi = ""
                        End Try
                        Try
                            xRESTG = ReplStr(MyReader.GetString(1))
                        Catch ex As Exception
                            xRESTG = Nothing
                        End Try
                        If xRESTG <> Nothing Then
                            If xURAIAN <> "" Then xURAIAN = "[NOTES]: " & xURAIAN & ""
                            xDesKripsi = xDesKripsi & " " & xURAIAN
                            If xRESWK <> "" Then
                                xRESWK = Mid(xRESWK, 1, 2) & ":" & Mid(xRESWK, 3, 2) & ":" & Mid(xRESWK, 5, 2)
                            Else
                                xRESWK = "00:00:00"
                            End If
                            xRESTG = xRESTG & " " & xRESWK
                            SQLstr = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description, status_dt) " _
                                     & " values ('" & xaju_no & "'," & iLoop & ",'" & xKPBC & "','" & xRESKD & "','" & xDesKripsi & "','" & xRESTG & "')"
                            affrow = DBQueryUpdate(SQLstr, MyConn1, False, Errmsg, UserData)
                        End If
                    End If
                End While
            End If
            MyReader.Close()
            SQLstr = "Call copy_pib_cont('" & xaju_no & "', " & xshipment_no & ")"
            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            'SQLstr = "" & _
            '    " SELECT ContNo, ContUkur, ContTipe " & _
            '    " FROM pib_tblPibCon " & _
            '    " Where CAR = '" & xaju_no & "' "
            'MyReader = DBQueryMyReader(SQLstr, MyConn, Errmsg, UserData)
            'If Not MyReader Is Nothing Then
            '    iLoop = 0
            '    While MyReader.Read
            '        Try
            '            xContNo = ReplStr(MyReader.GetString(0))
            '            xContUkur = ReplStr(MyReader.GetString(1))
            '            xContTipe = ReplStr(MyReader.GetString(2))
            '        Catch ex As Exception
            '            xContNo = ""
            '            xContUkur = ""
            '            xContTipe = ""
            '        End Try
            '        If xContNo <> "" Then
            '            iLoop = iLoop + 1
            '            SQLstr = "" & _
            '                " Select * from tbl_shipping_cont " & _
            '                " where shipment_no =" & xshipment_no & " and ord_no =" & iLoop & ""

            '            MyReader2 = DBQueryMyReader(SQLstr, MyConnT1, Errmsg, UserData)
            '            If MyReader2.HasRows Then
            '                SQLstr = "update tbl_shipping_cont set container_no='" & xContNo & "', unit_code='" & xContTipe & "' " _
            '                       & "where shipment_no =" & xshipment_no & " and ord_no =" & iLoop & ""
            '            Else
            '                SQLstr = "insert into tbl_shipping_cont (shipment_no, ord_no, container_no, unit_code) " _
            '                       & "values ('" & xshipment_no & "," & iLoop & ",'" & xContNo & "','" & xContTipe & "')"
            '            End If
            '            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            '        End If
            '    End While
            'End If
            'MyReader.Close()
            'SQLstr = "delete tbl_shipping_cont where shipment_no =" & xshipment_no & " and ord_no > " & iLoop & ""
            'affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
        End If
        If SQLstr2 <> Nothing Then
            affrow = DBQueryUpdate(SQLstr2, MyConn, False, Errmsg, UserData)
            txtsincronize = "1"
            Call ceklength()
            'If txtsincronize = "1" And (PunyaAkses("CS-P")) And txtemkl <> "" And spo >= 10 Then
            If txtsincronize = "1" And (PunyaAkses("CS-P")) And txtemkl <> "" Then
                btnSyn.Enabled = True
                If spo >= 10 Then
                    btnPrint.Enabled = (Status.Text <> "Rejected")
                    copytocsv.Enabled = (Status.Text <> "Rejected")
                End If
            End If

            btnCalc.Enabled = False
            SQLstr = "UPDATE tbl_shipping " & _
                     "SET sincronize='" & txtsincronize & "'" & _
                     " where SHIPMENT_NO='" & Ship & "' "
            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Function
            End If
        End If
    End Function

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

    Private Function ReplStr(ByVal Rstr As String) As String
        Dim temp As String

        temp = Replace(Rstr, "\", "/")
        Return temp
    End Function

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

    Sub New(ByVal No As Integer, ByVal ShipNo As Integer, ByVal ShipOrd As Integer, ByVal BLStat As String, ByVal BLNum As String)
        Dim lv_CTApp As Integer

        MODULE_CODE = "CS"
        MODULE_NAME = "Cost Slip "

        InitializeComponent()

        DTPrinted.Value = GetServerDate()
        approvedby.Text = ""
        financeappby.Text = ""
        CTApp.Text = ""
        CTFin.Text = ""
        AppDt.Value = GetServerDate()
        FinDT.Value = GetServerDate()
        AppDt.Checked = False 'GetServerDate()
        FinDT.Checked = False 'FinDT.Value(isnull) 'GetServerDate()
        FinalApp = False

        CSNo = No
        Ship = ShipNo
        ShipOrdNo = ShipOrd
        BLStatus = BLStat
        BLNo = BLNum


        'Call GetButtonAccess()

        fillcbPO()
        btnApprove.Enabled = False

        If Trim(No) <> 0 Then
            Call DisplayData()
            Call FillData()

            Call ceklength()

            If (btnPrint.Enabled) And (PunyaAkses("CS-P")) And txtemkl <> "" And spo >= 10 Then
                btnPrint.Enabled = True
            Else
                btnPrint.Enabled = False
            End If
            btnCalc.Enabled = False
            lv_CTApp = IIf(CTApp.Text = "", 0, CTApp.Text)
            If (Status.Text = "Approved") And (lv_CTApp = UserData.UserCT) Then
                btnApprove.Enabled = True
            Else
                btnApprove.Enabled = False
            End If
            If (Status.Text = "Final Approved") And (lv_CTApp = UserData.UserCT) Then
                btnReject.Enabled = True
            End If
        Else
            btnReject.Enabled = False
            btnApprove.Enabled = False
            btnPrint.Enabled = False
            NONum = "0"
            crt.Text = AmbilData("NAME", "tbm_users", "user_ct='" & UserData.UserCT & "'")
            crtdt.Text = GetServerDate.ToString
            Me.Text = MODULE_NAME & "- New"
        End If




        Call GetButtonAccess()
    End Sub

    Private Sub ceklength()
        If xaju_no = "" Then
            spo = "1"
        Else
            SQLstr = "SELECT MIN(LENGTH(TRIM(IF(ada=0,'1111111111111',a.container_no)))) pjng FROM tbl_shipping_cont a CROSS JOIN (SELECT COUNT(*) ada FROM pib_tblPibCon WHERE car = '" & xaju_no & "') b  WHERE shipment_no =  '" & Ship & "' and unit_code IN ('20F','40F')"
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        spo = ReplStr(MyReader.GetString(0))
                    Catch ex As Exception
                        spo = "15"
                    End Try
                End While
            End If
            MyReader.Close()
        End If
    End Sub
    Private Sub GetButtonAccess()
        Dim SQLStr, ModCode As String
        ModCode = "CS-C"

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        btnSave.Enabled = (DataExist(SQLStr) = True)
        If CTCrt.Text <> "" Then
            If CTApp.Text <> "" Then
                btnSave.Enabled = (btnSave.Enabled) And ((CTCrt.Text = UserData.UserCT) Or (CTApp.Text = UserData.UserCT)) And (f_getenable(Status.Text))
            Else
                btnSave.Enabled = (btnSave.Enabled) And (CTCrt.Text = UserData.UserCT) And (f_getenable(Status.Text))
            End If
        End If

        btnReject.Enabled = btnSave.Enabled
        If (CTApp.Text <> "" And Not (btnReject.Enabled)) Then btnReject.Enabled = ((Status.Text = "Final Approved") And (CTApp.Text = UserData.UserCT))

        ModCode = "CS-P"

        SQLStr = "Select tu.user_ct,tu.Name,tu.user_id from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & ModCode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT




        If txtsincronize = "0" Then
            btnPrint.Enabled = False
            copytocsv.Enabled = False
            btnSyn.Enabled = (Status.Text <> "Rejected")
        Else
            Call ceklength()
            btnPrint.Enabled = (DataExist(SQLStr) = True And txtemkl <> "" And spo >= 10)
            If btnPrint.Enabled Then
                btnPrint.Enabled = (Status.Text <> "Rejected")
                copytocsv.Enabled = (Status.Text <> "Rejected")
            End If
        End If
    End Sub

    Private Sub DisplayData()
        Dim strSQL, errMSg As String
        Dim TEMP As String = ""
        Dim tempExp As String = ""
        Dim tempPO As String

        Me.Text = "CS #" & CSNo
        errMSg = "CS data view failed"

        NONum = num.ToString
        strSQL = "SELECT DISTINCT t1.*, if(t2.po_no is null,'', t2.po_no) po_no, t2.po_item, m1.material_name FROM TBL_SHIPPING_DOC t1 " & _
                 "LEFT JOIN tbl_shipping_detail t2 ON t1.findoc_no=t2.po_no AND t1.findoc_reff=t2.po_item " & _
                 "LEFT JOIN tbm_material m1 ON t2.material_code=m1.material_code " & _
                 "WHERE t1.SHIPMENT_NO = '" & Ship & "' AND " & _
                 "t1.ORD_NO = '" & CSNo & "' AND t1.FINDOC_TYPE = '" & MODULE_CODE & "' and t1.FINDOC_GROUPTO = 'FIN'"
        ' new supram
        strSQL = "SELECT DISTINCT t1.*, if(t2.po_no is null,'', t2.po_no) po_no, t2.po_item, m1.material_name,  COALESCE(h1.sincronize,'0')  sincronize, coalesce(h1.EMKL_CODE) EMKL_CODE " & _
                 "FROM TBL_SHIPPING_DOC t1 " & _
                 "LEFT JOIN tbl_shipping_detail t2 ON t1.findoc_no = t2.po_no AND t1.findoc_reff = t2.po_item " & _
                 "LEFT JOIN tbm_material m1 ON t2.material_code = m1.material_code " & _
                 "INNER JOIN TBL_SHIPPING h1 ON h1.shipment_no = t1.shipment_no " & _
                 "WHERE t1.SHIPMENT_NO = '" & Ship & "' AND " & _
                 "t1.ORD_NO = '" & CSNo & "' AND t1.FINDOC_TYPE = '" & MODULE_CODE & "' and t1.FINDOC_GROUPTO = 'FIN'"

        '        SELECT MIN(LENGTH(TRIM(IF(ada=0,'1111111111111',a.container_no))))
        'FROM tbl_shipping_cont  a 
        'CROSS JOIN (SELECT COUNT(*) ada FROM pib_tblPibCon WHERE car = '07000000026820191025002115') b 
        'WHERE a.shipment_no =  '18886';


        approvedby.ReadOnly = True
        crt.ReadOnly = True
        AppDt.Enabled = False
        crtdt.ReadOnly = True
        FinDT.Enabled = False
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

                    txtpersen.Text = MyReader.GetString("FINDOC_VALPRC")
                    txtsincronize = MyReader.GetString("SINCRONIZE")
                    txtemkl = MyReader.GetString("EMKL_CODE")
                    spo = MyReader.GetString("FINDOC_NO")
                    sp_ord_no = MyReader.GetString("ORD_NO")
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
                    FinDT.Checked = False
                End Try
                If CTFin.Text <> "" Then
                    FinDT.Value = MyReader.GetString("FINDOC_FINAPPDT")
                    FinDT.Checked = True
                End If
                Status.Text = MyReader.GetString("FINDOC_STATUS")
                sp_ord_no = MyReader.GetString("ORD_NO")

                If Trim(MyReader.GetString("po_no")) <> "" Then
                    tempPO = Trim(MyReader.GetString("po_no")) & " [" & Trim(MyReader.GetString("po_item")) & ":" & Trim(MyReader.GetString("material_name")) & "]"
                Else
                    tempPO = ""
                End If

            End While
            CloseMyReader(MyReader, UserData)
            cbPO.Text = tempPO

            Call ceklength()

            btnSave.Text = "Update"
            'btnSave.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            btnSave.Enabled = (btnSave.Enabled And Not (Status.Text = "Rejected"))
            If btnReject.Enabled = True Then
                btnReject.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            End If
            'txtsincronize = "1"

            If txtsincronize = "1" And (PunyaAkses("CS-P")) And txtemkl <> "" Then
                btnSyn.Enabled = True
            End If

            If txtsincronize = "1" And (PunyaAkses("CS-P")) And txtemkl <> "" And spo >= 10 Then
                'supram
                btnPrint.Enabled = (Status.Text <> "Rejected")
                copytocsv.Enabled = (Status.Text <> "Rejected")
            Else
                'btnSyn.Enabled = False
                btnPrint.Enabled = False
                copytocsv.Enabled = False
            End If

            btnCalc.Enabled = False

            If (CTApp.Text <> "" And btnReject.Enabled) Then
                btnApprove.Enabled = True
            End If

            cbPO.Enabled = False
            txtpersen.ReadOnly = True

            dgvcost.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")

            DTPrinted.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            txtOthers.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")
            txtPIB.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")
            txtDesc.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")
            approvedby.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved")
            financeappby.ReadOnly = (Status.Text <> "Open" And Status.Text <> "Approved" And Status.Text <> "Final Approved")
            AppDt.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            'FinDT.Enabled = (Status.Text = "Open" Or Status.Text = "Approved")
            FinDT.Enabled = Not (Status.Text = "Rejected")
            Button3.Visible = (Status.Text = "Open" Or Status.Text = "Approved")
            'Button4.Visible = (Status.Text = "Open" Or Status.Text = "Approved")
            Button4.Visible = Not (Status.Text = "Rejected")
            If CTApp.Text <> "" Then
                approvedby.Text = GetName2(CTApp.Text)
            End If
            If CTFin.Text <> "" Then
                financeappby.Text = GetName2(CTFin.Text)
            End If
            crt.Text = GetName2(CTCrt.Text)
            txtbea.Text = ""
            Me.Text = MODULE_NAME & "# " & CSNo

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
            ModCode = "CS-A"
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
            ModCode = "CS-A"
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
        Dim lv_curr, counter As String
        Dim POStr, PONo, POOrd, TotAmount, TotPrc As String
        Dim num1 As Decimal
        Dim i As Integer

        If CekData() = False Then Exit Sub
        Try
            DTPrinted1 = Format(DTPrinted.Value, "yyyy-MM-dd")
            AppDt1 = Format(AppDt.Value, "yyyy-MM-dd")
            FinDt1 = Format(FinDT.Value, "yyyy-MM-dd")

            PONo = txtPO.Text
            POOrd = txtPOItem.Text

            TotAmount = GetNum(txttotal.Text)
            TotAmount = GetNum2(TotAmount)
            TotPrc = GetNum(txtpersen.Text)
            TotPrc = GetNum2(TotPrc)

            'isi cost2 yang fixed
            lv_cost_ord = Mid(1 & "           ", 1, 11)
            lv_cost_code = Mid("90001" & "     ", 1, 5)
            lv_cost_desc = Mid("Invoice Amount" & Space(40), 1, 40)

            lv_cost_amount = GetNum(txtcost.Text)
            lv_cost_amount = GetNum2(lv_cost_amount)

            lv_cost_amount = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_curr = Mid("IDR" & "     ", 1, 5)
            lv_cost_unit = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_cost_vat = Mid("0" & "                 ", 1, 17)
            insertStr &= lv_cost_ord & lv_cost_code & lv_cost_desc & lv_cost_amount & lv_curr & lv_cost_unit & lv_cost_vat & ";"

            lv_cost_ord = Mid(2 & "           ", 1, 11)
            lv_cost_code = Mid("90002" & "     ", 1, 5)
            lv_cost_desc = Mid("Protein Deficiency" & Space(40), 1, 40)

            lv_cost_amount = GetNum(txtdeficiency.Text)
            lv_cost_amount = GetNum2(lv_cost_amount)

            lv_cost_amount = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_curr = Mid("IDR" & "     ", 1, 5)
            lv_cost_unit = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_cost_vat = Mid("0" & "                 ", 1, 17)
            insertStr &= lv_cost_ord & lv_cost_code & lv_cost_desc & lv_cost_amount & lv_curr & lv_cost_unit & lv_cost_vat & ";"

            'isi cost2 variable
            dgvcost.CommitEdit(DataGridViewDataErrorContexts.Commit)
            counter = 0
            For i = 0 To dgvcost.RowCount - 1
                ErrMsg = "Failed to update CS detail data."
                If dgvcost.Rows(i).Cells("ItemCost").Value Is Nothing Then
                Else
                    lv_cost_ord = i + 3
                    lv_cost_code = dgvcost.Rows(i).Cells("ItemCost").Value.ToString
                    lv_cost_desc = dgvcost.Rows(i).Cells("Description").Value.ToString

                    lv_cost_amount = GetNum(dgvcost.Rows(i).Cells("Amount").Value.ToString)
                    lv_cost_amount = GetNum2(lv_cost_amount)

                    lv_curr = dgvcost.Rows(i).Cells("Currency").Value.ToString

                    lv_cost_unit = GetNum(dgvcost.Rows(i).Cells("Rate").Value.ToString)
                    lv_cost_unit = GetNum2(lv_cost_unit)

                    lv_cost_vat = "0"

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

            'isi cost2 yang fixed untuk Bank
            lv_cost_ord = Mid(i + 2 & "           ", 1, 11)
            lv_cost_code = Mid("80001" & "     ", 1, 5)
            lv_cost_desc = Mid("Others" & Space(40), 1, 40)

            lv_cost_amount = GetNum(txtOthers.Text)
            lv_cost_amount = GetNum2(lv_cost_amount)
            lv_cost_amount = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_curr = Mid("IDR" & "     ", 1, 5)
            lv_cost_unit = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_cost_vat = Mid(0 & "                 ", 1, 17)
            insertStr &= lv_cost_ord & lv_cost_code & lv_cost_desc & lv_cost_amount & lv_curr & lv_cost_unit & lv_cost_vat & ";"

            lv_cost_ord = Mid(i + 3 & "           ", 1, 11)
            lv_cost_code = Mid("80002" & "     ", 1, 5)
            lv_cost_desc = Mid("Cost of PIB" & Space(40), 1, 40)

            lv_cost_amount = GetNum(txtPIB.Text)
            lv_cost_amount = GetNum2(lv_cost_amount)
            lv_cost_amount = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_curr = Mid("IDR" & "     ", 1, 5)
            lv_cost_unit = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_cost_vat = Mid(0 & "                 ", 1, 17)
            insertStr &= lv_cost_ord & lv_cost_code & lv_cost_desc & lv_cost_amount & lv_curr & lv_cost_unit & lv_cost_vat & ";"

            lv_cost_ord = Mid(i + 4 & "           ", 1, 11)
            lv_cost_code = Mid("80003" & "     ", 1, 5)
            lv_cost_desc = Mid("Descrepancy" & Space(40), 1, 40)

            lv_cost_amount = GetNum(txtDesc.Text)
            lv_cost_amount = GetNum2(lv_cost_amount)
            lv_cost_amount = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_curr = Mid("IDR" & "     ", 1, 5)
            lv_cost_unit = Mid(lv_cost_amount & "                 ", 1, 17)
            lv_cost_vat = Mid(0 & "                 ", 1, 17)
            insertStr &= lv_cost_ord & lv_cost_code & lv_cost_desc & lv_cost_amount & lv_curr & lv_cost_unit & lv_cost_vat & ";"


            If MODULE_CODE = "CS" Then
                If btnSave.Text = "Save" Then
                    SQLStr = "Run Stored Procedure SaveCS (Save," & Ship & ",'CS'," & PONo & "," & POOrd & "," & TotPrc & "," & TotAmount & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & crt.Text & "," & crtdt.Text & "," & insertStr & ")"
                    keyprocess = "Save"
                ElseIf btnSave.Text = "Update" Then
                    SQLStr = "Run Stored Procedure SaveCS (Updt," & Ship & ",'CS'," & PONo & "," & POOrd & "," & TotPrc & "," & TotAmount & "," & DTPrinted1 & "," & CTApp.Text & "," & AppDt1 & "," & crt.Text & "," & crtdt.Text & "," & insertStr & ")"
                    keyprocess = "Updt"
                End If
            End If
            MyComm.CommandText = "SaveCS"

            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", Ship)
            MyComm.Parameters.AddWithValue("PONo", PONo)
            MyComm.Parameters.AddWithValue("POOrd", POOrd)
            MyComm.Parameters.AddWithValue("TotAmount", TotAmount)
            MyComm.Parameters.AddWithValue("TotPrc", TotPrc)
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
            If CTFin.Text = "" Then
                MyComm.Parameters.AddWithValue("FinBy", DBNull.Value)
                MyComm.Parameters.AddWithValue("FinDt", DBNull.Value)
            Else
                MyComm.Parameters.AddWithValue("FinBy", CTFin.Text)
                MyComm.Parameters.AddWithValue("FinDt", FinDt1)
            End If
            MyComm.Parameters.AddWithValue("Remark", "")
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

    Private Sub btnReject_AvailableChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReject.AvailableChanged

    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, val, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        If Status.Text = "Final Approved" Then
            msg = "Approval in cancel"
            val = "Approved"
        Else
            msg = "Reject"
            val = "Rejected"
        End If
        msg = msg & " " & MODULE_NAME & "#" & NONum & " of " & Ship & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_shipping_doc " & _
                     "SET FINDOC_STATUS='" & val & "'" & _
                     " where SHIPMENT_NO='" & Ship & "' " & _
                     " and ord_no=" & CSNo & "" & _
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
        Dim objek As New frmPilih
        Dim v_num As String

        If Len(CStr(CSNo)) = 1 Then
            v_num = " " & CSNo
        Else
            v_num = CSNo.ToString
        End If
        If MODULE_CODE = "CS" Then
            xto_sap2 = "CS"
            'ViewerFrm.Tag = "CSCS" & v_num & Ship
            objek.Tag() = "CSCS" & v_num & Ship
            objek.ShowDialog()
        Else
            ViewerFrm.Tag = "CSCS" & v_num & Ship
            ViewerFrm.ShowDialog()
        End If
        'ViewerFrm.ShowDialog()
    End Sub

    Private Sub refreshGrid()
        Dim in_field, infield, Vcost_code, v_no, v_ord As String
        Dim in_tbl As String = ""
        Dim dts As DataTable
        Dim sumAmount, sumVAT, sumTotal As Double

        in_field = "cost_ord_no as No, cost_code as ItemCost, cost_description as Description, currency_code as currency, cost_amount as Amount, cost_unit as Rate, cost_vat as Vat"
        in_tbl = "tbl_cost"
        SQLstr = "SELECT " & in_field & " from " & in_tbl & " where shipment_no = '" & v_no & "' and ship_ord_no = '" & v_ord & "' and type_code = 'CS'"
        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        dgvcost.DataSource = dts
        'Show_Grid_JoinTable(DGVDetail, in_field, in_tbl)
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
            dgvcost.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            dgvcost.Columns(3).DefaultCellStyle.Format = "N2"
            'get total
            sumAmount = AmbilData("sum(cost_amount)", "tbl_cost", "shipment_no = '" & v_no & "' and ship_ord_no = '" & v_ord & "' and type_code = 'CS'")
            sumVAT = AmbilData("sum(cost_vat)", "tbl_cost", "shipment_no = '" & v_no & "' and ship_ord_no = '" & v_ord & "' and type_code = 'CS'")
            sumTotal = sumAmount + sumVAT
        End If






    End Sub


    Private Sub btnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalc.Click
        Dim strSQL, errMSg As String
        Dim RstQa, RstQb As DataTableReader
        Dim tempVar As String()
        Dim xPos, xCek, xTot, b_POItem, b_PlantGrp As Integer
        Dim v_Str As String
        Dim v_costcat_code, v_costcat_name, v_costcat_collect, v_costcat_ref, v_currency As String
        Dim V_REFVAL, V_REFVALIDR, v_sub1, v_sub2, v_qty, v_total As Double
        Dim V_REFSTR, V_REFTYPE As String

        If cbPO.Text = "" Then
            MsgBox("Please select PO No. first! ", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        End If

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

        'get subgroup
        strSQL = "SELECT GROUP_CONCAT(subgroup_name SEPARATOR ', ') bea_name " & _
                 "FROM tbm_costcategory_subgroup t2 WHERE t2.group_code='00002' " & _
                 "GROUP BY t2.group_code"

        errMSg = "Failed when read data"
        RstQa = DBQueryDataReader(strSQL, MyConn, errMSg, UserData)
        If Not RstQa Is Nothing Then
            While RstQa.Read
                Try
                    txtbea.Text = RstQa.GetString(0)
                Catch ex As Exception
                    txtbea.Text = ""
                End Try
            End While
        End If

        v_sub2 = 0
        xCek = 0
        strSQL = "Select t1.costcat_code, t1.costcat_name, t1.subgroup_code, t1.collected, " & _
                 "if(t1.reference_field IS NULL,'',t1.reference_field) AS reference_field, t1.vat, " & _
                 "if(t1.currency_code IS NULL,'',t1.currency_code) currency_code From tbm_costcategory t1, tbm_costcategory_subgroup t2 " & _
                 "Where t1.subgroup_code= t2.subgroup_code and t2.group_code='00002' and t1.active=1 " & _
                 "order by t1.subgroup_code, t1.costcat_name"

        errMSg = "Failed when read data"
        RstQa = DBQueryDataReader(strSQL, MyConn, errMSg, UserData)

        While RstQa.Read
            v_costcat_code = RstQa.GetString(0)
            v_costcat_name = RstQa.GetString(1)
            v_costcat_collect = RstQa.GetString(3)
            v_costcat_ref = RstQa.GetString(4)
            v_currency = RstQa.GetString(6)
            If v_currency = "" Then
                v_currency = txtcurr.Text
            End If

            If xCek = 0 Then 'declare Cost pertama memiliki detail
                txtbea0.Text = v_costcat_name
                xCek = 1
            End If

            V_REFSTR = "Y"
            V_REFVAL = 0

            If v_costcat_ref <> "" Then

                xPos = InStr(v_costcat_ref, ".")
                If xPos > 0 Then
                    v_Str = Mid(v_costcat_ref, (xPos + 1), Len(v_costcat_ref) - xPos)
                    v_Str = Replace(v_Str, ":shipment_no", Ship)

                    v_Str = Replace(v_Str, ":po_no", "'" & txtPO.Text & "'")
                    v_Str = Replace(v_Str, ":po_item", "'" & txtPOItem.Text & "'")

                    b_PlantGrp = InStr(v_Str, ":plant_code")
                    v_Str = Replace(v_Str, ":plant_code", "'" & txtPlant.Text & "'")
                    v_Str = Replace(v_Str, ":group_code", "'" & txtGrpMaterial.Text & "'")

                    strSQL = v_Str
                    RstQb = DBQueryDataReader(strSQL, MyConn, errMSg, UserData)
                    While RstQb.Read
                        Try
                            V_REFVAL = RstQb.GetValue(0)
                        Catch ex As Exception
                            V_REFVAL = 0
                        End Try
                    End While

                    If b_PlantGrp > 0 Then
                        'if base on plant-material group dari yg harusnya per PO per Item
                        V_REFVAL = V_REFVAL * (txtpgcost.Text / txttotcost.Text)
                    Else
                        V_REFVAL = V_REFVAL * (txtcost.Text / txtnettotcost.Text)
                    End If

                Else
                    strSQL = "Select " & v_costcat_ref & " From tbl_shipping where shipment_no='" & Ship & "'"

                    RstQb = DBQueryDataReader(strSQL, MyConn, errMSg, UserData)
                    While RstQb.Read
                        Try
                            If RstQb.IsDBNull(0) Then
                                V_REFSTR = "N"
                            Else
                                V_REFTYPE = RstQb.Item(0).GetType.ToString
                                If V_REFTYPE = "System.DateTime" Then
                                    V_REFSTR = "Y"
                                ElseIf V_REFTYPE = "System.String" Then
                                    V_REFSTR = RstQb.GetString(0)
                                Else
                                    V_REFVAL = RstQb.GetValue(0)
                                End If
                            End If
                        Catch ex As Exception
                            V_REFSTR = "N"
                            V_REFVAL = 0
                        End Try
                    End While
                    V_REFVAL = V_REFVAL * (txtcost.Text / txtnettotcost.Text)
                End If
            End If

            'effective Maret 4, 2011
            V_REFVAL = Math.Round(V_REFVAL, 2)
            '-----------------------

            If v_currency <> "IDR" Then
                V_REFVALIDR = V_REFVAL * txtrate.Text
            Else
                V_REFVALIDR = V_REFVAL
            End If
            v_sub2 = v_sub2 + V_REFVALIDR

            'STORE KE GRID
            tempVar = New String() {v_costcat_code, v_costcat_name, v_currency, FormatNumber(V_REFVAL, 2), FormatNumber(V_REFVALIDR, 2)}
            dgvcost.Rows.Add(tempVar)
        End While

        v_qty = CDbl(txtqty.Text)

        'v_sub1 = (CDbl(txtcost.Text) + CDbl(txtdeficiency.Text))
        v_sub1 = CDbl(txtcost.Text)
        v_sub1 = ((v_sub1 * v_Rate) / (v_qty * (txtpersen.Text / 100))) / txttokgs.Text
        If v_sub1 < 0 Then v_sub1 = 0
        txtSub1.Text = FormatNumber(v_sub1, 2)

        Call Hitung_Total()

    End Sub

    Private Sub Hitung_Total()
        Dim v_sub2, v_qty, v_total As Double

        dgvcost.CommitEdit(DataGridViewDataErrorContexts.Commit)
        For i = 0 To dgvcost.RowCount - 1
            If dgvcost.Rows(i).Cells("ItemCost").Value Is Nothing Then
            Else
                v_sub2 = v_sub2 + CDbl(dgvcost.Rows(i).Cells("Amount").Value.ToString)
            End If
        Next
        v_qty = CDbl(txtqty.Text)

        v_sub2 = (v_sub2 / (v_qty * (txtpersen.Text / 100))) / txttokgs.Text
        txtSub2.Text = FormatNumber(v_sub2, 2)

        v_total = txtSub1.Text + v_sub2
        txttotal.Text = FormatNumber(v_total, 2)

        dgvcost.Rows.Item(0).Cells.Item(3).ReadOnly = True
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetName("Fin")
        FinDT.Checked = True
    End Sub

    Private Sub fillcbPO()
        Dim strSQL, errMSg, temp As String
        strSQL = "SELECT t1.po_no, t1.po_item, m1.material_name " & _
                 "FROM tbl_shipping_detail t1, tbm_material m1 WHERE t1.material_code=m1.material_code " & _
                 "AND t1.shipment_no='" & Ship & "' AND CONCAT(trim(t1.po_no), trim(t1.po_item)) NOT IN " & _
                 "  (SELECT CONCAT(trim(findoc_no),trim(findoc_reff)) FROM tbl_shipping_doc sd " & _
                 "   WHERE findoc_type='CS' AND trim(findoc_no) <> '' AND trim(findoc_reff) <> '' AND findoc_status <> 'Rejected' AND sd.shipment_no=t1.shipment_no) "

        errMSg = "Failed when read PO data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        cbPO.Refresh()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = Trim(MyReader.GetString("po_no")) & " [" & Trim(MyReader.GetString("po_item")) & ":" & Trim(MyReader.GetString("material_name")) & "]"
                    cbPO.Items.Add(temp)

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        txtCont.Text = "Curah"
        strSQL = "SELECT unitgroup, GROUP_CONCAT(unit SEPARATOR ', ') unit FROM " & _
                 "(SELECT t1.shipment_no, MAX(type_name) unitgroup, CONCAT(SUM(1), ' x ',m1.unit_name) unit FROM tbl_shipping_cont t1, tbm_unit m1, tbm_unit_type m2 " & _
                 "WHERE t1.unit_code=m1.unit_code AND m1.type_code=m2.type_code AND shipment_no='" & Ship & "' GROUP BY t1.unit_code) t1 GROUP BY shipment_no"

        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtCont.Text = MyReader.GetString("unitgroup")
                    txtContSize.Text = MyReader.GetString("unit")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub dgvcost_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvcost.CellEndEdit
        Dim lv_rate, lv_curr As String

        '---clear the error message---
        dgvcost.Rows(e.RowIndex).ErrorText = String.Empty
        If dgvcost.Columns(e.ColumnIndex).Name = "Rate" Then
            lv_rate = dgvcost.Rows(e.RowIndex).Cells(3).Value
            lv_curr = Trim(dgvcost.Rows(e.RowIndex).Cells(2).Value.ToString)
            If lv_curr <> "IDR" Then
                lv_rate = lv_rate * v_Rate
            End If

            dgvcost.Rows(e.RowIndex).Cells(4).Value = FormatNumber(lv_rate, 2)

            Call Hitung_Total()
        End If

    End Sub

    Private Sub FillData()
        Dim strSQL, strSQL2, SQLstr, errMSg As String
        Dim tempVar As String()
        Dim xCek, xTot, xPos As Integer
        Dim POStr, v_Str, mpo_item, vpo_no, vwessel, vpackinglist, infield, Vcost_code As String
        Dim vrate, vrate2 As Double
        Dim v_costcat_code, v_costcat_name, v_currency, pricelist, ikurs As String
        Dim v_amount, v_amountidr, v_sub1, v_sub2, v_qty, v_total, vvat As Double
        Dim V_valuedate, v_due_date As Date
        'Dim v_invoice, v_invoicefinal As Double

        If cbPO.Text = "" Then
            MsgBox("Please select PO No. first! ", MsgBoxStyle.Critical, "Warning")
            Exit Sub
        End If
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

        'get data shipment detail
        POStr = cbPO.Text
        If POStr <> "" Then
            txtPO.Text = Mid(POStr, 1, InStr(POStr, "[") - 2)
            txtPOItem.Text = Mid(POStr, InStr(POStr, "[") + 1, InStr(POStr, ":") - InStr(POStr, "[") - 1)
        End If
        strSQL = "SELECT MAX(t1.finalty) finalty, SUM(t2.invoice_amount-t2.invoice_penalty) totinvoice FROM tbl_shipping t1, tbl_shipping_invoice t2 " & _
                 "Where t1.shipment_no=t2.shipment_no and t1.shipment_no='" & Ship & "' GROUP BY t1.shipment_no"

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            xCek = 0
            While MyReader.Read
                Try
                    txtfinalty.Text = MyReader.GetString("finalty")
                Catch ex As Exception
                    txtfinalty.Text = 0
                End Try
                Try
                    txttotcost.Text = MyReader.GetString("totinvoice")
                    txtnettotcost.Text = txttotcost.Text - txtfinalty.Text
                Catch ex As Exception
                    txttotcost.Text = 0
                    txtnettotcost.Text = 0
                End Try
            End While
        End If
        CloseMyReader(MyReader, UserData)
        ' (coalesce(t0.due_dt due_date,''))

        strSQL = "SELECT t1.*, IF(m0.effective_kurs IS NULL,0,m0.effective_kurs) kurs FROM " & _
                 "(SELECT t0.EST_DELIVERY_DT, t0.currency_code," & _
                 "m1.currency_name, " & _
                 "t1.quantity, t2.unit_code, t2.price, t3.invoice_origin, if((t3.invoice_amount-t3.invoice_penalty) > t3.invoice_origin,0,(t3.invoice_origin - (t3.invoice_amount-t3.invoice_penalty))) deficiency, (t3.invoice_amount-t3.invoice_penalty) invoice_amount, m2.group_code, t4.plant_code " & _
                 "FROM tbl_shipping t0, tbl_shipping_detail t1, tbl_po_detail t2, tbl_shipping_invoice t3, tbl_po t4, tbm_currency m1, tbm_material m2 " & _
                 "WHERE t0.shipment_no=t1.shipment_no AND (t1.po_no = t2.po_no And t1.po_item = t2.po_item) AND " & _
                 "(t1.shipment_no=t3.shipment_no AND t1.po_no=t3.po_no AND t1.po_item=t3.ord_no) AND " & _
                 "t0.currency_code=m1.currency_code AND t2.material_code=m2.material_code AND t2.po_no = t4.po_no AND " & _
                 "t1.shipment_no='" & Ship & "' AND t1.po_no='" & txtPO.Text & "' AND t1.po_item='" & txtPOItem.Text & "') t1 " & _
                 "LEFT JOIN tbm_kurs m0 ON m0.currency_code=t1.currency_code AND m0.effective_date=t1.EST_DELIVERY_DT "
        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtqty.Text = MyReader.GetString("quantity")
                    txtqty.Text = FormatNumber(txtqty.Text, 5)
                    txtunitqty.Text = MyReader.GetString("unit_code")
                    txtunitprice.Text = MyReader.GetString("price")
                    txtunitprice.Text = FormatNumber(txtunitprice.Text, 2)
                    pricelist = MyReader.GetString("price")
                    txtcost.Text = MyReader.GetString("invoice_amount")
                    txtcost.Text = txtcost.Text - (txtfinalty.Text * (txtcost.Text / txttotcost.Text))

                    txtcost_org.Text = MyReader.GetString("invoice_origin")
                    txtcost_org.Text = FormatNumber(txtcost_org.Text, 2)
                    txtdeficiency.Text = MyReader.GetString("deficiency")
                    txtdeficiency.Text = FormatNumber(txtdeficiency.Text, 2)
                    txtcurr.Text = MyReader.GetString("currency_code")
                    txtcurrency.Text = MyReader.GetString("currency_name")
                    v_Rate = MyReader.GetString("kurs")
                    txtrate.Text = MyReader.GetString("kurs")
                    ikurs = MyReader.GetString("kurs")
                    txtrate.Text = FormatNumber(txtrate.Text, 2)
                    txtPlant.Text = MyReader.GetString("plant_code")
                    txtGrpMaterial.Text = MyReader.GetString("group_code")

                    v_sub1 = CDbl(txtcost.Text)
                    v_qty = MyReader.GetDecimal("quantity")
                    tanggal_sob = MyReader.GetDateTime("EST_DELIVERY_DT")

                Catch ex As Exception
                    txtqty.Text = 0
                    txtunitqty.Text = ""
                    txtunitprice.Text = 0
                    txtcost.Text = 0
                    txtdeficiency.Text = 0
                    txtcurr.Text = ""
                    txtcurrency.Text = ""
                    txtrate.Text = 0
                    txtPlant.Text = ""
                    txtGrpMaterial.Text = ""

                    v_sub1 = 0
                    'v_invoice = 0
                End Try
            End While

        End If
        CloseMyReader(MyReader, UserData)


        infield = "cost_ord_no as No, cost_code as ItemCost, cost_description as Description, currency_code as currency, cost_amount as Amount, cost_unit as Rate, cost_vat as Vat"
        SQLstr = "SELECT " & infield & " from tbl_cost where shipment_no = '" & Ship & "' AND ship_ord_no='" & CSNo & "' and type_code = 'CS' AND cost_code IN ('20002','20003','20004')"
        errMSg = "Datagrid view Failed"
        MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    Vcost_code = MyReader.GetString("ItemCost")
                    Select Case Vcost_code
                        Case "20002"
                            vrate = MyReader.GetString("Rate")
                        Case "20003"
                            vrate2 = MyReader.GetString("Rate")
                        Case "20004"
                            vvat = MyReader.GetString("Rate")
                    End Select
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
        Dim v_duedue As String
        Dim v_valued As String
        'mencari value_date & due date
        strSQL = "SELECT shipment_no,IF(ISNULL(tt_dt),'',date_foRMAT(tt_dt,'%Y%m%d')) value_date,IF(ISNULL(due_dt),'',date_foRMAT(due_dt,'%Y%m%d')) due_date FROM tbl_shipping WHERE shipment_no='" & Ship & "'"
        ' t0.tt_dt value_date,(ifnull(due_dt,'0000-00-00')) due_date
        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                v_valued = MyReader.GetString("value_date")
                v_duedue = MyReader.GetString("due_date")
            End While
        End If
        CloseMyReader(MyReader, UserData)

        strSQL = "SELECT COUNT(*) xrow FROM tbl_shipping_invoice WHERE shipment_no='" & Ship & "'"
        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                xCek = MyReader.GetString("xrow")
            End While
        End If
        CloseMyReader(MyReader, UserData)

        'sengaja di sini karena variabel ini di hitung dalam proses
        txtcost.Text = FormatNumber(txtcost.Text, 2)
        txtfinalty.Text = FormatNumber(txtfinalty.Text, 2)

        strSQL = "SELECT SUM(t1.invoice_amount-t1.invoice_penalty) totinvoice " & _
                 "FROM tbl_shipping_invoice t1, tbl_shipping_detail t2, tbl_po t3, tbm_material m1 " & _
                 "WHERE t1.shipment_no=t2.shipment_no AND t1.po_no=t2.po_no AND t1.ord_no=t2.po_item AND " & _
                 "t2.material_code=m1.material_code AND t2.po_no=t3.po_no AND " & _
                 "t1.shipment_no='" & Ship & "' AND m1.group_code='" & txtGrpMaterial.Text & "' AND t3.plant_code='" & txtPlant.Text & "'"
        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtpgcost.Text = MyReader.GetString("totinvoice")
                Catch ex As Exception
                    txtpgcost.Text = 0
                End Try
            End While
        End If
        CloseMyReader(MyReader, UserData)

        strSQL = "SELECT rate FROM tbm_unit_equivalent " & _
                 "WHERE unit_code='" & txtunitqty.Text & "' AND unit_code_to='KGS'"

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txttokgs.Text = MyReader.GetString("rate")
                Catch ex As Exception
                    txttokgs.Text = 1
                End Try
            End While
        End If
        CloseMyReader(MyReader, UserData)

        strSQL = "SELECT cost_code, cost_amount, cost_description, currency_code, cost_unit, cost_vat FROM tbl_cost " & _
                 "WHERE cost_code LIKE '200%' AND shipment_no='" & Ship & "' AND type_code='CS' AND ship_ord_no='" & CSNo & "' ORDER BY cost_ord_no"

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    v_costcat_code = MyReader.GetString("cost_code")
                    v_costcat_name = MyReader.GetString("cost_description")
                    v_currency = MyReader.GetString("currency_code")
                    v_amount = MyReader.GetDecimal("cost_unit")
                    v_amountidr = MyReader.GetDecimal("cost_amount")
                Catch ex As Exception
                End Try

                v_sub2 = v_sub2 + v_amountidr

                'STORE KE GRID
                tempVar = New String() {v_costcat_code, v_costcat_name, v_currency, FormatNumber(v_amount, 2), FormatNumber(v_amountidr, 2)}
                dgvcost.Rows.Add(tempVar)
            End While
        End If
        CloseMyReader(MyReader, UserData)
        txtOthers.Text = GetData("SELECT cost_amount FROM tbl_cost WHERE cost_code='80001' AND shipment_no='" & Ship & "' AND type_code='CS' AND ship_ord_no='" & CSNo & "'")
        If txtOthers.Text = "" Then txtOthers.Text = 0
        txtOthers.Text = FormatNumber(txtOthers.Text, 2)
        txtPIB.Text = GetData("SELECT cost_amount FROM tbl_cost WHERE cost_code='80002' AND shipment_no='" & Ship & "' AND type_code='CS' AND ship_ord_no='" & CSNo & "'")
        If txtPIB.Text = "" Then txtPIB.Text = 0
        txtPIB.Text = FormatNumber(txtPIB.Text, 2)
        txtDesc.Text = GetData("SELECT cost_amount FROM tbl_cost WHERE cost_code='80003' AND shipment_no='" & Ship & "' AND type_code='CS' AND ship_ord_no='" & CSNo & "'")
        If txtDesc.Text = "" Then txtDesc.Text = 0
        txtDesc.Text = FormatNumber(txtDesc.Text, 2)

        v_sub1 = ((v_sub1 * v_Rate) / (v_qty * (txtpersen.Text / 100))) / txttokgs.Text
        txtSub1.Text = FormatNumber(v_sub1, 2)

        Call Hitung_Total()

        Dim amt_estimated As String

        SQLstr = "Select sum(t1.cost_amount+t1.cost_vat) AS TOTAL " & _
               "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
               "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
               "and t2.findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
               "and t2.shipment_no='" & Ship & "'  AND  t4.subgroup_code not IN('00101','00002','20003') AND  t2.ORD_NO=1"

        '" & CSNo & "'

        errMSg = "Gagal baca data detail."
        MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    amt_estimated = amt_estimated + Math.Round(Val(MyReader.GetString(0)), 2)
                Catch ex As Exception
                    amt_estimated = 0
                End Try
            End While
        End If
        CloseMyReader(MyReader, UserData)


        strSQL = " SELECT tsd.po_no,ts.shipment_no AS 'ShipNo'," & _
           " zz.invoice_no AS 'PackingList', " & _
           " DATE_FORMAT(ts.received_doc_dt,'%d-%m-%Y') AS 'ReceiveDocDt', " & _
           " DATE_FORMAT(ts.EST_DELIVERY_DT,'%d-%m-%Y') AS 'EstDeliveryDt'," & _
           " DATE_FORMAT(ts.EST_ARRIVAL_DT,'%d-%m-%Y') AS 'EstArrivalDt', " & _
           " ts.VESSEL AS Wessel," & _
           " ts.BEA_MASUK AS 'BeaMasuk',ts.VAT," & _
           " tmu2.name AS 'CreatedBy',DATE_FORMAT(ts.CREATEDDT,'%d-%m-%Y') AS 'CreatedDate' " & _
           " FROM tbl_shipping AS ts  " & _
           " LEFT JOIN tbl_shipping_detail AS TSD ON TS.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
           " LEFT JOIN tbm_supplier AS tms ON ts.supplier_code = tms.supplier_code " & _
           " left join tbl_shipping_invoice as zz on zz.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
           " LEFT JOIN tbm_plant AS tmp ON ts.plant_code = tmp.plant_code " & _
           " LEFT JOIN tbm_port AS tmpo ON ts.port_code = tmpo.port_code " & _
           " LEFT JOIN tbm_users AS tmu1 ON ts.received_by = tmu1.user_ct " & _
           " LEFT JOIN tbm_users AS tmu2 ON ts.CREATEDBY = tmu2.user_ct " & _
           " LEFT JOIN tbm_lines AS tml ON ts.shipping_line = tml.line_code " & _
           " Left Join (SELECT shipment_no, CAST((GROUP_CONCAT(CONCAT(unit_tot,'x',unit_code) SEPARATOR ', ')) AS CHAR) container_size " & _
           " FROM (SELECT SUM(1) unit_tot, unit_code, shipment_no " & _
           " FROM(tbl_shipping_cont)" & _
           " GROUP BY shipment_no, unit_code) t1 " & _
           " GROUP BY shipment_no) t2 ON ts.shipment_no = t2.shipment_no WHERE TSD.po_no= '" & txtPO.Text & "' and ts.shipment_no='" & Ship & "' "

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    vpo_no = MyReader.GetString("po_no")
                    vwessel = MyReader.GetString("wessel")
                    vpackinglist = MyReader.GetString("packinglist")
                    'vvat = MyReader.GetDecimal("vat")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        'mencari nomor urut PO
        strSQL = " SELECT IF(LEFT(SUBSTRING(getpoorder(a.shipment_no,TRIM(b.po_no)),12,3),1)= ' ','1',LEFT(SUBSTRING(getpoorder(a.shipment_no,TRIM(b.po_no)),12,3),1)) AS nom_po " & _
                 " FROM tbl_shipping AS a INNER JOIN tbl_shipping_detail AS b ON a.shipment_no = b.shipment_no " & _
                 " WHERE a.shipment_no = '" & Ship & "' ORDER BY b.po_no "
        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    xpoitem = MyReader.GetString("nom_po")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        Dim approvby As String = approvedby.Text.ToUpper()
        Dim createdby As String = crt.Text.ToUpper()
        Dim unit2 As String = "USD"
        Dim unit8 As String = "USD"
        Dim unit10 As String = "RP"
        Dim unit9 As String = "RP"
        Dim unit11 As String = "RP"
        Dim flag_up As Integer = 2
        Dim fcontrol As Integer

        SQLstr = "Select user_ct,Name,user_id from tbm_users " & _
               "where user_ct=" & UserData.UserCT

        errMSg = "Failed when read data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)


        If Not MyReader Is Nothing Then
            While MyReader.Read
                vuserid = MyReader.GetString("user_id")
            End While
        End If
        CloseMyReader(MyReader, UserData)

        Dim RecordCount As Integer
        If Status.Text = "Approved" Then
            SQLstr = "select count(*) as jml_rec from template_poim where ebeln='" & txtPO.Text & "' " & _
                     "and shipment='" & xpoitem & "' and sob='" & Format(tanggal_sob, "yyyy-MM-dd") & "' "
            ' & _ "and user_id='" & vuserid & "'"
            MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    RecordCount = MyReader.GetInt16("jml_rec")

                End While
            End If
            CloseMyReader(MyReader, UserData)

            SQLstr = "SELECT COUNT(*) controlcsv FROM tbm_control WHERE loc_print = 'Y'"
            MyReader = DBQueryMyReader(SQLstr, MyConn, errMSg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    fcontrol = MyReader.GetInt16("controlcsv")
                End While
            End If
            CloseMyReader(MyReader, UserData)


            If RecordCount > 0 And fcontrol > 0 Then
                'MessageBox.Show("No PO INI  '" & vpo_no & "' sudah pernah di cetak")
            Else
                txtPO2 = txtPO.Text
                'supram
                'SQLstr = "delete from template_poim where ebeln = '" & txtPO.Text & "' and SHIPMENT = '" & xpoitem & "'"
                'affrow = DBQueryUpdate(SQLstr, MyConn, False, errMSg, UserData)

                'SQLstr = "" & _
                '    "INSERT INTO template_poim (" & _
                '    "   EBELN, SHIPMENT, SOB, approve, PREPARED, KURS, VESSEL, QUANTITY, UNIT_PRICE, UNIT2, INVOICE_NO, " & _
                '    "   INVOICE_DATE, INVOIC_DUE_DATE, VALUE_DATE, INSURANCE, UNIT8, PPN,UNIT10, IMPORT_DUTY, UNIT9, " & _
                '    "   CLEAReNCE_COST, UNIT11, flag_upload,User_id " & _
                '    ") VALUES (" & _
                '    "   '" & txtPO.Text & "', '" & xpoitem & "','" & Format(tanggal_sob, "yyyy-MM-dd") & "'," & _
                '    "   '" & approvby & "', '" & createdby & "','" & ikurs & "', '" & vwessel & "', (SELECT REPLACE('" & txtqty.Text & "',',',''))  ," & _
                '    "   '" & pricelist & "','" & unit2 & "','" & vpackinglist & "','" & Format(tanggal_sob, "yyyy-MM-dd") & "'," & _
                '    "   '" & v_duedue & "','" & v_valued & "'," & vrate & ",'" & unit8 & "','" & vvat & "','" & unit10 & "'," & _
                '    "   '" & vrate2 & "','" & unit9 & "','" & amt_estimated & "','" & unit11 & "','" & flag_up & "','" & vuserid & "'" & _
                '    ")"
                'affrow = DBQueryUpdate(SQLstr, MyConn, False, errMSg, UserData)

                sp_ebeln = txtPO.Text
                sp_shipment = xpoitem
                sp_sob = tanggal_sob
                sp_approve = approvby
                sp_prepared = createdby
                sp_kurs = ikurs
                sp_vessel = vwessel
                sp_quantity = txtqty.Text
                sp_unit_price = pricelist
                sp_unit2 = unit2
                sp_invoice_no = vpackinglist
                sp_invoice_date = tanggal_sob
                sp_invoic_due_date = v_duedue
                sp_value_date = v_valued
                sp_insurance = vrate
                sp_unit8 = unit8
                sp_ppn = vvat
                sp_unit10 = unit10
                sp_import_duty = vrate2
                sp_unit9 = unit9
                sp_clearence_cost = amt_estimated
                sp_unit11 = unit11
                sp_flag_upload = flag_up
                sp_user_id = vuserid
            End If

            'sebelum insert hapus dulu
            'SQLstr = "delete from template_csv"
            'affrow = DBQueryUpdate(SQLstr, MyConn, False, errMSg, UserData)

            'SQLstr = "INSERT INTO TEMPLATE_CSV (EBELN,INVOICE_NO,USER_ID,SHIPMENT) " & _
            '         "VALUES ('" & vpo_no & "','" & vpackinglist & "','" & vuserid & "','" & xpoitem & "')"
            'affrow = DBQueryUpdate(SQLstr, MyConn, False, errMSg, UserData)
        End If
    End Sub

    Private Sub cbPO_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPO.SelectedIndexChanged
        Call FillData()
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

    Private Sub SumCostOfbank()
        Dim lv_rate As String

        If IsNumeric(txtOthers.Text) And IsNumeric(txtPIB.Text) And IsNumeric(txtDesc.Text) Then
            lv_rate = CDbl(txtOthers.Text) + CDbl(txtPIB.Text) + CDbl(txtDesc.Text)
            dgvcost.Rows.Item(0).Cells.Item(3).Value = FormatNumber(lv_rate, 2)
            dgvcost.Rows.Item(0).Cells.Item(4).Value = FormatNumber(lv_rate, 2)

            Call Hitung_Total()
        End If
    End Sub

    Private Sub txtOthers_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOthers.Validated
        Call SumCostOfbank()
    End Sub

    Private Sub txtPIB_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPIB.Validated
        Call SumCostOfbank()
    End Sub

    Private Sub txtDesc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesc.Validated
        Call SumCostOfbank()
    End Sub

    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT
        PunyaAkses = (DataExist(SQLStr) = True)

    End Function


    Private Sub txtpersen_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpersen.Validated
        Call Hitung_Total()
    End Sub

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

    Private Sub txtSub1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSub1.TextChanged

    End Sub

    Private Sub dgvcost_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvcost.CellContentClick

    End Sub

    Private Sub crt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crt.TextChanged

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub copytocsv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles copytocsv.Click
        Dim v_num As String
        Dim f As New exp_csv

        '        exp_csv.Nomercsv(txtPO.Text, xpoitem, tanggal_sob)
        txtPO1 = txtPO.Text
        xpoitem1 = xpoitem
        tanggal_sob1 = tanggal_sob
        f.ShowDialog()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub copytocsv_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles copytocsv.DragOver

    End Sub

    Private Sub copytocsv_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles copytocsv.EnabledChanged

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnSyncronize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyn.Click
        'Dim v_num As String
        'Dim f As New exp_csv
        'f.ShowDialog()
        PIBSyncronize("123")
    End Sub


End Class