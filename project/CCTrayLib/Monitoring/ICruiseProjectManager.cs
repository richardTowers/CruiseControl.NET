
namespace ThoughtWorks.CruiseControl.CCTrayLib.Monitoring
{
	/// <summary>
	/// This is like ICruiseManager, but relates to an individual project.
	/// In due course, it may well be that cruise exposes a per-project 
	/// interface. Till then, this allows us to write code as if it does.
	/// </summary>
	public interface ICruiseProjectManager
	{
		void ForceBuild();
		void FixBuild(string fixingUserName);
		void CancelPendingRequest();
		string ProjectName { get; }
	}
}
