using JogoBarbie.Dominio.Implementation;
using JogoBarbie.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JogoBarbie
{
  public partial class MapaBarbie : Form
  {
    List<Tuple<int, int>> JaPassou = new List<Tuple<int, int>>();
    int[,] MatrizG;
    List<Amigo> Amigos;
    int Linha = 23;
    int Coluna = 19;

    public MapaBarbie()
    {
      InitializeComponent();
      var Desenha = new DesenhaMatriz();
      var matriz = Desenha.GeraMatriz();
      MatrizG = matriz;

      var gerarAmigos = new Amigo();
      Amigos = gerarAmigos.Amigos();

      int row, col, i = 0;

      #region
      for (col = 1; col < 42; col++)
      {
        for (row = 1; row < 42; row++)
        {

          switch (matriz[col, row])
          {
            case 5:
              var pc = new PictureBox();
              pc.BackColor = Color.Green;
              tableLayoutPanel1.Controls.Add(pc, row, col);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 10:
              pc = new PictureBox();
              pc.BackColor = Color.LightGray;
              tableLayoutPanel1.Controls.Add(pc, row, col);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 00:
              pc = new PictureBox();
              pc.BackColor = Color.Orange;
              tableLayoutPanel1.Controls.Add(pc, row, col);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 01:
              pc = new PictureBox();
              pc.BackColor = Color.Gray;
              tableLayoutPanel1.Controls.Add(pc, row, col);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              break;
            case 15:
              pc = new PictureBox();
              pc.BackColor = Color.HotPink;
              tableLayoutPanel1.Controls.Add(pc, row, col);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
              Amigo amigoPosicao;
              if (i <= 5)
              {
                amigoPosicao = Amigos[i];
                amigoPosicao.posicao = new Tuple<int, int>(row, col);
              }
              i++;
              break;
            default:
              pc = new PictureBox();
              pc.BackColor = Color.Sienna;
              tableLayoutPanel1.Controls.Add(pc, row, col);
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

    private void VerificarProximoMovimento(List<Tuple<string, int>> custo = null)
    {
      var linha = Linha;
      var coluna = Coluna;

      var distancia = PegaDistancia(linha, coluna);

      var menor = 999999999;
      var destino = "";
      if (custo == null)
        custo = PegaCusto(linha, coluna);

      if (custo.Count > 0)
      {
        foreach (var dist in distancia)
        {
          foreach (var c in custo)
          {
            var gh = c.Item2 + dist.Item1;
            if (menor > gh)
            {
              menor = gh;
              destino = c.Item1;
            }
          }
        }
      }

      Andar(destino, linha, coluna, custo);
    }

    private void VerificaSeJaFoiERecalculaRota(string destino, int linha, int coluna, List<Tuple<string, int>> custo)
    {
      switch (destino)
      {
        case "norte":

          var jaFoi = JaPassou.Any(s => s.Item1 == coluna - 1 && s.Item2 == linha);
          if (jaFoi)
          {
            var retiraCaminho = custo.First(s => s.Item1 == destino);
            custo.Remove(retiraCaminho);
            VerificarProximoMovimento(custo);
          }

          var andou = new Tuple<int, int>(Coluna - 1, Linha);
          JaPassou.Add(andou);
          break;
        case "sul":

          jaFoi = JaPassou.Any(s => s.Item1 == coluna + 1 && s.Item2 == linha);
          if (jaFoi)
          {
            var retiraCaminho = custo.First(s => s.Item1 == destino);
            custo.Remove(retiraCaminho);
            VerificarProximoMovimento(custo);
          }

          andou = new Tuple<int, int>(Coluna + 1, Linha);
          JaPassou.Add(andou);
          break;
        case "leste":

          jaFoi = JaPassou.Any(s => s.Item1 == coluna && s.Item2 == linha + 1);
          if (jaFoi)
          {
            var retiraCaminho = custo.First(s => s.Item1 == destino);
            custo.Remove(retiraCaminho);
            VerificarProximoMovimento(custo);
          }
          andou = new Tuple<int, int>(Coluna, Linha + 1);
          JaPassou.Add(andou);
          break;
        case "oeste":

          jaFoi = JaPassou.Any(s => s.Item1 == coluna && s.Item2 == linha - 1);
          if (jaFoi)
          {
            var retiraCaminho = custo.First(s => s.Item1 == destino);
            custo.Remove(retiraCaminho);
            VerificarProximoMovimento(custo);
          }

          andou = new Tuple<int, int>(Coluna, Linha - 1);
          JaPassou.Add(andou);
          break;
      }
    }


    private void Andar(string destino, int linha, int coluna, List<Tuple<string, int>> custo)
    {
      //VerificaSeJaFoiERecalculaRota(destino, linha, coluna, custo);

      switch (destino)
      {
        case "norte":
          var caminha = new PictureBox
          {
            BackColor = Color.Red
          };
          tableLayoutPanel1.Controls.Add(caminha, linha, coluna - 1);
          Coluna -= 1;

          caminha.Dock = DockStyle.Fill;
          caminha.Margin = new Padding(1);
          break;
        case "sul":
          caminha = new PictureBox
          {
            BackColor = Color.Red
          };
          tableLayoutPanel1.Controls.Add(caminha, linha, coluna + 1);
          Coluna += 1;
          caminha.Dock = DockStyle.Fill;
          caminha.Margin = new Padding(1);
          break;
        case "leste":
          caminha = new PictureBox();
          caminha.BackColor = Color.Red;
          tableLayoutPanel1.Controls.Add(caminha, linha + 1, coluna);
          Linha += 1;
          caminha.Dock = DockStyle.Fill;
          caminha.Margin = new Padding(1);
          break;
        case "oeste":
          caminha = new PictureBox
          {
            BackColor = Color.Red
          };
          tableLayoutPanel1.Controls.Add(caminha, linha - 1, coluna);
          Linha -= 1;
          caminha.Dock = DockStyle.Fill;
          caminha.Margin = new Padding(1);
          break;
      }
      tableLayoutPanel1.Update();
    }

    private List<Tuple<int, int>> PegaDistancia(int linha, int coluna)
    {
      var aX = linha;
      var bX = coluna;

      //Verifica para cada amigo
      var distancias = new List<Tuple<int, int>>();
      int i = 0;
      foreach (var amigo in Amigos)
      {
        var aY = amigo.posicao.Item1;
        var bY = amigo.posicao.Item2;

        //teorema de pitagoras
        var distancia = Math.Pow(Math.Abs((bX - aX) ^ 2) + Math.Abs((bY - aY) ^ 2), 2);
        var item = new Tuple<int, int>((int)distancia, i);
        distancias.Add(item);
        i++;
      }
      return distancias;
    }

    private List<Tuple<string, int>> PegaCusto(int linha, int coluna)
    {
      var custos = new List<Tuple<string, int>>();

      var norte = MatrizG[coluna - 1, linha];
      if (norte > 0)
      {
        var item = new Tuple<string, int>("norte", (int)norte);
        custos.Add(item);
      }

      var sul = MatrizG[coluna + 1, linha];
      if (sul > 0)
      {
        var item = new Tuple<string, int>("sul", (int)sul);
        custos.Add(item);
      }
      var leste = MatrizG[coluna, linha + 1];
      if (leste > 0)
      {
        var item = new Tuple<string, int>("leste", (int)leste);
        custos.Add(item);
      }
      var oeste = MatrizG[coluna, linha - 1];
      if (oeste > 0)
      {
        var item = new Tuple<string, int>("oeste", (int)oeste);
        custos.Add(item);
      }

      return custos;
    }

    private void btnInicial_Click(object sender, EventArgs e)
    {

      //Verificar se ele já passou por aquele lugar, escolhe outra posicao.
      //Verificar se a casa tem um amigo, e se tiver qual é o valor da propriedade aceita dele. Se for true setar jaAceitou true.
      var qtsAceitaram = Amigos.Count(s => s.jaAceitou);
      while (qtsAceitaram < 3)
      {
        VerificarProximoMovimento();
        qtsAceitaram = Amigos.Count(s => s.jaAceitou);
        
        
      }
    }
  }
}
