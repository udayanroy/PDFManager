



Class MainWindow

    Dim converter As New Imglist() 'imgtopdfPage()

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ShowsNavigationUI = False

        Me.NavigationService.Navigate(converter)
    End Sub
End Class
