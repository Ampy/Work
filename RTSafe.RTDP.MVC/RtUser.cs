using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Collections;

namespace RTSafe.RTDP.MVC
{
    public class RtUser : IPrincipal
    {
        private System.Security.Principal.IIdentity identity;
        private List<RTDP.Permission.Models.Role> roleList = new List<Permission.Models.Role>();

        public RtUser(string userID, string password) 
       { 
          // 
          // TODO: 在此处添加构造函数逻辑 
          // 
          identity = new RtIdentity(userID,password); 
          if(identity.IsAuthenticated) 
          { 
             //如果通过验证则获取该用户的Role，这里可以修改为从数据库中 
             //读取指定用户的Role并将其添加到Role中，本例中直接为用户添加一个Admin角色 
             //roleList = new ArrayList(); 
             //roleList.Add("Admin"); 

            RTSafe.RTDP.Permission.Models.User rtUser = new RTSafe.RTDP.Permission.Models.User();
            rtUser.UserId = Guid.NewGuid();
            rtUser.Name = userID;

            RTSafe.RTDP.Permission.Models.Role role = new RTSafe.RTDP.Permission.Models.Role();

            RTSafe.RTDP.Permission.Models.Operation operation= new RTSafe.RTDP.Permission.Models.Operation();

            operation.OperationId = Guid.Parse("{6396B227-ACD8-475A-9865-F38FD6A19556}");
            //role.Operations.Add(operation);

            roleList.Add(role);
          } 
          else 
          { 
             // do nothing 
          } 
       }

        public List<RTDP.Permission.Models.Role> RoleList
        {
            get
            {
                return roleList;
            }
        }
        #region IPrincipal 成员

        public System.Security.Principal.IIdentity Identity
        {
            get
            {
                // TODO:   添加 MyPrincipal.Identity getter 实现 
                return identity;
            }
            set
            {
                identity = value;
            }
        }

        public bool IsInRole(string role)
        {
            // TODO:   添加 MyPrincipal.IsInRole 实现 
            return roleList.SingleOrDefault(c => c.Name == role) != null;
        }

        public bool IsInPermission(string permissionId)
       {
           Guid perId = Guid.Parse(permissionId);
           bool hasPermission = false;

           foreach (var r in roleList)
           {
               //hasPermission = r.Operations.SingleOrDefault(c => c.ID == perId) != null;
           }
           return hasPermission;
       }

        #endregion
    }


}
