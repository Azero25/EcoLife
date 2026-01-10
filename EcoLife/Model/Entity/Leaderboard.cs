using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Entity
{
    public class Leaderboard
    {
        public int IdLeaderboard { get; set; }
        public int IdUser { get; set; }
        public int Point { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relasi
        public User User { get; set; }
    }
}
