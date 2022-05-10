using System;
using System.Collections.Generic;

namespace Commander.API.Models
{
    public partial class Command : BaseModel
    {
        //public Guid Id { get; set; }
        public string OperatingSystem { get; set; } = null!;
        public string RuntimeEnvironment { get; set; } = null!;
        public string CommandString { get; set; } = null!;
        public string Parameters { get; set; } = null!;
        public string ParametersSummary { get; set; } = null!;
    }
}
