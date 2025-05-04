using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectCar.Models
{
    [Table("CARS")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Column("MODEL")]
        public string Model { get; set; }

        [Column("MANUFACTURE")]
        public DateTime Manufacture { get; set; }

        [Column("POWER")]
        public int Power { get; set; }

        [Column("TURBO")]
        public bool Turbo { get; set; }

        [Column("BRANDID")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
    }
}