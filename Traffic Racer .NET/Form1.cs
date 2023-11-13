using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Traffic_Racer.NET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int SeritSayisi = 1;
        int yol = 0;
        int hız = 70;

        Random R = new Random();

   
        //araba sayısını ve arabayı oluşturma
        Random_Car[] rndCar = new Random_Car[2];
        void BringRandomCar(PictureBox pb)
        {
            int rnd = R.Next(0,4);

            switch (rnd)
            {
                case 0:
                    pb.Image = Properties.Resources.car01;
                    break;
                case 1:
                    pb.Image = Properties.Resources.car11;
                    break;
                case 2:
                    pb.Image = Properties.Resources.car21;
                    break;
                case 3:
                    pb.Image = Properties.Resources.car31;
                    break;

            }

            pb.SizeMode = PictureBoxSizeMode.StretchImage;


        }


        //aracın yerlerini ayarlayan kod
        private void AracYerine()
        {
            if(SeritSayisi == 1)
            {
                RedCar.Location = new Point(223,373);
            }
            else if (SeritSayisi == 0)
            {
                RedCar.Location = new Point(60,373);
            }
            else if (SeritSayisi == 2)
            {
                RedCar.Location = new Point(400,373);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //yolda ilerlerken şerit değiştirmeye yarayan kod
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (SeritSayisi < 2)

                    SeritSayisi++;

            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (SeritSayisi > 0)
                    SeritSayisi--;

            }
            //M tuşunun sesi kapatmasına yardımcı olan kod
            else if (e.KeyCode == Keys.M)
            {

                if (SesKonrtol == true)
                {
                    SesKonrtol = false;
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                    pictureBox1.Image = Properties.Resources.volumeOff1;
                }
                else if (SesKonrtol == false)
                {
                    SesKonrtol = true;
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    pictureBox1.Image = Properties.Resources.volumeON1;
                }
            }
            //P tuşu ile oyunu durdurma
            else if (e.KeyCode == Keys.P)
            {
                if (oyunKontrol == true)
                {
                    oyunKontrol = false;
                    timerRandomCar.Stop();
                    timerSerit.Stop();
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                    pictureBox2.Image = Properties.Resources.playicon1;
                }
                else if (oyunKontrol == false)
                {
                    oyunKontrol = true;
                    timerRandomCar.Start();
                    timerSerit.Start();
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                    pictureBox2.Image = Properties.Resources.pause_button_icon_123935;
                }
            

            }
            AracYerine();
        }
        //ses ekleme
        private void RandomMusicEkle()
        {
           

            axWindowsMediaPlayer1.URL = @"music/tokyo.mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();


        }
        //Yazdığımız işlevlerin form üzerinde çalışması için yazdığımız kod
        private void Form1_Load(object sender, EventArgs e)
        {
            for(var i = 0; i < rndCar.Length; i++)
            {
                rndCar[i] = new Random_Car();
            }
            rndCar[0].vakit = true;
            AracYerine();
            RandomMusicEkle();
           

            labelHighScore.Text = Settings1.Default.score.ToString();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
        //sesi açıp kapama kodu
        bool SesKonrtol = true;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (SesKonrtol == true)
            {
                SesKonrtol = false;
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                pictureBox1.Image = Properties.Resources.volumeOff1;
            }
            else if ( SesKonrtol== false)
            {
                SesKonrtol = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                pictureBox1.Image = Properties.Resources.volumeON1;
            }
        }
        //arabaları oluşturan döngünün kodu
        private void timerRandomCar_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rndCar.Length; i++)
            {
                if (!rndCar[i].FakeHaveCar && rndCar[i].vakit)
                {
                    rndCar[i].FakeCar = new PictureBox();
                    BringRandomCar(rndCar[i].FakeCar);
                    rndCar[i].FakeCar.Size = new Size(70, 120);
                    rndCar[i].FakeCar.Top = -rndCar[i].FakeCar.Height;//gelen araçtan sonraki gelen aracın ardı ardına gelmemesi için yapıyoruz
                                                                     //ve gelen araçların arasında bir mesafe oluyor.

                    int SeriteYerlestir = R.Next(0, 3);

                    if (SeriteYerlestir == 0)
                    {
                        rndCar[i].FakeCar.Left = 60;
                    }
                    else if (SeriteYerlestir == 1)
                    {
                        rndCar[i].FakeCar.Left = 223;
                    }
                    else if (SeriteYerlestir == 2)
                    {
                        rndCar[i].FakeCar.Left = 400;
                    }

                    this.Controls.Add(rndCar[i].FakeCar);
                    rndCar[i].FakeHaveCar = true;

                }
                else
                {
                    if (rndCar[i].vakit)
                    {
                        rndCar[i].FakeCar.Top += 20;
                        if (rndCar[i].FakeCar.Top >= 154)
                        {
                            for (int j = 0; j < rndCar.Length; j++)
                            {
                                if (!rndCar[j].vakit)
                                {
                                    rndCar[j].vakit = true;
                                    break;
                                }
                            }
                        }
                        //bu döngüyü yapma sebebimiz oluşturduğumuz arabaların döndden çıkmasını sağlamak
                        //eğer uzun süre döngüde kalırsa(FakeCar) döngümüz bir süre sonra kırılır ve programımız çalışmaz

                        if (rndCar[i].FakeCar.Top >= this.Height - 20)
                        {
                            rndCar[i].FakeCar.Dispose();
                            rndCar[i].FakeHaveCar = false;
                            rndCar[i].vakit = false;

                        }

                    }


                }

                //Kaza Durumunda çalışacak kod

                if (rndCar[i].vakit)
                {
                     
                    float mX = Math.Abs((RedCar.Left + (RedCar.Width / 2)) - (rndCar[i].FakeCar.Left + (rndCar[i].FakeCar.Width / 2)));
                    float mY = Math.Abs((RedCar.Top + (RedCar.Height / 2)) - (rndCar[i].FakeCar.Top + (rndCar[i].FakeCar.Height / 2)));
                    float FarkGenislik = (RedCar.Width / 2) + (rndCar[i].FakeCar.Width / 2);
                    float FarkYukseklik = (RedCar.Height / 2) + (rndCar[i].FakeCar.Height / 2);

                    //kaza durumunun gerçekleştiği kod
                    if ((FarkGenislik > mX) && (FarkYukseklik > mY))
                    {
                        //kaza durumunda durdurma ve ses ayarlama
                        timerRandomCar.Enabled = false;
                        timerSerit.Enabled = false;
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                        axWindowsMediaPlayer1.URL = @"music/crash.mp3";

                        //yüksek skor tutan yer

                        if (yol > Settings1.Default.score)
                        {

                            MessageBox.Show("Yeni Yüksek Skor: " + yol.ToString() + "m", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Settings1.Default.score = yol;
                            Settings1.Default.Save();


                        }
                        axWindowsMediaPlayer1.Ctlcontrols.play();

                        //kaza yaptıktan sonra uyarı vererek tekrar oynamak ister misin diye sorgulama döngüsü
                        DialogResult dr = MessageBox.Show("Oyun Bitti! Yeniden denemek ister misiniz? ", "Uyarı!", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        
                        if (dr == DialogResult.Yes)
                        {

                            AracYerine();

                            for(int j = 0; j < rndCar.Length; j++)
                            {
                                //sahte araçları kaldırıp zamanı durduruyor

                                rndCar[j].FakeCar.Dispose();
                                rndCar[j].FakeHaveCar = false;
                                rndCar[j].vakit = false;
                            }

                            yol = 0;
                            hız = 70;

                            rndCar[0].vakit = true;
                            timerRandomCar.Enabled = true;
                            timerRandomCar.Interval = 200;

                            timerSerit.Enabled = true;
                            timerSerit.Interval=200;

                            RandomMusicEkle();
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                            pictureBox1.Image = Properties.Resources.volumeON1;

                            labelHighScore.Text = Settings1.Default.score.ToString();

                        }
                        else
                        {
                            this.Close();
                        }
                    }

                }



            }
        }
       
        //yola göre level ayarlama
        void hizLevel()
        {
            if (yol < 150)
            {
                labelLevel.Text = "Level: 1";
                timerSerit.Interval = 125;
                timerRandomCar.Interval = 100;

            }

            //2. seviye
            else if (yol > 150 && yol < 300)
            {
                hız = 100;
                timerSerit.Interval = 110;
                timerRandomCar.Interval = 90;
                labelLevel.Text = "Level: 2 ";

            }

            //3.seviye
            else if (yol > 300 && yol < 550)
            {
                hız = 130;
                timerSerit.Interval = 100;
                timerRandomCar.Interval = 80;

                labelLevel.Text = "Level: 3";
            }

            //4.seviye
            else if (yol > 550)
            {
                hız = 170;
                timerSerit.Interval = 80;
                timerRandomCar.Interval = 20;
                labelLevel.Text = "Level: 4";
            }
        }
        //seritlerin hareketini ayarlayan kod
        bool SeritHareket = false;

        private void timerSerit_Tick(object sender, EventArgs e)
        {//bunun için sonsuz döngü yapacağız ki bu sayede seritlerin hareketi form çakıstıgı sürece devam etsin
            yol += 1;
            hizLevel();
            
            if (SeritHareket == false)
            {
                for(int i =1; i < 7; i++)
                {
                    this.Controls.Find("labelSolSerit"+ i.ToString(),true)[0].Top-= 25;
                    //ilgili nesneyi bulması için yazdık ve ilgili neseneyi yukarıyta doğru hareketini ayarladık
                    this.Controls.Find("labelSagSerit" + i.ToString(), true)[0].Top -= 25;
                    SeritHareket = true;
                }
            }
            else
            {
                for (int i = 1; i < 7; i++)
                {
                    this.Controls.Find("labelSolSerit" + i.ToString(), true)[0].Top += 25;
                    //ilgili nesneyi bulması için yazdık ve ilgili neseneyi yukarıyta doğru hareketini ayarladık
                    this.Controls.Find("labelSagSerit" + i.ToString(), true)[0].Top += 25;
                    SeritHareket = false;
                }
            }

            labelRoad.Text = yol.ToString() + "m";
            labelSpeed.Text = hız.ToString() + "km/h";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        //oyun kontrol
        bool oyunKontrol = true;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (oyunKontrol == true) {
                oyunKontrol = false;
                timerRandomCar.Stop();
                timerSerit.Stop();
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                pictureBox2.Image = Properties.Resources.playicon1;
            }
            else if(oyunKontrol == false)
            {
                oyunKontrol = true;
                timerRandomCar.Start();
                timerSerit.Start();
                axWindowsMediaPlayer1.Ctlcontrols.play();
                pictureBox2.Image = Properties.Resources.pause_button_icon_123935;
            }

        }
    }
}
