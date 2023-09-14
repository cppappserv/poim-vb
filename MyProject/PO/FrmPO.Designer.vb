<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPO
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPO))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnClosing = New System.Windows.Forms.ToolStripButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblLoadPort_Name = New System.Windows.Forms.Label
        Me.btnSearchLoadPort = New System.Windows.Forms.Button
        Me.txtLoadPort_Code = New System.Windows.Forms.TextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.btnSearchOri = New System.Windows.Forms.Button
        Me.lblInsurance_Desc = New System.Windows.Forms.Label
        Me.lblPayment_Name = New System.Windows.Forms.Label
        Me.lblPort_Name = New System.Windows.Forms.Label
        Me.lblPlant_Name = New System.Windows.Forms.Label
        Me.lblCompany_Name = New System.Windows.Forms.Label
        Me.TextBox = New System.Windows.Forms.TextBox
        Me.btnListPO = New System.Windows.Forms.Button
        Me.btnSearchMat = New System.Windows.Forms.Button
        Me.btnSearchInsurance = New System.Windows.Forms.Button
        Me.TxtInsurance = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnSearchPayment = New System.Windows.Forms.Button
        Me.txtPayment_Code = New System.Windows.Forms.TextBox
        Me.cbShipTerm = New System.Windows.Forms.ComboBox
        Me.DTPeriodeTO = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.DTPeriodeFR = New System.Windows.Forms.DateTimePicker
        Me.btnSearchPort = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtPort_Code = New System.Windows.Forms.TextBox
        Me.btnSearchDestin = New System.Windows.Forms.Button
        Me.btnSearchCompany = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCompany_Code = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPO_NO = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPlant_Code = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnSchedulePO = New System.Windows.Forms.Button
        Me.ETD = New System.Windows.Forms.DateTimePicker
        Me.Label38 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label37 = New System.Windows.Forms.Label
        Me.prodname = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.produsen = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.btnCD = New System.Windows.Forms.Button
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblKurs = New System.Windows.Forms.Label
        Me.lblSupplierName = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnSearchCurrency = New System.Windows.Forms.Button
        Me.txtCurrency_Code = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtTotal = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtTolerable_Del = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtPR_No = New System.Windows.Forms.TextBox
        Me.txtContract_No = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.btnSearchSupplier = New System.Windows.Forms.Button
        Me.txtSupplier_Code = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtIPA_No = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.crtcode = New System.Windows.Forms.TextBox
        Me.DTCreated = New System.Windows.Forms.DateTimePicker
        Me.CTFun = New System.Windows.Forms.TextBox
        Me.CTApp = New System.Windows.Forms.TextBox
        Me.CTpur = New System.Windows.Forms.TextBox
        Me.DtFundApp = New System.Windows.Forms.DateTimePicker
        Me.Label16 = New System.Windows.Forms.Label
        Me.btnUserFun = New System.Windows.Forms.Button
        Me.txtFUNDAPPBY = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.DtApproved = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnUserApp = New System.Windows.Forms.Button
        Me.txtAPPROVEDBY = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.DtPurchased = New System.Windows.Forms.DateTimePicker
        Me.Label13 = New System.Windows.Forms.Label
        Me.btnUserPur = New System.Windows.Forms.Button
        Me.txtPURCHASEDBY = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtStatus = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtCREATEDBY = New System.Windows.Forms.TextBox
        Me.DGVDetail = New System.Windows.Forms.DataGridView
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGVDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnNew, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnDelete, Me.ToolStripSeparator4, Me.btnClosing})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1021, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.AutoSize = False
        Me.btnClose.Image = Global.poim.My.Resources.Resources.CLOSE
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(60, 22)
        Me.btnClose.Text = "Close"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnNew
        '
        Me.btnNew.AutoSize = False
        Me.btnNew.Image = Global.poim.My.Resources.Resources.NewDocumentHS
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(60, 22)
        Me.btnNew.Text = "New"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Enabled = False
        Me.btnSave.Image = Global.poim.My.Resources.Resources.SaveHL
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(60, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.AutoSize = False
        Me.btnDelete.Enabled = False
        Me.btnDelete.Image = Global.poim.My.Resources.Resources.delete
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnClosing
        '
        Me.btnClosing.Enabled = False
        Me.btnClosing.Image = CType(resources.GetObject("btnClosing.Image"), System.Drawing.Image)
        Me.btnClosing.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClosing.Name = "btnClosing"
        Me.btnClosing.Size = New System.Drawing.Size(86, 22)
        Me.btnClosing.Text = "Closing PO"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblLoadPort_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchLoadPort)
        Me.GroupBox1.Controls.Add(Me.txtLoadPort_Code)
        Me.GroupBox1.Controls.Add(Me.Label40)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.Label35)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.Label30)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.btnSearchOri)
        Me.GroupBox1.Controls.Add(Me.lblInsurance_Desc)
        Me.GroupBox1.Controls.Add(Me.lblPayment_Name)
        Me.GroupBox1.Controls.Add(Me.lblPort_Name)
        Me.GroupBox1.Controls.Add(Me.lblPlant_Name)
        Me.GroupBox1.Controls.Add(Me.lblCompany_Name)
        Me.GroupBox1.Controls.Add(Me.TextBox)
        Me.GroupBox1.Controls.Add(Me.btnListPO)
        Me.GroupBox1.Controls.Add(Me.btnSearchMat)
        Me.GroupBox1.Controls.Add(Me.btnSearchInsurance)
        Me.GroupBox1.Controls.Add(Me.TxtInsurance)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnSearchPayment)
        Me.GroupBox1.Controls.Add(Me.txtPayment_Code)
        Me.GroupBox1.Controls.Add(Me.cbShipTerm)
        Me.GroupBox1.Controls.Add(Me.DTPeriodeTO)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.DTPeriodeFR)
        Me.GroupBox1.Controls.Add(Me.btnSearchPort)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtPort_Code)
        Me.GroupBox1.Controls.Add(Me.btnSearchDestin)
        Me.GroupBox1.Controls.Add(Me.btnSearchCompany)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtCompany_Code)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPO_NO)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtPlant_Code)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 32)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(502, 251)
        Me.GroupBox1.TabIndex = 149
        Me.GroupBox1.TabStop = False
        '
        'lblLoadPort_Name
        '
        Me.lblLoadPort_Name.AutoSize = True
        Me.lblLoadPort_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoadPort_Name.Location = New System.Drawing.Point(229, 113)
        Me.lblLoadPort_Name.Name = "lblLoadPort_Name"
        Me.lblLoadPort_Name.Size = New System.Drawing.Size(62, 13)
        Me.lblLoadPort_Name.TabIndex = 182
        Me.lblLoadPort_Name.Text = "Load Port"
        '
        'btnSearchLoadPort
        '
        Me.btnSearchLoadPort.Image = CType(resources.GetObject("btnSearchLoadPort.Image"), System.Drawing.Image)
        Me.btnSearchLoadPort.Location = New System.Drawing.Point(199, 107)
        Me.btnSearchLoadPort.Name = "btnSearchLoadPort"
        Me.btnSearchLoadPort.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchLoadPort.TabIndex = 179
        Me.btnSearchLoadPort.UseVisualStyleBackColor = True
        '
        'txtLoadPort_Code
        '
        Me.txtLoadPort_Code.Location = New System.Drawing.Point(137, 108)
        Me.txtLoadPort_Code.MaxLength = 5
        Me.txtLoadPort_Code.Name = "txtLoadPort_Code"
        Me.txtLoadPort_Code.ReadOnly = True
        Me.txtLoadPort_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtLoadPort_Code.TabIndex = 181
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(12, 115)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(53, 13)
        Me.Label40.TabIndex = 180
        Me.Label40.Text = "Load Port"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.Color.Red
        Me.Label36.Location = New System.Drawing.Point(56, 208)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(14, 17)
        Me.Label36.TabIndex = 178
        Me.Label36.Text = "*"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Red
        Me.Label35.Location = New System.Drawing.Point(88, 184)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(14, 17)
        Me.Label35.TabIndex = 177
        Me.Label35.Text = "*"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Red
        Me.Label34.Location = New System.Drawing.Point(88, 160)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(14, 17)
        Me.Label34.TabIndex = 176
        Me.Label34.Text = "*"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Red
        Me.Label30.Location = New System.Drawing.Point(96, 136)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(14, 17)
        Me.Label30.TabIndex = 175
        Me.Label30.Text = "*"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.Red
        Me.Label23.Location = New System.Drawing.Point(40, 88)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(14, 17)
        Me.Label23.TabIndex = 174
        Me.Label23.Text = "*"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Red
        Me.Label22.Location = New System.Drawing.Point(72, 64)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(14, 17)
        Me.Label22.TabIndex = 173
        Me.Label22.Text = "*"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Red
        Me.Label21.Location = New System.Drawing.Point(64, 40)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(14, 17)
        Me.Label21.TabIndex = 172
        Me.Label21.Text = "*"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Red
        Me.Label19.Location = New System.Drawing.Point(56, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(14, 17)
        Me.Label19.TabIndex = 171
        Me.Label19.Text = "*"
        '
        'btnSearchOri
        '
        Me.btnSearchOri.Image = CType(resources.GetObject("btnSearchOri.Image"), System.Drawing.Image)
        Me.btnSearchOri.Location = New System.Drawing.Point(420, 60)
        Me.btnSearchOri.Name = "btnSearchOri"
        Me.btnSearchOri.Size = New System.Drawing.Size(22, 18)
        Me.btnSearchOri.TabIndex = 170
        Me.btnSearchOri.UseVisualStyleBackColor = True
        Me.btnSearchOri.Visible = False
        '
        'lblInsurance_Desc
        '
        Me.lblInsurance_Desc.AutoSize = True
        Me.lblInsurance_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInsurance_Desc.Location = New System.Drawing.Point(229, 205)
        Me.lblInsurance_Desc.Name = "lblInsurance_Desc"
        Me.lblInsurance_Desc.Size = New System.Drawing.Size(71, 13)
        Me.lblInsurance_Desc.TabIndex = 169
        Me.lblInsurance_Desc.Text = "Description"
        '
        'lblPayment_Name
        '
        Me.lblPayment_Name.AutoSize = True
        Me.lblPayment_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayment_Name.Location = New System.Drawing.Point(229, 182)
        Me.lblPayment_Name.Name = "lblPayment_Name"
        Me.lblPayment_Name.Size = New System.Drawing.Size(91, 13)
        Me.lblPayment_Name.TabIndex = 168
        Me.lblPayment_Name.Text = "Payment Name"
        '
        'lblPort_Name
        '
        Me.lblPort_Name.AutoSize = True
        Me.lblPort_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPort_Name.Location = New System.Drawing.Point(229, 90)
        Me.lblPort_Name.Name = "lblPort_Name"
        Me.lblPort_Name.Size = New System.Drawing.Size(30, 13)
        Me.lblPort_Name.TabIndex = 167
        Me.lblPort_Name.Text = "Port"
        '
        'lblPlant_Name
        '
        Me.lblPlant_Name.AutoSize = True
        Me.lblPlant_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlant_Name.Location = New System.Drawing.Point(229, 68)
        Me.lblPlant_Name.Name = "lblPlant_Name"
        Me.lblPlant_Name.Size = New System.Drawing.Size(71, 13)
        Me.lblPlant_Name.TabIndex = 166
        Me.lblPlant_Name.Text = "Destination"
        '
        'lblCompany_Name
        '
        Me.lblCompany_Name.AutoSize = True
        Me.lblCompany_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompany_Name.Location = New System.Drawing.Point(229, 46)
        Me.lblCompany_Name.Name = "lblCompany_Name"
        Me.lblCompany_Name.Size = New System.Drawing.Size(58, 13)
        Me.lblCompany_Name.TabIndex = 165
        Me.lblCompany_Name.Text = "Company"
        '
        'TextBox
        '
        Me.TextBox.Location = New System.Drawing.Point(365, 37)
        Me.TextBox.Name = "TextBox"
        Me.TextBox.Size = New System.Drawing.Size(46, 20)
        Me.TextBox.TabIndex = 164
        Me.TextBox.Visible = False
        '
        'btnListPO
        '
        Me.btnListPO.Image = CType(resources.GetObject("btnListPO.Image"), System.Drawing.Image)
        Me.btnListPO.Location = New System.Drawing.Point(319, 15)
        Me.btnListPO.Name = "btnListPO"
        Me.btnListPO.Size = New System.Drawing.Size(22, 20)
        Me.btnListPO.TabIndex = 1
        Me.btnListPO.UseVisualStyleBackColor = True
        '
        'btnSearchMat
        '
        Me.btnSearchMat.Image = CType(resources.GetObject("btnSearchMat.Image"), System.Drawing.Image)
        Me.btnSearchMat.Location = New System.Drawing.Point(420, 37)
        Me.btnSearchMat.Name = "btnSearchMat"
        Me.btnSearchMat.Size = New System.Drawing.Size(22, 18)
        Me.btnSearchMat.TabIndex = 153
        Me.btnSearchMat.UseVisualStyleBackColor = True
        Me.btnSearchMat.Visible = False
        '
        'btnSearchInsurance
        '
        Me.btnSearchInsurance.Image = CType(resources.GetObject("btnSearchInsurance.Image"), System.Drawing.Image)
        Me.btnSearchInsurance.Location = New System.Drawing.Point(199, 201)
        Me.btnSearchInsurance.Name = "btnSearchInsurance"
        Me.btnSearchInsurance.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchInsurance.TabIndex = 9
        Me.btnSearchInsurance.UseVisualStyleBackColor = True
        '
        'TxtInsurance
        '
        Me.TxtInsurance.Location = New System.Drawing.Point(137, 202)
        Me.TxtInsurance.MaxLength = 1
        Me.TxtInsurance.Name = "TxtInsurance"
        Me.TxtInsurance.ReadOnly = True
        Me.TxtInsurance.Size = New System.Drawing.Size(58, 20)
        Me.TxtInsurance.TabIndex = 160
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 159
        Me.Label7.Text = "Incoterm"
        '
        'btnSearchPayment
        '
        Me.btnSearchPayment.Image = CType(resources.GetObject("btnSearchPayment.Image"), System.Drawing.Image)
        Me.btnSearchPayment.Location = New System.Drawing.Point(199, 178)
        Me.btnSearchPayment.Name = "btnSearchPayment"
        Me.btnSearchPayment.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchPayment.TabIndex = 8
        Me.btnSearchPayment.UseVisualStyleBackColor = True
        '
        'txtPayment_Code
        '
        Me.txtPayment_Code.Location = New System.Drawing.Point(137, 179)
        Me.txtPayment_Code.MaxLength = 1
        Me.txtPayment_Code.Name = "txtPayment_Code"
        Me.txtPayment_Code.ReadOnly = True
        Me.txtPayment_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtPayment_Code.TabIndex = 156
        '
        'cbShipTerm
        '
        Me.cbShipTerm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShipTerm.FormattingEnabled = True
        Me.cbShipTerm.Items.AddRange(New Object() {"W - Whole Shipment", "P  - Partial Shipment"})
        Me.cbShipTerm.Location = New System.Drawing.Point(137, 155)
        Me.cbShipTerm.Name = "cbShipTerm"
        Me.cbShipTerm.Size = New System.Drawing.Size(178, 21)
        Me.cbShipTerm.TabIndex = 7
        '
        'DTPeriodeTO
        '
        Me.DTPeriodeTO.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPeriodeTO.Location = New System.Drawing.Point(232, 132)
        Me.DTPeriodeTO.Name = "DTPeriodeTO"
        Me.DTPeriodeTO.Size = New System.Drawing.Size(83, 20)
        Me.DTPeriodeTO.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(220, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(13, 13)
        Me.Label5.TabIndex = 153
        Me.Label5.Text = "_"
        '
        'DTPeriodeFR
        '
        Me.DTPeriodeFR.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTPeriodeFR.Location = New System.Drawing.Point(137, 132)
        Me.DTPeriodeFR.Name = "DTPeriodeFR"
        Me.DTPeriodeFR.Size = New System.Drawing.Size(84, 20)
        Me.DTPeriodeFR.TabIndex = 5
        Me.DTPeriodeFR.Value = New Date(2009, 3, 19, 0, 0, 0, 0)
        '
        'btnSearchPort
        '
        Me.btnSearchPort.Image = CType(resources.GetObject("btnSearchPort.Image"), System.Drawing.Image)
        Me.btnSearchPort.Location = New System.Drawing.Point(199, 84)
        Me.btnSearchPort.Name = "btnSearchPort"
        Me.btnSearchPort.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchPort.TabIndex = 4
        Me.btnSearchPort.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 185)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(75, 13)
        Me.Label6.TabIndex = 150
        Me.Label6.Text = "Payment Term"
        '
        'txtPort_Code
        '
        Me.txtPort_Code.Location = New System.Drawing.Point(137, 85)
        Me.txtPort_Code.MaxLength = 5
        Me.txtPort_Code.Name = "txtPort_Code"
        Me.txtPort_Code.ReadOnly = True
        Me.txtPort_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtPort_Code.TabIndex = 149
        '
        'btnSearchDestin
        '
        Me.btnSearchDestin.Image = CType(resources.GetObject("btnSearchDestin.Image"), System.Drawing.Image)
        Me.btnSearchDestin.Location = New System.Drawing.Point(199, 61)
        Me.btnSearchDestin.Name = "btnSearchDestin"
        Me.btnSearchDestin.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchDestin.TabIndex = 3
        Me.btnSearchDestin.UseVisualStyleBackColor = True
        '
        'btnSearchCompany
        '
        Me.btnSearchCompany.Image = CType(resources.GetObject("btnSearchCompany.Image"), System.Drawing.Image)
        Me.btnSearchCompany.Location = New System.Drawing.Point(199, 39)
        Me.btnSearchCompany.Name = "btnSearchCompany"
        Me.btnSearchCompany.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCompany.TabIndex = 2
        Me.btnSearchCompany.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(12, 162)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 13)
        Me.Label11.TabIndex = 145
        Me.Label11.Text = "Shipment Term"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 139)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(84, 13)
        Me.Label10.TabIndex = 144
        Me.Label10.Text = "Shipment Period"
        '
        'txtCompany_Code
        '
        Me.txtCompany_Code.Location = New System.Drawing.Point(137, 39)
        Me.txtCompany_Code.MaxLength = 5
        Me.txtCompany_Code.Name = "txtCompany_Code"
        Me.txtCompany_Code.ReadOnly = True
        Me.txtCompany_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtCompany_Code.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 140
        Me.Label3.Text = "Port"
        '
        'txtPO_NO
        '
        Me.txtPO_NO.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.txtPO_NO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPO_NO.Location = New System.Drawing.Point(137, 15)
        Me.txtPO_NO.MaxLength = 20
        Me.txtPO_NO.Name = "txtPO_NO"
        Me.txtPO_NO.Size = New System.Drawing.Size(178, 20)
        Me.txtPO_NO.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 108
        Me.Label2.Text = "Company"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "Destination"
        '
        'txtPlant_Code
        '
        Me.txtPlant_Code.Location = New System.Drawing.Point(137, 62)
        Me.txtPlant_Code.MaxLength = 1
        Me.txtPlant_Code.Name = "txtPlant_Code"
        Me.txtPlant_Code.ReadOnly = True
        Me.txtPlant_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtPlant_Code.TabIndex = 104
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 13)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "PO No."
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(520, 32)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(494, 251)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage1.Controls.Add(Me.btnSchedulePO)
        Me.TabPage1.Controls.Add(Me.ETD)
        Me.TabPage1.Controls.Add(Me.Label38)
        Me.TabPage1.Controls.Add(Me.Label39)
        Me.TabPage1.Controls.Add(Me.Label37)
        Me.TabPage1.Controls.Add(Me.prodname)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.produsen)
        Me.TabPage1.Controls.Add(Me.Label20)
        Me.TabPage1.Controls.Add(Me.btnCD)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.lblKurs)
        Me.TabPage1.Controls.Add(Me.lblSupplierName)
        Me.TabPage1.Controls.Add(Me.Label12)
        Me.TabPage1.Controls.Add(Me.btnSearchCurrency)
        Me.TabPage1.Controls.Add(Me.txtCurrency_Code)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtTotal)
        Me.TabPage1.Controls.Add(Me.Label27)
        Me.TabPage1.Controls.Add(Me.txtTolerable_Del)
        Me.TabPage1.Controls.Add(Me.Label24)
        Me.TabPage1.Controls.Add(Me.txtPR_No)
        Me.TabPage1.Controls.Add(Me.txtContract_No)
        Me.TabPage1.Controls.Add(Me.Label25)
        Me.TabPage1.Controls.Add(Me.btnSearchSupplier)
        Me.TabPage1.Controls.Add(Me.txtSupplier_Code)
        Me.TabPage1.Controls.Add(Me.Label26)
        Me.TabPage1.Controls.Add(Me.txtIPA_No)
        Me.TabPage1.Controls.Add(Me.Label28)
        Me.TabPage1.Controls.Add(Me.Label29)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(486, 225)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "More Data"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnSchedulePO
        '
        Me.btnSchedulePO.Image = CType(resources.GetObject("btnSchedulePO.Image"), System.Drawing.Image)
        Me.btnSchedulePO.Location = New System.Drawing.Point(117, 9)
        Me.btnSchedulePO.Name = "btnSchedulePO"
        Me.btnSchedulePO.Size = New System.Drawing.Size(22, 20)
        Me.btnSchedulePO.TabIndex = 183
        Me.btnSchedulePO.UseVisualStyleBackColor = True
        '
        'ETD
        '
        Me.ETD.Checked = False
        Me.ETD.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.ETD.Location = New System.Drawing.Point(366, 10)
        Me.ETD.Name = "ETD"
        Me.ETD.ShowCheckBox = True
        Me.ETD.Size = New System.Drawing.Size(100, 20)
        Me.ETD.TabIndex = 184
        Me.ETD.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        Me.ETD.Visible = False
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.Red
        Me.Label38.Location = New System.Drawing.Point(102, 148)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(14, 17)
        Me.Label38.TabIndex = 219
        Me.Label38.Text = "*"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(12, 12)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(99, 13)
        Me.Label39.TabIndex = 183
        Me.Label39.Text = "Shipment Schedule"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Red
        Me.Label37.Location = New System.Drawing.Point(56, 84)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(14, 17)
        Me.Label37.TabIndex = 218
        Me.Label37.Text = "*"
        '
        'prodname
        '
        Me.prodname.AutoSize = True
        Me.prodname.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.prodname.Location = New System.Drawing.Point(211, 106)
        Me.prodname.Name = "prodname"
        Me.prodname.Size = New System.Drawing.Size(96, 13)
        Me.prodname.TabIndex = 217
        Me.prodname.Text = "Produsen Name"
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(181, 102)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 20)
        Me.Button1.TabIndex = 214
        Me.Button1.UseVisualStyleBackColor = True
        '
        'produsen
        '
        Me.produsen.Location = New System.Drawing.Point(117, 102)
        Me.produsen.MaxLength = 5
        Me.produsen.Name = "produsen"
        Me.produsen.ReadOnly = True
        Me.produsen.Size = New System.Drawing.Size(58, 20)
        Me.produsen.TabIndex = 216
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(12, 108)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(52, 13)
        Me.Label20.TabIndex = 215
        Me.Label20.Text = "Produsen"
        '
        'btnCD
        '
        Me.btnCD.Image = CType(resources.GetObject("btnCD.Image"), System.Drawing.Image)
        Me.btnCD.Location = New System.Drawing.Point(370, 125)
        Me.btnCD.Name = "btnCD"
        Me.btnCD.Size = New System.Drawing.Size(22, 20)
        Me.btnCD.TabIndex = 213
        Me.btnCD.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(187, 152)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(16, 13)
        Me.Label18.TabIndex = 212
        Me.Label18.Text = "%"
        '
        'lblKurs
        '
        Me.lblKurs.AutoSize = True
        Me.lblKurs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKurs.Location = New System.Drawing.Point(272, 173)
        Me.lblKurs.Name = "lblKurs"
        Me.lblKurs.Size = New System.Drawing.Size(14, 13)
        Me.lblKurs.TabIndex = 211
        Me.lblKurs.Text = "0"
        Me.lblKurs.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSupplierName
        '
        Me.lblSupplierName.AutoSize = True
        Me.lblSupplierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.Location = New System.Drawing.Point(211, 83)
        Me.lblSupplierName.Name = "lblSupplierName"
        Me.lblSupplierName.Size = New System.Drawing.Size(89, 13)
        Me.lblSupplierName.TabIndex = 210
        Me.lblSupplierName.Text = "Supplier Name"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(209, 173)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(45, 13)
        Me.Label12.TabIndex = 208
        Me.Label12.Text = "Rate :   "
        '
        'btnSearchCurrency
        '
        Me.btnSearchCurrency.Image = CType(resources.GetObject("btnSearchCurrency.Image"), System.Drawing.Image)
        Me.btnSearchCurrency.Location = New System.Drawing.Point(179, 172)
        Me.btnSearchCurrency.Name = "btnSearchCurrency"
        Me.btnSearchCurrency.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCurrency.TabIndex = 5
        Me.btnSearchCurrency.UseVisualStyleBackColor = True
        '
        'txtCurrency_Code
        '
        Me.txtCurrency_Code.Location = New System.Drawing.Point(117, 172)
        Me.txtCurrency_Code.MaxLength = 1
        Me.txtCurrency_Code.Name = "txtCurrency_Code"
        Me.txtCurrency_Code.ReadOnly = True
        Me.txtCurrency_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtCurrency_Code.TabIndex = 206
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 175)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 205
        Me.Label8.Text = "Currency"
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(117, 195)
        Me.txtTotal.MaxLength = 15
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(124, 20)
        Me.txtTotal.TabIndex = 204
        Me.txtTotal.Text = "0000000000"
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(12, 197)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(76, 13)
        Me.Label27.TabIndex = 203
        Me.Label27.Text = "Total Amount  "
        '
        'txtTolerable_Del
        '
        Me.txtTolerable_Del.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTolerable_Del.Location = New System.Drawing.Point(117, 149)
        Me.txtTolerable_Del.MaxLength = 5
        Me.txtTolerable_Del.Name = "txtTolerable_Del"
        Me.txtTolerable_Del.Size = New System.Drawing.Size(69, 20)
        Me.txtTolerable_Del.TabIndex = 4
        Me.txtTolerable_Del.Text = "10,00"
        Me.txtTolerable_Del.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 155)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(92, 13)
        Me.Label24.TabIndex = 201
        Me.Label24.Text = "Tolerable Delivery"
        '
        'txtPR_No
        '
        Me.txtPR_No.Location = New System.Drawing.Point(117, 56)
        Me.txtPR_No.MaxLength = 40
        Me.txtPR_No.Name = "txtPR_No"
        Me.txtPR_No.Size = New System.Drawing.Size(248, 20)
        Me.txtPR_No.TabIndex = 1
        '
        'txtContract_No
        '
        Me.txtContract_No.Location = New System.Drawing.Point(117, 125)
        Me.txtContract_No.MaxLength = 40
        Me.txtContract_No.Name = "txtContract_No"
        Me.txtContract_No.Size = New System.Drawing.Size(248, 20)
        Me.txtContract_No.TabIndex = 3
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(12, 131)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(64, 13)
        Me.Label25.TabIndex = 198
        Me.Label25.Text = "Contract No"
        '
        'btnSearchSupplier
        '
        Me.btnSearchSupplier.Image = CType(resources.GetObject("btnSearchSupplier.Image"), System.Drawing.Image)
        Me.btnSearchSupplier.Location = New System.Drawing.Point(181, 79)
        Me.btnSearchSupplier.Name = "btnSearchSupplier"
        Me.btnSearchSupplier.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchSupplier.TabIndex = 2
        Me.btnSearchSupplier.UseVisualStyleBackColor = True
        '
        'txtSupplier_Code
        '
        Me.txtSupplier_Code.Location = New System.Drawing.Point(117, 79)
        Me.txtSupplier_Code.MaxLength = 5
        Me.txtSupplier_Code.Name = "txtSupplier_Code"
        Me.txtSupplier_Code.ReadOnly = True
        Me.txtSupplier_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtSupplier_Code.TabIndex = 195
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(12, 85)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(45, 13)
        Me.Label26.TabIndex = 194
        Me.Label26.Text = "Supplier"
        '
        'txtIPA_No
        '
        Me.txtIPA_No.Location = New System.Drawing.Point(117, 33)
        Me.txtIPA_No.MaxLength = 40
        Me.txtIPA_No.Name = "txtIPA_No"
        Me.txtIPA_No.Size = New System.Drawing.Size(248, 20)
        Me.txtIPA_No.TabIndex = 0
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(12, 62)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(39, 13)
        Me.Label28.TabIndex = 192
        Me.Label28.Text = "PR No"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(12, 38)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(41, 13)
        Me.Label29.TabIndex = 191
        Me.Label29.Text = "IPA No"
        '
        'TabPage2
        '
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage2.Controls.Add(Me.crtcode)
        Me.TabPage2.Controls.Add(Me.DTCreated)
        Me.TabPage2.Controls.Add(Me.CTFun)
        Me.TabPage2.Controls.Add(Me.CTApp)
        Me.TabPage2.Controls.Add(Me.CTpur)
        Me.TabPage2.Controls.Add(Me.DtFundApp)
        Me.TabPage2.Controls.Add(Me.Label16)
        Me.TabPage2.Controls.Add(Me.btnUserFun)
        Me.TabPage2.Controls.Add(Me.txtFUNDAPPBY)
        Me.TabPage2.Controls.Add(Me.Label17)
        Me.TabPage2.Controls.Add(Me.DtApproved)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.btnUserApp)
        Me.TabPage2.Controls.Add(Me.txtAPPROVEDBY)
        Me.TabPage2.Controls.Add(Me.Label15)
        Me.TabPage2.Controls.Add(Me.DtPurchased)
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.btnUserPur)
        Me.TabPage2.Controls.Add(Me.txtPURCHASEDBY)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.TxtStatus)
        Me.TabPage2.Controls.Add(Me.Label31)
        Me.TabPage2.Controls.Add(Me.Label32)
        Me.TabPage2.Controls.Add(Me.Label33)
        Me.TabPage2.Controls.Add(Me.txtCREATEDBY)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(486, 225)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "User Data"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'crtcode
        '
        Me.crtcode.Location = New System.Drawing.Point(90, 36)
        Me.crtcode.MaxLength = 1
        Me.crtcode.Name = "crtcode"
        Me.crtcode.ReadOnly = True
        Me.crtcode.Size = New System.Drawing.Size(41, 20)
        Me.crtcode.TabIndex = 225
        Me.crtcode.Visible = False
        '
        'DTCreated
        '
        Me.DTCreated.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTCreated.Location = New System.Drawing.Point(137, 13)
        Me.DTCreated.Name = "DTCreated"
        Me.DTCreated.Size = New System.Drawing.Size(83, 20)
        Me.DTCreated.TabIndex = 0
        '
        'CTFun
        '
        Me.CTFun.Location = New System.Drawing.Point(90, 150)
        Me.CTFun.MaxLength = 1
        Me.CTFun.Name = "CTFun"
        Me.CTFun.ReadOnly = True
        Me.CTFun.Size = New System.Drawing.Size(41, 20)
        Me.CTFun.TabIndex = 224
        Me.CTFun.Visible = False
        '
        'CTApp
        '
        Me.CTApp.Location = New System.Drawing.Point(90, 128)
        Me.CTApp.MaxLength = 1
        Me.CTApp.Name = "CTApp"
        Me.CTApp.ReadOnly = True
        Me.CTApp.Size = New System.Drawing.Size(41, 20)
        Me.CTApp.TabIndex = 223
        Me.CTApp.Visible = False
        '
        'CTpur
        '
        Me.CTpur.Location = New System.Drawing.Point(90, 105)
        Me.CTpur.MaxLength = 1
        Me.CTpur.Name = "CTpur"
        Me.CTpur.ReadOnly = True
        Me.CTpur.Size = New System.Drawing.Size(41, 20)
        Me.CTpur.TabIndex = 222
        Me.CTpur.Visible = False
        '
        'DtFundApp
        '
        Me.DtFundApp.Checked = False
        Me.DtFundApp.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtFundApp.Location = New System.Drawing.Point(379, 146)
        Me.DtFundApp.Name = "DtFundApp"
        Me.DtFundApp.ShowCheckBox = True
        Me.DtFundApp.Size = New System.Drawing.Size(99, 20)
        Me.DtFundApp.TabIndex = 8
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(337, 153)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(30, 13)
        Me.Label16.TabIndex = 220
        Me.Label16.Text = "Date"
        '
        'btnUserFun
        '
        Me.btnUserFun.Image = CType(resources.GetObject("btnUserFun.Image"), System.Drawing.Image)
        Me.btnUserFun.Location = New System.Drawing.Point(310, 146)
        Me.btnUserFun.Name = "btnUserFun"
        Me.btnUserFun.Size = New System.Drawing.Size(22, 20)
        Me.btnUserFun.TabIndex = 7
        Me.btnUserFun.UseVisualStyleBackColor = True
        '
        'txtFUNDAPPBY
        '
        Me.txtFUNDAPPBY.Location = New System.Drawing.Point(137, 147)
        Me.txtFUNDAPPBY.MaxLength = 1
        Me.txtFUNDAPPBY.Name = "txtFUNDAPPBY"
        Me.txtFUNDAPPBY.ReadOnly = True
        Me.txtFUNDAPPBY.Size = New System.Drawing.Size(170, 20)
        Me.txtFUNDAPPBY.TabIndex = 218
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 153)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(70, 13)
        Me.Label17.TabIndex = 217
        Me.Label17.Text = "Fund App. by"
        '
        'DtApproved
        '
        Me.DtApproved.Checked = False
        Me.DtApproved.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtApproved.Location = New System.Drawing.Point(379, 124)
        Me.DtApproved.Name = "DtApproved"
        Me.DtApproved.ShowCheckBox = True
        Me.DtApproved.Size = New System.Drawing.Size(99, 20)
        Me.DtApproved.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(337, 131)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(30, 13)
        Me.Label14.TabIndex = 215
        Me.Label14.Text = "Date"
        '
        'btnUserApp
        '
        Me.btnUserApp.Image = CType(resources.GetObject("btnUserApp.Image"), System.Drawing.Image)
        Me.btnUserApp.Location = New System.Drawing.Point(310, 124)
        Me.btnUserApp.Name = "btnUserApp"
        Me.btnUserApp.Size = New System.Drawing.Size(22, 20)
        Me.btnUserApp.TabIndex = 5
        Me.btnUserApp.UseVisualStyleBackColor = True
        '
        'txtAPPROVEDBY
        '
        Me.txtAPPROVEDBY.Location = New System.Drawing.Point(137, 125)
        Me.txtAPPROVEDBY.MaxLength = 1
        Me.txtAPPROVEDBY.Name = "txtAPPROVEDBY"
        Me.txtAPPROVEDBY.ReadOnly = True
        Me.txtAPPROVEDBY.Size = New System.Drawing.Size(170, 20)
        Me.txtAPPROVEDBY.TabIndex = 213
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(12, 131)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(67, 13)
        Me.Label15.TabIndex = 212
        Me.Label15.Text = "Approved by"
        '
        'DtPurchased
        '
        Me.DtPurchased.Checked = False
        Me.DtPurchased.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DtPurchased.Location = New System.Drawing.Point(379, 102)
        Me.DtPurchased.Name = "DtPurchased"
        Me.DtPurchased.ShowCheckBox = True
        Me.DtPurchased.Size = New System.Drawing.Size(99, 20)
        Me.DtPurchased.TabIndex = 4
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(337, 109)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(30, 13)
        Me.Label13.TabIndex = 210
        Me.Label13.Text = "Date"
        '
        'btnUserPur
        '
        Me.btnUserPur.Image = CType(resources.GetObject("btnUserPur.Image"), System.Drawing.Image)
        Me.btnUserPur.Location = New System.Drawing.Point(310, 102)
        Me.btnUserPur.Name = "btnUserPur"
        Me.btnUserPur.Size = New System.Drawing.Size(22, 20)
        Me.btnUserPur.TabIndex = 3
        Me.btnUserPur.UseVisualStyleBackColor = True
        '
        'txtPURCHASEDBY
        '
        Me.txtPURCHASEDBY.Location = New System.Drawing.Point(137, 103)
        Me.txtPURCHASEDBY.MaxLength = 1
        Me.txtPURCHASEDBY.Name = "txtPURCHASEDBY"
        Me.txtPURCHASEDBY.ReadOnly = True
        Me.txtPURCHASEDBY.Size = New System.Drawing.Size(170, 20)
        Me.txtPURCHASEDBY.TabIndex = 208
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 207
        Me.Label9.Text = "Purchased by"
        '
        'TxtStatus
        '
        Me.TxtStatus.Location = New System.Drawing.Point(137, 59)
        Me.TxtStatus.MaxLength = 5
        Me.TxtStatus.Name = "TxtStatus"
        Me.TxtStatus.ReadOnly = True
        Me.TxtStatus.Size = New System.Drawing.Size(170, 20)
        Me.TxtStatus.TabIndex = 2
        Me.TxtStatus.Text = "Open"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(12, 65)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(37, 13)
        Me.Label31.TabIndex = 204
        Me.Label31.Text = "Status"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(12, 19)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(70, 13)
        Me.Label32.TabIndex = 202
        Me.Label32.Text = "Created Date"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(12, 42)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(59, 13)
        Me.Label33.TabIndex = 203
        Me.Label33.Text = "Created By"
        '
        'txtCREATEDBY
        '
        Me.txtCREATEDBY.Location = New System.Drawing.Point(137, 36)
        Me.txtCREATEDBY.MaxLength = 1
        Me.txtCREATEDBY.Name = "txtCREATEDBY"
        Me.txtCREATEDBY.ReadOnly = True
        Me.txtCREATEDBY.Size = New System.Drawing.Size(170, 20)
        Me.txtCREATEDBY.TabIndex = 1
        '
        'DGVDetail
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DGVDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DGVDetail.DefaultCellStyle = DataGridViewCellStyle2
        Me.DGVDetail.Location = New System.Drawing.Point(12, 289)
        Me.DGVDetail.Name = "DGVDetail"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DGVDetail.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DGVDetail.Size = New System.Drawing.Size(1002, 265)
        Me.DGVDetail.TabIndex = 153
        '
        'FrmPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1021, 566)
        Me.Controls.Add(Me.DGVDetail)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmPO"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Purchase Order"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.DGVDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPort_Code As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchDestin As System.Windows.Forms.Button
    Friend WithEvents btnSearchCompany As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPO_NO As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPlant_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DTPeriodeFR As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearchPort As System.Windows.Forms.Button
    Friend WithEvents btnSearchInsurance As System.Windows.Forms.Button
    Friend WithEvents TxtInsurance As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnSearchPayment As System.Windows.Forms.Button
    Friend WithEvents txtPayment_Code As System.Windows.Forms.TextBox
    Friend WithEvents cbShipTerm As System.Windows.Forms.ComboBox
    Friend WithEvents DTPeriodeTO As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DtFundApp As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnUserFun As System.Windows.Forms.Button
    Friend WithEvents txtFUNDAPPBY As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents DtApproved As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnUserApp As System.Windows.Forms.Button
    Friend WithEvents txtAPPROVEDBY As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DtPurchased As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnUserPur As System.Windows.Forms.Button
    Friend WithEvents txtPURCHASEDBY As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtCREATEDBY As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtTolerable_Del As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtPR_No As System.Windows.Forms.TextBox
    Friend WithEvents txtContract_No As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnSearchSupplier As System.Windows.Forms.Button
    Friend WithEvents txtSupplier_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtIPA_No As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSearchCurrency As System.Windows.Forms.Button
    Friend WithEvents txtCurrency_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSearchMat As System.Windows.Forms.Button
    Friend WithEvents btnListPO As System.Windows.Forms.Button
    Friend WithEvents TextBox As System.Windows.Forms.TextBox
    Friend WithEvents lblPort_Name As System.Windows.Forms.Label
    Friend WithEvents lblPlant_Name As System.Windows.Forms.Label
    Friend WithEvents lblCompany_Name As System.Windows.Forms.Label
    Friend WithEvents lblInsurance_Desc As System.Windows.Forms.Label
    Friend WithEvents lblPayment_Name As System.Windows.Forms.Label
    Friend WithEvents lblSupplierName As System.Windows.Forms.Label
    Friend WithEvents lblKurs As System.Windows.Forms.Label
    Friend WithEvents txtCompany_Code As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchOri As System.Windows.Forms.Button
    Friend WithEvents CTFun As System.Windows.Forms.TextBox
    Friend WithEvents CTApp As System.Windows.Forms.TextBox
    Friend WithEvents CTpur As System.Windows.Forms.TextBox
    Friend WithEvents DTCreated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents DGVDetail As System.Windows.Forms.DataGridView
    Friend WithEvents btnCD As System.Windows.Forms.Button
    Friend WithEvents prodname As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents produsen As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblLoadPort_Name As System.Windows.Forms.Label
    Friend WithEvents btnSearchLoadPort As System.Windows.Forms.Button
    Friend WithEvents txtLoadPort_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents ETD As System.Windows.Forms.DateTimePicker
    Friend WithEvents crtcode As System.Windows.Forms.TextBox
    Friend WithEvents btnSchedulePO As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnClosing As System.Windows.Forms.ToolStripButton
End Class
