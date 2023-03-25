using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SportsStore.Models
{
    //Salvare i prodotti senza conoscere i dettagli di
   // come sono memorizzati o come la classe di implementazione li fornirà Infatti IEnumerable e IQuerable potrebbero essere simili
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
    }
}