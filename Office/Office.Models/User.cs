using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Office.Models.Enums;

namespace Office.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Username { get; set; }

        [MinLength(6)]
        [MaxLength(30)]
        [RegularExpression(@"^(?=.*?[0-9])(?=.*[A-Z]).{6,12}$")]
        public string Password { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Team> TeamsCreated { get; set; } = new List<Team>();

        public ICollection<Event> EventsCreated { get; set; } = new List<Event>();

        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>
            ();
        public ICollection<UserTeam> Teams { get; set; } = new List<UserTeam>();
    }
}
