Imports System.ComponentModel

Class MainWindow

    Dim WithEvents img2pdfworker As New BackgroundWorker




    Private Sub Add_Click(sender As Object, e As RoutedEventArgs) Handles Add.Click
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        'dlg.FileName = "Document" ' Default file name
        ' dlg.DefaultExt = ".pdf" ' Default file extension
        dlg.Filter = "Text documents (.pdf)|*.pdf" ' Filter files by extension
        dlg.Multiselect = True
        ' Show open file dialog box 
        Dim result? As Boolean = dlg.ShowDialog()

        ' Process open file dialog box results 
        If result = True Then
            ' Open document 
            For Each filename As String In dlg.FileNames
                pdflist.Items.Add(filename)
            Next

        End If
    End Sub

    Private Sub remove_Click(sender As Object, e As RoutedEventArgs) Handles remove.Click
        Dim n = pdflist.SelectedIndex
        If n <> -1 Then
            pdflist.Items.RemoveAt(n)
        End If

    End Sub

    Private Sub add1_Click(sender As Object, e As RoutedEventArgs) Handles add1.Click
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        'dlg.FileName = "Document" ' Default file name
        ' dlg.DefaultExt = ".pdf" ' Default file extension
        ' dlg.Filter = "Text documents (.pdf)|*.pdf" ' Filter files by extension
        dlg.Multiselect = True
        ' Show open file dialog box 
        Dim result? As Boolean = dlg.ShowDialog()

        'Process open file dialog box results 
        If result = True Then
            ' Open document 
            For Each filename As String In dlg.FileNames
                imglist.Items.Add(filename)
            Next




        End If
    End Sub

    Private Sub remove1_Click(sender As Object, e As RoutedEventArgs) Handles remove1.Click
        Dim n = imglist.SelectedIndex
        If n <> -1 Then
            imglist.Items.RemoveAt(n)
        End If
    End Sub

    Private Sub img2pdfstart_Click(sender As Object, e As RoutedEventArgs) Handles img2pdfstart.Click

    End Sub

    
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        img2pdfworker.WorkerReportsProgress = True
        img2pdfworker.WorkerSupportsCancellation = True

    End Sub

    Private Sub img2pdfworker_DoWork(sender As Object, e As DoWorkEventArgs) Handles img2pdfworker.DoWork

    End Sub
End Class
