using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emlak_Ziyaret
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataClasses1DataContext dc = new DataClasses1DataContext();
        Resimleme Resimleme = new Resimleme();

        private void Form1_Load(object sender, EventArgs e)
        {
            Baslangic();
        }
        public void Baslangic()
        {
            foreach(var deg in dc.Tablo_ilan)
            {
                UC_Ilanlar uc = new UC_Ilanlar();
                uc.lbl_ilan_tarihi.Text = deg.Ilan_Tarihi.Value.ToShortDateString();
                uc.lbl_konm.Text = deg.Konum;
                uc.label3.Text = deg.Ucret.Value.ToString()+ " ₺";
                uc.bunifuButton1.Tag = deg.E_Kayit;
                uc.pictureBox1.Image = Resimleme.ResimGetirme(deg.Ilan_Temel_Gorsel.ToArray());
                uc.bunifuButton1.Click += T1_Click;
                uc.Dock = DockStyle.Top;
                
                panel1.Controls.Add(uc);
            }
        }
        private void T1_Click(object sender,EventArgs e)
        {
            int index = UC_Ilanlar.S_ilan_No;
            var sor = dc.Tablo_ilan.First(x => x.E_Kayit == index);
            lbl_ilan_no.Text = sor.E_Kayit.ToString();
            lbl_ilantel.Text = sor.Ilan_Veren_Telefon;
            lbl_aidat.Text = sor.Aidat.Value.ToString();
            lbl_alan.Text = sor.Alan;
            lbl_balkon.Text = sor.Balkon;
            lbl_bina_yasi.Text = sor.Bina_Yasi;
            lbl_ilan_veren.Text = sor.Ilan_Veren;
            lbl_isitma.Text = sor.Isıtma;
            lbl_ilan_tarihi.Text = sor.Ilan_Tarihi.Value.ToShortDateString();
            lbl_kat_sayisi.Text = sor.Kat_Sayisi;
            Gorsel_Getir(sor.E_Kayit);

        }
        public void Gorsel_Getir(int index)
        {
            tileControl1.Groups.Clear();
            TileGroup egroup = new TileGroup() { Text = "Emlak" };

            foreach (var deg in dc.Tablo_Gorseller.Where(x => x.Ilan_No == index))
            {
                TileItem item = new TileItem();
                item.Elements.Add(new TileItemElement()
                {
                    Image = Resimleme.ResimGetirme(deg.Gorsel.ToArray()),
                    ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.Stretch
                });
                item.AppearanceItem.Normal.BackColor = Color.White;
                item.AppearanceItem.Normal.BorderColor = Color.White;
                item.Padding = new Padding(0, 0, 0, 0);
                item.Name = deg.f_Record.ToString();
                egroup.Items.Add(item);
                tileControl1.Groups.Add(egroup);
            }
        }

        private void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            if (tileControl1.SelectedItem.Name != null)
            {
                int Secili_Gorsel = Convert.ToInt32(tileControl1.SelectedItem.Name.ToString());
                var sor = dc.Tablo_Gorseller.First(x => x.f_Record == Secili_Gorsel);
                Sd_GorselGoster sd = new Sd_GorselGoster();
                sd.pictureBox1.Image = Resimleme.ResimGetirme(sor.Gorsel.ToArray());
                sd.ShowDialog();
            }
           
        }

    }
}
