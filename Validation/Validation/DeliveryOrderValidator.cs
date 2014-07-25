﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interface.Validation;
using Core.DomainModel;
using Core.Interface.Service;
using Core.Constants;

namespace Validation.Validation
{
    public class DeliveryOrderValidator : IDeliveryOrderValidator
    {

        public DeliveryOrder VCustomer(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            Customer customer = _customerService.GetObjectById(deliveryOrder.CustomerId);
            if (customer == null)
            {
                deliveryOrder.Errors.Add("CustomerId", "Tidak terasosiasi dengan customer");
            }
            return deliveryOrder;
        }

        public DeliveryOrder VDeliveryDate(DeliveryOrder deliveryOrder)
        {
            /* deliveryDate is never null
            if (deliveryOrder.DeliveryDate == null)
            {
                deliveryOrder.Errors.Add("DeliveryDate", "Tidak boleh kosong");
            }
            */
            return deliveryOrder;
        }

        public DeliveryOrder VIsConfirmed(DeliveryOrder deliveryOrder)
        {
            if (deliveryOrder.IsConfirmed)
            {
                deliveryOrder.Errors.Add("Generic", "Tidak boleh sudah dikonfirmasi");
            }
            return deliveryOrder;
        }

        public DeliveryOrder VHasDeliveryOrderDetails(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            IList<DeliveryOrderDetail> details = _deliveryOrderDetailService.GetObjectsByDeliveryOrderId(deliveryOrder.Id);
            if (!details.Any())
            {
                deliveryOrder.Errors.Add("DeliveryOrderDetail", "Tidak boleh tidak ada");
            }
            return deliveryOrder;
        }

        public DeliveryOrder VHasItemQuantity(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService, IItemService _itemService)
        {
            IList<DeliveryOrderDetail> details = _deliveryOrderDetailService.GetObjectsByDeliveryOrderId(deliveryOrder.Id);
            foreach (var detail in details)
            {
                Item item = _itemService.GetObjectById(detail.ItemId);
                if (item.Quantity < 0)
                {
                    deliveryOrder.Errors.Add ("Quantity", "Tidak boleh kurang dari 0");
                }
            }
            return deliveryOrder;
        }

        public DeliveryOrder VAllDetailsHaveBeenFinished(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            IList<DeliveryOrderDetail> details = _deliveryOrderDetailService.GetObjectsByDeliveryOrderId(deliveryOrder.Id);
            foreach (var detail in details)
            {
                if (!detail.IsFinished)
                {
                    deliveryOrder.Errors.Add("Generic", "Semua detail harus sudah selesai");
                    return deliveryOrder;
                }
            }
            return deliveryOrder;
        }

        public DeliveryOrder VCreateObject(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            VCustomer(deliveryOrder, _customerService);
            if (!isValid(deliveryOrder)) { return deliveryOrder; }
            VDeliveryDate(deliveryOrder);
            return deliveryOrder;
        }

        public DeliveryOrder VUpdateObject(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            VCustomer(deliveryOrder, _customerService);
            if (!isValid(deliveryOrder)) { return deliveryOrder; }
            VDeliveryDate(deliveryOrder);
            if (!isValid(deliveryOrder)) { return deliveryOrder; }
            VIsConfirmed(deliveryOrder);
            return deliveryOrder;
        }

        public DeliveryOrder VDeleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            VIsConfirmed(deliveryOrder);
            return deliveryOrder;
        }

        public DeliveryOrder VConfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService, IItemService _itemService)
        {
            VIsConfirmed(deliveryOrder);
            if (!isValid(deliveryOrder)) { return deliveryOrder; }
            VHasDeliveryOrderDetails(deliveryOrder, _deliveryOrderDetailService);
            /*
            if (isValid(deliveryOrder))
            {
                IList<DeliveryOrderDetail> details = _deliveryOrderDetailService.GetObjectsByDeliveryOrderId(deliveryOrder.Id);
                IDeliveryOrderDetailValidator detailvalidator = new DeliveryOrderDetailValidator();
                foreach (var detail in details)
                {
                    detailvalidator.VConfirmObject(detail, _itemService);
                    foreach (var error in detail.Errors)
                    {
                        deliveryOrder.Errors.Add(error.Key, error.Value);
                    }
                    if (!isValid(deliveryOrder)) { return deliveryOrder; }
                }
            }
             * */
            // TODO
            return deliveryOrder;
        }

        public DeliveryOrder VCompleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            VAllDetailsHaveBeenFinished(deliveryOrder, _deliveryOrderDetailService);
            return deliveryOrder;
        }

        public DeliveryOrder VUnconfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService, IItemService _itemService)
        {
            VHasItemQuantity(deliveryOrder, _deliveryOrderDetailService, _itemService);
            /*
             * if (isValid(deliveryOrder))
            {
                IList<DeliveryOrderDetail> details = _deliveryOrderDetailService.GetObjectsByDeliveryOrderId(deliveryOrder.Id);
                foreach (var detail in details)
                {
                    if (!_deliveryOrderDetailService.GetValidator().ValidUnconfirmObject(detail, _deliveryOrderDetailService, _itemService))
                    {
                        foreach (var error in detail.Errors)
                        {
                            deliveryOrder.Errors.Add(error.Key, error.Value);
                        }
                        if (!isValid(deliveryOrder)) { return deliveryOrder; }
                    }
                }
            }
            */
            return deliveryOrder;
        }

        public bool ValidCreateObject(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            VCreateObject(deliveryOrder, _customerService);
            return isValid(deliveryOrder);
        }

        public bool ValidUpdateObject(DeliveryOrder deliveryOrder, ICustomerService _customerService)
        {
            deliveryOrder.Errors.Clear();
            VUpdateObject(deliveryOrder, _customerService);
            return isValid(deliveryOrder);
        }

        public bool ValidDeleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            deliveryOrder.Errors.Clear();
            VDeleteObject(deliveryOrder, _deliveryOrderDetailService);
            return isValid(deliveryOrder);
        }

        public bool ValidConfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService, IItemService _itemService)
        {
            deliveryOrder.Errors.Clear();
            VConfirmObject(deliveryOrder, _deliveryOrderDetailService, _itemService);
            return isValid(deliveryOrder);
        }

        public bool ValidUnconfirmObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService, IItemService _itemService)
        {
            deliveryOrder.Errors.Clear();
            VUnconfirmObject(deliveryOrder, _deliveryOrderDetailService, _itemService);
            return isValid(deliveryOrder);
        }

        public bool ValidCompleteObject(DeliveryOrder deliveryOrder, IDeliveryOrderDetailService _deliveryOrderDetailService)
        {
            deliveryOrder.Errors.Clear();
            VCompleteObject(deliveryOrder, _deliveryOrderDetailService);
            return isValid(deliveryOrder);
        }

        public bool isValid(DeliveryOrder obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(DeliveryOrder obj)
        {
            string erroroutput = "";
            KeyValuePair<string, string> first = obj.Errors.ElementAt(0);
            erroroutput += first.Key + "," + first.Value;
            foreach (KeyValuePair<string, string> pair in obj.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += pair.Key + "," + pair.Value;
            }
            return erroroutput;
        }

    }
}