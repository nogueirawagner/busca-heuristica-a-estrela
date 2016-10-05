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
    int Linha = 22;
    int Coluna = 18;

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
      for (row = 0; row < 42; row++)
      {
        for (col = 0; col < 42; col++)
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
            case 20:
              pc = new PictureBox();
              pc.BackColor = Color.Red;
              tableLayoutPanel1.Controls.Add(pc, row, col);
              pc.Dock = DockStyle.Fill;
              pc.Margin = new Padding(1);
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
      //var casaBarbie = new PictureBox
      //{
      //  BackColor = Color.Red
      //};
      //var p = new Point
      //{
      //  X = 23,
      //  Y = 19
      //};

      //var casaBarbie = tableLayoutPanel1.GetControlFromPosition(19, 23);
      //var picBarbie = (PictureBox)casaBarbie;
      //picBarbie.BackColor = Color.Red;



      //tableLayoutPanel1.get Add(casaBarbie, 23, 19);
      //casaBarbie.Dock = DockStyle.Fill;
      //casaBarbie.Margin = new Padding(1);

      //var casaBarbie2 = tableLayoutPanel1.GetControlFromPosition(20, 23);
      //var picBarbie2 = (PictureBox)casaBarbie2;
      //picBarbie2.BackColor = Color.Red;
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

      var jaFoi = JaPassou.Any(s => s.Item1 == coluna && s.Item2 == linha);
      if (jaFoi)
      {
        var retiraCaminho = custo.First(s => s.Item1 == destino);
        custo.Remove(retiraCaminho);
        VerificarProximoMovimento(custo);
      }

      switch (destino)
      {
        case "norte":
          break;
        case "sul":
          break;
        case "leste":
          break;
        case "oeste":
          break;
      }
    }


    private void Andar(string destino, int linha, int coluna, List<Tuple<string, int>> custo)
    {
      switch (destino)
      {
        case "norte":
          //VerificaSeJaFoiERecalculaRota(destino, linha, coluna - 1, custo);

          var caminha = tableLayoutPanel1.GetControlFromPosition(coluna - 1, linha);
          var picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          //var norte = new PictureBox
          //{
          //  BackColor = Color.Red
          //};
          //tableLayoutPanel1.Controls.Add(norte, linha, coluna - 1);
          

          //norte.Dock = DockStyle.Fill;
          //norte.Margin = new Padding(1);

          Coluna -= 1;
          var andou = new Tuple<int, int>(Coluna, Linha);
          JaPassou.Add(andou);


          break;
        case "sul":
          //VerificaSeJaFoiERecalculaRota(destino, linha, coluna + 1, custo);

          caminha = tableLayoutPanel1.GetControlFromPosition(coluna + 1, linha);
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          break;
        case "leste":
          //VerificaSeJaFoiERecalculaRota(destino, linha + 1, coluna, custo);

          caminha = tableLayoutPanel1.GetControlFromPosition(linha + 1, coluna);
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          //var leste = new PictureBox
          //{
          //  BackColor = Color.Red
          //};
          //tableLayoutPanel1.Controls.Add(leste, linha + 1, coluna);
          //Linha += 1;
          //andou = new Tuple<int, int>(Coluna, Linha);
          //JaPassou.Add(andou);
          //leste.Dock = DockStyle.Fill;
          //leste.Margin = new Padding(1);
          break;
        case "oeste":
          //VerificaSeJaFoiERecalculaRota(destino, linha - 1, coluna, custo);


          caminha = tableLayoutPanel1.GetControlFromPosition(coluna, linha - 1);
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          //var oeste = new PictureBox
          //{
          //  BackColor = Color.Red
          //};
          //tableLayoutPanel1.Controls.Add(oeste, linha - 1, coluna);
          //Linha -= 1;
          //andou = new Tuple<int, int>(Coluna, Linha);
          //JaPassou.Add(andou);
          //oeste.Dock = DockStyle.Fill;
          //oeste.Margin = new Padding(1);
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

      var norte = MatrizG[linha - 1, coluna];
      if (norte > 0)
      {
        var item = new Tuple<string, int>("norte", (int)norte);
        custos.Add(item);
      }

      var sul = MatrizG[linha + 1, coluna];
      if (sul > 0)
      {
        var item = new Tuple<string, int>("sul", (int)sul);
        custos.Add(item);
      }
      var leste = MatrizG[linha, coluna + 1];
      if (leste > 0)
      {
        var item = new Tuple<string, int>("leste", (int)leste);
        custos.Add(item);
      }
      var oeste = MatrizG[linha, coluna - 1];
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
