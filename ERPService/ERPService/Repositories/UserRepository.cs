
using Dapper;
using Dapper.Contrib.Extensions;
using ERPService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ERPService
{
    public class UserRepository
    {
        public IDbConnection connection { get { return DataIO.connection; } }

        public Operation<User> LoginUser(string UserName, string Password)
        {
            Operation<User> op_ = new Operation<User>();
            try
            {
                //string openPassword = DataIO.Cryptor.Decrypt(Password);
                string openPassword = new RijndaelCrypt("D8903F7F075E4877A6C5F62EC68A2018").Decrypt(Password);
                BaseUser baseUser = connection.Query<BaseUser>(" select * From [BaseUser] where UserName = @UserName AND PassHash = @PassHash ", new { UserName = UserName, PassHash = Password }).FirstOrDefault();
                if (baseUser == null)
                {
                    op_.Fail = "istifadəçi adı və ya şifrə yanlışdır";
                    return op_;
                }
                if (baseUser.IsActive == false)
                {
                    op_.Fail = "istifadəçi adı passivdir";
                    return op_;
                }
                LoginSession loginSession = connection.Query<LoginSession>("EXECUTE [dbo].[SP_GetSessionGuid] " + baseUser.Id.ToString()).FirstOrDefault();
                if (loginSession == null)
                {
                    op_.Fail = "Unable to get login session";
                    return op_;
                }

                List<PermissionDetail> permissionDetails = GetPermissionDetailsByUserId(baseUser.Id).Value;
                if (permissionDetails == null)
                {
                    op_.Fail = "Unable to get login permissions";
                    return op_;
                }

                User user = baseUser.GetEligibleOjbect<User>();
                user.PermissionDetails = permissionDetails;
                user.LoginSession = loginSession;
                op_.Value = user;
                op_.Successful = true;
            }
            catch (Exception ex)
            {
                op_.Fail = ex.Message;
            }

            return op_;
        }

        public Operation<User> GetUserById(int Id)
        {
            Operation<User> op_ = new Operation<User>();
            try
            {
                BaseUser baseUser = connection.Get<BaseUser>(Id);
                List<PermissionDetail> permissionDetails = GetPermissionDetailsByUserId(Id).Value;
                User user =  baseUser.GetEligibleOjbect<User>();
                user.PermissionDetails = permissionDetails;
                user.LoginSession = null;
                op_.Value = user;
                op_.Successful = true;
            }
            catch (Exception ex)
            {
                op_.Fail = ex.Message;
            }

            return op_;
        }



        public Operation<List<PermissionDetail>> GetPermissionDetailsByUserId(int Id,IDbTransaction transaction = null)
        {
            Operation<List<PermissionDetail>> operation = new Operation<List<PermissionDetail>>();
            try
            {
                operation.Value = connection.Query<PermissionDetail>("SELECT * FROM PermissionDetail WHERE UserId = " + Id.ToString(),null,transaction).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }


        public Operation<List<PermissionMaster>> GetPermissionMasters()
        {
            Operation<List<PermissionMaster>> operation = new Operation<List<PermissionMaster>>();
            try
            {
                operation.Value = connection.GetAll<PermissionMaster>().ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<List<BaseUser>> GetUsers()
        {
            Operation<List<BaseUser>> operation = new Operation<List<BaseUser>>();
            try
            {
                operation.Value = connection.GetAll<BaseUser>().Where(x=>x.Id != 0).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<User> PostUser(User user)
        {
            int UserId = 0;


            IDbTransaction transaction = null;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                if (user.Id == 0)
                {
                    user.PassHash = "123";
                   
                    connection.Insert(user as BaseUser,transaction);
                    user.PermissionDetails.ForEach(x => x.UserId = user.Id);
                    connection.Insert(user.PermissionDetails,transaction);
                    UserId = user.Id;
                }
                else
                {
                    UserId = user.Id;
                    connection.Update(user as BaseUser, transaction);
                    List<PermissionDetail> oldPermissions = GetPermissionDetailsByUserId(UserId,transaction).Value;
                    List<PermissionDetail> currentPermissions = user.PermissionDetails;
                    // delete nonexisting items
                    foreach (var old in oldPermissions)
                    {
                        if (!currentPermissions.Select(x => x.PermissionId).Contains(old.PermissionId))
                        {
                            connection.Delete(old, transaction);
                        }
                    }
                    // delete nonexisting items
                    foreach (var newPerm in currentPermissions)
                    {
                        if (!oldPermissions.Select(x => x.PermissionId).Contains(newPerm.PermissionId))
                        {
                            connection.Insert(newPerm, transaction);
                        }
                    }
                }
                transaction.Commit();
                connection.Close();
            }
            catch (Exception ex)
            {
                return new Operation<User>() { Fail = ex.Message };
            }
            return GetUserById(UserId);
        }

        public Operation<List<UserDataPermissionView>> GetUserDataPermissionView(int Id)
        {
            Operation<List<UserDataPermissionView>> operation = new Operation<List<UserDataPermissionView>>();
            try
            {
                operation.Value = connection.Query<UserDataPermissionView>("SP_GetUserDataPermissions " + Id.ToString()).ToList();
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<DataPermission> PostDataPermission(DataPermission dataPermission)
        {
           
            Operation<DataPermission> operation = new Operation<DataPermission>();

            IDbTransaction transaction = null;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();
                string query = "select COUNT(*) cnt from DataPermission where SourceId = {0} and PermissionId = {1}";
                query = string.Format(query,dataPermission.SourceId,dataPermission.PermissionId);
                int exists = connection.Query<int>(query,null,transaction).First();

                if (exists == 0)
                {
                    connection.Insert(dataPermission, transaction);
                }
               
                transaction.Commit();
                connection.Close();
                operation.Successful = true;
                operation.Value = dataPermission;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }

        public Operation<int> DeleteDataPermission(int dataPermissionId)
        {
            Operation<int> operation = new Operation<int>();
            try
            {
                DataPermission dataPermission = connection.Get<DataPermission>(dataPermissionId);
                connection.Delete(dataPermission);
                operation.Successful = true;
            }
            catch (Exception ex)
            {
                operation.Fail = ex.Message;
            }
            return operation;
        }
    }
}