'Title        : Master Data Material
'Form         : FM03_Material
'Created By   : YANTI
'Created Date : September 2008
'Table Used   : tbm_material, tbm_material_group

Imports vbs = Microsoft.VisualBasic.Strings
Imports xlns = Microsoft.Office.Interop.Excel
Imports System.Management
Imports System.Text.RegularExpressions
Imports poim.FM02_MaterialGroup
Public Class FM03_Material
    Dim baru As Boolean
    Dim edit As Boolean
    Dim ErrMsg, SQLstr As String
    Dim affrow As Integer
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan
    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnSave.Enabled = False
        btnSaveToExcell.Enabled = False
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

        DataGridView1.DataSource = Show_Grid(DataGridView1, "(select tm.MATERIAL_CODE as MaterialCode, tm.MATERIAL_NAME as MaterialName, tm.GROUP_CODE as GroupCode, tg.group_name as GroupName, tm.MATERIAL_SHORTNAME as Shortname, tm.HS_CODE as HSCode, tm.REGISTER_NO as RegisterNo, tm.ZAT_ACTIVE as ZatActive, tm.KELOMPOK_OBAT_HEWAN as KelompokObatHewan, tm.sap_code as SAPCodeVersion, tm.theos_code as TheosCodeVersion from tbm_material as tm inner join tbm_material_group as tg on tm.group_code = tg.group_code order by tm.material_code) as a")
        brs = DataGridView1.RowCount
        btnSave.Enabled = False
        btnDelete.Enabled = False
        If brs > 0 Then btnSaveToExcell.Enabled = True
        TextBox1.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox4.Clear()
        TextBox3.Clear()
        TextBox6.Clear()
        TextBox5.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox1.Focus()
        baru = True
        edit = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        'Close()
        'Dispose()
        closeForm(sender, e, Me)
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If (MsgBox("Are you sure", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        SQLstr = "DELETE from tbm_material " & _
                 "where material_code='" & TextBox1.Text & "'"

        ErrMsg = "Failed when deleting user data"
        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Delete failed...", MsgBoxStyle.Information, "Delete user data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful("Delete Data")
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_material")
        End If
    End Sub
    Private Function CekData() As Boolean
        CekData = True

        'Primary Key
        SQLstr = "Select * from tbm_material where material_code='" & TextBox1.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = False Then
            MsgBox("Material code already created! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox1.Focus()
            Exit Function
        End If

        'Foreign Key
        SQLstr = "Select * from tbm_material_group where group_code='" & TextBox2.Text & "'"
        If FM02_MaterialGroup.DataOK(SQLstr) = True Then
            MsgBox("Group code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox2.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks As String

        If baru Then
            If CekData() = False Then Exit Sub
            teks = "Save Data"
            ErrMsg = "Failed when saving user data"
            SQLstr = "INSERT INTO tbm_material (material_code,group_code,material_name,MATERIAL_SHORTNAME,hs_code,register_no,zat_active,kelompok_obat_hewan,sap_code,theos_code) " & _
                     "VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & _
                                   TextBox8.Text & "', '" & _
                                   TextBox4.Text & "', '" & TextBox5.Text & "', '" & TextBox6.Text & "', '" & _
                                   TextBox7.Text & "', '" & SAPCode.Text & "', '" & TheosCode.Text & "')"
        Else
            teks = "Update Data"
            ErrMsg = "Failed when updating material data"
            SQLstr = "UPDATE tbm_material " & _
                     "SET group_code = '" & TextBox2.Text & "'," & _
                     "material_name = '" & TextBox3.Text & "'," & _
                     "MATERIAL_SHORTNAME = '" & TextBox8.Text & "'," & _
                     "hs_code = '" & TextBox4.Text & "'," & _
                     "register_no = '" & TextBox5.Text & "'," & _
                     "zat_Active = '" & TextBox6.Text & "'," & _
                     "kelompok_obat_hewan = '" & TextBox7.Text & "', " & _
                     "sap_code = '" & SAPCode.Text & "', " & _
                     "theos_code = '" & TheosCode.Text & "' " & _
                     "where material_code='" & TextBox1.Text & "'"
        End If

        affrow = DBQueryUpdate(SQLstr, MyConn, False, ErrMsg, UserData)
        If affrow < 0 Then
            MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
            Exit Sub
        Else
            RefreshScreen()
            f_msgbox_successful(teks)
            'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_material")
        End If
    End Sub
    Private Sub RefreshTombolSave()
        btnSave.Enabled = (Len(Trim(TextBox1.Text)) > 0) And (Len(Trim(TextBox2.Text)) > 0) And _
                          (Len(Trim(TextBox3.Text)) > 0)

        'And (Len(Trim(TextBox4.Text)) > 0) And _
        '(Len(Trim(TextBox5.Text)) > 0) And (Len(Trim(TextBox6.Text)) > 0) And _
        '(Len(Trim(TextBox7.Text)) > 0) And (Len(Trim(TextBox8.Text)) > 0)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        baru = False
        edit = True
        brs = DataGridView1.CurrentCell.RowIndex
        TextBox1.Text = DataGridView1.Item(0, brs).Value.ToString
        TextBox2.Text = DataGridView1.Item(2, brs).Value.ToString
        TextBox3.Text = DataGridView1.Item(1, brs).Value.ToString
        TextBox4.Text = DataGridView1.Item(5, brs).Value.ToString
        TextBox5.Text = DataGridView1.Item(6, brs).Value.ToString
        TextBox6.Text = DataGridView1.Item(7, brs).Value.ToString
        TextBox7.Text = DataGridView1.Item(8, brs).Value.ToString
        TextBox8.Text = DataGridView1.Item(4, brs).Value.ToString
        SAPCode.Text = DataGridView1.Item(9, brs).Value.ToString
        TheosCode.Text = DataGridView1.Item(10, brs).Value.ToString
        TextBox1.Enabled = False
        btnDelete.Enabled = (Len(Trim(TextBox1.Text)) > 0)
        TextBox3.Focus()
    End Sub
    Private Sub f_getdata()
        Dim teks2 As String = ""
        Dim teks3 As String = ""
        Dim teks4 As String = ""
        Dim teks5 As String = ""
        Dim teks6 As String = ""
        Dim teks7 As String = ""
        Dim teks8 As String = ""
        Dim teks10 As String = ""
        Dim teks11 As String = ""

        SQLstr = "select * from tbm_material where material_code = '" & TextBox1.Text & "' "
        ErrMsg = "Failed when read Users Data"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    teks2 = MyReader.GetString("group_code")
                    teks3 = MyReader.GetString("material_name")
                    teks4 = MyReader.GetString("hs_code")
                    teks5 = MyReader.GetString("register_no")
                    teks6 = MyReader.GetString("zat_active")
                    teks7 = MyReader.GetString("kelompok_obat_hewan")
                    teks8 = MyReader.GetString("MATERIAL_SHORTNAME")
                    teks10 = MyReader.GetString("SAP_CODE")
                    teks11 = MyReader.GetString("THEOS_CODE")
                Catch ex As Exception
                End Try
            End While
            If MyReader.HasRows = False Then
                baru = True
                edit = False
                TextBox1.Enabled = True
                btnDelete.Enabled = False
            Else
                baru = False
                edit = True
                TextBox1.Enabled = False
                btnDelete.Enabled = True
                TextBox2.Text = teks2
                TextBox3.Text = teks3
                TextBox4.Text = teks4
                TextBox5.Text = teks5
                TextBox6.Text = teks6
                TextBox7.Text = teks7
                TextBox8.Text = teks8
                SAPCode.Text = teks10
                TheosCode.Text = teks11
            End If
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        RefreshTombolSave()
        'Call f_getdata()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        lblGroupName.Text = AmbilData("GROUP_NAME", "tbm_material_group", "GROUP_CODE='" & TextBox2.Text & "'")
        RefreshTombolSave()
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        RefreshTombolSave()
    End Sub
    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        RefreshTombolSave()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PilihanDlg.Text = "Select Group Code"
        PilihanDlg.LblKey1.Text = "Group Code"
        PilihanDlg.LblKey2.Text = "Group Name"
        PilihanDlg.SQLGrid = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group"
        PilihanDlg.SQLFilter = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' " & _
                               " and group_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblGroupName.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString()
        End If
    End Sub

    Private Sub btnSaveToExcell_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveToExcell.Click
        Dim app As New xlns.Application
        Dim wb As xlns.Workbook = app.Workbooks.Add(xlns.XlWBATemplate.xlWBATWorksheet)
        Dim xlsheet As New xlns.Worksheet
        Dim inApp As xlns.Application
        Dim xlwindow As xlns.Workbook
        xlsheet = CType(wb.ActiveSheet, xlns.Worksheet)

        Dim file_name As String
        Dim StrColumn, StrData As String
        Dim i, j, k As Integer

        Try
            app.Visible = False
            ErrMsg = "Gagal baca data detail."
            'Write judul dulu
            xlsheet.Cells(1, 1) = Me.Text

            DataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit)
            For j = 1 To DataGridView1.ColumnCount
                StrColumn = DataGridView1.Columns(j - 1).HeaderText
                xlsheet.Cells(2, j) = StrColumn
            Next

            For i = 0 To DataGridView1.RowCount - 1
                For j = 1 To DataGridView1.ColumnCount

                    StrColumn = DataGridView1.Columns(j - 1).HeaderText
                    StrData = DataGridView1.Rows(i).Cells(StrColumn).Value.ToString
                    If Mid(StrData, 1, 1) = "0" Then StrData = "'" & StrData

                    xlsheet.Cells(i + 3, j) = StrData
                Next
            Next
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, 1)).Cells.Font.Size = 12
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, 1)).Cells.Font.Bold = True

            xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, j)).Cells.Font.Size = 9
            xlsheet.Range(xlsheet.Cells(2, 1), xlsheet.Cells(2, j)).Cells.Font.Bold = True

            xlsheet.Range(xlsheet.Cells(3, 1), xlsheet.Cells(i + 3, j)).Cells.Font.Size = 9
            xlsheet.Range(xlsheet.Cells(3, 1), xlsheet.Cells(i + 3, j)).Cells.Font.Bold = False
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(i + 3, j)).EntireColumn.AutoFit()
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(i + 3, j)).EntireColumn.WrapText = False

            'Finally save the file
            ''file_name = "c:/" & UserData.UserId & "-" & vbs.Format(Now(), "ddMMyyyy-mmss") & ".xls"
            ''xlsheet.SaveAs(file_name)
            xlsheet = Nothing
            app.Visible = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub
End Class
