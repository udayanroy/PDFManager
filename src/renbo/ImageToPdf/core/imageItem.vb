


Public Class imageItem

    Public Property Name As String
    Public Property Size As String
    Public Property Dimensions As String
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Path As String
    Public Property FullName As String


    Public Sub New(filename As String)

        Dim imgInfo As New IO.FileInfo(filename)

        Name = imgInfo.Name
        Size = Convert(imgInfo.Length).ToString
        Path = imgInfo.DirectoryName
        FullName = filename

        Dim timg As BitmapImage = New BitmapImage(New Uri(filename))

        Dimensions = timg.PixelWidth & "×" & timg.PixelHeight
        Width = timg.PixelWidth
        Height = timg.PixelHeight
        timg.Freeze()

    End Sub

    Public Function Convert(value As String) As String
        Dim units As String() = {"B", "KB", "MB", "GB", "TB", "PB",
            "EB", "ZB", "YB"}
        Dim size As Double = CLng(value)
        Dim unit As Integer = 0

        While size >= 1024
            size /= 1024
            unit += 1
        End While

        Return [String].Format("{0:0.#} {1}", size, units(unit))
    End Function
End Class