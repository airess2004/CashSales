﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Validation;

namespace Core.Interface.Service
{
    public interface IRetailSalesInvoiceService
    {
        IRetailSalesInvoiceValidator GetValidator();
        IRetailSalesInvoiceRepository GetRepository();
        IList<RetailSalesInvoice> GetAll();
        RetailSalesInvoice GetObjectById(int Id);
        RetailSalesInvoice CreateObject(RetailSalesInvoice retailSalesInvoice, IWarehouseService _warehouseService);
        RetailSalesInvoice UpdateObject(RetailSalesInvoice retailSalesInvoice, IRetailSalesInvoiceDetailService _retailSalesInvoiceDetailService);
        RetailSalesInvoice SoftDeleteObject(RetailSalesInvoice retailSalesInvoice, IRetailSalesInvoiceDetailService _retailSalesInvoiceDetailService);
        RetailSalesInvoice ConfirmObject(RetailSalesInvoice retailSalesInvoice, DateTime ConfirmationDate, int ContactId,
                                         IRetailSalesInvoiceDetailService _retailSalesInvoiceDetailService, IContactService _contactService,
                                         IPriceMutationService _priceMutationService, IReceivableService _receivableService,
                                         IRetailSalesInvoiceService _retailSalesInvoiceService, IWarehouseItemService _warehouseItemService,
                                         IWarehouseService _warehouseService, IItemService _itemService, IBarringService _barringService, IStockMutationService _stockMutationService);
        RetailSalesInvoice UnconfirmObject(RetailSalesInvoice retailSalesInvoice, IRetailSalesInvoiceDetailService _retailSalesInvoiceDetailService,
                                           IReceivableService _receivableService, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                           IWarehouseItemService _warehouseItemService, IWarehouseService _warehouseService, IItemService _itemService,
                                           IBarringService _barringService, IStockMutationService _stockMutationService);
        RetailSalesInvoice PaidObject(RetailSalesInvoice retailSalesInvoice, decimal AmountPaid, ICashBankService _cashBankService, IReceivableService _receivableService,
                                      IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                      IContactService _contactService, ICashMutationService _cashMutationService);
        RetailSalesInvoice UnpaidObject(RetailSalesInvoice retailSalesInvoice, IReceiptVoucherService _receiptVoucherService, IReceiptVoucherDetailService _receiptVoucherDetailService,
                                        ICashBankService _cashBankService, IReceivableService _receivableService, ICashMutationService _cashMutationService);
        bool DeleteObject(int Id);
    }
}
