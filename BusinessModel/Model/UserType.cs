using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DummyProjectApi.BusinessModel.Model
{
    [Table("UserType")]
    public class UserType
    {
        /// <summary>
        /// Id is Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name That Defines User
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User Is Enum That Defines Types Of User
        /// </summary>
        public Types User { get; set; }
        /// <summary>
        /// CreatedDate Date Of Creation
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// UpdatedDate Date Of Modification
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// IsActive Identifies Whether The Record Is Activated or Has Been Deactivated
        /// </summary>
        public bool IsActive { get; set; } = false;
    }

    public enum Types
    {
        Admin=1,
        Vendor=2,
        User=3
    }
}
