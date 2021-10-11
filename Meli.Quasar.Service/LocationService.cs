using AutoMapper;
using Meli.Quasar.Common.Dtos.Communication;
using Meli.Quasar.Common.LogEvents;
using Meli.Quasar.Domain.Entities;
using Meli.Quasar.Domain.Exceptions;
using Meli.Quasar.Service.Interface;
using Microsoft.Extensions.Logging;
using System;

namespace Meli.Quasar.Service
{
   public class LocationService : ILocationService
    {
        private readonly ILogger<LocationService> _logger;
        private readonly IMapper _mapper;

        public LocationService(ILogger<LocationService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public PointDto GetLocation(PointDistance point1, PointDistance point2, PointDistance point3)
        {
            try
            {
                Point position = Trilateration(point1, point2, point3);

                return _mapper.Map<PointDto>(position);
            }
            catch (CalculatePositionException ex)
            {
                _logger.LogError(ServiceEvents.ExceptionCallingGetLocation, ex.Message, ex);
                throw;
            }

        }

        private static Point Trilateration(PointDistance p1, PointDistance p2, PointDistance p3)
        {
            try
            {
                double[] a = new double[3];
                double[] b = new double[3];
                double c, d, f, g, h;
                double[] i = new double[2];
                double k;
                c = p2.X - p1.X;
                d = p2.Y - p1.Y;
                f = (180 / Math.PI) * Math.Acos(Math.Abs(c) / Math.Abs(Math.Sqrt(Math.Pow(c, 2) + Math.Pow(d, 2))));
                if ((c > 0 && d > 0)) { f = 360 - f; }
                else if ((c < 0 && d > 0)) { f = 180 + f; }
                else if ((c < 0 && d < 0)) { f = 180 - f; }
                a = C(c, d, B(A(D(p2.Distance))), f);
                b = C(p3.X - p1.X, p3.Y - p1.Y, B(A(D(p3.Distance))), f);
                g = (Math.Pow(B(A(D(p1.Distance))), 2) - Math.Pow(a[2], 2) + Math.Pow(a[0], 2)) / (2 * a[0]);
                h = (Math.Pow(B(A(D(p1.Distance))), 2) - Math.Pow(b[2], 2) - Math.Pow(g, 2) + Math.Pow(g - b[0], 2) + Math.Pow(b[1], 2)) / (2 * b[1]);
                i = C(g, h, 0, -f);
                i[0] = i[0] + p1.X - 0.086;
                i[1] = i[1] + p1.Y - 0.004;
                k = E(i[0], i[1], p1.X, p1.Y);
                return new Point(i[0], i[1]);
            }
            catch (Exception)
            {
                throw new CalculatePositionException();
            }
        }
        private static double A(double a) { return a * 7.2; }
        private static double B(double a) { return a / 900000; }
        private static double[] C(double a, double b, double c, double d) { return new double[] { a * Math.Cos((Math.PI / 180) * d) - b * Math.Sin((Math.PI / 180) * d), a * Math.Sin((Math.PI / 180) * d) + b * Math.Cos((Math.PI / 180) * d), c }; }
        private static double D(double a) { return 730.24198315 + 52.33325511 * a + 1.35152407 * Math.Pow(a, 2) + 0.01481265 * Math.Pow(a, 3) + 0.00005900 * Math.Pow(a, 4) + 0.00541703 * 180; }
        private static double E(double a, double b, double c, double d) { double e = Math.PI, f = e * a / 180, g = e * c / 180, h = b - d, i = e * h / 180, j = Math.Sin(f) * Math.Sin(g) + Math.Cos(f) * Math.Cos(g) * Math.Cos(i); if (j > 1) { j = 1; } j = Math.Acos(j); j = j * 180 / e; j = j * 60 * 1.1515; j = j * 1.609344; return j; }
    }
}
