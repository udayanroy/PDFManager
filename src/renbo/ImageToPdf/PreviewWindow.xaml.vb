Public Class PreviewWindow

    Private Property Imagelist As List(Of imageItem)
    Private Property Setting As PDFmetadata


    Public Sub SetData(imagelist As List(Of imageItem), setting As PDFmetadata)

        Me.Imagelist = imagelist
        Me.Setting = setting

        Dim vm As New prvVM
        vm.Imagelist = imagelist
        vm.Setting = setting

        Me.DataContext = vm

    End Sub

    Private Class prvVM
        Public Property Imagelist As List(Of imageItem)
        Public Property Setting As PDFmetadata
    End Class
End Class


Public Class Previewpage

    Public Sub New(imageData As imageItem, setting As PDFmetadata)

    End Sub


End Class
