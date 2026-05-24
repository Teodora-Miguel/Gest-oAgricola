using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Exceptions
{
    public class CooperativaException : Exception
    {
        public CooperativaException(string mensagem) : base(mensagem) { }
    
    
    }
}
