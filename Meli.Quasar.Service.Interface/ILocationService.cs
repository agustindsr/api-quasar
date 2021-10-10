
using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Domain.Entities;

namespace Meli.Quasar.Service.Interface
{
    public interface ILocationService
    {
        PointDto GetLocation(PointDistance point1, PointDistance point2, PointDistance point3);
    }
}
