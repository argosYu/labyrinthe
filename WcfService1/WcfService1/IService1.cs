using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/laby/{tailleTableau}/{tailleTableau2}", ResponseFormat = WebMessageFormat.Json)]
        Plateau RecupLaby(string tailleTableau, string tailleTableau2);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/save/{Pseudo}/{Score}", ResponseFormat = WebMessageFormat.Json)]
        string SaveScore(string Pseudo, string Score);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/podium/{size}", ResponseFormat = WebMessageFormat.Json)]
        List<Podium> RecupResultat(string size);
    }
    //"class" permettant la sérialozation des données.
    [DataContract]
    public class Podium
    {
        [DataMember]
        public string Pseudo;
        [DataMember]
        public string Score;
    }
    [DataContract]
    public class Plateau
    {
        [DataMember]
        public List<LigneTab> tableaucase = new List<LigneTab>();
    }
    [DataContract]
    public class LigneTab
    {
        [DataMember]
        public List<Caselaby> listecase = new List<Caselaby>();
    }
    [DataContract]
    public class Caselaby
    {
        [DataMember]
        public InfoCase info = new InfoCase();
    }
    [DataContract]
    public class InfoCase
    {
        [DataMember]
        public int Nord = 0;
        [DataMember]
        public int Sud = 0;
        [DataMember]
        public int Est = 0;
        [DataMember]
        public int Ouest = 0;
        [DataMember]
        public int CaseDep = 0;
        [DataMember]
        public int CaseArr = 0;
        [DataMember]
        public int Perso = 0;
        [DataMember]
        public int Abscisse = 0;
        [DataMember]
        public int Ordonne = 0;
        [DataMember]
        public int Solution = 0;
        [DataMember]
        public int Piece = 0;
        [DataMember]
        public int Monstre = 0;
    }
}
