using MessageTypes.Messages;

namespace MessageTypes
{
	/// <summary>
    /// Interface which define which methodes which needs to be known by the messages.
	/// </summary>
	public interface IServerApp
	{
        /// <summary>
        /// Method signature for method <c>VerifyLogin</c>.
        /// </summary>
        /// <param name="loginMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void VerifyLogin(ILoginMsg loginMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>CreateUser</c>.
        /// </summary>
        /// <param name="createUserMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void CreateUser(ICreateUserMsg createUserMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>CreateJob</c>.
        /// </summary>
        /// <param name="createJobMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void CreateJob(ICreateJobMsg createJobMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>RequestJobs</c>.
        /// </summary>
        /// <param name="requestJobsMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void RequestJobs(IRequestJobsMsg requestJobsMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>DownloadJob</c>.
        /// </summary>
        /// <param name="downloadJobMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void DownloadJob(IDownloadJobMsg downloadJobMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>GetMaterials</c>.
        /// </summary>
        /// <param name="getMaterialsMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void GetMaterials(IGetMaterialsMsg getMaterialsMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>ActivationCodeRequest</c>.
        /// </summary>
        /// <param name="activationCodeRequestMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
	    void ActivationCodeRequest(IActivationCodeRequestMsg activationCodeRequestMsg, IServer server);

        /// <summary>
        /// Method signature for method <c>ActivateUser</c>.
        /// </summary>
        /// <param name="activationMsg">The message recieved.</param>
        /// <param name="server">The server to reply to.</param>
        void ActivateUser(IActivationMsg activationMsg, IServer server);
	}
}

