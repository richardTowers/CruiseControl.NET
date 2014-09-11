using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;
using System.Web;

namespace ThoughtWorks.CruiseControl.WebDashboard.MVC.ASPNET
{
    /// <summary>
    /// This HttpHandler only provides read-only access to session state
    /// which allows Asp.NET to handle multiple concurrent requests.
    /// </summary>
    /// <remarks>
    /// At the moment we're not implementing anything here, we're just
    /// adding the <see cref="IReadOnlySessionState"/> to <see cref="HttpHandler"/>.
    /// See <see cref="HttpHandler"/> for all method implementations.
    /// </remarks>
    public class ReadonlySessionStateHttpHandler : HttpHandler, IReadOnlySessionState
    {

    }
}