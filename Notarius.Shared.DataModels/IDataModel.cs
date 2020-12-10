using System;
using System.Collections.Generic;
using System.Text;

namespace Notarius.Shared.DataModel
{
    public interface IDataModel
    {
        bool IsDirty { get; set; }
    }
}
