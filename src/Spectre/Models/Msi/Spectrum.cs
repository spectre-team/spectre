using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Spectre.Models.Msi
{
    [DataContract]
    public class Spectrum
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public IEnumerable<double> Mz { get; set; }
        [DataMember]
        public IEnumerable<double> Intensities { get; set; }
        [DataMember]
        public int X { get; set; }
        [DataMember]
        public int Y { get; set; }
    }
}