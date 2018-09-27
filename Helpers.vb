Public Module Helpers
    Declare Function CreateCaret Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal hBmp As IntPtr, ByVal w As Integer, ByVal h As Integer) As Boolean
    Declare Function SetCaretPos Lib "user32.dll" (ByVal x As Integer, ByVal y As Integer) As Boolean
    Declare Function ShowCaret Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean
    Declare Function DestroyCaret Lib "user32.dll" () As Boolean

    Public Function MeasureTextSize(input As String, ByVal font As Font) As Size()
        Dim characterSizes(input.Length) As Size
        For i As Integer = 0 To input.Length - 1
            characterSizes(i).Width = MeasureString(input.Substring(i, 1), font).Width
            characterSizes(i).Height = TextRenderer.MeasureText(input.Substring(i, 1), font).Height
            If input.Substring(i, 1) = " " Then
                characterSizes(i).Width += 1
            End If
        Next
        Return characterSizes
    End Function

    Public Function MeasureString(ByVal text As String, ByVal font As Font) As Size
        Dim stringFormat As StringFormat = StringFormat.GenericTypographic
        Dim size As New Size
        stringFormat.FormatFlags = StringFormatFlags.MeasureTrailingSpaces
        size.Width = Graphics.FromImage(New Bitmap(1, 1)).MeasureString(text, font, -1, stringFormat).Width
        size.Height = Graphics.FromImage(New Bitmap(1, 1)).MeasureString(text, font, -1, stringFormat).Height
        Return size
    End Function
End Module