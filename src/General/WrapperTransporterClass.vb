Imports System
Imports System.Collections.Generic
Imports System.Text
Public Class WrapperTransporterClass
    Inherits MarshalByRefObject
    Public SendvaluePrivateTransporterDelegate As DelegateSendvaluePrivateTransporter
    Public Sub New()
    End Sub
    Public Sub RunDelegateSendvaluePrivateTransporter(Sender As String, Type As Integer, x As Integer, y As Integer, who As Integer, msg As String)
        SendvaluePrivateTransporterDelegate(Sender, Type, x, y, who, msg)
    End Sub
End Class
