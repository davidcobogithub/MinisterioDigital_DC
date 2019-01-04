Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Public Class ClsLibroInspeccionModelos

End Class

' ///
' ///    C A R A T U L A  
' ///

Public Class ClsCaratula

    'NUMERO DE ACTA
    Private _numeroActa As String
    Public Property numeroActa() As String
        Get
            Return _numeroActa
        End Get
        Set(ByVal value As String)
            _numeroActa = value
        End Set
    End Property

    'FECHA Y HORA EN QUE SE CONFECCIONA EL DOCUMENTO
    Private _fechaHoraActa As DateTime
    Public Property fechaHoraActa() As DateTime
        Get
            Return _fechaHoraActa
        End Get
        Set(ByVal value As DateTime)
            _fechaHoraActa = value
        End Set
    End Property

#Region "DOMICILIOS EMPRESAS"

    'LOCALIDAD DONDE SE CONFECCIONA EL DOCUMENTO
    Private _localidad As String
    Public Property Localidad() As String
        Get
            Return _localidad
        End Get
        Set(ByVal value As String)
            _localidad = value
        End Set
    End Property

    'DEPARTAMENTO DONDE SE CONFECCIONA EL DOCUMENTO
    Private _departamento As String
    Public Property Departamento() As String
        Get
            Return _departamento
        End Get
        Set(ByVal value As String)
            _departamento = value
        End Set
    End Property

    'DOMICILIO DONDE SE CONFECCIONA EL ACTA
    Private _domicilioActa As String
    Public Property domicilioActa() As String
        Get
            Return _domicilioActa
        End Get
        Set(ByVal value As String)
            _domicilioActa = value
        End Set
    End Property

    'DOMICILIO LEGAL
    Private _domicilioLegal As String
    Public Property domicilioLegal() As String
        Get
            Return _domicilioLegal
        End Get
        Set(ByVal value As String)
            _domicilioLegal = value
        End Set
    End Property

    'DOMICILIO LIBRO SUELDO
    Private _domicilioLibroSueldo As String
    Public Property domicilioLibroSueldo() As String
        Get
            Return _domicilioLibroSueldo
        End Get
        Set(ByVal value As String)
            _domicilioLibroSueldo = value
        End Set
    End Property

#End Region

    'CUIT EMPRESA
    Private _cuit As String
    Public Property Cuit As String
        Get
            Return _cuit
        End Get
        Set(ByVal value As String)
            _cuit = value
        End Set
    End Property

    'RAZON SOCIAL EMPRESA
    Private _razonSocial As String
    Public Property razonSocial As String
        Get
            Return _razonSocial
        End Get
        Set(ByVal value As String)
            _razonSocial = value
        End Set
    End Property

    'NOMBRE FANTASIA
    Private _nombrefantasia As String
    Public Property NombreFantasia As String
        Get
            Return _nombrefantasia
        End Get
        Set(ByVal value As String)
            _nombrefantasia = value
        End Set
    End Property

    'FECHA INSCRIPCION EMPRESA
    Private _fechainscripcion As DateTime
    Public Property FechaInscripcion As DateTime
        Get
            Return _fechainscripcion
        End Get
        Set(ByVal value As DateTime)
            _fechainscripcion = value
        End Set
    End Property

    'NUMERO DE MATRICULA
    Private _matricula As String
    Public Property Matricula As String
        Get
            Return _matricula
        End Get
        Set(ByVal value As String)
            _matricula = value
        End Set
    End Property

    ''ACTIVIDADES EMPRESA - 1ra Actividad principal, siguientes actividades secundarias
    'Private _actividades As List(Of String)
    'Public Property Actividades As List(Of String)
    '    Get
    '        Return _actividades
    '    End Get
    '    Set(ByVal value As List(Of String))
    '        _actividades = value
    '    End Set
    'End Property

    'ACTIVIDAD PRINCIPAL
    Private _actividadPrincipal As String
    Public Property actividadPrincipal As String
        Get
            Return _actividadPrincipal
        End Get
        Set(ByVal value As String)
            _actividadPrincipal = value
        End Set
    End Property

    'ACTIVIDADES SECUNDARIAS
    Private _actividadSecundaria As String
    Public Property actividadSecundaria As String
        Get
            Return _actividadSecundaria
        End Get
        Set(ByVal value As String)
            _actividadSecundaria = value
        End Set
    End Property

    'FECHA DE EMPLAZAMIENTO 
    Private _fechaEmplazamiento As DateTime
    Public Property FechaEmplazamiento As DateTime
        Get
            Return _fechaEmplazamiento
        End Get
        Set(ByVal value As DateTime)
            _fechaEmplazamiento = value
        End Set
    End Property


    'CANTIDAD DE EMPLEADOS DEPENDIENTES REGISTRADOS
    Private _cantidaddeempleados As Int16
    Public Property CantidadDeEmpleados() As Int16
        Get
            Return _cantidaddeempleados
        End Get
        Set(ByVal value As Int16)
            _cantidaddeempleados = value
        End Set
    End Property


    'CANTIDAD DE EMPLEADOS DEPENDIENTES VERIFICADOS
    Private _cantidaddeempleadosverificados As Int16
    Public Property CantidadDeEmpleadosVerificados() As Int16
        Get
            Return _cantidaddeempleadosverificados
        End Get
        Set(ByVal value As Int16)
            _cantidaddeempleadosverificados = value
        End Set
    End Property

    'HORA DE APERTURA DEL ACTA 
    Private _horaapertura As String
    Public Property HoraApertura() As String
        Get
            Return _horaapertura
        End Get
        Set(ByVal value As String)
            _horaapertura = value
        End Set
    End Property

    'HORA DE CIERRE DEL ACTA
    Private _horacierre As String
    Public Property HoraCierre() As String
        Get
            Return _horacierre
        End Get
        Set(ByVal value As String)
            _horacierre = value
        End Set
    End Property


