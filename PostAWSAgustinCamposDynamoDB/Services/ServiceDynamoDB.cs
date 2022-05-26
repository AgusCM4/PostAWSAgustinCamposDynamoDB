using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using PostAWSAgustinCamposDynamoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostAWSAgustinCamposDynamoDB.Services
{
    public class ServiceDynamoDB
    {
        private DynamoDBContext context;

        public ServiceDynamoDB()
        {
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();
            this.context = new DynamoDBContext(client);
        }

        public async Task CreateProductoAsync(Producto pro)
        {
            await this.context.SaveAsync<Producto>(pro);
        }

        public async Task DeleteProductoAsync(string idPro)
        {
            await this.context.DeleteAsync<Producto>(idPro);
        }

        public async Task<Producto> FindProductoAsync(int idPro)
        {
            //SI ESTAMOS BUSCANDO POR SU PARTITION KEY (PRIMARY KEY)
            //TENEMOS UN METODO PARA CARGAR SU BUSQUEDA
            return await this.context.LoadAsync<Producto>(idPro);
        }

        public async Task<List<Producto>> GetProductoAsync()
        {
            //LO PRIMERO ES RECUPERAR LA TABLA DE LOS OBJETOS
            Table tabla = this.context.GetTargetTable<Producto>();
            //PARA RECUPERAR TODOS O PARA BUSCAR, DEBEMOS INDICARLO 
            //CON UN OBJETO ScanOptions
            var scanOptions = new ScanOperationConfig();
            var results = tabla.Scan(scanOptions);
            //LO QUE DEVUELVE EN EL MOMENTO DE BUSCAR
            //SON OBJETOS DE LA CLASE Document
            List<Document> data = await results.GetNextSetAsync();
            //DEBEMOS CONVERTIR LOS OBJETOS Document A NUESTRO TIPO
            var productos = this.context.FromDocuments<Producto>(data);
            return productos.ToList();
        }
    }
}
