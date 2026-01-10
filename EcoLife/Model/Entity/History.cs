using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Entity
{
    public class History
    {
        public int IdHistory { get; set; }
        public int IdUser { get; set; }
        public string NameHistory { get; set; }
        public string DecsHistory { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relasi
        public User User { get; set; }
    }
}
