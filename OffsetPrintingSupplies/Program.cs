﻿using Core.DomainModel;
using Core.Interface.Service;
using Data.Context;
using Data.Repository;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation.Validation;

namespace OffsetPrintingSupplies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ICoreBuilderService _coreBuilderService;
            ICoreIdentificationService _coreIdentificationService;
            ICoreIdentificationDetailService _coreIdentificationDetailService;
            ICustomerService _customerService;
            IItemService _itemService;
            IItemTypeService _itemTypeService;
            IMachineService _machineService;
            IRecoveryAccessoryDetailService _recoveryAccessoryDetailService;
            IRecoveryOrderDetailService _recoveryOrderDetailService;
            IRecoveryOrderService _recoveryOrderService;
            IRollerBuilderService _rollerBuilderService;
            IRollerTypeService _rollerTypeService;
        
            var db = new OffsetPrintingSuppliesEntities();
            using (db)
            {
                db.DeleteAllTables();
                _coreBuilderService = new CoreBuilderService(new CoreBuilderRepository(), new CoreBuilderValidator());
                _coreIdentificationDetailService = new CoreIdentificationDetailService(new CoreIdentificationDetailRepository(), new CoreIdentificationDetailValidator());
                _coreIdentificationService = new CoreIdentificationService(new CoreIdentificationRepository(), new CoreIdentificationValidator());
                _customerService = new CustomerService(new CustomerRepository(), new CustomerValidator());
                _itemService = new ItemService(new ItemRepository(), new ItemValidator());
                _itemTypeService = new ItemTypeService(new ItemTypeRepository(), new ItemTypeValidator());
                _machineService = new MachineService(new MachineRepository(), new MachineValidator());
                _recoveryOrderDetailService = new RecoveryOrderDetailService(new RecoveryOrderDetailRepository(), new RecoveryOrderDetailValidator());
                _recoveryOrderService = new RecoveryOrderService(new RecoveryOrderRepository(), new RecoveryOrderValidator());
                _recoveryAccessoryDetailService = new RecoveryAccessoryDetailService(new RecoveryAccessoryDetailRepository(), new RecoveryAccessoryDetailValidator());
                _rollerBuilderService = new RollerBuilderService(new RollerBuilderRepository(), new RollerBuilderValidator());
                _rollerTypeService = new RollerTypeService(new RollerTypeRepository(), new RollerTypeValidator());

                _itemTypeService.CreateObject("Accessory", "Accessory");
                _itemTypeService.CreateObject("Bearing", "Bearing");
                _itemTypeService.CreateObject("Blanket", "Blanket");
                _itemTypeService.CreateObject("Chemical", "Chemical");
                _itemTypeService.CreateObject("Compound", "Compound");
                _itemTypeService.CreateObject("Consumable", "Consumable");
                _itemTypeService.CreateObject("Core", "Core");
                _itemTypeService.CreateObject("Glue", "Glue");
                _itemTypeService.CreateObject("Underpacking", "Underpacking");
                _itemTypeService.CreateObject("Roller", "Roller");

                _rollerTypeService.CreateObject("Damp", "Damp");
                _rollerTypeService.CreateObject("Found DT", "Found DT");
                _rollerTypeService.CreateObject("Ink Form X", "Ink Form X");
                _rollerTypeService.CreateObject("Ink Dist D", "Ink Dist D");
                _rollerTypeService.CreateObject("Ink Dist M", "Ink Dist M");
                _rollerTypeService.CreateObject("Ink Dist E", "Ink Dist E");
                _rollerTypeService.CreateObject("Ink Duct B", "Ink Duct B");
                _rollerTypeService.CreateObject("Ink Dist H", "Ink Dist H");
                _rollerTypeService.CreateObject("Ink Form W", "Ink Form W");
                _rollerTypeService.CreateObject("Ink Dist HQ", "Ink Dist HQ");
                _rollerTypeService.CreateObject("Damp Form DQ", "Damp Form DQ");
                _rollerTypeService.CreateObject("Ink Form Y", "Ink Form Y");

                Item item = new Item()
                {
                    ItemTypeId = _itemTypeService.GetObjectByName("Accessory").Id,
                    Sku = "ABC1001",
                    Name = "ABC",
                    Category = "ABC123",
                    UoM = "Pcs",
                    Quantity = 0
                };
                item = _itemService.CreateObject(item, _itemTypeService);

                Machine machine = new Machine()
                {
                    Code = "M00001",
                    Name = "Machine 00001",
                    Description = "Machine"
                };
                machine = _machineService.CreateObject(machine);
                CoreBuilder coreBuilder = new CoreBuilder()
                {
                    BaseSku = "CB00001",
                    SkuNewCore = "CB00001N",
                    SkuUsedCore = "CB00001U",
                    Name = "CoreBuilder00001",
                    Category = "X"
                };
                coreBuilder = _coreBuilderService.CreateObject(coreBuilder, _itemService, _itemTypeService);
                CoreIdentification coreIdentification = new CoreIdentification()
                {
                    CustomerId = null,
                    Code = "CI0001",
                    Quantity = 1,
                    IsInHouse = true,
                    IdentifiedDate = DateTime.Now
                };
                coreIdentification = _coreIdentificationService.CreateObject(coreIdentification, _customerService);
                CoreIdentificationDetail coreIdentificationDetail = new CoreIdentificationDetail()
                {
                    CoreIdentificationId = coreIdentification.Id,
                    DetailId = 1,
                    MaterialCase = 2,
                    CoreBuilderId = coreBuilder.Id,
                    RollerTypeId = _rollerTypeService.GetObjectByName("Found DT").Id,
                    MachineId = machine.Id,
                    RD = 12,
                    CD = 12,
                    RL = 12,
                    WL = 12,
                    TL = 12
                };
                coreIdentificationDetail = _coreIdentificationDetailService.CreateObject(coreIdentificationDetail,
                                           _coreIdentificationService, _coreBuilderService, _rollerTypeService, _machineService);

                Machine machine2 = new Machine()
                {
                    Code = "M00002",
                    Name = " ",
                    Description = "Machine"
                };
                machine2 = _machineService.CreateObject(machine2);
                if (!_machineService.GetValidator().isValid(machine2)) { Console.WriteLine( _machineService.GetValidator().PrintError(machine2) ); }

                Machine machine3 = new Machine()
                {
                    Code = "M00001",
                    Name = "Copycopycopy",
                    Description = "Machine"
                };
                machine2 = _machineService.CreateObject(machine3);
                if (!_machineService.GetValidator().isValid(machine3)) { Console.WriteLine( _machineService.GetValidator().PrintError(machine3) ); }

                machine = _machineService.SoftDeleteObject(machine, _rollerBuilderService, _coreIdentificationDetailService);


                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();
            }

        }
    }
}