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

Dim cnn1, cnnWr As New ADODB.Connection
Dim nmFile As String
Dim dtFile As String

Dim plCode As String
Dim dtFrom, dtTo As String
Dim SQLstr, SQLstr1, SQLstr2, strA As String
Dim LnPO, LnINV As Boolean

Dim po, po_ori, supp, suppnm, podt, plant, coa1, coa2, coa3 As String
Dim amount_idr, amount, kurs, amount_inv, ppn As String
Dim xFlag, invno, invdt, gudtpb, xcurr, bob, fcanctpb, cacntpb, ttdt, gudid As String

Dim errPO, err2PO As String
Dim plno, i As Integer

'dtFrom = Format(Now, "yyyy-01-01")
dtFrom = Format(DateAdd("m", -3, Now()), "yyyy-MM-01")
dtTo = Format(Now, "yyyy-MM-dd")
dtFile = Format(Now, "yyyy-MM-dd")
dtFile = Mid(dtFile, 9, 2) & Mid(dtFile, 6, 2)

Set cnn1 = New ADODB.Connection
cnn1.Open "DSN=impr;UID=root;pwd=vbdev"

Set cnnWr = New ADODB.Connection
cnnWr.Open "DSN=impr;UID=root;pwd=vbdev"

SQLstr = "DELETE FROM temp_tbr_toap2"
Set rsdata2 = cnn1.Execute(SQLstr)

