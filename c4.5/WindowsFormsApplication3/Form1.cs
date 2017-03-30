using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;


namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        double[] KullanilanOzellikler = new double[3];
        double BilgiPDegeri=0;
        double [] THREADDONUSLERI = new double[9];
        int[] THBolSinir = { 50, 60, 70, 62, 63, 64, 5, 10, 19 };
        int deneme=0;
        int THBOSDONGU = 9;
        public Form1()
        {
            InitializeComponent();
        }

        public void BilgiHesapla(int [,]hborjinal,int satirsayisi)
        {
            double birSayi=0;
            double ikiSayi=0;
            
            int aaa=0;
            while(aaa < satirsayisi)
            {
                if(hborjinal[aaa,3] == 1)
                {birSayi++;}
                else if(hborjinal[aaa,3]==2)
                {ikiSayi++;}
                else
                {MessageBox.Show("hata..BilgiHesapla Fonksiyonunda");}
                
             aaa++;
            }


            BilgiPDegeri = -(birSayi / satirsayisi)*(Math.Log((birSayi /satirsayisi), 2));
            BilgiPDegeri = BilgiPDegeri -(ikiSayi / satirsayisi) * (Math.Log((ikiSayi) / (satirsayisi), 2));

           // MessageBox.Show("Bilgi Degeri: "+BilgiPDegeri);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int satirsayisi = 0;
            StreamReader sr;
            sr = new StreamReader("haberman.txt");
            satirsayisi = File.ReadLines("haberman.txt").Count();

            int[,] haberorjinal;
            haberorjinal = new int[satirsayisi, 4];

            for (int sayac = 0; sayac < satirsayisi; sayac++)
            {
                String OkunanDeger = sr.ReadLine();
                String[] Parcalanmis = OkunanDeger.Split(',');//okunan satırı virgullere göre parçalıyor

                haberorjinal[sayac, 0] = Convert.ToInt32(Parcalanmis[0]);
                haberorjinal[sayac, 1] = Convert.ToInt32(Parcalanmis[1]);
                haberorjinal[sayac, 2] = Convert.ToInt32(Parcalanmis[2]);
                haberorjinal[sayac, 3] = Convert.ToInt32(Parcalanmis[3]);
            }


            BilgiHesapla(haberorjinal,satirsayisi);

            THler(haberorjinal, satirsayisi);
           
        }//button click sonu


        public void THler(int[,] matris, int satirsayisi)
        {
            if (KullanilanOzellikler[0] == 0)
            {
                Thread T_0A = new Thread(() => THFonksiyon1(matris, 0, satirsayisi, THBolSinir[0], 0));
                Thread T_0B = new Thread(() => THFonksiyon1(matris, 0, satirsayisi, THBolSinir[1], 1));
                Thread T_0C = new Thread(() => THFonksiyon1(matris, 0, satirsayisi, THBolSinir[2], 2));
                T_0A.Start();
                T_0B.Start();
                T_0C.Start();
            }

            if (KullanilanOzellikler[1] == 0)
            {
                Thread T_1A = new Thread(() => THFonksiyon1(matris, 1, satirsayisi, THBolSinir[3], 3));
                Thread T_1B = new Thread(() => THFonksiyon1(matris, 1, satirsayisi, THBolSinir[4], 4));
                Thread T_1C = new Thread(() => THFonksiyon1(matris, 1, satirsayisi, THBolSinir[5], 5));
                T_1A.Start();
                T_1B.Start();
                T_1C.Start();
            }
            if (KullanilanOzellikler[2] == 0)
            {
                Thread T_2A = new Thread(() => THFonksiyon1(matris, 2, satirsayisi, THBolSinir[6], 6));
                Thread T_2B = new Thread(() => THFonksiyon1(matris, 2, satirsayisi, THBolSinir[7], 7));
                Thread T_2C = new Thread(() => THFonksiyon1(matris, 2, satirsayisi, THBolSinir[8], 8));
                T_2A.Start();
                T_2B.Start();
                T_2C.Start();
            }
            
           
            
          
            
           

            while (deneme < THBOSDONGU)
            { /**threadler bittiğinde bu döngüdem kurtulur, bundan sonra bütün kazançlar hesaplanmış olur*/ }
           
            
            //threadlerin hesapladığı kazançların en büyük olanını seç
             int EniyiKazancIndis=EniyiKazancBul();
            int EniyiOzellikNo=EniyiKazancIndis/3;
             KullanilanOzellikler[EniyiOzellikNo] = 1;//agaca eklenen ozelligi 1 yapar

            /**Dizileri böl */
             int SinKucukAdet = 0, SinBuyukAdet = 0;
            
            for(int i=0;i<satirsayisi;i++)
            {
                if (matris[i, EniyiOzellikNo] < THBolSinir[EniyiKazancIndis])
                {
                    SinKucukAdet++;
                }
                else
                { SinBuyukAdet++; }
            }

            int[,] SolAltDizi=new int[SinKucukAdet,4];
            int[,] SagAltDizi = new int[SinBuyukAdet,4];
            int sol=0,sag=0;
            for(int i=0;i<satirsayisi;i++)
            {
                if (matris[i, EniyiOzellikNo] < THBolSinir[EniyiKazancIndis])
                {
                    SolAltDizi[sol, 0] = matris[i, 0];
                    SolAltDizi[sol, 1] = matris[i, 1];
                    SolAltDizi[sol, 2] = matris[i, 2];
                    SolAltDizi[sol, 3] = matris[i, 3];
                    sol++;
                }
                else
                {
                    SagAltDizi[sag, 0] = matris[i, 0];
                    SagAltDizi[sag, 1] = matris[i, 1];
                    SagAltDizi[sag, 2] = matris[i, 2];
                    SagAltDizi[sag, 3] = matris[i, 3];
                    sag++;
                }
            }
           
           
            /**homojenlik kontrol ekle*/
            int tmp = 0;
            for(int aa=0; aa<(SolAltDizi.GetUpperBound(0)+1) ; aa++ )
                {
                    if(SolAltDizi[aa,3]==1)
                    { tmp++; }
                        else
                         {tmp--;}

                }

            if ((Math.Abs(tmp)) == (SolAltDizi.GetUpperBound(0) + 1))
            {
                MessageBox.Show("Sol Homojen");
            }
            else
                { 
                MessageBox.Show("Sol Homojen değil");
               // deneme = 0;
                //THBOSDONGU -=3;
                //THler(SolAltDizi, (SolAltDizi.GetUpperBound(0) + 1));
            
                }

            //sag dizi
            int tmp2 = 0;
            for (int aa = 0; aa < (SagAltDizi.GetUpperBound(0) + 1); aa++)
            {
                if (SagAltDizi[aa, 3] == 1)
                { tmp2++; }
                else
                { tmp2--; }

            }
            if ((Math.Abs(tmp2)) == (SagAltDizi.GetUpperBound(0) + 1))
            {
                MessageBox.Show("Sag Homojen");
            }
            else
            { MessageBox.Show("Sag Homojen değil");
           

            }



            MessageBox.Show("bittiler");
        }

        private int EniyiKazancBul()
        {
            int Eniyiindis = 0;
            int sayac = 0;
            double temp = 999999;
            for (sayac = 0; sayac < THREADDONUSLERI.GetUpperBound(0)+1; sayac++)
            {
                if (THREADDONUSLERI[sayac] < temp && THREADDONUSLERI[sayac] != 0 && KullanilanOzellikler[sayac/3]==0 )
                {
                    temp = THREADDONUSLERI[sayac];
                    Eniyiindis = sayac;
                }
            }
               
            return Eniyiindis;  
        }


        private void THFonksiyon1(int [,] matris,int Fx,int satirsayisi, int sinir,int thid)
        {

//            MessageBox.Show(""+thid+"basladi");

            double KSayi=0,SKubir=0,SKuiki=0;
            double BSayi = 0, sBubir = 0, sBuiki=0;

            int sayac = 0;
            while (sayac < satirsayisi)
            {
                if (matris[sayac, Fx] < sinir)
                {   KSayi++;
                    if (matris[sayac, 3] == 1)
                        SKubir++;
                    else
                        SKuiki++;
                }
                else if (matris[sayac, Fx] >= sinir)
                        { BSayi++;
                        if (matris[sayac, 3] == 1)
                            sBubir++;
                        else
                            sBuiki++;
                        }

                sayac++;
            }
            double BilgiX1= (KSayi/satirsayisi) * (((-(SKubir/KSayi))*Math.Log((SKubir/KSayi),2))-(((SKuiki/KSayi)*Math.Log((SKuiki/KSayi),2))))
                +
                (BSayi / satirsayisi) * (((-(sBubir / BSayi)) * Math.Log((sBubir / BSayi), 2)) - (((sBuiki / BSayi) * Math.Log((sBuiki / BSayi), 2))));

            THREADDONUSLERI[thid] = BilgiX1;

            deneme++;

            //MessageBox.Show("" + thid + "bitti");

        }




        /** private void YEDEK(string param1, int param2)
        {
            int ii = 0;

            for (ii = 0; ii < 1000000000; ii++)
                {
                    if (ii % 13128777 == 0)
                    {
                        Invoke(new MethodInvoker(
                        delegate { listBox1.Items.Add(param1 + "__" + param2); }
                        ));
                        Thread.Sleep(100);
                    }

                }


        }*/


    }
}
