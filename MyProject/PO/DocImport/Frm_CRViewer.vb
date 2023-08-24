Imports CrystalDecisions.CrystalReports.Engine
'Imports BPMainUI.FrmReportMenu
Public Class Frm_CRViewer
    Dim ClientDecimalSeparator, ClientGroupSeparator As String
    Dim ServerDecimal, ServerSeparator As String
    Dim rpt As New Crystal_Report100JudulReport
    Dim aReport As New ReportClass
    Dim v_pono, v_shipmentno, v_type_report, ErrMsg As String
    Dim MyReader As MySqlDataReader
    Dim affrow As Integer




    Private Sub Frm_CRViewer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim SQlStr, SQlStrA, SQlStr_0, SQlStr_1, SQlStr_2, SQlStr_3, SQlStr_4, SQlStrr, SQlStrx As String
        Dim SQlStr_5, SQlStr_6, SQlStr_7, SQlStr_8, SQlStr_9, SQlStr_10, SQlStr_11, SQlStr_12, SQlStr_13, SQlStr_14 As String
        Dim v_flag, v_name4, v_name5 As String
        Dim SQLStr_A, SQLStr_B, SQLStr_C, SQLStr_D, SQLStr_E, SQLStr_F, SQLStr_G, SQLStr_H, SQLStr_I, SQLStr_J, SQLStr_K, SQLStr_L, SQLStr_M, SQLStr_N, SQLStr_SPR As String
        Dim v_isi0, v_isi1, v_isi2, v_isi3, v_isi4, v_isi5, v_isi6, v_isi7, v_isi8, v_isi9, v_isi10, v_isi11, v_isi12, v_isi13, v_isi14 As String
        Dim v_txt0, v_txt1, v_txt2, v_txt3, v_txt4, v_txt5, v_txt6, v_txt7, v_txt8, v_txt9, v_txt10, v_txt11, v_txt12, v_txt13, v_txt14 As String
        Dim v_doc As String
        Dim rows, rows2, xCek, rows3 As Integer
        Dim v_temp As String
        Dim v_length As Integer
        Dim v_string As Integer
        Dim v_type As String
        Dim v_num As String
        Dim v_docgrp As String
        Dim V_GETNORIL, V_NORIL As String
        Dim v_vartemp As String
        Dim v_printOn, selKO, line, v_usedreport As String
        Dim v_ttlpersent, v_cekpersent2 As Decimal



        Dim v_filter, v_dt, v_by, v_cp, v_ord, v_crea, v_matcd, v_matnm, v_bank, v_acc, v_name As String
        Dim arrTemp() As String
        Dim amt_PO, amt_efective, CS_No As Decimal


        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","

        v_isi0 = "Y"
        v_isi1 = "Y"
        v_isi2 = "Y"
        v_isi3 = "Y"
        v_isi4 = "Y"
        v_isi5 = "Y"
        v_isi6 = "Y"
        v_isi7 = "Y"
        v_isi8 = "Y"
        v_isi9 = "Y"
        v_isi10 = "Y"
        v_isi11 = "Y"
        v_isi12 = "Y"
        If (Me.Tag.ToString.Substring(0, 2) = "CS") Then
            CS_No = Int(Mid(Me.Tag.ToString, 5, 2))
        End If
        If (Me.Tag.ToString.Substring(0, 3) = "RIL") And (Me.Tag.ToString.Length = 3) Then
            v_type = "RIL"
        ElseIf (Me.Tag.ToString.Substring(0, 3) = "TTD") Then
            xCek = InStr(Me.Tag.ToString, ";")
            v_type = Mid(Me.Tag.ToString, 1, xCek - 1)
            v_filter = Mid(Me.Tag.ToString, xCek + 1, Me.Tag.ToString.Length - xCek)

            xCek = InStr(v_filter, ".")
            v_dt = Mid(v_filter, 1, xCek - 1)
            v_filter = Mid(v_filter, xCek + 1, v_filter.Length - xCek)

            xCek = InStr(v_filter, ".")
            v_by = Mid(v_filter, 1, xCek - 1)
            v_filter = Mid(v_filter, xCek + 1, v_filter.Length - xCek)

            xCek = InStr(v_filter, ".")
            v_cp = Mid(v_filter, 1, xCek - 1)
            v_filter = Mid(v_filter, xCek + 1, v_filter.Length - xCek)

            xCek = InStr(v_filter, ".")
            If xCek = 0 Then
                v_crea = Mid(v_filter, xCek + 1, v_filter.Length - xCek)
            Else
                v_crea = Mid(v_filter, 1, xCek - 1)
                v_filter = Mid(v_filter, xCek + 1, v_filter.Length - xCek)

                xCek = InStr(v_filter, ".")
                v_matcd = Mid(v_filter, 1, xCek - 1)
                v_matnm = Mid(v_filter, xCek + 1, v_filter.Length - xCek)
            End If
        Else

            If (Me.Tag.ToString.Substring(0, 4) = "RILL") Or (Me.Tag.ToString.Substring(0, 4) = "RILB") Or (Me.Tag.ToString.Substring(0, 4) = "RILT") Or (Me.Tag.ToString.Substring(0, 4) = "RILQ") Then
                v_length = Me.Tag.ToString.Length
                v_string = v_length - 44
                v_type = Me.Tag.ToString.Substring(0, 4)
                v_num = Me.Tag.ToString.Substring(4, 40)
                If (Me.Tag.ToString.Substring(0, 4) = "RILL") Or (Me.Tag.ToString.Substring(0, 4) = "RILT") Then
                    v_pono = Me.Tag.ToString.Substring(44, v_string)
                Else
                    v_shipmentno = Me.Tag.ToString.Substring(44, v_string)
                End If
                If Me.Tag.ToString.Substring(0, 4) = "RILQ" Then v_group_code = Me.Tag.ToString.Substring(44, v_string)
                v_num = Trim(v_num)
            Else
                v_length = Me.Tag.ToString.Length
                v_type = Me.Tag.ToString.Substring(0, 4)
                v_num = Me.Tag.ToString.Substring(4, 2)
                v_num = Trim(v_num)
                If (Me.Tag.ToString.Substring(0, 4) = "KOOO") Then
                    v_string = v_length - 7
                    selKO = Me.Tag.ToString.Substring(6, 1)
                    v_pono = Me.Tag.ToString.Substring(7, v_string)
                Else
                    If (Me.Tag.ToString.Substring(0, 4) = "SHIN") Then
                        v_string = v_length - 7
                        v_doc = Me.Tag.ToString.Substring(6, 1)
                        v_pono = Me.Tag.ToString.Substring(7, v_string)
                        xCek = InStr(v_pono, ";")
                        v_shipmentno = Mid(v_pono, 1, xCek - 1)
                        v_pono = Mid(v_pono, xCek + 1, Len(v_pono) - xCek)
                    Else
                        v_string = v_length - 6
                        v_pono = Trim(Me.Tag.ToString.Substring(6, v_string))
                        If InStr(v_pono, ";") > 0 Then
                            v_temp = v_pono
                            v_pono = Mid(v_temp, 1, InStr(v_temp, ";") - 1)
                            v_docgrp = Trim(Mid(v_temp, InStr(v_temp, ";") + 1, Len(v_temp)))
                        End If
                    End If
                End If
            End If
        End If
        Select Case CStr(v_type)
            Case "BOLC", "ICLC"
                'cek jumlah LC
                rows = 0
                SQlStrA = "SELECT COUNT(*) FROM tbl_budget " & _
                          "WHERE po_no = '" & v_pono & "' AND STATUS <> 'Rejected' "
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                If rows = 1 Then
                    'cek jumlah PO
                    rows = 0
                    SQlStrA = "SELECT COUNT(*) FROM tbl_budget " & _
                              "WHERE (po_no = '" & v_pono & "' OR trim(lc_no) = (SELECT trim(lc_no) FROM tbl_budget " & _
                              "                                            WHERE po_no = '" & v_pono & "' AND STATUS <> 'Rejected' AND trim(lc_no) <> '')) " & _
                              "AND STATUS <> 'Rejected' "
                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            rows = MyReader.GetValue(0)
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                    'cek jumlah Material
                    rows2 = 0
                    SQlStrA = "SELECT COUNT(DISTINCT material_code) totmaterial FROM tbl_po_detail WHERE po_no IN " & _
                              "  (SELECT po_no FROM tbl_budget " & _
                              "   WHERE (po_no = '" & v_pono & "' OR trim(lc_no) = (SELECT trim(lc_no) FROM tbl_budget " & _
                              "                  WHERE po_no = '" & v_pono & "' AND STATUS <> 'Rejected' AND trim(lc_no) <> '')) " & _
                              "   AND STATUS <> 'Rejected') "
                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            rows2 = MyReader.GetValue(0)
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                    If rows = 1 Then
                        '1 PO = 1 LC
                        If rows2 = 1 Then
                            'PO berisi 1 item material
                            SQlStrA = "(SELECT t1.po_no, m1.material_name, t1.quantity, t1.unit_code, t2.amount " & _
                                      "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1 " & _
                                      "WHERE t1.po_no=t2.po_no and t1.material_code = m1.material_code AND t2.status<>'Rejected' AND t2.type_code='BOLC') tbpd " & _
                                      "ON tbp.po_no = tbpd.po_no "

                        ElseIf rows2 > 1 Then
                            'PO berisi banyak item material
                            SQlStrA = "(SELECT t1.po_no, m2.group_name material_name, sum(t1.quantity) quantity, max(t1.unit_code) unit_code, " & _
                                      "(SELECT SUM(t2.amount) FROM tbl_budget t2 WHERE t1.po_no=t2.po_no AND t2.status<>'Rejected' AND t2.type_code='BOLC') amount " & _
                                      "FROM tbl_po_detail t1, tbm_material m1, tbm_material_group m2 " & _
                                      "WHERE t1.material_code = m1.material_code And m1.group_code = m2.group_code " & _
                                      "GROUP BY m2.group_name, t1.po_no) tbpd " & _
                                      "ON tbp.po_no = tbpd.po_no "
                        End If
                    ElseIf rows > 1 Then
                        'Banyak PO = 1 LC 
                        If rows2 = 1 Then
                            'LC berisi 1 Material yang sama
                            'SQlStrA = "(SELECT t2.lc_no, m1.material_name, GROUP_CONCAT(DISTINCT  t1.po_no SEPARATOR ', ') po_no, SUM(t1.quantity) quantity, MAX(t1.unit_code) unit_code, SUM(t2.amount) amount " & _
                            '          "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1 " & _
                            '          "WHERE (t1.po_no = t2.po_no And t1.material_code = m1.material_code AND t2.status<>'Rejected' AND t2.type_code='BOLC') " & _
                            '          "GROUP BY t2.lc_no, m1.material_name) tbpd " & _
                            '          "ON tbb.lc_no = tbpd.lc_no "
                            SQlStrA = "(SELECT t2.lc_no, m1.material_name, GROUP_CONCAT(DISTINCT  t1.po_no SEPARATOR ', ') po_no, SUM(t1.quantity) quantity, MAX(t1.unit_code) unit_code, " & _
                                      "(SELECT SUM(amount) FROM tbl_budget WHERE STATUS<>'Rejected' AND type_code = 'BOLC' AND lc_no=t2.lc_no) amount " & _
                                      "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1 " & _
                                      "WHERE (t1.po_no = t2.po_no And t1.material_code = m1.material_code AND t2.status<>'Rejected' AND t2.type_code='BOLC') " & _
                                      "GROUP BY t2.lc_no, m1.material_name) tbpd " & _
                                      "ON tbb.lc_no = tbpd.lc_no "
                        ElseIf rows2 > 1 Then
                            'LC berisi lebih dari 1 Material yang sama
                            'SQlStrA = "(SELECT t2.lc_no, m2.group_name material_name, GROUP_CONCAT(DISTINCT  t1.po_no SEPARATOR ', ') po_no, SUM(t1.quantity) quantity, MAX(t1.unit_code) unit_code, SUM(t2.amount) amount " & _
                            '          "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1, tbm_material_group m2 " & _
                            '          "WHERE (t1.po_no = t2.po_no And t1.material_code = m1.material_code And m1.group_code = m2.group_code AND t2.status<>'Rejected' AND t2.type_code='BOLC') " & _
                            '          "GROUP BY t2.lc_no, m2.group_name) tbpd " & _
                            '          "ON tbb.lc_no = tbpd.lc_no "

                            SQlStrA = "(SELECT t2.lc_no, m2.group_name material_name, GROUP_CONCAT(DISTINCT t1.po_no SEPARATOR ', ') po_no, SUM(t1.quantity) quantity, MAX(t1.unit_code) unit_code, " & _
                                      "(SELECT SUM(amount) FROM tbl_budget WHERE STATUS<>'Rejected' AND type_code = 'BOLC' AND lc_no=t2.lc_no) amount " & _
                                      "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1, tbm_material_group m2 " & _
                                      "WHERE (t1.po_no = t2.po_no AND t1.material_code = m1.material_code AND m1.group_code = m2.group_code AND t2.type_code = 'BOLC'  AND t2.status<>'Rejected') " & _
                                      "GROUP BY t2.lc_no, m2.group_name) tbpd " & _
                                      "ON tbb.lc_no = tbpd.lc_no "

                        End If
                    End If
                ElseIf rows > 1 Then
                    '1 PO banyak LC di mana LC di buat per item material (1 PO berisi banyak item material)
                    'SQlStrA = "(SELECT t2.lc_no, m1.material_name, t2.po_no, t1.quantity, t1.unit_code, t2.amount " & _
                    '          "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1 " & _
                    '          "WHERE(t1.po_no = t2.po_no And t1.material_code = m1.material_code ) " & _
                    '          "AND t2.po_no = '" & v_pono & "' AND t2.ord_no = '" & v_num & "' AND t2.status <> 'Rejected' AND t2.type_code='BOLC' " & _
                    '          "AND t1.po_item=" & _
                    '          " (SELECT COUNT(ord_no)+1 FROM tbl_budget WHERE po_no = '" & v_pono & "' AND ord_no < '" & v_num & "' AND STATUS <> 'Rejected' AND type_code='BOLC') " & _
                    '          ") tbpd ON tbb.lc_no = tbpd.lc_no "
                    SQlStrA = "(SELECT t2.lc_no, m1.material_name, t2.po_no, t1.quantity, t1.unit_code, " & _
                              "(SELECT SUM(amount) FROM tbl_budget WHERE STATUS<>'Rejected' AND type_code = 'BOLC' AND lc_no=t2.lc_no) amount " & _
                              "FROM tbl_po_detail t1, tbl_budget t2, tbm_material m1 " & _
                              "WHERE(t1.po_no = t2.po_no And t1.material_code = m1.material_code ) " & _
                              "AND t2.po_no = '" & v_pono & "' AND t2.ord_no = '" & v_num & "' AND t2.status <> 'Rejected' AND t2.type_code='BOLC' " & _
                              "AND t1.po_item=" & _
                              " (SELECT COUNT(ord_no)+1 FROM tbl_budget WHERE po_no = '" & v_pono & "' AND ord_no < '" & v_num & "' AND STATUS <> 'Rejected' AND type_code='BOLC') " & _
                              ") tbpd ON tbb.lc_no = tbpd.lc_no "
                End If

                SQlStr = _
                "select tbpd.po_no as po_no, tbb.ord_no as ord_no, IF(LOWER(tbb.lc_no) LIKE 'temp%','',tbb.lc_no) lc_no, tbb.account_no as account_no,date_format(tbb.openingdt,'%M %d, %Y') as openingdt, " & _
                "tbb.amount as poamount, tbpd.amount as amount, tbb.margin_deposit as margin_deposit,tbb.postage_charges as postage_charges,tbb.commision as commision," & _
                "tms.supplier_name as supplier_name,tbp.tolerable_delivery as tolerable_delivery, " & _
                "FormatDec(tbpd.quantity) as quantity, tbpd.unit_code as unit_code, tu.unit_name, " & _
                "tmc.company_name as company_name, tmu.name, TMV.NAME AS NAMEfin, tbpd.material_name as material_name,tmb.bank_name as bank_name, tmcr.currency_code as currency_code, " & _
                "tc.city_name as citybank, tbb.remark,tmb.more_less " & _
                "from tbl_budget as tbb inner join tbl_po as tbp on tbb.po_no = tbp.po_no " & _
                "inner join " & SQlStrA & " " & _
                "inner join tbm_company as tmc on tbp.company_code = tmc.company_code " & _
                "inner join tbm_supplier as tms on tbp.supplier_code = tms.supplier_code " & _
                "inner join tbm_currency as tmcr on tbp.currency_code = tmcr.currency_code " & _
                "inner join tbm_bank as tmb on tbb.bank_code = tmb.bank_code and tbp.currency_code = tmb.currency_code " & _
                "inner join tbm_city as tc on tmb.city_code = tc.city_code " & _
                "left join tbm_users as tmu on tbb.approvedby = tmu.user_ct " & _
                "left join tbm_users as tmv on tbb.FINANCEAPPBY = TMv.USER_CT " & _
                "inner join tbm_unit as tu on tbpd.unit_code = tu.unit_code " & _
                "where tbb.status <> 'Rejected' and tbb.po_no = '" & v_pono & "' and tbb.ord_no = '" & v_num & "' and tbb.type_code = '" & v_type & "' "

            Case "SHIN"
                SQlStr = "select distinct tsi.po_no as po_no, tsi.openingdt as openingdt, REPLACE(tsi.remark,'P/O No',CONCAT('P/O No ',tsi.po_no)) AS remark,  "
                If v_doc = "1" Then
                    SQlStr = SQlStr & "'' as doc_address,"
                Else
                    SQlStr = SQlStr & "tsi.doc_address as doc_address,"
                End If

                If v_shipmentno = "" Then
                    SQlStr = SQlStr & "tsi.footer_note as footer_note, if(tsi.note <> '', 'NB :', '') txtNote, tsi.note as note, tsd.doc_name as doc_name, tus.name as name, tpo.SHIPMENT_PERIOD_FR as SHIPMENT_PERIOD_FR, tpo.TOLERABLE_DELIVERY as TOLERABLE_DELIVERY, tpo.CONTRACT_NO as CONTRACT_NO,tpo.CURRENCY_CODE as CURRENCY_CODE, " & _
                    "tpd.QUANTITY as QUANTITY, FormatDec(tpd.QUANTITY) as QTY, tpd.material_code, tpd.country_code, tcr.country_name as country_name, tpd.hs_code as hs_code, tpd.price as price, " & _
                    "tms.supplier_name as supplier_name, tms.forward as forward,tms.fax as fax,tmc.company_shortname as company_shortname, " & _
                    "tmc.COMPANY_NAME as COMPANY_NAME, tmc.ADDRESS as ADDRESS, tmc.city_code, tcy.city_name as city_name, tpo.SHIPMENT_PERIOD_TO," & _
                    "tmi.INSURANCE_DESCRIPTION as INSURANCE_DESCRIPTION, tmp.PAYMENT_NAME as PAYMENT_NAME, concat(tpr.port_name,', ',tpy.city_name) port_name,concat(IF(tmi.insurance_code IS NULL,'',tmi.insurance_code),'    ',if(tpr.PORT_NAME IS NULL,'',concat(tpr.PORT_NAME,', ',tpy.city_name))) as destination, tmu.unit_name as unit_name, " & _
                    "tmm.MATERIAL_NAME as MATERIAL_NAME, tmm.MATERIAL_SHORTNAME as MATERIAL_SHORTNAME, tpc.pack_name as pack_name, (tpd.QUANTITY * tpd.price) as amount, tmc.NPWP as NPWP_SI, tplant.address AS address_plant, tplant.plant_name " & _
                    "from " & _
                    "tbl_si as tsi inner join tbl_po as tpo on tsi.po_no = tpo.po_no " & _
                    "inner join tbl_si_doc as tsd on tsi.po_no = tsd.po_no and tsi.ord_no = tsd.ord_no " & _
                    "inner join tbl_po_detail as tpd on tpo.po_no = tpd.po_no " & _
                    "inner join tbm_supplier as tms on tpo.supplier_code = tms.supplier_code " & _
                    "inner join tbm_company as tmc on tpo.company_code = tmc.company_code " & _
                    "inner join tbm_insurance as tmi on tpo.insurance_code = tmi.insurance_code " & _
                    "inner join tbm_payment_term as tmp on tpo.payment_code = tmp.payment_code " & _
                    "inner join tbm_port as tpr on tpo.port_code = tpr.port_code " & _
                    "inner join tbm_unit as tmu on tpd.unit_code = tmu.unit_code " & _
                    "inner join tbm_material as tmm on tpd.material_code = tmm.material_code " & _
                    "inner join tbm_country as tcr on tpd.country_code = tcr.country_code " & _
                    "inner join tbm_city as tcy on tmc.city_code = tcy.city_code " & _
                    "left join tbm_packing as tpc on tpd.package_code = tpc.pack_code " & _
                    "left join tbm_users as tus on tsi.CREATEDBY = tus.user_ct " & _
                    "left join tbm_city as tpy on tpy.city_code = tpr.city_code " & _
                    "INNER JOIN tbm_plant AS tplant ON tpo.plant_code = tplant.plant_code " & _
                    "where tsi.shipment_no is null and tsi.po_no = '" & v_pono & "' and tsi.ord_no = '" & v_num & "'"
                Else
                    SQlStr = SQlStr & "tsi.footer_note as footer_note, tsi.note as note, tsd.doc_name as doc_name, tus.name as name, tpo.SHIPMENT_PERIOD_FR as SHIPMENT_PERIOD_FR, tpo.TOLERABLE_DELIVERY as TOLERABLE_DELIVERY, tpo.CONTRACT_NO as CONTRACT_NO,tpp.CURRENCY_CODE as CURRENCY_CODE, " & _
                    "tps.QUANTITY as QUANTITY, FormatDec(tps.QUANTITY) as QTY, tps.material_code, tpd.country_code, tcr.country_name as country_name, tpd.hs_code as hs_code, tpd.price as price, " & _
                    "tms.supplier_name as supplier_name, tms.forward as forward,tms.fax as fax,tmc.company_shortname as company_shortname, " & _
                    "tmc.COMPANY_NAME as COMPANY_NAME, tmc.ADDRESS as ADDRESS, tmc.city_code, tcy.city_name as city_name, tpo.SHIPMENT_PERIOD_TO," & _
                    "tmi.INSURANCE_DESCRIPTION as INSURANCE_DESCRIPTION, tmp.PAYMENT_NAME as PAYMENT_NAME, concat(tpr.port_name,', ',tpy.city_name) port_name,concat(IF(tmi.insurance_code IS NULL,'',tmi.insurance_code),'    ',if(tpr.PORT_NAME IS NULL,'',concat(tpr.port_name,', ',tpy.city_name))) as destination, tmu.unit_name as unit_name, " & _
                    "tmm.MATERIAL_NAME as MATERIAL_NAME, tmm.MATERIAL_SHORTNAME as MATERIAL_SHORTNAME, tpc.pack_name as pack_name,  (tpi.invoice_amount-tpi.invoice_penalty) as amount, tmc.NPWP as NPWP_SI, tplant.address AS address_plant, tplant.plant_name  " & _
                    "FROM tbl_si AS tsi " & _
                    "INNER JOIN tbl_si_doc AS tsd ON tsi.po_no = tsd.po_no AND tsi.ord_no = tsd.ord_no " & _
                    "INNER JOIN tbl_shipping AS tpp ON tsi.shipment_no=tpp.shipment_no " & _
                    "INNER JOIN tbl_shipping_detail AS tps ON tsi.shipment_no=tps.shipment_no " & _
                    "INNER JOIN tbl_shipping_invoice AS tpi ON tps.shipment_no=tpi.shipment_no AND tps.po_no=tpi.po_no AND tps.po_item=tpi.ord_no " & _
                    "INNER JOIN tbl_po AS tpo ON tps.po_no = tpo.po_no " & _
                    "INNER JOIN tbl_po_detail AS tpd ON tps.po_no = tpd.po_no AND tps.po_item=tpd.po_item " & _
                    "INNER JOIN tbm_supplier AS tms ON tpp.supplier_code = tms.supplier_code " & _
                    "INNER JOIN tbm_plant AS tml ON tpp.plant_code=tml.plant_code " & _
                    "INNER JOIN tbm_company AS tmc ON tml.company_code = tmc.company_code " & _
                    "INNER JOIN tbm_insurance AS tmi ON tpo.insurance_code = tmi.insurance_code " & _
                    "INNER JOIN tbm_payment_term AS tmp ON tpo.payment_code = tmp.payment_code " & _
                    "INNER JOIN tbm_port AS tpr ON tpp.port_code = tpr.port_code " & _
                    "INNER JOIN tbm_unit AS tmu ON tpd.unit_code = tmu.unit_code " & _
                    "INNER JOIN tbm_material AS tmm ON tps.material_code = tmm.material_code " & _
                    "INNER JOIN tbm_country AS tcr ON tpd.country_code = tcr.country_code " & _
                    "INNER JOIN tbm_city AS tcy ON tmc.city_code = tcy.city_code " & _
                    "LEFT JOIN tbm_packing AS tpc ON tps.pack_code = tpc.pack_code " & _
                    "LEFT JOIN tbm_users AS tus ON tsi.CREATEDBY = tus.user_ct " & _
                    "LEFT JOIN tbm_city as tpy ON tpy.city_code = tpr.city_code " & _
                    "INNER JOIN tbm_plant AS tplant ON tpo.plant_code = tplant.plant_code " & _
                    "where tsi.shipment_no = '" & v_shipmentno & "' and tsi.ord_no = '" & v_num & "'"
                End If

            Case "RILL"
                SQlStr = _
                "select tr.po_no, TR.RIL_NO, TR.DOC_ADDRESS, TR.OPENINGDT,  trd.doc_no,  '' as doc_name, tr.GROUP_CODE, " & _
                "tu.name, tu.title from tbl_ril as tr " & _
                "inner join tbl_ril_doc as trd on tr.po_no = trd.po_no and tr.ril_no = trd.ril_no " & _
                "left join tbm_users as tu on tr.approvedby = tu.user_ct " & _
                "where tr.po_no = '" & v_pono & "' and tr.ril_no = '" & v_num & "'"
            Case "RILQ"
                SQlStr = _
                "SELECT  t1.*,t2.* FROM tbl_ril_quota AS t1 INNER JOIN " & _
                "tbl_ril_quota_detail AS t2 ON t1.ril_no=t2.ril_no " & _
                "WHERE t1.ril_no = '" & v_num & "'"
            Case "RILT"
                SQlStr = "SELECT t1.po_no, t1.ril_no, date_format(t1.openingdt,'%M %d, %Y') openingdt, t2.po_item, m1.company_name, m1.address, m1.npwp, m1.izin_perusahaan, m1.izin_deptan_no, m1.authorize_person, " & _
                         "t3.country_code, m2.material_name, t2.quantity, t3.quantity quantity_po, t3.unit_code, t3.package_code, t3.price, (t2.quantity * t3.price) tot_price, " & _
                         "m2.hs_code, if(m2.register_no is null,'-', m2.register_no) register_no, if(m2.zat_active is null,'-', m2.zat_active) zat_active, " & _
                         "if(m2.kelompok_obat_hewan is null,'-',m2.kelompok_obat_hewan) kelompok_obat_hewan, m3.name, m3.title " & _
                         "FROM tbl_ril t1, tbl_ril_detail t2, tbl_po_detail t3, tbl_po t4, tbm_company m1, tbm_material m2, tbm_users m3 " & _
                         "WHERE(t1.po_no = t2.po_no And t2.po_no = t3.po_no And t2.po_item = t3.po_item And t2.material_code = t3.material_code) " & _
                         "AND t1.po_no=t4.po_no AND t4.company_code=m1.company_code AND t1.approvedby=m3.user_ct " & _
                         "AND t2.material_code=m2.material_code AND t1.po_no='" & v_pono & "' and t1.ril_no = '" & v_num & "' and t1.status <> 'Rejected' "

            Case "RILB"
                SQlStr = _
                "select tr.shipment_no, TR.RIL_NO, TR.DOC_ADDRESS, TR.OPENINGDT,  trd.doc_no,  '' as doc_name, tr.GROUP_CODE, " & _
                "tu.name, tu.title from tbl_ril as tr " & _
                "inner join tbl_ril_doc as trd on tr.po_no = trd.po_no and tr.ril_no = trd.ril_no " & _
                "left join tbm_users as tu on tr.approvedby = tu.user_ct " & _
                "where tr.shipment_no = '" & v_shipmentno & "' and tr.ril_no = '" & v_num & "'"

            Case "BRRR"
                SQlStrA = _
                "SELECT po_no FROM tbl_remitance AS tr " & _
                "JOIN tbl_shipping_detail AS tsd ON tr.shipment_no = tsd.shipment_no " & _
                "where tr.shipment_no = '" & v_pono & "' and tr.ord_no = '" & v_num & "' and tr.type_code = 'BR' and tr.status <> 'Rejected'"

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                Dim s_pono As String
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        s_pono = Trim(MyReader.GetString(0))
                    End While
                End If
                CloseMyReader(MyReader, UserData)


                'cek jumlah BR dengan LC yang sama
                SQlStr = _
                "SELECT tr2.cutmargin,tr.shipment_no, tr.lc_no, tr.bank_code, tmb.bank_name, tr.account_no, tr2.amount, tr.margin_deposit, tr.commision, tr.postage_charges, " & _
                "tsd.po_no, v1.po_ord, tsd.material_code, tr3.invoice_amount, tmm.material_name, FormatDec(tsd.quantity) AS quantity, tpd.unit_code, tu.unit_name, tp.currency_code, tp.company_code, tmc.company_name, tp.tolerable_delivery, " & _
                "DATE_FORMAT(tr.openingdt,'%M %d, %Y') AS openingdt, tr.remark, tp.supplier_code, tms.supplier_name, tmu.name AS finname,tmu2.name AS appname,tms.note " & _
                "FROM tbl_remitance AS tr " & _
                "INNER JOIN (SELECT lc_no, openingdt, SUM(tr.cutmargin) cutmargin, SUM(tr.amount) amount FROM tbl_remitance tr where status <> 'Rejected' GROUP BY lc_no,openingdt) tr2 ON tr2.lc_no=tr.lc_no AND tr2.openingdt=tr.openingdt " & _
                "INNER JOIN (SELECT tsr.openingdt, tsr.lc_no, SUM(tsi.invoice_amount-tsi.invoice_penalty) invoice_amount FROM tbl_shipping_invoice tsi, tbl_remitance tsr WHERE tsi.shipment_no=tsr.shipment_no AND tsr.status <> 'Rejected' GROUP BY tsr.openingdt, tsr.lc_no) tr3 ON tr3.lc_no=tr.lc_no AND tr3.openingdt=tr.openingdt " & _
                "INNER JOIN tbl_shipping_detail AS tsd ON tr.shipment_no = tsd.shipment_no " & _
                "INNER JOIN tbl_shipping AS ts ON tr.shipment_no = ts.shipment_no " & _
                "INNER JOIN tbl_po AS tp ON tsd.po_no = tp.po_no " & _
                "INNER JOIN tbl_po_detail AS tpd ON tsd.po_no = tpd.po_no AND tsd.po_item = tpd.po_item " & _
                "INNER JOIN tbl_budget AS tb ON tb.status <> 'Rejected' and tr.lc_no = tb.lc_no " & _
                "INNER JOIN vw_po_display AS v1 ON v1.shipment_no = tr.shipment_no AND v1.po_no = tp.po_no " & _
                "LEFT JOIN tbm_company AS tmc ON tp.company_code = tmc.company_code " & _
                "LEFT JOIN tbm_material AS tmm ON tsd.material_code = tmm.material_code " & _
                "LEFT JOIN tbm_bank AS tmb ON tr.bank_code = tmb.bank_code AND tp.currency_code = tmb.currency_code  " & _
                "LEFT JOIN tbm_unit AS tu ON tpd.unit_code = tu.unit_code " & _
                "LEFT JOIN tbm_users AS tmu2 ON tr.approvedby = tmu2.user_ct " & _
                "LEFT JOIN tbm_users AS tmu ON tr.FINANCEAPPBY = tmu.USER_CT " & _
                "LEFT JOIN tbm_supplier AS tms ON tp.supplier_code = tms.supplier_code " & _
                "where tr.shipment_no = '" & v_pono & "' and tr.ord_no = '" & v_num & "' and tr.type_code = 'BR' and tr.STATUS <> 'Rejected'"

                SQlStr = _
                "SELECT tr2.cutmargin, tr.shipment_no, tr.lc_no, tr.bank_code, tmb.bank_name, tr.account_no, tr2.amount, tr.margin_deposit, tr.commision, tr.postage_charges, tsd.po_no, v1.po_ord, tsd.material_code, tr3.invoice_amount, tmm.material_name, FormatDec(tsd.quantity) AS quantity, tpd.unit_code, tu.unit_name, tp.currency_code, tp.company_code, tmc.company_name, tp.tolerable_delivery, DATE_FORMAT(tr.openingdt,'%M %d, %Y') AS openingdt, tr.remark, tp.supplier_code, tms.supplier_name, tmu.name AS finname,tmu2.name AS appname,tms.note " & _
                "FROM ( " & _
                "   SELECT *  " & _
                "   FROM(tbl_remitance) " & _
                "   WHERE shipment_no = '" & v_pono & "' AND ord_no = '" & v_num & "' AND type_code = 'BR' AND STATUS <> 'Rejected'  " & _
                ") AS tr  " & _
                "INNER JOIN ( " & _
                "   SELECT lc_no, openingdt, SUM(tr.cutmargin) cutmargin, SUM(tr.amount) amount " & _
                "   FROM tbl_remitance tr " & _
                "   WHERE STATUS <> 'Rejected' AND shipment_no = '" & v_pono & "' AND ord_no = '" & v_num & "' AND type_code = 'BR' " & _
                "   GROUP BY lc_no,openingdt " & _
                ") tr2 ON tr2.lc_no=tr.lc_no AND tr2.openingdt=tr.openingdt " & _
                "INNER JOIN ( " & _
                "   SELECT tsr.openingdt, tsr.lc_no, SUM(tsi.invoice_amount-tsi.invoice_penalty) invoice_amount " & _
                "   FROM tbl_shipping_invoice tsi, tbl_remitance tsr " & _
                "   WHERE tsi.shipment_no=tsr.shipment_no AND tsr.status <> 'Rejected' AND tsr.shipment_no = '" & v_pono & "' AND tsr.ord_no = '2' AND tsr.type_code = 'BR' " & _
                "   GROUP BY tsr.openingdt, tsr.lc_no " & _
                ") tr3 ON tr3.lc_no=tr.lc_no AND tr3.openingdt=tr.openingdt " & _
                "INNER JOIN tbl_shipping_detail AS tsd ON tr.shipment_no = tsd.shipment_no " & _
                "INNER JOIN tbl_shipping AS ts ON tr.shipment_no = ts.shipment_no " & _
                "INNER JOIN tbl_po AS tp ON tsd.po_no = tp.po_no " & _
                "INNER JOIN tbl_po_detail AS tpd ON tsd.po_no = tpd.po_no AND tsd.po_item = tpd.po_item " & _
                "INNER JOIN tbl_budget AS tb ON tb.status <> 'Rejected' AND tr.lc_no = tb.lc_no " & _
                "INNER JOIN ( " & _
                "   SELECT t1.shipment_no AS shipment_no, t1.po_no AS po_no, t1.received_copydoc_dt AS received_copydoc_dt, IF((t1.term_code = _latin1'P'),CONCAT(CONVERT(TRIM(t1.po_no) USING utf8),_utf8'(',CAST(t1.orde AS CHAR CHARSET utf8),_utf8')'),CONVERT(TRIM(t1.po_no) USING utf8)) AS po_ord " & _
                "   FROM ( " & _
                "       SELECT t1.SHIPMENT_NO AS shipment_no, t1.PO_NO AS po_no, t1.RECEIVED_COPYDOC_DT AS received_copydoc_dt, ( " & _
                "           SELECT COUNT(0) + 1 AS ttl " & _
                "           FROM ( " & _
                "               SELECT DISTINCT t1.SHIPMENT_NO AS SHIPMENT_NO, t1.PO_NO AS PO_NO, t2.RECEIVED_COPYDOC_DT AS RECEIVED_COPYDOC_DT, t3.SHIPMENT_TERM_CODE AS TERM_CODE " & _
                "               FROM tbl_shipping_detail t1 JOIN tbl_shipping t2 JOIN tbl_po t3 " & _
                "               WHERE t1.SHIPMENT_NO = t2.SHIPMENT_NO AND t1.PO_NO = t3.PO_NO AND t1.po_no = '" & s_pono & "' " & _
                "           ) sub_1 " & _
                "           WHERE(sub_1.PO_NO = t1.PO_NO) " & _
                "             AND sub_1.SHIPMENT_NO < t1.SHIPMENT_NO " & _
                "       ) AS orde, t1.TERM_CODE AS term_code " & _
                "       FROM ( " & _
                "           SELECT DISTINCT t1.SHIPMENT_NO AS SHIPMENT_NO, t1.PO_NO AS PO_NO, t2.RECEIVED_COPYDOC_DT AS RECEIVED_COPYDOC_DT, t3.SHIPMENT_TERM_CODE  AS TERM_CODE " & _
                "           FROM tbl_shipping_detail t1 JOIN tbl_shipping t2 JOIN tbl_po t3 " & _
                "           WHERE t1.SHIPMENT_NO = t2.SHIPMENT_NO AND t1.PO_NO = t3.PO_NO AND t1.po_no = '" & s_pono & "'" & _
                "      ) t1 " & _
                "   ) t1 " & _
                ") v1 ON v1.shipment_no = tr.shipment_no AND v1.po_no = tp.po_no " & _
                "LEFT JOIN tbm_company AS tmc ON tp.company_code = tmc.company_code " & _
                "LEFT JOIN tbm_material AS tmm ON tsd.material_code = tmm.material_code " & _
                "LEFT JOIN tbm_bank AS tmb ON tr.bank_code = tmb.bank_code AND tp.currency_code = tmb.currency_code " & _
                "LEFT JOIN tbm_unit AS tu ON tpd.unit_code = tu.unit_code " & _
                "LEFT JOIN tbm_users AS tmu2 ON tr.approvedby = tmu2.user_ct " & _
                "LEFT JOIN tbm_users AS tmu ON tr.FINANCEAPPBY = tmu.USER_CT " & _
                "LEFT JOIN tbm_supplier AS tms ON tp.supplier_code = tms.supplier_code "
            Case "DIII"
                SQlStr = _
                "select distinct td.shipment_no, td.ord_no, td.FINDOC_PRINTEDON, tc1.city_name as city1,td.FINDOC_PRINTEDdt, td.FINDOC_APPBY, " & _
                "tr.lc_no, tr.bank_code, tmb.CITY_CODE, tc2.city_name as city2, tmb.bank_name, tr.account_no, tr.amount, ts.CURRENCY_CODE , " & _
                "tsd.po_no, tsd.po_item, tp.supplier_code, tms.supplier_name, tmu.name as appname " & _
                "from tbl_shipping_doc as td inner join tbl_remitance as tr on td.shipment_no = tr.shipment_no and " & _
                "td.ord_no = tr.ord_no " & _
                "inner join tbl_shipping_detail as tsd on tr.shipment_no = tsd.shipment_no " & _
                "inner join tbl_shipping as ts on tr.shipment_no = ts.shipment_no " & _
                "inner join tbl_po as tp on tsd.po_no = tp.po_no " & _
                "inner join tbl_po_detail as tpd on tsd.po_item = tpd.po_item " & _
                "left join tbm_bank as tmb on tr.bank_code = tmb.bank_code and tp.currency_code = tmb.currency_code  " & _
                "left join tbm_users as tmu on td.FINDOC_APPBY = tmu.USER_CT " & _
                "left join tbm_supplier as tms on tp.supplier_code = tms.supplier_code " & _
                "left join tbm_city as tc1 on td.FINDOC_PRINTEDON = tc1.city_code " & _
                "left join tbm_city as tc2 on tmb.CITY_CODE = tc2.city_code " & _
                "where tD.shipment_no = '" & v_pono & "' and tD.ord_no = '" & v_num & "' and td.findoc_type = 'DI'"
            Case "PVVV"
                '" & UserData.UserName & "' as printby
                SQlStr = _
                "SELECT t1.*, IF(tt_amount IS NOT NULL, tt_amount,inv_amount) INVOICE_AMOUNT FROM " & _
                "   (select distinct tsdoc.shipment_no, tsdoc.findoc_printeddt,TSDOC.FINDOC_FINAPPBY, TU.NAME, TSDOC.FINDOC_APPBY, if(trim(TSDOC.FINDOC_NOTE)='','',concat('Note : ',TSDOC.FINDOC_NOTE)) FINDOC_NOTE, TU3.NAME AS NAME3,TU2.NAME AS NAME2, ts.supplier_code, TS.CURRENCY_CODE, " & _
                "    (select  sum(invoice_amount-invoice_penalty)-ts.finalty from tbl_shipping_invoice where shipment_no=" & v_pono & ") AS INV_AMOUNT," & _
                "    tsi.invoice_no, tp.company_code, tc.company_name, tsm.supplier_name, " & _
                "    ts.bank_name, date_format(ts.tt_dt,'%M %d, %Y') as tt_dt, TSM.FOR_CREDIT_BANK, TSM.ACCOUNT_NO, TSM.SWIFT, TSM.FAVOURING_SUPPLIER, TSM.FAVOURING_ACCOUNT, tsm.note " & _
                "    from tbl_shipping_doc as tsdoc " & _
                "    inner join tbl_shipping_detail as tsd on tsdoc.shipment_no = tsd.shipment_no " & _
                "    inner join tbl_shipping as ts on tsdoc.shipment_no = ts.shipment_no " & _
                "    inner join tbl_shipping_invoice as tsi on tsdoc.shipment_no = tsi.shipment_no " & _
                "    inner join tbl_po as tp on tsd.po_no = tp.po_no " & _
                "    inner join tbl_po_detail as tpd on tsd.po_no = tpd.po_no and tsd.po_item = tpd.po_item " & _
                "    left join tbm_company as tc on tp.company_code = tc.company_code " & _
                "    left join tbm_supplier as tsm on ts.supplier_code = tsm.supplier_code " & _
                "    LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_FINAPPBY = TU.USER_CT " & _
                "    LEFT JOIN TBM_USERS AS TU2 ON TSDOC.FINDOC_APPBY = TU2.USER_CT " & _
                "    LEFT JOIN TBM_USERS AS TU3 ON TSDOC.findoc_createdby = TU3.USER_CT " & _
                "    where TSDOC.shipment_no = '" & v_pono & "' and TSDOC.ord_no = '" & v_num & "' and TSDOC.FINDOC_TYPE = 'PV' " & _
                "    group by tsi.shipment_no) t1 " & _
                "LEFT JOIN  " & _
                "(SELECT shipment_no, amount TT_AMOUNT FROM tbl_budgets WHERE type_code='TT' AND STATUS <>'Rejected' AND shipment_no=5022) t2 ON t1.shipment_no=t2.shipment_no " & _
                " "
            Case "VGVV"
                SQlStr = _
                "select distinct tsdoc.shipment_no, date_format(tsdoc.findoc_printeddt,'%M %d, %Y') findoc_printeddt, TSDOC.FINDOC_VALAMT AMOUNT, TSDOC.FINDOC_VALCUR CURRENCY_CODE, " & _
                "TSDOC.FINDOC_TO, CONCAT(TU3.bank_name, ' Cab. ', TU4.city_name) bank_name, TU3.Account_no, " & _
                "TSDOC.FINDOC_FINAPPBY, TU.NAME, TSDOC.FINDOC_APPBY, TU2.NAME AS NAME2, ts.supplier_code, TS.CURRENCY_CODE INVOICE_CURRENCY_CODE, " & _
                "TSDOC.FINDOC_VALAMT AS INVOICE_AMOUNT," & _
                "tsi.invoice_no, tp.company_code, tc.company_name, tsm.supplier_name, " & _
                "date_format(ts.tt_dt,'%M %d, %Y') as tt_dt_bl, date_format(tp.openingdt,'%M %d, %Y') as tt_dt " & _
                "from tbl_shipping_doc as tsdoc " & _
                "inner join tbl_shipping_detail as tsd on tsdoc.shipment_no = tsd.shipment_no " & _
                "inner join tbl_shipping as ts on tsdoc.shipment_no = ts.shipment_no " & _
                "inner join tbl_shipping_invoice as tsi on tsdoc.shipment_no = tsi.shipment_no " & _
                "inner join tbl_po as tp on tsd.po_no = tp.po_no " & _
                "inner join tbl_po_detail as tpd on tsd.po_no = tpd.po_no and tsd.po_item = tpd.po_item " & _
                "left join tbm_company as tc on tp.company_code = tc.company_code " & _
                "left join tbm_supplier as tsm on ts.supplier_code = tsm.supplier_code " & _
                "LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_FINAPPBY = TU.USER_CT " & _
                "LEFT JOIN TBM_USERS AS TU2 ON TSDOC.FINDOC_APPBY = TU2.USER_CT " & _
                "LEFT JOIN tbm_bank AS TU3 ON TU3.bank_code=TSDOC.FINDOC_TO " & _
                "LEFT JOIN tbm_city AS TU4 ON TU4.city_code=TU3.city_code " & _
                "LEFT JOIN (SELECT * FROM tbl_budgets WHERE type_code='BP') tp ON  tsdoc.shipment_no=tp.shipment_no " & _
                "where TSDOC.shipment_no = '" & v_pono & "' and TSDOC.ord_no = '" & v_num & "' and TSDOC.FINDOC_TYPE = 'VG' " & _
                "group by tsi.shipment_no"
            Case "SPPP"
                SQlStr = _
                "select distinct tsp.SSPCP_OFFICE_CODE, tp.djbc_code , " & _
                "tc.company_name, tc.npwp, tc.address, tci.city_name, ts.aju_no, ts.PIB_DT, " & _
                "tsp.BEA_MASUK, tsp.SPM, tsp.DENDA_PABEAN, tsp.PEN_PABEAN_LAIN, tsp.CUKAI_ETIL, " & _
                "tsp.DENDA_CUKAI, tsp.PEN_CUKAI_LAIN, tsp.PNBP, tsp.VAT, tsp.PPNBM, tsp.PPH22, tsp.SSPCP_PERIOD " & _
                "from tbl_sspcp as tsp " & _
                "inner join tbl_shipping as ts on tsp.shipment_no = ts.shipment_no " & _
                "inner join tbl_shipping_detail as tsd on tsp.shipment_no = tsd.shipment_no " & _
                "inner join tbm_port as tp on tsp.SSPCP_OFFICE_CODE  = tp.port_code  " & _
                "inner join tbm_company as tc on tsp.COMPANY_CODE = tc.COMPANY_CODE " & _
                "inner join tbm_city as tci on tc.city_code = tci.city_code  " & _
                "where tsp.shipment_no = '" & v_pono & "' and tsp.ord_no = '" & v_num & "'"
            Case "CLLL"
                'SQlStr = _
                '"select tsd.findoc_printeddt, tc.city_name as cityPrint, tc2.city_name as cityEx, " & _
                '"te.company_code, te.company_name, te.address, tc2.city_name as cityExp, " & _
                '"ts.vessel, tp.port_name, ts.plant_code, tpl.plant_name, tu.name, tu.title " & _
                '"from tbl_shipping_doc as tsd " & _
                '"inner join tbl_shipping as ts on tsd.shipment_no = ts.shipment_no " & _
                '"left join tbm_city as tc on tsd.findoc_printedon = tc.city_code " & _
                '"left join tbm_expedition as te on tsd.findoc_to = te.company_code " & _
                '"left join tbm_city as tc2 on te.city_code = tc2.city_code " & _
                '"left join tbm_port as tp on ts.port_code = tp.port_code " & _
                '"left join tbm_plant as tpl on ts.plant_code = tpl.plant_code " & _
                '"left join tbm_users as tu on tsd.findoc_appby = tu.user_ct " & _
                '"where tsd.shipment_no = '" & v_pono & "' and tsd.ord_no = '" & v_num & "'  and tsd.findoc_type='CL'"

                'change by andi's request 11102010
                SQlStr = _
               "select tsd.findoc_printeddt, tc.city_name as cityPrint, tc2.city_name as cityEx, " & _
               "te.company_code, te.company_name, te.address, tc2.city_name as cityExp, " & _
               "ts.vessel, tp.port_name, ts.plant_code, tpl.plant_name, tu.name, tu.title, " & _
               " (SELECT CONCAT('P/O No. ',CAST(GROUP_CONCAT(DISTINCT(getPOorder(" & v_pono & ",TRIM(po_no))) SEPARATOR ', ') AS CHAR)) AS PO_NO " & _
               "  From tbl_shipping_detail WHERE SHIPMENT_NO = tsd.shipment_no) PO " & _
               "from tbl_shipping_doc as tsd " & _
               "inner join tbl_shipping as ts on tsd.shipment_no = ts.shipment_no " & _
               "left join tbm_city as tc on tsd.findoc_printedon = tc.city_code " & _
               "left join tbm_expedition as te on tsd.findoc_to = te.company_code " & _
               "left join tbm_city as tc2 on te.city_code = tc2.city_code " & _
               "left join tbm_port as tp on ts.port_code = tp.port_code " & _
               "left join tbm_plant as tpl on ts.plant_code = tpl.plant_code " & _
               "left join tbm_users as tu on tsd.findoc_appby = tu.user_ct " & _
               "where tsd.shipment_no = '" & v_pono & "' and tsd.ord_no = '" & v_num & "'  and tsd.findoc_type='CL'"

            Case "TTTT", "BPIB", "CADD"
                'Jika material lebih dari 1 di summary by group
                rows = 0
                SQlStrA = "SELECT COUNT(*) FROM tbl_shipping_detail t1 WHERE t1.shipment_no='" & v_pono & "'"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows = 1 Then
                    SQlStrA = "SELECT t1.shipment_no, m2.material_name, t1.quantity, m1.unit_name FROM tbl_shipping_detail t1, tbl_po_detail t2, tbm_unit m1, tbm_material m2 " & _
                              "WHERE t1.po_no=t2.po_no AND t1.po_item=t2.po_item AND t2.unit_code=m1.unit_code AND t1.material_code = m2.material_code"
                Else
                    SQlStrA = "SELECT t1.shipment_no, m3.group_name material_name, SUM(t1.quantity) quantity, MAX(m1.unit_name) unit_name FROM tbl_shipping_detail t1, tbl_po_detail t2, tbm_unit m1, tbm_material m2, tbm_material_group m3 " & _
                              "WHERE t1.po_no=t2.po_no AND t1.po_item=t2.po_item AND t2.unit_code=m1.unit_code AND t1.material_code = m2.material_code AND m2.group_code=m3.group_code GROUP BY t1.shipment_no, m3.group_name"
                End If

                SQlStr = _
                "select FormatDec(tsd.quantity) as quantity,tsd.unit_name,tbs.shipment_no, tbs.ord_no,tbs.openingdt,tbs.amount, ti.amount_invoice,tbs.APPROVEDBY,tbs.FINANCEAPPBY, " & _
                "tms.supplier_name, tmb.account_no, tbs.printeddt,tc.city_name, " & _
                "tmc.company_name, tmu.name as NAMEapp, TMV.NAME AS NAMEfin, "
                If v_type = "CADD" Then
                    SQlStr = SQlStr & "tmb.bank_name, concat('DEBET ACCOUNT VIA ',tmb.bank_name) as bank_name2"
                Else
                    SQlStr = SQlStr & "tmb.bank_name"
                End If
                SQlStr = SQlStr & ", tmcr.currency_code, " & _
                "tmcr.currency_name, " & _
                "tk.effective_kurs as ef_kurs, tk.kurs_pajak as kurs, tbs.remark, tsd.material_name as material_shortname " & _
                "from tbl_budgets as tbs " & _
                "left join tbl_shipping as ts on tbs.shipment_no = ts.shipment_no " & _
                "inner join (" & SQlStrA & ") tsd ON ts.shipment_no = tsd.shipment_no " & _
                "left join tbm_plant as tp on ts.plant_code = tp.plant_code " & _
                "left join tbm_company as tmc on tp.company_code = tmc.company_code " & _
                "left join tbm_supplier as tms on ts.supplier_code = tms.supplier_code " & _
                "left join tbm_currency as tmcr on ts.currency_code = tmcr.currency_code " & _
                "left join tbm_bank as tmb on tbs.bank_code = tmb.bank_code " & _
                "left join tbm_users as tmu on tbs.approvedby = tmu.user_ct " & _
                "left join tbm_users as tmv on tbs.FINANCEAPPBY = TMv.USER_CT " & _
                "left join tbm_city as tc on tbs.printedon = tc.city_code " & _
                "left join tbm_kurs as tk on tbs.openingdt = tk.effective_date and ts.currency_code = tk.currency_code " & _
                "left join (select shipment_no, sum(invoice_amount-invoice_penalty) amount_invoice from tbl_shipping_invoice group by shipment_no) ti on ts.shipment_no = ti.shipment_no " & _
                "where tbs.shipment_no = '" & v_pono & "' and tbs.ord_no = '" & v_num & "' and tbs.type_code = '" & Mid(v_type, 1, 2) & "'"

                If v_type = "TTTT" Then
                    SQlStr = "SELECT t1.*, IF(ef_kurs IS NULL, " & _
                             "               (SELECT kurs FROM tbm_kurs m2 WHERE t1.currency_code=m2.currency_code AND m2.effective_date <= t1.openingdt AND m2.kurs > 0 ORDER BY effective_date DESC LIMIT 1), ef_kurs) ef_kurs2 " & _
                             "FROM ( " & SQlStr & _
                             ") t1 "
                End If

            Case "VPPP"
                SQlStr = _
                "select tsdoc.shipment_no, TSDOC.FINDOC_createdBY, TU.NAME as nameCreated, TSDOC.FINDOC_APPBY, TU2.NAME AS NAMEApp, " & _
                "tsdoc.findoc_printeddt, tsdoc.findoc_valamt, tsdoc.findoc_valcur from tbl_shipping_doc as tsdoc " & _
                "LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_createdBY = TU.USER_CT " & _
                "LEFT JOIN TBM_USERS AS TU2 ON TSDOC.FINDOC_APPBY = TU2.USER_CT " & _
                "where tsdoc.shipment_no = '" & v_pono & "' and tsdoc.ord_no = '" & v_num & "' and tsdoc.findoc_type = 'VP' "
            Case "BPJU"
                'SQlStr = _
                '"select date_format(FINDOC_PRINTEDDT,'%d %M %Y') as findoc_printeddt, findoc_note," & _
                '"findoc_valamt, (findoc_valamt*(findoc_valprc/100)) as total, (findoc_valamt-(findoc_valamt*(findoc_valprc/100))) as lebihkurang " & _
                '"from tbl_shipping_doc where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='PP'"

                SQlStr = _
                "SELECT  findoc_no as bpjum_no, findoc_printeddt, CONCAT(t1.findoc_note, ' PO No.', t1.po) findoc_note, t1.findoc_valamt, t1.total, (t1.total - t1.findoc_valamt) AS lebihkurang " & _
                "FROM (SELECT t1.findoc_no, t1.findoc_printeddt, t1.findoc_note, t1.findoc_valamt, " & _
                "      (SELECT SUM(st1.cost_amount) " & _
                "       FROM tbl_cost st1 " & _
                "       WHERE st1.type_code='DP' " & _
                "       AND t1.findoc_reff LIKE CONCAT(st1.shipment_no,';',st1.ship_ord_no,'%')" & _
                "       AND t1.findoc_reff LIKE CONCAT(st1.shipment_no,'%')" & _
                "       ) total, 0 as lebihkurang, " & _
                "      (SELECT CAST(GROUP_CONCAT(DISTINCT(getpoorder(" & v_pono & ",TRIM(po_no))) SEPARATOR ', ') AS CHAR) AS po_no " & _
                "       FROM tbl_shipping_Detail WHERE shipment_no = " & v_pono & ") PO " & _
                "     FROM (select FINDOC_NO, date_format(FINDOC_PRINTEDDT,'%d %M %Y') as findoc_printeddt, findoc_note, findoc_valamt, findoc_reff " & _
                "     FROM tbl_shipping_doc where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='PP') t1) t1"
                'SQlStr = "call getResultSet(" & v_pono & "," & v_num & ")"
            Case "BPUM"
                SQlStr = _
                "select FINDOC_NO as bpum_no, date_format(FINDOC_PRINTEDDT,'%d %M %Y') as findoc_printeddt,findoc_valamt as jumlah " & _
                "from tbl_shipping_doc where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='DP'"
            Case "VPLL"
                SQlStr = _
                "select tcc.costcat_name, FORMAT(tc.cost_amount,2) AS COST_AMOUNT, tsdoc.findoc_valcur From " & _
                "tbl_cost tc, tbm_costcategory tcc, tbl_shipping_doc as tsdoc " & _
                "where tc.cost_code = tcc.costcat_code And tc.cost_amount > 0 And " & _
                "tsdoc.shipment_no = tc.shipment_no And tsdoc.findoc_type = tc.type_code And tsdoc.ord_no = tc.ship_ord_no and " & _
                "tsdoc.shipment_no = '" & v_pono & "' and tsdoc.ord_no = '" & v_num & "' and tsdoc.findoc_type = 'VP' "
            Case "CCCC"
                ''SQlStr = _
                ''"select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(T2.PO_No))) separator ', ') as char) AS PONO " & _
                ''"From tbl_shipping_doc as t1, (SELECT DISTINCT Shipment_no, getpoorder(shipment_no, po_no) po_no FROM tbl_shipping_detail) AS T2 " & _
                ''"WHERE(T1.Shipment_No = T2.Shipment_No) and t1.shipment_no=" & v_pono & " AND t1.Ord_No = " & v_num & " " & _
                ''"GROUP BY T1.Shipment_No"
                Dim v_ponox As String = v_pono
                v_pono = v_ponox.Substring(0, Len(v_ponox) - 2)
                v_type_report = v_ponox.Substring(Len(v_ponox) - 2)
                SQlStr = _
                "select CAST(GROUP_CONCAT(DISTINCT(T2.PO_No) SEPARATOR ', ') AS CHAR) AS PONO " & _
                "From tbl_shipping_doc as t1, (SELECT DISTINCT Shipment_no, getpoorder(shipment_no, trim(po_no)) po_no FROM tbl_shipping_detail where shipment_no=" & v_pono & ") AS T2 " & _
                "WHERE(T1.Shipment_No = T2.Shipment_No) and t1.shipment_no=" & v_pono & " AND (t1.Ord_No = " & v_num & "  " & _
                "or t1.ORD_NO='" & CS_No & "')  " & _
                "GROUP BY T1.Shipment_No"

                SQlStr = _
                "select CAST(GROUP_CONCAT(DISTINCT(T2.PO_No) SEPARATOR ', ') AS CHAR) AS PONO " & _
                "From tbl_shipping_doc as t1, (SELECT DISTINCT Shipment_no, getpoorder(shipment_no, trim(po_no)) po_no FROM tbl_shipping_detail where shipment_no=" & v_pono & ") AS T2 " & _
                "WHERE(T1.Shipment_No = T2.Shipment_No) and t1.shipment_no=" & v_pono & " AND t1.Ord_No = " & v_num & "  " & _
                "GROUP BY T1.Shipment_No"

            Case "CSCS"
                amt_PO = 0 : amt_efective = 0
                Dim v_ponox As String = v_pono
                Dim vnom_po, vnom_invoice As String
                v_pono = v_ponox.Substring(0, Len(v_ponox) - 2)
                v_type_report = v_ponox.Substring(Len(v_ponox) - 2)


                SQlStr = _
                "SELECT t1.*, IF(m5.effective_kurs IS NULL, 0, m5.effective_kurs ) effective_kurs, IF(m5.effective_kurs IS NULL, 0, m5.effective_kurs * t1.invoice_amount) effective_amount, " & _
                "CEIL(t1.findoc_valamt) findoc_valamt_round, getpoorder(" & v_pono & ",trim(t1.po_no)) poorder, " & _
                "(t1.invoice_amount / (t1.quantity * (t1.findoc_valprc/100))/m4.rate) cost, concat('( ',m3.name,' )') appby, if(t1.findoc_status = 'Final Approved', 'APPROVED','') appok, (t1.tot_subamount + IF(m5.effective_kurs IS NULL, 0, m5.effective_kurs * t1.invoice_origin)) tot_amount, " & _
                "IF(t1.deficiency > 0, 'Protein Deficiency', IF(t1.finalty > 0, 'Penalty', '')) txtMin1, " & _
                "IF(t1.deficiency > 0, t1.currency_code, IF(t1.finalty > 0, t1.currency_code, '')) currMin1, " & _
                "IF(t1.deficiency > 0, cast(t1.deficiency as char), IF(t1.finalty > 0, cast(format(t1.finalty,2) as char), '')) Min1, " & _
                "IF(t1.deficiency > 0 and t1.finalty > 0, 'Penalty', '') txtMin2, " & _
                "IF(t1.deficiency > 0 and t1.finalty > 0, t1.currency_code, '') currMin2, " & _
                "IF(t1.deficiency > 0 and t1.finalty > 0, cast(format(t1.finalty,2) as char), '') Min2 " & _
                "FROM " & _
                "(SELECT IF(t2.po_no IS NULL,'', t2.po_no) po_no, t2.po_item, m1.material_name, formatdec(t2.quantity) quantity, t4.unit_code, t4.price, " & _
                " t5.invoice_no, date_format(invoice_dt,'%M %d, %Y') invoice_dt, t5.invoice_origin invoice_origin_o, (t5.invoice_amount-t5.invoice_penalty) invoice_amount_o, t3.currency_code, t3.finalty finalty_o, " & _
                "(t3.finalty * ((t5.invoice_amount-t5.invoice_penalty)/(SELECT SUM(invoice_amount-invoice_penalty) FROM tbl_shipping_invoice WHERE shipment_no='" & v_pono & "'))) finalty, " & _
                " t5.invoice_origin, " & _
                "((t5.invoice_amount-t5.invoice_penalty) - (t3.finalty * ((t5.invoice_amount-t5.invoice_penalty)/(SELECT SUM(invoice_amount-invoice_penalty) FROM tbl_shipping_invoice WHERE shipment_no='" & v_pono & "')))) invoice_amount, " & _
                "(t5.invoice_origin - (t5.invoice_amount-t5.invoice_penalty)) deficiency, " & _
                "M2.SUPPLIER_NAME, t3.EST_DELIVERY_DT effective_date, date_format(t3.EST_DELIVERY_DT,'%M %d, %Y') EST_DELIVERY_DT, t3.VESSEL, date_format(t3.TT_DT,'%M %d, %Y') TT_DT, date_format(t3.DUE_DT,'%M %d, %Y') DUE_DT, t1.findoc_valprc, t1.findoc_valamt, t1.findoc_status, " & _
                "date_format(t1.findoc_printeddt,'%M %d, %Y') findoc_printeddt, t1.findoc_printedon, concat('( ',m3.name,' )') createdby,  t1.findoc_appby, " & _
                "(SELECT SUM(cost_amount) FROM tbl_cost WHERE cost_code LIKE '20%' AND shipment_no='" & v_pono & "' AND type_code='CS' AND ship_ord_no='" & v_num & "') tot_subamount " & _
                "FROM TBL_SHIPPING_DOC t1, tbl_shipping_detail t2, tbl_shipping t3, tbl_po_detail t4, tbl_shipping_invoice t5, " & _
                "tbm_material m1, tbm_supplier m2, tbm_users m3 " & _
                "WHERE t1.shipment_no=t2.shipment_no AND t1.findoc_no=t2.po_no AND t1.findoc_reff=t2.po_item  AND " & _
                "t1.ORD_NO='" & CS_No & "' and " & _
                "t1.shipment_no=t3.shipment_no AND " & _
                "t2.po_no=t4.po_no AND t2.po_item = t4.po_item AND " & _
                "t2.shipment_no=t5.shipment_no AND t2.po_no=t5.po_no AND t2.po_item = t5.ord_no AND " & _
                "t2.material_code=m1.material_code AND " & _
                "t3.supplier_code=m2.supplier_code AND " & _
                "t1.findoc_createdby=m3.user_ct AND " & _
                "t1.SHIPMENT_NO = '" & v_pono & "' AND " & _
                "t1.ORD_NO = '" & v_num & "' AND t1.FINDOC_TYPE = 'CS' AND t1.FINDOC_GROUPTO = 'FIN') t1 " & _
                "LEFT JOIN tbm_users m3 ON t1.findoc_appby=m3.user_ct " & _
                "LEFT JOIN tbm_unit_equivalent m4 ON m4.unit_code=t1.unit_code AND unit_code_to='KGS' " & _
                "LEFT JOIN tbm_kurs m5 ON t1.currency_code=m5.currency_code AND t1.effective_date=m5.effective_date "
                SQlStr_2 = SQlStr

                Dim vpoorder As String

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_2, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        amt_PO = Val(MyReader.GetString(30)) * Val(MyReader.GetString(34))
                        vnom_po = MyReader.GetString("po_no")
                        vnom_invoice = MyReader.GetString("invoice_no")
                        amt_efective = Val(MyReader.GetString(31))
                        sp_po = MyReader.GetString("poorder")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If txtPO2 <> "" And xto_sap = "1" Then
                    'supram
                    'SQlStr_2 = "delete from template_poim where ebeln = '" & sp_ebeln & "' and SHIPMENT = getShipment('" & sp_po & "') and INVOICE_NO = '" & sp_invoice_no & "'"
                    SQlStr_2 = "delete from template_poim where po = '" & sp_po & "' and ord_no = '" & sp_ord_no & "'"
                    affrow = DBQueryUpdate(SQlStr_2, MyConn, False, ErrMsg, UserData)


                    SQlStr_2 = "" & _
                        "INSERT INTO template_poim (" & _
                        "   EBELN, SHIPMENT, SOB, approve, PREPARED, KURS, VESSEL, QUANTITY, UNIT_PRICE, UNIT2, INVOICE_NO, " & _
                        "   INVOICE_DATE, INVOIC_DUE_DATE, VALUE_DATE, INSURANCE, UNIT8, PPN,UNIT10, IMPORT_DUTY, UNIT9, " & _
                        "   CLEAReNCE_COST, UNIT11, flag_upload, User_id, po, ord_no " & _
                        ") VALUES (" & _
                        "   '" & sp_ebeln & "', getShipment('" & sp_po & "'),'" & Format(sp_sob, "yyyy-MM-dd") & "'," & _
                        "   '" & sp_approve & "', '" & sp_prepared & "','" & sp_kurs & "', '" & sp_vessel & "', (SELECT REPLACE('" & sp_quantity & "',',',''))  ," & _
                        "   '" & sp_unit_price & "','" & sp_unit2 & "','" & sp_invoice_no & "','" & Format(sp_invoice_date, "yyyy-MM-dd") & "'," & _
                        "   '" & sp_invoic_due_date & "','" & sp_value_date & "'," & sp_insurance & ",'" & sp_unit8 & "','" & sp_ppn & "','" & sp_unit10 & "'," & _
                        "   '" & sp_import_duty & "','" & sp_unit9 & "','" & sp_clearence_cost & "','" & sp_unit11 & "','" & sp_flag_upload & "','" & sp_user_id & "','" & sp_po & "','" & sp_ord_no & "'" & _
                        ")"
                    affrow = DBQueryUpdate(SQlStr_2, MyConn, False, ErrMsg, UserData)

                    'affrow = DBQueryUpdate(SQlStr, MyConn, False, ErrMsg, UserData)
                End If





            Case "KOOO"
                If selKO = 1 Then
                    SQlStr = "SELECT DISTINCT TSDOC.SHIPMENT_NO,TU.NAME,TU.TITLE,TP.COMPANY_CODE, " & _
                    "TC.COMPANY_NAME,TC.ADDRESS,TC.NPWP,TC.CITY_CODE,TCI.CITY_NAME,TC.API_U_APIT_NO,TC.PHONE,TC.FAX, " & _
                    "TE.COMPANY_NAME as ExpName,TE.NPWP as ExpNPWP,TE.ADDRESS as ExpAddress,TE.EDI_NO,TE.PHONE as ExpPhone, " & _
                    "TE.FAX as ExpFax,TS.SUPPLIER_CODE,TSU.SUPPLIER_NAME, " & _
                    "tv.invoice PACKINGLIST_NO,TS.BL_NO,TS.INSURANCE_NO,TCI2.CITY_NAME as PrintCity,date_format(TSDOC.FINDOC_PRINTEDDT,'%d %M %Y') as FINDOC_PRINTEDDT,TE.AUTHORIZE_PERSON " & _
                    "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                    "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                    "INNER JOIN TBL_PO AS TP ON TSD.PO_NO = TP.PO_NO  " & _
                    "INNER JOIN TBL_SHIPPING AS TS ON TSDOC.SHIPMENT_NO = TS.SHIPMENT_NO  " & _
                    "INNER JOIN TBM_SUPPLIER AS TSU ON TS.SUPPLIER_CODE = TSU.SUPPLIER_CODE " & _
                    "LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_APPBY = TU.USER_CT " & _
                    "LEFT JOIN TBM_COMPANY AS TC ON TP.COMPANY_CODE = TC.COMPANY_CODE " & _
                    "LEFT JOIN TBM_CITY AS TCI ON TC.CITY_CODE = TCI.CITY_CODE " & _
                    "LEFT JOIN TBM_CITY AS TCI2 ON TSDOC.FINDOC_PRINTEDON = TCI2.CITY_CODE " & _
                    "LEFT JOIN TBM_EXPEDITION AS TE ON TSDOC.FINDOC_TO = TE.COMPANY_CODE " & _
                    "LEFT JOIN (select shipment_no, group_concat(trim(invoice_no),'   /   ',date_format(invoice_Dt,'%d %M %Y') separator ', ') as invoice " & _
                    "           from (select distinct shipment_no, invoice_no, invoice_Dt from tbl_shipping_invoice) t1 group by shipment_no " & _
                    "          ) as tv on TSDOC.SHIPMENT_NO=tv.shipment_no " & _
                    "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'KO' and tsdoc.ord_no = '" & v_num & "'"
                Else
                    SQlStr = "SELECT DISTINCT TSDOC.SHIPMENT_NO,TU.NAME,TU.TITLE,TP.COMPANY_CODE, " & _
                    "TC.COMPANY_NAME,TC.ADDRESS,TC.NPWP,TC.CITY_CODE,TCI.CITY_NAME,TC.API_U_APIT_NO,TC.PHONE,TC.FAX, " & _
                    "TE.AUTHORIZE_PERSON as ExpName,TE.TITLE as ExpNPWP,TE.ADDRESS as ExpAddress,TE.IDENTITY_NO AS EDI_NO,TE.PHONE as ExpPhone, " & _
                    "TE.FAX as ExpFax,TS.SUPPLIER_CODE,TSU.SUPPLIER_NAME, " & _
                    "tv.invoice PACKINGLIST_NO,TS.BL_NO,TS.INSURANCE_NO,TCI2.CITY_NAME as PrintCity,date_format(TSDOC.FINDOC_PRINTEDDT,'%d %M %Y') as FINDOC_PRINTEDDT,TE.AUTHORIZE_PERSON " & _
                    "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                    "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                    "INNER JOIN TBL_PO AS TP ON TSD.PO_NO = TP.PO_NO  " & _
                    "INNER JOIN TBL_SHIPPING AS TS ON TSDOC.SHIPMENT_NO = TS.SHIPMENT_NO  " & _
                    "INNER JOIN TBM_SUPPLIER AS TSU ON TS.SUPPLIER_CODE = TSU.SUPPLIER_CODE " & _
                    "LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_APPBY = TU.USER_CT " & _
                    "LEFT JOIN TBM_COMPANY AS TC ON TP.COMPANY_CODE = TC.COMPANY_CODE " & _
                    "LEFT JOIN TBM_CITY AS TCI ON TC.CITY_CODE = TCI.CITY_CODE " & _
                    "LEFT JOIN TBM_CITY AS TCI2 ON TSDOC.FINDOC_PRINTEDON = TCI2.CITY_CODE " & _
                    "LEFT JOIN TBM_EXPEDITION AS TE ON TSDOC.FINDOC_TO = TE.COMPANY_CODE " & _
                    "LEFT JOIN (select shipment_no, group_concat(trim(invoice_no),'   /   ',date_format(invoice_Dt,'%d %M %Y') separator ', ') as invoice " & _
                    "           from (select distinct shipment_no, invoice_no, invoice_Dt from tbl_shipping_invoice) t1 group by shipment_no " & _
                    "          ) as tv on TSDOC.SHIPMENT_NO=tv.shipment_no " & _
                    "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'KO' and tsdoc.ord_no = '" & v_num & "'"
                End If
            Case "SKKK"
                SQlStr = "SELECT DISTINCT TSDOC.SHIPMENT_NO,TU.NAME,TU.TITLE,TP.COMPANY_CODE, " & _
                "TC.COMPANY_NAME,TC.ADDRESS,TC.NPWP,TC.CITY_CODE,TCI.CITY_NAME,TC.API_U_APIT_NO,TC.PHONE,TC.FAX, " & _
                "TE.COMPANY_NAME,TE.NPWP as ExpNPWP,TE.ADDRESS as ExpAddress,TE.EDI_NO,TE.PHONE as ExpPhone, " & _
                "TE.FAX as ExpFax,TS.SUPPLIER_CODE,TSU.SUPPLIER_NAME, " & _
                "TS.PACKINGLIST_NO,concat(trim(TS.BL_NO),' / ',ifnull(date_format(ts.est_Delivery_dt,'%d-%m-%Y'),'')) as bl_no,TS.INSURANCE_NO,TCI2.CITY_NAME as PrintCity,date_format(TSDOC.FINDOC_PRINTEDDT,'%d %M %Y') as FINDOC_PRINTEDDT,TE.AUTHORIZE_PERSON as ExpName " & _
                "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                "INNER JOIN TBL_PO AS TP ON TSD.PO_NO = TP.PO_NO  " & _
                "INNER JOIN TBL_SHIPPING AS TS ON TSDOC.SHIPMENT_NO = TS.SHIPMENT_NO  " & _
                "INNER JOIN TBM_SUPPLIER AS TSU ON TS.SUPPLIER_CODE = TSU.SUPPLIER_CODE " & _
                "LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_APPBY = TU.USER_CT " & _
                "LEFT JOIN TBM_COMPANY AS TC ON TP.COMPANY_CODE = TC.COMPANY_CODE " & _
                "LEFT JOIN TBM_CITY AS TCI ON TC.CITY_CODE = TCI.CITY_CODE " & _
                "LEFT JOIN TBM_CITY AS TCI2 ON TSDOC.FINDOC_PRINTEDON = TCI2.CITY_CODE " & _
                "LEFT JOIN TBM_EXPEDITION AS TE ON TSDOC.FINDOC_TO = TE.COMPANY_CODE " & _
                "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'SK' and tsdoc.ord_no = '" & v_num & "'"
            Case "JCCC"
                SQlStrA = "SELECT FINDOC_REFF FROM TBL_SHIPPING_DOC  " & _
                          "WHERE SHIPMENT_NO = '" & v_pono & "' AND FINDOC_TYPE = 'JC' AND ORD_NO = " & v_num
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_bank = MyReader.GetString(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If v_bank <> "" Then
                    arrTemp = Split(v_bank, ";")
                    Try
                        v_bank = arrTemp(0) & " " & arrTemp(1)
                        v_acc = arrTemp(2)
                        v_name = arrTemp(3)
                    Catch
                    End Try
                End If

                SQlStr = "SELECT DISTINCT TSD2.PO, TE.COMPANY_NAME as ExpName,TE.ADDRESS as ExpAddress,TCI3.CITY_NAME as ExpCity, " & _
                "TU.NAME,TU.TITLE,TC.COMPANY_NAME,TC.ADDRESS,TCI.CITY_NAME, " & _
                "CONCAT(TC.PHONE, IF(TC.FAX<>'',CONCAT(' / ',TC.FAX),'')) PHONE, " & _
                "TE.AUTHORIZE_PERSON,TE.TITLE as Title2, " & _
                "TSDOC.FINDOC_VALCUR, TSDOC.FINDOC_VALAMT, TSDOC.FINDOC_NOTE, TSDOC.FINDOC_REFF, " & _
                "IF(TSDOC.FINDOC_REFF='',concat(TE.BANK_NAME,' ',TE.BANK_BRANCH),'" & v_bank & "') BANK_NAME, " & _
                "IF(TSDOC.FINDOC_REFF='',TE.ACCOUNT_NO,'" & v_acc & "') ACCOUNT_NO, " & _
                "IF(TSDOC.FINDOC_REFF='',TE.ACCOUNT_NAME,'" & v_name & "') ACCOUNT_NAME, " & _
                "TS.BL_NO, CONCAT('(',TU.NAME,')') as kuasa1," & _
                "CONCAT('(',TE.AUTHORIZE_PERSON,')') as kuasa2," & _
                "CONCAT(TCI.CITY_NAME,', ',date_format(TSDOC.FINDOC_PRINTEDDT,'%d %M %Y')) as FINDOC_PRINTEDDT, " & _
                "CONCAT('Untuk menarik jaminan container yang masih berada di ',TE.COMPANY_NAME,', dan keuangannya mohon dapat di transfer ke rekening atas nama ',TE.ACCOUNT_NAME,', dengan data-data sebagai berikut:') as ket " & _
                "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                "INNER JOIN TBL_PO AS TP ON TSD.PO_NO = TP.PO_NO  " & _
                "INNER JOIN TBL_SHIPPING AS TS ON TSDOC.SHIPMENT_NO = TS.SHIPMENT_NO  " & _
                "INNER JOIN TBM_SUPPLIER AS TSU ON TS.SUPPLIER_CODE = TSU.SUPPLIER_CODE " & _
                "LEFT JOIN TBM_USERS AS TU ON TSDOC.FINDOC_APPBY = TU.USER_CT " & _
                "LEFT JOIN TBM_COMPANY AS TC ON TP.COMPANY_CODE = TC.COMPANY_CODE " & _
                "LEFT JOIN TBM_CITY AS TCI ON TC.CITY_CODE = TCI.CITY_CODE " & _
                "LEFT JOIN TBM_CITY AS TCI2 ON TSDOC.FINDOC_PRINTEDON = TCI2.CITY_CODE " & _
                "LEFT JOIN TBM_EXPEDITION AS TE ON TSDOC.FINDOC_TO = TE.COMPANY_CODE " & _
                "LEFT JOIN TBM_CITY AS TCI3 ON TE.CITY_CODE = TCI3.CITY_CODE " & _
                "LEFT JOIN (SELECT shipment_no, group_concat(distinct(getPOorder(" & v_pono & ",trim(po_no))) separator ', ') PO FROM TBL_SHIPPING_DETAIL WHERE shipment_no = '" & v_pono & "' GROUP BY shipment_no) AS TSD2 ON TSDOC.SHIPMENT_NO = TSD2.SHIPMENT_NO " & _
                "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'JC' and tsdoc.ord_no = " & v_num
            Case "BCCC"
                SQlStr = "select b.name,b.title,e.company_name,e.address,f.city_name,e.npwp,e.api_u_apit_no,concat(if(e.phone is null,'',e.phone),' / ',if(e.fax IS NULL,'',e.fax)) as phonefax " & _
                         "from tbl_shipping_doc as a " & _
                         "left join tbm_users as b on a.findoc_appby = b.user_ct " & _
                         "inner join tbl_shipping_Detail as c on a.shipment_no=c.shipment_no " & _
                         "inner join tbl_po as d on c.po_no=d.po_no " & _
                         "left join tbm_company as e on d.company_code=e.company_code " & _
                         "left join tbm_city as f on e.city_code=f.city_code " & _
                         "where a.shipment_no=" & v_pono & " and a.findoc_type='BC' and a.ord_no = " & v_num & _
                         " group by name,title,company_name"
            Case "BSCC"
                SQlStr = "SELECT t1.*, t2.name appby, t2.title apptitle " & _
                         "FROM (SELECT a.shipment_no, DATE_FORMAT(findoc_printeddt,'%d %M %Y') printdt, m.city_name printon, " & _
                         "      b.bl_no, DATE_FORMAT(b.est_delivery_dt,'%d %M %Y') bldt, " & _
                         "      b.vessel, s.port_name, concat(s.port_name,'.') port_name2, o.company_name, o.address, p.city_name city, " & _
                         "      r.produsen_name produsen, r.address produsen_address, q.supplier_name broker, q.address broker_address, " & _
                         "      DATE_FORMAT(findoc_appdt,'%d %M %Y') appdt, findoc_appby " & _
                         "      FROM tbl_shipping_doc a, tbl_shipping b, " & _
                         "      tbm_city m, tbm_plant n, tbm_company o, tbm_city p, tbm_supplier q, tbm_produsen r, tbm_port s " & _
                         "       WHERE(a.shipment_no = b.shipment_no And a.findoc_printedon = m.city_code And b.plant_code = n.plant_code And n.company_code = o.company_code) " & _
                         "      AND o.city_code=p.city_code AND b.supplier_code=q.supplier_code AND a.findoc_to=r.produsen_code AND b.port_code=s.port_code " & _
                         "      AND a.shipment_no = " & v_pono & " AND a.findoc_type='BS' AND a.ord_no = " & v_num & ") t1 " & _
                         "LEFT JOIN tbm_users t2 ON t1.findoc_appby=t2.user_ct "

            Case "PCCC"
                SQlStr = "SELECT t1.vessel, t1.bl_no, GROUP_CONCAT(DISTINCT(getPOorder(t1.shipment_no,TRIM(t2.po_no))) SEPARATOR ', ') PO " & _
                         "FROM tbl_shipping t1, tbl_shipping_detail t2 WHERE t1.shipment_no=t2.shipment_no AND t1.shipment_no='" & v_pono & "' GROUP BY t1.shipment_no "

            Case "SZSZ"
                SQlStr = "SELECT sh.PO, t1.*, tu.name appby, tu.title FROM " & _
                        "  (SELECT t1.shipment_no, date_format(t1.findoc_printeddt,'%M %d, %Y') findoc_printeddt, concat(m2.city_name,', ') printedon, t1.findoc_appby, " & _
                        "   m1.perihal, m1.header1, m1.header2, '' as doc_name, m1.footer1, m1.footer2 " & _
                        "   FROM tbl_shipping_doc t1, tbm_cr_template m1, tbm_city m2 " & _
                        "   WHERE t1.shipment_no='" & v_pono & "' AND t1.ord_no=" & v_num & " AND t1.findoc_type='SZ' " & _
                        "   AND m1.type_code='SZ' AND m1.group_code='" & v_docgrp & "' " & _
                        "   AND t1.findoc_printedon = m2.city_code) t1 " & _
                        "LEFT JOIN tbm_users AS tu ON t1.findoc_appby = tu.user_ct " & _
                        "LEFT JOIN (SELECT shipment_no, GROUP_CONCAT(DISTINCT TRIM(po_no) SEPARATOR ', ') PO " & _
                        "           FROM tbl_shipping_detail WHERE shipment_no='" & v_pono & "' GROUP BY shipment_no) AS sh ON sh.shipment_no=t1.shipment_no "

            Case "SBSB"

                SQlStr = "SELECT sh.PO, t1.*, tu.name appby, tu.title FROM " & _
                        "  (SELECT m4.company_name, m4.address companyaddress, t1.shipment_no, date_format(t1.findoc_printeddt,'%M %d, %Y') findoc_printeddt, concat(m2.city_name,', ') printedon, t1.findoc_appby, " & _
                        "   m1.perihal, m1.header1, m1.header2, '' as doc_name, m1.footer1, m1.footer2 " & _
                        "   FROM tbl_shipping t0, tbl_shipping_doc t1, tbm_cr_template m1, tbm_city m2, tbm_plant m3, tbm_company m4 " & _
                        "   WHERE t0.shipment_no=t1.shipment_no AND t1.shipment_no='" & v_pono & "' AND t1.ord_no=" & v_num & " AND t1.findoc_type='SB' " & _
                        "   AND m1.type_code='SB' AND m1.group_code='" & v_docgrp & "' " & _
                        "   AND t1.findoc_printedon = m2.city_code AND t0.plant_code=m3.plant_code AND m3.company_code=m4.company_code) t1 " & _
                        "LEFT JOIN tbm_users AS tu ON t1.findoc_appby = tu.user_ct " & _
                        "LEFT JOIN (SELECT shipment_no, GROUP_CONCAT(DISTINCT(getPOorder(shipment_no,TRIM(po_no))) SEPARATOR ', ') PO " & _
                        "           FROM tbl_shipping_detail WHERE shipment_no='" & v_pono & "' GROUP BY shipment_no) AS sh ON sh.shipment_no=t1.shipment_no "

            Case "MCI1", "MCI2"
                SQlStr = "select a.doc_Address,concat('Jakarta, ',date_format(a.openingdt,'%d %M %Y')) as openingdt, " & _
                         "if(b.notice_arrival_Dt IS NULL,date_format(b.est_arrival_dt,'%d %M %Y'),date_format(b.notice_arrival_dt,'%d %M %Y')) as eta," & _
                         "b.vessel,CONCAT(CONCAT(c.port_name,', '),e.city_name) port_name, " & _
                         "b.total_container, a.survey_Req,a.footer_note,c.port_name,d.name,d.title " & _
                         "from tbl_mci as a inner join tbl_shipping as b on a.shipment_no=b.shipment_no " & _
                         "inner join tbm_port as c on b.port_code=c.port_code " & _
                         "INNER JOIN tbm_city AS e ON c.city_code=e.city_code " & _
                         "left join tbm_users as d on a.approvedby=d.user_Ct " & _
                         "where b.shipment_no=" & v_pono & " and a.ord_no=" & v_num

            Case "TTDPAJAK"
                SQlStr = "SELECT '" & UserData.UserName & "' as printby, m4.company_name, m3.name, DATE_FORMAT(t1.Verified2Dt,'%d %M %Y') Verified2Dt, IF(t1.aju_no IS NULL or trim(t1.aju_no)='','-', t1.aju_no) pib_no, t1.bea_masuk, t1.vat, t1.pph21, t1.piud, t1.kurs_pajak, " & _
                         "CONCAT(' ',(SELECT GROUP_CONCAT(DISTINCT getpoorder(shipment_no, TRIM(po_no))) FROM tbl_shipping_detail t2 WHERE t1.shipment_no=t2.shipment_no GROUP BY t2.shipment_no)) DetailofPO, t1.createdby, concat('*Daftar Tanda Terima Pajak dari dokumen ',m5.name) createdbyname, DATE_FORMAT(NOW(),'%d %M %Y') printeddt " & _
                         "FROM tbl_shipping t1, tbm_plant m1, tbm_port m2, tbm_users m3, tbm_company m4, tbm_users m5 " & _
                         "WHERE(t1.Verified2Dt Is Not NULL And t1.plant_code = m1.plant_code And t1.port_code = m2.port_code And t1.Verified2By = m3.user_ct And m1.company_code = m4.company_code And t1.createdby=m5.user_ct) " & _
                         "AND t1.Verified2Dt = '" & v_dt & "' AND t1.Verified2By = '" & v_by & "' AND m1.company_code = '" & v_cp & "' AND (t1.createdby = '" & v_crea & "' OR '' = '" & v_crea & "') ORDER BY DetailofPO "

            Case "TTDCSLIP"
                SQlStr = "SELECT '" & UserData.UserName & "' as printby, m2.company_name, t2.invoice_no, CONCAT(' ',getpoorder(t2.shipment_no, TRIM(t2.po_no))) po_no, t3.quantity, t4.unit_code, m1.material_name, t1.findoc_valamt, t1.findoc_finappby, m3.name, m3.title, DATE_FORMAT(t1.findoc_finappdt,'%d %M %Y') findoc_finappdt, " & _
                         "t5.createdby, concat('*Daftar Tanda Terima Cost Slip dari dokumen ',m4.name) createdbyname, DATE_FORMAT(NOW(),'%d %M %Y') printeddt " & _
                         "FROM tbl_shipping_doc t1, tbl_shipping_invoice t2, tbl_shipping_detail t3, tbl_po_detail t4, tbl_po t5, tbm_material m1, tbm_company m2, tbm_users m3, tbm_users m4 " & _
                         "WHERE t1.findoc_type='CS' AND t1.findoc_status='Final Approved' AND t1.findoc_finappdt IS NOT NULL " & _
                         "AND t1.shipment_no=t2.shipment_no AND t1.findoc_no=t2.po_no AND t1.findoc_reff=t2.ord_no " & _
                         "AND t2.shipment_no=t3.shipment_no AND t2.po_no=t3.po_no AND t2.ord_no=t3.po_item " & _
                         "AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item AND t4.po_no=t5.po_no " & _
                         "AND t3.material_code=m1.material_code AND t5.company_code=m2.company_code AND t1.findoc_finappby=m3.user_ct AND t5.createdby=m4.user_ct " & _
                         "AND t1.findoc_finappdt = '" & v_dt & "' AND t1.findoc_finappby = '" & v_by & "' AND t5.company_code = '" & v_cp & "' AND (t5.createdby = '" & v_crea & "' OR '' = '" & v_crea & "') ORDER BY t2.po_no "

            Case "TTDPV"
                SQlStr = "SELECT t1.*, IF(number_inv=1,invoice_amount - finaltybl, invoice_amount - (finaltybl * (invoice_amount/total_inv))) netinvoice_amount " & _
                         "FROM ( " & _
                         " SELECT t1.*, IF(t2.name IS NULL, '          ',t2.name) finby, IF(t2.title IS NULL or t2.title = '', '          ', CONCAT('( ',t2.title, ' )')) fintitle FROM " & _
                         "  (SELECT DATE_FORMAT('" & v_dt & "','%d %M %Y') AS printdt, '" & UserData.UserName & "' AS printby, t1.shipment_no, t1.invoice_no, t1.invoice_dt, DATE_FORMAT(t3.tt_dt,'%d %M %Y') tt_dt, getpoorder(t1.shipment_no, trim(t1.po_no)) po_no, t1.ord_no, t2.quantity, t4.unit_code, m1.material_name, concat('Group Material : ','" & v_matnm & "') as material_group, (t1.invoice_amount-t1.invoice_penalty) invoice_amount, t3.finalty finaltybl, " & _
                         "   (SELECT SUM(invoice_amount-invoice_penalty) FROM tbl_shipping_invoice st1 WHERE st1.shipment_no=t1.shipment_no) total_inv, " & _
                         "   (SELECT SUM(1) FROM tbl_shipping_invoice st1 WHERE st1.shipment_no=t1.shipment_no) number_inv,    " & _
                         "   (SELECT company_name FROM tbm_company WHERE company_code = '" & v_cp & "') company, m2.supplier_name " & _
                         "   FROM tbl_shipping_invoice t1, tbl_shipping_detail t2, tbl_shipping t3, tbl_po_detail t4, tbm_material m1, tbm_supplier m2 " & _
                         "   WHERE(t1.shipment_no = t2.shipment_no And t1.po_no = t2.po_no And t1.ord_no = t2.po_item) " & _
                         "   AND t1.shipment_no=t3.shipment_no " & _
                         "   AND t2.po_no=t4.po_no AND t2.po_item=t4.po_item " & _
                         "   AND t2.material_code=m1.material_code AND t3.supplier_code=m2.supplier_code" & _
                         "   AND t1.shipment_no IN " & _
                         "     (SELECT DISTINCT t1.shipment_no FROM tbl_shipping_doc t1, tbl_shipping_detail t2, tbm_material m1 " & _
                         "     WHERE t1.shipment_no=t2.shipment_no AND t2.material_code=m1.material_code " & _
                         "     AND t1.findoc_type='PV' AND t1.findoc_status <> 'Rejected' " & _
                         "     AND t1.findoc_createdby = '" & v_crea & "' AND (t1.findoc_finappdt='" & v_dt & "' OR '' = '" & v_dt & "') AND (t1.findoc_finappby = '" & v_by & "' OR '' = '" & v_by & "') AND (m1.group_code = '" & v_matcd & "' OR '' = '" & v_matcd & "')) " & _
                         " ) t1 " & _
                         " LEFT JOIN (SELECT NAME, title FROM tbm_users WHERE user_ct = '" & v_by & "') t2 ON 1=1 " & _
                         " ORDER BY t1.invoice_dt, t1.invoice_no, t1.po_no, t1.ord_no " & _
                         ") t1 "

            Case "OUTB"
                SQlStr = "SELECT clmstr1,clmstr2,clmstr3,clmstr4,clmstr5,clmstr6,clmstr7,clmstr8,clmdec1," & _
                         "clmdate1,clmdate2,clmstr9,clmstr10,clmstr11,clmstr12,clmstr13,clmstr14 " & _
                         "FROM " & TempTableName & " " & _
                         "ORDER BY clmstr7,clmdate1,clmdate2 "
            Case "DNPC"
                'SQlStr = "SELECT T1.shipment_no,T1.pib_no,DATE_FORMAT(T1.pib_dt,'%d-%b-%Y') pib_dt,t2.material_code,IF(COUNT(t2.material_code)=1, m5.material_name,m6.group_name) DescOfGroup, " & _
                '         "m2.company_name,m2.address, m2.phone,m2.fax,m4.city_name,DATE_FORMAT(t3.findoc_appdt,'%d-%b-%Y') FindocAppDt,t3.findoc_printedon,t3.findoc_appby,m3.supplier_name, " & _
                '         "t4.insurance_code,t4.currency_code, SUM(t5.invoice_amount-t5.invoice_penalty) TTL,m3.address SuppAddress, " & _
                '         "(SELECT m8.city_name FROM tbm_city m8 WHERE m8.city_code=t3.findoc_printedon) CityPrintedOn, " & _
                '         "(SELECT m9.city_name FROM tbm_city m9 WHERE m9.city_code=m3.city_code) CitySupplier, " & _
                '         "(SELECT m10.name FROM tbm_users m10 WHERE m10.user_ct=t3.findoc_appby) UserName " & _
                '         "FROM tbl_shipping T1,tbl_shipping_detail t2,tbl_shipping_doc t3,tbl_po t4,tbl_shipping_invoice t5,tbm_plant m1,tbm_company m2, " & _
                '         "tbm_supplier m3,tbm_city m4,tbm_material m5,tbm_material_group m6, tbm_users m10 " & _
                '         "WHERE T1.shipment_no = '" & v_pono & "' AND t3.ord_no=" & v_num & " AND t3.findoc_type='NP' AND T1.shipment_no = T2.shipment_no " & _
                '         "AND T1.shipment_no = T3.shipment_no AND t2.po_no=t4.po_no AND m1.plant_code = t1.plant_code " & _
                '         "AND T1.shipment_no = T5.shipment_no AND m2.company_code = m1.company_code AND m3.supplier_code =  t1.supplier_code " & _
                '         "AND m4.city_code = m2.city_code AND m5.material_code = t2.material_code AND m6.group_code = m5.group_code "

                SQlStr = "SELECT T1.shipment_no,T1.pib_no,DATE_FORMAT(T1.pib_dt,'%d-%b-%Y') pib_dt, " & _
                         "IF(COUNT(t2.material_code)=1, m5.material_name,m6.group_name) DescOfGroup, m2.company_name, " & _
                         "m2.address,m4.city_name, t3.findoc_printedon,t3.findoc_appby,m3.supplier_name,SUM(t5.invoice_amount-t5.invoice_penalty) TTL, " & _
                         "(SELECT m10.name FROM tbm_users m10 WHERE m10.user_ct=t3.findoc_appby) UserName, " & _
                         "IF(m2.fax<>'',CONCAT(m2.phone,' / ',m2.fax),m2.phone) PhoneFax, " & _
                         "IF(m3.address<>'',CONCAT(UPPER(m3.address),' , ',UPPER((SELECT m9.city_name FROM tbm_city m9 WHERE m9.city_code=m3.city_code))),'') SuppAddress, " & _
                         "CONCAT((SELECT m8.city_name FROM tbm_city m8 WHERE m8.city_code=t3.findoc_printedon),' , ',DATE_FORMAT(t3.findoc_appdt,'%d-%b-%Y')) PrintedOn, " & _
                         "CONCAT(t4.insurance_code,' ',t4.currency_code,' ') Insurance " & _
                         "FROM tbl_shipping T1,tbl_shipping_detail t2,tbl_shipping_doc t3,tbl_po t4,tbl_shipping_invoice t5, " & _
                         "tbm_plant m1,tbm_company m2, tbm_supplier m3,tbm_city m4,tbm_material m5,tbm_material_group m6, " & _
                         "tbm_users m10 " & _
                         "WHERE T1.shipment_no = '" & v_pono & "' AND t3.ord_no=" & v_num & " AND t3.findoc_type='NP' AND " & _
                         "T1.shipment_no = T2.shipment_no And T1.shipment_no = T3.shipment_no And t2.po_no = t4.po_no " & _
                         "AND m1.plant_code = t1.plant_code AND T1.shipment_no = T5.shipment_no AND  " & _
                         "m2.company_code = m1.company_code AND m3.supplier_code =  t1.supplier_code AND " & _
                         "m4.city_code = m2.city_code AND m5.material_code = t2.material_code AND " & _
                         "m6.group_code = m5.group_code "
            Case "RIL" 'Report Request Import Lisence' Added By Prie 02.11.2010
                SQlStr = "select clmint1,clmdec1,clmdec2,clmdec3,clmdec4,clmdec5,clmstr1,clmstr2,clmstr3,clmstr4,clmstr5,clmstr6,clmstr7,clmstr8,clmstr9,clmstr10,clmstr11,clmstr12,clmstr13,clmstr14 " & _
                         "from " & TempTableName & " " & _
                         "order by ClmStr1"
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

        'Dim aReport As ReportClass
        '===================================================================================
        ' SECOND QUERY
        '===================================================================================
        Select Case CStr(v_type)
            Case "BOLC", "ICLC"
                If v_type = "BOLC" Then
                    aReport = New CR01LC
                Else
                    aReport = New CRICLC
                End If

                SQlStr_9 = "Select tms.note " & _
                           "from tbl_budget as tb inner join tbl_po as tp on tb.po_no = tp.po_no  " & _
                           "left join tbm_supplier as tms on tp.supplier_code = tms.supplier_code  " & _
                           "where tb.status <> 'Rejected' and tb.po_no = '" & v_pono & "' and tb.ord_no = '" & v_num & "' and tb.type_code = '" & v_type & "'"
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_9, MyConn, "", ErrMsg, UserData))
            Case "SHIN"
                Select Case v_doc
                    Case "5"   'For Additive 
                        aReport = New CR02SI_A
                    Case "4"   'For Premix
                        aReport = New CR02SI
                    Case Else
                        aReport = New CR02SI_B    '" di cari tanggal batasa di pakai
                        ' aReport = New CR02SI_D   ' DI batalkan
                End Select

                'aReport = New CR02SI
                Dim txtOpeningDt As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtOpeningDt"), TextObject)
                Dim txtShipment_Periode_FR As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtShipment_Periode_FR"), TextObject)
                Dim txt36 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("text36"), TextObject)
                Dim v_openingdt As Date
                Dim v_shipment_fr, v_shipment_to As Date
                Dim temp As String

                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_openingdt = MyReader.GetString("openingdt")
                        v_shipment_fr = MyReader.GetString("SHIPMENT_PERIOD_FR")
                        v_shipment_to = MyReader.GetString("SHIPMENT_PERIOD_TO")
                    End While
                End If
                txtOpeningDt.Text = Format(CDate(v_openingdt), "dd-MMM-yyyy")
                txtShipment_Periode_FR.Text = UCase(Format(CDate(v_shipment_fr), "dd MMMMM  yyyy")) & " - " & UCase(Format(CDate(v_shipment_to), "dd MMMMM  yyyy"))
                CloseMyReader(MyReader, UserData)

                Dim tot As Double
                Dim Txt_gab_mat As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("TxtGabMat"), TextObject)

                If v_shipmentno = "" Then
                    SQlStrA = "select FormatDec(tpd.QUANTITY) as QTY, tpd.material_code, tmu.unit_name, tpd.price, tmm.MATERIAL_NAME, tpd.QUANTITY as QUANTITY " & _
                    "from tbl_si as tsi " & _
                    "inner join tbl_po as tpo on tsi.po_no = tpo.po_no " & _
                    "inner join tbl_po_detail as tpd on tpo.po_no = tpd.po_no " & _
                    "inner join tbm_unit as tmu on tpd.unit_code = tmu.unit_code " & _
                    "inner join tbm_material as tmm on tpd.material_code = tmm.material_code " & _
                    "where tsi.shipment_no is null and tsi.po_no = '" & v_pono & "' and tsi.ord_no = '" & v_num & "'"
                Else
                    SQlStrA = "select FormatDec(tpo.QUANTITY) as QTY, tpo.material_code, tmu.unit_name, tpd.price, tmm.MATERIAL_NAME, tpo.QUANTITY as QUANTITY " & _
                   "from tbl_si as tsi " & _
                   "inner join tbl_shipping_detail AS tpo ON tsi.shipment_no = tpo.shipment_no " & _
                   "inner join tbl_po_detail AS tpd ON tpo.po_no = tpd.po_no AND tpo.po_item = tpd.po_item " & _
                   "inner join tbm_unit AS tmu ON tpd.unit_code = tmu.unit_code " & _
                   "inner join tbm_material AS tmm ON tpo.material_code = tmm.material_code " & _
                   "where tsi.shipment_no = '" & v_shipmentno & "' and tsi.ord_no = '" & v_num & "'"
                End If

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                temp = ""
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        temp = MyReader.GetString(0) & " " & MyReader.GetString(2) & " " & Trim(MyReader.GetString(4))
                        Txt_gab_mat.Text = Txt_gab_mat.Text & " & " & temp
                        tot = tot + (MyReader.GetString(0) * MyReader.GetString(3))
                    End While
                End If
                Txt_gab_mat.Text = Txt_gab_mat.Text.Substring(2, Txt_gab_mat.Text.Length - 2)

                CloseMyReader(MyReader, UserData)

                'Dim v_gabsidoc As String
                Dim strDoc() As String
                Dim z As Integer
                Dim no As Integer = 1
                Dim v_docname As String

            Case "RILL", "RILB", "RILQ"
                Dim BodyStr As String
                aReport = New CR03RIL
                If v_type = "RILB" Then
                    v_group_code = AmbilData("GROUP_CODE", "tbl_ril", "shipment_no ='" & v_shipmentno & "' and ril_no = '" & v_num & "'")
                    SQlStrA = "select * from tbm_cr_template where type_code= 'RLB' and group_code = '" & v_group_code & "'"
                ElseIf v_type = "RILL" Then
                    v_group_code = AmbilData("GROUP_CODE", "tbl_ril", "po_no ='" & v_pono & "' and ril_no = '" & v_num & "'")
                    SQlStrA = "select * from tbm_cr_template where type_code= 'RL' and group_code = '" & v_group_code & "'"
                End If
                If v_type = "RILQ" Then
                    aReport = New CR03RILQ
                    SQlStrA = "select * from tbm_cr_template where type_code= 'RLQ' and group_code = '" & v_group_code & "'"
                End If
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Dim Txt_Perihal As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Txt_Perihal"), TextObject)
                        Dim Txt_Header1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Txt_Header1"), TextObject)
                        Dim Txt_Header2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Txt_Header2"), TextObject)

                        Txt_Perihal.Text = MyReader.GetString("PERIHAL")
                        Txt_Header1.Text = MyReader.GetString("HEADER1")
                        Txt_Header2.Text = MyReader.GetString("HEADER2")

                        BodyStr = MyReader.GetString("BODY1")

                        Dim txt_Footer1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txt_Footer1"), TextObject)
                        Dim txt_Footer2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txt_Footer2"), TextObject)
                        txt_Footer1.Text = MyReader.GetString("FOOTER1")
                        txt_Footer2.Text = MyReader.GetString("FOOTER2")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                Dim SQlStr2, WhrStr As String
                Dim ItemStr, ValStr As String

                If CStr(v_type) = "RILL" Then
                    SQlStr = "select query_code, query_str from tbm_cr_query where query_code='RILL'"
                    WhrStr = " where pono='" & v_pono & "' and ril_no='" & v_num & "' "

                ElseIf CStr(v_type) = "RILB" Then
                    SQlStr = "select query_code, query_str from tbm_cr_query where query_code='RILB'"
                    WhrStr = " where shipmentno='" & v_shipmentno & "' and ril_no='" & v_num & "' "
                ElseIf CStr(v_type) = "RILQ" Then
                    SQlStr = "select query_code, query_str from tbm_cr_query where query_code='RILQ'"
                    WhrStr = ""
                End If
                ErrMsg = "Gagal baca data query."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            SQlStr = MyReader.GetString("QUERY_STR")
                        Catch
                            SQlStr = ""
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                If CStr(v_type) = "RILL" Then
                    SQlStr = Replace(SQlStr, "1=1", "t1.po_no='" & v_pono & "'")
                ElseIf CStr(v_type) = "RILB" Then
                    SQlStr = Replace(SQlStr, "1=1", "t1.shipment_no='" & v_shipmentno & "'")
                ElseIf CStr(v_type) = "RILQ" Then
                    SQlStr = Replace(SQlStr, "1=1", "t1.ril_no='" & v_num & "'")
                End If

                SQlStr = SQlStr & " " & WhrStr

                ErrMsg = "Gagal baca template letter."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        ItemStr = MyReader.GetString("fill_name")

                        Try
                            ValStr = MyReader.GetString(MyReader.GetString("fill_source"))
                        Catch
                            ValStr = ""
                        End Try

                        BodyStr = Replace(BodyStr, ItemStr, ValStr)
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                If CStr(v_type) = "RILL" Then
                    SQlStr = _
                    "select tr.po_no, TR.RIL_NO, TR.DOC_ADDRESS, TR.OPENINGDT,  trd.doc_no,  '" & BodyStr & "' as doc_name, tr.GROUP_CODE, " & _
                    "tu.name, tu.title from tbl_ril as tr " & _
                    "inner join tbl_ril_doc as trd on tr.po_no = trd.po_no and tr.ril_no = trd.ril_no " & _
                    "inner join tbm_users as tu on tr.approvedby = tu.user_ct " & _
                    "where tr.po_no = '" & v_pono & "' and tr.ril_no = '" & v_num & "'"
                ElseIf CStr(v_type) = "RILB" Then
                    SQlStr = _
                    "select tr.shipment_no, TR.RIL_NO, TR.DOC_ADDRESS, TR.OPENINGDT,  trd.doc_no,  '" & BodyStr & "' as doc_name, tr.GROUP_CODE, " & _
                    "tu.name, tu.title from tbl_ril as tr " & _
                    "inner join tbl_ril_doc as trd on tr.po_no = trd.po_no and tr.ril_no = trd.ril_no " & _
                    "inner join tbm_users as tu on tr.approvedby = tu.user_ct " & _
                    "where tr.shipment_no = '" & v_shipmentno & "' and tr.ril_no = '" & v_num & "'"
                ElseIf CStr(v_type) = "RILQ" Then
                    SQlStr = _
                    "select  TR.RIL_NO, TR.DOC_ADDRESS, TR.OPENINGDT, '" & BodyStr & "' as doc_name, tr.GROUP_CODE, " & _
                    "tu.name, tu.title from tbl_ril_quota as tr " & _
                    "left join tbm_users as tu on tr.approvedby = tu.user_ct " & _
                    "where tr.ril_no = '" & v_num & "'"
                End If

                ErrMsg = "Gagal baca data."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Dim tgl_approve As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("tgl_approve"), TextObject)
                        Try
                            tgl_approve.Text = Format(CDate(MyReader.GetString("OPENINGDT")), "dd - MMMM - yyyy")
                        Catch
                            tgl_approve.Text = ""
                        End Try

                    End While
                End If
                CloseMyReader(MyReader, UserData)

            Case "RILT"
                aReport = New CR03RILT

            Case "BRRR"
                aReport = New CR04BR
            Case "DIII"
                aReport = New CRDI
                'ISI txtSpelling from Amount
                Dim TxtSpelling As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("TxtSpelling"), TextObject)
                Dim TxtHead As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("TxtHead"), TextObject)
                Dim v_amount As Double
                Dim v_curr_code, V_CITY_H As String
                Dim V_TGL_H As Date
                SQlStrA = _
                "select distinct td.shipment_no, td.ord_no, td.FINDOC_PRINTEDON, tc1.city_name as city1,td.FINDOC_PRINTEDdt, td.FINDOC_APPBY, " & _
                "tr.lc_no, tr.bank_code, tmb.CITY_CODE, tc2.city_name as city2, tmb.bank_name, tr.account_no, tr.amount, ts.CURRENCY_CODE " & _
                "from tbl_shipping_doc as td inner join tbl_remitance as tr on td.shipment_no = tr.shipment_no and " & _
                "td.ord_no = tr.ord_no " & _
                "inner join tbl_shipping_detail as tsd on tr.shipment_no = tsd.shipment_no " & _
                "inner join tbl_shipping as ts on tr.shipment_no = ts.shipment_no " & _
                "inner join tbl_po as tp on tsd.po_no = tp.po_no " & _
                "inner join tbl_po_detail as tpd on tsd.po_item = tpd.po_item " & _
                "left join tbm_bank as tmb on tr.bank_code = tmb.bank_code and tp.currency_code = tmb.currency_code  " & _
                "left join tbm_users as tmu on td.FINDOC_APPBY = tmu.USER_CT " & _
                "left join tbm_supplier as tms on tp.supplier_code = tms.supplier_code " & _
                "left join tbm_city as tc1 on td.FINDOC_PRINTEDON = tc1.city_code " & _
                "left join tbm_city as tc2 on tmb.CITY_CODE = tc2.city_code " & _
                "where tD.shipment_no = '" & v_pono & "' and tD.ord_no = '" & v_num & "' and tr.type_code = 'BR'"

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                v_amount = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_amount = MyReader.GetString("Amount")
                        v_curr_code = MyReader.GetString("CURRENCY_CODE")
                        Try
                            V_CITY_H = MyReader.GetString("CITY1")
                        Catch ex As Exception
                            V_CITY_H = ""
                        End Try
                        V_TGL_H = MyReader.GetString("FINDOC_PRINTEDdt")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                v_curr_code = AmbilData("currency_name", "tbm_currency", "currency_code ='" & v_curr_code & "'")
                TxtSpelling.Text = "( " & v_curr_code & " : " & TerbilangInggris(v_amount) & ")"
                TxtHead.Text = V_CITY_H & ", " & Format(V_TGL_H, "MMMM dd, yyyy")

            Case "PVVV"

                SQlStrx = _
                    "SELECT IF(findoc_createddt >= '20200722',1,0) flag, cc.name4 name4, cc.name5 name5 " & _
                    "FROM tbl_shipping_doc TSDOC " & _
                    "CROSS JOIN tbm_cc_pv cc " & _
                    "where TSDOC.shipment_no = '" & v_pono & "' and TSDOC.ord_no = '" & v_num & "' and TSDOC.FINDOC_TYPE = 'PV' "

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrx, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_flag = MyReader.GetString("FLAG")
                        v_name4 = MyReader.GetString("NAME4")
                        v_name5 = MyReader.GetString("NAME5")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                If v_flag = "0" Then
                    aReport = New CRPV
                Else


                    SQlStr_2 = "SELECT CONCAT('PO No : ',TRIM(t1.po_no),' QTY : ') fdet_po, SUM(ROUND(t1.quantity,2)) fquantity, MAX(t2.currency_code) fdet_currency, SUM(ROUND(invoice_amount-invoice_penalty,2)) fdet_amount " & _
                            "FROM tbl_shipping_Detail t1, tbl_shipping t2, tbl_shipping_invoice t3 " & _
                            "WHERE(t1.shipment_no = t2.shipment_no And (t1.shipment_no = t3.shipment_no And t1.po_no = t3.po_no And t1.po_item = t3.ord_no)) " & _
                            "AND t1.shipment_no = " & v_pono & " " & _
                            "GROUP BY t1.po_no "
                    'SQlStrx = "select count(*) ada from (" & SQlStr_2 & ") y1"

                    'MyReader = DBQueryMyReader(SQlStrx, MyConn, ErrMsg, UserData)
                    'rows3 = 0
                    'If Not MyReader Is Nothing Then
                    '    While MyReader.Read
                    '        rows3 = MyReader.GetValue(0)
                    '    End While
                    'End If
                    'CloseMyReader(MyReader, UserData)



                    'If rows3 <> 1 Then
                    aReport = New CRPV_20200722
                    aReport.Subreports.Item("info").SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))
                    '    Else
                    '    aReport = New CRPV_1PO
                    'End If





                End If







                'crpv_3
                rows = 0
                SQlStrA = "SELECT COUNT(material_code) totmaterial FROM tbl_shipping_detail WHERE shipment_no = '" & v_pono & "'"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows = 1 Then
                    '1 BL = 1 PO (1 PO berisi 1 item material)
                    SQlStr_2 = "SELECT TPD.MATERIAL_CODE, TM.MATERIAL_NAME, cast(SUM(TSD.QUANTITY) as char) AS QTY_1, " & _
                               "TPD.UNIT_CODE, TU.UNIT_NAME FROM TBL_PO_DETAIL AS TPD, TBL_PO AS TP, TBL_SHIPPING_DETAIL AS TSD, " & _
                               "TBL_SHIPPING_DOC AS TSDOC, TBM_MATERIAL AS TM, TBM_UNIT AS TU " & _
                               "WHERE(TPD.PO_NO = TP.PO_NO And TSD.PO_NO = TP.PO_NO And TSD.PO_ITEM = TPD.PO_ITEM) " & _
                               "AND TSD.SHIPMENT_NO = TSDOC.SHIPMENT_NO AND TPD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                               "AND TPD.UNIT_CODE = TU.UNIT_CODE " & _
                               "AND TSDOC.shipment_no = '" & v_pono & "' AND TSDOC.ord_no = '" & v_num & "' AND TSDOC.FINDOC_TYPE = 'PV' " & _
                               "GROUP BY TPD.MATERIAL_CODE "
                Else
                    SQlStr_2 = "SELECT TMG.GROUP_CODE MATERIAL_CODE, TMG.GROUP_NAME MATERIAL_NAME, cast(SUM(TSD.QUANTITY) as char) AS QTY_1, " & _
                               "TPD.UNIT_CODE, TU.UNIT_NAME FROM TBL_PO_DETAIL AS TPD, TBL_PO AS TP, TBL_SHIPPING_DETAIL AS TSD, " & _
                               "TBL_SHIPPING_DOC AS TSDOC, TBM_MATERIAL AS TM, TBM_MATERIAL_GROUP AS TMG, TBM_UNIT AS TU " & _
                               "WHERE(TPD.PO_NO = TP.PO_NO And TSD.PO_NO = TP.PO_NO And TSD.PO_ITEM = TPD.PO_ITEM) " & _
                               "AND TSD.SHIPMENT_NO = TSDOC.SHIPMENT_NO AND TPD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                               "AND TM.GROUP_CODE=TMG.GROUP_CODE AND TPD.UNIT_CODE = TU.UNIT_CODE " & _
                               "AND TSDOC.shipment_no = '" & v_pono & "' AND TSDOC.ord_no = '" & v_num & "' AND TSDOC.FINDOC_TYPE = 'PV' " & _
                               "GROUP BY TMG.GROUP_CODE "
                End If

                SQlStr_2 = "Select *, FormatDec(QTY_1) AS QTY from (" & SQlStr_2 & ") t"
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))

                Dim test As FieldObject = CType(aReport.ReportDefinition.ReportObjects.Item("note1"), FieldObject)
                Dim suppl As String
                Dim note As New RichTextBox
                Dim baris, ukuranFont As Integer

                suppl = AmbilData("supplier_code", "tbl_shipping", "shipment_no=" & v_pono)
                note.Text = AmbilData("note", "tbm_supplier", "supplier_code='" & suppl & "'")
                baris = note.Lines.Count

                Select Case baris
                    Case 1, 2, 3, 4, 5, 6
                        ukuranFont = test.Font.Size
                    Case 7
                        ukuranFont = 8
                    Case Else
                        ukuranFont = 7
                End Select
                Dim ft As New Font(test.Font.Name, ukuranFont, test.Font.Style, test.Font.Unit)
                test.ApplyFont(ft)

                Dim TGL As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("TGL"), TextObject)
                'Dim NAME4 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("V_NAME4"), TextObject)
                'Dim NAME5 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("V_NAME5"), TextObject)
                Dim V_TGL_H As Date
                Dim v_curr_code As String
                Dim v_amount As Double
                Dim V_SPELL As String
                Dim txtTerbilang1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTerbilang1"), TextObject)
                Dim txtCreatedBy As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("NAME3"), TextObject)
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                v_amount = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtCreatedBy.Text = MyReader.GetString("NAME3")
                        v_amount = MyReader.GetString("INVOICE_AMOUNT")
                        v_curr_code = MyReader.GetString("CURRENCY_CODE")
                        V_TGL_H = MyReader.GetString("FINDOC_PRINTEDdt")
                    End While
                End If
                CloseMyReader(MyReader, UserData)






                v_curr_code = AmbilData("currency_name", "tbm_currency", "currency_code ='" & v_curr_code & "'")
                V_SPELL = v_curr_code & " : " & TerbilangInggris(v_amount)
                txtTerbilang1.Text = V_SPELL
                TGL.Text = Format(V_TGL_H, "MMMM dd, yyyy")

                'cek jumlah POnya
                rows = 0
                SQlStrA = "SELECT COUNT(distinct po_no) FROM tbl_shipping_Detail WHERE shipment_no = " & v_pono
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows > 1 Then
                    'PO
                    SQlStr_2 = "SELECT 'Note' as txtNote, CONCAT('PO No : ',t1.po_no) det_po, MAX(t2.currency_code) det_currency, SUM(invoice_amount-invoice_penalty) det_amount " & _
                               "FROM tbl_shipping_Detail t1, tbl_shipping t2, tbl_shipping_invoice t3 " & _
                               "WHERE t1.shipment_no = t2.shipment_no And (t1.shipment_no = t3.shipment_no And t1.po_no = t3.po_no AND t1.po_item = t3.ord_no) " & _
                               "AND t1.shipment_no = " & v_pono & " GROUP BY t1.po_no "

                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))


                End If

                'SQlStr_2 = "SELECT CONCAT('PO No : ',TRIM(t1.po_no),' QTY : ') fdet_po, SUM(ROUND(t1.quantity,2)) fquantity, MAX(t2.currency_code) fdet_currency, SUM(ROUND(invoice_amount-invoice_penalty,2)) fdet_amount " & _
                '            "FROM tbl_shipping_Detail t1, tbl_shipping t2, tbl_shipping_invoice t3 " & _
                '            "WHERE(t1.shipment_no = t2.shipment_no And (t1.shipment_no = t3.shipment_no And t1.po_no = t3.po_no And t1.po_item = t3.ord_no)) " & _
                '            "AND t1.shipment_no = " & v_pono & " " & _
                '            "GROUP BY t1.po_no "

                'aReport.Subreports.Item("info").SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))
                ''aReport.Subreports.Item("PO").SetDataSource(DBQueryDa

            Case "VGVV"
                aReport = New CRVG

                'crvg_3
                rows = 0
                SQlStrA = "SELECT COUNT(material_code) totmaterial FROM tbl_shipping_detail WHERE shipment_no = '" & v_pono & "'"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows = 1 Then
                    '1 BL = 1 PO (1 PO berisi 1 item material)
                    SQlStr_2 = "SELECT TPD.MATERIAL_CODE, TM.MATERIAL_NAME, cast(SUM(TSD.QUANTITY) as char) AS QTY_1, " & _
                               "TPD.UNIT_CODE, TU.UNIT_NAME FROM TBL_PO_DETAIL AS TPD, TBL_PO AS TP, TBL_SHIPPING_DETAIL AS TSD, " & _
                               "TBL_SHIPPING_DOC AS TSDOC, TBM_MATERIAL AS TM, TBM_UNIT AS TU " & _
                               "WHERE(TPD.PO_NO = TP.PO_NO And TSD.PO_NO = TP.PO_NO And TSD.PO_ITEM = TPD.PO_ITEM) " & _
                               "AND TSD.SHIPMENT_NO = TSDOC.SHIPMENT_NO AND TPD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                               "AND TPD.UNIT_CODE = TU.UNIT_CODE " & _
                               "AND TSDOC.shipment_no = '" & v_pono & "' AND TSDOC.ord_no = '" & v_num & "' AND TSDOC.FINDOC_TYPE = 'VG' " & _
                               "GROUP BY TPD.MATERIAL_CODE "
                Else
                    SQlStr_2 = "SELECT TMG.GROUP_CODE MATERIAL_CODE, TMG.GROUP_NAME MATERIAL_NAME, cast(SUM(TSD.QUANTITY) as char) AS QTY_1, " & _
                               "TPD.UNIT_CODE, TU.UNIT_NAME FROM TBL_PO_DETAIL AS TPD, TBL_PO AS TP, TBL_SHIPPING_DETAIL AS TSD, " & _
                               "TBL_SHIPPING_DOC AS TSDOC, TBM_MATERIAL AS TM, TBM_MATERIAL_GROUP AS TMG, TBM_UNIT AS TU " & _
                               "WHERE(TPD.PO_NO = TP.PO_NO And TSD.PO_NO = TP.PO_NO And TSD.PO_ITEM = TPD.PO_ITEM) " & _
                               "AND TSD.SHIPMENT_NO = TSDOC.SHIPMENT_NO AND TPD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                               "AND TM.GROUP_CODE=TMG.GROUP_CODE AND TPD.UNIT_CODE = TU.UNIT_CODE " & _
                               "AND TSDOC.shipment_no = '" & v_pono & "' AND TSDOC.ord_no = '" & v_num & "' AND TSDOC.FINDOC_TYPE = 'VG' " & _
                               "GROUP BY TMG.GROUP_CODE "
                End If

                SQlStr_2 = "Select *, FormatDec(QTY_1) AS QTY from (" & SQlStr_2 & ") t"
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))

                Dim suppl As String
                Dim note As New RichTextBox
                Dim baris, ukuranFont As Integer

                suppl = AmbilData("supplier_code", "tbl_shipping", "shipment_no=" & v_pono)

                Dim TGL As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("TGL"), TextObject)
                Dim V_TGL_H As Date
                Dim v_curr_code As String
                Dim v_amount As Double
                Dim V_SPELL As String
                Dim txtTerbilang1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTerbilang1"), TextObject)

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                v_amount = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_amount = MyReader.GetString("AMOUNT")
                        v_curr_code = MyReader.GetString("CURRENCY_CODE")
                        V_TGL_H = MyReader.GetString("FINDOC_PRINTEDdt")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                v_curr_code = AmbilData("currency_name", "tbm_currency", "currency_code ='" & v_curr_code & "'")
                'V_SPELL = v_curr_code & " : " & TerbilangInggris(v_amount)
                V_SPELL = v_curr_code & " : " & TerbilangAll("IND", v_amount)
                txtTerbilang1.Text = V_SPELL
                TGL.Text = Format(V_TGL_H, "MMMM dd, yyyy")
                'TGL.Text = V_TGL_H

                'cek jumlah POnya
                rows = 0
                SQlStrA = "SELECT COUNT(distinct po_no) FROM tbl_shipping_Detail WHERE shipment_no = " & v_pono
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows > 1 Then
                    'PO
                    SQlStr_2 = "SELECT 'Note' as vgNote, CONCAT('PO No : ',t1.po_no) vg_po, MAX(t2.currency_code) vg_curr, SUM(invoice_amount-invoice_penalty) vg_amount " & _
                               "FROM tbl_shipping_Detail t1, tbl_shipping t2, tbl_shipping_invoice t3 " & _
                               "WHERE t1.shipment_no = t2.shipment_no And (t1.shipment_no = t3.shipment_no And t1.po_no = t3.po_no AND t1.po_item = t3.ord_no) " & _
                               "AND t1.shipment_no = " & v_pono & " GROUP BY t1.po_no "

                    aReport.Subreports.Item("PO").SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))
                End If
            Case "SPPP"
                aReport = New CRSP_F01
                SQlStr_2 = "select distinct tsp.shipment_no, tsp.ord_no, tsd.po_no " & _
                "from tbl_sspcp as tsp " & _
                "inner join tbl_shipping as ts on tsp.shipment_no = ts.shipment_no " & _
                "inner join tbl_shipping_detail as tsd on tsp.shipment_no = tsd.shipment_no " & _
                "where tsp.shipment_no = '" & v_pono & "' AND TSP.ORD_NO = '" & v_num & "'"
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))


                Dim txtNPWP1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtNPWP1"), TextObject)
                Dim txtNPWP2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtNPWP2"), TextObject)
                Dim txtNPWP3 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtNPWP3"), TextObject)
                Dim txtPIBDT As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPIBDT"), TextObject)

                Dim txtBEA_MASUK As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtBEA_MASUK"), TextObject)
                Dim txtSPM As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtSPM"), TextObject)
                Dim txtDENDA_PABEAN As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtDENDA_PABEAN"), TextObject)
                Dim txtPEN_PABEAN_LAIN As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPEN_PABEAN_LAIN"), TextObject)
                Dim txtCUKAI_ETIL As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCUKAI_ETIL"), TextObject)
                Dim txtDENDA_CUKAI As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtDENDA_CUKAI"), TextObject)
                Dim txtPEN_CUKAI_LAIN As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPEN_CUKAI_LAIN"), TextObject)
                Dim txtPNBP As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPNBP"), TextObject)
                Dim txtVAT As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtVAT"), TextObject)
                Dim txtPPNBM As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPPNBM"), TextObject)
                Dim txtPPH22 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPPH22"), TextObject)
                Dim txttotal As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txttotal"), TextObject)
                Dim txtTerbilang As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTerbilang"), TextObject)
                Dim txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11 As String
                Dim v_tgl As Date
                Dim v_temp_npwp As String = ""
                Dim v_periode As String
                Dim v_curr_code As String
                Dim v_amount As Double
                Dim V_SPELL As String
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            v_temp_npwp = MyReader.GetString("NPWP")
                        Catch ex As Exception
                            v_temp_npwp = ""
                        End Try
                        Try
                            v_tgl = MyReader.GetString("PIB_DT")
                            txtPIBDT.Text = Format(v_tgl, "dd-MM-yyyy")
                        Catch ex As Exception
                        End Try
                        v_periode = MyReader.GetString("SSPCP_PERIOD")
                        txt1 = FormatNumber(MyReader.GetString("BEA_MASUK"), 0)
                        txt2 = FormatNumber(MyReader.GetString("SPM"), 0)
                        txt3 = FormatNumber(MyReader.GetString("DENDA_PABEAN"), 0)
                        txt4 = FormatNumber(MyReader.GetString("PEN_PABEAN_LAIN"), 0)
                        txt5 = FormatNumber(MyReader.GetString("CUKAI_ETIL"), 0)
                        txt6 = FormatNumber(MyReader.GetString("DENDA_CUKAI"), 0)
                        txt7 = FormatNumber(MyReader.GetString("PEN_CUKAI_LAIN"), 0)
                        txt8 = FormatNumber(MyReader.GetString("PNBP"), 0)
                        txt9 = FormatNumber(MyReader.GetString("VAT"), 0)
                        txt10 = FormatNumber(MyReader.GetString("PPNBM"), 0)
                        txt11 = FormatNumber(MyReader.GetString("PPH22"), 0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                txtNPWP1.Text = f_SPLITNPWP(v_temp_npwp, 5)
                txtNPWP2.Text = f_SPLITNPWP(v_temp_npwp, 3)
                txtNPWP3.Text = f_SPLITNPWP(v_temp_npwp, 3)
                'txtPIBDT.Text = Format(v_tgl, "dd-MM-yyyy")
                v_curr_code = "IDR"
                Dim bln01 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln01"), TextObject)
                Dim bln02 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln02"), TextObject)
                Dim bln03 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln03"), TextObject)
                Dim bln04 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln04"), TextObject)
                Dim bln05 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln05"), TextObject)
                Dim bln06 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln06"), TextObject)
                Dim bln07 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln07"), TextObject)
                Dim bln08 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln08"), TextObject)
                Dim bln09 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln09"), TextObject)
                Dim bln10 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln10"), TextObject)
                Dim bln11 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln11"), TextObject)
                Dim bln12 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("bln12"), TextObject)
                Dim txtThn As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtThn"), TextObject)

                Select Case Mid(v_periode, 5, 2)
                    Case "01" : bln01.Text = "XXX"
                    Case "02" : bln02.Text = "XXX"
                    Case "03" : bln03.Text = "XXX"
                    Case "04" : bln04.Text = "XXX"
                    Case "05" : bln05.Text = "XXX"
                    Case "06" : bln06.Text = "XXX"
                    Case "07" : bln07.Text = "XXX"
                    Case "08" : bln08.Text = "XXX"
                    Case "09" : bln09.Text = "XXX"
                    Case "10" : bln10.Text = "XXX"
                    Case "11" : bln11.Text = "XXX"
                    Case "12" : bln12.Text = "XXX"
                End Select

                txtThn.Text = Mid(v_periode, 1, 4)
                txtThn.Text = f_SPLITSpace(txtThn.Text, 8)
                v_amount = CDbl(txt1) + CDbl(txt2) + CDbl(txt3) + CDbl(txt4) + _
                              CDbl(txt5) + CDbl(txt6) + CDbl(txt7) + CDbl(txt8) + _
                              CDbl(txt9) + CDbl(txt10) + CDbl(txt11)
                txttotal.Text = CStr(FormatNumber(v_amount, 0))
                If txt1 = "0" Then txt1 = ""
                If txt2 = "0" Then txt2 = ""
                If txt3 = "0" Then txt3 = ""
                If txt4 = "0" Then txt4 = ""
                If txt5 = "0" Then txt5 = ""
                If txt6 = "0" Then txt6 = ""
                If txt7 = "0" Then txt7 = ""
                If txt8 = "0" Then txt8 = ""
                If txt9 = "0" Then txt9 = ""
                If txt10 = "0" Then txt10 = ""
                If txt11 = "0" Then txt11 = ""
                txtBEA_MASUK.Text = txt1
                txtSPM.Text = txt2
                txtDENDA_PABEAN.Text = txt3
                txtPEN_PABEAN_LAIN.Text = txt4
                txtCUKAI_ETIL.Text = txt5
                txtDENDA_CUKAI.Text = txt6
                txtPEN_CUKAI_LAIN.Text = txt7
                txtPNBP.Text = txt8
                txtVAT.Text = txt9
                txtPPNBM.Text = txt10
                txtPPH22.Text = txt11
                V_SPELL = TerbilangAll("ID", v_amount) & " ## "
                txtTerbilang.Text = UCase(V_SPELL)
            Case "CLLL"
                'aReport = New CRCL
                aReport = New CRCVL
                Dim v_city As String
                Dim v_printdt As Date
                Dim txtCityTgl As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCityTgl"), TextObject)
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            v_city = MyReader.GetString("cityPrint")
                        Catch ex As Exception
                            v_city = ""
                        End Try
                        Try
                            v_printdt = MyReader.GetString("findoc_printeddt")
                        Catch ex As Exception
                            v_printdt = ""
                        End Try
                    End While
                End If
                txtCityTgl.Text = v_city & ", " & Format(v_printdt, "dd MMMM yyyy")
                CloseMyReader(MyReader, UserData)

                rows = 0
                SQlStrA = "SELECT COUNT(distinct material_code) totmaterial FROM tbl_shipping_detail WHERE shipment_no = '" & v_pono & "'"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows = 1 Then

                    SQlStr_1 = "SELECT CONCAT(FormatDec(SUM(a.quantity)),' ',MAX(b.unit_code),' ',c.material_name) AS material FROM " & _
                                "tbl_shipping_Detail AS a, tbl_po_detail AS b, tbm_material AS c " & _
                                "WHERE(a.po_no = b.po_no And a.po_item = b.po_item And a.material_code = c.material_code) " & _
                                "AND a.shipment_no ='" & v_pono & "' " & _
                                "GROUP BY c.material_code "
                Else
                    SQlStr_1 = "SELECT GROUP_CONCAT(material SEPARATOR ', ') AS material FROM " & _
                               "(SELECT CONCAT(FormatDec(SUM(a.quantity)), ' ', MAX(b.unit_code), ' ',d.group_name) AS material, d.group_name " & _
                               "FROM tbl_shipping_Detail AS a, tbl_po_detail AS b, tbm_material AS c, tbm_material_group AS d " & _
                               "WHERE(a.po_no = b.po_no And a.po_item = b.po_item And a.material_code = c.material_code And c.group_code = d.group_code) " & _
                               "AND a.shipment_no = '" & v_pono & "' GROUP BY c.group_code) t1 "
                End If

                Dim txtMat As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Mat"), TextObject)
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtMat.Text = MyReader.GetString("Material")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr_1 = "SELECT GROUP_CONCAT(material SEPARATOR ', ') AS material FROM " & _
                           "(SELECT CONCAT(FormatDec(SUM(t1.pack_quantity)), ' ', MAX(t3.pack_name)) AS material, t3.pack_name " & _
                           "FROM tbl_shipping_detail AS t1,tbm_material AS t2,tbm_packing AS t3 " & _
                           "WHERE(t1.material_code = t2.material_code And t1.pack_code = t3.pack_code) " & _
                           "AND T1.SHIPMENT_NO = '" & v_pono & "' " & _
                           "GROUP BY t3.pack_name) t1 "

                Dim temp As String = ""
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            temp = MyReader.GetString("material")
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr_1 = "select cast(group_concat(trim(packing) separator ', ') as char(256)) as packing from " & _
                "(select concat(count(container_no),'X',unit_code) as packing " & _
                "from tbl_shipping_cont where SHIPMENT_NO = " & v_pono & " group by unit_code) as a"

                Dim MAT As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("MATERIAL"), TextObject)
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            MAT.Text = temp & "     IN " & MyReader.GetString("packing")
                        Catch ex As Exception
                            MAT.Text = temp
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr_3 = "Select t1.doc_seq as nomx, " & _
                "CONCAT(trim(t2.doc_name),' ',trim(t1.doc_no),' ',ifnull(date_format(t1.doc_dt,'%d-%m-%Y'),''),' ',trim(t1.doc_remark)) as document " & _
                "From tbl_shipping_doc_detail as t1, tbm_document as t2 " & _
                "Where t1.finddoc_type='CL' and t1.doc_code=t2.doc_code " & _
                "and t1.shipment_no='" & v_pono & "' and t1.ord_no = '" & v_num & "' " & _
                "Order by t1.finddoc_no"

                ErrMsg = "Gagal baca data CL"
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))
            Case "TTTT", "BPIB", "CADD"
                If v_type = "TTTT" Then
                    aReport = New CRTT
                ElseIf v_type = "CADD" Then
                    aReport = New CRCAD
                ElseIf v_type = "BPIB" Then
                    aReport = New CRBPIB
                End If
                Dim v_city As String
                Dim v_printdt As Date
                Dim v_openingdt As Date
                Dim txtCityTgl As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCityTgl"), TextObject)
                Dim txtOpeningDt As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtOpeningDt"), TextObject)
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            v_openingdt = MyReader.GetString("openingdt")
                        Catch ex As Exception
                            v_openingdt = ""
                        End Try
                        Try
                            v_city = MyReader.GetString("city_name")
                        Catch ex As Exception
                            v_city = ""
                        End Try
                        Try
                            v_printdt = MyReader.GetString("printeddt")
                        Catch ex As Exception
                            v_printdt = ""
                        End Try
                    End While
                End If
                txtCityTgl.Text = v_city & ", " & Format(v_printdt, "MMMM dd, yyyy")
                txtOpeningDt.Text = Format(v_openingdt, "dd-MMM-yyyy")
                CloseMyReader(MyReader, UserData)

                Dim txtVessel As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtVessel"), TextObject)
                Dim txtETD As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtETD"), TextObject)
                Dim txtETA As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtETA"), TextObject)
                Dim txtBL As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtBL"), TextObject)
                Dim V_ETD, V_ETD1, V_ETA, V_ETA1 As String
                Dim V_ETD2, V_ETA2 As Date
                Dim v_etd2n, v_eta2n As String
                SQlStr_1 = "select ts.vessel, ts.bl_no, tp.port_name loadport_name, tc.city_name loadcity_name, " & _
                "if(ts.est_delivery_dt is null,'',ts.est_delivery_dt) etd, tp2.port_name, tc2.city_name city_name, " & _
                "if((ts.notice_arrival_dt is null) or (ts.notice_arrival_dt=''), ts.est_arrival_dt, ts.notice_arrival_dt) eta " & _
                "from tbl_shipping ts, tbm_port tp, tbm_port tp2, tbm_city tc, tbm_city tc2 " & _
                "where(ts.loadport_code = tp.port_code And ts.port_code = tp2.port_code) " & _
                "and tp.city_code = tc.city_code and tp2.city_code = tc2.city_code " & _
                "and ts.shipment_no = '" & v_pono & "'"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                v_etd2n = ""
                v_eta2n = ""
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            txtVessel.Text = MyReader.GetString("VESSEL")
                        Catch ex As Exception
                            txtVessel.Text = ""
                        End Try
                        Try
                            V_ETD = MyReader.GetString("LOADPORT_NAME")
                        Catch ex As Exception
                            V_ETD = ""
                        End Try
                        Try
                            V_ETD1 = MyReader.GetString("LOADCITY_NAME")
                        Catch ex As Exception
                            V_ETD1 = ""
                        End Try
                        Try
                            V_ETD2 = MyReader.GetString("ETD")
                        Catch ex As Exception
                            v_etd2n = "Y"
                        End Try
                        Try
                            V_ETA = MyReader.GetString("PORT_NAME")
                        Catch ex As Exception
                            V_ETA = ""
                        End Try
                        Try
                            V_ETA1 = MyReader.GetString("CITY_NAME")
                        Catch ex As Exception
                            V_ETA1 = ""
                        End Try

                        Try
                            V_ETA2 = MyReader.GetString("ETA")
                        Catch ex As Exception
                            v_eta2n = "Y"
                        End Try
                        Try
                            txtBL.Text = MyReader.GetString("BL_NO")
                        Catch ex As Exception
                            txtBL.Text = ""
                        End Try
                    End While
                End If
                If v_etd2n = "Y" Then
                    txtETD.Text = V_ETD & ", " & V_ETD1
                Else
                    txtETD.Text = V_ETD & ", " & V_ETD1 & "   " & Format(V_ETD2, "dd-MMM-yy")
                End If
                If v_eta2n = "Y" Then
                    txtETA.Text = V_ETA & ", " & V_ETA1
                Else
                    txtETA.Text = V_ETA & ", " & V_ETA1 & "   " & Format(V_ETA2, "dd-MMM-yy")
                End If

                CloseMyReader(MyReader, UserData)
                Dim strPO As String = "GetPOorder(" & v_pono & "," & "trim(T2.PO_No))"
                Dim txtPO As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPO"), TextObject)
                SQlStr_2 = "SELECT GROUP_CONCAT(trim(" & strPO & ") separator ', ') AS VPO " & _
                "FROM tbl_shipping AS T1, (SELECT DISTINCT Shipment_no, PO_No FROM tbl_shipping_detail) AS T2 " & _
                "WHERE T1.Shipment_No=T2.Shipment_No and T1.shipment_no = '" & v_pono & "'  " & _
                "GROUP BY T1.Shipment_No"
                ErrMsg = "Gagal baca data PO."
                MyReader = DBQueryMyReader(SQlStr_2, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            txtPO.Text = MyReader.GetString("VPO")
                        Catch ex As Exception
                            txtPO.Text = ""
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                Dim txtCont As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCont"), TextObject)
                SQlStr_4 = "Select GROUP_CONCAT(CONCAT(ts.tot,' x ',ts.unit_name) separator ', ') as packing " & _
                "From (Select sum(1) tot, tu.unit_name From tbl_shipping_cont ts, tbm_unit tu " & _
                "Where ts.unit_code=tu.unit_code and ts.shipment_no= '" & v_pono & "' " & _
                "GROUP BY ts.unit_code) as ts"
                ErrMsg = "Gagal baca data Packing."
                MyReader = DBQueryMyReader(SQlStr_4, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            txtCont.Text = MyReader.GetString("packing")
                        Catch ex As Exception
                            txtCont.Text = ""
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If v_type = "BPIB" Then
                    Dim v_bea, v_vat, v_pph22, v_piud, v_total As Double
                    Dim txtTBM As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTBM"), TextObject)
                    Dim txtVAT As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtVAT"), TextObject)
                    Dim txtPPH22 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPPH22"), TextObject)
                    Dim txtPIUD As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPIUD"), TextObject)
                    Dim txtTotal As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTotal"), TextObject)
                    Dim txtKursPajak As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("kurs1"), TextObject)

                    'CEK KURS PAJAK
                    Dim kurs, kurs1, kurs2 As Decimal
                    Dim curr, openingdt As String
                    Try
                        curr = AmbilData("currency_code", "tbl_shipping", "shipment_no=" & v_pono)
                    Catch ex As Exception
                        curr = ""
                    End Try
                    'kurs lama di tbl_shipping
                    Try
                        kurs1 = AmbilData("kurs_pajak", "tbl_shipping", "shipment_no=" & v_pono)
                    Catch ex As Exception
                        kurs1 = 0
                    End Try
                    Try
                        openingdt = AmbilData("openingdt", "tbl_budgets", "shipment_no=" & v_pono & " and ord_no=" & v_num & " and type_code='BP'")
                    Catch ex As Exception
                        openingdt = ""
                    End Try
                    'kurs baru di tbm_kurs
                    Try
                        kurs2 = AmbilData("kurs_pajak", "tbm_kurs", "currency_code='" & curr & "' and effective_date='" & Format(CDate(openingdt), "yyyy-MM-dd") & "'")
                    Catch ex As Exception
                        kurs2 = 0
                    End Try
                    kurs = kurs1
                    Dim KursComp As Decimal
                    If kurs1 <> kurs2 And kurs2 > 0 Then
                        If (MsgBox("Changes rate " & FormatNumber(kurs1, 2, , , TriState.True) & " to " & FormatNumber(kurs2, 2, , , TriState.True) & "?", MsgBoxStyle.YesNo, "Confirmation")) = vbYes Then
                            kurs = kurs2
                        Else
                            kurs = kurs1
                        End If
                        KursComp = kurs / kurs1
                    Else
                        KursComp = 1
                    End If

                    SQlStr_5 = "select bea_masuk, vat, pph21 as pph22, piud " & _
                               "from tbl_shipping  " & _
                               "where shipment_no = '" & v_pono & "'"
                    ErrMsg = "Gagal baca data"
                    txtTBM.Text = 0
                    txtVAT.Text = 0
                    txtPPH22.Text = 0
                    txtPIUD.Text = 0

                    txtKursPajak.Text = FormatNumber(kurs, 2, , , TriState.True)

                    MyReader = DBQueryMyReader(SQlStr_5, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            Try
                                txtTBM.Text = FormatNumber(MyReader.GetString("bea_masuk"), 0)
                            Catch ex As Exception
                                txtTBM.Text = 0
                            End Try
                            Try
                                txtVAT.Text = FormatNumber(MyReader.GetString("vat"), 0)
                            Catch ex As Exception
                                txtVAT.Text = 0
                            End Try
                            Try
                                txtPPH22.Text = FormatNumber(MyReader.GetString("pph22"), 0)
                            Catch ex As Exception
                                txtPPH22.Text = 0
                            End Try
                            Try
                                txtPIUD.Text = FormatNumber(MyReader.GetString("piud"), 0)
                            Catch ex As Exception
                                txtPIUD.Text = 0
                            End Try
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                    'Cek status rounded
                    Dim stat, plant As String
                    plant = AmbilData("plant_code", "tbl_shipping", "shipment_no=" & v_pono)
                    stat = AmbilData("tax_rounded", "tbm_plant", "plant_code=" & plant)

                    Dim num1, num2, num3, num4 As Decimal
                    num1 = CDec(txtTBM.Text) * KursComp
                    num2 = CDec(txtVAT.Text) * KursComp
                    num3 = CDec(txtPPH22.Text) * KursComp
                    num4 = CDec(txtPIUD.Text)  'PIUD tidak perlu dikali kurs!!!

                    'angka yg sudah dibulatkan HANYA untuk print saja, di tabel tidak usah dibulatkan
                    'function GetNum membulatkan 3 digit terakhir (ribuan)
                    Dim str1, str2, str3, str4 As String
                    str1 = GetNum(FormatNumber(num1, 0), stat)
                    str2 = GetNum(FormatNumber(num2, 0), stat)
                    str3 = GetNum(FormatNumber(num3, 0), stat)
                    str4 = GetNum(FormatNumber(num4, 0), stat)

                    txtTBM.Text = str1
                    txtVAT.Text = str2
                    txtPPH22.Text = str3
                    txtPIUD.Text = str4
                    txtKursPajak.Text = FormatNumber(txtKursPajak.Text, 2)

                    'Ada perubahan kurs -> update ke tbl_shipping
                    'PIUD tidak perlu di update ke tabel karena PIUD tidak dikali kurs
                    'Function GetNum2 mengubah format angka local PC ke format angka di MySQL server
                    If KursComp <> 1 Then Update_Tbl_Shipping(v_pono, GetNum2(num1), GetNum2(num2), GetNum2(num3), GetNum2(txtKursPajak.Text))

                    v_total = CDbl(txtTBM.Text) + CDbl(txtVAT.Text) + CDbl(txtPPH22.Text) + CDbl(txtPIUD.Text)
                    txtTotal.Text = FormatNumber(v_total, 0)
                ElseIf v_type = "TTTT" Then
                    SQlStr_9 = "select distinct(tms.note) " & _
                             "from tbl_budgets as tr " & _
                             "inner join tbl_shipping as ts on tr.shipment_no = ts.shipment_no " & _
                             "left join tbm_supplier as tms on ts.supplier_code = tms.supplier_code " & _
                             "where tr.shipment_no = '" & v_pono & "' and tr.ord_no = '" & v_num & "' and tr.type_code = 'TT'"

                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_9, MyConn, "", ErrMsg, UserData))
                End If
            Case "VPPP"
                aReport = New CRVP
                Dim v_printdt As Date
                Dim v_curr_code As String
                Dim v_amount As Double
                Dim V_SPELL As String
                Dim txtTerbilang1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTerbilang1"), TextObject)
                Dim txtTGL As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTGL"), TextObject)

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                v_amount = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_amount = MyReader.GetString("findoc_valamt")
                        v_curr_code = MyReader.GetString("findoc_valcur")
                        v_printdt = MyReader.GetString("findoc_printeddt")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                v_curr_code = AmbilData("currency_name", "tbm_currency", "currency_code ='" & v_curr_code & "'")
                If v_curr_code = "IDR" Then
                    V_SPELL = v_curr_code & " : " & TerbilangAll("ID", v_amount)
                Else
                    V_SPELL = v_curr_code & " : " & TerbilangAll("EN", v_amount)
                End If
                txtTerbilang1.Text = V_SPELL
                txtTGL.Text = Format(v_printdt, "dd-MMM-yy")

            Case "BPJU"
                SQLStr_SPR = "SELECT COUNT(*) adarec, val1 ada FROM tbm_addproc WHERE key1 = 'BPJUM'  "
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQLStr_SPR, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_usedreport = MyReader.GetString("ada")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If v_usedreport = 0 Then
                    aReport = New CRBPJUM
                Else
                    aReport = New CRBPJUMR
                End If

                'pt(1)
                SQlStr_1 = "select TRIM(REPLACE(c.company_name,'PT.','')) company_name " & _
                "from tbl_Shipping as a " & _
                "inner join tbm_plant as b on a.plant_Code=b.plant_code " & _
                "inner join tbm_company as c on b.company_code=c.company_code " & _
                "where shipment_no = " & v_pono
                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'po(0)
                SQlStr_1 = "select concat('PO No.', cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(t1.po_no))) separator ', ') as char),' ',t0.vessel,' ', t0.total_container) as po_no, " & _
                "t2.findoc_note AS description " & _
                "from tbl_shipping t0, tbl_shipping_Detail t1, tbl_shipping_doc t2 " & _
                "where t0.shipment_no=t1.shipment_no and t1.shipment_no = t2.shipment_no and t2.findoc_type='PP' and t1.shipment_no = " & v_pono

                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'user(2)
                SQlStr_1 = "select b.name " & _
                "from tbl_shipping_doc as a " & _
                "inner join tbm_users as b on a.findoc_appby=b.user_Ct " & _
                "where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='PP'"
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'terbilang
                Dim V_SPELL As String
                Dim v_amount As Double
                Dim txtTerbilang As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Terbilang"), TextObject)
                Dim jumlah As String

                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                v_amount = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_amount = MyReader.GetString("findoc_valamt")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                V_SPELL = " ## " & TerbilangAll("ID", v_amount) & " ## "
                txtTerbilang.Text = UCase(V_SPELL)
            Case "BPUM"
                aReport = New CRBPUM

                'pt
                SQlStr_1 = "select TRIM(REPLACE(c.company_name,'PT.','')) company_name " & _
                "from tbl_Shipping as a " & _
                "inner join tbm_plant as b on a.plant_Code=b.plant_code " & _
                "inner join tbm_company as c on b.company_code=c.company_code " & _
                "where shipment_no = " & v_pono
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'po
                'SQlStr_1 = "select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(po_no))) separator ', ') as char) as po_no " & _
                '"from tbl_shipping_Detail where shipment_no = " & v_pono

                SQlStr_1 = "SELECT CONCAT('PO No.', CAST(GROUP_CONCAT(DISTINCT(getpoorder(" & v_pono & ",TRIM(t1.po_no))) SEPARATOR ', ') AS CHAR),' ',t0.vessel,' ', t0.total_container) AS po_no, " & _
                "t2.findoc_note AS description " & _
                "FROM tbl_shipping t0, tbl_shipping_Detail t1, tbl_shipping_doc t2 " & _
                "WHERE t1.shipment_no=t0.shipment_no AND t1.shipment_no = t2.shipment_no AND t2.shipment_no=" & v_pono & " AND t2.ord_no=" & v_num & " AND t2.findoc_type='DP' "
                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'user
                SQlStr_1 = "select b.name " & _
                "from tbl_shipping_doc as a " & _
                "inner join tbm_users as b on a.findoc_createdby=b.user_Ct " & _
                "where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='DP'"

                'user app
                SQlStr_1 = "select b.name " & _
                "from tbl_shipping_doc as a " & _
                "inner join tbm_users as b on a.FINDOC_APPBY=b.user_Ct " & _
                "where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='DP'"
                aReport.Subreports.Item(3).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
                aReport.Subreports.Item(4).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'bank
                'SQlStr_1 = "select concat(if(b.bank_name is null,'',b.bank_name)," & _
                '"if(b.bank_branch is null,'',concat(' - ',trim(b.bank_branch)))," & _
                '"if(b.account_no is null,'',concat(' / ',trim(b.account_no)))," & _
                '"if(b.account_name is null,'',concat(' / ',trim(b.account_name)))) as bank " & _
                '"from tbl_shipping_doc  as a " & _
                '"inner join tbm_expedition as b on a.findoc_to=b.company_code " & _
                '"where a.shipment_no=" & v_pono & " and a.findoc_type='DP' and a.ord_no=" & v_num

                SQlStr_1 = "SELECT REPLACE(findoc_groupto,';',' / ') bank from tbl_shipping_doc  as a " & _
                "where a.shipment_no=" & v_pono & " and a.findoc_type='DP' and a.ord_no=" & v_num
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'terbilang
                Dim V_SPELL As String
                Dim v_amount As Double
                Dim txtTerbilang As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Terbilang"), TextObject)
                Dim jumlah As String

                Dim ket As String
                SQlStr_1 = _
                "select findoc_valamt as jumlah,findoc_note " & _
                "from tbl_shipping_doc where shipment_no=" & v_pono & " and ord_no=" & v_num & " and findoc_type='DP'"
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                v_amount = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_amount = MyReader.GetString("jumlah")
                        ket = MyReader.GetString("findoc_note")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                V_SPELL = " ## " & TerbilangAll("ID", v_amount) & " ## "
                txtTerbilang.Text = UCase(V_SPELL)
            Case "VPLL"
                aReport = New CRVPLamp
                Dim txtVessel As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtVessel"), TextObject)
                Dim txtMengetahui As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtMengetahui"), TextObject)
                SQLStr_A = "select ts.vessel, tsdoc.findoc_appby, tu.name " & _
                "from tbl_shipping_doc as tsdoc " & _
                "inner join tbl_shipping as ts on tsdoc.shipment_no = ts.shipment_no " & _
                "inner join tbm_users as tu on tsdoc.findoc_appby = tu.user_ct " & _
                "where tsdoc.shipment_no = '" & v_pono & "' and tsdoc.ord_no = '" & v_num & "' and tsdoc.findoc_type = 'VP' "
                ErrMsg = "Gagal baca data header."
                MyReader = DBQueryMyReader(SQLStr_A, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtVessel.Text = MyReader.GetString("vessel")
                        txtMengetahui.Text = MyReader.GetString("name")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                'PONO - Lamp1                
                'SQlStr_1 = "SELECT GROUP_CONCAT(trim(Tdet.PO_No) separator ', ') AS PONO " & _
                SQlStr_1 = "select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(Tdet.PO_No))) separator ', ') as char) as pono " & _
                "FROM tbl_shipping AS Ts, (SELECT DISTINCT Shipment_no, PO_No FROM tbl_shipping_detail) AS Tdet, " & _
                "tbl_shipping_doc as tsdoc " & _
                "WHERE(Ts.Shipment_No = Tdet.Shipment_No And ts.shipment_no = tsdoc.shipment_no) " & _
                "and tsdoc.findoc_type = 'VP' and tsdoc.shipment_no = '" & v_pono & "' and tsdoc.ord_no = '" & v_num & "' " & _
                "GROUP BY Ts.Shipment_No"
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'NAMABARANG - Lamp2
                SQlStr_2 = "SELECT GROUP_CONCAT(Tdet.material_name separator ', ') AS nama_barang " & _
                "FROM tbl_shipping AS Ts, " & _
                "(SELECT tsd.Shipment_no, tm.material_name FROM tbl_shipping_detail tsd, tbm_material tm " & _
                "Where tsd.material_code=tm.material_code) AS Tdet, " & _
                "tbl_shipping_doc as tsdoc " & _
                "WHERE(Ts.Shipment_No = Tdet.Shipment_No And ts.shipment_no = tsdoc.shipment_no) " & _
                "and tsdoc.findoc_type = 'VP' and tsdoc.shipment_no = '" & v_pono & "' and tsdoc.ord_no = '" & v_num & "' " & _
                "GROUP BY Ts.Shipment_No"
                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))

                'JUMLAH - LAMP3
                SQlStr_3 = "select trim(GROUP_CONCAT(tsd.quantity, ' ', tu.unit_name separator ', ')) as jumlah " & _
                "from tbl_shipping_doc as tsdoc " & _
                "inner join tbl_shipping_detail as tsd on tsdoc.shipment_no = tsd.shipment_no " & _
                "inner join tbl_po_detail as tpd on tsd.po_no = tpd.po_no and tsd.po_item = tpd.po_item " & _
                "left join tbm_unit as tu on tpd.unit_code = tu.unit_code " & _
                "where tsdoc.findoc_type = 'VP' and tsdoc.shipment_no = '" & v_pono & "' and tsdoc.ord_no = '" & v_num & "' " & _
                "group by tsdoc.shipment_no"
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))

            Case "CCCC"
                aReport = New CRCC
                Dim sql_opt As String
                Dim txtJudul As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtJudul"), TextObject)



                Dim v_eta, v_clr, v_docdt, v_printedon As Date
                Dim v_total As String
                Dim txtFTD As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtFTD"), TextObject)
                Dim txtETA As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtETA"), TextObject)
                Dim txtCLR As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCLR"), TextObject)
                Dim txtTotal As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTotal"), TextObject)
                Dim txtDocDT As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtDocDT"), TextObject)
                Dim txtPrintedOn As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPrintedOn"), TextObject)
                Dim txtOpeningDt As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtOpeningDt"), TextObject)
                Dim txtPlant As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPlant"), TextObject)
                Dim txtPort As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPort"), TextObject)
                Dim txtVessel As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtVessel"), TextObject)
                Dim txtKurs As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtKurs"), TextObject)
                Dim txtMaterial As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtMaterial"), TextObject)
                Dim txtCont As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCont"), TextObject)
                Dim txtTextCont As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTextCont"), TextObject)
                Dim txtQty As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtQty"), TextObject)
                Dim txtUnit As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtUnit"), TextObject)
                Dim txtRemark As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtRemark"), TextObject)

                SQlStr_1 = "SELECT t1.*, m3.kurs_pajak  FROM " & _
                           "(SELECT DATE_FORMAT(td.FINDOC_FINAPPDT,'%d-%b-%y') docdt, DATE_FORMAT(ts.NOTICE_ARRIVAL_DT,'%d-%b-%y') eta, ts.free_time ftd, " & _
                           "IF(ts.bapb_dt IS NULL OR ts.bapb_dt='', IF(ts.EST_BAPB_DT is null OR ts.EST_BAPB_DT='','',DATE_FORMAT(ts.EST_BAPB_DT,'%d-%b-%y')), DATE_FORMAT(ts.BAPB_DT,'%d-%b-%y')) clr, IF(td.findoc_appdt IS NULL, '', DATE_FORMAT(td.findoc_appdt,'%d-%b-%y')) findoc_printeddt, DATE_FORMAT(td.findoc_printeddt,'%d-%b-%y') OpeningDt, td.findoc_printeddt kursdt, " & _
                           "td.findoc_no plant_code, m1.plant_name, td.findoc_reff group_code, m3.group_name material, " & _
                           "m2.port_name, ts.vessel, ts.kurs_pajak kurspajak_1, td.findoc_valsize, td.findoc_valunit, td.findoc_note " & _
                           "FROM tbl_shipping_doc td, tbl_shipping ts, " & _
                           "tbm_plant m1, tbm_port m2, tbm_material_group m3 " & _
                           "where(ts.shipment_no = td.shipment_no) " & _
                           "and td.ORD_NO='" & CS_No & "'  " & _
                           "AND td.findoc_reff=m3.group_code AND td.findoc_no=m1.plant_code AND ts.port_code=m2.port_code " & _
                           "AND td.shipment_no='" & v_pono & "' AND td.ord_no = '" & v_num & "' AND td.findoc_STATUS <> 'Rejected') t1 " & _
                           "LEFT JOIN tbm_kurs m3 ON m3.currency_code='USD' AND m3.effective_date=t1.kursdt "

                SQlStr_1 = "SELECT t1.*, m3.kurs_pajak  FROM " & _
                          "(SELECT DATE_FORMAT(td.FINDOC_FINAPPDT,'%d-%b-%y') docdt, DATE_FORMAT(ts.NOTICE_ARRIVAL_DT,'%d-%b-%y') eta, ts.free_time ftd, " & _
                          "IF(ts.bapb_dt IS NULL OR ts.bapb_dt='', IF(ts.EST_BAPB_DT is null OR ts.EST_BAPB_DT='','',DATE_FORMAT(ts.EST_BAPB_DT,'%d-%b-%y')), DATE_FORMAT(ts.BAPB_DT,'%d-%b-%y')) clr, IF(td.findoc_appdt IS NULL, '', DATE_FORMAT(td.findoc_appdt,'%d-%b-%y')) findoc_printeddt, DATE_FORMAT(td.findoc_printeddt,'%d-%b-%y') OpeningDt, td.findoc_printeddt kursdt, " & _
                          "td.findoc_no plant_code, m1.plant_name, td.findoc_reff group_code, m3.group_name material, " & _
                          "m2.port_name, ts.vessel, ts.kurs_pajak kurspajak_1, td.findoc_valsize, td.findoc_valunit, td.findoc_note " & _
                          "FROM tbl_shipping_doc td, tbl_shipping ts, " & _
                          "tbm_plant m1, tbm_port m2, tbm_material_group m3 " & _
                          "where(ts.shipment_no = td.shipment_no) " & _
                          "AND td.findoc_reff=m3.group_code AND td.findoc_no=m1.plant_code AND ts.port_code=m2.port_code " & _
                          "AND td.shipment_no='" & v_pono & "' AND td.ord_no = '" & v_num & "' AND td.findoc_STATUS <> 'Rejected') t1 " & _
                          "LEFT JOIN tbm_kurs m3 ON m3.currency_code='USD' AND m3.effective_date=t1.kursdt "

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtFTD.Text = MyReader.GetString("ftd")
                        txtETA.Text = MyReader.GetString("eta")
                        txtCLR.Text = MyReader.GetString("clr")
                        txtDocDT.Text = MyReader.GetString("docdt")
                        txtPrintedOn.Text = MyReader.GetString("findoc_printeddt")
                        txtOpeningDt.Text = MyReader.GetString("OpeningDt")
                        txtPlant.Text = MyReader.GetString("plant_name")
                        txtPort.Text = MyReader.GetString("port_name")
                        txtVessel.Text = MyReader.GetString("vessel")
                        txtMaterial.Text = MyReader.GetString("material")
                        txtKurs.Text = CStr(FormatNumber(MyReader.GetString("kurs_pajak"), 2))

                        If Not IsDBNull(MyReader.GetString("findoc_valsize")) Then
                            If MyReader.GetString("findoc_valunit") <> "" Then
                                txtTextCont.Text = "CONTAINER"
                                txtCont.Text = MyReader.GetString("findoc_valsize") & " x " & MyReader.GetString("findoc_valunit")
                            Else
                                txtTextCont.Text = "CURAH"
                            End If
                        End If
                        txtRemark.Text = MyReader.GetString("findoc_note")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If v_type_report = "01" Then
                    txtJudul.Text = "Clearance Cost "
                End If
                If v_type_report = "02" Then
                    sql_opt = "  AND   t4.subgroup_code NOT IN('00101','00002','20003') "
                    txtJudul.Text = "Clearance Cost without Estimated Other Cost"
                End If
                If v_type_report = "03" Then
                    sql_opt = "  AND   t4.subgroup_code IN('00101','00002','20003') "
                    txtJudul.Text = "Clearance Cost only Estimated Other Cost"
                End If

                SQlStr_2 = "Select sum(t1.cost_amount+t1.cost_vat) AS TOTAL " & _
                "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                "and t2.findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                "and t2.ORD_NO='" & CS_No & "'  " & _
                "and t2.shipment_no='" & v_pono & "' and t2.ord_no = '" & v_num & "'" & sql_opt

                SQlStr_2 = "Select sum(t1.cost_amount+t1.cost_vat) AS TOTAL " & _
                "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                "and t2.findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                "and t2.shipment_no='" & v_pono & "' and t2.ord_no = '" & v_num & "'" & sql_opt

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_2, MyConn, ErrMsg, UserData)
                v_total = 0
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_total = MyReader.GetString("TOTAL")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                txtTotal.Text = FormatNumber(v_total, 2)
                SQlStr_3 = "Select t4.subgroup_name, sum(t1.cost_amount+t1.cost_vat) amount " & _
                   "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                   "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                   "and t2.ORD_NO='" & CS_No & "'  " & _
                   "and findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                   "and t1.shipment_no = '" & v_pono & "' AND t1.ship_ord_no=" & v_num & sql_opt & " group by t4.subgroup_code order by t4.subgroup_name"

                SQlStr_3 = "Select t4.subgroup_name, sum(t1.cost_amount+t1.cost_vat) amount " & _
                   "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                   "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                   "and findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                   "and t1.shipment_no = '" & v_pono & "' AND t1.ship_ord_no=" & v_num & sql_opt & " group by t4.subgroup_code order by t4.subgroup_name"
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))


                'SQlStr_3 = "Select t4.subgroup_name, sum(t1.cost_amount+t1.cost_vat) amount " & _
                '"From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                '"Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                '"and findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                '"and t1.shipment_no = '" & v_pono & "' AND t1.ship_ord_no=" & v_num & "  group by t4.subgroup_code order by t4.subgroup_name"
                'aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))

                SQlStr_5 = "SELECT SUM(t1.quantity) quantity, MAX(t3.unit_name) unit_name " & _
                "FROM tbl_shipping_detail t1, tbl_po_detail t2, tbm_unit t3 " & _
                "WHERE(t1.po_no = t2.po_no And t1.po_item = t2.po_item And t2.unit_code = t3.unit_code) " & _
                "AND t1.shipment_no = '" & v_pono & "' "

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_5, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtQty.Text = FormatNumber(MyReader.GetString("quantity"), 5)
                        txtUnit.Text = MyReader.GetString("unit_name")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
            Case "CSCS"
                Dim sql_opt1, sql_opt2, sql_opt3 As String
                Dim v_amount As Double
                Dim amt_estimated As Decimal = 0
                sql_opt1 = "" : sql_opt2 = "" : sql_opt3 = ""

                If v_type_report = "03" Then
                    sql_opt3 = "  AND   t4.subgroup_code IN('00101','00002','20003') "
                End If
                If v_type_report = "02" Then
                    sql_opt3 = "  AND   t4.subgroup_code not IN('00101','00002','20003') "
                End If
                'SQlStr_11 = "Select t4.subgroup_name, sum(t1.cost_amount+t1.cost_vat) amount " & _
                '            "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                '            "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                '            "and findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                '            "and t1.shipment_no = '" & v_pono & "' AND t1.ship_ord_no=" & v_num & sql_opt3 & " group by t4.subgroup_code order by t4.subgroup_name"

                SQlStr_11 = "select COALESCE(a1.TOTAL,0) as TOTAL from (Select sum(t1.cost_amount+t1.cost_vat) AS TOTAL " & _
                "From tbl_cost t1, tbl_shipping_doc t2, tbm_costcategory t3, tbm_costcategory_subgroup t4 " & _
                "Where(t1.shipment_no = t2.shipment_no And t1.SHIP_ord_no = t2.ord_no) " & _
                "and t2.findoc_type='CC' and t1.type_code='CC' and t1.cost_code=t3.costcat_code and t3.subgroup_code=t4.subgroup_code " & _
                "and t2.shipment_no='" & v_pono & "'  " & sql_opt3 & " AND  t2.ORD_NO='" & CS_No & "') a1"

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_11, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        amt_estimated = amt_estimated + Math.Round(Val(MyReader.GetString(0)), 2)
                    End While
                End If
                CloseMyReader(MyReader, UserData)



                If v_type_report = "03" Then
                    aReport = New CRCS_01
                    '---------------------------------------03----------------------------------------------
                    Dim txt_Header As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Header"), TextObject)
                    Dim txt_amt_cost As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost"), TextObject)
                    Dim txt_amt_cost_round As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost_round"), TextObject)
                    Dim txt_amt_cost_kg As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost_kg"), TextObject)
                    Dim txt_amt_cost2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost2"), TextObject)
                    Dim txt_amt_cost_round2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost_round2"), TextObject)
                    txt_Header.Text = "R/M COST SLIP only ESTIMATED OTHER COST"
                    amt_efective = 0
                    amt_PO = 0
                    sql_opt1 = " AND t1.cost_code IN('20011','20012') "
                    sql_opt2 = " AND m2.subgroup_code IN('20002','20003')  "
                    SQlStr_3 = "SELECT t1.*, (t1.cost_amount / (t1.quantity * (t1.findoc_valprc/100))/m1.rate) costkg, " & _
                            "((t1.cost_amount / (t1.quantity * (t1.findoc_valprc/100))/m1.rate)/t10.findoc_valamt) costpersen, " & _
                            "IF(t1.currency_code='' or t1.cost_unit=0,'',CONCAT(t1.currency_code,'  ',cast(t1.cost_unit as char), ' @ ', IF(m2.effective_kurs IS NULL,'0', cast(format(m2.effective_kurs,2) as char)))) desc_cost FROM " & _
                            "(SELECT t1.cost_code," & _
                             "IF (t1.cost_code='20011'," & amt_estimated & ",t1.cost_amount)cost_amount," & _
                             "IF (t1.cost_code='20011','Demurrage/Penumpukan',t1.cost_description)cost_description," & _
                             "if(t1.currency_code='IDR','',t1.currency_code) currency_code, t1.cost_unit, t1.cost_vat, t2.findoc_valprc, t3.quantity, t4.unit_code, t5.EST_DELIVERY_DT " & _
                            "FROM tbl_cost t1, tbl_shipping_doc t2, tbl_shipping_detail t3, tbl_po_detail t4, tbl_shipping t5 " & _
                            ",tbm_costcategory  tt1, tbm_costcategory_subgroup tt2 " & _
                            "WHERE   (t1.cost_code = tt1.costcat_code AND tt1.subgroup_code = tt2.subgroup_code) AND  t2.ORD_NO='" & CS_No & "' " & _
                             "AND (t1.shipment_no = t2.shipment_no And t1.type_code = t2.findoc_type And t1.ship_ord_no = t2.ord_no) " & _
                            "AND t3.shipment_no=t2.shipment_no AND t3.po_no=t2.findoc_no AND t3.po_item=t2.findoc_reff " & _
                            "AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item AND t1.shipment_no=t5.shipment_no " & _
                            "AND t1.cost_code LIKE '200%' AND t1.shipment_no='" & v_pono & "' AND t1.type_code='CS' AND t1.ship_ord_no='" & v_num & "' " & sql_opt1 & _
                            " ORDER BY t1.cost_ord_no) t1 " & _
                            "LEFT JOIN tbm_unit_equivalent m1 ON m1.unit_code=t1.unit_code AND m1.unit_code_to='KGS' " & _
                            "LEFT JOIN tbm_kurs m2 ON m2.currency_code=t1.currency_code AND m2.effective_date=t1.EST_DELIVERY_DT " & _
                            "LEFT JOIN tbl_shipping_doc t10 ON t10.findoc_type='CS' AND t10.shipment_no='" & v_pono & "' AND t10.ord_no='" & v_num & "' "
                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))
                    SQlStr_1 = SQlStr_3
                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            amt_efective = amt_efective + Math.Round(Val(MyReader.GetString(1)), 2)
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)
                    txt_amt_cost_kg.Text = FormatNumber(amt_efective, 2)
                    SQlStr_3 = "SELECT t1.*, m3.rate, (t1.cost_amount / (t1.quantity * (t1.findoc_valprc/100))/m3.rate) cost FROM " & _
                           "(SELECT m1.subgroup_code,  " & _
                           "IF(m2.subgroup_code='20002','Demurrage/Penumpukan',upper(m2.subgroup_name))subgroup_name," & _
                            "IF(m2.subgroup_code='20002'," & amt_estimated & ", SUM(t1.cost_amount))cost_amount," & _
                            "MAX(t1.currency_code) currency_code, SUM(t1.cost_unit) cost_unit, SUM(t1.cost_vat) cost_vat, " & _
                           "t2.findoc_valprc, t3.quantity, t4.unit_code, t4.price " & _
                           "FROM tbl_cost t1, tbm_costcategory m1, tbm_costcategory_subgroup m2, " & _
                           "tbl_shipping_doc t2, tbl_shipping_detail t3, tbl_po_detail t4 " & _
                           "WHERE(t1.cost_code = m1.costcat_code And m1.subgroup_code = m2.subgroup_code)   AND  t2.ORD_NO='" & CS_No & "' " & _
                            sql_opt2 & _
                           "AND t1.shipment_no=t2.shipment_no AND t1.type_code=t2.findoc_type AND t1.ship_ord_no=t2.ord_no " & _
                           "AND t2.shipment_no=t3.shipment_no AND t2.findoc_no=t3.po_no AND t2.findoc_reff=t3.po_item " & _
                           "AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item " & _
                           "AND t1.cost_code LIKE '200%' AND t1.shipment_no='" & v_pono & "' AND t1.type_code='CS' AND t1.ship_ord_no='" & v_num & "' GROUP BY m1.subgroup_code) t1 " & _
                           "LEFT JOIN tbm_unit_equivalent m3 ON m3.unit_code=t1.unit_code AND unit_code_to='KGS'"
                    aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))
                    SQlStr_1 = SQlStr_3
                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            amt_PO = Math.Round(Val(MyReader.GetString(11)), 2) + Math.Round(amt_PO, 2)
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                    txt_amt_cost.Text = FormatNumber(amt_PO, 2)
                    txt_amt_cost_round.Text = FormatNumber(Math.Round(Math.Ceiling(amt_PO), 0), 0)
                    txt_amt_cost2.Text = FormatNumber(amt_PO, 2)
                    txt_amt_cost_round2.Text = FormatNumber(Math.Round(Math.Ceiling(amt_PO), 0), 0)
                    '---------------------------------------End 03----------------------------------------------
                Else
                    aReport = New CRCS
                    '-----------------------------------------02/01------------------------------------------------
                    Dim txtCommision As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtCommision"), TextObject)
                    Dim txtPIB As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtPIB"), TextObject)
                    Dim txtDiscrepancy As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtDiscrepancy"), TextObject)
                    Dim txtTotalBank As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtTotalBank"), TextObject)
                    Dim txt_amt_cost2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost2"), TextObject)
                    Dim txt_amt_cost_round2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost_round2"), TextObject)
                    Dim txt_amt_cost_kg As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost_kg"), TextObject)
                    Dim txt_amt_cost As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost"), TextObject)
                    Dim txt_amt_cost_round As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("amt_cost_round"), TextObject)
                    Dim txt_Header As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Header"), TextObject)

                    If v_type_report = "01" Then
                        txt_Header.Text = "R/M COST SLIP with ESTIMATED OTHER COST"
                    End If
                    'sql_opt3 = " t1.cost_amount "
                    sql_opt3 = " IF(t1.cost_code='20011'," & amt_estimated & " ,t1.cost_amount)cost_amount "
                    If v_type_report = "02" Then

                        txt_Header.Text = "R/M COST SLIP without ESTIMATED OTHER COST"
                        sql_opt1 = " AND tt2.subgroup_code NOT IN('00101','00002','20003') "
                        sql_opt2 = " AND m2.subgroup_code NOT IN('00101','00002','20003')  "

                        'sql_opt3 = " IF(t1.cost_code='20011',t1.cost_amount-" & amt_estimated & " ,t1.cost_amount)cost_amount "
                    End If
                    v_ttlpersent = 0
                    SQlStr_3 = "SELECT t1.*, (t1.cost_amount / (t1.quantity * (t1.findoc_valprc/100))/m1.rate) costkg, " & _
                               "((t1.cost_amount / (t1.quantity * (t1.findoc_valprc/100))/m1.rate)/t10.findoc_valamt) costpersen, " & _
                               "IF(t1.currency_code='' or t1.cost_unit=0,'',CONCAT(t1.currency_code,'  ',cast(t1.cost_unit as char), ' @ ', IF(m2.effective_kurs IS NULL,'0', cast(format(m2.effective_kurs,2) as char)))) desc_cost FROM " & _
                               "(SELECT t1.cost_code," & sql_opt3 & ", t1.cost_description, if(t1.currency_code='IDR','',t1.currency_code) currency_code, t1.cost_unit, t1.cost_vat, t2.findoc_valprc, t3.quantity, t4.unit_code, t5.EST_DELIVERY_DT " & _
                               "FROM tbl_cost t1, tbl_shipping_doc t2, tbl_shipping_detail t3, tbl_po_detail t4, tbl_shipping t5 " & _
                               ",tbm_costcategory  tt1, tbm_costcategory_subgroup tt2 " & _
                               "WHERE   (t1.cost_code = tt1.costcat_code AND tt1.subgroup_code = tt2.subgroup_code)  " & _
                                sql_opt1 & _
                               "AND (t1.shipment_no = t2.shipment_no And t1.type_code = t2.findoc_type And t1.ship_ord_no = t2.ord_no) " & _
                               "AND t3.shipment_no=t2.shipment_no AND t3.po_no=t2.findoc_no AND t3.po_item=t2.findoc_reff " & _
                               "AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item AND t1.shipment_no=t5.shipment_no " & _
                               "AND t1.cost_code LIKE '200%' AND t1.shipment_no='" & v_pono & "' AND t1.type_code='CS' AND t1.ship_ord_no='" & v_num & "' ORDER BY t1.cost_ord_no) t1 " & _
                               "LEFT JOIN tbm_unit_equivalent m1 ON m1.unit_code=t1.unit_code AND m1.unit_code_to='KGS' " & _
                               "LEFT JOIN tbm_kurs m2 ON m2.currency_code=t1.currency_code AND m2.effective_date=t1.EST_DELIVERY_DT " & _
                               "LEFT JOIN tbl_shipping_doc t10 ON t10.findoc_type='CS' AND t10.shipment_no='" & v_pono & "' AND t10.ord_no='" & v_num & "' "
                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))
                    SQlStr_1 = SQlStr_3
                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            amt_efective = amt_efective + Math.Round(Val(MyReader.GetString(1)), 2)
                            v_ttlpersent = v_ttlpersent + Math.Round(Val(MyReader.GetString(11)), 2)
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)
                    txt_amt_cost_kg.Text = FormatNumber(amt_efective, 2)
                    SQlStr_3 = "SELECT t1.*, m3.rate, (t1.cost_amount / (t1.quantity * (t1.findoc_valprc/100))/m3.rate) cost FROM " & _
                           "(SELECT m1.subgroup_code, upper(m2.subgroup_name) subgroup_name, SUM(t1.cost_amount) cost_amount, MAX(t1.currency_code) currency_code, SUM(t1.cost_unit) cost_unit, SUM(t1.cost_vat) cost_vat, " & _
                           "t2.findoc_valprc, t3.quantity, t4.unit_code, t4.price " & _
                           "FROM tbl_cost t1, tbm_costcategory m1, tbm_costcategory_subgroup m2, " & _
                           "tbl_shipping_doc t2, tbl_shipping_detail t3, tbl_po_detail t4 " & _
                           "WHERE(t1.cost_code = m1.costcat_code And m1.subgroup_code = m2.subgroup_code) " & _
                            sql_opt2 & _
                           "AND t1.shipment_no=t2.shipment_no AND t1.type_code=t2.findoc_type AND t1.ship_ord_no=t2.ord_no " & _
                           "AND t2.shipment_no=t3.shipment_no AND t2.findoc_no=t3.po_no AND t2.findoc_reff=t3.po_item " & _
                           "AND t3.po_no=t4.po_no AND t3.po_item=t4.po_item " & _
                           "AND t1.cost_code LIKE '200%' AND t1.shipment_no='" & v_pono & "' AND t1.type_code='CS' AND t1.ship_ord_no='" & v_num & "' GROUP BY m1.subgroup_code) t1 " & _
                           "LEFT JOIN tbm_unit_equivalent m3 ON m3.unit_code=t1.unit_code AND unit_code_to='KGS'"

                    aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))
                    SQlStr_1 = SQlStr_3  ' di buka
                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            'amt_PO = Math.Round(Val(MyReader.GetString(10)), 2) + Math.Round(amt_PO, 2) berhubungan dengan remark di buka
                            amt_PO = Math.Round(Val(MyReader.GetString(11)), 2) + Math.Round(amt_PO, 2)
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                    txt_amt_cost.Text = FormatNumber(amt_PO, 2)
                    txt_amt_cost_round.Text = FormatNumber(Math.Round(Math.Ceiling(amt_PO), 0), 0)
                    txt_amt_cost2.Text = FormatNumber(amt_PO, 2)
                    txt_amt_cost_round2.Text = FormatNumber(Math.Round(Math.Ceiling(amt_PO), 0), 0)
                    Try
                        v_amount = AmbilData("cost_amount", "tbl_cost", "shipment_no ='" & v_pono & "' and type_code='CS' and ship_ord_no = '" & v_num & "' and COST_CODE='80001'")
                    Catch ex As Exception
                        v_amount = 0
                    End Try
                    txtCommision.Text = FormatNumber(v_amount, 2)
                    Try
                        v_amount = AmbilData("cost_amount", "tbl_cost", "shipment_no ='" & v_pono & "' and type_code='CS' and ship_ord_no = '" & v_num & "' and COST_CODE='80002'")
                    Catch ex As Exception
                        v_amount = 0
                    End Try
                    txtPIB.Text = FormatNumber(v_amount, 2)
                    Try
                        v_amount = AmbilData("cost_amount", "tbl_cost", "shipment_no ='" & v_pono & "' and type_code='CS' and ship_ord_no = '" & v_num & "' and COST_CODE='80003'")
                    Catch ex As Exception
                        v_amount = 0
                    End Try
                    txtDiscrepancy.Text = FormatNumber(v_amount, 2)
                    Try
                        v_amount = AmbilData("sum(cost_amount)", "tbl_cost", "shipment_no ='" & v_pono & "' and type_code='CS' and ship_ord_no = '" & v_num & "' and COST_CODE like '8000%'")
                    Catch ex As Exception
                        v_amount = 0
                    End Try
                    txtTotalBank.Text = FormatNumber(v_amount, 2)
                    '---------------------------------------End 02/01----------------------------------------------
                End If
            Case "KOOO", "SKKK"
                'Dim txtKotaTgl As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtKotaTgl"), TextObject)
                Dim i_tmpCek As Integer
                If v_type = "KOOO" Then
                    aReport = New CRKO
                    If selKO = 2 Then
                        'Print By Name
                        Dim nama As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text74"), TextObject)
                        Dim jabatan As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text73"), TextObject)
                        Dim alamat As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text76"), TextObject)
                        Dim ktp As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text75"), TextObject)
                        nama.Text = "Nama"
                        jabatan.Text = "Jabatan"
                        alamat.Text = "Alamat"
                        ktp.Text = "No KTP/SIM"
                    End If

                    SQlStr_1 = "SELECT COUNT(DISTINCT TM.MATERIAL_NAME) chk " & _
                               "FROM TBL_SHIPPING_DOC AS TSDOC, TBL_SHIPPING_DETAIL AS TSD, TBM_MATERIAL AS TM " & _
                               "WHERE TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO And TSD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                               "AND TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'KO' AND tsdoc.ord_no = '" & v_num & "'"

                    ErrMsg = "Gagal baca data detail."
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            i_tmpCek = MyReader.GetString("chk")
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)
                    If i_tmpCek > 1 Then
                        SQlStr_1 = "SELECT DISTINCT GM.GROUP_NAME MATERIAL_NAME " & _
                                   "FROM TBL_SHIPPING_DOC AS TSDOC, TBL_SHIPPING_DETAIL AS TSD, TBM_MATERIAL AS TM, TBM_MATERIAL_GROUP GM " & _
                                   "WHERE(TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO And TSD.MATERIAL_CODE = TM.MATERIAL_CODE And TM.GROUP_CODE = GM.GROUP_CODE) " & _
                                   "AND TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'KO' AND tsdoc.ord_no = '" & v_num & "'"
                    Else
                        SQlStr_1 = "SELECT DISTINCT TM.MATERIAL_NAME " & _
                                   "FROM TBL_SHIPPING_DOC AS TSDOC, TBL_SHIPPING_DETAIL AS TSD, TBM_MATERIAL AS TM " & _
                                   "WHERE(TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO And TSD.MATERIAL_CODE = TM.MATERIAL_CODE) " & _
                                   "AND TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'KO' AND tsdoc.ord_no = '" & v_num & "'"
                    End If

                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                    SQlStr_2 = "select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(a.po_no))) separator ', ') as char) as po_no from (" & _
                                "SELECT distinct TSDOC.SHIPMENT_NO,TSD.PO_NO " & _
                                "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                                "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                                "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'KO' and tsdoc.ord_no = '" & v_num & "'" & _
                                ") as a inner join vw_po_by_shipord as b on a.shipment_no=b.shipment_no and a.po_no=b.po_no"

                    aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))

                    'SQlStr_3 = "SELECT  distinct ts.shipment_no, tsi.invoice_no, date_format(tsi.invoice_dt,'%d-%M-%Y') as invoice_dt " & _
                    '            "from tbl_shipping as ts " & _
                    '            "inner join tbl_shipping_invoice as tsi on ts.shipment_no = tsi.shipment_no " & _
                    '            "WHERE ts.SHIPMENT_NO = '" & v_pono & "'"

                    SQlStr_3 = "select group_concat(trim(invoice_no),'   /   ',date_format(invoice_Dt,'%d %M %Y') separator ', ') as invoice_no " & _
                               "from (select distinct shipment_no, invoice_no, invoice_Dt from tbl_shipping_invoice) t1  where shipment_no =" & v_pono
                    aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))

                    SQlStr_4 = "select sum(tsi.invoice_amount-tsi.invoice_penalty) as InvAmount, ts.currency_code from tbl_shipping_invoice as tsi " & _
                                "inner join tbl_shipping as ts on tsi.shipment_no = ts.shipment_no " & _
                                "WHERE ts.SHIPMENT_NO = '" & v_pono & "' " & _
                                "group by tsi.shipment_no"
                    aReport.Subreports.Item(3).SetDataSource(DBQueryDataTable(SQlStr_4, MyConn, "", ErrMsg, UserData))

                    'Kemasan1
                    SQlStr_1 = "SELECT CONCAT(TRIM(CAST(formatdec(a.quantity) AS CHAR)),' ',d.unit_name,'    =   ',TRIM(CAST(formatdec(SUM(a.pack_quantity))AS CHAR)), ' ', c.pack_name) AS data1 FROM " & _
                               "(SELECT shipment_no, SUM(quantity) quantity, SUM(pack_quantity) pack_quantity, MIN(pack_code) pack_code, MIN(po_no) po_no, MIN(po_item) po_item FROM tbl_shipping_Detail WHERE shipment_no=" & v_pono & "  GROUP BY shipment_no) AS a " & _
                               "INNER JOIN tbl_po_Detail AS b ON a.po_no=b.po_no AND a.po_item=b.po_item " & _
                               "LEFT JOIN tbm_packing AS c ON a.pack_code=c.pack_Code " & _
                               "LEFT JOIN tbm_unit AS d ON b.unit_code=d.unit_code "

                    Dim tt7 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("kemasan"), TextObject)
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            Try
                                tt7.Text = MyReader.GetString(0)
                            Catch ex As Exception
                            End Try
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)

                    SQlStr_1 = "select cast(group_concat(trim(data2) separator ', ') as char) as data2 " & _
                                "from(" & _
                                "select concat(count(unit_code),'X',unit_code) as data2 " & _
                                "from tbl_Shipping_cont where shipment_no=" & v_pono & _
                                " group by unit_code) as a "
                    MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                    If Not MyReader Is Nothing Then
                        While MyReader.Read
                            Try
                                tt7.Text = Trim(tt7.Text) & "   =   " & MyReader.GetString(0)
                            Catch ex As Exception
                            End Try
                        End While
                    End If
                    CloseMyReader(MyReader, UserData)
                ElseIf v_type = "SKKK" Then
                    aReport = New CRSK
                    'SQlStr_1 = "SELECT TSDOC.SHIPMENT_NO,TSD.PO_NO,TSD.PO_ITEM,TSD.MATERIAL_CODE,TM.MATERIAL_NAME " & _
                    '            "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                    '            "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                    '            "INNER JOIN TBM_MATERIAL AS TM ON TSD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                    '            "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'SK' and tsdoc.ord_no = '" & v_num & "'"

                    SQlStr_1 = "SELECT GROUP_CONCAT(DISTINCT TM.MATERIAL_NAME SEPARATOR ', ') MATERIAL_NAME " & _
                                "FROM TBL_SHIPPING_DOC AS TSDOC " & _
                                "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO " & _
                                "INNER JOIN TBM_MATERIAL AS TM ON TSD.MATERIAL_CODE = TM.MATERIAL_CODE " & _
                                "WHERE TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'SK' and tsdoc.ord_no = '" & v_num & "'"
                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
                End If
            Case "JCCC"
                aReport = New CRJC
                SQlStr_1 = "Select concat('PT. ',line_name) line_name, address, concat('Di ',city_name) city_name from tbm_lines, tbl_shipping, tbm_city " & _
                               "where tbm_lines.line_code=tbl_shipping.shipping_line and " & _
                               "tbm_lines.city_code = tbm_city.city_code And tbl_shipping.shipment_no = " & v_pono
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                SQlStr_1 = "select concat(trim(b.city_name),', ',date_format(a.findoc_printeddt,'%d %M %Y')) as tgl from tbl_shipping_doc as a " & _
                           "left join tbm_city as b on a.findoc_printedon=b.city_code " & _
                           "where shipment_no=" & v_pono & " and findoc_type='JC' and ord_no=" & v_num

                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
            Case "BCCC"
                aReport = New CRBC
                ''' diganti pake text object
                ''' sebab klo pake fieldObject kadang data gak keluar ==
                Dim i_tmpCek As Integer
                Dim name As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("nama"), TextObject)
                Dim title As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("jabatan"), TextObject)
                Dim companyname As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("companyname"), TextObject)
                Dim companyname2 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("companyname2"), TextObject)
                Dim address As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("alamat"), TextObject)
                Dim cityname As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("cityname"), TextObject)
                Dim npwp As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("npwp"), TextObject)
                Dim api As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("api"), TextObject)
                Dim phonefax As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("phonefax"), TextObject)
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            name.Text = MyReader.GetString("name")
                        Catch ex As Exception
                        End Try
                        Try
                            title.Text = MyReader.GetString("title")
                        Catch ex As Exception
                        End Try
                        Try
                            companyname.Text = MyReader.GetString("company_name")
                            companyname2.Text = MyReader.GetString("company_name")
                        Catch ex As Exception
                        End Try
                        Try
                            address.Text = MyReader.GetString("address")
                        Catch ex As Exception
                        End Try
                        Try
                            cityname.Text = MyReader.GetString("city_name")
                        Catch ex As Exception
                        End Try
                        Try
                            npwp.Text = MyReader.GetString("npwp")
                        Catch ex As Exception
                        End Try
                        Try
                            api.Text = MyReader.GetString("api_u_apit_no")
                        Catch ex As Exception
                        End Try
                        Try
                            phonefax.Text = MyReader.GetString("phonefax")
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                'PO
                'SQlStr_1 = "SELECT DISTINCT(trim(PO_NO)) as po_no FROM TBL_SHIPPING_DETAIL WHERE SHIPMENT_NO=" & v_pono
                SQlStr_1 = "select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(po_no))) separator ', ') as char) as po_no " & _
                           "FROM TBL_SHIPPING_DETAIL WHERE SHIPMENT_NO=" & v_pono
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Penerima Kuasa
                SQlStr_1 = "select b.company_name,b.npwp,b.address,b.edi_no,concat(if(b.phone is null,'',b.phone),' / ',if(b.fax is null,'',b.fax)) as phonefax " & _
                           "from tbl_shipping_doc as a " & _
                           "left join tbm_expedition as b on a.findoc_to=b.company_code " & _
                           "where a.shipment_no=" & v_pono & " and a.findoc_type='BC' and a.ord_no=" & v_num
                aReport.Subreports.Item(5).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                SQlStr = "SELECT count(DISTINCT TM.MATERIAL_NAME) chk " & _
                               "FROM TBL_SHIPPING_DOC AS TSDOC, TBL_SHIPPING_DETAIL AS TSD, TBM_MATERIAL AS TM " & _
                               "WHERE(TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO And TSD.MATERIAL_CODE = TM.MATERIAL_CODE) " & _
                               "AND TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'BC' AND tsdoc.ord_no = '" & v_num & "'"

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        i_tmpCek = MyReader.GetString("chk")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If i_tmpCek > 1 Then
                    SQlStr = "SELECT DISTINCT GM.GROUP_NAME MATERIAL_NAME " & _
                               "FROM TBL_SHIPPING_DOC AS TSDOC, TBL_SHIPPING_DETAIL AS TSD, TBM_MATERIAL AS TM, TBM_MATERIAL_GROUP GM " & _
                               "WHERE(TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO And TSD.MATERIAL_CODE = TM.MATERIAL_CODE And TM.GROUP_CODE = GM.GROUP_CODE) " & _
                               "AND TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'BC' AND tsdoc.ord_no = '" & v_num & "'"

                Else
                    SQlStr = "SELECT DISTINCT TM.MATERIAL_NAME " & _
                               "FROM TBL_SHIPPING_DOC AS TSDOC, TBL_SHIPPING_DETAIL AS TSD, TBM_MATERIAL AS TM " & _
                               "WHERE(TSDOC.SHIPMENT_NO = TSD.SHIPMENT_NO And TSD.MATERIAL_CODE = TM.MATERIAL_CODE) " & _
                               "AND TSDOC.SHIPMENT_NO = '" & v_pono & "' AND TSDOC.FINDOC_TYPE = 'BC' AND tsdoc.ord_no = '" & v_num & "'"

                End If


                Dim txtmaterial As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtmaterial"), TextObject)

                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            txtmaterial.Text = MyReader.GetString("MATERIAL_NAME")
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                'Invoice
                SQlStr_1 = "select shipment_no, group_concat(trim(invoice_no),'   /   ',date_format(invoice_Dt,'%d %M %Y') separator ', ') as invoice " & _
                           "from (select distinct shipment_no, invoice_no, invoice_Dt from tbl_shipping_invoice) t1  where shipment_no =" & v_pono & " group by shipment_no "
                aReport.Subreports.Item(3).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Dokumen
                SQlStr_1 = "select b.supplier_name, d.currency_code, sum(c.invoice_amount-c.invoice_penalty) as invoice, e.invoice packinglist_no,a.bl_no,a.insurance_no " & _
                               "from tbl_shipping as a " & _
                               "left join tbm_supplier as b on a.supplier_code=b.supplier_code " & _
                               "left join tbl_shipping_invoice as c on a.shipment_no=c.shipment_no " & _
                               "left join (select distinct t1.shipment_no, currency_code from tbl_shipping_detail t1, tbl_po t2 where t1.po_no=t2.po_no) as d on a.shipment_no=d.shipment_no " & _
                               "left join (select shipment_no, group_concat(trim(invoice_no),'   /   ',date_format(invoice_Dt,'%d %M %Y') separator ', ') as invoice " & _
                               "           from (select distinct shipment_no, invoice_no, invoice_Dt from tbl_shipping_invoice) t1 group by shipment_no " & _
                               "          ) as e on a.shipment_no=e.shipment_no " & _
                               "where a.shipment_no=" & v_pono & " group by a.shipment_no"


                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                SQlStr_1 = "SELECT CONCAT(TRIM(CAST(formatdec(a.quantity) AS CHAR)),' ',d.unit_name,'    =    ',TRIM(CAST(formatdec(SUM(a.pack_quantity))AS CHAR)), ' ', c.pack_name) AS data1 FROM " & _
                                      "(SELECT shipment_no, SUM(quantity) quantity, SUM(pack_quantity) pack_quantity, MIN(pack_code) pack_code, MIN(po_no) po_no, MIN(po_item) po_item FROM tbl_shipping_Detail WHERE shipment_no=" & v_pono & "  GROUP BY shipment_no) AS a " & _
                                      "INNER JOIN tbl_po_Detail AS b ON a.po_no=b.po_no AND a.po_item=b.po_item " & _
                                      "LEFT JOIN tbm_packing AS c ON a.pack_code=c.pack_Code " & _
                                      "LEFT JOIN tbm_unit AS d ON b.unit_code=d.unit_code "

                Dim tt7 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("kemasan"), TextObject)
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            tt7.Text = MyReader.GetString(0)
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr_1 = "select cast(group_concat(trim(data2) separator ', ') as char) as data2 " & _
                                                "from(" & _
                                                "select concat(count(unit_code),'X',unit_code) as data2 " & _
                                                "from tbl_Shipping_cont where shipment_no=" & v_pono & _
                                                " group by unit_code) as a "
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            tt7.Text = Trim(tt7.Text) & "   =   " & MyReader.GetString(0)
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                'Tgl
                SQlStr_1 = "select trim(concat(b.city_name,', ',date_format(a.findoc_printeddt,'%d %M %Y'))) as tgl from tbl_shipping_Doc as a " & _
                           "left join tbm_city as b on a.findoc_printedon=b.city_code " & _
                           "where a.shipment_no = '" & v_pono & "' and a.findoc_type='BC' and ord_no=" & v_num
                aReport.Subreports.Item(6).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Penerima Kuasa2
                SQlStr_1 = "select b.company_name " & _
                           "from tbl_shipping_doc as a " & _
                           "left join tbm_expedition as b on a.findoc_to=b.company_code " & _
                           "where a.shipment_no=" & v_pono & " and a.findoc_type='BC' and a.ord_no=" & v_num
                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Nama
                SQlStr_1 = "select concat('(',b.name,')') as nama " & _
                         "from tbl_shipping_doc as a " & _
                         "left join tbm_users as b on a.findoc_appby = b.user_ct " & _
                         "inner join tbl_shipping_Detail as c on a.shipment_no=c.shipment_no " & _
                         "inner join tbl_po as d on c.po_no=d.po_no " & _
                         "left join tbm_company as e on d.company_code=e.company_code " & _
                         "left join tbm_city as f on e.city_code=f.city_code " & _
                         "where a.shipment_no=" & v_pono & " and a.findoc_type='BC' and a.ord_no = " & v_num & _
                         " group by name,title,company_name"
                aReport.Subreports.Item(4).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
            Case "BSCC"
                aReport = New CRBS
                Dim Description As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Description"), TextObject)
                Dim Material As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Material"), TextObject)
                Dim lv_invoice_no, lv_invoice_dt, lv_material As String
                Dim i_tmpCek As Integer

                'PO
                SQlStr_1 = "select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(po_no))) separator ', ') as char) as po_no " & _
                           "FROM TBL_SHIPPING_DETAIL WHERE SHIPMENT_NO=" & v_pono
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Invoice
                SQlStr_1 = "select cast(group_concat(distinct(invoice_no) separator ', ') as char) as invoice_no, DATE_FORMAT(MAX(invoice_dt),'%d %M %Y') invoice_dt " & _
                           "FROM TBL_SHIPPING_INVOICE WHERE SHIPMENT_NO=" & v_pono & " GROUP BY shipment_no "

                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            lv_invoice_no = MyReader.GetString("invoice_no")
                            lv_invoice_dt = MyReader.GetString("invoice_dt")
                        Catch ex As Exception
                            lv_invoice_no = ""
                            lv_invoice_dt = ""
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            Description.Text = "Dengan ini menyatakan bahwa " & MyReader.GetString("broker") & " dengan alamat " & MyReader.GetString("broker_address") & " " & _
                            " yang tertera di invoice no. " & lv_invoice_no & " tertanggal " & lv_invoice_dt & " adalah broker, sedangkan " & MyReader.GetString("produsen") & " " & _
                            " dengan alamat " & MyReader.GetString("produsen_address") & " yang tertera dalam B/L No. " & MyReader.GetString("bl_no") & " adalah sebagai shipper. "
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr_1 = "SELECT count(distinct t1.material_code) chk " & _
                           "FROM tbl_shipping_detail t1 " & _
                           "WHERE t1.shipment_no = " & v_pono & " "

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        i_tmpCek = MyReader.GetString("chk")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                'Material
                If i_tmpCek > 1 Then
                    SQlStr_1 = "SELECT FormatDec(SUM(t1.quantity)) AS quantity, MAX(t2.unit_code) unit_code,  m2.group_name material_name " & _
                            "FROM tbl_shipping_detail t1, tbl_po_detail t2, tbm_material m1, tbm_material_group m2 " & _
                            "WHERE t1.po_no = t2.po_no And t1.po_item = t2.po_item " & _
                            "AND t1.material_code = m1.material_code AND m1.group_code=m2.group_code " & _
                            "AND t1.shipment_no = " & v_pono & " GROUP BY m2.group_code "
                Else
                    SQlStr_1 = "SELECT FormatDec(t1.quantity) AS quantity, t2.unit_code,  m1.material_name " & _
                               "FROM tbl_shipping_detail t1, tbl_po_detail t2, tbm_material m1 " & _
                               "WHERE t1.po_no = t2.po_no And t1.po_item = t2.po_item And t1.material_code = m1.material_code And t1.shipment_no = " & v_pono & " "
                End If

                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            lv_material = MyReader.GetString("quantity") & " " & MyReader.GetString("unit_code") & " " & MyReader.GetString("material_name") & ", "
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If lv_material <> "" Then Material.Text = Mid(lv_material, 1, InStr(lv_material, ",") - 1)
            Case "PCCC"
                Dim i_tmpCek As Integer

                aReport = New CRPC
                'tgl
                SQlStr_1 = "select trim(concat(b.city_name,', ',date_format(a.findoc_printeddt,'%d %M %Y'))) as tgl from tbl_shipping_Doc as a " & _
                           "inner join tbm_city as b on a.findoc_printedon=b.city_code " & _
                           "where a.shipment_no = '" & v_pono & "' and a.findoc_type='PC' and ord_no=" & v_num
                aReport.Subreports.Item(3).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'header
                SQlStr_1 = "Select concat('PT. ',line_name) line_name, address, concat('Di ',city_name) city_name from tbm_lines, tbl_shipping, tbm_city " & _
                               "where tbm_lines.line_code=tbl_shipping.shipping_line and " & _
                               "tbm_lines.city_code = tbm_city.city_code And tbl_shipping.shipment_no = " & v_pono
                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'material
                SQlStr_1 = "SELECT COUNT(material_code) chk FROM tbl_shipping_Detail  WHERE shipment_no = " & v_pono & ""
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        i_tmpCek = MyReader.GetString("chk")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If i_tmpCek > 1 Then
                    SQlStr_1 = "SELECT GROUP_CONCAT(distinct c.group_name SEPARATOR ', ') material_name FROM tbl_shipping_Detail AS a, tbm_material AS b, tbm_material_group AS c " & _
                               "WHERE a.material_code=b.material_Code AND b.group_code=c.group_code AND a.shipment_no = " & v_pono
                Else
                    SQlStr_1 = "select b.material_name from tbl_shipping_Detail as a, tbm_material as b where a.material_code=b.material_Code " & _
                               "and a.shipment_no = " & v_pono
                End If
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'footer
                SQlStr_1 = "select c.name,c.title from tbl_shipping_doc as a " & _
                           "inner join tbl_shipping as b on a.shipment_no=b.shipment_no " & _
                           "inner join tbm_users as c on a.findoc_appby=c.user_Ct " & _
                           "where a.shipment_no = " & v_pono & " and a.findoc_type='PC' and ord_no=" & v_num
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'container
                Dim container As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("kemasan"), TextObject)
                SQlStr_1 = "select cast(group_concat(trim(data2) separator ', ') as char) as data2 " & _
                                                "from(" & _
                                                "select concat(count(unit_code),' X ',unit_code) as data2 " & _
                                                "from tbl_Shipping_cont where shipment_no=" & v_pono & _
                                                " group by unit_code) as a "
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            container.Text = MyReader.GetString("data2")
                        Catch ex As Exception
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                'aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Hormat kami
                Dim hormatkami As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("hormatkami"), TextObject)
                SQlStr_1 = "select distinct(c.company_name) " & _
                           "from tbl_Shipping_Detail as a " & _
                           "inner join tbl_po as b on a.po_no=b.po_no " & _
                           "inner join tbm_company as c on b.company_code=c.company_Code " & _
                           "where a.shipment_no = " & v_pono
                ErrMsg = "Gagal baca data"
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        hormatkami.Text = MyReader.GetString("company_name")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
            Case "SZSZ"
                aReport = New CRSZ

                Dim BodyStr As String
                Dim SQlStr2, WhrStr As String
                Dim ItemStr, ValStr As String

                SQlStrA = "select * from tbm_cr_template where type_code= 'SZ' and group_code = '" & v_docgrp & "'"

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        BodyStr = MyReader.GetString("BODY1")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr = "select query_code, query_str from tbm_cr_query where query_code='SZDB' "
                WhrStr = "t1.shipment_no='" & v_pono & "' "

                ErrMsg = "Gagal baca data query."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            SQlStr = MyReader.GetString("QUERY_STR")
                        Catch
                            SQlStr = ""
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr = Replace(SQlStr, ":KONDISI", WhrStr)

                ErrMsg = "Gagal baca template letter."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        ItemStr = MyReader.GetString("fill_name")

                        Try
                            ValStr = MyReader.GetString(MyReader.GetString("fill_source"))
                        Catch
                            ValStr = ""
                        End Try

                        BodyStr = Replace(BodyStr, ItemStr, ValStr)
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr = "SELECT sh.PO, t1.*, tu.name appby, tu.title FROM " & _
                         "  (SELECT t1.shipment_no, date_format(t1.findoc_printeddt,'%M %d, %Y') findoc_printeddt, concat(m2.city_name,', ') printedon, t1.findoc_appby, " & _
                         "   m1.perihal, m1.header1, m1.header2, '" & BodyStr & "' as doc_name, m1.footer1, m1.footer2 " & _
                         "   FROM tbl_shipping_doc t1, tbm_cr_template m1, tbm_city m2 " & _
                         "   WHERE t1.shipment_no='" & v_pono & "' AND t1.ord_no=" & v_num & " AND t1.findoc_type='SZ' " & _
                         "   AND m1.type_code='SZ' AND m1.group_code='" & v_docgrp & "' " & _
                         "   AND t1.findoc_printedon = m2.city_code) t1 " & _
                         "LEFT JOIN tbm_users AS tu ON t1.findoc_appby = tu.user_ct " & _
                         "LEFT JOIN (SELECT shipment_no, GROUP_CONCAT(DISTINCT TRIM(po_no) SEPARATOR ', ') PO " & _
                         "           FROM tbl_shipping_detail WHERE shipment_no='" & v_pono & "' GROUP BY shipment_no) AS sh ON sh.shipment_no=t1.shipment_no "

            Case "SBSB"
                aReport = New CRSB

                Dim BodyStr As String
                Dim SQlStr2, WhrStr As String
                Dim ItemStr, ValStr As String

                SQlStrA = "select * from tbm_cr_template where type_code= 'SB' and group_code = '" & v_docgrp & "'"

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        BodyStr = MyReader.GetString("BODY1")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr = "select query_code, query_str from tbm_cr_query where query_code='SBDB' "
                WhrStr = "t1.shipment_no='" & v_pono & "' "

                ErrMsg = "Gagal baca data query."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            SQlStr = MyReader.GetString("QUERY_STR")
                        Catch
                            SQlStr = ""
                        End Try
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr = Replace(SQlStr, ":KONDISI", WhrStr)

                ErrMsg = "Gagal baca template letter."
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        ItemStr = MyReader.GetString("fill_name")

                        Try
                            ValStr = MyReader.GetString(MyReader.GetString("fill_source"))
                        Catch
                            ValStr = ""
                        End Try

                        BodyStr = Replace(BodyStr, ItemStr, ValStr)
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                SQlStr = "SELECT sh.PO, t1.*, tu.name appby, tu.title FROM " & _
                        "  (SELECT m4.company_name, m4.address companyaddress, t1.shipment_no, date_format(t1.findoc_printeddt,'%M %d, %Y') findoc_printeddt, concat(m2.city_name,', ') printedon, t1.findoc_appby, " & _
                        "   t1.findoc_no, t1.findoc_reff, DATE_FORMAT(t1.findoc_finappdt,'%M %d, %Y') findoc_dt, findoc_note, " & _
                        "   MID(t0.sppb_no,1,INSTR(t0.sppb_no,'/')-1) sppb_no, DATE_FORMAT(t0.sppb_dt,'%M %d, %Y') sppb_dt, " & _
                        "   CONCAT('Hal : ',m1.perihal) perihal, m1.header1, m1.header2, '" & BodyStr & "' as doc_name, m1.footer1, m1.footer2 " & _
                        "   FROM tbl_shipping t0, tbl_shipping_doc t1, tbm_cr_template m1, tbm_city m2, tbm_plant m3, tbm_company m4  " & _
                        "   WHERE t0.shipment_no=t1.shipment_no AND t1.shipment_no='" & v_pono & "' AND t1.ord_no=" & v_num & " AND t1.findoc_type='SB' " & _
                        "   AND m1.type_code='SB' AND m1.group_code='" & v_docgrp & "' " & _
                        "   AND t1.findoc_printedon = m2.city_code AND t0.plant_code=m3.plant_code AND m3.company_code=m4.company_code) t1 " & _
                        "LEFT JOIN tbm_users AS tu ON t1.findoc_appby = tu.user_ct " & _
                        "LEFT JOIN (SELECT shipment_no, GROUP_CONCAT(DISTINCT(getPOorder(shipment_no,TRIM(po_no))) SEPARATOR ', ') PO " & _
                        "           FROM tbl_shipping_detail WHERE shipment_no='" & v_pono & "' GROUP BY shipment_no) AS sh ON sh.shipment_no=t1.shipment_no "

            Case "MCI1", "MCI2"
                aReport = New CRMCI

                Dim obj2 As Section = CType(aReport.ReportDefinition.Sections("PageHeaderSection13"), Section)
                obj2.SectionFormat.EnableSuppress = (v_type = "MCI2")
                Dim obj2a As Section = CType(aReport.ReportDefinition.Sections("PageHeaderSection2"), Section)
                obj2a.SectionFormat.EnableSuppress = (v_type = "MCI2")
                Dim obj3 As Section = CType(aReport.ReportDefinition.Sections("PageHeaderSection16"), Section)
                obj3.SectionFormat.EnableSuppress = (v_type = "MCI1")
                Dim obj3a As Section = CType(aReport.ReportDefinition.Sections("PageHeaderSection15"), Section)
                obj3a.SectionFormat.EnableSuppress = (v_type = "MCI1")

                'Survey req (main query di atas)
                'Kalau Survey_Req kosong Text36 di kosongkan
                Dim Req As String = AmbilData("survey_Req", "tbl_mci", "shipment_no=" & v_pono & " and ord_no=" & v_num)
                Dim txtReq As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("Text36"), TextObject)
                If Req = "" Then
                    txtReq.Text = ""
                    Dim obj1 As Section = CType(aReport.ReportDefinition.Sections("PageHeaderSection8"), Section)
                    obj1.SectionFormat.EnableSuppress = True
                End If

                'barang
                SQlStr_1 = "select FormatDec(a.quantity) AS quantity,c.unit_name,d.material_name " & _
                         "from tbl_shipping_Detail as a " & _
                         "inner join tbl_po_Detail as b on a.po_no=b.po_no " & _
                         "inner join tbm_unit as c on b.unit_code=c.unit_code " & _
                         "inner join tbm_material as d on b.material_code=d.material_code " & _
                         "where a.shipment_no=" & v_pono
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'PO
                SQlStr_1 = "select cast(group_concat(distinct(getpoorder(" & v_pono & ",trim(po_no))) separator ', ') as char) AS po_no " & _
                           "from tbl_shipping_Detail where shipment_no=" & v_pono
                aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Barang2
                SQlStr_1 = "select d.material_name " & _
                         "from tbl_shipping_Detail as a " & _
                         "inner join tbl_po_Detail as b on a.po_no=b.po_no " & _
                         "inner join tbm_material as d on b.material_code=d.material_code " & _
                         "where a.shipment_no=" & v_pono
                aReport.Subreports.Item(3).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'Qty2
                SQlStr_1 = "select FormatDec(a.quantity) AS quantity, c.unit_name, d.total_container " & _
                         "from tbl_shipping_Detail as a " & _
                         "INNER JOIN tbl_shipping AS d ON d.shipment_no=a.shipment_no " & _
                         "inner join tbl_po_Detail as b on a.po_no=b.po_no " & _
                         "inner join tbm_unit as c on b.unit_code=c.unit_code " & _
                         "where a.shipment_no=" & v_pono
                aReport.Subreports.Item(5).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'LC
                Dim tbl As String = "tbl_shipping_Detail as a " & _
                                    "inner join tbl_po_Detail as b on a.po_no=b.po_no " & _
                                    "inner join tbl_budget as c on b.po_no=c.po_no "
                Dim where As String = "a.shipment_no=" & v_pono & " and c.status<>'Rejected' and c.lc_no<>''"

                SQlStr_1 = "select distinct c.lc_no from " & tbl & " where " & where
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

                'jika tidak ada LC tulis "NON L/C"
                Dim temp As String = AmbilData("group_concat(c.lc_no)", tbl, where)
                If temp = "" Then
                    SQlStr_1 = "select 'NON L/C' as lc_no"
                    aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
                End If

                'PORT2
                SQlStr_1 = "select b.pack_quantity,concat(c.pack_name,' di ',d.port_name) as port " & _
                           "from tbl_shipping as a " & _
                           "inner join tbl_shipping_detail as b on a.shipment_no=b.shipment_no " & _
                           "inner join tbm_packing as c on b.pack_code=c.pack_Code " & _
                           "inner join tbm_port as d on a.port_code=d.port_code " & _
                           "where b.shipment_no=" & v_pono
                aReport.Subreports.Item(4).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))

            Case "TTDPAJAK"
                aReport = New CRTandaTerimaPajak

            Case "TTDCSLIP"
                aReport = New CRTandaCSlip

            Case "TTDPV"
                aReport = New CRTandaTerimaPV
            Case "OUTB"   ' added by estrika 141010
                aReport = New CROutBPUM

                'Mengetahui
                Dim sign1 As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("sign1"), TextObject)
                SQlStr_1 = "select sign_name from tbr_signname where sign_modul='CROUTBPUM'"
                ErrMsg = "Gagal baca data"
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        sign1.Text = MyReader.GetString("sign_name")
                    End While
                End If
                CloseMyReader(MyReader, UserData)
            Case "DNPC"
                aReport = New CRDNP
            Case "RIL"
                aReport = New CRRIL 'Added by Prie 03.11.2010
            Case Else
                CloseMyConn(MyConn)
                Me.Close()
                Exit Sub
        End Select

        '=======================================
        'third query
        '=======================================
        Try
            ErrMsg = "Gagal baca data untuk laporan."
            aReport.SetDataSource(DBQueryDataTable(SQlStr, MyConn, "", ErrMsg, UserData))
            '            aReport.SetDataSource(DBQueryDataTable(SQLStr_A, MyConn, "", ErrMsg, UserData))
            CRV_Viewer.ReportSource = aReport
            If CStr(v_type) = "SHIN" Then
                'comodity
                If v_shipmentno = "" Then

                    SQlStr_0 = "select tpd.QUANTITY, " & _
                    "FormatDec(tpd.QUANTITY) as QTY, " & _
                    "tpd.material_code, tpd.price, tmu.unit_name, tpo.CURRENCY_code, " & _
                    "tmm.MATERIAL_NAME, concat('PACKED IN ' ,tpc.pack_name) pack_name, tpo.TOLERABLE_DELIVERY " & _
                    "from tbl_si as tsi " & _
                    "inner join tbl_po as tpo on tsi.po_no = tpo.po_no " & _
                    "inner join tbl_po_detail as tpd on tpo.po_no = tpd.po_no " & _
                    "inner join tbm_unit as tmu on tpd.unit_code = tmu.unit_code " & _
                    "inner join tbm_material as tmm on tpd.material_code = tmm.material_code " & _
                    "left join tbm_packing as tpc on tpd.package_code = tpc.pack_code " & _
                    "where tsi.shipment_no is null and tsi.po_no = '" & v_pono & "' and tsi.ord_no = '" & v_num & "'"
                Else
                    SQlStr_0 = "select tps.QUANTITY, " & _
                    "FormatDec(tpd.QUANTITY) as QTY, " & _
                    "tps.material_code,tpd.price,tmu.unit_name,tpp.CURRENCY_code," & _
                    "tmm.MATERIAL_NAME, CONCAT('PACKED IN ' ,tpc.pack_name) pack_name, tpo.TOLERABLE_DELIVERY " & _
                    "from tbl_si as tsi " & _
                    "INNER JOIN tbl_shipping AS tpp ON tsi.shipment_no = tpp.shipment_no " & _
                    "INNER JOIN tbl_shipping_detail AS tps ON tsi.shipment_no = tps.shipment_no " & _
                    "INNER JOIN tbl_po AS tpo ON tps.po_no = tpo.po_no " & _
                    "INNER JOIN tbl_po_detail AS tpd ON tps.po_no = tpd.po_no AND tps.po_item = tpd.po_item " & _
                    "INNER JOIN tbm_unit AS tmu ON tpd.unit_code = tmu.unit_code " & _
                    "INNER JOIN tbm_material AS tmm ON tps.material_code = tmm.material_code " & _
                    "LEFT JOIN tbm_packing AS tpc ON tps.pack_code = tpc.pack_code " & _
                    "where tsi.shipment_no = '" & v_shipmentno & "' and tsi.ord_no = '" & v_num & "'"
                End If
                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_0, MyConn, "", ErrMsg, UserData))

                'unit price
                If v_doc <> "3" Then
                    SQlStr_1 = SQlStr_0
                    aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
                End If

                'country
                SQlStr_2 = "select distinct tmc.country_name " & _
                "from tbl_si as tsi " & _
                "inner join tbl_po as tpo on tsi.po_no = tpo.po_no " & _
                "inner join tbl_po_detail as tpd on tpo.po_no = tpd.po_no " & _
                "inner join tbm_country as tmc on tpd.country_code = tmc.country_code " & _
                "where tsi.po_no = '" & v_pono & "' and tsi.ord_no = '" & v_num & "'"
                aReport.Subreports.Item(2).SetDataSource(DBQueryDataTable(SQlStr_2, MyConn, "", ErrMsg, UserData))

                'hsnumber
                SQlStr_3 = "select distinct tpd.hs_code " & _
                "from tbl_si as tsi " & _
                "inner join tbl_po as tpo on tsi.po_no = tpo.po_no " & _
                "inner join tbl_po_detail as tpd on tpo.po_no = tpd.po_no " & _
                "where tsi.po_no = '" & v_pono & "' and tsi.ord_no = '" & v_num & "'"
                aReport.Subreports.Item(3).SetDataSource(DBQueryDataTable(SQlStr_3, MyConn, "", ErrMsg, UserData))

                'NB -> bagian ini di tutup di form inputnya
                'Dim NB As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("NB"), TextObject)
                'NB.Text = GetNB(v_pono, v_num, aReport)

                If v_doc = "1" Then
                    Dim obj1 As Section = CType(aReport.ReportDefinition.Sections("ReportHeaderSection13"), Section)
                    obj1.SectionFormat.EnableSuppress = True
                End If
                If v_doc = "2" Then
                    Dim obj2 As Section = CType(aReport.ReportDefinition.Sections("ReportHeaderSection16"), Section)
                    obj2.SectionFormat.EnableSuppress = True
                End If
                If v_doc = "3" Then
                    Dim obj3 As Section = CType(aReport.ReportDefinition.Sections("ReportHeaderSection18"), Section)
                    obj3.SectionFormat.EnableSuppress = True
                    Dim obj4 As Section = CType(aReport.ReportDefinition.Sections("ReportHeaderSection7"), Section)
                    obj4.SectionFormat.EnableSuppress = True
                    Dim txt As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("text85"), TextObject)
                    txt.Text = "To avoid any dispute with the shipping agent at the destination port, please arrange with the shipping company " & _
                    "to provide free time demurrage at least 21 days and shipping company's statement to this effect is required." & Chr(13) & Chr(10) & _
                    "Must be quote on Bill of Lading 21 days free time demurange."
                End If
            End If

            If CStr(v_type) = "RILL" Then
                'prepare item to be listed
            End If

            If CStr(v_type) = "RIBL" Then
                'prepare item to be listed
            End If

            If CStr(v_type) = "RILT" Then
                'prepare item to be listed
            End If

            If CStr(v_type) = "BRRR" Then
                'prepare item to be listed
                Dim no_oke As Integer

                'Jika material lebih dari 1 di summary by group per lc dan opening date yang sama
                rows = 0
                SQlStrA = "SELECT COUNT(DISTINCT t1.material_code) FROM tbl_shipping_detail t1, tbl_remitance t2 WHERE t1.shipment_no=t2.shipment_no AND t2.shipment_no IN " & _
                          "(SELECT shipment_no FROM tbl_remitance WHERE status <> 'Rejected' and concat(lc_no,openingdt) = " & _
                          "(SELECT concat(lc_no,openingdt) FROM tbl_remitance WHERE status <> 'Rejected' and shipment_no='" & v_pono & "'))"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStrA, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        rows = MyReader.GetValue(0)
                    End While
                End If
                CloseMyReader(MyReader, UserData)
                If rows = 1 Then
                    SQlStrA = "SELECT MAX(t3.tolerable_delivery) tolerable_delivery, MAX(t3.currency_code) currency_code, MAX(m1.material_name) material_name, SUM(t1.quantity) quantity, MAX(unit_code) unit_code " & _
                              "FROM tbl_shipping_detail t1, tbl_po_detail t2, tbl_po t3, tbm_material m1 " & _
                              "WHERE(t1.po_no = t2.po_no And t1.po_item = t2.po_item And t1.po_no = t3.po_no And t1.material_code = m1.material_code) " & _
                              "AND t1.shipment_no IN (SELECT shipment_no FROM tbl_remitance WHERE status <> 'Rejected' and concat(lc_no,openingdt) = " & _
                              "         (SELECT concat(lc_no,openingdt) FROM tbl_remitance WHERE status <> 'Rejected' and shipment_no='" & v_pono & "'))"
                Else
                    SQlStrA = "SELECT MAX(t3.tolerable_delivery) tolerable_delivery, MAX(t3.currency_code) currency_code, MAX(m2.group_name) material_name, SUM(t1.quantity) quantity, MAX(t2.unit_code) unit_code " & _
                              "FROM tbl_shipping_detail t1, tbl_po_detail t2, tbl_po t3, tbm_material m1, tbm_material_group m2 " & _
                              "WHERE(t1.po_no = t2.po_no And t1.po_item = t2.po_item And t1.po_no = t3.po_no And t1.material_code = m1.material_code And m1.group_code = m2.group_code) " & _
                              "AND t1.shipment_no IN (SELECT shipment_no FROM tbl_remitance WHERE status <> 'Rejected' and concat(lc_no,openingdt) = " & _
                              "         (SELECT concat(lc_no,openingdt) FROM tbl_remitance WHERE status <> 'Rejected' and shipment_no='" & v_pono & "'))"
                End If

                aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStrA, MyConn, "", ErrMsg, UserData))

                Dim txtPO As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("PO"), TextObject)
                SQlStr_1 = "SELECT CAST(GROUP_CONCAT(DISTINCT getpoorder(t1.shipment_no, TRIM(t1.po_no)) SEPARATOR ', ') AS CHAR) PO FROM tbl_shipping_detail t1, tbl_remitance t2 WHERE t1.shipment_no=t2.shipment_no " & _
                           "AND t2.status <> 'Rejected' AND concat(t2.lc_no,t2.openingdt) = (SELECT concat(lc_no,openingdt) FROM tbl_remitance WHERE status <> 'Rejected' and shipment_no='" & v_pono & "') "

                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtPO.Text = MyReader.GetString("PO")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                Dim txtBL As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtBL"), TextObject)
                SQlStr_1 = "SELECT CAST(GROUP_CONCAT(bl_no SEPARATOR ', ') AS CHAR) BL FROM tbl_remitance WHERE status <> 'Rejected' and concat(lc_no,openingdt) = (SELECT concat(lc_no,openingdt) FROM tbl_remitance WHERE status <> 'Rejected' and shipment_no='" & v_pono & "')"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtBL.Text = MyReader.GetString("BL")
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                Dim txtVessel As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtVessel"), TextObject)
                Dim txtETD As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtETD"), TextObject)
                Dim txtETA As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("txtETA"), TextObject)

                Dim V_ETD, V_ETD1, V_ETA, V_ETA1 As String
                Dim V_ETD2, V_ETA2 As Date
                Dim v_etd2n, v_eta2n As String
                SQlStr_2 = "select ts.vessel, ts.bl_no, tp.port_name loadport_name, tc.city_name loadcity_name, " & _
                "if(ts.est_delivery_dt is null,'',ts.est_delivery_dt) etd, tp2.port_name, tc2.city_name city_name, " & _
                "if((ts.notice_arrival_dt is null) or (ts.notice_arrival_dt=''), ts.est_arrival_dt, ts.notice_arrival_dt) eta " & _
                "from tbl_shipping ts, tbm_port tp, tbm_port tp2, tbm_city tc, tbm_city tc2 " & _
                "where(ts.loadport_code = tp.port_code And ts.port_code = tp2.port_code) " & _
                "and tp.city_code = tc.city_code and tp2.city_code = tc2.city_code " & _
                "and ts.shipment_no = '" & v_pono & "'"
                ErrMsg = "Gagal baca data detail."
                MyReader = DBQueryMyReader(SQlStr_2, MyConn, ErrMsg, UserData)
                v_etd2n = ""
                v_eta2n = ""
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        Try
                            txtVessel.Text = MyReader.GetString("VESSEL")
                        Catch ex As Exception
                            txtVessel.Text = ""
                        End Try
                        Try
                            V_ETD = MyReader.GetString("LOADPORT_NAME")
                        Catch ex As Exception
                            V_ETD = ""
                        End Try
                        Try
                            V_ETD1 = MyReader.GetString("LOADCITY_NAME")
                        Catch ex As Exception
                            V_ETD1 = ""
                        End Try
                        Try
                            V_ETD2 = MyReader.GetString("ETD")
                        Catch ex As Exception
                            v_etd2n = "Y"
                        End Try
                        Try
                            V_ETA = MyReader.GetString("PORT_NAME")
                        Catch ex As Exception
                            V_ETA = ""
                        End Try
                        Try
                            V_ETA1 = MyReader.GetString("CITY_NAME")
                        Catch ex As Exception
                            V_ETA1 = ""
                        End Try

                        Try
                            V_ETA2 = MyReader.GetString("ETA")
                        Catch ex As Exception
                            v_eta2n = "Y"
                        End Try
                    End While
                End If
                If v_etd2n = "Y" Then
                    txtETD.Text = V_ETD & ", " & V_ETD1
                Else
                    txtETD.Text = V_ETD & ", " & V_ETD1 & "   " & Format(V_ETD2, "dd-MMM-yy")
                End If
                If v_eta2n = "Y" Then
                    txtETA.Text = V_ETA & ", " & V_ETA1
                Else
                    txtETA.Text = V_ETA & ", " & V_ETA1 & "   " & Format(V_ETA2, "dd-MMM-yy")
                End If

                CloseMyReader(MyReader, UserData)

            End If

            If (CStr(v_type) = "PVVV") Or (CStr(v_type) = "VGVV") Then
                'prepare item to be listed
                Dim no_oke As Integer
                no_oke = 0

                'SQlStr_0 = "select group_concat(data separator ', ') as data from " & _
                '"(select concat(trim(a.invoice_no),' ',trim(b.currency_Code),' ',trim(cast(format(sum(a.invoice_amount),2) as char(15)))) as data " & _
                '"from tbl_shipping_invoice as a " & _
                '"inner join tbl_shipping as b on a.shipment_no=b.shipment_no " & _
                '"where a.shipment_no = " & v_pono & _
                '" GROUP BY a.INVOICE_NO) as x "

                SQlStr_0 = "select group_concat(data separator ', ') as data from " & _
                "(select trim(a.invoice_no) as data " & _
                "from tbl_shipping_invoice as a " & _
                "inner join tbl_shipping as b on a.shipment_no=b.shipment_no " & _
                "where a.shipment_no = " & v_pono & _
                " GROUP BY a.INVOICE_NO) as x "

                'aReport.Subreports.Item(0).SetDataSource(DBQueryDataTable(SQlStr_0, MyConn, "", ErrMsg, UserData))
                Dim txtInvNo As TextObject = CType(aReport.Subreports.Item(0).ReportDefinition.ReportObjects.Item("invoice"), TextObject)
                MyReader = DBQueryMyReader(SQlStr_0, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtInvNo.Text = MyReader.GetString("data") & " " & txtInvNo.Text
                    End While
                End If
                CloseMyReader(MyReader, UserData)

                Dim strPO As String = "GetPOorder(" & v_pono & "," & "trim(TPD.PO_NO))"
                'SQlStr_1 = "select cast(group_concat(distinct TPD.PO_NO separator ', ') as char(256)) as po_no " & _
                SQlStr_1 = "select cast(group_concat(distinct " & strPO & " separator ', ') as char(256)) as po_no " & _
                "FROM TBL_PO_DETAIL AS TPD " & _
                "INNER JOIN TBL_PO AS TP ON TPD.PO_NO = TP.PO_NO " & _
                "INNER JOIN TBL_SHIPPING_DETAIL AS TSD ON TSD.PO_NO = TP.PO_NO AND TSD.PO_ITEM = TPD.PO_ITEM " & _
                "INNER JOIN TBL_SHIPPING_DOC AS TSDOC ON TSD.SHIPMENT_NO = TSDOC.SHIPMENT_NO " & _
                "where TSDOC.shipment_no = '" & v_pono & "' and TSDOC.ord_no = '" & v_num & "' and TSDOC.FINDOC_TYPE = '" & Mid(CStr(v_type), 1, 2) & "'"
                'aReport.Subreports.Item(1).SetDataSource(DBQueryDataTable(SQlStr_1, MyConn, "", ErrMsg, UserData))
                Dim txtPONO As TextObject = CType(aReport.Subreports.Item(1).ReportDefinition.ReportObjects.Item("txtPONO"), TextObject)
                MyReader = DBQueryMyReader(SQlStr_1, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        txtPONO.Text = MyReader.GetString("PO_NO") & " " & txtPONO.Text
                    End While
                End If
                CloseMyReader(MyReader, UserData)


            End If
        Catch ex As Exception
            MsgBox("Laporan gagal ditampilkan." & ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try

        Me.Show()
        'CloseMyConn(MyConn)
        CRV_Viewer.Refresh()
    End Sub

    Private Function f_huruf(ByVal strAbjad As String) As String
        Dim label1 As New Label

        Select Case strAbjad
            Case 1
                label1.Text = "a."
            Case 2
                label1.Text = "b."
            Case 3
                label1.Text = "c."
            Case 4
                label1.Text = "d."
            Case 5
                label1.Text = "e."
            Case 6
                label1.Text = "f."
            Case 7
                label1.Text = "g."
            Case 8
                label1.Text = "h."
            Case 9
                label1.Text = "i."
            Case 10
                label1.Text = "j."
            Case 11
                label1.Text = "k."
            Case 12
                label1.Text = "l."
            Case 13
                label1.Text = "m."
            Case 14
                label1.Text = "n."

        End Select

        Return label1.Text
    End Function
    Private Function GetNum(ByVal strnum As String, ByVal stat As String) As String
        Dim last3dig, num As Decimal

        'rounded ribuan dibulatkan ke atas
        'contoh 74.575.559 menjadi 74.576.000

        '152.340   340   152.340-340 = 152.000
        '253.907   907   253.907-907+1000 = 254.000
        '355.460   460   355.460-460=355.000

        num = CDec(strnum)
        If stat = "1" Then   'rounded
            If Len(strnum) <= 3 Then
                last3dig = strnum
            Else
                last3dig = Microsoft.VisualBasic.Right(Trim(strnum), 3)
            End If
            num = num - CDec(last3dig)
            If CDec(last3dig) >= 500 Then
                num += 1000
            End If
        End If
        GetNum = FormatNumber(num, 0)
    End Function
    Private Function GetNB(ByVal PO As String, ByVal ord As String, ByVal aReport As ReportClass) As String
        Dim cnt, a As Integer
        Dim lcode As New RichTextBox
        Dim str As String = ""

        lcode.Text = AmbilData("line_code", "tbl_si_line", "po_no = '" & PO & "' and ord_no = " & ord & " and line_code<>'00000'")
        cnt = lcode.Lines.Count() - 1
        GetNB = ""
        For a = 0 To cnt
            If str <> "" Then str = str & ","
            str = str & "'" & lcode.Lines(a).ToString() & "'"
        Next
        If str <> "" Then
            str = "line_code in (" & str & ")"
            GetNB = AmbilData("cast(group_concat(line_name separator ', ') as char(256))", "tbm_lines", str)
        End If
        If GetNB <> "" Then
            GetNB = "Shipment by : " & GetNB
            If cnt = 0 Then
                GetNB = GetNB & " is prohibited"
            Else
                GetNB = GetNB & " are prohibited"
            End If
        Else
            Dim txtNB As TextObject = CType(aReport.ReportDefinition.ReportObjects.Item("textNB"), TextObject)
            txtNB.Text = ""
        End If
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function
    Private Sub Update_Tbl_Shipping(ByVal shipno As Integer, ByVal bea As String, ByVal vat As String, ByVal pph22 As String, ByVal kurs As String)
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim SQLStr As String

        SQLStr = "Update tbl_shipping " & _
                 "set bea_masuk=" & bea & _
                 ", vat=" & vat & _
                 ", pph21=" & pph22 & _
                 ", kurs_pajak=" & kurs & _
                 " where shipment_no=" & shipno
        Try
            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)
            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)
            If hasil = True Then
                'f_msgbox_successful("Update tbl_shipping ")
            Else
                MsgBox("Update failed...", MsgBoxStyle.Information, "Update tbl_shipping")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub printButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SQlStr As String

        Dim v_po_no, v_ord_no As String

        Try
            If (v_type_report = "02" And Me.Tag.ToString.Substring(0, 2) = "CS") Then
                SQlStr = "select po_no, ord_no from tmp_generaldata where shipment_no = '" & v_pono & "' "
                MyReader = DBQueryMyReader(SQlStr, MyConn, ErrMsg, UserData)
                If Not MyReader Is Nothing Then
                    While MyReader.Read
                        v_po_no = MyReader.GetValue(0)
                        v_ord_no = MyReader.GetValue(1)
                    End While
                End If
                CloseMyReader(MyReader, UserData)


                SQlStr = "UPDATE template_poim " & _
                         "SET flag_upload = 0 " & _
                         " where po = '" & sp_po & "' and ord_no = '" & sp_ord_no & "'"

                affrow = DBQueryUpdate(SQlStr, MyConn1, False, ErrMsg, UserData)


            End If
                

            Dim PrintDialog As New PrintDialog()
            If PrintDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                aReport.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName

                aReport.PrintToPrinter(PrintDialog.PrinterSettings.Copies, False, PrintDialog.PrinterSettings.FromPage, PrintDialog.PrinterSettings.ToPage)


            End If
        Catch ex As Exception


        End Try
        'End If


    End Sub


    Private Sub tsItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ' Put your code here, before print

        Dim PrintDialog As New PrintDialog()




        If PrintDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            rpt.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName

            rpt.PrintToPrinter(PrintDialog.PrinterSettings.Copies, False, PrintDialog.PrinterSettings.FromPage, PrintDialog.PrinterSettings.ToPage)


        End If

    End Sub

    Private Sub tsItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Put your code here, before print

        Dim PrintDialog As New PrintDialog()


        If PrintDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            rpt.PrintOptions.PrinterName = PrintDialog.PrinterSettings.PrinterName

            rpt.PrintToPrinter(PrintDialog.PrinterSettings.Copies, PrintDialog.PrinterSettings.Collate, PrintDialog.PrinterSettings.FromPage, PrintDialog.PrinterSettings.ToPage)

        End If

    End Sub

    Private Sub CRV_Viewer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CRV_Viewer.Load
        '' Hide default button
        'CRV_Viewer.ShowPrintButton = False
        'CRV_Viewer.ShowExportButton = False




        ''New print button
        CRV_Viewer.ShowPrintButton = False

        For Each ctrl As Control In CRV_Viewer.Controls
            If TypeOf ctrl Is Windows.Forms.ToolStrip Then
                Dim btnNew As New ToolStripButton
                Dim btnNew2 As New ToolStripButton
                btnNew.Text = "Print"
                btnNew.ToolTipText = "Print"
                btnNew.Image = My.Resources.print2
                btnNew.DisplayStyle = ToolStripItemDisplayStyle.Image
                CType(ctrl, ToolStrip).Items.Insert(0, btnNew)
                AddHandler btnNew.Click, AddressOf printButton_Click
            End If
        Next
        '' ---------------------------------------------
    End Sub
End Class