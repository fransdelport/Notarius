using System;
using System.Collections.Generic;
using System.Text;

namespace Notarius.Client.DataModel
{
    public interface IDataModelUI
    {
        bool IsDirty { get; set; }
    }
}
