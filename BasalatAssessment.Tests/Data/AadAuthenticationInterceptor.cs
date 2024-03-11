using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace BasalatAssessment.Tests.Data
{
    /// <summary>
    /// Connection interceptor that sets the connection access token to the specified AAD access token.
    /// </summary>
    public class AadAuthenticationInterceptor : DbConnectionInterceptor
    {
        private readonly string? _accessToken;

        public AadAuthenticationInterceptor(string? accessToken = null)
        {
            _accessToken = accessToken;
        }

        public override InterceptionResult ConnectionOpening(DbConnection connection, ConnectionEventData eventData, InterceptionResult result)
        {
            if (_accessToken != null)
            {
                var sqlConnection = (SqlConnection)connection;
                sqlConnection.AccessToken = _accessToken;
            }

            return result;
        }

        public override ValueTask<InterceptionResult> ConnectionOpeningAsync(DbConnection connection, ConnectionEventData eventData, InterceptionResult result,
            CancellationToken cancellationToken = default)
        {
            if (_accessToken != null)
            {
                var sqlConnection = (SqlConnection)connection;
                sqlConnection.AccessToken = _accessToken;
            }

            return ValueTask.FromResult(result);
        }
    }
}
