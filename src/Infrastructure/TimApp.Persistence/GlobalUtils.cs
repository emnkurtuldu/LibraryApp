using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimApp.Persistence
{
    public static class GlobalUtils
    {
        public static readonly List<DateTime> PublicHolidays = new()
        {
            new DateTime(DateTime.Now.Year,1,1), // Yılbaşı
            new DateTime(DateTime.Now.Year,4,23), // 23 Nisan
            new DateTime(DateTime.Now.Year,5,19), // 19 Mayıs
            new DateTime(DateTime.Now.Year,7,15), // 15 Temmuz
            new DateTime(DateTime.Now.Year,8,30), // 30 Ağustos
            new DateTime(DateTime.Now.Year,10,29) // 29 Ekim
        };

        public static double CalculateDelayPenaltyFee(double day)
        {
            int x = 0;
            int y = 1;
            double penaltyFee = 0;

            // Birinci gün ve öncesi ceza yok
            if (day <= 1)
                return penaltyFee;

            // İkinci gün
            penaltyFee = y * GlobalParams.rentDelayFee + penaltyFee;

            for (int i = 3; i <= day; i++)
            {
                int z = x + y;
                // Sonraki günler
                penaltyFee = z * GlobalParams.rentDelayFee + penaltyFee;

                x = y;
                y = z;

            }
            return penaltyFee;
        }
    
        private static bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private static bool IsPublicHoliday(DateTime date)
        {
            return PublicHolidays.Contains(date);
        }

        public static DateTime GetBookDeliveryDate(DateTime date)
        {
            int cnt = 0;
            do
            {
                if (!IsWeekend(date) && !IsPublicHoliday(date))
                {
                    cnt++;
                }
                date = date.AddDays(1);
            } while (cnt < GlobalParams.bookDeliveryDay);
            return date;
        }


    }
}
