'Title                         : Master Data Kurs
'Form                          : FM26_CostCat_SubGroup
'Table Used                    : Tbm_CostCategory_SubGroup, tbm_CostCategory_Group
'Stored Procedure Used (MySQL) : RunSQL
'Created By                    : tedi 13.02.2009

Imports vbs = Microsoft.VisualBasic.Strings
Imports xlns = Microsoft.Office.Interop.Excel
Imports System.Management
Imports System.Text.RegularExpressions
Public Class FM28_CostRates
    Dim baru, edit, proses, v_find As Boolean
    Dim ErrMsg, SQLstr, desimal As String
    Dim MyReader As MySqlDataReader
    Dim PilihanDlg As New DlgPilihan

    Dim mask As String
    Dim ret1, ret2, ret3, dec1, dec2, dec3 As Boolean
    Dim ClientDecimalSeparator, ClientGroupSeparator, ServerDecimal As String
    Dim RegionalSetting As System.Globalization.CultureInfo
    Dim pjg As Integer
    Dim strsql As String

    Sub New()
        InitializeComponent()
        btnDelete.Enabled = False
        btnView.Enabled = False
        Group_Name0.Text = ""
        Group_Name1.Text = ""
        Group_Name2.Text = ""
        Group_Name3.Text = ""
        Group_Name4.Text = ""
        Group_Name5.Text = ""
        Group_Name6.Text = ""
        Group_Name7.Text = ""

        Call RefreshVar()
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim value = New System.Drawing.Point(0, 0)

        Me.Location = value
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        v_find = False
        RefreshScreen()
    End Sub
    Private Sub RefreshScreen()
        Dim brs As Integer
        Dim CatCode As String

        CatCode = textbox0.Text
        If CatCode = "" Then CatCode = "0"

        SQLstr = "select " & _
                 "a.COSTCAT_CODE as CategoryCode,b.COSTCAT_NAME as CategoryName," & _
                 "EFFECTIVE_DT as EffectiveDate, " & _
                 "a.PLANT_CODE as PlantCode, if(a.Plant_Code='00000','All',c.Plant_Name) as PlantName," & _
                 "a.PORT_CODE as PortCode,if(a.Port_Code='00000','All',d.Port_Name) as PortName," & _
                 "a.SUPPLIER_CODE as SupplierCode, if(a.SUPPLIER_CODE='00000','All',e.Supplier_Name) as SupplierName," & _
                 "a.EXPEDITION_CODE as ExpeditionCode, if(a.EXPEDITION_CODE='00000','All',f.Company_Name) as ExpeditionName," & _
                 "a.MATERIAL_GROUP as MaterialGroup, if(a.MATERIAL_GROUP='00000','All',g.group_Name) as MaterialGroupName," & _
                 "UNIT_CODE as UnitCode, DAYS as DayMin, SIZE_MIN as SizeMin, DAYS2 as DayMax, SIZE_MAX as SizeMax, a.CURRENCY_CODE as CurrencyCode, RATE as Rate from tbm_costrates as a " & _
                 "left join tbm_costcategory as b on a.COSTCAT_CODE=b.COSTCAT_CODE " & _
                 "left join tbm_plant as c on a.plant_Code=c.Plant_Code " & _
                 "left join tbm_port as d on a.port_code=d.port_code " & _
                 "left join tbm_supplier as e on a.supplier_code=e.supplier_code " & _
                 "left join tbm_Expedition as f on a.expedition_code=f.company_code " & _
                 "left join tbm_material_group as g on a.material_group=g.group_code" & _
                 " "

        If Not v_find Then
            DataGridView1.DataSource = Show_Grid(DataGridView1, "(" & SQLstr & ") as x")
        Else
            DataGridView1.DataSource = Show_Grid(DataGridView1, "(" & SQLstr & "WHERE a.COSTCAT_CODE=" & CatCode & ") as x")
        End If

        DataGridView1.Columns(14).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(15).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(16).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(17).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns(19).DefaultCellStyle.Format = "N2"
        DataGridView1.Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DataGridView1.Columns(2).Width = 100
        DataGridView1.Columns(1).Frozen = True

        brs = DataGridView1.RowCount
        btnDelete.Enabled = False
        btnView.Enabled = False
        textbox0.Enabled = True
        txt_date.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        TextBox6.Enabled = True
        TextBox7.Enabled = True
        txt_day_min.Enabled = True
        txt_size_min.Enabled = True

        btnSearch1.Visible = (TextBox1.Enabled = True)
        btnSearch2.Visible = (TextBox2.Enabled = True)
        btnSearch3.Visible = (TextBox3.Enabled = True)
        btnSearch4.Visible = (TextBox4.Enabled = True)
        btnSearch5.Visible = (TextBox5.Enabled = True)
        btnSearch6.Visible = (TextBox6.Enabled = True)
        btnSearch7.Visible = (TextBox7.Enabled = True)

        textbox0.Clear()
        txt_date.Value = GetServerDate()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        txt_day_min.Text = "0"
        txt_day_max.Text = "0"
        txt_size_min.Text = "0"
        txt_size_max.Text = "0"
        txt_rate.Clear()

        textbox0.Focus()
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
        v_find = False
        RefreshScreen()
        baru = True
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim v_tgl As String

        If (MsgBox("Are you sure?", MsgBoxStyle.YesNo, "Confirmation")) = vbNo Then Exit Sub

        v_tgl = Format(txt_date.Value, "yyyy-MM-dd")  'format case-sensitive
        SQLstr = " DELETE from tbm_costrates " & _
                 " where costcat_code    = '" & textbox0.Text & "'" & _
                 "   and effective_dt    = '" & v_tgl & "'" & _
                 "   and plant_code      = '" & TextBox1.Text & "'" & _
                 "   and port_code       = '" & TextBox2.Text & "'" & _
                 "   and supplier_code   = '" & TextBox3.Text & "'" & _
                 "   and expedition_code = '" & TextBox4.Text & "'" & _
                 "   and material_group  = '" & TextBox5.Text & "'" & _
                 "   and unit_code       = '" & TextBox6.Text & "'" & _
                 "   and days            = '" & txt_day_min.Text & "'" & _
                 "   and size_min        = '" & txt_size_min.Text & "'" & _
                 " "
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
                'DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_costrates")
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
        If textbox0.Text = "" Then
            MsgBox("CostCat code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            textbox0.Focus()
            Exit Function
            '        ElseIf txt_date.Value = "" Then
            '            MsgBox("Effective Date does not exist! ", MsgBoxStyle.Critical, "Warning")
            '            CekData = False
            '            txt_date.Focus()
            '            Exit Function
        ElseIf TextBox1.Text = "" Then
            MsgBox("Plant code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox1.Focus()
            Exit Function
        ElseIf TextBox2.Text = "" Then
            MsgBox("Port code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox2.Focus()
            Exit Function
        ElseIf TextBox3.Text = "" Then
            MsgBox("Supplier code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox3.Focus()
            Exit Function
        ElseIf TextBox4.Text = "" Then
            MsgBox("Expedition code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox4.Focus()
            Exit Function
        ElseIf TextBox5.Text = "" Then
            MsgBox("Material code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox5.Focus()
            Exit Function
        ElseIf TextBox6.Text = "" Then
            MsgBox("Unit code does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            TextBox6.Focus()
            Exit Function
        ElseIf txt_day_min.Text = "" Then
            MsgBox("Day does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txt_day_min.Focus()
            Exit Function
        ElseIf txt_size_min.Text = "" Then
            MsgBox("Size does not exist! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            txt_size_min.Focus()
            Exit Function
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim teks, v_tgl As String
        Dim Errmsg As String
        Dim hasil As Boolean = False
        Dim dr As MySqlClient.MySqlDataReader
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim v_day_max, v_day_min, v_size_max, v_size_min, v_rate As String

        If CekData() = False Then Exit Sub

        If txt_day_max.Text <> "" Then
            v_day_max = Replace(CStr(CDec(txt_day_max.Text)), ",", ".")
        Else
            v_day_max = "0"
        End If

        If txt_size_max.Text <> "" Then
            v_size_max = Replace(CStr(CDec(txt_size_max.Text)), ",", ".")
        Else
            v_size_max = "0"
        End If

        If txt_rate.Text <> "" Then
            v_rate = Replace(CStr(CDec(txt_rate.Text)), ",", ".")
        Else
            v_rate = "0"
        End If

        If txt_day_min.Text <> "" Then
            v_day_min = Replace(CStr(CDec(txt_day_min.Text)), ",", ".")
        Else
            v_day_min = "0"
        End If

        If txt_size_min.Text <> "" Then
            v_size_min = Replace(CStr(CDec(txt_size_min.Text)), ",", ".")
        Else
            v_size_min = "0"
        End If

        If baru Then
            teks = "Save Data"
            Errmsg = "Failed when saving user data"

            v_tgl = Format(txt_date.Value, "yyyy-MM-dd")  'format case-sensitive

            SQLstr = " INSERT INTO tbm_costrates (COSTCAT_CODE, EFFECTIVE_DT, PLANT_CODE, PORT_CODE, SUPPLIER_CODE, " & _
                     " EXPEDITION_CODE, MATERIAL_GROUP, UNIT_CODE, DAYS, SIZE_MIN, " & _
                     " DAYS2, SIZE_MAX, CURRENCY_CODE, RATE) " & _
                     "VALUES ('" & textbox0.Text & "', " & _
                             "'" & v_tgl & "', " & _
                             "'" & TextBox1.Text & "'," & _
                             "'" & TextBox2.Text & "'," & _
                             "'" & TextBox3.Text & "'," & _
                             "'" & TextBox4.Text & "'," & _
                             "'" & TextBox5.Text & "'," & _
                             "'" & TextBox6.Text & "'," & _
                             "'" & v_day_min & "', " & _
                             "'" & v_size_min & "', " & _
                             "'" & v_day_max & "', " & _
                             "'" & v_size_max & "', " & _
                             "'" & TextBox7.Text & "'," & _
                             "'" & v_rate & "'" & _
                             ")"
        Else
            v_tgl = Format(txt_date.Value, "yyyy-MM-dd")  'format case-sensitive
            teks = "Update Data"
            Errmsg = "Failed when updating user data"

            SQLstr = "UPDATE tbm_costrates " & _
                     "SET days2      = '" & v_day_max & "' ," & _
                     " size_max      = '" & v_size_max & "' , " & _
                     " currency_code = '" & TextBox7.Text & "' ," & _
                     " rate          = '" & v_rate & "'" & _
                     " where costcat_code = '" & textbox0.Text & "'" & _
                     "   and effective_dt = '" & v_tgl & "'" & _
                     "   and plant_code   = '" & TextBox1.Text & "' " & _
                     "   and port_code       = '" & TextBox2.Text & "' " & _
                     "   and supplier_code   = '" & TextBox3.Text & "' " & _
                     "   and expedition_code   = '" & TextBox4.Text & "' " & _
                     "   and material_group    = '" & TextBox5.Text & "' " & _
                     "   and unit_code   = '" & TextBox6.Text & "' " & _
                     "   and days       = '" & v_day_min & "' " & _
                     "   and size_min    = '" & v_size_min & "' " & _
                     " "
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
        textbox0.Text = DataGridView1.Item(0, brs).Value.ToString
        txt_date.Text = DataGridView1.Item(2, brs).Value.ToString
        TextBox1.Text = DataGridView1.Item(3, brs).Value.ToString
        TextBox2.Text = DataGridView1.Item(5, brs).Value.ToString
        TextBox3.Text = DataGridView1.Item(7, brs).Value.ToString
        TextBox4.Text = DataGridView1.Item(9, brs).Value.ToString
        TextBox5.Text = DataGridView1.Item(11, brs).Value.ToString
        TextBox6.Text = DataGridView1.Item(13, brs).Value.ToString
        If DataGridView1.Item(14, brs).Value.ToString <> "" Then
            txt_day_min.Text = CStr(FormatNumber(DataGridView1.Item(14, brs).Value, 2))
        Else
            txt_day_min.Text = DataGridView1.Item(14, brs).Value.ToString
        End If

        If DataGridView1.Item(15, brs).Value.ToString <> "" Then
            txt_size_min.Text = CStr(FormatNumber(DataGridView1.Item(15, brs).Value, 2))
        Else
            txt_size_min.Text = DataGridView1.Item(15, brs).Value.ToString
        End If

        If DataGridView1.Item(16, brs).Value.ToString <> "" Then
            txt_day_max.Text = CStr(FormatNumber(DataGridView1.Item(16, brs).Value, 2))
        Else
            txt_day_max.Text = DataGridView1.Item(16, brs).Value.ToString
        End If

        If DataGridView1.Item(17, brs).Value.ToString <> "" Then
            txt_size_max.Text = CStr(FormatNumber(DataGridView1.Item(17, brs).Value, 2))
        Else
            txt_size_max.Text = DataGridView1.Item(17, brs).Value.ToString
        End If

        TextBox7.Text = DataGridView1.Item(18, brs).Value.ToString

        If DataGridView1.Item(19, brs).Value.ToString <> "" Then
            txt_rate.Text = CStr(FormatNumber(DataGridView1.Item(19, brs).Value, 2))
        Else
            txt_rate.Text = DataGridView1.Item(19, brs).Value.ToString
        End If

        textbox0.Enabled = False
        txt_date.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        TextBox6.Enabled = False
        txt_day_min.Enabled = False
        txt_size_min.Enabled = False

        btnSearch1.Visible = (TextBox1.Enabled = True)
        btnSearch2.Visible = (TextBox2.Enabled = True)
        btnSearch3.Visible = (TextBox3.Enabled = True)
        btnSearch4.Visible = (TextBox4.Enabled = True)
        btnSearch5.Visible = (TextBox5.Enabled = True)
        btnSearch6.Visible = (TextBox6.Enabled = True)

        btnDelete.Enabled = (Len(Trim(textbox0.Text)) > 0)
        btnView.Enabled = btnDelete.Enabled
        txt_date.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim v_temp As String
        v_temp = "(select PLANT_CODE , PLANT_NAME , COMPANY_CODE from tbm_plant union Select '00000' , 'All' , '-' From dual ) as a "
        Group_Name1.Text = AmbilData("PLANT_NAME", v_temp, "PLANT_CODE='" & TextBox1.Text & "'")
        '        Call f_getdata()
    End Sub

    Private Sub f_getdata()
        If (Len(Trim(textbox0.Text))) = 0 Then Exit Sub
        '        tptgl = Format(tgl.Value, "yyyy-MM-dd")  'format case-sensitive
        SQLstr = "select * from tbm_costcategory where costcat_code = '" & textbox0.Text & "' "

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
                textbox0.Enabled = True
                btnDelete.Enabled = False
                btnView.Enabled = False
            Else
                baru = False
                edit = True
                textbox0.Enabled = False
                btnDelete.Enabled = True
                btnView.Enabled = True
            End If
            btnSearch1.Visible = (TextBox1.Enabled = True)
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub btnSearch1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch1.Click
        PilihanDlg.Text = "Select plant_code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_plant "
        PilihanDlg.SQLGrid = "Select '00000' as PlantCode, 'All' as PlantName, '-' as CompanyCode From dual union select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, COMPANY_CODE as CompanyCode from tbm_plant "

        PilihanDlg.SQLFilter = " Select '00000' as PlantCode, 'All' as PlantName, '-' as CompanyCode From dual union select PLANT_CODE as PlantCode, PLANT_NAME as PlantName, COMPANY_CODE as CompanyCode from tbm_plant " & _
                               " WHERE plant_code LIKE 'FilterData1%' " & _
                               " and plant_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_plant"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name1.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
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
        '        txt_date.Text = ""
        TextBox1.Text = ""
    End Sub
    Private Sub ClearMask()
        '        txt_subgroup_name.Mask = ""
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        Dim v_temp As String
        v_temp = "(select SUPPLIER_CODE , SUPPLIER_NAME from tbm_supplier where active = '1' union Select '00000' , 'All' From dual ) as a "
        Group_Name3.Text = AmbilData("SUPPLIER_NAME", v_temp, "SUPPLIER_CODE='" & TextBox3.Text & "'")
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        Dim v_temp As String
        v_temp = "(select GROUP_CODE , GROUP_NAME from tbm_material_group union Select '00000' , 'All' From dual ) as a "
        Group_Name5.Text = AmbilData("GROUP_NAME", v_temp, "GROUP_CODE='" & TextBox5.Text & "'")
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        Dim v_temp As String
        v_temp = "(select PORT_CODE , PORT_NAME from tbm_port union Select '00000' , 'All' From dual ) as a "
        Group_Name2.Text = AmbilData("PORT_NAME", v_temp, "PORT_CODE='" & TextBox2.Text & "'")
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        Dim v_temp As String
        v_temp = "(select COMPANY_CODE , COMPANY_NAME from tbm_expedition union Select '00000' , 'All' From dual ) as a "
        Group_Name4.Text = AmbilData("COMPANY_NAME", v_temp, "COMPANY_CODE='" & TextBox4.Text & "'")
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        Group_Name6.Text = AmbilData("UNIT_NAME", "tbm_unit", "TYPE_CODE IN ('2','3') AND UNIT_CODE='" & TextBox6.Text & "'")
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        Group_Name7.Text = AmbilData("CURRENCY_NAME", "tbm_currency", "CURRENCY_CODE='" & TextBox7.Text & "'")
    End Sub

    Private Sub btnSearch3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch3.Click
        PilihanDlg.Text = "Select supplier_code"
        PilihanDlg.LblKey1.Text = "Supplier Code"
        PilihanDlg.LblKey2.Text = "Supplier Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_supplier "
        PilihanDlg.SQLGrid = "Select '00000' SupplierCode, 'All' SupplierName  From dual UNION select SUPPLIER_CODE as SupplierCode, SUPPLIER_NAME as SupplierName from tbm_supplier where active = '1' "

        PilihanDlg.SQLFilter = "Select '00000' SupplierCode, 'All' SupplierName  From dual UNION select SUPPLIER_CODE as SupplierCode, SUPPLIER_NAME as SupplierName from tbm_supplier  where active = '1' " & _
                               "WHERE supplier_code LIKE 'FilterData1%' " & _
                               " and supplier_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_supplier"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox3.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name3.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearch5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch5.Click
        PilihanDlg.Text = "Select group_code"
        PilihanDlg.LblKey1.Text = "Group Code"
        PilihanDlg.LblKey2.Text = "Group Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_material_group "
        PilihanDlg.SQLGrid = "Select '00000' GroupCode, 'All' GroupName From dual UNION select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group "

        PilihanDlg.SQLFilter = "Select '00000' GroupCode, 'All' GroupName From dual UNION select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' " & _
                               " and group_name  LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox5.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name5.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearch2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch2.Click
        PilihanDlg.Text = "Select port_code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_port "
        PilihanDlg.SQLGrid = " Select '00000' PortCode, 'All' PortName From dual UNION  select PORT_CODE as PortCode, PORT_NAME as PortName from tbm_port "

        PilihanDlg.SQLFilter = " Select '00000' PortCode, 'All' PortName From dual UNION  select PORT_CODE as PortCode, PORT_NAME as PortName from tbm_port " & _
                               " WHERE port_code LIKE 'FilterData1%' " & _
                               " and port_name like  'FilterData2%' "
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox2.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name2.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearch4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch4.Click
        PilihanDlg.Text = "Select company_code"
        PilihanDlg.LblKey1.Text = "Expedition Code"
        PilihanDlg.LblKey2.Text = "Expedition Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_expedition "
        PilihanDlg.SQLGrid = "Select '00000' ExpeditionCode, 'All' ExpeditionName  From dual union select COMPANY_CODE as ExpeditionCode, COMPANY_NAME as ExpeditionName from tbm_expedition "

        PilihanDlg.SQLFilter = "Select '00000' ExpeditionCode, 'All' ExpeditionName  From dual union select COMPANY_CODE as ExpeditionCode, COMPANY_NAME as ExpeditionName from tbm_expedition " & _
                               " WHERE company_code LIKE 'FilterData1%' " & _
                               " and company_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_expedition"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox4.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name4.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearch6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch6.Click
        PilihanDlg.Text = "Select unit_code"
        PilihanDlg.LblKey1.Text = "Unit Code"
        PilihanDlg.LblKey2.Text = "Unit Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_unit where type_code in ('2','3') "
        PilihanDlg.SQLGrid = "select UNIT_CODE as UnitCode, UNIT_NAME as UnitName from tbm_unit where type_code in ('1','2','3') "

        PilihanDlg.SQLFilter = "select UNIT_CODE as UnitCode, UNIT_NAME as UnitName from tbm_unit  " & _
                               "WHERE type_code in ('1','2','3') and unit_code LIKE 'FilterData1%' " & _
                               " and unit_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_unit"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox6.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name6.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearch7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch7.Click
        PilihanDlg.Text = "Select currency_code"
        PilihanDlg.LblKey1.Text = "Currency Code"
        PilihanDlg.LblKey2.Text = "Currency Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_currency "
        PilihanDlg.SQLGrid = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency "

        PilihanDlg.SQLFilter = "select CURRENCY_CODE as CurrencyCode, CURRENCY_NAME as CurrencyName from tbm_currency " & _
                               "WHERE currency_code LIKE 'FilterData1%' " & _
                               " and currency_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_currency"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            TextBox7.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name7.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub btnSearch0_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch0.Click
        PilihanDlg.Text = "Select costcat_code"
        PilihanDlg.LblKey1.Text = "CostCat Code"
        PilihanDlg.LblKey2.Text = "CostCat Name"
        'PilihanDlg.SQLGrid = "SELECT * FROM tbm_costcategory "
        PilihanDlg.SQLGrid = "select COSTCAT_CODE as CostCategoryCode, COSTCAT_NAME as CostCategoryName, SUBGROUP_CODE as SubGroupCode, ACTIVE as Active from tbm_costcategory "

        PilihanDlg.SQLFilter = " SELECT * FROM tbm_costcategory " & _
                               " WHERE costcat_code LIKE 'FilterData1%' " & _
                               " and costcat_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_costcategory"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            textbox0.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Group_Name0.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If

    End Sub

    Private Sub textbox0_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textbox0.TextChanged
        Group_Name0.Text = AmbilData("COSTCAT_NAME", "tbm_costcategory", "COSTCAT_CODE='" & textbox0.Text & "'")
        If Group_Name0.Text = "" Then
            btnView.Text = "View One Category"
        Else
            btnView.Text = "View " & Group_Name0.Text & " Only"
        End If
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        baru = False
        edit = False
        v_find = True
        RefreshScreen()
        txt_day_min.Text = ""
        txt_day_max.Text = ""
        txt_size_min.Text = ""
        txt_size_max.Text = ""
        baru = False
        edit = False
    End Sub

    Private Sub txt_rate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_rate.LostFocus
        Try
            If txt_rate.Text <> "" Then
                txt_rate.Text = FormatNumber(txt_rate.Text, 2)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            txt_rate.Text = ""
            txt_rate.Focus()
        End Try
    End Sub

    Private Sub txt_day_min_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_day_min.LostFocus
        Try
            If txt_day_min.Text <> "" Then
                txt_day_min.Text = FormatNumber(txt_day_min.Text, 2)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            txt_day_min.Text = ""
            txt_day_min.Focus()
        End Try

    End Sub

    Private Sub txt_size_min_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_size_min.LostFocus
        Try
            If txt_size_min.Text <> "" Then
                txt_size_min.Text = FormatNumber(txt_size_min.Text, 2)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            txt_size_min.Text = ""
            txt_size_min.Focus()
        End Try

    End Sub
    '
    Private Sub txt_day_max_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_day_max.LostFocus
        Try
            If txt_day_max.Text <> "" Then
                txt_day_max.Text = FormatNumber(txt_day_max.Text, 2)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            txt_day_max.Text = ""
            txt_day_max.Focus()
        End Try

    End Sub

    Private Sub txt_size_max_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_size_max.LostFocus
        Try
            If txt_size_max.Text <> "" Then
                txt_size_max.Text = FormatNumber(txt_size_max.Text, 2)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            txt_size_max.Text = ""
            txt_size_max.Focus()
        End Try

    End Sub

    Private Sub txt_rate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_rate.TextChanged

    End Sub

    Private Sub btnViewAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewAll.Click
        baru = False
        edit = False
        v_find = False
        RefreshScreen()
        txt_day_min.Text = ""
        txt_day_max.Text = ""
        txt_size_min.Text = ""
        txt_size_max.Text = ""
        baru = False
        edit = False
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
