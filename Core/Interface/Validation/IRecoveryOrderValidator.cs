﻿using Core.DomainModel;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Validation
{
    public interface IRecoveryOrderValidator
    {
        RecoveryOrder VHasUniqueCode(RecoveryOrder recoveryOrder);
        RecoveryOrder VHasCoreIdentification(RecoveryOrder recoveryOrder, ICoreIdentificationService _coreIdentificationService);
        RecoveryOrder VHasRecoveryOrderDetails(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService);
        RecoveryOrder VIsConfirmedRecoveryAccessoryDetails(RecoveryOrder recoveryOrder, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService);
        RecoveryOrder VIsPackedRecoveryOrderDetails(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService);
        RecoveryOrder VIsAssembledRecoveryOrderDetails(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService);
        RecoveryOrder VHasQuantityReceived(RecoveryOrder recoveryOrder);
        RecoveryOrder VQuantityReceivedLessThanCoreIdentification(RecoveryOrder recoveryOrder, ICoreIdentificationService _coreIdentificationService);
        RecoveryOrder VQuantityReceivedEqualDetails(RecoveryOrder recoveryOrder);
        RecoveryOrder VIsConfirmed(RecoveryOrder recoveryOrder);
        RecoveryOrder VIsNotConfirmed(RecoveryOrder recoveryOrder);
        RecoveryOrder VIsFinished(RecoveryOrder recoveryOrder);
        RecoveryOrder VIsNotFinished(RecoveryOrder recoveryOrder);

        RecoveryOrder VCreateObject(RecoveryOrder recoveryOrder, ICoreIdentificationService _coreIdentificationService);
        RecoveryOrder VUpdateObject(RecoveryOrder recoveryOrder, ICoreIdentificationService _coreIdentificationService);
        RecoveryOrder VDeleteObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService);
        RecoveryOrder VConfirmObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        RecoveryOrder VUnconfirmObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        RecoveryOrder VFinishObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        RecoveryOrder VUnfinishObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);

        bool ValidCreateObject(RecoveryOrder recoveryOrder, ICoreIdentificationService _coreIdentificationService);
        bool ValidUpdateObject(RecoveryOrder recoveryOrder, ICoreIdentificationService _coreIdentificationService);
        bool ValidDeleteObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService);
        bool ValidConfirmObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        bool ValidUnconfirmObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        bool ValidFinishObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        bool ValidUnfinishObject(RecoveryOrder recoveryOrder, IRecoveryOrderDetailService _recoveryOrderDetailService, IRecoveryAccessoryDetailService _recoveryAccessoryDetailService, IItemService _itemService);
        bool isValid(RecoveryOrder recoveryOrder);
        string PrintError(RecoveryOrder recoveryOrder);
    }
}