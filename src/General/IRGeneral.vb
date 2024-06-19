Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Net

Public Interface IRGeneral
    Function SignIn(Username As String) As Boolean
    Sub RegisterWrapperTransporterClass(Sender As String, wtc As WrapperTransporterClass)
    Sub SendMessageToServer(Sender As String, MsgCont As String)
    Sub SendValuePrivate(Sender As String, Receiver As String, type As Integer, x As Integer, y As Integer, who As Integer, msg As String)
End Interface
