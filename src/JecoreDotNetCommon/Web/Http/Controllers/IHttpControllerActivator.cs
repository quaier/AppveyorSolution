using System;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace JecoreDotNetCommon.Web.Http.Controllers
{
    public interface IHttpControllerActivator
    {
        IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType);
    }

    public class ExceptionHandlingControllerActivator : IHttpControllerActivator
    {
        private IHttpControllerActivator _concreteActivator;

        public ExceptionHandlingControllerActivator(IHttpControllerActivator httpControllerActivator)
        {
            _concreteActivator = httpControllerActivator;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            try
            {
                return _concreteActivator.Create(request, controllerDescriptor, controllerType);
            }
            catch
            {
                return _concreteActivator.Create(request, controllerDescriptor, controllerType);
            }
        }
    }
}
