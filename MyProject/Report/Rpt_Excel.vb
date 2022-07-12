Imports xlns = Microsoft.Office.Interop.Excel
'Imports xlnw = Microsoft.Office.Interop.Excel.Gridlines
Imports Microsoft.Office.Interop.Excel.Constants
Imports Microsoft.Office.Interop.Excel.XlBordersIndex
Imports Microsoft.Office.Interop.Excel.XlLineStyle
Imports Microsoft.Office.Interop.Excel.XlBorderWeight
Imports Microsoft.Office.Interop.Excel.XlHAlign
Imports vbs = Microsoft.VisualBasic.Strings




Public Class Rpt_Excel
    Private _v_type As String
    Private _date1, _date2 As Date
    Private _field1, _field2, _field3, _field4, _field5, _field6, _field7 As String
    Private _con1, _con2, _con3, _conb As Boolean
    'Dim aReport As New ReportClass
    Dim v_pos1, v_pos2 As Integer
    Dim SQlStr, SQlStrA, SQlStr_0, SQlStr_1, SQlStr_2, SQlStr_3, SQlStr_4, ErrMsg As String
    Dim MyReader As MySqlDataReader, MyReader2 As MySqlDataReader
    Dim MyDataReader, MyDataReader2, MyDataReader3 As DataTableReader
    Dim vrow, vcol, vcol_akhir, vrow_datastart, vcol_Actual As Integer
    Dim app As New xlns.Application
    Dim wb As xlns.Workbook = app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)
    'Dim ww As Microsoft.Office.Interop.Excel.Windows = app.Windows.Application
    '.Workbook = app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)
    'Dim label1, label2, label3, label4, label5, label6, label7, label8, label9 As String

    Dim file_name As String


    Public Property v_type() As String
        Get
            Return _v_type
        End Get
        Set(ByVal Value As String)
            _v_type = Value
        End Set
    End Property
    Public Property date1() As Date
        Get
            Return _date1
        End Get
        Set(ByVal Value As Date)
            _date1 = Value
        End Set
    End Property
    Public Property date2() As Date
        Get
            Return _date2
        End Get
        Set(ByVal Value As Date)
            _date2 = Value
        End Set
    End Property
    Public Property field1() As String
        Get
            Return _field1
        End Get
        Set(ByVal Value As String)
            _field1 = Value
        End Set
    End Property
    Public Property field2() As String
        Get
            Return _field2
        End Get
        Set(ByVal Value As String)
            _field2 = Value
        End Set
    End Property
    Public Property field3() As String
        Get
            Return _field3
        End Get
        Set(ByVal Value As String)
            _field3 = Value
        End Set
    End Property
    Public Property field4() As String
        Get
            Return _field4
        End Get
        Set(ByVal Value As String)
            _field4 = Value
        End Set
    End Property
    Public Property field5() As String
        Get
            Return _field5
        End Get
        Set(ByVal Value As String)
            _field5 = Value
        End Set
    End Property
    Public Property field6() As String
        Get
            Return _field6
        End Get
        Set(ByVal Value As String)
            _field6 = Value
        End Set
    End Property
    Public Property field7() As String
        Get
            Return _field7
        End Get
        Set(ByVal Value As String)
            _field7 = Value
        End Set
    End Property
    Public Property con1() As Boolean
        Get
            Return _con1
        End Get
        Set(ByVal Value As Boolean)
            _con1 = Value
        End Set
    End Property
    Public Property con2() As Boolean
        Get
            Return _con2
        End Get
        Set(ByVal Value As Boolean)
            _con2 = Value
        End Set
    End Property
    Public Property con3() As Boolean
        Get
            Return _con3
        End Get
        Set(ByVal Value As Boolean)
            _con3 = Value
        End Set
    End Property
    Public Property conb() As Boolean
        Get
            Return conb
        End Get
        Set(ByVal Value As Boolean)
            _conb = Value
        End Set
    End Property

    Private Sub Rpt_Excel_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        If (app IsNot Nothing) Then
            'app.Quit()
            app = Nothing
            Dim myProcesses() As Process
            Dim myProcess As Process

            myProcesses = Process.GetProcessesByName("EXCEL")
            For Each myProcess In myProcesses
                myProcess.Kill()
            Next
        End If
    End Sub

    Private Sub Rpt_Excel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQlStr, SQlStrA, SQlStr_0, SQlStr_1, SQlStr_2, SQlStr_3, SQlStr_4, SQlStr_5, ErrMsg As String
        Dim MyReader As MySqlDataReader
        Dim v_length As Integer
        Dim v_ShipFR, v_shipTo As Date
        Dim v_CompCd, v_Plant, v_Supp, v_MatGrp, v_Mat As String

        '--------------------------------
        'QUERY AWAL
        '--------------------------------
        'MsgBox("Please close and SAVE all Excel application opened !", MsgBoxStyle.Information)

        Select Case CStr(v_type)
            Case "RP03"
                SQlStr = "Select " & _
                        "if(refer_to='purchaseddt',date_format(purchaseddt,'%m-%d-%Y'), " & _
                        "if(refer_to='received_copydoc_dt',date_format(received_copydoc_dt,'%m-%d-%Y')," & _
                        "if(refer_to='received_doc_dt',date_format(received_doc_dt,'%m-%d-%Y')," & _
                        "if(refer_to='est_delivery_dt',date_format(est_delivery_dt,'%m-%d-%Y')," & _
                        "if(refer_to='est_arrival_dt',date_format(est_arrival_dt,'%m-%d-%Y')," & _
                        "if(refer_to='notice_arrival_dt',date_format(notice_arrival_dt,'%m-%d-%Y')," & _
                        "if(refer_to='clr_dt',date_format(clr_dt,'%m-%d-%Y'),0))))))) DueDate," & _
                        "Supplier, PONo, MaterialGroup, MaterialName, " & _
                        "CountryOfOrigin, ArrivalDate, if(notice_arrival_dt is null or notice_arrival_dt='','estimated','allocated') BudgetAlocation, ShipmentType, class_code, Amount, Currency " & _
                        "From " & _
                        "( " & _
                        "Select t5.company_code, t1.supplier_code, m1.supplier_name Supplier, t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName, " & _
                        "t4.country_code, m4.country_name CountryOfOrigin," & _
                        "if(t1.notice_arrival_dt is null or t1.notice_arrival_dt = '', date_format(t1.est_arrival_dt,'%m-%d-%Y'), date_format(t1.notice_arrival_dt,'%m-%d-%Y')) ArrivalDate," & _
                        "t2.pack_code, m6.pack_name ShipmentType," & _
                        "t5.payment_code, m5.payment_name, m5.payment_days, m5.refer_to, m5.class_code, t3.invoice_amount Amount, t1.currency_code Currency, " & _
                        "adddate(t5.purchaseddt,m5.payment_days) purchaseddt, adddate(t1.received_copydoc_dt,m5.payment_days) received_copydoc_dt, " & _
                        "adddate(t1.received_doc_dt,m5.payment_days) received_doc_dt, adddate(t1.est_delivery_dt,m5.payment_days) est_delivery_dt, " & _
                        "adddate(t1.est_arrival_dt,m5.payment_days) est_arrival_dt, adddate(t1.notice_arrival_dt,m5.payment_days) notice_arrival_dt, " & _
                        "adddate(t1.clr_dt,m5.payment_days) clr_dt " & _
                        "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, tbl_po t5, " & _
                        "tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, tbm_payment_term m5, tbm_packing m6 " & _
                        "Where(t1.shipment_no = t2.shipment_no) " & _
                        "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                        "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                        "and t2.po_no=t5.po_no " & _
                        "and t1.supplier_code=m1.supplier_code " & _
                        "and t2.material_code=m2.material_code " & _
                        "and m2.group_code=m3.group_code " & _
                        "and t4.country_code=m4.country_code " & _
                        "and t5.payment_code=m5.payment_code " & _
                        "and t2.pack_code=m6.pack_code " & _
                        "AND (T5.COMPANY_CODE = '" & field1 & "' or ''='" & field1 & "')" & _
                        "AND (M3.group_code = '" & field2 & "' or ''='" & field2 & "')" & _
                        "AND (t2.material_code = '" & field3 & "' or ''='" & field3 & "')" & _
                        "AND (M5.CLASS_CODE = '" & field4 & "' or ''='" & field4 & "')" & _
                        "AND (t5.PAYMENT_CODE = '" & field5 & "' or ''='" & field5 & "')" & _
                        "AND (T1.CURRENCY_CODE = '" & field6 & "' or ''='" & field6 & "')                "

                If conb = True Then
                    SQlStr = SQlStr & "group by t4.material_code, m4.country_name "
                End If
                SQlStr = SQlStr & ") t1 where "
                If con1 = True Then
                    SQlStr = SQlStr & "notice_arrival_dt is not null and "
                ElseIf con2 = True Then
                    SQlStr = SQlStr & "notice_arrival_dt is null and "
                End If
                SQlStr = SQlStr & "if(refer_to='purchaseddt',purchaseddt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND purchaseddt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "', " & _
                        "if(refer_to='received_copydoc_dt',received_copydoc_dt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND received_copydoc_dt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "'," & _
                        "if(refer_to='received_doc_dt',received_doc_dt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND received_copydoc_dt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "'," & _
                        "if(refer_to='est_delivery_dt',est_delivery_dt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND est_delivery_dt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "'," & _
                        "if(refer_to='est_arrival_dt',est_arrival_dt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND est_arrival_dt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "'," & _
                        "if(refer_to='notice_arrival_dt',notice_arrival_dt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND notice_arrival_dt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "'," & _
                        "if(refer_to='clr_dt',clr_dt>= '" & vbs.Format(date1, "yyyy/MM/dd") & "' AND clr_dt <= '" & vbs.Format(date2, "yyyy/MM/dd") & "',''))))))) "
                SQlStr = SQlStr & "order by DueDate"

                'get kolom utk judul payment 
                SQlStr_0 = "select * from tbm_payment_class order by class_code"

                'Call CreateExcelAppAndNewWorkbook(v_type, SQlStr, SQlStr_0)
            Case "RP04"
                SQlStr = "Select year(DueDate) xYear, month(DueDate) xMonth, week(DueDate) xWeek, count(PONo) NumberOfPO, class_code, sum(Amount) Amount, Currency from " & _
                        "( " & _
                        "Select t1.company_code, t1.group_code,t1.material_code,t1.payment_code, " & _
                        "if(refer_to='purchaseddt',purchaseddt, " & _
                        "if(refer_to='received_copydoc_dt',received_copydoc_dt, " & _
                        "if(refer_to='received_doc_dt',received_doc_dt, " & _
                        "if(refer_to='est_delivery_dt',est_delivery_dt, " & _
                        "if(refer_to='est_arrival_dt',est_arrival_dt, " & _
                        "if(refer_to='notice_arrival_dt',notice_arrival_dt, " & _
                        "if(refer_to='clr_dt',clr_dt,0))))))) DueDate, " & _
                        "PONo,  " & _
                        "class_code, Amount, Currency " & _
                        "from " & _
                        "( " & _
                        "Select t5.company_code, t1.supplier_code, m1.supplier_name Supplier, t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName,  " & _
                        "t4.country_code, m4.country_name CountryOfOrigin, " & _
                        "if(t1.notice_arrival_dt is null or t1.notice_arrival_dt = '', t1.est_arrival_dt, t1.notice_arrival_dt) ArrivalDate, " & _
                        "t2.pack_code, m6.pack_name ShipmentType, " & _
                        "t5.payment_code, m5.payment_name, m5.payment_days, m5.refer_to, m5.class_code, t3.invoice_amount Amount, t1.currency_code Currency,  " & _
                        "adddate(t5.purchaseddt,m5.payment_days) purchaseddt, adddate(t1.received_copydoc_dt,m5.payment_days) received_copydoc_dt, " & _
                        "adddate(t1.received_doc_dt,m5.payment_days) received_doc_dt, adddate(t1.est_delivery_dt,m5.payment_days) est_delivery_dt, " & _
                        "adddate(t1.est_arrival_dt,m5.payment_days) est_arrival_dt, adddate(t1.notice_arrival_dt,m5.payment_days) notice_arrival_dt, " & _
                        "adddate(t1.clr_dt,m5.payment_days) clr_dt " & _
                        "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, tbl_po t5, " & _
                        "tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, tbm_payment_term m5, tbm_packing m6 " & _
                        "Where t1.shipment_no=t2.shipment_no " & _
                        "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                        "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                        "and t2.po_no=t5.po_no " & _
                        "and t1.supplier_code=m1.supplier_code " & _
                        "and t2.material_code=m2.material_code " & _
                        "and m2.group_code=m3.group_code " & _
                        "and t4.country_code=m4.country_code " & _
                        "and t5.payment_code=m5.payment_code " & _
                        "and t2.pack_code=m6.pack_code " & _
                        "and (t5.company_code = '" & field1 & "' or '' = '" & field1 & "') " & _
                        "and (m2.group_code = '" & field2 & "' or '' = '" & field2 & "') " & _
                        "and (t2.material_code = '" & field3 & "' or '' = '" & field3 & "') " & _
                        "and (m5.class_code = '" & field4 & "' or '' = '" & field4 & "') " & _
                        "and (t5.payment_code = '" & field5 & "' or '' = '" & field5 & "') " & _
                        "and (t1.currency_code = '" & field6 & "' or '' = '" & field6 & "') " & _
                        ") t1 order by DueDate " & _
                        ") t1 " & _
                        "where duedate>='" & vbs.Format(date1, "yyyy-MM-dd") & "' and duedate<='" & vbs.Format(date2, "yyyy-MM-dd") & "' " & _
                        "group by year(DueDate), month(DueDate), week(DueDate), class_code, Currency "

                'get kolom utk judul payment 
                SQlStr_0 = "select * from tbm_payment_class order by class_code"

                'Call CreateExcelAppAndNewWorkbook(v_type, SQlStr, SQlStr_0)
            Case "RP09"  'Created by Prie 01/05/2009
                SQlStr = "Select t1.*, t2.EMKLCode, t2.EMKLName, t2.CostCode ActualCostCode, t2.Amount ActualAmount from ( "
                SQlStr_0 = ") t1 Left Join("
                SQlStr_3 = ") t2 " & _
                           "On t1.shipment_no=t2.shipment_no " & _
                           "Where(t2.Amount Is Not null Or t2.Amount <> 0) "
                SQlStr_5 = "AND (NoticeOnArrival >= '" & vbs.Format(date1, "yyyy-MM-dd") & "' and NoticeOnArrival<='" & vbs.Format(date2, "yyyy-MM-dd") & "')" & _
                           "AND (COMPANY_CODE = '" & field1 & "' or ''='" & field1 & "')" & _
                           "AND (MaterialGroupCode = '" & field2 & "' or ''='" & field2 & "')" & _
                           "AND (Material_Code = '" & field3 & "' or ''='" & field3 & "')" & _
                           "AND (ShippingLines = '" & field4 & "' or ''='" & field4 & "')" & _
                           "AND (EMKLCOde = '" & field5 & "' or ''='" & field5 & "')"
                If con1 = True And con2 = True Then
                    SQlStr_1 = "Select t3.company_code,t1.shipment_no, t1.bl_no BLNo, t1.supplier_code, " & _
                               "concat(t2.po_no,'(',v1.orde,')') PONo, t1.notice_arrival_dt NoticeOnArrival, m2.group_code MaterialGroupCode, t2.material_code, " & _
                               "m2.material_name MaterialName, t1.sppb_dt SPPBDate, t1.clr_dt ClearanceDate, t1.free_time FTD, t1.shipping_line, m1.line_name ShippingLines " & _
                               "fro  tbl_shipping t1, tbl_shipping_detail t2,tbl_po t3, " & _
                               "tbm_lines m1, tbm_material m2, " & _
                               "vw_po_by_shipord v1 " & _
                               "where(t1.shipment_no = t2.shipment_no) " & _
                               "and t2.shipment_no = v1.shipment_no and t2.po_no=v1.po_no " & _
                               "and t1.shipping_line=m1.line_code " & _
                               "and t2.material_code=m2.material_code and t2.po_no=t3.po_no "

                    SQlStr_2 = "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, t2.cost_code CostCode, sum(t2.cost_amount) Amount " & _
                             "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1 " & _
                             "where t1.findoc_status <> 'Rejected' and t1.findoc_type = 'VP' " & _
                             "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                             "and t1.findoc_to=m1.company_code Group by t1.shipment_no, t1.findoc_to, t2.cost_code "

                    SQlStr = SQlStr + SQlStr_1 + SQlStr_0 + SQlStr_2 + SQlStr_3 + SQlStr_5 + "order by t1.shipment_no, t1.PONo, t2.CostCode "

                    SQlStr_4 = "Select CostCat_Code CostCode, CostCat_Name CostName From tbm_costcategory Where active=1 order by CostCat_Code"

                ElseIf con2 = True Then
                    SQlStr_1 = "Select distinct t3.company_code,t1.shipment_no, t1.bl_no BLNo, t1.supplier_code, " & _
                               "concat(t2.po_no,'(',v1.orde,')') PONo, t1.notice_arrival_dt NoticeOnArrival, m2.group_code MaterialGroupCode,t2.material_code, " & _
                               "m3.group_name MaterialGroup, t1.sppb_dt SPPBDate, t1.clr_dt ClearanceDate, t1.free_time FTD, t1.shipping_line, m1.line_name ShippingLines " & _
                               "from tbl_shipping t1, tbl_shipping_detail t2,tbl_po t3, " & _
                               "tbm_lines m1, tbm_material m2, tbm_material_group m3, " & _
                               "vw_po_by_shipord v1 " & _
                               "where(t1.shipment_no = t2.shipment_no) " & _
                               "and t2.shipment_no = v1.shipment_no and t2.po_no=v1.po_no " & _
                               "and t1.shipping_line=m1.line_code " & _
                               "and t2.material_code=m2.material_code " & _
                               "and m2.group_code=m3.group_code and t2.po_no=t3.po_no "

                    SQlStr_2 = "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, t2.cost_code CostCode, sum(t2.cost_amount) Amount " & _
                             "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1 " & _
                             "where t1.findoc_status <> 'Rejected' and t1.findoc_type = 'VP' " & _
                             "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                             "and t1.findoc_to=m1.company_code " & _
                             "Group by t1.shipment_no, t1.findoc_to, t2.cost_code "

                    SQlStr = SQlStr + SQlStr_1 + SQlStr_0 + SQlStr_2 + SQlStr_3 + SQlStr_5 + "order by t1.shipment_no, t1.PONo, t2.CostCode "
                    SQlStr_4 = "Select distinct m1.subgroup_code CostCode, m1.subgroup_name CostName From tbm_costcategory_subgroup m1, tbm_costcategory m2 " & _
                               "where(m1.subgroup_code = m1.subgroup_code And m2.active = 1) " & _
                               "order by m1.subgroup_code"
                ElseIf con1 = True Then
                    SQlStr_1 = "Select t3.company_code,t1.shipment_no, t1.bl_no BLNo, t1.supplier_code, " & _
                               "concat(t2.po_no,'(',v1.orde,')') PONo, t1.notice_arrival_dt NoticeOnArrival, m2.group_code MaterialGroupCode, t2.material_code, " & _
                               "m2.material_name MaterialName, t1.sppb_dt SPPBDate, t1.clr_dt ClearanceDate, t1.free_time FTD, t1.shipping_ ine, m1.line_name ShippingLines " & _
                               "from tbl_shipping t1, tbl_shipping_detail t2,tbl_po t3, " & _
                               "tbm_lines m1, tbm_material m2, " & _
                               "vw_po_by_shipord v1 " & _
                               "where(t1.shipment_no = t2.shipment_no) " & _
                               "and t2.shipment_no = v1.shipment_no and t2.po_no=v1.po_no " & _
                               "and t1.shipping_line=m1.line_code " & _
                               "and t2.material_code=m2.material_code and t2.po_no=t3.po_no "

                    SQlStr_2 = "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, m2.subgroup_code CostCode, sum(t2.cost_amount) Amount " & _
                               "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1, tbm_costcategory m2 " & _
                               "where t1.findoc_status <> 'Rejected' " & _
                               "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                               "and t1.findoc_to=m1.company_code " & _
                               "and t2.cost_code=m2.costcat_code " & _
                               "Group by t1.shipment_no, t1.findoc_to, m2.subgroup_code"

                    SQlStr = SQlStr + SQlStr_1 + SQlStr_0 + SQlStr_2 + SQlStr_3 + SQlStr_5 + "order by t1.shipment_no, t1.PONo, t2.CostCode "
                    SQlStr_4 = "Select distinct m1.subgroup_code CostCode, m1.subgroup_name CostName From tbm_costcategory_subgroup m1, tbm_costcategory m2 " & _
                               "where(m1.subgroup_code = m1.subgroup_code And m2.active = 1) " & _
                               "order by m1.subgroup_code"
                End If
                'Call CreateExcelAppAndNewWorkbookRP09(v_type, SQlStr, SQlStr_4)

            Case "RP10"  'Created by Prie 08/05/2009
                SQlStr_5 = "(NoticeOnArrival >= '" & vbs.Format(date1, "yyyy-MM-dd") & "' and NoticeOnArrival<='" & vbs.Format(date2, "yyyy-MM-dd") & "')" & _
                           "AND (Company_Code = '" & field1 & "' or ''='" & field1 & "')" & _
                           "AND (Supplier_code = '" & field2 & "' or ''='" & field2 & "')" & _
                           "AND (MaterialGroupCode = '" & field3 & "' or ''='" & field3 & "')" & _
                           "AND (MaterialGroup = '" & field4 & "' or ''='" & field4 & "')" & _
                           "AND (ShippingLines = '" & field5 & "' or ''='" & field5 & "')" & _
                           "AND (t2.EMKLCOde = '" & field6 & "' or ''='" & field6 & "') "
                If con1 = True Then
                    SQlStr = "Select t1.*,t2.EMKLCode, t2.EMKLName, " & _
                    "t2.CostCode ActualCostCode, t2.Amount ActualAmount, " & _
                    "t3.CostCode EstimatedCostCode, t3.Amount EstimatedAmount from " & _
                    "(" & _
                    "Select distinct t3.company_code,t1.shipment_no, t1.bl_no, t1.supplier_code, " & _
                    "concat(t2.po_no,'(',v1.orde,')') PONo, t1.notice_arrival_dt NoticeOnArrival, m2.group_code MaterialGroupCode, " & _
                    "m3.group_name MaterialGroup, t1.sppb_dt SPPBDate, t1.clr_dt ClearanceDate, t1.free_time FTD, t1.shipping_line, m1.line_name ShippingLines " & _
                    "from tbl_shipping t1, tbl_shipping_detail t2,tbl_po t3, " & _
                    "tbm_lines m1, tbm_material m2, tbm_material_group m3," & _
                    "vw_po_by_shipord v1 " & _
                    "where(t1.shipment_no = t2.shipment_no) " & _
                    "and t2.shipment_no = v1.shipment_no and t2.po_no=v1.po_no " & _
                    "and t1.shipping_line=m1.line_code " & _
                    "and t2.material_code=m2.material_code " & _
                    "and m2.group_code=m3.group_code " & _
                    "and t2.po_no=t3.po_no " & _
                    ") t1 " & _
                    "Left Join " & _
                    "(" & _
                    "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, m2.subgroup_code CostCode, sum(t2.cost_amount) Amount " & _
                    "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1, tbm_costcategory m2 " & _
                    "where t1.findoc_status <> 'Rejected' and t1.findoc_type = 'VP' " & _
                    "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                    "and t1.findoc_to=m1.company_code " & _
                    "and t2.cost_code=m2.costcat_code " & _
                    "Group by t1.shipment_no, t1.findoc_to, m2.subgroup_code  " & _
                    ") t2 " & _
                    "On t1.shipment_no=t2.shipment_no " & _
                    "Left Join " & _
                    "( " & _
                    "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, m2.subgroup_code CostCode, sum(t2.cost_amount) Amount " & _
                    "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1, tbm_costcategory m2 " & _
                    "where t1.findoc_status <> 'Rejected' and t1.findoc_type = 'CC' " & _
                    "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                    "and t1.findoc_to=m1.company_code " & _
                    "and t2.cost_code=m2.costcat_code " & _
                    "Group by t1.shipment_no, t1.findoc_to, m2.subgroup_code  " & _
                    ") t3 " & _
                    "On t1.shipment_no=t3.shipment_no and t2.EMKLCode=t3.EMKLCode and t2.CostCode=t3.CostCode Where "

                    SQlStr_1 = "and (t2.Amount Is Not null Or t2.Amount <> 0) or (t3.Amount Is Not null Or t3.Amount <> 0) order by t1.shipment_no, t1.PONo, t2.CostCode "
                ElseIf con1 = False Then
                    SQlStr = "Select t1.*, t2.EMKLCode, t2.EMKLName, " & _
                    "t2.Amount ActualAmount, " & _
                    "t3.Amount EstimatedAmount from " & _
                    "( " & _
                    "Select distinct t3.company_code,t1.shipment_no, t1.bl_no, t1.supplier_code, " & _
                    "concat(t2.po_no,'(',v1.orde,')') PONo, t1.notice_arrival_dt NoticeOnArrival, m2.group_code MaterialGroupCode, " & _
                    "m3.group_name MaterialGroup, t1.sppb_dt SPPBDate, t1.clr_dt ClearanceDate, t1.free_time FTD, t1.shipping_line, m1.line_name ShippingLines " & _
                    "from tbl_shipping t1, tbl_shipping_detail t2,tbl_po t3, " & _
                    "tbm_lines m1, tbm_material m2, tbm_material_group m3, " & _
                    "vw_po_by_shipord v1 " & _
                    "where(t1.shipment_no = t2.shipment_no) " & _
                    "and t2.shipment_no = v1.shipment_no and t2.po_no=v1.po_no " & _
                    "and t1.shipping_line=m1.line_code " & _
                    "and t2.material_code=m2.material_code " & _
                    "and m2.group_code=m3.group_code " & _
                    "and t2.po_no=t3.po_no " & _
                    ") t1 " & _
                    "Left Join " & _
                    "(" & _
                    "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, sum(t2.cost_amount) Amount " & _
                    "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1, tbm_costcategory m2 " & _
                    "where t1.findoc_status <> 'Rejected' and t1.findoc_type = 'VP' " & _
                    "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                    "and t1.findoc_to=m1.company_code " & _
                    "Group by t1.shipment_no, t1.findoc_to " & _
                    ") t2 " & _
                    "On t1.shipment_no=t2.shipment_no " & _
                    "Left Join " & _
                    "( " & _
                    "Select t1.shipment_no, t1.findoc_to EMKLCode, m1.company_name EMKLName, sum(t2.cost_amount) Amount " & _
                    "from tbl_shipping_doc t1, tbl_cost t2, tbm_expedition m1 " & _
                    "where t1.findoc_status <  'Rejected' and t1.findoc_type = 'CC' " & _
                    "and t1.shipment_no=t1.shipment_no and t1.ord_no=t1.ord_no " & _
                    "and t1.findoc_to=m1.company_code " & _
                    "Group by t1.shipment_no, t1.findoc_to " & _
                    ") t3 " & _
                    "On t1.shipment_no=t3.shipment_no and t2.EMKLCode=t3.EMKLCode Where "

                    SQlStr_1 = " and (t2.Amount is not null or t2.Amount <> 0) or (t3.Amount is not null or t3.Amount <> 0) order by t1.shipment_no, t1.PONo "

                End If
                SQlStr = SQlStr + SQlStr_5 + SQlStr_1

                SQlStr_4 = "Select distinct m1.subgroup_code CostCode, m1.subgroup_name CostName From tbm_costcategory_subgroup m1, tbm_costcategory m2 " & _
                           "where m1.subgroup_code=m1.subgroup_code and m2.active=1 order by m1.subgroup_code "
                'Call CreateExcelAppAndNewWorkbookRP10(v_type, SQlStr, SQlStr_4)

            Case Else
                Me.Hide()
                MsgBox("Tidak ada tipe laporan yang sesuai untuk dicetak.", MsgBoxStyle.Exclamation)
                Me.Close()
                Exit Sub
        End Select

        ErrMsg = "Failed when reading data."
        If DBQueryGetTotalRows(SQlStr, MyConn, ErrMsg, False, UserData) <= 0 Then
            Me.Hide()
            MsgBox("No data matches with your selection.", MsgBoxStyle.Information)
            Me.Close()
            Exit Sub
        End If

        MsgBox("Please SAVE and close all Excel application opened !", MsgBoxStyle.Information)
        Select CStr(v_type)
            Case "RP03"
                Call CreateExcelAppAndNewWorkbook(v_type, SQlStr, SQlStr_0)
            Case "RP04"
                Call CreateExcelAppAndNewWorkbook(v_type, SQlStr, SQlStr_0)
            Case "RP09"
                Call CreateExcelAppAndNewWorkbookRP09(v_type, SQlStr, SQlStr_4)
            Case "RP10"
                Call CreateExcelAppAndNewWorkbookRP10(v_type, SQlStr, SQlStr_4)
        End Select
    End Sub
    Public Function f_assignoptxl(ByVal v_rpt As String, ByVal index As Integer, ByVal a As Boolean, ByVal b As Boolean, ByVal c As Boolean, ByVal vconb As Boolean) As String
        Select Case index
            Case 8
                If v_rpt = "RP03" Or v_rpt = "RP04" Then
                    If a = True Then
                        f_assignoptxl = "Allocated"
                    ElseIf b = True Then
                        f_assignoptxl = "Estimated"
                    ElseIf c = True Then
                        f_assignoptxl = "All"
                    End If
                End If
        End Select
    End Function
    Private Sub CreateExcelAppAndNewWorkbook(ByVal strType As String, ByVal SQlStr As String, ByVal SQlStr_0 As String)
        Dim xlsheet As New xlns.Worksheet
        Dim xlwindow As xlns.Workbook

        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)

        Try
            ' Lihat Excel automation.
            '
            ' Comment baris code berikut, jika tdk ingin lihat excel instancenya.         
            app.Visible = False

            ' Buat workbook baru.

            Dim vcol_payment, jmlrec As Integer
            ' workbook task with wb object start here.
            ' misalnya: input angka 100 di cell A1 di ActiveSheet (Sheet1):

            '-----------------------------------------
            '=====            RP03            ========
            '-----------------------------------------
            If strType = "RP03" Then
                ErrMsg = "Gagal baca data detail."
                'Write judul dulu
                vrow = 2
                vcol = 1                
                xlsheet.Cells(vrow, 1) = Mid(Me.Text, 18, Len(Me.Text) - 18)
                vrow = vrow + 1

                'write selection screen dan 'write isi selection screen
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 1, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigndate(date1, date2)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 2, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtComp", field1)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 3, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMatGrp", field2)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 4, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMat", field3)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 5, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayType", field4)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 6, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayTerm", field5)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 7, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtCurr", field6)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 8, con1, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & f_assignoptxl(v_type, 8, con1, con2, con3, False)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 9, False, con2, False, False)
                vrow = vrow + 1



                'write header
                vrow = 14
                vcol = 1

                xlsheet.Cells(vrow, vcol) = "Due Date"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Supplier"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "PO No"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Material Group"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Material Name"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Country Of Origin"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Arrival Date"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Budget Allocation"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Shipment Type"
                vcol = vcol + 1

                jmlrec = DBQueryGetTotalRows(SQlStr_0, MyConn, ErrMsg, True, UserData)
                vcol_payment = vcol
                xlsheet.Cells(vrow, vcol) = "Payment Type"
                xlsheet.Range(xlsheet.Cells(vrow, vcol_payment), xlsheet.Cells(vrow, vcol + jmlrec - 1)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol_payment), xlsheet.Cells(vrow, vcol + jmlrec - 1)).HorizontalAlignment = xlHAlignCenter

                vrow = vrow + 1
                MyReader = DBQueryMyReader(SQlStr_0, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        xlsheet.Cells(vrow, vcol) = MyReader.GetString("class_name")
                        vcol = vcol + 1
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                vrow = vrow - 1
                xlsheet.Cells(vrow, vcol) = "Total"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Currency"
                vcol = vcol + 1
                vcol_akhir = vcol - 1

                For i = 1 To vcol - 1
                    Call f_kotak(14, i, vrow + 1, i)
                Next i
                For i = vcol_payment To vcol - 3
                    Call f_kotak(15, i, vrow + 1, i)
                Next i

                'fill the data
                vrow = vrow + 2
                vrow_datastart = vrow
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        vcol = 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("DUEDATE")
                            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("SUPPLIER")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("PONO")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("MATERIALGROUP")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("MaterialName")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("CountryOfOrigin")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("ArrivalDate")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("BudgetAlocation")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("ShipmentType")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol_akhir - 1) = MyReader.GetString("Amount")
                            'xlsheet.Range(xlsheet.Cells(vrow, vcol_akhir - 1), xlsheet.Cells(vrow, vcol_akhir - 1)).NumberFormat = "#,##0.00;[Red](#,##0)"
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir - 1) = 0
                        End Try
                        vcol = vcol + 1


                        Try
                            xlsheet.Cells(vrow, vcol_akhir) = MyReader.GetString("currency")
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir) = ""
                        End Try

                        Try
                            xlsheet.Cells(vrow, vcol_akhir + 1) = MyReader.GetString("Amount")
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir + 1) = ""
                        End Try

                        Try
                            xlsheet.Cells(vrow, vcol_akhir + 2) = MyReader.GetString("Class_code")
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir + 2) = ""
                        End Try
                        vrow = vrow + 1
                    End While
                End If
                CloseMyReader(MyReader, UserData)



                For Z = vrow_datastart To vrow - 1
                    vcol = vcol_payment
                    MyReader = DBQueryMyReader(SQlStr_0, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            'isi rumus di kolom excel
                            xlsheet.Cells(Z, vcol) = "=IF($" & f_getletter(vcol_akhir + 2) & Z & "=" & MyReader.GetString("Class_code") & ";$" & f_getletter(vcol_akhir + 1) & Z & ";)"
                            'xlsheet.Range(xlsheet.Cells(Z, vcol), xlsheet.Cells(Z, vcol)).NumberFormat = "#,##0.00"
                            vcol = vcol + 1
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                Next Z

                For i = 1 To vcol + 1
                    Call f_kotak(vrow_datastart, i, vrow - 1, i)
                Next i

                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Name = "Tahoma"
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Size = 8
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_payment - 1)).EntireColumn.HorizontalAlignment = xlLeft
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.AutoFit()
                xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, 1)).Cells.Font.Size = 16
                xlsheet.Range(xlsheet.Cells(vrow, vcol_akhir + 2), xlsheet.Cells(vrow, vcol_akhir + 1)).EntireColumn.Hidden = True
                'xlwindow.Close()
                'app.Quit()

            End If

            '-----------------------------------------
            '=====            RP04            ========
            '-----------------------------------------
            If strType = "RP04" Then
                ErrMsg = "Gagal baca data detail."
                'Write judul dulu
                vrow = 2
                vcol = 1
                xlsheet.Cells(vrow, 1) = Mid(Me.Text, 18, Len(Me.Text) - 18)
                vrow = vrow + 1

                'write selection screen dan 'write isi selection screen
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 1, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigndate(date1, date2)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 2, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtComp", field1)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 3, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMatGrp", field2)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 4, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMat", field3)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 5, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayType", field4)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 6, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayTerm", field5)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 7, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtCurr", field6)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 8, con1, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & f_assignoptxl(v_type, 8, con1, con2, con3, False)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 9, False, con2, False, False)
                vrow = vrow + 1



                'write header
                vrow = 14
                vcol = 1

                xlsheet.Cells(vrow, vcol) = "DueDate Year"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "DueDate Month"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Week"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Number of PO"
                vcol = vcol + 1

                jmlrec = DBQueryGetTotalRows(SQlStr_0, MyConn, ErrMsg, True, UserData)
                vcol_payment = vcol
                xlsheet.Cells(vrow, vcol) = "Payment Type"
                xlsheet.Range(xlsheet.Cells(vrow, vcol_payment), xlsheet.Cells(vrow, vcol + jmlrec - 1)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol_payment), xlsheet.Cells(vrow, vcol + jmlrec - 1)).HorizontalAlignment = xlHAlignCenter

                vrow = vrow + 1
                MyReader = DBQueryMyReader(SQlStr_0, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        xlsheet.Cells(vrow, vcol) = MyReader.GetString("class_name")
                        vcol = vcol + 1
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                vrow = vrow - 1
                xlsheet.Cells(vrow, vcol) = "Total"
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Currency"
                vcol = vcol + 1
                vcol_akhir = vcol - 1

                For i = 1 To vcol - 1
                    Call f_kotak(14, i, vrow + 1, i)
                Next i
                For i = vcol_payment To vcol - 3
                    Call f_kotak(15, i, vrow + 1, i)
                Next i

                'fill the data
                vrow = vrow + 2
                vrow_datastart = vrow
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        vcol = 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("XYEAR")
                            'xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("XMONTH")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("XWEEK")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("NUMBEROFPO")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1                        
                        Try
                            xlsheet.Cells(vrow, vcol_akhir - 1) = MyReader.GetString("Amount")
                            'xlsheet.Range(xlsheet.Cells(vrow, vcol_akhir - 1), xlsheet.Cells(vrow, vcol_akhir - 1)).NumberFormat = "#,##0.00;[Red](#,##0)"
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir - 1) = 0
                        End Try
                        vcol = vcol + 1


                        Try
                            xlsheet.Cells(vrow, vcol_akhir) = MyReader.GetString("currency")
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir) = ""
                        End Try

                        Try
                            xlsheet.Cells(vrow, vcol_akhir + 1) = MyReader.GetString("Amount")
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir + 1) = ""
                        End Try

                        Try
                            xlsheet.Cells(vrow, vcol_akhir + 2) = MyReader.GetString("Class_code")
                        Catch
                            xlsheet.Cells(vrow, vcol_akhir + 2) = ""
                        End Try
                        vrow = vrow + 1
                    End While
                End If
                CloseMyReader(MyReader, UserData)



                For Z = vrow_datastart To vrow - 1
                    vcol = vcol_payment
                    MyReader = DBQueryMyReader(SQlStr_0, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            'isi rumus di kolom excel
                            xlsheet.Cells(Z, vcol) = "=IF($" & f_getletter(vcol_akhir + 2) & Z & "=" & MyReader.GetString("Class_code") & ";$" & f_getletter(vcol_akhir + 1) & Z & ";)"
                            'xlsheet.Range(xlsheet.Cells(Z, vcol), xlsheet.Cells(Z, vcol)).NumberFormat = "#,##0.00"
                            vcol = vcol + 1
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                Next Z

                For i = 1 To vcol + 1
                    Call f_kotak(vrow_datastart, i, vrow - 1, i)
                Next i

                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Name = "Tahoma"
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Size = 8
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_payment - 1)).EntireColumn.HorizontalAlignment = xlLeft
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.AutoFit()
                xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, 1)).Cells.Font.Size = 16
                xlsheet.Range(xlsheet.Cells(vrow, vcol_akhir + 2), xlsheet.Cells(vrow, vcol_akhir + 1)).EntireColumn.Hidden = True
                'xlwindow.Close()
                'app.Quit()

            End If

            'FOR ALL EXCEL
            xlsheet.Protect(DrawingObjects:=True, Contents:=True, Scenarios:=True, Password:="poimport")

            app.Windows(1).DisplayGridlines = False
            app.Windows(1).DisplayHeadings = False

            'Finally save the file
            file_name = "c:\" & strType & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
            xlsheet.SaveAs(file_name)
            TextBox1.Text = file_name
            xlsheet = Nothing
            app.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'Finally
            'If (app IsNot Nothing) Then
            '    'app.Quit()
            '    app = Nothing
            '    Dim myProcesses() As Process
            '    Dim myProcess As Process

            '    myProcesses = Process.GetProcessesByName("EXCEL")
            '    For Each myProcess In myProcesses
            '        myProcess.Kill()
            '    Next
            'End If
        End Try
    End Sub

    'Private Sub f_assignfield(ByRef xlsheet As Object, ByRef vrow As Integer, ByVal vcol As Integer, ByVal v_data As String)
    '    Try
    '        xlsheet.Cells(vrow, vcol) = v_data
    '    Catch
    '        xlsheet.Cells(vrow, vcol) = ""
    '    End Try
    '    vcol = vcol + 1
    'End Sub
    Private Function f_getletter(ByVal strInt As Integer) As String
        Select Case strInt
            Case 10 : f_getletter = "J"
            Case 11 : f_getletter = "K"
            Case 12 : f_getletter = "L"
            Case 13 : f_getletter = "M"
            Case 14 : f_getletter = "N"
            Case 15 : f_getletter = "O"
            Case 16 : f_getletter = "P"
            Case 17 : f_getletter = "Q"
            Case 18 : f_getletter = "R"
            Case 19 : f_getletter = "S"
            Case 20 : f_getletter = "T"
            Case 21 : f_getletter = "U"
            Case 22 : f_getletter = "V"
            Case 23 : f_getletter = "W"
            Case 24 : f_getletter = "X"
            Case 25 : f_getletter = "Y"
            Case 26 : f_getletter = "Z"
        End Select
    End Function


    Public Sub f_kotak(ByVal brsXLa As Integer, ByVal A As Integer, ByVal BrsXL As Integer, ByVal B As Integer)

        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B))
            .Select()
            .Borders(xlDiagonalDown).LineStyle = xlNone
            .Borders(xlDiagonalUp).LineStyle = xlNone
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeLeft)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeTop)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeBottom)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
        With CType(wb.ActiveSheet, xlns.Worksheet).Range(CType(wb.ActiveSheet, xlns.Worksheet).Cells(brsXLa, A), CType(wb.ActiveSheet, xlns.Worksheet).Cells(BrsXL, B)).Borders(xlEdgeRight)
            .LineStyle = xlContinuous
            .Weight = xlThin
            .ColorIndex = xlAutomatic
        End With
    End Sub

    Private Sub CreateExcelAppAndNewWorkbookRP09(ByVal strType As String, ByVal SQlStr As String, ByVal SQlStr_4 As String)
        Dim xlsheet As New xlns.Worksheet
        Dim inApp As xlns.Application
        Dim xlwindow As xlns.Workbook
        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)

        Try
            app.Visible = False
            Dim vcol_payment, jmlrec As Integer
            If strType = "RP09" Then
                ErrMsg = "Gagal baca data detail."
                'Write judul dulu
                vrow = 2
                vcol = 1
                xlsheet.Cells(vrow, 1) = Mid(Me.Text, 18, Len(Me.Text) - 18)
                vrow = vrow + 1

                'write selection screen dan 'write isi selection screen
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 1, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigndate(date1, date2)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 2, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtComp", field1)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 3, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMatGrp", field2)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 4, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMat", field3)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 5, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayType", field4)
                vrow = vrow + 1
                xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 6, False, False, False, False)
                xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayTerm", field5)
                'write header
                vrow = 14
                vcol = 1

                xlsheet.Cells(vrow, vcol) = "BL NO"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter
                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "PO"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Cells(vrow + 1, vcol) = "(per Shipment)"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Notice On Arrival"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Material Group"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                If con1 = True Then
                    vcol = vcol + 1
                    xlsheet.Cells(vrow, vcol) = "Material Name"
                    xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                    xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                    xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter
                End If

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "SPPB Date"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Clearance"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Cells(vrow + 1, vcol) = "Date"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "FTD"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Shipment Lines"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "EMKL"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                vcol = vcol + 1
                vcol_payment = vcol
                jmlrec = DBQueryGetTotalRows(SQlStr_4, MyConn, ErrMsg, True, UserData) ' Header
                xlsheet.Cells(vrow, vcol) = "Actual Cost"
                xlsheet.Range(xlsheet.Cells(vrow, vcol_payment), xlsheet.Cells(vrow, (vcol + jmlrec - 1))).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol_payment), xlsheet.Cells(vrow, (vcol + jmlrec - 1))).HorizontalAlignment = xlHAlignCenter

                vrow = vrow + 1
                MyDataReader = DBQueryDataReader(SQlStr_4, MyConn, ErrMsg, UserData)
                If Not MyDataReader Is Nothing Then
                    While MyDataReader.Read
                        xlsheet.Cells(vrow, vcol) = MyDataReader.GetString(1)
                        vcol = vcol + 1
                    End While
                End If
                CloseDataReader(MyDataReader, UserData)
                vcol_akhir = vcol - 1
                For i = 1 To vcol - 1
                    Call f_kotak(14, i, vrow, i)
                Next i
                For i = vcol_payment To vcol - 1
                    Call f_kotak(15, i, vrow, i)
                Next i
                Dim ActualCostCode As String
                Dim ActualAmount, Z As Integer
                Dim BLNo As String = Nothing
                Dim PONo As String = Nothing
                Dim FlagStart As Boolean = True
                'fill the data
                vrow = vrow + 1
                vrow_datastart = vrow
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        vcol = 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("BLNO")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("PONo")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("NoticeOnArrival")
                            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = "'" + MyReader.GetString("MaterialGroupCode")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        If con1 = True Then
                            vcol = vcol + 1
                            Try
                                xlsheet.Cells(vrow, vcol) = MyReader.GetString("MaterialName")
                            Catch
                                xlsheet.Cells(vrow, vcol) = ""
                            End Try
                        End If
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("SPPBDATE")
                            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("ClearanceDate")
                            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("FTD")
                        Catch
                            xlsheet.Cells(vrow, vcol) = 0
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("ShippingLines")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vcol = vcol + 1
                        Try
                            xlsheet.Cells(vrow, vcol) = MyReader.GetString("EMKLName")
                        Catch
                            xlsheet.Cells(vrow, vcol) = ""
                        End Try
                        vrow = vrow + 1
                        Try
                            ActualCostCode = MyReader.GetString("ActualCostCode")
                        Catch
                            ActualCostCode = ""
                        End Try
                        Try
                            ActualAmount = CInt(MyReader.GetString("ActualAmount"))
                        Catch
                            ActualAmount = 0
                        End Try
                        If BLNo = MyReader.GetString("BLNO") And PONo = MyReader.GetString("PONo") Then
                            vcol = vcol_payment
                            MyDataReader2 = DBQueryDataReader(SQlStr_4, MyConn1, ErrMsg, UserData)
                            If Not MyDataReader2 Is Nothing Then
                                While MyDataReader2.Read
                                    If MyDataReader2.GetString(0) = ActualCostCode Then
                                        xlsheet.Cells(Z, vcol) = ActualAmount
                                    End If
                                    vcol = vcol + 1
                                End While
                            End If
                            CloseDataReader(MyDataReader2, UserData)
                        Else
                            If BLNo = "" And PONo = "" Then
                                Z = vrow_datastart
                            Else
                                Z = vrow - 1
                            End If
                            vcol = vcol_payment
                            MyDataReader2 = DBQueryDataReader(SQlStr_4, MyConn1, ErrMsg, UserData)
                            If Not MyDataReader2 Is Nothing Then
                                While MyDataReader2.Read
                                    If MyDataReader2.GetString(0) = ActualCostCode Then
                                        xlsheet.Cells(Z, vcol) = ActualAmount
                                    End If
                                    vcol = vcol + 1
                                End While
                            End If
                            CloseDataReader(MyDataReader2, UserData)
                        End If
                        BLNo = MyReader.GetString("BLNO")
                        PONo = MyReader.GetString("PONo")
                    End While
                End If
                CloseMyReader(MyReader, UserData)


                For i = 1 To vcol - 1
                    Call f_kotak(vrow_datastart, i, vrow - 1, i)
                Next i

                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Name = "Tahoma"
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Size = 8
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_payment - 1)).EntireColumn.HorizontalAlignment = xlLeft
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.AutoFit()
                xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, 1)).Cells.Font.Size = 16

                'FOR ALL EXCEL
                xlsheet.Protect(DrawingObjects:=True, Contents:=True, Scenarios:=True, Password:="poimport")

                app.Windows(1).DisplayGridlines = False
                app.Windows(1).DisplayHeadings = False

                'Finally save the file
                file_name = "c:\" & strType & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
                xlsheet.SaveAs(file_name)
                TextBox1.Text = file_name
                xlsheet = Nothing
                app.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'Finally
            '    If (app IsNot Nothing) Then
            '        'app.Quit()
            '        app = Nothing
            '        Dim myProcesses() As Process
            '        Dim myProcess As Process

            '        myProcesses = Process.GetProcessesByName("EXCEL")
            '        For Each myProcess In myProcesses
            '            myProcess.Kill()
            '        Next
            '    End If
        End Try
    End Sub


    Private Sub CreateExcelAppAndNewWorkbookRP10(ByVal strType As String, ByVal SQlStr As String, ByVal SQlStr_4 As String)
        Dim xlsheet As New xlns.Worksheet
        Dim inApp As xlns.Application
        Dim xlwindow As xlns.Workbook
        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)

        Try
            app.Visible = True
            Dim vcol_payment, crow_estimate, jmlrec As Integer
            ErrMsg = "Gagal baca data detail."
            'Write judul dulu
            vrow = 2
            vcol = 1
            xlsheet.Cells(vrow, 1) = Mid(Me.Text, 18, Len(Me.Text) - 18)
            vrow = vrow + 1

            'write selection screen dan 'write isi selection screen
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 1, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigndate(date1, date2)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 2, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtComp", field1)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 3, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMatGrp", field2)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 4, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtMat", field3)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 5, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayType", field4)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 6, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayTerm", field5)
            vrow = vrow + 1
            xlsheet.Cells(vrow, 1) = Rpt_CRViewer.f_assignlabel(v_type, 7, False, False, False, False)
            xlsheet.Cells(vrow, 2) = ":  " & Rpt_CRViewer.f_assigntext("txtPayTerm", field5)


            'write header
            vrow = 14
            vcol = 1

            xlsheet.Cells(vrow, vcol) = "BL NO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "PO"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            xlsheet.Cells(vrow + 1, vcol) = "(per Shipment)"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter

            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "Notice On Arrival"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "Material Group"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter
            vcol = vcol + 1
            xlsheet.Cells(vrow, vcol) = "Clearance"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
            xlsheet.Cells(vrow + 1, vcol) = "Date"
            xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter


            If con1 = True Then
                vcol = vcol + 1
                vcol_payment = vcol
                crow_estimate = vrow
                vcol_Actual = vcol
                jmlrec = DBQueryGetTotalRows(SQlStr_4, MyConn, ErrMsg, True, UserData) ' Header
                xlsheet.Cells(crow_estimate, vcol) = "Actual Cost"
                xlsheet.Range(xlsheet.Cells(crow_estimate, vcol_payment), xlsheet.Cells(crow_estimate, vcol + jmlrec - 1)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(crow_estimate, vcol_payment), xlsheet.Cells(crow_estimate, vcol + jmlrec - 1)).HorizontalAlignment = xlHAlignCenter

                vrow = crow_estimate + 1
                MyDataReader = DBQueryDataReader(SQlStr_4, MyConn, ErrMsg, UserData)
                If Not MyDataReader Is Nothing Then
                    While MyDataReader.Read
                        xlsheet.Cells(vrow, vcol) = MyDataReader.GetString(1)
                        vcol = vcol + 1
                    End While
                End If
                CloseDataReader(MyDataReader, UserData)


                vcol_payment = vcol
                jmlrec = DBQueryGetTotalRows(SQlStr_4, MyConn, ErrMsg, True, UserData) ' Header
                xlsheet.Cells(crow_estimate, vcol) = "Estimated Cost"
                xlsheet.Range(xlsheet.Cells(crow_estimate, vcol_payment), xlsheet.Cells(crow_estimate, vcol + jmlrec - 1)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(crow_estimate, vcol_payment), xlsheet.Cells(crow_estimate, vcol + jmlrec - 1)).HorizontalAlignment = xlHAlignCenter

                vrow = crow_estimate + 1
                MyDataReader = DBQueryDataReader(SQlStr_4, MyConn, ErrMsg, UserData)
                If Not MyDataReader Is Nothing Then
                    While MyDataReader.Read
                        xlsheet.Cells(vrow, vcol) = MyDataReader.GetString(1)
                        vcol = vcol + 1
                    End While
                End If
                CloseDataReader(MyDataReader, UserData)
            Else
                vcol = vcol + 1
                vcol_payment = vcol
                xlsheet.Cells(vrow, vcol) = "Actual Cost"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter

                vcol = vcol + 1
                xlsheet.Cells(vrow, vcol) = "Estimated Cost"
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).MergeCells = True
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).HorizontalAlignment = xlHAlignCenter
                xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow + 1, vcol)).VerticalAlignment = xlHAlignCenter
                vrow = vrow + 1
                vcol = vcol + 1
            End If
            vcol_akhir = vcol - 1
            If _con1 = True Then
                vcol_payment = vcol_Actual
            End If
            For i = 1 To vcol - 1
                Call f_kotak(14, i, vrow, i)
            Next i
            For i = vcol_payment To vcol - 1
                Call f_kotak(15, i, vrow, i)
            Next i
            Dim ActualCostCode As String
            Dim ActualAmount, Z As Integer

            Dim EstimatedCostCode As String
            Dim EstimatedAmount As Integer

            Dim BLNo As String = Nothing
            Dim PONo As String = Nothing
            Dim FlagStart As Boolean = True
            'fill the data
            vrow = vrow + 1
            vrow_datastart = vrow
            MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    vcol = 1
                    Try
                        xlsheet.Cells(vrow, vcol) = MyReader.GetString("BL_NO")
                    Catch
                        xlsheet.Cells(vrow, vcol) = ""
                    End Try
                    vcol = vcol + 1
                    Try
                        xlsheet.Cells(vrow, vcol) = MyReader.GetString("PONo")
                    Catch
                        xlsheet.Cells(vrow, vcol) = ""
                    End Try
                    vcol = vcol + 1
                    Try
                        xlsheet.Cells(vrow, vcol) = MyReader.GetString("NoticeOnArrival")
                        xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                    Catch
                        xlsheet.Cells(vrow, vcol) = ""
                    End Try
                    vcol = vcol + 1
                    Try
                        xlsheet.Cells(vrow, vcol) = "'" + MyReader.GetString("MaterialGroupCode")
                    Catch
                        xlsheet.Cells(vrow, vcol) = ""
                    End Try
                    vcol = vcol + 1
                    Try
                        xlsheet.Cells(vrow, vcol) = MyReader.GetString("ClearanceDate")
                        xlsheet.Range(xlsheet.Cells(vrow, vcol), xlsheet.Cells(vrow, vcol)).NumberFormat = "mm-dd-yyyy"
                    Catch
                        xlsheet.Cells(vrow, vcol) = ""
                    End Try
                    If con1 = True Then
                        Try
                            ActualCostCode = MyReader.GetString("ActualCostCode")
                        Catch
                            ActualCostCode = ""
                        End Try
                    End If
                    Try
                        ActualAmount = CInt(MyReader.GetString("ActualAmount"))
                    Catch
                        ActualAmount = 0
                    End Try
                    If con1 = True Then
                        Try
                            EstimatedCostCode = MyReader.GetString("EstimatedCostCode")
                        Catch
                            EstimatedCostCode = ""
                        End Try
                    End If
                    Try
                        EstimatedAmount = CInt(MyReader.GetString("EstimatedAmount"))
                    Catch
                        EstimatedAmount = 0
                    End Try
                    If con1 = True Then
                        If BLNo = MyReader.GetString("BL_NO") And PONo = MyReader.GetString("PONo") Then
                            vcol = vcol_Actual
                            MyDataReader2 = DBQueryDataReader(SQlStr_4, MyConn1, ErrMsg, UserData)
                            If Not MyDataReader2 Is Nothing Then
                                While MyDataReader2.Read
                                    If MyDataReader2.GetString(0) = ActualCostCode Then
                                        xlsheet.Cells(Z, vcol) = ActualAmount
                                    End If
                                    vcol = vcol + 1
                                End While
                            End If
                            CloseDataReader(MyDataReader2, UserData)
                            MyDataReader2 = DBQueryDataReader(SQlStr_4, MyConn1, ErrMsg, UserData)
                            If Not MyDataReader2 Is Nothing Then
                                While MyDataReader2.Read
                                    If MyDataReader2.GetString(0) = EstimatedCostCode Then
                                        xlsheet.Cells(Z, vcol) = EstimatedAmount
                                    End If
                                    vcol = vcol + 1
                                End While
                            End If
                            CloseDataReader(MyDataReader2, UserData)
                        Else
                            If BLNo = "" And PONo = "" Then
                                Z = vrow_datastart
                            Else
                                Z = vrow ' - 1
                            End If
                            vcol = vcol_Actual
                            MyDataReader2 = DBQueryDataReader(SQlStr_4, MyConn1, ErrMsg, UserData)
                            If Not MyDataReader2 Is Nothing Then
                                While MyDataReader2.Read
                                    If MyDataReader2.GetString(0) = ActualCostCode Then
                                        xlsheet.Cells(Z, vcol) = ActualAmount
                                    End If
                                    vcol = vcol + 1
                                End While
                            End If
                            CloseDataReader(MyDataReader2, UserData)
                            MyDataReader2 = DBQueryDataReader(SQlStr_4, MyConn1, ErrMsg, UserData)
                            If Not MyDataReader2 Is Nothing Then
                                While MyDataReader2.Read
                                    If MyDataReader2.GetString(0) = EstimatedCostCode Then
                                        xlsheet.Cells(Z, vcol) = EstimatedAmount
                                    End If
                                    vcol = vcol + 1
                                End While
                            End If
                            CloseDataReader(MyDataReader2, UserData)
                        End If
                        BLNo = MyReader.GetString("BL_NO")
                        PONo = MyReader.GetString("PONo")
                    Else
                        If BLNo = MyReader.GetString("BL_NO") And PONo = MyReader.GetString("PONo") Then
                            'vcol = vcol_payment
                            'xlsheet.Cells(Z, vcol) = ActualAmount
                            'xlsheet.Cells(Z, vcol + 1) = EstimatedAmount
                        Else
                            If BLNo = "" And PONo = "" Then
                                Z = vrow_datastart
                            Else
                                Z = vrow
                            End If
                            vcol = vcol_payment
                            xlsheet.Cells(Z, vcol) = ActualAmount
                            xlsheet.Cells(Z, vcol + 1) = EstimatedAmount
                        End If
                        BLNo = MyReader.GetString("BL_NO")
                        PONo = MyReader.GetString("PONo")
                    End If
                    vrow = vrow + 1
                End While
                CloseMyReader(MyReader, UserData)
                If con1 = False Then
                    vcol = 9
                End If

                For i = 1 To vcol - 1
                    Call f_kotak(vrow_datastart, i, vrow - 1, i)
                Next i

                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Name = "Tahoma"
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.Font.Size = 8
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_payment - 1)).EntireColumn.HorizontalAlignment = xlLeft
                xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(vrow, vcol_akhir + 3)).EntireColumn.AutoFit()
                xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, 1)).Cells.Font.Size = 16

                'FOR ALL EXCEL
                xlsheet.Protect(DrawingObjects:=True, Contents:=True, Scenarios:=True, Password:="poimport")

                app.Windows(1).DisplayGridlines = False
                app.Windows(1).DisplayHeadings = False

                'Finally save the file
                file_name = "c:\" & strType & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
                xlsheet.SaveAs(file_name)
                TextBox1.Text = file_name
                xlsheet = Nothing
                app.Visible = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'Finally
            '    If (app IsNot Nothing) Then
            '        'app.Quit()
            '        app = Nothing
            '        Dim myProcesses() As Process
            '        Dim myProcess As Process

            '        myProcesses = Process.GetProcessesByName("EXCEL")
            '        For Each myProcess In myProcesses
            '            myProcess.Kill()
            '        Next
            '    End If

        End Try
    End Sub
End Class