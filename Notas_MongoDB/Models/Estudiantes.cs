using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Notas_MongoDB.Models
{
    public class Estudiantes
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }
        
        [BsonElement("nota1")]
        public int Nota1 { get; set; }

        [BsonElement("nota2")]
        public int Nota2 { get; set; }
    }
}
