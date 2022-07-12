VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_Load()
' Set ODBC untuk impr --- MySQL
Dim cnn1, cnnWr As New ADODB.Connection
Dim i

Set cnn1 = New ADODB.Connection
cnn1.Open "DSN=impr;UID=root;pwd=vbdev"

Set cnnWr = New ADODB.Connection
cnnWr.Open "DSN=impr;UID=root;pwd=vbdev"

strsqlA = "SELECT 0 shipment_no, pd.po_no, pd.po_item ord_no, '' bl_no, pd.po_no PO, my.company_name Company, my.line_bussines BussinesLine, mp.plant_name DestinationPlant, mt.port_name DestinationPort, " _
        & "IF(po.shipment_term_code='P', 'Partial Shipment','Whole Shipment') ShipmentTerm, " _
        & "IF(30-DATE_FORMAT(po.shipment_period_fr,'%d')+1 > DATE_FORMAT(po.shipment_period_to,'%d'), DATE_FORMAT(po.shipment_period_fr,'%M %Y'), DATE_FORMAT(po.shipment_period_to,'%M %Y')) ShipmentPeriod, CAST(po.shipment_period_fr AS CHAR) shipment_period_fr, CAST(po.shipment_period_to AS CHAR) shipment_period_to, " _
        & "po.ipa_no IPA, mm.material_name DescriptionOfGoods, mc.country_name Origin, pd.hs_code HSCode, pd.specification Protein, po.tolerable_delivery Tolerable, pd.quantity POQuantity, 0 ShipQuantity, " _
        & "pd.unit_code Unit, po.currency_code Currency, FORMAT(pd.price,2) UnitPrice, po.insurance_code Incoterm, FORMAT((pd.quantity * pd.price),2) POAmount, " _
        & "'' InvoiceNo, '' InvoiceDate, 0 InvoiceAmount, 0 PackedQuantity, '' PackedUnit, '' SGS, '' BL, '' HostBL, " _
        & "ms.supplier_name Supplier, ms2.produsen_name Produsen, po.contract_no Contract, '' CopyDoc, '' OriginDoc, '' ForwardDoc, CAST(IF(po.est_delivery_dt IS NULL,'',po.est_delivery_dt) AS CHAR) ShipOnBoard, " _
        & "'' ETA, '' ActualArrival, 0 FreeTime, '' DeliveryDate, '' ClearanceDate, '' Container, '' FinalPlant, '' FinalPort, '' LoadPort, '' ShippingLine, '' Vessel, " _
        & "FORMAT(0,2) TaxRate, FORMAT(0,2) BeaMasuk, FORMAT(0,2) VAT, FORMAT(0,2) PPH22, FORMAT(0,2) PIUD, '' TdTerimaPajak, '' InsuranceNo, FORMAT(0,2) InsuranceAmount, '' TTDate, '' DueDate, '' PIBNo, '' PIBDate, '' AJU, '' SPPBDate, '' SPPBNo, mu.name CreatedBy " _
        & "FROM tbl_po_detail pd, tbl_po po, " _
        & "tbm_company my, tbm_plant mp, tbm_port mt, tbm_material mm, tbm_material_group gm, tbm_country mc, tbm_supplier ms, tbm_produsen ms2, tbm_users mu "
        
strsqlA = strsqlA & " " _
        & "Where (pd.po_no = po.po_no) " _
        & "AND po.company_code=my.company_code AND po.plant_code=mp.plant_code AND po.port_code=mt.port_code " _
        & "AND pd.material_code=mm.material_code AND mm.group_code=gm.group_code AND pd.country_code=mc.country_code AND po.supplier_code=ms.supplier_code " _
        & "AND po.produsen_code=ms2.produsen_code AND po.createdby = mu.user_ct " _
        & "AND po.po_no NOT IN (SELECT po_no FROM tbl_shipping_detail) "

