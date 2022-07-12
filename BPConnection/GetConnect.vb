Imports System.IO
Imports System.Data.OleDb
Imports Microsoft.Win32

Public Class MyConnection

    Public Shared Function FncMyConnValue(ByVal ConfigData As SetConfig) As String()
        Dim ConnValue(3) As String
        Dim StrValue As String

        If ConfigData.KeepIn = "File" Then
            'Dim ConnectionFile As String = "koneksi.enc"
            'Dim ConnectionFile As String = ConfigData.FullName
            'Dim FullName As String = My.Application.Info.DirectoryPath & "\" & ConnectionFile

            If My.Computer.FileSystem.FileExists(ConfigData.FullName) Then
                Dim objStreamReader As StreamReader
                Dim Cnt As Integer

                'Pass the file path and the file name to the StreamReader constructor.
                objStreamReader = New StreamReader(ConfigData.FullName)

                'Read the first line of text.
                StrValue = objStreamReader.ReadLine

                'Continue to read until you reach the end of the file.
                Do While Not StrValue Is Nothing
                    Cnt = Cnt + 1

                    Select Case Cnt
                        Case 1 : ConnValue(0) = EncryptionUtility.ReadEncryption(StrValue)
                        Case 2 : ConnValue(1) = EncryptionUtility.ReadEncryption(StrValue)
                        Case 3 : ConnValue(2) = EncryptionUtility.ReadEncryption(StrValue)
                        Case 4 : ConnValue(3) = EncryptionUtility.ReadEncryption(StrValue)
                    End Select

                    'Read the next line.
                    StrValue = objStreamReader.ReadLine
                Loop
                'Close the file.
                objStreamReader.Close()
                Return ConnValue
            Else
                Return ConnValue
            End If

        Else
            ' ambil dari registry
            'Dim KeyName As String = Registry.LocalMachine.ToString & "\SOFTWARE\CPI\BukuPetambak"
            Dim SetItem() As String = {"DBServer", "DBName", "UserName", "UserPass"}

            For i = 0 To SetItem.Length - 1
                Try
                    StrValue = CStr(Registry.GetValue(ConfigData.FullName, SetItem(i), ""))
                    ConnValue(i) = EncryptionUtility.ReadEncryption(StrValue)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
                End Try

            Next

            Return ConnValue
        End If
    End Function

    Public Shared Function FncMyConnStr(ByVal ConnValue() As String) As String
        Dim ConnStr As String
        If ConnValue(0) = "" Or ConnValue(1) = "" Or ConnValue(2) = "" Or ConnValue(3) = "" Then
            ConnStr = "False"
        Else
            ConnStr = String.Format("Persist Security Info=False;server={0};database={1};user id={2};Password={3}", _
                                     ConnValue(0), ConnValue(1), ConnValue(2), ConnValue(3))
        End If
        Return ConnStr
    End Function

    'Public Shared Function FncMyConnection(Optional ByVal KeepConnectionIn As String = "Registry") _
    Public Shared Function FncMyConnection(ByVal ConfigData As SetConfig) _
                                           As MySqlConnection
        Dim ConnValue(3), ConnStr As String
        Dim MyConn As MySqlConnection = Nothing
        'If ConnStr = "" Then
        ConnValue = FncMyConnValue(ConfigData)
        ConnStr = FncMyConnStr(ConnValue)
        'End If

        If ConnStr <> "False" Then
            Try
                MyConn = New MySqlConnection(ConnStr)
                ConnStr = Nothing
                MyConn.Open()
            Catch ex1 As MySqlException
                MsgBox("Connection fail" & vbCrLf & ex1.Message, MsgBoxStyle.OkOnly, "Information")
                MyConn = Nothing
            Catch ex2 As ArgumentException
                MsgBox("Connection fail" & vbCrLf & ex2.Message, MsgBoxStyle.OkOnly, "Information")
                MyConn = Nothing
            End Try
        Else
            MsgBox("File Koneksi Tidak Ada Atau Isinya Salah.", MsgBoxStyle.OkOnly, "Informasi")
        End If

        Return MyConn
    End Function
    Public Shared Function CekMyConn(ByVal MyConn As MySqlConnection, ByVal ConfigData As SetConfig) _
                                  As MySqlConnection
        If MyConn Is Nothing Then
            MyConn = FncMyConnection(ConfigData)
        Else
            If MyConn.State.ToString = "Closed" Then
                MyConn.Open()
            End If
        End If
        Return MyConn
    End Function
    Public Shared Sub CloseMyConn(ByVal MyConn As MySqlConnection)
        Try
            If Not MyConn Is Nothing Then
                If MyConn.State.ToString = "Open" Then
                    MyConn.Close()
                End If
            End If
        Catch ex As Exception
            MsgBox("Mysql Connection gagal ditutup.", MsgBoxStyle.Exclamation, "Error")
            MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
        End Try
    End Sub

End Class

Public Class OleConnection

    Public Shared Function FncOleConnection(ByVal FileName As String) As OleDbConnection
        Dim OleConn As OleDbConnection = Nothing
        Dim OleConnStr As String
        OleConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
                     "Data Source=" & Trim(FileName) & ";" & _
                     "Extended Properties=""Excel 8.0;HDR=YES"""
        Try
            OleConn = New OleDbConnection(OleConnStr)
            OleConn.Open()
        Catch ex As Exception
            MsgBox("File Excel Tidak Dapat Dibuka disebabkan." & vbCrLf & _
                   ex.Message, MsgBoxStyle.Exclamation, "Error")
            OleConn = Nothing
        End Try

        Return OleConn
    End Function

    Public Shared Sub CloseOleConn(ByVal OleConn As OleDbConnection)
        Try
            If Not OleConn Is Nothing Then
                If OleConn.State.ToString = "Open" Then
                    OleConn.Close()
                End If
            End If
        Catch ex As Exception
            'If UserData.TrapErr Then
            MsgBox("Ole Connection gagal ditutup.", MsgBoxStyle.Exclamation, "Error")
            'If UserData.DbgAccess Then
            MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
            'End If
            'End If
        End Try
    End Sub
End Class