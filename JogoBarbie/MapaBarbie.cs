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
    List<Tuple<int, int>> DistanciaAtual = new List<Tuple<int, int>>();

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
    }

    private void VerificarProximoMovimento(List<Tuple<string, int>> custo = null)
    {
      var linha = Linha;
      var coluna = Coluna;


      if (custo == null)
      {
        custo = PegaCusto(linha, coluna);
        DistanciaAtual = PegaDistancia(linha, coluna);
      }

      var distancia = DistanciaAtual;

      var menor = 999999999;
      var destino = "";

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

      if (destino == "sul")
      {
      }

      if (VerificaSeJaFoiERecalculaRota(destino, linha, coluna, custo))
      {
        VerificarProximoMovimento(custo);
      }

      if (!(linha != Linha || coluna != Coluna)) // Se isso acontecer é pq ele andou quando recalculou a rota, então nao anda de novo.
        Andar(destino, linha, coluna, custo);

    }

    private bool VerificaSeJaFoiERecalculaRota(string destino, int linha, int coluna, List<Tuple<string, int>> custo)
    {
      //leste coluna + 1;
      //oeste coluna - 1;
      //norte linha + 1;
      //sul linha - 1;

      if (custo.Count > 1)
      {
        switch (destino)
        {
          case "norte":
            //destino, linha - 1, coluna
            var jaFoi = JaPassou.Any(s => s.Item1 == linha - 1 && s.Item2 == coluna);
            if (jaFoi)
            {
              var retiraCaminho = custo.First(s => s.Item1 == destino);
              custo.Remove(retiraCaminho);
              return true;
              // VerificarProximoMovimento(custo);
            }
            break;
          case "sul":
            //linha + 1, coluna
            jaFoi = JaPassou.Any(s => s.Item1 == linha + 1 && s.Item2 == coluna);
            if (jaFoi)
            {
              var retiraCaminho = custo.First(s => s.Item1 == destino);
              custo.Remove(retiraCaminho);
              return true;
              //VerificarProximoMovimento(custo);
            }
            break;
          case "leste":
            //linha, coluna + 1
            jaFoi = JaPassou.Any(s => s.Item1 == linha && s.Item2 == coluna + 1);
            if (jaFoi)
            {
              var retiraCaminho = custo.First(s => s.Item1 == destino);
              custo.Remove(retiraCaminho);
              return true;
              //VerificarProximoMovimento(custo);
            }
            break;
          default:
            //linha, coluna - 1
            jaFoi = JaPassou.Any(s => s.Item1 == linha && s.Item2 == coluna - 1);
            if (jaFoi)
            {
              var retiraCaminho = custo.First(s => s.Item1 == destino);
              custo.Remove(retiraCaminho);
              return true;
              //VerificarProximoMovimento(custo);
            }
            break;
        }
      }
      return false;
    }


    private void Andar(string destino, int linha, int coluna, List<Tuple<string, int>> custo)
    {

      //leste coluna + 1;
      //oeste coluna - 1;
      //norte linha - 1;
      //sul linha + 1;

      if (JaPassou.Count() == 0)
      {
        var casa = new Tuple<int, int>(Linha, Coluna);
        JaPassou.Add(casa);
      }

      switch (destino)
      {
        case "norte":
          //VerificaSeJaFoiERecalculaRota(destino, linha - 1, coluna, custo);

          var caminha = tableLayoutPanel1.GetControlFromPosition(coluna, linha - 1);
          var picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;
          Linha -= 1;

          var andou = new Tuple<int, int>(Linha, Coluna);
          JaPassou.Add(andou);

          break;
        case "sul":
          //VerificaSeJaFoiERecalculaRota(destino, linha + 1, coluna, custo);

          caminha = tableLayoutPanel1.GetControlFromPosition(coluna, linha + 1);
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;
          Linha += 1;

          andou = new Tuple<int, int>(Linha, Coluna);
          JaPassou.Add(andou);
          break;
        case "leste":
          //VerificaSeJaFoiERecalculaRota(destino, linha, coluna + 1, custo);

          caminha = tableLayoutPanel1.GetControlFromPosition(coluna + 1, linha);
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;
          Coluna += 1;

          andou = new Tuple<int, int>(Linha, Coluna);
          JaPassou.Add(andou);
          break;
        default:
          //VerificaSeJaFoiERecalculaRota(destino, linha, coluna - 1, custo);

          caminha = tableLayoutPanel1.GetControlFromPosition(coluna - 1, linha);
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;
          Coluna -= 1;

          andou = new Tuple<int, int>(Linha, Coluna);
          JaPassou.Add(andou);
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
        var distancia = Math.Ceiling(Math.Sqrt(Math.Pow(Math.Abs((aY - aX)), 2)  + Math.Pow(Math.Abs((bY - bX)), 2)));
        var item = new Tuple<int, int>((int)distancia, i);
        distancias.Add(item);
        i++;
      }
      return distancias;
    }

    private List<Tuple<string, int>> PegaCusto(int linha, int coluna)
    {
      var custos = new List<Tuple<string, int>>();

      var linhaParede = 0;
      var colunaParede = 0;
      var norte = 0;
      var sul = 0;
      var leste = 0;
      var oeste = 0;

      if (linha == 0 && coluna == 0)
        norte = MatrizG[linhaParede, colunaParede];
      else if (linha == 0 && coluna != 0)
        norte = MatrizG[linhaParede, coluna];
      else if (linha != 0 && coluna == 0)
        norte = MatrizG[linha - 1, colunaParede];
      else
        norte = MatrizG[linha - 1, coluna];
      if (norte > 0)
      {
        var item = new Tuple<string, int>("norte", (int)norte);
        custos.Add(item);
      }

      if (linha == 0 && coluna == 0)
        sul = MatrizG[linhaParede, colunaParede];
      else if (linha == 0 && coluna != 0)
        sul = MatrizG[linhaParede, coluna];
      else if (linha != 0 && coluna == 0)
        sul = MatrizG[linha + 1, colunaParede];
      else
        sul = MatrizG[linha + 1, coluna];
      if (sul > 0)
      {
        var item = new Tuple<string, int>("sul", (int)sul);
        custos.Add(item);
      }

      if (linha == 0 && coluna == 0)
        leste = MatrizG[linhaParede, colunaParede];
      else if (linha == 0 && coluna != 0)
        leste = MatrizG[linhaParede, coluna + 1];
      else if (linha != 0 && coluna == 0)
        leste = MatrizG[linha, colunaParede];
      else
        leste = MatrizG[linha, coluna + 1];
      if (leste > 0)
      {
        var item = new Tuple<string, int>("leste", (int)leste);
        custos.Add(item);
      }

      if (linha == 0 && coluna == 0)
        oeste = MatrizG[linhaParede, colunaParede];
      else if (linha == 0 && coluna != 0)
        oeste = MatrizG[linhaParede, coluna - 1];
      else if (linha != 0 && coluna == 0)
        oeste = MatrizG[linha, colunaParede];
      else
        oeste = MatrizG[linha, coluna - 1];
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
