using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface.Service
{
    public interface IPurchaseInvoiceService
    {
        IPurchaseInvoiceValidator GetValidator();
        IList<PurchaseInvoice> GetAll();
        PurchaseInvoice GetObjectById(int Id);
        IList<PurchaseInvoice> GetObjectsByPurchaseReceivalId(int purchaseReceivalId);
        PurchaseInvoice CreateObject(PurchaseInvoice purchaseInvoice, IPurchaseReceivalService _purchaseReceivalService);
        PurchaseInvoice CreateObject(int purchaseReceivalId, string description, int discount, bool isTaxable, DateTime InvoiceDate, DateTime DueDate, IPurchaseReceivalService _purchaseReceivalService);
        PurchaseInvoice UpdateObject(PurchaseInvoice purchaseInvoice, IPurchaseReceivalService _purchaseReceivalService, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService);
        PurchaseInvoice SoftDeleteObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService);
        bool DeleteObject(int Id);
        PurchaseInvoice ConfirmObject(PurchaseInvoice purchaseInvoice, DateTime ConfirmationDate, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService, IPurchaseOrderService _purchaseOrderService,
                                      IPurchaseReceivalService _purchaseReceivalService, IPurchaseReceivalDetailService _purchaseReceivalDetailService, IPayableService _payableService);
        PurchaseInvoice UnconfirmObject(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService,
                                        IPurchaseReceivalService _purchaseReceivalService, IPurchaseReceivalDetailService _purchaseReceivalDetailService,
                                        IPaymentVoucherDetailService _paymentVoucherDetailService, IPayableService _payableService);
        PurchaseInvoice CalculateAmountPayable(PurchaseInvoice purchaseInvoice, IPurchaseInvoiceDetailService _purchaseInvoiceDetailService);
    }
}