strsqlB = "SELECT sd.shipment_no, sd.po_no, sd.po_item ord_no, ss.bl_no, getpoorder(sd.shipment_no, TRIM(sd.po_no)) PO, " _
        & "mc.company_name Company, mc.line_bussines BussinesLine, mp2.plant_name DestinationPlant, mo3.port_name DestinationPort, " _
        & "IF(po.shipment_term_code='P', 'Partial Shipment','Whole Shipment') ShipmentTerm, " _
        & "IF(30-DATE_FORMAT(po.shipment_period_fr,'%d')+1 > DATE_FORMAT(po.shipment_period_to,'%d'), DATE_FORMAT(po.shipment_period_fr,'%M %Y'), DATE_FORMAT(po.shipment_period_to,'%M %Y')) ShipmentPeriod, CAST(po.shipment_period_fr AS CHAR) shipment_period_fr, CAST(po.shipment_period_to AS CHAR) shipment_period_to, " _
        & "po.IPA_no IPA, " _
        & "mm.material_name DescriptionOfGoods, my.country_name Origin, pd.hs_code HSCode, pd.specification Protein, po.tolerable_delivery Tolerable, " _
        & "pd.Quantity POQuantity, sd.Quantity ShipQuantity, pd.unit_code Unit, " _
        & "po.currency_code Currency, FORMAT(pd.price,2) UnitPrice, po.insurance_code Incoterm, FORMAT((pd.quantity * pd.price),2) POAmount, sv.invoice_no InvoiceNo, CAST(sv.invoice_dt AS CHAR) InvoiceDate, (sv.invoice_amount-sv.invoice_penalty) InvoiceAmount, " _
        & "sd.pack_quantity PackedQuantity, ma.pack_name PackedUnit, sd.specification SGS, " _
        & "ss.bl_no BL, ss.HostBL HostBL, ms.supplier_name Supplier, mn.produsen_name Produsen, po.contract_no Contract, " _
        & "CAST(IF(ss.received_copydoc_dt IS NULL,'',ss.received_copydoc_dt) AS CHAR) CopyDoc, CAST(IF(ss.received_doc_dt IS NULL,'',ss.received_doc_dt) AS CHAR) OriginDoc, CAST(IF(ss.forward_doc_dt IS NULL,'',ss.forward_doc_dt) AS CHAR) ForwardDoc, " _
        & "CAST(IF(ss.est_delivery_dt IS NULL,'',ss.est_delivery_dt) AS CHAR) ShipOnBoard, CAST(IF(ss.est_arrival_dt IS NULL,'',ss.est_arrival_dt) AS CHAR) ETA, CAST(IF(ss.Notice_arrival_dt IS NULL,'',ss.Notice_arrival_dt) AS CHAR) ActualArrival, " _
        & "IF(ss.free_time IS NULL,0,ss.free_time) FreeTime, CAST(IF(ss.clr_dt IS NULL, IF(ss.est_clr_dt IS NULL,'',ss.est_clr_dt), ss.clr_dt) AS CHAR) DeliveryDate, " _
        & "CAST(IF(ss.bapb_dt IS NULL, IF(ss.est_bapb_dt IS NULL,'',ss.est_bapb_dt), ss.bapb_dt) AS CHAR) ClearanceDate, IF(ss.total_container IS NULL,'',ss.total_container) Container, " _
        & "mp.plant_name FinalPlant, mo.port_name FinalPort, mo2.port_name LoadPort, ml.line_name ShippingLine, ss.Vessel Vessel, " _
        & "FORMAT(ss.kurs_pajak,2) TaxRate, FORMAT(ss.bea_masuk,2) BeaMasuk, FORMAT(ss.Vat,2) VAT, FORMAT(ss.pph21,2) PPH22, FORMAT(ss.PIUD,2) PIUD, CAST(IF(ss.verified2dt IS NULL,'',ss.verified2dt) AS CHAR) TdTerimaPajak, ss.insurance_no InsuranceNo, FORMAT(ss.insurance_amount,2) InsuranceAmount, " _
        & "CAST(IF(ss.tt_dt IS NULL,'',ss.tt_dt) AS CHAR) TTDate, CAST(IF(ss.due_dt IS NULL,'',ss.due_dt) AS CHAR) DueDate, ss.pib_no PIBNo, CAST(IF(ss.pib_dt IS NULL,'',ss.pib_dt) AS CHAR) PIBDate, ss.aju_no AJU, CAST(IF(ss.sppb_dt IS NULL,'',ss.sppb_dt) AS CHAR) SPPBDate, " _
        & "ss.sppb_no SPPBNo, mu.name CreatedBy " _
        & "FROM tbl_shipping_detail sd, tbl_shipping ss, tbl_po_detail pd, tbl_po po, tbl_shipping_invoice sv, " _
        & "tbm_material mm, tbm_material_group gm, tbm_packing ma, tbm_supplier ms, tbm_plant mp, tbm_port mo, tbm_port mo2, tbm_lines ml, tbm_users mu, " _
        & "tbm_country my, tbm_company mc, tbm_plant mp2, tbm_port mo3, tbm_produsen mn "
        
 strsqlB = strsqlB & " " _
        & "Where (sd.shipment_no = ss.shipment_no) " _
        & "AND sd.po_no=pd.po_no AND sd.po_item=pd.po_item " _
        & "AND sd.po_no=po.po_no " _
        & "AND sd.shipment_no=sv.shipment_no AND sd.po_no=sv.po_no AND sd.po_item=sv.ord_no " _
        & "AND sd.material_code=mm.material_code AND mm.group_code=gm.group_code AND sd.pack_code=ma.pack_code " _
        & "AND ss.supplier_code=ms.supplier_code AND ss.plant_code=mp.plant_code AND ss.port_code=mo.port_code AND ss.loadport_code=mo2.port_code " _
        & "AND ss.shipping_line=ml.line_code AND ss.createdby=mu.user_ct AND pd.country_code=my.country_code " _
        & "AND po.company_code=mc.company_code AND po.plant_code=mp2.plant_code AND po.port_code=mo3.port_code " _
        & "AND po.produsen_code=mn.produsen_code "

