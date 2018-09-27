Public Class bTextbox
    Inherits Control

    Sub New()
        DoubleBuffered = True
        Size = New Size(100, 20)
        Text = Name
    End Sub

    Dim ActiveArea As New Rectangle(New Point(1, 3), New Size(Width - 2, Height - 6))

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics

        g.Clear(Color.White)
        ControlPaint.DrawBorder(g, ClientRectangle, Color.Black, ButtonBorderStyle.Solid)

        Dim pointerPosition As New Point(0, 2)
        For Each ProcessedLine As String In ProcessLines(Text, Font, g)
            TextRenderer.DrawText(g, ProcessedLine, Font, New Point(ActiveArea.X + pointerPosition.X, ActiveArea.Y + pointerPosition.Y), ForeColor)
            pointerPosition.Y += MeasureString(IIf(String.IsNullOrEmpty(ProcessedLine), "I", ProcessedLine), Font).Height
        Next
    End Sub

    Private Function ProcessLines(input As String, font As Font, g As Graphics) As String()
        Dim processedLines As New List(Of String)

        For Each newLine As String In input.Split(New String() {Environment.NewLine}, StringSplitOptions.None)
            processedLines.Add(newLine)
        Next

        Return processedLines.ToArray
    End Function

    Protected Overrides Sub OnParentFontChanged(e As EventArgs)
        MyBase.OnParentFontChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnFontChanged(e As EventArgs)
        MyBase.OnFontChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub
End Class