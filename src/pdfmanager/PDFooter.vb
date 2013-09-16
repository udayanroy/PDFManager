Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class PDFFooter
    Inherits PdfPageEventHelper




    ' write on top of document
    Public Overrides Sub OnOpenDocument(writer As PdfWriter, document As Document)
        MyBase.OnOpenDocument(writer, document)

        If _header IsNot Nothing Then
            Dim footer As Paragraph = New Paragraph(_header, FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL))
            footer.Alignment = Element.ALIGN_LEFT
            Dim footerTbl = New PdfPTable(1)
            footerTbl.TotalWidth = 300
            footerTbl.HorizontalAlignment = Element.ALIGN_LEFT
            Dim cell = New PdfPCell(footer)
            cell.Border = 0
            cell.PaddingLeft = 10
            footerTbl.AddCell(cell)
            footerTbl.WriteSelectedRows(0, -1, 415, 30, writer.DirectContent)
        End If

    End Sub

    '// write on start of each page
    Public Overrides Sub OnStartPage(writer As PdfWriter, document As Document)
        MyBase.OnStartPage(writer, document)
    End Sub


    ' write on end of each page
    Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
        MyBase.OnEndPage(writer, document)

        If _footer IsNot Nothing Then
            Dim footer As Paragraph = New Paragraph(_footer, FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL))
            footer.Alignment = Element.ALIGN_LEFT
            Dim footerTbl = New PdfPTable(1)
            footerTbl.TotalWidth = 300
            footerTbl.HorizontalAlignment = Element.ALIGN_LEFT
            Dim cell = New PdfPCell(footer)
            cell.Border = 0
            cell.PaddingLeft = 10
            footerTbl.AddCell(cell)
            footerTbl.WriteSelectedRows(0, -1, 415, 30, writer.DirectContent)
        End If


    End Sub


    '//write on close of document
    Public Overrides Sub OnCloseDocument(writer As PdfWriter, document As Document)
        MyBase.OnCloseDocument(writer, document)
    End Sub


    Dim _header, _footer As String

    Public Sub New(Optional header As String = Nothing, Optional footer As String = Nothing)
        _header = header
        _footer = footer
    End Sub
End Class