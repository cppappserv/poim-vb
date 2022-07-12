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

   ' Set ODBC untuk dbPIB --- MS Access
   ' Set ODBC untuk impr --- MySQL

On Error GoTo ErrorHandler

    Dim cnn1, cnn2, cnnWr As New ADODB.Connection
    Dim xBL, xSUP, xSUPNM, xAJU, xPIB, xSPPB
    Dim BCAJU, BCPIB, BCSPPB, BCPIB_DT, BCSPPB_DT, DCNO, DCKD, DCDT
    Dim iRec, iOrd, iLoop, iTot
    Dim xRESKD, xRESTG, xKPBC, xRESDES, xRESKRIP, xCONT
    
    Set cnnWr = New ADODB.Connection
    cnnWr.Open "DSN=impr;UID=root;pwd=vbdev"
    
    Set cnn1 = New ADODB.Connection
    cnn1.Open "DSN=impr;UID=root;pwd=vbdev"
    
    Set cnn2 = New ADODB.Connection
    cnn2.Open "DSN=dbPIB;pwd=MumtazFarisHana"

    strsql = "Select t1.bl_no, t1.received_copydoc_dt, t1.supplier_code, m2.supplier_name, " _
           & "if(t1.aju_no is null, '', t1.aju_no ) aju_no, if(t1.pib_no is null, '', t1.pib_no) pib_no, if(t1.sppb_no is null, '', t1.sppb_no) sppb_no " _
           & "From tbl_shipping t1, tbm_supplier m2 " _
           & "Where t1.supplier_code=m2.supplier_code " _
           & "and received_copydoc_dt >= DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 60 DAY),'%Y-%m-%d') "
    
    Set rsdata1 = cnn1.Execute(strsql)
    If Not rsdata1.EOF Then
        Do While Not rsdata1.EOF
            xBL = rsdata1("bl_no")
            xSUP = rsdata1("supplier_code")
            xSUPNM = rsdata1("supplier_name")
            xAJU = rsdata1("aju_no")
            xPIB = rsdata1("pib_no")
            xSPPB = rsdata1("sppb_no")
            
            '---Update AJU---
                BCAJU = ""
                strsql = "SELECT tblPibDok.CAR From tblPibDok " _
                       & "Where (tblPibDok.DokKd = '705') And (tblPibDok.DokNo= '" & xBL & "')"
            
                Set rsdata2 = cnn2.Execute(strsql)
                If Not rsdata2.EOF Then BCAJU = rsdata2("CAR")
            
                strsql = "Update tbl_shipping set aju_no='" & BCAJU & "' " _
                         & "where bl_no='" & xBL & "' and supplier_code = '" & xSUP & "'"
            
                Set rsdata3 = cnnWr.Execute(strsql)
                
                xAJU = BCAJU
            
            '---Update PIB---
                BCPIB = ""
                strsql = "SELECT tblPibHdr.PibNo, tblPibHdr.PibTg " _
                       & "FROM tblPibHdr " _
                       & "Where tblPibHdr.CAR = '" & xAJU & "'"
                
                
                Set rsdata2 = cnn2.Execute(strsql)
                If Not rsdata2.EOF Then
                    BCPIB = rsdata2("PibNo")
                    BCPIB_DT = rsdata2("PibTg")
                End If

                If BCPIB_DT = "" Then
                    strsql = "Update tbl_shipping set pib_no='" & BCPIB & "' " _
                             & "where aju_no='" & xAJU & "'"
                Else
                    strsql = "Update tbl_shipping set pib_no='" & BCPIB & "', pib_dt = STR_TO_DATE('" & BCPIB_DT & "', '%m/%d/%Y') " _
                             & "where aju_no='" & xAJU & "'"
                End If
                Set rsdata3 = cnnWr.Execute(strsql)
            
            '---Update SPPB dan Respon History---
                BCSPPB = ""
                strsql = "SELECT tblPibRes.DOKRESNO, tblPibRes.DOKRESTG " _
                       & "From tblPibRes " _
                       & "WHERE tblPibRes.RESKD='300' And tblPibRes.CAR = '" & xAJU & "'"
                
                Set rsdata2 = cnn2.Execute(strsql)
                If Not rsdata2.EOF Then
                    BCSPPB = rsdata2("DOKRESNO")
                    BCSPPB_DT = rsdata2("DOKRESTG")
                End If
     
                If BCSPPB_DT = "" Then
                    strsql = "Update tbl_shipping set sppb_no='" & BCSPPB & "' " _
                             & "where aju_no='" & xAJU & "'"
                Else
                    strsql = "Update tbl_shipping set sppb_no='" & BCSPPB & "', sppb_dt = STR_TO_DATE('" & BCSPPB_DT & "', '%m/%d/%Y') " _
                             & "where aju_no='" & xAJU & "'"
                End If
                Set rsdata3 = cnnWr.Execute(strsql)
            
                iRec = 0
                strsql = "Select if(Max(ord_no) is null,0, Max(ord_no)) ord_no from tbl_pib_history " _
                       & "where aju_no='" & xAJU & "'"
                   
                Set rsdata3 = cnnWr.Execute(strsql)
                If Not rsdata3.EOF Then iRec = rsdata3("ord_no")

                strsql = "SELECT tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK,  tblPibRes.KPBC, tblTabel.URAIAN, tblPibRes.DesKripsi " _
                        & "FROM tblPibRes, tblTabel Where tblPibRes.RESKD = tblTabel.KDREC " _
                        & "And tblPibRes.CAR = '" & xAJU & "' " _
                        & "ORDER BY tblPibRes.CAR, tblPibRes.RESKD, tblPibRes.RESTG, tblPibRes.RESWK "
  
                Set rsdata2 = cnn2.Execute(strsql)
                If Not rsdata2.EOF Then
                    iLoop = 1
                    Do While Not rsdata2.EOF
                        If iLoop > iRec Then
                            xRESKD = rsdata2("RESKD")
                            xRESTG = rsdata2("RESTG")
                            xKPBC = rsdata2("KPBC")
                            
                            xRESKRIP = rsdata2("DesKripsi")
                            If Trim(xRESKRIP) <> "" Then xRESKRIP = "[NOTES]: " & xRESKRIP & ""
                            
                            xRESDES = rsdata2("URAIAN") & " " & xRESKRIP
                    
                            If xRESTG = "" Then
                                strsql = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description) " _
                                         & " values ('" & xAJU & "','" & iLoop & "','" & xKPBC & "','" & xRESKD & "','" & xRESDES & "')"
                            Else
                                strsql = "insert into tbl_pib_history (aju_no, ord_no, kpbc_code, status_code, status_description, status_dt) " _
                                         & " values ('" & xAJU & "','" & iLoop & "','" & xKPBC & "','" & xRESKD & "','" & xRESDES & "',STR_TO_DATE('" & xRESTG & "', '%m/%d/%Y'))"
                            End If
                            Set rsdata3 = cnnWr.Execute(strsql)

                        End If
                        iLoop = iLoop + 1
                        rsdata2.MoveNext
                    Loop
                
                '---Update Container---
                strsql = "SELECT tblPibCon.ContNo, tblPibCon.ContUkur, tblPibCon.ContTipe " _
                        & "FROM tblPibCon Where tblPibCon.CAR = '" & xAJU & "' "

                Set rsdata2 = cnn2.Execute(strsql)
                If Not rsdata2.EOF Then
                
                    strsql = "Select shipment_no from tbl_shipping " _
                           & "where aju_no = '" & xAJU & "' "
                           
                    Set rsdata3 = cnnWr.Execute(strsql)
                    If Not rsdata3.EOF Then
                        iRec = rsdata3("shipment_no")
                
                        strsql = "Select count(ord_no) ord_no from tbl_shipping_cont " _
                               & "where shipment_no = '" & iRec & "' "
                   
                        Set rsdata3 = cnnWr.Execute(strsql)
                        iLoop = rsdata3("ord_no")
                    
                        If iLoop = 0 Then
                            Do While Not rsdata2.EOF
                                iLoop = iLoop + 1
                                xContNo = rsdata2("ContNo")
                                xContTipe = rsdata2("ContUkur") & rsdata2("ContTipe")

                                strsql = "insert into tbl_shipping_cont (shipment_no, ord_no, container_no, unit_code) " _
                                         & "values (" & iRec & "," & iLoop & ",'" & xContNo & "','" & xContTipe & "')"

                                Set rsdata3 = cnnWr.Execute(strsql)
                                rsdata2.MoveNext
                            Loop
                        End If
                        
                        xCONT = ""
                        strsql = "SELECT SUM(1) tot, unit_code FROM tbl_shipping_cont " _
                        & " WHERE shipment_no='" & iRec & "' GROUP BY unit_code"
                                   
                        Set rsdata3 = cnnWr.Execute(strsql)
                        Do While Not rsdata3.EOF
                            xCONT = "," & rsdata3("tot") & " x " & rsdata3("unit_code")
                            rsdata3.MoveNext
                        Loop
                            
                        If xCONT <> "" Then
                            xCONT = Mid(xCONT, 2, Len(xCONT) - 1)
                            strsql = "Update tbl_shipping set total_container='" & xCONT & "' where shipment_no='" & iRec & "'"
                            Set rsdata3 = cnnWr.Execute(strsql)
                        End If
                    End If
                End If
                
                '---Update Supporting Documents---
                strsql = "SELECT tblPibDok.DokKd, tblPibDok.DokNo, tblPibDok.DokTg " _
                        & "FROM tblPibDok Where tblPibDok.CAR = '" & xAJU & "' "

                Set rsdata2 = cnn2.Execute(strsql)
                If Not rsdata2.EOF Then
                    strsql = "Select shipment_no from tbl_shipping " _
                           & "where aju_no = '" & xAJU & "' "
                           
                    Set rsdata3 = cnnWr.Execute(strsql)
                    If Not rsdata3.EOF Then
                        iRec = rsdata3("shipment_no")
                
                        strsql = "Select ifnull(0,max(ord_no)) ord_no from tbl_doc_custom " _
                               & "where shipment_no = '" & iRec & "' "
                   
                        Set rsdata3 = cnnWr.Execute(strsql)
                        iLoop = rsdata3("ord_no")
                        Do While Not rsdata2.EOF
                            
			    iLoop = iLoop + 1
                            DCKD = rsdata2("DokKd")
                            DCDT = rsdata2("DokTg")
                            DCNO = rsdata2("DokNo")


                            strsql = "Select doc_code from tbm_document " _
                                   & "where trim(refer_to)=trim('" & DCKD & "') "
                           
                            Set rsdata3 = cnnWr.Execute(strsql)
                            If Not rsdata3.EOF Then DCKD = rsdata3("doc_code")
                            
                            strsql = "select ord_no from tbl_doc_custom " _
                                        & "where shipment_no='" & iRec & "' and doc_code='" & DCKD & "' "
                                
                            Set rsdata3 = cnnWr.Execute(strsql)
                                
                            If rsdata3.EOF Then
                            	strsql = "Insert into tbl_doc_custom (shipment_no, ord_no, doc_code, doc_no, doc_dt, doc_remark) " _
                                	& "values (" & iRec & "," & iLoop & ",'" & DCKD & "','" & DCNO & "',STR_TO_DATE('" & DCDT & "', '%m/%d/%Y'),'')"
                            Else
                                iOrd = rsdata3("ord_no")
                                
                                strsql = "Update tbl_doc_custom  set doc_no = '" & DCNO & "', doc_dt = STR_TO_DATE('" & DCDT & "', '%m/%d/%Y') " _
                                        & "where shipment_no='" & iRec & "' and ord_no='" & iOrd & "' "
                            End If
                            Set rsdata3 = cnnWr.Execute(strsql)

                            rsdata2.MoveNext
                        Loop
                    End If
                End If
                
            End If
            '---End Update SPPB dan Respon History---
            rsdata1.MoveNext
        Loop
        
        strsql = "UPDATE tbl_shipping SET pib_dt = NULL WHERE pib_dt='0000-00-00'"
        Set rsdata1 = cnn1.Execute(strsql)
        
        strsql = "UPDATE tbl_shipping SET sppb_dt = NULL WHERE sppb_dt='0000-00-00'"
        Set rsdata1 = cnn1.Execute(strsql)
    End If

Endprocess:
    Set rsdata1 = Nothing
    Set rsdata2 = Nothing
    Set rsdata3 = Nothing
    Set cnn1 = Nothing
    Set cnn2 = Nothing
    Set cnnWr = Nothing
    Unload Me
    
Exit Sub        ' Exit to avoid handler.
ErrorHandler:   ' Error-handling routine.
    Set rsdata1 = cnn1.Execute("insert into ot_errorlog (user_id, error_date, query_string, error_msg) value (null, now(), '" & strsql & "', 'Abort Prosess UpdatePIB'")

GoTo Endprocess

End Sub
