Public Class Form1
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader



    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dts As DataTable
        '====================================
        ''contoh kalau dlm query trdpt field iBLOB, field tsb tinggal di CAST as CHAR (contoh field container_size)
        '====================================

        SQLstr = _
                    "Select t1.*, m1.effective_kurs rate, t2.container_size from " & _
        "(Select t2.est_arrival_dt, t2.notice_arrival_dt, t2.vessel, t2.supplier_code, m3.supplier_name, t2.shipping_line, m5.line_name,  " & _
        "t1.po_no, t3.contract_no, m6.company_code, m8.company_name, t2.plant_code, m6.plant_name, t2.port_code, m7.port_name,  " & _
        "m4.group_name, t1.material_code, m1.material_name, t4.country_code, m9.country_name, t2.received_doc_dt, t2.pib_dt, t5.invoice_no, t5.invoice_dt,  " & _
        "t1.quantity, (t4.price * t1.quantity) final_amount, t2.currency_code, t1.shipment_no, t1.po_item, t1.pack_quantity, t1.pack_code, m2.pack_name " & _
        "From tbl_shipping_detail t1, tbl_shipping t2, tbl_po t3, tbl_po_detail t4, tbl_shipping_invoice t5, " & _
        "tbm_material m1, tbm_packing m2, tbm_supplier m3, tbm_material_group m4, tbm_lines m5, " & _
        "tbm_plant m6, tbm_port m7, tbm_company m8, tbm_country m9 " & _
        "Where t1.shipment_no=t2.shipment_no and t2.status<>'Rejected' " & _
        "and t1.po_no=t3.po_no and t1.po_no=t4.po_no and t1.po_item=t4.po_item and t1.material_code=t4.material_code " & _
        "and t1.shipment_no=t5.shipment_no and t1.po_no=t5.po_no " & _
        "and t1.material_code=m1.material_code and t1.pack_code=m2.pack_code " & _
        "and t2.supplier_code=m3.supplier_code and m1.group_code=m4.group_code " & _
        "and t2.shipping_line=m5.line_code and t2.plant_code=m6.plant_code and t2.port_code=m7.port_code " & _
        "and m6.company_code=m8.company_code and t4.country_code=m9.country_code) t1 " & _
        "Left Join  " & _
        "tbm_kurs m1 " & _
        "On t1.currency_code=m1.currency_code and t1.invoice_dt=m1.effective_date " & _
        "Left Join  " & _
        "(Select shipment_no, cast((GROUP_CONCAT(CONCAT(unit_tot,'x',unit_code) separator ', ')) as char) container_size " & _
        "From " & _
        "(Select sum(1) unit_tot, unit_code, shipment_no " & _
        "From tbl_shipping_cont  " & _
        "Group by shipment_no, unit_code) t1 " & _
        "Group by shipment_no) t2 " & _
        "On t1.shipment_no = t2.shipment_no"
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts

    End Sub
End Class