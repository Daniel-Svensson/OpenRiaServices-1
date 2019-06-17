// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Linq;
using OpenRiaServices.DomainControllers.Server.EntityFramework;
using OpenRiaServices.DomainControllers.Server.Test.Models.EF;

namespace OpenRiaServices.DomainControllers.Server.Test
{
    public class NorthwindEFTestController : LinqToEntitiesDomainController<NorthwindEntities>
    {
        public IQueryable<Product> GetProducts()
        {
            return this.ObjectContext.Products;
        }

        public void InsertProduct(Product product)
        {
        }

        public void UpdateProduct(Product product)
        {
        }

        protected override NorthwindEntities CreateObjectContext()
        {
            return new NorthwindEntities(TestHelpers.GetTestEFConnectionString());
        }
    }
}

namespace OpenRiaServices.DomainControllers.Server.Test.Models.EF
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
        internal sealed class ProductMetadata
        {
            [Editable(false, AllowInitialValue = true)]
            [StringLength(777, MinimumLength = 2)]
            public string QuantityPerUnit { get; set; }

            [Range(0, 1000000)]
            public string UnitPrice { get; set; }
        }
    }
}
