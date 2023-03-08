using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercici2
{
    public class Exercici2Codi
    {
        public string URL = "";
        public DirectoryInfo directoriInfo;
        public StringBuilder sb = new StringBuilder();

        public Exercici2Codi(string direct)
        {
            directoriInfo = new(direct);
            URL = direct;
        }

        public string ObtenirDirectori()
        {
            var fitxers = directoriInfo.GetFiles();

            foreach(var fitxer in fitxers)
            {
                sb.AppendLine(fitxer.FullName);
            }
            return sb.ToString();
        }

        public List<string> ObtenirDirectoriRecursiu(string ruta)
        {

            List<string> llistaFitxers = new();

            foreach (var fitxer in Directory.GetFiles(ruta))
            {
                llistaFitxers.Add(Path.GetFullPath(fitxer));
            }

            foreach (var dir in Directory.GetDirectories(ruta))
            {
                llistaFitxers.AddRange(ObtenirDirectoriRecursiu(dir));
            }

            return llistaFitxers;
        }

    }
}
