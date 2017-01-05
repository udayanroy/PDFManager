


Imports System.ComponentModel

Public Class imageItem
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _dimension As String

    Public Property Name As String
    Public Property Size As String

    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Path As String
    Public Property FullName As String
    Public Property Dimensions As String
        Get
            Return _dimension
        End Get
        Set(value As String)
            _dimension = value
            ChangedPropertyValue("Dimensions")
        End Set
    End Property

    Public Sub New(filename As String)

        Dim imgInfo As New IO.FileInfo(filename)

        Name = imgInfo.Name
        Size = Convert(imgInfo.Length).ToString
        Path = imgInfo.DirectoryName
        FullName = filename

        'Dim timg As BitmapImage = New BitmapImage(New Uri(filename))

        'Dimensions = timg.PixelWidth & "×" & timg.PixelHeight
        'Width = timg.PixelWidth
        'Height = timg.PixelHeight
        'timg.Freeze()

    End Sub

    Private Sub ChangedPropertyValue(propName As String)
        Dim arg As New PropertyChangedEventArgs(propName)
        RaiseEvent PropertyChanged(Me, arg)
    End Sub

    Public Function Convert(value As String) As String
        Dim units As String() = {"B", "KB", "MB", "GB", "TB", "PB",
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