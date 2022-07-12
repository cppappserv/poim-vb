'Title                         : Monitoring Status
'Form                          : FM00_MonitoringList
'Table Used                    : all
'Stored Procedure Used (MySQL) : RunSQL

Imports vbs = Microsoft.VisualBasic.Strings
Imports xlns = Microsoft.Office.Interop.Excel
Imports System.Management
Imports System.Text.RegularExpressions

Public Class FM00_Monitoring
    Dim ErrMsg, SQLstr, SQLstr1, SQLstr2, SQLstr3, SQLstr4, SQLstr5, SQLstr6, SQLstrA, SQLStrE As String
    Dim Whrstr1, Whrstr2 As String
    Dim Footstr As String
    Dim PilihanDlg As New DlgPilihan
    Dim v_type As String
    Dim MyReader As MySqlDataReader
    Dim GroupUser As Integer

    Dim PrdM As Integer
    Dim strF As String
    Dim mac1 As String
    Dim affrow As Integer
    Dim SQLstrCE, TypeCE, WhrCE As String


    Sub New(ByVal caller As String)
        Dim lv_Buss As String
        Dim NoComp, TotComp As Integer

        v_type = caller

        InitializeComponent()
        tgl1.Value = DateAdd(DateInterval.Month, -1, Now)
        tgl2.Value = DateAdd(DateInterval.Month, 1, Now)

        tgl1_2.Value = tgl1.Value
        tgl2_2.Value = tgl2.Value

        SQLstr = "SELECT group_id FROM tbm_users WHERE user_ct = " & UserData.UserCT & ""
        ErrMsg = "Gagal baca ListData..."
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                GroupUser = MyReader.GetString("group_id")
            End While
            CloseMyReader(MyReader, UserData)
        End If

        '--- di tutup saja
        'If GroupUser = 1 Then
        'Footstr = ""
        'Else
        'Footstr = "Hak Akses anda terhadap data yang ada terbatas untuk dokumen 6 bulan terakhir saja "
        'End If

        'lblDataLimited.Text = Footstr
        'If v_type = "TF" Then lblDataLimited.Text = "" 'untuk Tax Forecast tidak perlu di batasi perhitungan bisa salah karena summary menghilangkan 1 atau lebih data
        '--- end

        'minus 1 karena di master ada company '-' untuk menyatakan All
        SQLstr = "SELECT COUNT(company_code) noComp, (SELECT COUNT(company_code) - 1 FROM tbm_company) totComp FROM tbm_users_company WHERE user_ct = " & UserData.UserCT & ""
        ErrMsg = "Gagal baca ListData..."
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                NoComp = MyReader.GetString("noComp")
                TotComp = MyReader.GetString("totComp")
            End While
            CloseMyReader(MyReader, UserData)
        End If
        If NoComp <> TotComp Then Footstr = "Hak Akses anda terhadap data terbatas untuk " & NoComp & " company saja"
        lblDataLimited.Text = Footstr

        cbPrdM_fill()

        SQLstr = "SELECT DISTINCT m1.LINE_BUSSINES FROM tbm_users_company t1, tbm_company m1 WHERE t1.company_code=m1.company_code AND t1.user_ct = " & UserData.UserCT & " LIMIT 1"
        ErrMsg = "Gagal baca ListData..."
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                lv_Buss = MyReader.GetString("line_bussines")
            End While
            CloseMyReader(MyReader, UserData)
        End If

        txtBuss.Text = lv_Buss

        lblPlant_Name.Text = ""
        lblPort_Name.Text = ""
        lblGrpMat.Text = ""
        lblSupplier_Name.Text = ""
        LblExpedition_Name.Text = ""

        strF = "SELECT t2.plant_code FROM tbm_users_company t1, tbm_plant t2 WHERE t1.company_code=t2.company_code AND t1.user_ct = " & UserData.UserCT
    End Sub
    Private Sub MasterForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MyConn = GetMyConn(MyConn)
        If MyConn Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        MyConn1 = GetMyConn(MyConn1)
        If MyConn1 Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        btnOpen.Enabled = False
        btnSave.Enabled = False
        btnFile.Enabled = False
        BtnPrint.Visible = False
        If v_type = "CC" Then
            Me.Text = "Inklaring Status"
            lblDate.Text = "Inklaring Date"
            lblDate2.Text = "Est/Act. Arrival Date"
            lblStatus.Text = "Inklaring Status"
            lblStatus2.Text = "Checker Status"

            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False
            tgl1_2.Checked = True
            lblPeriodGrp.Visible = False
            cbPrdM.Visible = False
            cbPrdY.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()
            Call cbstatus2_OKNot()

        ElseIf v_type = "CS" Then
            Me.Text = "Cost Slip Status"
            lblDate.Text = "Cost Slip Date"
            lblDate2.Text = "Est/Act. Arrival Date"
            lblStatus.Text = "Cost Slip Status"
            lblStatus2.Text = "Checker Status"

            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False
            tgl1_2.Checked = True
            lblPeriodGrp.Visible = False
            cbPrdM.Visible = False
            cbPrdY.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()
            Call cbstatus2_OKNot()

        ElseIf v_type = "GD" Then
            Me.Text = "General Data"
            lblDate2.Text = "Est/Act. Arrival Date"
            lblStatus.Text = "*Status"
            lblStatus2.Text = "Display Template"

            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False
            tgl1_2.Checked = False
            lblDate.Visible = False
            tgl1.Visible = False
            tgl2.Visible = False
            lblsp1.Visible = False
            lblPONO.Text = "*Fill Criteria"
            txtPONO.Visible = False

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = True
            Call cbSource_isi()
            Call cbPrdY_fill()
            Call cbstatus_ShipmentStatus()
            Call cbstatus2_DocInfo()

        ElseIf v_type = "SD" Then
            Me.Text = "Shipment Status"
            lblDate.Text = "SI Date"
            lblDate2.Text = "Est/Act. Arrival Date"
            lblStatus.Text = "*Status"
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()

        ElseIf v_type = "LS" Then
            Me.Text = "Logistic Status"
            lblDate2.Text = "Est/Act. Arrival Date"
            lblStatus.Text = "*Schedule Status"

            lblDate.Visible = False
            tgl1.Visible = False
            tgl2.Visible = False
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            lblPeriodGrp.Text = "Doc.To Expedition"
            lblSupplier.Text = "EMKL"
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()
            Call cbstatus2_OKNot()

        ElseIf v_type = "DP" Then
            Me.Text = "BPUM and BPJUM Status"
            lblSupplier.Text = "Expedition"
            lblPeriodGrp.Visible = False
            cbPrdM.Visible = False
            cbPrdY.Visible = False
            lblDate2.Text = "BPUM Printed"
            lblDate.Text = "BPJUM Printed"
            lblStatus.Text = "*Status"
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            BtnPrint.Visible = True
            BtnPrint.Enabled = False
            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()

            mac1 = GetUserMACAddress()

        ElseIf v_type = "PS" Then
            Me.Text = "Payment Status"
            lblDate.Text = "T/T Date"
            lblDate2.Text = "Est/Act. Arrival Date"
            lblStatus.Text = "*Status"
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()

        ElseIf v_type = "CO" Then
            Me.Text = "Contract Status"

            lblPlant.Visible = False
            txtPlant_Code.Visible = False
            btnSearchPlant.Visible = False
            lblPlant_Name.Visible = False
            lblPort.Visible = False
            txtPort_Code.Visible = False
            btnSearchPort.Visible = False
            lblPort_Name.Visible = False
            lblMatGrp.Visible = False
            txtGrpMat.Visible = False
            btnGrpMat.Visible = False
            lblGrpMat.Visible = False
            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False

            lblDate2.Visible = False
            tgl1_2.Visible = False
            lblsp1_2.Visible = False
            tgl2_2.Visible = False
            lblDate.Visible = False
            tgl1.Visible = False
            lblsp1.Visible = False
            tgl2.Visible = False
            lblCreated.Visible = False
            txtCreatedby.Visible = False
            btnUserPur.Visible = False

            lblPeriodGrp.Text = "Contract Period"
            lblStatus.Text = "*Status"
            lblPONO.Text = "Contract No."
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()

        ElseIf v_type = "TF" Then
            Me.Text = "Tax Forecast Status"

            lblMatGrp.Visible = False
            txtGrpMat.Visible = False
            btnGrpMat.Visible = False
            lblGrpMat.Visible = False
            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False

            lblCreated.Visible = False
            txtCreatedby.Visible = False
            btnUserPur.Visible = False

            lblPONO.Visible = False
            txtPONO.Visible = False

            lblDate.Text = "T/T Date"
            lblStatus.Text = "*Status"
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*PO No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()

        ElseIf v_type = "CE" Then        ' Add by AK 3/11/2010
            Me.Text = "Cost EMKL Status"

            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False

            lblCreated.Visible = False
            txtCreatedby.Visible = False
            btnUserPur.Visible = False

            lblPeriodGrp.Visible = False
            cbPrdM.Visible = False
            cbPrdY.Visible = False
            lblDate2.Text = "Printed Date"

            tgl1.Value = DateAdd(DateInterval.Month, -3, Now)
            tgl2.Value = Now

            tgl1_2.Value = tgl1.Value
            tgl2_2.Value = tgl2.Value

            lblDate.Visible = False
            tgl1.Visible = False
            tgl2.Visible = False

            lblStatus.Text = "Document"
            lblStatus2.Text = "Cost Item"
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Visible = False
            txtPONO.Visible = False

            LblExpedition.Location = New System.Drawing.Point(12, 123)
            TxtExpedition.Location = New System.Drawing.Point(109, 119)
            BtnExpedition.Location = New System.Drawing.Point(174, 119)
            LblExpedition_Name.Location = New System.Drawing.Point(201, 123)

            cbSource.Visible = True
            Call cbSource_isi()
            Call cbStatus_CreatedNot()
            Call cbStatus2_Created()
        ElseIf v_type = "DQ" Then
            Me.Text = "Deptan Quota Status"

            lblPlant.Visible = False
            txtPlant_Code.Visible = False
            btnSearchPlant.Visible = False
            lblPlant_Name.Visible = False
            lblSupplier.Visible = False
            txtSupplier_Code.Visible = False
            btnSupplier.Visible = False
            lblSupplier_Name.Visible = False

            lblCreated.Visible = False
            txtCreatedby.Visible = False
            btnUserPur.Visible = False

            lblPeriodGrp.Visible = False
            cbPrdM.Visible = False
            cbPrdY.Visible = False
            lblDate2.Text = "Submited Date"
            lblDate.Text = "Issued Date"
            lblStatus.Text = "*Status"
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*RIL/Deptan No."

            LblExpedition.Visible = False
            TxtExpedition.Visible = False
            BtnExpedition.Visible = False
            LblExpedition_Name.Visible = False

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()

        ElseIf v_type = "DS" Then
            Me.Text = "Deptan Status"

            lblCreated.Visible = False
            txtCreatedby.Visible = False
            btnUserPur.Visible = False

            lblDate.Text = "Submited Date"
            lblStatus.Text = "*Status"
            lblStatus2.Visible = False
            cbStatus2.Visible = False
            cbFill.Visible = False
            txtFill.Visible = False
            lblPONO.Text = "*RIL/Deptan No."

            LblExpedition.Text = "Origin"

            cbSource.Visible = False

            Call cbPrdY_fill()
            Call cbStatus_CreatedNot()
        End If
    End Sub

    Private Sub cbSource_isi()
        Dim dt As String

        If v_type = "GD" Then
            SQLstr = "SELECT CREATEDDT FROM tmp_generaldata limit 1"
            ErrMsg = "Gagal baca TemporaryData..."
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    dt = MyReader.GetString("CREATEDDT")
                End While
                CloseMyReader(MyReader, UserData)
            End If
            If dt = "" Then
                dt = "No Data"
            Else
                dt = "Query for Data Updated " & dt
            End If

            cbSource.Items.Clear()
            cbSource.Items.Insert(0, dt)
            cbSource.Items.Insert(1, "Query for Realtime Data")
            cbSource.SelectedIndex = 0
        End If

        If v_type = "CE" Then
            SQLstr = "SELECT CREATEDDT FROM tmp_costemkl_bipp limit 1"
            ErrMsg = "Gagal baca TemporaryData..."
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    dt = MyReader.GetString("CREATEDDT")
                End While
                CloseMyReader(MyReader, UserData)
            End If
            If dt = "" Then
                dt = "No Data"
            Else
                dt = "Query for Data Updated " & dt
            End If

            cbSource.Items.Clear()
            cbSource.Items.Insert(0, dt)
            cbSource.SelectedIndex = 0
        End If
    End Sub

    Private Sub cbStatus2_Created()
        Dim iCnt, Cnt As Integer
        Dim vHasil, FieldNm As String

        If v_type = "CE" Then
            cbStatus2.Items.Clear()
            Select cbStatus.Text
                Case "BPJUM Data" : TypeCE = "t1.findoc_type = 'PP'" : SQLstrCE = "SELECT * FROM tmp_costemkl_pp"
                Case "Billing Data" : TypeCE = "t1.findoc_type = 'BI'" : SQLstrCE = "SELECT * FROM tmp_costemkl_bi"
                Case "BPJUM + Billing Data" : TypeCE = "(t1.findoc_type = 'PP' OR t1.findoc_type = 'BI')" : SQLstrCE = "SELECT * FROM tmp_costemkl_bipp"
            End Select

            MyReader = DBQueryMyReader(SQLstrCE & " limit 1", MyConn, ErrMsg, UserData)
            Cnt = MyReader.FieldCount
            For iCnt = 0 To Cnt - 1
                If iCnt > 1 Then
                    FieldNm = MyReader.GetName(iCnt)
                    If iCnt < Cnt - 1 Then 'field terakhir adalah date update temporary table yg tidak perlu di tampilkan
                        cbStatus2.Items.Insert(iCnt - 2, FieldNm)
                    End If
                End If
            Next
            'cbStatus2.Items.Insert(iCnt - 2, "ALL")
            'cbStatus2.SelectedIndex = iCnt - 2
            cbStatus2.Items.Insert(iCnt - 3, "ALL")
            cbStatus2.SelectedIndex = iCnt - 3
            CloseMyReader(MyReader, UserData)
        End If
    End Sub

    Private Sub cbStatus_CreatedNot()
        If v_type = "CS" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Not Created")
            cbStatus.Items.Insert(1, "Created (All Status)")
            cbStatus.Items.Insert(2, "To be Approved")
            cbStatus.Items.Insert(3, "Final Approved")
            cbStatus.Items.Insert(4, "All")
            cbStatus.Items.Insert(5, "Not Created (Inklaring Created)")
            cbStatus.Items.Insert(6, "Not Created (Org Doc. Received)")
            cbStatus.Items.Insert(7, "Not Created (Org Doc. Not Received)")

            cbStatus.SelectedIndex = 1
        ElseIf v_type = "CC" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Not Created")
            cbStatus.Items.Insert(1, "Created (All Status)")
            cbStatus.Items.Insert(2, "Approved")
            cbStatus.Items.Insert(3, "All")

            cbStatus.SelectedIndex = 1
        ElseIf v_type = "SD" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "No Schedule")
            cbStatus.Items.Insert(1, "Schedule")
            cbStatus.Items.Insert(2, "Shipment")
            cbStatus.Items.Insert(3, "All PO")
            cbStatus.Items.Insert(4, "Pending Shipping Instruction")
            cbStatus.Items.Insert(5, "Pending Clearance")
            cbStatus.Items.Insert(6, "Clearance")
            cbStatus.Items.Insert(7, "PO To be Closed")

            cbStatus.SelectedIndex = 3

        ElseIf v_type = "LS" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Deliver")
            cbStatus.Items.Insert(1, "On Deliver")
            cbStatus.Items.Insert(2, "Pending Schedule")
            cbStatus.Items.Insert(3, "Pending SPPB")
            cbStatus.Items.Insert(4, "Pending Clearance")
            cbStatus.Items.Insert(5, "Clearance")
            cbStatus.Items.Insert(6, "All")

            cbStatus.SelectedIndex = 1

        ElseIf v_type = "DP" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Outstanding BPUM")
            cbStatus.Items.Insert(1, "Closed BPUM")
            cbStatus.Items.Insert(2, "Outstanding BPJUM")
            cbStatus.Items.Insert(3, "Outstanding based on TransfDt/Arrival")
            cbStatus.Items.Insert(4, "Closed BPJUM")
            cbStatus.Items.Insert(5, "All")

            cbStatus.SelectedIndex = 1

        ElseIf v_type = "PS" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Payment Paid")
            cbStatus.Items.Insert(1, "Pending Payment")
            cbStatus.Items.Insert(2, "All")

            cbStatus.SelectedIndex = 1

        ElseIf v_type = "CO" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Closed Contract")
            cbStatus.Items.Insert(1, "Outstanding Contract")
            cbStatus.Items.Insert(2, "All")

            cbStatus.SelectedIndex = 1

        ElseIf v_type = "TF" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Shipment")
            cbStatus.Items.Insert(1, "Outstanding Shipment")
            cbStatus.Items.Insert(2, "All")
            cbStatus.Items.Insert(3, "Outstanding Shipment (Detil)")

            cbStatus.SelectedIndex = 1

        ElseIf v_type = "CE" Then         ' Add by AK 4/11/2010
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "BPJUM Data")
            cbStatus.Items.Insert(1, "Billing Data")
            cbStatus.Items.Insert(2, "BPJUM + Billing Data")

            cbStatus.SelectedIndex = 0

        ElseIf v_type = "DQ" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Closed (Zero Difference)")
            cbStatus.Items.Insert(1, "Open")
            cbStatus.Items.Insert(2, "All")

            cbStatus.SelectedIndex = 1

        ElseIf v_type = "DS" Then
            cbStatus.Items.Clear()
            cbStatus.Items.Insert(0, "Not Created")
            cbStatus.Items.Insert(1, "Pending Deptan")
            cbStatus.Items.Insert(2, "Issued Deptan")
            cbStatus.Items.Insert(3, "All")

            cbStatus.SelectedIndex = 1
        End If

    End Sub

    Private Sub cbstatus2_OKNot()
        cbStatus2.Items.Clear()
        cbStatus2.Items.Insert(0, "Not OK")
        cbStatus2.Items.Insert(1, "OK")
        cbStatus2.Items.Insert(2, "All")
        cbStatus2.SelectedIndex = 1
    End Sub

    Private Sub cbPrdM_fill()
        Dim FieldNm, FieldStr, FieldNmLim As String
        Dim FieldNo As Integer

        cbPrdM.Items.Clear()
        cbPrdM.Items.Insert(0, "")
        While FieldNo < 12
            FieldNo = FieldNo + 1
            If FieldNo < 10 Then
                FieldStr = "2010-0" & FieldNo & "-01"
            Else
                FieldStr = "2010-" & FieldNo & "-01"
            End If
            SQLstr = "SELECT MONTHNAME('" & FieldStr & "') as PrdM, MONTH(DATE_SUB(NOW(), INTERVAL 3 MONTH)) AS PrdMLim FROM dual"
            ErrMsg = "Gagal baca ListData..."
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    FieldNm = MyReader.GetString("PrdM")
                    FieldNmLim = MyReader.GetString("PrdMLim")

                    'If GroupUser <> 1 Then
                    'If FieldNo > FieldNmLim Then cbPrdM.Items.Insert(FieldNo - FieldNmLim, FieldNm)
                    'Else
                    cbPrdM.Items.Insert(FieldNo, FieldNm)
                    'End If
                End While
                CloseMyReader(MyReader, UserData)
            End If
        End While
    End Sub

    Private Sub cbPrdY_fill()
        Dim FieldNm As String
        Dim FieldNo As Integer

        cbPrdY.Items.Clear()
        cbPrdY.Items.Insert(0, "")

        SQLstr = "SELECT DISTINCT DATE_FORMAT(shipment_period_fr,'%Y') PrdY, DATE_FORMAT(NOW(),'%c') PrdM FROM tbl_po ORDER BY PrdY desc"
        ErrMsg = "Gagal baca ListData..."
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                PrdM = MyReader.GetString("PrdM")
                FieldNm = MyReader.GetString("PrdY")
                FieldNo = FieldNo + 1
                'If GroupUser <> 1 Then
                'If (PrdM = 1 And FieldNo = 1) Then
                'FieldNm = FieldNm - 1
                'cbPrdY.Items.Insert(FieldNo, FieldNm) 'jika awal tahun/januari data di berikan tahun lalu
                'Else
                'If (PrdM > 1 And FieldNo <= 2) Then cbPrdY.Items.Insert(FieldNo, FieldNm) 'jika lewat januari data di berikan 2 tahun saja
                'End If
                'FieldNm = FieldNm - 1
                'Else
                cbPrdY.Items.Insert(FieldNo, FieldNm)
                'End If
            End While
            CloseMyReader(MyReader, UserData)
        End If
        cbPrdY.SelectedIndex = 1
        cbPrdM.SelectedIndex = 1
    End Sub

    Private Sub cbstatus_ShipmentStatus()
        'jika ganti/tambah item, update juga script querynya !
        'jika ganti/tambah item, update juga Private Sub cbStatus_SelectedIndexChanged !
        cbStatus.Items.Clear()
        cbStatus.Items.Insert(0, "All PO")
        cbStatus.Items.Insert(1, "Shipment")
        cbStatus.Items.Insert(2, "Pending Shipment")
        cbStatus.Items.Insert(3, "Pending Copy Doc")
        cbStatus.Items.Insert(4, "Pending Origin Doc")
        cbStatus.Items.Insert(5, "Pending Actual Arrival")
        cbStatus.Items.Insert(6, "Pending Shipment by ETA")
        cbStatus.Items.Insert(7, "Pending Req.Import Lisence")
        cbStatus.Items.Insert(8, "Pending Deptan")
        cbStatus.Items.Insert(9, "Pending Insurance")
        cbStatus.Items.Insert(10, "Pending Payment (DueDate)")
        cbStatus.Items.Insert(11, "Pending Tax (B-PIB)")

        cbStatus.SelectedIndex = 1
    End Sub

    Private Sub cbstatus2_DocInfo()
        Dim FieldNm As String
        Dim FieldNo As Integer

        cbStatus2.Items.Clear()
        cbStatus2.Items.Insert(0, "All Item")

        SQLstr = "SELECT DISTINCT ListName FROM tbm_listdata"

        ErrMsg = "Gagal baca ListData..."
        MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                FieldNm = MyReader.GetString("ListName")
                FieldNo = FieldNo + 1
                cbStatus2.Items.Insert(FieldNo, FieldNm)
            End While
            CloseMyReader(MyReader, UserData)
        End If
        cbStatus2.SelectedIndex = 0
    End Sub

    Private Sub RefreshScreen(ByVal v_modul As String)
        If v_modul = "CC" Then
            Call Inklaring()
        ElseIf v_modul = "CS" Then
            Call CostSlip()
        ElseIf v_modul = "GD" Then
            Call POBL_List()
        ElseIf v_modul = "SD" Then
            Call Shipment_List()
        ElseIf v_modul = "LS" Then
            Call Logistic_List()
        ElseIf v_modul = "DP" Then
            Call DP_List()
        ElseIf v_modul = "PS" Then
            Call Payment_List()
        ElseIf v_modul = "CO" Then
            Call Contract_List()
        ElseIf v_modul = "TF" Then
            Call TaxForecast_List()
        ElseIf v_modul = "CE" Then         ' Add by AK 5/11/2010
            Call CostEMKL_List()
        ElseIf v_modul = "DQ" Then
            Call DeptanQ_List()
        ElseIf v_modul = "DS" Then
            Call Deptan_List()
        End If
    End Sub

    Private Sub Inklaring()
        Dim dts As DataTable

        '"AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t2.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr = "SELECT t1.shipment_no, t1.po_no, t2.ord_no, t1.BL bl_no, t1.PO, t1.BussinesLine, t1.GroupMaterial, t1.BL, t1.Plant, cast(if(t7.UCont='CURAH','',t7.TCont) as char) as TotalContainer, " & _
                 "cast(t7.UCont as char) as UnitContainer,t1.Quantity, t1.Unit, t2.InklaringAmount, t1.Port, t1.ShipOnBoard, t1.ETA, " & _
                 "t1.ActualArrival, t1.RcvOriginDoc, t1.AreaRcvDoc, t5.RcvDeptan, t1.AreaRcvDeptan, t2.RequestDate, t2.InklaringDate, t1.FreeTime, t1.FreeTimeExt, t1.SPPBDate, t1.EstClearence EstDeliveryDate, t1.Clearence DeliveryDate, t1.EstBAPB EstClearanceDate, t1.BAPB ClearanceDate, t1.OverBrengen, t1.TahanOB, t1.ClearanceNote, " & _
                 "IF(t3.CheckerStatus='0','Not OK','OK') DocCheckerStatus, IF(t3.Note=',','',t3.note) NoteChecker, t1.Created DocCreatedBy, t4.name ApprovedBy, if(t2.Status = 'Approved', t2.ApprovedDate, null) ApprovedDate, t2.Remark " & _
                 "FROM " & _
                 "(SELECT t1.shipment_no, t1.po_no, getpoorder(t1.shipment_no, trim(t1.po_no)) PO, t1.BussinesLine, t1.group_code, m5.group_name GroupMaterial, t1.Quantity, t1.Unit, " & _
                 "t2.bl_no BL, t2.total_container, m1.supplier_name Supplier, t2.plant_code, m2.plant_name Plant, t2.port_code, m3.port_name Port, t2.received_copydoc_dt CopyDoc, t2.received_doc_dt OriginDoc, t2.forward_doc_dt RcvOriginDoc, t2.area_rcv_doc_dt AreaRcvDoc, t2.area_rcv_ril_dt AreaRcvDeptan, t2.est_delivery_dt ShipOnBoard, t2.est_arrival_dt ETA, " & _
                 "t2.notice_arrival_dt ActualArrival, t2.free_time FreeTime, t2.fte FreeTimeExt, t2.est_clr_dt EstClearence, t2.clr_dt Clearence, t2.est_bapb_dt EstBAPB, t2.bapb_dt BAPB, " & _
                 "IF(t2.ob_dt='0000-00-00',NULL,t2.ob_dt) OverBrengen, IF(t2.tahan_ob='N','','Yes') TahanOB, t2.fte_note ClearanceNote, " & _
                 "t2.total_container Container, t2.pib_no PIBNo, t2.pib_dt PIBDate, t2.sppb_no SPPB, t2.sppb_dt SPPBDate, t2.createdby, m4.name Created, t2.createddt CreatedDt " & _
                 "FROM (SELECT t1.shipment_no, t1.po_no, m2.line_bussines BussinesLine, m1.group_code, SUM(t1.quantity) Quantity, MAX(unit_code) Unit FROM tbl_shipping_detail t1, tbl_po_detail t2, tbl_po t3, tbm_material m1, tbm_company m2 " & _
                 "      WHERE t1.po_no=t2.po_no AND t1.po_item=t2.po_item AND t2.po_no=t3.po_no AND t1.material_code=m1.material_code AND t3.company_code=m2.company_code GROUP BY t1.shipment_no, t1.po_no, m2.line_bussines, m1.group_code) t1, " & _
                 "tbl_shipping t2, tbm_supplier m1, tbm_plant m2, tbm_port m3, tbm_users m4, tbm_material_group m5 " & _
                 "WHERE t1.shipment_no = t2.shipment_no And t2.supplier_code = m1.supplier_code " & _
                 "AND t2.plant_code=m2.plant_code AND t2.port_code=m3.port_code AND t2.createdby=m4.user_ct AND t1.group_code=m5.group_code " & _
                 "AND t2.plant_code IN (" & strF & ") " & _
                 "AND (( " & _
                 "(" & tgl1_2.Checked & " AND ((t2.NOTICE_ARRIVAL_DT is null AND t2.EST_ARRIVAL_DT >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t2.EST_ARRIVAL_DT <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') " & _
                 "                         OR (t2.NOTICE_ARRIVAL_DT is not null AND t2.NOTICE_ARRIVAL_DT >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t2.NOTICE_ARRIVAL_DT <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) " & _
                 "  OR (not " & tgl1_2.Checked & ")) " & _
                 "        AND (t2.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                 "        AND (t2.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                 "        AND (t1.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                 "        AND (t1.BussinesLine = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                 "        AND (t2.createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') AND trim('" & txtPONO.Text & "') = '') " & _
                 "    OR (trim('" & txtPONO.Text & "') <> '' AND trim(t1.po_no) LIKE trim('" & txtPONO.Text & "'))) " & _
                 ") t1 " & _
                 "LEFT JOIN " & _
                 "(SELECT d1.shipment_no, d1.ord_no, d1.findoc_no, d1.findoc_reff, d1.FINDOC_FINAPPDT RequestDate, d1.findoc_printeddt InklaringDate, d1.findoc_valamt InklaringAmount, d1.findoc_valsize NumberofContainer, d1.findoc_valunit SizeOfContainer, d1.findoc_appby, d1.findoc_appdt ApprovedDate, d1.findoc_status Status, d1.findoc_note Remark " & _
                 "FROM tbl_shipping_doc d1 WHERE d1.findoc_type='CC' AND d1.findoc_status<>'Rejected') t2 " & _
                 "ON t1.shipment_no=t2.shipment_no AND t1.plant_code=t2.findoc_no AND t1.group_code=t2.findoc_reff " & _
                 "LEFT JOIN (SELECT s1.shipment_no, '0' CheckerStatus, REPLACE(GROUP_CONCAT(s1.doc_remark SEPARATOR ','),',,','') Note FROM tbl_doc_supplier s1 WHERE s1.doc_copy=0 GROUP BY s1.shipment_no) t3 " & _
                 "ON t1.shipment_no=t3.shipment_no " & _
                 "LEFT JOIN tbm_users t4 ON t2.findoc_appby=t4.user_ct " & _
                 "LEFT JOIN " & _
                 "(SELECT t9.shipment_no, REPLACE(GROUP_CONCAT(TRIM(t9.cont) SEPARATOR ','),',,','') TCont," & _
                 "REPLACE(GROUP_CONCAT(TRIM(t9.zcode) SEPARATOR ','),',,','') UCont " & _
                 "FROM " & _
                 "(SELECT t8.shipment_no,COUNT(t8.container_no) cont ,t8.Unit_code zcode " & _
                 "FROM tbl_shipping_cont t8 " & _
                 "INNER JOIN " & _
                 "(SELECT shipment_no FROM tbl_shipping GROUP BY shipment_no) t10 " & _
                 "ON t8.shipment_no=t10.shipment_no " & _
                 "GROUP BY t8.shipment_no,t8.unit_code ) t9 " & _
                 "GROUP BY t9.shipment_no) t7 " & _
                 "ON t7.shipment_no=t1.shipment_no " & _
                 "LEFT JOIN (SELECT shipment_no, po_no, MAX(issueddt) RcvDeptan FROM tbl_ril GROUP BY shipment_no, po_no) t5 On ((t1.po_no=t5.po_no and t1.shipment_no=t5.shipment_no) OR (t1.po_no=t5.po_no and t5.shipment_no IS NULL))" & _
                 "WHERE ((" & tgl1.Checked & " AND t2.InklaringDate >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND t2.InklaringDate <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (t2.InklaringDate is null and '" & cbStatus.SelectedIndex & "' = '0') OR (NOT " & tgl1.Checked & ")) " & _
                 "AND   ((t2.InklaringAmount <> '' and '" & cbStatus.SelectedIndex & "' = '1') OR (t2.InklaringAmount is null and '" & cbStatus.SelectedIndex & "' = '0') OR ('" & cbStatus.SelectedIndex & "' = '3') OR (t2.Status = 'Approved' and '" & cbStatus.SelectedIndex & "' = '2')) " & _
                 "AND   ((t3.CheckerStatus is null and '" & cbStatus2.SelectedIndex & "' = '1') OR (t3.CheckerStatus = '0' and '" & cbStatus2.SelectedIndex & "' = '0') OR ('" & cbStatus2.SelectedIndex & "' = '2')) " & _
                 "ORDER BY PO Desc "

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Width = 150
            'DataGridView1.Columns(10).DefaultCellStyle.Format = "N5"
            'DataGridView1.Columns(12).DefaultCellStyle.Format = "N2"
            'DataGridView1.Columns(12).DefaultCellStyle.BackColor = Color.Thistle
            'DataGridView1.Columns(22).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(11).DefaultCellStyle.Format = "N5"
            DataGridView1.Columns(13).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(13).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(23).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(4).Frozen = True
        End If
    End Sub

    Private Sub CostSlip()
        Dim dts As DataTable

        '"  AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t2.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr = "SELECT t1.shipment_no, t1.po_no, t2.ord_no, t1.BL bl_no, t1.PO,  t1.BussinesLine, t1.material_name DescriptionOfGoods, t1.country_name Origin, t1.BL, t1.Plant, CAST(if(t7.UCont='CURAH','',t7.TCont) AS CHAR) as TotalContainer, CAST(t7.UCont AS CHAR) as UnitContainer,t1.Quantity, t1.Unit, " & _
                 "t2.CostKg, t2.ShortPercentage, t1.Port, t1.ShipOnBoard, t1.ETA, " & _
                 "t1.ActualArrival, t1.RcvOriginDoc, t2.RequestDate, t5.InklaringDate, t2.CostSlipDate, format(t6.kurs_pajak,2) CostSlipRate, t1.FreeTime, t1.FreeTimeExt, t1.SPPBDate, t1.EstClearence EstDeliveryDate, t1.Clearence DeliveryDate, t1.EstBAPB EstClearanceDate, t1.BAPB ClearanceDate, " & _
                 "IF(t3.CheckerStatus='0','Not OK','OK') DocCheckerStatus, IF(t3.Note=',','',t3.note) NoteChecker, t1.Created DocCreatedBy, t4.name ApprovedBy, if(t2.Status = 'Final Approved', t2.ApprovedDate,null) ApprovedDate " & _
                 "From " & _
                 "  (SELECT t1.shipment_no, t1.po_no, getpoorder(t1.shipment_no, TRIM(t1.po_no)) PO, t1.po_item, t1.group_code, t1.material_code, t1.material_name, t1.country_name, t1.currency_code, t1.Quantity, t1.Unit, t1.BussinesLine, " & _
                 "  t2.bl_no BL, t2.total_container, m1.supplier_name Supplier, t2.plant_code, m2.plant_name Plant, t2.port_code, m3.port_name PORT, t2.received_copydoc_dt CopyDoc, t2.received_doc_dt OriginDoc, t2.forward_doc_dt RcvOriginDoc, t2.est_delivery_dt ShipOnBoard, t2.est_arrival_dt ETA, " & _
                 "  t2.notice_arrival_dt ActualArrival, t2.free_time FreeTime, t2.fte FreeTimeExt, t2.est_clr_dt EstClearence, t2.clr_dt Clearence, t2.est_bapb_dt EstBAPB, t2.bapb_dt BAPB, " & _
                 "  t2.total_container Container, t2.pib_no PIBNo, t2.pib_dt PIBDate, t2.sppb_no SPPB, t2.sppb_dt SPPBDate, t2.createdby, m4.name Created, t2.createddt CreatedDt " & _
                 "  FROM (SELECT t1.shipment_no, t1.po_no, t1.po_item, t1.material_code, m1.material_name, m1.group_code, m3.country_name, t1.quantity Quantity, t2.unit_code Unit, t3.currency_code, m2.line_bussines BussinesLine " & _
                 "      FROM tbl_shipping_detail t1, tbl_po_detail t2, tbl_po t3, tbm_material m1, tbm_company m2, tbm_country m3 " & _
                 "      WHERE t1.po_no=t2.po_no AND t1.po_item=t2.po_item AND t2.po_no=t3.po_no AND t1.material_code=m1.material_code and t3.company_code=m2.company_code and t2.country_code=m3.country_code) t1, " & _
                 "  tbl_shipping t2, tbm_supplier m1, tbm_plant m2, tbm_port m3, tbm_users m4 " & _
                 "  WHERE (t1.shipment_no = t2.shipment_no And t2.supplier_code = m1.supplier_code) " & _
                 "  AND t2.plant_code=m2.plant_code AND t2.port_code=m3.port_code AND t2.createdby=m4.user_ct " & _
                 "  AND t2.plant_code IN (" & strF & ") " & _
                 "  AND (( " & _
                 "  (" & tgl1_2.Checked & " AND ((t2.NOTICE_ARRIVAL_DT is null AND t2.EST_ARRIVAL_DT >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t2.EST_ARRIVAL_DT <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') " & _
                 "                           OR (t2.NOTICE_ARRIVAL_DT is not null AND t2.NOTICE_ARRIVAL_DT >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t2.NOTICE_ARRIVAL_DT <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) " & _
                 "    OR (not " & tgl1_2.Checked & ")) " & _
                 "         AND (t2.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                 "         AND (t2.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                 "         AND (t1.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                 "         AND (t1.BussinesLine = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                 "         AND (t2.createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') AND trim('" & txtPONO.Text & "') = '') " & _
                 "      OR (trim('" & txtPONO.Text & "') <> '' AND trim(t1.po_no) LIKE trim('" & txtPONO.Text & "'))) " & _
                 "  ) t1 " & _
                 "LEFT JOIN " & _
                 "  (SELECT d1.shipment_no, d1.ord_no, d1.findoc_no, d1.findoc_reff, d1.FINDOC_FINAPPDT RequestDate, d1.findoc_printeddt CostSlipDate, d1.findoc_valamt CostKg, d1.findoc_valprc ShortPercentage, d1.findoc_appby, d1.findoc_appdt ApprovedDate, d1.findoc_status Status " & _
                 "  FROM tbl_shipping_doc d1 WHERE d1.findoc_type='CS' AND d1.findoc_status<>'Rejected') t2 " & _
                 "ON t1.shipment_no=t2.shipment_no AND t1.po_no=t2.findoc_no AND t1.po_item=t2.findoc_reff " & _
                 "LEFT JOIN " & _
                 "(SELECT s1.shipment_no, '0' CheckerStatus, REPLACE(GROUP_CONCAT(s1.doc_remark SEPARATOR ','),',,','') Note FROM tbl_doc_supplier s1 WHERE s1.doc_copy=0 GROUP BY s1.shipment_no) t3 " & _
                 "ON t1.shipment_no=t3.shipment_no " & _
                 "LEFT JOIN tbm_users t4 ON t2.findoc_appby=t4.user_ct " & _
                 "LEFT JOIN (SELECT shipment_no, findoc_no, findoc_reff, findoc_printeddt InklaringDate, findoc_valamt InklaringAmount FROM tbl_shipping_doc WHERE findoc_type = 'CS' AND findoc_status <> 'Rejected') t5 ON t1.shipment_no=t5.shipment_no AND t1.po_no=t5.findoc_no AND t1.po_item=t5.findoc_reff " & _
                 "LEFT JOIN tbm_kurs t6 ON t6.currency_code=t1.currency_code AND t6.effective_date=t1.ShipOnBoard " & _
                 "LEFT JOIN (SELECT t9.shipment_no, REPLACE(GROUP_CONCAT(TRIM(t9.cont) SEPARATOR ','),',,','') TCont ," & _
                 "REPLACE(GROUP_CONCAT(TRIM(t9.zcode) SEPARATOR ','),',,','') UCont " & _
                 "FROM (SELECT t8.shipment_no,COUNT(t8.container_no) cont ,t8.Unit_code zcode " & _
                 "FROM tbl_shipping_cont t8 " & _
                 "INNER JOIN (SELECT shipment_no FROM tbl_shipping GROUP BY shipment_no) t10 " & _
                 "ON t8.shipment_no=t10.shipment_no " & _
                 "GROUP BY t8.shipment_no,t8.unit_code ) t9 GROUP BY t9.shipment_no) t7 " & _
                 "ON t7.shipment_no=t1.shipment_no " & _
                 "WHERE ((" & tgl1.Checked & " AND t2.CostSlipDate >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND t2.CostSlipDate <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (t2.CostSlipDate IS NULL AND '" & cbStatus.SelectedIndex & "' = '0') OR (t2.CostSlipDate IS NULL AND '" & cbStatus.SelectedIndex & "' = '5') OR (NOT " & tgl1.Checked & ")) " & _
                 "AND   ((t2.CostKg <> '' AND '" & cbStatus.SelectedIndex & "'= '1') OR (t2.CostKg IS NULL AND '" & cbStatus.SelectedIndex & "' = '0') OR ('" & cbStatus.SelectedIndex & "' = '4') OR (t2.Status = 'Approved' and '" & cbStatus.SelectedIndex & "' = '2') OR (t2.Status = 'Final Approved' and '" & cbStatus.SelectedIndex & "' = '3') OR (t2.CostKg IS NULL AND t5.InklaringDate IS NOT NULL AND '" & cbStatus.SelectedIndex & "' = '5') OR (t2.CostKg IS NULL AND t1.RcvOriginDoc IS NOT NULL AND '" & cbStatus.SelectedIndex & "'= '6') OR (t2.CostKg IS NULL AND t1.RcvOriginDoc IS NULL AND '" & cbStatus.SelectedIndex & "'= '7')) " & _
                 "AND   ((t3.CheckerStatus is null AND '" & cbStatus2.SelectedIndex & "' = '1') OR (t3.CheckerStatus = '0' AND '" & cbStatus2.SelectedIndex & "' = '0') OR ('" & cbStatus2.SelectedIndex & "' = '2')) " & _
                 "ORDER BY PO DESC "

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Width = 150
            'DataGridView1.Columns(11).DefaultCellStyle.Format = "N5"
            'DataGridView1.Columns(13).DefaultCellStyle.Format = "N2"
            'DataGridView1.Columns(13).DefaultCellStyle.BackColor = Color.Thistle
            'DataGridView1.Columns(22).DefaultCellStyle.BackColor = Color.Thistle
            'DataGridView1.Columns(23).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(12).DefaultCellStyle.Format = "N5"
            DataGridView1.Columns(14).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(14).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(23).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(24).DefaultCellStyle.BackColor = Color.Thistle

            DataGridView1.Columns(4).Frozen = True
        End If
    End Sub

    Private Sub Shipment_List()
        Dim dts As DataTable
        Dim FiltSIDate As String
        Dim xcbPrdM As String

        If cbPrdM.SelectedIndex < 10 Then
            xcbPrdM = "0" & cbPrdM.SelectedIndex
        Else
            xcbPrdM = cbPrdM.SelectedIndex
        End If

        '" AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t1.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT t2.shipment_no, t1.po_no, t1.po_item, '' bl_no, " & _
                  "getpoorder(t2.shipment_no, trim(t1.po_no)) PO, t1.shipment_period_fr ShipmentPeriodFrom, t1.shipment_period_to ShipmentPeriodTo, " & _
                  "t1.plant_name Plant, t1.supplier_name Supplier, t1.contract_no ContractNo, " & _
                  "t1.material_name DescriptionOfGoods, t1.country_name Origin, FORMAT(t1.price,2) UnitPrice, FORMAT(0,2) perKGs, " & _
                  "'' BL, t2.est_delivery_dt ShipOnBoard, t2.est_arrival_dt ETA, NULL as ActualArrival, NULL as EstClearanceDate, NULL as ClearanceDate, t2.Vessel Vessel, " & _
                  "t1.package_code Package, '' TotalContainer, '' UnitContainer, FORMAT( t1.Quantity,5) POQuantity, FORMAT(t2.quantity,5)  ShipQuantity, " & _
                  "FORMAT(0,5) CummulativeShipQuantity, FORMAT(0,5) GRQuantity, FORMAT(0,5) CummulativeGRQuantity, d1.ShippingInstruction SIDate FROM " & _
                  "(SELECT t1.po_no, t2.po_item, " & _
                  " m4.plant_name, m5.supplier_name, t1.contract_no, " & _
                  " m1.material_name, m6.country_name, t2.Price, " & _
                  " t1.shipment_period_fr, t1.shipment_period_to, t2.package_code, t2.Quantity " & _
                  " FROM tbl_po t1, tbl_po_detail t2, " & _
                  " tbm_material m1, tbm_material_group m2, tbm_company m3, tbm_plant m4, tbm_supplier m5, tbm_country m6, tbm_users m7 " & _
                  " WHERE t1.po_no = t2.po_no " & _
                  " AND t2.material_code=m1.material_code AND m1.group_code=m2.group_code AND t1.company_code=m3.company_code AND t1.plant_code=m4.plant_code " & _
                  " AND t1.supplier_code=m5.supplier_code AND t2.country_code=m6.country_code AND t1.createdby = m7.user_ct " & _
                  " AND t1.plant_code IN (" & strF & ") " & _
                  " AND t1.po_no NOT IN (SELECT po_no FROM tbl_shipping_detail t1, tbl_shipping t2 WHERE t1.shipment_no=t2.shipment_no AND t2.status <> 'Rejected') " & _
                  " AND ( " & _
                  "     (     (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "       AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "       AND (t1.supplier_code = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "       AND (m2.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "       AND (m3.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "       AND (t1.createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                  "       AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t1.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t1.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "       AND trim('" & txtPONO.Text & "') = '' AND 1=1) " & _
                  "     OR (trim('" & txtPONO.Text & "') LIKE trim(t1.po_no)) " & _
                  "     ) " & _
                  ") t1 " & _
                  "LEFT JOIN tbl_po_schedule t2 ON t2.po_no=t1.po_no AND t2.po_item=t1.po_item " & _
                  "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ShippingInstruction FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & _
                  " "
        '" AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t1.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr2 = "SELECT t1.shipment_no, t1.po_no, t1.po_item, t1.bl_no, " & _
                  "getpoorder(t1.shipment_no, trim(t1.po_no)) PO, t1.shipment_period_fr ShipmentPeriodFrom, t1.shipment_period_to ShipmentPeriodTo, " & _
                  "t1.plant_name Plant, t1.supplier_name Supplier, t1.contract_no ContractNo, " & _
                  "t1.material_name DescriptionOfGoods, t1.country_name Origin, FORMAT(t1.price,2) UnitPrice, perKGs, " & _
                  "t1.bl_no BL, t1.est_delivery_dt ShipOnBoard, t1.est_arrival_dt ETA, t1.notice_arrival_dt ActualArrival, t1.est_bapb_dt EstClearanceDate, t1.bapb_dt ClearanceDate, t1.Vessel Vessel, " & _
                  "t1.pack_code Package, if(t1.TotalContainer=0 or t1.UnitContainer='CURAH','',cast(t1.TotalContainer as char)) TotalContainer, t1.UnitContainer, " & _
                  "FORMAT( t1.Quantity,5) POQuantity, FORMAT(t1.ShipQuantity,5) ShipQuantity, " & _
                  "FORMAT(getCumQuantity(t1.shipment_no, t1.po_no, t1.po_item),5) CummulativeShipQuantity, FORMAT(IF(d4.GRQuantity IS NULL, 0, d4.GRQuantity),5) GRQuantity, " & _
                  "FORMAT(IF(d4.GRQuantity IS NULL, 0, getCumGR(t1.poord, t1.po_no, t1.po_item)) ,5) CummulativeGRQuantity, " & _
                  "IF(d1.ShippingInstruction_po IS NULL, d2.ShippingInstruction_bl, d1.ShippingInstruction_po) SIDate FROM " & _
                  "(SELECT t1.shipment_no, t2.po_no, getorder(t1.shipment_no, t2.po_no) poord, t2.po_item, t1.bl_no, " & _
                  " m4.plant_name, m5.supplier_name, t3.contract_no, " & _
                  " m1.material_name, m6.country_name, t4.price, " & _
                  " t3.shipment_period_fr, t3.shipment_period_to, " & _
                  " t1.est_delivery_dt, t1.est_arrival_dt, t1.notice_arrival_dt, t1.est_bapb_dt, t1.bapb_dt, t1.Vessel, t2.pack_code, " & _
                  " (SELECT COUNT(ts1.ord_no) FROM tbl_shipping_cont ts1 WHERE ts1.shipment_no=t1.shipment_no) TotalContainer, (SELECT IF(COUNT(ts1.ord_no)=0,'', GROUP_CONCAT(DISTINCT ts1.unit_code SEPARATOR ',')) FROM tbl_shipping_cont ts1 WHERE ts1.shipment_no=t1.shipment_no) UnitContainer, t4.Quantity, t2.quantity ShipQuantity " & _
                  " FROM tbl_shipping t1, tbl_shipping_detail t2, tbl_po t3, tbl_po_detail t4, " & _
                  " tbm_material m1, tbm_material_group m2, tbm_company m3, tbm_plant m4, tbm_supplier m5, tbm_country m6, tbm_users m7 " & _
                  " WHERE(t1.shipment_no = t2.shipment_no And t2.po_no = t3.po_no And t2.po_no = t4.po_no And t2.po_item = t4.po_item) " & _
                  " AND t2.material_code=m1.material_code AND m1.group_code=m2.group_code AND t1.plant_code=m4.plant_code AND m4.company_code=m3.company_code " & _
                  " AND t1.supplier_code=m5.supplier_code AND t4.country_code=m6.country_code AND t1.createdby = m7.user_ct " & _
                  " AND t1.plant_code IN (" & strF & ") " & _
                  " AND ( " & _
                  "     (    (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "      AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "      AND (t1.supplier_code = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "      AND (m2.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "      AND (m3.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "      AND (t1.createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                  "      AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t3.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t3.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "      AND trim('" & txtPONO.Text & "') = '' AND 2=2) " & _
                  "    OR (trim('" & txtPONO.Text & "') LIKE trim(t3.po_no)) " & _
                  " ) " & _
                  ") t1 " & _
                  "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ShippingInstruction_po FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & _
                  "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ShippingInstruction_bl FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d2 ON d2.shipment_no=t1.shipment_no " & _
                  "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CostSlip, FORMAT(FINDOC_VALAMT,2) perKGs, MIN(findoc_finappdt) TdTerimaCostSlip FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status<>'Rejected' GROUP BY shipment_no) d3 ON d3.shipment_no=t1.shipment_no " & _
                  "LEFT JOIN (SELECT shipment_no, po_no, po_item, SUM(quantity) GRQuantity FROM tbl_bapb_detail GROUP BY shipment_no, po_no, po_item) d4 ON d4.shipment_no=t1.shipment_no AND d4.po_no = t1.po_no AND d4.po_item = t1.po_item " & _
                  " "
        '"LEFT JOIN (SELECT po_no, CAST(GROUP_CONCAT(DISTINCT(OpeningDt) SEPARATOR ',') AS CHAR) ShippingInstruction_po FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & _
        '"LEFT JOIN (SELECT shipment_no, CAST(GROUP_CONCAT(DISTINCT(OpeningDt) SEPARATOR ',') AS CHAR) ShippingInstruction_bl FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d2 ON d2.shipment_no=t1.shipment_no " & _
        '"LEFT JOIN (SELECT shipment_no, CAST(GROUP_CONCAT(DISTINCT(findoc_printeddt) SEPARATOR ',') AS CHAR) CostSlip, FORMAT(FINDOC_VALAMT,2) perKGs, CAST(GROUP_CONCAT(DISTINCT(findoc_finappdt) SEPARATOR ',') AS CHAR) TdTerimaCostSlip FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status<>'Rejected' GROUP BY shipment_no) d3 ON d3.shipment_no=t1.shipment_no " & _
        '"LEFT JOIN (SELECT shipment_no, po_no, po_item, SUM(quantity) GRQuantity FROM tbl_bapb_detail GROUP BY shipment_no, po_no, po_item) d4 ON d4.shipment_no=t1.shipment_no AND d4.po_no = t1.po_no AND d4.po_item = t1.po_item " & _

        FiltSIDate = "( ((" & tgl1.Checked & " AND SIDate >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND SIDate <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) " & _
                     "AND ((" & tgl1_2.Checked & " AND ((ActualArrival IS NULL AND ETA >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND ETA <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') OR (ActualArrival IS NOT NULL AND ActualArrival >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND ActualArrival <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "'))) OR (Not " & tgl1_2.Checked & ")) " & _
                     "AND (trim('" & txtPONO.Text & "') = '') OR (trim('" & txtPONO.Text & "') <> '')) "

        '"AND ((ShipmentPeriod = '" & cbPrdM.Text & " " & cbPrdY.Text & "') OR ('" & cbPrdM.Text & "' = '')) " & _

        Select Case cbStatus.Text
            Case "No Schedule" : SQLstr = "SELECT t1.* FROM (" & SQLstr1 & ") t1 WHERE shipment_no IS NULL AND " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, po_no DESC "
            Case "Schedule" : SQLstr = "SELECT t1.* FROM (" & SQLstr1 & ") t1 WHERE shipment_no > 0 AND " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, po_no DESC "
            Case "Shipment" : SQLstr = "SELECT t1.* FROM (" & SQLstr2 & ") t1 WHERE " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, po_no DESC "
            Case "All PO" : SQLstr = "SELECT t1.* FROM (" & SQLstr1 & " UNION " & SQLstr2 & ") t1 WHERE " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, PO DESC "
            Case "Pending Shipping Instruction" : SQLstr = "SELECT t1.* FROM (" & SQLstr1 & " UNION " & SQLstr2 & ") t1 Where SIDate IS NULL AND " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, PO DESC "
            Case "Pending Clearance" : SQLstr = "SELECT t1.* FROM (" & Replace(SQLstr2, "2=2", "(t1.bapb_dt IS NULL)") & ") t1 WHERE " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, PO DESC "
            Case "Clearance" : SQLstr = "SELECT t1.* FROM (" & Replace(SQLstr2, "2=2", "(t1.bapb_dt IS NOT NULL)") & ") t1 WHERE " & FiltSIDate & " ORDER BY ShipmentPeriodFrom, PO DESC "
            Case "PO To be Closed" : SQLstr = "SELECT t1.* FROM (" & SQLstr2 & ") t1 WHERE " & FiltSIDate & " AND CummulativeGRQuantity >= POQuantity ORDER BY ShipmentPeriodFrom, po_no DESC "
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(7).Width = 150

            DataGridView1.Columns(14).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(15).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(16).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(17).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(18).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(23).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(24).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(25).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(26).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(27).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(28).DefaultCellStyle.BackColor = Color.Thistle

            DataGridView1.Columns(4).Frozen = True
        End If
    End Sub

    Private Sub Logistic_List()
        Dim dts As DataTable

        '" AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t1.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT t1.shipment_no, t1.po_no, t1.po_item, t1.bl_no, " & _
                  "getpoorder(t1.shipment_no, TRIM(t1.po_no)) PO, t1.plant_name FinalPlant, t1.port_name FinalPort, t1.est_arrival_dt ETA, t1.notice_arrival_dt ActualArrival, " & _
                  "m1.company_name EMKL, t1.material_name DescriptionOfGoods, t1.country_name Origin, t1.Supplier, " & _
                  "DATE_FORMAT(t1.FORWARD_COPYDOC_DT_2,'%M %Y') MonthDocToExp, t1.FORWARD_COPYDOC_DT_2 DocToExpedition, t1.area_rcv_doc_dt AreaDocReceived, t1.AREA_RCV_RIL_DT DeptanReceivedArea, t1.ExpeditionNote, " & _
                  "v2.DocExpedition, v2.DocExpReceived, t1.aju_no AJU, " & _
                  "IF(t1.sppb_dt IS NULL, t1.est_sppb_dt, t1.sppb_dt) SPPBDate, t1.sppb_no SPPBNo, t1.est_clr_dt EstDeliveryDate, t1.clr_dt DeliveryDate, t1.est_bapb_dt EstClearanceDate, t1.bapb_dt ClearanceDate, (t1.free_time + t1.fte) FTD, t1.OverBrengen, t1.TahanOB, t1.ClearanceNote, " & _
                  "FORMAT(t1.quantity,5) Quantity, t1.line_name ShippingLines, t1.Vessel Vessel, if(UnitContainer = 'CURAH', null, v1.TotalContainer) TotalContainer, v1.UnitContainer FROM " & _
                  "(SELECT t1.shipment_no, t2.po_no, t2.po_item, t1.bl_no, " & _
                  " m4.plant_name, m5.port_name, t1.est_arrival_dt, t1.notice_arrival_dt, t1.emkl_code, m1.material_name, m6.country_name, m9.supplier_name Supplier, t1.FORWARD_COPYDOC_DT_2, t1.area_rcv_doc_dt, t1.AREA_RCV_RIL_DT, t1.exp_note ExpeditionNote, " & _
                  " t1.aju_no, t1.est_sppb_dt, t1.sppb_dt, t1.sppb_no, t1.est_clr_dt, t1.clr_dt, t1.est_bapb_dt, t1.free_time, t1.fte, t1.bapb_dt, " & _
                  " IF(t1.ob_dt='0000-00-00',NULL,t1.ob_dt) OverBrengen, IF(t1.tahan_ob='N','','Yes') TahanOB, t1.fte_note ClearanceNote, " & _
                  " t2.quantity, m7.line_name, t1.Vessel, t1.total_container " & _
                  " FROM tbl_shipping t1, tbl_shipping_detail t2, tbl_po t3, tbl_po_detail t4, " & _
                  " tbm_material m1, tbm_material_group m2, tbm_company m3, tbm_plant m4, tbm_port m5, tbm_country m6, tbm_lines m7, tbm_users m8, tbm_supplier m9 " & _
                  " WHERE (t1.shipment_no = t2.shipment_no And t2.po_no = t3.po_no And t2.po_no = t4.po_no And t2.po_item = t4.po_item) " & _
                  " AND t2.material_code=m1.material_code AND m1.group_code=m2.group_code AND t1.plant_code=m4.plant_code AND m4.company_code=m3.company_code " & _
                  " AND t1.port_code=m5.port_code AND t4.country_code=m6.country_code AND t1.shipping_line = m7.line_code AND t1.createdby = m8.user_ct AND t3.supplier_code=m9.supplier_code " & _
                  " AND t1.plant_code IN (" & strF & ") " & _
                  " AND ( " & _
                  "     (((" & tgl1_2.Checked & " AND (t1.NOTICE_ARRIVAL_DT is null AND (cast(t1.EST_ARRIVAL_DT as char(10)) >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND cast(t1.EST_ARRIVAL_DT as char(10)) <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') " & _
                  "            OR (cast(t1.NOTICE_ARRIVAL_DT as char(10)) >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND cast(t1.NOTICE_ARRIVAL_DT as char(10)) <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) " & _
                  "        ) OR Not " & tgl1_2.Checked & ") " & _
                  "      AND (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "      AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "      AND (m2.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "      AND (t1.emkl_code = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "      AND (DATE_FORMAT(t1.FORWARD_COPYDOC_DT_2,'%M %Y') = '" & cbPrdM.Text & " " & cbPrdY.Text & "' OR '' = '" & cbPrdY.Text & "') " & _
                  "      AND (m3.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "      AND (t1.createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                  "      AND trim('" & txtPONO.Text & "') = '') " & _
                  "    OR (trim('" & txtPONO.Text & "') LIKE trim(t3.po_no)) " & _
                  " ) AND 1=1 " & _
                  ") t1 " & _
                  "LEFT JOIN tbm_expedition m1 ON t1.emkl_code=m1.company_code " & _
                  "LEFT JOIN (SELECT shipment_no, SUM(1) TotalContainer, GROUP_CONCAT(DISTINCT unit_code) UnitContainer FROM tbl_shipping_cont GROUP BY shipment_no) v1 ON t1.shipment_no=v1.shipment_no " & _
                  "LEFT JOIN (SELECT t1.shipment_no, GROUP_CONCAT(t2.doc_name,' ') DocExpedition, GROUP_CONCAT(distinct(DATE_FORMAT(t1.doc_dt,'%c/%e/%Y')),' ') DocExpReceived FROM tbl_doc_expedition t1, tbm_document t2 WHERE t1.doc_code=t2.doc_code GROUP BY t1.shipment_no) v2 ON t1.shipment_no=v2.shipment_no " & _
                  " "

        Select Case cbStatus.Text
            Case "Deliver" : SQLstr = Replace(SQLstr1, "1=1", "t1.clr_dt < DATE_FORMAT(now(),'%Y-%m-%d')")
            Case "On Deliver" : SQLstr = Replace(SQLstr1, "1=1", "t1.est_clr_dt >= DATE_FORMAT(now(),'%Y-%m-%d') AND t1.clr_dt IS NULL")
            Case "Pending Schedule" : SQLstr = Replace(SQLstr1, "1=1", "(t1.est_sppb_dt IS NOT NULL or t1.sppb_dt IS NOT NULL) AND (t1.est_clr_dt IS NULL AND t1.clr_dt IS NULL)")
            Case "Pending SPPB" : SQLstr = Replace(SQLstr1, "1=1", "(t1.est_sppb_dt IS NULL AND t1.sppb_dt IS NULL)")
            Case "Pending Clearance" : SQLstr = Replace(SQLstr1, "1=1", "t1.bapb_dt IS NULL")
            Case "Clearance" : SQLstr = Replace(SQLstr1, "1=1", "t1.bapb_dt IS NOT NULL")
            Case "All" : SQLstr = SQLstr1
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Width = 150

            DataGridView1.Columns(17).Width = 300
            
            DataGridView1.Columns(21).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(23).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(24).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(25).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(26).DefaultCellStyle.BackColor = Color.Thistle

            DataGridView1.Columns(4).Frozen = True
        End If
    End Sub

    Private Sub DP_List()
        Dim dts As DataTable

        '"  AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t2.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT * FROM " & _
                  " (SELECT t1.shipment_no, '' po_no, 0 po_item,  t2.bl_no, t2.bl_no BL, " & _
                  "  t3.DetailOfPO, t3.GroupMaterial, t1.findoc_no BPUMNo, t1.findoc_printeddt BPUMPrinted, t1.findoc_finappdt TransferDate, t2.forward_copydoc_dt_2 ForwardToExp, t1.findoc_note Note, t1.findoc_valamt Amount, " & _
                  "     (SELECT st1.findoc_printeddt FROM tbl_shipping_doc st1 " & _
                  "      WHERE st1.findoc_type = 'PP' AND st1.findoc_status <> ' Rejected' AND st1.findoc_reff LIKE CONCAT('%', t1.shipment_no,';',t1.ord_no, '%') LIMIT 1) BPJUMPrinted, " & _
                  "  m2.company_name Expedition, m2.company_shortname ExpShort, m4.plant_name FinalPlant, m5.port_name FinalPort, t2.vessel Vessel,t2.est_arrival_dt ETA, t2.notice_arrival_dt ActualArrival, t2.est_sppb_dt EstClearance, t2.sppb_dt Clearance, " & _
                  "  CONCAT(m2.bank_name, ' ',m2.bank_branch) Bank, m2.account_no AccountNo, m2.account_name AccountName, " & _
                  "  IF(t1.findoc_reff IS NULL,'',t1.findoc_reff) BPJUMNo, " & _
                  "  m1.name CreatedBy, m3.name RequestBy " & _
                  "  FROM tbl_shipping_doc t1, tbl_shipping t2, " & _
                  "  (SELECT t1.shipment_no, GROUP_CONCAT(DISTINCT getpoorder(t1.shipment_no, TRIM(t1.po_no))) DetailOfPO, GROUP_CONCAT(DISTINCT m2.group_name) GroupMaterial " & _
                  "   FROM tbl_shipping_detail t1, tbm_material m1, tbm_material_group m2 " & _
                  "   WHERE (t1.material_code = m1.material_code And m1.group_code = m2.group_code) " & _
                  "   GROUP BY t1.shipment_no) t3, " & _
                  "  tbm_users m1, tbm_expedition m2, tbm_users m3, tbm_plant m4, tbm_port m5, tbm_company m6 " & _
                  "  WHERE t1.shipment_no = t2.shipment_no And t1.shipment_no = t3.shipment_no " & _
                  "  AND t1.findoc_createdby = m1.user_ct AND t1.findoc_appby = m3.user_ct  AND t1.findoc_to=m2.company_code " & _
                  "  AND t2.plant_code=m4.plant_code AND t2.port_code = m5.port_code AND m4.company_code=m6.company_code " & _
                  "  AND t2.plant_code IN (" & strF & ") " & _
                  "  AND t1.findoc_type = 'DP' AND t1.findoc_status <> 'Rejected' " & _
                  "  AND (( " & _
                  "        ((" & tgl1_2.Checked & " AND t1.findoc_printeddt >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.findoc_printeddt <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1_2.Checked & ")) " & _
                  "     AND (m6.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "     AND (t2.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "     AND (t2.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "     AND (t3.GroupMaterial LIKE '%" & txtGrpMat.Text & "%' OR '' = '" & txtGrpMat.Text & "') " & _
                  "     AND (t1.findoc_to = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "     AND (t1.findoc_createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                  "     AND trim('" & txtPONO.Text & "') = '') " & _
                  "    OR ('" & txtPONO.Text & "' <> '' AND t3.DetailOfPO LIKE trim('%" & txtPONO.Text & "%')) " & _
                  " ) AND 1=1 " & _
                  ") t1 " & _
                  "WHERE ((" & tgl1.Checked & " AND BPJUMPrinted >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND BPJUMPrinted <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) " & _
                  "ORDER BY t1.BPUMPrinted, t1.TransferDate "

        Select Case cbStatus.Text
            Case "Outstanding BPUM" : SQLstr = Replace(SQLstr1, "1=1", "t1.findoc_finappdt IS NULL")
            Case "Closed BPUM" : SQLstr = Replace(SQLstr1, "1=1", "t1.findoc_finappdt IS NOT NULL")
            Case "Outstanding BPJUM" : SQLstr = Replace(SQLstr1, "1=1", "t1.findoc_reff IS NULL AND t1.findoc_finappdt IS NOT NULL")
            Case "Outstanding based on TransfDt/Arrival" : SQLstr = Replace(SQLstr1, "1=1", "t1.findoc_reff IS NULL " & _
                                                "AND (DATE_FORMAT(DATE_ADD(t1.findoc_finappdt, INTERVAL 1 MONTH),'%Y-%m-%d') < DATE_FORMAT(NOW(),'%Y-%m-%d') " & _
                                                " OR (DATE_FORMAT(DATE_ADD(t2.est_arrival_dt, INTERVAL 1 MONTH),'%Y-%m-%d') < DATE_FORMAT(NOW(),'%Y-%m-%d') AND t2.notice_arrival_dt IS NULL) " & _
                                                " OR DATE_FORMAT(DATE_ADD(t2.notice_arrival_dt, INTERVAL 1 MONTH),'%Y-%m-%d') < DATE_FORMAT(NOW(),'%Y-%m-%d'))")

            Case "Closed BPJUM" : SQLstr = Replace(SQLstr1, "1=1", "t1.findoc_reff IS NOT NULL")
            Case "All" : SQLstr = SQLstr1
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)
        SQLStrE = SQLstr
        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(14).Visible = False
            DataGridView1.Columns(4).Width = 150

            DataGridView1.Columns(12).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(7).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(8).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(9).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(11).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(12).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(13).DefaultCellStyle.BackColor = Color.Thistle

            DataGridView1.Columns(7).Frozen = True
        End If
    End Sub


    Private Sub Payment_List()
        Dim dts As DataTable

        '" AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t1.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT  t1.shipment_no, t1.po_no, t1.po_item, t1.bl_no, " & _
                  "getpoorder(t1.shipment_no, TRIM(t1.po_no)) PO, t1.material_name DescriptionOfGoods, t1.country_name Origin, " & _
                  "t1.ShipmentPeriod, t1.est_arrival_dt ETA, t1.notice_arrival_dt ActualArrival, t1.tt_dt TTDate, t1.due_dt DueDate, " & _
                  "t1.payment_name PaymentTerm, t1.invoice_no InvoiceNo, t1.invoice_dt InvoiceDate, t1.currency_code Currency, FORMAT(t1.invoice_amount-t1.invoice_penalty,2) InvoiceAmount, " & _
                  " FORMAT(t1.quantity,5) ShipQuantity, t1.unit_code Unit, t1.supplier_name Supplier, t1.plant_name FinalPlant, t1.port_name FinalPort, t1.total_container Container, " & _
                  "d0.LCNo, d1.BudgetOpeningLC, d2.BugdetRemitance, d3.BudgetTT, d4.PaymentVoucher, t1.name DocCreatedBy  FROM " & _
                  "(SELECT t1.shipment_no, t2.po_no, t2.po_item, t1.bl_no, " & _
                  "IF(30-DATE_FORMAT(t3.shipment_period_fr,'%d')+1 > DATE_FORMAT(t3.shipment_period_to,'%d'), DATE_FORMAT(t3.shipment_period_fr,'%M %Y'), DATE_FORMAT(t3.shipment_period_to,'%M %Y')) ShipmentPeriod, " & _
                  " m1.material_name, t3.shipment_period_fr, t3.shipment_period_to, t1.est_arrival_dt, t1.notice_arrival_dt, t1.tt_dt, t1.due_dt, " & _
                  " t1.currency_code, t5.invoice_no, t5.invoice_dt, t5.invoice_amount, t5.invoice_penalty, m8.payment_name, m6.country_name, t2.quantity, t4.unit_code, " & _
                  " m7.supplier_name, m4.plant_name, m5.port_name, t1.total_container, mu.name " & _
                  " FROM tbl_shipping t1, tbl_shipping_detail t2, tbl_po t3, tbl_po_detail t4, tbl_shipping_invoice t5, " & _
                  " tbm_material m1, tbm_material_group m2, tbm_company m3, tbm_plant m4, tbm_port m5, tbm_country m6, tbm_supplier m7, tbm_payment_term m8, tbm_users mu " & _
                  " WHERE (t1.shipment_no = t2.shipment_no) AND (t2.po_no = t3.po_no) AND (t2.po_no = t4.po_no And t2.po_item = t4.po_item) AND (t2.shipment_no=t5.shipment_no AND t2.po_no=t5.po_no AND t2.po_item=t5.ord_no) " & _
                  " AND t2.material_code=m1.material_code AND m1.group_code=m2.group_code AND t1.plant_code=m4.plant_code AND m4.company_code=m3.company_code " & _
                  " AND t1.port_code=m5.port_code AND t4.country_code=m6.country_code AND t1.supplier_code = m7.supplier_code AND t3.payment_code=m8.payment_code AND t1.createdby = mu.user_ct " & _
                  " AND t1.plant_code IN (" & strF & ") " & _
                  " AND (( " & _
                  "          ((" & tgl1_2.Checked & " AND ((t1.NOTICE_ARRIVAL_DT is null AND t1.EST_ARRIVAL_DT  >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.EST_ARRIVAL_DT  <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) OR (t1.NOTICE_ARRIVAL_DT is not null AND t1.NOTICE_ARRIVAL_DT >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.NOTICE_ARRIVAL_DT <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) OR (Not " & tgl1_2.Checked & ")) " & _
                  "      AND ((" & tgl1.Checked & " AND t1.tt_dt >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND t1.tt_dt <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) " & _
                  "      AND (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "      AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "      AND (m2.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "      AND (t1.supplier_code = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "      AND (m3.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "      AND (t1.createdby = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                  "      AND trim('" & txtPONO.Text & "') = '') " & _
                  "    OR (trim('" & txtPONO.Text & "') LIKE trim(t3.po_no)) " & _
                  " ) AND 1=1 " & _
                  ") t1 " & _
                  "LEFT JOIN (SELECT po_no, CAST(GROUP_CONCAT(DISTINCT(lc_no) SEPARATOR ',') AS CHAR) LCNo FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d0 ON d0.po_no=t1.po_no " & _
                  "LEFT JOIN (SELECT po_no, MIN(OpeningDt) BudgetOpeningLC FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & _
                  "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BugdetRemitance FROM tbl_remitance WHERE STATUS<>'Rejected' GROUP BY shipment_no) d2 ON d2.shipment_no=t1.shipment_no " & _
                  "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetTT FROM tbl_budgets WHERE type_code='TT' AND STATUS<>'Rejected') d3 ON d3.shipment_no=t1.shipment_no " & _
                  "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) PaymentVoucher FROM tbl_shipping_doc WHERE findoc_type='PV' AND findoc_status<>'Rejected' GROUP BY shipment_no) d4 ON d4.shipment_no=t1.shipment_no " & _
                  "Where (ShipmentPeriod = '" & cbPrdM.Text & " " & cbPrdY.Text & "' OR '' = '" & cbPrdY.Text & "') " & _
                  "AND 2=2 "

        Select Case cbStatus.Text
            Case "Payment Paid" : SQLstr = Replace(SQLstr1, "1=1", "t1.due_dt IS NOT NULL")
            Case "Pending Payment" : SQLstr = Replace(SQLstr1, "1=1", "t1.due_dt IS NULL")
            Case "All" : SQLstr = SQLstr1
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Width = 150

            DataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(11).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(12).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(13).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(14).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(16).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(25).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(27).DefaultCellStyle.BackColor = Color.Thistle

            DataGridView1.Columns(4).Frozen = True
        End If
    End Sub

    Private Sub Contract_List()
        Dim dts As DataTable

        '"  AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t1.contract_dt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT t1.contract_no, t1.contract_no ContractNo, t1.contract_dt ContractDate, t1.company_name Company, t1.supplier_name Supplier, t1.material_name DescriptionOfGoods, t1.Quantity, t1.unit_code Unit, t1.price UnitPrice, t4.PO, t3.TotalShipment, IF(t2.ShipmentQuantity IS NULL,0, t2.ShipmentQuantity) ShipmentQuantity FROM " & _
                  " (SELECT t1.contract_no, t1.contract_dt, m1.company_name, m2.supplier_name, t2.material_code, m3.material_name, t2.quantity, t2.unit_code, t2.price " & _
                  "  FROM tbl_contract t1, tbl_contract_detail t2, tbm_company m1, tbm_supplier m2, tbm_material m3 " & _
                  "  WHERE t1.contract_no = t2.contract_no " & _
                  "  AND t1.company_code=m1.company_code AND t1.supplier_code=m2.supplier_code AND t2.material_code=m3.material_code " & _
                  "  AND t1.company_code IN (SELECT DISTINCT company_code FROM tbm_plant WHERE plant_code IN (" & strF & ")) " & _
                  "  AND ( ((m1.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "         AND (DATE_FORMAT(t1.contract_dt,'%M %Y') = '" & cbPrdM.Text & " " & cbPrdY.Text & "' OR '' = '" & cbPrdY.Text & "') AND trim('" & txtPONO.Text & "') = '') " & _
                  "       OR (trim('" & txtPONO.Text & "') LIKE trim(t1.contract_no)) )" & _
                  " ) t1 " & _
                  "LEFT JOIN " & _
                  " (SELECT t2.contract_no, t1. material_code, SUM(t1.quantity) ShipmentQuantity " & _
                  "  FROM tbl_shipping_detail t1, tbl_po t2 " & _
                  "  WHERE t1.po_no=t2.po_no AND t2.contract_no IS NOT NULL AND TRIM(t2.contract_no) <> '' " & _
                  "  GROUP BY t2.contract_no, t1. material_code " & _
                  " ) t2 ON t1.contract_no=t2.contract_no AND t1.material_code=t2.material_code " & _
                  "LEFT JOIN " & _
                  " (SELECT t2.contract_no, t1. material_code, COUNT(DISTINCT t1.shipment_no) TotalShipment " & _
                  "  FROM tbl_shipping_detail t1, tbl_po t2 " & _
                  "  WHERE t1.po_no=t2.po_no AND t2.contract_no IS NOT NULL AND TRIM(t2.contract_no) <> '' GROUP BY t2.contract_no, t1. material_code " & _
                  " ) t3 ON t1.contract_no=t3.contract_no AND t1.material_code=t3.material_code " & _
                  "LEFT JOIN " & _
                  " (SELECT contract_no, GROUP_CONCAT(DISTINCT po_no SEPARATOR ', ') PO FROM tbl_po t1 " & _
                  "  WHERE contract_no <> '' GROUP BY contract_no) t4 ON t1.contract_no=t4.contract_no " & _
                  "WHERE 1=1 "

        Select Case cbStatus.Text
            Case "Closed Contract" : SQLstr = Replace(SQLstr1, "1=1", "(t1.quantity <= t2.ShipmentQuantity)")
            Case "Outstanding Contract" : SQLstr = Replace(SQLstr1, "1=1", "(t1.Quantity > t2.ShipmentQuantity OR t2.ShipmentQuantity IS NULL)")
            Case "All" : SQLstr = SQLstr1
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Width = 150

            DataGridView1.Columns(1).Frozen = True
        End If
    End Sub
    Private Sub TaxForecast_List()
        Dim dts As DataTable
        Dim xcbPrdM As String

        If cbPrdM.SelectedIndex < 10 Then
            xcbPrdM = "0" & cbPrdM.SelectedIndex
        Else
            xcbPrdM = cbPrdM.SelectedIndex
        End If

        SQLstr1 = "SELECT t1.shipment_no, t1.po_no, 0 po_item, t1.bl_no, " & _
                  "t1.port_name Port, t1.plant_name Plant, t1.shipment_period_fr ShipmentPeriodFrom, t1.shipment_period_to ShipmentPeriodTo, DATE_FORMAT(t1.est_delivery_dt,'%d/%m/%Y') ShipOnBoard, DATE_FORMAT(t1.est_arrival_dt,'%d/%m/%Y') ETA, DATE_FORMAT(IF(t1.notice_arrival_dt IS NULL, '', t1.notice_arrival_dt),'%d/%m/%Y') ActualArrival, " & _
                  "DATE_FORMAT(t1.tt_dt,'%d/%m/%Y') TTDate, DATE_FORMAT(IF(t2.openingdt IS NULL, '', t2.openingdt),'%d/%m/%Y') BudgetPIBDate, FORMAT(t1.bea_masuk,2) DUTY, FORMAT(t1.vat,2) VAT, FORMAT(t1.pph21,2) PPH22, FORMAT(t1.SumOfTax,2) SumOfTax, " & _
                  "t1.po_no PO, '' DetailOfOrder, cast(po_detail as char) DetailOfShipped, '' DetailOfOutStanding, 'PO Shipped' Control " & _
                  "FROM " & _
                  " (SELECT t1.shipment_no, t1.bl_no, GROUP_CONCAT(DISTINCT TRIM(t2.po_no) SEPARATOR ', ') po_no, GROUP_CONCAT(CONCAT(quantity,':',m3.material_name) SEPARATOR ', ') po_detail, " & _
                  " t0.shipment_period_fr, t0.shipment_period_to, m2.port_name, m1.plant_name, t1.est_delivery_dt, t1.est_arrival_dt, t1.notice_arrival_dt, t1.tt_dt, t1.bea_masuk, t1.vat, t1.pph21, t1.piud, (t1.bea_masuk + t1.VAT + t1.pph21) SumOfTax " & _
                  "  FROM tbl_po t0, tbl_shipping t1, tbl_shipping_detail t2, tbm_plant m1, tbm_port m2, tbm_material m3, tbm_company m4 " & _
                  "  WHERE (t0.po_no=t2.po_no AND t1.shipment_no = t2.shipment_no And t1.plant_code = m1.plant_code And t1.port_code = m2.port_code AND t2.material_code=m3.material_code AND m1.company_code=m4.company_code) " & _
                  "   AND t1.plant_code IN (" & strF & ") " & _
                  "   AND (m4.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "   AND (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "   AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "  GROUP BY t1.shipment_no) t1 " & _
                  "LEFT JOIN  " & _
                  "(SELECT shipment_no, openingdt FROM tbl_budgets WHERE type_code='TT' AND STATUS <>' Rejected') t2 ON t1.shipment_no=t2.shipment_no " & _
                  "WHERE (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t1.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t1.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "AND ((" & tgl1_2.Checked & " AND ((t1.NOTICE_ARRIVAL_DT is null AND t1.EST_ARRIVAL_DT  >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.EST_ARRIVAL_DT  <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) OR (t1.NOTICE_ARRIVAL_DT is not null AND t1.NOTICE_ARRIVAL_DT >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.NOTICE_ARRIVAL_DT <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) OR (Not " & tgl1_2.Checked & ")) " & _
                  "AND ((" & tgl1.Checked & " AND t1.tt_dt  >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND t1.tt_dt  <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) " & _
                  " "

        SQLstr2 = "SELECT t1.port_name Port, t1.plant_name Plant, t1.po_no PO, t1.material_name DescriptionOfGoods, t1.country_name Origin, " & _
                  "DATE_FORMAT(t1.etd,'%d/%m/%Y') ShipOnBoard, DATE_FORMAT(DATE_ADD(t1.etd , INTERVAL t1.lt DAY),'%d/%m/%Y') ETA, t1.payment_name PaymentTerm, DATE_FORMAT(DATE_ADD(DATE_ADD(t1.etd , INTERVAL t1.lt DAY), INTERVAL t1.payment_days DAY),'%d/%m/%Y') TTDate, " & _
                  "t1.shipment_period_fr, t1.shipment_period_to, t1.poqty, t1.shipqty, t1.outstdqty, currency_code Currency, FORMAT(t1.Price,2) UnitPrice, " & _
                  "t1.bea_masuk PrcDUTY, (((t1.bea_masuk /100) * (t1.outstdqty * t1.Price)) * taxkurs) DUTY, " & _
                  "t1.ppn PrcVAT, (((t1.ppn /100) * ((t1.outstdqty * t1.Price) + ((t1.bea_masuk /100) * (t1.outstdqty * t1.Price)))) * taxkurs) VAT, " & _
                  "t1.pph_21 PrcPPH22, (((t1.pph_21 /100) * ((t1.outstdqty * t1.Price) + ((t1.bea_masuk /100) * (t1.outstdqty * t1.Price)))) * taxkurs) PPH22 " & _
                  "FROM " & _
                  "     (SELECT t1.*, IF(t1.shipqty IS NULL,t1.poqty,(t1.poqty-t1.shipqty)) outstdqty, " & _
                  "      IF(shipqty IS NULL, IF(t1.shipment_period_fr > last10Day, t1.shipment_period_fr, next5Day), " & _
                  "                          IF(lastETD > last10Day, DATE_ADD(lastETD, INTERVAL 5 DAY), next5Day)) ETD, " & _
                  "      t3.lt, t2.bea_masuk, t2.ppn, t2.pph_21, " & _
                  "      (SELECT t2.kurs_pajak FROM tbm_kurs t2 WHERE t2.currency_code=t1.currency_code AND t2.effective_date <= ETD AND kurs_pajak <> 0 ORDER BY t2.effective_date DESC LIMIT 1) taxkurs " & _
                  "      FROM " & _
                  "         (SELECT m2.port_name, m1.plant_name, t1.po_no, t1.material_code, m3.material_name, t1.country_code, m4.country_name, t2.currency_code, t1.price, SUM(t1.quantity) poqty, " & _
                  "          (SELECT SUM(tt2.quantity) FROM tbl_shipping_detail tt2 WHERE tt2.po_no=t1.po_no AND tt2.material_code=t1.material_code) shipqty, " & _
                  "          t2.shipment_period_fr, t2.shipment_period_to, DATE_SUB(NOW(), INTERVAL 10 DAY) last10Day, " & _
                  "          (SELECT MAX(ttt1.est_delivery_dt) FROM tbl_shipping ttt1, tbl_shipping_detail ttt2 WHERE ttt1.shipment_no=ttt2.shipment_no AND ttt2.po_no=t1.po_no GROUP BY ttt2.po_no) lastETD, " & _
                  "          DATE_ADD(NOW(), INTERVAL 5 DAY) next5Day, " & _
                  "          m5.payment_name, payment_days " & _
                  "          FROM tbl_po_detail t1, tbl_po t2, tbm_plant m1, tbm_port m2, tbm_material m3, tbm_country m4, tbm_payment_term m5, tbm_company m6 " & _
                  "          WHERE (t1.po_no = t2.po_no And t2.plant_code = m1.plant_code And t2.port_code = m2.port_code And t1.material_code = m3.material_code And t1.country_code = m4.country_code And t2.payment_code = m5.payment_code And m1.company_code=m6.company_code And t2.status <> 'Closed' AND t2.status <> 'Rejected') " & _
                  "           AND t2.plant_code IN (" & strF & ") " & _
                  "           AND (m6.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "           AND (t2.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "           AND (t2.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "          GROUP BY t1.po_no, t1.material_code, t1.country_code " & _
                  "         ) t1 " & _
                  "         LEFT JOIN tbm_material_origin t2 ON t1.material_code=t2.material_code AND t1.country_code=t2.country_code " & _
                  "         LEFT JOIN tbm_country t3 ON t1.country_code=t3.country_code " & _
                  "         WHERE (IF(t1.shipqty IS NULL,t1.poqty,(t1.poqty-t1.shipqty))) > 0 " & _
                  ") t1 " & _
                  "WHERE ((" & tgl1_2.Checked & " AND DATE_FORMAT(DATE_ADD(t1.etd , INTERVAL t1.lt DAY),'%Y-%m-%d')  >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND DATE_FORMAT(DATE_ADD(t1.etd , INTERVAL t1.lt DAY),'%Y-%m-%d') <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1_2.Checked & ")) " & _
                  "AND ((" & tgl1.Checked & " AND DATE_FORMAT(DATE_ADD(DATE_ADD(t1.etd , INTERVAL t1.lt DAY), INTERVAL t1.payment_days DAY),'%Y-%m-%d') >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND DATE_FORMAT(DATE_ADD(DATE_ADD(t1.etd , INTERVAL t1.lt DAY), INTERVAL t1.payment_days DAY),'%Y-%m-%d') <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) " & _
                  " "

        SQLstr3 = "SELECT 0 shipment_no, '' po_no, 0 po_item, '' bl_no, " & _
                  "t1.Port, t1.Plant, t1.PO, t1.DescriptionOfGoods, t1.Origin, t1.shipment_period_fr ShipmentPeriodFrom, t1.shipment_period_to ShipmentPeriodTo, t1.ShipOnBoard, t1.ETA, t1.PaymentTerm, t1.TTDate, FORMAT(t1.outstdqty,5) Quantity, t1.Currency, t1.UnitPrice, " & _
                  "t1.PrcDUTY, FORMAT(t1.DUTY,2) DUTY, t1.PrcVAT, FORMAT(t1.VAT,2) VAT, t1.PrcPPH22, FORMAT(t1.PPH22,2) PPH22 " & _
                  "FROM (" & SQLstr2 & ") t1 " & _
                  "WHERE (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t1.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t1.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  " "

        SQLstr4 = "SELECT 0 shipment_no, '' po_no, 0 po_item, '' bl_no, " & _
                  " t1.Port, t1.Plant, t1.shipment_period_fr ShipmentPeriodFrom, t1.shipment_period_to ShipmentPeriodTo, MIN(t1.ShipOnBoard) ShipOnBoard, t1.ETA, '' ActualArrival, " & _
                  " MIN(TTDate) TTDate, '' BudgetPIBDate, FORMAT(SUM(t1.DUTY),2) DUTY, FORMAT(SUM(t1.VAT),2) VAT, FORMAT(SUM(t1.PPH22),2) PPH22, FORMAT(SUM(t1.DUTY + t1.VAT + t1.PPH22),2) SumOfTax, " & _
                  " GROUP_CONCAT(DISTINCT t1.PO SEPARATOR ', ') PO, " & _
                  " cast(GROUP_CONCAT(CONCAT(t1.poqty,':',t1.DescriptionOfGoods) SEPARATOR ', ') as char) DetailOfOrder, " & _
                  " cast(GROUP_CONCAT(CONCAT(t1.shipqty,':',t1.DescriptionOfGoods) SEPARATOR ', ') as char) DetailOfShipped, " & _
                  " cast(GROUP_CONCAT(CONCAT(t1.outstdqty,':',t1.DescriptionOfGoods) SEPARATOR ', ') as char) DetailOfOutStanding, 'PO OutStanding' Control " & _
                  "FROM (" & SQLstr2 & ") t1 " & _
                  "WHERE (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t1.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t1.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "GROUP BY t1.Port, t1.Plant, t1.ETA  "

        Select Case cbStatus.Text
            Case "Shipment" : SQLstr = SQLstr1 & " order by ETA, t1.port_name, t1.plant_name "
            Case "Outstanding Shipment" : SQLstr = SQLstr4 & " order by t1.ETA, t1.Port, t1.Plant "
            Case "All" : SQLstr = "Select * from (" & SQLstr1 & " UNION " & SQLstr4 & ") t1 order by t1.Port, t1.Plant, t1.ETA "
            Case "Outstanding Shipment (Detil)" : SQLstr = SQLstr3 & " order by t1.ETA, t1.Port, t1.Plant, t1.PO "
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False

            If cbStatus.Text <> "Outstanding Shipment (Detil)" Then
                DataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(11).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(12).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(13).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(14).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(15).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(5).Frozen = True
            Else
                DataGridView1.Columns(13).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(18).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(20).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(22).DefaultCellStyle.BackColor = Color.Thistle
                DataGridView1.Columns(6).Frozen = True
            End If
        End If

    End Sub

    Private Sub DeptanQ_List()
        Dim dts As DataTable

        '"AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t0.issueddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr = "SELECT t1.ril_no RILNo, t0.Deptan_No DeptanNo, " & _
                 "t1.material_code MaterialCode, m1.material_name DescriptionOfGoods,  m2.country_name Origin, t1.Quantity, t1.quantity_used Used, (t1.Quantity - t1.quantity_used) Difference, t1.unit_code Unit, " & _
                 "t0.submitedDt SubmitedDate, t0.issueddt IssuedDate, m3.company_name Company, m4.port_name DestinationPort, m5.port_name LoadPort, t0.group_code RILGroup, t0.Remark, m0.name CreatedBy " & _
                 "FROM tbl_ril_quota_detail t1, tbl_ril_quota t0, " & _
                 "tbm_users m0, tbm_material m1, tbm_country m2, tbm_company m3, tbm_port m4, tbm_port m5 " & _
                 "WHERE t1.ril_no=t0.ril_no AND t0.status<>'Rejected' " & _
                 "AND t0.createdby=m0.user_ct AND t1.material_code=m1.material_code AND t1.country_code=m2.country_code " & _
                 "AND t0.company_code=m3.company_code AND t0.port_code=m4.port_code AND t0.loadport_code=m5.port_code " & _
                 "AND t0.company_code IN (SELECT DISTINCT company_code FROM tbm_plant WHERE plant_code IN (" & strF & ")) " & _
                 "AND (( " & _
                 "      (m3.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                 "  AND (t0.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                 "  AND (m1.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                 "  AND ((" & tgl1_2.Checked & " AND t0.submitedDt  >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t0.submitedDt <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1_2.Checked & ")) " & _
                 "  AND ((" & tgl1.Checked & " AND t0.issueddt  >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND t0.issueddt <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) " & _
                 "  AND trim('" & txtPONO.Text & "') = '') " & _
                 " OR (trim('" & txtPONO.Text & "') <> '' AND (t1.ril_no LIKE '%" & txtPONO.Text & "%' OR t0.deptan_no LIKE '%" & txtPONO.Text & "%')) " & _
                 " ) "

        Select Case cbStatus.Text
            Case "Closed (Zero Difference)" : SQLstr = SQLstr & " AND (t1.Quantity - t1.quantity_used) <= 0 order by t0.issueddt, t1.ril_no "
            Case "Open" : SQLstr = SQLstr & " AND (t1.Quantity - t1.quantity_used) > 0 order by t0.issueddt, t1.ril_no "
            Case "All" : SQLstr = SQLstr & " order by t0.issueddt, t1.ril_no "
        End Select

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then
            DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(1).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(2).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(3).Frozen = True
        End If

    End Sub

    Private Sub Deptan_List()
        Dim dts As DataTable
        Dim xcbPrdM As String

        If cbPrdM.SelectedIndex < 10 Then
            xcbPrdM = "0" & cbPrdM.SelectedIndex
        Else
            xcbPrdM = cbPrdM.SelectedIndex
        End If

        'PO2 yang belum shipment
        SQLstr1 = "SELECT 0 shipment_no, t1.po po_no, t1.po_item ord_no, '' bl_no, " & _
                  "t1.PO, t1.Company, t1.FinalPlant, t1.FinalPort, t1.DescriptionOfGoods, t1.Origin, t1.Supplier, t1.POQuantity, NULL ShipQuantity, t1.Unit, NULL ETA, NULL ActualArrival, " & _
                  "j1.ril_no RILNo, j1.openingdt CreatedDate, j1.submiteddt SubmitedDate, j1.issueddt IssuedDate, j1.deptan_no DeptanNo, " & _
                  "(SELECT NAME FROM tbm_users WHERE tbm_users.user_ct=j1.createdby) DocCreatedBy " & _
                  "FROM (SELECT t1.po_no PO, t2.po_item, " & _
                  "      t1.company_code, m1.company_name Company, m1.line_bussines, t1.plant_code, m2.plant_name FinalPlant, t1.port_code, m6.port_name FinalPort, " & _
                  "      t2.material_code, m3.material_name DescriptionOfGoods, m3.group_code, t2.country_code, m4.country_name Origin, " & _
                  "      t1.supplier_code, m5.supplier_name Supplier, t2.quantity POQuantity, t2.unit_code Unit " & _
                  "      FROM tbl_po t1, tbl_po_detail t2, " & _
                  "      tbm_company m1, tbm_plant m2, tbm_material m3, tbm_country m4, tbm_supplier m5, tbm_port m6 " & _
                  "      WHERE t1.po_no = t2.po_no " & _
                  "      AND t1.po_no NOT IN (SELECT po_no FROM tbl_shipping_detail) " & _
                  "      AND t1.company_code=m1.company_code AND t1.plant_code=m2.plant_code AND t2.material_code=m3.material_code " & _
                  "      AND t2.country_code=m4.country_code AND t1.supplier_code=m5.supplier_code AND t1.port_code=m6.port_code " & _
                  "      AND t1.plant_code IN (" & strF & ") " & _
                  "      AND (((" & tgl1_2.Checked & " AND FALSE)" & _
                  "            AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t1.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t1.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "            AND (m1.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "            AND (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "            AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "            AND (m3.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "            AND (t1.supplier_code = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "            AND (t2.country_code = '" & TxtExpedition.Text & "' OR '' = '" & TxtExpedition.Text & "') " & _
                  "            AND trim('" & txtPONO.Text & "') = '' ) " & _
                  "           OR trim('" & txtPONO.Text & "') <> '' ) " & _
                  "      ) T1 " & _
                  "LEFT JOIN tbl_ril j1 ON j1.po_no=t1.po AND j1.shipment_no IS NULL "

        'PO2 yang sudah shipment
        SQLstr2 = "SELECT t1.shipment_no, t1.po po_no, t1.po_item ord_no, t1.bl_no, " & _
                  "getpoorder(t1.shipment_no, t1.PO) PO, t1.Company, t1.FinalPlant, t1.FinalPort, t1.DescriptionOfGoods, t1.Origin, t1.Supplier, t1.POQuantity, t1.ShipQuantity, t1.Unit, t1.ETA, t1.ActualArrival, " & _
                  "j1.ril_no RILNo, j1.openingdt CreatedDate, j1.submiteddt SubmitedDate, j1.issueddt IssuedDate, j1.deptan_no DeptanNo, " & _
                  "(SELECT NAME FROM tbm_users WHERE tbm_users.user_ct=j1.createdby) DocCreatedBy " & _
                  "FROM (SELECT t1.shipment_no, t1.bl_no, t1.est_arrival_dt ETA, t1.notice_arrival_dt ActualArrival, t2.po_no PO, t3.po_item, " & _
                  "      m1.company_code, m1.company_name Company, m1.line_bussines, t1.plant_code, m2.plant_name FinalPlant, t1.port_code, m6.port_name FinalPort, " & _
                  "      t2.material_code, m3.material_name DescriptionOfGoods, m3.group_code, t3.country_code, m4.country_name Origin, " & _
                  "      t1.supplier_code, m5.supplier_name Supplier, t3.quantity POQuantity, t2.quantity ShipQuantity, t3.unit_code Unit " & _
                  "      FROM tbl_shipping t1, tbl_shipping_detail t2, tbl_po_detail t3, tbl_po t4, " & _
                  "      tbm_company m1, tbm_plant m2, tbm_material m3, tbm_country m4, tbm_supplier m5, tbm_port m6 " & _
                  "      WHERE t1.shipment_no = t2.shipment_no AND t2.po_no = t3.po_no AND t2.po_item=t3.po_item AND t3.po_no=t4.po_no " & _
                  "      AND t2.po_no NOT IN (SELECT po_no FROM tbl_ril WHERE shipment_no IS NULL AND STATUS <> 'Rejected') " & _
                  "      AND t1.plant_code=m2.plant_code AND m2.company_code=m1.company_code AND t2.material_code=m3.material_code " & _
                  "      AND t3.country_code=m4.country_code AND t1.supplier_code=m5.supplier_code AND t1.port_code=m6.port_code " & _
                  "      AND (((" & tgl1_2.Checked & " AND ((t1.notice_arrival_dt is null AND t1.est_arrival_dt >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.est_arrival_dt<= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') " & _
                  "                         OR (t1.notice_arrival_dt is not null AND t1.notice_arrival_dt >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.notice_arrival_dt <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) " & _
                  "                 OR (not " & tgl1_2.Checked & ")) " & _
                  "             AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(t4.shipment_period_fr,'%Y%m') AND DATE_FORMAT(t4.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "             AND (m1.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "             AND (t1.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "             AND (t1.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "             AND (m3.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "            AND (t1.supplier_code = '" & txtSupplier_Code.Text & "' OR '' = '" & txtSupplier_Code.Text & "') " & _
                  "            AND (t3.country_code = '" & TxtExpedition.Text & "' OR '' = '" & TxtExpedition.Text & "') " & _
                  "             AND trim('" & txtPONO.Text & "') = '' ) " & _
                  "           OR trim('" & txtPONO.Text & "') <> '' ) " & _
                  "      ) t1 " & _
                  "LEFT JOIN tbl_ril j1 ON j1.po_no=t1.po AND j1.shipment_no=t1.shipment_no "

        'untuk filter berdasarkan RIL No atau Deptan No
        Whrstr1 = "WHERE (trim('" & txtPONO.Text & "') = '' " & _
                  "           OR (trim('" & txtPONO.Text & "') <> '' AND (j1.ril_no LIKE '%" & txtPONO.Text & "%' OR j1.deptan_no LIKE '%" & txtPONO.Text & "%'))) "

        Whrstr2 = "AND ((" & tgl1.Checked & " AND j1.submiteddt  >= '" & Format(tgl1.Value, "yyyy-MM-dd") & "' AND j1.submiteddt <= '" & Format(tgl2.Value, "yyyy-MM-dd") & "') OR (Not " & tgl1.Checked & ")) "

        SQLstr = "SELECT * FROM ( " & _
                 SQLstr1 & Whrstr1 & Whrstr2 & _
                 "UNION " & _
                 SQLstr2 & Whrstr1 & Whrstr2 & _
                 ") t1 "

        Select Case cbStatus.Text
            Case "Not Created" : SQLstr = SQLstr & " WHERE RILNo IS NULL "
            Case "Pending Deptan" : SQLstr = SQLstr & " WHERE (RILNo IS NOT NULL AND (DeptanNo IS NULL OR trim(DeptanNo)='')) "
            Case "Issued Deptan" : SQLstr = SQLstr & " WHERE (DeptanNo IS NOT NULL AND trim(DeptanNo)<>'') "
            Case "All" : SQLstr = SQLstr
        End Select
        SQLstr = SQLstr & " ORDER BY t1.ActualArrival DESC, t1.IssuedDate Desc, t1.SubmitedDate Desc, t1.CreatedDate Desc "

        ErrMsg = "Datagrid view Failed"
        DataGridView1.Columns.Clear()
        dts = DBQueryDataTable(SQLstr, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Width = 150

            DataGridView1.Columns(4).Frozen = True
            DataGridView1.Columns(11).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(12).DefaultCellStyle.Format = "N2"
            DataGridView1.Columns(16).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(17).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(18).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(19).DefaultCellStyle.BackColor = Color.Thistle
            DataGridView1.Columns(20).DefaultCellStyle.BackColor = Color.Thistle
        End If

    End Sub

    Private Sub POBL_List()
        Dim dts As DataTable
        Dim FieldList, FieldFill, ValFill As String
        Dim xcbPrdM As String

        FieldFill = cbFill.Text
        ValFill = Trim(txtFill.Text)

        If txtStatus2.Text = "All Item" Then
            'all field
            FieldList = "*"
        Else
            FieldList = ""
            SQLstr = "SELECT FieldName FROM tbm_listdata WHERE listname = '" & txtStatus2.Text & "' order by OrdNo"
            ErrMsg = "Gagal baca ListData..."
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                FieldList = ""
                While MyReader.Read
                    FieldList = FieldList & "," & MyReader.GetString("FieldName")
                End While
                CloseMyReader(MyReader, UserData)
                FieldList = Mid(FieldList, 2, Len(FieldList))
            End If
            'trap error
            If FieldList = "" Then
                MsgBox("Field invalid. Please check items of your template! ", MsgBoxStyle.Critical, "Warning")
                FieldList = "*"
            End If
        End If

        If cbPrdM.SelectedIndex < 10 Then
            xcbPrdM = "0" & cbPrdM.SelectedIndex
        Else
            xcbPrdM = cbPrdM.SelectedIndex
        End If

        '"AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND po.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 6 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT 0 shipment_no, pd.po_no, pd.po_item ord_no, '' bl_no, pd.po_no PO, my.company_name Company, my.line_bussines BussinesLine, " & _
                  "IF(po.shipment_term_code='P', 'Partial Shipment','Whole Shipment') ShipmentTerm, " & _
                  "IF(30-DATE_FORMAT(po.shipment_period_fr,'%d')+1 > DATE_FORMAT(po.shipment_period_to,'%d'), DATE_FORMAT(po.shipment_period_fr,'%M %Y'), DATE_FORMAT(po.shipment_period_to,'%M %Y')) ShipmentPeriod, " & _
                  "po.ipa_no IPA, mm.material_name DescriptionOfGoods, mc.country_name Origin, pd.hs_code HSCode, pd.specification Protein, po.tolerable_delivery Tolerable, pd.quantity POQuantity, 0 ShipQuantity, " & _
                  "pd.unit_code Unit, po.currency_code Currency, FORMAT(pd.price,2) UnitPrice, po.insurance_code Incoterm, FORMAT((pd.quantity * pd.price),2) POAmount, " & _
                  "'' InvoiceNo, '' InvoiceDate, 0 InvoiceAmount, 0 PackedQuantity, '' PackedUnit, '' SGS, '' BL, '' HostBL, " & _
                  "ms.supplier_name Supplier, ms2.produsen_name Produsen, po.contract_no Contract, '' CopyDoc, '' OriginDoc, '' ForwardDoc, cast(if(po.est_delivery_dt is null,'',po.est_delivery_dt) as char) ShipOnBoard, " & _
                  "'' ETA, '' ActualArrival, 0 FreeTime, '' DeliveryDate, '' ClearanceDate, '' Container, '' FinalPlant, '' FinalPort, '' LoadPort, '' ShippingLine, '' Vessel, " & _
                  "FORMAT(0,2) TaxRate, FORMAT(0,2) BeaMasuk, FORMAT(0,2) VAT, FORMAT(0,2) PPH22, FORMAT(0,2) PIUD, '' TdTerimaPajak, '' InsuranceNo, FORMAT(0,2) InsuranceAmount, '' TTDate, '' DueDate, '' PIBNo, '' PIBDate, '' AJU, '' SPPBDate, '' SPPBNo, mu.name CreatedBy " & _
                  "FROM tbl_po_detail pd, tbl_po po, " & _
                  "tbm_company my, tbm_material mm, tbm_material_group gm, tbm_country mc, tbm_supplier ms, tbm_produsen ms2, tbm_users mu " & _
                  "WHERE (pd.po_no = po.po_no) " & _
                  "AND po.company_code=my.company_code " & _
                  "AND pd.material_code=mm.material_code AND mm.group_code=gm.group_code AND pd.country_code=mc.country_code AND po.supplier_code=ms.supplier_code " & _
                  "AND po.produsen_code=ms2.produsen_code AND po.createdby = mu.user_ct " & _
                  "AND po.po_no NOT IN (SELECT po_no FROM tbl_shipping_detail) " & _
                  "AND po.plant_code IN (" & strF & ") " & _
                  "AND ( " & _
                  "     (    (po.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                  "      AND (po.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                  "      AND (gm.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                  "      AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(po.shipment_period_fr,'%Y%m') AND DATE_FORMAT(po.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                  "      AND (my.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "      AND (mu.user_ct = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                  "      AND trim('" & txtPONO.Text & "') = '') " & _
                  "    OR ('" & FieldFill & "' <> '' AND '" & ValFill & "' <> '') " & _
                  "    ) AND 1=1 " & _
                  "ORDER BY po.shipment_period_fr DESC, pd.po_no "

        '"  AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND ss.createddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 6 MONTH),'%Y-%m-%d'))) " & _
        '"      AND (DATE_FORMAT(po.shipment_period_fr,'%M %Y') = '" & cbPrdM.Text & " " & cbPrdY.Text & "' OR '' = '" & cbPrdY.Text & "') " & _

        SQLstr2 = "SELECT sd.shipment_no, sd.po_no, sd.po_item ord_no, ss.bl_no, getpoorder(sd.shipment_no, TRIM(sd.po_no)) PO, " & _
                 "  mc.company_name Company, mc.line_bussines BussinesLine, " & _
                 "  IF(po.shipment_term_code='P', 'Partial Shipment','Whole Shipment') ShipmentTerm, " & _
                 "  IF(30-DATE_FORMAT(po.shipment_period_fr,'%d')+1 > DATE_FORMAT(po.shipment_period_to,'%d'), DATE_FORMAT(po.shipment_period_fr,'%M %Y'), DATE_FORMAT(po.shipment_period_to,'%M %Y')) ShipmentPeriod, " & _
                 "  po.IPA_no IPA, " & _
                 "  mm.material_name DescriptionOfGoods, my.country_name Origin, pd.hs_code HSCode, pd.specification Protein, po.tolerable_delivery Tolerable, " & _
                 "  pd.Quantity POQuantity, sd.Quantity ShipQuantity, pd.unit_code Unit," & _
                 "  po.currency_code Currency, FORMAT(pd.price,2) UnitPrice, po.insurance_code Incoterm, FORMAT((pd.quantity * pd.price),2) POAmount, sv.invoice_no InvoiceNo, cast(sv.invoice_dt as char) InvoiceDate, (sv.invoice_amount-sv.invoice_penalty) InvoiceAmount, " & _
                 "  sd.pack_quantity PackedQuantity, ma.pack_name PackedUnit, sd.specification SGS, " & _
                 "  ss.bl_no BL, ss.HostBL HostBL, ms.supplier_name Supplier, mn.produsen_name Produsen, po.contract_no Contract, " & _
                 "  cast(if(ss.received_copydoc_dt is null,'',ss.received_copydoc_dt) as char) CopyDoc, cast(if(ss.received_doc_dt is null,'',ss.received_doc_dt) as char) OriginDoc, cast(if(ss.forward_doc_dt is null,'',ss.forward_doc_dt) as char) ForwardDoc, " & _
                 "  cast(if(ss.est_delivery_dt is null,'',ss.est_delivery_dt) as char) ShipOnBoard, cast(if(ss.est_arrival_dt is null,'',ss.est_arrival_dt) as char) ETA, cast(if(ss.Notice_arrival_dt is null,'',ss.Notice_arrival_dt) as char) ActualArrival, " & _
                 "  if(ss.free_time is null,0,ss.free_time) FreeTime, cast(IF(ss.clr_dt IS NULL, if(ss.est_clr_dt is null,'',ss.est_clr_dt), ss.clr_dt) as char) DeliveryDate, " & _
                 "  cast(IF(ss.bapb_dt IS NULL, if(ss.est_bapb_dt is null,'',ss.est_bapb_dt), ss.bapb_dt) as char) ClearanceDate, if(ss.total_container is null,'',ss.total_container) Container, " & _
                 "  mp.plant_name FinalPlant, mo3.port_name FinalPort, mo2.port_name LoadPort, ml.line_name ShippingLine, ss.Vessel Vessel, " & _
                 "  FORMAT(ss.kurs_pajak,2) TaxRate, FORMAT(ss.bea_masuk,2) BeaMasuk, FORMAT(ss.Vat,2) VAT, FORMAT(ss.pph21,2) PPH22, FORMAT(ss.PIUD,2) PIUD, cast(if(ss.verified2dt is null,'',ss.verified2dt) as char) TdTerimaPajak, ss.insurance_no InsuranceNo, FORMAT(ss.insurance_amount,2) InsuranceAmount, " & _
                 "  cast(if(ss.tt_dt is null,'',ss.tt_dt) as char) TTDate, cast(if(ss.due_dt is null,'',ss.due_dt) as char) DueDate, ss.pib_no PIBNo, cast(if(ss.pib_dt is null,'',ss.pib_dt) as char) PIBDate, ss.aju_no AJU, cast(if(ss.sppb_dt is null,'',ss.sppb_dt) as char) SPPBDate, " & _
                 "  ss.sppb_no SPPBNo, mu.name CreatedBy " & _
                 "  FROM tbl_shipping_detail sd, tbl_shipping ss, tbl_po_detail pd, tbl_po po, tbl_shipping_invoice sv, " & _
                 "  tbm_material mm, tbm_material_group gm, tbm_packing ma, tbm_supplier ms, tbm_plant mp, tbm_port mo2, tbm_lines ml, tbm_users mu, " & _
                 "  tbm_country my, tbm_company mc, tbm_port mo3, tbm_produsen mn " & _
                 "  WHERE(sd.shipment_no = ss.shipment_no) " & _
                 "  AND sd.po_no=pd.po_no AND sd.po_item=pd.po_item " & _
                 "  AND sd.po_no=po.po_no " & _
                 "  AND sd.shipment_no=sv.shipment_no AND sd.po_no=sv.po_no AND sd.po_item=sv.ord_no " & _
                 "  AND sd.material_code=mm.material_code AND mm.group_code=gm.group_code AND sd.pack_code=ma.pack_code " & _
                 "  AND ss.supplier_code=ms.supplier_code AND ss.plant_code=mp.plant_code AND ss.loadport_code=mo2.port_code " & _
                 "  AND ss.shipping_line=ml.line_code AND ss.createdby=mu.user_ct AND pd.country_code=my.country_code " & _
                 "  AND po.company_code=mc.company_code AND ss.port_code=mo3.port_code " & _
                 "  AND po.produsen_code=mn.produsen_code " & _
                 "  AND ss.plant_code IN (" & strF & ") " & _
                 "  AND ( " & _
                 "      (((" & tgl1_2.Checked & " AND (ss.NOTICE_ARRIVAL_DT is null AND (cast(ss.EST_ARRIVAL_DT as char(10)) >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND cast(ss.EST_ARRIVAL_DT as char(10)) <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') " & _
                 "          OR (ss.NOTICE_ARRIVAL_DT is not null AND cast(ss.NOTICE_ARRIVAL_DT as char(10)) >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND cast(ss.NOTICE_ARRIVAL_DT as char(10)) <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "'))) " & _
                 "        OR (not " & tgl1_2.Checked & " )) " & _
                 "        AND (ss.plant_code = '" & txtPlant_Code.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                 "        AND (ss.port_code = '" & txtPort_Code.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                 "        AND (gm.group_code = '" & txtGrpMat.Text & "' OR '' = '" & txtGrpMat.Text & "') " & _
                 "        AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(po.shipment_period_fr,'%Y%m') AND DATE_FORMAT(po.shipment_period_to,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                 "        AND (mc.line_bussines  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                 "        AND (mu.user_ct = '" & userct.Text & "' OR '' = '" & userct.Text & "') " & _
                 "        AND trim('" & txtPONO.Text & "') = '') " & _
                 "      OR ('" & FieldFill & "' <> '' AND '" & ValFill & "' <> '') " & _
                 "      ) AND 2=2 " & _
                 "  ORDER BY ss.Notice_arrival_dt DESC, ss.est_arrival_dt DESC "

        '"        AND (DATE_FORMAT(po.shipment_period_fr,'%M %Y') = '" & cbPrdM.Text & " " & cbPrdY.Text & "' OR '' = '" & cbPrdY.Text & "') " & _

        'PO yang sudah ada Shipment (BL Documents)
        SQLstr3 = "SELECT t1.*, cast(if(d1.BudgetOpeningLC is null,'',d1.BudgetOpeningLC) as char) BudgetOpeningLC, cast(if(d0.LCNo is null,'',d0.LCNo) as char) LCNo, " & _
                     "cast(IF(d2.ReqImportLisence_po is null, if(d4.ReqImportLisence_bl is null,'',d4.ReqImportLisence_bl), d2.ReqImportLisence_po) as char) ReqImportLisence, " & _
                     "cast(IF(d20.RILNo_po is null, if(d21.RILNo_bl is null,'',d21.RILNo_bl), d20.RILNo_po) as char) RILNo, " & _
                     "cast(IF(d22.Deptan_po is null, if(d23.Deptan_bl is null,'',d23.Deptan_bl), d22.Deptan_po) as char) IssuedDeptan, " & _
                     "cast(IF(d17.DeptanNo_po is null, if(d16.DeptanNo_bl is null,'',d16.DeptanNo_bl), d17.DeptanNo_po) as char) DeptanNo, " & _
                     "cast(IF(d3.ShippingInstruction_po is null, if(d5.ShippingInstruction_bl is null,'',d5.ShippingInstruction_bl), d3.ShippingInstruction_po) as char) ShippingInstruction, " & _
                     "cast(if(d6.BugdetRemitance is null,'',d6.BugdetRemitance) as char) BugdetRemitance, cast(if(d7.PaymentVoucher is null,'',d7.PaymentVoucher) as char) PaymentVoucher, cast(if(d8.VoucherGiro is null,'',d8.VoucherGiro) as char) VoucherGiro, cast(if(d9.CoverLetter is null,'',d9.CoverLetter) as char) CoverLetter, " & _
                     "cast(if(d12.MCI is null,'',d12.MCI) as char) MCI, cast(if(d13.BudgetTT is null,'',d13.BudgetTT) as char) BudgetTT, cast(if(d14.BudgetPIB is null,'',d14.BudgetPIB) as char) BudgetPIB, cast(if(d15.BudgetCAD is null,'',d15.BudgetCAD) as char) BudgetCAD, " & _
                     "cast(if(d10.Inklaring is null,'',d10.Inklaring) as char) Inklaring, cast(if(d11.CostSlip is null,'',d11.CostSlip) as char) CostSlip, FORMAT(if(d18.effective_kurs is null,0,d18.effective_kurs),2) CostSlipRate, cast(if(d11.perKGs is null,'',FORMAT(d11.perKGs,2)) as char) perKGs, cast(if(d11.TdTerimaCostSlip is null,'',d11.TdTerimaCostSlip) as char) TdTerimaCostSlip FROM " & _
                     "(" & SQLstr2 & ") t1 " & _
                     "LEFT JOIN (SELECT po_no, MIN(OpeningDt) BudgetOpeningLC FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, GROUP_CONCAT(lc_no SEPARATOR ',') LCNo FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d0 ON d0.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ReqImportLisence_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d2 ON d2.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ShippingInstruction_po FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d3 ON d3.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ReqImportLisence_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d4 ON d4.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) ShippingInstruction_bl FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d5 ON d5.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BugdetRemitance FROM tbl_remitance WHERE STATUS<>'Rejected' GROUP BY shipment_no) d6 ON d6.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) PaymentVoucher FROM tbl_shipping_doc WHERE findoc_type='PV' AND findoc_status<>'Rejected' GROUP BY shipment_no) d7 ON d7.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) VoucherGiro FROM tbl_shipping_doc WHERE findoc_type='VG' AND findoc_status<>'Rejected' GROUP BY shipment_no) d8 ON d8.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CoverLetter FROM tbl_shipping_doc WHERE findoc_type='CL' AND findoc_status<>'Rejected' GROUP BY shipment_no) d9 ON d9.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) Inklaring FROM tbl_shipping_doc WHERE findoc_type='CC' AND findoc_status<>'Rejected' GROUP BY shipment_no) d10 ON d10.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(findoc_printeddt) CostSlip, MIN(FINDOC_VALAMT) perKGs, CAST(MIN(findoc_finappdt) AS CHAR) TdTerimaCostSlip FROM tbl_shipping_doc WHERE findoc_type='CS' AND findoc_status<>'Rejected' GROUP BY shipment_no) d11 ON d11.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) MCI FROM tbl_mci WHERE STATUS<>'Rejected' GROUP BY shipment_no) d12 ON d12.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetTT FROM tbl_budgets WHERE type_code='TT' AND STATUS<>'Rejected' GROUP BY shipment_no) d13 ON d13.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetPIB FROM tbl_budgets WHERE type_code='BP' AND STATUS<>'Rejected' GROUP BY shipment_no) d14 ON d14.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(OpeningDt) BudgetCAD FROM tbl_budgets WHERE type_code='CA' AND STATUS<>'Rejected' GROUP BY shipment_no) d15 ON d15.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(deptan_no) DeptanNo_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL AND deptan_no <> '' GROUP BY shipment_no) d16 ON d16.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(deptan_no) DeptanNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL AND deptan_no <> '' GROUP BY po_no) d17 ON d17.po_no=t1.po_no " & _
                     "LEFT JOIN tbm_kurs d18 ON d18.currency_code=t1.Currency AND d18.effective_date=t1.ShipOnBoard " & _
                     "LEFT JOIN (SELECT po_no, MIN(ril_no) RILNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d20 ON d20.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(ril_no) RILNo_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d21 ON d21.shipment_no=t1.shipment_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(IssuedDt) Deptan_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d22 ON d22.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT shipment_no, MIN(IssuedDt) Deptan_bl FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NOT NULL GROUP BY shipment_no) d23 ON d23.shipment_no=t1.shipment_no " & _
                     " "

        'PO yang belum ada Shipment (BL Documents)
        SQLstr4 = "SELECT t1.*, cast(if(d1.BudgetOpeningLC is null,'',d1.BudgetOpeningLC) as char) BudgetOpeningLC, cast(if(d0.LCNo is null,'',d0.LCNo) as char) LCNo, " & _
                     "cast(if(d2.ReqImportLisence_po is null,'',d2.ReqImportLisence_po) as char) ReqImportLisence, " & _
                     "cast(if(d20.RILNo_po is null,'',d20.RILNo_po) as char) RILNo, cast(if(d22.Deptan_po is null,'',d22.Deptan_po) as char) Deptan, cast(if(d17.DeptanNo_po is null,'',d17.DeptanNo_po) as char) DeptanNo, " & _
                     "cast(if(d3.ShippingInstruction_po is null,'',d3.ShippingInstruction_po) as char) ShippingInstruction, " & _
                     "'' BugdetRemitance, '' PaymentVoucher, '' VoucherGiro, '' CoverLetter, '' MCI, '' BudgetTT, '' BudgetPIB, '' BudgetCAD, " & _
                     "'' Inklaring, '' CostSlip, FORMAT(0,2) CostSlipRate, FORMAT(0,2) perKGs, '' TdTerimaCostSlip FROM " & _
                     "(" & SQLstr1 & ") t1 " & _
                     "LEFT JOIN (SELECT po_no, MIN(OpeningDt) BudgetOpeningLC FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d1 ON d1.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, GROUP_CONCAT(lc_no SEPARATOR ',') LCNo FROM tbl_budget WHERE type_code='BOLC' AND STATUS<>'Rejected' GROUP BY po_no) d0 ON d0.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ReqImportLisence_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d2 ON d2.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(OpeningDt) ShippingInstruction_po FROM tbl_si WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d3 ON d3.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, GROUP_CONCAT(deptan_no SEPARATOR ',') DeptanNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL AND deptan_no <> '' GROUP BY po_no) d17 ON d17.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, GROUP_CONCAT(ril_no SEPARATOR ',') RILNo_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d20 ON d20.po_no=t1.po_no " & _
                     "LEFT JOIN (SELECT po_no, MIN(IssuedDt) Deptan_po FROM tbl_ril WHERE STATUS<>'Rejected' AND shipment_no IS NULL GROUP BY po_no) d22 ON d22.po_no=t1.po_no " & _
                     " "

        Whrstr1 = "((" & tgl1_2.Checked & " AND " & _
                  "  (  (ActualArrival='' AND ETA >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND ETA <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "') " & _
                  "  OR (ActualArrival<>'' AND ActualArrival >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND ActualArrival <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) " & _
                  " ) " & _
                  " OR (not " & tgl1_2.Checked & " )) "

        'Data not realtime. tmp_generaldata adalah table penampung yg di refresh setiap beberapa jam dengan backgroud program di server.
        SQLstr6 = "SELECT shipment_no, po_no,ord_no,bl_no,PO,Company,BussinesLine,ShipmentTerm,ShipmentPeriodFrom, ShipmentPeriodTo ,IPA " & _
                     ",DescriptionOfGoods,Origin,HSCode,Protein,Tolerable,POQuantity,ShipQuantity,Unit,Currency,UnitPrice,Incoterm ,POAmount,InvoiceNo " & _
                     ",InvoiceDate,InvoiceAmount,PackedQuantity,PackedUnit,SGS,BL,HostBL,Supplier,Produsen ,Contract ,CopyDoc ,OriginDoc ,ForwardDoc " & _
                     ",ShipOnBoard,ETA,ActualArrival,FreeTime,DeliveryDate,ClearanceDate,Container,FinalPlant,FinalPort,LoadPort,ShippingLine,Vessel " & _
                     ",TaxRate,BeaMasuk,VAT,PPH22 ,PIUD,TdTerimaPajak,InsuranceNo,InsuranceAmount,TTDate,DueDate,PIBNo,PIBDate,AJU,SPPBDate,SPPBNo " & _
                     ",CreatedBy,BudgetOpeningLC,LCNo,ReqImportLisence,RILNo,Deptan,DeptanNo,ShippingInstruction,BugdetRemitance,PaymentVoucher,VoucherGiro,CoverLetter " & _
                     ",MCI,BudgetTT,BudgetPIB,BudgetCAD,Inklaring,CostSlip,CostSlipRate,perKGs,TdTerimaCostSlip " & _
                     "FROM tmp_generaldata " & _
                     "WHERE IF(FinalPlant='',DestinationPlant,FinalPlant) IN (SELECT TRIM(plant_name) FROM tbm_plant WHERE company_code IN (SELECT company_code FROM tbm_users_company WHERE user_ct=" & UserData.UserCT & ")) " & _
                     "AND (  (  (" & Whrstr1 & ") " & _
                     "      AND (IF(FinalPlant='', DestinationPlant, FinalPlant) = '" & lblPlant_Name.Text & "' OR '' = '" & txtPlant_Code.Text & "') " & _
                     "      AND (IF(FinalPort='', DestinationPort, FinalPort) = '" & lblPort_Name.Text & "' OR '' = '" & txtPort_Code.Text & "') " & _
                     "      AND (trim(DescriptionOfGoods) IN (SELECT TRIM(material_name) FROM tbm_material WHERE group_code = '" & txtGrpMat.Text & "') OR ('' = '" & txtGrpMat.Text & "')) " & _
                     "      AND (('" & cbPrdY.Text & xcbPrdM & "' BETWEEN DATE_FORMAT(ShipmentPeriodFrom,'%Y%m') AND DATE_FORMAT(ShipmentPeriodTo,'%Y%m')) OR '' = '" & cbPrdY.Text & "') " & _
                     "      AND (BussinesLine  = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                     "      AND (CreatedBy = '" & txtCreatedby.Text & "' OR '' = '" & txtCreatedby.Text & "') " & _
                     "      AND trim('" & txtPONO.Text & "') = '') " & _
                     "    OR ('" & FieldFill & "' <> '' AND '" & ValFill & "' <> '')) " & _
                     "ORDER BY ActualArrival DESC, ETA DESC, ShipmentPeriod DESC, po_no"

        '"      AND (ShipmentPeriod = '" & cbPrdM.Text & " " & cbPrdY.Text & "' OR '' = '" & cbPrdY.Text & "') " & _

        If cbSource.SelectedIndex = 0 Then 'untuk query data temporary yg di backgroud job di server menggabungkan query SQLstr3 + SQLstr4 di atas agar lebih cepat
            Select Case cbStatus.Text
                Case "All PO" : SQLstr = SQLstr6
                Case "Shipment" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0"
                Case "Pending Shipment" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no = 0 AND ShipOnBoard = ''"
                Case "Pending Copy Doc" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no = 0"
                Case "Pending Origin Doc" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0 AND OriginDoc = ''"
                Case "Pending Actual Arrival" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0 AND ActualArrival = ''"
                Case "Pending Shipment by ETA" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0 AND ((IF(ActualArrival='',ETA,ActualArrival) >= DATE_FORMAT(NOW(),'%Y-%m-%d')) OR (ETA = ''))"
                Case "Pending Req.Import Lisence" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE ReqImportLisence = ''"
                Case "Pending Deptan" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE ReqImportLisence <> '' AND DeptanNo = ''"
                Case "Pending Insurance" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0 AND po_no in (SELECT po_no FROM tbl_po WHERE insurance_code = 'CNF') AND InsuranceAmount = ''"
                Case "Pending Payment (DueDate)" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0 AND DueDate = ''"
                Case "Pending Tax (B-PIB)" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE shipment_no <> 0 AND BudgetPIB = ''"
            End Select
        Else
            Select Case cbStatus.Text
                Case "All PO" : SQLstr = SQLstr6
                Case "Shipment" : SQLstr = SQLstr3
                Case "Pending Shipment" : SQLstr = Replace(SQLstr4, "1=1", "po.est_delivery_dt is null")
                Case "Pending Copy Doc" : SQLstr = SQLstr4
                Case "Pending Origin Doc" : SQLstr = Replace(SQLstr3, "2=2", "ss.received_doc_dt is null")
                Case "Pending Actual Arrival" : SQLstr = Replace(SQLstr3, "2=2", "ss.Notice_arrival_dt is null")
                Case "Pending Shipment by ETA" : SQLstr = Replace(SQLstr3, "2=2", "(IF(ss.Notice_arrival_dt IS NULL, ss.est_arrival_dt, ss.Notice_arrival_dt) >= DATE_FORMAT(NOW(),'%Y-%m-%d') OR ss.est_arrival_dt IS NULL)")
                Case "Pending Req.Import Lisence" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE ReqImportLisence = ''"
                Case "Pending Deptan" : SQLstr = "Select * from (" & SQLstr6 & ") t1 WHERE ReqImportLisence <> '' AND DeptanNo = ''"
                Case "Pending Insurance" : SQLstr = Replace(SQLstr3, "2=2", "po.insurance_code = 'CNF' AND ss.insurance_amount = 0")
                Case "Pending Payment (DueDate)" : SQLstr = Replace(SQLstr3, "2=2", "ss.due_dt is null")
                Case "Pending Tax (B-PIB)" : SQLstr = Replace(SQLstr3, "2=2", "sd.shipment_no not in (SELECT shipment_no FROM tbl_budgets WHERE type_code='BP')")
            End Select
        End If

        If FieldFill = "" Then
            SQLstrA = "Select " & FieldList & " From (" & SQLstr & ") t1 "
        Else
            SQLstrA = "Select " & FieldList & " From (" & SQLstr & ") t1 " & _
                      " Where " & FieldFill & " LIKE '%" & ValFill & "%'"
        End If

        ErrMsg = "Datagrid view Failed"
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        Application.DoEvents()
        dts = DBQueryDataTable(SQLstrA, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then

            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Visible = False
            DataGridView1.Columns(4).Width = 150

            DataGridView1.Columns(4).Frozen = True
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        closeForm(sender, e, Me)
        'If v_type = "DP" Then
        '    If TempTableName <> "" Then
        '        DBQueryUpdate("DROP TABLE IF EXISTS `" & TempTableName & "`", MyConn1, False, _
        '                     "Gagal Hapus table temporary.", UserData)
        '    End If
        'End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim brs As Integer

        brs = DataGridView1.CurrentCell.RowIndex

        txtShipment.Text = DataGridView1.Item(0, brs).Value.ToString
        txtPO.Text = DataGridView1.Item(1, brs).Value.ToString
        txtBL.Text = DataGridView1.Item(3, brs).Value.ToString
    End Sub

    Private Sub btnSearchPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPort.Click
        PilihanDlg.Text = "Select Port Code"
        PilihanDlg.LblKey1.Text = "Port Code"
        PilihanDlg.LblKey2.Text = "Port Name"
        PilihanDlg.SQLGrid = "SELECT PORT_CODE as PortCode, PORT_NAME as PortName FROM tbm_port WHERE country_code='ID'"
        PilihanDlg.SQLFilter = "SELECT PORT_CODE as PortCode, PORT_NAME as PortName FROM tbm_port " & _
                               "WHERE country_code='ID' AND PORT_CODE LIKE 'FilterData1%' AND " & _
                               "PORT_NAME LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPort_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPort_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnUserPur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUserPur.Click
        PilihanDlg.Text = "Select User"
        PilihanDlg.LblKey1.Text = "User Name"
        PilihanDlg.LblKey2.Visible = False
        PilihanDlg.TxtKey2.Visible = False
        PilihanDlg.SQLGrid = "Select distinct tu.user_ct as UserCT,tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                             "tu.user_ct = tum.user_ct where tum.modul_code like 'SD%' "
        PilihanDlg.SQLFilter = "Select distinct tu.user_ct as UserCT,tu.Name as UserName from tbm_users as tu inner join tbm_users_modul as tum on " & _
                               "tu.user_ct = tum.user_ct where tum.modul_code like 'SD%' " & _
                               "and tu.name LIKE 'FilterData1%' "
        PilihanDlg.Tables = "tbm_users as tu inner join tbm_users_modul as tum on tu.user_ct = tum.user_ct"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCreatedby.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            userct.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub btnSearchPlant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchPlant.Click

        PilihanDlg.Text = "Select Plant Code"
        PilihanDlg.LblKey1.Text = "Plant Code"
        PilihanDlg.LblKey2.Text = "Plant Name"
        PilihanDlg.SQLGrid = "SELECT PLANT_CODE as PlantCode, PLANT_NAME as PlantName FROM tbm_plant WHERE PLANT_CODE IN (" & strF & ")"
        PilihanDlg.SQLFilter = "SELECT PLANT_CODE as PlantCode, PLANT_NAME as PlantName FROM tbm_plant " & _
                               "WHERE PLANT_CODE IN (" & strF & ") AND PLANT_CODE LIKE 'FilterData1%' AND PLANT_NAME LIKE 'FilterData2%'"
        PilihanDlg.Tables = "tbm_port"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtPlant_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblPlant_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
        End If
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        RefreshScreen(v_type)
        btnOpen.Enabled = (v_type <> "CO") And (v_type <> "TF")

        btnSave.Enabled = True
        btnFile.Enabled = True
        If v_type = "GD" Then
            btnList.Visible = True
            lblList.Visible = True
        ElseIf v_type = "DP" Then
            BtnPrint.Enabled = True
        End If

    End Sub

    Private Sub Display_FD()
        If f_GetAccess("FD-L") Then
            Dim f As New FrmBL
            Dim cnt, a As Integer
            Dim nemu As Boolean = False

            f.Text = "Funds & Finance"

            cnt = f.ToolStrip1.Items.Count - 1
            For a = 0 To cnt
                If f.ToolStrip1.Items(a).Name = "btnBPUM" Then nemu = True
                If f.ToolStrip1.Items(a).Name = "btnBC" Then nemu = False
                f.ToolStrip1.Items(a).Visible = nemu
            Next
            f.btnClose.Visible = True
            f.btnClose.Enabled = True
            f.ToolStripSeparator1.Visible = True
            f.grid4.AllowUserToAddRows = False
            f.GridInv.AllowUserToAddRows = False
            f.IsMdiContainer = True
            f.Show()
            DisabledScreen(f)
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub Display_CC()
        If f_GetAccess("SD-L") Then
            Dim f As New FrmBL
            Dim cnt, a As Integer
            Dim nemu As Boolean = False

            f.Text = "Custom Clearance"

            cnt = f.ToolStrip1.Items.Count - 1
            For a = 0 To cnt
                If f.ToolStrip1.Items(a).Name = "btnBC" And nemu = False Then nemu = True
                f.ToolStrip1.Items(a).Visible = nemu
            Next
            f.btnClose.Visible = True
            f.btnClose.Enabled = True
            f.ToolStripSeparator1.Visible = True
            f.grid2.AllowUserToAddRows = False
            f.grid3.AllowUserToAddRows = False
            f.grid4.AllowUserToAddRows = False
            f.Show()
            DisabledScreen(f)
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub Display_DP()
        If f_GetAccess("FD-L") Then
            Dim f As New FrmBL
            Dim cnt, a As Integer
            Dim nemu As Boolean = False

            f.Text = "Funds & Finance"

            cnt = f.ToolStrip1.Items.Count - 1
            For a = 0 To cnt
                If f.ToolStrip1.Items(a).Name = "btnBPUM" Then nemu = True
                If f.ToolStrip1.Items(a).Name = "btnBC" Then nemu = False
                f.ToolStrip1.Items(a).Visible = nemu
            Next
            f.btnClose.Visible = True
            f.btnClose.Enabled = True
            f.ToolStripSeparator1.Visible = True
            f.grid4.AllowUserToAddRows = False
            f.GridInv.AllowUserToAddRows = False
            f.IsMdiContainer = True
            f.Show()
            DisabledScreen(f)
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub Display_SD()
        If txtBL.Text <> "" Then
            If f_GetAccess("SD-L") Then
                Dim f As New FrmBL
                Dim cnt, a As Integer
                Dim nemu As Boolean = False

                f.Text = "Post Import Document"

                cnt = f.ToolStrip1.Items.Count - 1
                For a = 0 To cnt
                    If f.ToolStrip1.Items(a).Name = "btnRIL" Then nemu = True
                    If f.ToolStrip1.Items(a).Name = "btnBPUM" Then nemu = False
                    f.ToolStrip1.Items(a).Visible = nemu
                Next
                f.btnClose.Visible = True
                f.btnClose.Enabled = True
                f.ToolStripSeparator1.Visible = True
                f.grid4.AllowUserToAddRows = False
                f.GridInv.AllowUserToAddRows = False
                f.IsMdiContainer = True
                f.Show()
                DisabledScreen(f)
            Else
                f_msgbox_otorisasi("")
            End If
        Else
            If f_GetAccess("PO-L") Then
                Dim f As New FrmDOC_Import
                f.Show()
                f.txtPO_NO.Text = txtPO.Text
                f.txtPO_NO.ReadOnly = True
                f.btnPO.Enabled = False
            Else
                f_msgbox_otorisasi("")
            End If
        End If
    End Sub

    Private Sub Display_DS()
        If txtBL.Text <> "" Then
            If f_GetAccess("SD-L") Then
                Dim f As New FrmBL
                Dim cnt, a As Integer
                Dim nemu As Boolean = False

                f.Text = "Post Import Document"

                cnt = f.ToolStrip1.Items.Count - 1
                For a = 0 To cnt
                    If f.ToolStrip1.Items(a).Name = "btnRIL" Then nemu = True
                    If f.ToolStrip1.Items(a).Name = "btnBPUM" Then nemu = False
                    f.ToolStrip1.Items(a).Visible = nemu
                Next
                f.btnClose.Visible = True
                f.btnClose.Enabled = True
                f.ToolStripSeparator1.Visible = True
                f.grid4.AllowUserToAddRows = False
                f.GridInv.AllowUserToAddRows = False
                f.IsMdiContainer = True
                f.Show()
                DisabledScreen(f)
            Else
                f_msgbox_otorisasi("")
            End If
        Else
            If f_GetAccess("PO-L") Then
                Dim f As New FrmDOC_Import
                f.Show()
                f.txtPO_NO.Text = txtPO.Text
                f.txtPO_NO.ReadOnly = True
                f.btnPO.Enabled = False
            Else
                f_msgbox_otorisasi("")
            End If
        End If
    End Sub

    Private Sub Display_GD()
        If txtBL.Text <> "" Then
            If f_GetAccess("SD-L") Then
                Dim f As New FrmBL
                Dim cnt, a As Integer
                Dim nemu As Boolean = False

                f.Text = "Post Import Document"

                cnt = f.ToolStrip1.Items.Count - 1
                For a = 0 To cnt
                    If f.ToolStrip1.Items(a).Name = "btnRIL" Then nemu = True
                    If f.ToolStrip1.Items(a).Name = "btnBPUM" Then nemu = False
                    f.ToolStrip1.Items(a).Visible = nemu
                Next
                f.btnClose.Visible = True
                f.btnClose.Enabled = True
                f.ToolStripSeparator1.Visible = True
                f.grid4.AllowUserToAddRows = False
                f.GridInv.AllowUserToAddRows = False
                f.IsMdiContainer = True
                f.Show()
                DisabledScreen(f)
            Else
                f_msgbox_otorisasi("")
            End If
        Else
            If f_GetAccess("PO-L") Then
                Dim f As New FrmDOC_Import
                f.Show()
                f.txtPO_NO.Text = txtPO.Text
                f.txtPO_NO.ReadOnly = True
                f.btnPO.Enabled = False
            Else
                f_msgbox_otorisasi("")
            End If
        End If
    End Sub

    Private Sub DisabledScreen(ByVal frm As FrmBL)
        With frm
            .blno.Text = txtBL.Text
            .hostbl.Focus()
            .blno.ReadOnly = True
            .hostbl.ReadOnly = True
            .suppl.ReadOnly = True
            .btnSearchSupplier.Visible = False
            .packlist.ReadOnly = True
            .dtCopy.Enabled = False
            .dtETD.Enabled = False
            .dtETA.Enabled = False
            .DestPlant.ReadOnly = True
            .DestPort.ReadOnly = True
            .ajuNo.ReadOnly = True
            .PIBNo.ReadOnly = True
            .SPPBNo.ReadOnly = True
            .Button9.Visible = False
            .Button5.Enabled = False
            .Button3.Visible = False
            .Button6.Visible = False
            .Button7.Visible = False
            .bankname.ReadOnly = True
            .BeaMasuk.ReadOnly = True
            .vat.ReadOnly = True
            .pph.ReadOnly = True
            .piud.ReadOnly = True
            .KursPajak.ReadOnly = True
            .dtVerifySuppDoc.Enabled = False
            .dtRecBeaByAcc.Enabled = False
            .dtPIB.Enabled = False
            .dtSPPB.Enabled = False
            .dtAJU.Enabled = False
            .dtRec.Enabled = False
            .dtArrival.Enabled = False
            .dtOB.Enabled = False
            .dtEstDelivery.Enabled = False
            .dtDelivery.Enabled = False
            .dtEstClearance.Enabled = False
            .dtClearance.Enabled = False
            .dtForward.Enabled = False
            .dtValue.Enabled = False
            .dtTT.Enabled = False
            .InsNo.ReadOnly = True
            .InsAmt.ReadOnly = True
            .btnClearD.Enabled = False
            .btnSearch.Visible = False
            .LoadPort.ReadOnly = True
            .Button1.Visible = False
            .ShipLine.ReadOnly = True
            .Button2.Visible = False
            .vessel.ReadOnly = True
            .ClearAll.Enabled = False
            .BeaMasuk.ReadOnly = True
            .vat.ReadOnly = True
            .pph.ReadOnly = True
            .piud.ReadOnly = True
            .finalty.ReadOnly = True
            '.TabControl1.SelectTab(8)
        End With
    End Sub

    Private Function f_GetAccess(ByVal modcode As String) As Boolean
        Dim SQLStr As String

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & modcode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        f_GetAccess = (DataExist(SQLStr) = True)
    End Function

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

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        If txtPO.Text <> "" Then
            If v_type = "CC" Or v_type = "CS" Then
                Call Display_FD()
            ElseIf v_type = "SD" Then
                Call Display_SD()
            ElseIf v_type = "GD" Then
                Call Display_GD()
            ElseIf v_type = "LS" Then
                Call Display_CC()
            ElseIf v_type = "PS" Then
                Call Display_SD()
            ElseIf v_type = "DS" Then
                Call Display_DS()
            End If
        Else
            'If v_type = "DP" Then
            If v_type = "DP" Or v_type = "CE" Then     ' Edit by AK 8/11/2010
                Call Display_DP()
            End If
        End If
    End Sub

    Private Sub btnList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnList.Click
        Dim iCnt, Cnt As Integer
        Dim FieldNm As String


        If txtStatus2.Text = "All Item" Then
            SQLstr = SQLstrA
            ErrMsg = "Datagrid view Failed"
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            Cnt = MyReader.FieldCount
            For iCnt = 0 To Cnt - 1
                FieldNm = FieldNm & ";" & MyReader.GetName(iCnt)
            Next
            CloseMyReader(MyReader, UserData)
        Else
            SQLstr = "SELECT * FROM tbm_listdata WHERE listname = '" & txtStatus2.Text & "' order by OrdNo"
            MyReader = DBQueryMyReader(SQLstr, MyConn, ErrMsg, UserData)
            If Not MyReader Is Nothing Then
                While MyReader.Read
                    FieldNm = FieldNm & ";" & MyReader.GetString("FieldName")
                    'dno = MyReader.GetString("OrdNo")
                End While
                CloseMyReader(MyReader, UserData)
            End If
        End If

        FieldNm = Mid(FieldNm, 2, Len(FieldNm))

        Dim PilihItem As New FM00_MonitoringList(txtStatus2.Text, FieldNm)
        PilihItem.ShowDialog()
        Call MasterForm_Load(sender, e)
    End Sub

    Private Sub btnFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFile.Click
        Shell("c:\imr\alchemy\AuSearch C:\Database\kamto_1 /query")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
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
            For j = 4 To DataGridView1.ColumnCount - 1
                StrColumn = DataGridView1.Columns(j).HeaderText
                xlsheet.Cells(2, j - 3) = StrColumn
            Next

            For i = 0 To DataGridView1.RowCount - 1
                For j = 4 To DataGridView1.ColumnCount - 1

                    StrColumn = DataGridView1.Columns(j).HeaderText
                    StrData = DataGridView1.Rows(i).Cells(StrColumn).Value.ToString

                    xlsheet.Cells(i + 3, j - 3) = StrData
                Next
            Next
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, 1)).Cells.Font.Size = 14
            xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(i + 3, j - 3)).EntireColumn.AutoFit()

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

    Private Sub btnGrpMat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGrpMat.Click
        PilihanDlg.Text = "Select Group Code"
        PilihanDlg.LblKey1.Text = "Group Code"
        PilihanDlg.LblKey2.Text = "Group Name"
        PilihanDlg.SQLGrid = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group"
        PilihanDlg.SQLFilter = "select GROUP_CODE as GroupCode, GROUP_NAME as GroupName from tbm_material_group " & _
                               "WHERE group_code LIKE 'FilterData1%' " & _
                               " and group_name LIKE 'FilterData2%' "
        PilihanDlg.Tables = "tbm_material_group"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtGrpMat.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
            lblGrpMat.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString()
        End If
    End Sub

    Private Sub btnBuss_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuss.Click
        PilihanDlg.Text = "Select Bussines Line"
        PilihanDlg.LblKey1.Text = "Bussines Line"
        PilihanDlg.LblKey2.Text = ""
        PilihanDlg.SQLGrid = "SELECT DISTINCT line_bussines FROM tbm_company"
        PilihanDlg.SQLFilter = "SELECT DISTINCT line_bussines FROM tbm_company " & _
                               "WHERE line_bussines LIKE  'FilterData1%'"
        PilihanDlg.Tables = "tbm_company"
        If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtBuss.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
        End If
    End Sub

    Private Sub cbPrdM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrdM.SelectedIndexChanged
        If cbPrdM.SelectedIndex = 0 Then
            cbPrdY.SelectedIndex = 0
            If v_type = "GD" Then cbSource.SelectedIndex = 0 'requested by andi 271010
            'cbSource.SelectedIndex = 0
            'tgl1_2.Checked = True
        Else
            If cbPrdY.SelectedIndex = 0 Then
                cbPrdY.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub cbPrdY_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPrdY.SelectedIndexChanged
        If cbPrdY.SelectedIndex = 0 Then
            cbPrdM.SelectedIndex = 0
            If v_type = "GD" Then cbSource.SelectedIndex = 0 'requested by andi 271010
            'cbSource.SelectedIndex = 0
            'tgl1_2.Checked = True
        Else
            If cbPrdM.SelectedIndex = 0 Then
                cbPrdM.SelectedIndex = 1
            End If
        End If
    End Sub

    Private Sub tgl1_2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tgl1_2.ValueChanged
        'If (tgl1_2.Checked = False And cbPrdY.SelectedIndex >= 0) Then
        'cbPrdM.SelectedIndex = PrdM
        'cbPrdY.SelectedIndex = 1
        'End If

    End Sub

    Private Sub cbStatus2_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbStatus2.SelectedValueChanged
        txtStatus2.Text = cbStatus2.Text
    End Sub

    Private Sub btnSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplier.Click

        If v_type = "LS" Or v_type = "DP" Then
            PilihanDlg.Text = "Select EMKL Code"
            PilihanDlg.LblKey1.Text = "EMKL Code"
            PilihanDlg.LblKey2.Text = "EMKL Name"
            PilihanDlg.SQLGrid = "SELECT COMPANY_CODE as EMKLCode, COMPANY_NAME as EMKLName FROM tbm_expedition"
            PilihanDlg.SQLFilter = "SELECT COMPANY_CODE as EMKLCode, COMPANY_NAME as EMKLName FROM tbm_expedition " & _
                                   "WHERE COMPANY_CODE LIKE 'FilterData1%' AND " & _
                                   "COMPANY_NAME LIKE 'FilterData2%'"
            PilihanDlg.Tables = "tbm_expedition"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txtSupplier_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                lblSupplier_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
        Else

            PilihanDlg.Text = "Select Supplier Code"
            PilihanDlg.LblKey1.Text = "Supplier Code"
            PilihanDlg.LblKey2.Text = "Supplier Name"
            PilihanDlg.SQLGrid = "SELECT SUPPLIER_CODE as SupplierCode, SUPPLIER_NAME as SupplierName FROM tbm_supplier"
            PilihanDlg.SQLFilter = "SELECT SUPPLIER_CODE as SupplierCode, SUPPLIER_NAME as SupplierName FROM tbm_supplier " & _
                                   "WHERE SUPPLIER_CODE LIKE 'FilterData1%' AND " & _
                                   "SUPPLIER_NAME LIKE 'FilterData2%'"
            PilihanDlg.Tables = "tbm_supplier"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                txtSupplier_Code.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                lblSupplier_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub cbStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStatus.SelectedIndexChanged
        If v_type = "GD" Then 'khusus untuk General Data
            If (cbStatus.SelectedIndex = 0) Or (cbStatus.SelectedIndex = 7) Or (cbStatus.SelectedIndex = 8) Then
                cbSource.SelectedIndex = 0
            End If
        ElseIf v_type = "CE" Then 'khusus untuk CostEMKL
            Call cbStatus2_Created()
        End If
    End Sub

    Private Sub cbFill_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFill.SelectedIndexChanged
        If v_type = "GD" Then 'khusus untuk General Data
            If (cbFill.SelectedIndex <> 0) Then
                cbSource.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub cbSource_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSource.SelectedIndexChanged
        If v_type = "GD" Then
            If cbSource.SelectedIndex = 0 Then
                'lblDataLimited.Text = ""
            Else
                lblDataLimited.Text = Footstr
                If (cbStatus.SelectedIndex = 0) Or (cbStatus.SelectedIndex = 7) Or (cbStatus.SelectedIndex = 8) Then
                    cbSource.SelectedIndex = 0
                End If
            End If
        End If
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Dim ViewerFrm As New Frm_CRViewer
        If CreateTempTableName() = False Then Exit Sub
        ProsesTempOuts()

        ViewerFrm.Tag = "OUTBPUM"
        ViewerFrm.ShowDialog()
    End Sub

    Private Function CreateTempTableName() As Boolean
        TempTableName = "temp_" & mac1 & "_outbpum"
        If DBCreateTableFromTable("ot_report_column", TempTableName, MyConn1, UserData, True) = 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Sub ProsesTempOuts()
        Dim TglBPUM, TglBPUM1, TglTransfer, TglTransfer1, ForwardToExp, ForwardToExp1, TglReal, ETA As String
        Dim NoBPUM, PO, Keperluan, Vessel, EMKL As String
        Dim JmlBPUM As Decimal
        Dim TglUpdate As String = Format(Now, "MMMMM dd,yyyy")
        Dim Header As String = ""

        SQLstr1 = "SELECT t1.plant_code,t1.company_code,t2.company_name " & _
                  "FROM tbm_plant t1, tbm_company t2 " & _
                  "WHERE t1.plant_code='" & txtPlant_Code.Text & "' AND t1.company_code = t2.company_code "
        MyReader = DBQueryMyReader(SQLstr1, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Application.DoEvents()
                Try
                    Header = MyReader.GetString(2)
                Catch ex As Exception
                    Header = ""
                End Try
            End While
        End If
        CloseMyReader(MyReader, UserData)

        MyReader = DBQueryMyReader(SQLStrE, MyConn, ErrMsg, UserData)
        Dim TglBPUMold, TglTransferOld As String
        TglBPUMold = "" : TglTransferOld = ""
        If Not MyReader Is Nothing Then
            While MyReader.Read
                Application.DoEvents()
                Try
                    TglBPUM = "'" & Format(MyReader.GetDateTime("BPUMPrinted"), "yyyy-MM-dd") & "'"
                    TglBPUM1 = Format(MyReader.GetDateTime("BPUMPrinted"), "dd-MMM-yyyy")
                Catch ex As Exception
                    TglBPUM = "null"
                    TglBPUM1 = ""
                End Try
                Try
                    TglTransfer = "'" & Format(MyReader.GetDateTime("TransferDate"), "yyyy-MM-dd") & "'"
                    TglTransfer1 = Format(MyReader.GetDateTime("TransferDate"), "dd-MMM-yyyy")
                Catch ex As Exception
                    TglTransfer = "null"
                    TglTransfer1 = ""
                End Try
                Try
                    ForwardToExp = "'" & Format(MyReader.GetDateTime("ForwardToExp"), "yyyy-MM-dd") & "'"
                    ForwardToExp1 = Format(MyReader.GetDateTime("ForwardToExp"), "dd-MMM-yyyy")
                Catch ex As Exception
                    ForwardToExp = "null"
                    ForwardToExp1 = ""
                End Try
                If TglBPUMold = TglBPUM1 Then
                    TglBPUM1 = ""
                    TglTransfer1 = ""
                End If
                Try
                    NoBPUM = MyReader.GetString("BPUMNo")
                Catch ex As Exception
                    NoBPUM = ""
                End Try
                Try
                    PO = MyReader.GetString("DetailOfPO")
                Catch ex As Exception
                    PO = ""
                End Try
                Try
                    ETA = Format(MyReader.GetDateTime("ETA"), "dd-MMM-yyyy")
                Catch ex As Exception
                    ETA = ""
                End Try
                Try
                    Keperluan = MyReader.GetString("Note")
                Catch ex As Exception
                    Keperluan = ""
                End Try
                Try
                    Vessel = MyReader.GetString("Vessel")
                Catch ex As Exception
                    Vessel = ""
                End Try
                Try
                    EMKL = MyReader.GetString("ExpShort")
                Catch ex As Exception
                    EMKL = ""
                End Try
                Try
                    JmlBPUM = MyReader.GetDecimal("Amount")
                Catch ex As Exception
                    JmlBPUM = 0
                End Try
                Try
                    TglReal = Format(MyReader.GetDateTime("BPJUMPrinted"), "dd-MMM-yyyy")
                Catch ex As Exception
                    TglReal = ""
                End Try

                ErrMsg = "Failed when saving user data"
                SQLstr = "INSERT INTO " & TempTableName & " (clmstr1,clmstr2,clmstr3,clmstr4,clmstr5,clmstr6,clmstr7,clmstr8,clmdec1," & _
                            "clmdate1,clmdate2,clmstr9,clmstr10,clmstr11,clmstr12,clmstr13,clmstr14) " & _
                         "VALUES ('" & cbStatus.Text & "','" & EscapeStr(Header) & "','" & EscapeStr(NoBPUM) & "','" & EscapeStr(PO) & "'," & _
                            "'" & EscapeStr(Keperluan) & " ','" & EscapeStr(Vessel) & "', '" & EscapeStr(EMKL) & "','" & EscapeStr(UserData.UserName) & "'," & _
                            "" & JmlBPUM & "," & TglBPUM & "," & TglTransfer & ",'" & TglBPUM1 & "','" & TglTransfer1 & "','" & ETA & "','" & TglReal & "','" & TglUpdate & "','" & ForwardToExp1 & "')"

                affrow = DBQueryUpdate(SQLstr, MyConn1, False, ErrMsg, UserData)
                If affrow < 0 Then
                    MsgBox("Saving failed...", MsgBoxStyle.Information, "Input User data")
                    CloseMyReader(MyReader, UserData)
                    Exit Sub
                Else
                    'f_msgbox_successful("Save Data")
                    ''DataGridView1.DataSource = Show_Grid(DataGridView1, "tbm_material")
                End If
                TglBPUMold = TglBPUM1
                TglTransferOld = TglTransfer1
            End While
            CloseMyReader(MyReader, UserData)
        End If

    End Sub

    Function GetUserMACAddress() As String
        Dim strQuery As String = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"
        Dim query As ManagementObjectSearcher = New ManagementObjectSearcher(strQuery)
        Dim queryCollection As ManagementObjectCollection = query.Get()
        Dim mo As ManagementObject

        GetUserMACAddress = ""
        For Each mo In queryCollection
            GetUserMACAddress = mo("MacAddress").ToString()
            GetUserMACAddress = Regex.Replace(GetUserMACAddress, ":", "_")
            Exit For
        Next
    End Function

    Private Sub BtnExpediction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExpedition.Click

        If v_type = "DS" Then ' untuk Deptan Status
            PilihanDlg.Text = "Select Origin Code"
            PilihanDlg.LblKey1.Text = "Origin Code"
            PilihanDlg.LblKey2.Text = "Origin Name"
            PilihanDlg.SQLGrid = "SELECT COUNTRY_CODE as OriginCode, COUNTRY_NAME as OriginName FROM tbm_country"
            PilihanDlg.SQLFilter = "SELECT COUNTRY_CODE as OriginCode, COUNTRY_NAME as OriginName FROM tbm_country " & _
                                   "WHERE COUNTRY_CODE LIKE 'FilterData1%' AND " & _
                                   "COUNTRY_NAME LIKE 'FilterData2%'"
            PilihanDlg.Tables = "tbm_country"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TxtExpedition.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                LblExpedition_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If

        Else
            PilihanDlg.Text = "Select EMKL Code"
            PilihanDlg.LblKey1.Text = "EMKL Code"
            PilihanDlg.LblKey2.Text = "EMKL Name"
            PilihanDlg.SQLGrid = "SELECT COMPANY_CODE as EMKLCode, COMPANY_NAME as EMKLName FROM tbm_expedition"
            PilihanDlg.SQLFilter = "SELECT COMPANY_CODE as EMKLCode, COMPANY_NAME as EMKLName FROM tbm_expedition " & _
                                   "WHERE COMPANY_CODE LIKE 'FilterData1%' AND " & _
                                   "COMPANY_NAME LIKE 'FilterData2%'"
            PilihanDlg.Tables = "tbm_expedition"
            If PilihanDlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
                TxtExpedition.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(0).Value.ToString
                LblExpedition_Name.Text = PilihanDlg.DgvResult.CurrentRow.Cells.Item(1).Value.ToString
            End If
        End If
    End Sub

    Private Sub CostEMKL_List()     ' Add by AK 2/11/2010
        Dim dts As DataTable
        Dim MyReader As MySqlDataReader
        Dim mSqlQuery As String = ""
        Dim strtb, whrtb As String

        If cbStatus2.Text <> "ALL" Then
            strtb = "tb.shipment_no, tb.exp_code, tb." & cbStatus2.Text & " "
            whrtb = "AND tb." & cbStatus2.Text & " <> '' "
        Else
            strtb = "tb.* "
            whrtb = " "
        End If
        SQLstrCE = SQLstrCE

        '"AND (" & GroupUser & " = 1 OR (" & GroupUser & " <> 1 AND t1.findoc_printeddt > DATE_FORMAT(DATE_SUB(NOW(), INTERVAL 3 MONTH),'%Y-%m-%d'))) " & _

        SQLstr1 = "SELECT t2.shipment_no, '' po_no, 0 po_item, bl_no BL, m1.plant_name FinalPlant, m2.port_name FinalPort, " & _
                  "(SELECT GROUP_CONCAT(DISTINCT getpoorder(st1.shipment_no, TRIM(st1.po_no))) FROM tbl_shipping_detail st1 WHERE st1.shipment_no=t1.shipment_no) DetailOfPO, " & _
                  "(SELECT GROUP_CONCAT(DISTINCT sm2.group_name) FROM tbl_shipping_detail st2, tbm_material sm1, tbm_material_group sm2 WHERE st2.material_code = sm1.material_code AND sm1.group_code = sm2.group_code AND st2.shipment_no=t1.shipment_no) GroupMaterial, " & _
                  "(SELECT SUM(1) FROM tbl_shipping_cont st3 WHERE st3.unit_code<>'CURAH' AND  st3.shipment_no=t1.shipment_no) TotalContainer, " & _
                  "(SELECT GROUP_CONCAT(DISTINCT st4.unit_code) FROM tbl_shipping_cont st4 WHERE st4.shipment_no=t1.shipment_no) UnitContainer, " & _
                  "t2.est_arrival_dt ETA, t2.notice_arrival_dt ActualArrival, t2.sppb_dt Clearance, (t2.free_time + t2.fte) FTD, t2.vessel Vessel, m4.company_name Expedition " & _
                  "FROM tbl_shipping_doc t1, tbl_shipping t2, tbm_plant m1, tbm_port m2, tbm_company m3, tbm_expedition m4 " & _
                  "WHERE t1.shipment_no = t2.shipment_no AND t1.findoc_status<>'Rejected' AND " & TypeCE & " " & _
                  "AND ((not " & tgl1_2.Checked & ") OR (" & tgl1_2.Checked & " AND t1.findoc_printeddt >= '" & Format(tgl1_2.Value, "yyyy-MM-dd") & "' AND t1.findoc_printeddt <= '" & Format(tgl2_2.Value, "yyyy-MM-dd") & "')) " & _
                  "AND t2.plant_code=m1.plant_code AND t2.port_code=m2.port_code AND m1.company_code=m3.company_code AND t1.findoc_to=m4.company_code " & _
                  "AND t2.plant_code IN (" & strF & ") " & _
                  "AND (m3.line_bussines = '" & txtBuss.Text & "' OR '-' = '" & txtBuss.Text & "' OR '' = '" & txtBuss.Text & "') " & _
                  "AND (t2.plant_code LIKE TRIM('%" & txtPlant_Code.Text & "%') OR '' = '" & txtPlant_Code.Text & "') " & _
                  "AND (t2.port_code LIKE TRIM('%" & txtPort_Code.Text & "%') OR '' = '" & txtPort_Code.Text & "') " & _
                  "AND (t1.findoc_to LIKE TRIM('%" & TxtExpedition.Text & "%') OR '' = '" & TxtExpedition.Text & "') " & _
                  "AND ((SELECT GROUP_CONCAT(DISTINCT sm2.group_name) FROM tbl_shipping_detail st2, tbm_material sm1, tbm_material_group sm2 WHERE st2.material_code = sm1.material_code AND sm1.group_code = sm2.group_code AND st2.shipment_no=t1.shipment_no) LIKE TRIM('%" & lblGrpMat.Text & "%') OR '' = '" & lblGrpMat.Text & "') " & _
                  "ORDER BY t2.notice_arrival_dt DESC, t2.est_arrival_dt DESC " & _
                  " "

        SQLstr1 = "SELECT ta.*, " & strtb & " " & _
                  "FROM (" & SQLstr1 & ") ta " & _
                  "LEFT JOIN (" & SQLstrCE & ") tb " & _
                  "ON ta.shipment_no=tb.shipment_no " & _
                  "Where 1=1 " & whrtb

        ErrMsg = "Datagrid view Failed"
        DataGridView1.DataSource = Nothing
        DataGridView1.Columns.Clear()
        Application.DoEvents()
        dts = DBQueryDataTable(SQLstr1, MyConn, "", ErrMsg, UserData)

        DataGridView1.DataSource = dts
        If DataGridView1.RowCount > 0 Then
            DataGridView1.Columns(0).Visible = False
            DataGridView1.Columns(1).Visible = False
            DataGridView1.Columns(2).Visible = False
            DataGridView1.Columns(3).Width = 100
            DataGridView1.Columns(16).Visible = False
            DataGridView1.Columns(17).Visible = False
        End If
    End Sub

    Private Sub txtPlant_Code_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPlant_Code.TextChanged
        If txtPlant_Code.Text = "" Then
            lblPlant_Name.Text = ""
        End If
    End Sub

    Private Sub txtPort_Code_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPort_Code.TextChanged
        If txtPort_Code.Text = "" Then
            lblPort_Name.Text = ""
        End If
    End Sub

    Private Sub txtGrpMat_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGrpMat.TextChanged
        If txtGrpMat.Text = "" Then
            lblGrpMat.Text = ""
        End If
    End Sub

    Private Sub TxtExpedition_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtExpedition.TextChanged
        If TxtExpedition.Text = "" Then
            LblExpedition_Name.Text = ""
        End If
    End Sub

    Private Sub txtBuss_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBuss.TextChanged

    End Sub

    Private Sub lblPeriodGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblPeriodGrp.Click

    End Sub

    Private Sub cbStatus2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStatus2.SelectedIndexChanged

    End Sub
End Class

