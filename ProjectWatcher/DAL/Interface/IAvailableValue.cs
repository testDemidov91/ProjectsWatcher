﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IAvailableValue
    {
        Object GetValue();

        Int32 Id { get; }

    }
}
