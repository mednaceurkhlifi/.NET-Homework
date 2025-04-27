using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Domain
{
    public enum Specialite
    {
        Hematologie,
        Biochimie,
        Autre
    }
    public class Infirmier
    {
        public int InfirmierId { get; set; }
        public string NomComplet { get; set; }
        public Specialite Specialite { get; set; }

        public virtual ICollection<Bilan> Bilans { get; set; }
    }
}
