Public Class AboutDialog
    Private Sub hyperlink_RequestNavigate(sender As Object, e As RequestNavigateEventArgs)
        Dim Uri = e.Uri.AbsoluteUri
        Process.Start(New ProcessStartInfo(Uri))

        e.Handled = True
    End Sub
End Class