strsql = "SELECT t1.*, CAST(IF(d1.BudgetOpeningLC IS NULL,'',d1.BudgetOpeningLC) AS CHAR) BudgetOpeningLC, CAST(IF(d0.LCNo IS NULL,'',d0.LCNo) AS CHAR) LCNo, " _
       & "CAST(IF(d2.ReqImportLisence_po IS NULL, IF(d4.ReqImportLisence_bl IS NULL,'',d4.ReqImportLisence_bl), d2.ReqImportLisence_po) AS CHAR) ReqImportLisence, " _
       & "CAST(IF(d20.RILNo_po IS NULL, IF(d19.RILNo_bl IS NULL,'',d19.RILNo_bl),  d20.RILNo_po) AS CHAR) RILNo, " _
       & "CAST(IF(d22.Deptan_po IS NULL, IF(d21.Deptan_bl IS NULL,'',d21.Deptan_bl), d22.Deptan_po) AS CHAR) Deptan, " _
       & "CAST(IF(d17.DeptanNo_po IS NULL, IF(d16.DeptanNo_bl IS NULL,'',d16.DeptanNo_bl),  d17.DeptanNo_po) AS CHAR) DeptanNo, " _
       & "CAST(IF(d3.ShippingInstruction_po IS NULL, IF(d5.ShippingInstruction_bl IS NULL,'',d5.ShippingInstruction_bl), d3.ShippingInstruction_po) AS CHAR) ShippingInstruction, " _
       & "CAST(IF(d6.BugdetRemitance IS NULL,'',d6.BugdetRemitance) AS CHAR) BugdetRemitance, CAST(IF(d7.PaymentVoucher IS NULL,'',d7.PaymentVoucher) AS CHAR) PaymentVoucher, CAST(IF(d8.VoucherGiro IS NULL,'',d8.VoucherGiro) AS CHAR) VoucherGiro, CAST(IF(d9.CoverLetter IS NULL,'',d9.CoverLetter) AS CHAR) CoverLetter, " _
       & "CAST(IF(d12.MCI IS NULL,'',d12.MCI) AS CHAR) MCI, CAST(IF(d13.BudgetTT IS NULL,'',d13.BudgetTT) AS CHAR) BudgetTT, CAST(IF(d14.BudgetPIB IS NULL,'',d14.BudgetPIB) AS CHAR) BudgetPIB, CAST(IF(d15.BudgetCAD IS NULL,'',d15.BudgetCAD) AS CHAR) BudgetCAD, " _
       & "CAST(IF(d10.Inklaring IS NULL,'',d10.Inklaring) AS CHAR) Inklaring, CAST(IF(d11.CostSlip IS NULL,'',d11.CostSlip) AS CHAR) CostSlip, cast(IF(d18.effective_kurs IS NULL,'',format(d18.effective_kurs,2)) as char) CostSlipRate, cast(IF(d11.perKGs IS NULL,'',if(d11.perKGs=0,'',format(d11.perKGs,2))) as char) perKGs, CAST(IF(d11.TdTerimaCostSlip IS NULL,'',d11.TdTerimaCostSlip) AS CHAR) TdTerimaCostSlip, cast(NOW() as char) CREATEDDT FROM " _
       & "(" & strsqlA & " " _
       & "UNION " _
       & " " & strsqlB & ") t1 "
       
       
