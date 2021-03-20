using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace abpos_test.Models.DB
{
    public partial class Vehiculos
    {

        public int IdVehiculo { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Anio { get; set; }
    }
}
