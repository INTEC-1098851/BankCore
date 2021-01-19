using CoreBll;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
                    ObjectParameter newId=new ObjectParameter("NewId",typeof(int));
                    db.InsertOrUpdateUser(user.Id, user.UserName, user.Email, user.Password, user.ClientId,
                        (int)Status.Active, newId
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
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateUser(user.Id, user.UserName, user.Email, user.Password, user.ClientId,
                        user.StatusId, newId
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
            var result = db.ValidateUserCredentials1(userName, password);
            if (result.FirstOrDefault().Value == 1)
                return GetByUserName(userName);

            return null;
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
            var rows = db.getusers2(null, null,null,null).ToList();
            if (rows.Any())
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
            var rows = db.getusers2(id, null,null,null).ToList();
            if (rows.Count() > 0)
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
            var rows = db.getusers2(null, null, null, clientId).ToList();
            if (rows.Count() > 0)
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
            var rows = db.getusers2(null, userName, null, null).ToList();
            if (rows.Count() > 0)
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
            var rows = db.getusers2(null, null,email, null).ToList();
            if (rows.Count() > 0)
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