Imports System.ComponentModel
Imports renbo

Public Class PDFSettingDlg

    Private OptionViewModel As pdfOptionVM = New pdfOptionVM()


    Private Sub PDFSettingDlg_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Me.DataContext = OptionViewModel
    End Sub

    Public Sub SetPDFOption(pdfoption As pdfoption)
        OptionViewModel.SetPDFOption(pdfoption)
    End Sub

    Public Function getPDFOption() As pdfoption
        Return OptionViewModel.GetPDFOption
    End Function

    Private Sub okButton_Click(sender As Object, e As RoutedEventArgs) Handles okButton.Click
        Me.DialogResult = True
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

Public Class PDFPageType

    Public Property Name As String
    Public Property Width As Single
    Public Property Height As Single

    Public Sub New(name As String, width As Single, height As Single)
        Me.Name = name
        Me.Width = width
        Me.Height = height
    End Sub

    Public Shared Function GetPageTypes() As List(Of PDFPageType)
        Dim list As New List(Of PDFPageType)
        Dim n As String, w As Single, h As Single

        n = "A0"
        w = iTextSharp.text.PageSize.A0.Width
        h = iTextSharp.text.PageSize.A0.Height
        list.Add(New PDFPageType(n, w, h))

        n = "A1"
        w = iTextSharp.text.PageSize.A1.Width
        h = iTextSharp.text.PageSize.A1.Height
        list.Add(New PDFPageType(n, w, h))
        'list.Add(New PDFPageType("", w, h))
        w = iTextSharp.text.PageSize.A2.Width
        h = iTextSharp.text.PageSize.A2.Height
        list.Add(New PDFPageType("A2", w, h))

        w = iTextSharp.text.PageSize.A4.Width
        h = iTextSharp.text.PageSize.A4.Height
        list.Add(New PDFPageType("A4", w, h))

        w = iTextSharp.text.PageSize.A5.Width
        h = iTextSharp.text.PageSize.A5.Height
        list.Add(New PDFPageType("A5", w, h))

        w = iTextSharp.text.PageSize.A6.Width
        h = iTextSharp.text.PageSize.A6.Height
        list.Add(New PDFPageType("A6", w, h))

        w = iTextSharp.text.PageSize.A7.Width
        h = iTextSharp.text.PageSize.A7.Height
        list.Add(New PDFPageType("A7", w, h))

        w = iTextSharp.text.PageSize.A8.Width
        h = iTextSharp.text.PageSize.A8.Height
        list.Add(New PDFPageType("A8", w, h))

        w = iTextSharp.text.PageSize.A9.Width
        h = iTextSharp.text.PageSize.A9.Height
        list.Add(New PDFPageType("A9", w, h))

        w = iTextSharp.text.PageSize.A10.Width
        h = iTextSharp.text.PageSize.A10.Height
        list.Add(New PDFPageType("A10", w, h))

        w = iTextSharp.text.PageSize.LETTER.Width
        h = iTextSharp.text.PageSize.LETTER.Height
        list.Add(New PDFPageType("Letter", w, h))

        w = iTextSharp.text.PageSize.B0.Width
        h = iTextSharp.text.PageSize.B0.Height
        list.Add(New PDFPageType("B0", w, h))

        w = iTextSharp.text.PageSize.B1.Width
        h = iTextSharp.text.PageSize.B1.Height
        list.Add(New PDFPageType("B1", w, h))

        w = iTextSharp.text.PageSize.B2.Width
        h = iTextSharp.text.PageSize.B2.Height
        list.Add(New PDFPageType("B2", w, h))

        w = iTextSharp.text.PageSize.B3.Width
        h = iTextSharp.text.PageSize.B3.Height
        list.Add(New PDFPageType("B3", w, h))

        w = iTextSharp.text.PageSize.B4.Width
        h = iTextSharp.text.PageSize.B4.Height
        list.Add(New PDFPageType("B4", w, h))

        w = iTextSharp.text.PageSize.B5.Width
        h = iTextSharp.text.PageSize.B5.Height
        list.Add(New PDFPageType("B5", w, h))

        w = iTextSharp.text.PageSize.B6.Width
        h = iTextSharp.text.PageSize.B6.Height
        list.Add(New PDFPageType("B6", w, h))

        w = iTextSharp.text.PageSize.B7.Width
        h = iTextSharp.text.PageSize.B7.Height
        list.Add(New PDFPageType("B7", w, h))

        w = iTextSharp.text.PageSize.B8.Width
        h = iTextSharp.text.PageSize.B8.Height
        list.Add(New PDFPageType("B8", w, h))

        w = iTextSharp.text.PageSize.B9.Width
        h = iTextSharp.text.PageSize.B9.Height
        list.Add(New PDFPageType("B9", w, h))

        w = iTextSharp.text.PageSize.B10.Width
        h = iTextSharp.text.PageSize.B10.Height
        list.Add(New PDFPageType("B10", w, h))

        list.Add(New PDFPageType("Custom", w, h))
        Return list
    End Function
End Class
