using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
    public class CaseDeLaby
    {
        private bool nord;
        private bool sud;
        private bool est;
        private bool ouest;
        private bool caseDepart;
        private bool caseArrive;
        private bool perso;
        private bool solution;
        private bool visiteSoluce;
        private bool piece;
        private bool monstre;
        private int abscisse;
        private int ordonne;
        public CaseDeLaby(int or, int ab)
        {
            nord = sud = est = ouest = caseDepart = caseArrive = perso = solution = VisiteSoluce = Piece = Monstre = false;
            Abscisse = or;
            Ordonne = ab;
        }
        public bool TousLesMurs()
        {
            if (!nord && !sud && !est && !ouest)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Nord
        {
            get
            {
                return nord;
            }

            set
            {
                nord = value;
            }
        }

        public bool Sud
        {
            get
            {
                return sud;
            }

            set
            {
                sud = value;
            }
        }

        public bool Est
        {
            get
            {
                return est;
            }

            set
            {
                est = value;
            }
        }

        public bool Ouest
        {
            get
            {
                return ouest;
            }

            set
            {
                ouest = value;
            }
        }

        public bool CaseDepart
        {
            get
            {
                return caseDepart;
            }

            set
            {
                caseDepart = value;
            }
        }

        public bool CaseArrive
        {
            get
            {
                return caseArrive;
            }

            set
            {
                caseArrive = value;
            }
        }

        public bool Perso
        {
            get
            {
                return perso;
            }

            set
            {
                perso = value;
            }
        }

        public int Abscisse
        {
            get
            {
                return abscisse;
            }

            set
            {
                abscisse = value;
            }
        }

        public int Ordonne
        {
            get
            {
                return ordonne;
            }

            set
            {
                ordonne = value;
            }
        }

        public bool Solution
        {
            get
            {
                return solution;
            }

            set
            {
                solution = value;
            }
        }

        public bool VisiteSoluce
        {
            get
            {
                return visiteSoluce;
            }

            set
            {
                visiteSoluce = value;
            }
        }

        public bool Piece
        {
            get
            {
                return piece;
            }

            set
            {
                piece = value;
            }
        }

        public bool Monstre
        {
            get
            {
                return monstre;
            }

            set
            {
                monstre = value;
            }
        }
    }
}
