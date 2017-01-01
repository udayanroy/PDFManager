Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class PdfMaker
    Dim outputstream As Stream
    Dim document As Document
    Dim pdfwriter As PdfWriter
    Dim pagerect As Rectangle

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
        document.AddCreationDate()
        'document.
        '//////
        document.SetMargins(10, 10, 10, 10)  ' its not working 

        'If _option.isOwnerpasswordEnable Then
        '    pdfwriter.SetEncryption(PdfWriter.STRENGTH128BITS, _option.UserPassword, _option.OwnerPassword, PdfWriter.ALLOW_SCREENREADERS And PdfWriter.AllowPrinting)

        'Else
        '    If _option.isUserpasswordEnable Then
        '        pdfwriter.SetEncryption(pdfwriter.STRENGTH128BITS, _option.UserPassword, _option.UserPassword, pdfwriter.ALLOW_SCREENREADERS And pdfwriter.AllowPrinting)
        '    End If
        'End If
        If _option.isOwnerpasswordEnable Or _option.isUserpasswordEnable Then

            Dim userpass = If(_option.isUserpasswordEnable, _option.UserPassword, "")
            Dim ownerpass = If(_option.isOwnerpasswordEnable, _option.OwnerPassword, "")



            pdfwriter.SetEncryption(PdfWriter.STRENGTH128BITS, userpass, ownerpass, PdfWriter.ALLOW_SCREENREADERS And PdfWriter.AllowPrinting)

        End If

        document.Open()

    End Sub
    Public Sub AddImage(file As String)


        Using imgstrm = System.IO.File.Open(file, FileMode.Open)
            Dim image = iTextSharp.text.Image.GetInstance(imgstrm)

            'If _metadata.sizetype = sizingType.Auto Then
            '    pagerect = New Rectangle(image.Width, image.Height)
            'Else

            '# create pdf page...

            Dim pageWidth = _metadata.width
            Dim pageHeight = _metadata.height

            If _metadata.Orientasion = "Landscape" Then
                pagerect = New Rectangle(pageHeight, pageWidth)
            Else
                pagerect = New Rectangle(pageWidth, pageHeight)
            End If
            document.SetPageSize(pagerect)
            document.NewPage()

            '# set color of the page...
            Dim Background As New Rectangle(pagerect)
            Dim color = _metadata.BackColor
            Background.BackgroundColor = New BaseColor(color.R, color.G, color.B)
            document.Add(Background)

            '# place the Image within the page...
            If _metadata.ImageFill = "Stretch" Then
                AddStretchedImage(image)
            ElseIf _metadata.ImageFill = "Fit" Then
                AddFitImage(image)

            ElseIf _metadata.ImageFill = "Fill" Then
                AddFillImage(image)

                'ElseIf _metadata.ImageFill = "Tile" Then


            ElseIf _metadata.ImageFill = "Center" Then
                AddCenterImage(image)

                'ElseIf _metadata.ImageFill = "Span" Then


            End If

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

    Private Function getFillSize(pagesize As Rectangle, imagesize As Rectangle) As Rectangle
        Dim rect As Rectangle

        Dim pn = pagesize.Width / pagesize.Height
        Dim imn = imagesize.Width / imagesize.Height

        Dim ih = pagesize.Width / imn

        If ih <= pagesize.Height Then
            rect = New Rectangle(-(imn * pagesize.Height), pagesize.Height)
        Else
            rect = New Rectangle(-pagesize.Width, ih)

        End If

        Return rect
    End Function

    Private Sub AddImage(Image As Image, rect As Rect, Optional isFlip As Boolean = False)
        If isFlip Then
            Image.ScaleAbsolute(-rect.Width, rect.Height)
            Image.SetAbsolutePosition(rect.X + rect.Width, rect.Y)
        Else
            Image.ScaleAbsolute(rect.Width, rect.Height)
            Image.SetAbsolutePosition(rect.X, rect.Y)
        End If

        document.Add(Image)
    End Sub

    Private Sub AddStretchedImage(Image As Image)
        Dim rect As New Rect(0, 0, pagerect.Width, pagerect.Height)
        Me.AddImage(Image, rect, _metadata.isImageFlipped)
    End Sub

    Private Sub AddFitImage(Image As Image)
        Dim scaleRct = getbestfitImgsize(pagerect, New Rectangle(Image.Width, Image.Height))
        Dim x = (pagerect.Width - scaleRct.Width) / 2
        Dim y = (pagerect.Height - scaleRct.Height) / 2
        Dim rect As New Rect(x, y, scaleRct.Width, scaleRct.Height)

        Me.AddImage(Image, rect, _metadata.isImageFlipped)
    End Sub

    Private Sub AddFillImage(Image As Image)
        Dim rect As Rect

        Dim pn = pagerect.Width / pagerect.Height
        Dim imn = Image.Width / Image.Height

        Dim ih = pagerect.Width / imn

        If ih <= pagerect.Height Then
            Dim width = (imn * pagerect.Height)
            Dim x = (pagerect.Width - width) / 2
            rect = New Rect(x, 0, width, pagerect.Height)
        Else
            Dim y = (pagerect.Height - ih) / 2
            rect = New Rect(0, y, pagerect.Width, ih)
        End If


        Me.AddImage(Image, rect, _metadata.isImageFlipped)
    End Sub
    Private Sub AddCenterImage(Image As Image)
        Dim rect As Rect

        Dim centerX = pagerect.Width / 2
        Dim centerY = pagerect.Height / 2

        Dim x = (pagerect.Width - Image.Width) / 2
        Dim y = (pagerect.Height - Image.Height) / 2

        rect = New Rect(x, y, Image.Width, Image.Height)

        Me.AddImage(Image, rect, _metadata.isImageFlipped)
    End Sub

End Class
