using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAWSAgustinCamposDynamoDB.Models
{
    [DynamoDBTable("productos")]
    public class Producto
    {
        [DynamoDBHashKey]
        [DynamoDBProperty("idproducto")]
        public string IdProducto { get; set; }
        [DynamoDBProperty("nombre")]
        public string Nombre { get; set; }
        [DynamoDBProperty("precio")]
        public int precio { get; set; }
    }
}