strsql = strsql & " " _
       & "LEFT JOIN (SELECT po_no, MIN(OpeningDt) BudgetOpeningLC FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d1 ON d1.po_no=t1.po_no " _
       & "LEFT JOIN (SELECT po_no, GROUP_CONCAT(lc_no SEPARATOR ',') LCNo FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d0 ON d0.po_no=t1.po_no " _
       & "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ReqImportLisence_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d2 ON d2.po_no=t1.po_no " _
       & "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ShippingInstruction_po FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d3 ON d3.po_no=t1.po_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ReqImportLisence_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d4 ON d4.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ShippingInstruction_bl FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d5 ON d5.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BugdetRemitance FROM tbl_remitance WHERE STATUS<>'Rejected' GROUP BY shipment_no) d6 ON d6.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) PaymentVoucher FROM tbl_shipping_doc WHERE findoc_type='PV' AND findoc_status<>'Rejected' GROUP BY shipment_no) d7 ON d7.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) VoucherGiro FROM tbl_shipping_doc WHERE findoc_type='VG' AND findoc_status<>'Rejected' GROUP BY shipment_no) d8 ON d8.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CoverLetter FROM tbl_shipping_doc WHERE findoc_type='CL' AND findoc_status<>'Rejected' GROUP BY shipment_no) d9 ON d9.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) Inklaring FROM tbl_shipping_doc WHERE findoc_type='CC' AND findoc_status<>'Rejected' GROUP BY shipment_no) d10 ON d10.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CostSlip, MIN(FINDOC_VALAMT) perKGs, CAST(MIN(findoc_finappdt) AS CHAR) TdTerimaCostSlip FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status<>'Rejected' GROUP BY shipment_no) d11 ON d11.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) MCI FROM tbl_mci WHERE STATUS<>'Rejected' GROUP BY shipment_no) d12 ON d12.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetTT FROM tbl_budgets WHERE type_code='TT' AND STATUS<>'Rejected' GROUP BY shipment_no) d13 ON d13.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetPIB FROM tbl_budgets WHERE type_code='BP' AND STATUS<>'Rejected' GROUP BY shipment_no) d14 ON d14.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetCAD FROM tbl_budgets WHERE type_code='CA' AND STATUS<>'Rejected' GROUP BY shipment_no) d15 ON d15.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(deptan_no) DeptanNo_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL AND deptan_no <> '' GROUP BY shipment_no) d16 ON d16.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT po_no, MIN(deptan_no) DeptanNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL AND deptan_no <> '' GROUP BY po_no) d17 ON d17.po_no=t1.po_no " _
       & "LEFT JOIN tbm_kurs d18 ON d18.currency_code=t1.Currency AND d18.effective_date=t1.ShipOnBoard " _
       & "LEFT JOIN (SELECT shipment_no, MIN(ril_no) RILNo_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d19 ON d19.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT po_no, MIN(ril_no) RILNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d20 ON d20.po_no=t1.po_no " _
       & "LEFT JOIN (SELECT shipment_no, MIN(IssuedDt) Deptan_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d21 ON d21.shipment_no=t1.shipment_no " _
       & "LEFT JOIN (SELECT po_no, MIN(IssuedDt) Deptan_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d22 ON d22.po_no=t1.po_no " _
       & " "

strsqlw = "Delete from tmp_generaldata"
Set rsdataWr = cnnWr.Execute(strsqlw)
 
