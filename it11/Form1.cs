using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace it11
{
  public partial class Form1 : Form
  {  //Объявляем переменные доступные в каждом обработчике события
    private Point PreviousPoint, point; //Точка до перемещения курсора мыши и текущая точка
    private Bitmap bmp;
    private Pen blackPen;
    private Graphics g;

    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      blackPen = new Pen(Color.Black, 4); //подготавливаем перо для рисования
    }

    private void button1_Click(object sender, EventArgs e)
    {  //открытиефайла
      OpenFileDialog dialog = new OpenFileDialog(); //описываемипорождаемобъект dialog классаOpenFileDialog
                                                    //задаемрасширенияфайлов
      dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
      if (dialog.ShowDialog() == DialogResult.OK)//вызываем диалоговое окно и проверяем выбран ли файл
      {
        Image image = Image.FromFile(dialog.FileName); //Загружаем в image изображение из выбранного файла
        int width = image.Width;
        int height = image.Height;
        pictureBox1.Width = width;
        pictureBox1.Height = height;

        bmp = new Bitmap(image, width, height); //создаемизагружаемиз image изображениевформате bmp

        pictureBox1.Image = bmp; //записываем изображение в формате bmp в pictureBox1
        g = Graphics.FromImage(pictureBox1.Image); //подготавливаем объект Graphics для рисования в pictureBox1

      }
      button5.Enabled = true;
      button4.Enabled = true;
      button3.Enabled = true;
    }

    private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
    { // обработчик события нажатия кнопки на мыши
      // записываем в предыдущую точку (PreviousPoint) текущие координаты
      PreviousPoint.X = e.X;
      PreviousPoint.Y = e.Y;
    }


    private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
    {//Обработчик события перемещения мыши по pictuteBox1
      if (e.Button == MouseButtons.Left) //Проверяем нажата ли левая кнопка мыши
      {  //запоминаем в point текущее положение курсора мыши
        point.X = e.X;
        point.Y = e.Y;

        //соеденяем линией предыдущую точку с текущей
        g.DrawLine(blackPen, PreviousPoint, point);

        //текущее положение курсора мыши сохраняем в PreviousPoint
        PreviousPoint.X = point.X;
        PreviousPoint.Y = point.Y;
        pictureBox1.Invalidate();//Принудительновызываемпереррисовку pictureBox1
      }
    }
    private void button3_Click(object sender, EventArgs e)
    {
       //циклы для перебора всех пикселей на изображении
        for (int i = 0; i < bmp.Width; i++)
          for (int j = 0; j < bmp.Height; j++)
          {
            int R = bmp.GetPixel(i, j).R; //извлекаем в R значение красного цвета в текущей точке
           
           
            Color p = Color.FromArgb(255,R,0,0); //переводим int в значение цвета. 255 - показывает степень прозрачности. остальные значения одинаковы для трех каналов R,G,B
            bmp.SetPixel(i, j, p); //записываме полученный цвет в текущую точку
          }
        Refresh(); //вызываем функцию перерисовки окна
      button5.Enabled = false;
      button4.Enabled = false;
      button3.Enabled = false;

    }

    private void button4_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < bmp.Width; i++)
        for (int j = 0; j < bmp.Height; j++)
        {
          int G = bmp.GetPixel(i, j).G; //извлекаем в R значение красного цвета в текущей точке


          Color p = Color.FromArgb(255, 0, G, 0); //переводим int в значение цвета. 255 - показывает степень прозрачности. остальные значения одинаковы для трех каналов R,G,B
          bmp.SetPixel(i, j, p); //записываме полученный цвет в текущую точку
        }
      Refresh(); //вызываем функцию перерисовки окна
      button5.Enabled = false;
      button4.Enabled = false;
      button3.Enabled = false;
    }

    private void button5_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < bmp.Width; i++)
        for (int j = 0; j < bmp.Height; j++)
        {
          int B = bmp.GetPixel(i, j).B; //извлекаем в R значение красного цвета в текущей точке


          Color p = Color.FromArgb(255, 0, 0, B); //переводим int в значение цвета. 255 - показывает степень прозрачности. остальные значения одинаковы для трех каналов R,G,B
          bmp.SetPixel(i, j, p); //записываме полученный цвет в текущую точку
        }
      Refresh(); //вызываем функцию перерисовки окна
      button5.Enabled = false;
      button4.Enabled = false;
      button3.Enabled = false;
    }

    private void button2_Click(object sender, EventArgs e)
    { //сохранениефайла
      SaveFileDialog savedialog = new SaveFileDialog();//описываемипорождаемобъектsavedialog
                                                       //задаем свойства для savedialog
      savedialog.Title = "Сохранить картинку как ...";
      savedialog.OverwritePrompt = true;
      savedialog.CheckPathExists = true;
      savedialog.Filter =
      "Bitmap File(*.bmp)|*.bmp|" +
      "GIF File(*.gif)|*.gif|" +
      "JPEG File(*.jpg)|*.jpg|" +
      "TIF File(*.tif)|*.tif|" +
      "PNG File(*.png)|*.png";
      savedialog.ShowHelp = true;
      // If selected, save
      if (savedialog.ShowDialog() == DialogResult.OK)//вызываемдиалоговоеокноипроверяемзаданолиимяфайла
      {
        // в строку fileName записываем указанный в savedialog полный путь к файлу
        string fileName = savedialog.FileName;
        // Убираем из имени три последних символа (расширение файла)
        string strFilExtn =
        fileName.Remove(0, fileName.Length - 3);
        // Сохраняем файл в нужном формате и с нужным расширением
        switch (strFilExtn)
        {
          case "bmp":
            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
            break;
          case "jpg":
            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            break;
          case "gif":
            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif);
            break;
          case "tif":
            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff);
            break;
          case "png":
            bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            break;
          default:
            break;
        }
      }
    }


  }
}