//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gestion_Stagaires__Al_Omrane_.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stagaires
    {
        public string CIN { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public Nullable<System.DateTime> Date_Debut { get; set; }
        public string Durée { get; set; }
        public string Sujet { get; set; }
        public string Rapport { get; set; }
        public Nullable<bool> Depos_Rappoert { get; set; }
        public Nullable<double> Note { get; set; }
        public Nullable<int> S_Service { get; set; }
        public Nullable<int> Profil { get; set; }

        public virtual Profiles Profiles { get; set; }
        public virtual Services Services { get; set; }
    }
}