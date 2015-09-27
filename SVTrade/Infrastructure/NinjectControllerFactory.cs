using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using SVTrade.Abstract;
using SVTrade.Concrete;

namespace SVTrade.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            // создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
              ? null
              : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // конфигурирование контейнера
            ninjectKernel.Bind<IRepository>().To<EFTradeRepository>();
        }
    }
}