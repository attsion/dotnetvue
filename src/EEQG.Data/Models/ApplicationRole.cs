using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EEQG.ServerData.Models
{
    public class ApplicationRole : IdentityRole
    {
        public const string 管理员 = "管理员";
        public const string 编辑员 = "编辑员";
        public const string 用户 = "用户";
        public const string 管理员和编辑员 = 管理员 + "," + 编辑员;
        public static string[] RoleNames = new string[] { 管理员, 编辑员, 用户 };
    }
}
