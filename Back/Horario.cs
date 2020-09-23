using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorio.Back
{
    public class Horario
    {
        private List<string> dias = new List<string>();
        public List<string> Dias
        {
            get { return dias; }
            set { dias = value; }
        }
        public string desde { get; set; }
        public string hasta { get; set; }
    }
}
