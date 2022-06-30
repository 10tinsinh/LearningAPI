using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using PrCore.DataContext;
using PrOpenApi.Models;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrOpenApi.BO
{
    public class UserNameBO: MongoAbsBO<Username>
    {
        private PrSystem.BO.SysErrorBO runErr = null;
        public UserNameBO(IMongoDatabase _PrDB):base(_PrDB, "pr_cuong_aaa")
        {

        }
        public Username LoadUser(string username)
        {
            var query = Builders<Username>.Filter.Where(
                e=>e.UserName == username
                );
            return FindOne(query);
            
        }
        public Username FindUser(ObjectId id)
        {
            var query = Builders<Username>.Filter.Where(
                e => e._id == id
                );
            return FindOne(query);
        }
        public object UpdateUser(Username username)
        {
            if(CheckUserSysCode(username) == false)
            {
                throw new Exception();
            }    
            var query = Builders<Username>.Filter
                            .Where(e => e.SysCode == username.SysCode);
            var data = Builders<Username>.Update
                .Set(x => x.UserName, username.UserName)
                .Set(x => x.Password, username.Password);
            Update(query, data);
            return FindOne(query);
        }
        public Object CreateUser(Username username)
        {
            if (string.IsNullOrEmpty(PrUtility.PrCommon.GetTrimString(username.SysCode)))
            {
                username.SysCode = PrCommon.GenerateUuid();
            }
            while (ExistsBySysCode(username.SysCode))
            {
                username.SysCode = PrCommon.GenerateUuid();
            }
            return username;

        }
        public void DeleteUser(string sysCode)
        {
            var username = new Username();
            username.SysCode = sysCode;
            if (CheckUserSysCode(username) == false)
            {
                throw new Exception();
            }
            var query = Builders<Username>.Filter.Where(
                x=>x.SysCode == sysCode
                );
            Remove(query);
        }
        public Object AllUser(string username)
        {
            var user = FindAll();
            
            if(!string.IsNullOrEmpty(username))
            {
                var value = Builders<Username>.Filter.Where(x => x.UserName.Contains(username));
                return Find(value);
                
            }
            return user;
            
        }
        private bool CheckUserSysCode(Username username)
        {
            var query = Builders<Username>.Filter.Where(
                x=>x.SysCode == username.SysCode
                );
            var data = FindOne(query);
            //SysCode: Exist = true, not Exist = false
            return data != null ? true : false;
        }
        private bool ExistsBySysCode(string SysCode)
        {
            try
            {
                SysCode = PrUtility.PrCommon.GetTrimString(SysCode);
                if (SysCode != "")
                {
                    var query = Builders<Username>.Filter
                    .Where(e => e.SysCode == SysCode
                    && PrCore.Para.SysStatusInUseList().Contains(e.SysStatus));
                    return (Count(query) > 0);
                }
            }
            catch (Exception ex)
            {
                runErr.Insert(System.Reflection.MethodBase.GetCurrentMethod(), ex);
            }

            return false;
        }
    }
}
