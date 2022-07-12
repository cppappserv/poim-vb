'Title        : FrmReceiver

Public Class frmBAPB
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihPO As New FrmListShipment
    Dim DataError, Baru As Boolean

    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnSave.Enabled = False
    End Sub

    Private Sub FrmBAPB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub RefreshList()
        Dim brs As Integer

        If txtShipmentNo.Text = "" Or txtPO.Text = "" Then Exit Sub

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.po_no, getorder(t1.shipment_no, t1.po_no) ShipNo, t1.bapb_dt ReceivedDate, t1.truck_no TruckNo, t1.container_no ContainerNo " & _
                                    " FROM tbl_bapb t1 WHERE t1.shipment_no = '" & txtShipmentNo.Text & "' AND t1.po_no = '" & txtPO.Text & "') as a")

        brs = DataGridView1.RowCount

        DataGridView1.Columns(0).Visible = False
        DataGridView1.Columns(1).Visible = False
        DataGridView1.Columns(2).Visible = False

        DataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.Gray
        DataGridView1.Columns(4).DefaultCellStyle.BackColor = Color.Gray
        DataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.Gray

        DataGridView2.DataSource = Nothing
        DataGridView2.Columns.Clear()

        btnSave.Enabled = False
        btnDelete.Enabled = False
        ReceivedDt.Checked = False
        txtTruck.Clear()
        txtContainer.Clear()
        txtTruck.Focus()
    End Sub

    Private Sub RefreshDetail()
        Dim brs As Integer
        Dim bapbdt As String

        btnDelete.Enabled = False
        txtPOItem.Clear()

        If txtShipmentNo.Text = "" Or txtPO.Text = "" Then Exit Sub

        If ReceivedDt.Checked Then
            bapbdt = Format(ReceivedDt.Value, "yyyy-MM-dd")

            DataGridView2.DataSource = Show_Grid(DataGridView1, "(SELECT t1.shipment_no, t1.po_no, t1.bapb_dt, t1.truck_no, t1.po_item, t1.material_code, m1.material_name DescriptionOfGoods, " & _
                                                 "t1.Quantity ReceivedQuantity, t3.unit_code Unit, t2.quantity OutStdQuantity  " & _
                                                 "FROM tbl_bapb_detail t1, tbl_shipping_detail t2, tbl_po_detail t3, tbm_material m1, tbm_country m2 " & _
                                                 "WHERE (t1.shipment_no = t2.shipment_no And t1.po_no = t2.po_no And t1.po_item = t2.po_item) " & _
                                                 "   AND t2.po_no=t3.po_no AND t2.po_item=t3.po_item AND t1.material_code=m1.material_code AND t3.country_code=m2.country_code " & _
                                                 "   AND t1.shipment_no = '" & txtShipmentNo.Text & "' AND t1.po_no = '" & txtPO.Text & "' AND t1.bapb_dt='" & bapbdt & "' AND t1.truck_no = '" & txtTruck.Text & "') as a")

            brs = DataGridView2.RowCount
        End If

        If brs > 0 Then
            Baru = False
        Else
            Baru = True
            DataGridView2.DataSource = Show_Grid(DataGridView1, "(SELECT '" & txtShipmentNo.Text & "' shipment_no, '" & txtPO.Text & "' po_no, '" & bapbdt & "' bapb_dt, '1' truck_no, t2.po_item, t2.material_code, m1.material_name DescriptionOfGoods, " & _
                                            "0 ReceivedQuantity, t3.unit_code Unit, t2.quantity OutStdQuantity " & _
                                            "FROM tbl_shipping_detail t2, tbl_po_detail t3, tbm_material m1, tbm_country m2 " & _
                                            "WHERE (t2.po_no = t3.po_no And t2.po_item = t3.po_item And t2.material_code = m1.material_code And t3.country_code = m2.country_code) " & _
                                            "AND t2.shipment_no = '" & txtShipmentNo.Text & "' AND t2.po_no = '" & txtPO.Text & "') as a")
        End If
        brs = DataGridView2.RowCount

        DataGridView2.Columns(0).Visible = False
        DataGridView2.Columns(1).Visible = False
        DataGridView2.Columns(2).Visible = False
        DataGridView2.Columns(3).Visible = False
        DataGridView2.Columns(4).Visible = False
        DataGridView2.Columns(5).Visible = False

        DataGridView2.Columns(6).Width = 250

        DataGridView2.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView2.Columns(7).DefaultCellStyle.Format = "N5"
        DataGridView2.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView2.Columns(9).DefaultCellStyle.Format = "N5"

        DataGridView2.Columns(6).ReadOnly = True
        DataGridView2.Columns(8).ReadOnly = True
        DataGridView2.Columns(9).ReadOnly = True

        DataGridView2.Columns(6).DefaultCellStyle.BackColor = Color.Gray
        DataGridView2.Columns(8).DefaultCellStyle.BackColor = Color.Gray
        DataGridView2.Columns(9).DefaultCellStyle.BackColor = Color.Gray
    End Sub

    Private Sub GetData()
        Dim strSQL, errMsg As String

        If txtShipmentNo.Text = "" Or txtPO.Text = "" Then Exit Sub

        lblHist.Text = "Clearance History of " & txtPO.Text & " (" & txtShipNo.Text & ")"

        txtBL.Clear()
        txtSupplier.Clear()
        txtPort.Clear()
        txtLines.Clear()
        txtVessel.Clear()
        txtExpedition.Clear()

        strSQL = "SELECT t1.*, L1.company_name emkl_name, if(t1.bapb_dt IS NULL, ltbapb_dt, bapb_dt) ltbapb_dt, if(L2.TotQty IS NULL, 0, FORMAT(TotQty,5)) TotQty FROM " & _
                 " (SELECT DISTINCT t2.bl_no, t2.est_delivery_dt, t2.notice_arrival_dt, t2.clr_dt, t2.bapb_dt, if(t2.bapb_dt is null, 'Open','Close') bapbstatus, m1.supplier_name, m2.port_name, m3.line_name, t2.vessel, t2.emkl_code " & _
                 " FROM tbl_shipping_detail t1, tbl_shipping t2, tbm_supplier m1, tbm_port m2, tbm_lines m3 " & _
                 " WHERE(t1.shipment_no = t2.shipment_no And t2.supplier_code = m1.supplier_code And t2.port_code = m2.port_code And t2.shipping_line = m3.line_code) " & _
                 " AND t1.shipment_no = '" & txtShipmentNo.Text & "' AND t1.po_no = '" & txtPO.Text & "') t1 " & _
                 "LEFT JOIN tbm_expedition L1 ON L1.company_code=t1.emkl_code " & _
                 "LEFT JOIN (SELECT shipment_no, po_no, MAX(bapb_dt) ltbapb_dt, SUM(quantity) TotQty FROM tbl_bapb_detail WHERE shipment_no='" & txtShipmentNo.Text & "' AND po_no='" & txtPO.Text & "' Group By shipment_no, po_no) L2 ON 1=1 "

        errMsg = "Failed when read user data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtBL.Text = MyReader.GetString("bl_no")
                    txtSupplier.Text = MyReader.GetString("supplier_name")
                    dtETD.Value = MyReader.GetDateTime("est_delivery_dt")
                    Try
                        dtETA1.Value = MyReader.GetDateTime("notice_arrival_dt")
                    Catch ex As Exception
                    End Try
                    Try
                        dtDeliver1.Value = MyReader.GetDateTime("clr_dt")
                    Catch ex As Exception
                    End Try
                    Try
                        dtClear1.Value = MyReader.GetDateTime("ltbapb_dt")
                    Catch ex As Exception
                    End Try
                    If MyReader.GetString("bapbstatus") = "Open" Then
                        lblClear1.Visible = True
                    Else
                        lblClear1.Visible = False
                    End If
                    GRQty.Text = MyReader.GetString("TotQty")
                    txtPort.Text = MyReader.GetString("port_name")
                    txtLines.Text = MyReader.GetString("line_name")
                    txtVessel.Text = MyReader.GetString("vessel")
                    txtExpedition.Text = MyReader.GetString("emkl_name")
                Catch ex As Exception
                End Try
                dtETA1.Enabled = False
                dtDeliver1.Enabled = False
                dtClear1.Enabled = False
            End While
        End If
        CloseMyReader(MyReader, UserData)
    End Sub

    Private Sub GetData2()
        Dim strSQL, errMsg As String
        Dim RcvDt As String
        Dim tg As Date

        tg = GetServerDate()

        DTCreated.Value = tg
        crtcode.Text = UserData.UserCT.ToString
        txtCREATEDBY.Text = UserData.UserName

        If (txtShipmentNo.Text <> "" And txtPO.Text <> "" And ReceivedDt.Checked And txtTruck.Text <> "") Then

            RcvDt = ReceivedDt.Text

            strSQL = "SELECT  t1.createdby, m1.name createdbyname, t1.createddt FROM tbl_bapb t1, tbm_users m1 " & _
                     "WHERE t1.createdby = m1.user_ct and t1.shipment_no = '" & txtShipmentNo.Text & "' AND t1.po_no = '" & txtPO.Text & "' AND t1.bapb_dt='" & Format(CDate(RcvDt), "yyyy-MM-dd") & "' AND t1.truck_no = '" & txtTruck.Text & "'"

            errMsg = "Failed when read user data"
            MyReader = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

            If Not MyReader Is Nothing Then
                While MyReader.Read
                    Try
                        DTCreated.Value = MyReader.GetDateTime("createddt")
                        crtcode.Text = MyReader.GetString("createdby")
                        txtCREATEDBY.Text = MyReader.GetString("createdbyname")
                    Catch ex As Exception
                    End Try
                End While
            End If
            CloseMyReader(MyReader, UserData)
        End If
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshList()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader

        Dim hasil As Boolean = False
        Dim keyprocess As String = ""
        Dim insertStr As String = ""

        Dim hd_shipno, hd_pono, hd_truck, hd_contain As String
        Dim hd_bapbdt, hd_createddt As String

        Dim Msg As String

        Msg = "This data will be deleted PERMANENTLY!!!" & Chr(13) & Chr(10) & "Are you sure to delete it?"
        If (MsgBox(msg, MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        ErrMsg = "Failed to process BAPB data."

        Try
            hd_bapbdt = Format(ReceivedDt.Value, "yyyy-MM-dd")
            hd_createddt = Format(DTCreated.Value, "yyyy-MM-dd")
            hd_shipno = txtShipmentNo.Text
            hd_pono = txtPO.Text
            hd_truck = txtTruck.Text
            hd_contain = txtContainer.Text

            SQLstr = "Run Stored Procedure SaveBAPB"
            keyprocess = "Delete"

            MyComm.CommandText = "SaveBAPB"
            MyComm.CommandType = CommandType.StoredProcedure
            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", hd_shipno)
            MyComm.Parameters.AddWithValue("PONo", hd_pono)
            MyComm.Parameters.AddWithValue("BAPBDt", hd_bapbdt)
            MyComm.Parameters.AddWithValue("Truck", hd_truck)
            MyComm.Parameters.AddWithValue("Contain", hd_contain)
            MyComm.Parameters.AddWithValue("CreatedBy", UserData.UserCT)
            MyComm.Parameters.AddWithValue("CreatedDt", hd_createddt)
            MyComm.Parameters.AddWithValue("InsertStr", "")
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount

            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(keyprocess & " BAPB Data")
            Else
                MsgBox("Process BAPB Data failed'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        RefreshList()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)

        brs = DataGridView1.CurrentCell.RowIndex

        btnDelete.Enabled = True And (crtcode.Text = UserData.UserCT)
        ReceivedDt.Checked = True
        ReceivedDt.Text = DataGridView1.Item(3, brs).Value.ToString
        txtTruck.Text = DataGridView1.Item(4, brs).Value.ToString
        txtContainer.Text = DataGridView1.Item(5, brs).Value.ToString
    End Sub

    Private Sub btnPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPO.Click
        If PilihPO.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPO.Text = Trim(PilihPO.v_pono.Text)
            txtShipmentNo.Text = PilihPO.v_shipmentno.Text
            txtShipNo.Text = PilihPO.v_shipno.Text

            GetData()
            GetData2()
            RefreshList()
        End If
    End Sub

    Private Sub txtPO_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtPO.Validating
        GetData()
        GetData2()
        RefreshList()
    End Sub

    Private Sub ReceivedDt_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReceivedDt.ValueChanged
        GetData2()
        RefreshDetail()
    End Sub

    Private Sub txtShipmentNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtShipmentNo.TextChanged
        GetData()
        GetData2()
        RefreshList()
    End Sub

    Private Sub DataGridView2_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles DataGridView2.CellValidating
        btnSave.Enabled = True
    End Sub

    Private Function GridOK(ByVal baris As Integer) As Boolean
        Dim val As Integer
        Dim cnt As Integer

        GridOK = False
        For cnt = 0 To baris - 1
            val = DataGridView2.Item(7, cnt).Value
            If val > 0 Then GridOK = True
        Next
        If GridOK = False Then MsgBox("Quantity should be > 0")
    End Function

    Private Function HeaderOK() As Boolean
        HeaderOK = False
        If (Len(Trim(txtPO.Text)) > 0) And (Len(Trim(txtShipNo.Text)) > 0) And ReceivedDt.Checked And (Len(Trim(txtTruck.Text)) > 0) Then HeaderOK = True
        If HeaderOK = False Then MsgBox("PO No, Shipment No, Clearance Date and Truck No should be filled")
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader

        Dim hasil As Boolean = False
        Dim keyprocess As String = ""
        Dim insertStr As String = ""

        Dim brs As Integer
        Dim hd_shipno, hd_pono, hd_truck, hd_contain As String
        Dim hd_bapbdt, hd_createddt As String
        Dim ls_poitem, ls_matcode, ls_qty As String
        Dim num As Decimal

        DataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit)

        brs = DataGridView2.RowCount

        If GridOK(brs) = False Then Exit Sub
        If HeaderOK() = False Then Exit Sub

        ErrMsg = "Failed to process BAPB data."

        Try
            hd_bapbdt = Format(ReceivedDt.Value, "yyyy-MM-dd")
            hd_createddt = Format(DTCreated.Value, "yyyy-MM-dd")
            hd_shipno = txtShipmentNo.Text
            hd_pono = txtPO.Text
            hd_truck = txtTruck.Text
            hd_contain = txtContainer.Text

            SQLstr = "Run Stored Procedure SaveBAPB"

            For i = 0 To brs - 1
                ErrMsg = "Failed to update CC detail data."
                ls_poitem = DataGridView2.Item(4, i).Value.ToString
                ls_matcode = DataGridView2.Item(5, i).Value.ToString

                ls_poitem = Mid(ls_poitem & "     ", 1, 5)
                ls_matcode = Mid(ls_matcode & "          ", 1, 10)

                num = DataGridView2.Item(7, i).Value
                If num > 0 Then
                    ls_qty = GetNum2(FormatNumber(num, 5, , , TriState.True))
                    ls_qty = Microsoft.VisualBasic.Mid(ls_qty & "           ", 1, 20)

                    insertStr &= ls_poitem & ls_matcode & ls_qty & ";"
                End If
            Next

            If Baru Then
                keyprocess = "Save"
            Else
                keyprocess = "Update"
            End If

            MyComm.CommandText = "SaveBAPB"
            MyComm.CommandType = CommandType.StoredProcedure
            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("keyprocess", keyprocess)
            MyComm.Parameters.AddWithValue("Ship", hd_shipno)
            MyComm.Parameters.AddWithValue("PONo", hd_pono)
            MyComm.Parameters.AddWithValue("BAPBDt", hd_bapbdt)
            MyComm.Parameters.AddWithValue("Truck", hd_truck)
            MyComm.Parameters.AddWithValue("Contain", hd_contain)
            MyComm.Parameters.AddWithValue("CreatedBy", UserData.UserCT)
            MyComm.Parameters.AddWithValue("CreatedDt", hd_createddt)
            MyComm.Parameters.AddWithValue("InsertStr", insertStr)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)

            If hasil = True Then
                f_msgbox_successful(keyprocess & " BAPB Data")
            Else
                MsgBox("Process BAPB Data failed'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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


    Private Sub txtTruck_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTruck.Validating
        GetData2()
        RefreshDetail()
    End Sub

    Private Sub txtTruck_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTruck.TextChanged
        GetData2()
        RefreshDetail()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class
