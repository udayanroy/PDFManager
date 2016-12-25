


Public Class PDFPageType

    Public Property Name As String
    Public Property Width As Single
    Public Property Height As Single

    Public Sub New(name As String, width As Single, height As Single)
        Me.Name = name
        Me.Width = width
        Me.Height = height
    End Sub

    Public Shared Function GetPageTypes() As List(Of PDFPageType)
        Dim list As New List(Of PDFPageType)
        Dim n As String, w As Single, h As Single

        n = "A0"
        w = iTextSharp.text.PageSize.A0.Width
        h = iTextSharp.text.PageSize.A0.Height
        list.Add(New PDFPageType(n, w, h))

        n = "A1"
        w = iTextSharp.text.PageSize.A1.Width
        h = iTextSharp.text.PageSize.A1.Height
        list.Add(New PDFPageType(n, w, h))
        'list.Add(New PDFPageType("", w, h))
        w = iTextSharp.text.PageSize.A2.Width
        h = iTextSharp.text.PageSize.A2.Height
        list.Add(New PDFPageType("A2", w, h))

        w = iTextSharp.text.PageSize.A4.Width
        h = iTextSharp.text.PageSize.A4.Height
        list.Add(New PDFPageType("A4", w, h))

        w = iTextSharp.text.PageSize.A5.Width
        h = iTextSharp.text.PageSize.A5.Height
        list.Add(New PDFPageType("A5", w, h))

        w = iTextSharp.text.PageSize.A6.Width
        h = iTextSharp.text.PageSize.A6.Height
        list.Add(New PDFPageType("A6", w, h))

        w = iTextSharp.text.PageSize.A7.Width
        h = iTextSharp.text.PageSize.A7.Height
        list.Add(New PDFPageType("A7", w, h))

        w = iTextSharp.text.PageSize.A8.Width
        h = iTextSharp.text.PageSize.A8.Height
        list.Add(New PDFPageType("A8", w, h))

        w = iTextSharp.text.PageSize.A9.Width
        h = iTextSharp.text.PageSize.A9.Height
        list.Add(New PDFPageType("A9", w, h))

        w = iTextSharp.text.PageSize.A10.Width
        h = iTextSharp.text.PageSize.A10.Height
        list.Add(New PDFPageType("A10", w, h))

        w = iTextSharp.text.PageSize.LETTER.Width
        h = iTextSharp.text.PageSize.LETTER.Height
        list.Add(New PDFPageType("Letter", w, h))

        w = iTextSharp.text.PageSize.B0.Width
        h = iTextSharp.text.PageSize.B0.Height
        list.Add(New PDFPageType("B0", w, h))

        w = iTextSharp.text.PageSize.B1.Width
        h = iTextSharp.text.PageSize.B1.Height
        list.Add(New PDFPageType("B1", w, h))

        w = iTextSharp.text.PageSize.B2.Width
        h = iTextSharp.text.PageSize.B2.Height
        list.Add(New PDFPageType("B2", w, h))

        w = iTextSharp.text.PageSize.B3.Width
        h = iTextSharp.text.PageSize.B3.Height
        list.Add(New PDFPageType("B3", w, h))

        w = iTextSharp.text.PageSize.B4.Width
        h = iTextSharp.text.PageSize.B4.Height
        list.Add(New PDFPageType("B4", w, h))

        w = iTextSharp.text.PageSize.B5.Width
        h = iTextSharp.text.PageSize.B5.Height
        list.Add(New PDFPageType("B5", w, h))

        w = iTextSharp.text.PageSize.B6.Width
        h = iTextSharp.text.PageSize.B6.Height
        list.Add(New PDFPageType("B6", w, h))

        w = iTextSharp.text.PageSize.B7.Width
        h = iTextSharp.text.PageSize.B7.Height
        list.Add(New PDFPageType("B7", w, h))

        w = iTextSharp.text.PageSize.B8.Width
        h = iTextSharp.text.PageSize.B8.Height
        list.Add(New PDFPageType("B8", w, h))

        w = iTextSharp.text.PageSize.B9.Width
        h = iTextSharp.text.PageSize.B9.Height
        list.Add(New PDFPageType("B9", w, h))

        w = iTextSharp.text.PageSize.B10.Width
        h = iTextSharp.text.PageSize.B10.Height
        list.Add(New PDFPageType("B10", w, h))

        list.Add(New PDFPageType("Custom", w, h))
        Return list
    End Function
End Class
