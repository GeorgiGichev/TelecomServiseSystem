namespace TelecomServiceSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using TelecomServiceSystem.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
