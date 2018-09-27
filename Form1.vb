Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        BTextbox1.Text = TextBox1.Text
        BTextbox1.Invalidate()
    End Sub
End Class
