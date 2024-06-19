Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Http
Imports System.Runtime.Serialization.Formatters
Imports System.Net
Imports General


Module Program
    Dim port As Integer = 6123
    Dim OnlineUserList As Hashtable
    Public i As Integer = 0
    Public Usenames As String() = New String(100) {}
    Dim httpchnl As HttpChannel = Nothing


    Sub Main(args As String())
        Dim ClientProvider = New BinaryClientFormatterSinkProvider()
        Dim ServerProvider = New BinaryServerFormatterSinkProvider()
        ServerProvider.TypeFilterLevel = TypeFilterLevel.Full
        Dim props As IDictionary = New Hashtable()
        props.Item("port") = port
        props.Item("typeFilterLevel") = TypeFilterLevel.Full
        httpchnl = New HttpChannel(props, ClientProvider, ServerProvider)
        ChannelServices.RegisterChannel(httpchnl, False)
        Dim soap As New ServicesClass()
        Dim obj As ObjRef = RemotingServices.Marshal(soap, "ServicesClass")
        Console.WriteLine("Server startup at:" & DateTime.Now.ToString)
        Console.WriteLine("Server URI:" & obj.URI)

        'Thêm tài khoản vào kho dữ liệu
        OnlineUserList = New Hashtable()


        Console.WriteLine("Press Enter to Stop Services")
        Console.ReadLine()

        'Tiến trình ngưng dịch vụ đang thực thi
        RemotingServices.Disconnect(soap)
        Console.WriteLine("Services has stopped")

        'Tiến trình ngắt kết nối máy chủ
        ChannelServices.UnregisterChannel(httpchnl)
        Console.WriteLine("server is shutdown ")

        'Hỗ trợ tiến trình ngắt kết nối
        Console.ReadLine()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub
   
    Friend Sub AddUserToOnlineUserList(Username As String, wtc As WrapperTransporterClass)
        OnlineUserList.Add(Username, wtc)
    End Sub
    Friend Sub ModifyUserOnlineListWrapper(Username As String, wtc As WrapperTransporterClass)
        OnlineUserList.Item(Username) = wtc
    End Sub
    Friend Function GetConnectionTo(Username As String) As WrapperTransporterClass
        Return CType(OnlineUserList.Item(Username), WrapperTransporterClass)
    End Function
    Public Sub SetUsername(name As String)
        Usenames(i) = name
        System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1) 'i++
    End Sub
    Public Function GetUsername(i As Integer)
        Return Usenames(i)
    End Function
End Module
