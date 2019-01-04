Imports Microsoft.VisualBasic

Public Class ClsBarandillaManualDetalle

    Private _id_detallebarandocmanual As Integer
    Public Property id_detallebarandocmanual As Integer
        Get
            Return _id_detallebarandocmanual
        End Get
        Set(ByVal value As Integer)
            _id_detallebarandocmanual = value
        End Set
    End Property

    Private _idbarandilladocmanual As Integer
    Public Property idbarandilladocmanual As Integer
        Get
            Return _idbarandilladocmanual
        End Get
        Set(ByVal value As Integer)
            _idbarandilladocmanual = value
        End Set
    End Property

    Private _id_empleado As Integer
    Public Property id_empleado As Integer
        Get
            Return _id_empleado
        End Get
        Set(ByVal value As Integer)
            _id_empleado = value
        End Set
    End Property

    Private _nrolibro As Integer
    Public Property nrolibro As Integer
        Get
            Return _nrolibro
        End Get
        Set(ByVal value As Integer)
            _nrolibro = value
        End Set
    End Property


    Private _nrofolio As Integer
    Public Property nrofolio As Integer
        Get
            Return _nrofolio
        End Get
        Set(ByVal value As Integer)
            _nrofolio = value
        End Set
    End Property

    Private _totalneto As Decimal
    Public Property totalneto As Decimal
        Get
            Return _totalneto
        End Get
        Set(ByVal value As Decimal)
            _totalneto = value
        End Set
    End Property

    Private _obs As String
    Public Property obs As String
        Get
            Return _obs
        End Get
        Set(ByVal value As String)
            _obs = value
        End Set
    End Property



End Class
