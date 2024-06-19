Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Http
Imports System.Runtime.Serialization.Formatters
Imports System.Threading
Imports General


Partial Public Class Login
    Friend obj As IRGeneral
    Friend Username As String
    Private wtc As WrapperTransporterClass, _wtc As WrapperTransporterClass
    Private Sub MakeConnectionToServer(IP As String)
        Try
            Dim clientProvider As New BinaryClientFormatterSinkProvider()
            Dim serverProvider As New BinaryServerFormatterSinkProvider()
            serverProvider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full
            Dim props As IDictionary = New Hashtable()
            props("port") = 0
            Dim s As String = System.Guid.NewGuid().ToString()
            props("name") = s
            props("typeFilterLevel") = TypeFilterLevel.Full
            Dim chan As New HttpChannel(props, clientProvider, serverProvider)
            Dim Port As Integer = 6123
            ChannelServices.RegisterChannel(chan, False)
            '   obj = CType(Activator.GetObject(GetType(IRGeneral), "http://" & IP & ":" + Port + "/ServicesClass", WellKnownObjectMode.SingleCall), IRGeneral)
            obj = DirectCast(Activator.GetObject(GetType(IRGeneral), "http://" & IP & ":" & Port & "/ServicesClass", WellKnownObjectMode.SingleCall), IRGeneral)
        Catch ex As Exception
            MessageBox.Show("Unable to connect to server")
        End Try
    End Sub
    Private Sub Runmain()
        Application.Run(New Game(obj, Username))
    End Sub

   

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButton1.Checked = True
        wtc = New WrapperTransporterClass()
        BackgroundWorker1.WorkerSupportsCancellation = False
        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

      
        Try

            Username = TextBox1.Text
            If obj.SignIn(Username) Then
                Dim clientThread As New Thread(New ThreadStart(AddressOf Runmain))
                clientThread.Start()
                Me.Close()

            End If
        Catch ex As Exception
            MessageBox.Show("Unable to connect to server of IP " + IPTemp)
        End Try
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            TextBox2.Enabled = False
        Else
            TextBox2.Enabled = True
        End If
    End Sub
    Dim IPTemp As String
    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        MakeConnectionToServer("127.0.0.1")
    End Sub
End Class