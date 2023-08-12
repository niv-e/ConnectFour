using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConnectFourWinformClient.Model
{
    public record SessionRecordDto
    {
        public Guid SessionId { get; set; }
        public List<GameStep> GameSteps { get; set; }
    }

    public record SessionRecordEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid SessionId { get; set; }
        public string FormatedGameSteps { get; set; }
    }

    public enum PawnType { Player = 1, Server = 2 }

    public record GameStep
    {
        
        public PawnType PawnType { get; set; }

        public Tuple<int,int> Position { get; set; }

        public string ToFormatedGameStep()
        {
            return $"{PawnType},{Position.Item1},{Position.Item2}";
        }

        public GameStep FromFormatedGameStep(string formatedGameStep)
        {
            var separatedString = formatedGameStep.Split(',');
            Enum.TryParse<PawnType>(separatedString[0], out PawnType enumValue);

            return new GameStep
            {
                PawnType = enumValue,
                Position = new Tuple<int, int>(int.Parse(separatedString[1]), int.Parse(separatedString[2]))
            };


        }

    }
}
