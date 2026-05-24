using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Exceptions
{

    public class CooperativistaNaoEncontradoException : CooperativaException
    {
        public CooperativistaNaoEncontradoException(string msg) : base(msg) { }
    }

}
