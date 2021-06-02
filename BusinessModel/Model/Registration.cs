using AutoMapper.Configuration.Annotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DummyProjectApi.BusinessModel.Model
{
    [Table("Registration")]
    public class Registration
    {
        /// <summary>
        /// Id Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name Of User
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email Of User
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Mobile Number Of User
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Password Of User
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Date Of Creation
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Date Of Updation
        /// </summary>
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Field To check whether user has been deleted or not
        /// </summary>
        public bool IsActive { get; set; } = false;
    }
}
