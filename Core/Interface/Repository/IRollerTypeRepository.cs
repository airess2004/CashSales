﻿using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IRollerTypeRepository : IRepository<RollerType>
    {
        IList<RollerType> GetAll();
        RollerType GetObjectById(int Id);
        RollerType CreateObject(RollerType rollerType);
        RollerType UpdateObject(RollerType rollerType);
        RollerType SoftDeleteObject(RollerType rollerType);
        bool DeleteObject(int Id);
    }
}