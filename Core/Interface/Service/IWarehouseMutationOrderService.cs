﻿using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IWarehouseMutationOrderService
    {
        IWarehouseMutationOrderValidator GetValidator();
        IList<WarehouseMutationOrder> GetAll();
        WarehouseMutationOrder GetObjectById(int Id);
        Warehouse GetWarehouseFrom(WarehouseMutationOrder warehouseMutationOrder);
        Warehouse GetWarehouseTo(WarehouseMutationOrder warehouseMutationOrder);
        WarehouseMutationOrder CreateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        WarehouseMutationOrder UpdateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        WarehouseMutationOrder SoftDeleteObject(WarehouseMutationOrder warehouseMutationOrder);
        bool DeleteObject(int Id);
        WarehouseMutationOrder ConfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                             IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        WarehouseMutationOrder UnconfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                               IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        WarehouseMutationOrder CompleteObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService);
    }
}