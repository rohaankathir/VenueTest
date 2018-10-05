using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using Autofac.Integration.WebApi;

namespace WebAPI.Library.Filter
{
    public class WebApiRoleAccessFilter : IAutofacAuthorizationFilter
    {
        private readonly IWebApiRoleAccessService _IWebApiRoleAccessService;
        public WebApiRoleAccessFilter(IWebApiRoleAccessService iWebApiRoleAccessService)
        {
            _IWebApiRoleAccessService = iWebApiRoleAccessService;
        }
        public Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var result = _IWebApiRoleAccessService.OnAuthorizationAsync(actionContext, cancellationToken);
            return result;
        }
    }
}