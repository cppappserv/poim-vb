<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBAPB
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtPOItem = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.ReceivedDt = New System.Windows.Forms.DateTimePicker
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.txtShipmentNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtTruck = New System.Windows.Forms.TextBox
        Me.lblGroupName = New System.Windows.Forms.Label
        Me.btnPO = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtContainer = New System.Windows.Forms.TextBox
        Me.txtShipNo = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtPO = New System.Windows.Forms.TextBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtExpedition = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtVessel = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtLines = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPort = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSupplier = New System.Windows.Forms.TextBox
        Me.txtBL = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.dtETD = New System.Windows.Forms.DateTimePicker
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.crtcode = New System.Windows.Forms.TextBox
        Me.DTCreated = New System.Windows.Forms.DateTimePicker
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.txtCREATEDBY = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBox9 = New System.Windows.Forms.TextBox
        Me.TextBox10 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblHist = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.dtETA1 = New System.Windows.Forms.DateTimePicker
        Me.dtDeliver1 = New System.Windows.Forms.DateTimePicker
        Me.dtClear1 = New System.Windows.Forms.DateTimePicker
        Me.GRQty = New System.Windows.Forms.MaskedTextBox
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.lblClear1 = New System.Windows.Forms.Label
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 342)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(502, 211)
        Me.DataGridView1.TabIndex = 8
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnClose, Me.ToolStripSeparator1, Me.btnNew, Me.ToolStripSeparator2, Me.btnSave, Me.ToolStripSeparator3, Me.btnDelete})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1021, 25)
        Me.ToolStrip1.TabIndex = 9
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnClose
        '
        Me.btnClose.AutoSize = False
        Me.btnClose.Image = Global.poim.My.Resources.Resources.Critical
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtPOItem)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.ReceivedDt)
        Me.GroupBox2.Controls.Add(Me.DataGridView2)
        Me.GroupBox2.Controls.Add(Me.txtShipmentNo)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtTruck)
        Me.GroupBox2.Controls.Add(Me.lblGroupName)
        Me.GroupBox2.Controls.Add(Me.btnPO)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtContainer)
        Me.GroupBox2.Controls.Add(Me.txtShipNo)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtPO)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 36)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(502, 285)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'txtPOItem
        '
        Me.txtPOItem.Location = New System.Drawing.Point(242, 40)
        Me.txtPOItem.MaxLength = 40
        Me.txtPOItem.Name = "txtPOItem"
        Me.txtPOItem.ReadOnly = True
        Me.txtPOItem.Size = New System.Drawing.Size(22, 20)
        Me.txtPOItem.TabIndex = 209
        Me.txtPOItem.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(240, 68)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(71, 12)
        Me.Label20.TabIndex = 208
        Me.Label20.Text = "*Received Date"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(16, 138)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 13)
        Me.Label16.TabIndex = 207
        Me.Label16.Text = "Detail of BAPB"
        '
        'ReceivedDt
        '
        Me.ReceivedDt.Checked = False
        Me.ReceivedDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.ReceivedDt.Location = New System.Drawing.Point(141, 63)
        Me.ReceivedDt.Name = "ReceivedDt"
        Me.ReceivedDt.ShowCheckBox = True
        Me.ReceivedDt.Size = New System.Drawing.Size(100, 20)
        Me.ReceivedDt.TabIndex = 206
        Me.ReceivedDt.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(19, 157)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(470, 115)
        Me.DataGridView2.TabIndex = 11
        '
        'txtShipmentNo
        '
        Me.txtShipmentNo.Location = New System.Drawing.Point(180, 40)
        Me.txtShipmentNo.MaxLength = 40
        Me.txtShipmentNo.Name = "txtShipmentNo"
        Me.txtShipmentNo.ReadOnly = True
        Me.txtShipmentNo.Size = New System.Drawing.Size(61, 20)
        Me.txtShipmentNo.TabIndex = 72
        Me.txtShipmentNo.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "Truck No"
        '
        'txtTruck
        '
        Me.txtTruck.Location = New System.Drawing.Point(141, 86)
        Me.txtTruck.MaxLength = 40
        Me.txtTruck.Name = "txtTruck"
        Me.txtTruck.Size = New System.Drawing.Size(203, 20)
        Me.txtTruck.TabIndex = 69
        '
        'lblGroupName
        '
        Me.lblGroupName.AutoSize = True
        Me.lblGroupName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroupName.Location = New System.Drawing.Point(218, 68)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(0, 13)
        Me.lblGroupName.TabIndex = 68
        '
        'btnPO
        '
        Me.btnPO.Image = Global.poim.My.Resources.Resources.search
        Me.btnPO.Location = New System.Drawing.Point(322, 18)
        Me.btnPO.Name = "btnPO"
        Me.btnPO.Size = New System.Drawing.Size(22, 18)
        Me.btnPO.TabIndex = 2
        Me.btnPO.TabStop = False
        Me.btnPO.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 115)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(72, 13)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Container No."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(16, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(71, 13)
        Me.Label12.TabIndex = 63
        Me.Label12.Text = "Shipment No."
        '
        'txtContainer
        '
        Me.txtContainer.Location = New System.Drawing.Point(141, 108)
        Me.txtContainer.MaxLength = 40
        Me.txtContainer.Name = "txtContainer"
        Me.txtContainer.Size = New System.Drawing.Size(203, 20)
        Me.txtContainer.TabIndex = 3
        '
        'txtShipNo
        '
        Me.txtShipNo.Location = New System.Drawing.Point(141, 40)
        Me.txtShipNo.MaxLength = 2
        Me.txtShipNo.Name = "txtShipNo"
        Me.txtShipNo.ReadOnly = True
        Me.txtShipNo.Size = New System.Drawing.Size(33, 20)
        Me.txtShipNo.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(16, 69)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(61, 13)
        Me.Label13.TabIndex = 62
        Me.Label13.Text = "BAPB Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(16, 24)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(39, 13)
        Me.Label14.TabIndex = 61
        Me.Label14.Text = "PO No"
        '
        'txtPO
        '
        Me.txtPO.Location = New System.Drawing.Point(141, 17)
        Me.txtPO.MaxLength = 20
        Me.txtPO.Name = "txtPO"
        Me.txtPO.ReadOnly = True
        Me.txtPO.Size = New System.Drawing.Size(178, 20)
        Me.txtPO.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(521, 36)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(494, 518)
        Me.TabControl1.TabIndex = 10
        '
        'TabPage1
        '
        Me.TabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Controls.Add(Me.Label17)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.txtExpedition)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.txtVessel)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.txtLines)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtPort)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtSupplier)
        Me.TabPage1.Controls.Add(Me.txtBL)
        Me.TabPage1.Controls.Add(Me.Label29)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(486, 492)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "More Data"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(12, 132)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(96, 13)
        Me.Label18.TabIndex = 2200
        Me.Label18.Text = "Notice Arrival Date"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(12, 110)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(100, 13)
        Me.Label19.TabIndex = 2199
        Me.Label19.Text = "Ship on Board Date"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(12, 289)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 13)
        Me.Label15.TabIndex = 200
        Me.Label15.Text = "Expedition"
        '
        'txtExpedition
        '
        Me.txtExpedition.Location = New System.Drawing.Point(117, 283)
        Me.txtExpedition.MaxLength = 40
        Me.txtExpedition.Name = "txtExpedition"
        Me.txtExpedition.ReadOnly = True
        Me.txtExpedition.Size = New System.Drawing.Size(248, 20)
        Me.txtExpedition.TabIndex = 199
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 266)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(38, 13)
        Me.Label7.TabIndex = 198
        Me.Label7.Text = "Vessel"
        '
        'txtVessel
        '
        Me.txtVessel.Location = New System.Drawing.Point(117, 260)
        Me.txtVessel.MaxLength = 40
        Me.txtVessel.Name = "txtVessel"
        Me.txtVessel.ReadOnly = True
        Me.txtVessel.Size = New System.Drawing.Size(248, 20)
        Me.txtVessel.TabIndex = 197
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 243)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 196
        Me.Label6.Text = "Shipping Line"
        '
        'txtLines
        '
        Me.txtLines.Location = New System.Drawing.Point(117, 237)
        Me.txtLines.MaxLength = 40
        Me.txtLines.Name = "txtLines"
        Me.txtLines.ReadOnly = True
        Me.txtLines.Size = New System.Drawing.Size(248, 20)
        Me.txtLines.TabIndex = 195
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 220)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 194
        Me.Label5.Text = "Port"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(117, 214)
        Me.txtPort.MaxLength = 40
        Me.txtPort.Name = "txtPort"
        Me.txtPort.ReadOnly = True
        Me.txtPort.Size = New System.Drawing.Size(248, 20)
        Me.txtPort.TabIndex = 193
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 192
        Me.Label2.Text = "Supplier"
        '
        'txtSupplier
        '
        Me.txtSupplier.Location = New System.Drawing.Point(117, 48)
        Me.txtSupplier.MaxLength = 40
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.ReadOnly = True
        Me.txtSupplier.Size = New System.Drawing.Size(248, 20)
        Me.txtSupplier.TabIndex = 1
        '
        'txtBL
        '
        Me.txtBL.Location = New System.Drawing.Point(117, 25)
        Me.txtBL.MaxLength = 40
        Me.txtBL.Name = "txtBL"
        Me.txtBL.ReadOnly = True
        Me.txtBL.Size = New System.Drawing.Size(248, 20)
        Me.txtBL.TabIndex = 0
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(12, 30)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(85, 13)
        Me.Label29.TabIndex = 191
        Me.Label29.Text = "B/L or AWB No."
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(7, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(469, 79)
        Me.GroupBox1.TabIndex = 2202
        Me.GroupBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(7, 195)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(469, 125)
        Me.GroupBox3.TabIndex = 2203
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dtDeliver1)
        Me.GroupBox4.Controls.Add(Me.dtETA1)
        Me.GroupBox4.Controls.Add(Me.dtETD)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 89)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(469, 99)
        Me.GroupBox4.TabIndex = 2204
        Me.GroupBox4.TabStop = False
        '
        'dtETD
        '
        Me.dtETD.Enabled = False
        Me.dtETD.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtETD.Location = New System.Drawing.Point(126, 18)
        Me.dtETD.Name = "dtETD"
        Me.dtETD.Size = New System.Drawing.Size(83, 20)
        Me.dtETD.TabIndex = 209
        '
        'TabPage2
        '
        Me.TabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TabPage2.Controls.Add(Me.crtcode)
        Me.TabPage2.Controls.Add(Me.DTCreated)
        Me.TabPage2.Controls.Add(Me.Label32)
        Me.TabPage2.Controls.Add(Me.Label33)
        Me.TabPage2.Controls.Add(Me.txtCREATEDBY)
        Me.TabPage2.Controls.Add(Me.GroupBox5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(486, 492)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "User Data"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'crtcode
        '
        Me.crtcode.Location = New System.Drawing.Point(113, 48)
        Me.crtcode.MaxLength = 1
        Me.crtcode.Name = "crtcode"
        Me.crtcode.ReadOnly = True
        Me.crtcode.Size = New System.Drawing.Size(22, 20)
        Me.crtcode.TabIndex = 225
        Me.crtcode.Visible = False
        '
        'DTCreated
        '
        Me.DTCreated.Enabled = False
        Me.DTCreated.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTCreated.Location = New System.Drawing.Point(137, 25)
        Me.DTCreated.Name = "DTCreated"
        Me.DTCreated.Size = New System.Drawing.Size(83, 20)
        Me.DTCreated.TabIndex = 0
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(12, 31)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(70, 13)
        Me.Label32.TabIndex = 202
        Me.Label32.Text = "Created Date"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(12, 54)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(59, 13)
        Me.Label33.TabIndex = 203
        Me.Label33.Text = "Created By"
        '
        'txtCREATEDBY
        '
        Me.txtCREATEDBY.Location = New System.Drawing.Point(137, 48)
        Me.txtCREATEDBY.MaxLength = 1
        Me.txtCREATEDBY.Name = "txtCREATEDBY"
        Me.txtCREATEDBY.ReadOnly = True
        Me.txtCREATEDBY.Size = New System.Drawing.Size(222, 20)
        Me.txtCREATEDBY.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Location = New System.Drawing.Point(7, 4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(469, 79)
        Me.GroupBox5.TabIndex = 2203
        Me.GroupBox5.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 192
        Me.Label3.Text = "Supplier"
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(117, 56)
        Me.TextBox9.MaxLength = 40
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(248, 20)
        Me.TextBox9.TabIndex = 1
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(117, 33)
        Me.TextBox10.MaxLength = 40
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(248, 20)
        Me.TextBox10.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 191
        Me.Label4.Text = "B/L or AWB No."
        '
        'lblHist
        '
        Me.lblHist.AutoSize = True
        Me.lblHist.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHist.Location = New System.Drawing.Point(12, 324)
        Me.lblHist.Name = "lblHist"
        Me.lblHist.Size = New System.Drawing.Size(93, 13)
        Me.lblHist.TabIndex = 71
        Me.lblHist.Text = "Clearance History "
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(12, 155)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 13)
        Me.Label17.TabIndex = 2201
        Me.Label17.Text = "Delivery Date"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(81, 13)
        Me.Label8.TabIndex = 2203
        Me.Label8.Text = "Clearence Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 2204
        Me.Label9.Text = "Total Received"
        '
        'dtETA1
        '
        Me.dtETA1.Checked = False
        Me.dtETA1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtETA1.Location = New System.Drawing.Point(126, 41)
        Me.dtETA1.Name = "dtETA1"
        Me.dtETA1.ShowCheckBox = True
        Me.dtETA1.Size = New System.Drawing.Size(100, 20)
        Me.dtETA1.TabIndex = 2205
        Me.dtETA1.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'dtDeliver1
        '
        Me.dtDeliver1.Checked = False
        Me.dtDeliver1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDeliver1.Location = New System.Drawing.Point(126, 64)
        Me.dtDeliver1.Name = "dtDeliver1"
        Me.dtDeliver1.ShowCheckBox = True
        Me.dtDeliver1.Size = New System.Drawing.Size(100, 20)
        Me.dtDeliver1.TabIndex = 2206
        Me.dtDeliver1.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'dtClear1
        '
        Me.dtClear1.Checked = False
        Me.dtClear1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtClear1.Location = New System.Drawing.Point(127, 19)
        Me.dtClear1.Name = "dtClear1"
        Me.dtClear1.ShowCheckBox = True
        Me.dtClear1.Size = New System.Drawing.Size(100, 20)
        Me.dtClear1.TabIndex = 2207
        Me.dtClear1.Value = New Date(2008, 10, 6, 0, 0, 0, 0)
        '
        'GRQty
        '
        Me.GRQty.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.GRQty.Enabled = False
        Me.GRQty.Location = New System.Drawing.Point(126, 42)
        Me.GRQty.Name = "GRQty"
        Me.GRQty.Size = New System.Drawing.Size(101, 20)
        Me.GRQty.TabIndex = 2208
        Me.GRQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.GRQty.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.lblClear1)
        Me.GroupBox6.Controls.Add(Me.GRQty)
        Me.GroupBox6.Controls.Add(Me.dtClear1)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 323)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(469, 75)
        Me.GroupBox6.TabIndex = 2205
        Me.GroupBox6.TabStop = False
        '
        'lblClear1
        '
        Me.lblClear1.AutoSize = True
        Me.lblClear1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClear1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClear1.Location = New System.Drawing.Point(228, 22)
        Me.lblClear1.Name = "lblClear1"
        Me.lblClear1.Size = New System.Drawing.Size(182, 12)
        Me.lblClear1.TabIndex = 2209
        Me.lblClear1.Text = "*temporal date (the last date of BAPB Date)"
        '
        'frmBAPB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1021, 566)
        Me.Controls.Add(Me.lblHist)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmBAPB"
        Me.Text = "BAPB"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPO As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtContainer As System.Windows.Forms.TextBox
    Friend WithEvents txtShipNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtPO As System.Windows.Forms.TextBox
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTruck As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtSupplier As System.Windows.Forms.TextBox
    Friend WithEvents txtBL As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents crtcode As System.Windows.Forms.TextBox
    Friend WithEvents DTCreated As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtCREATEDBY As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtVessel As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtLines As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPort As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtExpedition As System.Windows.Forms.TextBox
    Friend WithEvents txtShipmentNo As System.Windows.Forms.TextBox
    Friend WithEvents ReceivedDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents lblHist As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dtETD As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPOItem As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtClear1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtDeliver1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtETA1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GRQty As System.Windows.Forms.MaskedTextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblClear1 As System.Windows.Forms.Label
End Class