End Class

' ///
' ///    A C T A   D E   C O N S T A T A C I O N
' ///

Public Class ClsActaConstatacion
    Inherits ClsCaratula

#Region "ATENDIDO POR"

    'ATENDIDO POR NOMBRE
    Private _atendidopornombre As String
    Public Property AtendidoPorNombre() As String
        Get
            Return _atendidopornombre
        End Get
        Set(ByVal value As String)
            _atendidopornombre = value
        End Set
    End Property

    'ATENDIDO POR DOCUMENTO
    Private _atendidopordocumento As String
    Public Property AtendidoPorDocumento() As String
        Get
            Return _atendidopordocumento
        End Get
        Set(ByVal value As String)
            _atendidopordocumento = value
        End Set
    End Property

    'ATENDIDO POR CARACTER
    Private _atendidoporcaracter As String
    Public Property AtendidoPorCaracter() As String
        Get
            Return _atendidoporcaracter
        End Get
        Set(ByVal value As String)
            _atendidoporcaracter = value
        End Set
    End Property

#End Region

    'ACOMPAÑANTES
    Private _acompanantes As String
    Public Property Acompanantes() As String
        Get
            Return _acompanantes
        End Get
        Set(ByVal value As String)
            _acompanantes = value
        End Set
    End Property

    'REGISTRO DE PERSONAL SI / NO
    Private _registrodepersonal As Boolean
    Public Property RegistroDePersonal() As Boolean
        Get
            Return _registrodepersonal
        End Get
        Set(ByVal value As Boolean)
            _registrodepersonal = value
        End Set
    End Property

    'DOCUMENTACION PRESENTADA POR LA EMPLEADORA
    Private _documentacionpresentada As String
    Public Property DocumentacionPresentada() As String
        Get
            Return _documentacionpresentada
        End Get
        Set(ByVal value As String)
            _documentacionpresentada = value
        End Set
    End Property

    'DOCUMENTACION A PRESENTAR POR LA EMPLEADORA
    Private _documentacionApresentar As String
    Public Property DocumentacionAPresentar() As String
        Get
            Return _documentacionApresentar
        End Get
        Set(ByVal value As String)
            _documentacionApresentar = value
        End Set
    End Property



    'MANIFIESTO SINDICAL
    Private _manifiestoSindical As String
    Public Property ManifiestoSindical() As String
        Get
            Return _manifiestoSindical
        End Get
        Set(ByVal value As String)
            _manifiestoSindical = value
        End Set
    End Property

    'MANIFIESTO EMPLEADOR
    Private _manifiestoempleador As String
    Public Property ManifiestoEmpleador() As String
        Get
            Return _manifiestoempleador
        End Get
        Set(ByVal value As String)
            _manifiestoempleador = value
        End Set
    End Property

    'PLAZO
    Private _plazo As String
    Public Property Plazo() As String
        Get
            Return _plazo
        End Get
        Set(ByVal value As String)
            _plazo = value
        End Set
    End Property

    'OBSERVACIONES
    Private _observaciones As String
    Public Property Observaciones() As String
        Get
            Return _observaciones
        End Get
        Set(ByVal value As String)
            _observaciones = value
        End Set
    End Property

    'HACER VISIBLE EL PARRAFO PARA RELEVAMIENTO PERSONAL
    Private _visibleRP As String
    Public Property VisibleRP() As String
        Get
            Return _visibleRP
        End Get
        Set(ByVal value As String)
            _visibleRP = value
        End Set
    End Property

    'HACER VISIBLE EL PARRAFO PARA AUDIENCIA
    Private _visibleAudiencia As String
    Public Property VisibleAudiencia() As String
        Get
            Return _visibleAudiencia
        End Get
        Set(ByVal value As String)
            _visibleAudiencia = value
        End Set
    End Property

    'PARRAFO AUDIENCIA
    Private _audiencia As String
    Public Property Audiencia() As String
        Get
            Return _audiencia
        End Get
        Set(ByVal value As String)
            _audiencia = value
        End Set
    End Property

    'CCT APLICADO
    Private _cctaplicado As String
    Public Property CCTAplicado() As String
        Get
            Return _cctaplicado
        End Get
        Set(ByVal value As String)
            _cctaplicado = value
        End Set
    End Property

    'PARRAFO SI MODIFICO EL DOMICILIO
    Private _modificaDomicilio As String
    Public Property ModificaDomicilio() As String
        Get
            Return _modificaDomicilio
        End Get
        Set(ByVal value As String)
            _modificaDomicilio = value
        End Set
    End Property

End Class

' ///
' ///    A C T A   D E   I N F R A C C I O N
' ///

Public Class ClsActaInfraccion
    Inherits ClsCaratula

    'ARTICULOS  
    Private _articulos As String
    Public Property Articulos As String
        Get
            Return _articulos
        End Get
        Set(ByVal value As String)
            _articulos = value
        End Set
    End Property

    'PLAZO
    Private _plazo As Int16
    Public Property Plazo() As Int16
        Get
            Return _plazo
        End Get
        Set(ByVal value As Int16)
            _plazo = value
        End Set
    End Property

    'LUGAR PRESENTACION
    Private _lugarpresentacion As String
    Public Property LugarPresentacion() As String
        Get
            Return _lugarpresentacion
        End Get
        Set(ByVal value As String)
            _lugarpresentacion = value
        End Set
    End Property

End Class