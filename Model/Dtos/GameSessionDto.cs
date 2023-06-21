﻿using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class GameSessionDto
    {
        public Guid SessionId { get; set; }
        public GameStateDto? GameState { get; set; }
        public PlayerDto? Player { get; set; }
        public DateTime StaringTime { get; set; }
        public TimeSpan GameDuration { get; set; }

    }
}
