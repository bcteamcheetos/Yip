using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarGalleryApp.CustomAttributes
{
    public class YearRangeAttribute:RangeAttribute
    {
        public YearRangeAttribute(): base(typeof(int), DateTime.Now.AddYears(-50).Year.ToString(), DateTime.Now.AddYears(1).Year.ToString())
        {

        }
    }
}
