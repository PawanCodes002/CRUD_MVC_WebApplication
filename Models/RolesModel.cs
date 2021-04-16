using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD_MVC_WebApplication.Models
{
    public class RolesModel
    {
        [Display(Name = "Role Id")]
        public int RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Display(Name = "Controller Name")]
        public string ControllerName { get; set; }

        [Display(Name = "Creation Date")]
        public string CreationDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Modification Date")]
        public string ModificationDate { get; set; }

        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

    }
}