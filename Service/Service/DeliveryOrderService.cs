using Core.DomainModel;
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
    public class DeliveryOrderService : IDeliveryOrderService
    {
        private IDeliveryOrderRepository _repository;
        private IDeliveryOrderValidator _validator;

        public DeliveryOrderService(IDeliveryOrderRepository _deliveryOrderRepository, IDeliveryOrderValidator _deliveryOrderValidator)
        {
            _repository = _deliveryOrderRepository;
            _validator = _deliveryOrderValidator;
        }

        public IDeliveryOrderValidator GetValidator()
        {
            return _validator;
        }

        public IList<DeliveryOrder> GetAll()
        {
            return _repository.GetAll();
        }

        public DeliveryOrder GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public IList<DeliveryOrder> GetObjectsByCustomerId(int customerId)
        {
            return _repository.GetObjectsByCustomerId(customerId);
        }

        public DeliveryOrder CreateObject(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            deliveryOrder.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(deliveryOrder, _customerService) ? _repository.CreateObject(deliveryOrder) : deliveryOrder);
        }

        public DeliveryOrder CreateObject(int customerId, DateTime deliveryDate, ICustomerService _customerService)
        {
            DeliveryOrder deliveryOrder = new DeliveryOrder
            {
                CustomerId = customerId,
                DeliveryDate = deliveryDate
            };
            return this.CreateObject(deliveryOrder, _customerService);
        }

        public DeliveryOrder UpdateObject(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            return (_validator.ValidUpdateObject(deliveryOrder, _customerService) ? _repository.UpdateObject(deliveryOrder) : deliveryOrder);
        }

        public DeliveryOrder SoftDeleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            return (_validator.ValidDeleteObject(deliveryOrder, _deliveryOrderDetailService) ? _repository.SoftDeleteObject(deliveryOrder) : deliveryOrder);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        public DeliveryOrder ConfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService,
                                    ISalesOrderDetailService _salesOrderDetailService, IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidConfirmObject(deliveryOrder, _deliveryOrderDetailService, _itemService))
            {
                _repository.ConfirmObject(deliveryOrder);
            }
            return deliveryOrder;
        }

        public DeliveryOrder UnconfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService,
                                    IStockMutationService _stockMutationService, IItemService _itemService)
        {
            if (_validator.ValidUnconfirmObject(deliveryOrder, _deliveryOrderDetailService, _itemService))
            {
                _repository.UnconfirmObject(deliveryOrder);
            }
            return deliveryOrder;
        }

        public DeliveryOrder CompleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            return (deliveryOrder = _validator.ValidCompleteObject(deliveryOrder, _deliveryOrderDetailService) ? _repository.CompleteObject(deliveryOrder) : deliveryOrder);
        }
    }
}