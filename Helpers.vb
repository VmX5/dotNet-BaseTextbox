Imports System.Runtime.CompilerServices

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

    Public Function resize(ByVal line As Line) As Size
        Dim returnSize As New Size
        Dim str As String = ""

        For Each segment As Segment In line.Segments
            str &= segment.Text
        Next

        For Each charSize As Size In MeasureTextSize(str, line.Segments(0).Font)
            returnSize = New Size(returnSize.Width + charSize.Width, MeasureString(line.Segments(0).Text, line.Segments(0).Font).Height)
        Next
    End Function

    Public Structure Segment
        Public Property Font As Font
        Public Property g As Graphics
        Private _Text As String

        Public Property Text As String
            Get
                Return _Text
            End Get
            Set(value As String)
                _Text = value
            End Set
        End Property

        Public Sub reSize()
            For Each charSize As Size In MeasureTextSize(_Text, Font)
                _Size.Width += charSize.Width
                _Size.Height = charSize.Height
            Next
        End Sub

        Private _Size As Size

        Public ReadOnly Property Size As Size
            Get
                Return _Size
            End Get
        End Property

        Public Function Length()
            Return Text.Length
        End Function
    End Structure

    Public Class Line
        Private _segments As New List(Of Segment)
        Public Property Segments As List(Of Segment)
            Get
                Return _segments
            End Get
            Set(value As List(Of Segment))
                _segments = value
            End Set
        End Property
        Private _size As Size
        Public Property Size As Size
            Get
                Return _size
            End Get
            Set(value As Size)
                _size = value
            End Set
        End Property
        Private _text As String
        Public ReadOnly Property Text As String
            Get
                Return _text
            End Get
        End Property

        Public Sub resize()
            _text = ""
            _size = New Size()

            For Each segment As Segment In _segments
                _text &= segment.Text
            Next

            For Each charSize As Size In MeasureTextSize(_text, _segments(0).Font)
                _size = New Size(_size.Width + charSize.Width, MeasureString(_segments(0).Text, _segments(0).Font).Height)
            Next
        End Sub

        Public Sub Add(font As Font, text As String, g As Graphics)
            _segments.Add(New Segment() With {.Font = font, .Text = text, .g = g})
            resize()
        End Sub
    End Class
End Module