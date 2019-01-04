Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web

Public Class Usuario
    Public Property CUIL() As [String]
        Get
            Return m_CUIL
        End Get
        Set(ByVal value As [String])
            m_CUIL = value
        End Set
    End Property
    Private m_CUIL As [String]
    Public Property NroDocumento() As [String]
        Get
            Return m_NroDocumento
        End Get
        Set(ByVal value As [String])
            m_NroDocumento = value
        End Set
    End Property
    Private m_NroDocumento As [String]
    Public Property Apellido() As [String]
        Get
            Return m_Apellido
        End Get
        Set(ByVal value As [String])
            m_Apellido = value
        End Set
    End Property
    Private m_Apellido As [String]
    Public Property Nombre() As [String]
        Get
            Return m_Nombre
        End Get
        Set(ByVal value As [String])
            m_Nombre = value
        End Set
    End Property
    Private m_Nombre As [String]
    Public Property FechaNacimiento() As [String]
        Get
            Return m_FechaNacimiento
        End Get
        Set(ByVal value As [String])
            m_FechaNacimiento = value
        End Set
    End Property
    Private m_FechaNacimiento As [String]
    Public Property Id_Sexo() As [String]
        Get
            Return m_Id_Sexo
        End Get
        Set(ByVal value As [String])
            m_Id_Sexo = value
        End Set
    End Property
    Private m_Id_Sexo As [String]
    Public Property PaiCodPais() As [String]
        Get
            Return m_PaiCodPais
        End Get
        Set(ByVal value As [String])
            m_PaiCodPais = value
        End Set
    End Property
    Private m_PaiCodPais As [String]
    Public Property Id_Numero() As Integer
        Get
            Return m_Id_Numero
        End Get
        Set(ByVal value As Integer)
            m_Id_Numero = value
        End Set
    End Property
    Private m_Id_Numero As Integer
    Public Property Id_Estado() As String
        Get
            Return m_Id_Estado
        End Get
        Set(ByVal value As String)
            If value = String.Empty Then
                m_Id_Estado = 1
            Else
                m_Id_Estado = value
            End If
        End Set
    End Property
    Private m_Id_Estado As String
    Public Property Estado() As [String]
        Get
            Return m_Estado
        End Get
        Set(ByVal value As [String])
            m_Estado = value
        End Set
    End Property
    Private m_Estado As [String]
    Public Property Email() As [String]
        Get
            Return m_Email
        End Get
        Set(ByVal value As [String])
            m_Email = value
        End Set
    End Property
    Private m_Email As [String]
    Public Property TelArea() As [String]
        Get
            Return m_TelArea
        End Get
        Set(ByVal value As [String])
            m_TelArea = value
        End Set
    End Property
    Private m_TelArea As [String]
    Public Property TelNro() As [String]
        Get
            Return m_TelNro
        End Get
        Set(ByVal value As [String])
            m_TelNro = value
        End Set
    End Property
    Private m_TelNro As [String]
    Public Property CelArea() As [String]
        Get
            Return m_CelArea
        End Get
        Set(ByVal value As [String])
            m_CelArea = value
        End Set
    End Property
    Private m_CelArea As [String]
    Public Property CelNro() As [String]
        Get
            Return m_CelNro
        End Get
        Set(ByVal value As [String])
            m_CelNro = value
        End Set
    End Property
    Private m_CelNro As [String]
    Public Property Empleado() As [String]
        Get
            Return m_Empleado
        End Get
        Set(ByVal value As [String])
            m_Empleado = value
        End Set
    End Property
    Private m_Empleado As [String]
    Public Property Id_Empleado() As [String]
        Get
            Return m_Id_Empleado
        End Get
        Set(ByVal value As [String])
            m_Id_Empleado = value
        End Set
    End Property
    Private m_Id_Empleado As [String]
    Public Property FechaRegistro() As [String]
        Get
            Return m_FechaRegistro
        End Get
        Set(ByVal value As [String])
            m_FechaRegistro = value
        End Set
    End Property
    Private m_FechaRegistro As [String]
    Public Property FechaBloqueo() As [String]
        Get
            Return m_FechaBloqueo
        End Get
        Set(ByVal value As [String])
            m_FechaBloqueo = value
        End Set
    End Property
    Private m_FechaBloqueo As [String]
    Public Property Domicilio() As Domicilio
        Get
            Return m_Domicilio
        End Get
        Set(ByVal value As Domicilio)
            m_Domicilio = value
        End Set
    End Property
    Private m_Domicilio As Domicilio
    Public Property Respuesta() As Respuesta
        Get
            Return m_Respuesta
        End Get
        Set(ByVal value As Respuesta)
            m_Respuesta = value
        End Set
    End Property
    Private m_Respuesta As Respuesta
End Class


