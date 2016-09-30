using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoBarbie.Dominio.Implementation
{
  public class Amigo
  {
    public bool aceita { get; set; }
    public int[,] posicao { get; set; }

    public Amigo()
    {
      Amigos();
    }

    public Amigo(bool Aceitar)
    {

    }

    private List<Amigo> DefineAceitacao(List<Amigo> amigos)
    {
      var aindaNaoAceitaram = amigos.Where(s => !s.aceita).ToList();
      if (amigos.Count(s => s.aceita) <= 2)
      {
        var qtdAmigos = amigos.Count(s => !s.aceita);
        var selectAmigo = new Random();
        var amigo = selectAmigo.Next(0, qtdAmigos);

        var define = new Random();
        bool flag;

        var sn = define.Next(0, 2);
        if (sn == 0)
          flag = false;
        else
          flag = true;

        aindaNaoAceitaram[amigo].aceita = flag;
        DefineAceitacao(amigos);
      }

      return amigos;

    }

    public List<Amigo> Amigos()
    {
      var amigos = new List<Amigo>();

      var amigoA = new Amigo(true);
      var amigoB = new Amigo(true);
      var amigoC = new Amigo(true);
      var amigoD = new Amigo(true);
      var amigoE = new Amigo(true);
      var amigoF = new Amigo(true);

      amigos.Add(amigoA);
      amigos.Add(amigoB);
      amigos.Add(amigoC);
      amigos.Add(amigoD);
      amigos.Add(amigoE);
      amigos.Add(amigoF);

      DefineAceitacao(amigos);

      return amigos;
    }
  }
}
