using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveSystem
{
    public class SaveSystemManager
    {
        ISaveSystem saveSystem;
        public ISaveSystem SaveSystem => saveSystem;
        
        public SaveSystemManager(ISaveSystem saveSystem)
        {
            this.saveSystem = saveSystem;
        }
    }
}
