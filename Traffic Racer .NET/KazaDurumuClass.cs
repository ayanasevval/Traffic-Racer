using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Traffic_Racer.NET
{
    class KazaDurumuClass
    {

        private void timerRandomCar_Tick(object sender, EventArgs e)
        {
            int yol=0;
            int hız=70;
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

                    if (yol > Settings1.Default.HighScore)
                    {

                        MessageBox.Show("Yeni Yüksek Skor: " + yol.ToString() + "m", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Settings1.Default.HighScore = yol;
                        Settings1.Default.Save();


                    }
                    axWindowsMediaPlayer1.Ctlcontrols.play();

                    //kaza yaptıktan sonra uyarı vererek tekrar oynamak ister misin diye sorgulama döngüsü
                    DialogResult dr = MessageBox.Show("Oyun Bitti! Yeniden denemek ister misiniz? ", "Uyarı!", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {

                        AracYerine();

                        for (int j = 0; j < rndCar.Length; j++)
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
                        timerSerit.Interval = 200;

                        RandomMusicEkle();
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                        pictureBox1.Image = Properties.Resources.volumeON;

                        labelHighScore.Text = Settings1.Default.HighScore.ToString();

                    }
                    else
                    {
                        this.Close();
                    }
                }

            }
        }
    }
}
