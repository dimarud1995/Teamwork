//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SVTrade.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChoosedCategory
    {
        public int chosenCategoryID { get; set; }
        public int productCategoryID { get; set; }
        public int userID { get; set; }
    
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual User User { get; set; }
    }
}
