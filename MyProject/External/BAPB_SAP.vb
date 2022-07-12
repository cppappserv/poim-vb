Imports System.Data.OleDb
Imports System.Management
Imports System.Text.RegularExpressions

Imports System.IO

Public Class BAPB_SAP
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim SQLstr, SQLwrt As String
    Dim MyReader As MySqlDataReader
    Dim DataOk As Boolean = True
    Dim CountOK As Integer

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Dim PrmPathExcelFile As String
        Dim MyConnection As System.Data.OleDb.OleDbConnection

        PrmPathExcelFile = TxtFile.Text
        If PrmPathExcelFile <> "" Then
            Try
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & "data source='" & PrmPathExcelFile & " '; " & "Extended Properties=Excel 8.0;")

                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select '0' as chk, * from [sheet1$]", MyConnection)
                MyCommand.TableMappings.Add("Table", "Attendence")
                DtSet = New System.Data.DataSet

                MyCommand.Fill(DtSet)
                dgv.DataSource = DtSet.Tables(0)

                dgv.Columns(0).Visible = False
                dgv.Columns(14).DefaultCellStyle.Format = "N3"
                dgv.Columns(15).DefaultCellStyle.Format = "N3"
                dgv.Columns(19).DefaultCellStyle.Format = "N3"
                dgv.Columns(20).DefaultCellStyle.Format = "N3"
                dgv.Columns(22).DefaultCellStyle.Format = "N5"

                MyConnection.Close()
                CheckData()
                'If DataOk Then UpdateData()
                'UpdateData()
                'If DataOk Then
                If CountOK > 0 Then
                    lblProcess.Text = "Process successfully. " & CountOK & " lines updated"
                Else
                    lblProcess.Text = "Process failed, please checking your data."
                End If

            Catch ex As Exception
                MyConnection.Close()
                lblProcess.Text = "Process failed"
            End Try
        Else
            lblProcess.Text = "Select source file, first !"
        End If
    End Sub

    Private Sub CheckData()
        Dim RstQA As DataTableReader
        Dim ErrMsg As String
        Dim brs, iShip As Integer
        Dim ls_PO, ls_ship, ls_MatCdSAP, ls_unit, ls_unitto As String
        Dim dConvUnit, dQty, dQtyTo As Decimal
        Dim v_RefType As String

        ErrMsg = "Failed when reading data"

        lblProcess.Text = "Checking selected file"

        brs = dgv.RowCount
        For i = 0 To brs - 1
            ls_PO = dgv.Item(4, i).Value.ToString
            ls_ship = dgv.Item(8, i).Value.ToString
            Try
                iShip = CInt(ls_ship)
            Catch ex As Exception
                iShip = 0
            End Try
            ls_MatCdSAP = dgv.Item(5, i).Value.ToString
            ls_unit = dgv.Item(23, i).Value.ToString
            dQty = dgv.Item(22, i).Value

            If dQty < 0 Then
                dgv.Item(0, i).Value = 0
                dgv.Rows(i).DefaultCellStyle.BackColor = Color.Red
                DataOk = False
            Else
                SQLstr = "SELECT shipment_no FROM tbl_shipping_detail " & _
                         "WHERE trim(po_no) = trim('" & ls_PO & "') AND getorder(shipment_no, po_no) = " & iShip & ""

                If DataExist(SQLstr) = False Then
                    dgv.Item(0, i).Value = 0
                    dgv.Rows(i).DefaultCellStyle.BackColor = Color.IndianRed
                    DataOk = False
                Else
                    SQLstr = "SELECT t1.po_item FROM tbl_shipping_detail t1, tbm_material m1 " & _
                             "WHERE t1.material_code=m1.material_code AND getorder(t1.shipment_no, t1.po_no) = " & iShip & " AND trim(t1.po_no) = trim('" & ls_PO & "') AND trim(m1.sap_code) = trim('" & ls_MatCdSAP & "')"

                    If DataExist(SQLstr) = False Then
                        dgv.Item(0, i).Value = 0
                        dgv.Rows(i).DefaultCellStyle.BackColor = Color.Orange
                        DataOk = False
                    Else
                        dQtyTo = 0
                        SQLstr = "SELECT m1.rate, m1.unit_code_to FROM tbm_unit_equivalent m1, tbm_unit m2 " & _
                                 "WHERE m1.unit_code=m2.unit_code AND m2.sap_code = '" & ls_unit & "' AND m1.unit_code_to = " & _
                                 "          (SELECT t1.unit_code FROM tbl_po_detail t1, tbm_material m1 WHERE t1.material_code=m1.material_code AND trim(t1.po_no) = trim('" & ls_PO & "') AND trim(m1.sap_code) = trim('" & ls_MatCdSAP & "') LIMIT 1) "

                        RstQA = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)
                        If Not RstQA Is Nothing Then
                            While RstQA.Read
                                Try
                                    dConvUnit = CDec(RstQA.GetValue(0))
                                    ls_unitto = Trim(RstQA.GetString(1))
                                    dQtyTo = dQty * dConvUnit
                                Catch ex As Exception
                                    dConvUnit = 0
                                    ls_unitto = ""
                                    dQtyTo = 0
                                End Try
                            End While
                            If dConvUnit = 0 Then
                                dgv.Item(0, i).Value = 0
                                dgv.Rows(i).DefaultCellStyle.BackColor = Color.LimeGreen
                                DataOk = False
                            Else
                                If dQty <= 0 Then
                                    dgv.Item(0, i).Value = 0
                                    dgv.Rows(i).DefaultCellStyle.BackColor = Color.Red
                                    DataOk = False
                                Else
                                    dgv.Item(23, i).Value = "1" & ls_unit & "=" & dConvUnit & "" & ls_unitto
                                    dgv.Item(0, i).Value = "" & dQtyTo
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub UpdateData()
        Dim RstQA As DataTableReader
        Dim affrow As Integer
        Dim ErrMsg As String
        Dim brs, iShip As Integer
        Dim dQty As Decimal
        Dim ls_PO, ls_Date, ls_Truck, ls_Cont, ls_item, ls_MatCdSAP, ls_MatCd, ls_Qty, ls_Qtyto As String
        Dim ishipment As Integer

        CountOK = 0
        lblProcess.Text = "Updating to database"

        ErrMsg = "Failed when updating data"
        brs = dgv.RowCount
        For i = 0 To brs - 1
            ls_Qtyto = dgv.Item(0, i).Value.ToString
            If ls_Qtyto <> "0" And ls_Qtyto <> "" Then
                ls_Qtyto = GetNum2(ls_Qtyto)
                ls_Qty = ls_Qtyto

                iShip = CInt(dgv.Item(8, i).Value.ToString)
                ls_PO = Trim(dgv.Item(4, i).Value.ToString)
                ls_Date = dgv.Item(21, i).Value.ToString
                ls_Truck = Trim(dgv.Item(9, i).Value.ToString)

                ls_Cont = Trim("-")
                ls_MatCdSAP = Trim(dgv.Item(5, i).Value.ToString)

                ishipment = 0
                ls_item = ""
                ls_MatCd = ""

                SQLstr = "SELECT t1.shipment_no, t1.po_item, t1.material_code FROM tbl_shipping_detail t1, tbm_material m1 " & _
                         "WHERE t1.material_code=m1.material_code AND getorder(t1.shipment_no, t1.po_no) = " & iShip & " AND trim(t1.po_no) = '" & ls_PO & "' AND trim(m1.sap_code) = '" & ls_MatCdSAP & "'"

                RstQA = DBQueryDataReader(SQLstr, MyConn, ErrMsg, UserData)
                If Not RstQA Is Nothing Then
                    While RstQA.Read
                        Try
                            ishipment = CInt(RstQA.GetValue(0))
                            ls_item = Trim(RstQA.GetString(1))
                            ls_MatCd = Trim(RstQA.GetString(2))
                        Catch ex As Exception
                            ishipment = 0
                            ls_item = ""
                            ls_MatCd = ""
                        End Try
                    End While
                End If

                If ishipment > 0 Then

                    SQLstr = "SELECT shipment_no FROM tbl_bapb " & _
                                "WHERE  shipment_no  = " & ishipment & " AND trim(po_no) = '" & ls_PO & "' AND bapb_dt = '" & Format(CDate(ls_Date), "yyyy-MM-dd") & "' AND trim(truck_no) = '" & ls_Truck & "'"

                    SQLwrt = ""
                    If DataExist(SQLstr) = False Then
                        SQLwrt = "INSERT INTO tbl_bapb (shipment_no, po_no, bapb_dt, truck_no, container_no, createdby, createddt) " & _
                                  "VALUES ('" & ishipment & "','" & ls_PO & "','" & Format(CDate(ls_Date), "yyyy-MM-dd") & "','" & ls_Truck & "','" & ls_Cont & "','" & UserData.UserCT & "','" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "'); "
                    End If

                    SQLstr = "SELECT shipment_no FROM tbl_bapb_detail " & _
                             "WHERE shipment_no = " & ishipment & " AND trim(po_no) = '" & ls_PO & "' AND bapb_dt = '" & Format(CDate(ls_Date), "yyyy-MM-dd") & "' AND trim(truck_no) = '" & ls_Truck & "' AND trim(po_item) = '" & ls_item & "'"

                    If DataExist(SQLstr) Then
                        SQLwrt = SQLwrt & "UPDATE tbl_bapb_detail " & _
                                          "SET quantity = '" & ls_Qty & "' " & _
                                          "WHERE shipment_no = " & ishipment & " AND trim(po_no) = '" & ls_PO & "' AND bapb_dt = '" & Format(CDate(ls_Date), "yyyy-MM-dd") & "' AND trim(truck_no) = '" & ls_Truck & "' AND trim(po_item) = '" & ls_item & "';"
                    Else
                        SQLwrt = SQLwrt & "INSERT INTO tbl_bapb_detail (shipment_no, po_no, bapb_dt, truck_no, po_item, material_code, quantity) " & _
                                          "VALUES ('" & ishipment & "','" & ls_PO & "','" & Format(CDate(ls_Date), "yyyy-MM-dd") & "','" & ls_Truck & "','" & ls_item & "','" & ls_MatCd & "','" & ls_Qty & "');"
                    End If
                    affrow = DBQueryUpdate(SQLwrt, MyConn, False, ErrMsg, UserData)
                    If affrow > 0 Then
                        CountOK = CountOK + 1
                    Else
                        dgv.Rows(i).DefaultCellStyle.BackColor = Color.Red
                    End If
                Else
                    dgv.Rows(i).DefaultCellStyle.BackColor = Color.Red
                End If
            End If
        Next

    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Dim opendialog As New OpenFileDialog

        If opendialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            TxtFile.Text = opendialog.FileName
            lblProcess.Text = "Click Process button to start processing"
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

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

    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub BAPB_SAP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If

        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","

        lblProcess.Text = "Select source file, first"
    End Sub
End Class