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
    
    public partial class Responsables
    {
        public string CIN { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public Nullable<int> R_Service { get; set; }
    
        public virtual Services Services { get; set; }
    }
}