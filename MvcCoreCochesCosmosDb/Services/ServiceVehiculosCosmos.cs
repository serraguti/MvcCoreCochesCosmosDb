using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using MvcCoreCochesCosmosDb.Models;

namespace MvcCoreCochesCosmosDb.Services
{
    public class ServiceVehiculosCosmos
    {
        private CosmosClient cosmosClient;
        private Container containerCosmos;

        public ServiceVehiculosCosmos
            (CosmosClient cosmosClient
            , Container containerCosmos)
        {
            this.cosmosClient = cosmosClient;
            this.containerCosmos = containerCosmos;
        }

        public async Task CreateDatabaseAsync()
        {
            ContainerProperties properties = new ContainerProperties("containercoches"
                , "/id");
            await this.cosmosClient.CreateDatabaseAsync("cochescosmos");
            await this.cosmosClient.GetDatabase("cochescosmos")
                .CreateContainerAsync(properties);
        }

        public async Task AddVehiculoAsync(Vehiculo car)
        {
            await
                this.containerCosmos.CreateItemAsync<Vehiculo>(car
                , new PartitionKey(car.Id));
        }

        public async Task<List<Vehiculo>> GetVehiculosAsync()
        {
            var query = this.containerCosmos.GetItemQueryIterator<Vehiculo>();
            List<Vehiculo> coches = new List<Vehiculo>();
            while (query.HasMoreResults)
            {
                var results = await query.ReadNextAsync();
                coches.AddRange(results);
            }
            return coches;
        }

        public async Task<List<Vehiculo>> GetVehiculosMarcaAsync(string marca)
        {
            //DEBEMOS HACER LA CONSULTA SQL
            string sql = "select * from c where c.Marca='" + marca + "'";
            //DEBEMOS REALIZAR UNA DEFINICION CON LA CONSULTA SQL
            QueryDefinition definition = new QueryDefinition(sql);
            var query =
                this.containerCosmos.GetItemQueryIterator<Vehiculo>(definition);
            List<Vehiculo> cars = new List<Vehiculo>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                cars.AddRange(response.ToList());
            }
            return cars;
        }

        public async Task UpdateVehiculoAsync(Vehiculo car)
        {
            //TENEMOS UN METODO QUE ES UPSERT
            //SI LO ENCUENTRA, LO SUSTITUYE Y, SI NO LO ENCUENTRA
            //LO INSERTA
            await this.containerCosmos.UpsertItemAsync<Vehiculo>(car, new PartitionKey(car.Id));
        }

        public async Task<Vehiculo> FindVehiculoAsync(string id)
        {
            ItemResponse<Vehiculo> response =
                await this.containerCosmos.ReadItemAsync<Vehiculo>
                (id, new PartitionKey(id));
            return response.Resource;
        }

        public async Task DeleteVehiculoAsync(string id)
        {
            await this.containerCosmos.DeleteItemAsync<Vehiculo>(id, new PartitionKey(id));
        }
    }
}
