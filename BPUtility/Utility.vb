Imports Microsoft.Win32
Imports System.Data.OleDb
Imports System.IO
Imports System.Security

Public Class MyDataUtility
    Private Shared DbgMsg As String = "SQL yang gagal adalah "

    Public Shared Function DBQueryUpdate(ByVal SQLStr As String, ByVal MyConn As MySqlConnection, _
                                         ByVal TransTipe As Boolean, ByVal ErrMsg As String, _
                                         ByVal UserData As SetUserData) _
                                         As Integer
        ' cek myconn - added by estrika 251010
        MyConn = CekMyConn(MyConn, UserData.ConfigData)
        If MyConn Is Nothing Then Return -1

        ' inisialisasi
        Dim SQLSuccess As String
        Dim SQLArr As String()
        Dim Pembagi As String() = {" "}
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim AffectedRows As Integer

        SQLArr = SQLStr.Split(Pembagi, StringSplitOptions.None)

        Try
            ' try execute query
            MyComm.CommandText = SQLStr
            AffectedRows = MyComm.ExecuteNonQuery
            ' update ke history transaksi untuk tipe Insert, Update & Delete
            If UserData.SaveAudittrail Then
                SQLArr(0) = SQLArr(0).ToUpper()
                If SQLArr(0) = "INSERT" Or SQLArr(0) = "UPDATE" Or SQLArr(0) = "DELETE" Then
                    SQLSuccess = "INSERT INTO tbl_audittrail (user_ct, transactiondt, transaction) " & _
                                 "VALUES ('" & UserData.UserCT & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                                         "'" & EscapeStr(SQLStr) & "')"
                    UpdHistory(SQLSuccess, MyConn, UserData)
                End If
            End If

            Return AffectedRows

        Catch ex As Exception
            ' inisialisasi Query yg gagal
            If ErrMsg.Equals("") Then
                ErrMsg = "Perintah SQL gagal dijalankan."
            End If
            ' debug error msg
            If UserData.TrapErr Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
            ' Rollback data
            If TransTipe Then
                Try
                    MyComm.CommandText = "ROLLBACK"
                    MyComm.ExecuteNonQuery()
                Catch ex1 As Exception
                    MsgBox("Rollback Data Gagal.", MsgBoxStyle.Exclamation, "Error")
                End Try
            End If

            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                                             "'" & EscapeStr(SQLStr) & "', '" & EscapeStr(ex.Message) & "')"
            UpdErrorLog(SQLError, MyConn, UserData)

            Return -1
        End Try
    End Function

    Public Shared Function DBQueryMyReader(ByVal SQLStr As String, ByVal MyConn As MySqlConnection, _
                                           ByVal ErrMsg As String, ByVal UserData As SetUserData) _
                                           As MySqlDataReader
        ' inisialisasi
        Dim MyReader As MySqlDataReader = Nothing
        Dim MyCmd As New MySqlCommand(SQLStr, MyConn)

        Try
            MyReader = MyCmd.ExecuteReader()
            Return MyReader
        Catch ex As MySqlException
            If ErrMsg.Equals("") Then
                ErrMsg = "Perintah SQL gagal dijalankan."
            End If
            ' debug error msg
            If UserData.TrapErr Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
            ' update error log untuk query yang gagal
            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                                             "'" & EscapeStr(SQLStr) & "', '" & EscapeStr(ex.Message) & "')"
            UpdErrorLog(SQLError, MyConn, UserData)
            Return Nothing
        End Try
    End Function

    Public Shared Function DBQueryDataReader(ByVal SQLStr As String, ByVal MyConn1 As MySqlConnection, _
                                             ByVal ErrMsg As String, ByVal UserData As SetUserData) _
                                             As DataTableReader
        ' inisialisasi            
        Dim Reader As DataTableReader = Nothing
        Dim MyAdapter = New MySqlDataAdapter(SQLStr, MyConn1)
        Dim Data = New DataSet()

        Try
            MyAdapter.Fill(Data)
            Reader = Data.CreateDataReader
            Return Reader

        Catch ex1 As MySqlConversionException
            If ErrMsg.Equals("") Then
                ErrMsg = "Ada konversi tipe data yang gagal."
            End If
            ' debug error msg
            If UserData.TrapErr Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex1.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
            ' update error log untuk query yang gagal
            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                                             "'" & EscapeStr(SQLStr) & "', '" & EscapeStr(ex1.Message) & "')"
            Dim MyConn2 As MySqlConnection = Nothing
            UpdErrorLog(SQLError, MyConn2, UserData)
            Return Nothing

        Catch ex2 As MySqlException
            If ErrMsg.Equals("") Then
                ErrMsg = "Perintah SQL gagal dijalankan."
            End If
            ' debug error msg
            If UserData.TrapErr Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex2.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
            ' update error log untuk query yang gagal
            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                                             "'" & EscapeStr(SQLStr) & "', '" & EscapeStr(ex2.Message) & "')"
            Dim MyConn2 As MySqlConnection = Nothing
            UpdErrorLog(SQLError, MyConn2, UserData)
            Return Nothing
        End Try
    End Function

    Public Shared Function DBQueryDataTable(ByVal SQLStr As String, ByVal MyConn As MySqlConnection, _
                                            ByVal JoinTables As String, ByVal ErrMsg As String, _
                                            ByVal UserData As SetUserData) _
                                            As DataTable
        Try
            Dim MyAdapter As New MySqlDataAdapter(SQLStr, MyConn)

            If JoinTables = "" Then
                Dim MyDataSet As New DataSet
                MyAdapter.Fill(MyDataSet)
                Return MyDataSet.Tables(0)
            Else
                Dim MyDataSet As New DataSet(JoinTables)
                MyAdapter.Fill(MyDataSet, JoinTables)
                Return MyDataSet.Tables(JoinTables)
            End If

        Catch ex As Exception
            If ErrMsg.Equals("") Then
                ErrMsg = "Perintah SQL gagal dijalankan."
            End If

            If UserData.TrapErr Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If

            ' update error log untuk query yang gagal
            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                                             "'" & EscapeStr(SQLStr) & "', '" & EscapeStr(ex.Message) & "')"
            Dim MyConn2 As MySqlConnection = Nothing 'FncMyConnection()
            UpdErrorLog(SQLError, MyConn2, UserData)
            'CloseMyConn(MyConn2, UserData)

            Return Nothing
        End Try
    End Function

    Public Shared Function DBQueryGetTotalRows(ByVal SQLStr As String, ByVal MyConn As MySqlConnection, _
                                               ByVal ErrMsg As String, ByVal MustCount As Boolean, _
                                               ByVal UserData As SetUserData) _
                                               As Integer

        Dim MyReader As MySqlDataReader = Nothing
        Dim i As Integer = 0
        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)

        Try
            If MyReader Is Nothing Then
                i = -1
            Else
                If MyReader.HasRows Then
                    If MustCount = False Then
                        i = 1 ' return 1 if knowing total not necessary
                    Else
                        While (MyReader.Read())
                            i += 1
                        End While
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

        CloseMyReader(MyReader, UserData)
        Return i

    End Function

    Private Shared Sub UpdHistory(ByVal SQLStr As String, ByVal MyConn As MySqlConnection, _
                                  ByVal UserData As SetUserData)
        Try
            Dim MyComm As New MySqlCommand(SQLStr, MyConn)
            MyComm.ExecuteNonQuery()

        Catch ex As Exception
            If UserData.TrapErr Then
                MsgBox("History update data gagal dicatat.", MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If

            SQLStr = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd HH:mm:ss") & "', " & _
                             "'" & EscapeStr(SQLStr) & "', '" & EscapeStr(ex.Message) & "')"
            UpdErrorLog(SQLStr, MyConn, UserData)
        End Try

    End Sub

    Public Shared Sub UpdErrorLog(ByVal SQLStr As String, ByVal MyConn As MySqlConnection, _
                                  ByVal UserData As SetUserData)
        Dim CloseConn As Boolean
        If MyConn Is Nothing Then
            MyConn = FncMyConnection(UserData.ConfigData)
            CloseConn = True
        End If
        Try
            Dim MyComm As New MySqlCommand(SQLStr, MyConn)
            MyComm.ExecuteNonQuery()

        Catch ex As Exception
            If UserData.TrapErr Then
                MsgBox("History error gagal dicatat.", MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message & vbCrLf & DbgMsg & vbCrLf & SQLStr, MsgBoxStyle.Information, "Debug Information")
                End If
            End If

        Finally
            If CloseConn Then
                CloseMyConn(MyConn)
                MyConn = Nothing
            End If
        End Try
    End Sub

    Public Shared Sub CloseMyReader(ByVal MyReader As MySqlDataReader, _
                                    ByVal UserData As SetUserData)
        Try
            If Not MyReader Is Nothing Then
                If Not MyReader.IsClosed Then
                    MyReader.Close()
                End If
            End If
        Catch ex As Exception
            If UserData.TrapErr Then
                MsgBox("MysqlDataReader gagal ditutup.", MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
        End Try
    End Sub

    Public Shared Function EscapeStr(ByVal SQLStr As String) As String
        Try
            Return MySqlHelper.EscapeString(SQLStr)
        Catch ex As Exception
            Return SQLStr
        End Try
    End Function
    'Added by prie 03/05/2009
    Public Shared Sub CloseDataReader(ByVal MyDataReader As DataTableReader, _
                                  ByVal UserData As SetUserData)
        Try
            If Not MyDataReader Is Nothing Then
                If Not MyDataReader.IsClosed Then
                    MyDataReader.Close()
                End If
            End If
        Catch ex As Exception
            If UserData.TrapErr Then
                MsgBox("DataTableReader gagal ditutup.", MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
        End Try
    End Sub
    'added by estrika 25102010
    Public Shared Function DBCheckExistTable(ByVal TableName As String, ByVal MyConn As MySqlConnection, _
                                             ByVal UserData As SetUserData) As Boolean
        MyConn = CekMyConn(MyConn, UserData.ConfigData)
        If MyConn Is Nothing Then Return False

        If DBQueryGetTotalRows("SELECT table_name FROM information_schema.tables " & _
                               "WHERE table_schema='" & MyConn.Database & "' AND table_name='" & TableName & "'", _
                               MyConn, "Gagal cek status table", False, UserData) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function DBCreateTableFromTable(ByVal FrTableName As String, ByVal ToTableName As String, _
                                                  ByVal MyConn As MySqlConnection, ByVal Userdata As SetUserData, _
                                                  Optional ByVal DropExist As Boolean = False, Optional ByVal ToEngineMemory As Boolean = False) _
                                                  As Integer
        ' 0 = success , 1 = fail in connection , 2 = fail on get structure
        ' 3 = destination table already exist , 4 = fail on create new table
        Dim SQLStr As String = ""
        MyConn = CekMyConn(MyConn, Userdata.ConfigData)

        If MyConn Is Nothing Then Return 1

        Dim MyReader As MySqlDataReader = DBQueryMyReader("SHOW CREATE TABLE " & FrTableName, MyConn, _
                                                          "Gagal baca struktur table " & FrTableName, Userdata)
        Try
            If MyReader.HasRows Then
                MyReader.Read()
                Try
                    SQLStr = MyReader.GetString(1).ToLower ' ("Create Table")
                Catch ex As Exception
                    Return 2
                End Try
            End If
        Catch ex As Exception
            Return 2
        Finally
            CloseMyReader(MyReader, Userdata)
        End Try

        If DropExist Then
            DBQueryUpdate("DROP TABLE IF EXISTS `" & ToTableName & "`", MyConn, False, _
                          "Gagal Hapus table " & ToTableName, Userdata)
        Else
            If DBCheckExistTable(ToTableName, MyConn, Userdata) Then
                MsgBox("Table " & ToTableName & " sudah ada", MsgBoxStyle.Information, "Informasi")
                Return 3
            End If
        End If

        SQLStr = SQLStr.Replace(FrTableName.ToLower, ToTableName.ToLower)
        If ToEngineMemory Then
            'TODO : cek masalah engine
            'SQLStr = SQLStr.Replace("engine=innodb", "engine=memory")
            'SQLStr = SQLStr.Replace("engine=myisam", "engine=memory")
        End If

        If DBQueryUpdate(SQLStr, MyConn, False, "Gagal create table " & ToTableName, Userdata) < 0 Then Return 4

        Return 0 ' success
    End Function
End Class

Public Class OleDataUtility

    Public Shared Function OleQueryReader(ByVal SQLStr As String, ByVal OleConn As OleDbConnection, _
                                          ByVal ErrMsg As String, ByVal UserData As SetUserData) _
                                          As OleDbDataReader
        ' inisialisasi
        Dim OleReader As OleDbDataReader = Nothing
        Dim OleCmd As New System.Data.OleDb.OleDbCommand(SQLStr, OleConn)

        Try
            OleReader = OleCmd.ExecuteReader
            Return OleReader
        Catch ex1 As OleDbException
            If ErrMsg.Equals("") Then
                ErrMsg = "Perintah SQL gagal dijalankan."
            End If
            ' debug error msg
            If UserData.TrapErr Then
                MsgBox(ErrMsg, MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex1.Message & vbCrLf & "SQL yang gagal adalah " & vbCrLf & _
                           SQLStr, MsgBoxStyle.Exclamation, "Debug Information")
                End If
            End If
            ' update error log untuk query yang gagal
            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd hh:mm:ss") & "', " & _
                                             "'" & MyDataUtility.EscapeStr(SQLStr) & "', '" & MyDataUtility.EscapeStr(ex1.Message) & "')"

            Dim MyConn As MySqlConnection = Nothing
            MyDataUtility.UpdErrorLog(SQLError, MyConn, UserData)
            Return Nothing
        Catch ex2 As Exception
            If UserData.TrapErr Then
                MsgBox("Cek kembali file data yang akan dibaca.", MsgBoxStyle.Exclamation, "Perhatian")
            End If
            Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                     "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd hh:mm:ss") & "', " & _
                                             "'" & MyDataUtility.EscapeStr(SQLStr) & "', '" & MyDataUtility.EscapeStr(ex2.Message) & "')"

            Dim MyConn As MySqlConnection = Nothing
            MyDataUtility.UpdErrorLog(SQLError, MyConn, UserData)
            Return Nothing
        End Try
    End Function

    Public Shared Sub CloseOleReader(ByVal OleReader As OleDbDataReader, _
                                     ByVal UserData As SetUserData)
        Try
            If Not OleReader Is Nothing Then
                If Not OleReader.IsClosed Then
                    OleReader.Close()
                End If
            End If
        Catch ex As Exception
            If UserData.TrapErr Then
                MsgBox("Ole Reader gagal ditutup.", MsgBoxStyle.Exclamation, "Error")
                If UserData.DbgAccess Then
                    MsgBox(ex.Message, MsgBoxStyle.Information, "Debug Information")
                End If
            End If
        End Try
    End Sub

End Class

Public Class RegistryUtility
    
    Public Shared Function SetRegistry(ByVal ValueName As String, ByVal ValueData As Object, _
                                       ByVal ValueKind As RegistryValueKind, ByVal UserData As SetUserData) _
                                       As Boolean
        Dim HaveError, Result As Boolean
        Dim InfoError As String = ""
        Dim DetailError As String = ""
        Try
            Registry.SetValue(UserData.ConfigData.FullName, ValueName, ValueData, ValueKind)
            Result = True
        Catch ex1 As ArgumentNullException
            HaveError = True
            InfoError = "Value is null."
            DetailError = ex1.Message
        Catch ex2 As ArgumentException
            HaveError = True
            InfoError = "KeyName '" & UserData.ConfigData.FullName & "' does not begin with a valid registry root. Or " & _
                        "valueName is longer than the maximum length allowed (255 characters)."
            DetailError = ex2.Message
        Catch ex3 As UnauthorizedAccessException
            HaveError = True
            InfoError = "The '" & UserData.ConfigData.FullName & "' is read-only, and thus cannot be written to; " & _
                        "for example, it is a root-level node."
            DetailError = ex3.Message
        Catch ex4 As SecurityException
            HaveError = True
            InfoError = "The user does not have the permissions required to create or modify registry keys."
            DetailError = ex4.Message
        Finally
            If HaveError Then
                If UserData.TrapErr Then
                    MsgBox("Gagal menulis config di registry.", MsgBoxStyle.Exclamation, "Error")
                    If UserData.DbgAccess Then
                        MsgBox(InfoError & vbCrLf & DetailError & vbCrLf, MsgBoxStyle.Exclamation, "Debug Information")
                    End If
                End If
                Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                         "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd hh:mm:ss") & "', " & _
                                                 "'" & MyDataUtility.EscapeStr(InfoError) & "', '" & MyDataUtility.EscapeStr(DetailError) & "')"
                Dim MyConn As MySqlConnection = Nothing
                MyDataUtility.UpdErrorLog(SQLError, MyConn, UserData)
                Result = False
            End If
        End Try
        Return Result
    End Function

    Public Shared Function GetRegistry(ByVal ValueName As String, ByVal UserData As SetUserData) _
                                       As String
        Dim HaveError As Boolean
        Dim Result As String = ""
        Dim InfoError As String = ""
        Dim DetailError As String = ""
        Try
            Result = CStr(Registry.GetValue(UserData.ConfigData.FullName, ValueName, ""))
            'msgbox(result)
        Catch ex1 As ArgumentException
            HaveError = True
            InfoError = "KeyName '" & UserData.ConfigData.FullName & "' does not begin with a valid registry root. "
            DetailError = ex1.Message
        Catch ex2 As IOException
            HaveError = True
            InfoError = "The '" & UserData.ConfigData.FullName & "' that contains the specified value has been marked for deletion."
            DetailError = ex2.Message
        Catch ex3 As SecurityException
            HaveError = True
            InfoError = "The user does not have the permissions required to read from the registry key."
            DetailError = ex3.Message
        Finally
            If Result Is Nothing Then Result = ""
            If HaveError Then
                If UserData.TrapErr Then
                    MsgBox("Gagal membaca config di registry.", MsgBoxStyle.Exclamation, "Error")
                    If UserData.DbgAccess Then
                        MsgBox(InfoError & vbCrLf & DetailError & vbCrLf, MsgBoxStyle.Exclamation, "Debug Information")
                    End If
                End If
                Dim SQLError As String = "INSERT INTO ot_errorlog (user_id, error_date, query_string, error_msg) " & _
                                         "VALUES ('" & UserData.UserId & "', '" & Format(Now, "yyyy-MM-dd hh:mm:ss") & "', " & _
                                             "'" & MyDataUtility.EscapeStr(InfoError) & "', '" & MyDataUtility.EscapeStr(DetailError) & "')"
                Dim MyConn As MySqlConnection = Nothing
                MyDataUtility.UpdErrorLog(SQLError, MyConn, UserData)
            End If
        End Try
        Return Result
    End Function

End Class