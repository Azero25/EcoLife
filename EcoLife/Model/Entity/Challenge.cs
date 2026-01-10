using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Entity
{
    public class Challenge
    {
        public int IdChallenge { get; set; }
        public string NameChallenge { get; set; }
        public string DecsChallenge { get; set; }
        public int PointChallenge { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
