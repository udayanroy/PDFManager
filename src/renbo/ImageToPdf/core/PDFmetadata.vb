




Public Class PDFmetadata

    Public Property OutputFile As String

    'Pdf Info
    Public Property Title As String
    Public Property Author As String
    Public Property Subject As String
    Public Property Creator As String
    Public Property Producer As String
    Public Property KeyWords As String

    'PDF Security
    Public Property isUserpasswordEnable As Boolean
    Public Property isOwnerpasswordEnable As Boolean
    Public Property UserPassword As String
    Public Property OwnerPassword As String

    'PDF Page Setting
    Public Property pagetype As PDFPageType
    Public Property PageTypeList As IList(Of PDFPageType)
    Public Property width As Single
    Public Property height As Single
    Public Property ImageFill As String
    Public Property ImageFillTypeList As IList(Of String)
    Public Property Orientasion As String
    Public Property OrientasionTypeList As IList(Of String)
    Public Property BackColor As Color
    Public Property isImageFlipped As Boolean = False
    'Public Property size As Size
    'Public Property sizetype As sizingType

    Private Sub New()

    End Sub

    Public Shared Function CreateNew() As PDFmetadata
        Dim data As New PDFmetadata
        data.Init()
        Return data
    End Function
    Public Function Clone() As PDFmetadata
        Dim data As New PDFmetadata
        With data
            .Title = Me.Title
            .Author = Me.Author
            .Subject = Me.Subject
            .Creator = Me.Creator
            .Producer = Me.Producer
            .KeyWords = Me.KeyWords

            .isUserpasswordEnable = Me.isUserpasswordEnable
            .isOwnerpasswordEnable = Me.isOwnerpasswordEnable
            .UserPassword = UserPassword
            .OwnerPassword = OwnerPassword

            .pagetype = Me.pagetype
            .PageTypeList = Me.PageTypeList
            .width = Me.width
            .height = Me.height
            .ImageFill = Me.ImageFill
            .ImageFillTypeList = Me.ImageFillTypeList
            .Orientasion = Me.Orientasion
            .OrientasionTypeList = Me.OrientasionTypeList
            .BackColor = Me.BackColor
            .isImageFlipped = Me.isImageFlipped
        End With

        Return data
    End Function
    Private Sub Init()

        isUserpasswordEnable = False
        isOwnerpasswordEnable = False
        'sizetype = sizingType.Auto
        ' size = PDFSize.GetPageSize(PageType.A4)
        'isStrech = False

        PageTypeList = PDFPageType.GetPageTypes()

        CreateOriantaionList()
        CreateImageFillList()


        Me.pagetype = PageTypeList(3)
        Me.ImageFill = ImageFillTypeList(0)
        Me.Orientasion = OrientasionTypeList(0)
        Me.BackColor = Colors.White
        Me.width = pagetype.Width
        Me.height = pagetype.Height
    End Sub
    '//  Deprecated Function  //
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

            '.sizetype = If(pdfoption.imagesizeAuto, sizingType.Auto, sizingType.Fixed)
            '.size = New Size(pdfoption.width, pdfoption.height)
            '.isStrech = If(pdfoption.imagesizeAuto, False, True)
        End With

        Return metadata
    End Function

    Public Sub CreateOriantaionList()
        Dim lst As New List(Of String)
        lst.Add("Potrait")
        lst.Add("Landscape")
        Me.OrientasionTypeList = lst
    End Sub

    Public Sub CreateImageFillList()
        Dim lst As New List(Of String)
        lst.Add("Fill")
        lst.Add("Fit")
        lst.Add("Strech")
        lst.Add("Tile")
        lst.Add("Center")
        lst.Add("Span")
        Me.ImageFillTypeList = lst
    End Sub
End Class

