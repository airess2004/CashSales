﻿using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class WarehouseMutationOrderService : IWarehouseMutationOrderService
    {
        private IWarehouseMutationOrderRepository _repository;
        private IWarehouseMutationOrderValidator _validator;
        public WarehouseMutationOrderService(IWarehouseMutationOrderRepository _warehouseMutationOrderRepository, IWarehouseMutationOrderValidator _warehouseMutationOrderValidator)
        {
            _repository = _warehouseMutationOrderRepository;
            _validator = _warehouseMutationOrderValidator;
        }

        public IWarehouseMutationOrderValidator GetValidator()
        {
            return _validator;
        }

        public IWarehouseMutationOrderRepository GetRepository()
        {
            return _repository;
        }

        public IList<WarehouseMutationOrder> GetAll()
        {
            return _repository.GetAll();
        }

        public Warehouse GetWarehouseFrom(WarehouseMutationOrder warehouseMutationOrder)
        {
            return _repository.GetWarehouseFrom(warehouseMutationOrder);
        }

        public Warehouse GetWarehouseTo(WarehouseMutationOrder warehouseMutationOrder)
        {
            return _repository.GetWarehouseFrom(warehouseMutationOrder);
        }

        public WarehouseMutationOrder GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public WarehouseMutationOrder CreateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService)
        {
            warehouseMutationOrder.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(warehouseMutationOrder, _warehouseService) ? _repository.CreateObject(warehouseMutationOrder) : warehouseMutationOrder);
        }

        public WarehouseMutationOrder UpdateObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseService _warehouseService)
        {
            return (warehouseMutationOrder = _validator.ValidUpdateObject(warehouseMutationOrder, _warehouseService) ? _repository.UpdateObject(warehouseMutationOrder) : warehouseMutationOrder);
        }

        public WarehouseMutationOrder SoftDeleteObject(WarehouseMutationOrder warehouseMutationOrder)
        {
            return (warehouseMutationOrder = _validator.ValidDeleteObject(warehouseMutationOrder) ? _repository.SoftDeleteObject(warehouseMutationOrder) : warehouseMutationOrder);
        }

        public WarehouseMutationOrder ConfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService,
                                                    IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService, IItemService _itemService,
                                                    IBarringService _barringService, IWarehouseItemService _warehouseItemService)
        {
            if (_validator.ValidConfirmObject(warehouseMutationOrder, _warehouseMutationOrderService, _warehouseMutationOrderDetailService,
                                              _itemService, _barringService, _warehouseItemService))
            {
                IList<WarehouseMutationOrderDetail> details = _warehouseMutationOrderDetailService.GetObjectsByWarehouseMutationOrderId(warehouseMutationOrder.Id);
                foreach (var detail in details)
                {
                    if (!_warehouseMutationOrderDetailService.GetValidator().ValidConfirmObject(detail, this, _itemService, _barringService, _warehouseItemService))
                    {
                        warehouseMutationOrder.Errors.Add("Generic", "Tidak dapat mengkonfirmasi stock adjustment");
                        return warehouseMutationOrder;
                    }
                }

                _repository.ConfirmObject(warehouseMutationOrder);
                foreach (var detail in details)
                {
                    _warehouseMutationOrderDetailService.ConfirmObject(detail, this, _itemService, _barringService, _warehouseItemService);
                }
            }
            return warehouseMutationOrder;
        }

        public WarehouseMutationOrder UnconfirmObject(WarehouseMutationOrder warehouseMutationOrder, IWarehouseMutationOrderService _warehouseMutationOrderService,
                                                    IWarehouseMutationOrderDetailService _warehouseMutationOrderDetailService, IItemService _itemService,
                                                    IBarringService _barringService, IWarehouseItemService _warehouseItemService)
        {
            if (_validator.ValidUnconfirmObject(warehouseMutationOrder, _warehouseMutationOrderService, _warehouseMutationOrderDetailService,
                                                _itemService, _barringService, _warehouseItemService))
            {
                IList<WarehouseMutationOrderDetail> details = _warehouseMutationOrderDetailService.GetObjectsByWarehouseMutationOrderId(warehouseMutationOrder.Id);
                foreach (var detail in details)
                {
                    if (!_warehouseMutationOrderDetailService.GetValidator().ValidUnconfirmObject(detail, this, _itemService, _barringService, _warehouseItemService))
                    {
                        warehouseMutationOrder.Errors.Add("Generic", "Tidak dapat mengkonfirmasi stock adjustment");
                        return warehouseMutationOrder;
                    }
                }

                _repository.UnconfirmObject(warehouseMutationOrder);
                foreach (var detail in details)
                {
                    _warehouseMutationOrderDetailService.UnconfirmObject(detail, this, _itemService, _barringService, _warehouseItemService);
                }
            }
            return warehouseMutationOrder;
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }
    }
}