VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3090
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3090
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub Form_Load()
'Perhatikan format pake yg inggrish, date = yyyy-mm-dd

'#include Excel Template
' Set ODBC untuk impr --- MySQL

On Error GoTo ErrorHandler

    Dim MyPath, MyPathCopy
    Dim cnn1 As New ADODB.Connection
    Dim xStat
    Dim xdate, xTime
    Dim strsql
    Dim test
    
    Set cnn1 = New ADODB.Connection
    cnn1.Open "DSN=impr_svr;UID=root;pwd=vbdev"
    
    'list file yang akan diprocess
    MyPath = CurDir & "\FrExcell"
    MyPathCopy = CurDir & "\FrExcellDone"
    MyFind = MyPath & "\*"
    MyFile = Dir(MyFind, vbDirectory)
    
    xLoop = 0

    
    'process file
    Do While MyFile <> ""
        xColumn = ""
        If MyFile <> "." And MyFile <> ".." Then
            Set xlApp = CreateObject("Excel.Application")
            xlApp.Application.Visible = False
            Set xlBook = xlApp.Workbooks.Open(MyPath & "\" & MyFile)
            Set xlSheet = xlBook.Worksheets(1)
            
            '----------------------------------------
            'process record in file u/ Purchase Order
            xEnd = 0
            xrow = 2
            xLastPONo = ""
            xPOItem = 0
            
            'clear temporary warning
            If xLoop = 0 Then
                Set rsdata1 = cnn1.Execute("delete from tbr_offprocess where msg_type = 'PO' or msg_type = 'PO Detail'")
            End If
    
            Do While xEnd = 0
                xColumn = "A, O, Y, AD, G, C, D, E, AT, T, Q, R, W, BY, B, F"
                xPONO = xlSheet.Cells(xrow, "A").Value
                'MsgBox (xPONO)
                If Trim(xPONO) = "" Then xEnd = 1
                If xEnd = 0 Then
                    iPONo = InStr(1, xPONO, "(", 0) 'contoh data 12345(1), 12346(K)(1)
                    If iPONo > 0 Then
                        If IsNumeric(Mid(xPONO, iPONo + 1, 1)) Then 'contoh data 12345(1) -> 12345
                            xPONO = Mid(xPONO, 1, iPONo - 1)
                        Else
                            jPONo = InStr(iPONo + 1, xPONO, "(", 0)
 
                            If jPONo > 0 Then
                                If IsNumeric(Mid(xPONO, jPONo + 1, 1)) Then 'contoh data 12346(K)(1) -> 12346(K)
                                    xPONO = Mid(xPONO, 1, jPONo - 1)
                                End If
                            End If
                        End If
                    End If

                    'untuk header
                    If xEnd = 0 And xPONO <> xLastPONo Then
                    
                        StrErr = "" 'declare : jika ada error data tidak perlu di insert ke transaksi
                        
                        xCOMPANY = Trim(xlSheet.Cells(xrow, "O").Value)
                        Set rsdata1 = cnn1.Execute("SELECT company_code FROM tbm_company WHERE lower(company_shortname) = lower('" & xCOMPANY & "')")
                        If Not rsdata1.EOF Then
                            xCOMPANY_CODE = rsdata1("company_code")
                        Else
                            StrErr = "Company : " & xCOMPANY & " (column O) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        
                        xPLANT = Trim(xlSheet.Cells(xrow, "Y").Value)
                        Set rsdata1 = cnn1.Execute("SELECT plant_code FROM tbm_plant WHERE (lower(plant_name) = lower('" & xCOMPANY & "-" & xPLANT & "')) or (lower(plant_name) = lower('" & xPLANT & "'))")
                        If Not rsdata1.EOF Then
                            xPLANT_CODE = rsdata1("plant_code")
                        Else
                            StrErr = "Plant : " & xPLANT & " (column Y) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        
                        xPORT = Trim(xlSheet.Cells(xrow, "AD").Value)
                        Set rsdata1 = cnn1.Execute("SELECT port_code FROM tbm_port WHERE lower(port_name) = lower('" & xPORT & "')")
                        If Not rsdata1.EOF Then
                            xPORT_CODE = rsdata1("port_code")
                        Else
                            StrErr = "Port : " & xPORT & " (column AD) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        
                        xSHIPMENT_PERIOD = Trim(xlSheet.Cells(xrow, "G").Value)
                        Set rsdata1 = cnn1.Execute("SELECT period_fr, period_to FROM tbm_period WHERE lower(period) = lower('" & xSHIPMENT_PERIOD & "')")
                        If Not rsdata1.EOF Then
                            xSHIPMENT_PERIOD_FR = rsdata1("period_fr")
                            xSHIPMENT_PERIOD_TO = rsdata1("period_to")
                        Else
                            StrErr = "Period : " & xSHIPMENT_PERIOD & " (column G) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        
                        xSHIPMENT_TERM = "P"
                        
                        xSUPPLIER = Trim(xlSheet.Cells(xrow, "C").Value)
                        Set rsdata1 = cnn1.Execute("SELECT supplier_code FROM tbm_supplier WHERE lower(supplier_name) = lower('" & xSUPPLIER & "')")
                        If Not rsdata1.EOF Then
                            xSUPPLIER_CODE = rsdata1("supplier_code")
                        Else
                            StrErr = "Supplier : " & xSUPPLIER & " (column C) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        
                        xCONTRACT_NO = Trim(xlSheet.Cells(xrow, "D").Value)
                        xIPA_NO = Trim(xlSheet.Cells(xrow, "E").Value)
                        xPR_NO = ""

                        xPAYMENT = Trim(xlSheet.Cells(xrow, "AT").Value) & " " & Trim(xlSheet.Cells(xrow, "AX").Value)
                        Set rsdata1 = cnn1.Execute("SELECT payment_code FROM tbm_payment_term WHERE lower(payment_name) = lower('" & xPAYMENT & "')")
                        If Not rsdata1.EOF Then
                            xPAYMENT_CODE = rsdata1("payment_code")
                        Else
                            StrErr = "Payment Term : " & xPAYMENT & " (column AT) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        
                        xTOLERABLE_DELIVERY = Trim(xlSheet.Cells(xrow, "T").Value)
                        iCek = InStr(1, xTOLERABLE_DELIVERY, "%", 0)
                        If iCek > 0 Then xTOLERABLE_DELIVERY = Trim(Mid(xTOLERABLE_DELIVERY, 1, iCek - 1))
                        If Not IsNumeric(xTOLERABLE_DELIVERY) Then xTOLERABLE_DELIVERY = 0
                        xTOLERABLE_DELIVERY = Round(xTOLERABLE_DELIVERY, 2)

                        xINSURANCE = Trim(xlSheet.Cells(xrow, "Q").Value)
                        Set rsdata1 = cnn1.Execute("SELECT insurance_code FROM tbm_insurance WHERE LOWER(insurance_code) = lower('" & xINSURANCE & "')")
                        If Not rsdata1.EOF Then
                            xINSURANCE_CODE = rsdata1("insurance_code")
                        Else
                            StrErr = "Shipping Term : " & xINSURANCE & " (column Q) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If

                        xCURRENCY = Trim(xlSheet.Cells(xrow, "R").Value)
                        Set rsdata1 = cnn1.Execute("SELECT currency_code FROM tbm_currency WHERE LOWER(currency_code) = lower('" & xCURRENCY & "')")
                        If Not rsdata1.EOF Then
                            xCURRENCY_CODE = rsdata1("currency_code")
                        Else
                            StrErr = "Currency : " & xCURRENCY & " (column R) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If

                        xKURS = xlSheet.Cells(xrow, "W").Value
                        If Not IsNumeric(xKURS) Then xKURS = 0
                        If xKURS = "" Then xKURS = 0
                        xKURS = Round(xKURS, 2)
                        
                        xSTATUS = "Open"
                        xNOTE = "Transfer from excell"

                        xCREATEDBY = Trim(xlSheet.Cells(xrow, "BY").Value)
                        If Trim(xCREATEDBY) <> "" Then
                            Set rsdata1 = cnn1.Execute("SELECT user_ct FROM tbm_users WHERE LOWER(user_id) = lower('" & xCREATEDBY & "')")
                            If Not rsdata1.EOF Then
                                xCREATEDBY_CODE = rsdata1("user_ct")
                            Else
                               StrErr = "User : " & xCREATEDBY & " (column BY) tidak di kenal di master data"
                               Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                            End If
                        Else
                            StrErr = "User (column BY) tidak boleh kosong"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If

                        xPURCHASEDBY = Trim(xlSheet.Cells(xrow, "F").Value)
                        xPURCHASEDBY = ""
                        If Trim(xPURCHASEDBY) <> "" Then
                            Set rsdata1 = cnn1.Execute("SELECT user_ct FROM tbm_users WHERE LOWER(user_id) = lower('" & xPURCHASEDBY & "')")
                            If Not rsdata1.EOF Then
                                xPURCHASEDBY_CODE = rsdata1("user_ct")
                            Else
                                StrErr = "CommDiv Person : " & xPURCHASEDBY & " (column F) tidak di kenal di master data"
                                Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                            End If
                        End If

                        xCREATEDDT = xlSheet.Cells(xrow, "B").Value
                        If Not IsDate(xCREATEDDT) Then xCREATEDDT = ""
                        If Trim(xCREATEDDT) = "" Then xCREATEDDT = xSHIPMENT_PERIOD_FR
                        If InStr(xCREATEDDT, "1900") > 0 Then xCREATEDDT = ""
                            
                        If xCREATEDDT = "" Then
                            StrErr = "Tanggal PO (Column B) tidak boleh kosong"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
                        End If
                        xPURCHASEDDT = xCREATEDDT
                        
                        If StrErr = "" Then
                            Set rsdata1 = cnn1.Execute("Select po_no from tbl_po where po_no = '" & xPONO & "'")
                            
                            If rsdata1.EOF Then
                                
                                strsql = ""
                                strsql = strsql & " Insert into tbl_po (PO_NO,COMPANY_CODE,PLANT_CODE,PORT_CODE, SHIPMENT_PERIOD_FR , SHIPMENT_PERIOD_TO, SHIPMENT_TERM_CODE, " _
                                       & "SUPPLIER_CODE, PRODUSEN_CODE, CONTRACT_NO, IPA_NO, PR_NO, PAYMENT_CODE, TOLERABLE_DELIVERY, INSURANCE_CODE, " _
                                       & "CURRENCY_CODE, KURS, STATUS, NOTE, CREATEDBY, CREATEDDT, PURCHASEDDT, PURCHASEDBY) value " _
                                       & "('" & xPONO & "','" & xCOMPANY_CODE & "','" & xPLANT_CODE & "','" & xPORT_CODE & "'," _
                                       & "'" & xSHIPMENT_PERIOD_FR & "','" & xSHIPMENT_PERIOD_TO & "','" & xSHIPMENT_TERM & "'," _
                                       & "'" & xSUPPLIER_CODE & "',null, '" & xCONTRACT_NO & "','" & xIPA_NO & "','" & xPR_NO & "'," _
                                       & "'" & xPAYMENT_CODE & "'," & xTOLERABLE_DELIVERY & ",'" & xINSURANCE_CODE & "'," _
                                       & "'" & xCURRENCY_CODE & "'," & xKURS & ",'" & xSTATUS & "','" & xNOTE & "'," _
                                       & "" & xCREATEDBY_CODE & ",'" & xCREATEDDT & "',IF('" & xPURCHASEDBY & "'='',NULL,'" & xPURCHASEDDT & "'),IF('" & xPURCHASEDBY & "'='',NULL,'" & xPURCHASEDBY_CODE & "')); "
                        
                                'MsgBox (strsql)
                                Set rsdata1 = cnn1.Execute(strsql)
                                
                            Else
                                'status tetap
                                strsql = ""
                                strsql = strsql & " Update tbl_po Set " _
                                       & "COMPANY_CODE='" & xCOMPANY_CODE & "',PLANT_CODE='" & xPLANT_CODE & "',PORT_CODE='" & xPORT_CODE & "', " _
                                       & "SHIPMENT_PERIOD_FR='" & xSHIPMENT_PERIOD_FR & "', SHIPMENT_PERIOD_TO='" & xSHIPMENT_PERIOD_TO & "', SHIPMENT_TERM_CODE='" & xSHIPMENT_TERM & "', " _
                                       & "SUPPLIER_CODE='" & xSUPPLIER_CODE & "', CONTRACT_NO='" & xCONTRACT_NO & "', " _
                                       & "IPA_NO='" & xIPA_NO & "', PR_NO='" & xPR_NO & "', PAYMENT_CODE='" & xPAYMENT_CODE & "', TOLERABLE_DELIVERY=" & xTOLERABLE_DELIVERY & ", " _
                                       & "INSURANCE_CODE='" & xINSURANCE_CODE & "', CURRENCY_CODE='" & xCURRENCY_CODE & "', KURS=" & xKURS & ", " _
                                       & "NOTE='" & xNOTE & "', CREATEDBY=" & xCREATEDBY_CODE & ", CREATEDDT='" & xCREATEDDT & "', " _
                                       & "PURCHASEDDT=IF('" & xPURCHASEDBY & "'='',NULL,'" & xPURCHASEDDT & "'), PURCHASEDBY=IF('" & xPURCHASEDBY & "'='',NULL,'" & xPURCHASEDBY_CODE & "') " _
                                       & "Where PO_NO = '" & xPONO & "' ; "
                        
                                Set rsdata1 = cnn1.Execute(strsql)
                                
                            End If
                        End If
                        xLastPONo = xPONO
                        
                        xPOItem = 0
                        StrErr2 = "" 'declare : jika ada error data tidak perlu di insert ke transaksi
                    End If
                End If
                
                If xEnd = 0 Then
                    'untuk detail
                    xColumn = "A-> PO, I->MATERIAL, K->COUNTRY, J->HS, AN->SPESIFICATION, L-> QUANTITY, M->UNIT, H->PRICE"
                    
                    xPOItem = xPOItem + 1
                     
                    xMATERIAL = Trim(xlSheet.Cells(xrow, "I").Value)
                    Set rsdata1 = cnn1.Execute("SELECT material_code FROM tbm_material WHERE lower(material_name) = lower('" & xMATERIAL & "') OR lower(material_shortname) = lower('" & xMATERIAL & "')")
                    If Not rsdata1.EOF Then
                        xMATERIAL_CODE = rsdata1("material_code")
                    Else
                        StrErr2 = "Material : " & xMATERIAL & " (column I) tidak di kenal di master data"
                        Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO Detail', '" & MyFile & "', " & xrow & ", '" & StrErr2 & "',now())")
                    End If
                
                    xCOUNTRY = Trim(xlSheet.Cells(xrow, "K").Value)
                    Set rsdata1 = cnn1.Execute("SELECT country_code FROM tbm_country WHERE lower(country_name) = lower('" & xCOUNTRY & "')")
                    If Not rsdata1.EOF Then
                        xCOUNTRY_CODE = rsdata1("country_code")
                    Else
                        StrErr2 = "Country : " & xCOUNTRY & " (column K) tidak di kenal di master data"
                        Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO Detail', '" & MyFile & "', " & xrow & ", '" & StrErr2 & "',now())")
                    End If
                
                    xHS_CODE = Trim(xlSheet.Cells(xrow, "J").Value)
                    xSPECIFICATION = Trim(xlSheet.Cells(xrow, "AN").Value)
                
                    xQUANTITY = xlSheet.Cells(xrow, "L").Value
                    If Not IsNumeric(xQUANTITY) Then xQUANTITY = 0
                    If xQUANTITY = "" Then xQUANTITY = 0
                
                    xUNIT_CODE = Trim(xlSheet.Cells(xrow, "M").Value)
                    Set rsdata1 = cnn1.Execute("SELECT unit_code FROM tbm_unit WHERE lower(unit_name) = lower('" & xUNIT_CODE & "')")
                    If Not rsdata1.EOF Then
                        xUNIT_CODE = rsdata1("unit_code")
                    Else
                        StrErr2 = "UNIT : " & xUNIT_CODE & " (column M) tidak di kenal di master data"
                        Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('PO Detail', '" & MyFile & "', " & xrow & ", '" & StrErr2 & "',now())")
                    End If
                
                    xPrice = xlSheet.Cells(xrow, "H").Value
                    If Not IsNumeric(xPrice) Then xPrice = 0
                    If xPrice = "" Then xPrice = 0
                    xPrice = Round(xPrice, 2)
                        
                    If StrErr2 = "" And xQUANTITY > 0 Then 'quantity PO harus ada, jika tidak ada berarti baris ini adalah untuk shipping detail
                        
                        Set rsdata1 = cnn1.Execute("Select po_no from tbl_po where po_no = '" & xPONO & "'")
                        If Not rsdata1.EOF Then
                            Set rsdata1 = cnn1.Execute("Select po_no from tbl_po_detail where po_no = '" & xPONO & "'" _
                                        & " and po_item = '" & xPOItem & "'")
                                
                            If rsdata1.EOF Then
                                strsql = ""
                                strsql = strsql & " Insert into tbl_po_detail (PO_NO, PO_ITEM, MATERIAL_CODE, COUNTRY_CODE, HS_CODE, " _
                                        & "SPECIFICATION, QUANTITY, UNIT_CODE, PRICE) value " _
                                        & "('" & xPONO & "','" & xPOItem & "','" & xMATERIAL_CODE & "','" & xCOUNTRY_CODE & "'," _
                                        & "'" & xHS_CODE & "','" & xSPECIFICATION & "'," & xQUANTITY & ",'" & xUNIT_CODE & "'," _
                                        & " " & xPrice & "); "

                                Set rsdata1 = cnn1.Execute(strsql)
                                
                            Else
                                strsql = ""
                                strsql = strsql & " Update tbl_po_detail Set " _
                                        & "MATERIAL_CODE='" & xMATERIAL_CODE & "', COUNTRY_CODE='" & xCOUNTRY_CODE & "', HS_CODE='" & xHS_CODE & "', " _
                                        & "SPECIFICATION='" & xSPECIFICATION & "', QUANTITY=" & xQUANTITY & ", UNIT_CODE='" & xUNIT_CODE & "', PRICE=" & xPrice & " " _
                                        & "Where PO_NO='" & xPONO & "' and  PO_ITEM='" & xPOItem & "' ; "

                                Set rsdata1 = cnn1.Execute(strsql)
                                
                            End If
                        End If
                    End If
                              
                    'MsgBox (xPONo)
                End If
                xrow = xrow + 1
            Loop
            StrErr = "Purchase Order - End Process : " & xrow & " lines"
            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('End Process', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
            
            '----------------------------------
            'process record in file u/ shipment
            xEnd = 0
            xrow = 2
            xLastBL = ""
            'xPOItem = 0

            'clear temporary warning
            If xLoop = 0 Then
                Set rsdata1 = cnn1.Execute("delete from tbr_offprocess where msg_type = 'Shipping' or msg_type = 'Shipping Detail' or msg_type = 'Shipping Invoice'")
                xLoop = 1
            End If
            
            Do While xEnd = 0
                ''If xrow = 902 Then
                    ''test = 1
                ''End If
            
                xColumn = "AM, C, AZ, BO, BP, AC, AE, AF, BT, AG, AH, BV, AD, AB, Z, AA, R, BJ, BK, BL, BE, BD, AL, BB, AK, BN, BM, BN, BQ, BR, BI, BY, AZ"
                xPONO = xlSheet.Cells(xrow, "A").Value
                'MsgBox (xPONo)
                If Trim(xPONO) = "" Then xEnd = 1
                If xEnd = 0 Then
                    iPONo = InStr(1, xPONO, "(", 0) 'contoh data 12345(1), 12346(K)(1)
                    If iPONo > 0 Then
                        If IsNumeric(Mid(xPONO, iPONo + 1, 1)) Then 'contoh data 12345(1) -> 12345
                            xPONO = Mid(xPONO, 1, iPONo - 1)
                        Else
                            jPONo = InStr(iPONo + 1, xPONO, "(", 0)
 
                            If jPONo > 0 Then
                                If IsNumeric(Mid(xPONO, jPONo + 1, 1)) Then 'contoh data 12346(K)(1) -> 12346(K)
                                    xPONO = Mid(xPONO, 1, jPONo - 1)
                                End If
                            End If
                        End If
                    End If
                    
                    'untuk shipping
                    StrErr3 = "" 'declare : jika ada error data tidak perlu di insert ke transaksi
                    
                    xBL = Trim(xlSheet.Cells(xrow, "AM").Value)
                    
                    
                    If xEnd = 0 And xBL <> xLastBL And xBL <> "" Then
                        
                        xSUPPLIER = Trim(xlSheet.Cells(xrow, "C").Value)
                        Set rsdata1 = cnn1.Execute("SELECT supplier_code FROM tbm_supplier WHERE lower(supplier_name) = lower('" & xSUPPLIER & "')")
                        If Not rsdata1.EOF Then
                            xSUPPLIER_CODE = rsdata1("supplier_code")
                        Else
                            StrErr3 = "Supplier : " & xSUPPLIER & " (column C) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                    
                        xRECEIVED_COPYDOC_DT = xlSheet.Cells(xrow, "AZ").Value
                        If Not IsDate(xRECEIVED_COPYDOC_DT) Then xRECEIVED_COPYDOC_DT = ""
                        If InStr(xRECEIVED_COPYDOC_DT, "1900") > 0 Then xRECEIVED_COPYDOC_DT = ""
                        xRECEIVED_DOC_DT = xlSheet.Cells(xrow, "BO").Value
                        If Not IsDate(xRECEIVED_DOC_DT) Then xRECEIVED_DOC_DT = ""
                        If InStr(xRECEIVED_DOC_DT, "1900") > 0 Then xRECEIVED_DOC_DT = ""
                        xFORWARD_DOC_DT = xlSheet.Cells(xrow, "BP").Value
                        If Not IsDate(xFORWARD_DOC_DT) Then xFORWARD_DOC_DT = ""
                        If InStr(xFORWARD_DOC_DT, "1900") > 0 Then xFORWARD_DOC_DT = ""
                        xEST_DELIVERY_DT = xlSheet.Cells(xrow, "AC").Value
                        If Not IsDate(xEST_DELIVERY_DT) Then xEST_DELIVERY_DT = ""
                        If InStr(xEST_DELIVERY_DT, "1900") > 0 Then xEST_DELIVERY_DT = ""
                        xEST_ARRIVAL_DT = xlSheet.Cells(xrow, "AE").Value
                        If Not IsDate(xEST_ARRIVAL_DT) Then xEST_ARRIVAL_DT = ""
                        If InStr(xEST_ARRIVAL_DT, "1900") > 0 Then xEST_ARRIVAL_DT = ""
                        xNOTICE_ARRIVAL_DT = xlSheet.Cells(xrow, "AF").Value
                        If Not IsDate(xNOTICE_ARRIVAL_DT) Then xNOTICE_ARRIVAL_DT = ""
                        If InStr(xNOTICE_ARRIVAL_DT, "1900") > 0 Then xNOTICE_ARRIVAL_DT = ""
                    
                        xFTE_NOTE = ""
                    
                        xEST_BAPB_DT = xlSheet.Cells(xrow, "BT").Value
                        If Not IsDate(xEST_BAPB_DT) Then xEST_BAPB_DT = ""
                        If InStr(xEST_BAPB_DT, "1900") > 0 Then xEST_BAPB_DT = ""
                        xBAPB_DT = xlSheet.Cells(xrow, "BT").Value
                        If Not IsDate(xBAPB_DT) Then xBAPB_DT = ""
                        If InStr(xBAPB_DT, "1900") > 0 Then xBAPB_DT = ""

                        xTOTAL_CONTAINER = xlSheet.Cells(xrow, "AG").Value
                    
                        If xTOTAL_CONTAINER <> "" Then
                             xTOTAL_CONTAINER = xTOTAL_CONTAINER & " x " & Trim(xlSheet.Cells(xrow, "AH").Value)
                             xTOTAL_CONTAINER = Replace(xTOTAL_CONTAINER, "'", " ")
                        End If
                        
                        xPLANT = Trim(xlSheet.Cells(xrow, "BV").Value)
                        Set rsdata1 = cnn1.Execute("SELECT plant_code FROM tbm_plant WHERE (lower(plant_name) = lower('" & xCOMPANY & "-" & xPLANT & "')) or (lower(plant_name) = lower('" & xPLANT & "'))")
                        If Not rsdata1.EOF Then
                            xPLANT_CODE = rsdata1("plant_code")
                        Else
                            StrErr3 = "Plant : " & xPLANT & " (column BV) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                    
                        xPORT = Trim(xlSheet.Cells(xrow, "AD").Value)
                        Set rsdata1 = cnn1.Execute("SELECT port_code FROM tbm_port WHERE lower(port_name) = lower('" & xPORT & "')")
                        If Not rsdata1.EOF Then
                            xPORT_CODE = rsdata1("port_code")
                        Else
                            StrErr3 = "Port : " & xPORT & " (column AD) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                        
                        
                        xLOADPORT = Trim(xlSheet.Cells(xrow, "AB").Value)
                        xLOADPORT_a = ""
                    
                        MyPOs = InStr(xLOADPORT, ",")
                        If MyPOs > 0 Then xLOADPORT_a = Mid(xLOADPORT, 1, MyPOs - 1)
    
                        Set rsdata1 = cnn1.Execute("SELECT port_code FROM tbm_port WHERE lower(port_name) = lower('" & xLOADPORT & "') OR lower(port_name) like lower('" & xLOADPORT_a & "%')")
                        If Not rsdata1.EOF Then
                            xLOADPORT_CODE = rsdata1("port_code")
                        Else
                            StrErr3 = "Port : " & xLOADPORT & " (column AB) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                    
                        xSHIPPING_LINE = Trim(xlSheet.Cells(xrow, "Z").Value)
                        Set rsdata1 = cnn1.Execute("SELECT line_code FROM tbm_lines WHERE lower(line_name) = lower('" & xSHIPPING_LINE & "')")
                        If Not rsdata1.EOF Then
                            xSHIPPING_LINE = rsdata1("line_code")
                        Else
                            StrErr3 = "Shipping Lines : " & xSHIPPING_LINE & " (column Z) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                    
                        xVESSEL = Trim(xlSheet.Cells(xrow, "AA").Value)
                        xPACKINGLIST_NO = ""
                    
                        xCURRENCY = Trim(xlSheet.Cells(xrow, "R").Value)
                        Set rsdata1 = cnn1.Execute("SELECT currency_code FROM tbm_currency WHERE LOWER(currency_code) = lower('" & xCURRENCY & "')")
                        If Not rsdata1.EOF Then
                            xCURRENCY_CODE = rsdata1("currency_code")
                        Else
                            StrErr3 = "Currency : " & xCURRENCY & " (column R) tidak di kenal di master data"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                        
                        xBEA_MASUK = xlSheet.Cells(xrow, "BJ").Value
                        If Not IsNumeric(xBEA_MASUK) Then xBEA_MASUK = 0
                        If xBEA_MASUK = "" Then xBEA_MASUK = 0
                        xBEA_MASUK = Round(xBEA_MASUK, 2)
                    
                        xVAT = xlSheet.Cells(xrow, "BK").Value
                        If Not IsNumeric(xVAT) Then xVAT = 0
                        If xVAT = "" Then xVAT = 0
                        xVAT = Round(xVAT, 2)
                    
                        xPPH21 = xlSheet.Cells(xrow, "BL").Value
                        If Not IsNumeric(xPPH21) Then xPPH21 = 0
                        If xPPH21 = "" Then xPPH21 = 0
                        xPPH21 = Round(xPPH21, 2)
                    
                        xPIUD = 0
                        xINSURANCE_NO = Trim(xlSheet.Cells(xrow, "BE").Value)
                    
                        xINSURANCE_AMOUNT = xlSheet.Cells(xrow, "BD").Value
                        If Not IsNumeric(xINSURANCE_AMOUNT) Then xINSURANCE_AMOUNT = 0
                        If xINSURANCE_AMOUNT = "" Then xINSURANCE_AMOUNT = 0
                        xINSURANCE_AMOUNT = Round(xINSURANCE_AMOUNT, 2)

                        xBANK_NAME = ""
                        xACCOUNT_NO = Trim(xlSheet.Cells(xrow, "AL").Value)
                        xTT_DT = xlSheet.Cells(xrow, "BB").Value
                        If Not IsDate(xTT_DT) Then xTT_DT = ""
                        If InStr(xTT_DT, "1900") > 0 Then xTT_DT = ""
                        xDUE_DT = xlSheet.Cells(xrow, "AK").Value
                        If Not IsDate(xDUE_DT) Then xDUE_DT = ""
                        If InStr(xDUE_DT, "1900") > 0 Then xDUE_DT = ""
                        
                        xPIB_NO = Trim(xlSheet.Cells(xrow, "BN").Value)
                    
                        xPIB_DT = xlSheet.Cells(xrow, "BM").Value
                        If Not IsDate(xPIB_DT) Then xPIB_DT = ""
                        If InStr(xPIB_DT, "1900") > 0 Then xPIB_DT = ""
                    
                        xAJU_NO = Trim(xlSheet.Cells(xrow, "BN").Value)
                    
                        xEST_SPPB_DT = xlSheet.Cells(xrow, "BQ").Value
                        If Not IsDate(xEST_SPPB_DT) Then xEST_SPPB_DT = ""
                        If InStr(xEST_SPPB_DT, "1900") > 0 Then xEST_SPPB_DT = ""
                        
                        xSPPB_DT = xlSheet.Cells(xrow, "BQ").Value
                        If Not IsDate(xSPPB_DT) Then xSPPB_DT = ""
                        If InStr(xSPPB_DT, "1900") > 0 Then xSPPB_DT = ""
                    
                        xSPPB_NO = Trim(xlSheet.Cells(xrow, "BR").Value)

                        xKURS_PAJAK = xlSheet.Cells(xrow, "BI").Value
                        If Not IsNumeric(xKURS_PAJAK) Then xKURS_PAJAK = 0
                        If xKURS_PAJAK = "" Then xKURS_PAJAK = 0
                        xKURS_PAJAK = Round(xKURS_PAJAK, 2)
                        
                        xSTATUS = "Open"
                    
                        xCREATEDBY = xlSheet.Cells(xrow, "BY").Value
                        If Trim(xCREATEDBY) <> "" Then
                            Set rsdata1 = cnn1.Execute("SELECT user_ct FROM tbm_users WHERE LOWER(user_id) = lower('" & xCREATEDBY & "')")
                            If Not rsdata1.EOF Then
                                xCREATEDBY_CODE = rsdata1("user_ct")
                            Else
                                StrErr3 = "User : " & xCREATEDBY & " (column BY) tidak di kenal di master data"
                                Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                            End If
                        Else
                            StrErr3 = "User (column BY) tidak boleh kosong"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                        End If
                        
                        xCREATEDDT = xlSheet.Cells(xrow, "AZ").Value
                        If Not IsDate(xCREATEDDT) Then xCREATEDDT = ""
                        If InStr(xCREATEDDT, "1900") > 0 Then xCREATEDDT = ""
                        If xCREATEDDT = "" Then
                            If xRECEIVED_DOC_DT = "" Then
                                StrErr3 = "Tanggal Doc Shipment (Column AZ / BO) tidak boleh kosong"
                                Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping', '" & MyFile & "', " & xrow & ", '" & StrErr3 & "',now())")
                            Else
                                xCREATEDDT = xRECEIVED_DOC_DT
                            End If
                        End If

                        If StrErr3 = "" Then
                            Set rsdata1 = cnn1.Execute("Select bl_no from tbl_shipping where bl_no = '" & xBL & "'")
                                
                            If rsdata1.EOF Then
                                Set rsdata1 = cnn1.Execute("SELECT MAX(shipment_no)+1 shipno FROM tbl_shipping")
                                If Not rsdata1.EOF Then
                                    xShipNo = rsdata1("shipno")
                        
                                    strsql = ""
                                    strsql = strsql & " Insert into tbl_shipping (SHIPMENT_NO, BL_NO, HOSTBL, SUPPLIER_CODE, " _
                                            & " RECEIVED_COPYDOC_DT, RECEIVED_DOC_DT, FORWARD_DOC_DT, EST_DELIVERY_DT, " _
                                            & " EST_ARRIVAL_DT, NOTICE_ARRIVAL_DT, FTE_NOTE, EST_BAPB_DT, BAPB_DT, " _
                                            & " TOTAL_CONTAINER, PLANT_CODE, PORT_CODE, LOADPORT_CODE, SHIPPING_LINE, " _
                                            & " VESSEL, PACKINGLIST_NO, CURRENCY_CODE, BEA_MASUK, VAT, PPH21, PIUD, " _
                                            & " INSURANCE_NO, INSURANCE_AMOUNT, BANK_NAME, ACCOUNT_NO, TT_DT, DUE_DT, " _
                                            & " PIB_NO, PIB_DT, AJU_NO, EST_SPPB_DT, SPPB_DT, SPPB_NO, KURS_PAJAK, " _
                                            & " STATUS, CREATEDBY, CREATEDDT) value " _
                                            & "(" & xShipNo & ",'" & xBL & "','','" & xSUPPLIER_CODE & "'," _
                                            & "IF('" & xRECEIVED_COPYDOC_DT & "'='',NULL,'" & xRECEIVED_COPYDOC_DT & "'),IF('" & xRECEIVED_DOC_DT & "'='',NULL,'" & xRECEIVED_DOC_DT & "')," _
                                            & "IF('" & xFORWARD_DOC_DT & "'='',NULL,'" & xFORWARD_DOC_DT & "'),IF('" & xEST_DELIVERY_DT & "'='',NULL,'" & xEST_DELIVERY_DT & "')," _
                                            & "IF('" & xEST_ARRIVAL_DT & "'='',NULL,'" & xEST_ARRIVAL_DT & "'),IF('" & xNOTICE_ARRIVAL_DT & "'='',NULL,'" & xNOTICE_ARRIVAL_DT & "')," _
                                            & "'',IF('" & xEST_BAPB_DT & "'='',NULL,'" & xEST_BAPB_DT & "'),IF('" & xBAPB_DT & "'='',NULL,'" & xBAPB_DT & "')," _
                                            & "'" & xTOTAL_CONTAINER & "','" & xPLANT_CODE & "','" & xPORT_CODE & "','" & xLOADPORT_CODE & "','" & xSHIPPING_LINE & "'," _
                                            & "'" & xVESSEL & "','" & xPACKINGLIST_NO & "','" & xCURRENCY_CODE & "'," & xBEA_MASUK & "," & xVAT & "," & xPPH21 & "," & xPIUD & "," _
                                            & "'" & xINSURANCE_NO & "'," & xINSURANCE_AMOUNT & ",'" & xBANK_NAME & "','" & xACCOUNT_NO & "'," _
                                            & "IF('" & xTT_DT & "'='',NULL,'" & xTT_DT & "'),IF('" & xDUE_DT & "'='',NULL,'" & xDUE_DT & "')," _
                                            & "'" & xPIB_NO & "',IF('" & xPIB_DT & "'='',NULL,'" & xPIB_DT & "'),'" & xAJU_NO & "'," _
                                            & "IF('" & xEST_SPPB_DT & "'='',NULL,'" & xEST_SPPB_DT & "'),IF('" & xSPPB_DT & "'='',NULL,'" & xSPPB_DT & "'),'" & xSPPB_NO & "'," _
                                            & "" & xKURS_PAJAK & ",'" & xSTATUS & "','" & xCREATEDBY_CODE & "','" & xCREATEDDT & "'); "
                                
                                    'MsgBox (strsql)
                                    Set rsdata1 = cnn1.Execute(strsql)
                                Else
                                     'status tetap
                                     strsql = ""
                                     strsql = strsql & " Update tbl_shipping Set " _
                                            & "BL_NO='" & xBL & "', SUPPLIER_CODE='" & xSUPPLIER_CODE & "', " _
                                            & "RECEIVED_COPYDOC_DT=IF('" & xRECEIVED_COPYDOC_DT & "'='',NULL,'" & xRECEIVED_COPYDOC_DT & "'), " _
                                            & "RECEIVED_DOC_DT=IF('" & xRECEIVED_DOC_DT & "'='',NULL,'" & xRECEIVED_DOC_DT & "'), " _
                                            & "FORWARD_DOC_DT=IF('" & xFORWARD_DOC_DT & "'='',NULL,'" & xFORWARD_DOC_DT & "'), " _
                                            & "EST_DELIVERY_DT=IF('" & xEST_DELIVERY_DT & "'='',NULL,'" & xEST_DELIVERY_DT & "'), " _
                                            & "EST_ARRIVAL_DT=IF('" & xEST_ARRIVAL_DT & "'='',NULL,'" & xEST_ARRIVAL_DT & "'), " _
                                            & "NOTICE_ARRIVAL_DT=IF('" & xNOTICE_ARRIVAL_DT & "'='',NULL,'" & xNOTICE_ARRIVAL_DT & "'), " _
                                            & "EST_BAPB_DT=IF('" & xEST_BAPB_DT & "'='',NULL,'" & xEST_BAPB_DT & "'), " _
                                            & "BAPB_DT=IF('" & xBAPB_DT & "'='',NULL,'" & xBAPB_DT & "'), " _
                                            & "PLANT_CODE='" & xPLANT_CODE & "', PORT_CODE='" & xPORT_CODE & "', LOADPORT_CODE='" & xLOADPORT_CODE & "', " _
                                            & "SHIPPING_LINE='" & xSHIPPING_LINE & "', VESSEL='" & xVESSEL & "', PACKINGLIST_NO='" & xPACKINGLIST_NO & "', " _
                                            & "CURRENCY_CODE='" & xCURRENCY_CODE & "', BEA_MASUK=" & xBEA_MASUK & ", VAT=" & xVAT & ", PPH21=" & xPPH21 & ", PIUD=" & xPIUD & ", " _
                                            & "INSURANCE_NO='" & xINSURANCE_NO & "', INSURANCE_AMOUNT=" & xINSURANCE_AMOUNT & ", BANK_NAME='" & xBANK_NAME & "', " _
                                            & "ACCOUNT_NO='" & xACCOUNT_NO & "', TT_DT=IF('" & xTT_DT & "'='',NULL,'" & xTT_DT & "'), DUE_DT=IF('" & xDUE_DT & "'='',NULL,'" & xDUE_DT & "'), " _
                                            & "PIB_NO='" & xPIB_NO & "', PIB_DT=IF('" & xPIB_DT & "'='',NULL,'" & xPIB_DT & "'), AJU_NO='" & xAJU_NO & "', " _
                                            & "EST_SPPB_DT=IF('" & xEST_SPPB_DT & "'='',NULL,'" & xEST_SPPB_DT & "'), SPPB_DT=IF('" & xSPPB_DT & "'='',NULL,'" & xSPPB_DT & "'), SPPB_NO='" & xSPPB_NO & "', " _
                                            & "KURS_PAJAK=" & xKURS_PAJAK & ", CREATEDBY='" & xCREATEDBY_CODE & "', CREATEDDT='" & xCREATEDDT & "' " _
                                            & "Where SHIPMENT_NO=" & xShipNo & " ; "
                                
                                    'MsgBox (strsql)
                                    Set rsdata1 = cnn1.Execute(strsql)
                                End If
                            End If
                        End If
                        xLastBL = xBL
                    End If
                End If
                
                If xEnd = 0 Then
                    'untuk shipping detail dan invoice
                    xColumn = "A->PO, I->MATERIAL, N->QUANTITY, P->PACKING, AO->SPESIFICATION, AI->INVOICE NO, AJ->INVOICE DATE, X->INVOICE AMOUNT"
                    StrErr4 = "" 'declare : jika ada error data tidak perlu di insert ke transaksi
                    
                    xBL = Trim(xlSheet.Cells(xrow, "AM").Value)
                    If xBL = "" Then
                        'skip
                    Else
                        Set rsdata1 = cnn1.Execute("SELECT shipment_no FROM tbl_shipping WHERE bl_no = '" & xBL & "'")
                        If Not rsdata1.EOF Then
                            xSHIPMENT_NO = rsdata1("shipment_no")
                            
                            xPO_NO = xPONO
                            
                            xMATERIAL_CODE = ""
                            xMATERIAL = Trim(xlSheet.Cells(xrow, "I").Value)
                            Set rsdata1 = cnn1.Execute("SELECT material_code FROM tbm_material WHERE lower(material_name) = lower('" & xMATERIAL & "') OR lower(material_shortname) = lower('" & xMATERIAL & "')")
                            If Not rsdata1.EOF Then
                                xMATERIAL_CODE = rsdata1("material_code")
                            Else
                                StrErr4 = "Material : " & xMATERIAL & " (column I) tidak di kenal di master data"
                                Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping Detail', '" & MyFile & "', " & xrow & ", '" & StrErr4 & "',now())")
                            End If
                    
                            Set rsdata1 = cnn1.Execute("SELECT po_item, price FROM tbl_po_detail WHERE po_no = '" & xPO_NO & "' and material_code = '" & xMATERIAL_CODE & "'")
                            If Not rsdata1.EOF Then
                                xPO_ITEM = rsdata1("po_item")
                                xPrice = rsdata1("price")
                            Else
                                StrErr4 = "Material : " & xMATERIAL & " belum ada di PO Detail. Detil data tidak bisa di tambahkan"
                                Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping Detail', '" & MyFile & "', " & xrow & ", '" & StrErr4 & "',now())")
                            End If
                            
                            xQUANTITY = xlSheet.Cells(xrow, "N").Value
                            If Not IsNumeric(xQUANTITY) Then xQUANTITY = 0
                            If xQUANTITY = "" Then xQUANTITY = 0
                            
                            xPACK_QUANTITY = 0
                            xPACK = xlSheet.Cells(xrow, "P").Value
                            If xPACK <> "" Then
                                MyPOs = InStr(xPACK, " ")
                                MyLen = Len(xPACK)
                                If MyPOs = 0 And MyLen > 0 Then
                                    xPACK_QUANTITY = 1
                                Else
                                    xPACK_QUANTITY = Mid(xPACK, 1, (MyPOs - 1))
                                    xPACK = Trim(Mid(xPACK, (MyPOs + 1), (MyLen - MyPOs)))
                                    
                                    If Not IsNumeric(xPACK_QUANTITY) Then xPACK_QUANTITY = 0
                                    If xPACK_QUANTITY = "" Then xPACK_QUANTITY = 0
                                End If
                            End If
                            xPACK_QUANTITY = Replace(xPACK_QUANTITY, ",", "")
                            If Not IsNumeric(xPACK_QUANTITY) Then xPACK_QUANTITY = 0
                            
                            Set rsdata1 = cnn1.Execute("SELECT pack_code FROM tbm_packing WHERE lower(pack_code) = lower('" & xPACK & "') or lower(pack_name) = lower('" & xPACK & "')")
                            If Not rsdata1.EOF Then
                                xPACK_CODE = rsdata1("pack_code")
                            Else
                                StrErr4 = "PACK : " & xPACK & " (column P) tidak di kenal di master data"
                                Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping Detail', '" & MyFile & "', " & xrow & ", '" & StrErr4 & "',now())")
                            End If
                            
                            xSPECIFICATION = xlSheet.Cells(xrow, "AO").Value
                            
                            xINVOICE_NO = Trim(xlSheet.Cells(xrow, "AI").Value)
                            If xINVOICE_NO = "" Then xINVOICE_NO = "-"
                            
                            xINVOICE_DT = xlSheet.Cells(xrow, "AJ").Value
                            If Not IsDate(xINVOICE_DT) Then xINVOICE_DT = ""
                            If InStr(xINVOICE_DT, "1900") > 0 Then xINVOICE_DT = ""

                            xINVOICE_ORIGIN = xQUANTITY * xPrice
                            xINVOICE_ORIGIN = Round(xINVOICE_ORIGIN, 2)
                            
                            xINVOICE_AMOUNT = xlSheet.Cells(xrow, "X").Value
                            If Not IsNumeric(xINVOICE_AMOUNT) Then xINVOICE_AMOUNT = 0
                            If xINVOICE_AMOUNT = "" Then xINVOICE_AMOUNT = 0
                            xINVOICE_AMOUNT = Round(xINVOICE_AMOUNT, 2)
                            
                        Else
                            StrErr4 = "Header BL No : " & xBL & " belum ada. Detil data tidak bisa di tambahkan"
                            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Shipping Detail', '" & MyFile & "', " & xrow & ", '" & StrErr4 & "',now())")
                        End If
                        
                        If StrErr4 = "" And xQUANTITY > 0 Then
                          Set rsdata1 = cnn1.Execute("Select shipment_no from tbl_shipping where shipment_no = '" & xSHIPMENT_NO & "'")
                          If Not rsdata1.EOF Then
                          
                            Set rsdata1 = cnn1.Execute("Select shipment_no from tbl_shipping_detail where shipment_no = " & xSHIPMENT_NO & " and po_no = '" & xPO_NO & "' and po_item = '" & xPO_ITEM & "'")
                            If rsdata1.EOF Then
                        
                                strsql = ""
                                strsql = strsql & " Insert into tbl_shipping_detail (SHIPMENT_NO, PO_NO, PO_ITEM, MATERIAL_CODE, " _
                                    & "QUANTITY, PACK_QUANTITY, PACK_CODE, SPECIFICATION) value (" _
                                    & "" & xSHIPMENT_NO & ",'" & xPO_NO & "','" & xPO_ITEM & "','" & xMATERIAL_CODE & "', " _
                                    & "" & xQUANTITY & "," & xPACK_QUANTITY & ",'" & xPACK_CODE & "','" & xSPECIFICATION & "'); "
                                
                                'MsgBox (strsql)
                                Set rsdata1 = cnn1.Execute(strsql)
                            Else
                                strsql = ""
                                strsql = strsql & " Update tbl_shipping_detail Set " _
                                    & "MATERIAL_CODE='" & xMATERIAL_CODE & "', " _
                                    & "QUANTITY=" & xQUANTITY & ", PACK_QUANTITY=" & xPACK_QUANTITY & ", " _
                                    & "PACK_CODE='" & xPACK_CODE & "', SPECIFICATION='" & xSPECIFICATION & "' " _
                                    & "Where SHIPMENT_NO=" & xSHIPMENT_NO & " and PO_NO='" & xPO_NO & "' and PO_ITEM='" & xPO_ITEM & "' ; "
                                
                                'MsgBox (strsql)
                                Set rsdata1 = cnn1.Execute(strsql)
                            End If
                            
                            Set rsdata1 = cnn1.Execute("Select shipment_no from tbl_shipping_invoice where shipment_no = " & xSHIPMENT_NO & " and po_no = '" & xPO_NO & "' and ord_no = '" & xPO_ITEM & "'")
                            If rsdata1.EOF Then
                                strsql = ""
                                strsql = strsql & " Insert into tbl_shipping_invoice (SHIPMENT_NO, PO_NO, ORD_NO, INVOICE_NO, " _
                                    & "INVOICE_DT, INVOICE_ORIGIN, INVOICE_AMOUNT) value (" _
                                    & "" & xSHIPMENT_NO & ",'" & xPO_NO & "','" & xPO_ITEM & "','" & xINVOICE_NO & "', " _
                                    & "IF('" & xINVOICE_DT & "'='',NULL,'" & xINVOICE_DT & "')," & xINVOICE_ORIGIN & "," & xINVOICE_AMOUNT & "); "
                            
                            
                                'MsgBox (strsql)
                                Set rsdata1 = cnn1.Execute(strsql)
                            Else
                                strsql = ""
                                strsql = strsql & " Update tbl_shipping_invoice Set " _
                                    & "INVOICE_NO='" & xINVOICE_NO & "', INVOICE_DT=IF('" & xINVOICE_DT & "'='',NULL,'" & xINVOICE_DT & "'), " _
                                    & "INVOICE_ORIGIN=" & xINVOICE_ORIGIN & ", INVOICE_AMOUNT=" & xINVOICE_AMOUNT & " " _
                                    & "Where SHIPMENT_NO=" & xSHIPMENT_NO & " and PO_NO='" & xPO_NO & "' and ORD_NO='" & xPO_ITEM & "' ; "
                            
                                'MsgBox (strsql)
                                Set rsdata1 = cnn1.Execute(strsql)
                            End If
                          End If
                        End If
                    End If
                End If
                
                xrow = xrow + 1
            Loop
            StrErr = "Shipment Data - End Process : " & xrow & " lines"
            Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('End Process', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")

            xdate = Format(Date, "Long Date")
            xTime = Format(Time, "hhmmssAMPM")
            
            'backup file yang selesai di process
            nmfile = Mid(MyFile, 1, Len(MyFile) - 4) & " Process " & xdate & "_" & xTime & ".ofl"
            xlBook.SaveAs MyPathCopy & "\" & nmfile
            
            xlApp.Application.Quit
            Set ExcelSheet = Nothing
            Set xlBook = Nothing
            Set xlApp = Nothing
            
            'delete file untuk menerima file baru yang akan di process
            'Kill (MyPath & "\" & MyFile)
            
        End If
        
NextFile:
        MyFile = Dir
    Loop
    
EndProc:
    'amankan data PO dari data2 detail yg gagal di insert
    Set rsdata1 = cnn1.Execute("DELETE FROM tbl_po WHERE po_no NOT IN (SELECT po_no FROM tbl_po_detail); ")
    
    'amankan data Shipping dari data2 detail yg gagal di insert
    'Set rsdata1 = cnn1.Execute("DELETE FROM tbl_shipping_invoice WHERE shipment_no NOT IN (SELECT shipment_no FROM tbl_shipping_detail); ")
    'Set rsdata1 = cnn1.Execute("DELETE FROM tbl_shipping_detail WHERE shipment_no NOT IN (SELECT shipment_no FROM tbl_shipping_invoice); ")
    'Set rsdata1 = cnn1.Execute("DELETE FROM tbl_shipping WHERE shipment_no NOT IN (SELECT shipment_no FROM tbl_shipping_detail); ")

    Set rsdata1 = Nothing
    Set cnn1 = Nothing
    Unload Me
    
Exit Sub        ' Exit to avoid handler.
ErrorHandler:   ' Error-handling routine.

     xlApp.Application.Quit
     Set ExcelSheet = Nothing
     Set xlBook = Nothing
     Set xlApp = Nothing
       
     StrErr = "Abort Process on line : " & xrow
     Set rsdata1 = cnn1.Execute("insert into tbr_offprocess (msg_type, msg_file, msg_recno, msg_text, msg_date) value ('Error', '" & MyFile & "', " & xrow & ", '" & StrErr & "', now())")
     
     GoTo EndProc
End Sub

