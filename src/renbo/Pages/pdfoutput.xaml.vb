Class pdfoutput
    Dim _mdata As PDFmetadata
    Dim progresspage As ProgressPage

    Private Sub outputfilebox_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles outputfilebox.MouseLeftButtonDown
        Dim dlg As New Microsoft.Win32.SaveFileDialog()
        dlg.FileName = "Document" ' Default file name
        ' dlg.DefaultExt = ".txt" ' Default file extension
        dlg.Filter = "PDF documents(.pdf)| *.pdf" ' Filter files by extension

        ' Show open file dialog box 
        Dim result? As Boolean = dlg.ShowDialog()

        ' Process open file dialog box results 
        If result = True Then
            ' Open document 
            '  Dim filename As String = dlg.FileName
           
            outputfilebox.Text = dlg.FileName
            _mdata.OutputFile = dlg.FileName
        End If

    End Sub

    Public Sub New(lst As ItemCollection, mdata As PDFmetadata)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _mdata = mdata
        progresspage = New ProgressPage(lst, mdata)
    End Sub

    Private Sub Next_Click(sender As Object, e As RoutedEventArgs) Handles [Next].Click

        If _mdata.OutputFile = "" Then
            MessageBox.Show("Please Select a File")
        Else
            Me.NavigationService.Navigate(progresspage)
        End If

    End Sub

    Private Sub Prev_Click(sender As Object, e As RoutedEventArgs) Handles Prev.Click
        Me.NavigationService.GoBack()
    End Sub
End Class
