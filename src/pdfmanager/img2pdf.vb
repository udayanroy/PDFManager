Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class img2pdf

    Dim outputstream As Stream
    Dim document As Document
    Dim pdfwriter As PdfWriter

    Public Sub New(outputFile As String)
        outputstream = New FileStream(outputFile, FileMode.OpenOrCreate)

    End Sub

    Public Sub Initialize()

        document = New Document(iTextSharp.text.PageSize.A4)
        pdfwriter = pdfwriter.GetInstance(document, outputstream)

        document.Open()

    End Sub
    Public Sub Initialize(_option As pdfOption)

        document = New Document(iTextSharp.text.PageSize.A4)
        pdfwriter = pdfwriter.GetInstance(document, outputstream)

        pdfwriter.PageEvent = New PDFFooter(_option.Header, _option.footer)

        If _option.ownerPassword IsNot Nothing Or _option.userPassword IsNot Nothing Then
            pdfwriter.SetEncryption(pdfwriter.STRENGTH128BITS, _option.userPassword, _option.ownerPassword, pdfwriter.ALLOW_SCREENREADERS And pdfwriter.AllowPrinting)

        End If

        document.Open()

    End Sub
    Public Sub AddImage(file As String)
        Using imgstrm = System.IO.File.Open(file, FileMode.Open)
            Dim image = iTextSharp.text.Image.GetInstance(imgstrm)
            image.ScaleAbsolute(iTextSharp.text.PageSize.A4)
            image.SetAbsolutePosition(0, 0)
            document.Add(image)
        End Using

    End Sub

    Public Sub close()
        document.Close()
        pdfwriter.Dispose()
        outputstream.Dispose()
    End Sub
End Class

Public Class pdfOption
    Public Property Header As String
    Public Property footer As String
    Public Property userPassword As String
    Public Property ownerPassword As String
End Class