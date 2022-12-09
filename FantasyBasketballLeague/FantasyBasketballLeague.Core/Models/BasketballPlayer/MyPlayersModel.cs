using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static FantasyBasketballLeague.Infrastructure.Data.Constants.ValidationConstants;


namespace FantasyBasketballLeague.Core.Models.BasketballPlayer
{
#nullable disable

    public class MyPlayersModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int PositionId { get; set; }

        public int TeamId { get; set; }

        [Display(Name = "Is Team Captain")]
        public string IsTeamCaptain { get; set; }

        [Display(Name = "Is Starter")]
        public string IsStarter { get; set; }

        [Required]
        [Display(Name = "Seasons Played")]
        public byte SeasonsPlayed { get; set; }

        [Required]
        [StringLength(JerseyMaxNumer, MinimumLength = JerseyMinNumer)]
        [Display(Name = "Jersey Number")]
        public string JerseyNumber { get; set; }

        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength)]
        public string Position { get; set; }

        [StringLength(TeamNameMaxLength, MinimumLength = TeamNameMinLength)]
        public string Team { get; set; }

        public string Experience { get; set; }
    }
}
