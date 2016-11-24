using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
    public class Labyrinthe
    {
        private Random random = new Random();
        private int tailleCoteLaby;
        private int tailleHauteurLaby;
        private CaseDeLaby[,] tableauCase;

        public CaseDeLaby[,] TableauCase
        {
            get
            {
                return tableauCase;
            }

            set
            {
                tableauCase = value;
            }
        }

        public Labyrinthe(int tailleCote, int tailleHaut)
        {
            tailleCoteLaby = tailleCote;
            tailleHauteurLaby = tailleHaut;
            TableauCase = new CaseDeLaby[tailleCoteLaby, tailleHauteurLaby];
            generationTableauCase();
            generationSolution();
        }
        private void generationTableauCase()
        {
            Stack<CaseDeLaby> LIFO = new Stack<CaseDeLaby>();
            for (int k = 0; k < tailleCoteLaby; k++)
            {
                for (int l = 0; l < tailleHauteurLaby; l++)
                {
                    TableauCase[k, l] = new CaseDeLaby(k, l);
                }
            }
            int i = random.Next(0, tailleCoteLaby);
            int j = random.Next(0, tailleHauteurLaby);
            LIFO.Push(TableauCase[i, j]);
            int NbCell = 1;
            int MaxCell = tailleCoteLaby * tailleHauteurLaby;
            while (NbCell < MaxCell)
            {
                List<CaseDeLaby> caseSuivantePotentiel = generationListCaseSuivantePotentiel(i, j);
                if (caseSuivantePotentiel.Count > 0)
                {
                    CaseDeLaby nextCase = choixCaseSuivanteEtModificationTableau(ref tableauCase, caseSuivantePotentiel, LIFO);
                    LIFO.Push(nextCase);
                    NbCell++;
                }
                else
                {
                    LIFO.Pop();
                }
                i = LIFO.Peek().Abscisse;
                j = LIFO.Peek().Ordonne;
            }
        }
        private CaseDeLaby choixCaseSuivanteEtModificationTableau(ref CaseDeLaby[,] tableauCase, List<CaseDeLaby> caseSuivantePotentiel, Stack<CaseDeLaby> LIFO)
        {
            CaseDeLaby nextCase = caseSuivantePotentiel[random.Next(0, caseSuivantePotentiel.Count)];
            if (nextCase.Abscisse == (LIFO.Peek().Abscisse - 1 + tailleCoteLaby) % tailleCoteLaby)
            {
                TableauCase[nextCase.Abscisse, nextCase.Ordonne].Sud = true;
                TableauCase[LIFO.Peek().Abscisse, LIFO.Peek().Ordonne].Nord = true;
            }
            if (nextCase.Ordonne == (LIFO.Peek().Ordonne - 1 + tailleHauteurLaby) % tailleHauteurLaby)
            {
                TableauCase[nextCase.Abscisse, nextCase.Ordonne].Est = true;
                TableauCase[LIFO.Peek().Abscisse, LIFO.Peek().Ordonne].Ouest = true;
            }
            if (nextCase.Abscisse == (LIFO.Peek().Abscisse + 1) % tailleCoteLaby)
            {
                TableauCase[nextCase.Abscisse, nextCase.Ordonne].Nord = true;
                TableauCase[LIFO.Peek().Abscisse, LIFO.Peek().Ordonne].Sud = true;
            }
            if (nextCase.Ordonne == (LIFO.Peek().Ordonne + 1) % tailleHauteurLaby)
            {
                TableauCase[nextCase.Abscisse, nextCase.Ordonne].Ouest = true;
                TableauCase[LIFO.Peek().Abscisse, LIFO.Peek().Ordonne].Est = true;
            }
            return nextCase;
        }
        private List<CaseDeLaby> generationListCaseSuivantePotentiel(int i, int j)
        {
            List<CaseDeLaby> caseSuivantePotentiel = new List<CaseDeLaby>();
            if (TableauCase[(i + 1) % tailleCoteLaby, j].TousLesMurs())
            {
                caseSuivantePotentiel.Add(TableauCase[(i + 1) % tailleCoteLaby, j]);
            }
            if (TableauCase[(i - 1 + tailleCoteLaby) % tailleCoteLaby, j].TousLesMurs())
            {
                caseSuivantePotentiel.Add(TableauCase[(i - 1 + tailleCoteLaby) % tailleCoteLaby, j]);
            }
            if (TableauCase[i, (j + 1) % tailleHauteurLaby].TousLesMurs())
            {
                caseSuivantePotentiel.Add(TableauCase[i, (j + 1) % tailleHauteurLaby]);
            }
            if (TableauCase[i, (j - 1 + tailleHauteurLaby) % tailleHauteurLaby].TousLesMurs())
            {
                caseSuivantePotentiel.Add(TableauCase[i, (j - 1 + tailleHauteurLaby) % tailleHauteurLaby]);
            }
            return caseSuivantePotentiel;
        }
        private List<CaseDeLaby> generationListCaseDeRechercheSuivantePotentiel(int i, int j)
        {
            List<CaseDeLaby> listCase = new List<CaseDeLaby>();
            if (TableauCase[i, j].Sud && !TableauCase[(i + 1) % tailleCoteLaby, j].VisiteSoluce)
            {
                listCase.Add(TableauCase[(i + 1) % tailleCoteLaby, j]);
            }
            if (TableauCase[i, j].Nord && !TableauCase[(i - 1 + tailleCoteLaby) % tailleCoteLaby, j].VisiteSoluce)
            {
                listCase.Add(TableauCase[(i - 1 + tailleCoteLaby) % tailleCoteLaby, j]);
            }
            if (TableauCase[i, j].Est && !TableauCase[i, (j + 1) % tailleHauteurLaby].VisiteSoluce)
            {
                listCase.Add(TableauCase[i, (j + 1) % tailleHauteurLaby]);
            }
            if (TableauCase[i, j].Ouest && !TableauCase[i, (j - 1 + tailleHauteurLaby) % tailleHauteurLaby].VisiteSoluce)
            {
                listCase.Add(TableauCase[i, (j - 1 + tailleHauteurLaby) % tailleHauteurLaby]);
            }
            return listCase;
        }
        private void generationSolution()
        {
            CaseDeLaby dep = tableauCase[random.Next(0, tailleCoteLaby), random.Next(0, tailleHauteurLaby)];
            CaseDeLaby arr = tableauCase[random.Next(0, tailleCoteLaby), random.Next(0, tailleHauteurLaby)];
            while (arr.Abscisse == dep.Abscisse && arr.Ordonne == dep.Ordonne)
            {
                dep = tableauCase[random.Next(0, tailleCoteLaby), random.Next(0, tailleHauteurLaby)];
                arr = tableauCase[random.Next(0, tailleCoteLaby), random.Next(0, tailleHauteurLaby)];
            }
            tableauCase[dep.Abscisse, dep.Ordonne].CaseDepart = true;
            tableauCase[dep.Abscisse, dep.Ordonne].VisiteSoluce = true;
            tableauCase[dep.Abscisse, dep.Ordonne].Perso = true;
            tableauCase[arr.Abscisse, arr.Ordonne].CaseArrive = true;
            int i = dep.Abscisse;
            int j = dep.Ordonne;
            Stack<CaseDeLaby> LIFO = new Stack<CaseDeLaby>();
            LIFO.Push(TableauCase[i, j]);
            while (i != arr.Abscisse || j != arr.Ordonne)
            {
                List<CaseDeLaby> listCase = generationListCaseDeRechercheSuivantePotentiel(i, j);
                if (listCase.Count > 0)
                {
                    CaseDeLaby nextCase = listCase[random.Next(0, listCase.Count)];
                    tableauCase[nextCase.Abscisse, nextCase.Ordonne].VisiteSoluce = true;
                    LIFO.Push(nextCase);
                }
                else
                {
                    LIFO.Pop();
                }
                i = LIFO.Peek().Abscisse;
                j = LIFO.Peek().Ordonne;
            }
            tracerLeChemin(LIFO);
            List<CaseDeLaby> casePrise = new List<CaseDeLaby>();
            casePrise.Add(dep);
            casePrise.Add(arr);
            generationDesEntites(casePrise);
        }
        private void tracerLeChemin(Stack<CaseDeLaby> LIFO)
        {
            foreach (CaseDeLaby c in LIFO)
            {
                tableauCase[c.Abscisse, c.Ordonne].Solution = true;
            }
        }
        private void generationDesEntites(List<CaseDeLaby> casePrise)
        {
            int i = 0;
            while (i < (tailleCoteLaby + tailleHauteurLaby) / 2)
            {
                i = generationDesPieces(i, ref casePrise);
            }
            i = 0;
            while (i < (tailleCoteLaby + tailleHauteurLaby) / 4)
            {
                i = generationDesMonstres(i, ref casePrise);
            }
        }
        private int generationDesPieces(int i, ref List<CaseDeLaby> casePrise)
        {
            CaseDeLaby casePiece = tableauCase[random.Next(0, tailleCoteLaby), random.Next(0, tailleHauteurLaby)];
            bool nouvelPiece = true;
            foreach (CaseDeLaby c in casePrise)
            {
                if (c.Ordonne == casePiece.Ordonne && c.Abscisse == casePiece.Abscisse)
                {
                    nouvelPiece = false;
                }
            }
            if (nouvelPiece)
            {
                casePrise.Add(casePiece);
                tableauCase[casePiece.Abscisse, casePiece.Ordonne].Piece = true;
                i++;
            }
            return i;
        }
        private int generationDesMonstres(int i, ref List<CaseDeLaby> casePrise)
        {
            CaseDeLaby caseMonstre = tableauCase[random.Next(0, tailleCoteLaby), random.Next(0, tailleHauteurLaby)];
            bool monstre = true;
            foreach (CaseDeLaby c in casePrise)
            {
                if (c.Ordonne == caseMonstre.Ordonne && c.Abscisse == caseMonstre.Abscisse)
                {
                    monstre = false;
                }
            }
            if (monstre)
            {
                casePrise.Add(caseMonstre);
                tableauCase[caseMonstre.Abscisse, caseMonstre.Ordonne].Monstre = true;
                i++;
            }
            return i;
        }
    }
}
