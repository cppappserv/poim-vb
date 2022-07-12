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

Set cnn1 = New ADODB.Connection
cnn1.Open "DSN=impr;UID=root;pwd=vbdev"

Set cnnWr = New ADODB.Connection
cnnWr.Open "DSN=impr;UID=root;pwd=vbdev"

'BPJUM ----------------------------------------------------------------------
strsql = "SELECT getsqlbilling_byshipbyexpbycode_pp('') AS strCE FROM DUAL"
Set rsdata1 = cnn1.Execute(strsql)
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
        strsqlCE = rsdata1("strCE")
        rsdata1.MoveNext
    Loop
End If

If strsqlCE <> "" Then
    strsqlw = "DROP TABLE IF EXISTS tmp_costEMKL_pp"
    Set rsdataWr = cnnWr.Execute(strsqlw)

    strsqlCE = "CREATE TABLE tmp_costEMKL_pp (" & strsqlCE & ")"
    Set rsdata1 = cnn1.Execute(strsqlCE)
End If

'Billing --------------------------------------------------------------------
strsql = "SELECT getsqlbilling_byshipbyexpbycode_bi('') AS strCE FROM DUAL"
Set rsdata1 = cnn1.Execute(strsql)
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
        strsqlCE = rsdata1("strCE")
        rsdata1.MoveNext
    Loop
End If

If strsqlCE <> "" Then
    strsqlw = "DROP TABLE IF EXISTS tmp_costEMKL_bi"
    Set rsdataWr = cnnWr.Execute(strsqlw)

    strsqlCE = "CREATE TABLE tmp_costEMKL_bi (" & strsqlCE & ")"
    Set rsdata1 = cnn1.Execute(strsqlCE)
End If

'Billing dan BPJUM ----------------------------------------------------------
strsql = "SELECT getsqlbilling_byshipbyexpbycode_bipp('') AS strCE FROM DUAL"
Set rsdata1 = cnn1.Execute(strsql)
If Not rsdata1.EOF Then
    Do While Not rsdata1.EOF
        strsqlCE = rsdata1("strCE")
        rsdata1.MoveNext
    Loop
End If

If strsqlCE <> "" Then
    strsqlw = "DROP TABLE IF EXISTS tmp_costEMKL_bipp"
    Set rsdataWr = cnnWr.Execute(strsqlw)

    strsqlCE = "CREATE TABLE tmp_costEMKL_bipp (" & strsqlCE & ")"
    Set rsdata1 = cnn1.Execute(strsqlCE)
End If

Set rsdata1 = Nothing
Set rsdataWr = Nothing
Set cnn1 = Nothing
Set cnnWr = Nothing
Unload Me
End Sub














