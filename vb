Imports System.Data.SqlClient

Public Class Form1
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Dim itemid As Integer = txtitemid.Text
        Dim itemname As String = txtname.Text
        Dim quantity As Integer = txtquantity.Text
        Dim price As Integer = txtprice.Text
        Dim con As New SqlConnection("Data Source=DESKTOP-U7M0CO7;Initial Catalog=RUIRU;Integrated Security=True")
        Try
            con.Open()
            Dim query As String = "INSERT INTO RUIRU(itemid,itemname,quantity,price) VALUES(@itemid,@itemname,@quantity,@price)"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@itemid", itemid)
            cmd.Parameters.AddWithValue("@itemname", itemname)
            cmd.Parameters.AddWithValue("@quantity", quantity)
            cmd.Parameters.AddWithValue("@price", price)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Item successfully saved", "Saving")
        Catch ex As Exception
            MessageBox.Show("Error! Record not saved" & ex.Message)
        Finally
            con.Close()
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'RUIRUDataSet.ruiru' table. You can move, or remove it, as needed.
        Me.RuiruTableAdapter.Fill(Me.RUIRUDataSet.ruiru)

    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnpreview.Click
        Dim con As New SqlConnection("Data Source=DESKTOP-U7M0CO7;Initial Catalog=RUIRU;Integrated Security=True")
        Try
            con.Open()
            Dim myquery As String = "SELECT * FROM ruiru"
            Dim cmd As New SqlCommand(myquery, con)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvpreview.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error! Record not saved" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
End Class
