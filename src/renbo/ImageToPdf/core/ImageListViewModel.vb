


Imports System.Collections.ObjectModel

Public Class ImageListViewModel

    Public Sub New()
        Me.ImageList = New ObservableCollection(Of imageItem)
        Me.SelectedImages = New ObservableCollection(Of imageItem)
    End Sub

    Public ReadOnly Property ImageList As ObservableCollection(Of imageItem)
    Public Property SelectedImages As ObservableCollection(Of imageItem)

    Public Sub AddImage()

    End Sub

    Public Sub AddImages(filenames As String())

        For Each fl As String In filenames
            Me.ImageList.Add(New imageItem(fl))
        Next

    End Sub


End Class
