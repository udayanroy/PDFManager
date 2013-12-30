Imports System.ComponentModel

Class ProgressPage


    Dim _Items As ItemCollection
    Dim _outFile As String
    Dim _metadata As PDFmetadata

    Dim imgfilecount As Integer

    Dim WithEvents worker As New BackgroundWorker


    Public Sub New(list As ItemCollection, metadata As PDFmetadata)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Items = list
        '   _outFile = metadata.OutputFile
        _metadata = metadata




        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True

    End Sub


    Private Sub ProgressPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        imgfilecount = _Items.Count

        pgbar.Value = 0
        worker.RunWorkerAsync()
        pstatus.Text = "Converting..."

        ok.Visibility = Windows.Visibility.Hidden
        Cancle.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub worker_DoWork(sender As Object, e As DoWorkEventArgs) Handles worker.DoWork
        Try
            If IO.File.Exists(_metadata.OutputFile) Then
                IO.File.Delete(_metadata.OutputFile)
            End If

            Dim converter As New PdfMaker(_metadata.OutputFile)
            converter.Initialize(_metadata)

            For i As Integer = 0 To _Items.Count - 1
                converter.AddImage(_Items(i).FullName)
                worker.ReportProgress(i + 1)

            Next

            converter.close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            e.Cancel = True
        End Try
    End Sub



    Private Sub worker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles worker.ProgressChanged
        score.Text = e.ProgressPercentage & " of " & imgfilecount
        pgbar.Value = (e.ProgressPercentage * 100) / imgfilecount
    End Sub

    Private Sub Cancle_Click(sender As Object, e As RoutedEventArgs) Handles Cancle.Click
        If worker.IsBusy Then
            worker.CancelAsync()
            IO.File.Delete(_metadata.OutputFile)
        End If
        Me.NavigationService.GoBack()
        Me.NavigationService.GoBack()
        Me.NavigationService.GoBack()
    End Sub

    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        pstatus.Text = "Completed"

        ok.Visibility = Windows.Visibility.Visible
        Cancle.Visibility = Windows.Visibility.Hidden

    End Sub

    Private Sub ok_Click(sender As Object, e As RoutedEventArgs) Handles ok.Click

        Me.NavigationService.GoBack()
        Me.NavigationService.GoBack()
        Me.NavigationService.GoBack()

    End Sub
End Class
