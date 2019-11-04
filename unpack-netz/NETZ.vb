Imports System.Configuration
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip.Compression.Streams

Public Class NETZ
    Public Shared Sub GenerateDecryptedFile(data As Byte())
        Dim memoryStream As MemoryStream = Nothing

        Try
            memoryStream = UnZip(data)
            memoryStream.Seek(0L, SeekOrigin.Begin)

            Dim aux = New MemoryStream(memoryStream.ToArray)
            Dim path = ConfigurationManager.AppSettings("OUTPUT_FILE")
            path = Helpers.GetExecutionPath() + path
            Dim file = New FileStream(path, FileMode.Create, FileAccess.Write)

            aux.WriteTo(file)
            file.Close()
        Finally
            If memoryStream IsNot Nothing Then
                memoryStream.Close()
            End If
        End Try
    End Sub

    Private Shared Function UnZip(data As Byte()) As MemoryStream
        If data Is Nothing Then
            Return Nothing
        End If

        Dim memoryStream As MemoryStream = Nothing
        Dim aux As MemoryStream = Nothing
        Dim inflaterInputStream As InflaterInputStream = Nothing

        Try
            memoryStream = New MemoryStream(data)
            aux = New MemoryStream()
            inflaterInputStream = New InflaterInputStream(memoryStream)
            Dim array As Byte() = New Byte(data.Length - 1) {}

            While True
                Dim num As Integer = inflaterInputStream.Read(array, 0, array.Length)
                If num <= 0 Then
                    Exit While
                End If
                aux.Write(array, 0, num)
            End While

            aux.Flush()
            aux.Seek(0L, SeekOrigin.Begin)

        Finally
            If memoryStream IsNot Nothing Then
                memoryStream.Close()
            End If
            If inflaterInputStream IsNot Nothing Then
                inflaterInputStream.Close()
            End If
        End Try

        Return aux
    End Function
End Class
