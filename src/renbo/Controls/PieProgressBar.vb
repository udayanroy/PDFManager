Public Class PieProgressBar
    Inherits Grid



    Dim pie As New PieShape
    Dim _progress As Double = 0

    Public Event ProgressChanged(progress As Double)

   
    Public Sub New()


      
    End Sub


    Public Property Progress As Double
        Get
            Return _progress
        End Get
        Set(value As Double)
            If value > 100 Then
                _progress = 100
            ElseIf value < 0 Then
                _progress = 0
            Else
                _progress = value
            End If
            OnChangeProgress()
        End Set
    End Property

    Private Sub OnChangeProgress()
        Dim endAngle = 359.9999 * (_progress / 100)
        pie.endAngle = 90 - endAngle

        RaiseEvent ProgressChanged(_progress)
    End Sub

    Private Sub PieProgressBar_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        pie.startAngle = 90
        pie.Width = 300
        pie.Height = 300
        ' pie.StrokeThickness = 5
        '  pie.Stroke = Brushes.Black
        pie.Fill = CType(Me.FindResource("PieBrush"), Brush)
        'Brushes.Green

        'Dim b As New Binding("ThumbBrush")
        'b.Mode = BindingMode.OneTime
        'b.Source = Resources.Item("PieBrush")

        'pie.SetBinding(Shape.FillProperty, b)
        pie.HorizontalAlignment = Windows.HorizontalAlignment.Center
        pie.VerticalAlignment = Windows.VerticalAlignment.Center

        Me.Children.Add(pie)
    End Sub

    Private Sub PieProgressBar_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs) Handles Me.PreviewMouseDown
        Me.Progress = (New Random).Next(0, 100)
    End Sub
End Class



Public Class PieShape
    Inherits Shape

    Public Sub New()
        ' Shape.StretchProperty.OverrideMetadata(GetType(PieShape), New FrameworkPropertyMetadata(Stretch.Fill))

    End Sub


    Public Shared ReadOnly StartAngleProperty As DependencyProperty = DependencyProperty.Register("startAngle", GetType(Double), GetType(PieShape), New FrameworkPropertyMetadata(0.0, (FrameworkPropertyMetadataOptions.AffectsRender Or FrameworkPropertyMetadataOptions.AffectsMeasure Or Stretch.Fill)))
    Public Shared ReadOnly endAngleProperty As DependencyProperty = DependencyProperty.Register("endAngle", GetType(Double), GetType(PieShape), New FrameworkPropertyMetadata(0.0, (FrameworkPropertyMetadataOptions.AffectsRender Or FrameworkPropertyMetadataOptions.AffectsMeasure Or Stretch.Fill)))




    Public Property startAngle As Double
        Get
            Return CDbl(MyBase.GetValue(PieShape.StartAngleProperty))
        End Get
        Set(value As Double)
            MyBase.SetValue(PieShape.StartAngleProperty, value)
        End Set
    End Property

    Public Property endAngle As Double
        Get
            Return CDbl(MyBase.GetValue(PieShape.endAngleProperty))
        End Get
        Set(value As Double)
            MyBase.SetValue(PieShape.endAngleProperty, value)
        End Set
    End Property


   


    Protected Overrides ReadOnly Property DefiningGeometry As Geometry
        Get
            Return createGeom()
        End Get
    End Property

    








    Private Function createGeom() As Geometry
        Dim startPoint = PointAtAngle(Math.Min(startAngle, endAngle))
        Dim endPoint = PointAtAngle(Math.Max(startAngle, endAngle))

        Dim arcSize = New Size(Math.Max(0, (RenderSize.Width - StrokeThickness) / 2), Math.Max(0, (RenderSize.Height - StrokeThickness) / 2))
        Dim isLargeArc = Math.Abs(endAngle - startAngle) > 180


        Dim geom As New StreamGeometry
        geom.FillRule = FillRule.EvenOdd

        Using context = geom.Open
            context.BeginFigure(startPoint, True, True)
            context.ArcTo(endPoint, arcSize, 0, isLargeArc, SweepDirection.Counterclockwise, True, False)
            context.LineTo(New Point(RenderSize.Width / 2, RenderSize.Height / 2), True, False)
            'context.LineTo(startPoint, True, True)
        End Using

        geom.Transform = New TranslateTransform(StrokeThickness / 2, StrokeThickness / 2)
        Return geom
    End Function




    Private Function PointAtAngle(angle As Double) As Point

        Dim radAngle As Double = angle * (Math.PI / 180)
        Dim xRadius As Double = (RenderSize.Width - StrokeThickness) / 2
        Dim yRadius As Double = (RenderSize.Height - StrokeThickness) / 2
        Dim x As Double = xRadius + xRadius * Math.Cos(radAngle)
        Dim y As Double = yRadius - yRadius * Math.Sin(radAngle)
        Return New Point(x, y)

    End Function
End Class