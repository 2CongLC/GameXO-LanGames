<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Game
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Group_Player1_Properties = New System.Windows.Forms.GroupBox()
        Me.lb_player1_score = New System.Windows.Forms.Label()
        Me.Group_Player2_Properties = New System.Windows.Forms.GroupBox()
        Me.lb_player2_score = New System.Windows.Forms.Label()
        Me.Game_tables = New System.Windows.Forms.PictureBox()
        Me.Group_Times = New System.Windows.Forms.GroupBox()
        Me.lb_times = New System.Windows.Forms.Label()
        Me.bt_ok = New System.Windows.Forms.Button()
        Me.Time_Rounds = New System.Windows.Forms.Timer(Me.components)
        Me.Rich_ReceiverChats = New System.Windows.Forms.RichTextBox()
        Me.Rich_SendChats = New System.Windows.Forms.RichTextBox()
        Me.bt_SendChats = New System.Windows.Forms.Button()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Group_Player1_Properties.SuspendLayout()
        Me.Group_Player2_Properties.SuspendLayout()
        CType(Me.Game_tables, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Group_Times.SuspendLayout()
        Me.SuspendLayout()
        '
        'Group_Player1_Properties
        '
        Me.Group_Player1_Properties.Controls.Add(Me.lb_player1_score)
        Me.Group_Player1_Properties.Location = New System.Drawing.Point(572, 136)
        Me.Group_Player1_Properties.Name = "Group_Player1_Properties"
        Me.Group_Player1_Properties.Size = New System.Drawing.Size(199, 99)
        Me.Group_Player1_Properties.TabIndex = 0
        Me.Group_Player1_Properties.TabStop = False
        Me.Group_Player1_Properties.Text = "Player1"
        '
        'lb_player1_score
        '
        Me.lb_player1_score.AutoSize = True
        Me.lb_player1_score.Location = New System.Drawing.Point(41, 38)
        Me.lb_player1_score.Name = "lb_player1_score"
        Me.lb_player1_score.Size = New System.Drawing.Size(39, 13)
        Me.lb_player1_score.TabIndex = 0
        Me.lb_player1_score.Text = "Label1"
        '
        'Group_Player2_Properties
        '
        Me.Group_Player2_Properties.Controls.Add(Me.lb_player2_score)
        Me.Group_Player2_Properties.Location = New System.Drawing.Point(572, 241)
        Me.Group_Player2_Properties.Name = "Group_Player2_Properties"
        Me.Group_Player2_Properties.Size = New System.Drawing.Size(199, 99)
        Me.Group_Player2_Properties.TabIndex = 1
        Me.Group_Player2_Properties.TabStop = False
        Me.Group_Player2_Properties.Text = "Player2"
        '
        'lb_player2_score
        '
        Me.lb_player2_score.AutoSize = True
        Me.lb_player2_score.Location = New System.Drawing.Point(44, 38)
        Me.lb_player2_score.Name = "lb_player2_score"
        Me.lb_player2_score.Size = New System.Drawing.Size(39, 13)
        Me.lb_player2_score.TabIndex = 0
        Me.lb_player2_score.Text = "Label2"
        '
        'Game_tables
        '
        Me.Game_tables.Location = New System.Drawing.Point(12, 5)
        Me.Game_tables.Name = "Game_tables"
        Me.Game_tables.Size = New System.Drawing.Size(545, 481)
        Me.Game_tables.TabIndex = 2
        Me.Game_tables.TabStop = False
        '
        'Group_Times
        '
        Me.Group_Times.Controls.Add(Me.lb_times)
        Me.Group_Times.Location = New System.Drawing.Point(572, 64)
        Me.Group_Times.Name = "Group_Times"
        Me.Group_Times.Size = New System.Drawing.Size(199, 66)
        Me.Group_Times.TabIndex = 3
        Me.Group_Times.TabStop = False
        Me.Group_Times.Text = "Time_Round"
        '
        'lb_times
        '
        Me.lb_times.AutoSize = True
        Me.lb_times.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lb_times.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lb_times.Location = New System.Drawing.Point(72, 16)
        Me.lb_times.Name = "lb_times"
        Me.lb_times.Size = New System.Drawing.Size(59, 31)
        Me.lb_times.TabIndex = 0
        Me.lb_times.Text = "120"
        '
        'bt_ok
        '
        Me.bt_ok.Location = New System.Drawing.Point(358, 492)
        Me.bt_ok.Name = "bt_ok"
        Me.bt_ok.Size = New System.Drawing.Size(199, 56)
        Me.bt_ok.TabIndex = 4
        Me.bt_ok.Text = "OK"
        Me.bt_ok.UseVisualStyleBackColor = True
        '
        'Time_Rounds
        '
        Me.Time_Rounds.Enabled = True
        Me.Time_Rounds.Interval = 1000
        '
        'Rich_ReceiverChats
        '
        Me.Rich_ReceiverChats.Location = New System.Drawing.Point(572, 346)
        Me.Rich_ReceiverChats.Name = "Rich_ReceiverChats"
        Me.Rich_ReceiverChats.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth
        Me.Rich_ReceiverChats.Size = New System.Drawing.Size(199, 107)
        Me.Rich_ReceiverChats.TabIndex = 5
        Me.Rich_ReceiverChats.Text = ""
        '
        'Rich_SendChats
        '
        Me.Rich_SendChats.Location = New System.Drawing.Point(572, 459)
        Me.Rich_SendChats.Name = "Rich_SendChats"
        Me.Rich_SendChats.Size = New System.Drawing.Size(142, 51)
        Me.Rich_SendChats.TabIndex = 6
        Me.Rich_SendChats.Text = ""
        '
        'bt_SendChats
        '
        Me.bt_SendChats.Location = New System.Drawing.Point(720, 459)
        Me.bt_SendChats.Name = "bt_SendChats"
        Me.bt_SendChats.Size = New System.Drawing.Size(51, 51)
        Me.bt_SendChats.TabIndex = 7
        Me.bt_SendChats.Text = "Send"
        Me.bt_SendChats.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(572, 12)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(199, 21)
        Me.ComboBox1.TabIndex = 8
        '
        'Game
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 560)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.bt_SendChats)
        Me.Controls.Add(Me.Rich_SendChats)
        Me.Controls.Add(Me.Rich_ReceiverChats)
        Me.Controls.Add(Me.bt_ok)
        Me.Controls.Add(Me.Group_Times)
        Me.Controls.Add(Me.Game_tables)
        Me.Controls.Add(Me.Group_Player2_Properties)
        Me.Controls.Add(Me.Group_Player1_Properties)
        Me.MaximizeBox = False
        Me.Name = "Game"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game"
        Me.Group_Player1_Properties.ResumeLayout(False)
        Me.Group_Player1_Properties.PerformLayout()
        Me.Group_Player2_Properties.ResumeLayout(False)
        Me.Group_Player2_Properties.PerformLayout()
        CType(Me.Game_tables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Group_Times.ResumeLayout(False)
        Me.Group_Times.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Group_Player1_Properties As System.Windows.Forms.GroupBox
    Friend WithEvents Group_Player2_Properties As System.Windows.Forms.GroupBox
    Friend WithEvents Game_tables As System.Windows.Forms.PictureBox
    Friend WithEvents Group_Times As System.Windows.Forms.GroupBox
    Friend WithEvents bt_ok As System.Windows.Forms.Button
    Friend WithEvents Time_Rounds As System.Windows.Forms.Timer
    Friend WithEvents Rich_ReceiverChats As System.Windows.Forms.RichTextBox
    Friend WithEvents Rich_SendChats As System.Windows.Forms.RichTextBox
    Friend WithEvents bt_SendChats As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lb_times As System.Windows.Forms.Label
    Friend WithEvents lb_player1_score As System.Windows.Forms.Label
    Friend WithEvents lb_player2_score As System.Windows.Forms.Label
End Class
