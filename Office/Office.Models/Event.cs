using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Office.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<TeamEvent> Teams { get; set; } = new List<TeamEvent>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Name} {this.StartDate} {this.EndDate} {Environment.NewLine}" +
                          $"{this.Description}{Environment.NewLine}" +
                          $"Teams:");

            if (this.Teams.Any())
            {
                sb.AppendLine($"--{String.Join(Environment.NewLine, this.Teams.Select(p => p.Team.Name))}");
            }
            return sb.ToString().Trim();
        }
    }
}
