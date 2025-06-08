using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    //sealed class => مينفعش حد يورث منه
    public sealed class ProductNotFoundException(int id ) : NotFoundException($"Product With Id = {id} is not Found")
    {

    }
}
