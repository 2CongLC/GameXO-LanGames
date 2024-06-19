Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Text
Imports General

'Xác định vị trí quân cờ tại điểm đã dùng
Structure Get_Point
    Public value As Integer
End Structure

Public Class Game
    REM ================= KẾT NỐI ==============================
    Dim obj As IRGeneral
    Dim wtc_game As WrapperTransporterClass
    Dim Usernames As String()
    Dim Count_Players As Integer

    REM ================= BÀN CỜ XO ============================

    ' Fields : Ghi nhớ các vị trí của bàn cờ
    Dim temp_tables As Get_Point(,) = New Get_Point(21, 21) {}

    ' Fields : Ghi nhớ các vị trí của quân cờ
    Dim temp_flags As Integer(,) = New Integer(19, 19) {}

    ' Fields : Tạo quân cờ X hoặc O trên bàn cờ | X = 1 , O = 2
    Dim flag_types As Integer

    ' Fields : Vị trí quân cờ player1 đã đánh
    Dim p1_x, p1_y As Integer

    ' Fields : Vị trí quân cờ player2 đã đánh
    Dim p2_x, p2_y As Integer

    ' Fields : Kiểm tra lượt đánh  | Player1 = true , Player2 = false
    Dim Check_returns As Boolean = False

    'Fields : Thiết kế bàn cờ
    Dim tables As Bitmap = New Bitmap("images\caro.jpg")
    Dim flag_x As Bitmap = New Bitmap("images\x.jpg")
    Dim flag_o As Bitmap = New Bitmap("images\o.jpg")
    Dim flag_x1 As Bitmap = New Bitmap("images\xvang.jpg")
    Dim flag_o1 As Bitmap = New Bitmap("images\ovang.jpg")

    ' Fields : Kiểm tra ô cờ | Chưa sử dụng =1, Đã sử dụng =0 
    Dim Check_flags As Integer
    '============================================================

    REM ================ DỮ LIỆU TRUYỀN TẢI =====================

    'Fields : Chọn X hay O cho Player1 (Your)
    Dim Player1_Types As Integer ' kiểu X = 1 , O = 0

    ' Fields : Kiểm tra xem ai là người đi đầu tiên 
    Dim Players_First As Integer

    ' Fields : Kiểm tra điều kiện thắng thua của player1(your) vs player2
    Dim player1_win As Integer
    Dim player2_win As Integer

    ' Fields : Kiểm tra số trận thắng của mỗi người chơi
    Dim Player1_score As Integer
    Dim Player2_score As Integer
  
    ' Fields  : Tên người chơi player1 và player2
    Dim player1_name As String
    Dim Player2_name As String

    ' Fields : Thời gian của hiệp đấu
    Dim Game_time As Integer = 120
    ' Fields : Xem người chơi đã sẵn sàng hay chưa
    Dim Game_Ready As Integer

    'Sender : Gửi thông tin cho máy chủ, Receive : Gửi thông tin lại cho máy trạm
    Delegate Sub DelegateSendMessageToServer(Sender As String, MsgCont As String)
    Delegate Sub DelegateSendValuePrivate(Sender As String, Receive As String, type As Integer, x As Integer, y As Integer, who As Integer, msg As String)
    Delegate Sub AddTextToDisplayDelegate(Content As String)
    Private Sub DoNothingInCallBack(Res As IAsyncResult)
    End Sub
    Private Sub AddTextToChatDisplay(Cont As [String])
        If Me.InvokeRequired Then
            Dim d As New AddTextToDisplayDelegate(AddressOf AddTextToChatDisplay)
            Me.Invoke(d, Cont)
        Else
            Rich_ReceiverChats.AppendText(Cont + Environment.NewLine)
        End If
    End Sub
    'Properties : Nhận dữ liệu từ máy chủ
    Private Sub DataReceive(Sender As String, type As Integer, x As Integer, y As Integer, who As Integer, msg As String)
        If type = 1 AndAlso Player2_name.Equals(Sender) Then
            draw2(x, y)
            player2_win = who
            Check_returns = True
            Game_time = 120
        End If
        If type = 0 Then
            AddTextToChatDisplay(Sender + " :" + msg)
            bt_SendChats.BackColor = System.Drawing.Color.Yellow
        End If
        If type = 2 Then
            MessageBox.Show("Erro!không thể chát với :" + msg)
        End If
        If type = 3 Then
            If MessageBox.Show(Sender + " Muốn chơi Game với bạn ", "Ok", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Player2_name = Sender
                Check_returns = True
                Game_Ready = 1
                Player1_Types = 1
                Player1_score = 0
                Player2_score = 0
                Game_time = 120
            Else
                Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
                dsmts.BeginInvoke(player1_name, Sender, 4, 0, 0, player1_win, _
                    Nothing, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
            End If
        End If
        If type = 4 Then
            MessageBox.Show(Sender + " không chấp nhận chơi !")
            Check_flags = 1
        End If
        If type = 5 Then
            MessageBox.Show("không thể kết nối với : " + msg)
            Check_flags = 1
        End If
        If type = 6 Then
            If Not msg.Equals(Sender) Then
                Dim j As Integer = 0
                While j <= Count_Players
                    If msg.Equals(Usernames(j)) Then
                        Exit While
                    End If
                    If j = Count_Players Then
                        Usernames(Count_Players) = msg
                        System.Math.Max(System.Threading.Interlocked.Increment(Count_Players), Count_Players - 1)
                        Exit While
                    End If
                    System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
                End While
            End If
        End If
        If type = 7 Then
            AddTextToChatDisplay(Sender + "Đã đánh bại " + msg)
        End If
        If type = 8 Then
            MessageBox.Show("Đã thắng do đối thủ hết p1_x. Click để đánh ván khác")
            player2_win = who
            Check_returns = True
        End If
        If type = 9 Then
            MessageBox.Show("Cố lên !")
            Players_First = 0
            Check_returns = True
            Game_time = 120
        End If
    End Sub

    'Properties : Dữ liệu gửi đi và đến
    Private Sub RegisterMethodToWrapperTransporterClass()
        Me.wtc_game.SendvaluePrivateTransporterDelegate = DirectCast([Delegate].Combine(Me.wtc_game.SendvaluePrivateTransporterDelegate, New DelegateSendvaluePrivateTransporter(AddressOf Me.DataReceive)), DelegateSendvaluePrivateTransporter)
    End Sub
    Public Sub draw2(x As Integer, y As Integer)
        Const onclick As Integer = 32
        Dim g As Graphics = Game_tables.CreateGraphics()
        Dim i As Integer = 0, j As Integer = 0
        p1_x = 0
        p1_y = 0
        p2_x = 0
        p2_y = 0
        While i < x
            System.Math.Max(System.Threading.Interlocked.Increment(p1_x), p1_x - 1)
            i += onclick
        End While
        While j < y
            System.Math.Max(System.Threading.Interlocked.Increment(p1_y), p1_y - 1)
            j += onclick
        End While
        If temp_flags(p1_x, p1_y) = 0 Then
            If flag_types = 1 Then
                g.DrawImage(flag_x1, New Point((p1_x - 1) * onclick, (p1_y - 1) * onclick))
                flag_types = 2
                temp_flags(p1_x, p1_y) = 1
                temp_tables((p1_x + 1), (p1_y + 1)).value = 1
            Else
                g.DrawImage(flag_o1, New Point((p1_x - 1) * onclick, (p1_y - 1) * onclick))
                flag_types = 1
                temp_flags(p1_x, p1_y) = 2
                temp_tables((p1_x + 1), (p1_y + 1)).value = 0
            End If
            p2_x = p1_x + 1
            p2_y = p1_y + 1
            Players_First = 1
        End If
    End Sub
    Public Sub draw(x As Integer, y As Integer)
        Const onclick As Integer = 32
        Dim g As Graphics = Game_tables.CreateGraphics()
        Dim i As Integer = 0, j As Integer = 0
        p1_x = 0
        p1_y = 0
        While i < x
            System.Math.Max(System.Threading.Interlocked.Increment(p1_x), p1_x - 1)
            i += onclick
        End While
        While j < y
            System.Math.Max(System.Threading.Interlocked.Increment(p1_y), p1_y - 1)
            j += onclick
        End While
        If temp_flags(p1_x, p1_y) = 0 Then
            Check_returns = False
            If flag_types = 1 Then
                g.DrawImage(flag_x, New Point((p1_x - 1) * onclick, (p1_y - 1) * onclick))
                flag_types = 2
                temp_flags(p1_x, p1_y) = 1
                temp_tables((p1_x + 1), (p1_y + 1)).value = 1
                game_engine((p1_x + 1), (p1_y + 1))
            Else
                g.DrawImage(flag_o, New Point((p1_x - 1) * onclick, (p1_y - 1) * onclick))
                flag_types = 1
                temp_flags(p1_x, p1_y) = 1
                temp_tables((p1_x + 1), (p1_y + 1)).value = 0
                game_engine((p1_x + 1), (p1_y + 1))
            End If
            If Players_First <> 0 Then
                If temp_tables(p2_x, p2_y).value = 0 Then
                    g.DrawImage(flag_o, New Point((p2_x - 2) * onclick, (p2_y - 2) * onclick))
                Else
                    g.DrawImage(flag_x, New Point((p2_x - 2) * onclick, (p2_y - 2) * onclick))
                End If
            Else
                Players_First = 1
                Game_time = 120
                lb_times.Visible = True
            End If
        End If
    End Sub
    Sub game_engine(x As Integer, y As Integer)
        Dim Click_right As Integer = 0, Click_left As Integer = 0, Click_up As Integer = 0, Click_down As Integer = 0, Click_UpRight As Integer = 0, Click_DownRight As Integer = 0, _
            Click_Upleft As Integer = 0, Click_Downleft As Integer = 0
        Dim Click_count As Integer = temp_tables(x, y).value
        Dim i As Integer = 1
        While i < 19
            If x - i >= 0 Then
                If temp_tables(x - i, y).value = Click_count Then
                    System.Math.Max(System.Threading.Interlocked.Increment(Click_left), Click_left - 1)
                Else
                    Exit While
                End If
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        '   Dim i As Integer = 1
        While i < 18
            If x + i < 19 Then
                If temp_tables(x + i, y).value = Click_count Then
                    System.Math.Max(System.Threading.Interlocked.Increment(Click_right), Click_right - 1)
                Else
                    Exit While
                End If
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        If Click_right + Click_left = 4 AndAlso (temp_tables(x + Click_right + 1, y).value = 2 OrElse temp_tables(x - Click_left - 1, y).value = 2) Then
            If Click_count = Player1_Types Then
                MessageBox.Show(player1_name + " thang")
                Dim dsmts As New DelegateSendMessageToServer(AddressOf obj.SendMessageToServer)
                dsmts.BeginInvoke(player1_name, Player2_name, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                System.Math.Max(System.Threading.Interlocked.Increment(Player1_score), Player1_score - 1)
                Player1_Types = 0
                game_reset()
                player1_win = 1
                Players_First = 0
                Return
            Else
                MessageBox.Show(Player2_name + " thang")
                Player2_score = 2
                Player1_Types = 1
                game_reset()
                player1_win = 1
                Players_First = 0
                Return
            End If
        End If
        '   Dim i As Integer = 1
        While i < 19
            If y - i >= 0 Then
                If temp_tables(x, y - i).value = Click_count Then
                    System.Math.Max(System.Threading.Interlocked.Increment(Click_up), Click_up - 1)
                Else
                    Exit While
                End If
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        '  Dim i As Integer = 1
        While i < 18
            If y + i < 19 Then
                If temp_tables(x, y + i).value = Click_count Then
                    System.Math.Max(System.Threading.Interlocked.Increment(Click_down), Click_down - 1)
                Else
                    Exit While
                End If
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        If Click_up + Click_down = 4 AndAlso (temp_tables(x, y + Click_down + 1).value = 2 OrElse temp_tables(x, y - Click_up - 1).value = 2) Then
            If Click_count = Player1_Types Then
                MessageBox.Show(player1_name + " thang")
                Dim dsmts As New DelegateSendMessageToServer(AddressOf obj.SendMessageToServer)
                dsmts.BeginInvoke(player1_name, Player2_name, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                System.Math.Max(System.Threading.Interlocked.Increment(Player1_score), Player1_score - 1)
                Player1_Types = 0
                game_reset()
                player1_win = 1
                Players_First = 0
                Return
            Else
                MessageBox.Show(Player2_name + " thang")
                Player2_score = 2
                Player1_Types = 1
                game_reset()
                player1_win = 1
                Return
            End If
        End If
        '  Dim i As Integer = 1
        While i < 19
            If x - i < 0 Then
                Exit While
            End If
            If y + i > 19 Then
                Exit While
            End If
            If temp_tables(x - i, y + i).value = Click_count Then
                System.Math.Max(System.Threading.Interlocked.Increment(Click_DownRight), Click_DownRight - 1)
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        '   Dim i As Integer = 1
        While i < 19
            If x + i > 19 Then
                Exit While
            End If
            If y - i < 0 Then
                Exit While
            End If
            If temp_tables(x + i, y - i).value = Click_count Then
                System.Math.Max(System.Threading.Interlocked.Increment(Click_UpRight), Click_UpRight - 1)
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        If Click_UpRight + Click_DownRight = 4 AndAlso (temp_tables(x + Click_UpRight + 1, y - Click_UpRight - 1).value = 2 OrElse temp_tables(x - Click_DownRight - 1, y + Click_DownRight + 1).value = 2) Then
            If Click_count = Player1_Types Then
                MessageBox.Show(player1_name + " thang")
                Dim dsmts As New DelegateSendMessageToServer(AddressOf obj.SendMessageToServer)
                dsmts.BeginInvoke(player1_name, Player2_name, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                System.Math.Max(System.Threading.Interlocked.Increment(Player1_score), Player1_score - 1)
                Player1_Types = 0
                game_reset()
                player1_win = 1
                Players_First = 0
                Return
            Else
                MessageBox.Show(Player2_name + " thang")
                Player2_score = 2
                Player1_Types = 1
                game_reset()
                player1_win = 1
                Return
            End If
        End If
        '    Dim i As Integer = 1
        While i < 19
            If x - i < 0 Then
                Exit While
            End If
            If y - i < 0 Then
                Exit While
            End If
            If temp_tables(x - i, y - i).value = Click_count Then
                System.Math.Max(System.Threading.Interlocked.Increment(Click_Upleft), Click_Upleft - 1)
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        '   Dim i As Integer = 1
        While i < 19
            If x + i > 19 Then
                Exit While
            End If
            If y + i > 19 Then
                Exit While
            End If
            If temp_tables(x + i, y + i).value = Click_count Then
                System.Math.Max(System.Threading.Interlocked.Increment(Click_Downleft), Click_Downleft - 1)
            Else
                Exit While
            End If
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        If Click_Upleft + Click_Downleft = 4 AndAlso (temp_tables(x - Click_Upleft - 1, y - Click_Upleft - 1).value = 2 OrElse temp_tables(x + Click_Downleft + 1, y + Click_Downleft + 1).value = 2) Then
            If Click_count = Player1_Types Then
                MessageBox.Show(player1_name + " thang")
                Dim dsmts As New DelegateSendMessageToServer(AddressOf obj.SendMessageToServer)
                dsmts.BeginInvoke(player1_name, Player2_name, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                System.Math.Max(System.Threading.Interlocked.Increment(Player1_score), Player1_score - 1)
                Player1_Types = 0
                game_reset()
                player1_win = 1
                Players_First = 0
                Return
            Else
                MessageBox.Show(Player2_name + " thang")
                Player2_score = 2
                Player1_Types = 1
                game_reset()
                player1_win = 1
                Return
            End If
        End If
    End Sub
    Sub game_reset()
        Dim i As Integer = 0
        While i <= 20
            Dim j As Integer = 0
            While j <= 20
                temp_tables(i, j).value = 2
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
            End While
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        ' Dim i As Integer = 0
        While i < 19
            Dim j As Integer = 0
            While j < 19
                temp_flags(i, j) = 0
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
            End While
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        flag_types = 1
        player2_win = 0
        lb_player1_score.Text = Player1_score.ToString()
        lb_player2_score.Text = Player2_score.ToString()
        Game_time = 120
        Game_tables.Refresh()
    End Sub

    Public Sub New(obj As IRGeneral, player1_name As String)
        InitializeComponent()

      

        Me.obj = obj
        Me.player1_name = player1_name
        Me.Text = player1_name

        wtc_game = New WrapperTransporterClass()
        RegisterMethodToWrapperTransporterClass()

        obj.RegisterWrapperTransporterClass(player1_name, wtc_game)

        Dim i As Integer = 0
        While i < 19
            Dim j As Integer = 0
            While j < 19
                temp_flags(i, j) = 0
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
            End While
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        '  Dim i As Integer = 0
        While i <= 20
            Dim j As Integer = 0
            While j <= 20
                temp_tables(i, j).value = 2
                System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
            End While
            System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        flag_types = 2
        player1_win = 0
        player2_win = 0
        Player2_name = ""
        Check_flags = 0
        Game_Ready = 0
        Group_Player1_Properties.Text = Me.player1_name
        lb_player1_score.Text = Player1_score.ToString()
        Player1_Types = 0
        Player1_score = 0
        Player2_score = 0
        Usernames = New String(100) {}
        Usernames(0) = ""
        Count_Players = 0
        Players_First = 0
        lb_times.Visible = False
    End Sub

    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Game_tables.Image = tables      
        Catch ex As Exception
            Console.WriteLine("Game table not found")
        End Try
    End Sub

    Private Sub bt_SendChats_Click(sender As Object, e As EventArgs) Handles bt_SendChats.Click
        If Rich_SendChats.Text IsNot Nothing AndAlso Rich_SendChats.Text.Length > 0 Then
            AddTextToChatDisplay(player1_name + ": " + Rich_SendChats.Text)
            If Player2_name.Equals("", StringComparison.CurrentCultureIgnoreCase) Then
            Else
                Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
                dsmts.BeginInvoke(player1_name, Player2_name, 0, 0, 0, player1_win, _
                   Rich_SendChats.Text, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
            End If
            Rich_SendChats.Clear()
        End If
    End Sub

    Private Sub Rich_SendChats_TextChanged(sender As Object, e As EventArgs) Handles Rich_SendChats.TextChanged
        Me.bt_SendChats.BackColor = System.Drawing.Color.Ivory
    End Sub

    Private Sub ComboBox1_Click(sender As Object, e As EventArgs) Handles ComboBox1.Click
        ComboBox1.Items.Clear()
        Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
        dsmts.BeginInvoke(player1_name, Nothing, 6, 0, 0, player1_win, _
            Nothing, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
        Dim j As Integer = 0
        While j < Count_Players
            ComboBox1.Items.Add(Usernames(j))
            System.Math.Max(System.Threading.Interlocked.Increment(j), j - 1)
        End While
    End Sub

  

    Private Sub Time_Rounds_Tick(sender As Object, e As EventArgs) Handles Time_Rounds.Tick
        System.Math.Max(System.Threading.Interlocked.Decrement(Game_time), Game_time + 1)
        lb_times.Text = Game_time.ToString()
        If Game_time = 119 Then
            lb_times.ForeColor = System.Drawing.Color.Green
        End If
        If Game_time < 90 Then
            lb_times.ForeColor = System.Drawing.Color.Yellow
        End If
        If Game_time < 30 Then
            lb_times.ForeColor = System.Drawing.Color.Yellow
        End If
        If Game_time = 0 AndAlso Players_First <> 0 Then
            If Check_returns Then
                Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
                dsmts.BeginInvoke(player1_name, Player2_name, 8, 0, 0, 2, _
                    Nothing, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                MessageBox.Show("Hết H thua rồi")
                System.Math.Max(System.Threading.Interlocked.Increment(Player2_score), Player2_score - 1)
                player1_win = 1
                game_reset()
                Check_returns = False
            End If
        End If
    End Sub

    Private Sub bt_ok_Click(sender As Object, e As EventArgs) Handles bt_ok.Click
        Try
            Player2_name = ComboBox1.SelectedItem.ToString()
        Catch ex As Exception
        End Try
        lb_times.Visible = True
        bt_ok.Visible = False
        Group_Player2_Properties.Text = Player2_name
        lb_player2_score.Text = Player2_score.ToString()
        Player1_Types = 0
        game_reset()
        Check_returns = False
        Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
        dsmts.BeginInvoke(player1_name, Player2_name, 3, 0, 0, player1_win, _
            Nothing, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
    End Sub

    Private Sub Game_tables_MouseClick(sender As Object, e As MouseEventArgs) Handles Game_tables.MouseClick
        If Check_flags = 1 Then
            lb_times.Visible = False
            bt_ok.Visible = True
            Check_flags = 0
        End If
        If Game_Ready = 1 Then
            bt_ok.Visible = False
            Group_Player2_Properties.Text = Player2_name
            lb_player2_score.Text = Player2_score.ToString()
            Game_Ready = 0
            game_reset()
        End If
        If Check_returns Then
            If player2_win = 0 Then
                draw(e.X, e.Y)
                Game_time = 120
                Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
                dsmts.BeginInvoke(player1_name, Player2_name, 1, (e.X), (e.Y), player1_win, _
                    Nothing, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                If player1_win = 1 Then
                    player1_win = 0
                End If
            Else
                If player2_win = 2 Then
                    Dim dsmts As New DelegateSendValuePrivate(AddressOf obj.SendValuePrivate)
                    dsmts.BeginInvoke(player1_name, Player2_name, 9, 0, 0, player1_win, _
                        Nothing, New AsyncCallback(AddressOf DoNothingInCallBack), Nothing)
                    Player1_Types = 0
                    game_reset()
                    Check_returns = False
                    System.Math.Max(System.Threading.Interlocked.Increment(Player1_score), Player1_score - 1)
                    lb_player1_score.Text = Player1_score.ToString()
                    lb_player2_score.Text = Player2_score.ToString()
                Else
                    MessageBox.Show("U Lose Play again OK!")
                    System.Math.Max(System.Threading.Interlocked.Increment(Player2_score), Player2_score - 1)
                    lb_player1_score.Text = Player1_score.ToString()
                    lb_player2_score.Text = Player2_score.ToString()
                    game_reset()
                    Check_returns = True
                    Player1_Types = 1
                    Players_First = 0
                End If
            End If
        End If
    End Sub

   
End Class