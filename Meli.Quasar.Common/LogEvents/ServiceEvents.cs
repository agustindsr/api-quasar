using Microsoft.Extensions.Logging;

namespace Meli.Quasar.Common.LogEvents
{
    public class ServiceEvents
    {
        public static EventId ExceptionCallingTopSecret { get; } = new EventId(10000, "Excepción llamando a Meli.Quasar.Service.TopSecret.");
        public static EventId ExceptionCallingTopSecretSplit { get; } = new EventId(10001, "Excepción llamando a Meli.Quasar.Service.TopSecretSplit.");
        public static EventId ExceptionCallingGetLocation { get; } = new EventId(10002, "Excepción llamando a Meli.Quasar.Service.GetLocation.");
        public static EventId ExceptionCallingGetMessage { get; } = new EventId(10003, "Excepción llamando a Meli.Quasar.Service.GetMessage.");


        public static EventId CallingTopSecret { get; } = new EventId(10010, "Llamada al servicio Meli.Quasar.Service.TopSecret.");
        public static EventId CallingTopSecretSplit { get; } = new EventId(10011, "Llamada al servicio Meli.Quasar.Service.TopSecretSplit.");
        public static EventId CallingGetSatellitesSplits { get; } = new EventId(100112, "Llamada al servicio Meli.Quasar.Service.GetSatellitesSplits.");
        public static EventId CallingDeleteSatelliteSplits { get; } = new EventId(100113, "Llamada al servicio Meli.Quasar.Service.DeleteSatelliteSplits.");
        public static EventId CallingAddOrUpdateSatelliteSplit { get; } = new EventId(10014, "Llamada al servicio Meli.Quasar.Service.AddOrUpdateSatelliteSplit.");
        public static EventId CallingGetLocation { get; } = new EventId(10015, "Llamada al servicio Meli.Quasar.Service.GetLocation.");
        public static EventId CallingGetMessage { get; } = new EventId(10016, "Llamada al servicio Meli.Quasar.Service.GetMessage.");

        //Responses
        public static EventId ReponseTopSecret { get; } = new EventId(10020, "Respuesta del servicio Meli.Quasar.Service.TopSecret.");
        public static EventId ReponseTopSecretSplit { get; } = new EventId(10021, "Respuesta del servicio Meli.Quasar.Service.TopSecretSplit.");
        public static EventId ReponseGetSatellitesSplits { get; } = new EventId(100122, "Respuesta del servicio Meli.Quasar.Service.GetSatellitesSplits.");
        public static EventId ReponseDeleteSatelliteSplits { get; } = new EventId(100123, "Respuesta del servicio Meli.Quasar.Service.DeleteSatelliteSplits.");
        public static EventId ReponseAddOrUpdateSatelliteSplit { get; } = new EventId(10024, "Respuesta del servicio Meli.Quasar.Service.AddOrUpdateSatelliteSplit.");
        public static EventId ReponseGetLocation { get; } = new EventId(10025, "Respuesta del servicio Meli.Quasar.Service.GetLocation.");
        public static EventId ReponseGetMessage { get; } = new EventId(10026, "Respuesta del servicio Meli.Quasar.Service.GetMessage.");
    }
}
