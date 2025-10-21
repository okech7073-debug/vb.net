Imports System.Data.SqlClient

Public Class Form1
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub preview()
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
    Private Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click
        Dim con As New SqlConnection("Data Source=DESKTOP-U7M0CO7;Initial Catalog=RUIRU;Integrated Security=True")
        Try
            con.Open()
            Dim query As String = "SELECT * FROM ruiru WHERE itemname LIKE @itemname"
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@itemname", "%" & txtsearch.Text & "%")
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvpreview.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Error:" & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Dim con As New SqlConnection("Data Source=DESKTOP-U7M0CO7;Initial Catalog=RUIRU;Integrated Security=True")
        Try
            If dgvpreview.SelectedCells.Count = 0 Then
                MessageBox.Show("Please select a record to delete")
                Return
            End If
            Dim itemid As Integer = Convert.ToInt32(dgvpreview.SelectedRows(0).Cells(0).Value)
            Dim confirm As DialogResult = MessageBox.Show("Are you sure you want to delete this record?",
                                                          "Confirm Delete", MessageBoxButtons.YesNo)
            If confirm = DialogResult.No Then
                Return
            End If
            Dim query As String = "DELETE FROM ruiru WHERE itemid=@itemid"
            con.Open()
            Dim cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("itemid", itemid)
            cmd.ExecuteNonQuery()
            con.Close()
            MessageBox.Show("Record successfully deleted")
            preview()
        Catch ex As Exception
            MessageBox.Show("Error!: ", ex.Message)
        Finally
            con.Close()
        End Try
        Call refreshdata()
    End Sub
    Sub refreshdata()
        Dim con As New SqlConnection("Data Source=DESKTOP-U7M0CO7;Initial Catalog=RUIRU;Integrated Security=True")
        Try
            con.Open()
            Dim cmd As New SqlCommand("SELECT COUNT (*) FROM ruiru", con)
            Dim recordcount As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            txtitems.text = recordcount
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub
End Class
