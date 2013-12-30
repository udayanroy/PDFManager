Public Class pictureviewer

    Dim _file As String = ""


    Public Sub setImageFile(file As String)

        _file = file
        Dim ImageSource = New BitmapImage(New Uri(_file))
        imgpreview.Source = ImageSource


    End Sub

End Class
