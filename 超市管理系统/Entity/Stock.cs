//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 超市管理系统.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public Nullable<System.DateTime> InsertDate { get; set; }
    }
}
