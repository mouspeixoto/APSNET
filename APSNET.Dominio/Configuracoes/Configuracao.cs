using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APSNET.Dominio.Configuracoes
{
    public class Configuracao
    {
        public string IConfiguracao { get; set; }
        public string Valor1 { get; set; }
        public string Valor2 { get; set; }

        public Configuracao()
        {
            IConfiguracao = "";
            Valor1 = "";
            Valor2 = "";
        }

    }
}
