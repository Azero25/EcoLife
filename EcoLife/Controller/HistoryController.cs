using EcoLife.Model.Context;
using EcoLife.Model.Entity;
using EcoLife.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLife.Controller
{
    public class HistoryController
    {
        private HistoryRepository _historyRepo;
        private User user;

        public HistoryController(User currentUser)
        {
            user = currentUser;
        }

        public List<History> ReadAllHistoryUser()
        {
            List<History> histories = new List<History>();

            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    histories = _historyRepo.GetAllHistoryUser(user);
                }
            }

            return histories;
        }

        public List<History> GetHistoryByDateRange(DateTime startDate, DateTime endDate)
        {
            List<History> histories = new List<History>();
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    histories = _historyRepo.GetHistoryByDateRange(user.IdUser, startDate, endDate);
                }
            }
            return histories;
        }

        public List<History> GetRecentHistory(int hours = 24)
        {
            DateTime sinceDate = DateTime.Now.AddHours(-hours);
            return GetHistoryByDateRange(sinceDate, DateTime.Now);
        }

        // Get history by activity type
        public List<History> GetHistoryByActivityType(string activityType)
        {
            List<History> histories = new List<History>();
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    histories = _historyRepo.GetHistoryByActivityType(user.IdUser, activityType);
                }
            }
            return histories;
        }

        // Get history by ID
        public History GetHistoryById(int historyId)
        {
            History history = null;
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    history = _historyRepo.GetHistoryById(historyId);
                }
            }
            return history;
        }

        // ===== METHOD TAMBAHAN - CREATE =====

        // Add history entry
        public bool AddHistory(History history)
        {
            bool result = false;
            if (user != null && history != null)
            {
                // Set IdUser jika belum di-set
                if (history.IdUser == 0)
                {
                    history.IdUser = user.IdUser;
                }

                // Set ActivityTime jika belum di-set
                if (history.ActivityTime == DateTime.MinValue)
                {
                    history.ActivityTime = DateTime.Now;
                }

                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    result = _historyRepo.Create(history);
                }
            }
            return result;
        }

        // Log activity - method shortcut untuk menambah history
        public bool LogActivity(string activityType, string description, int points = 0)
        {
            History history = new History
            {
                IdUser = user.IdUser,
                ActivityType = activityType,
                Description = description,
                Points = points,
                ActivityTime = DateTime.Now
            };
            return AddHistory(history);
        }

        // Log task completion
        public bool LogTaskCompletion(string taskName, int points)
        {
            return LogActivity("Task Completed", $"Completed task: {taskName}", points);
        }

        // Log challenge completion
        public bool LogChallengeCompletion(string challengeName, int points)
        {
            return LogActivity("Challenge Completed", $"Completed challenge: {challengeName}", points);
        }

        // Log user login
        public bool LogLogin()
        {
            return LogActivity("Login", "User logged in to the system", 5);
        }

        // ===== METHOD TAMBAHAN - UPDATE =====

        // Update existing history
        public bool UpdateHistory(History history)
        {
            bool result = false;
            if (user != null && history != null && history.IdHistory > 0)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    result = _historyRepo.Update(history);
                }
            }
            return result;
        }

        // ===== METHOD TAMBAHAN - DELETE =====

        // Delete history by ID
        public bool DeleteHistory(int historyId)
        {
            bool result = false;
            if (user != null && historyId > 0)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    result = _historyRepo.Delete(historyId);
                }
            }
            return result;
        }

        // Delete old history (lebih dari X hari)
        public bool DeleteOldHistory(int daysOld = 90)
        {
            bool result = false;
            if (user != null)
            {
                DateTime cutoffDate = DateTime.Now.AddDays(-daysOld);
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    result = _historyRepo.DeleteOldHistory(user.IdUser, cutoffDate);
                }
            }
            return result;
        }

     
        public int GetTotalPointsEarned()
        {
            int totalPoints = 0;
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    totalPoints = _historyRepo.GetTotalPointsEarned(user.IdUser);
                }
            }
            return totalPoints;
        }

        // Get activity count by type (untuk statistik)
        public Dictionary<string, int> GetActivityCountByType()
        {
            Dictionary<string, int> activityCounts = new Dictionary<string, int>();
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    activityCounts = _historyRepo.GetActivityCountByType(user.IdUser);
                }
            }
            return activityCounts;
        }

        // Get history count
        public int GetHistoryCount()
        {
            int count = 0;
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    count = _historyRepo.GetHistoryCount(user.IdUser);
                }
            }
            return count;
        }

        // Get most recent activity
        public History GetMostRecentActivity()
        {
            History history = null;
            if (user != null)
            {
                using (DbContext context = new DbContext())
                {
                    _historyRepo = new HistoryRepository(context);
                    history = _historyRepo.GetMostRecentActivity(user.IdUser);
                }
            }
            return history;
        }

        // ===== METHOD TAMBAHAN - UTILITY =====

        // Check if user has any history
        public bool HasHistory()
        {
            return GetHistoryCount() > 0;
        }

        // Set current user (jika perlu ganti user)
        public void SetCurrentUser(User newUser)
        {
            user = newUser;
        }

        // Get current user
        public User GetCurrentUser()
        {
            return user;
        }
    }
}
