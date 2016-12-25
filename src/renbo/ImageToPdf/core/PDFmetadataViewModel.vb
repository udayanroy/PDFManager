


Imports System.ComponentModel

Public Class PDFmetadataViewModel
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _data As PDFmetadata
    Private _isCustom As Boolean = False


    Public Sub New()
        _data = New PDFmetadata
        'InitializePageTypes()
        'Me.PDFPageType = PageTypes.Item(3)
    End Sub

    'Public Sub New(pdfoption As pdfoption)
    '    _PdfOption = pdfoption
    'End Sub
    Friend Function GetPDFOption() As PDFmetadata
        Return _data
    End Function

    Public Sub SetPDFOption(pdfoption As PDFmetadata)
        _data = pdfoption
        'If _data.pagetype Is Nothing Then
        '    Me.PDFPageType = PageTypes.Item(3)
        'End If
        CheckIfCustom()
    End Sub


    Private Sub ChangedPropertyValue(propName As String)
        Dim arg As New PropertyChangedEventArgs(propName)
        RaiseEvent PropertyChanged(Me, arg)
    End Sub

    Public Property Title As String
        Get
            Return _data.Title
        End Get
        Set(value As String)
            _data.Title = value
            ChangedPropertyValue("Title")
        End Set
    End Property

    Public Property Author As String
        Get
            Return _data.Author
        End Get
        Set(value As String)
            _data.Author = value
            ChangedPropertyValue("Author")
        End Set
    End Property

    Public Property Subject As String
        Get
            Return _data.Subject
        End Get
        Set(value As String)
            _data.Subject = value
            ChangedPropertyValue("Subject")
        End Set
    End Property

    Public Property Creator As String
        Get
            Return _data.Creator
        End Get
        Set(value As String)
            _data.Creator = value
            ChangedPropertyValue("Creator")
        End Set
    End Property

    Public Property Producer As String
        Get
            Return _data.Producer
        End Get
        Set(value As String)
            _data.Producer = value
            ChangedPropertyValue("Producer")
        End Set
    End Property

    Public Property KeyWords As String
        Get
            Return _data.KeyWords
        End Get
        Set(value As String)
            _data.KeyWords = value
            ChangedPropertyValue("KeyWords")
        End Set
    End Property
    'Public Property isImagesizeAuto As Boolean
    '    Get
    '        Return _PdfOption.imagesizeAuto
    '    End Get
    '    Set(value As Boolean)
    '        _PdfOption.imagesizeAuto = value
    '        ChangedPropertyValue("isImagesizeAuto")
    '        CheckIfCustom()
    '    End Set
    'End Property

    'Public Property isImagesizeStrech As Boolean
    '    Get
    '        Return Not _PdfOption.imagesizeAuto
    '    End Get
    '    Set(value As Boolean)
    '        _PdfOption.imagesizeAuto = Not value
    '        ChangedPropertyValue("isImagesizeStrech")
    '        CheckIfCustom()
    '    End Set
    'End Property

    Public ReadOnly Property PageTypes As IList
        Get
            Return _data.PageTypeList
        End Get
    End Property

    Public Property PDFPageType As PDFPageType
        Get
            Return _data.pagetype
        End Get
        Set(value As PDFPageType)
            _data.pagetype = value
            ChangedPropertyValue("PDFPageType")
            SetPagesize(value)
        End Set
    End Property

    Public Property IsCustom As Boolean
        Get
            Return _isCustom
        End Get
        Set(value As Boolean)
            _isCustom = value
            ChangedPropertyValue("IsCustom")
        End Set
    End Property

    Public Property Width As Single
        Get
            Return _data.width
        End Get
        Set(value As Single)
            _data.width = value
            ChangedPropertyValue("Width")
        End Set
    End Property

    Public Property Height As Single
        Get
            Return _data.height
        End Get
        Set(value As Single)
            _data.height = value
            ChangedPropertyValue("Height")
        End Set
    End Property

    Public ReadOnly Property OrientasionTypeList As IList
        Get
            Return _data.OrientasionTypeList
        End Get
    End Property

    Public Property Orientasion As String
        Get
            Return _data.Orientasion
        End Get
        Set(value As String)
            _data.Orientasion = value
            ChangedPropertyValue("Orientasion")
        End Set
    End Property

    Public ReadOnly Property ImageFillTypeList As IList
        Get
            Return _data.ImageFillTypeList
        End Get
    End Property

    Public Property ImageFill As String
        Get
            Return _data.ImageFill
        End Get
        Set(value As String)
            _data.ImageFill = value
            ChangedPropertyValue("ImageFill")
        End Set
    End Property

    Public Property BackColor As Color
        Get
            Return _data.BackColor
        End Get
        Set(value As Color)
            _data.BackColor = value
            ChangedPropertyValue("BackColor")
        End Set
    End Property

    Public Property isImageFlipped As Boolean
        Get
            Return _data.isImageFlipped
        End Get
        Set(value As Boolean)
            _data.isImageFlipped = value
            ChangedPropertyValue("isImageFlipped")
        End Set
    End Property

    Public Property OwnerPassword As String
        Get
            Return _data.OwnerPassword
        End Get
        Set(value As String)
            _data.OwnerPassword = value
            ChangedPropertyValue("OwnerPassword")
        End Set
    End Property

    Public Property UserPassword As String
        Get
            Return _data.UserPassword
        End Get
        Set(value As String)
            _data.UserPassword = value
            ChangedPropertyValue("UserPassword")
        End Set
    End Property

    Public Property IsUserPassword As Boolean
        Get
            Return _data.isUserpasswordEnable
        End Get
        Set(value As Boolean)
            _data.isUserpasswordEnable = value
            ChangedPropertyValue("IsUserPassword")
        End Set
    End Property
    Public Property IsOwnerPassword As Boolean
        Get
            Return _data.isOwnerpasswordEnable
        End Get
        Set(value As Boolean)
            _data.isOwnerpasswordEnable = value
            ChangedPropertyValue("IsOwnerPassword")
        End Set
    End Property
    Private Sub CheckIfCustom()

        If PDFPageType.Name = "Custom" Then
            IsCustom = True
        Else
            IsCustom = False
        End If

    End Sub

    Private Sub SetPagesize(pdfpagetype As PDFPageType)
        If Not pdfpagetype.Name = "Custom" Then

            Width = pdfpagetype.Width
            Height = pdfpagetype.Height
        End If
        CheckIfCustom()
    End Sub


End Class

