Public Class Form1

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        BaseTextbox1.Text = TextBox1.Text
        BaseTextbox1.Invalidate()
    End Sub
End Class