using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFilesAPI;

namespace $safeprojectname$
{
	/// <summary>
	/// Simple configuration.
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// Reference to a test class.
		/// </summary>
		[MFClass( Required = false )]
		public MFIdentifier TestClassID = "FailAlias";
	}

	/// <summary>
	/// Simple vault application to demonstrate VAF.
	/// </summary>
	public class VaultApplication : VaultApplicationBase
	{
		/// <summary>
		/// Simple configuration member. MFConfiguration-attribute will cause this member to be loaded from the named value storage from the given namespace and key.
		/// Here we override the default alias of the Configuration class with a default of the config member.
		/// The default will be written in the named value storage, if the value is missing.
		/// Use Named Value Manager to change the configurations in the named value storage.
		/// </summary>
		[MFConfiguration( "$safeprojectname$", "config" )]
		private Configuration config = new Configuration() { TestClassID = "TestClassAlias" };
		
		/// <summary>
		/// The method, that is run when the vault goes online.
		/// </summary>
		protected override void StartApplication()
		{
			// Start writing extension method output to the event log every ten seconds. The background operation will continue until the vault goes offline.
			this.BackgroundOperations.StartRecurringBackgroundOperation( "Recurring Hello World Operation", TimeSpan.FromSeconds( 10 ), () =>
				{
					// Prepare input for the extension method.
					string input = "Hello from $safeprojectname$";

					// Execute the extension method. Wrapping code to an extension method ensures transactionality for the vault operations.
					string output = this.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod( "TestVaultExtensionMethod", input );

					// Report extension method output to event log.
					SysUtils.ReportInfoToEventLog( output );
				} );
		}

		/// <summary>
		/// A vault extension method, that will be installed to the vault with the application.
		/// The vault extension method can be called through the API.
		/// </summary>
		/// <param name="env">The event handler environment for the method.</param>
		/// <returns>The output string to the caller.</returns>
		[VaultExtensionMethod( "TestVaultExtensionMethod", RequiredVaultAccess = MFVaultAccess.MFVaultAccessNone )]
		private string TestVaultExtensionMethod( EventHandlerEnvironment env )
		{
			// Return the input and the alias and id of the test class. If the class is missing, ID is -1.
			return env.Input + ": " + config.TestClassID.Alias + ": " + config.TestClassID.ID;
		}
	}
}