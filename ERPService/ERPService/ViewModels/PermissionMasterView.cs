using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPService.ViewModels
{
    public class PermissionMasterView
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public string KeyWord { get; set; }

        public bool IsActive { get; set; }

        public bool IsPermitted { get; set; }
    }
}
