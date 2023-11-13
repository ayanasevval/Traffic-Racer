using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic_Racer.NET
{
    class FakeCarClass
    {
        for(var i = 0; i<rndCar.Length; i++)
            {
                rndCar[i] = private new Random_Car();
    }
    
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
            }
    }
}
