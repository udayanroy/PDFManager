




Public Class PDFmetadata

    Public Property Title As String
    Public Property Author As String
    Public Property Subject As String
    Public Property OutputFile As String

    Public Property isUserpasswordEnable As Boolean
    Public Property isOwnerpasswordEnable As Boolean
    Public Property UserPassword As String
    Public Property OwnerPassword As String

    Public Property isStrech As Boolean
    Public Property size As Size
    Public Property sizetype As sizingType


    Public Sub New()
        Title = ""
        Author = ""
        Subject = ""
        isUserpasswordEnable = False
        isOwnerpasswordEnable = False
        sizetype = sizingType.Auto
        size = PDFSize.GetPageSize(PageType.A4)
        isStrech = False
    End Sub

    Public Shared Function FromPDFOption(pdfoption As pdfoption)
        Dim metadata As New PDFmetadata()

        With metadata
            .Title = pdfoption.Title
            .Author = pdfoption.Auther
            .Subject = pdfoption.Subject

            .isUserpasswordEnable = pdfoption.IsUserPassword
            .isOwnerpasswordEnable = pdfoption.IsOwnerPassword
            .UserPassword = pdfoption.userPassword
            .OwnerPassword = pdfoption.ownerPassword

            .sizetype = If(pdfoption.imagesizeAuto, sizingType.Auto, sizingType.Fixed)
            .size = New Size(pdfoption.width, pdfoption.height)
            .isStrech = If(pdfoption.imagesizeAuto, False, True)
        End With

        Return metadata
    End Function
End Class
