using System.Runtime.Serialization;

namespace Spectre.Models.Msi
{
    [DataContract]
    public class Preparation
    {
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public int Id { get; private set; }

        public Preparation(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}