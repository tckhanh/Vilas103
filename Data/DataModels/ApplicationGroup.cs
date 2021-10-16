using Data.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.DataModels
{
    [Table("ApplicationGroups")]
    public class ApplicationGroup : Auditable
    {
        [Key]
        public string Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }

        public virtual IEnumerable<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public virtual IEnumerable<ApplicationUserGroup> ApplicationUserGroups { get; set; }

        public ApplicationGroup()
        {
            Id = Guid.NewGuid().ToString();
            ApplicationRoleGroups = new List<ApplicationRoleGroup>();
            ApplicationUserGroups = new List<ApplicationUserGroup>();
        }

        public ApplicationGroup(string name) : this()
        {
            Name = name;
        }

        public ApplicationGroup(string name, string description) : this(name)
        {
            this.Description = description;
        }
    }
}