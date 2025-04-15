// 2. Mājas/Ieskaites darbs, Anita Līva

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafiskais_redaktors_md2
{
    public partial class Form1 : Form
    {
        enum ToolType { Pen, Line, Rectangle, Ellipse, Eraser } // riki kas bus programai
        ToolType currentTool = ToolType.Pen; //sak ar zimuli

        // sakuma izveles, vajag grafiku, canvas, zimuli un krasu uc., sakuma aizildijums 
        Graphics graphics;
        Bitmap canvas;
        Pen pen = new Pen(Color.Black, 2);
        Point startPoint, endPoint;
        bool drawing = false;
        bool fillShapes = false; // sakuma nav aizpildijuma 
        Color fillColor = Color.White; // aizpildijuma krasa ja nospiez

        // forma
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // buferis lai labak zimetu
            this.Load += new System.EventHandler(Form1_Load); // kas notiek kad atvera forma1
            this.Resize += new System.EventHandler(Form1_Resize); // var parveidot attela izmerus, kad maina formas izmerus

        }


        // kas notiks, kad atvera forma1 - attelam vares mainit izmeru, izveido jaunu canva un grafiku, sakuma visu notiris no canvas 
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(canvas);
            graphics.Clear(Color.White);
            pictureBox1.Image = canvas;
        }

        //pen - zimulis
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Pen;
        }

        //line - linija
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Line;
        }

        //rectangle - taisnsturis
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Rectangle;
        }

        //ellipse - elipses riks
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Ellipse;
        }

        //eraser - dzesgumiaj
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            currentTool = ToolType.Eraser;
        }
        //color dialog - krasu izvele no vairakam jau dotam krasam
        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = pen.Color; // pasreizeja krasa, kas jau bija
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    pen.Color = colorDialog.Color; // izvelas jaunu krasu uzliek jauno
                }
            }
        }
        //fill - aizpildijums
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            fillShapes = !fillShapes;

            //vares izveleties aizpildijuma krasa un nomainit
            if (fillShapes)
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    colorDialog.Color = fillColor;
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        fillColor = colorDialog.Color;
                    }
                }
            }
        }


        //new - jauns attels
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySound();
            graphics.Clear(Color.White); // notira visu un uzliek baltu fonu
            pictureBox1.Invalidate(); // parada jauno zimejumu uz ekrana
        }
        //open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySound();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName); // ver vala failu
                graphics.DrawImage(img, new Rectangle(0, 0, canvas.Width, canvas.Height)); //maina izmeru ja grib atvert par lielu
                pictureBox1.Invalidate(); // parada jauno zimejumu uz ekrana
            }
        }
        //save - saglaba failu ar doto nosaukumu
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySound();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                canvas.Save(saveFileDialog1.FileName);
        }
        //print - izauc print funkc lai izprintetu visu kas ir canvas
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySound();
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(myPrinting);
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }
        //printing - panem visu canvas saturu
        private void myPrinting(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(canvas, e.MarginBounds);
        }
        //sound - veido skanu beep
        private void PlaySound()
        {
            System.Media.SystemSounds.Beep.Play();
        }



        //picture box1 - mouse down, mouse move, mouse up - peles klikski
        //kad piespiez peli 
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // ja pele piespiesta tad zime 
            drawing = true;
            startPoint = e.Location;
            if (currentTool == ToolType.Pen) // zimulim sakuma sakums un beigas viena punkta pectam atajauninas beigu punktu
                endPoint = e.Location;
        }
        //kad kustina peli
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // ja pele piespiesta tad zime un no sakuma uz beigam
            if (drawing)
            {
                if (currentTool == ToolType.Pen) // ja zimulis 
                {
                    //zime no iepriekseja punkta lidz jaunajam
                    graphics.DrawLine(pen, endPoint, e.Location);
                    // beigu punkts paliek par sakumu nakamajai linijai
                    endPoint = e.Location;
                    pictureBox1.Invalidate(); // parada atjauninajumu pec katras linijas
                }
            }
        }
        //kad atlaiz peli
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false; // ja beidz zimet un pele atlaista
            endPoint = e.Location; //beigas ir atlaisanas punkts

            //taisnsturim vajag sakumu un beigu puntku
            Rectangle rect = GetRectangle(startPoint, endPoint);
            Brush fillBrush = new SolidBrush(fillColor); // izveido krasas otu aipildijumam

            switch (currentTool)
            {
                //liniju zime no sakuma punkta lidz beigu punktam
                case ToolType.Line:
                    graphics.DrawLine(pen, startPoint, endPoint);
                    break;
                // taisnsturi zime 
                case ToolType.Rectangle:
                    if (fillShapes) //ja aizpildijums aizpildis
                        graphics.FillRectangle(fillBrush, rect);
                    graphics.DrawRectangle(pen, rect); //areja  kontura taisnsturim
                    break;
                //elipse
                case ToolType.Ellipse:
                    //zime ja aizpildits
                    if (fillShapes)
                        graphics.FillEllipse(fillBrush, rect);
                    //kontura elipsei
                    graphics.DrawEllipse(pen, rect);
                    break;
                //dzesgumija zime ar baltu taisnsturi
                case ToolType.Eraser:
                    graphics.FillRectangle(Brushes.White, rect);
                    break;
            }

            fillBrush.Dispose(); // nonem otu
            pictureBox1.Invalidate(); //parada jauno zimejumu
        }


        private Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(
                Math.Min(p1.X, p2.X), // kurs mazakais x? to panem, lai zimetu no mazakajiem punktiem uz lielakajiem
                Math.Min(p1.Y, p2.Y), // kurs mazakais y? to panem, lai zimetu no mazakajiem punktiem uz lielakajiem
                Math.Abs(p1.X - p2.X), // platums
                Math.Abs(p1.Y - p2.Y)); //augstums
        }

        //resize - var mainit izmeru formai un canvas mainas
        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeCanvas();
        }
        //help - info par projektu
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Šis ir grafiskais redaktors MyPaint 2025, Anita Līva", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        // mainit attela canvas izmeru
        private void ResizeCanvas()
        {
            //vai ir platums augstums, tad var zimet tiasnsturi
            if (pictureBox1.Width > 0 && pictureBox1.Height > 0)
            {
                //bitmap ar picturebox1 izmeriem
                Bitmap newCanvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                // grafika lai zimetu uz canvas
                Graphics newGraphics = Graphics.FromImage(newCanvas);

                //notira canvas - viss balts
                newGraphics.Clear(Color.White);
                //zime veco atelu uz ajuniem canvas ar citiem izmeriem
                newGraphics.DrawImage(canvas, new Rectangle(0, 0, newCanvas.Width, newCanvas.Height));

                // jaunas canvas  grafika un picturebox ir ieksa jauna canvas
                canvas = newCanvas;
                graphics = newGraphics;
                pictureBox1.Image = canvas;
            }
        }

    }
}
