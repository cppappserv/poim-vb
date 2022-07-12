''' <summary>
''' Title                : Transaksi Document Import  ==> menjadi Pre Import Document
''' Form                 : FrmDoc_Import
''' Sub Form             : FrmBOLC, FrmSHIN, FrmRIL
''' Table Used           : tbl_docimpr, tbl_po, tbl_po_detail, tbm_company, tbm_plant, tbm_port, tbm_payment_term, tbm_insurance, tbm_supplier, tbm_currency, tbm_kurs             
''' Form Created By      : Hanny, 07.10.2008
''' Program Coding By    : Yanti, 14.10.2008
''' Modify By            : Yanti, 17.12.2008 Display angka di Grid di format 
''' Modify By            : Yanti, 08.01.2009
''' Modify Note          : - RIL bisa di create selama Quantity dari Materialnya masih tersisa
'''                          perhitunganya memasukkan Tolerable Delivery dari PO nya.
'''                          Contoh BM dari Argentina di order 1000 ton dengan Tolerable = 0,10 (10%) 
'''                          berarti RIL bisa maksimal hingga 1100 ton).
''' 
''' </summary>
''' <remarks></remarks>


Public Class FrmDOC_Import
    Dim ErrMsg, SQLstr As String
    Dim PilihanDlg As New DlgPilihan
    Dim MyReader, MyReader2 As MySqlDataReader
    Public Shared ClientDecimalSeparator, ClientGroupSeparator As String
    Public Shared RegionalSetting As System.Globalization.CultureInfo
    Public Shared ServerDecimal, ServerSeparator As String
    Dim PilihPO As New FrmListPO
    Private Sub FrmDOC_Import_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim loc = New System.Drawing.Point(0, 0)

        Me.Location = loc
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        RegionalSetting = Global.System.Globalization.CultureInfo.CurrentCulture
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
    End Sub
    Private Function DataExist(ByVal str As String) As Boolean
        MyReader = DBQueryMyReader(str, MyConn, "", UserData)

        If MyReader Is Nothing Then
            Return False
        Else
            While MyReader.Read
                CloseMyReader(MyReader, UserData)
                Return True
            End While
        End If
        CloseMyReader(MyReader, UserData)
    End Function
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        SQLstr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLstr) = True)
    End Function
    Sub New()
        InitializeComponent()

        Company_Name.Text = ""
        Plant_Name.Text = ""
        Port_Name.Text = ""
        Payment_Name.Text = ""
        Insurance_Name.Text = ""
        Supplier_Name.Text = ""
        Currency_Name.Text = ""
        BOLC.Enabled = False
        InLC.Enabled = False
        SHIN.Enabled = False
        RIL.Enabled = False
        ServerDecimal = "."
        ServerSeparator = ","
    End Sub
    Private Function DataOK(ByVal jns As String) As Boolean
        Dim mess1, mess2 As String
        Dim status As String = ""
        Dim sisaQty As Decimal

        If jns = "BOLC" Then
            ErrMsg = "Failed when read BOLC data"
            mess1 = "Budget Opening LC has been closed"
            mess2 = "Budget Opening LC has been created"

            SQLstr = "select status from tbl_budget " & _
                     "where po_no='" & txtPO_NO.Text & "'" & " and ord_no=(select max(ord_no) from tbl_budget where po_no='" & txtPO_NO.Text & "') " & _
                     "and type_code='" & jns & "'"
        End If

        If jns = "SHIN" Then
            ErrMsg = "Failed when read SI data"
            mess1 = "Shipment Instruction has been closed"
            mess2 = "Shipment Instruction has been created"

            SQLstr = "select status from tbl_si " & _
                     "where po_no='" & txtPO_NO.Text & "'" & " and ord_no=(select max(ord_no) from tbl_si_doc where po_no='" & txtPO_NO.Text & "') "
        End If

        If jns = "RIL" Then
            ErrMsg = "Failed when read RIL data"
            mess1 = "PO Request Import Licence has been closed"
            mess2 = "All material of PO " & txtPO_NO.Text & " already has RIL document," & Chr(13) & Chr(10) & "Can't create RIL anymore"

            SQLstr = "select status from tbl_ril " & _
                     "where po_no='" & txtPO_NO.Text & "'" & " and ril_no=(select max(ril_no) from tbl_ril_doc where po_no='" & txtPO_NO.Text & "') "
        End If

        If jns = "SUPP" Then
            ErrMsg = "Failed when read SUPPLIER DOCUMENT data"
            mess1 = "Supplier Document has been closed"
            mess2 = "Supplier Document has been created"

            SQLstr = "select status from tbl_shipping " & _
                     "where po_no='" & txtPO_NO.Text & "'" & " and shipment_no=(select max(shipment_no) from tbl_shipping where po_no='" & txtPO_NO.Text & "') "

        End If
        If jns = "BP" Then
            ErrMsg = "Failed when read BP data"
            mess1 = "Budget Remitance has been closed"
            mess2 = "Budget Remitance has been created"

            SQLstr = "select status from tbl_budget " & _
                     "where po_no='" & txtPO_NO.Text & "'" & " and ord_no=(select max(ord_no) from tbl_budget where po_no='" & txtPO_NO.Text & "' and type_code='" & jns & "') " & _
                     "and type_code='" & jns & "'"

        End If

        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    status = MyReader.GetString("status")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If

        'If (status = "Approved" Or status = "Closed") Then
        If (status <> "Rejected") Then
            If status <> "" Then
                If status = "Closed" Then
                    MsgBox(mess1)
                Else
                    If jns <> "RIL" And jns <> "BOLC" Then
                        MsgBox(mess2)
                        DataOK = False
                    Else
                        'YANTI - 08.01.2009
                        '=====sisa qty di cek di FrmRIL on load
                        'sisaQty = GetSisaQty()
                        'If sisaQty > 0 Then
                        DataOK = True
                        'Else
                        '    MsgBox(mess2)
                        '    DataOK = False
                        'End If
                    End If
                    End If
            Else
                    DataOK = True
                End If
        Else
            DataOK = True
        End If
    End Function
    Private Function GetQty(ByVal str As String) As Decimal
        MyReader = DBQueryMyReader(str, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    GetQty = MyReader.GetString("qty")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Function
    Private Function GetRILQty() As Decimal
        Dim strSQL As String

        strSQL = " select sum(quantity) as 'qty' from tbl_ril_Detail where po_no='" & txtPO_NO.Text & "' group by po_no"
        GetRILQty = GetQty(strSQL)
    End Function
    Private Function GetSisaQty() As Decimal
        Dim strSQL As String
        Dim RILQty, POTolerableQty As Decimal

        RILQty = GetRILQty()
        strSQL = "select sum(a.quantity*((100+b.tolerable_delivery)/100)) as 'qty' " & _
                 "from tbl_po_Detail as a " & _
                 "inner join tbl_po as b on a.po_no=b.po_no " & _
                 "where a.po_no='" & txtPO_NO.Text & "' " & _
                 "group by a.po_no"
        POTolerableQty = GetQty(strSQL)
        GetSisaQty = POTolerableQty - RILQty
    End Function
    Private Sub btnPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPO.Click
        'Call Pilih(txtPO_NO)        
        If PilihPO.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPO_NO.Text = PilihPO.v_pono.Text
            Call f_getdata(txtPO_NO.Text)
            Call f_getDOCImportList(txtPO_NO.Text)
        End If
    End Sub
    Private Sub ClearScreen()
        prodname.Text = ""
        txtCompany_Code.Text = ""
        Company_Name.Text = ""
        txtPlant_Code.Text = ""
        Plant_Name.Text = ""
        txtPort_Code.Text = ""
        Port_Name.Text = ""
        DTPeriodeFR.Text = ""
        DTPeriodeTO.Text = ""
        ShipTerm.Text = ""
        txtPayment_Code.Text = ""
        Payment_Name.Text = ""
        txtInsurance_Code.Text = ""
        Insurance_Name.Text = ""
        txtIPA_No.Text = ""
        txtPR_No.Text = ""
        txtSupplier_Code.Text = ""
        Supplier_Name.Text = ""
        txtContract_No.Text = ""
        Tol_Delivery.Text = ""
        txtCurrency_Code.Text = ""
        Currency_Name.Text = ""
        txtrate.Text = ""
        total.Text = ""
        CreatedDate.Text = ""
        CreatedBy.Text = ""
        Status.Text = ""
        PurchasedBy.Text = ""
        PurchasedDate.Text = ""
        ApprovedBy.Text = ""
        ApprovedDate.Text = ""
        FundAppBy.Text = ""
        FundAppDate.Text = ""
        CTApp.Text = ""
        CTpur.Text = ""
        CTCre.Text = ""
        CTFun.Text = ""
    End Sub
    Private Sub Pilih(ByVal PO As System.Windows.Forms.TextBox)
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select PO"
        PilihanDlg.LblKey1.Text = "PO"
        PilihanDlg.LblKey2.Text = "Company Code"
        PilihanDlg.SQLGrid = "SELECT * FROM tbl_po"
        PilihanDlg.SQLFilter = "SELECT * FROM tbl_po " & _
                               "WHERE company_code LIKE 'FilterData1%' AND " & _
                               "company_code LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_company"

        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PO.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            Call f_getdata(PO.Text)
            Call f_getDOCImportList(PO.Text)
        End If
    End Sub
    Private Sub f_getdata(ByVal PO As String)
        Dim cont As Boolean = False
        Dim in_field, temp As String
        Dim in_tbl As String = ""        
        Dim dts As DataTable

        ClearScreen()
        SQLstr = "select a.*,b.company_name,c.plant_name,d.port_name,e.payment_name,f.insurance_Description,g.Supplier_name,h.Currency_Name " & _
                 " from tbl_po as a " & _
                 " inner join tbm_company as b on a.company_code=b.company_code " & _
                 " inner join tbm_plant as c on a.plant_code=c.plant_code " & _
                 " inner join tbm_port as d on a.port_code=d.port_code " & _
                 " inner join tbm_payment_term as e on a.payment_code=e.payment_code" & _
                 " left join tbm_insurance as f on a.insurance_code=f.insurance_code" & _
                 " inner join tbm_supplier as g on a.supplier_code=g.supplier_code" & _
                 " inner join tbm_currency as h on a.currency_code=h.currency_code" & _
                 " where po_no = '" & PO & "'"

        ErrMsg = "Failed when read PO"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    txtCompany_Code.Text = MyReader.GetString("company_code")
                    Company_Name.Text = MyReader.GetString("company_name")
                    txtPlant_Code.Text = MyReader.GetString("plant_code")
                    Plant_Name.Text = MyReader.GetString("plant_name")
                    txtPort_Code.Text = MyReader.GetString("port_code")
                    Port_Name.Text = MyReader.GetString("port_name")
                    DTPeriodeFR.Text = MyReader.GetString("shipment_period_fr")
                    DTPeriodeTO.Text = MyReader.GetString("shipment_period_to")
                    ShipTerm.Text = IIf(MyReader.GetString("shipment_term_code") = "W", "Whole Shipment", "Partial Shipment")
                    txtPayment_Code.Text = MyReader.GetString("payment_code")
                    Payment_Name.Text = MyReader.GetString("payment_name")
                    txtInsurance_Code.Text = MyReader.GetString("insurance_code")
                    Insurance_Name.Text = MyReader.GetString("insurance_description")
                    txtIPA_No.Text = MyReader.GetString("ipa_no")
                    txtPR_No.Text = MyReader.GetString("pr_no")
                    txtSupplier_Code.Text = MyReader.GetString("supplier_code")
                    Supplier_Name.Text = MyReader.GetString("supplier_name")
                    txtContract_No.Text = MyReader.GetString("contract_no")
                    Tol_Delivery.Text = FormatNumber(MyReader.GetString("tolerable_delivery"), 2, , , TriState.True)
                    txtCurrency_Code.Text = MyReader.GetString("currency_code")
                    Currency_Name.Text = MyReader.GetString("currency_name")
                    Try
                        txtrate.Text = FormatNumber(MyReader.GetString("kurs"), 2, , , TriState.True)
                    Catch ex As Exception
                        txtrate.Text = 0
                    End Try
                    CreatedDate.Text = MyReader.GetString("createddt")
                    'CreatedBy.Text = MyReader.GetString("createdby")
                    CTCre.Text = MyReader.GetString("createdby")
                    Status.Text = MyReader.GetString("status")
                    temp = MyReader.GetString("produsen_code")
                    Try
                        CTpur.Text = MyReader.GetString("purchasedby")
                        If CTpur.Text <> "" Then
                            PurchasedDate.Text = MyReader.GetString("purchaseddt")
                        End If
                    Catch ex As Exception
                        CTpur.Text = ""
                        PurchasedDate.Text = ""
                    End Try
                    Try
                        CTApp.Text = MyReader.GetString("approvedby")
                        If CTApp.Text <> "" Then
                            ApprovedDate.Text = MyReader.GetString("approveddt")
                        End If
                    Catch ex As Exception
                        CTApp.Text = ""
                        ApprovedDate.Text = ""
                    End Try
                    Try
                        CTFun.Text = MyReader.GetString("fundappby")
                        If CTFun.Text <> "" Then
                            FundAppDate.Text = MyReader.GetString("fundappdt")
                        End If
                    Catch ex As Exception
                        CTFun.Text = ""
                        FundAppDate.Text = ""
                    End Try
                Catch ex As Exception
                End Try
            End While

            BOLC.Enabled = (MyReader.HasRows)
            InLC.Enabled = (MyReader.HasRows)
            SHIN.Enabled = (MyReader.HasRows)
            RIL.Enabled = (MyReader.HasRows)

            cont = (MyReader.HasRows)
            CloseMyReader(MyReader, UserData)
            total.Text = FormatNumber(GetAmount(txtPO_NO.Text), 2, , , TriState.True)
            produsen.Text = temp
            prodname.Text = AmbilData("produsen_name", "tbm_produsen", "produsen_code='" & produsen.Text & "'")
            CreatedBy.Text = AmbilData("name", "tbm_users", "user_ct='" & CTCre.Text & "'")
            PurchasedBy.Text = AmbilData("name", "tbm_users", "user_ct='" & CTpur.Text & "'")
            ApprovedBy.Text = AmbilData("name", "tbm_users", "user_ct='" & CTApp.Text & "'")
            FundAppBy.Text = AmbilData("name", "tbm_users", "user_ct='" & CTFun.Text & "'")

            'SQLstr = "SELECT PO_ITEM,MATERIAL_CODE,COUNTRY_CODE,HS_CODE,SPECIFICATION,QUANTITY,WEIGHT,UNIT_CODE,PACKAGE_CODE,PRICE,NOTE " & _
            '         " from tbl_po_Detail where po_no='" & PO & "'"

            'dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
            in_field = "dpo.po_item as Item,dpo.material_code as MaterialCode,tmat.MATERIAL_name as MaterialName, dpo.country_code as OriginCode,tcou.COUNTRY_NAME as OriginName, " & _
                       "dpo.hs_code as HSCode,dpo.SPECIFICATION as Specification,dpo.quantity as Quantity,dpo.unit_code as UnitCode,dpo.package_code as PackCode, " & _
                       "dpo.price as UnitPrice, (dpo.quantity * dpo.price) as Amount, dpo.note as Remark "
            in_tbl = "tbl_po as tpo inner join tbl_po_detail as dpo on " & _
                     "tpo.po_no = dpo.po_no inner join tbm_material as tmat on dpo.material_code = tmat.material_code " & _
                     "inner join tbm_country as tcou on dpo.country_code = tcou.country_code " & _
                     "left join tbm_packing as tpac on dpo.package_code = tpac.PACK_CODE"
            SQLstr = "SELECT " & in_field & " from " & in_tbl & " where tpo.po_no = '" & PO & "'"
            ErrMsg = "Datagrid view Failed"
            dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

            Grid.DataSource = dts
            Grid.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grid.Columns(7).DefaultCellStyle.Format = "N5" '7 ' 5
            Grid.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grid.Columns(8).DefaultCellStyle.Format = "N2" '8 ' 6
            Grid.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grid.Columns(10).DefaultCellStyle.Format = "N2"
            Grid.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grid.Columns(11).DefaultCellStyle.Format = "N2" ' 13 ' 9
            Grid.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grid.Columns(12).DefaultCellStyle.Format = "N2" ' 13 ' 9

            BOLC.Enabled = (BOLC.Enabled) And (PunyaAkses("BO-C"))
            InLC.Enabled = (InLC.Enabled) And (PunyaAkses("IC-C"))
            SHIN.Enabled = (SHIN.Enabled) And (PunyaAkses("SI-C"))
            RIL.Enabled = (RIL.Enabled) And (PunyaAkses("RL-C"))
        End If
    End Sub

    Private Sub f_getDOCImportList(ByVal PO As String)
        Dim teks, v_teks, v_teks1 As String
        Dim in_field As String
        Dim in_tbl As String = ""
        Dim dts1 As DataTable

        If Trim(PO) = "" Then Exit Sub
        ListBox1.Items.Clear()
        SQLstr = "select concat(t1.doc_type,'LC #',t1.doc_no) as data , t2.status as data2 from tbl_docimpr as t1 " & _
                 "inner join tbl_budget as t2 on t1.po_no = t2.po_no and t2.ord_no = t1.doc_no and t2.type_code = 'BOLC' " & _
                 "where t1.po_no = '" & PO & "' and t1.doc_type='BO' " & _
                 "union " & _
                 "select concat('SI #',ord_no) as data, status as data2 from tbl_si " & _
                 "where shipment_no is null and po_no = '" & PO & "' " & _
                 "union " & _
                 "select concat('RIL #',ril_no) as data, status as data2 from tbl_ril " & _
                 "where po_no = '" & PO & "' and shipment_no is null " & _
                 "union " & _
                 "select concat(t1.doc_type,'LC #',t1.doc_no) as data , t2.status as data2 from tbl_docimpr as t1 " & _
                 "inner join tbl_budget as t2 on t1.po_no = t2.po_no and t2.ord_no = t1.doc_no and t2.type_code = 'ICLC' " & _
                 "where t1.po_no = '" & PO & "' and t1.doc_type='IC' "

        ErrMsg = "Failed when read Doc Import"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    teks = ""
                    v_teks = MyReader.GetString("data")
                    v_teks1 = MyReader.GetString("data2")
                    teks = Mid(v_teks & Space(45), 1, 45) & " - " & Microsoft.VisualBasic.Left(v_teks1 & Space(10), 10)
                    ListBox1.Items.Add(teks)
                Catch ex As Exception
                End Try
            End While

            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Function GetAmount(ByVal PO As String) As String
        SQLstr = "select sum(quantity * price) as total " & _
                 " from tbl_po_detail " & _
                 " where po_no = '" & PO & "'"

        ErrMsg = "Failed when read calculate amount"
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    GetAmount = MyReader.GetString("total")
                    GetAmount = Mid(GetAmount, 1, Len(GetAmount) - 2)
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Function
    Private Function PeriksaAkses(ByVal kdAkses1 As String, ByVal tombol As String, ByVal num As String) As Boolean
        If Not PunyaAkses(kdAkses1) Then
            MsgBox("You are not authorized to view " & tombol)
            PeriksaAkses = False
            Exit Function
        End If
        PeriksaAkses = True
    End Function
    Private Sub ListBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        Dim chosen, num, str As String
        Dim RILQty As Decimal
        Dim pjg, start As Integer

        If ListBox1.Items.Count < 0 Or ListBox1.SelectedIndex < 0 Then Exit Sub
        chosen = ListBox1.Items(ListBox1.SelectedIndex).ToString
        chosen = Trim(Mid(chosen, 1, 45))
        pjg = Len(chosen)
        start = InStr(chosen, "#") + 1
        'num = CInt(Mid(chosen, start, pjg))
        num = Mid(chosen, start, pjg)
        start = InStr(chosen, " ")
        str = Trim(Microsoft.VisualBasic.Left(chosen, start - 1))

        Select Case str
            Case "BOLC"
                If PeriksaAkses("BO-L", "BOLC", num) Then
                    Dim f As New FrmBOLC(txtPO_NO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, chosen)
                    f.ShowDialog()
                End If
            Case "SI"
                If PeriksaAkses("SI-L", "SHIN", num) Then
                    Dim f As New FrmSHIN(txtPO_NO.Text, chosen)
                    f.ShowDialog()
                End If
            Case "RIL"
                RILQty = GetRILQty()
                If PeriksaAkses("RL-L", "RIL", Trim(num.ToString)) Then
                    Dim f As New FrmRIL(txtPO_NO.Text, chosen, RILQty, If(ListBox1.Items.Count > 0, True, False))
                    f.ShowDialog()
                End If
            Case "ICLC"
                If PeriksaAkses("IC-L", "ICLC", num) Then
                    Dim f As New FrmIncLC(txtPO_NO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, chosen)
                    f.ShowDialog()
                End If
        End Select
        Call f_getDOCImportList(txtPO_NO.Text)
    End Sub

    Private Sub txtPO_NO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPO_NO.TextChanged
        Call f_getdata(txtPO_NO.Text)
        Call f_getDOCImportList(txtPO_NO.Text)
    End Sub
    Private Sub BOLC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BOLC.Click
        Dim f As New FrmBOLC(txtPO_NO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, "")

        If DataOK("BOLC") = True Then
            f.ShowDialog()
            Call f_getDOCImportList(txtPO_NO.Text)
        End If
    End Sub
    Private Sub SHIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SHIN.Click
        Dim f As New FrmSHIN(txtPO_NO.Text, "")
        Dim dahAda As Boolean

        'Klo sudah ada BL SI tidak boleh buat PO SI dan sebaliknya
        dahAda = CekExistingBLSI()

        If dahAda = False Then
            If DataOK("SHIN") = True Then
                f.ShowDialog()
                Call f_getDOCImportList(txtPO_NO.Text)
            End If
        Else
            MsgBox("PO " & txtPO_NO.Text & " already has BL SI," & Chr(13) & Chr(10) & "Can't create PO SI")
        End If
    End Sub
    Private Function CekExistingBLRIL() As Boolean
        Dim temp As String

        temp = AmbilData("po_no", "tbl_ril", "po_no='" & txtPO_NO.Text & "' and status<>'Rejected' and shipment_no is not null")
        CekExistingBLRIL = (Trim(temp) <> "")
    End Function
    Private Function CekExistingBLSI() As Boolean
        Dim temp As String

        temp = AmbilData("po_no", "tbl_si", "po_no='" & txtPO_NO.Text & "' and status<>'Rejected' and shipment_no is not null")
        CekExistingBLSI = (Trim(temp) <> "")
    End Function
    Private Sub RIL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RIL.Click
        Dim RILQty As Decimal
        Dim dahAda As Boolean

        'Klo sudah ada BL RIL tidak boleh buat PO RIL dan sebaliknya
        RILQty = GetRILQty()
        dahAda = CekExistingBLRIL()

        If dahAda = False Then
            Dim f As New FrmRIL(txtPO_NO.Text, "", RILQty, If(ListBox1.Items.Count > 0, True, False))
            If DataOK("RIL") = True Then
                f.ShowDialog()
                Call f_getDOCImportList(txtPO_NO.Text)
            End If
        Else
            MsgBox("PO " & txtPO_NO.Text & " already has BL RIL," & Chr(13) & Chr(10) & "Can't create PO RIL")
        End If
    End Sub
    Private Sub BR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New FrmBR(txtPO_NO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, "")

        If DataOK("BP") = True Then
            f.ShowDialog()
            Call f_getDOCImportList(txtPO_NO.Text)
        End If
    End Sub

    Private Sub DI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New FrmDI(txtPO_NO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, "")

        If DataOK("DI") = True Then
            f.ShowDialog()
            Call f_getDOCImportList(txtPO_NO.Text)
        End If
    End Sub

    Private Sub InLC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InLC.Click

        'If DataOK("INLC") = True Then

        'End If

        Dim v_IC, v_BO As String
        v_IC = AmbilData("count(ord_no)", "tbl_budget", "po_no = '" & txtPO_NO.Text & "' and TYPE_CODE = 'ICLC'")
        v_BO = AmbilData("count(ord_no)", "tbl_budget", "po_no = '" & txtPO_NO.Text & "' and TYPE_CODE = 'BOLC'")
        If (v_BO - 1) <> v_IC Then
            MsgBox("BOLC no available")
        Else
            Dim f As New FrmIncLC(txtPO_NO.Text, txtCurrency_Code.Text, Currency_Name.Text, total.Text, txtrate.Text, "")
            f.ShowDialog()
            Call f_getDOCImportList(txtPO_NO.Text)
        End If

    End Sub

    Private Sub btnSchedulePO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSchedulePO.Click
        Dim PilihItem As New FrmPOSchedule(txtPO_NO.Text, CTCre.Text)
        PilihItem.ShowDialog()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub
End Class