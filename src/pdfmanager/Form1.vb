Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim file As String = "C:\Users\Udayan\Desktop\exm.pdf"
        Dim strm As New FileStream(file, FileMode.OpenOrCreate)

        Dim docu As New Document(iTextSharp.text.PageSize.A4)
        Dim witer As PdfWriter = PdfWriter.GetInstance(docu, strm)

        docu.Open()
        Dim p As New Paragraph("hello i am udayan")
        ' p.
        docu.Add(p)

        Dim cb As PdfContentByte = witer.DirectContent
        cb.MoveTo(docu.PageSize.Width / 2, docu.PageSize.Height / 2)
        cb.LineTo(docu.PageSize.Width / 2, docu.PageSize.Height)
        cb.Stroke()
        Dim image = iTextSharp.text.Image.GetInstance(System.IO.File.Open("H:\udayan\My data\subject\Dharma\shibsamhita\00000066.tif", FileMode.Open))



        image.ScaleAbsolute(iTextSharp.text.PageSize.A4)
        'image.ScaleToFit(200, 200)
        'image.SetAbsolutePosition(1, 1)


        docu.Add(image)
        Dim image1 = iTextSharp.text.Image.GetInstance(System.IO.File.Open("H:\udayan\My data\subject\Dharma\shibsamhita\00000067.tif", FileMode.Open))



        image1.ScaleAbsolute(iTextSharp.text.PageSize.A4)
        'image.ScaleToFit(200, 200)
        'image.SetAbsolutePosition(1, 1)


        docu.Add(image1)
        docu.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using opndlg As New OpenFileDialog
            opndlg.Multiselect = True
            If opndlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then

                Dim imgpdf As New image2pdf

                imgpdf.Files.AddRange(opndlg.FileNames)


                imgpdf.createPdf("C:\Users\Udayan\Desktop\out.pdf")

            End If
        End Using
    End Sub
End Class
