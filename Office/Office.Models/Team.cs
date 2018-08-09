using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Office.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }

        [MinLength(3)]
        [MaxLength(3)]
        [Required]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }

      public ICollection<UserTeam> TeamMembers { get; set; } = new List<UserTeam>();

        public ICollection<TeamEvent> Events { get; set; } = new List<TeamEvent>();

        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{this.Name} {this.Acronym}{Environment.NewLine}" +
                          $"Members:");

            if (this.TeamMembers.Any())
            {
                sb.AppendLine( $"--{String.Join(Environment.NewLine, this.TeamMembers.Select(p => p.User.Username))}");
            }
                  
            return sb.ToString().Trim();

        }
    }
}
