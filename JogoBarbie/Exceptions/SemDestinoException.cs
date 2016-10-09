using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoBarbie.Exceptions
{
  public class SemDestinoException : Exception
  {
    public SemDestinoException()
    {
      throw new Exception("Você não tem um destino.");
    }
  }
}
