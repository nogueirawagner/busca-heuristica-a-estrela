using JogoBarbie.Dominio.Implementation;
using JogoBarbie.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JogoBarbie.Exceptions;

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
    int CustoTotal = 0;
    Tuple<int, string> MinRecursao = new Tuple<int, string>(9999, "");

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

      if (linha > 41 || coluna > 41 || linha < 0 || coluna < 0)
        throw new TamanhoExcedidoException();

      if (custo == null)
      {
        custo = PegaCusto(linha, coluna);
        DistanciaAtual = PegaDistancia(linha, coluna);
      }

      var distancia = DistanciaAtual;

      var menor = 999999999;
      var destino = "";
      var amigoMaisProximo = 9999;
      var destinosPossiveis = new List<Tuple<string, int>>();

      if (custo.Count > 0)
      {
        foreach (var c in custo)
        {
          foreach (var dist in distancia)
          {
            var gh = c.Item2 + dist.Item1 + CustoTotal;
            if (menor >= gh)
            {
              menor = gh;
              destino = c.Item1;
              amigoMaisProximo = dist.Item2;

              if (destinosPossiveis.Count > 0)
              {
                var remove = destinosPossiveis.FirstOrDefault(s => s.Item1 == destino);
                if (remove != null)
                  destinosPossiveis.Remove(remove);
              }

              var destPossivel = new Tuple<string, int>(destino, amigoMaisProximo);
              destinosPossiveis.Add(destPossivel);
            }
          }
        }
      }

      if (destinosPossiveis.Count > 1)
      {
        destino = RetornaOMenorDestino(destinosPossiveis, linha, coluna, false);
        if (string.IsNullOrEmpty(destino))
        {
          throw new SemDestinoException();
        }
      }

      if (VerificaSeJaFoiERecalculaRota(destino, linha, coluna, custo))
      {
        VerificarProximoMovimento(custo);
      }

      if (!(linha != Linha || coluna != Coluna)) // Se isso acontecer é pq ele andou quando recalculou a rota, então nao anda de novo.
        Andar(destino, linha, coluna, custo);
    }

    private string RetornaOMenorDestino(List<Tuple<string, int>> destinos, int linha, int coluna, bool recursao)
    {
      //Linha coluna Atual
      var aX = VerificaLimite(linha); //Linha
      var bX = VerificaLimite(coluna); //Coluna

      //Linha coluna atual do amigo mais proximo
      var amigoMaisProximo = Amigos[destinos[0].Item2];
      var aY = VerificaLimite(amigoMaisProximo.posicao.Item1); //Linha
      var bY = VerificaLimite(amigoMaisProximo.posicao.Item2); //Coluna

      var destinoF = "";
      var menor = 999999;
      int jaPassouEmQts = 0;
      int totalDestinos = destinos.Count();
      var destinosPossiveis = new List<Tuple<string, int>>();

      foreach (var destino in destinos)
      {
        switch (destino.Item1)
        {
          case "norte":
            aX -= 1;

            var jaFoi = JaPassou.Any(s => s.Item1 == aX && s.Item2 == bX);
            if (jaFoi && totalDestinos != jaPassouEmQts + 1)
            {
              jaPassouEmQts++;
              continue;
            }

            var distancia = Math.Ceiling(Math.Sqrt(Math.Pow(Math.Abs((aY - aX)), 2) + Math.Pow(Math.Abs((bY - bX)), 2)));
            if (distancia < menor)
            {
              menor = (int)distancia;
              destinoF = "norte";
              if (!recursao)
              {
                var possivel = new Tuple<string, int>("norte", destinos[0].Item2);
                destinosPossiveis.Add(possivel);
              }
            }
            break;
          case "sul":
            aX += 1;

            jaFoi = JaPassou.Any(s => s.Item1 == aX && s.Item2 == bX);
            if (jaFoi && totalDestinos != jaPassouEmQts + 1)
            {
              jaPassouEmQts++;
              continue;
            }


            distancia = Math.Ceiling(Math.Sqrt(Math.Pow(Math.Abs((aY - aX)), 2) + Math.Pow(Math.Abs((bY - bX)), 2)));
            if (distancia < menor)
            {
              menor = (int)distancia;
              destinoF = "sul";
              if (!recursao)
              {
                var possivel = new Tuple<string, int>("sul", destinos[0].Item2);
                destinosPossiveis.Add(possivel);
              }
            }
            break;
          case "leste":
            bY += 1;

            jaFoi = JaPassou.Any(s => s.Item1 == aX && s.Item2 == bX);
            if (jaFoi && totalDestinos != jaPassouEmQts + 1)
            {
              jaPassouEmQts++;
              continue;
            }

            distancia = Math.Ceiling(Math.Sqrt(Math.Pow(Math.Abs((aY - aX)), 2) + Math.Pow(Math.Abs((bY - bX)), 2)));
            if (distancia <= menor)
            {
              menor = (int)distancia;
              destinoF = "leste";
              if (!recursao)
              {
                var possivel = new Tuple<string, int>("leste", destinos[0].Item2);
                destinosPossiveis.Add(possivel);
              }
            }
            break;
          default:
            bY -= 1;

            jaFoi = JaPassou.Any(s => s.Item1 == aX && s.Item2 == bX);
            if (jaFoi && totalDestinos != jaPassouEmQts + 1)
            {
              jaPassouEmQts++;
              continue;
            }


            distancia = Math.Ceiling(Math.Sqrt(Math.Pow(Math.Abs((aY - aX)), 2) + Math.Pow(Math.Abs((bY - bX)), 2)));
            if (distancia <= menor)
            {
              menor = (int)distancia;
              destinoF = "oeste";
              if (!recursao)
              {
                var possivel = new Tuple<string, int>("oeste", destinos[0].Item2);
                destinosPossiveis.Add(possivel);
              }
            }
            break;
        }
        if (recursao)
        {
          if (MinRecursao.Item1 > menor)
            MinRecursao = new Tuple<int, string>(menor, destinos[0].Item1);
        }

      }

      var destinosP = new List<Tuple<string, int>>();

      if (destinosPossiveis.Count > 1)
      {
        var menorP = 999999999;
        foreach (var destP in destinosPossiveis)
        {
          switch (destP.Item1)
          {
            case "norte":
              aX -= 2;

              var dest = new Tuple<string, int>("norte", destinos[0].Item2);
              destinosP.Add(dest);
              RetornaOMenorDestino(destinosP, aX, bX, true);
              if (MinRecursao.Item1 < menorP)
                destinoF = "norte";
              break;
            case "sul":
              aX += 2;

              dest = new Tuple<string, int>("sul", destinos[0].Item2);
              destinosP.Add(dest);
              RetornaOMenorDestino(destinosP, aX, bX, true);
              if (MinRecursao.Item1 < menorP)
                destinoF = "sul";

              break;
            case "leste":
              bY += 2;

              dest = new Tuple<string, int>("leste", destinos[0].Item2);
              destinosP.Add(dest);
              RetornaOMenorDestino(destinosP, aX, bX, true);
              if (MinRecursao.Item1 < menorP)
                destinoF = "leste";
              break;
            default:
              bY -= 2;

              dest = new Tuple<string, int>("oeste", destinos[0].Item2);
              destinosP.Add(dest);
              RetornaOMenorDestino(destinosP, aX, bX, true);
              if (MinRecursao.Item1 < menorP)
                destinoF = "oeste";
              break;
          }
        }
      }
      return destinoF;
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
          var caminha = tableLayoutPanel1.GetControlFromPosition(VerificaLimite(coluna), VerificaLimite(linha - 1));
          var picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          CustoTotal += custo.First(s => s.Item1 == "norte").Item2;
          var andou = new Tuple<int, int>(VerificaLimite(Linha), VerificaLimite(Coluna));
          JaPassou.Add(andou);
          Linha -= 1;
          break;
        case "sul":
          caminha = tableLayoutPanel1.GetControlFromPosition(VerificaLimite(coluna), VerificaLimite(linha + 1));
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          CustoTotal += custo.First(s => s.Item1 == "sul").Item2;
          andou = new Tuple<int, int>(VerificaLimite(Linha), VerificaLimite(Coluna));
          JaPassou.Add(andou);
          Linha += 1;
          break;
        case "leste":
          caminha = tableLayoutPanel1.GetControlFromPosition(VerificaLimite(coluna + 1), VerificaLimite(linha));
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;


          CustoTotal += custo.First(s => s.Item1 == "leste").Item2;

          andou = new Tuple<int, int>(VerificaLimite(Linha), VerificaLimite(Coluna));
          JaPassou.Add(andou);
          Coluna += 1;
          break;
        default:
          caminha = tableLayoutPanel1.GetControlFromPosition(VerificaLimite(coluna - 1), VerificaLimite(linha));
          picBarbie = (PictureBox)caminha;
          picBarbie.BackColor = Color.Red;

          CustoTotal += custo.First(s => s.Item1 == "oeste").Item2;
          andou = new Tuple<int, int>(VerificaLimite(Linha), VerificaLimite(Coluna));
          JaPassou.Add(andou);
          Coluna -= 1;
          break;
      }
      tableLayoutPanel1.Update();
    }

    private List<Tuple<int, int>> PegaDistancia(int linha, int coluna)
    {
      var aX = VerificaLimite(linha);
      var bX = VerificaLimite(coluna);

      //Verifica para cada amigo
      var distancias = new List<Tuple<int, int>>();
      int i = 0;
      foreach (var amigo in Amigos)
      {
        var aY = VerificaLimite(amigo.posicao.Item1);
        var bY = VerificaLimite(amigo.posicao.Item2);

        //teorema de pitagoras
        var distancia = Math.Ceiling(Math.Sqrt(Math.Pow(Math.Abs((aY - aX)), 2) + Math.Pow(Math.Abs((bY - bX)), 2)));
        var item = new Tuple<int, int>((int)distancia, i);
        distancias.Add(item);
        i++;
      }
      return distancias;
    }

    private int VerificaLimite(int valor)
    {
      var tam = Math.Sqrt(MatrizG.Length);


      if (valor <= 0)
        return 0;
      if (valor >= tam - 1)
        return (int)tam - 1;
      return valor;
    }


    private List<Tuple<string, int>> PegaCusto(int linha, int coluna)
    {
      var custos = new List<Tuple<string, int>>();

      var norte = 0;
      var sul = 0;
      var leste = 0;
      var oeste = 0;

      norte = MatrizG[VerificaLimite(linha - 1), VerificaLimite(coluna)];
      if (norte > 0 && VerificaLimite(linha - 1) > 0 && linha > 0)
      {
        var item = new Tuple<string, int>("norte", (int)norte);
        custos.Add(item);
      }

      sul = MatrizG[VerificaLimite(linha + 1), VerificaLimite(coluna)];
      if (sul > 0 && VerificaLimite(linha + 1) > 0)
      {
        var item = new Tuple<string, int>("sul", (int)sul);
        custos.Add(item);
      }

      leste = MatrizG[VerificaLimite(linha), VerificaLimite(coluna + 1)];
      if (leste > 0 && VerificaLimite(coluna + 1) < 41 && coluna + 1 > 0)
      {
        var item = new Tuple<string, int>("leste", (int)leste);
        custos.Add(item);
      }

      oeste = MatrizG[VerificaLimite(linha), VerificaLimite(coluna - 1)];
      if (oeste > 0 && VerificaLimite(coluna - 1) > 0 && VerificaLimite(coluna - 1) > 0 && coluna + 1 > 0)
      {
        var item = new Tuple<string, int>("oeste", (int)oeste);
        custos.Add(item);
      }

      return custos;
    }

    private void btnInicial_Click(object sender, EventArgs e)
    {
      var qtsAceitaram = Amigos.Count(s => s.jaAceitou);
      while (qtsAceitaram < 3)
      {
        VerificarProximoMovimento();
        qtsAceitaram = Amigos.Count(s => s.jaAceitou);
      }
    }
  }
}
