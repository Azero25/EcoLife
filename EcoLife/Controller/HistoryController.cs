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
    }
}
