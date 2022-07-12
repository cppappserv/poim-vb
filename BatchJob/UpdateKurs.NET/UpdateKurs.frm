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
Dim xSelisihHari, xHari
Dim xDate

Set cnn1 = New ADODB.Connection
cnn1.Open "DSN=impr;UID=root;pwd=vbdev"
    
Set cnnWr = New ADODB.Connection
cnnWr.Open "DSN=impr;UID=root;pwd=vbdev"


strsql = "SELECT t1.currency_code, t1.effective_date, t2.kurs, t2.kurs_pajak, DATE_FORMAT(NOW(),'%Y-%m-%d') today, DATEDIFF(NOW(), t1.effective_date) difday " _
       & "FROM " _
       & "(SELECT currency_code, DATE_FORMAT(MAX(effective_date),'%Y-%m-%d') effective_date FROM tbm_kurs GROUP BY currency_code) t1 " _
       & "Left Join " _
       & "(SELECT currency_code, effective_date, kurs, kurs_pajak FROM tbm_kurs) t2 " _
       & "ON t1.currency_code=t2.currency_code AND t1.effective_date=t2.effective_date "


Set rsdata1 = cnn1.Execute(strsql)
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
        xHari = 0
        xSelisihHari = rsdata1("difday")
        xDate = rsdata1("effective_date")

        Do While xHari < xSelisihHari
            xHari = xHari + 1

            strsql = "Insert into tbm_kurs (currency_code, effective_date, kurs, kurs_pajak) value " _
               & "('" & rsdata1("currency_code") & "', " _
               & "DATE_ADD('" & xDate & "', INTERVAL " & xHari & " DAY) ,'" _
               & rsdata1("kurs") & "','" & rsdata1("kurs_pajak") & "') "
    
            'MsgBox (strsql)
            Set rsdataWr = cnnWr.Execute(strsql)
        Loop
        rsdata1.MoveNext
    Loop
End If

strsql = "Select currency_code, kurs, kurs_pajak, min(effective_date) mindt, weekofyear(effective_date) weekdt, year(effective_date) yeardt " _
       & "from tbm_kurs " _
       & "Where date_format(effective_date,'%W') in ('Monday','Senin') " _
       & "group by currency_code, weekofyear(effective_date), year(effective_date) "
       
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
