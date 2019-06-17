// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using OpenRiaServices.DomainControllers.Server.EntityFramework;
using OpenRiaServices.DomainControllers.Server.EntityFramework.Metadata;
using OpenRiaServices.DomainControllers.Server.Test.Models;
using Xunit;

namespace OpenRiaServices.DomainControllers.Server.Test
{
    public class DomainControllerDescriptionTest
    {
        // verify that the LinqToEntitiesMetadataProvider is registered by default for
        // LinqToEntitiesDomainController<T> derived types
        [Fact]
        public void EFMetadataProvider_AttributeInference()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor
            {
                Configuration = configuration,
                ControllerType = typeof(NorthwindEFTestController),
            };
            DomainControllerDescription description = GetDomainControllerDescription(typeof(NorthwindEFTestController));
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product));

            // verify key attribute
            Assert.NotNull(properties["ProductID"].Attributes[typeof(KeyAttribute)]);
            Assert.Null(properties["ProductName"].Attributes[typeof(KeyAttribute)]);

            // verify StringLengthAttribute
            StringLengthAttribute sla = (StringLengthAttribute)properties["ProductName"].Attributes[typeof(StringLengthAttribute)];
            Assert.NotNull(sla);
            Assert.Equal(40, sla.MaximumLength);

            // verify RequiredAttribute
            RequiredAttribute ra = (RequiredAttribute)properties["ProductName"].Attributes[typeof(RequiredAttribute)];
            Assert.NotNull(ra);
            Assert.False(ra.AllowEmptyStrings);

            // verify association attribute
            AssociationAttribute aa = (AssociationAttribute)properties["Category"].Attributes[typeof(AssociationAttribute)];
            Assert.NotNull(aa);
            Assert.Equal("Category_Product", aa.Name);
            Assert.True(aa.IsForeignKey);
            Assert.Equal("CategoryID", aa.ThisKey);
            Assert.Equal("CategoryID", aa.OtherKey);

            // verify metadata from "buddy class"
            PropertyDescriptor pd = properties["QuantityPerUnit"];
            sla = (StringLengthAttribute)pd.Attributes[typeof(StringLengthAttribute)];
            Assert.NotNull(sla);
            Assert.Equal(777, sla.MaximumLength);
            EditableAttribute ea = (EditableAttribute)pd.Attributes[typeof(EditableAttribute)];
            Assert.False(ea.AllowEdit);
            Assert.True(ea.AllowInitialValue);
        }

        [Fact]
        public void EFTypeDescriptor_ExcludedEntityMembers()
        {
            PropertyDescriptor pd = TypeDescriptor.GetProperties(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product))["EntityState"];
            Assert.True(LinqToEntitiesTypeDescriptor.ShouldExcludeEntityMember(pd));

            pd = TypeDescriptor.GetProperties(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product))["EntityState"];
            Assert.True(LinqToEntitiesTypeDescriptor.ShouldExcludeEntityMember(pd));

            pd = TypeDescriptor.GetProperties(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product))["SupplierReference"];
            Assert.True(LinqToEntitiesTypeDescriptor.ShouldExcludeEntityMember(pd));
        }

        [Fact]
        public void DescriptionValidation_NonAuthorizationFilter()
        {
            Assert.Throws<NotSupportedException>(
                () => GetDomainControllerDescription(typeof(InvalidController_NonAuthMethodFilter)),
                String.Format(String.Format(Resource.InvalidAction_UnsupportedFilterType, "InvalidController_NonAuthMethodFilter", "UpdateProduct")));
        }

        /// <summary>
        /// Verify that associated entities are correctly registered in the description when
        /// using explicit data contracts
        /// </summary>
        [Fact]
        public void AssociatedEntityTypeDiscovery_ExplicitDataContract()
        {
            DomainControllerDescription description = GetDomainControllerDescription(typeof(IncludedAssociationTestController_ExplicitDataContract));
            List<Type> entityTypes = description.EntityTypes.ToList();
            Assert.Equal(8, entityTypes.Count);
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Order)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Order_Detail)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Customer)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Employee)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Category)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Supplier)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Shipper)));
        }

        /// <summary>
        /// Verify that associated entities are correctly registered in the description when
        /// using implicit data contracts
        /// </summary>
        [Fact]
        public void AssociatedEntityTypeDiscovery_ImplicitDataContract()
        {
            DomainControllerDescription description = GetDomainControllerDescription(typeof(IncludedAssociationTestController_ImplicitDataContract));
            List<Type> entityTypes = description.EntityTypes.ToList();
            Assert.Equal(3, entityTypes.Count);
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.Customer)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.Order)));
            Assert.True(entityTypes.Contains(typeof(OpenRiaServices.DomainControllers.Server.Test.Models.Order_Detail)));
        }

        /// <summary>
        /// Verify that DomainControllerDescription correctly handles Task returning actions and discovers
        /// entity types from those as well (unwrapping the task type).
        /// </summary>
        [Fact]
        public void TaskReturningGetActions()
        {
            DomainControllerDescription desc = GetDomainControllerDescription(typeof(TaskReturningGetActionsController));
            Assert.Equal(4, desc.EntityTypes.Count());
            Assert.True(desc.EntityTypes.Contains(typeof(City)));
            Assert.True(desc.EntityTypes.Contains(typeof(CityWithInfo)));
            Assert.True(desc.EntityTypes.Contains(typeof(CityWithEditHistory)));
            Assert.True(desc.EntityTypes.Contains(typeof(State)));
        }

        internal static DomainControllerDescription GetDomainControllerDescription(Type controllerType)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            HttpControllerDescriptor controllerDescriptor = new HttpControllerDescriptor
            {
                Configuration = configuration,
                ControllerType = controllerType
            };
            return DomainControllerDescription.GetDescription(controllerDescriptor);
        }
    }

    internal class InvalidController_NonAuthMethodFilter : DomainController
    {
        // attempt to apply a non-auth filter
        [TestActionFilter]
        public void UpdateProduct(OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product product)
        {
        }

        // the restriction doesn't apply for non CUD actions
        [TestActionFilter]
        public IEnumerable<OpenRiaServices.DomainControllers.Server.Test.Models.EF.Product> GetProducts()
        {
            return null;
        }
    }

    internal class TaskReturningGetActionsController : DomainController
    {
        public Task<IEnumerable<City>> GetCities()
        {
            return null;
        }

        public Task<State> GetState(string name)
        {
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class TestActionFilterAttribute : ActionFilterAttribute
    {
    }

    internal class IncludedAssociationTestController_ExplicitDataContract : LinqToEntitiesDomainController<OpenRiaServices.DomainControllers.Server.Test.Models.EF.NorthwindEntities>
    {
        public IQueryable<OpenRiaServices.DomainControllers.Server.Test.Models.EF.Order> GetOrders() { return null; }
    }

    internal class IncludedAssociationTestController_ImplicitDataContract : DomainController
    {
        public IQueryable<OpenRiaServices.DomainControllers.Server.Test.Models.Customer> GetCustomers() { return null; }
    }
}
