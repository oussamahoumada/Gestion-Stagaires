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
    
    public partial class Etablissements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etablissements()
        {
            this.Spécialité = new HashSet<Spécialité>();
        }
    
        public int Id_E { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public Nullable<int> ville { get; set; }
    
        public virtual Villes Villes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Spécialité> Spécialité { get; set; }
    }
}