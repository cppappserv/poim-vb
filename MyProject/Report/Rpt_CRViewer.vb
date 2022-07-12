Imports CrystalDecisions.CrystalReports.Engine
'Imports BPMainUI.FrmReportMenu

Public Class Rpt_CRViewer
    Private _v_type As String
    Private _date1, _date2 As Date
    Private _field1, _field2, _field3, _field4, _field5, _field6, _field7 As String
    Private _con1, _con2, _con3, _conb As Boolean
    Dim aReport As New ReportClass
    Dim v_pos1, v_pos2 As Integer

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

    Private Sub Rpt_CRViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQlStr, ErrMsg As String
        Dim v_length As Integer
        Dim v_ShipFR, v_shipTo As Date
        Dim v_CompCd, v_Plant, v_Supp, v_MatGrp, v_Mat As String

        v_CompCd = ""
        v_Plant = ""
        v_Supp = ""
        v_MatGrp = ""
        v_Mat = ""
        '--------------------------------
        'QUERY AWAL
        '--------------------------------
        Select Case CStr(v_type)
            Case "RP01"                
                aReport = New RP01
                v_ShipFR = date1
                v_shipTo = date2
                v_CompCd = field1
                v_Plant = field2
                v_Supp = field3
                v_MatGrp = field4
                v_Mat = field5
                If con1 = True Then
                    '====================================
                    ''fyi kalau dlm query trdpt field iBLOB, field tsb tinggal di CAST as CHAR (contoh field container_size)
                    '====================================
                    SQlStr = _
                    "Select t1.*, t2.xarrdate, t2.xarrstatus, t2.xqty_shipment, t2.xcum_shipment, (t1.quantity-if(t2.xcum_shipment is null,0,t2.xcum_shipment)) xqty_outstd,if(t1.rateunit is null,1,t1.rateunit) rateunit From " & _
                    "(Select t1.shipment_period_fr, t1.shipment_period_to, t1.company_code, m1.company_name, t1.plant_code, m2.plant_name, t1.port_code, m3.port_name, " & _
                    "t1.supplier_code, m4.supplier_name, t1.contract_no, t1.po_no, m7.group_name, t2.material_code, m5.material_name, t2.country_code, m6.country_name, " & _
                    "t1.tolerable_delivery, t2.quantity,  (t2.quantity * t1.tolerable_delivery) xqty_po, t2.po_item, t2.unit_code " & _
                    ",(Select mz.rate from tbm_unit_equivalent mz Where mz.unit_code=t2.unit_code and mz.unit_code_to='" & field6 & "') rateunit " & _
                    "From tbl_po t1, tbl_po_detail t2, " & _
                    "tbm_company m1, tbm_plant m2, tbm_port m3, tbm_supplier m4, tbm_material m5, tbm_country m6, tbm_material_group m7 " & _
                    "Where status <> 'Rejected' and status <> 'Closed' " & _
                    "and t1.po_no=t2.po_no " & _
                    "and t1.company_code=m1.company_code and t1.plant_code=m2.plant_code " & _
                    "and t1.port_code=m3.port_code and t1.supplier_code=m4.supplier_code " & _
                    "and t2.material_code=m5.material_code and t2.country_code=m6.country_code and m5.group_code=m7.group_code " & _
                    "AND (T1.SHIPMENT_PERIOD_FR >= '" & Format(v_ShipFR, "yyyy/MM/dd") & "' AND T1.SHIPMENT_PERIOD_TO <= '" & Format(v_shipTo, "yyyy/MM/dd") & "') " & _
                    "AND (T1.COMPANY_CODE = '" & v_CompCd & "' or ''='" & v_CompCd & "')" & _
                    "AND (T1.PLANT_CODE = '" & v_Plant & "' or ''='" & v_Plant & "') " & _
                    "AND (T1.SUPPLIER_CODE = '" & v_Supp & "' or ''='" & v_Supp & "') " & _
                    "AND (M7.GROUP_CODE = '" & v_MatGrp & "' or ''='" & v_MatGrp & "') " & _
                    "AND (M5.MATERIAL_CODE = '" & v_Mat & "' or ''='" & v_Mat & "') " & _
                    "order by t1.shipment_period_fr, t1.shipment_period_to, t1.po_no, t2.po_item) t1 " & _
                    "Left Join " & _
                    "(Select t1.shipment_no, t1.po_no, t1.po_item, t1.material_code, t1.quantity xqty_shipment, " & _
                    "if(t2.clr_dt<>'',date_format(t2.clr_dt,'%b %d, %Y'),if(t2.notice_arrival_dt<>'',date_format(t2.notice_arrival_dt,'%b %d, %Y'),date_format(t2.est_delivery_dt,'%b %d, %Y'))) xarrdate, " & _
                    "if(t2.clr_dt<>'','Clearance',if(t2.notice_arrival_dt<>'','Actual','Estimate')) xarrstatus, " & _
                    "(select sum(b1.quantity) From tbl_shipping_detail b1 where b1.po_no=t1.po_no and b1.po_item=t1.po_item and b1.shipment_no<t1.shipment_no) xcum_shipment " & _
                    "From tbl_shipping_detail t1, tbl_shipping t2 " & _
                    "Where(t1.shipment_no = t2.shipment_no) " & _
                    "Group by t1.shipment_no,t1.po_no, t1.po_item, t1.material_code) t2 " & _
                    "On t1.po_no=t2.po_no and t1.po_item=t2.po_item and t1.material_code=t2.material_code " & _
                    "and (xqty_shipment < quantity or (xqty_shipment > quantity and xqty_shipment < xqty_po))  "
                Else
                    SQlStr = _
                    "Select t1.*, t2.xqty_shipment, (t1.quantity-if(t2.xqty_shipment is null,0,t2.xqty_shipment)) xqty_outstd,if(t1.rateunit is null,1,t1.rateunit) rateunit From " & _
                    "(Select t1.shipment_period_fr, t1.shipment_period_to, t1.company_code, m1.company_name, t1.plant_code, m2.plant_name, t1.port_code, m3.port_name, " & _
                    "t1.supplier_code, m4.supplier_name, t1.contract_no, t1.po_no, m7.group_name, t2.material_code, m5.material_name, t2.country_code, m6.country_name, " & _
                    "t1.tolerable_delivery, t2.quantity,  (t2.quantity * t1.tolerable_delivery) xqty_po, t2.po_item, t2.unit_code " & _
                    ",(Select mz.rate from tbm_unit_equivalent mz Where mz.unit_code=t2.unit_code and mz.unit_code_to='" & field6 & "') rateunit " & _
                    "From tbl_po t1, tbl_po_detail t2, " & _
                    "tbm_company m1, tbm_plant m2, tbm_port m3, tbm_supplier m4, tbm_material m5, tbm_country m6, tbm_material_group m7 " & _
                    "Where status <> 'Rejected' and status <> 'Closed' " & _
                    "and t1.po_no=t2.po_no " & _
                    "and t1.company_code=m1.company_code and t1.plant_code=m2.plant_code " & _
                    "and t1.port_code=m3.port_code and t1.supplier_code=m4.supplier_code " & _
                    "and t2.material_code=m5.material_code and t2.country_code=m6.country_code and m5.group_code=m7.group_code " & _
                    "AND (T1.SHIPMENT_PERIOD_FR >= '" & Format(v_ShipFR, "yyyy/MM/dd") & "' AND T1.SHIPMENT_PERIOD_TO <= '" & Format(v_shipTo, "yyyy/MM/dd") & "') " & _
                    "AND (T1.COMPANY_CODE = '" & v_CompCd & "' or ''='" & v_CompCd & "')" & _
                    "AND (T1.PLANT_CODE = '" & v_Plant & "' or ''='" & v_Plant & "') " & _
                    "AND (T1.SUPPLIER_CODE = '" & v_Supp & "' or ''='" & v_Supp & "') " & _
                    "AND (M7.GROUP_CODE = '" & v_MatGrp & "' or ''='" & v_MatGrp & "') " & _
                    "AND (M5.MATERIAL_CODE = '" & v_Mat & "' or ''='" & v_Mat & "') " & _
                    "order by t1.shipment_period_fr, t1.shipment_period_to, t1.po_no, t2.po_item) t1 " & _
                    "Left Join " & _
                    "(Select po_no, po_item, material_code, sum(quantity) xqty_shipment " & _
                    "From(tbl_shipping_detail) " & _
                    "Group by po_no, po_item, material_code) t2 " & _
                    "On t1.po_no=t2.po_no and t1.po_item=t2.po_item and t1.material_code=t2.material_code " & _
                    "and (xqty_shipment < quantity or (xqty_shipment > quantity and xqty_shipment < xqty_po)) "
                End If
            Case "RP02"
                If (con1 = True And con2 = True) Or (con1 = False And con2 = False) Then
                    aReport = New RP02
                    SQlStr = "Select ValueDate, Years, Mon, MaterialGroup, MaterialName, CountryOfOrigin, PaymentType, PaymentTerm, sum(Amount) SumofAmount, Currency, count(PONo) SumofOverDue From " & _
                        "( " & _
                        "Select t1.tt_dt,t5.company_code, t1.supplier_code, m1.supplier_name Supplier, t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName, " & _
                        "t4.country_code, m4.country_name CountryOfOrigin, date_format(t1.tt_dt,'%m-%d-%Y') as ValueDate, date_format(t1.tt_dt,'%Y') as Years, date_format(t1.tt_dt,'%b') as Mon, " & _
                        "m5.class_code, m6.class_name PaymentType, t5.payment_code, m5.payment_name PaymentTerm, t3.invoice_amount Amount, t1.currency_code Currency " & _
                        "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, " & _
                        "tbl_po t5, tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, " & _
                        "tbm_payment_term m5, tbm_payment_class m6 " & _
                        "Where t1.shipment_no=t2.shipment_no " & _
                        "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                        "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                        "and t2.po_no=t5.po_no " & _
                        "and t1.supplier_code=m1.supplier_code " & _
                        "and t2.material_code=m2.material_code " & _
                        "and m2.group_code=m3.group_code " & _
                        "and t4.country_code=m4.country_code " & _
                        "and t5.payment_code=m5.payment_code " & _
                        "and m5.class_code=m6.class_code " & _
                        "and (t1.tt_dt is not null or t1.tt_dt <> '') " & _
                        "AND (T5.COMPANY_CODE = '" & field1 & "' or ''='" & field1 & "')" & _
                        "AND (M3.group_code = '" & field2 & "' or ''='" & field2 & "')" & _
                        "AND (M2.material_code = '" & field3 & "' or ''='" & field3 & "')" & _
                        "AND (M5.CLASS_CODE = '" & field4 & "' or ''='" & field4 & "')" & _
                        "AND (M5.PAYMENT_CODE = '" & field5 & "' or ''='" & field5 & "')" & _
                        "AND (T1.CURRENCY_CODE = '" & field6 & "' or ''='" & field6 & "')" & _
                        "AND (T1.tt_dt >= '" & Format(date1, "yyyy/MM/dd") & "' AND T1.tt_dt <= '" & Format(date2, "yyyy/MM/dd") & "') " & _
                        ") t1 " & _
                        "group by ValueDate, MaterialGroup, MaterialName, CountryOfOrigin, PaymentType, PaymentTerm, Currency "                    
                ElseIf con1 = False And con2 = True Then
                    'by payment term
                    aReport = New RP02a
                    SQlStr = "Select ValueDate,Years, Mon,  MaterialGroup, PaymentType, PaymentTerm, sum(Amount) SumofAmount, Currency, count(PONo) SumofOverDue From " & _
                        "( " & _
                        "Select t1.tt_dt,t5.company_code, t1.supplier_code, m1.supplier_name Supplier, t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName, " & _
                        "t4.country_code, m4.country_name CountryOfOrigin, date_format(t1.tt_dt,'%m-%d-%Y') as ValueDate, date_format(t1.tt_dt,'%Y') as Years, date_format(t1.tt_dt,'%b') as Mon," & _
                        "m5.class_code, m6.class_name PaymentType, t5.payment_code, m5.payment_name PaymentTerm, t3.invoice_amount Amount, t1.currency_code Currency " & _
                        "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, " & _
                        "tbl_po t5, tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, " & _
                        "tbm_payment_term m5, tbm_payment_class m6 " & _
                        "Where t1.shipment_no=t2.shipment_no " & _
                        "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                        "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                        "and t2.po_no=t5.po_no " & _
                        "and t1.supplier_code=m1.supplier_code " & _
                        "and t2.material_code=m2.material_code " & _
                        "and m2.group_code=m3.group_code " & _
                        "and t4.country_code=m4.country_code " & _
                        "and t5.payment_code=m5.payment_code " & _
                        "and m5.class_code=m6.class_code " & _
                        "and (t1.tt_dt is not null or t1.tt_dt <> '') " & _
                        "AND (T5.COMPANY_CODE = '" & field1 & "' or ''='" & field1 & "')" & _
                        "AND (M3.group_code = '" & field2 & "' or ''='" & field2 & "')" & _
                        "AND (M2.material_code = '" & field3 & "' or ''='" & field3 & "')" & _
                        "AND (M5.CLASS_CODE = '" & field4 & "' or ''='" & field4 & "')" & _
                        "AND (M5.PAYMENT_CODE = '" & field5 & "' or ''='" & field5 & "')" & _
                        "AND (T1.CURRENCY_CODE = '" & field6 & "' or ''='" & field6 & "')" & _
                        "AND (T1.tt_dt >= '" & Format(date1, "yyyy/MM/dd") & "' AND T1.tt_dt <= '" & Format(date2, "yyyy/MM/dd") & "') " & _
                        ") t1 " & _
                        "group by ValueDate, MaterialGroup, PaymentType, PaymentTerm, Currency "

                ElseIf con1 = True And con2 = False Then
                    'by material name
                    aReport = New RP02b
                    SQlStr = "Select ValueDate, Years, Mon, MaterialGroup, MaterialName, CountryOfOrigin, PaymentType, sum(Amount) SumofAmount, Currency, count(PONo) SumofOverDue From " & _
                        "( " & _
                        "Select t1.tt_dt,t5.company_code, t1.supplier_code, m1.supplier_name Supplier, t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName, " & _
                        "t4.country_code, m4.country_name CountryOfOrigin, date_format(t1.tt_dt,'%m-%d-%Y') as ValueDate,date_format(t1.tt_dt,'%Y') as Years, date_format(t1.tt_dt,'%b') as Mon, " & _
                        "m5.class_code, m6.class_name PaymentType, t5.payment_code, m5.payment_name PaymentTerm, t3.invoice_amount Amount, t1.currency_code Currency " & _
                        "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, " & _
                        "tbl_po t5, tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, " & _
                        "tbm_payment_term m5, tbm_payment_class m6 " & _
                        "Where t1.shipment_no=t2.shipment_no " & _
                        "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                        "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                        "and t2.po_no=t5.po_no " & _
                        "and t1.supplier_code=m1.supplier_code " & _
                        "and t2.material_code=m2.material_code " & _
                        "and m2.group_code=m3.group_code " & _
                        "and t4.country_code=m4.country_code " & _
                        "and t5.payment_code=m5.payment_code " & _
                        "and m5.class_code=m6.class_code " & _
                        "and (t1.tt_dt is not null or t1.tt_dt <> '') " & _
                        "AND (T5.COMPANY_CODE = '" & field1 & "' or ''='" & field1 & "')" & _
                        "AND (M3.group_code = '" & field2 & "' or ''='" & field2 & "')" & _
                        "AND (M2.material_code = '" & field3 & "' or ''='" & field3 & "')" & _
                        "AND (M5.CLASS_CODE = '" & field4 & "' or ''='" & field4 & "')" & _
                        "AND (M5.PAYMENT_CODE = '" & field5 & "' or ''='" & field5 & "')" & _
                        "AND (T1.CURRENCY_CODE = '" & field6 & "' or ''='" & field6 & "')" & _
                        "AND (T1.tt_dt >= '" & Format(date1, "yyyy/MM/dd") & "' AND T1.tt_dt <= '" & Format(date2, "yyyy/MM/dd") & "') " & _
                        ") t1 " & _
                        "group by ValueDate, MaterialGroup, MaterialName, CountryOfOrigin, PaymentType, Currency "
                End If

            Case "RP05"
                SQlStr = GetSQLStr_RP05()
                aReport = New RP05
                'RP06 added by Andra
            Case "RP06"
                aReport = New RP06
                'SQlStr = "select t1.*,if(tbm_kurs.effective_kurs is null, 1, tbm_kurs.effective_kurs) rate  from " & _
                '         "(Select t5.company_code, m8.company_name, t1.supplier_code, m1.supplier_name Supplier, " & _
                '         "t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName, " & _
                '         "t4.country_code, m4.country_name CountryOfOrigin, t3.invoice_no InvoiceNo, date_format(t3.invoice_dt,'%m-%d-%Y') InvoiceDate, " & _
                '         "m5.class_code, m7.class_name PaymentType, " & _
                '         "t5.payment_code, m5.payment_name PaymentTerm, t3.invoice_amount Amount, t1.currency_code Currency, " & _
                '         "if(t1.tt_dt is null, '01-01-2000',date_format(t1.tt_dt,'%m-%d-%Y')) tt_dt, date_format(t1.tt_dt,'%m-%d-%Y') TTDate, " & _
                '         "t1.PLANT_CODE " & _
                '         "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, tbl_po t5, " & _
                '         "tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, tbm_payment_term m5, " & _
                '         "tbm_payment_class m7, tbm_company m8 " & _
                '         "Where(t1.shipment_no = t2.shipment_no) " & _
                '         "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                '         "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                '         "and t2.po_no=t5.po_no " & _
                '         "and t1.supplier_code=m1.supplier_code " & _
                '         "and t2.material_code=m2.material_code " & _
                '         "and m2.group_code=m3.group_code " & _
                '         "and t4.country_code=m4.country_code " & _
                '         "and t5.payment_code=m5.payment_code " & _
                '         "and m7.class_code = '" & field6 & "' or '' = '" & field6 & "' " & _
                '         "and m5.payment_name = '" & field7 & "' or '' = '" & field7 & "' " & _
                '         "and m5.class_code=m7.class_code) t1 " & _
                '         "left join tbm_kurs on tbm_kurs.currency_code=currency and tbm_kurs.effective_date=tt_dt and " & _
                '        "(t1.company_code = '" & field1 & "' or '' = '" & field1 & "') and " & _
                '        "(t1.plant_code = '" & field2 & "' or '' = '" & field2 & "') and " & _
                '        "(t1.supplier_code = '" & field3 & "' or ''='" & field3 & "') and " & _
                '        "(t1.group_code = '" & field4 & "' or '' = '" & field4 & "') and " & _
                '        "(t1.material_code = '" & field5 & "' or '' = '" & field5 & "') "
                '
                '        ========NOTE===============
                '        '' = ''   ==> don't use this
                '        ur query will be running FOREVER and u NEVER get the result !!
                '        and ur PC will hang !!
                '        ===========================    
                Dim str1, str2, str3, str4, str5, strKondisi As String

                str1 = If(field1 = "", "", "company_code = '" & field1 & "'")
                str2 = If(field2 = "", "", "plant_code = '" & field2 & "'")
                If str2 <> "" Then str2 = If(str1 = "", str2, " and " & str2)
                str3 = If(field3 = "", "", "supplier_code = '" & field3 & "'")
                If str3 <> "" Then str3 = If(str2 = "", str3, " and " & str3)
                str4 = If(field4 = "", "", "group_code = '" & field4 & "'")
                If str4 <> "" Then str4 = If(str3 = "", str4, " and " & str4)
                str5 = If(field5 = "", "", "material_code = '" & field5 & "'")
                If str5 <> "" Then str5 = If(str4 = "", str5, " and " & str5)
                strKondisi = str1 & str2 & str3 & str4 & str5
                strKondisi = If(strKondisi = "", "", " where " & strKondisi)

                SQlStr = "select t1.*,if(tbm_kurs.effective_kurs is null, 1, tbm_kurs.effective_kurs) rate  from " & _
                         "(Select t5.company_code, m8.company_name, t1.supplier_code, m1.supplier_name Supplier, " & _
                         "t2.po_no PONo, m2.group_code, m3.group_name MaterialGroup, t2.material_code, m2.material_name MaterialName, " & _
                         "t4.country_code, m4.country_name CountryOfOrigin, t3.invoice_no InvoiceNo, date_format(t3.invoice_dt,'%m-%d-%Y') InvoiceDate, " & _
                         "m5.class_code, m7.class_name PaymentType, " & _
                         "t5.payment_code, m5.payment_name PaymentTerm, t3.invoice_amount Amount, t1.currency_code Currency, " & _
                         "if(t1.tt_dt is null, '01-01-2000',date_format(t1.tt_dt,'%m-%d-%Y')) tt_dt, date_format(t1.tt_dt,'%m-%d-%Y') TTDate, " & _
                         "t1.PLANT_CODE " & _
                         "From tbl_shipping t1, tbl_shipping_detail t2, tbl_shipping_invoice t3, tbl_po_detail t4, tbl_po t5, " & _
                         "tbm_supplier m1, tbm_material m2, tbm_material_group m3, tbm_country m4, tbm_payment_term m5, " & _
                         "tbm_payment_class m7, tbm_company m8 " & _
                         "Where(t1.shipment_no = t2.shipment_no) " & _
                         "and t1.shipment_no=t3.shipment_no and t2.po_no=t3.po_no and t2.po_item=t3.ord_no " & _
                         "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                         "and t2.po_no=t5.po_no " & _
                         "and t1.supplier_code=m1.supplier_code " & _
                         "and t2.material_code=m2.material_code " & _
                         "and m2.group_code=m3.group_code " & _
                         "and t4.country_code=m4.country_code " & _
                         "and t5.payment_code=m5.payment_code " & _
                         If(field6 = "", "", "and m7.class_code = '" & field6 & "'") & _
                         If(field7 = "", "", "and m5.payment_name = '" & field7 & "'") & _
                         "and m5.class_code=m7.class_code) t1 " & _
                         "left join tbm_kurs on tbm_kurs.currency_code=currency and tbm_kurs.effective_date=tt_dt " & _
                         strKondisi
            Case "RP07"
                aReport = New RP07
                SQlStr = "Select t1.shipment_no,t1.group_code, t1.group_name, t1.material_code, t1.material_name, t1.quantity, t1.unit_code, " & _
                        "if(t1.rate is null,1,t1.rate) rate, " & _
                        "t1.company_code, t1.company_name, t1.plant_code , t1.supplier_code, t1.supplier_name, t1.country_code, t1.CountryOfOrigin, " & _
                        "date_format(t1.ETA,'%m-%d-%Y') ETA, date_format(t1.NoticeArrival,'%m-%d-%Y') NoticeArrival, t1.po_no,  " & _
                        "t1.shipping_line, t1.ShippingLine, date_format(t1.DateCopyDocs,'%m-%d-%Y') DateCopyDocs, " & _
                        "date_format(t1.DateOfValue,'%m-%d-%Y') DateOfValue, date_format(t1.DateOfOriginalDocs,'%m-%d-%Y') DateOfOriginalDocs,  " & _
                        "date_format(t2.IssuedDt,'%m-%d-%Y') DateOfDeptan, t2.Deptan_No Deptan_No, date_format(t1.DateOfSPPB,'%m-%d-%Y') DateOfSPPB,  " & _
                        "date_format(t1.DateOfClearance,'%m-%d-%Y') DateOfClearance, cast(Quantity as char) QuantityInContainer, '' cc_status " & _
                        "from " & _
                        "(Select t1.shipment_no, m2.group_code, m3.group_name, t2.po_item, t2.material_code, m2.material_name, t2.quantity, t4.unit_code, " & _
                        "t3.company_code, m4.company_name, t1.supplier_code, m1.supplier_name, t4.country_code, m5.country_name CountryOfOrigin,  " & _
                        "t1.est_arrival_dt ETA, t1.notice_arrival_dt NoticeArrival, t2.po_no, t1.shipping_line, m6.line_name ShippingLine, " & _
                        "t1.received_copydoc_dt DateCopyDocs, t1.DUE_DT DateOfValue, t1.received_doc_dt DateOfOriginalDocs,  " & _
                        "sppb_dt DateOfSPPB, if(clr_dt is null or clr_dt='',t1.est_clr_dt,clr_dt) DateOfClearance, t3.plant_code, " & _
                        "(Select mz.rate from tbm_unit_equivalent mz Where mz.unit_code=t4.unit_code and mz.unit_code_to='" & field6 & "') rate " & _
                        "from tbl_shipping t1, tbm_supplier m1, tbl_shipping_detail t2, tbm_material m2,  " & _
                        "tbm_material_group m3, tbl_po t3,  " & _
                        "tbm_company m4, tbl_po_detail t4, tbm_country m5, tbm_lines m6 " & _
                        "where t1.supplier_code=m1.supplier_code " & _
                        "and t1.shipment_no=t2.shipment_no " & _
                        "and t2.material_code=m2.material_code and m2.group_code=m3.group_code " & _
                        "and t2.po_no=t3.po_no " & _
                        "and t3.company_code=m4.company_code " & _
                        "and t2.po_no=t4.po_no and t2.po_item=t4.po_item and t2.material_code=t4.material_code " & _
                        "and t4.country_code=m5.country_code " & _
                        "and t1.shipping_line=m6.line_code " & _
                        "and (t1.est_arrival_dt >= '" & Format(date1, "yyyy/MM/dd") & "' and t1.est_arrival_dt <= '" & Format(date2, "yyyy/MM/dd") & "') " & _
                        ") t1 " & _
                        "left join tbl_ril t2 " & _
                        "on t1.po_no=t2.po_no and t2.status <> 'Rejected' and " & _
                        "(t1.company_code = '" & field1 & "' or '' = '" & field1 & "') and " & _
                        "(t1.plant_code = '" & field2 & "' or '' = '" & field2 & "') and " & _
                        "(t1.supplier_code = '" & field3 & "' or ''='" & field3 & "') and " & _
                        "(t1.group_code = '" & field4 & "' or '' = '" & field4 & "') and " & _
                        "(t1.material_code = '" & field5 & "' or '' = '" & field5 & "') "
                If con1 = True Then
                    SQlStr = SQlStr & "and (t1.dateofclearance is null) "
                End If
                SQlStr = SQlStr & "left join  " & _
                        "(Select shipment_no, GROUP_CONCAT(CONCAT(unit_tot,'x',unit_code) separator ', ') QuantityInContainer       From " & _
                        "(Select sum(1) unit_tot, unit_code, shipment_no " & _
                        "From tbl_shipping_cont " & _
                        "Group by shipment_no, unit_code) t1 " & _
                        "Group by shipment_no) t3 " & _
                        "On t1.shipment_no = t3.shipment_no "

            Case "RP08"
                aReport = New RP08
                SQlStr = "select t1.*, if(isnull(t2.used),'',t2.used) used , (t1.quantity-t2.used) remaining,if(t1.rate is null,1,t1.rate) rate from " & _
                        "(select t1.po_no, m2.group_name, t2.po_item, t2.material_code, m1.material_name, m3.country_name, date_format(t1.openingdt,'%d-%m-%Y') dateofapplicant, " & _
                        "t1.deptan_no, date_format(t1.issueddt,'%d-%m-%Y') as issueddt, t2.quantity " & _
                        ",(Select m1.rate from tbm_unit_equivalent m1 Where m1.unit_code=t3.unit_code and m1.unit_code_to='" & field5 & "') rate " & _
                        "from tbl_ril t1, tbl_ril_detail t2, tbm_material m1, tbm_material_group m2, tbl_po_detail t3, tbm_country m3 " & _
                        ", tbl_po t4 " & _
                        "where(t1.po_no = t2.po_no And t1.ril_no = t2.ril_no) " & _
                        "and t4.po_no = t1.po_no " & _
                        "and t2.material_code=m1.material_code and m1.group_code=m2.group_code " & _
                        "and t1.po_no=t3.po_no and t2.po_item=t3.po_item and t2.material_code=t3.material_code " & _
                        "and t3.country_code=m3.country_code " & _
                        "AND (T1.openingdt >= '" & Format(date1, "yyyy/MM/dd") & "' AND T1.openingdt <= '" & Format(date2, "yyyy/MM/dd") & "') " & _
                        "AND (T4.COMPANY_CODE = '" & field1 & "' or ''='" & field1 & "')" & _
                        "AND (T4.PLANT_CODE = '" & field2 & "' or ''='" & field2 & "') " & _
                        "AND (M2.GROUP_CODE = '" & field3 & "' or ''='" & field3 & "') " & _
                        "AND (M1.MATERIAL_CODE = '" & field4 & "' or ''='" & field4 & "') " & _
                        ") t1 " & _
                        "Left join  " & _
                        "(select po_no, po_item, material_code, sum(quantity) used from tbl_shipping_detail " & _
                        "group by  po_no, po_item, material_code) t2 " & _
                        "on t1.po_no=t2.po_no and t1.po_item=t2.po_item and t1.material_code=t2.material_code "

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


        '===================================================================================
        'utk keperluan selection di subreport 00JudulReport
        'Report selection
        Dim txtSel01 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel01"), TextObject)
        Dim txtSel02 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel02"), TextObject)
        Dim txtSel03 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel03"), TextObject)
        Dim txtSel04 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel04"), TextObject)
        Dim txtSel05 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel05"), TextObject)
        Dim txtSel06 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel06"), TextObject)
        Dim txtSel07 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel07"), TextObject)
        Dim txtSel08 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel08"), TextObject)
        Dim txtSel09 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtSel09"), TextObject)
        Dim lblSel01 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel01"), TextObject)
        Dim lblSel02 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel02"), TextObject)
        Dim lblSel03 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel03"), TextObject)
        Dim lblSel04 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel04"), TextObject)
        Dim lblSel05 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel05"), TextObject)
        Dim lblSel06 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel06"), TextObject)
        Dim lblSel07 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel07"), TextObject)
        Dim lblSel08 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel08"), TextObject)
        Dim lblSel09 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("lblSel09"), TextObject)
        Dim ttk01 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk01"), TextObject)
        Dim ttk02 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk02"), TextObject)
        Dim ttk03 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk03"), TextObject)
        Dim ttk04 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk04"), TextObject)
        Dim ttk05 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk05"), TextObject)
        Dim ttk06 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk06"), TextObject)
        Dim ttk07 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk07"), TextObject)
        Dim ttk08 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk08"), TextObject)
        Dim ttk09 As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("ttk09"), TextObject)
        '===================================================================================

        '--------------------------------
        'ASSIGN TEXTOBJECT, dll
        '--------------------------------
        Select Case CStr(v_type)
            Case "RP01"
                Dim box1 As BoxObject = CType(aReport.ReportDefinition.ReportObjects.Item("Box1"), BoxObject)
                Dim line14 As LineObject = CType(aReport.ReportDefinition.ReportObjects.Item("Line14"), LineObject)
                Dim line13 As LineObject = CType(aReport.ReportDefinition.ReportObjects.Item("Line13"), LineObject)
                Dim LineH1 As LineObject = CType(aReport.ReportDefinition.ReportObjects.Item("LineH1"), LineObject)
                Dim Line11 As LineObject = CType(aReport.ReportDefinition.ReportObjects.Item("Line11"), LineObject)
                Dim txtArrival As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtArrival"), TextObject)
                Dim txtStatus As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtStatus"), TextObject)
                Dim txtQty As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtQty"), TextObject)
                Dim txtOutQty As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtOutQty"), TextObject)
                Dim xqtyshipment As FieldObject = CType(aReport.ReportDefinition.ReportObjects.Item("txqtyshipment1"), FieldObject)
                Dim xqtyoutstd As FieldObject = CType(aReport.ReportDefinition.ReportObjects.Item("txqtyoutstd1"), FieldObject)
                Dim xarrdate As FieldObject = CType(aReport.ReportDefinition.ReportObjects.Item("xarrdate1"), FieldObject)
                Dim xarrstatus As FieldObject = CType(aReport.ReportDefinition.ReportObjects.Item("xarrstatus1"), FieldObject)
                Dim text3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("text3"), TextObject)

                txtSel01.Text = f_assigndate(v_ShipFR, v_shipTo)
                txtSel02.Text = f_assigntext("txtComp", v_CompCd)
                txtSel03.Text = f_assigntext("txtPlant", v_Plant)
                txtSel04.Text = f_assigntext("txtSupp", v_Supp)
                txtSel05.Text = f_assigntext("txtMatGrp", v_MatGrp)
                txtSel06.Text = f_assigntext("txtMat", v_Mat)
                lblSel01.Text = f_assignlabel(v_type, 1, False, False, False, False)
                lblSel02.Text = f_assignlabel(v_type, 2, False, False, False, False)
                lblSel03.Text = f_assignlabel(v_type, 3, False, False, False, False)
                lblSel04.Text = f_assignlabel(v_type, 4, False, False, False, False)
                lblSel05.Text = f_assignlabel(v_type, 5, False, False, False, False)
                lblSel06.Text = f_assignlabel(v_type, 6, False, False, False, False)
                ttk07.ObjectFormat.EnableSuppress = True
                ttk08.ObjectFormat.EnableSuppress = True
                ttk09.ObjectFormat.EnableSuppress = True
                v_length = Len(Me.Text)
                text3.Text = UCase(Mid(Me.Text, 25, v_length))
                'LineH1.LineStyle = CrystalDecisions.Shared.LineStyle.DoubleLine
                'con1 = true = display arrival date n status
                'lebarin box, pasang posisi arrdate n status = 
                If con1 = True Then
                    lblSel07.Text = f_assignlabel(v_type, 7, False, False, False, False)
                    txtSel07.Text = f_assignlabel(v_type, 7, True, False, False, False)
                    ttk07.ObjectFormat.EnableSuppress = False
                    box1.Right = 16050
                    LineH1.Right = 16050
                    Line11.Right = 16050
                    v_pos1 = txtQty.Left
                    v_pos2 = txtOutQty.Left
                    txtQty.Left = txtArrival.Left
                    txtOutQty.Left = txtStatus.Left
                    txtArrival.Left = v_pos1
                    txtStatus.Left = v_pos2
                    v_pos1 = xqtyshipment.Left
                    v_pos2 = xqtyoutstd.Left
                    xqtyshipment.Left = xarrdate.Left
                    xqtyoutstd.Left = xarrstatus.Left
                    xarrdate.Left = v_pos1
                    xarrstatus.Left = v_pos2
                Else
                    txtArrival.ObjectFormat.EnableSuppress = True
                    txtStatus.ObjectFormat.EnableSuppress = True
                    xarrdate.ObjectFormat.EnableSuppress = True
                    xarrstatus.ObjectFormat.EnableSuppress = True
                    line14.ObjectFormat.EnableSuppress = True
                    line13.ObjectFormat.EnableSuppress = True
                End If
            Case "RP02"
                Dim text3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text3"), TextObject)
                v_length = Len(Me.Text)
                text3.Text = UCase(Mid(Me.Text, 25, v_length))
                txtSel01.Text = f_assigndate(date1, date2)
                txtSel02.Text = f_assigntext("txtComp", field1)
                txtSel03.Text = f_assigntext("txtMatGrp", field2)
                txtSel04.Text = f_assigntext("txtMat", field3)
                txtSel05.Text = f_assigntext("txtPayType", field4)
                txtSel06.Text = f_assigntext("txtPayTerm", field5)
                txtSel07.Text = f_assigntext("txtCurr", field6)
                lblSel01.Text = f_assignlabel(v_type, 1, False, False, False, False)
                lblSel02.Text = f_assignlabel(v_type, 2, False, False, False, False)
                lblSel03.Text = f_assignlabel(v_type, 3, False, False, False, False)
                lblSel04.Text = f_assignlabel(v_type, 4, False, False, False, False)
                lblSel05.Text = f_assignlabel(v_type, 5, False, False, False, False)
                lblSel06.Text = f_assignlabel(v_type, 6, False, False, False, False)
                lblSel07.Text = f_assignlabel(v_type, 7, False, False, False, False)
                lblSel08.Text = f_assignlabel(v_type, 8, con1, False, False, False)
                lblSel09.Text = f_assignlabel(v_type, 9, False, con2, False, False)
                ttk07.ObjectFormat.EnableSuppress = True
                ttk08.ObjectFormat.EnableSuppress = True
                ttk09.ObjectFormat.EnableSuppress = True
            Case "RP05"
                Dim Judul1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Judul1"), TextObject)
                Dim Judul2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Judul2"), TextObject)
                Dim Judul3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Judul3"), TextObject)
                Dim Judul4 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Judul4"), TextObject)
                Judul1.Text = IIf(con1, "Est.Target Arrival Date", "     PO       No.")
                Judul2.Text = IIf(con1, "Notice Arrival Date", "Contract No.")
                Judul3.Text = IIf(con1, "     PO       No.", "Est.Target Arrival Date")
                Judul4.Text = IIf(con1, "Contract No.", "Notice Arrival Date")
                txtSel01.Text = f_assigndate(date1, date2)
                txtSel02.Text = f_assigntext("txtComp", v_CompCd)
                txtSel03.Text = f_assigntext("txtPlant", v_Plant)
                txtSel04.Text = f_assigntext("txtSupp", v_Supp)
                txtSel05.Text = f_assigntext("txtMatGrp", v_MatGrp)
                txtSel06.Text = f_assigntext("txtMat", v_Mat)
                lblSel01.Text = f_assignlabel(v_type, 1, False, False, False, False)
                lblSel02.Text = f_assignlabel(v_type, 2, False, False, False, False)
                lblSel03.Text = f_assignlabel(v_type, 3, False, False, False, False)
                lblSel04.Text = f_assignlabel(v_type, 4, False, False, False, False)
                lblSel05.Text = f_assignlabel(v_type, 5, False, False, False, False)
                lblSel06.Text = f_assignlabel(v_type, 6, False, False, False, False)
                lblSel07.Text = f_assignlabel(v_type, 7, False, False, False, False)
                txtSel07.Text = f_assignlabel(v_type, 7, con1, con2, False, False)

                ttk08.ObjectFormat.EnableSuppress = True
                ttk09.ObjectFormat.EnableSuppress = True
                Dim text3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text3"), TextObject)
                v_length = Len(Me.Text)
                text3.Text = UCase(Mid(Me.Text, 25, v_length))
                'RP06 - Added by Andra
            Case "RP06"
                txtSel01.Text = f_assigndate(date1, date2)
                txtSel02.Text = f_assigntext("txtComp", field1)
                txtSel03.Text = f_assigntext("txtPlant", field2)
                txtSel04.Text = f_assigntext("txtSupp", field3)
                txtSel05.Text = f_assigntext("txtMatGrp", field4)
                txtSel06.Text = f_assigntext("txtMat", field5)
                txtSel07.Text = f_assigntext("txtPayType", field6)
                txtSel08.Text = f_assigntext("txtPayTerm", field7)
                lblSel01.Text = f_assignlabel(v_type, 1, False, False, False, False)
                lblSel02.Text = f_assignlabel(v_type, 2, False, False, False, False)
                lblSel03.Text = f_assignlabel(v_type, 3, False, False, False, False)
                lblSel04.Text = f_assignlabel(v_type, 4, False, False, False, False)
                lblSel05.Text = f_assignlabel(v_type, 5, False, False, False, False)
                lblSel06.Text = f_assignlabel(v_type, 6, False, False, False, False)
                lblSel07.Text = f_assignlabel(v_type, 7, False, False, False, False)
                lblSel08.Text = f_assignlabel(v_type, 8, False, False, False, False)
                'lblSel09.Text = f_assignlabel(v_type, 9, False, False, False, False)
                'txtSel07.Text = f_assignlabel(v_type, 7, con1, con2, False, False)
                'ttk08.ObjectFormat.EnableSuppress = True
                ttk09.ObjectFormat.EnableSuppress = True
                Dim text3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text3"), TextObject)
                v_length = Len(Me.Text)
                text3.Text = UCase(Mid(Me.Text, 25, v_length))
            Case "RP07"
                Dim text3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("text3"), TextObject)

                txtSel01.Text = f_assigndate(date1, date2)
                txtSel02.Text = f_assigntext("txtComp", field1)
                txtSel03.Text = f_assigntext("txtPlant", field2)
                txtSel04.Text = f_assigntext("txtSupp", field3)
                txtSel05.Text = f_assigntext("txtMatGrp", field4)
                txtSel06.Text = f_assigntext("txtMat", field5)
                txtSel07.Text = f_assigntext("txtUnitCon", field6)
                lblSel01.Text = f_assignlabel(v_type, 1, False, False, False, False)
                lblSel02.Text = f_assignlabel(v_type, 2, False, False, False, False)
                lblSel03.Text = f_assignlabel(v_type, 3, False, False, False, False)
                lblSel04.Text = f_assignlabel(v_type, 4, False, False, False, False)
                lblSel05.Text = f_assignlabel(v_type, 5, False, False, False, False)
                lblSel06.Text = f_assignlabel(v_type, 6, False, False, False, False)
                lblSel07.Text = f_assignlabel(v_type, 7, False, False, False, False)

                lblSel08.Text = f_assignlabel(v_type, 8, con1, False, False, False)                

                ttk06.ObjectFormat.EnableSuppress = True
                ttk07.ObjectFormat.EnableSuppress = True
                ttk08.ObjectFormat.EnableSuppress = True
                ttk09.ObjectFormat.EnableSuppress = True
                v_length = Len(Me.Text)
                text3.Text = UCase(Mid(Me.Text, 25, v_length))
            Case "RP08"
                Dim text3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("text3"), TextObject)

                txtSel01.Text = f_assigndate(date1, date2)
                txtSel02.Text = f_assigntext("txtComp", field1)
                txtSel03.Text = f_assigntext("txtPlant", field2)
                txtSel04.Text = f_assigntext("txtMatGrp", field3)
                txtSel05.Text = f_assigntext("txtMat", field4)
                lblSel01.Text = f_assignlabel(v_type, 1, False, False, False, False)
                lblSel02.Text = f_assignlabel(v_type, 2, False, False, False, False)
                lblSel03.Text = f_assignlabel(v_type, 3, False, False, False, False)
                lblSel04.Text = f_assignlabel(v_type, 4, False, False, False, False)
                lblSel05.Text = f_assignlabel(v_type, 5, False, False, False, False)
                ttk06.ObjectFormat.EnableSuppress = True
                ttk07.ObjectFormat.EnableSuppress = True
                ttk08.ObjectFormat.EnableSuppress = True
                ttk09.ObjectFormat.EnableSuppress = True
                v_length = Len(Me.Text)
                text3.Text = UCase(Mid(Me.Text, 25, v_length))
            Case Else
                CloseMyConn(MyConn)
                Me.Close()
                Exit Sub
        End Select


        '--------------------------------
        'EXECUTE MAIN REPORT USING DATASOURCE
        'AND ASSIGN JUDUL FROM SUBREPORT (CHECK SUBREPORT ITEM, SHOULD BE ITEM 0)
        '--------------------------------
        Try
            ErrMsg = "Gagal baca data untuk laporan."
            Dim txtJudul As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("txtJudul"), TextObject)
            v_length = Len(Me.Text)
            txtJudul.Text = UCase(Mid(Me.Text, 25, v_length))
            aReport.SetDataSource(DBQueryDataTable(SQlStr, MyConn, "", ErrMsg, UserData))
            CRV_Viewer.ReportSource = aReport

        Catch ex As Exception
            MsgBox("Laporan gagal ditampilkan." & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try

        Me.Show()
        'CloseMyConn(MyConn)
        CRV_Viewer.Refresh()


    End Sub

    Public Function f_assigndate(ByVal date1 As Date, ByVal date2 As Date) As String
        f_assigndate = Format(date1, "dd/MM/yyyy") & " - " & Format(date2, "dd/MM/yyyy")
    End Function
    Public Function f_assigntext(ByVal idkey As String, ByVal key As String) As String
        Dim field_get, table, field_cd As String
        Select Case idkey
            Case "txtComp"
                field_get = "company_name"
                table = "tbm_company"
                field_cd = "company_code"
            Case "txtPlant"
                field_get = "plant_name"
                table = "tbm_plant"
                field_cd = "plant_code"
            Case "txtSupp"
                field_get = "supplier_name"
                table = "tbm_supplier"
                field_cd = "supplier_code"
            Case "txtMatGrp"
                field_get = "group_name"
                table = "tbm_material_group"
                field_cd = "group_code"
            Case "txtMat"
                field_get = "material_name"
                table = "tbm_material"
                field_cd = "material_code"
            Case "txtPayType"
                field_get = "class_name"
                table = "tbm_payment_class"
                field_cd = "class_code"
            Case "txtPayTerm"
                field_get = "payment_name"
                table = "tbm_payment_term"
                field_cd = "payment_code"
            Case "txtCurr"
                field_get = "CURRENCY_NAME"
                table = "tbm_currency"
                field_cd = "CURRENCY_CODE"
            Case "txtUnitCon"
                field_get = "UNIT_CODE_TO"
                table = "tbm_unit_equivalent"
                field_cd = "UNIT_CODE_TO"
        End Select
        f_assigntext = IIf(key = "", "All", key & " - " & AmbilData(field_get, table, field_cd & "='" & key & "'"))
    End Function

    Public Function f_assignlabel(ByVal v_rpt As String, ByVal index As Integer, ByVal a As Boolean, ByVal b As Boolean, ByVal c As Boolean, ByVal vconb As Boolean) As String
        Select Case index
            Case 1
                Select Case v_rpt
                    Case "RP01" : f_assignlabel = "Shipment Periode"
                    Case "RP02", "RP03", "RP04" : f_assignlabel = "Due Date"
                    Case "RP05" : f_assignlabel = "Estimate Target Arrival"
                    Case "RP06" : f_assignlabel = "T/T Date"
                    Case "RP07" : f_assignlabel = "Estimate Target Arrival"
                    Case "RP08" : f_assignlabel = "Date of Applicant"
                    Case "RP09", "RP10" : f_assignlabel = "Notice On Arrival"
                End Select
            Case 2
                f_assignlabel = "Company"
            Case 3
                If v_rpt = "RP01" Or v_rpt = "RP05" Or v_rpt = "RP06" Or v_rpt = "RP07" Or v_rpt = "RP08" Then
                    f_assignlabel = "Destination Plant"
                ElseIf v_rpt = "RP02" Or v_rpt = "RP03" Or v_rpt = "RP04" Then
                    f_assignlabel = "Material Group"
                End If
            Case 4
                If v_rpt = "RP01" Or v_rpt = "RP05" Or v_rpt = "RP06" Or v_rpt = "RP07" Then
                    f_assignlabel = "Supplier"
                ElseIf v_rpt = "RP08" Or v_rpt = "RP10" Then
                    f_assignlabel = "Material Group"
                ElseIf v_rpt = "RP02" Or v_rpt = "RP03" Or v_rpt = "RP04" Or v_rpt = "RP09" Then
                    f_assignlabel = "Material Group"
                    'ElseIf v_rpt = "RP10" Then
                    '    f_assignlabel = "Supplier"
                End If
            Case 5
                If v_rpt = "RP01" Or v_rpt = "RP05" Or v_rpt = "RP06" Or v_rpt = "RP07" Then
                    f_assignlabel = "Material Group"
                ElseIf v_rpt = "RP08" Or v_rpt = "RP10" Then
                    f_assignlabel = "Material"
                ElseIf v_rpt = "RP02" Or v_rpt = "RP03" Or v_rpt = "RP04" Then
                    f_assignlabel = "Payment Type"
                ElseIf v_rpt = "RP09" Then
                    f_assignlabel = "Shipping Lines"
                End If
            Case 6
                If v_rpt = "RP01" Or v_rpt = "RP05" Or v_rpt = "RP06" Or v_rpt = "RP07" Then
                    f_assignlabel = "Material"
                ElseIf v_rpt = "RP08" Then
                    f_assignlabel = ""
                ElseIf v_rpt = "RP02" Or v_rpt = "RP03" Or v_rpt = "RP04" Then
                    f_assignlabel = "Payment Term"
                ElseIf v_rpt = "RP09" Then
                    f_assignlabel = "EMKL"
                ElseIf v_rpt = "RP10" Then
                    f_assignlabel = "Shipping Lines"
                End If
            Case 7
                If v_rpt = "RP01" Then
                    f_assignlabel = "Display Ship Arrival"
                    If a = True Then
                        f_assignlabel = "Yes"
                    End If
                ElseIf v_rpt = "RP05" Then
                    f_assignlabel = "Display By"
                    If a = True Then
                        f_assignlabel = "ETA"
                    ElseIf b = True Then
                        f_assignlabel = "PO"
                    End If
                ElseIf v_rpt = "RP06" Then
                    f_assignlabel = "Payment Type"
                ElseIf v_rpt = "RP07" Then
                    f_assignlabel = "Display Qty on"
                ElseIf v_rpt = "RP02" Or v_rpt = "RP03" Or v_rpt = "RP04" Then
                    f_assignlabel = "Currency"
                ElseIf v_rpt = "RP10" Then
                    f_assignlabel = "EMKL"
                Else
                    f_assignlabel = ""
                End If
            Case 8
                If v_rpt = "RP03" Or v_rpt = "RP04" Then
                    f_assignlabel = "Status"
                ElseIf v_rpt = "RP02" And a = True Then
                    f_assignlabel = "Display Material Name"
                ElseIf v_rpt = "RP06" Then
                    f_assignlabel = "Payment Term"
                ElseIf v_rpt = "RP07" And a = True Then
                    f_assignlabel = "Outstanding Clearance Only"
                Else
                    f_assignlabel = ""
                End If
            Case 9
                If v_rpt = "RP02" And b = True Then
                    f_assignlabel = "Display Payment Term"
                ElseIf v_rpt = "RP03" Then
                    f_assignlabel = "Display Material Name"
                Else
                    f_assignlabel = ""
                End If
        End Select
    End Function
    'added by yti
    Private Function GetDateFormat() As String
        Dim fmt, str1, str2, str3, separator As String
        Dim pos1, pos2 As Integer

        separator = Global.System.Globalization.DateTimeFormatInfo.CurrentInfo.DateSeparator
        fmt = Global.System.Globalization.DateTimeFormatInfo.CurrentInfo.ShortDatePattern
        pos1 = InStr(fmt, separator)
        pos2 = InStr(Microsoft.VisualBasic.Mid(fmt, pos1 + 1, Len(fmt) - pos1), separator) + pos1
        str1 = LCase(Microsoft.VisualBasic.Left(fmt, 1))
        str1 = IIf(str1 = "y", "Y", str1)
        str2 = LCase(Microsoft.VisualBasic.Mid(fmt, pos1 + 1, 1))
        str2 = IIf(str2 = "y", "Y", str2)
        str3 = LCase(Microsoft.VisualBasic.Mid(fmt, pos2 + 1, 1))
        str3 = IIf(str3 = "y", "Y", str3)
        GetDateFormat = "\'%" & str1 & separator & "%" & str2 & separator & "%" & str3 & "\'"
    End Function
    'added by yti
    Private Function GetSQLStr_RP05() As String
        Dim fmt, data1, data2, data3, data4, data5, data6, filter As String

        fmt = GetDateFormat()

        data1 = IIf(con1, "date_format(t2.est_arrival_dt," & fmt & ") as data1", "t1.po_no as data1")
        data2 = IIf(con1, "date_format(t2.notice_arrival_dt," & fmt & ") as data2", "t3.contract_no as data2")
        data3 = IIf(con1, "t1.po_no as data3", "date_format(t2.est_arrival_dt," & fmt & ") as data3")
        data4 = IIf(con1, "t3.contract_no as data4", "date_format(t2.notice_arrival_dt," & fmt & ") as data4")
        data5 = "date_format(t2.received_doc_dt," & fmt & ") as received_doc_dt"
        data6 = "date_format(t2.pib_dt," & fmt & ") as pib_dt"

        'untuk call SP, special karakter (misalnya petik satu) harus didahului karakter \
        filter = "and (t2.est_arrival_dt >= \'" & Format(date1, "yyyy/MM/dd") & "\' and t2.est_arrival_dt <= \'" & Format(date2, "yyyy/MM/dd") & "\') "
        If field1 <> "" Then filter = filter & " and m6.company_code = \'" & field1 & "\' "
        If field2 <> "" Then filter = filter & " and t2.plant_code = \'" & field2 & "\' "
        If field3 <> "" Then filter = filter & " and t2.supplier_code = \'" & field3 & "\' "
        If field4 <> "" Then filter = filter & " and m1.group_code = \'" & field4 & "\' "
        If field5 <> "" Then filter = filter & " and t1.material_code = \'" & field5 & "\' "

        GetSQLStr_RP05 = _
        "Select t1.*,m1.effective_kurs rate,if(t1.rateunit is null,1,t1.rateunit) rateunit from " & _
        "(Select " & data1 & "," & data2 & ", t2.vessel, t2.supplier_code, " & _
        "m3.supplier_name, t2.shipping_line, m5.line_name, " & data3 & "," & data4 & ", m6.company_code, m8.company_name, t2.plant_code, " & _
        "m6.plant_name, t2.port_code, m7.port_name, m4.group_name, t1.material_code, m1.material_name, t4.country_code, m9.country_name, " & _
        data5 & "," & data6 & ", t5.invoice_no, t5.invoice_dt, t1.quantity, " & _
        "(t4.price * t1.quantity) final_amount, t2.currency_code, t1.shipment_no, t1.po_item, t1.pack_quantity, t1.pack_code, m2.pack_name,GetContainer(t1.shipment_no) as container " & _
        ",(Select mz.rate from tbm_unit_equivalent mz Where mz.unit_code=t4.unit_code and mz.unit_code_to=\'" & field6 & "\') rateunit " & _
        "From tbl_shipping_detail t1, tbl_shipping t2, tbl_po t3, tbl_po_detail t4, tbl_shipping_invoice t5,tbm_material m1, " & _
        "tbm_packing m2, tbm_supplier m3, tbm_material_group m4, tbm_lines m5,tbm_plant m6, tbm_port m7, tbm_company m8, tbm_country m9 " & _
        "Where t1.shipment_no=t2.shipment_no and t2.status<>\" & Chr(39) & "Rejected\" & Chr(39) & _
        "and t1.po_no=t3.po_no and t1.po_no=t4.po_no and t1.po_item=t4.po_item and t1.material_code=t4.material_code " & _
        "and t1.shipment_no=t5.shipment_no and t1.po_no=t5.po_no " & _
        "and t1.material_code=m1.material_code and t1.pack_code=m2.pack_code " & _
        "and t2.supplier_code=m3.supplier_code and m1.group_code=m4.group_code " & _
        "and t2.shipping_line=m5.line_code and t2.plant_code=m6.plant_code and t2.port_code=m7.port_code " & _
        "and m6.company_code=m8.company_code and t4.country_code=m9.country_code " & _
        filter & ") t1 " & _
        "Left Join tbm_kurs m1 On t1.currency_code=m1.currency_code and t1.invoice_dt=m1.effective_date "

        GetSQLStr_RP05 = "Call Report_RP05('" & GetSQLStr_RP05 & "')"
    End Function

    Private Sub CRV_Viewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRV_Viewer.Load

    End Sub
End Class