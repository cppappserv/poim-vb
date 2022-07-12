'Title                         : Master Data Kurs
'Form                          : FM26_CostCat_SubGroup
'Table Used                    : Tbm_CostCategory_SubGroup, tbm_CostCategory_Group
'Stored Procedure Used (MySQL) : RunSQL
'Created By                    : tedi 13.02.2009

Public Class FM27_CostCat
    Dim baru, edit, proses As Boolean
    Dim ErrMsg, SQLstr, desimal As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan

    Dim mask As String
    Dim ret1, ret2, ret3, dec1, dec2, dec3 As Boolean
    Dim ClientDecimalSeparator, ClientGroupSeparator, ServerDecimal As String
    Dim RegionalSetting As System.Globalization.CultureInfo
    Dim pjg As Integer

    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        Group_Name.Text = ""

        '        mask = "##,###,###.##"
        '        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        '        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        '        RegionalSetting = Global.System.Globalization.CultureInfo.CurrentCulture
        '        ServerDecimal = "."
        Call RefreshVar()
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()
        Dim brs As Integer

        'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_costcategory")
        DataGridView1.DataSource = Show_Grid(DataGridView1, "( SELECT t1.COSTCAT_CODE AS CostCategoryCode, t1.COSTCAT_NAME AS CostCategoryName, t1.SUBGROUP_CODE AS SubGroupCode, t2.subgroup_name AS SubGroupName, t2.GROUP_CODE AS GroupCode, t3.group_name AS GroupName, IF (t1.active='1','Yes','No') AS Active, t1.ord AS SortOrder FROM tbm_costcategory t1, tbm_costcategory_subgroup t2, tbm_costcategory_group t3 WHERE t1.SUBGROUP_CODE=t2.SUBGROUP_CODE AND t2.GROUP_CODE=t3.GROUP_CODE) as a")

        '        DataGridView1.Columns(2).DefaultCellStyle.Format = "N2"
        '        DataGridView1.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        brs = DataGridView1.RowCount
        btnDelete.Enabled = False
        txt_costcat_code.Enabled = True
        txt_costcat_name.Enabled = True
        TextBox1.Enabled = True
        btnSearch.Visible = (TextBox1.Enabled = True)
        chk_active.Enabled = True
        txt_ord.Enabled = True

        txt_costcat_code.Clear()
        txt_costcat_name.Clear()
        TextBox1.Clear()
        chk_active.Checked = False
        txt_ord.Clear()

        txt_costcat_code.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub RefreshVar()
        dec1 = False
        dec2 = False
        dec3 = False
        ret1 = True
        ret2 = True
        ret3 = True
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        If (MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_costcategory " & _
                 "where costcat_code='" & txt_costcat_code.Text & "'"

        ErrMsg = "Failed when deleting User data"

        Try
            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", SQLstr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)
            RefreshScreen()

            If hasil = True Then
                f_msgbox_successful("Delete Data")
                'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_costcategory")
            Else
                MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete User data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CekData() As Boolean
        Dim ee As New System.EventArgs

        CekData = True
        If txt_costcat_code.Text = "" Then
            MsgBox("CostCat code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txt_costcat_code.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String
        Dim Errmsg As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim v_chk_active As String

        If CekData() = False Then Exit Sub

        If chk_active.Checked = True Then
            v_chk_active = "1"
        Else
            v_chk_active = "0"
        End If

        If baru Then
            teks = "Save Data"
            Errmsg = "Failed when saving user data"

            SQLstr = "INSERT INTO tbm_costcategory (costcat_code,costcat_name,subgroup_code, active, ord) " & _
                     "VALUES ('" & txt_costcat_code.Text & "','" & txt_costcat_name.Text & "','" & TextBox1.Text & "','" & v_chk_active & "','" & txt_ord.Text & "')"
        Else
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbm_costcategory " & _
                     "SET costcat_name = '" & txt_costcat_name.Text & "' ," & _
                     " subgroup_code = '" & TextBox1.Text & "' , " & _
                     " active = '" & v_chk_active & "' , " & _
                     " ord = '" & txt_ord.Text & "'" & _
                     " where costcat_code='" & txt_costcat_code.Text & "'"
        End If

        Try
            MyComm.CommandText = "RunSQL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("SQLStr", SQLstr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Hasil", hasil)

            dr = MyComm.ExecuteReader()
            hasil = dr.FieldCount
            CloseMyReader(dr, UserData)
            RefreshScreen()

            If hasil = True Then
                f_msgbox_successful(teks)
            Else
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function GetNum(ByVal strnum As String) As String
        Dim temp As String
        Dim msk As New Windows.Forms.MaskedTextBox

        msk.Text = strnum
        msk.Mask = GetMaskText2Koma(msk.Text)
        temp = msk.Text
        temp = Replace(temp, ClientGroupSeparator, "")
        temp = Replace(temp, ClientDecimalSeparator, ServerDecimal)
        GetNum = temp
    End Function
    'GAK DIPAKE LAGI, SETELAH DI FORM LAIN DIPERBAIKI
    'HAPUS AJA FUNCTION INI
    Public Function GetdbNum(ByVal strnum As String) As Decimal
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        temp = Replace(strnum, ClientGroupSeparator, "")
        GetdbNum = CDec(temp)
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientDecimalSeparator, "")
        temp = Replace(temp, ClientGroupSeparator, "")
        GetNum2 = Trim(temp)
    End Function
    Private Function GetNum3(ByVal strnum As String) As String
        GetNum3 = Replace(strnum, ClientDecimalSeparator, "")
    End Function
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        Call RefreshVar()
        Call ClearText()
        Call ClearMask()
        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        txt_costcat_code.Text = DataGridView1.Item(0, brs).Value.ToString
        txt_costcat_name.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox1.Text = DataGridView1.Item(2, brs).Value.ToString

        If DataGridView1.Item(6, brs).Value.ToString = "1" Or _
        DataGridView1.Item(6, brs).Value.ToString = "Yes" Then
            chk_active.Checked = True
        Else
            chk_active.Checked = False
        End If
        txt_ord.Text = DataGridView1.Item(7, brs).Value.ToString

        txt_costcat_code.Enabled = False
        btnSearch.Visible = (TextBox1.Enabled = True)
        btnDelete.Enabled = (Len(Trim(txt_costcat_code.Text)) > 0)
        txt_costcat_name.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Group_Name.Text = AmbilData("SUBGROUP_NAME", "tbm_costcategory_subgroup", "SUBGROUP_CODE='" & TextBox1.Text & "'")
        '        Call f_getdata()
    End Sub

    Private Sub f_getdata()
        If (Len(Trim(txt_costcat_code.Text))) = 0 Then Exit Sub
        '        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        'SQLstr = "select * from tbm_costcategory where costcat_code = '" & txt_costcat_code.Text & "' "
        SQLstr = "select COSTCAT_CODE as CostCategoryCode, COSTCAT_NAME as CostCategoryName, SUBGROUP_CODE as SubGroupCode, ACTIVE as Active from tbm_costcategory where costcat_code = '" & txt_costcat_code.Text & "' "

        ErrMsg = "Failed when read Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    'ClearMask()
                    'txt_subgroup_name.Text = MyReader.GetString("kurs")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                txt_costcat_code.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                txt_costcat_code.Enabled = False
                btnDelete.Enabled = True
            End If
            btnSearch.Visible = (TextBox1.Enabled = True)
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        PilihanDlg.Text = "Select SubGroup Code"
        PilihanDlg.LblKey1.Text = "SubGroup Code"
        PilihanDlg.LblKey2.Text = "SubGroup Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_costcategory_subgroup "
        PilihanDlg.SQLGrid = "select SUBGROUP_CODE as SubGroupCode, SUBGROUP_NAME as SubGroupName, a.GROUP_CODE as GroupCode, b.GROUP_NAME as GroupName from tbm_costcategory_subgroup as a " & _
                             "inner join tbm_costcategory_group as b on a.group_code=b.group_Code"

        PilihanDlg.SQLFilter = PilihanDlg.SQLGrid & _
                               " WHERE a.subgroup_code LIKE 'FilterData1%' " & _
                               " and a.subgroup_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_costcategory_subgroup as a inner join tbm_costcategory_group as b on a.group_code=b.group_Code"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub
    '----GAK DIPAKE LAGI COI
    'setelah di form lain di hapus
    'hapus aje
    Public Function GetMaskText(ByVal teks As String) As String
        Dim pjg As Integer
        Dim result As String

        'contoh teks 345,67 with koma
        'format indonesia
        teks = Replace(teks, ".", "")
        pjg = Len(Trim(teks))
        If pjg = 0 Then Exit Function

        result = Microsoft.VisualBasic.Right(teks, 3)
        pjg = pjg - 3

        While pjg > 3
            teks = Microsoft.VisualBasic.Left(teks, pjg)
            result = Microsoft.VisualBasic.Right(teks, 3) & result
            If pjg > 3 Then result = "." & result
            pjg = pjg - 3
        End While
        If pjg <= 0 Then pjg = Len(teks)
        result = Microsoft.VisualBasic.Left(teks, pjg) & result
        GetMaskText = result
    End Function

    Private Function GetMaskText2Koma(ByVal teks As String) As String
        'decimal (10,2)
        'mask tetap memakai format inggris 
        'apapun Regional Setting komputer client
        Dim pjg As Integer
        Dim result As String

        teks = Replace(teks, ClientDecimalSeparator, "")
        teks = Replace(teks, ClientGroupSeparator, "")
        pjg = Len(Trim(teks))
        If pjg = 0 Then Exit Function
        If pjg < 3 Then
            result = Microsoft.VisualBasic.Left("###", pjg) & "." & "##"
            GetMaskText2Koma = result
            Exit Function
        End If

        result = Microsoft.VisualBasic.Right(teks, 2)
        result = "." & Microsoft.VisualBasic.Left("##", Len(result))
        pjg = pjg - 2
        If pjg <= 3 Then
            GetMaskText2Koma = Microsoft.VisualBasic.Left("###", pjg) & result
            Exit Function
        End If
        While pjg > 3
            teks = Microsoft.VisualBasic.Left(teks, pjg)
            If pjg > 3 Then result = "," & "###" & result
            pjg = pjg - 3
        End While
        result = Microsoft.VisualBasic.Left("###", pjg) & result
        GetMaskText2Koma = result
    End Function
    Private Sub ClearText()
        txt_costcat_name.Text = ""
        txt_costcat_name.Text = ""
        TextBox1.Text = ""
        chk_active.Checked = False
    End Sub
    Private Sub ClearMask()
        '        txt_subgroup_name.Mask = ""
    End Sub

    Private Sub chk_active_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_active.CheckedChanged

    End Sub

    Private Sub txt_costcat_name_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_costcat_name.TextChanged

    End Sub
End Class
