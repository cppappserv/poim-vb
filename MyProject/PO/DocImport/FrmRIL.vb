''' <summary>
''' Title                         : Transaksi Document Import - RIL
''' Form                          : FrmRIL
''' Table Used                    : tbm_document, tbm_attachment_doc, tbm_users, tbm_users_modul, 
'''                                 tbl_ril_doc, tbl_ril, tbl_ril_Detail
''' Stored Procedure Used (MySQL) : SaveRIL
''' Created By                    : Yanti, Nov 2008
''' 
''' Modify By                     : Yanti, 08 Jan 2009 
''' Modify Note                   : - RILNo di input
'''                                 - RIL bisa di create selama Quantity dari Materialnya masih tersisa
'''                                   perhitunganya memasukkan Tolerable Delivery dari PO nya.
'''                                   Contoh BM dari Argentina di order 1000 ton dengan Tolerable = 0,10 (10%) 
'''                                   berarti RIL bisa maksimal hingga 1100 ton).
'''                                 - Detail Request Import Lisence di tambahkan kolom Origin.
'''                                   1 PO untuk material yg sama bisa di import dari 1 atau lebih origin/negara produsen 
''' 
''' </summary>
''' <remarks></remarks>



Public Class FrmRIL
    Private ClientDecimalSeparator, ClientGroupSeparator As String
    Private ServerDecimal, ServerSeparator As String
    Dim TolDelivery, RILQty As Decimal
    Dim MyReader, MyReader2 As MySqlDataReader
    Public Shared GridRILQty As String
    Dim RILNum, PO, strSQL, errMSg As String
    Dim DataError As Boolean = False
    Dim SudahAdaPORIL As Boolean
    Private Sub RefreshDoc()
        Dim cnt, ulang As Integer

        cnt = doc_no.Lines.Length - 1
        ulang = 0

        While ulang <= cnt
            Call doc_no_TextChanged(doc_no.Lines(ulang).ToString)
            ulang = ulang + 1
        End While
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
    Sub New(ByVal pono As String, ByVal RIL As String, ByVal Qty As Decimal, ByVal Eksis As Boolean)
        InitializeComponent()
        PO = Trim(pono)
        RILQty = Qty
        RILNum = Trim(RIL)
        SudahAdaPORIL = Eksis
        tgl.Value = GetServerDate()
        appdate.Value = GetServerDate()
        dtIssued.Value = GetServerDate()
        dtSubmited.Value = GetServerDate()
        'Call GetButtonAccess()

        If Trim(RIL) <> "" Then
            'existing date
            Call DisplayData(RIL)
            If (btnPrint.Enabled) And ((crtby.Text = UserData.UserCT) Or (PunyaAkses("CF-P"))) Then
                btnSave.Enabled = True
            Else
                btnSave.Enabled = False
            End If

            btnReject.Enabled = (btnReject.Enabled) And (crtby.Text = UserData.UserCT)
            If (btnPrint.Enabled) And (PunyaAkses("RL-P")) Then
                btnPrint.Enabled = True
                btnAttachment.Enabled = True
            Else
                btnPrint.Enabled = False
                btnAttachment.Enabled = False
            End If


        Else
            'create new
            btnReject.Enabled = False
            btnPrint.Enabled = False
            btnAttachment.Enabled = False
            crtby.Text = UserData.UserCT
            crtdate.Text = GetServerDate.ToString
            Me.Text = "PO Request Import Licence - New"
        End If
    End Sub
    Private Sub DisplayData(ByVal RIL As String)
        Dim pjg As Integer
        Dim temp1, temp2 As String

        pjg = Len(RTrim(RIL)) - 5
        RILNum = Mid(RIL, 6, pjg)
        Grid.ReadOnly = True
        no.Enabled = False

        strSQL = "select a.*,b.*, DATEDIFF(b.issueddt, b.submiteddt)+1 leadtime from tbl_ril_doc as a " & _
                 "inner join tbl_ril as b on a.po_no=b.po_no and a.ril_no=b.ril_no " & _
                 "where a.po_no = '" & PO & "' and a.Ril_no='" & RILNum & "'"

        errMSg = "Failed when read RIL data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)

        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    no.Text = MyReader.GetString("RIL_No")
                    tgl.Text = MyReader.GetString("OpeningDt")
                    doc_no.Text = MyReader.GetString("Doc_No")
                    Status.Text = MyReader.GetString("Status")
                    temp1 = GetAppBy("ApprovedBy")
                    GetTgl("ApprovedDt", appdate)
                    GetTgl("IssuedDt", dtIssued)
                    GetTgl("SubmitedDt", dtSubmited)
                    Try
                        txtDeptanNo.Text = MyReader.GetString("DEPTAN_NO")
                        txtRemark.Text = MyReader.GetString("REMARK")
                    Catch ex As Exception
                        txtDeptanNo.Text = ""
                        txtRemark.Text = ""
                    End Try
                    doc_no.Text = MyReader.GetString("Doc_No")
                    temp2 = MyReader.GetString("CreatedBy")
                    crtdate.Text = MyReader.GetString("CreatedDt")

                    Try
                        cbDG.Text = MyReader.GetString("Group_Code")
                    Catch ex As Exception
                        cbDG.Text = ""
                    End Try
                    Try
                        txtLeadtime.Text = MyReader.GetString("leadtime")
                    Catch ex As Exception
                        txtLeadtime.Text = "0"
                    End Try
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
            RefreshDoc()
            btnSave.Text = "Update"
            btnSave.Enabled = f_getenable(Status.Text)
            btnReject.Enabled = btnSave.Enabled
            btnPrint.Enabled = btnSave.Enabled
            btnAttachment.Enabled = btnSave.Enabled
            appcode.Text = temp1
            crtby.Text = temp2
            Me.Text = "PO Request Import Licence # " & Trim(RILNum) & " - Update"
        End If
    End Sub
    Private Sub GetTgl(ByVal fld As String, ByVal obj As DateTimePicker)
        Dim temp As Object
        Try
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then obj.Value = temp
        Catch ex As Exception
        End Try
        obj.Checked = Not (temp Is Nothing)
    End Sub
    Private Function GetAppBy(ByVal fld As String) As String
        Dim temp As Object
        Try
            GetAppBy = ""
            temp = MyReader.GetString(fld)
            If Not temp Is Nothing Then GetAppBy = temp
        Catch ex As Exception
        End Try
    End Function
    Private Sub FrmRIL_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dts As DataTable
        Dim SQLStr, ErrMsg, SQLStr1, SQLStr2, SQLStr3, SQLStr4, mess As String
        'Dim ss = New System.Drawing.Point(649, 389)
        Dim loc = New System.Drawing.Point(253, 250)
        Dim DT As New System.Data.DataTable

        'Me.Size = ss
        Me.Location = loc
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        ClientDecimalSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
        ClientGroupSeparator = Global.System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
        ServerDecimal = "."
        ServerSeparator = ","
        fillcbDG()

        ErrMsg = "RIL data view failed"

        If RILNum = "" Then
            'input newdata
            SQLStr1 = "0.00000 as 'RIL Quantity',d.group_code "
            SQLStr2 = "left outer join tbl_ril_Detail as b on b.po_no=a.po_no and b.po_item = a.po_item " & _
                      "left outer join tbl_ril as c on c.po_no=b.po_no and c.ril_no=b.ril_no "
            SQLStr3 = " "
            SQLStr4 = If(SudahAdaPORIL, " where Sisa>0", " ")
        Else
            'display data
            SQLStr1 = "b.quantity as 'RIL Quantity',d.group_code "
            SQLStr2 = "inner join tbl_ril_Detail as b on b.po_no=a.po_no and b.po_item = a.po_item " & _
                      "inner join tbl_ril as c on c.po_no=b.po_no and c.ril_no=b.ril_no "
            SQLStr3 = "and c.Ril_no='" & RILNum & "' and c.shipment_no is null "
            SQLStr4 = " "
        End If

        ErrMsg = "RIL data view failed"
        SQLStr = "select * from (" & _
                                 "select a.PO_Item,a.material_code as 'Material Code',d.material_name as 'Material Name',f.country_name as 'Origin',a.quantity as 'PO Quantity'," & _
                                 "GetSisaQty2(a.po_no,a.po_item,a.material_code) as 'Sisa'," & SQLStr1 & _
                                 "from tbl_po_Detail as a " & SQLStr2 & _
                                 "inner join tbm_material as d on a.material_Code=d.material_code " & _
                                 "inner join tbm_country as f on a.country_Code=f.country_code " & _
                                 "where a.po_no='" & PO & "' " & SQLStr3 & _
                                 "group by a.po_item" & _
                                ") as x " & SQLStr4

        dts = DBQueryDataTable(SQLStr, MyConn, "", ErrMsg, UserData)
        Grid.DataSource = dts

        DT = Grid.DataSource
        If DT.Rows.Count = 0 And RILNum = "" Then
            mess = "All material of PO " & PO & " already has RIL document," & Chr(13) & Chr(10) & "Can't create RIL anymore"
            MsgBox(mess)
            Me.Close()
        End If

        Grid.Columns(0).Visible = False
        Grid.Columns(1).DefaultCellStyle.BackColor = Color.Gray
        Grid.Columns(2).DefaultCellStyle.BackColor = Color.Gray
        Grid.Columns(3).DefaultCellStyle.BackColor = Color.Gray
        Grid.Columns(4).DefaultCellStyle.BackColor = Color.Gray
        Grid.Columns(5).DefaultCellStyle.BackColor = Color.Gray
        Grid.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Grid.Columns(4).DefaultCellStyle.Format = "N5"
        Grid.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Grid.Columns(5).DefaultCellStyle.Format = "N5"
        Grid.Columns(5).HeaderText = "Available Quantity"
        Grid.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Grid.Columns(6).DefaultCellStyle.Format = "N5"

        If RILNum <> "" Then
            Grid.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grid.Columns(5).Visible = False
        End If

        Grid.Columns(1).ReadOnly = True
        Grid.Columns(2).ReadOnly = True
        Grid.Columns(3).ReadOnly = True
        Grid.Columns(4).ReadOnly = True
        Grid.Columns(5).ReadOnly = True
        Grid.Columns(7).Visible = False   'material group code to get DocAddress
        'Grid.Columns(8).Visible = False   'RIL status
        Grid.Columns(1).Width = 60
        Grid.Columns(2).Width = 110
        Grid.Columns(4).Width = 88
        Grid.Columns(5).Width = 88
        Grid.Columns(6).Width = 88
        Grid.ColumnHeadersHeight = 35
    End Sub
    Private Sub fillcbDG()
        Dim strSQL, errMSg, temp As String
        strSQL = "select group_code from tbm_cr_template where type_code='RL'"

        errMSg = "Failed when read template data"
        MyReader = DBQueryMyReader(strSQL, MyConn, errMSg, UserData)
        cbDG.Refresh()
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Try
                    temp = MyReader.GetString("group_code")
                    cbDG.Items.Add(temp)

                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader, UserData)
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
        Dispose()
    End Sub

    Private Function CekData() As Boolean
        Dim cnt, brs As Integer
        Dim totqty, qty1, qty2, TolDel As Decimal
        Dim ee As System.Windows.Forms.DataGridViewCellEventArgs

        CekData = True
        no.Focus()   'invoke grid event, jika data terakhir yg diinput adalah Grid 
        If DataError = True Then Exit Function

        'Document
        If doc_no.Lines.Length = 0 Then
            MsgBox("Documents Req. should be filled! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            Exit Function
        End If

        'RIL quantity
        If RILNum = "" Then
            brs = Grid.RowCount
            totqty = 0
            For cnt = 1 To brs
                totqty = totqty + CDec(Grid.Item(6, cnt - 1).Value)
                qty1 = Grid.Item(5, cnt - 1).Value
                qty2 = Grid.Item(6, cnt - 1).Value
                TolDel = Grid.Item(7, cnt - 1).Value
                CekData = QtyOK(cnt - 1, qty1, qty2, TolDel)
                If CekData = False Then Exit For
            Next
            If CekData = False Then Exit Function
            If totqty = 0 Then
                MsgBox("At least one of RIL Quantity should be filled! ", MsgBoxStyle.Critical, "Warning")
                CekData = False
                Grid.Focus()
                Grid.CurrentCell = Grid(6, 0)
                Exit Function
            End If
        End If

        If no.Text = "" Then
            MsgBox("No. should be filled! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            no.Focus()
            Exit Function
        End If
        If appcode.Text <> "" And appdate.Checked = False Then
            MsgBox("Approved Date should be filled! ", MsgBoxStyle.Critical, "Warning")
            CekData = False
            appdate.Focus()
            Exit Function
        End If
    End Function
    Private Function GetNum2(ByVal strnum As String) As String
        Dim temp As String

        temp = Replace(strnum, ClientGroupSeparator, "")
        GetNum2 = Replace(temp, ClientDecimalSeparator, ServerDecimal)
    End Function
    Private Function GetDocAddress() As String
        Dim brs, cnt As Integer
        Dim temp, str As String
        Dim num As Decimal

        brs = Grid.RowCount
        temp = ""
        For cnt = 1 To brs
            num = Grid.Item(6, cnt - 1).Value
            If num > 0 Then
                If temp <> "" Then temp = temp & ","
                str = Grid.Item(7, cnt - 1).Value
                temp = temp & "'" & str & "'"
            End If
        Next

        strSQL = "select line_text from tbm_gov_address where doc_type='RL' and group_code in ('" & cbDG.Text & "') group by line_no,line_text"
        errMsg = "Failed when read tbm_gov_address"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        GetDocAddress = ""
        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    If GetDocAddress <> "" Then GetDocAddress = GetDocAddress & Chr(13) & Chr(10)
                    GetDocAddress = GetDocAddress & MyReader2.GetString("line_text")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()
        Dim dr As MySqlClient.MySqlDataReader
        Dim OpeningDt, AppDt, dtIssued1, dtSubmited1, SQLStr, DetailText, DocAddress, MaterialCode, Qty, POItem, str, RetMess As String
        Dim hasil As Boolean = False
        Dim cnt As Integer
        Dim num As Decimal

        If Grid.IsCurrentCellDirty Then no.Focus()
        If DataError Then Exit Sub
        If CekData() = False Then Exit Sub

        DetailText = ""
        DocAddress = GetDocAddress()
        OpeningDt = Format(tgl.Value, "yyyy-MM-dd")
        AppDt = IIf(appdate.Checked, Format(appdate.Value, "yyyy-MM-dd"), "")
        dtIssued1 = IIf(dtIssued.Checked, Format(dtIssued.Value, "yyyy-MM-dd"), "")
        dtSubmited1 = IIf(dtSubmited.Checked, Format(dtSubmited.Value, "yyyy-MM-dd"), "")
        Try
            If RILNum = "" Then

                For cnt = 1 To Grid.RowCount
                    MaterialCode = Grid.Item(1, cnt - 1).Value
                    MaterialCode = Microsoft.VisualBasic.Mid(MaterialCode & "          ", 1, 10)
                    POItem = Grid.Item(0, cnt - 1).Value
                    POItem = Microsoft.VisualBasic.Mid(POItem & "     ", 1, 5)
                    num = Grid.Item(6, cnt - 1).Value
                    If num > 0 Then
                        str = GetNum2(FormatNumber(num, 2, , , TriState.True))
                        Qty = Microsoft.VisualBasic.Mid(str & "           ", 1, 11)
                        DetailText = DetailText & MaterialCode & Qty & POItem & ";"
                        detail.Text = detail.Text & POItem & Chr(13) & Chr(10) & MaterialCode & Chr(13) & Chr(10) & Qty & Chr(13) & Chr(10)
                    End If
                Next
            End If

            SQLStr = "Run Stored Procedure SavePORIL (" & If(btnSave.Text = "Save", "", RILNum) & "," & PO & "," & OpeningDt & "," _
                     & doc_no.Text & "," & doc_text.Text & "," & DocAddress & "," & If(appcode.Text = "", "", appcode.Text) & "," _
                     & If(appcode.Text = "", "", AppDt) & "," & If(appcode.Text = "", "Open", "Approved") & "," & UserData.UserCT _
                     & DetailText & "," & no.Text & "," & cbDG.Text & "," & txtDeptanNo.Text & "," & If(dtIssued.Checked = False, "", dtIssued1) & "," & If(dtSubmited.Checked = False, "", dtSubmited1) & "," & txtRemark.Text & ")"

            MyComm.CommandText = "SavePORIL"
            MyComm.CommandType = CommandType.StoredProcedure

            MyComm.Parameters.Clear()
            MyComm.Parameters.AddWithValue("UpdateRILNo", IIf(btnSave.Text = "Save", "", RILNum))
            MyComm.Parameters.AddWithValue("PO", PO)
            MyComm.Parameters.AddWithValue("OpeningDt", OpeningDt)
            MyComm.Parameters.AddWithValue("DocNo", doc_no.Text)
            MyComm.Parameters.AddWithValue("DocName", doc_text.Text)
            MyComm.Parameters.AddWithValue("DocAddr", DocAddress)
            MyComm.Parameters.AddWithValue("ApprovedBy", If(appcode.Text = "", "", appcode.Text))
            MyComm.Parameters.AddWithValue("ApprovedDt", If(appcode.Text = "", "", AppDt))
            MyComm.Parameters.AddWithValue("Stat", If(appcode.Text = "", "Open", "Approved"))
            MyComm.Parameters.AddWithValue("AuditStr", SQLStr)
            MyComm.Parameters.AddWithValue("UserCT", UserData.UserCT)
            MyComm.Parameters.AddWithValue("Detail", DetailText)
            MyComm.Parameters.AddWithValue("RILNo", no.Text)
            MyComm.Parameters.AddWithValue("GroupCd", cbDG.Text)
            MyComm.Parameters.AddWithValue("DeptanNo", txtDeptanNo.Text)
            MyComm.Parameters.AddWithValue("IssuedDt", If(dtIssued.Checked = False, "", dtIssued1))
            MyComm.Parameters.AddWithValue("SubmitedDt", If(dtSubmited.Checked = False, "", dtSubmited1))
            MyComm.Parameters.AddWithValue("Remark", txtRemark.Text)
            MyComm.Parameters.AddWithValue("Hasil", hasil)
            MyComm.Parameters.AddWithValue("Message", RetMess)

            MyComm.ExecuteNonQuery()
            hasil = MyComm.Parameters("hasil").Value
            RetMess = MyComm.Parameters("message").Value

            If hasil = True Then
                f_msgbox_successful(btnSave.Text & " RIL")
                btnClose_Click(sender, e)
            Else
                'MsgBox(btnSave.Text & " RIL failed'")
                MsgBox(RetMess)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject.Click
        Dim msg, teks, Errmsg, SQLstr As String
        Dim affrow As Integer

        msg = "Reject PO Request Import Licence #" & RILNum & " of " & PO & Chr(13) & Chr(10) & "Are you sure ?"
        If MsgBox(msg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            teks = "Update Data"
            Errmsg = "Failed when updating user data"
            SQLstr = "UPDATE tbl_ril " & _
                     "SET status='Rejected'" & _
                     " where PO_No='" & PO & "' " & _
                     " and ril_no='" & RILNum & "'"

            affrow = DBQueryUpdate(SQLstr, MyConn, False, Errmsg, UserData)
            If affrow < 0 Then
                MsgBox("Saving failed...", MsgBoxStyle.Information, "Update User data")
                Exit Sub
            Else
                msg = "PO Request Import Licence #" & RILNum & " of " & PO & " has been rejected"
                MsgBox(msg)
            End If

        End If
        btnClose_Click(sender, e)
    End Sub
    Private Sub doc_no_TextChanged(ByVal doccode As String)
        Dim strSQL, errMsg As String
        Dim nomor As Integer

        strSQL = "select doc_name from tbm_document where doc_code='" & doccode & "'"
        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    nomor = doc_text.Lines.Length + 1
                    If doc_text.Text <> "" Then doc_text.Text = doc_text.Text & Chr(13) & Chr(10)
                    doc_text.Text = doc_text.Text & nomor.ToString & ". " & MyReader2.GetString("doc_name")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strSQL As String
        Dim PilihanDlg As New DlgPilihan
        Dim temp As Integer
        Dim strsqlF As String

        strSQL = "select a.doc_code as DocumentCode, a.doc_name as DocumentName from tbm_document as a " & _
                 "inner join tbm_attachment_doc as b on a.doc_code=b.doc_code " & _
                 "where b.doc_code LIKE 'RL%'"

        strsqlF = strSQL & " and a.doc_code like 'FilterData1%' and a.doc_name like 'FilterData2%'  group by a.doc_code"
        strSQL = strSQL & " group by a.doc_code"

        PilihanDlg.Text = "Select Document Code"
        PilihanDlg.LblKey1.Text = "Document Code"
        PilihanDlg.LblKey2.Text = "Document Name"

        PilihanDlg.SQLGrid = strSQL
        PilihanDlg.SQLFilter = strsqlF
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If doc_no.Lines.Length >= 1 Then doc_no.Text = doc_no.Text & Chr(13) & Chr(10)
            doc_no.Text = doc_no.Text & PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            temp = doc_no.Lines.Length - 1
            doc_no_TextChanged(doc_no.Lines(temp).ToString)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim PilihanDlg As New DlgPilihan

        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User ID"
        PilihanDlg.LblKey2.Text = "User Name"
        PilihanDlg.SQLGrid = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code = 'RL-A'"
        PilihanDlg.SQLFilter = "Select tu.user_ct as UserCT,tu.user_id as UserID, tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code = 'RL-A'" & _
                               "and tu.user_id LIKE 'FilterData1%' and tu.name LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then appcode.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        appdate.Checked = True
    End Sub
    Private Function GetName2(ByVal code As String) As String
        Dim strSQL, errMsg As String

        strSQL = "Select * from tbm_users " & _
                 "where user_ct = '" & code & "'"

        errMsg = "Failed when read user data"
        MyReader2 = DBQueryMyReader(strSQL, MyConn, errMsg, UserData)

        If Not MyReader2 Is Nothing Then
            While MyReader2.Read
                Try
                    GetName2 = MyReader2.GetString("name")
                Catch ex As Exception
                End Try
            End While
            CloseMyReader(MyReader2, UserData)
        End If
    End Function
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        v_num = Mid(RILNum & Space(40), 1, 40)
        ViewerFrm.Tag = "RILL" & v_num & PO
        ViewerFrm.ShowDialog()
    End Sub


    Private Sub btnClearD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearD.Click
        doc_no.Text = ""
        doc_text.Text = ""
    End Sub
    Private Function QtyOK(ByVal baris As Integer, ByVal Qty1 As Decimal, ByVal Qty2 As Decimal, ByVal TolDel As Decimal) As Boolean
        Dim str As String
        Dim max As Decimal

        'qty1 => available qty
        'qty2 => RIL qty (input)
        'RIL Qty <= Available Quantity X ((100 + PO.TOLERABLE_DELIVERY) %)

        QtyOK = True
        If Qty2 < 0 Then
            Grid.Focus()
            Grid.CurrentCell = Grid(6, baris)
            MsgBox("RIL Quantity should be > 0")
            QtyOK = False
            Exit Function
        End If
        max = Qty1 * ((100 + TolDel) / 100)
        If (Qty2 > max) Then
            str = FormatNumber(max, 2, , , TriState.True)
            Grid.Focus()
            Grid.CurrentCell = Grid(6, baris)
            MsgBox("Maximum Tolerable RIL Quantity " & str)
            QtyOK = False
        End If
    End Function
    Private Sub Grid_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.CurrentCellDirtyStateChanged
        If DataError = True And Grid.IsCurrentCellDirty = False Then DataError = False
    End Sub
    Private Sub Grid_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles Grid.DataError
        MsgBox("Invalid RIL Quantity, input numeric value")
        DataError = True
    End Sub
    Function GetServerDate() As Date
        Dim temp As Date
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = "select curdate()"
        MyComm.CommandType = CommandType.Text
        GetServerDate = MyComm.ExecuteScalar()
        MyComm.Dispose()
    End Function

    Private Sub appcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles appcode.TextChanged
        appname.Text = AmbilData("name", "tbm_users", "user_ct=" & appcode.Text)
    End Sub

    Private Sub crtby_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles crtby.TextChanged
        crtname.Text = AmbilData("name", "tbm_users", "user_ct=" & crtby.Text)
    End Sub

    Private Sub btnAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAttachment.Click
        Dim ViewerFrm As New Frm_CRViewer
        Dim v_num As String

        v_num = Mid(RILNum & Space(40), 1, 40)
        ViewerFrm.Tag = "RILT" & v_num & PO
        ViewerFrm.ShowDialog()
    End Sub
    Private Function PunyaAkses(ByVal kd As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLStr) = True)
    End Function

    Private Sub dtSubmited_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtSubmited.ValueChanged
        Call LeadTime()
    End Sub
    Private Sub LeadTime()
        Dim selisih As Integer
        Dim tg1, tg2 As Date

        If dtSubmited.Text = "" Then Exit Sub
        If dtIssued.Text = "" Then Exit Sub

        tg1 = CDate(dtSubmited.Text)
        tg2 = CDate(dtIssued.Text)

        selisih = DateDiff(DateInterval.Day, tg1, tg2)
        txtLeadtime.Text = FormatNumber(selisih, 0, , , TriState.True)
        If txtLeadtime.Text > 0 Then
            txtLeadtime.Text = txtLeadtime.Text + 1
        Else
            txtLeadtime.Text = 0
        End If
    End Sub

    Private Sub dtIssued_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtIssued.ValueChanged
        Call LeadTime()
    End Sub

    Private Sub cbDG_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbDG.SelectedIndexChanged

    End Sub

    Private Sub doc_no_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles doc_no.TextChanged

    End Sub
End Class