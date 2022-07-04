Public Class GlobalVariable
    Private _count As String

    Public Property user() As String
        Get
            Return _count
        End Get
        Set(ByVal value As String)
            _count = value
        End Set
    End Property
End Class
