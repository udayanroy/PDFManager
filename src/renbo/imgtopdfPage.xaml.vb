Imports System.Collections.ObjectModel

Class imgtopdfPage



    Dim visibleItems As ObservableCollection(Of Object) = New ObservableCollection(Of Object)()


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FileList.ItemsSource = visibleItems
    End Sub




    Private Sub addbutton_Click(sender As Object, e As RoutedEventArgs) Handles addbutton.Click

        ' Configure open file dialog box 
        Dim dlg As New Microsoft.Win32.OpenFileDialog()
        dlg.FileName = "Document" ' Default file name
        ' dlg.DefaultExt = ".txt" ' Default file extension
        ' dlg.Filter = "Text documents (.txt)|*.txt" ' Filter files by extension
        dlg.Multiselect = True
        ' Show open file dialog box 
        Dim result? As Boolean = dlg.ShowDialog()

        ' Process open file dialog box results 
        If result = True Then
            ' Open document 
            '  Dim filename As String = dlg.FileName
            For Each fl As String In dlg.FileNames

                visibleItems.Add(fl)
            Next


        End If


    End Sub


End Class
