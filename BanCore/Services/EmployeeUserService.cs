using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using CoreBll;
namespace BanCore
{
    public class EmployeeUserService
    {
        private static CoreDbEntities db = new CoreDbEntities();
        public string Insert(EmployeeUser user)
        {
            if (ValidationHandler.ValidateEmployeeUser(user))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateEmpUser(user.Id,user.UserName,user.Email,user.Password,user.EmployeeId,user.RoleId, 
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
        public string Update(EmployeeUser user)
        {
            if (ValidationHandler.ValidateEmployeeUser(user))
            {
                try
                {
                    db.Database.BeginTransaction();
                    ObjectParameter newId = new ObjectParameter("NewId", typeof(int));
                    db.InsertOrUpdateEmpUser(user.Id, user.UserName, user.Email, user.Password, user.EmployeeId, user.RoleId,
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
        public EmployeeUser ValidateCredential(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;
            var result = db.ValidateEmpUserCredentials4(userName, password);
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
        public List<EmployeeUser> GetAll()
        {
            List<EmployeeUser> users = null;
            var rows = db.GetEmpUsers(null, null,null,null,null).ToList();
            if (rows.Any())
            {
                users = new List<EmployeeUser>();
                foreach (var r in rows)
                {
                    users.Add(new EmployeeUser
                    {
                        Id = (int)r.Id,
                        EmployeeId = (int)r.EmployeeId,
                        Email = r.Email,
                        Password = r.Password,
                        Status =(Status)r.StatusId,
                        UserName = r.UserName, 
                        Role = (Role)r.RoleId
                    });
                }
            }
            return users;
        }
        public EmployeeUser GetById(int id)
        {
            EmployeeUser user = null;
            var rows = db.GetEmpUsers(id, null,null,null,null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                user = new EmployeeUser
                {
                    Id = (int)r.Id,
                    EmployeeId = (int)r.EmployeeId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName,
                    Role = (Role)r.RoleId
                };
            }
            return user;
        }
        public EmployeeUser GetByEmployeeId(int employeeId)
        {
            EmployeeUser user=null;
            var rows = db.GetEmpUsers(null, null, null, employeeId,null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                user = new EmployeeUser
                {
                    Id = (int)r.Id,
                    EmployeeId = (int)r.EmployeeId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName,
                    Role = (Role)r.RoleId
                };
            }
            return user;
        }
        public EmployeeUser GetByUserName(string userName)
        {
            EmployeeUser user = null;
            var rows = db.GetEmpUsers(null, userName, null, null,null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                user = new EmployeeUser
                {
                    Id = (int)r.Id,
                    EmployeeId = (int)r.EmployeeId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName,
                    Role = (Role)r.RoleId
                }; 
            }
            return user;
        }
        public EmployeeUser GetByEmail(string email)
        {
            EmployeeUser user = null;
            var rows = db.GetEmpUsers(null, null,email, null,null).ToList();
            if (rows.Count() > 0)
            {
                var r = rows.FirstOrDefault();
                user = new EmployeeUser
                {
                    Id = (int)r.Id,
                    EmployeeId = (int)r.EmployeeId,
                    Email = r.Email,
                    Password = r.Password,
                    Status = (Status)r.StatusId,
                    UserName = r.UserName,
                    Role = (Role)r.RoleId
                };
            }
            return user;
        }
        public List<EmployeeUser> GetByRole(int roleId)
        {
            List<EmployeeUser> users = null;
            var rows = db.GetEmpUsers(null, null, null, null, roleId).ToList();
            if (rows.Any())
            {
                users = new List<EmployeeUser>();
                foreach (var r in rows)
                {
                    users.Add(new EmployeeUser
                    {
                        Id = (int)r.Id,
                        EmployeeId = (int)r.EmployeeId,
                        Email = r.Email,
                        Password = r.Password,
                        Status = (Status)r.StatusId,
                        UserName = r.UserName,
                        Role = (Role)r.RoleId
                    });
                }
            }
            return users;
        }
    }
}
