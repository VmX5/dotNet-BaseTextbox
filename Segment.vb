Public Class Segment

    Private _index As Integer
    Public Property Index As Integer
        Get
            Return _index
        End Get
        Set(value As Integer)
            _index = value
        End Set
    End Property

    Private _length As Integer
    Public Property Length As Integer
        Get
            Return _length
        End Get
        Set(value As Integer)
            _length = value
        End Set
    End Property
End Class