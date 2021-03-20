using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace abpos_test.Models.DB
{
    public partial class Vehiculos
    {

        public int IdVehiculo { get; set; }
        [Required]
        [MaxLength(100)]
        public string Marca { get; set; }
        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; }
        [Required]
        [MaxLength(10)]
        public string Anio { get; set; }
    }
}
