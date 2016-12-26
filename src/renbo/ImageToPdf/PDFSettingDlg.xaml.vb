Imports System.ComponentModel
Imports renbo

Public Class PDFSettingDlg

    Private OptionViewModel As PDFmetadataViewModel = New PDFmetadataViewModel()


    Private Sub PDFSettingDlg_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Me.DataContext = OptionViewModel
    End Sub

    Public Sub SetPDFOption(pdfoption As PDFmetadata)
        OptionViewModel.SetPDFOption(pdfoption.Clone)
    End Sub

    Public Function getPDFOption() As PDFmetadata
        Return OptionViewModel.GetPDFOption
    End Function

    Private Sub okButton_Click(sender As Object, e As RoutedEventArgs) Handles okButton.Click
        Me.DialogResult = True
    End Sub

    Private Sub cancelButton_Click(sender As Object, e As RoutedEventArgs) Handles cancelButton.Click
        Me.DialogResult = False
    End Sub
End Class


Public Class pdfOptionVM
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private _PdfOption As pdfoption

    Private _isCustom As Boolean = False

    Public Sub New()
        _PdfOption = New pdfoption
        InitializePageTypes()
        Me.PDFPageType = PageTypes.Item(3)
    End Sub

    'Public Sub New(pdfoption As pdfoption)
    '    _PdfOption = pdfoption
    'End Sub

    Public Sub SetPDFOption(pdfoption As pdfoption)
        _PdfOption = pdfoption
        If _PdfOption.pagetype Is Nothing Then
            Me.PDFPageType = PageTypes.Item(3)
        End If
        CheckIfCustom()
    End Sub
    Private Sub InitializePageTypes()
        If PageTypes Is Nothing Then
            PageTypes = PDFPageType.GetPageTypes()
        End If
    End Sub

    Private Sub ChangedPropertyValue(propName As String)
        Dim arg As New PropertyChangedEventArgs(propName)
        RaiseEvent PropertyChanged(Me, arg)
    End Sub

    Public Property Title As String
        Get
            Return _PdfOption.Title
        End Get
        Set(value As String)
            _PdfOption.Title = value
            ChangedPropertyValue("Title")
        End Set
    End Property

    Public Property Auther As String
        Get
            Return _PdfOption.Auther
        End Get
        Set(value As String)
            _PdfOption.Auther = value
            ChangedPropertyValue("Auther")
        End Set
    End Property

    Public Property Subject As String
        Get
            Return _PdfOption.Subject
        End Get
        Set(value As String)
            _PdfOption.Subject = value
            ChangedPropertyValue("Subject")
        End Set
    End Property

    Public Property isImagesizeAuto As Boolean
        Get
            Return _PdfOption.imagesizeAuto
        End Get
        Set(value As Boolean)
            _PdfOption.imagesizeAuto = value
            ChangedPropertyValue("isImagesizeAuto")
            CheckIfCustom()
        End Set
    End Property

    Public Property isImagesizeStrech As Boolean
        Get
            Return Not _PdfOption.imagesizeAuto
        End Get
        Set(value As Boolean)
            _PdfOption.imagesizeAuto = Not value
            ChangedPropertyValue("isImagesizeStrech")
            CheckIfCustom()
        End Set
    End Property

    Public Shared Property PageTypes As IList


    Public Property PDFPageType As PDFPageType
        Get
            Return _PdfOption.pagetype
        End Get
        Set(value As PDFPageType)
            _PdfOption.pagetype = value
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
            Return _PdfOption.width
        End Get
        Set(value As Single)
            _PdfOption.width = value
            ChangedPropertyValue("Width")
        End Set
    End Property

    Public Property Height As Single
        Get
            Return _PdfOption.height
        End Get
        Set(value As Single)
            _PdfOption.height = value
            ChangedPropertyValue("Height")
        End Set
    End Property

    Public Property OwnerPassword As String
        Get
            Return _PdfOption.ownerPassword
        End Get
        Set(value As String)
            _PdfOption.ownerPassword = value
            ChangedPropertyValue("OwnerPassword")
        End Set
    End Property

    Public Property UserPassword As String
        Get
            Return _PdfOption.userPassword
        End Get
        Set(value As String)
            _PdfOption.userPassword = value
            ChangedPropertyValue("UserPassword")
        End Set
    End Property

    Public Property IsUserPassword As Boolean
        Get
            Return _PdfOption.IsUserPassword
        End Get
        Set(value As Boolean)
            _PdfOption.IsUserPassword = value
            ChangedPropertyValue("IsUserPassword")
        End Set
    End Property
    Public Property IsOwnerPassword As Boolean
        Get
            Return _PdfOption.IsOwnerPassword
        End Get
        Set(value As Boolean)
            _PdfOption.IsOwnerPassword = value
            ChangedPropertyValue("IsOwnerPassword")
        End Set
    End Property
    Private Sub CheckIfCustom()

        If Not isImagesizeAuto AndAlso PDFPageType.Name = "Custom" Then
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

    Friend Function GetPDFOption() As pdfoption
        Return _PdfOption
    End Function

End Class
