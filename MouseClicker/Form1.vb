Public Class Form1
    <System.Runtime.InteropServices.DllImport("USER32.DLL")> _
    Private Shared Sub mouse_event(ByVal dwFlags As Integer, ByVal dx As Integer _
                                   , ByVal dy As Integer, ByVal cButtons As Integer _
                                   , ByVal dwExtraInfo As Integer)
    End Sub

    Private Const MOUSEEVENTF_LEFTUP As Integer = &H4
    Private Const MOUSEEVENTF_LEFTDOWN As Integer = &H2
    Private Const MOUSEEVENTF_RIGHTUP As Integer = &H10
    Private Const MOUSEEVENTF_RIGHTDOWN As Integer = &H8

    Private ltimer As New Timers.Timer
    Private rtimer As New Timers.Timer

    Private lCount As Integer = 0
    Private rCount As Integer = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ltimer.AutoReset = True
        Me.ltimer.Interval = 1
        AddHandler Me.ltimer.Elapsed, AddressOf MouseLeftClickDispacher

        Me.rtimer.AutoReset = True
        Me.rtimer.Interval = 1
        AddHandler Me.rtimer.Elapsed, AddressOf MouseRightClickDispacher
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not Me.ltimer.Enabled Then
            Me.lCount = 0
            Me.Button1.Enabled = False
            Me.ltimer.Start()
        Else
            Me.ltimer.Stop()
            Me.Button1.Enabled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not Me.rtimer.Enabled Then
            Me.rCount = 0
            Me.Button2.Enabled = False
            Me.rtimer.Start()
        Else
            Me.rtimer.Stop()
            Me.Button2.Enabled = True
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.ltimer.Stop()
        Me.rtimer.Stop()
        Me.Button1.Enabled = True
        Me.Button2.Enabled = True
    End Sub

    Private Delegate Sub MouseDispacher()

    Private Sub MouseLeftClickDispacher()
        Me.BeginInvoke(New MouseDispacher(AddressOf MouseLeftClick))
    End Sub

    Private Sub MouseRightClickDispacher()
        Me.BeginInvoke(New MouseDispacher(AddressOf MouseRightClick))
    End Sub

    Private Sub MouseLeftClick()
        Me.lCount += 1
        Me.Label1.Text = Me.lCount.ToString
        If Me.lCount = CInt(Me.TextBox1.Text) Then
            Me.Button1.Enabled = True
            Me.ltimer.Stop()
        End If
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
    End Sub

    Private Sub MouseRightClick()
        Me.rCount += 1
        Me.Label2.Text = Me.rCount.ToString
        If Me.rCount = CInt(Me.TextBox1.Text) Then
            Me.Button2.Enabled = True
            Me.rtimer.Stop()
        End If
        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        Me.TopMost = Me.CheckBox1.Checked
    End Sub
End Class
