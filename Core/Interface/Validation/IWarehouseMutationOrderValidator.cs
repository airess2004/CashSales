﻿using Core.DomainModel;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Validation
{
    public interface IWarehouseMutationOrderValidator
    {
        WarehouseMutationOrder VHasDifferentWarehouse(WarehouseMutationOrder warehouseMutationOrder);
        WarehouseMutationOrder VHasWarehouseFrom(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        WarehouseMutationOrder VHasWarehouseTo(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        WarehouseMutationOrder VHasWarehouseMutationOrderDetails(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService);
        WarehouseMutationOrder VHasNotBeenConfirmed(WarehouseMutationOrder warehouseMutationOrder);
        WarehouseMutationOrder VHasBeenConfirmed(WarehouseMutationOrder warehouseMutationOrder);
        WarehouseMutationOrder VDetailsAreVerifiedConfirmable(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                                              IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        WarehouseMutationOrder VAllDetailsHaveNotBeenFinished(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService);
        WarehouseMutationOrder VAllDetailsHaveBeenFinished(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService);
        WarehouseMutationOrder VCreateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        WarehouseMutationOrder VUpdateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        WarehouseMutationOrder VDeleteObject(WarehouseMutationOrder warehouseMutationOrder);
        WarehouseMutationOrder VConfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                              IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        WarehouseMutationOrder VUnconfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                                IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        WarehouseMutationOrder VCompleteObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService);
        bool ValidCreateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        bool ValidUpdateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService);
        bool ValidDeleteObject(WarehouseMutationOrder warehouseMutationOrder);
        bool ValidConfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        bool ValidUnconfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService,
                                  IItemService _itemService, IBarringService _barringService, IWarehouseItemService _warehouseItemService);
        bool ValidCompleteObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService);
        bool isValid(WarehouseMutationOrder warehouseMutationOrder);
        string PrintError(WarehouseMutationOrder warehouseMutationOrder);
    }
}