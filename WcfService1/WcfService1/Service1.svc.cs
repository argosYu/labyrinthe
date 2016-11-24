using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        public Plateau RecupLaby(string tailleTableau, string tailleTableau2)
        {
            Labyrinthe p = new Labyrinthe(Convert.ToInt32(tailleTableau), Convert.ToInt32(tailleTableau2));
            Plateau pRen = new Plateau();
            List<Caselaby> tabCase = new List<Caselaby>();
            for (int i = 0; i < Convert.ToInt32(tailleTableau); i++)
            {
                pRen.tableaucase.Add(new LigneTab());
                for (int j = 0; j < Convert.ToInt32(tailleTableau2); j++)
                {
                    Caselaby tempC = new Caselaby();
                    CaseDeLaby tempCase = p.TableauCase[i, j];
                    if (tempCase.Nord)
                    {
                        tempC.info.Nord = 1;
                    }
                    if (tempCase.Sud)
                    {
                        tempC.info.Sud = 1;
                    }
                    if (tempCase.Est)
                    {
                        tempC.info.Est = 1;
                    }
                    if (tempCase.Ouest)
                    {
                        tempC.info.Ouest = 1;
                    }
                    if (tempCase.CaseDepart)
                    {
                        tempC.info.CaseDep = 1;
                    }
                    if (tempCase.CaseArrive)
                    {
                        tempC.info.CaseArr = 1;
                    }
                    if (tempCase.Perso)
                    {
                        tempC.info.Perso = 1;
                    }
                    if (tempCase.Solution)
                    {
                        tempC.info.Solution = 1;
                    }
                    if (tempCase.Piece)
                    {
                        tempC.info.Piece = 1;
                    }
                    if (tempCase.Monstre)
                    {
                        tempC.info.Monstre = 1;
                    }
                    tempC.info.Abscisse = tempCase.Abscisse;
                    tempC.info.Ordonne = tempCase.Ordonne;
                    pRen.tableaucase[i].listecase.Add(tempC);
                }
            }
            return pRen;
        }
        public string SaveScore(string Pseudo, string Score)
        {
            using (Database1Entities context = new Database1Entities())
            {
                Score s = new Score();
                Score temp = new Score();
                temp = context.Score.Select(x => x).ToList().Last();
                s.Id = temp.Id + 1;
                s.pseudo = Pseudo;
                s.score1 = Convert.ToInt32(Score);
                context.Score.Add(s);
                context.SaveChanges();
            }
            return "Score sauvegardé!";
        }
        public List<Podium> RecupResultat(string size)
        {
            List<Podium> p = new List<Podium>();
            for (int i = 0; i < Convert.ToInt32(size); i++)
            {
                p.Add(new Podium());
            }
            using (Database1Entities context = new Database1Entities())
            {
                List<Score> s = context.Score.Select(x => x).OrderByDescending(x => x.score1).ToList();
                for (int i = 0; i < Convert.ToInt32(size) && i < s.Count; i++)
                {
                    p[i].Pseudo = s[i].pseudo;
                    p[i].Score = s[i].score1.ToString();
                }
            }
            return p;
        }
    }
}
