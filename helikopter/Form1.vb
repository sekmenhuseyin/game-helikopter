Imports System.IO 'bu k�t�phaneyi eklemeyi unutmay�n...
Public Class Form1
    'Korh@n GeRi� prestige 2005'

    'b�t�n picturebox lar i�in size mode strech image se�ildi

    'arka plan rengi design ekran�nda formun background image �zelli�ine 
    'bin\debug dosyas�n�n i�indeki resimler klas�r�nden se�ilmi�tir

#Region "global de�i�kenler"

    'helikopterin ate�i olu�turuluyor
    Dim helikopter_ates As New PictureBox()
    Dim kus1_ates(3) As PictureBox
    Dim kus2_ates(3) As PictureBox
    Dim kus3_ates(3) As PictureBox
    Dim animasyon_patlama As New PictureBox()
    Dim animasyon_tebrikler As New PictureBox()
    'hortum i�in yerle�tirilen picturebox lar bu diziye al�nacak
    Dim hortum(3) As PictureBox

    Dim ates_hizi As Integer = 0
    Dim dusme_hizi As Integer = 0
    'visible � a��lan hortumun indisini burda tutucam
    Dim hangi_hortum As Integer

    'ku�lar�n hareketlerinin h�z�n� ayarl�yorum
    Dim hareket_x_kus1 As Integer = -5, hareket_y_kus1 As Integer = -10
    Dim hareket_x_kus2 As Integer = -10, hareket_y_kus2 As Integer = -5
    Dim hareket_x_kus3 As Integer = -5, hareket_y_kus3 As Integer = -10

#End Region

