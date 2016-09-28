using JogoBarbie.Dominio;
using JogoBarbie.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JogoBarbie
{
  public partial class MapaBarbie : Form
  {

    int[,] matrizG;

    public MapaBarbie()
    {
      InitializeComponent();
      var Desenha = new DesenhaMatriz();
      var matriz = Desenha.GeraMatriz();

      matrizG = matriz;

      int row, col;

      #region
      for (col = 0; col < 42; col++)
      {
        for (row = 0; row < 42; row++)
        {
          switch (matriz[row, col])
          {
            case 5:
              var pc = new PictureBox();
              pc.BackColor = Color.Green;
              tableLayoutPanel1.Controls.Add(pc, col, row);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 10:
              pc = new PictureBox();
              pc.BackColor = Color.LightGray;
              tableLayoutPanel1.Controls.Add(pc, col, row);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 00:
              pc = new PictureBox();
              pc.BackColor = Color.Orange;
              tableLayoutPanel1.Controls.Add(pc, col, row);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 01:
              pc = new PictureBox();
              pc.BackColor = Color.Gray;
              tableLayoutPanel1.Controls.Add(pc, col, row);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 15:
              pc = new PictureBox();
              pc.BackColor = Color.HotPink;
              tableLayoutPanel1.Controls.Add(pc, col, row);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            default:
              pc = new PictureBox();
              pc.BackColor = Color.Sienna;
              tableLayoutPanel1.Controls.Add(pc, col, row);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
          }
        }
      }
      #endregion

      //Pintar casa da Barbie
      var casaBarbie = new PictureBox
      {
        BackColor = Color.Red
      };
      tableLayoutPanel1.Controls.Add(casaBarbie, 23, 19);
      casaBarbie.Dock = DockStyle.Fill;
      casaBarbie.Margin = new Padding(1);
    }

    private void VerificarProximoMovimento()
    {
      //Orientação: Norte, Sul, Leste, Oeste

      var linha = 23;
      var coluna = 19;
      

      matrizG = new int[linha, coluna];
      var matrizY = new int[linha, coluna];
      var distancia = PegaDistancia(matrizG, matrizY);


      var casaBarbie = new PictureBox
      {
        BackColor = Color.Red
      };
      tableLayoutPanel1.Controls.Add(casaBarbie, 23, 19);
      casaBarbie.Dock = DockStyle.Fill;
      casaBarbie.Margin = new Padding(1);

    }

    private int PegaDistancia(int[,] matriz1, int[,] matriz2)
    {
      var matriz = 0;
      for (int i = 0; i < 1; i++)
      {
        for (int j = 0; j < 1; j++)
        {
          matriz = matriz1[i, j] - matriz2[i, j];
        }
      }
      return matriz;
    }

    private void btnInicial_Click(object sender, EventArgs e)
    {
      VerificarProximoMovimento();
    }
  }
}

