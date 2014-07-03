﻿using Core.DomainModel;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Validation
{
    public interface ICoreIdentificationValidator
    { 
        CoreIdentification VHasUniqueCode(CoreIdentification coreIdentification, ICoreIdentificationService _coreIdentificationService);
        CoreIdentification VInHouseOrHasCustomer(CoreIdentification coreIdentification, ICustomerService _customerService);
        CoreIdentification VQuantity(CoreIdentification coreIdentification);
        CoreIdentification VHasIdentifiedDate(CoreIdentification coreIdentification);
        CoreIdentification VHasNotBeenConfirmed(CoreIdentification coreIdentification);
        CoreIdentification VHasBeenConfirmed(CoreIdentification coreIdentification);
        CoreIdentification VIsInRecoveryOrder(CoreIdentification coreIdentification, IRecoveryOrderService _recoveryOrderService);
        CoreIdentification VHasCoreIdentificationDetails(CoreIdentification coreIdentification, ICoreIdentificationDetailService _coreIdentificationDetailService);
        CoreIdentification VQuantityEqualNumberOfDetails(CoreIdentification coreIdentification, ICoreIdentificationDetailService _coreIdentificationDetailService);
        CoreIdentification VQuantityIsInStock(CoreIdentification coreIdentification, ICoreIdentificationDetailService _coreIdentificationDetailService,
                                              ICoreBuilderService _coreBuilderService);
        CoreIdentification VCreateObject(CoreIdentification coreIdentification, ICoreIdentificationService _coreIdentificationService, ICustomerService _customerService);
        CoreIdentification VUpdateObject(CoreIdentification coreIdentification, ICoreIdentificationService _coreIdentificationService, ICustomerService _customerService);
        CoreIdentification VDeleteObject(CoreIdentification coreIdentification, IRecoveryOrderService _recoveryOrderService);
        CoreIdentification VConfirmObject(CoreIdentification coreIdentification, ICoreIdentificationDetailService _coreIdentificationDetailService,
                                          ICoreBuilderService _coreBuilderService);
        CoreIdentification VUnconfirmObject(CoreIdentification coreIdentification, IRecoveryOrderService _recoveryOrderService);
        bool ValidCreateObject(CoreIdentification coreIdentification, ICoreIdentificationService _coreIdentificationService, ICustomerService _customerService);
        bool ValidUpdateObject(CoreIdentification coreIdentification, ICoreIdentificationService _coreIdentificationService, ICustomerService _customerService);
        bool ValidDeleteObject(CoreIdentification coreIdentification, IRecoveryOrderService _recoveryOrderService);
        bool ValidConfirmObject(CoreIdentification coreIdentification, ICoreIdentificationDetailService _coreIdentificationDetailService, ICoreBuilderService _coreBuilderService);
        bool ValidUnconfirmObject(CoreIdentification coreIdentification, IRecoveryOrderService _recoveryOrderService);
        bool isValid(CoreIdentification coreIdentification);
        string PrintError(CoreIdentification coreIdentification);
    }
}