#Region "prosed�rler"
    Private Sub resimleri_forma_yerlestir()
        Dim yol As String = Directory.GetCurrentDirectory()
        'picturebox lara resimler aktif path ten al�n�yor
        manzara1.Image = Image.FromFile(yol + "\resimler\manzara1.jpg")
        manzara2.Image = Image.FromFile(yol + "\resimler\manzara2.jpg")
        manzara3.Image = Image.FromFile(yol + "\resimler\manzara3.jpg")
        manzara4.Image = Image.FromFile(yol + "\resimler\manzara4.jpg")
        'kuslar� al
        kus1.Image = Image.FromFile(yol + "\resimler\kus1.gif")
        kus2.Image = Image.FromFile(yol + "\resimler\kus1.gif")
        kus3.Image = Image.FromFile(yol + "\resimler\kus2.gif")

        'helikopteri al
        '�nce form2 deki de�i�kenleri kontrol et
        If (helikoptersecimi.resim_sec Is "") Then
            helikopter.Image = Image.FromFile(yol + "\resimler\helikopter1.gif")
        Else
            helikopter.Image = helikoptersecimi.resim_sec
        End If
        If (helikoptersecimi.atishizi = 0) Then
            ates_hizi = 40
        Else
            ates_hizi = helikoptersecimi.atishizi
        End If
        If (helikoptersecimi.dusmehizi = 0) Then
            dusme_hizi = 10
        Else
            dusme_hizi = helikoptersecimi.dusmehizi
        End If
        'helikopter patlamas� i�in 
        animasyon_patlama.Image = Image.FromFile(yol + "\resimler\patlama.gif")
        'kazan�nca animasyon ��ks�n
        animasyon_tebrikler.Image = Image.FromFile(yol + "\resimler\tebrikler.gif")
        'hortumun animasyonunu al
        pictureBox1.Image = Image.FromFile(yol + "\resimler\hortum.gif")
        pictureBox2.Image = Image.FromFile(yol + "\resimler\hortum.gif")
        pictureBox3.Image = Image.FromFile(yol + "\resimler\hortum.gif")

        'hortum pictureboxlar�n� diziye al�yorum..
        hortum(0) = pictureBox1
        hortum(1) = pictureBox2
        hortum(2) = pictureBox3
    End Sub

    Private Sub oyun_baslat()
        'resimlerin yerle�tirilmesi i�in �a�r�l�yor
        resimleri_forma_yerlestir()
        'helikopterin a�a��ya doru �ekilmesini sa�layan timer
        timer_helikopter.Interval = 500
        timer_helikopter.Enabled = True
        'tu�lar�n �al��maya ba�lamas� i�in
        Me.KeyPreview = True
        'helikopter_ates i at�ld��� do�rultuda g�nderecek timerin interval �n� ayarla,
        timer_helikopter_ates.Interval = 100
        'kus1 in hareketini sa�layan timer � a�
        timer_kus1_hareket.Interval = 500
        timer_kus1_hareket.Enabled = True
        'kus2 nin hareketini sa�layan timer � a�
        timer_kus2_hareket.Interval = 500
        timer_kus2_hareket.Enabled = True
        'kus3 �n hareketi
        timer_kus3_hareket.Interval = 500
        timer_kus3_hareket.Enabled = True
        'kus1 in ate�i i�in
        timer_kus1_ates.Interval = 20
        'kus2 i�in
        timer_kus2_ates.Interval = 60
        'kus3
        timer_kus3_ates.Interval = 100
        If (helikoptersecimi.hortum <> False) Then
            'hortumun g�r�n�rl��� i�in
            timer_hortum.Enabled = True
            timer_hortum.Interval = 5000
        End If

        Dim i As Integer
        For i = 0 To 2
            'ilk �nce tan�ml�yorum ��nk� kus1_ates_olustur
            'prosed�r�nde �nceden olu�turulmu� olanalar Dispose ediliyor
            kus1_ates(i) = New PictureBox()
            kus2_ates(i) = New PictureBox()
            kus3_ates(i) = New PictureBox()
        Next

        'oyunu tekrar ba�lat�lmas� durumu i�in ku�lar�n visible lar�n� a�
        kus1.Visible = True
        kus2.Visible = True
        kus3.Visible = True

        'oyunun tekrar ba�lat�lmas� durumu i�in helikopterin yerini tekrar belirle
        helikopter.Visible = True
        helikopter.Left = 617
        helikopter.Top = 69

        'hepsinin ate�ini att�rmak i�in
        timer_genel_ates.Interval = 7000
        timer_genel_ates.Enabled = True

        'ku�lar�n ate�ini olu�tur
        kus1_ates_olustur()
        kus2_ates_olustur()
        kus3_ates_olustur()

        timer_kus1_ates.Enabled = True
        timer_kus2_ates.Enabled = True
        timer_kus3_ates.Enabled = True

        'hortum lar� gizleyerek ba�la
        pictureBox1.Visible = False
        pictureBox2.Visible = False
        pictureBox3.Visible = False

        'di�er oyunlar�n bitiminden kalan animasyonlar�n g�r�n�rl���n� kapat
        animasyon_patlama.Visible = False
        animasyon_tebrikler.Visible = False

    End Sub

    Private Sub ates_olustur()
        'daha �nceden olu�turulmu� bir ate� varsa onu yok et
        helikopter_ates.Dispose()
        Me.Refresh()
        'yeni ate� nesnesini olu�tur
        helikopter_ates = New PictureBox()
        Me.Controls.Add(helikopter_ates)
        helikopter_ates.SetBounds(helikopter.Left, helikopter.Top + helikopter.Height / 2, 10, 3)
        helikopter_ates.BackColor = Color.Red
        'ate� hareketini ba�lat
        timer_helikopter_ates.Enabled = True

    End Sub

    Private Function binalara_degdi_manzara1(ByVal yon As String) As Boolean
        'birinci manzara i�in sadece a�a��s�nda kalma durumu var
        If (yon = "asag�") Then
            If ((manzara1.Left + manzara1.Width >= helikopter.Left) And (manzara1.Left <= helikopter.Left)) Then
                If ((420 <= helikopter.Top)) Then
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Function binalara_degdi_manzara2(ByVal yon As String) As Boolean
        'ikinci manzara i�in hem sa��ndan hem de a�a��s�ndan de�me durumu var
        If (yon = "sag") Then
            If ((manzara2.Left + manzara2.Width >= helikopter.Left + helikopter.Width) And (manzara2.Left <= helikopter.Left + helikopter.Width)) Then
                If ((317 <= helikopter.Top)) Then
                    Return False
                End If
            End If
        ElseIf (yon = "asag�") Then
            If ((manzara2.Left + manzara2.Width >= helikopter.Left) And (manzara2.Left <= helikopter.Left)) Then
                If ((317 <= helikopter.Top)) Then
                    Me.Text = "yand�n"
                    Return False
                End If
            End If
        End If
        Return True
    End Function

    Private Function binalara_degdi_manzara3(ByVal yon As String) As Boolean
        '���nc� manzara i�in sol sa� ve a�a�� durumu var
        If (yon = "sol") Then
            'helikopterin s�n�rlar�na degdimi
            If ((manzara3.Left + manzara3.Width >= helikopter.Left + helikopter.Width - 32) And (manzara3.Left <= helikopter.Left + helikopter.Width - 32)) Then
                If ((210 <= helikopter.Top)) Then
                    'e�er yanarsa false de�eri d�nd�r
                    Return False
                End If
            End If
        ElseIf (yon = "sag") Then
            If ((manzara3.Left + manzara3.Width >= helikopter.Left + helikopter.Width) And (manzara3.Left <= helikopter.Left + helikopter.Width)) Then
                If ((213 <= helikopter.Top)) Then
                    Return False
                End If
            End If
        ElseIf (yon = "asag�") Then
            If ((manzara3.Left + manzara3.Width >= helikopter.Left) And (manzara3.Left <= helikopter.Left)) Then
                If ((213 <= helikopter.Top)) Then
                    Return False
                End If
            End If

        End If
        Return True

    End Function

    Private Function binalara_degdi_manzara4(ByVal yon As String) As Boolean
        'd�rd�nc� manzara i�in a�a�� durumu var
        If (yon = "asag�") Then
            If ((manzara4.Left + manzara4.Width >= helikopter.Left) And (manzara4.Left <= helikopter.Left)) Then
                If ((409 <= helikopter.Top)) Then
                    Return False
                End If
            End If
        End If
        Return True

    End Function

    Private Sub hortuma_degdi()
        'aktif olan hortuma de�dimi
        If (hortum(hangi_hortum).Visible = True) Then
            If ((hortum(hangi_hortum).Left + hortum(hangi_hortum).Width >= helikopter.Left) And (hortum(hangi_hortum).Left <= helikopter.Left)) Then
                If ((hortum(hangi_hortum).Top <= helikopter.Top) And (hortum(hangi_hortum).Top + hortum(hangi_hortum).Height >= helikopter.Top)) Then
                    patlat()
                End If
            End If
        End If
    End Sub

    Private Sub kus1e_degdi()
        'kus1 e de�dimi diye bak�l�yor
        If ((kus1.Left + kus1.Width >= helikopter.Left) And (kus1.Left <= helikopter.Left)) Then
            If ((kus1.Top <= helikopter.Top) And (kus1.Top + kus1.Height >= helikopter.Top)) Then
                patlat()
            End If
        End If
    End Sub

    Private Sub kus2e_degdi()
        'kus2 i�in bak�l�yor
        If ((kus2.Left + kus2.Width >= helikopter.Left) And (kus2.Left <= helikopter.Left)) Then
            If ((kus2.Top <= helikopter.Top) And (kus2.Top + kus2.Height >= helikopter.Top)) Then
                patlat()
            End If
        End If
    End Sub

    Private Sub kus3e_degdi()
        'kus3 i�in bak�l�yor
        If ((kus3.Left + kus3.Width >= helikopter.Left) And (kus3.Left <= helikopter.Left)) Then
            If ((kus3.Top <= helikopter.Top) And (kus3.Top + kus3.Height >= helikopter.Top)) Then
                patlat()
            End If
        End If
    End Sub

    Private Sub kus1_ates_olustur()
        'olu�turulan ate�ler bir taneymi� gibi g�z�kmesin diye aral�k de�i�keni tan�ml�yoruz
        Dim aral�k As Integer = 0
        Dim i As Integer
        If (timer_kus1_hareket.Enabled = True) Then
            For i = 0 To 2
                kus1_ates(i).Dispose()
                'daha �nceden olu�turulmu� bir ate� varsa onu yok et
                kus1_ates(i) = New PictureBox()
                Me.Controls.Add(kus1_ates(i))
                kus1_ates(i).SetBounds(kus1.Left, kus1.Top - aral�k + kus1.Height / 2, 10, 1)
                kus1_ates(i).BackColor = Color.Blue
                kus3_ates(i).BringToFront()
                Me.Refresh()
                aral�k -= 10
            Next
        End If

    End Sub

    Private Sub kus2_ates_olustur()
        'olu�turulan ate�ler bir taneymi� gibi g�z�kmesin diye aral�k de�i�keni tan�ml�yoruz
        Dim aral�k As Integer = 0
        Dim i As Integer
        'daha �nceden olu�turulmu� bir ate� varsa onu yok et
        If (timer_kus2_hareket.Enabled = True) Then
            For i = 0 To 2
                kus2_ates(i).Dispose()
                kus2_ates(i) = New PictureBox()
                Me.Controls.Add(kus2_ates(i))
                kus2_ates(i).SetBounds(kus2.Left, kus2.Top - aral�k + kus2.Height / 2, 10, 1)
                kus2_ates(i).BackColor = Color.Green
                kus3_ates(i).BringToFront()
                Me.Refresh()
                aral�k -= 30
            Next
        End If

    End Sub

    Private Sub kus3_ates_olustur()
        'olu�turulan ate�ler bir taneymi� gibi g�z�kmesin diye aral�k de�i�keni tan�ml�yoruz
        Dim aral�k As Integer = 0
        Dim i As Integer
        'daha �nceden olu�turulmu� bir ate� varsa onu yok et
        If (timer_kus3_hareket.Enabled = True) Then
            For i = 0 To 2
                kus3_ates(i).Dispose()
                kus3_ates(i) = New PictureBox()
                Me.Controls.Add(kus3_ates(i))
                kus3_ates(i).SetBounds(kus3.Left - aral�k, kus3.Top + kus3.Height / 2, 10, 1)
                kus3_ates(i).BackColor = Color.Yellow
                kus3_ates(i).BringToFront()
                Me.Refresh()
                aral�k -= 50
            Next
        End If

    End Sub

    Private Sub helikopter_vuruldu()
        'olu�turulan ate�ler her ku� i�in helikoptere bak�l�yor
        Dim i As Integer
        For i = 0 To 2
            If ((kus1_ates(i).Left < helikopter.Left + helikopter.Width) And (kus1_ates(i).Left > helikopter.Left)) Then
                If ((kus1_ates(i).Top < helikopter.Top + helikopter.Height) And (kus1_ates(i).Top > helikopter.Top)) Then
                    patlat()
                    kus1_ates(i).Dispose()
                    Me.Refresh()
                    Return
                End If
            End If
            If ((kus2_ates(i).Left < helikopter.Left + helikopter.Width) And (kus2_ates(i).Left > helikopter.Left)) Then
                If ((kus2_ates(i).Top < helikopter.Top + helikopter.Height) And (kus2_ates(i).Top > helikopter.Top)) Then
                    patlat()
                    kus2_ates(i).Dispose()
                    Me.Refresh()
                    Return
                End If
            End If
            If ((kus3_ates(i).Left < helikopter.Left + helikopter.Width) And (kus3_ates(i).Left > helikopter.Left)) Then
                If ((kus3_ates(i).Top < helikopter.Top + helikopter.Height) And (kus3_ates(i).Top > helikopter.Top)) Then
                    patlat()
                    kus3_ates(i).Dispose()
                    Me.Refresh()
                    Return
                End If
            End If
        Next
    End Sub

    Private Sub patlat()
        'animasyonu a�
        animasyon_patlama.Visible = True
        animasyon_patlama.SetBounds(helikopter.Left, helikopter.Top, 100, 100)
        animasyon_patlama.BackgroundImage = helikopter.BackgroundImage
        animasyon_patlama.BringToFront()
        Me.Refresh()
        'helikopterin g�r�n�rl���n� kapat
        helikopter.Visible = False
        Me.Refresh()

        'olu�turulmu� ate�ler varsa yok et
        Dim i As Integer
        For i = 0 To 2
            kus1_ates(i).Dispose()
            kus2_ates(i).Dispose()
            kus3_ates(i).Dispose()
        Next
        'helikopterin ate�i duruyorsa onuda yok et
        helikopter_ates.Dispose()
        timer_hortum.Enabled = False
        'hortumlar�n g�r�n�rl���n� kapat
        For i = 0 To 2
            hortum(i).Visible = False
        Next

        'timerlar� kapat
        timer_genel_ates.Enabled = False
        timer_helikopter.Enabled = False
        timer_kus1_ates.Enabled = False
        timer_helikopter_ates.Enabled = False
        timer_kus1_hareket.Enabled = True
        timer_kus2_ates.Enabled = True
        timer_kus2_hareket.Enabled = False
        timer_kus3_ates.Enabled = False
        timer_kus3_hareket.Enabled = False

        'ku�lar�n g�r�n�rl���n� kapat
        kus1.Visible = False
        kus2.Visible = False
        kus3.Visible = False

        'men�y� aktifle�tir
        menuStrip1.Enabled = True
    End Sub

    Private Sub kazandin()
        'animasyon_tebrikler = new PictureBox() 
        animasyon_tebrikler.Visible = True
        animasyon_tebrikler.SetBounds(300, 100, 100, 100)
        animasyon_tebrikler.BackgroundImage = helikopter.BackgroundImage
        helikopter.Visible = False
        Me.Refresh()

        timer_hortum.Enabled = False
        Dim i As Integer
        For i = 0 To 2
            hortum(i).Visible = False
        Next
        helikopter_ates.Dispose()

        'timerlar� kapat
        timer_genel_ates.Enabled = False
        timer_helikopter.Enabled = False
        timer_kus1_ates.Enabled = False
        timer_helikopter_ates.Enabled = False
        timer_kus1_hareket.Enabled = True
        timer_kus2_ates.Enabled = True
        timer_kus2_hareket.Enabled = False
        timer_kus3_ates.Enabled = False
        timer_kus3_hareket.Enabled = False

        menuStrip1.Enabled = True

    End Sub

    Private Sub kuslar_vuruldu()
        'her ku� vuruldumu diye bak
        If (kus1.Visible = True) Then
            If ((helikopter_ates.Left < kus1.Left + kus1.Width) And (helikopter_ates.Left > kus1.Left)) Then
                If ((helikopter_ates.Top < kus1.Top + kus1.Height) And (helikopter_ates.Top > kus1.Top)) Then
                    'vurulduysa ate�lerini yok et
                    'kusun g�r�n�rl���n� kapat
                    kus1.Visible = False
                    'helikopterin ate�ini yok et
                    helikopter_ates.Dispose()
                    Me.Refresh()
                    'ku�un hareketini sa�layan timer� kapat
                    timer_kus1_hareket.Enabled = False
                    Return
                End If
            End If
        End If
        If (kus2.Visible = True) Then
            If ((helikopter_ates.Left < kus2.Left + kus2.Width) And (helikopter_ates.Left > kus2.Left)) Then
                If ((helikopter_ates.Top < kus2.Top + kus2.Height) And (helikopter_ates.Top > kus2.Top)) Then
                    kus2.Visible = False
                    helikopter_ates.Dispose()
                    Me.Refresh()
                    timer_kus2_hareket.Enabled = False
                    Return
                End If
            End If
        End If
        If (kus3.Visible = True) Then
            If ((helikopter_ates.Left < kus3.Left + kus3.Width) And (helikopter_ates.Left > kus3.Left)) Then
                If ((helikopter_ates.Top < kus3.Top + kus3.Height) And (helikopter_ates.Top > kus3.Top)) Then
                    kus3.Visible = False
                    helikopter_ates.Dispose()
                    Me.Refresh()
                    timer_kus3_hareket.Enabled = False
                    Return
                End If
            End If
        End If
    End Sub

    Private Sub kuslar_bitti()
        'ku�lar bittiyse oyunu bitir
        If ((kus1.Visible = False) And (kus2.Visible = False) And (kus3.Visible = False)) Then
            kazandin()
        End If
    End Sub

    Private Sub hortum_cikar()
        '3 hortumundan birini random ��kar�ca
        'rasgele bir se�im yap�l�yor
        Dim i As Integer
        Dim deger As Integer = Rnd() * 3
        For i = 0 To 2
            If (deger = i) Then
                hortum(i).Visible = True
                hangi_hortum = i
            End If
        Next
    End Sub

