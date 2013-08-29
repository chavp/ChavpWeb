using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Web.Models
{
    public class InMemoryRoleProvider
        : RoleProvider
    {
        

        public InMemoryRoleProvider()
        {
            
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                MvcApplication.UserRoleDic.Add(username, roleNames);
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (MvcApplication.UserRoleDic.ContainsKey(username))
                return MvcApplication.UserRoleDic[username];

            return null;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (MvcApplication.UserRoleDic.ContainsKey(username))
                return MvcApplication.UserRoleDic[username].Contains(roleName);

            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}