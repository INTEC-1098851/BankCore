using CoreBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanCore.Services
{
    public class UserService
    {
        private static CoreDbEntities db = new CoreDbEntities();
        public string Insert(User user)
        {
            if (ValidationHandler.ValidateUser(user))
            {
                try
                {
                    db.Database.BeginTransaction();
                    db.InsertOrUpdateUser(user.Id,user.UserName,user.Email,user.Password,user.ClientId, 
                        (int)Status.Active, null
                    );
                    db.Database.CurrentTransaction.Commit();
                    return "Usuario Creado";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "El usuario no puede ser registrado, favor verifique los campos requeridos";
        }
        public string Update(User user)
        {
            if (ValidationHandler.ValidateUser(user))
            {
                try
                {
                    db.Database.BeginTransaction();
                    db.InsertOrUpdateUser(user.Id, user.UserName, user.Email, user.Password, user.ClientId,
                        user.StatusId, null
                    );
                    db.Database.CurrentTransaction.Commit();
                    return "Usuario Modificado";
                }
                catch (Exception e)
                {
                    db.Database.CurrentTransaction.Rollback();
                }
            }
            return "El usuario no puede ser modificado, favor verifique los campos requeridos";
        }
        public User ValidateCredential(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;
            var result = db.ValidateUserCredentials(userName, password);
            if (result == null)
                return null;
            var row = result.FirstOrDefault();
            var user = new User
            {
                Email = row.Email,
                ClientId = (int)row.ClientId,
                Id = (int)row.Id,
                Password = row.Password,
                Status = (Status)row.StatusId,
                UserName = row.UserName
            };
            return user;
        }
        public string Delete(int id)
        {
            var User = GetById(id);
            if (User.Id == 0)
                return "Este usuario no existe";
            User.Status = Status.Inactive;
            Update(User);

            return "usuario desactivado.";
        }
        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            var rows = db.getusers(null, null,null,null);
            if (rows.Count() > 0)
            {
                foreach (var r in rows)
                {
                    users.Add(new User
                    {
                        Id = (int)r.Id,
                        ClientId = (int)r.ClientId,
                        Email = r.Email,
                        Password = r.Password,
                        Status =(Status)r.StatusId,
                        UserName = r.UserName
                    });
                }
            }
            return users;
        }
        public User GetById(int id)
        {
            User user = new User();
            var rows = db.getusers(id, null,null,null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                user = new User
                {
                    Id = (int)r.Id,
                    ClientId = (int)r.ClientId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName
                };
            }
            return user;
        }
        public User GetByClientId(int clientId)
        {
            User user = new User();
            var rows = db.getusers(null, null, null, clientId);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                user = new User
                {
                    Id = (int)r.Id,
                    ClientId = (int)r.ClientId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName
                };
            }
            return user;
        }
        public User GetByUserName(string userName)
        {
            User user = new User();
            var rows = db.getusers(null, userName, null, null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                user = new User
                {
                    Id = (int)r.Id,
                    ClientId = (int)r.ClientId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName
                };
            }
            return user;
        }
        public User GetByEmail(string email)
        {
            User user = new User();
            var rows = db.getusers(null, null,email, null);
            if (rows.Count() == 1)
            {
                var r = rows.FirstOrDefault();
                user = new User
                {
                    Id = (int)r.Id,
                    ClientId = (int)r.ClientId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName
                };
            }
            return user;
        }
    }
}