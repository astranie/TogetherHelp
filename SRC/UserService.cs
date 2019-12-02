using BLL;
using BLL.Repository;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SRC
{
    public class UserService : IUserService
    {
        private UserRepository userRepository;
        private User user;

        public UserService(UserRepository userrepository)
        {
            userRepository = userrepository;
            user = new User();
        }
        public User LogIn(string username, string password)
        {
            user = userRepository.GetByName(username);
            //只能显示是否注册，如果密码输错，需要另一个值来判断    或者让UI层根据查询结果进行判断

            if (user.PassWord == password)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User Register(string username, string password)
        {


            user.PassWord = GetMD5(password);
            user.UserName = username;
            user.TimeCreated = DateTime.Now;

            userRepository.Save(user);

            return user;
        }

        public User GetById(string id)
        {
            return userRepository.GetById(id);
            //这样的逻辑应该加上null判断及异常处理
        }

        public User HasExisted(string username)
        {

            if (userRepository.GetByName(username) != null)
            {
                user = userRepository.GetByName(username);
            }

            return null;
        }

        public User GetByName(string name)
        {
            return userRepository.GetByName(name);

        }

        public string GetMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input + Const.SALT));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }

        }

        public void DeleteMessage(string id, User user)
        {
            userRepository.DeleteMessage(Convert.ToInt32(id), user);
        }

        public IQueryable<Message> FindMessage(User user)
        {
            return userRepository.FindMeesage(user);
        }

        public void HasReaded(string messageid, User user)
        {
            userRepository.HasReaded(Convert.ToInt32(messageid), user);
        }

        public void AddHeader(string path, string userId)
        {
            userRepository.AddHeader(path, userId);
        }

        public byte[] GetHeader(string userId)
        {
            string path = GetById(userId).HeaderPath?? @"C:\Users\nnnzx\source\icon_boss.png";
            //Stream stream = new MemoryStream();
            //StreamReader streamReader = new StreamReader(path);
            
            return File.ReadAllBytes(path);
        }
    }
}
