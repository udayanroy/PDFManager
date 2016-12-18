


Imports System.Globalization

Public Class ProgressToAngleConverter
    Implements IMultiValueConverter

    Public Function Convert(values() As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert
        Dim progress As Double = CDbl(values(0))
        Dim progressBar As ProgressBar = CType(values(1), ProgressBar)
        Dim endAngle = 359.9999 * (progress / (progressBar.Maximum - progressBar.Minimum))
        '= 359.9999 * (_progress / 100)
        Return 90 - endAngle
    End Function

    Public Function ConvertBack(value As Object, targetTypes() As Type, parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function

End Class
