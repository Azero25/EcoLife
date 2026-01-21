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
    public class UserController
    {
        private UserRepository _userRepo;
        public static User CurrentUser { get; private set; }

        public int RegisterUser(User user)
        {
            int result = 0;

            if (string.IsNullOrEmpty(user.Name))
            {
                MessageBox.Show("Nama harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                MessageBox.Show("Email harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (!user.Email.Contains("@") || !user.Email.Contains("."))
            {
                MessageBox.Show("Email harus mengandung '@' dan '.' !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Password harus diisi !!!", "Peringatan",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            using (DbContext context = new DbContext())
            {
                _userRepo = new UserRepository(context);
                if (_userRepo.IsEmailExist(user.Email) == false)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.Role = "user";
                    result = _userRepo.CreateUser(user);
                }
                else
                {
                    MessageBox.Show("Akun sudah ada, silahkan gunakan akun lain!!!", "Peringatan",
                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            }

            return result;
        }

        public User LoginUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email harus diisi !!!");
                return null;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password harus diisi !!!");
                return null;
            }

            using (DbContext context = new DbContext())
            {
                _userRepo = new UserRepository(context);
                User user = _userRepo.Login(email, password);

                if (user == null)
                {
                    MessageBox.Show("Email atau password salah !!!");
                    return null;
                }

                // SET SESSION USER
                CurrentUser = user;
                return user;
            }
        }

        public List<User> ReadAllUser()
        {
            List<User> listUser = new List<User>();

            using (DbContext context = new DbContext())
            {
                _userRepo = new UserRepository(context);
                listUser = _userRepo.GetAllUsers();
            }

            return listUser;
        }

        public List<User> GetUserByName(string name)
        {
            List<User> listUser = new List<User>();
            using (DbContext context = new DbContext())
            {
                _userRepo = new UserRepository(context);
                listUser = _userRepo.GetUserByName(name);
            }
            return listUser;
        }

        public User GetUserById(int idUser)
        {
            User user = null;
            using (DbContext context = new DbContext())
            {
                _userRepo = new UserRepository(context);
                user = _userRepo.GetUserById(idUser);
            }
            return user;
        }

        public int UpdateUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.Name)) throw new Exception("Nama harus diisi");
            if (string.IsNullOrWhiteSpace(user.Email)) throw new Exception("Email harus diisi");
            if (string.IsNullOrWhiteSpace(user.Role)) throw new Exception("Role harus diisi");

            using (DbContext context = new DbContext())
            {
                _userRepo = new UserRepository(context);

                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }
                else
                {
                    user.Password = null;
                }

                return _userRepo.UpdateUser(user);
            }
        }

        public void DeleteUser(User user)
        {
            if (user != null && user.IdUser > 0)
            {
                using (DbContext context = new DbContext())
                {
                    _userRepo = new UserRepository(context);
                    _userRepo.DeleteUser(user);
                }
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            Application.Restart();
        }

        public int GetUserScore(int idUser)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUserScore(int userId, int newScore)
        {
            if (userId <= 0)
            {
                MessageBox.Show("User ID tidak valid!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            try
            {
                using (DbContext context = new DbContext())
                {
                    _userRepo = new UserRepository(context);
                    int result = _userRepo.UpdateUserScore(userId, newScore);

                    if (result > 0)
                    {
                        
                        if (CurrentUser != null && CurrentUser.IdUser == userId)
                        {
                            CurrentUser.TotalScore = newScore;
                        }
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mengupdate score: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
