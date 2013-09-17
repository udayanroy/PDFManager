Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class pdfmerger

    Dim outputstream As Stream
    Dim document As Document
    Dim pdfwriter As PdfCopy


    Public Sub New(outputFile As String)
         outputstream = New FileStream(outputFile, FileMode.OpenOrCreate)
    End Sub

    Public Sub Initialize()
        document = New Document(iTextSharp.text.PageSize.A4)
        pdfwriter = New PdfCopy(document, outputstream)

        document.Open()


    End Sub
    Public Sub Initialize(_option As pdfOption)
        document = New Document(iTextSharp.text.PageSize.A4)
        pdfwriter = New PdfCopy(document, outputstream)

        pdfwriter.PageEvent = New PDFFooter(_option.Header, _option.footer)
        If _option.ownerPassword IsNot Nothing Or _option.userPassword IsNot Nothing Then
            pdfwriter.SetEncryption(pdf.PdfWriter.STRENGTH128BITS, _option.userPassword, _option.ownerPassword, pdf.PdfWriter.ALLOW_SCREENREADERS And pdf.PdfWriter.AllowPrinting)

        End If

        document.Open()
    End Sub
    Public Sub AddPDF(file As String)
        Using reader = New PdfReader(file)
            Dim n = reader.NumberOfPages

            For i As Integer = 0 To n - 1
                document.SetPageSize(reader.GetPageSizeWithRotation(i + 1))
                Dim importedPage = pdfwriter.GetImportedPage(reader, i)
                pdfwriter.AddPage(importedPage)
            Next

        End Using
    End Sub
    Public Sub AddPage(page As PdfImportedPage, pazesize As Rectangle)

        document.SetPageSize(pazesize)
        pdfwriter.AddPage(page)

    End Sub

    Public Sub close()
        document.Close()
        pdfwriter.Dispose()
        outputstream.Dispose()
    End Sub
End Class
