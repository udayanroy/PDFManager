Class optionpage

    Dim psett As New PDFmetadata

    Dim outputpage As pdfoutput

    Public Sub New(lst As ItemCollection)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' combo setting 
        pagetypecombo.ItemsSource = [Enum].GetValues(GetType(PageType))
        ' pagetypecombo.Items.Add("Custom")
        pagetypecombo.SelectedIndex = 3
        '/////////////////


        outputpage = New pdfoutput(lst, psett)

    End Sub

    Private Sub ownpassCheck_Checked(sender As Object, e As RoutedEventArgs) Handles ownpassCheck.Checked
        ownpass.IsEnabled = True
        psett.OwnerPassword = True
    End Sub

    Private Sub ownpassCheck_Unchecked(sender As Object, e As RoutedEventArgs) Handles ownpassCheck.Unchecked
        ownpass.IsEnabled = False
        psett.OwnerPassword = False
    End Sub

    Private Sub usepassCheck_Checked(sender As Object, e As RoutedEventArgs) Handles usepassCheck.Checked
        usepass.IsEnabled = True
        psett.isUserpasswordEnable = True
    End Sub


    Private Sub usepassCheck_Unchecked(sender As Object, e As RoutedEventArgs) Handles usepassCheck.Unchecked
        usepass.IsEnabled = False
        psett.isUserpasswordEnable = False
    End Sub

    Private Sub pagetypecombo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles pagetypecombo.SelectionChanged

        If pagetypecombo.SelectedIndex <> 22 Then
            Dim sz = PDFSize.GetPageSize(pagetypecombo.SelectedIndex)

            pwidth.Text = sz.Width
            pheight.Text = sz.Height

            pwidth.IsEnabled = False
            pheight.IsEnabled = False

            psett.size = sz
        Else
            pwidth.IsEnabled = True
            pheight.IsEnabled = True

            psett.size = New Size(pwidth.Text, pheight.Text)
        End If


    End Sub

    Private Sub strechCheck_Checked(sender As Object, e As RoutedEventArgs) Handles strechCheck.Checked
        psett.isStrech = True
    End Sub

    Private Sub strechCheck_Unchecked(sender As Object, e As RoutedEventArgs) Handles strechCheck.Unchecked
        psett.isStrech = False
    End Sub


    Private Sub Titlebox_KeyUp(sender As Object, e As KeyEventArgs) Handles Titlebox.KeyUp
        psett.Title = Titlebox.Text
    End Sub

    Private Sub subjectbox_KeyUp(sender As Object, e As KeyEventArgs) Handles subjectbox.KeyUp
        psett.Subject = subjectbox.Text
    End Sub

    Private Sub Authorebox_KeyUp(sender As Object, e As KeyEventArgs) Handles Authorebox.KeyUp
        psett.Author = Authorebox.Text
    End Sub

    Private Sub ownpass_KeyUp(sender As Object, e As KeyEventArgs) Handles ownpass.KeyUp

        psett.OwnerPassword = ownpass.Password
    End Sub

    Private Sub usepass_KeyUp(sender As Object, e As KeyEventArgs) Handles usepass.KeyUp
        psett.UserPassword = usepass.Password
    End Sub

    Private Sub cfixed_Checked(sender As Object, e As RoutedEventArgs) Handles cfixed.Checked
        pagetypecombo.IsEnabled = True
        strechCheck.IsEnabled = True

        If pagetypecombo.SelectedIndex = 22 Then
           
            pwidth.IsEnabled = True
            pheight.IsEnabled = True

        End If

        psett.sizetype = sizingType.Fixed
    End Sub

    Private Sub cauto_Checked(sender As Object, e As RoutedEventArgs) Handles cfixed.Unchecked
        pagetypecombo.IsEnabled = False
        strechCheck.IsEnabled = False
        pwidth.IsEnabled = False
        pheight.IsEnabled = False

        psett.sizetype = sizingType.Auto
    End Sub


    Private Sub pwidth_KeyUp(sender As Object, e As KeyEventArgs) Handles pwidth.KeyUp
        psett.size = New Size(pwidth.Text, pheight.Text)
    End Sub

    Private Sub pheight_KeyUp(sender As Object, e As KeyEventArgs) Handles pheight.KeyUp
        psett.size = New Size(pwidth.Text, pheight.Text)
    End Sub

    Private Sub optionpage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded


    End Sub

    Private Sub Next_Click(sender As Object, e As RoutedEventArgs) Handles [Next].Click
        Me.NavigationService.Navigate(outputpage)
    End Sub

    Private Sub Back_Click(sender As Object, e As RoutedEventArgs) Handles Back.Click
        Me.NavigationService.GoBack()
    End Sub
