


Public Enum sizingType
    Auto
    Fixed
End Enum

Public Enum PageType
    A0
    A1
    A2
    A4
    A5
    A6
    A7
    A8
    A9
    A10
    Letter
    B0
    B1
    B2
    B3
    B4
    B5
    B6
    B7
    B8
    B9
    B10
    Custom
End Enum


Public Structure PDFSize

    Public Property type As sizingType
    Public Property isStrech As Boolean
    Public Property size As Size

    Public Shared Function GetPageSize(type As PageType) As Size
        Dim h, w As Single

        Select Case type

            Case PageType.A0
                w = iTextSharp.text.PageSize.A0.Width
                h = iTextSharp.text.PageSize.A0.Height
            Case PageType.A1
                w = iTextSharp.text.PageSize.A1.Width
                h = iTextSharp.text.PageSize.A1.Height
            Case PageType.A2
                w = iTextSharp.text.PageSize.A2.Width
                h = iTextSharp.text.PageSize.A2.Height
            Case PageType.A4
                w = iTextSharp.text.PageSize.A4.Width
                h = iTextSharp.text.PageSize.A4.Height
            Case PageType.A5
                w = iTextSharp.text.PageSize.A5.Width
                h = iTextSharp.text.PageSize.A5.Height
            Case PageType.A6
                w = iTextSharp.text.PageSize.A6.Width
                h = iTextSharp.text.PageSize.A6.Height
            Case PageType.A7
                w = iTextSharp.text.PageSize.A7.Width
                h = iTextSharp.text.PageSize.A7.Height
            Case PageType.A8
                w = iTextSharp.text.PageSize.A8.Width
                h = iTextSharp.text.PageSize.A8.Height
            Case PageType.A9
                w = iTextSharp.text.PageSize.A9.Width
                h = iTextSharp.text.PageSize.A9.Height
            Case PageType.A10
                w = iTextSharp.text.PageSize.A10.Width
                h = iTextSharp.text.PageSize.A10.Height
            Case PageType.Letter
                w = iTextSharp.text.PageSize.LETTER.Width
                h = iTextSharp.text.PageSize.LETTER.Height
            Case PageType.B0
                w = iTextSharp.text.PageSize.B0.Width
                h = iTextSharp.text.PageSize.B0.Height
            Case PageType.B1
                w = iTextSharp.text.PageSize.B1.Width
                h = iTextSharp.text.PageSize.B1.Height
            Case PageType.B2
                w = iTextSharp.text.PageSize.B2.Width
                h = iTextSharp.text.PageSize.B2.Height
            Case PageType.B3
                w = iTextSharp.text.PageSize.B3.Width
                h = iTextSharp.text.PageSize.B3.Height
            Case PageType.B4
                w = iTextSharp.text.PageSize.B4.Width
                h = iTextSharp.text.PageSize.B4.Height
            Case PageType.B5
                w = iTextSharp.text.PageSize.B5.Width
                h = iTextSharp.text.PageSize.B5.Height
            Case PageType.B6
                w = iTextSharp.text.PageSize.B6.Width
                h = iTextSharp.text.PageSize.B6.Height
            Case PageType.B7
                w = iTextSharp.text.PageSize.B7.Width
                h = iTextSharp.text.PageSize.B7.Height
            Case PageType.B8
                w = iTextSharp.text.PageSize.B8.Width
                h = iTextSharp.text.PageSize.B8.Height
            Case PageType.B9
                w = iTextSharp.text.PageSize.B9.Width
                h = iTextSharp.text.PageSize.B9.Height
            Case PageType.B10
                w = iTextSharp.text.PageSize.B10.Width
                h = iTextSharp.text.PageSize.B10.Height

            Case Else
                w = 0
                h = 0

        End Select
        Return New Size(w, h)
    End Function
End Structure


