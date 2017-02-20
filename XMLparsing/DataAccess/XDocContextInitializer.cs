using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using XMLParsing;

namespace XMLparsing.DataAccess
{
    public class XDocContextInitializer : DropCreateDatabaseIfModelChanges<XDocContext>
    {
    }
}
