using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;

namespace SportsStore.Models.ViewModels
{
    //Viene utilizzata com model in Index e modificata in HomeController con LINQ
    public class ProductsListViewModel
    {

            //Lista per i prodotti
            public IEnumerable<Product> Products { get; set; }

            //Lista per le pagine
            public PagingInfo PagingInfo { get; set; }
            
            //Indicare la categoria useremo questa property
            public string CurrentCategory { get; set; }

    }
}
