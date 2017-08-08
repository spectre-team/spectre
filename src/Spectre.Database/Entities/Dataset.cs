using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spectre.Database.Entities
{
    public class Dataset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Index]
        public string hash { get; set; }
        [Index]
        public string FriendlyName { get; set; }
        public string Owner { get; set; }
        public DateTime UploadTime { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}