Set rsdata1 = cnn1.Execute(strsql)
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
            xshipment_no = rsdata1("shipment_no")
            xpo_no = rsdata1("po_no")
            xord_no = rsdata1("ord_no")
            xbl_no = rsdata1("bl_no")
            xPO = rsdata1("PO")
            xCompany = rsdata1("Company")
            xBussinesLine = rsdata1("BussinesLine")
            xDestinationPlant = rsdata1("DestinationPlant")
            xDestinationPort = rsdata1("DestinationPort")
            xShipmentTerm = rsdata1("ShipmentTerm")
            xShipmentPeriod = rsdata1("ShipmentPeriod")
            xShipmentPeriodFr = rsdata1("shipment_period_fr")
            xShipmentPeriodTo = rsdata1("shipment_period_to")
            xIPA = rsdata1("IPA")
            xDescriptionOfGoods = rsdata1("DescriptionOfGoods")
            xOrigin = rsdata1("Origin")
            xHSCode = rsdata1("HSCode")
            xProtein = rsdata1("Protein")
            xTolerable = rsdata1("Tolerable")
            xPOQuantity = rsdata1("POQuantity")
            xShipQuantity = rsdata1("ShipQuantity")
            xUnit = rsdata1("Unit")
            xCurrency = rsdata1("Currency")
            xUnitPrice = rsdata1("UnitPrice")
            xIncoterm = rsdata1("Incoterm")
            xPOAmount = rsdata1("POAmount")
            xInvoiceNo = rsdata1("InvoiceNo")
            xInvoiceDate = rsdata1("InvoiceDate")
            xInvoiceAmount = rsdata1("InvoiceAmount")
            xPackedQuantity = rsdata1("PackedQuantity")
            xPackedUnit = rsdata1("PackedUnit")
            xSGS = rsdata1("SGS")
            xBL = rsdata1("BL")
            xHostBL = rsdata1("HostBL")
            xSupplier = rsdata1("Supplier")
            xProdusen = rsdata1("Produsen")
            xContract = rsdata1("Contract")
            xCopyDoc = rsdata1("CopyDoc")
            xOriginDoc = rsdata1("OriginDoc")
            xForwardDoc = rsdata1("ForwardDoc")
            xShipOnBoard = rsdata1("ShipOnBoard")
            xETA = rsdata1("ETA")
            xActualArrival = rsdata1("ActualArrival")
            xFreeTime = rsdata1("FreeTime")
            xDeliveryDate = rsdata1("DeliveryDate")
            xClearanceDate = rsdata1("ClearanceDate")
            xContainer = rsdata1("Container")
            xFinalPlant = rsdata1("FinalPlant")
            xFinalPort = rsdata1("FinalPort")
            xLoadPort = rsdata1("LoadPort")
            xShippingLine = rsdata1("ShippingLine")
            xVessel = rsdata1("Vessel")
            xTaxRate = rsdata1("TaxRate")
            xBeaMasuk = rsdata1("BeaMasuk")
            xVAT = rsdata1("VAT")
            xPPH22 = rsdata1("PPH22")
            xPIUD = rsdata1("PIUD")
            xTdTerimaPajak = rsdata1("TdTerimaPajak")
            xInsuranceNo = rsdata1("InsuranceNo")
            xInsuranceAmount = rsdata1("InsuranceAmount")
            xTTDate = rsdata1("TTDate")
            xDueDate = rsdata1("DueDate")
            xPIBNo = rsdata1("PIBNo")
            xPIBDate = rsdata1("PIBDate")
            xAJU = rsdata1("AJU")
            xSPPBDate = rsdata1("SPPBDate")
            xSPPBNo = rsdata1("SPPBNo")
            xCreatedBy = rsdata1("CreatedBy")
            xBudgetOpeningLC = rsdata1("BudgetOpeningLC")
            xLCNo = rsdata1("LCNo")
            xReqImportLisence = rsdata1("ReqImportLisence")
            xDeptanNo = rsdata1("DeptanNo")
            xRILno = rsdata1("RILNo")
            xDeptan = rsdata1("Deptan")
            xShippingInstruction = rsdata1("ShippingInstruction")
            xBugdetRemitance = rsdata1("BugdetRemitance")
            xPaymentVoucher = rsdata1("PaymentVoucher")
            xVoucherGiro = rsdata1("VoucherGiro")
            xCoverLetter = rsdata1("CoverLetter")
            xMCI = rsdata1("MCI")
            xBudgetTT = rsdata1("BudgetTT")
            xBudgetPIB = rsdata1("BudgetPIB")
            xBudgetCAD = rsdata1("BudgetCAD")
            xInklaring = rsdata1("Inklaring")
            xCostSlip = rsdata1("CostSlip")
            xCostSlipRate = rsdata1("CostSlipRate")
            xperKGs = rsdata1("perKGs")
            xTdTerimaCostSlip = rsdata1("TdTerimaCostSlip")
            xCREATEDDT = rsdata1("CREATEDDT")
  
  
  MsgBox ("ichonk")
        rsdata1.MoveNext
    Loop
End If

Set rsdata1 = Nothing
Set rsdataWr = Nothing
Set cnn1 = Nothing
Set cnnWr = Nothing
Unload Me
End Sub














