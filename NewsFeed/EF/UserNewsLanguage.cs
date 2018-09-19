namespace NewsFeed.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserNewsLanguage")]
    public partial class UserNewsLanguage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NewsLanguage { get; set; }

        public DateTime DatedCreated { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
