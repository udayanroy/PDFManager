Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class PdfMaker
    Dim outputstream As Stream
    Dim document As Document
    Dim pdfwriter As PdfWriter


    Dim _metadata As PDFmetadata

    Public Sub New(outputFile As String)
        If File.Exists(outputFile) Then
            File.Delete(outputFile)
        End If
        outputstream = New FileStream(outputFile, FileMode.Create)
    End Sub

    Public Sub Initialize()
        ' isSizeAuto = True

        document = New Document(iTextSharp.text.PageSize.A4)
        pdfwriter = pdfwriter.GetInstance(document, outputstream)
        document.Open()

    End Sub
    Public Sub Initialize(_option As PDFmetadata)
        _metadata = _option

        document = New Document(iTextSharp.text.PageSize.A4)
        pdfwriter = pdfwriter.GetInstance(document, outputstream)


        '//////////header///////////////
        document.AddTitle(_option.Title)
        document.AddAuthor(_option.Author)
        document.AddSubject(_option.Subject)
        '//////


        If _option.isOwnerpasswordEnable Then
            pdfwriter.SetEncryption(pdfwriter.STRENGTH128BITS, _option.UserPassword, _option.OwnerPassword, pdfwriter.ALLOW_SCREENREADERS And pdfwriter.AllowPrinting)

        Else
            If _option.isUserpasswordEnable Then
                pdfwriter.SetEncryption(pdfwriter.STRENGTH128BITS, _option.UserPassword, _option.UserPassword, pdfwriter.ALLOW_SCREENREADERS And pdfwriter.AllowPrinting)
            End If
        End If

        document.Open()

    End Sub
    Public Sub AddImage(file As String)
        Dim pagerect As Rectangle

        Using imgstrm = System.IO.File.Open(file, FileMode.Open)
            Dim image = iTextSharp.text.Image.GetInstance(imgstrm)

            If _metadata.sizetype = sizingType.Auto Then
                pagerect = New Rectangle(image.Width, image.Height)
            Else

                pagerect = New Rectangle(_metadata.size.Width, _metadata.size.Height)
                If _metadata.isStrech Then
                    image.ScaleAbsolute(pagerect)
                Else
                    image.ScaleAbsolute(getbestfitImgsize(pagerect, New Rectangle(image.Width, image.Height)))
                End If

            End If

            image.SetAbsolutePosition(0, 0)
            document.SetPageSize(pagerect)

            document.NewPage()
            document.Add(image)
        End Using

    End Sub

    Public Sub close()
        document.Close()
        pdfwriter.Dispose()
        outputstream.Dispose()
    End Sub


    Private Function getbestfitImgsize(pagesize As Rectangle, imagesize As Rectangle) As Rectangle
        Dim rect As Rectangle

        Dim pn = pagesize.Width / pagesize.Height
        Dim imn = imagesize.Width / imagesize.Height

        Dim ih = pagesize.Width / imn

        If ih <= pagesize.Height Then
            rect = New Rectangle(pagesize.Width, ih)
        Else
            rect = New Rectangle(imn * pagesize.Height, pagesize.Height)
        End If

        Return rect
    End Function
End Class
