Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports General
Public Class ServicesClass
    Inherits MarshalByRefObject
    Implements IRGeneral
    Public Sub RegisterWrapperTransporterClass(Sender As String, wtc As WrapperTransporterClass) Implements IRGeneral.RegisterWrapperTransporterClass
        ModifyUserOnlineListWrapper(Sender, wtc)
    End Sub

    Public Sub SendMessageToServer(Sender As String, MsgCont As String) Implements IRGeneral.SendMessageToServer
        Dim j As Integer = 0
        While j < i
            CType(GetConnectionTo(Usenames(i)), WrapperTransporterClass).RunDelegateSendvaluePrivateTransporter(Sender, 7, 0, 0, 0, MsgCont)
            System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
        End While
    End Sub

    Public Sub SendValuePrivate(Sender As String, Receiver As String, type As Integer, x As Integer, y As Integer, who As Integer, _
         Msg As [String]) Implements IRGeneral.SendValuePrivate
        If type = 6 Then
            Dim j As Integer = 0
            While j < i
                CType(GetConnectionTo(Sender), WrapperTransporterClass).RunDelegateSendvaluePrivateTransporter(Sender, 6, x, y, who, Usenames(j))
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
            End While
        Else
            Try
                CType(GetConnectionTo(Receiver), WrapperTransporterClass).RunDelegateSendvaluePrivateTransporter(Sender, type, x, y, who, Msg)
            Catch e As Exception
                Try
                    If type = 0 Then
                        CType(GetConnectionTo(Sender), WrapperTransporterClass).RunDelegateSendvaluePrivateTransporter(Nothing, 2, x, y, who, Receiver)
                    Else
                        CType(GetConnectionTo(Sender), WrapperTransporterClass).RunDelegateSendvaluePrivateTransporter(Nothing, 5, x, y, who, Receiver)
                    End If
                Catch e1 As Exception
                End Try
            End Try
        End If
    End Sub

    Public Function SignIn(Username As String) As Boolean Implements IRGeneral.SignIn
        If ((Not Username Is Nothing) AndAlso (Username.Length > 0)) Then
            Try
                AddUserToOnlineUserList(Username, Nothing)
            Catch exception As Exception
                MsgBox("Thử lại với tên đăng nhập khác ^_^!")
                Return False
            End Try
            If (i < 100) Then
                Console.WriteLine("#info : " & Username & " Sigin at " & DateTime.Now.ToString)
                Usenames(i) = Username
                System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)  'i ++
                Return True
            End If
            MsgBox("Server đầy....!")
            Return False
        End If
        Return False
    End Function
End Class
