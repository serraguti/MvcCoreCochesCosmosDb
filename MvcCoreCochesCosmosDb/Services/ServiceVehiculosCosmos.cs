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
                , "/location");
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
    }
}
