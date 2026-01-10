using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Model.Entity
{
    public class Badge
    {
        public int Id_Badge { get; set; }
        public string Name_Badge { get; set; }
        public string File_Path { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
