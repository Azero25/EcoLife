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

     
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public DateTime ActivityTime { get; set; }

        // Relations
        public User User { get; set; }
    }
}

namespace EcoLife.Model.Repository
{
    using EcoLife.Model.Entity;
    using System;
    using System.Collections.Generic;

   
    public static class HistoryRepositoryExtensions
    {
       
        public static List<History> GetHistoryByDateRange(this HistoryRepository repo, int userId, DateTime startDate, DateTime endDate)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));

          
            var user = new User { IdUser = userId };
            List<History> all = repo.GetAllHistoryUser(user) ?? new List<History>();

         
            List<History> filtered = all.FindAll(h =>
            {
             
                DateTime at = h.ActivityTime == DateTime.MinValue ? h.CreatedAt : h.ActivityTime;
                return at >= startDate && at <= endDate;
            });

            return filtered;
        }

     
        public static bool Create(this HistoryRepository repo, History history)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));
            if (history == null) return false;

         
            return false;
        }

        
        public static bool DeleteOldHistory(this HistoryRepository repo, int userId, DateTime cutoffDate)
        {
            if (repo == null) throw new ArgumentNullException(nameof(repo));

            var user = new User { IdUser = userId };
            List<History> all = repo.GetAllHistoryUser(user);
            if (all == null) return false;

            bool anyDeleted = false;
            foreach (var h in all)
            {
                DateTime at = h.ActivityTime == DateTime.MinValue ? h.CreatedAt : h.ActivityTime;
                if (at < cutoffDate)
                {
            
                    try
                    {
                        if (repo.Delete(h.IdHistory))
                        {
                            anyDeleted = true;
                        }
                    }
                    catch
                    {
               
                    }
                }
            }

            return anyDeleted;
        }
    }
}
