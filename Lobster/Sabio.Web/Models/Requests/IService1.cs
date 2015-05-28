using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Sabio.Web.Models.Requests
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();
    }
}
