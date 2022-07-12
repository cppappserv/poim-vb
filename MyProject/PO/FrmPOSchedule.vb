'Title                         : Shipment Schedule
'Form                          : FrmPOSchedule
'Table Used                    : all
'Stored Procedure Used (MySQL) : RunSQL

Imports vbs = Microsoft.VisualBasic.Strings

Public Class FrmPOSchedule
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim SQLstr As String
    Dim ErrMsg As String
    Dim MyReader As MySqlDataReader

    Dim PONo, UserID As String
    Dim StatD As String

    Private Sub FrmPOSchedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","

        btnDelete.Enabled = False
        btnSave.Enabled = False

        txtPO.Text = PONo
        crtcode.Text = UserID
        StatD = ""
        refreshGrid()
    End Sub

    Private Sub refreshGrid()
        Dim dts As DataTable

        dgList.DataSource = Nothing

        If StatD = "New" Then

            SQLstr = "SELECT * FROM ( " & _
                    "   SELECT t1.shipment_no ShipmentNo, t1.est_delivery_dt EstDeliveryDate, t1.est_arrival_dt EstArrivalDate, t1.vessel Vessel, t1.po_item Item, m1.material_name MaterialName, m2.country_name Origin, m2.lt LeadTime, t1.quantity EstQuantity " & _
                    "   FROM tbl_po_schedule t1, tbl_po_detail t2, tbm_material m1, tbm_country m2 WHERE t1.po_no=t2.po_no AND t1.po_item=t1.po_item AND t2.material_code=m1.material_code AND t2.country_code=m2.country_code AND trim(t1.po_no) = trim('" & PONo & "') " & _
                    "   UNION " & _
                    "   SELECT (SELECT IF(MAX(shipment_no) IS NULL,1,MAX(shipment_no)+1) FROM tbl_po_schedule WHERE po_no=t1.po_no) ShipmentNo, " & _
                    "   t1.SHIPMENT_PERIOD_FR EstDeliveryDate, t1.SHIPMENT_PERIOD_TO EstArrivalDate, '' Vessel, t2.po_item Item, m1.material_name MaterialName, m2.country_name Origin, m2.lt LeadTime, 0 EstQuantity " & _
                    "   FROM tbl_po t1, tbl_po_detail t2, tbm_material m1, tbm_country m2 WHERE t1.po_no=t2.po_no AND t2.material_code=m1.material_code AND t2.country_code=m2.country_code AND trim(t2.po_no) = trim('" & PONo & "') " & _
                    ") t1 ORDER BY  t1.ShipmentNo, t1.Vessel, t1.Item "
        Else

            SQLstr = "SELECT t1.shipment_no ShipmetNo, t1.est_delivery_dt EstDeliveryDate, t1.est_arrival_dt EstArrivalDate, t1.vessel Vessel, t1.po_item Item, m1.material_name MaterialName, m2.country_name Origin, m2.lt LeadTime, t1.quantity EstQuantity " & _
                    "   FROM tbl_po_schedule t1, tbl_po_detail t2, tbm_material m1, tbm_country m2 WHERE t1.po_no=t2.po_no AND t1.po_item=t1.po_item AND t2.material_code=m1.material_code AND t2.country_code=m2.country_code AND trim(t1.po_no) = trim('" & PONo & "') " & _
                    "   ORDER BY  t1.shipment_no, t1.vessel, t1.po_item "
        End If

        ErrMsg = "Datagrid view Failed"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        dgList.DataSource = dts
        If DBQueryGetTotalRows(SQLstr, MyConn, ErrMsg, False, UserData) > 0 Then
            dgList.Columns(0).Width = 70
            dgList.Columns(1).Width = 90
            dgList.Columns(2).Width = 90
            dgList.Columns(3).Width = 200
            dgList.Columns(4).Width = 30
            dgList.Columns(5).Width = 200
            dgList.Columns(6).Width = 70
            dgList.Columns(7).Width = 60
            dgList.Columns(8).Width = 70
            dgList.Columns(0).ReadOnly = True
            dgList.Columns(4).ReadOnly = True
            dgList.Columns(5).ReadOnly = True
            dgList.Columns(6).ReadOnly = True
            dgList.Columns(7).ReadOnly = True
            dgList.Columns(1).DefaultCellStyle.Format = "d"
            dgList.Columns(2).DefaultCellStyle.Format = "d"
            dgList.Columns(8).DefaultCellStyle.Format = "N5"
        End If
    End Sub

    Public Sub New(ByVal POStr As String, ByVal UserStr As String)
        PONo = POStr
        UserID = UserStr
        InitializeComponent()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim shipno, itemp As String
        Dim irow As Integer

        irow = dgList.CurrentCell.RowIndex
        shipno = dgList.Item(0, irow).Value.ToString
        itemp = dgList.Item(3, irow).Value.ToString

        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbl_po_schedule " & _
                 "where po_no='" & txtPO.Text & "' AND shipment_no ='" & shipno & "'"

        ErrMsg = "Failed when deleting data"
        irow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If irow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete PO Schedule")
        Else
            f_msgbox_successful("Delete Data")
        End If
        StatD = ""
        refreshGrid()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim irow, crow, iship, dship, itemp As Integer
        Dim ditem, dvsl, dqty, detd, deta As String

        StatD = ""
        SQLstr = "DELETE from tbl_po_schedule where po_no='" & txtPO.Text & "'"
        crow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        SQLstr = "INSERT INTO tbl_po_schedule (po_no, po_item, shipment_no, est_delivery_dt, est_arrival_dt, vessel, quantity) VALUE "
        dgList.CommitEdit(DataGridViewDataErrorContexts.Commit)
        For irow = 0 To dgList.RowCount - 2
            dship = dgList.Rows(irow).Cells(0).Value
            If itemp <> dship Then
                iship = iship + 1
                itemp = dship
            End If
            detd = Format(CDate(dgList.Rows(irow).Cells(1).Value.ToString), "yyyy-MM-dd")
            deta = Format(CDate(dgList.Rows(irow).Cells(2).Value.ToString), "yyyy-MM-dd")

            dvsl = dgList.Rows(irow).Cells(3).Value.ToString
            ditem = dgList.Rows(irow).Cells(4).Value.ToString
            dqty = GetNum(dgList.Rows(irow).Cells(8).Value.ToString)
            dqty = GetNum2(dqty)

            SQLstr = SQLstr & "('" & txtPO.Text & "'," & ditem & "," & iship & ",'" & detd & "','" & deta & "','" & dvsl & "'," & dqty & "), "
        Next
        SQLstr = Mid(SQLstr, 1, Len(SQLstr) - 2)
        crow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If crow < 0 Then
            MsgBox("Save failed...", MsgBoxStyle.Information, "Save PO Schedule")
            Exit Sub
        End If
        StatD = ""
        btnNew.Enabled = True And (UserData.UserCT = crtcode.Text)
        btnSave.Enabled = False And (UserData.UserCT = crtcode.Text)
        refreshGrid()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        StatD = "New"
        btnNew.Enabled = False And (UserData.UserCT = crtcode.Text)
        btnSave.Enabled = True And (UserData.UserCT = crtcode.Text)
        refreshGrid()
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

    Private Sub dgList_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgList.CellEndEdit
        If dgList.RowCount - 1 > 0 Then btnSave.Enabled = (UserData.UserCT = crtcode.Text)

        If dgList.Columns(e.ColumnIndex).Name = "EstDeliveryDate" Then
            dgList.Rows(e.RowIndex).Cells(2).Value = DateAdd(DateInterval.Day, dgList.Rows(e.RowIndex).Cells(7).Value, dgList.Rows(e.RowIndex).Cells(1).Value)
        End If

    End Sub

    Private Sub dgList_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgList.CellContentClick
        If dgList.RowCount - 1 > 0 Then btnDelete.Enabled = (UserData.UserCT = crtcode.Text)
    End Sub

    Private Sub crtcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crtcode.TextChanged

    End Sub
End Class
