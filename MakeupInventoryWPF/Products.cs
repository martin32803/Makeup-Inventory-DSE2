//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MakeupInventoryWPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public Nullable<int> Stock { get; set; }
        public Nullable<int> MinStock { get; set; }
        public Nullable<int> MaxStock { get; set; }
    }
}
