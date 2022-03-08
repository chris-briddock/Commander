using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Commander.API.Models
{
    public class Command : BaseModel
    {
        [DisallowNull]
        [Required]
        public string ?TheCommand { get; set; }
        [DisallowNull]
        [Required]
        public string ?HelpText { get; set; }
        [DisallowNull]
        [Required]
        public string ?Description { get; set; }
        [DisallowNull]
        [Required]
        public string ?Platform { get; set; }
    }
}