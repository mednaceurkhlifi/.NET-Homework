using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;

namespace Examen.ApplicationCore.Services
{
    public class BilanService : Service<Bilan>, IBilanService
    {
        public BilanService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public double CalculerMontantTotal(Bilan bilan)
        {
            var bilansPatient = GetMany(b => b.CodePatient == bilan.CodePatient);
            int nbPrelevements = bilansPatient.Count();

            double montant = bilan.Analyses.Sum(a => a.PrixAnalyse);

            if (nbPrelevements > 5)
                montant *= 0.9;

            return montant;
        }

        public double GetPercentageOfNursesBySpecialty(List<Infirmier> infirmiers, string specialty)
        {
            var totalNurses = infirmiers.Count;
            var nursesInSpecialty = infirmiers.Count(n => n.Specialite.Equals(specialty));

            if (totalNurses == 0) return 0; 

            return (double)nursesInSpecialty / totalNurses * 100;
        }

        public IEnumerable<IGrouping<string, Analyse>> GetAbnormalAnalysesByPatient(List<Analyse> analyses, int patientId)
        {
            var currentYear = DateTime.Now.Year;

            var abnormalAnalyses = analyses
                .Where(a => a.Bilan.Patient.CodePatient.Equals(patientId) && a.Bilan.DatePrelevement.Year == currentYear &&
                            (a.ValeurAnalyse > a.ValeurMinNormale || a.ValeurAnalyse < a.ValeurMinNormale))
                .GroupBy(a => a.Bilan.ToString()); 

            return abnormalAnalyses;
        }

        public DateTime GetBilanReadyDate(Bilan bilan)
        {
            var readyDates = bilan.Analyses
                .Select(a => bilan.DatePrelevement.AddDays(a.DureeResultat))  
                .ToList();
            if (!readyDates.Any())
            {
                return bilan.DatePrelevement;
            }
            return readyDates.Max();
        }

    }
}
