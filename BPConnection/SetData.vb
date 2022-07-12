Imports Microsoft.Win32
Imports System.IO
Imports System.Security.Cryptography

Public Class EncryptionUtility
    Private Const KeyPasword As String = "sjhdKJhfg122bHbhbjbsd.;sedfdsa56&1#s\ae"

    Public Shared Function ReadEncryption(ByVal EncString As String) As String
        Dim Wrapper As New Simple3Des(KeyPasword)
        Dim DecString As String
        Try
            DecString = Wrapper.DecryptData(EncString, "asdfghjkl213=*;'.,")
        Catch ex As System.Security.Cryptography.CryptographicException
            DecString = ""
        End Try
        Return DecString
    End Function

    Public Shared Function GetEncryption(ByVal StrValue As String) As String
        Dim Wrapper As New Simple3Des(KeyPasword)
        Dim EncString As String
        Try
            EncString = Wrapper.EncryptData(StrValue)
        Catch ex As System.Security.Cryptography.CryptographicException
            EncString = ""
        End Try
        Return EncString
    End Function

    Public Shared Sub WriteEncryption(ByVal ItemList() As String, _
                                      Optional ByVal TipeSimpan As String = "File")
        'Dim i As Integer
        Dim Wrapper As New Simple3Des(KeyPasword)

        If TipeSimpan.Equals("File") Then
            Dim ConnectionFile As String = "koneksi.enc"
            Dim FullName As String = My.Application.Info.DirectoryPath & "\" & ConnectionFile
            Dim objStreamWriter As StreamWriter

            'Pass the file path and the file name to the StreamWriter constructor.
            objStreamWriter = New StreamWriter(FullName, False)

            ' read array list
            For i = 0 To ItemList.Length - 1
                ' encrypt data
                objStreamWriter.WriteLine(Wrapper.EncryptData(ItemList(i)))
            Next

            'Close the file.
            objStreamWriter.Close()
        Else
            ' Simpan dalam registry
            Dim KeyName As String = Registry.LocalMachine.ToString & "\SOFTWARE\CPI\POIMPORT"
            Dim SetItem() As String = {"DBServer", "DBName", "UserName", "UserPass"}

            ' read array list
            For i = 0 To ItemList.Length - 1
                ' encrypt data
                Try
                    Registry.SetValue(KeyName, SetItem(i), Wrapper.EncryptData(ItemList(i)), RegistryValueKind.String)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
                End Try
            Next

        End If

    End Sub

End Class

Public NotInheritable Class Simple3Des
    Private TripleDes As New TripleDESCryptoServiceProvider

    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider
        'Dim tDESalg As New TripleDESCryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte = _
            System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function

    Sub New(ByVal key As String)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)

    End Sub

    Public Function EncryptData(ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte = _
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms, _
            TripleDes.CreateEncryptor(), _
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function

    Public Function DecryptData(ByVal EncryptedText As String, _
                                Optional ByVal FlagAccess As String = "") As String

        ' validasi access function
        If EncryptedText Is Nothing Then Return Nothing
        If FlagAccess <> "asdfghjkl213=*;'.," Or EncryptedText.Equals("") Then Return Nothing

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(EncryptedText)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms, _
            TripleDes.CreateDecryptor(), _
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function

End Class

Public Class SetConfig
    Private _KeepIn, _FullName As String

    Public Property KeepIn() As String
        Get
            KeepIn = _KeepIn
        End Get
        Set(ByVal value As String)
            If value = "Registry" Or value = "File" Then
                _KeepIn = value
            Else
                _KeepIn = "Registry"
            End If
        End Set
    End Property

    Public Property FullName() As String
        Get
            FullName = _FullName
        End Get
        Set(ByVal value As String)
            _FullName = value
        End Set
    End Property

End Class
