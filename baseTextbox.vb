Public Class baseTextbox
    Inherits Control

    Sub New()
        DoubleBuffered = True
        Size = New Size(100, 20)
        Text = Name
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)
        Me.OnGotFocus(New EventArgs)
    End Sub

    Protected Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)
        CreateCaret(Handle, IntPtr.Zero, 1, TextRenderer.MeasureText("I", Font).Height)
        SetCaretPos(30, 3)
        ShowCaret(Handle)
    End Sub

    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)
        DestroyCaret
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Up
                MsgBox("w")
                Return MyBase.ProcessCmdKey(msg, keyData)
            Case Keys.Left
                MsgBox("a")
                Return MyBase.ProcessCmdKey(msg, keyData)
            Case Keys.Down
                MsgBox("s")
                Return MyBase.ProcessCmdKey(msg, keyData)
            Case Keys.Right
                MsgBox("d")
                Return MyBase.ProcessCmdKey(msg, keyData)
        End Select
    End Function

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics

        g.Clear(Color.White)
        ControlPaint.DrawBorder(g, ClientRectangle, Color.Black, ButtonBorderStyle.Solid)

        Dim newG As Graphics = CreateGraphics()

        Dim pointerLocation As Point = New Point(1, 3)

        Dim processedLines As Line() = processText(Text, Font, g)
        For Each processedLine In processedLines
            TextRenderer.DrawText(g, processedLine.Text, Font, pointerLocation, ForeColor)
            pointerLocation.Y += processedLine.Size.Height
        Next
    End Sub

    Public Function processText(input As String, font As Font, g As Graphics) As Line()
        Dim processedLines As New List(Of Line)

        For Each NewLineByChar As String In input.Split(New String() {vbNewLine}, StringSplitOptions.None)

            Dim newProcessedLine As New Line
            Dim SegmentsByChar() As String = NewLineByChar.Split(" "c)
            For Each Segment In SegmentsByChar
                Dim FixedSegment As String = IIf(Segment = SegmentsByChar(0), "", " ") & Segment

                For Each CharsInSegment As Char In FixedSegment.ToCharArray

                    If newProcessedLine.Size.Width < (Width + 1) Then
                        newProcessedLine.Add(font, CharsInSegment, g)
                    Else
                        processedLines.Add(newProcessedLine)
                        newProcessedLine = New Line
                        newProcessedLine.Add(font, CharsInSegment, g)
                    End If

                Next

            Next
            processedLines.Add(newProcessedLine)
        Next

        Return processedLines.ToArray
    End Function

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

End Class