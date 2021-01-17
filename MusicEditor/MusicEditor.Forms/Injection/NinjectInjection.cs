using MusicEditor.Bussines.APIs;
using MusicEditor.Bussines.Models;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicEditor.Forms.Injection
{
    class NinjectInjection : NinjectModule
    {
        public override void Load()
        {

            Bind<FormGestion>().To<FormGestion>();

            Bind<IMusicApi>().To<MusicAPI>().InSingletonScope();
            Bind<IRepositoryManager>().ToMethod(
                rm => 
                {
                    var context = new MusicContext();

                    return new RepositoryManager(
                                                   new Repository<Music>(context),
                                                   context);


                });
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            Bind<DbContext>().To<MusicContext>().InTransientScope();
        }
    }
}
