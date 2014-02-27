Imports System.Threading.Tasks
Imports System.Windows.Threading
Imports System.ComponentModel

Class I2pMain

    Private Sub Add_Click(sender As Object, e As RoutedEventArgs) Handles Add.Click
        ' Configure open file dialog box 
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        ' dlg.FileName = "Document" ' Default file name
        ' dlg.DefaultExt = ".txt" ' Default file extension
        dlg.Filter = "All Images |*.bmp;*.png;*.jpg;*.tif;*.gif;*.ico" ' Filter files by extension
        dlg.Multiselect = True
        ' Show open file dialog box 
        Dim result? As Boolean = dlg.ShowDialog()

        ' Process open file dialog box results 
        If result = True Then
            ' Open document 
            '  Dim filename As String = dlg.FileName
            For Each fl As String In dlg.FileNames

                imgList.Items.Add(New imageItem(fl))

            Next

        End If
    End Sub

    Private Sub imgList_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles imgList.SelectionChanged
        If imgList.SelectedIndex <> -1 Then
            Dim item As imageItem = imgList.SelectedItem
            Dim memorystrm As New IO.MemoryStream(IO.File.ReadAllBytes(item.FullName))
            Dim btm As New BitmapImage

            btm.BeginInit()
            ' btm.UriSource = New Uri(item.FullName)
            ' btm.CacheOption = BitmapCacheOption.OnLoad
            btm.StreamSource = memorystrm
            btm.EndInit()

            imgview.Source = btm
            btm.Freeze()
            ' End Using

        End If

    End Sub

    Private Sub Remove_Click(sender As Object, e As RoutedEventArgs) Handles Remove.Click
        Dim selected = New ArrayList(imgList.SelectedItems)
        For Each item In selected

            imgList.Items.Remove(item) '; // or whereever you need to remove them..

        Next
    End Sub

    Private Sub up_Click(sender As Object, e As RoutedEventArgs) Handles up.Click
        If imgList.SelectedItems.Count <> 0 Then

            Dim selected = New ArrayList(imgList.SelectedItems)
            selected.Sort(New itemCompare(imgList))

            If imgList.Items.IndexOf(selected(0)) > 0 Then
                For Each item In selected
                    Dim index = imgList.Items.IndexOf(item)

                    imgList.Items.Remove(item) '; // or whereever you need to remove them..

                    imgList.Items.Insert(index - 1, item)


                Next
                For Each item In selected
                    imgList.SelectedItems.Add(item)
                Next
            End If

        End If

    End Sub

    Private Class itemCompare
        Implements IComparer

        Dim _lv As ListBox

        Public Sub New(lv As ListBox)
            _lv = lv
        End Sub

        Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
            Dim ix = _lv.Items.IndexOf(x)
            Dim iy = _lv.Items.IndexOf(y)

            If ix > iy Then
                Return 1
            ElseIf ix < iy Then
                Return -1
            Else
                Return 0
            End If

        End Function
    End Class



    Private Sub down_Click(sender As Object, e As RoutedEventArgs) Handles down.Click
        If imgList.SelectedItems.Count <> 0 Then
            Dim selected = New ArrayList(imgList.SelectedItems)

            selected.Sort(New itemCompare(imgList))
            selected.Reverse()

            If imgList.Items.IndexOf(selected(0)) < imgList.Items.Count - 1 Then
                For Each item In selected
                    Dim index = imgList.Items.IndexOf(item)

                    imgList.Items.Remove(item) '; // or whereever you need to remove them..

                    imgList.Items.Insert(index + 1, item)


                Next
                For Each item In selected
                    imgList.SelectedItems.Add(item)
                Next
            End If
        End If
    End Sub

    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs) Handles Clear.Click
        imgList.Items.Clear()
    End Sub

    Private Sub pdfsave_Click(sender As Object, e As RoutedEventArgs) Handles pdfsave.Click

        Dim dlg As New Microsoft.Win32.SaveFileDialog()
        dlg.FileName = "Document" ' Default file name
        ' dlg.DefaultExt = ".txt" ' Default file extension
        dlg.Filter = "PDF documents(.pdf)| *.pdf" ' Filter files by extension

        ' Show open file dialog box 
        Dim result? As Boolean = dlg.ShowDialog()

        ' Process open file dialog box results 
        If result = True Then
            If imgList.SelectedItems.Count = 0 Then Exit Sub

            file = dlg.FileName
            progbar.Progress = 0
            progGrid.Visibility = Windows.Visibility.Visible
            ' progbar.Dispatcher.BeginInvoke(DispatcherPriority.Normal, New Action(AddressOf SavePDF))
            ' Dim task As New Task(AddressOf SavePDF)
            ' task.Start()

            worker.RunWorkerAsync()
        End If




    End Sub

    Dim WithEvents worker As New BackgroundWorker




    Dim file As String
    Delegate Sub ProgressUpdateDelegate(p As Integer)
    Dim progress As Double

    Sub UpdateProgress(p As Integer)
        progbar.Progress = p
    End Sub

    Private Sub SavePDF()

        Dim _metadata As New PDFmetadata
        _metadata.OutputFile = file
        If IO.File.Exists(_metadata.OutputFile) Then
            IO.File.Delete(_metadata.OutputFile)
        End If

        Dim converter As New PdfMaker(_metadata.OutputFile)
        converter.Initialize(_metadata)
        Dim nimg = imgList.Items.Count
        For i As Integer = 0 To nimg - 1
            converter.AddImage(imgList.Items(i).FullName)

            progbar.Progress = CInt((i * 100) / nimg)
            ' progbar.Dispatcher.BeginInvoke(New ProgressUpdateDelegate(AddressOf Me.UpdateProgress), CInt((i * 100) / nimg))
        Next

        converter.close()
    End Sub





    Private Sub worker_DoWork(sender As Object, e As DoWorkEventArgs) Handles worker.DoWork
        Try



            Dim _metadata As New PDFmetadata
            _metadata.OutputFile = file
            If IO.File.Exists(_metadata.OutputFile) Then
                IO.File.Delete(_metadata.OutputFile)
            End If

            Dim converter As New PdfMaker(_metadata.OutputFile)
            converter.Initialize(_metadata)
            Dim nimg = imgList.Items.Count
            For i As Integer = 0 To nimg - 1
                converter.AddImage(imgList.Items(i).FullName)
                progress = (i * 100) / nimg
                worker.ReportProgress(progress)
                ' progbar.Dispatcher.BeginInvoke(New ProgressUpdateDelegate(AddressOf Me.UpdateProgress), CInt((i * 100) / nimg))
            Next

            converter.close()
        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True
    End Sub

    Private Sub worker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles worker.ProgressChanged
        progbar.Progress = progress
    End Sub

    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        progGrid.Visibility = Windows.Visibility.Hidden
    End Sub
End Class


