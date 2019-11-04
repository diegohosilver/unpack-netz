Imports System.Configuration
Imports System.IO
Imports unpack_netz.NETZ

Module Module1

    Sub Main()
        'Path del archivo a desencriptar
        Dim path = ConfigurationManager.AppSettings("ENCRYPTED_FILE")
        path = Helpers.GetExecutionPath() + path

        'Generar nuevo archivo
        GenerateDecryptedFile(File.ReadAllBytes(path))
    End Sub

End Module