#End Region

    Private Sub timer_helikopter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_helikopter.Tick
        'binalara de�di mi diye teker teker bak
        If (binalara_degdi_manzara1("asag�") = False) Then

            patlat()
        End If
        If (binalara_degdi_manzara2("asag�") = False) Then

            patlat()
        End If
        If (binalara_degdi_manzara3("asag�") = False) Then

            patlat()
        End If
        If (binalara_degdi_manzara4("asag�") = False) Then

            patlat()
        End If

        'ku�lar bittimi diye bak
        kuslar_bitti()

        'a��k olan hortuma g�re hortumun kendisine g�re �ekmesini sa�la
        If (hortum(hangi_hortum).Visible = True) Then

            If ((hortum(hangi_hortum).Left <= helikopter.Left) And (hortum(hangi_hortum).Top >= helikopter.Top)) Then

                helikopter.Top += dusme_hizi
                helikopter.Left -= dusme_hizi

            ElseIf ((hortum(hangi_hortum).Left <= helikopter.Left) And (hortum(hangi_hortum).Top <= helikopter.Top)) Then

                helikopter.Top -= dusme_hizi
                helikopter.Left -= dusme_hizi

            ElseIf ((hortum(hangi_hortum).Left >= helikopter.Left) And (hortum(hangi_hortum).Top >= helikopter.Top)) Then

                helikopter.Top += dusme_hizi
                helikopter.Left += dusme_hizi

            ElseIf ((hortum(hangi_hortum).Left >= helikopter.Left) And (hortum(hangi_hortum).Top <= helikopter.Top)) Then

                helikopter.Top -= dusme_hizi
                helikopter.Left += dusme_hizi

            End If
        Else

            helikopter.Top += dusme_hizi
        End If
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'bas�lan tu�a g�re helikopterin hareketini belirle
        If (e.KeyCode = Keys.Up) Then

            If (helikopter.Top - 25 > 0) Then

                helikopter.Top -= 7
            End If

        ElseIf (e.KeyCode = Keys.Left) Then

            'binalara de�di mi kontrol et
            If (binalara_degdi_manzara3("sol") = False) Then

                patlat()
                Return
            End If
            helikopter.Left -= 10

        ElseIf (e.KeyCode = Keys.Right) Then

            If (binalara_degdi_manzara2("sag") = False) Then

                patlat()
                Return
            End If
            If (binalara_degdi_manzara3("sag") = False) Then

                patlat()
                Return
            End If
            helikopter.Left += 7


        ElseIf (e.KeyCode = Keys.Down) Then

            If (binalara_degdi_manzara1("asag�") = False) Then

                patlat()
                Return
            End If
            If (binalara_degdi_manzara2("asag�") = False) Then

                patlat()
                Return
            End If
            If (binalara_degdi_manzara3("asag�") = False) Then

                patlat()
                Return
            End If
            If (binalara_degdi_manzara4("asag�") = False) Then

                patlat()
                Return
            End If
            helikopter.Top += 7
        End If

        'ku�lara de�dimi diye kontrol et
        kus1e_degdi()
        kus2e_degdi()
        kus3e_degdi()
        'hortuma de�di mi diye kontrol et
        hortuma_degdi()
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        'ate� olu�tur
        If (e.KeyCode = Keys.Space) Then
            ates_olustur()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'animasyon nesnelerini olu�tur
        Me.Controls.Add(animasyon_tebrikler)
        Me.Controls.Add(animasyon_patlama)
        animasyon_patlama.Visible = False
        animasyon_tebrikler.Visible = False

    End Sub

    Private Sub timer_helikopter_ates_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_helikopter_ates.Tick
        helikopter_ates.Left -= ates_hizi
    End Sub

    Private Sub timer_kus1_hareket_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_kus1_hareket.Tick

        'kus 1 i�in blirlenen koordinatlar�n d���na ��k�lmas�n� engeller
        If ((kus1.Left <= 0)) Then

            '�nce en sol kenar i�in bak�l�yor
            kus1.Left += 10
            hareket_x_kus1 = -hareket_x_kus1

        ElseIf (kus1.Left >= 130) Then

            'sonra sa� koordinat i�in bak�l�yor
            kus1.Left -= 10
            hareket_x_kus1 = -hareket_x_kus1

        ElseIf ((kus1.Top <= 0)) Then

            'yukar�s� i�in bak�l�yor
            kus1.Top += 10
            hareket_y_kus1 = -hareket_y_kus1

        ElseIf (kus1.Top >= 130) Then

            'a�a��s� i�in bak�l�yor
            kus1.Top -= 10
            hareket_y_kus1 = -hareket_y_kus1
        End If
        'de�ilse artt�r
        kus1.Left += hareket_x_kus1
        kus1.Top += hareket_y_kus1
        helikopter_vuruldu()
        kuslar_vuruldu()
    End Sub

    Private Sub timer_kus2_hareket_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_kus2_hareket.Tick
        If ((kus2.Left <= 130)) Then

            kus2.Left += 10
            hareket_x_kus2 = -hareket_x_kus2

        ElseIf (kus2.Left >= 230) Then

            kus2.Left -= 10
            hareket_x_kus2 = -hareket_x_kus2

        ElseIf ((kus2.Top <= 130)) Then

            kus2.Top += 10
            hareket_y_kus2 = -hareket_y_kus2

        ElseIf (kus2.Top >= 230) Then

            kus2.Top -= 10
            hareket_y_kus2 = -hareket_y_kus2
        End If
        kus2.Left += hareket_x_kus2
        kus2.Top += hareket_y_kus2
        helikopter_vuruldu()
        kuslar_vuruldu()
    End Sub

    Private Sub timer_kus3_hareket_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_kus3_hareket.Tick
        If ((kus3.Left <= 240)) Then

            kus3.Left += 10
            hareket_x_kus3 = -hareket_x_kus3

        ElseIf (kus3.Left >= 350) Then

            kus3.Left -= 10
            hareket_x_kus3 = -hareket_x_kus3

        ElseIf ((kus3.Top <= 0)) Then

            kus3.Top += 10
            hareket_y_kus3 = -hareket_y_kus3

        ElseIf (kus3.Top >= 130) Then

            kus3.Top -= 10
            hareket_y_kus3 = -hareket_y_kus3
        End If
        kus3.Left += hareket_x_kus3
        kus3.Top += hareket_y_kus3
        helikopter_vuruldu()
        kuslar_vuruldu()
    End Sub

    Private Sub timer_kus1_ates_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_kus1_ates.Tick
        Dim i As Integer
        For i = 0 To 2
            kus1_ates(i).Left += 10
        Next
    End Sub

    Private Sub timer_kus2_ates_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_kus2_ates.Tick
        Dim i As Integer
        For i = 0 To 2
            kus2_ates(i).Left += 10
        Next
    End Sub

    Private Sub timer_kus3_ates_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_kus3_ates.Tick
        Dim i As Integer
        For i = 0 To 2
            kus3_ates(i).Left += 10
        Next
    End Sub

    Private Sub timer_genel_ates_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_genel_ates.Tick
        kus1_ates_olustur()
        kus2_ates_olustur()
        kus3_ates_olustur()
    End Sub

    Private Sub helikopterSe�ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles helikopterSe�ToolStripMenuItem.Click
        'ikinci formu a�
        Dim yeni As New helikoptersecimi()
        yeni.Show()
    End Sub

    Private Sub ba�latToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ba�latToolStripMenuItem.Click
        'oyunu ba�lat
        oyun_baslat()
        menuStrip1.Enabled = False
    End Sub

    Private Sub timer_hortum_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer_hortum.Tick
        'belli zaman aral�klar�na g�re hortuma ��kar
        If (hortum(hangi_hortum).Visible = False) Then
            hortum_cikar()
        Else
            hortum(hangi_hortum).Visible = False
        End If
    End Sub
End Class
