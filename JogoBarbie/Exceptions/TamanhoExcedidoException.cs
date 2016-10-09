using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoBarbie.Exceptions
{
  public class TamanhoExcedidoException : Exception
  {
    public TamanhoExcedidoException()
    {
      throw new Exception("Tamanho do índice excedido");
    }
  }
}
