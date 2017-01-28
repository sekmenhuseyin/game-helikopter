Imports System.IO 'eklemeyi unutmayýn
Public Class helikoptersecimi

#Region "global deðiþkenler"
    Public Shared resim_sec As Image = Image.FromFile(Directory.GetCurrentDirectory() + "\resimler\helikopter1.gif")
    Public Shared dusmehizi As Integer = 0
    Public Shared tekrarli_ates As Boolean = False
    Public Shared hortum As Boolean = False
    Public Shared atishizi As Integer = 0
#End Region

#Region "eventler"
    Private Sub helikoptersecimi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim yol As String = Directory.GetCurrentDirectory()
        'picturebox lara resimler aktif path ten alýnýyor
        pictureBox1.Image = Image.FromFile(yol + "\resimler\helikopter1.gif")
        pictureBox2.Image = Image.FromFile(yol + "\resimler\helikopter4.gif")
        pictureBox3.Image = Image.FromFile(yol + "\resimler\helikopter3.gif")
        pictureBox4.Image = Image.FromFile(yol + "\resimler\helikopter2.gif")

    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        resim_sec = pictureBox1.Image
        dusmehizi = 10
        tekrarli_ates = True
        hortum = True
        atishizi = 40
        Me.Close()
    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        resim_sec = pictureBox2.Image
        dusmehizi = 5
        tekrarli_ates = False
        hortum = False
        atishizi = 25
        Me.Close()
    End Sub

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click
        resim_sec = pictureBox3.Image
        dusmehizi = 15
        tekrarli_ates = True
        hortum = True
        atishizi = 60
        Me.Close()
    End Sub

    Private Sub button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button4.Click
        resim_sec = pictureBox4.Image
        dusmehizi = 10
        tekrarli_ates = False
        hortum = False
        atishizi = 40
        Me.Close()
    End Sub
#End Region

End Class