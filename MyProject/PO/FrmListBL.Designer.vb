﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmListBL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmListBL))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnViewBL = New System.Windows.Forms.ToolStripButton
        Me.btnSearchSupplier = New System.Windows.Forms.Button
        Me.Label26 = New System.Windows.Forms.Label
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
        Me.txtSupplier_Code = New System.Windows.Forms.TextBox
        Me.txtIPA_No = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.DGVHeader = New System.Windows.Forms.DataGridView
        Me.DGVDetail = New System.Windows.Forms.DataGridView
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtCreatedby = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.DTCreated1 = New System.Windows.Forms.DateTimePicker
        Me.DTCreated2 = New System.Windows.Forms.DateTimePicker
        Me.btnUserPur = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPlant_Code = New System.Windows.Forms.TextBox
        Me.btnSearchPlant = New System.Windows.Forms.Button
        Me.lblPlant_Name = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSuppCode = New System.Windows.Forms.TextBox
        Me.btnSup = New System.Windows.Forms.Button
        Me.lblSuppName = New System.Windows.Forms.Label
        Me.txtPONO = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnFind = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.userct = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.v_blno = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtBL = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPL = New System.Windows.Forms.TextBox
        Me.v_shipno = New System.Windows.Forms.TextBox
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DGVHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnViewBL})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(905, 25)
        Me.ToolStrip1.TabIndex = 1
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
        'btnViewBL
        '
        Me.btnViewBL.Enabled = False
        Me.btnViewBL.Image = Global.poim.My.Resources.Resources.search
        Me.btnViewBL.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnViewBL.Name = "btnViewBL"
        Me.btnViewBL.Size = New System.Drawing.Size(73, 22)
        Me.btnViewBL.Text = "View B/L"
        '
        'btnSearchSupplier
        '
        Me.btnSearchSupplier.Image = CType(resources.GetObject("btnSearchSupplier.Image"), System.Drawing.Image)
        Me.btnSearchSupplier.Location = New System.Drawing.Point(199, 58)
        Me.btnSearchSupplier.Name = "btnSearchSupplier"
        Me.btnSearchSupplier.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchSupplier.TabIndex = 197
        Me.btnSearchSupplier.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(12, 65)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(45, 13)
        Me.Label26.TabIndex = 194
        Me.Label26.Text = "Supplier"
        '
        'lblKurs
        '
        Me.lblKurs.AutoSize = True
        Me.lblKurs.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKurs.Location = New System.Drawing.Point(292, 153)
        Me.lblKurs.Name = "lblKurs"
        Me.lblKurs.Size = New System.Drawing.Size(11, 13)
        Me.lblKurs.TabIndex = 211
        Me.lblKurs.Text = "-"
        Me.lblKurs.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSupplierName
        '
        Me.lblSupplierName.AutoSize = True
        Me.lblSupplierName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.Location = New System.Drawing.Point(229, 62)
        Me.lblSupplierName.Name = "lblSupplierName"
        Me.lblSupplierName.Size = New System.Drawing.Size(89, 13)
        Me.lblSupplierName.TabIndex = 210
        Me.lblSupplierName.Text = "Supplier Name"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(229, 153)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 13)
        Me.Label12.TabIndex = 208
        Me.Label12.Text = "Rate :  Rp. "
        '
        'btnSearchCurrency
        '
        Me.btnSearchCurrency.Image = CType(resources.GetObject("btnSearchCurrency.Image"), System.Drawing.Image)
        Me.btnSearchCurrency.Location = New System.Drawing.Point(199, 148)
        Me.btnSearchCurrency.Name = "btnSearchCurrency"
        Me.btnSearchCurrency.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchCurrency.TabIndex = 207
        Me.btnSearchCurrency.UseVisualStyleBackColor = True
        '
        'txtCurrency_Code
        '
        Me.txtCurrency_Code.Location = New System.Drawing.Point(137, 148)
        Me.txtCurrency_Code.MaxLength = 1
        Me.txtCurrency_Code.Name = "txtCurrency_Code"
        Me.txtCurrency_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtCurrency_Code.TabIndex = 206
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 155)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 205
        Me.Label8.Text = "Currency"
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(137, 171)
        Me.txtTotal.MaxLength = 1
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(167, 26)
        Me.txtTotal.TabIndex = 204
        Me.txtTotal.Text = "0000000000"
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(11, 174)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(126, 20)
        Me.Label27.TabIndex = 203
        Me.Label27.Text = "Total Amount  "
        '
        'txtTolerable_Del
        '
        Me.txtTolerable_Del.Location = New System.Drawing.Point(137, 103)
        Me.txtTolerable_Del.MaxLength = 1
        Me.txtTolerable_Del.Name = "txtTolerable_Del"
        Me.txtTolerable_Del.Size = New System.Drawing.Size(84, 20)
        Me.txtTolerable_Del.TabIndex = 202
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(12, 109)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(92, 13)
        Me.Label24.TabIndex = 201
        Me.Label24.Text = "Tolerable Delivery"
        '
        'txtPR_No
        '
        Me.txtPR_No.Location = New System.Drawing.Point(137, 36)
        Me.txtPR_No.MaxLength = 1
        Me.txtPR_No.Name = "txtPR_No"
        Me.txtPR_No.Size = New System.Drawing.Size(84, 20)
        Me.txtPR_No.TabIndex = 200
        '
        'txtContract_No
        '
        Me.txtContract_No.Location = New System.Drawing.Point(137, 82)
        Me.txtContract_No.MaxLength = 1
        Me.txtContract_No.Name = "txtContract_No"
        Me.txtContract_No.Size = New System.Drawing.Size(84, 20)
        Me.txtContract_No.TabIndex = 199
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(12, 88)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(64, 13)
        Me.Label25.TabIndex = 198
        Me.Label25.Text = "Contract No"
        '
        'txtSupplier_Code
        '
        Me.txtSupplier_Code.Location = New System.Drawing.Point(137, 59)
        Me.txtSupplier_Code.MaxLength = 1
        Me.txtSupplier_Code.Name = "txtSupplier_Code"
        Me.txtSupplier_Code.Size = New System.Drawing.Size(58, 20)
        Me.txtSupplier_Code.TabIndex = 195
        '
        'txtIPA_No
        '
        Me.txtIPA_No.Location = New System.Drawing.Point(137, 13)
        Me.txtIPA_No.MaxLength = 1
        Me.txtIPA_No.Name = "txtIPA_No"
        Me.txtIPA_No.Size = New System.Drawing.Size(84, 20)
        Me.txtIPA_No.TabIndex = 193
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(12, 42)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(39, 13)
        Me.Label28.TabIndex = 192
        Me.Label28.Text = "PR No"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(12, 19)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(41, 13)
        Me.Label29.TabIndex = 191
        Me.Label29.Text = "IPA No"
        '
        'DGVHeader
        '
        Me.DGVHeader.AllowUserToAddRows = False
        Me.DGVHeader.AllowUserToDeleteRows = False
        Me.DGVHeader.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVHeader.Location = New System.Drawing.Point(16, 308)
        Me.DGVHeader.Name = "DGVHeader"
        Me.DGVHeader.ReadOnly = True
        Me.DGVHeader.Size = New System.Drawing.Size(875, 193)
        Me.DGVHeader.TabIndex = 57
        '
        'DGVDetail
        '
        Me.DGVDetail.AllowUserToAddRows = False
        Me.DGVDetail.AllowUserToDeleteRows = False
        Me.DGVDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDetail.Location = New System.Drawing.Point(16, 534)
        Me.DGVDetail.Name = "DGVDetail"
        Me.DGVDetail.ReadOnly = True
        Me.DGVDetail.Size = New System.Drawing.Size(875, 147)
        Me.DGVDetail.TabIndex = 58
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(13, 286)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(76, 16)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "List of B/L"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(12, 512)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(76, 16)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Detail B/L"
        '
        'txtCreatedby
        '
        Me.txtCreatedby.Location = New System.Drawing.Point(137, 39)
        Me.txtCreatedby.MaxLength = 40
        Me.txtCreatedby.Name = "txtCreatedby"
        Me.txtCreatedby.ReadOnly = True
        Me.txtCreatedby.Size = New System.Drawing.Size(197, 20)
        Me.txtCreatedby.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Create Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "Create By"
        '
        'DTCreated1
        '
        Me.DTCreated1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTCreated1.Location = New System.Drawing.Point(137, 15)
        Me.DTCreated1.Name = "DTCreated1"
        Me.DTCreated1.Size = New System.Drawing.Size(90, 20)
        Me.DTCreated1.TabIndex = 55
        '
        'DTCreated2
        '
        Me.DTCreated2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTCreated2.Location = New System.Drawing.Point(244, 15)
        Me.DTCreated2.Name = "DTCreated2"
        Me.DTCreated2.Size = New System.Drawing.Size(90, 20)
        Me.DTCreated2.TabIndex = 56
        '
        'btnUserPur
        '
        Me.btnUserPur.Image = CType(resources.GetObject("btnUserPur.Image"), System.Drawing.Image)
        Me.btnUserPur.Location = New System.Drawing.Point(340, 38)
        Me.btnUserPur.Name = "btnUserPur"
        Me.btnUserPur.Size = New System.Drawing.Size(22, 20)
        Me.btnUserPur.TabIndex = 210
        Me.btnUserPur.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 212
        Me.Label3.Text = "Status"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Open", "Progress", "Closed"})
        Me.ComboBox1.Location = New System.Drawing.Point(137, 63)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(87, 21)
        Me.ComboBox1.TabIndex = 213
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 214
        Me.Label4.Text = "Plant"
        '
        'txtPlant_Code
        '
        Me.txtPlant_Code.Location = New System.Drawing.Point(137, 88)
        Me.txtPlant_Code.MaxLength = 5
        Me.txtPlant_Code.Name = "txtPlant_Code"
        Me.txtPlant_Code.Size = New System.Drawing.Size(60, 20)
        Me.txtPlant_Code.TabIndex = 215
        '
        'btnSearchPlant
        '
        Me.btnSearchPlant.Image = CType(resources.GetObject("btnSearchPlant.Image"), System.Drawing.Image)
        Me.btnSearchPlant.Location = New System.Drawing.Point(202, 87)
        Me.btnSearchPlant.Name = "btnSearchPlant"
        Me.btnSearchPlant.Size = New System.Drawing.Size(22, 20)
        Me.btnSearchPlant.TabIndex = 216
        Me.btnSearchPlant.UseVisualStyleBackColor = True
        '
        'lblPlant_Name
        '
        Me.lblPlant_Name.AutoSize = True
        Me.lblPlant_Name.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlant_Name.Location = New System.Drawing.Point(229, 94)
        Me.lblPlant_Name.Name = "lblPlant_Name"
        Me.lblPlant_Name.Size = New System.Drawing.Size(36, 13)
        Me.lblPlant_Name.TabIndex = 217
        Me.lblPlant_Name.Text = "Plant"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 118)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 218
        Me.Label6.Text = "Supplier"
        '
        'txtSuppCode
        '
        Me.txtSuppCode.Location = New System.Drawing.Point(137, 112)
        Me.txtSuppCode.MaxLength = 1
        Me.txtSuppCode.Name = "txtSuppCode"
        Me.txtSuppCode.Size = New System.Drawing.Size(60, 20)
        Me.txtSuppCode.TabIndex = 219
        '
        'btnSup
        '
        Me.btnSup.Image = CType(resources.GetObject("btnSup.Image"), System.Drawing.Image)
        Me.btnSup.Location = New System.Drawing.Point(201, 112)
        Me.btnSup.Name = "btnSup"
        Me.btnSup.Size = New System.Drawing.Size(22, 20)
        Me.btnSup.TabIndex = 220
        Me.btnSup.UseVisualStyleBackColor = True
        '
        'lblSuppName
        '
        Me.lblSuppName.AutoSize = True
        Me.lblSuppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuppName.Location = New System.Drawing.Point(229, 118)
        Me.lblSuppName.Name = "lblSuppName"
        Me.lblSuppName.Size = New System.Drawing.Size(89, 13)
        Me.lblSuppName.TabIndex = 221
        Me.lblSuppName.Text = "Supplier Name"
        '
        'txtPONO
        '
        Me.txtPONO.Location = New System.Drawing.Point(137, 184)
        Me.txtPONO.MaxLength = 40
        Me.txtPONO.Name = "txtPONO"
        Me.txtPONO.Size = New System.Drawing.Size(197, 20)
        Me.txtPONO.TabIndex = 226
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 192)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 13)
        Me.Label10.TabIndex = 227
        Me.Label10.Text = "PO No."
        '
        'btnFind
        '
        Me.btnFind.Location = New System.Drawing.Point(137, 210)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(102, 24)
        Me.btnFind.TabIndex = 228
        Me.btnFind.Text = "Find Data"
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(230, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(13, 13)
        Me.Label14.TabIndex = 229
        Me.Label14.Text = "_"
        '
        'userct
        '
        Me.userct.Location = New System.Drawing.Point(88, 40)
        Me.userct.Name = "userct"
        Me.userct.Size = New System.Drawing.Size(38, 20)
        Me.userct.TabIndex = 230
        Me.userct.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.v_blno)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtBL)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtPL)
        Me.GroupBox1.Controls.Add(Me.v_shipno)
        Me.GroupBox1.Controls.Add(Me.userct)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.btnFind)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtPONO)
        Me.GroupBox1.Controls.Add(Me.lblSuppName)
        Me.GroupBox1.Controls.Add(Me.btnSup)
        Me.GroupBox1.Controls.Add(Me.txtSuppCode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblPlant_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchPlant)
        Me.GroupBox1.Controls.Add(Me.txtPlant_Code)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnUserPur)
        Me.GroupBox1.Controls.Add(Me.DTCreated2)
        Me.GroupBox1.Controls.Add(Me.DTCreated1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCreatedby)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(875, 246)
        Me.GroupBox1.TabIndex = 56
        Me.GroupBox1.TabStop = False
        '
        'v_blno
        '
        Me.v_blno.Location = New System.Drawing.Point(431, 138)
        Me.v_blno.Name = "v_blno"
        Me.v_blno.Size = New System.Drawing.Size(67, 20)
        Me.v_blno.TabIndex = 236
        Me.v_blno.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 166)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 235
        Me.Label7.Text = "BL No."
        '
        'txtBL
        '
        Me.txtBL.Location = New System.Drawing.Point(137, 160)
        Me.txtBL.MaxLength = 40
        Me.txtBL.Name = "txtBL"
        Me.txtBL.Size = New System.Drawing.Size(197, 20)
        Me.txtBL.TabIndex = 234
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 233
        Me.Label5.Text = "Packing List"
        '
        'txtPL
        '
        Me.txtPL.Location = New System.Drawing.Point(137, 136)
        Me.txtPL.MaxLength = 40
        Me.txtPL.Name = "txtPL"
        Me.txtPL.Size = New System.Drawing.Size(197, 20)
        Me.txtPL.TabIndex = 232
        '
        'v_shipno
        '
        Me.v_shipno.Location = New System.Drawing.Point(431, 112)
        Me.v_shipno.Name = "v_shipno"
        Me.v_shipno.Size = New System.Drawing.Size(67, 20)
        Me.v_shipno.TabIndex = 231
        Me.v_shipno.Visible = False
        '
        'FrmListBL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(905, 693)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.DGVDetail)
        Me.Controls.Add(Me.DGVHeader)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "FrmListBL"
        Me.ShowIcon = False
        Me.Text = "Find B/L"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DGVHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    'Friend WithEvents ImprDataSetpack As POIM.imprDataSetpack
    'Friend WithEvents TbmpackingBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents Tbm_packingTableAdapter As POIM.imprDataSetpackTableAdapters.tbm_packingTableAdapter
    Friend WithEvents btnViewBL As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchSupplier As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblKurs As System.Windows.Forms.Label
    Friend WithEvents lblSupplierName As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnSearchCurrency As System.Windows.Forms.Button
    Friend WithEvents txtCurrency_Code As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtTolerable_Del As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtPR_No As System.Windows.Forms.TextBox
    Friend WithEvents txtContract_No As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtSupplier_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtIPA_No As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents DGVHeader As System.Windows.Forms.DataGridView
    Friend WithEvents DGVDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCreatedby As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DTCreated1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTCreated2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnUserPur As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPlant_Code As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchPlant As System.Windows.Forms.Button
    Friend WithEvents lblPlant_Name As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSuppCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSup As System.Windows.Forms.Button
    Friend WithEvents lblSuppName As System.Windows.Forms.Label
    Friend WithEvents txtPONO As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnFind As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents userct As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents v_shipno As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtBL As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPL As System.Windows.Forms.TextBox
    Friend WithEvents v_blno As System.Windows.Forms.TextBox
End Class
