Imports System.Data.OleDb
Imports System.Management
Imports System.Text.RegularExpressions

Imports System.IO

Public Class DataAP2
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String

    Dim PilihanDlg As New DlgPilihan

    Dim ErrMsg, SQLstr, SQLstrA, SQLstr1, SQLstr2 As String
    Dim MyReader As MySqlDataReader
    Dim v_idtable As String = "Synchronize AP2"
    Dim ErrPO As String = ""
    Dim affrow As Integer

    Sub New()
        Dim ad As String

        InitializeComponent()

        lblProcess.Text = "Create dump file for Transaction"
        listError.Items.Clear()
        txtID.Text = "0"

        dt1.Value = Now()
        'dt2_1.Value = DateAdd(DateInterval.Month, -1, Now)
        'dt2_1.Value = Month(dt1.Value) & "/01/" & Year(dt1.Value)
        'dt2_1.Value = "01/01/" & Year(dt1.Value)
        dt2_1.Value = DateAdd("m", -1, Now())
        dt2_2.Value = Now()
        txtFolder_Name1.Clear()
        txtFolder_Name2_1.Clear()

        Label3.Visible = False
        listError.Visible = False
    End Sub

    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshScreen()
    End Sub

    Private Sub RefreshScreen()
        txtFolder_Name1.Clear()
        txtFolder_Name2_1.Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub

    Private Sub TabControl1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.Click
        lblProcess.Text = "Create dump file for " & TabControl1.SelectedTab.Text
        txtID.Text = TabControl1.SelectedIndex.ToString
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If txtID.Text = "0" Then
            ProcessTransaction()
        ElseIf txtID.Text = "1" Then
            ProcessSupplier()
        End If
    End Sub

    Private Sub ProcessSupplier()
        Dim supcode, supname As String

        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        Dim oRead As System.IO.StreamReader
        Dim nmFile As String

        txtFolder_Name1.Clear()

        If TxtPlantCode2.Text = "" Then
            MsgBox("Select plant name first", MsgBoxStyle.Information, "Information")
            Exit Sub
        End If

        nmFile = "C:\Supplier.txt"

        SQLstr = "SELECT trim(supplier_code) supplier_code, supplier_name " & _
                 "FROM tbm_supplier WHERE supplier_code IN " & _
                 "  (SELECT DISTINCT supplier_code FROM tbl_po WHERE plant_code='" & TxtPlantCode2.Text & "' " & _
                 "   UNION " & _
                 "   SELECT DISTINCT supplier_code FROM tbl_shipping t1, tbl_shipping_plant t2 WHERE t1.shipment_no=t2.shipment_no AND t2.plant_code='" & TxtPlantCode2.Text & "' " & _
                 "  ) "

        ErrMsg = "Failed when read File Data. "
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            If oFile.Exists(nmFile) Then oFile.Delete(nmFile)
            oWrite = oFile.CreateText(nmFile)

            While MyReader.Read
                supcode = "I" & MyReader.GetString("supplier_code")
                supname = Replace(MyReader.GetString("supplier_name"), ",", " ")
                'create line : code, name, coa1, coa2, coa3
                oWrite.WriteLine(supcode & "," & supname & ",,,")
            End While
            CloseMyReader(MyReader, UserData)
            oWrite.Close()
            txtFolder_Name1.Text = "C:\Supplier.txt"
        End If
    End Sub

    Private Sub ProcessTransaction()
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        Dim oRead As System.IO.StreamReader
        Dim nmF, nmFile, nmFiles As String
        Dim nmF1, nmF2, nmF3, nmF4 As String
        Dim plFile, dtFrom, dtFrom3, dtTo, dtFile As String
        Dim po, po_ori, supp, suppnm, podt, plant, coa1, coa2, coa3 As String
        Dim amount_idr, amount, kurs, amount_inv, ppn As String
        Dim xFlag, invno, invdt, gudtpb, xcurr, bob, fcanctpb, cacntpb, ttdt, gudid As String

        listError.Items.Clear()
        txtFolder_Name2_1.Text = ""
        txtFolder_Name2_2.Text = ""
        Label3.Visible = True
        listError.Visible = True

        If TxtPlantCode.Text = "" Then
            MsgBox("Select plant name first", MsgBoxStyle.Information, "Information")
            Exit Sub
        End If

        If ((Format(dt2_1.Value, "yyyy") < "2010") Or (Format(dt2_2.Value, "yyyy") < "2010")) Then
            MsgBox("Before 2010 can not be in the process", MsgBoxStyle.Information, "Information")
            Exit Sub
        End If

        nmFiles = "No file created. Please another Plant dan Period Transaction"
        plFile = Replace(TxtPlantName.Text, " ", "")
        dtFrom = Format(dt2_1.Value, "yyyy-MM-dd")
        dtTo = Format(dt2_2.Value, "yyyy-MM-dd")

        dt2_3.Value = DateAdd(DateInterval.Month, -1, dt2_1.Value)
        dtFrom3 = Format(dt2_3.Value, "yyyy-MM-dd")

        'dtFile = Mid(dtFrom, 9, 2) & Mid(dtFrom, 6, 2)
        dtFile = Format(Now, "yyyy-MM-dd")
        dtFile = Mid(dtFile, 9, 2) & Mid(dtFile, 6, 2)

        txtFolder_Name2_1.Clear()

        'create IMPO yaitu PO2 yang belum ada shipmentnya.
        nmF = "Impo" & dtFile & "." & plFile
        nmFile = "C:\" & nmF

        SQLstr1 = _
                "SELECT t1.po_no, trim(t1.supplier_code) supplier_code, MID(trim(supplier_name),1,30) supplier_name, t1.purchaseddt, CAST(DATE_FORMAT(t1.purchaseddt,'%d-%m-%Y') AS CHAR) po_dt, 0 GUDID, currency_code, " & _
                "(SELECT SUM(st.price * st.quantity) FROM tbl_po_detail st WHERE st.po_no=t1.po_no) po_amount, 0 kurs_po, 0 ppn, '' COA1, '' COA2, '' COA3, m1.plant_name " & _
                "FROM tbl_po t1, tbm_plant m1, tbm_supplier m2  " & _
                "WHERE t1.plant_code = m1.plant_code " & _
                "AND t1.supplier_code = m2.supplier_code " & _
                "AND t1.status <> 'Rejected' and t1.status <> 'Closed' " & _
                "AND (DATE_FORMAT(t1.purchaseddt,'%Y') > 2010 OR t1.purchaseddt IS NULL) " & _
                "AND t1.po_no NOT IN (SELECT distinct po_no FROM tbl_shipping_detail) " & _
                "AND t1.plant_code = '" & TxtPlantCode.Text & "' " & _
                " "
        'AND (DATE_FORMAT(t1.purchaseddt,'%Y-%m-%d') BETWEEN '" & dtFrom & "' AND '" & dtTo & "') " & _

        SQLstr2 = _
                "SELECT po_no, MID(po_no,1,13) po_no13, po_no_withplant, " & _
                " IF(stryear <> '', CONCAT(po_no_withyear,stryear),'') po_no_withyear, " & _
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

        SQLstrA = _
        "SELECT t2.po_org, t2.po_no, trim(t1.supplier_code) supplier_code, supplier_name, if(t1.po_dt is null,'',t1.po_dt) po_dt, t1.GUDID, t1.currency_code, ROUND(t1.po_amount,2) po_amount, IF((m1.effective_kurs IS NULL OR m1.effective_kurs = 0), 0, ROUND(m1.effective_kurs,2)) kurs_po, IF((m1.effective_kurs IS NULL OR m1.effective_kurs = 0), 0, ROUND(t1.po_amount * m1.effective_kurs,2)) po_amount_idr, IF(t1.ppn = 0, 0, ROUND(t1.ppn,2)) ppn, t1.COA1, t1.COA2, t1.COA3, t1.plant_name " & _
        "FROM " & _
        "(" & SQLstr1 & ") t1 " & _
        "LEFT JOIN (" & SQLstr2 & ") t2 ON t1.po_no=t2.po_org " & _
        "LEFT JOIN tbm_kurs m1 ON m1.currency_code=t1.currency_code AND m1.effective_date=t1.purchaseddt " & _
        "ORDER BY t1.po_no " & _
        " "

        'Nomor PO yang di ambil adalah apa adanya. Query pemotongan PO untuk pengembangan. 
        'Sehingga SQLstr bisa di ambil dari SQLstr1 saja!

        ErrMsg = "Not found Outstanding PO. "
        MyReader = DBQueryMyReader(SQLstrA, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If oFile.Exists(nmFile) Then oFile.Delete(nmFile)
            oWrite = oFile.CreateText(nmFile)
            While MyReader.Read
                amount = MyReader.GetString("po_amount")
                amount = Replace(amount, ",", ".")
                kurs = MyReader.GetString("kurs_po")
                kurs = Replace(kurs, ",", ".")
                amount_idr = MyReader.GetString("po_amount_idr")
                amount_idr = Replace(amount_idr, ",", ".")
                ppn = MyReader.GetString("ppn")
                ppn = Replace(ppn, ",", ".")

                po_ori = MyReader.GetString("po_org")
                po = MyReader.GetString("po_no")
                '---aturan Theos mesti 13 digits -------------
                If Len(po) > 13 Then po = Replace(po, "-", "")

                supp = "I" & MyReader.GetString("supplier_code")
                '---aturan Theos mesti 30 digits -------------
                suppnm = MyReader.GetString("supplier_name")
                podt = "" & MyReader.GetString("po_dt") & ""
                xcurr = MyReader.GetString("currency_code")
                gudid = MyReader.GetString("GUDID")
                coa1 = MyReader.GetString("COA1")
                coa2 = MyReader.GetString("COA2")
                coa3 = MyReader.GetString("COA3")
                plant = MyReader.GetString("plant_name")

                If po <> "" Then
                    'create line :1.No PO, 2.Kode Supplier, 3.Tgl PO, 4.GUDID, 5.Currency, 6.Amount, 7.Kurs CostSlip, 8.(Amount * Kurs CostSlip), 9.PPN, 10.COA1, 11.COA2, 12.COA3, 13.PLANT
                    If (podt <> "" And kurs > 0) Then
                        oWrite.WriteLine(po & "," & supp & "," & podt & "," & _
                                    "" & gudid & "," & xcurr & "," & amount & "," & _
                                    "" & kurs & "," & amount_idr & "," & ppn & "," & _
                                    "" & suppnm & "," & coa2 & "," & coa3 & "," & plant)

                    Else
                        ErrPO = ErrPO & ", " & po_ori
                    End If
                    ErrMsg = ""
                End If
            End While
            CloseMyReader(MyReader, UserData)
            oWrite.Close()
            nmFiles = nmFile
        End If

        If ErrMsg <> "" Then
            listError.Items.Add(ErrMsg)
        Else
            txtFolder_Name2_1.Text = nmFiles
            nmF1 = nmF
            'FtpProcess(nmF)
        End If

        '-------------------------------------------------------
        'create IMDP
        nmF = "Imdp" & dtFile & "." & plFile
        nmFile = "C:\" & nmF

        ErrMsg = "Not found Outstanding PO. "
        MyReader = DBQueryMyReader(SQLstrA, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If oFile.Exists(nmFile) Then oFile.Delete(nmFile)
            oWrite = oFile.CreateText(nmFile)
            While MyReader.Read
                amount = MyReader.GetString("po_amount")
                amount = Replace(amount, ",", ".")

                po_ori = MyReader.GetString("po_org")
                po = MyReader.GetString("po_no")
                '---aturan Theos mesti 13 digits -------------
                If Len(po) > 13 Then po = Replace(po, "-", "")

                podt = "" & MyReader.GetString("po_dt") & ""
                ttdt = ""
                xcurr = MyReader.GetString("currency_code")
                plant = MyReader.GetString("plant_name")

                kurs = MyReader.GetString("kurs_po")
                If po <> "" Then
                    If (podt <> "" And kurs > 0) Then
                        'create line :1.No PO, 2.'1', 3.Tgl PO, 4.Currency, 5.Amount, 6.PLANT
                        oWrite.WriteLine(po & ", 1 ," & podt & "," & _
                                    "" & xcurr & "," & amount & "," & plant)
                        'Else
                        'tidak perlu karena sudah di handle saat buat IMPO
                        'ErrPO = ErrPO & ", " & po_ori
                    End If
                    ErrMsg = ""
                End If
            End While
            CloseMyReader(MyReader, UserData)
            oWrite.Close()
            nmFiles = nmFile
        End If

        If ErrMsg <> "" Then
            listError.Items.Add(ErrMsg)
        Else
            txtFolder_Name2_3.Text = nmFiles
            nmF2 = nmF
            'FtpProcess(nmF)
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
               "AND t0.plant_code = m1.plant_code " & _
               "AND t0.supplier_code = m2.supplier_code " & _
               "AND t0.plant_code = '" & TxtPlantCode.Text & "' AND (DATE_FORMAT(t1.invoice_dt,'%Y-%m-%d') BETWEEN '" & dtFrom & "' AND '" & dtTo & "') " & _
               " "

        SQLstrA = _
        "SELECT t2.po_org, t2.po_no, if(t1.po_dt is null,'',t1.po_dt) po_dt, t1.flag, t1.est_delivery_dt, " & _
        "IF(t1.beginrule>='2011-03-03', IF(LENGTH(t1.invoice_aft)>20,REPLACE(t1.invoice_aft,' ',''),t1.invoice_aft), t1.invoice_bef) invoice_no, " & _
        "t1.invoice_dt, t1.GUDTPB, t1.currency_code, ROUND(t1.po_amount,2) po_amount, ROUND(t1.invoice_gross,2) invoice_gross, ROUND(t1.invoice_amount-t1.invoice_penalty,2) invoice_amount, IF(m1.effective_kurs IS NULL OR m1.effective_kurs = 0, 0, ROUND(m1.effective_kurs,2)) kurs_cs, t1.supplier_code, t1.supplier_name, IF((m1.effective_kurs IS NULL OR m1.effective_kurs = 0), 0, ROUND(t1.invoice_amount * m1.effective_kurs,2)) invoice_amount_idr, IF(t1.ppn = 0, 0, ROUND(t1.ppn,2)) ppn, t1.tt_dt, t1.BOB, t1.FCANCTPB, t1.CANCNTPB, t1.plant_name " & _
        "FROM " & _
        "(" & SQLstr1 & ") t1 " & _
        "LEFT JOIN (" & SQLstr2 & ") t2 ON t1.po_no=t2.po_org " & _
        "LEFT JOIN tbm_kurs m1 ON m1.currency_code=t1.currency_code AND m1.effective_date=t1.est_delivery_dt " & _
        "WHERE t2.po_org IS NOT NULL " & _
        "ORDER BY t1.invoice_aft, t2.po_org " & _
        " "

        'Nomor PO yang di ambil adalah apa adanya. Query pemotongan PO untuk pengembangan. 
        'Sehingga SQLstr bisa di ambil dari SQLstr1 saja!

        ErrMsg = "Not found Invoice. "
        MyReader = DBQueryMyReader(SQLstrA, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If oFile.Exists(nmFile) Then oFile.Delete(nmFile)
            oWrite = oFile.CreateText(nmFile)

            While MyReader.Read
                amount = MyReader.GetString("po_amount")
                amount = Replace(amount, ",", ".")
                kurs = MyReader.GetString("kurs_cs")
                kurs = Replace(kurs, ",", ".")
                ppn = MyReader.GetString("ppn")
                ppn = Replace(ppn, ",", ".")
                amount_inv = MyReader.GetString("invoice_amount")
                amount_inv = Replace(amount_inv, ",", ".")

                xFlag = MyReader.GetString("flag")
                invno = MyReader.GetString("invoice_no")
                invdt = "" & MyReader.GetString("invoice_dt") & ""
                gudtpb = MyReader.GetString("GUDTPB")
                'po = MyReader.GetString("po_org")
                po = MyReader.GetString("po_no")
                '---aturan Theos mesti 13 digits -------------
                If Len(po) > 13 Then po = Replace(po, "-", "")

                podt = "" & MyReader.GetString("po_dt") & ""
                xcurr = MyReader.GetString("currency_code")
                supp = "I" & MyReader.GetString("supplier_code")
                '---aturan Theos mesti 30 digits -------------
                suppnm = MyReader.GetString("supplier_name")
                bob = MyReader.GetString("BOB")
                fcanctpb = MyReader.GetString("FCANCTPB")
                cacntpb = MyReader.GetString("CANCNTPB")
                plant = MyReader.GetString("plant_name")
                ttdt = "" & MyReader.GetString("tt_dt") & ""

                'create line :1.'INV', 2.No Invoice, 3.Tgl Invoice, 4.GUDTPB, 5.No PO, 6.Tgl PO 7.Currency, 8.ORIGPOTBB, 9.Kurs CostSlip, 10.Kode Supplier, 11.ORIAMTPB, 12.PPN, 13.DueDate, 14.BOB, 15.FCANCTPB, 16.CANCNTPB, 17.PLANT
                oWrite.WriteLine(xFlag & "," & invno & "," & invdt & "," & _
                            "" & gudtpb & "," & po & "," & podt & "," & _
                            "" & xcurr & "," & amount & "," & kurs & "," & supp & "," & _
                            "" & amount_inv & "," & ppn & "," & ttdt & "," & _
                            "" & bob & "," & fcanctpb & "," & cacntpb & "," & plant & "," & suppnm)


                If po <> "" Then ErrMsg = ""
            End While
            CloseMyReader(MyReader, UserData)
            oWrite.Close()
            nmFiles = nmFile
        End If
        MyReader = Nothing

        If ErrMsg <> "" Then
            listError.Items.Add(ErrMsg)
        Else
            txtFolder_Name2_2.Text = nmFiles
            nmF3 = nmF
            'FtpProcess(nmF)
            '-------------------------------------------------------
            If ErrPO <> "" Then ErrPO = "PO berikut : " & Mid(ErrPO, 2, Len(ErrPO)) & " belum dapat di transfer ke server AP2. Data tidak di transfer karena Date of Purchase belum di lengkapi dan/atau nilai kurs belum di isi pada tanggal tersebut"
            listError.Items.Add(ErrPO)
        End If

        '-------------------------------------------------------
        'create IMDB
        nmF = "Imdb" & dtFile & "." & plFile
        nmFile = "C:\" & nmF


        ErrMsg = "Not found Invoice. "
        MyReader = DBQueryMyReader(SQLstrA, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            If oFile.Exists(nmFile) Then oFile.Delete(nmFile)
            oWrite = oFile.CreateText(nmFile)
            While MyReader.Read

                xFlag = MyReader.GetString("flag")

                invno = MyReader.GetString("invoice_no")
                invdt = "" & MyReader.GetString("invoice_dt") & ""
                'po = MyReader.GetString("po_org")
                po = MyReader.GetString("po_no")
                '---aturan Theos mesti 13 digits -------------
                If Len(po) > 13 Then po = Replace(po, "-", "")

                ttdt = "" & MyReader.GetString("tt_dt") & ""
                xcurr = MyReader.GetString("currency_code")
                amount_inv = MyReader.GetString("invoice_amount")
                amount_inv = Replace(amount_inv, ",", ".")
                plant = MyReader.GetString("plant_name")

                'create line :1.'INV', 2.No Invoice, 3.No PO, 4.DueDate, 5.Currency, 6.ORIAMTPB, 7.PLANT
                oWrite.WriteLine(xFlag & "," & invno & "," & po & "," & ttdt & "," & _
                            "" & xcurr & "," & amount_inv & "," & plant)

                If po <> "" Then ErrMsg = ""
            End While
            CloseMyReader(MyReader, UserData)
            oWrite.Close()
            nmFiles = nmFile
        End If

        If ErrMsg <> "" Then
            listError.Items.Add(ErrMsg)
        Else
            txtFolder_Name2_4.Text = nmFiles
            nmF4 = nmF
            FtpProcess(nmF1, nmF2, nmF3, nmF4)
        End If
        '-------------------------------------------------------
        'create log
        nmF = "ErrLog" & dtFile & "." & plFile
        nmFile = "C:\" & nmF
        If oFile.Exists(nmFile) Then oFile.Delete(nmFile)
        oWrite = oFile.CreateText(nmFile)
        oWrite.WriteLine(ErrPO)
        oWrite.Close()

        SQLstr = "UPDATE tbm_status_jobproses SET stat_text=trim('" & ErrPO & "') WHERE stat_code='AP'"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
    End Sub

    Private Sub btnPlant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlant.Click
        PilihanDlg.Text = "Select Plant Code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, COMPANY_CODE  as CompanyCode from tbm_plant"
        PilihanDlg.SQLFilter = "select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, COMPANY_CODE  as CompanyCode from tbm_plant " & _
                               " WHERE Plant_code LIKE 'FilterData1%' " & _
                               " and plant_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtPlantCode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub TxtPlantCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPlantCode.TextChanged
        TxtPlantName.Text = AmbilData("PLANT_NAME", "tbm_plant", "PLANT_CODE='" & TxtPlantCode.Text & "'")
    End Sub

    Private Sub FtpProcess(ByVal nmF1 As String, ByVal nmF2 As String, ByVal nmF3 As String, ByVal nmF4 As String)
        Dim ipsvr, usersvr, passsvr As String
        Dim oFile As System.IO.File
        Dim oWrite As System.IO.StreamWriter
        Dim oRead As System.IO.StreamReader
        Dim FtpFile As String

        SQLstr = "SELECT ipsvr, usersvr, passsvr FROM tbm_plant WHERE plant_code= '" & TxtPlantCode.Text & "'"
        ErrMsg = "Failed when read File Data "
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            ErrMsg = ""
            While MyReader.Read
                ipsvr = Trim(MyReader.GetString("ipsvr"))
                usersvr = MyReader.GetString("usersvr")
                passsvr = MyReader.GetString("passsvr")
            End While

            If ipsvr <> "" Then
                Try
                    FtpFile = "C:\ftpCommand.ftp"
                    If oFile.Exists(FtpFile) Then oFile.Delete(FtpFile)
                    oWrite = oFile.CreateText(FtpFile)
                    oWrite.WriteLine("open " & ipsvr) '-------> open FTP site/location
                    oWrite.WriteLine(usersvr) '--------------> ftp user name
                    oWrite.WriteLine(passsvr) '--------------> ftp user password
                    oWrite.WriteLine("lcd C:\")
                    oWrite.WriteLine("put " & nmF1)
                    oWrite.WriteLine("put " & nmF2)
                    oWrite.WriteLine("put " & nmF3)
                    oWrite.WriteLine("put " & nmF4)
                    oWrite.WriteLine("bye") '--------------> bye //ftp command to come out of the ftp command promt
                    oWrite.Close()
                    oWrite = Nothing

                    Dim Proc = New Process
                    With Proc.StartInfo
                        .FileName = "ftp.exe"
                        .Arguments = "-i -s:" & FtpFile
                        .UseShellExecute = False
                        .RedirectStandardOutput = True
                        .CreateNoWindow = True
                    End With
                    Proc.Start()
                    ErrMsg = "Transfered to " & ipsvr & " succesfully. "
                Catch ex As Exception
                    ErrMsg = "Transfered file fail. Server " & ipsvr & " not found. Please manual transfer from c:\. "
                End Try
            Else
                ErrMsg = "Server not identified. Please manual transfer from c:\. "
            End If

            If ErrMsg <> "" Then listError.Items.Add(ErrMsg)
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub btnPlant2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPlant2.Click
        PilihanDlg.Text = "Select Plant Code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, COMPANY_CODE  as CompanyCode from tbm_plant"
        PilihanDlg.SQLFilter = "select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, ADDRESS as Address, CITY_CODE as CityCode, PHONE as Phone, FAX as Fax, COMPANY_CODE  as CompanyCode from tbm_plant " & _
                               " WHERE Plant_code LIKE 'FilterData1%' " & _
                               " and plant_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TxtPlantCode2.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub TxtPlantCode2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtPlantCode2.TextChanged
        TxtPlantName2.Text = AmbilData("PLANT_NAME", "tbm_plant", "PLANT_CODE='" & TxtPlantCode2.Text & "'")
    End Sub
End Class