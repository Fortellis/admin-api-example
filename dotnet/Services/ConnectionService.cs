using System;
using System.Collections.Generic;
using AdminAPIImplementation.Models;

namespace AdminAPIImplementation.Services
{
    public class ConnectionService
    {
        public static ActivateResponse activateConnection(ActivateRequest activateRequest)
        {
            Console.WriteLine("Activate request received: ", activateRequest.ToString());

            /********************************************/
            /*    TODO - Perisist activation request.   */
            /********************************************/

            ActivateResponse activateResponse = new ActivateResponse();

            List<ActionLink> links = new List<ActionLink>();
            ActionLink link = new ActionLink();
            link.href = "https://example.com/deactivate";
            link.rel = "deactivate";
            link.method = "POST";
            link.title = "title";
            links.Add(link);

            activateResponse.links = links;

            return activateResponse;
        }

        public static DeactivateResponse deactivateConnection(string connectionId)
        {
            Console.WriteLine("Deactivate request received, connectionId:  {0}", connectionId);

            /***********************************************/
            /* TODO - Perform deactivation business logic. */
            /***********************************************/

            DeactivateResponse deactivateResponse = new DeactivateResponse();

            List<ActionLink> links = new List<ActionLink>();
            ActionLink link = new ActionLink();
            link.href = "https://example.com/activate";
            link.rel = "activate";
            link.method = "POST";
            link.title = "title";
            links.Add(link);

            deactivateResponse.links = links;

            return deactivateResponse;
        }
    }
}
