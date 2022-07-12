
Public Class DlgPilihan
    'Dim MyConn As MySqlConnection
    Dim SQLStr, ErrMsg As String
    Private _SQLGrid, _SQLFilter As String
    Private _Tables As String

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
        If Not Tag.Equals("Closing") Then FilterChanged()
    End Sub

    Private Sub TxtKey2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtKey2.TextChanged
        If Not Tag.Equals("Closing") Then FilterChanged()
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

End Class
