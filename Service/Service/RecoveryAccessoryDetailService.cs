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
    public class RecoveryAccessoryDetailService : IRecoveryAccessoryDetailService
    {
        private IRecoveryAccessoryDetailRepository _repository;
        private IRecoveryAccessoryDetailValidator _validator;
        public RecoveryAccessoryDetailService(IRecoveryAccessoryDetailRepository _recoveryAccessoryDetailRepository, IRecoveryAccessoryDetailValidator _recoveryAccessoryDetailValidator)
        {
            _repository = _recoveryAccessoryDetailRepository;
            _validator = _recoveryAccessoryDetailValidator;
        }

        public IRecoveryAccessoryDetailValidator GetValidator()
        {
            return _validator;
        }

        public IRecoveryAccessoryDetailRepository GetRepository()
        {
            return _repository;
        }

        public IList<RecoveryAccessoryDetail> GetAll()
        {
            return _repository.GetAll();
        }

        public IList<RecoveryAccessoryDetail> GetObjectsByRecoveryOrderDetailId(int recoveryOrderDetailId)
        {
            return _repository.GetObjectsByRecoveryOrderDetailId(recoveryOrderDetailId);
        }

        public IList<RecoveryAccessoryDetail> GetObjectsByAccessoryId(int ItemId)
        {
            return _repository.GetObjectsByAccessoryId(ItemId);
        }

        public RecoveryAccessoryDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public RecoveryAccessoryDetail CreateObject(int RecoveryOrderDetailId, int AccessoryId, int Quantity, IRecoveryOrderDetailService _recoveryOrderDetailService, 
                                                    IItemService _itemService, IItemTypeService _itemTypeService)
        {
            RecoveryAccessoryDetail recoveryAccessoryDetail = new RecoveryAccessoryDetail
            {
                RecoveryOrderDetailId = RecoveryOrderDetailId,
                AccessoryId = AccessoryId,
                Quantity = Quantity
            };
            return this.CreateObject(recoveryAccessoryDetail, _recoveryOrderDetailService, _itemService, _itemTypeService);
        }

        public RecoveryAccessoryDetail CreateObject(RecoveryAccessoryDetail recoveryAccessoryDetail, IRecoveryOrderDetailService _recoveryOrderDetailService,
                                                    IItemService _itemService, IItemTypeService _itemTypeService)
        {
            recoveryAccessoryDetail.Errors = new Dictionary<String, String>();
            return (recoveryAccessoryDetail = _validator.ValidCreateObject(recoveryAccessoryDetail, _recoveryOrderDetailService, _itemService, _itemTypeService) ?
                                              _repository.CreateObject(recoveryAccessoryDetail) : recoveryAccessoryDetail);
        }

        public RecoveryAccessoryDetail UpdateObject(RecoveryAccessoryDetail recoveryAccessoryDetail, IRecoveryOrderDetailService _recoveryOrderDetailService,
                                                    IItemService _itemService, IItemTypeService _itemTypeService)
        {
            return (recoveryAccessoryDetail = _validator.ValidUpdateObject(recoveryAccessoryDetail, _recoveryOrderDetailService, _itemService, _itemTypeService) ?
                                              _repository.UpdateObject(recoveryAccessoryDetail) : recoveryAccessoryDetail);
        }

        public RecoveryAccessoryDetail SoftDeleteObject(RecoveryAccessoryDetail recoveryAccessoryDetail)
        {
            return (recoveryAccessoryDetail = _validator.ValidDeleteObject(recoveryAccessoryDetail) ? _repository.SoftDeleteObject(recoveryAccessoryDetail) : recoveryAccessoryDetail);
        }

        public RecoveryAccessoryDetail ConfirmObject(RecoveryAccessoryDetail recoveryAccessoryDetail, IRecoveryOrderService _recoveryOrderService, IRecoveryOrderDetailService _recoveryOrderDetailService, IItemService _itemService)
        {
            return (recoveryAccessoryDetail = _validator.ValidConfirmObject(recoveryAccessoryDetail, _recoveryOrderService, _recoveryOrderDetailService, _itemService) ?
                                              _repository.UpdateObject(recoveryAccessoryDetail) : recoveryAccessoryDetail);
        }

        public RecoveryAccessoryDetail UnconfirmObject(RecoveryAccessoryDetail recoveryAccessoryDetail, IRecoveryOrderService _recoveryOrderService, IRecoveryOrderDetailService _recoveryOrderDetailService, IItemService _itemService)
        {
            return (recoveryAccessoryDetail = _validator.ValidUnconfirmObject(recoveryAccessoryDetail, _recoveryOrderService, _recoveryOrderDetailService, _itemService) ?
                                              _repository.UpdateObject(recoveryAccessoryDetail) : recoveryAccessoryDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }
    }
}