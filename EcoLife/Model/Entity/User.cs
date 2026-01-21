using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Entity
{
    public class User
    {
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relasi
        public ICollection<History> Histories { get; set; }
        public ICollection<Leaderboard> Leaderboards { get; set; }
        public int TotalScore { get; internal set; }
        public string Username { get; internal set; }
    }
}
