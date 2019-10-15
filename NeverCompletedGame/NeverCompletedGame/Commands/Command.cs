using System;
using System.ComponentModel.DataAnnotations;

namespace NeverCompletedGame.Commands
{
    public class Command
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string AggregateType { get; set; }

        [Required]
        public Guid AggregateId { get; set; }

        public string Payload { get; set; }
    }
}
