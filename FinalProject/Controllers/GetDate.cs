using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Controllers
{
    public static class GetDate
    {
        public static DateTime GetDateNow (DateTime submitedDate)
        {
            
            DateTime rightDate = submitedDate.Date;
            DateTime returnDate = rightDate;
            return returnDate;
        }
    }
}