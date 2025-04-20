using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Application.ImmobilienHausgelder.DTOs;
using BE.Application.ImmobilienTypes.DTOs;
using BE.Domain.Entities;

namespace BE.Application.ImmobilienOverviews.DTOs
{
    public class ImmobilienOverviewDto
    {
        public int Id { get; set; }
        public string ImmobilienName { get; set; }
        public ImmobilienTypeDto ImmobilienType { get; set; }
        public uint Kaufpreis { get; set; }
        public decimal ZimmerAnzahl { get; set; }
        public double Wohnflaeche { get; set; }
        public double BruttoMietRendite { get; set; }
        public decimal ImmobilienUeberschuss { get; set; }
        public ImmobilienHausgeldDto ImmobilienHausgeld { get; set; }
    }
}
