using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x46696E616C.TechManager.Stats
{
    public class Stats
    {
        List<Stat> stats;
        public Stats()
        {
            stats = new List<Stat>();
        }
        public Stats(List<Stat> stats)
        {
            this.stats = stats;
        }
        public int Count
        {
            get { return stats.Count; }
        }
        public Stat this[int number]
        {
            get { return stats[number]; }
        }
        public Stat this[Type stat]
        {
            get
            {
                foreach (Stat currentStat in stats)
                {
                    if (stat == currentStat.GetType())
                    {
                        return currentStat;
                    }
                }
                return null;
            }
            set
            {
                foreach (Stat currentStat in stats)
                {
                    if (stat.GetType() == currentStat.GetType())
                    {
                        currentStat.Upgrade(value.Value);
                    }
                }
            }
        }
        public void Add(Stat stat)
        {
            foreach (Stat currentStat in stats)
            {
                if (stat.GetType() == currentStat.GetType())
                {
                    this[currentStat.GetType()].Upgrade(-(this[currentStat.GetType()].Value));
                    this[currentStat.GetType()].Upgrade(stat.Value);
                    return;
                }
            }
            stats.Add(stat);
        }

    }
}
