Public Class SetUserData
    Private _UserCT As Integer
    Private _UserId, _UserName As String
    Private _AdmAccess, _TrapErr, _DbgAccess, _SaveAudittrail As Boolean
    Private _ConfigData As SetConfig
    Private _OtorisasiComp As String

    Public Property UserCT() As Integer
        Get
            Return _UserCT
            'UserCT = _UserCT
        End Get
        Set(ByVal Value As Integer)
            _UserCT = Value
        End Set
    End Property
    Public Property UserId() As String
        Get
            'Return _UserId
            UserId = _UserId
        End Get
        Set(ByVal Value As String)
            _UserId = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            UserName = _UserName
        End Get
        Set(ByVal Value As String)
            _UserName = Value
        End Set
    End Property

    Public Property AdmAccess() As Boolean
        Get
            AdmAccess = _AdmAccess
        End Get
        Set(ByVal Value As Boolean)
            _AdmAccess = Value
        End Set
    End Property

    Public Property TrapErr() As Boolean
        Get
            TrapErr = _TrapErr
        End Get
        Set(ByVal Value As Boolean)
            _TrapErr = Value
        End Set
    End Property

    Public Property DbgAccess() As Boolean
        Get
            DbgAccess = _DbgAccess
        End Get
        Set(ByVal Value As Boolean)
            _DbgAccess = Value
        End Set
    End Property

    Public Property SaveAudittrail() As Boolean
        Get
            SaveAudittrail = _SaveAudittrail
        End Get
        Set(ByVal Value As Boolean)
            _SaveAudittrail = Value
        End Set
    End Property

    Public Property ConfigData() As SetConfig
        Get
            ConfigData = _ConfigData
        End Get
        Set(ByVal Value As SetConfig)
            _ConfigData = Value
        End Set
    End Property
    Public Property OtorisasiComp() As String
        Get
            OtorisasiComp = _OtorisasiComp
        End Get
        Set(ByVal Value As String)
            _OtorisasiComp = Value
        End Set
    End Property
End Class

Public Class SetApplData
    Private _NamaPT, _AlamatPT, _AwalTglProses, _SimpanKoneksi As String

    Public Property NamaPT() As String
        Get
            NamaPT = _NamaPT
        End Get
        Set(ByVal value As String)
            _NamaPT = value
        End Set
    End Property

    Public Property AlamatPT() As String
        Get
            AlamatPT = _AlamatPT
        End Get
        Set(ByVal value As String)
            _AlamatPT = value
        End Set
    End Property

    Public Property AwalTglProses() As String
        Get
            AwalTglProses = _AwalTglProses
        End Get
        Set(ByVal value As String)
            _AwalTglProses = value
        End Set
    End Property

    Public Property SimpanKoneksi() As String
        Get
            SimpanKoneksi = _SimpanKoneksi
        End Get
        Set(ByVal value As String)
            _SimpanKoneksi = value
        End Set
    End Property
End Class