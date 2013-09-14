Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class main

    Dim files As New List(Of String)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using opndlg As New OpenFileDialog
            opndlg.Multiselect = True
            If opndlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then



                files.AddRange(opndlg.FileNames)
                ProgressBar1.Maximum = files.Count - 1
                ProgressBar1.Value = 0
                Label1.Text = files.Count & " files selected, Press Start to create pdf"


                Button1.Enabled = True
                Button2.Enabled = True
                Button3.Enabled = False

            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        BackgroundWorker1.RunWorkerAsync()

        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        AboutBox1.ShowDialog(Me)

    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Using strm As New FileStream(TextBox1.Text, FileMode.OpenOrCreate)
            Using docu As New Document(iTextSharp.text.PageSize.A4), witer As PdfWriter = PdfWriter.GetInstance(docu, strm)

                docu.Open()

                For i = 0 To files.Count - 1
                    docu.NewPage()
                    Using imgstrm = System.IO.File.Open(files(i), FileMode.Open)
                        Dim image = iTextSharp.text.Image.GetInstance(imgstrm)
                        image.ScaleAbsolute(iTextSharp.text.PageSize.A4)
                        image.SetAbsolutePosition(0, 0)
                        docu.Add(image)

                    End Using
                    BackgroundWorker1.ReportProgress(i)
                Next

                docu.Close()
            End Using

        End Using


    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
        ProgressBar1.Value = e.ProgressPercentage

        Label1.Text = e.ProgressPercentage + 1 & "/" & files.Count

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        files.Clear()
        ' ProgressBar1.Value = 0
        Label1.Text = "Completed"
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        BackgroundWorker1.CancelAsync()
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Using savedlg As New SaveFileDialog
            savedlg.Filter = "PDF File|*.pdf"
            If savedlg.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                TextBox1.Text = savedlg.FileName
            End If
        End Using
    End Sub

    Private Sub main_Load(sender As Object, e As EventArgs) Handles Me.Load
        Button1.Enabled = True
        Button2.Enabled = False
        Button3.Enabled = False
    End Sub
End Class