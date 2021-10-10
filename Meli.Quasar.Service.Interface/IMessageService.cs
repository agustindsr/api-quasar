using System.Collections.Generic;

namespace Meli.Quasar.Service.Interface
{
    public interface IMessageService
    {
        string GetMessage(List<List<string>> messages);
    }
}
