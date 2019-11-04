Public Class Helpers
    Public Shared Function GetExecutionPath() As String
        Return String.Format("{0}\", Environment.CurrentDirectory)
    End Function
End Class