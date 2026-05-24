using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestãoCooperativaAgrícola.Exceptions
{
    public class PeriodoEncerradoException : CooperativaException
    {
        public PeriodoEncerradoException(string msg) : base(msg) { }
    }
}
