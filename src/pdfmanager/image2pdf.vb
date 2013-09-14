Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class image2pdf
    Dim _file As New List(Of String)


    Public Sub New()

    End Sub

    Public ReadOnly Property Files As List(Of String)
        Get
            Return _file
        End Get
    End Property

    Public Sub createPdf(pdfile As String)
        Using strm As New FileStream(pdfile, FileMode.OpenOrCreate)
            Using docu As New Document(iTextSharp.text.PageSize.A4), witer As PdfWriter = PdfWriter.GetInstance(docu, strm)

                docu.Open()

                For i = 0 To Files.Count - 1

                    Using imgstrm = System.IO.File.Open(Files(i), FileMode.Open)
                        Dim image = iTextSharp.text.Image.GetInstance(imgstrm)
                        image.ScaleAbsolute(iTextSharp.text.PageSize.A4)
                        docu.Add(image)
                    End Using

                Next

                docu.Close()
            End Using

        End Using


    End Sub

End Class
