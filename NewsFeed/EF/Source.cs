namespace NewsFeed.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Source")]
    public partial class Source
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        public string ImagePath { get; set; }

        [StringLength(50)]
        public string Url { get; set; }

        [StringLength(50)]
        public string NameId { get; set; }
    }
}
