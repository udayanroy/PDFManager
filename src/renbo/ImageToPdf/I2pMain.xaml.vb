Imports System.Threading.Tasks
Imports System.Windows.Threading
Imports System.ComponentModel

Class I2pMain

    Private _option As PDFmetadata

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
        enabledesableButtons()
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
        enabledesableButtons()
    End Sub

    Private Sub Remove_Click(sender As Object, e As RoutedEventArgs) Handles Remove.Click
        Dim selected = New ArrayList(imgList.SelectedItems)
        For Each item In selected

            imgList.Items.Remove(item) '; // or whereever you need to remove them..

        Next
        enabledesableButtons()
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
                imgList.ScrollIntoView(selected(0))
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
                imgList.ScrollIntoView(selected(0))
            End If
        End If
    End Sub

    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs) Handles Clear.Click
        imgList.Items.Clear()
        enabledesableButtons()
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

            file = dlg.FileName
            progbar.Value = 0
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
        'progbar.Progress = p
    End Sub

    Private Sub SavePDF()

        Dim _metadata = _option 'New PDFmetadata
        _metadata.OutputFile = file
        If IO.File.Exists(_metadata.OutputFile) Then
            IO.File.Delete(_metadata.OutputFile)
        End If

        Dim converter As New PdfMaker(_metadata.OutputFile)
        converter.Initialize(_metadata)
        Dim nimg = imgList.Items.Count
        For i As Integer = 0 To nimg - 1
            converter.AddImage(imgList.Items(i).FullName)

            progbar.Value = CInt((i * 100) / nimg)
            ' progbar.Dispatcher.BeginInvoke(New ProgressUpdateDelegate(AddressOf Me.UpdateProgress), CInt((i * 100) / nimg))
        Next

        converter.close()
    End Sub





    Private Sub worker_DoWork(sender As Object, e As DoWorkEventArgs) Handles worker.DoWork
        Try



            Dim _metadata = _option 'PDFmetadata.FromPDFOption() 'As New PDFmetadata
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
                If worker.CancellationPending Then
                    e.Cancel = True
                    Exit For
                End If
            Next

            converter.close()

            If e.Cancel Then
                IO.File.Delete(_metadata.OutputFile)
            End If
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
        progbar.Value = progress
    End Sub

    Private Sub worker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles worker.RunWorkerCompleted
        progGrid.Visibility = Windows.Visibility.Hidden
        If e.Cancelled Then
            If IO.File.Exists(file) Then
                IO.File.Delete(file)
            End If
        End If
    End Sub

    Private Sub enabledesableButtons()
        Dim ImageCount = imgList.Items.Count
        Dim SelectionCount = imgList.SelectedItems.Count

        If ImageCount = 0 Then
            Add.IsEnabled = True
            Remove.IsEnabled = False
            up.IsEnabled = False
            down.IsEnabled = False
            Clear.IsEnabled = False
            pdfsave.IsEnabled = False
            settingsBtn.IsEnabled = False
            PrevwBtn.IsEnabled = False
        ElseIf SelectionCount = 0 Then
            Add.IsEnabled = True
            Remove.IsEnabled = False
            up.IsEnabled = False
            down.IsEnabled = False
            Clear.IsEnabled = True
            pdfsave.IsEnabled = True
            settingsBtn.IsEnabled = True
            PrevwBtn.IsEnabled = True
        Else
            Add.IsEnabled = True
            Remove.IsEnabled = True
            up.IsEnabled = True
            down.IsEnabled = True
            Clear.IsEnabled = True
            pdfsave.IsEnabled = True
            settingsBtn.IsEnabled = True
            PrevwBtn.IsEnabled = True
        End If
    End Sub

    Private Sub I2pMain_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        _option = PDFmetadata.CreateNew
        enabledesableButtons()
    End Sub

    Private Sub settingsBtn_Click(sender As Object, e As RoutedEventArgs) Handles settingsBtn.Click
        Dim settingdlg = New PDFSettingDlg()

        ' Configure the dialog box
        settingdlg.Owner = Window.GetWindow(Me)
        'dlg.DocumentMargin = this.documentTextBox.Margin;
        settingdlg.SetPDFOption(_option)
        ' Open the dialog box modally 
        Dim dr = settingdlg.ShowDialog()
        If dr Then
            _option = settingdlg.getPDFOption
        End If

    End Sub

    Private Sub CancleBtn_Click(sender As Object, e As RoutedEventArgs) Handles CancleBtn.Click

        If worker.IsBusy Then
            worker.CancelAsync()
        End If

    End Sub

    Private Sub PrevwBtn_Click(sender As Object, e As RoutedEventArgs) Handles PrevwBtn.Click
        Dim prvdlg = New PreviewWindow()

        ' Configure the dialog box
        prvdlg.Owner = Window.GetWindow(Me)
        'dlg.DocumentMargin = this.documentTextBox.Margin;
        prvdlg.SetData(toList, _option)
        ' Open the dialog box modally 
        prvdlg.ShowDialog()


    End Sub

    Private Function toList() As List(Of imageItem)
        Dim lst As New List(Of imageItem)(imgList.Items.Count)

        For Each item As imageItem In imgList.Items
            lst.Add(item)
        Next

        Return lst
    End Function

    Private Sub infoBtn_Click(sender As Object, e As RoutedEventArgs) Handles infoBtn.Click
        cntxmnu.IsOpen = True
        cntxmnu.StaysOpen = True


    End Sub
End Class


