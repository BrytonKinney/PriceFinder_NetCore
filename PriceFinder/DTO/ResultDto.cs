using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceFinder.DTO
{
    public class ResultDto
    {
        public MatchDto Item { get; set; }
        public MatchDto Price { get; set; }
    }
}
