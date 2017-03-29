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
        double BilgiPDegeri=0;

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

            MessageBox.Show("Bilgi Degeri: "+BilgiPDegeri);
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
            int BolmeSiniri=0;

           
        
        Thread Thread1111 = new Thread(() => THFonksiyon1(haberorjinal,0,satirsayisi,BolmeSiniri));
        Thread1111.Start();
        }///button click sonu


        private void THFonksiyon1(int [,] matris,int Fx,int satirsayisi, int sinir)
        {

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
                else if (matris[sayac, Fx] > sinir)
                        { BSayi++;
                        if (matris[sayac, 3] == 1)
                            sBubir++;
                        else
                            sBuiki++;
                        }

               
                sayac++;
            }


            double BilgiX1= (KSayi/satirsayisi) * (-(SKubir/KSayi)*Math.Log(SKubir/KSayi,2))-((SKuiki/KSayi)*Math.Log((SKuiki/KSayi),2))
                +
                (BSayi / satirsayisi) * (-(sBubir / BSayi) * Math.Log(sBubir / BSayi, 2)) - ((sBuiki / BSayi) * Math.Log((sBuiki / BSayi), 2)) 
                ;


            MessageBox.Show("BilgiX1: "+ BilgiX1);


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
