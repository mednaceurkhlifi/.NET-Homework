using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public class Bilan
    {
        public DateTime DatePrelevement { get; set; }
        public string EmailMedecin { get; set; }
        public bool Paye { get; set; }

        // Clés étrangères
        public int InfirmierId { get; set; }
        public virtual Infirmier Infirmier { get; set; }

        public string CodePatient { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual ICollection<Analyse> Analyses { get; set; }
    }
}
