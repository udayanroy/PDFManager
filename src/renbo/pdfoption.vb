﻿Public Class pdfoption
    Public Property Title As String
    Public Property Auther As String
    Public Property Subject As String
    Public Property Header As String
    Public Property footer As String
    Public Property userPassword As String
    Public Property ownerPassword As String
    Public Property imagesizeAuto As Boolean
    Public Property width As Single
    Public Property height As Single
    Public Property pagetype As String

    Public Sub New()
        imagesizeAuto = False
        pagetype = "A4"
        width = 595
        height = 842
    End Sub
End Class
