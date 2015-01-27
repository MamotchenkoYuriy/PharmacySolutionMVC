using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PharmacySolution.Core;
using PharmacySolution.Web.Core.Models;

namespace PharmacySolution.Web
{
    public class AutoMapperConfigurator
    {
        public AutoMapperConfigurator()
        {
        }

        public void Configure()
        {
            // Для Pharmacy
            Mapper.CreateMap<Pharmacy, PharmacyViewModel>();
            Mapper.CreateMap<PharmacyViewModel, Pharmacy>();
            
            // Для Medicament
            Mapper.CreateMap<Medicament, MedicamentViewModel>();
            Mapper.CreateMap<MedicamentViewModel, Medicament>();

            // для MedicamentPriceHistory
            Mapper.CreateMap<MedicamentPriceHistory, MedicamentPriceHistoryViewModel>();
            Mapper.CreateMap<MedicamentPriceHistoryViewModel, MedicamentPriceHistory>();

            // для Order
            Mapper.CreateMap<Order, OrderViewModel>();
            Mapper.CreateMap<OrderViewModel, Order>();

            // для Order
            Mapper.CreateMap<OrderDetails, OrderDetailsCreateViewModel>();
            Mapper.CreateMap<OrderDetailsCreateViewModel, OrderDetails>();
            Mapper.CreateMap<OrderDetailsListViewModel, OrderDetails>();
            Mapper.CreateMap<OrderDetails, OrderDetailsListViewModel>();

            // для Order
            Mapper.CreateMap<Storage, StorageCreateViewModel>();
            Mapper.CreateMap<Storage, StorageViewModel>();
            Mapper.CreateMap<StorageCreateViewModel, Storage>();
            Mapper.CreateMap<StorageViewModel, Storage>();
        }
    }
}