SQLstr = "SELECT plant_code, plant_name FROM tbm_plant WHERE ipsvr IS NOT NULL AND ipsvr <> ''"
Set rsdata2 = cnn1.Execute(SQLstr)
If Not rsdata2.EOF Then
  Do While Not rsdata2.EOF
    
    plno = plno + 1
    LnPO = True
    LnINV = True
    
    ErrMsg = ""
    
    plCode = rsdata2("plant_code")
    plFile = rsdata2("plant_name")
    plFile = Replace(plFile, " ", "")
    'plCode = "62011"
    'plFile = Replace("CPB-Lampung", " ", "")


    'create IMPO yaitu PO2 yang belum ada shipmentnya.
    nmF = "Impo" & dtFile & "." & plFile
    nmFile = "C:\" & nmF

    SQLstr1 = _
    "SELECT t1.po_no, trim(t1.supplier_code) supplier_code, MID(trim(m2.supplier_name),1,30) supplier_name, t1.purchaseddt, CAST(DATE_FORMAT(t1.purchaseddt,'%d-%m-%Y') AS CHAR) po_dt, 0 GUDID, currency_code, " & _
    "(SELECT SUM(st.price * st.quantity) FROM tbl_po_detail st WHERE st.po_no=t1.po_no) po_amount, 0 kurs_po, 0 ppn, '' COA1, '' COA2, '' COA3, m1.plant_name " & _
    "FROM tbl_po t1, tbm_plant m1, tbm_supplier m2  " & _
    "WHERE t1.plant_code = m1.plant_code AND t1.supplier_code = m2.supplier_code " & _
    "AND t1.status <> 'Rejected' and t1.status <> 'Closed' " & _
    "AND (DATE_FORMAT(t1.purchaseddt,'%Y') > 2010 OR t1.purchaseddt IS NULL) " & _
    "AND t1.po_no NOT IN (SELECT distinct po_no FROM tbl_shipping_detail) " & _
    "AND t1.plant_code = '" & plCode & "' " & _
    " "
    'AND (DATE_FORMAT(t1.purchaseddt,'%Y-%m-%d') BETWEEN '" & dtFrom & "' AND '" & dtTo & "') " & _

    SQLstr2 = _
                "SELECT po_no, MID(po_no,1,13) po_no13, po_no_withplant, " & _
                " IF(stryear <> '', CONCAT(po_no_withyear,stryear),'') po_no_withyear, " & _
                " "
                
    SQLstr2 = SQLstr2 & _
                " IF(INSTR(UPPER(po_no_withyear),'-S/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-S/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_s, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-D/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-D/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_d, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-K/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-K/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_k, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-T/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-T/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_t, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-INT/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-INT/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_int, " & _
                " "
                
    SQLstr2 = SQLstr2 & _
                " IF(INSTR(UPPER(po_no_withyear),'-PMX/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-PMX/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_pmx, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-SBG/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-SBG/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_sbg, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-JKT/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-JKT/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_jkt, " & _
                " IF(INSTR(UPPER(po_no_withyear),'-SR/') > 0, " & _
                "      CONCAT(MID(po_no_withyear,1,INSTR(UPPER(po_no_withyear),'-SR/')-1),'/',stryear) " & _
                "      ,'') po_no_withyear_sr " & _
                " "
                
    SQLstr2 = SQLstr2 & _
                " FROM " & _
                "  (SELECT po_no, " & _
                "   IF(RIGHT(po_no_withplant,4) BETWEEN '2007' AND '2020', " & _
                "       '', po_no_withplant) po_no_withplant, " & _
                "   IF(po_no_withyear <> '', po_no_withyear, " & _
                "       IF(RIGHT(po_no_withplant,4) BETWEEN '2007' AND '2020', " & _
                "            MID(po_no_withplant,1,LENGTH(po_no_withplant)-4),'')) po_no_withyear, " & _
                "   IF(stryear<>'',stryear, " & _
                "       IF(RIGHT(po_no_withplant,4) BETWEEN '2007' AND '2020', " & _
                "           RIGHT(po_no_withplant, 2), '')) stryear, " & _
                "   IF(stradd1y<>'',stradd1y, " & _
                "       IF(RIGHT(po_no_withplant,4) BETWEEN '2007' AND '2020', " & _
                "           LEFT(RIGHT(po_no_withplant, 4),1),'')) stradd1y " & _
                " "
                
    SQLstr2 = SQLstr2 & _
                "  FROM " & _
                "    (SELECT t1.po_no, " & _
                "     IF(INSTR(t1.po_no,'(') > 0, " & _
                "        MID(t1.po_no,1,INSTR(t1.po_no,'(')-1) " & _
                "        ,'') po_no_withplant, " & _
                "     IF(RIGHT(t1.po_no,4) BETWEEN '2007' AND '2020', " & _
                "        MID(t1.po_no,1,LENGTH(t1.po_no)-4) " & _
                "        ,'') po_no_withyear, " & _
                "     IF(RIGHT(t1.po_no,4) BETWEEN '2007' AND '2020', " & _
                "       RIGHT(t1.po_no, 2) " & _
                "       ,'') stryear, " & _
                "     IF(RIGHT(t1.po_no,4) BETWEEN '2007' AND '2020', " & _
                "       LEFT(RIGHT(t1.po_no, 4),1)        ,'') stradd1y " & _
                "    FROM tbl_po t1 WHERE t1.status <> 'Rejected' AND t1.status <> 'Closed') t1 " & _
                " ) t1 "
                
    SQLstr2 = "SELECT t1.po_no po_org, " & _
        "IF(t1.po_no_withplant<>'',t1.po_no_withplant, " & _
        "  IF(t1.po_no_withyear_s<>'',t1.po_no_withyear_s, " & _
        "    IF(t1.po_no_withyear_d<>'',t1.po_no_withyear_d, " & _
        "      IF(t1.po_no_withyear_k<>'',t1.po_no_withyear_k, " & _
        "        IF(t1.po_no_withyear_t<>'',t1.po_no_withyear_t, " & _
        "          IF(t1.po_no_withyear_int<>'',t1.po_no_withyear_int, " & _
        "            IF(t1.po_no_withyear_pmx<>'',t1.po_no_withyear_pmx, " & _
        "              IF(t1.po_no_withyear_sbg<>'',t1.po_no_withyear_sbg, " & _
        "                IF(t1.po_no_withyear_jkt<>'',t1.po_no_withyear_jkt, " & _
        "                  IF(t1.po_no_withyear<>'',t1.po_no_withyear, t1.po_no13)))))))))) po_no FROM (" & SQLstr2 & ") t1 " & _
        " "

    SQLstr = _
        "SELECT t2.po_org, t2.po_no, t1.supplier_code, t1.supplier_name, if(t1.po_dt is null,'',t1.po_dt) po_dt, t1.GUDID, t1.currency_code, ROUND(t1.po_amount,2) po_amount, IF((m1.effective_kurs IS NULL OR m1.effective_kurs = 0), 0, ROUND(m1.effective_kurs,2)) kurs_po, IF((m1.effective_kurs IS NULL OR m1.effective_kurs = 0), 0, ROUND(t1.po_amount * m1.effective_kurs,2)) po_amount_idr, IF(t1.ppn = 0, 0, ROUND(t1.ppn,2)) ppn, t1.COA1, t1.COA2, t1.COA3, t1.plant_name " & _
        "FROM " & _
        "(" & SQLstr1 & ") t1 " & _
        "LEFT JOIN (" & SQLstr2 & ") t2 ON t1.po_no=t2.po_org " & _
        "LEFT JOIN tbm_kurs m1 ON m1.currency_code=t1.currency_code AND m1.effective_date=t1.purchaseddt " & _
        "ORDER BY t1.po_no " & _
        " "

    'Nomor PO yang di ambil adalah apa adanya. Query pemotongan PO untuk pengembangan.
    'Sehingga SQLstr bisa di ambil dari SQLstr1 saja!

    i = 0
    Set rsdata1 = cnn1.Execute(SQLstr)
    If Not rsdata1.EOF Then
        Open nmFile For Output As 1
        Do While Not rsdata1.EOF

            amount = rsdata1("po_amount")
            amount = Replace(amount, ",", ".")
            kurs = rsdata1("kurs_po")
            kurs = Replace(kurs, ",", ".")
            amount_idr = rsdata1("po_amount_idr")
            amount_idr = Replace(amount_idr, ",", ".")
            ppn = rsdata1("ppn")
            ppn = Replace(ppn, ",", ".")

            po_ori = rsdata1("po_org")
            po = rsdata1("po_no")
            
            'aturan di Theos di batasi 13 digit
            If Len(po) > 13 Then po = Replace(po, "-", "")
            
            supp = "I" & rsdata1("supplier_code")
            'aturan di Theos di batasi 30 digit
            suppnm = rsdata1("supplier_name")
            podt = "" & rsdata1("po_dt") & ""
            xcurr = rsdata1("currency_code")
            gudid = rsdata1("GUDID")
            coa1 = rsdata1("COA1")
            coa2 = rsdata1("COA2")
            coa3 = rsdata1("COA3")
            plant = rsdata1("plant_name")

            If po <> "" Then
            'create line :1.No PO, 2.Kode Supplier, 3.Tgl PO, 4.GUDID, 5.Currency, 6.Amount, 7.Kurs CostSlip, 8.(Amount * Kurs CostSlip), 9.PPN, 10.Supplier Name, 11.COA2, 12.COA3, 13.PLANT
                If (podt <> "" And kurs > 0) Then
                    strA = po & "," & supp & "," & podt & "," & gudid & "," & xcurr & "," & amount & "," & kurs & "," & amount_idr & "," & ppn & "," & suppnm & "," & coa2 & "," & coa3 & "," & plant
                    Print #1, strA
                    
                    i = i + 1
                    SQLstr3 = "INSERT INTO temp_tbr_toap2 (plant_code, file_code, line_no, line_text) " & _
                            "VALUE ('" & plCode & "','IMPO'," & i & ",'" & strA & "')"
                    
                    Set rsdata3 = cnnWr.Execute(SQLstr3)
                Else
                    errPO = errPO & ", " & po_ori
                End If
            End If
            rsdata1.MoveNext
        Loop
        Close #1
        nmF1 = nmF
    End If

    '-------------------------------------------------------
    'create IMDP
    nmF = "Imdp" & dtFile & "." & plFile
    nmFile = "C:\" & nmF
    
    Set rsdata1 = cnn1.Execute(SQLstr)
    If Not rsdata1.EOF Then
        Open nmFile For Output As 1
        Do While Not rsdata1.EOF
            amount = rsdata1("po_amount")
            amount = Replace(amount, ",", ".")

            'po = rsdata1("po_org")
            po = rsdata1("po_no")
            
            'aturan di Theos di batasi 13 digit
            If Len(po) > 13 Then po = Replace(po, "-", "")
            
            podt = "" & rsdata1("po_dt") & ""
            ttdt = ""
            xcurr = rsdata1("currency_code")
            plant = rsdata1("plant_name")

            kurs = rsdata1("kurs_po")
            If po <> "" Then
                If (podt <> "" And kurs > 0) Then
                'create line :1.No PO, 2.'1', 3.Tgl PO, 4.Currency, 5.Amount, 6.PLANT
                    strA = po & ", 1 ," & podt & "," & xcurr & "," & amount & "," & plant
                    Print #1, strA
                End If
            End If
            rsdata1.MoveNext
        Loop
        Close #1
        nmF2 = nmF
    End If
    
    '-------------------------------------------------------
    'create IMBP
    nmF = "Imbp" & dtFile & "." & plFile
    nmFile = "C:\" & nmF
    
    'tambahan format invoice sejak 3 Maret 2011 jika hanya ada 1 no invoice yg sama tidak perlu di tambahkan #1, #2 dstnya.
    SQLstr1 = _
        "SELECT 'INV' flag, t0.est_delivery_dt, " & _
        "CONCAT(t1.invoice_no , '#' , getorder(t2.shipment_no, t2.po_no)) invoice_bef, " & _
        "CONCAT(t1.invoice_no , getinvorder(t2.shipment_no, t2.po_no)) invoice_aft, " & _
        "CAST(DATE_FORMAT(t1.invoice_dt,'%d-%m-%Y') AS CHAR) invoice_dt, t1.invoice_dt beginrule, '' GUDTPB, t1.po_no, t0.currency_code, (t3.price * t3.quantity) po_amount, " & _
        "(t1.invoice_amount-t1.invoice_penalty) invoice_gross, trim(t0.supplier_code) supplier_code, MID(trim(m2.supplier_name),1,30) supplier_name, t0.vat ppn, IF(t0.tt_dt IS NULL, '', CAST(DATE_FORMAT(t0.tt_dt,'%d-%m-%Y') AS CHAR))  tt_dt, '' BOB, '' FCANCTPB, '' CANCNTPB,  m1.plant_name, " & _
        "((t1.invoice_amount-t1.invoice_penalty) - (t0.finalty * ((t1.invoice_amount-t1.invoice_penalty) / " & _
        "                     (SELECT SUM(st1.invoice_amount-st1.invoice_penalty) FROM tbl_shipping_invoice st1 WHERE st1.shipment_no=t1.shipment_no)) " & _
        ")) AS invoice_amount, t1.invoice_penalty, CAST(DATE_FORMAT(t4.purchaseddt,'%d-%m-%Y') AS CHAR) po_dt " & _
        "FROM tbl_shipping t0, tbl_shipping_invoice t1, tbl_shipping_detail t2, tbl_po_detail t3, tbl_po t4, tbm_plant m1, tbm_supplier m2 " & _
        "WHERE t0.shipment_no = t1.shipment_no " & _
        "AND t1.shipment_no=t2.shipment_no AND t1.po_no=t2.po_no AND t1.ord_no=t2.po_item " & _
        "AND t2.po_no=t3.po_no AND t2.material_code=t3.material_code " & _
        "AND t3.po_no=t4.po_no " & _
        "AND t0.plant_code = m1.plant_code AND t0.supplier_code = m2.supplier_code " & _
        "AND t0.plant_code = '" & plCode & "' AND (DATE_FORMAT(t1.invoice_dt,'%Y-%m-%d') BETWEEN '" & dtFrom & "' AND '" & dtTo & "') " & _
        " "

    SQLstr = _
        "SELECT t2.po_org, t2.po_no, if(t1.po_dt is null,'',t1.po_dt) po_dt, t1.flag, t1.est_delivery_dt, " & _
        "IF(t1.beginrule>='2011-03-03', IF(LENGTH(t1.invoice_aft)>20,REPLACE(t1.invoice_aft,' ',''),t1.invoice_aft), t1.invoice_bef) invoice_no, " & _
        "t1.invoice_dt, t1.GUDTPB, t1.currency_code, ROUND(t1.po_amount,2) po_amount, ROUND(t1.invoice_gross,2) invoice_gross, ROUND(t1.invoice_amount-t1.invoice_penalty,2) invoice_amount, IF(m1.effective_kurs IS NULL OR m1.effective_kurs = 0, 0, ROUND(m1.effective_kurs,2)) kurs_cs, t1.supplier_code, t1.supplier_name, IF((m1.effective_kurs IS NULL OR m1.effective_kurs = 0), 0, ROUND(t1.invoice_amount * m1.effective_kurs,2)) invoice_amount_idr, IF(t1.ppn = 0, 0, ROUND(t1.ppn,2)) ppn, t1.tt_dt, t1.BOB, t1.FCANCTPB, t1.CANCNTPB, t1.plant_name " & _
        "FROM " & _
        "(" & SQLstr1 & ") t1 " & _
        "LEFT JOIN (" & SQLstr2 & ") t2 ON t1.po_no=t2.po_org " & _
        "LEFT JOIN tbm_kurs m1 ON m1.currency_code=t1.currency_code AND m1.effective_date=t1.est_delivery_dt " & _
        "ORDER BY t1.invoice_aft, t2.po_org " & _
        " "

    'Nomor PO yang di ambil adalah apa adanya. Query pemotongan PO untuk pengembangan.
    'Sehingga SQLstr bisa di ambil dari SQLstr1 saja!

    i = 0
    Set rsdata1 = cnn1.Execute(SQLstr)
    If Not rsdata1.EOF Then
        Open nmFile For Output As 1
        Do While Not rsdata1.EOF

            amount = rsdata1("po_amount")
            amount = Replace(amount, ",", ".")
            kurs = rsdata1("kurs_cs")
            kurs = Replace(kurs, ",", ".")
            ppn = rsdata1("ppn")
            ppn = Replace(ppn, ",", ".")
            amount_inv = rsdata1("invoice_amount")
            amount_inv = Replace(amount_inv, ",", ".")

            xFlag = rsdata1("flag")
            invno = rsdata1("invoice_no")
            invdt = "" & rsdata1("invoice_dt") & ""
            gudtpb = rsdata1("GUDTPB")
            'po = rsdata1("po_org")
            po = rsdata1("po_no")
            
            'aturan di Theos di batasi 13 digit
            If Len(po) > 13 Then po = Replace(po, "-", "")
            
            podt = "" & rsdata1("po_dt") & ""
            xcurr = rsdata1("currency_code")
            supp = "I" & rsdata1("supplier_code")
            'aturan di Theos di batasi 30 digit
            suppnm = rsdata1("supplier_name")
            bob = rsdata1("BOB")
            fcanctpb = rsdata1("FCANCTPB")
            cacntpb = rsdata1("CANCNTPB")
            plant = rsdata1("plant_name")
            ttdt = "" & rsdata1("tt_dt") & ""

            'create line :1.'INV', 2.No Invoice, 3.Tgl Invoice, 4.GUDTPB, 5.No PO, 6.Tgl PO, 7.Currency, 8.ORIGPOTBB, 9.Kurs CostSlip, 10.Kode Supplier, 11.ORIAMTPB, 12.PPN, 13.DueDate, 14.BOB, 15.FCANCTPB, 16.CANCNTPB, 17.PLANT
            strA = xFlag & "," & invno & "," & invdt & "," & gudtpb & "," & po & "," & podt & "," & xcurr & "," & amount & "," & kurs & "," & supp & "," & amount_inv & "," & ppn & "," & ttdt & "," & bob & "," & fcanctpb & "," & cacntpb & "," & plant & "," & suppnm
            Print #1, strA

            i = i + 1
            SQLstr3 = "INSERT INTO temp_tbr_toap2 (plant_code, file_code, line_no, line_text) " & _
                            "VALUE ('" & plCode & "','IMBP'," & i & ",'" & strA & "')"
                    
            Set rsdata3 = cnnWr.Execute(SQLstr3)

            rsdata1.MoveNext
        Loop
        Close #1
        nmF3 = nmF
    End If

    '-------------------------------------------------------
    'create IMDB
    nmF = "Imdb" & dtFile & "." & plFile
    nmFile = "C:\" & nmF

    Set rsdata1 = cnn1.Execute(SQLstr)
    If Not rsdata1.EOF Then
        Open nmFile For Output As 1
        Do While Not rsdata1.EOF

            xFlag = rsdata1("flag")
            invno = rsdata1("invoice_no")
            invdt = "" & rsdata1("invoice_dt") & ""
            'po = rsdata1("po_org")
            po = rsdata1("po_no")
            
            'aturan di Theos di batasi 13 digit
            If Len(po) > 13 Then po = Replace(po, "-", "")
            
            ttdt = "" & rsdata1("tt_dt") & ""
            xcurr = rsdata1("currency_code")
            amount_inv = rsdata1("invoice_amount")
            amount_inv = Replace(amount_inv, ",", ".")
            plant = rsdata1("plant_name")

            'create line :1.'INV', 2.No Invoice, 3.No PO, 4.DueDate, 5.Currency, 6.ORIAMTPB, 7.PLANT
            strA = xFlag & "," & invno & "," & po & "," & ttdt & "," & xcurr & "," & amount_inv & "," & plant
            Print #1, strA

            rsdata1.MoveNext
        Loop
        Close #1
        nmF4 = nmF
    End If

    If ErrMsg = "" Then
        Dim ipsvr, usersvr, passsvr As String
        Dim FtpFile As String

        SQLstr = "SELECT ipsvr, usersvr, passsvr FROM tbm_plant WHERE plant_code= '" & plCode & "'"
        Set rsdata1 = cnn1.Execute(SQLstr)
        If Not rsdata1.EOF Then
            Do While Not rsdata1.EOF
                ipsvr = Trim(rsdata1("ipsvr"))
                usersvr = rsdata1("usersvr")
                passsvr = rsdata1("passsvr")
                rsdata1.MoveNext
            Loop
            
            'cek jika tidak ada perubahan data, file tidak di transfer
            SQLstr = "SELECT count(line_no) tot1, (SELECT IF(MAX(line_no) IS NULL,0,MAX(line_no)) FROM tbr_toap2 WHERE plant_code='" & plCode & "' AND file_code='IMPO') tot2 " & _
                     "FROM temp_tbr_toap2 WHERE plant_code='" & plCode & "' AND file_code='IMPO' " & _
                     "AND line_text IN (SELECT line_text FROM tbr_toap2 WHERE plant_code='" & plCode & "' AND file_code='IMPO')"

            Set rsdata1 = cnn1.Execute(SQLstr)
            If Not rsdata1.EOF Then If rsdata1("tot1") = rsdata1("tot2") And rsdata1("tot1") > 0 Then LnPO = False
            
            SQLstr = "SELECT count(line_no) tot1, (SELECT IF(MAX(line_no) IS NULL,0,MAX(line_no)) FROM tbr_toap2 WHERE plant_code='" & plCode & "' AND file_code='IMBP') tot2 " & _
                     "FROM temp_tbr_toap2 WHERE plant_code='" & plCode & "' AND file_code='IMBP' " & _
                     "AND line_text IN (SELECT line_text FROM tbr_toap2 WHERE plant_code='" & plCode & "' AND file_code='IMBP')"

            Set rsdata1 = cnn1.Execute(SQLstr)
            If Not rsdata1.EOF Then If rsdata1("tot1") = rsdata1("tot2") And rsdata1("tot1") > 0 Then LnPO = False
            
            '' transfer di lakukan setiap saat saja tidak tergantung kondisi data yg berubah
            ''If LnPO Or LnBP Then
                'refresh table pembanding untuk proses berikutnya
                SQLstr = "DELETE FROM tbr_toap2 WHERE plant_code='" & plCode & "'"
                Set rsdata1 = cnn1.Execute(SQLstr)
                SQLstr = "INSERT INTO tbr_toap2 (SELECT * FROM temp_tbr_toap2 WHERE plant_code='" & plCode & "')"
                Set rsdata1 = cnn1.Execute(SQLstr)
             
                If ipsvr <> "" Then
                    FtpFile = "C:\ftpCom" & plno & ".ftp"
                    Open FtpFile For Output As 1
                    Print #1, "open " & ipsvr '-------> open FTP site/location
                    Print #1, usersvr '--------------> ftp user name
                    Print #1, passsvr '--------------> ftp user password
                    Print #1, "lcd C:\"
                    Print #1, "put " & nmF1
                    Print #1, "put " & nmF2
                    Print #1, "put " & nmF3
                    Print #1, "put " & nmF4
                    Print #1, "bye" '--------------> bye //ftp command to come out of the ftp command promt
                    Close #1

                    'FtpFile = "C:\ftpCommand1.ftp"
                    ProgId = Shell("ftp.exe -i -s:" & FtpFile, vbHide)
                    If ProgId > 0 Then
                        ErrMsg = "Transfered to " & ipsvr & " succesfully. "
                    Else
                        ErrMsg = "Transfered file fail. Server " & ipsvr & " not found. Please manual transfer from c:\. "
                    End If
                Else
                    ErrMsg = "Server not identified. Please manual transfer from c:\. "
                End If
            ''Else
            ''    ErrMsg = "No change from previous data. Files on c:\ is not in the transfer."
            ''End If
        End If
    End If

    'create log
    '-------------------------------------------------------
    If errPO <> "" Then
        errPO = "PO Plant " & plCode & " - " & plFile & " berikut : " & Mid(errPO, 2, Len(errPO)) & " belum dapat di transfer ke server AP2 (" & ipsvr & "). Data tidak di transfer karena Date of Purchase belum di lengkapi dan/atau nilai kurs belum di isi pada tanggal tersebut. "
        err2PO = err2PO & errPO
        
        nmF = "ErrLog" & dtFile & "." & plFile
        nmFile = "C:\" & nmF
        Open nmFile For Output As 1
        Print #1, errPO
        Print #1, ErrMsg
        Close #1
    End If

    rsdata2.MoveNext
  Loop
  
  SQLstr3 = "UPDATE tbm_status_jobproses SET stat_text=trim('" & err2PO & "') WHERE stat_code='AP'"
  Set rsdata3 = cnnWr.Execute(SQLstr3)
   
End If

Set rsdata1 = Nothing
Set rsdata2 = Nothing
Set rsdata3 = Nothing
Set cnn1 = Nothing
Set cnnWr = Nothing
Unload Me
End Sub















