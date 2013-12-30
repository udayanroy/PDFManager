Imports System.Globalization

Class Imglist

    Dim _lst As New List(Of imageItem)

    Dim optionpage As optionpage

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

                Imglistview.Items.Add(New imageItem(fl))

            Next

        End If

    End Sub

    Private Sub Remove_Click(sender As Object, e As RoutedEventArgs) Handles Remove.Click
        
        Dim selected = New ArrayList(Imglistview.SelectedItems)
        For Each item In selected

            Imglistview.Items.Remove(item) '; // or whereever you need to remove them..

        Next
    End Sub

    Private Sub Clear_Click(sender As Object, e As RoutedEventArgs) Handles Clear.Click
        Imglistview.Items.Clear()

    End Sub

    Private Sub Up_Click(sender As Object, e As RoutedEventArgs) Handles Up.Click
        If Imglistview.SelectedItems.Count <> 0 Then

            Dim selected = New ArrayList(Imglistview.SelectedItems)
            selected.Sort(New itemCompare(Imglistview))

            If Imglistview.Items.IndexOf(selected(0)) > 0 Then
                For Each item In selected
                    Dim index = Imglistview.Items.IndexOf(item)

                    Imglistview.Items.Remove(item) '; // or whereever you need to remove them..

                    Imglistview.Items.Insert(index - 1, item)


                Next
                For Each item In selected
                    Imglistview.SelectedItems.Add(item)
                Next
            End If

        End If


    End Sub

    Private Sub Down_Click(sender As Object, e As RoutedEventArgs) Handles Down.Click

        If Imglistview.SelectedItems.Count <> 0 Then
            Dim selected = New ArrayList(Imglistview.SelectedItems)

            selected.Sort(New itemCompare(Imglistview))
            selected.Reverse()

            If Imglistview.Items.IndexOf(selected(0)) < Imglistview.Items.Count - 1 Then
                For Each item In selected
                    Dim index = Imglistview.Items.IndexOf(item)

                    Imglistview.Items.Remove(item) '; // or whereever you need to remove them..

                    Imglistview.Items.Insert(index + 1, item)


                Next
                For Each item In selected
                    Imglistview.SelectedItems.Add(item)
                Next
            End If
        End If

    End Sub


    Private Class itemCompare
        Implements IComparer

        Dim _lv As ListView

        Public Sub New(lv As ListView)
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


    Private Sub Next_Click(sender As Object, e As RoutedEventArgs) Handles [Next].Click
        Me.NavigationService.Navigate(optionpage)
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' Imglistview.ItemsSource = _lst
        optionpage = New optionpage(Imglistview.Items)
    End Sub
End Class

Public Class imageItem

    Public Property Name As String
    Public Property Size As String
    Public Property Dimensions As String
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Path As String
    Public Property FullName As String


    Public Sub New(filename As String)

        Dim imgInfo As New IO.FileInfo(filename)

        Name = imgInfo.Name
        Size = Convert(imgInfo.Length).ToString
        Path = imgInfo.DirectoryName
        FullName = filename

        Dim timg As BitmapImage = New BitmapImage(New Uri(filename))

        Dimensions = timg.PixelWidth & "×" & timg.PixelHeight
        Width = timg.PixelWidth
        Height = timg.PixelHeight
        timg.Freeze()
        
    End Sub

    Public Function Convert(value As String) As String
        Dim units As String() = {"B", "KB", "MB", "GB", "TB", "PB", _
            "EB", "ZB", "YB"}
        Dim size As Double = CLng(value)
        Dim unit As Integer = 0

        While size >= 1024
            size /= 1024
            unit += 1
        End While

        Return [String].Format("{0:0.#} {1}", size, units(unit))
    End Function
End Class