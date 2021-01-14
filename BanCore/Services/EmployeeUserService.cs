using System;
using System.Collections.Generic;
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
                    db.InsertOrUpdateEmpUser(user.Id,user.UserName,user.Email,user.Password,user.EmployeeId,user.RoleId, 
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
        public string Update(EmployeeUser user)
        {
            if (ValidationHandler.ValidateEmployeeUser(user))
            {
                try
                {
                    db.Database.BeginTransaction();
                    db.InsertOrUpdateEmpUser(user.Id, user.UserName, user.Email, user.Password, user.EmployeeId, user.RoleId,
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
        public EmployeeUser ValidateCredential(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;
            var result = db.ValidateEmpUserCredentials(userName, password);
            if (result == null)
                return null;
            var row = result.FirstOrDefault();
            var user = new EmployeeUser
            {
                Email = row.Email,
                EmployeeId = (int)row.EmployeeId,
                Id = (int)row.Id,
                Password = row.Password,
                Role = (Role)row.RoleId,
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
        public List<EmployeeUser> GetAll()
        {
            List<EmployeeUser> users = new List<EmployeeUser>();
            var rows = db.GetEmpUsers(null, null,null,null,null);
            if (rows.Count() > 0)
            {
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
            EmployeeUser user = new EmployeeUser();
            var rows = db.GetEmpUsers(id, null,null,null,null);
            if (rows.Count() == 1)
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
            EmployeeUser user = new EmployeeUser();
            var rows = db.GetEmpUsers(null, null, null, employeeId,null);
            if (rows.Count() == 1)
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
            EmployeeUser user = new EmployeeUser();
            var rows = db.GetEmpUsers(null, userName, null, null,null);
            if (rows.Count() == 1)
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
            EmployeeUser user = new EmployeeUser();
            var rows = db.GetEmpUsers(null, null,email, null,null);
            if (rows.Count() == 1)
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
            List<EmployeeUser> users = new List<EmployeeUser>();
            var rows = db.GetEmpUsers(null, null, null, null, roleId);
            if (rows.Count() > 0)
            {
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
