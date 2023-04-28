using EsiniBul.Properties;

namespace EsiniBul
{
    public partial class Form1 : Form
    {


        int boyut = 4;
        int w = 96;
        List<int> resimler; //= new List<int>();
        List<PictureBox> aciklar; //= new List<PictureBox>();
        int gizlenenAdet; //= 0;

        public Form1()
        {
            InitializeComponent();
            //PictureBox pb = new PictureBox();
            //pb.Size = new Size(64, 64);
            //pb.Location = new Point(64, 64);
            //pb.SizeMode = PictureBoxSizeMode.Zoom;  
            //pb.Image = Resim(4);
            //pnlKartlar.Controls.Add(pb);

            //ResimleriBelirle();
            //KartlariOlustur();
            YeniOyunHazirla();

        }

        private void ResimleriBelirle()
        {
            int toplamKartAdet = boyut * boyut;
            int resimCesitAdet = toplamKartAdet / 2;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j <= resimCesitAdet; j++)
                {
                    resimler.Add(j);
                }
            }
            ListeKaristir(resimler);
        }

        private void ListeKaristir(List<int> resimler)
        {
            Random rnd = new Random();
            int temp, talihliIndeks;

            for (int i = 0; i < resimler.Count - 1; i++)
            {
                talihliIndeks = rnd.Next(i, resimler.Count);
                temp = resimler[i];
                resimler[i] = resimler[talihliIndeks];
                resimler[talihliIndeks] = temp;
            }

        }

        private void KartlariOlustur()
        {
            int no = 0;
            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < boyut; j++)
                {
                    PictureBox pb = new PictureBox();
                    pb.Size = new Size(w, w);
                    pb.Location = new Point(j * w, i * w);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Image = Resim(0);
                    pb.Tag = no;
                    pb.Click += Pb_Click;
                    pnlKartlar.Controls.Add(pb);
                    no++;
                }
            }
            pnlKartlar.Size = new Size(boyut * w, boyut * w);
            ClientSize = new Size(boyut * w + 12, boyut * w + 12);
        }

        private void Pb_Click(object? sender, EventArgs e)
        {
            PictureBox tiklanan = (PictureBox)sender;
            int tiklananNo = (int)tiklanan.Tag;

            // eger tiklanan kart zaten aciksa hicbir sey yapmadan cik
            if (aciklar.Contains(tiklanan))
            {
                return;
            }

            // eger zaten 2 acik varsa once onlari kapat
            if (aciklar.Count == 2)
            {
                AciklariKapat();

            }
            aciklar.Add(tiklanan);
            int oncekiNo = (int)aciklar[0].Tag;
            tiklanan.Image = Resim(resimler[tiklananNo]);

            // eger acik kart sayisi 2 ise ve bunlar ayni ise gizle
            if (aciklar.Count == 2 && resimler[oncekiNo] == resimler[tiklananNo])
            {
                Update();
                Thread.Sleep(200);
                AciklariGizle();

            }

            // eger tum kartlar gizlendiyse o zaman oyunu bitir

            if (gizlenenAdet == resimler.Count)
            {
                DialogResult cevap = MessageBox.Show("Oyun bitti tekrar oynamak istermisiniz", "tekrar oyna?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (cevap == DialogResult.Yes)
                {
                    YeniOyunHazirla();
                }
                else
                {
                    Close();
                }

            }

            //MessageBox.Show(tiklananNo.ToString());
        }

        private void YeniOyunHazirla()
        {
            pnlKartlar.Controls.Clear();
            resimler = new List<int>();
            aciklar = new List<PictureBox>();
            gizlenenAdet = 0;
            ResimleriBelirle();
            KartlariOlustur();

        }

        private void AciklariGizle()
        {
            gizlenenAdet += 2;
            foreach (var pb in aciklar)
            {
                pb.Hide();
            }
            aciklar.Clear();
        }

        private void AciklariKapat()
        {
            foreach (var pb in aciklar)
            {
                pb.Image = Resim(0);
            }
            aciklar.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        Bitmap Resim(int no)
        {
            return (Bitmap)Resources.ResourceManager.GetObject(no.ToString());
        }
    }
}
//}

//    }
//}