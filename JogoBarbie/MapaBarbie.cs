using JogoBarbie.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace JogoBarbie
{
  public partial class MapaBarbie : Form
  {
    public MapaBarbie()
    {
      InitializeComponent();
      var Desenha = new DesenhaMatriz();
      var matriz = Desenha.GeraMatriz();

      int row, col;

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

      //Pintar casa da Barbie
      var casaBarbie = new PictureBox
      {
        BackColor = Color.Red
      };
      tableLayoutPanel1.Controls.Add(casaBarbie, 23, 19);
      casaBarbie.Dock = DockStyle.Fill;
      casaBarbie.Margin = new Padding(1);
    }

    private static void VerificarProximoMovimento(int[,] PosicaoAtual)
    {
      //Orientação: Norte, Sul, Leste, Oeste

      //Verificar posição norte

    }


    private void btnInicial_Click(object sender, EventArgs e)
    {

    }
  }
}

