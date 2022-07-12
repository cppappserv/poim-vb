'Imports frmshin
Public Class DlgPilihanAddress
    'Dim MyConn As MySqlConnection
    Dim SQLStr, ErrMsg As String
    Private _SQLGrid, _SQLFilter As String
    Private _Tables, ItemCode As String

    Public Property SQLGrid() As String
        Get
            Return _SQLGrid
        End Get
        Set(ByVal Value As String)
            _SQLGrid = Value
        End Set
    End Property

    Public Property SQLFilter() As String
        Get
            Return _SQLFilter
        End Get
        Set(ByVal Value As String)
            _SQLFilter = Value
        End Set
    End Property

    Public Property Tables() As String
        Get
            Return _Tables
        End Get
        Set(ByVal Value As String)
            _Tables = Value
        End Set
    End Property

    Private Sub DlgPilihan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs
        Me.Tag = ""
        'MyConn = FncMyConnection()
        'If MyConn Is Nothing Then
        '    FrmSettingConnection.ShowDialog()
        '    MyConn = FncMyConnection()
        '    If MyConn Is Nothing Then
        '        Me.Close()
        '        Exit Sub
        '    End If
        'End If
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If

        ErrMsg = "Gagal baca data."
        DgvResult.DataSource = DBQueryDataTable(SQLGrid, MyConn, Tables, ErrMsg, UserData)
        If Me.Text = "Select Footer Note History" Then DgvResult.Columns(1).Visible = False

        If DgvResult.RowCount > 0 Then
            DgvResult.CurrentCell = DgvResult(0, 0)
            DgvResult_CellClick(sender, ee)
        End If
    End Sub

    Private Sub DlgPilihan_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Tag = "Closing"
        TxtKey1.Text = ""
        TxtKey2.Text = ""
        'CloseMyConn(MyConn)
    End Sub

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub lblKey_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LblKey1.Click
        TxtKey1.Focus()
    End Sub

    Private Sub TxtKey1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKey1.TextChanged
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs

        If Not Tag.Equals("Closing") Then FilterChanged()
        address.Text = ""
        DgvResult_CellClick(sender, ee)
    End Sub

    Private Sub TxtKey2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKey2.TextChanged
        If Not Tag.Equals("Closing") Then FilterChanged()
    End Sub

    Private Sub DgvResult_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DgvResult.CellClick
        Dim brs, kol, pil As Integer
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs
        Dim cnt, a As Integer

        If DgvResult.RowCount > 0 Then
            brs = DgvResult.CurrentCell.RowIndex
            kol = DgvResult.CurrentCell.ColumnIndex
            pil = DgvResult.Item(kol, brs).Value
            address.Clear()

            If Me.Text = "Select Document Address History" Then
                'address.Text = AmbilData("item_description", "tbm_item_history", "item_no=" & pil & " and item_code LIKE 'SI%'")
                address.Text = AmbilData("item_description", "tbm_item_history", "item_no=" & pil & " and item_code LIKE '" & ItemCode & "'")
            Else
                If Me.Text = "Select Survey Req. History" Then
                    'address.Text = AmbilData("item_description", "tbm_item_history", "item_no=" & pil & " and item_code='SR'")
                    address.Text = AmbilData("item_description", "tbm_item_history", "item_no=" & pil & " and item_code='" & ItemCode & "'")
                Else
                    If Me.Text = "Select Footer Note History" Then
                        address.Text = DgvResult.Item(1, brs).Value
                    Else
                        'Get Document Req. Name
                        temp.Text = AmbilData("item_description", "tbm_item_history", "item_no=" & pil & " and item_code='" & ItemCode & "'")
                        cnt = temp.Lines.Count - 1
                        For a = 0 To cnt
                            doc_no_TextChanged(temp.Lines(a).ToString)
                        Next
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub doc_no_TextChanged(ByVal doccode As String)
        Dim strSQL, errMsg As String
        Dim nomor As Integer
        Dim MyReader2 As MySqlDataReader

        strSQL = "select doc_name from tbm_document where doc_code='" & doccode & "'"
        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    nomor = address.Lines.Length + 1
                    If address.Text <> "" Then address.Text = address.Text & Chr(13) & Chr(10)
                    address.Text = address.Text & nomor.ToString & ". " & MyReader2.GetString("doc_name")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Sub
    Private Sub DgvResult_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgvResult.DataSourceChanged
        If DgvResult.RowCount = 0 Then
            BtnOK.Enabled = False
        Else
            BtnOK.Enabled = True
        End If
    End Sub

    Private Sub DgvResult_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DgvResult.DoubleClick
        OK_Button_Click(New Object, New EventArgs)
    End Sub

    Private Sub FilterChanged()
        If TxtKey1.Text = "" And TxtKey2.Text = "" Then
            SQLStr = SQLGrid
        Else
            SQLStr = SQLFilter.Replace("FilterData1", TxtKey1.Text)
            SQLStr = SQLStr.Replace("FilterData2", TxtKey2.Text)
        End If
        ErrMsg = "Gagal baca data."
        DgvResult.DataSource = DBQueryDataTable(SQLStr, MyConn, Tables, ErrMsg, UserData)
    End Sub

    Public Sub New(ByVal kode As String)
        InitializeComponent()
        ItemCode = kode
    End Sub
End Class