End Class

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
End Class

Public Structure PDFsecurity

End Structure

Public Structure PDFSize

    Public Property type As sizingType
    Public Property isStrech As Boolean
    Public Property size As Size

    Public Shared Function GetPageSize(type As PageType) As Size
        Dim h, w As Single

        Select Case type

            Case PageType.A0
                w = iTextSharp.text.PageSize.A0.Width
                h = iTextSharp.text.PageSize.A0.Height
            Case PageType.A1
                w = iTextSharp.text.PageSize.A1.Width
                h = iTextSharp.text.PageSize.A1.Height
            Case PageType.A2
                w = iTextSharp.text.PageSize.A2.Width
                h = iTextSharp.text.PageSize.A2.Height
            Case PageType.A4
                w = iTextSharp.text.PageSize.A4.Width
                h = iTextSharp.text.PageSize.A4.Height
            Case PageType.A5
                w = iTextSharp.text.PageSize.A5.Width
                h = iTextSharp.text.PageSize.A5.Height
            Case PageType.A6
                w = iTextSharp.text.PageSize.A6.Width
                h = iTextSharp.text.PageSize.A6.Height
            Case PageType.A7
                w = iTextSharp.text.PageSize.A7.Width
                h = iTextSharp.text.PageSize.A7.Height
            Case PageType.A8
                w = iTextSharp.text.PageSize.A8.Width
                h = iTextSharp.text.PageSize.A8.Height
            Case PageType.A9
                w = iTextSharp.text.PageSize.A9.Width
                h = iTextSharp.text.PageSize.A9.Height
            Case PageType.A10
                w = iTextSharp.text.PageSize.A10.Width
                h = iTextSharp.text.PageSize.A10.Height
            Case PageType.Letter
                w = iTextSharp.text.PageSize.LETTER.Width
                h = iTextSharp.text.PageSize.LETTER.Height
            Case PageType.B0
                w = iTextSharp.text.PageSize.B0.Width
                h = iTextSharp.text.PageSize.B0.Height
            Case PageType.B1
                w = iTextSharp.text.PageSize.B1.Width
                h = iTextSharp.text.PageSize.B1.Height
            Case PageType.B2
                w = iTextSharp.text.PageSize.B2.Width
                h = iTextSharp.text.PageSize.B2.Height
            Case PageType.B3
                w = iTextSharp.text.PageSize.B3.Width
                h = iTextSharp.text.PageSize.B3.Height
            Case PageType.B4
                w = iTextSharp.text.PageSize.B4.Width
                h = iTextSharp.text.PageSize.B4.Height
            Case PageType.B5
                w = iTextSharp.text.PageSize.B5.Width
                h = iTextSharp.text.PageSize.B5.Height
            Case PageType.B6
                w = iTextSharp.text.PageSize.B6.Width
                h = iTextSharp.text.PageSize.B6.Height
            Case PageType.B7
                w = iTextSharp.text.PageSize.B7.Width
                h = iTextSharp.text.PageSize.B7.Height
            Case PageType.B8
                w = iTextSharp.text.PageSize.B8.Width
                h = iTextSharp.text.PageSize.B8.Height
            Case PageType.B9
                w = iTextSharp.text.PageSize.B9.Width
                h = iTextSharp.text.PageSize.B9.Height
            Case PageType.B10
                w = iTextSharp.text.PageSize.B10.Width
                h = iTextSharp.text.PageSize.B10.Height

            Case Else
                w = 0
                h = 0

        End Select
        Return New Size(w, h)
    End Function
End Structure

Public Enum sizingType
    Auto
    Fixed
End Enum

Public Enum PageType
    A0
    A1
    A2
    A4
    A5
    A6
    A7
    A8
    A9
    A10
    Letter
    B0
    B1
    B2
    B3
    B4
    B5
    B6
    B7
    B8
    B9
    B10
    Custom
End Enum
