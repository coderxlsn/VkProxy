using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkProxy.Models
{
    public class PlayerModel
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public List<Brawler> Brawlers { get; set; }
        public ClubClass Club { get; set; }
        public class ClubClass
        {
            public string Tag { get; set; }
            public string Name { get; set; }
        }
        public class Brawler
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
