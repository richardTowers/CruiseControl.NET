using System;
using System.Collections;
using System.Runtime.Remoting;
using ThoughtWorks.CruiseControl.Remote;

namespace ThoughtWorks.CruiseControl.WebDashboard.Dashboard
{
	public class LocalCruiseManagerAggregator
	{
		private ArrayList connectionExceptions = new ArrayList();
		private ArrayList projectDetails = new ArrayList();
		private IDictionary urlsForProjects = new Hashtable();

		public LocalCruiseManagerAggregator(IList urls) 
		{
			ConnectToRemoteServers(urls);
		}

		private void ConnectToRemoteServers(IList urls)
		{
			foreach (string url in urls)
			{
				try
				{
					ICruiseManager remoteCC = (ICruiseManager) RemotingServices.Connect(typeof(ICruiseManager), url);
					foreach (ProjectStatus status in remoteCC.GetProjectStatus())
					{
						projectDetails.Add(status);
						urlsForProjects[status.Name] = url;
					}
				}
				catch (Exception ex)
				{
					connectionExceptions.Add(new ConnectionException(url, ex));
				}
			}
		}

		public ArrayList ProjectDetails
		{
			get { return projectDetails; }
		}

		public IList ConnectionExceptions
		{
			get { return connectionExceptions; }
		}

		public void ForceBuild(string projectName)
		{
			ICruiseManager remoteCC = (ICruiseManager) RemotingServices.Connect(typeof(ICruiseManager), (string)urlsForProjects[projectName]);
			remoteCC.ForceBuild(projectName);
		}
	}

	public struct ConnectionException
	{
		private string _url;
		private Exception _exception;

		public ConnectionException(string URL, Exception exception)
		{
			this._url = URL;
			this._exception = exception;
		}

		public string URL
		{
			get { return _url; }
		}

		public string Message
		{
			get { return _exception.Message; }
		}

		public Exception Exception
		{
			get { return _exception; }
		}
	}
}
