using EcoLife.Model.Context;
using EcoLife.Model.Entity;
using EcoLife.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLife.Controller
{
    public class ChallengeController
    {
        private ChallengeRepository _challengeRepo;

        public List<Challenge> GetAllChallenge()
        {
            List<Challenge> challenges = new List<Challenge>();

            using (DbContext context = new DbContext())
            {
                _challengeRepo = new ChallengeRepository(context);

                challenges = _challengeRepo.GetAllChallenge();
            }

            return challenges;
        }

        public List<Challenge> SearchChallenge(string nama)
        {
            List<Challenge> challenges = new List<Challenge>();

            using (DbContext context = new DbContext())
            {
                _challengeRepo = new ChallengeRepository(context);
                challenges = _challengeRepo.ReadByNamaChallenge(nama);
            }

            return challenges;
        }

        public void CreateChallenge(Challenge challenge, User user)
        {
            if (user != null && user.Role == "admin")
            {
                if (string.IsNullOrEmpty(challenge.NameChallenge))
                {
                    MessageBox.Show("Nama Challenge harus diisi !!!", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (challenge.PointChallenge == 0)
                {
                    MessageBox.Show("Point Challenge harus diisi !!!", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                using (DbContext context = new DbContext())
                {
                    _challengeRepo = new ChallengeRepository(context);
                    _challengeRepo.CreateChallenge(challenge, user);
                }
            }
        }

        public void UpdateChallenge(Challenge challenge, User user)
        {
            if (user != null && user.Role == "admin")
            {
                if (string.IsNullOrEmpty(challenge.NameChallenge))
                {
                    MessageBox.Show("Nama Challenge harus diisi !!!", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (challenge.PointChallenge == 0)
                {
                    MessageBox.Show("Point Challenge harus diisi !!!", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                using (DbContext context = new DbContext())
                {
                    _challengeRepo = new ChallengeRepository(context);
                    _challengeRepo.UpdateChallenge(challenge, user);
                }
            }
        }

        public void DeleteChallenge(int IdChallenge, User user)
        {
            if (user != null && user.Role == "admin")
            {
                if (IdChallenge > 0)
                {
                    using (DbContext context = new DbContext())
                    {
                        _challengeRepo = new ChallengeRepository(context);
                        _challengeRepo.DeleteChallenge(IdChallenge, user);
                    }
                }
            }
        }
    }
}
