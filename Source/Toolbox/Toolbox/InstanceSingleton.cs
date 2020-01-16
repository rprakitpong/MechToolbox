using System;
using System.Threading.Tasks;

namespace ToolBox
{
    public sealed class InventorInstanceSingleton
    {
        private static InventorInstanceSingleton instance = null;
        private static readonly object padlock = new object();

        Inventor.Application app;

        InventorInstanceSingleton()
        {
        }

        public static InventorInstanceSingleton Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InventorInstanceSingleton();
                    }
                    return instance;
                }
            }
        }

        public async Task<Inventor.Application> getInventor()
        {
            if (app == null)
            {
                return await Task<Inventor.Application>.Run(() => {

                    app = Activator.CreateInstance(Type.GetTypeFromProgID("Inventor.Application")) as Inventor.Application;


                    return app;

                });
            }

            return app;
        }

        public Inventor.PartDocument open
    }
}
