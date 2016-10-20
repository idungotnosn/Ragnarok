using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.userinteraction
{
    class UserInteractionFactory
    {
        public static UserInteraction createUserInteraction(){
            return new CommandLineInteraction();
        }

    }
}
