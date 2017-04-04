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
using System.Drawing;
using System.Drawing.Drawing2D;


namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        double[] KullanilanOzellikler = new double[3];
        double BilgiPDegeri=0;
        //                    0  1   2  /3   4   5  / 6  7   8
        int[] THBolSinir = { 50, 60, 70, 62, 63, 64, 5, 10, 19 };
        int deneme=0;
        int YUZDEHATA = 3;

        int GloSayac = 0;
        int[] Dugumler = new int[100];

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
            MessageBox.Show("Button Click Bitti ");
        }//button click sonu

        int list2index = 0;
        public void THler(int[,] matris, int satirsayisi)
        {

            deneme = 0;
            double[] THreturn = new double[9];


            if (KullanilanOzellikler[0] == 0)
            {
                Thread T_0A = new Thread(() => THreturn[0] = THFonksiyon1(matris, 0, satirsayisi, THBolSinir[0], 0));
                Thread T_0B = new Thread(() => THreturn[1] = THFonksiyon1(matris, 0, satirsayisi, THBolSinir[1], 1));
                Thread T_0C = new Thread(() => THreturn[2] = THFonksiyon1(matris, 0, satirsayisi, THBolSinir[2], 2));
                T_0A.Start();
                T_0B.Start();
                T_0C.Start();
            }
            else
            { deneme += 3; }


            if (KullanilanOzellikler[1] == 0)
            {
                Thread T_1A = new Thread(() => THreturn[3] = THFonksiyon1(matris, 1, satirsayisi, THBolSinir[3], 3));
                Thread T_1B = new Thread(() => THreturn[4] = THFonksiyon1(matris, 1, satirsayisi, THBolSinir[4], 4));
                Thread T_1C = new Thread(() => THreturn[5] = THFonksiyon1(matris, 1, satirsayisi, THBolSinir[5], 5));
                T_1A.Start();
                T_1B.Start();
                T_1C.Start();
            }else
             { deneme += 3; }

            if (KullanilanOzellikler[2] == 0)
            {
                Thread T_2A = new Thread(() => THreturn[6] = THFonksiyon1(matris, 2, satirsayisi, THBolSinir[6], 6));
                Thread T_2B = new Thread(() => THreturn[7] = THFonksiyon1(matris, 2, satirsayisi, THBolSinir[7], 7));
                Thread T_2C = new Thread(() => THreturn[8] = THFonksiyon1(matris, 2, satirsayisi, THBolSinir[8], 8));
                T_2A.Start();
                T_2B.Start();
                T_2C.Start();
            }else
            { deneme += 3; }
            
           
           

             while (deneme < 9)
            {
                 /**threadler bittiğinde bu döngüdem kurtulur, bundan sonra bütün kazançlar hesaplanmış olur*/ 
            }
            

            //threadlerin hesapladığı kazançların en büyük olanını seç
             int EniyiKazancIndis = EniyiHesapla(THreturn);
             int EniyiOzellikNo=EniyiKazancIndis/3;
            
            int bolmesiniri = THBolSinir[EniyiKazancIndis];

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


            double SolAltEntropi = SinirEntropiHesap(SolAltDizi, SolAltDizi.GetUpperBound(0) + 1);
            double SagAltEntropi = SinirEntropiHesap(SagAltDizi, SagAltDizi.GetUpperBound(0) + 1);

            
            if(SolAltEntropi==-1  && SolAltDizi.GetUpperBound(0)+1>1 )//homojen degilse
            {
                if(KullanilanOzellikler[0]==0 ||KullanilanOzellikler[1]==0 || KullanilanOzellikler[2]==0 )
                {
                    KullanilanOzellikler[EniyiOzellikNo] = 1;//agaca eklenen ozelligi 1 
                   if(list2index<10000){
                       listBox2.Items.Add(EniyiOzellikNo).ToString();
                       listBox2.Items.Add(bolmesiniri).ToString();
                       list2index++;
                       Dugumler[GloSayac] = EniyiOzellikNo;
                       GloSayac++;
                       Dugumler[GloSayac] = bolmesiniri;
                       GloSayac++;
                   }
                    //Recursive
                    THler(SolAltDizi, (SolAltDizi.GetUpperBound(0) + 1));
                    listBox1.Items.Add("sol_" + (SolAltDizi.GetUpperBound(0) + 1) + "");
                    KullanilanOzellikler[EniyiOzellikNo] = 0;
                }
            }


            if (SagAltEntropi == -1 && SagAltDizi.GetUpperBound(0) + 1 > 1)//homojen degilse
            {
                if (KullanilanOzellikler[0] == 0 || KullanilanOzellikler[1] == 0 || KullanilanOzellikler[2] == 0)
                {
                    KullanilanOzellikler[EniyiOzellikNo] = 1;//agaca eklenen ozelligi 1 yapar
                    THler(SagAltDizi, (SagAltDizi.GetUpperBound(0) + 1));
                    if (list2index < 10000)
                    {
                        listBox2.Items.Add(EniyiOzellikNo).ToString();
                        listBox2.Items.Add(bolmesiniri).ToString();
                        list2index++;
                        Dugumler[GloSayac] = EniyiOzellikNo;
                        GloSayac++;
                        Dugumler[GloSayac] = bolmesiniri;
                        GloSayac++;
                    }
                    listBox1.Items.Add("_sag" + (SagAltDizi.GetUpperBound(0) + 1) + "");
                    KullanilanOzellikler[EniyiOzellikNo] = 0;
                }
            }
            


            

            

            //sol alta geçmek için
            //kabul koşulunu sağlamalı-sol dizisi boş olmamalı-
            //bütün özellikler kullanılmış olmamalı-
            //butun ozellikler bittiyse REC çalıştırma






          // THler(SolAltDizi, (SolAltDizi.GetUpperBound(0) + 1));
                                  
               

         
           



           // MessageBox.Show("bittiler");
        }


        private double SinirEntropiHesap(int[,] matris, int satirsayisi)
        {
            double birSayi = 0;
            double ikiSayi = 0;

            int aaa = 0;
            while (aaa < satirsayisi)
            {
                if (matris[aaa, 3] == 1)
                { birSayi++; }
                else if (matris[aaa, 3] == 2)
                { ikiSayi++; }
                else
                { MessageBox.Show("hata..sinirhesapla Fonksiyonunda"); }

                aaa++;
            }
            //bir veya iki sayisi sınırdan fazla
           if(birSayi >ikiSayi)
           {
               if (((ikiSayi / satirsayisi) * 100) >= YUZDEHATA)
               { return -1; }
               else
               { return 1; }
           }else
            {
                if (((birSayi / satirsayisi) * 100) >= YUZDEHATA)
                { return -1; }
                else
                { return 2; }
            }

        }


        private int EniyiHesapla(double [] SinirGain)
        {
            int Minindex = 0;
            double MaxDeger =99999;

            for (int aa = 0;aa<SinirGain.GetUpperBound(0)+1; aa++ )
            {
                if (SinirGain[aa] < MaxDeger && SinirGain[aa] != 0)
                {
                    MaxDeger = SinirGain[aa];
                    Minindex = aa;
                }

            }

            return Minindex;
        }
   
        private double THFonksiyon1(int [,] matris,int Fx,int satirsayisi, int sinir,int thid)
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
                    { SKubir++; }
                    else
                    { SKuiki++; }
                }
                else if (matris[sayac, Fx] >= sinir)
                        { BSayi++;
                         if (matris[sayac, 3] == 1)
                             { sBubir++; }
                            else
                             { sBuiki++; }
                        }

                sayac++;
            }
           double BilgiX1= 
                ((KSayi/satirsayisi) * (((-(SKubir/KSayi))*(Math.Log((SKubir/KSayi),2)))-(((SKuiki/KSayi)*(Math.Log((SKuiki/KSayi),2))))))
                +
                ((BSayi/satirsayisi) * (((-(sBubir/BSayi))*Math.Log((sBubir/BSayi), 2)) - (((sBuiki/BSayi)*Math.Log((sBuiki/BSayi), 2)))));
           

          

            deneme++;

            return BilgiX1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //Ağaç Çizme Button
        private void agacCiz_Click(object sender, EventArgs e)
        {
            int a=0;
            int BasX = pictureBox1.Width, BasY = 20; ;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);

            //g.DrawRectangle(new Pen(Color.Black, 3), pictureBox1.Width-40, 0, pictureBox1.Width + 40, 40);
           // g.DrawRectangle(new Pen(Color.Black, 1), pictureBox1.Width/2,40,40,40);
            //                                   ,  X  ,yuksek,genişlik , yuksek ,);  
            g.DrawEllipse(new Pen(Color.Black, 1), BasX/2, BasY, 70, 40);
            using (Font myFont = new Font("Arial", 10)) { g.DrawString("f "+Dugumler[0].ToString(), myFont, Brushes.Green, new Point(BasX/2+20, BasY+15)); }
            using (Font myFont = new Font("Arial", 10)) { g.DrawString("__"+Dugumler[1].ToString(), myFont, Brushes.Green, new Point(BasX / 2 + 40, BasY + 15)); }



            int durum = 0;
            int tekrar = 0;
            
            int mevcutX=BasX /2-90;
            int mevcutY = BasY + 80;
            ///cizdirme işlemi 4.04.2017_03.58
            for(int ii=2; ii<GloSayac;ii++)
            {
                 if(Dugumler[ii] != Dugumler[ii+2])
                 {//sol çizgi
                     g.DrawLine(new Pen(Color.Red, 2), BasX/2+35, BasY+40, BasX/2-60, BasY+80);

                     g.DrawEllipse(new Pen(Color.Black, 1), mevcutX , mevcutY, 70, 40);
                     using (Font myFont = new Font("Arial", 10)) { g.DrawString("f "+Dugumler[ii], myFont, Brushes.Green, new Point(mevcutX,mevcutY+15)); }
                     using (Font myFont = new Font("Arial", 10)) { g.DrawString("__" + Dugumler[ii+1].ToString(), myFont, Brushes.Green, new Point(mevcutX+30, mevcutY+15)); }


                 }


            }



            
            
            
            
            
            pictureBox1.Image = bmp;
        }




     


    }
}
