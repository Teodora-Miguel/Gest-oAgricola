using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Exceptions
{


    public class QuantidadeInvalidaException : CooperativaException
    {
        public QuantidadeInvalidaException(string msg) : base(msg) { }
    }
}
