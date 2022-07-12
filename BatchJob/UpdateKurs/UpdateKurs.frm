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

' Set ODBC untuk impr --- MySQL
Dim cnn1, cnnWr As New ADODB.Connection
Dim xDate, xDate2
Dim xHari

Set cnn1 = New ADODB.Connection
cnn1.Open "DSN=impr;UID=root;pwd=vbdev"
    
Set cnnWr = New ADODB.Connection
cnnWr.Open "DSN=impr;UID=root;pwd=vbdev"


strsql = "Select t1.currency_code, date_format(t1.effective_date,'%W') effective_day, Date_Add(t1.effective_date,interval 1 day) effective_date, Date_Add(t1.effective_date,interval 2 day) effective_date2, t2.kurs_pajak from " _
       & "(select currency_code, max(effective_date) effective_date from tbm_kurs group by currency_code) t1 " _
       & "Left Join " _
       & "(select currency_code, effective_date, kurs, kurs_pajak from tbm_kurs) t2 " _
       & "on t1.currency_code=t2.currency_code and t1.effective_date=t2.effective_date " _
       & "where t1.effective_date < now() "

Set rsdata1 = cnn1.Execute(strsql)
    
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
        
        xDate = rsdata1("effective_date")
        xHari = rsdata1("effective_day")
        strsql = "Insert into tbm_kurs (currency_code, effective_date, kurs, kurs_pajak) value " _
               & "('" & rsdata1("currency_code") & "',STR_TO_DATE('" & xDate & "', '%m/%d/%Y'),,'" & rsdata1("kurs") & "','" & rsdata1("kurs_pajak") & "') "
        
        'MsgBox (strsql)
        Set rsdataWr = cnnWr.Execute(strsql)
        
        If (xHari = "Friday" Or xHari = "Jumat") Then
            xDate2 = rsdata1("effective_date2")
            strsql = "Insert into tbm_kurs (currency_code, effective_date, kurs, kurs_pajak) value " _
               & "('" & rsdata1("currency_code") & "',STR_TO_DATE('" & xDate2 & "', '%m/%d/%Y'),'" & rsdata1("kurs") & "','" & rsdata1("kurs_pajak") & "') "
        
            'MsgBox (strsql)
            Set rsdataWr = cnnWr.Execute(strsql)
        
        End If
        
        rsdata1.MoveNext
    Loop
End If

strsql = "Select currency_code, kurs, kurs_pajak, min(effective_date) mindt, weekofyear(effective_date) weekdt, year(effective_date) yeardt " _
       & "from tbm_kurs " _
       & "group by currency_code, weekofyear(effective_date), year(effective_date) "
       
       '&"Where date_format(effective_date,'%W') in ('Monday','Senin') " _

Set rsdata1 = cnn1.Execute(strsql)
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
        
            strsql = "Update tbm_kurs set kurs = " & rsdata1("kurs") & ", kurs_pajak = " & rsdata1("kurs_pajak") & " " _
                   & "Where currency_code='" & rsdata1("currency_code") & "' " _
                   & "and weekofyear(effective_date)=" & rsdata1("weekdt") & " " _
                   & "and year(effective_date)=" & rsdata1("yeardt") & " "
               
            'MsgBox (strsql)
            Set rsdataWr = cnnWr.Execute(strsql)
        
            rsdata1.MoveNext
    Loop
End If

Set rsdata1 = Nothing
Set rsdataWr = Nothing
Set cnn1 = Nothing
Set cnnWr = Nothing
Unload Me

End Sub
