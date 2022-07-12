Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Management
Imports System.Text.RegularExpressions
Public Class MDIParent1
    'Dim MyConn As MySqlConnection
    Dim PilihanDlg As New DlgPilihan
    Dim MyReader As MySqlDataReader
    Dim lockedCheckState As Boolean = False
    Dim FormCode As New Hashtable(1000)
    Dim SQLStr, ErrMsg, HelpDir As String
    Dim TotRow As Integer

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.Close()
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Use My.Computer.Clipboard to insert the selected text or images into the clipboard
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Use My.Computer.Clipboard.GetText() or My.Computer.Clipboard.GetData to retrieve information from the clipboard.
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Close all child forms of the parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Function GetData(ByVal strSQL As String) As String
        Dim MyComm As MySqlCommand = MyConn.CreateCommand()

        MyComm.CommandText = strSQL
        MyComm.CommandType = CommandType.Text
        Try
            GetData = MyComm.ExecuteScalar()
        Catch ex As Exception
            GetData = Nothing
        End Try
        MyComm.Dispose()
    End Function

    Function GetUserMACAddress() As String
        Dim strQuery As String = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = True"
        Dim query As ManagementObjectSearcher = New ManagementObjectSearcher(strQuery)
        Dim queryCollection As ManagementObjectCollection = query.Get()
        Dim mo As ManagementObject

        GetUserMACAddress = ""
        For Each mo In queryCollection
            GetUserMACAddress = mo("MacAddress").ToString()
            GetUserMACAddress = Regex.Replace(GetUserMACAddress, ":", "-")
            Exit For
        Next
    End Function

    Function GetUserIPAddress() As String
        Try
            Dim h As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName)
            Return h.AddressList.GetValue(0).ToString
        Catch ex As Exception
            Return "127.0.0.1"
        End Try
    End Function

    Private Sub MDIParent1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ver1, mac1, tgl As String
        Dim ver2, mac2 As Object
        Dim mess, warn, warntip, ip As String
        Dim pos As Integer

        Dim oFile As System.IO.File
        Dim fileName As String = "POIM.exe"
        Dim acc As String

        If oFile.Exists(fileName) Then
            'acc = oFile.GetLastAccessTime(fileName).ToString
            'acc = oFile.GetCreationTime(fileName).ToString
            acc = oFile.GetLastWriteTime(fileName).ToString
        End If

        MyConn = FncMyConnection(UserData.ConfigData)
        If MyConn Is Nothing Then
            FrmSettingConnection.ShowDialog()
            MyConn = FncMyConnection(UserData.ConfigData)
            If MyConn Is Nothing Then
                Me.Close()
                Exit Sub
            End If
        End If
        HelpDir = "\\" & MyConn.DataSource & "\temp\Share\AdmImport.chm"

        'Version
        ver1 = String.Format("Version {0}", My.Application.Info.Version.ToString)
        'tgl = String.Format(Microsoft.VisualBasic.Mid(My.Application.Info.Description.ToString, 62, Len(My.Application.Info.Description.ToString) - 61))
        Me.Text = Me.Text
        pos = InStr(ver1, " ")
        ver1 = Microsoft.VisualBasic.Mid(ver1, pos + 1, Len(ver1) - pos)
        ver2 = GetData("select version from tbm_version")
        warn = GetData("select warning from tbm_warning")
        If warn = "" Then
            warn = " Kurs/pajak hari ini :" & GetData("SELECT GROUP_CONCAT(' ',currency_code,' ',kurs,'/',pajak) as warning FROM " & _
                              " (SELECT currency_code, effective_date, CAST(FORMAT(effective_kurs,2) AS CHAR) kurs, CAST(FORMAT(kurs_pajak,2) AS CHAR) pajak " & _
                              "  FROM tbm_kurs WHERE effective_date = DATE_FORMAT(NOW(),'%Y-%m-%d') " & _
                              ") t1 GROUP BY effective_date")
        End If
        If ver2 Is Nothing Then
            mess = "Invalid Program version " & Chr(13) & Chr(10) & _
                   "Versi program saat ini : " & ver2.ToString & " " & Chr(13) & Chr(10) & _
                   "Silahkan menghubungi User Support setempat"
            MsgBox(mess)
            Application.Exit()
        Else
            'If ver1 <> ver2.ToString And UserData.UserName <> "Supram Maharwantijo" Then
            If ver1 <> ver2.ToString Then
                mess = "Versi program yang Anda pakai (" & ver1 & ") sudah tidak up-to-date " & Chr(13) & Chr(10) & _
                       "Versi terbaru adalah : " & ver2.ToString & " " & Chr(13) & Chr(10) & _
                       "Silahkan menghubungi User Support setempat"
                MsgBox(mess)
                Application.Exit()
            End If
        End If

        'MAC Address
        mac1 = GetUserMACAddress()
        mac2 = GetData("select mac_address from tbm_mac where trim(mac_Address)=trim('" & mac1 & "')")

        'IP Address
        ip = GetUserIPAddress()

        If mac2 Is Nothing Then
            mess = "You are not authorized to use this program"
            MsgBox(mess)
            Application.Exit()
        Else
            SQLStr = "UPDATE tbm_mac " & _
                 "SET ip_address='" & ip & "', " & _
                 "    prg_version='" & ver1 & " Updated " & acc & "', " & _
                 "    last_access='" & Format(Now(), "yyyy-MM-dd h:mm:ss") & "' " & _
                 "WHERE trim(mac_Address)=trim('" & mac1 & "')"

            TotRow = DBQueryUpdate(SQLStr, MyConn, False, ErrMsg, UserData)
        End If
        txtStatus2.Text = " Login as " & UserData.UserName & ""
        txtStatus3.Text = " Host name : " & DbHost & ""
        txtStatus4.Text = " Db name : " & DbName & " "
        txtStatus5.Text = " Version : " & ver1 & " "
        txtStatus6.Text = " Updated : " & acc & " "
        txtStatus7.Text = warn

        SQLStr = "SELECT stat_text FROM tbm_status_jobproses WHERE stat_display='Y' and stat_text<>'' ORDER BY stat_ord"

        MyReader = DBQueryMyReader(SQLStr, MyConn, ErrMsg, UserData)
        If Not MyReader Is Nothing Then
            While MyReader.Read
                mess = MyReader.GetString(0)
                MsgBox(mess, MsgBoxStyle.Information, "Information")
            End While
        End If
        CloseMyReader(MyReader, UserData)
    End Sub


    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Form.ActiveForm.Close()
    End Sub

    Private Sub UserProfileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New FM01_UserAdmin   'FrmSettingAksesUser  '
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub DocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DocumentToolStripMenuItem.Click
        If f_GetAccess("M02") Then
            Dim f As New FM05_Document   'FrmSettingAksesUser  '
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub FrmSettingUserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New FrmSettingUser
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub MaterialGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CurrencyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrencyToolStripMenuItem.Click
        If f_GetAccess("M05") Then
            Dim f As New FM10_Currency
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub MaterialOriginToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialOriginToolStripMenuItem1.Click
        If f_GetAccess("M01") Then
            Dim f As New FM04_Material_Origin
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub MaterialGroupToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialGroupToolStripMenuItem1.Click
        If f_GetAccess("M01") Then
            Dim f As New FM02_MaterialGroup
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub MaterialDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MaterialDataToolStripMenuItem.Click
        If f_GetAccess("M01") Then
            Dim f As New FM03_Material
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub CountryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CountryToolStripMenuItem.Click
        If f_GetAccess("M03") Then
            Dim f As New FM07_Country
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub CompanyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompanyToolStripMenuItem1.Click
        If f_GetAccess("M04") Then
            Dim f As New FM09_Company
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub PaymentClassToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentClassToolStripMenuItem1.Click
        If f_GetAccess("M11") Then
            Dim f As New FM17_PaymentClass
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If

    End Sub

    Private Sub PaymentTermToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentTermToolStripMenuItem1.Click
        If f_GetAccess("M11") Then
            Dim f As New FM18_PaymentTerm
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If

    End Sub

    Private Sub CityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CityToolStripMenuItem.Click
        If f_GetAccess("M03") Then
            Dim f As New FM08_City
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub PlantToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlantToolStripMenuItem.Click
        If f_GetAccess("M12") Then
            Dim f As New FM19_Plant
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub SupplierCompanyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierCompanyToolStripMenuItem1.Click
        If f_GetAccess("M13") Then
            Dim f As New FM22_SupplierComp
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub UnitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnitToolStripMenuItem.Click
        If f_GetAccess("M14") Then
            Dim f As New FM23_Unit
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub LinesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinesToolStripMenuItem.Click
        Dim f As New FM24_Line
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub PortToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PortToolStripMenuItem.Click
        If f_GetAccess("M12") Then
            Dim f As New FM20_Port
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub PackingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PackingToolStripMenuItem.Click
        If f_GetAccess("M10") Then
            Dim f As New FM16_Packing
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub AttachmentDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AttachmentDocumentToolStripMenuItem.Click
        If f_GetAccess("M02") Then
            Dim f As New FM06_AttachDoc
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If

    End Sub

    Private Sub BankRefferenceToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankRefferenceToolStripMenuItem1.Click
        If f_GetAccess("M07") Then
            Dim f As New FM13_BankReff
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub CurrencyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrencyToolStripMenuItem1.Click
        If f_GetAccess("M06") Then
            Dim f As New FM11_Kurs
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub InsuranceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsuranceToolStripMenuItem.Click
        If f_GetAccess("M08") Then
            'Dim f As New FM14_Insurance
            Dim f As New FM14_Incoterm
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub SupplierDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SupplierDataToolStripMenuItem.Click
        If f_GetAccess("M13") Then
            Dim f As New FM21_Supplier
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub BankDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BankDataToolStripMenuItem.Click
        If f_GetAccess("M07") Then
            Dim f As New FM12a_Bank
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub FinancialDocumentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinancialDocumentsToolStripMenuItem.Click
        Dim f As New FrmBL
        Dim cnt, a As Integer
        Dim nemu As Boolean = False

        If Not PunyaAkses("FD-L") Then
            'MsgBox("You are not authorized for using Financial Documents")
            MsgBox("You are not authorized for using Funds & Finance")
            Exit Sub
        End If
        f.MdiParent = Me
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
        f.Show()
        DisabledScreen(f)
    End Sub

    Private Sub DisabledScreen(ByVal frm As FrmBL)
        With frm
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
            .dtAreaDeptan.Enabled = False
            .dtAreaRec.Enabled = False
            .free.ReadOnly = True
            .free_ext.ReadOnly = True
            .free_ext_prosdt.Enabled = False
            .free_ext_appdt.Enabled = False
            .free_ext_note.ReadOnly = True
            .ChkRedLn.Enabled = False
            .ChkMCI.Enabled = False
            .GrpCurah.Enabled = False
            .dtRecExp.Enabled = False
            .Expedition.ReadOnly = True
            .btnExpedition.Enabled = False
            .dtForwExp.Enabled = False
            .exp_note.ReadOnly = True
            .dtValue.Enabled = False
            .dtTT.Enabled = False
            .InsNo.ReadOnly = True
            .InsAmt.ReadOnly = True
            '.btnClearD.Visible = False
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
        End With
    End Sub

    Private Sub CustomFacilitiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CustomFacilitiesToolStripMenuItem.Click
        If f_GetAccess("SD-L") Then
            Dim f As New FrmBL
            Dim cnt, a As Integer
            Dim nemu As Boolean = False

            f.MdiParent = Me
            f.Text = "Customs Clearance"

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
            f.GridInv.AllowUserToAddRows = False
            f.Show()
            DisabledScreen(f)
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub CostCategorySubGroupToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostCategorySubGroupToolStripMenuItem.Click
        Dim f As New FM26_CostCat_SubGroup
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub CostCategoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostCategoryToolStripMenuItem.Click
        Dim f As New FM27_CostCat
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub CostRatesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostRatesToolStripMenuItem.Click
        Dim f As New FM28_CostRates
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP01OutstandingShipmentScheduleToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP01OutstandingShipmentScheduleToolStripMenuItem.Click
        Dim f As New ReportSelection("RP01", RP01OutstandingShipmentScheduleToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP02RemittanceHistoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP02RemittanceHistoryToolStripMenuItem.Click
        Dim f As New ReportSelection("RP02", RP02RemittanceHistoryToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP03DailyBudgetDetailsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP03DailyBudgetDetailsToolStripMenuItem.Click
        Dim f As New ReportSelection("RP03", RP03DailyBudgetDetailsToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP04WeeklyBudgetsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP04WeeklyBudgetsToolStripMenuItem.Click
        Dim f As New ReportSelection("RP04", RP04WeeklyBudgetsToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP05ShipArrivalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP05ShipArrivalToolStripMenuItem.Click
        Dim f As New ReportSelection("RP05", RP05ShipArrivalToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP06OverdueTradePayableToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP06OverdueTradePayableToolStripMenuItem.Click
        Dim f As New ReportSelection("RP06", RP06OverdueTradePayableToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP07DocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP07DocumentToolStripMenuItem.Click
        Dim f As New ReportSelection("RP07", RP07DocumentToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP08DEPTANLicensiQuotaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP08DEPTANLicensiQuotaToolStripMenuItem.Click
        Dim f As New ReportSelection("RP08", RP08DEPTANLicensiQuotaToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP09ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP09ToolStripMenuItem.Click
        Dim f As New ReportSelection("RP09", RP09ToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub RP10ActualCostVSEstimatedCostToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RP10ActualCostVSEstimatedCostToolStripMenuItem.Click
        Dim f As New ReportSelection("RP10", RP10ActualCostVSEstimatedCostToolStripMenuItem.Text)
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Function f_GetAccess(ByVal modcode As String) As Boolean
        Dim SQLStr As String
        'get module name
        'select * from tbm_moduls where modul_name like 'M%%'

        SQLStr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & modcode & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        f_GetAccess = (DataExist(SQLStr) = True)
    End Function

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim f As New AboutPOIM
        f.MdiParent = Me
        f.Show()
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
        Dim SQLStr As String

        SQLstr = "Select tu.user_ct,tu.Name from tbm_users as tu inner join tbm_users_modul as tum on " & _
                 "tu.user_ct = tum.user_ct where tum.modul_code = '" & kd & "' " & _
                 "and tu.user_ct=" & UserData.UserCT

        PunyaAkses = (DataExist(SQLstr) = True)
    End Function

    Private Sub ListingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListingToolStripMenuItem.Click
        Dim f As New FrmListImportData
        f.MdiParent = Me
        f.Show()
    End Sub
    Private Sub ClosingPOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosingPOToolStripMenuItem.Click
        If f_GetAccess("FM01") Or f_GetAccess("PO-C") Then
            Dim f As New FrmClosingPO
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub SetPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetPasswordToolStripMenuItem.Click
        Dim f As New FM29_PassUser
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        If f_GetAccess("FM01") Then
            Dim f As New FM01_UserAdmin   'FrmSettingAksesUser  '
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("You are not authorized to use User Admin")
        End If
    End Sub

    Private Sub Help()
        'Help.ShowHelp(Me, HelpDir, HelpNavigator.TableOfContents)
    End Sub

    Private Sub ExpeditionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpeditionToolStripMenuItem.Click
        Dim f As New FM25_Expedition
        f.MdiParent = Me
        f.Show()
    End Sub

    Private Sub PreImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PreImportToolStripMenuItem.Click
        If f_GetAccess("PO-L") Then
            Dim f As New FrmDOC_Import
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub PostImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostImportToolStripMenuItem.Click
        If f_GetAccess("SD-L") Then
            Dim f As New FrmBL
            Dim cnt, a As Integer
            Dim nemu As Boolean = False

            f.MdiParent = Me
            f.Text = "Post Import Document"

            cnt = f.ToolStrip1.Items.Count - 1
            For a = 0 To cnt
                If f.ToolStrip1.Items(a).Name = "btnRIL" And nemu = False Then nemu = True
                If f.ToolStrip1.Items(a).Name = "btnBPUM" Then nemu = False
                f.ToolStrip1.Items(a).Visible = nemu
            Next
            f.btnClose.Visible = True
            f.btnClose.Enabled = True
            f.ToolStripSeparator1.Visible = True
            f.grid2.AllowUserToAddRows = False
            f.grid3.AllowUserToAddRows = False
            f.grid4.AllowUserToAddRows = False
            f.GridInv.AllowUserToAddRows = False
            f.Show()
            DisabledScreen(f)
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub ContractToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContractToolStripMenuItem.Click
        If Not PunyaAkses("CD-C") Then
            MsgBox("You are not authorized for using Contract Document")
            Exit Sub
        End If
        Dim f As New FrmCD
        f.ShowDialog()
    End Sub

    Private Sub BillOfLadingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillOfLadingToolStripMenuItem.Click
        If f_GetAccess("SD-C") Then
            Dim f As New FrmBL
            Dim cnt, a As Integer
            Dim nemu As Boolean = True

            f.MdiParent = Me
            f.TabPage8.Dispose()
            cnt = f.ToolStrip1.Items.Count - 1
            For a = 0 To cnt
                If f.ToolStrip1.Items(a).Name = "ToolStripSeparator4" Then nemu = False
                f.ToolStrip1.Items(a).Visible = nemu
                f.ToolStrip1.Items(a).Enabled = nemu
            Next
            f.btnDelete.Enabled = False
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub POToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POToolStripMenuItem.Click
        If f_GetAccess("PO-C") Then
            Dim f As New FrmPO
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        If f_GetAccess("M17") Then
            Dim f As New FM30_Produsen
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub InklaringStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InklaringStatusToolStripMenuItem.Click
        If f_GetAccess("MO-02") Then
            Dim f As New FM00_Monitoring("CC")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub CostSlipStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostSlipStatusToolStripMenuItem.Click
        If f_GetAccess("MO-04") Then
            Dim f As New FM00_Monitoring("CS")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub ShipmentStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShipmentStatusToolStripMenuItem.Click
        If f_GetAccess("MO-01") Then
            Dim f As New FM00_Monitoring("GD")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub DataPIBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataPIBToolStripMenuItem.Click
        If f_GetAccess("SS-1") Then
            Dim f As New DataPIB
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub SynchronizePIBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeToolStripMenuItem.Click
        Dim appWord As New Word.Application
        Dim docWord As New Word.Document

        docWord = appWord.Documents.Open(My.Application.Info.DirectoryPath & "\Manual\Synchronize PIB.im2")
        appWord.Visible = True
    End Sub


    Private Sub PurchaseOrderToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchaseOrderToolStripMenuItem1.Click
        Dim appWord As New Word.Application
        Dim docWord As New Word.Document

        docWord = appWord.Documents.Open(My.Application.Info.DirectoryPath & "\Manual\PurchaseOrder.im2")
        appWord.Visible = True
    End Sub

    Private Sub BillOfLadingToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BillOfLadingToolStripMenuItem2.Click
        Dim appWord As New Word.Application
        Dim docWord As New Word.Document

        docWord = appWord.Documents.Open(My.Application.Info.DirectoryPath & "\Manual\BillOfLading.im2")
        appWord.Visible = True
    End Sub

    Private Sub ShipmentStatusToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShipmentStatusToolStripMenuItem1.Click
        If f_GetAccess("MO-02") Then
            Dim f As New FM00_Monitoring("SD")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub LogisticStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogisticStatusToolStripMenuItem.Click
        If f_GetAccess("MO-05") Then
            Dim f As New FM00_Monitoring("LS")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub PaymentStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentStatusToolStripMenuItem.Click
        If f_GetAccess("MO-06") Then
            Dim f As New FM00_Monitoring("PS")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub ContractStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContractStatusToolStripMenuItem.Click
        If f_GetAccess("MO-07") Then
            Dim f As New FM00_Monitoring("CO")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub TaxForecastControlToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TaxForecastControlToolStripMenuItem.Click
        If f_GetAccess("MO-08") Then
            Dim f As New FM00_Monitoring("TF")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub TandaTerimaPajakToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TandaTerimaPajakToolStripMenuItem.Click
        If f_GetAccess("SD-C") Then
            Dim f As New TandaTerimaPajak
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub TandaTerimaCostSlipToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TandaTerimaCostSlipToolStripMenuItem.Click
        If f_GetAccess("CS-C") Then
            Dim f As New TandaTerimaCSlip
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub SynchronizeToAP2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeToAP2ToolStripMenuItem.Click
        If f_GetAccess("AP-2") Then
            Dim f As New DataAP2
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub BAPBToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BAPBToolStripMenuItem.Click
        If f_GetAccess("GR-C") Then
            Dim f As New frmBAPB
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub SynchronizeAP2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SynchronizeAP2ToolStripMenuItem.Click
        Dim appWord As New Word.Application
        Dim docWord As New Word.Document

        docWord = appWord.Documents.Open(My.Application.Info.DirectoryPath & "\Manual\Synchronize AP2.im2")
        appWord.Visible = True
    End Sub

    Private Sub DataMonitoringToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataMonitoringToolStripMenuItem.Click
        Dim appWord As New Word.Application
        Dim docWord As New Word.Document

        docWord = appWord.Documents.Open(My.Application.Info.DirectoryPath & "\Manual\Data Monitoring.im2")
        appWord.Visible = True
    End Sub

    Private Sub FromSAPToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromSAPToolStripMenuItem.Click
        If f_GetAccess("SS-2") Then
            Dim f As New BAPB_SAP
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub TandaTerimaPVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TandaTerimaPVToolStripMenuItem.Click
        If f_GetAccess("PV-C") Then
            Dim f As New TandaTerimaPV
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub BPUMStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BPUMStatusToolStripMenuItem.Click
        If f_GetAccess("MO-09") Then
            Dim f As New FM00_Monitoring("DP")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub
    Private Sub AdmImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdmImportToolStripMenuItem.Click
        If f_GetAccess("RL-C") Then
            Dim f As New FRMRILQuota
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub
    Private Sub CostEMKLStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CostEMKLStatusToolStripMenuItem.Click
        ' Add by AK 2/11/2010
        If f_GetAccess("MO-10") Then

            Dim f As New FM00_Monitoring("CE")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub DeptanQuotaStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeptanQuotaStatusToolStripMenuItem.Click
        If f_GetAccess("MO-11") Then
            Dim f As New FM00_Monitoring("DQ")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub MonitoringToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MonitoringToolStripMenuItem.Click

    End Sub

    Private Sub DeptanStatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeptanStatusToolStripMenuItem.Click
        If f_GetAccess("MO-11") Then
            Dim f As New FM00_Monitoring("DS")
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub MasterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MasterToolStripMenuItem.Click

    End Sub

    Private Sub LaytimeCalculationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LaytimeCalculationToolStripMenuItem.Click
        If f_GetAccess("LT-C") Then
            Dim f As New FrmLT
            f.MdiParent = Me
            f.Show()
        Else
            f_msgbox_otorisasi("")
        End If
    End Sub

    Private Sub txtStatus2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStatus2.Click

    End Sub

    Private Sub txtStatus5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStatus5.Click

    End Sub

    Private Sub MenuStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip.ItemClicked

    End Sub

    Private Sub StatusStrip_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles StatusStrip.ItemClicked

    End Sub
End Class
