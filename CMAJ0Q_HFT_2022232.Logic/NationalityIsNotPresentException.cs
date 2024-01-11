using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMAJ0Q_HFT_2022232.Logic
{
    [Serializable]
    public class NationalityIsNotPresentException : Exception
    {
        public NationalityIsNotPresentException(string nationalityName) : base($"Currently no one has this nationality: {nationalityName}")
        {

        }
    }
}
