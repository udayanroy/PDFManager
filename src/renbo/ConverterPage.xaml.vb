Imports System.ComponentModel

Class ConverterPage


    Dim outputPdf As String
    Dim imagefiles As List(Of String)
    Dim convertOptions As pdfoption



    Dim WithEvents worker As New BackgroundWorker



    Public Sub New(pdffile As String, images As List(Of String), pdfoption As pdfoption)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        '/////
        outputPdf = pdffile
        imagefiles = images
        convertOptions = pdfoption

        '//////////////
        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True
        '//////////////////
        makingProgress.Maximum = imagefiles.Count
        makingProgress.Value = 0

    End Sub


    Private Sub ConverterPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        worker.RunWorkerAsync()
    End Sub




    Private Sub worker_DoWork(sender As Object, e As DoWorkEventArgs) Handles worker.DoWork
        Try

            Dim converter As New PdfMaker(outputPdf)
            ' converter.Initialize(convertOptions)

            For i As Integer = 0 To imagefiles.Count - 1
                converter.AddImage(imagefiles(i))
                worker.ReportProgress(i + 1)

            Next

            converter.close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            e.Cancel = True
        End Try
    End Sub

    Private Sub worker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles worker.ProgressChanged
        makingProgress.Value = e.ProgressPercentage
    End Sub


    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        Pressme.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub Pressme_Click(sender As Object, e As RoutedEventArgs) Handles Pressme.Click
        NavigationService.GoBack()
    End Sub
End Class
