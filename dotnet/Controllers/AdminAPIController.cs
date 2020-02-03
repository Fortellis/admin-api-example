using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdminAPIImplementation.Models;
using AdminAPIImplementation.Services;
using System;
using Newtonsoft.Json;

namespace AdminAPIImplementation.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class AdminAPIController : ControllerBase
    {

        public IFortellisConnectionCallbackService FortellisConnectionCallbackService { get; }
        public AdminAPIController(IFortellisConnectionCallbackService fortellisConnectionCallbackService)
        {
            FortellisConnectionCallbackService = fortellisConnectionCallbackService;
        }

        [HttpGet]
        [Route("health")]
        public Object health()
        {
            return new { status = "UP" };
        }

        [Authorize]
        [HttpPost]
        [Route("activate")]
        public ActionResult<ActivateResponse> Post([FromBody] ActivateRequest request)
        {
            try {
            ActivateResponse activateResponse = ConnectionService.activateConnection(request);
            return activateResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process activate request, message - {ex.Message}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("deactivate/{connectionId}")]
        public ActionResult<DeactivateResponse> Deactivate(string connectionId)
        {
            try
            {
            DeactivateResponse deactivateResponse = ConnectionService.deactivateConnection(connectionId);
            return deactivateResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process deactivate request, message -  {ex.Message}");
                return StatusCode(500);
            }
        }

        //   THIS API IS NOT REQUIRED FOR ADMIN IMPLEMENTATION,   //
        //    ONLY TO DEMONSTRATE CONNECTION CALLBACK API         //
        [Authorize]
        [HttpPost]
        [Route("approve/activate/request/{connectionId}")]
        public Object approveActivateRequest(string connectionId)
        {
            try
            {
                // Step 1: Get accessToken from Fortellis authorization server using the apikey/secret on your API Implementation.
                //         You can find the credentials in your developer account- https://developer.fortellis.io   
                string accessToken = FortellisConnectionCallbackService.getFortellisToken();

                // Step 2: Invoke Fortellis Connection Callback API with accepted/rejected status.
                FortellisConnectionCallbackService.approveFortellisActivationRequest(accessToken, connectionId, CallbackRequest.ACCEPTED);
                return new { message = "Request activation successful." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to activate request, message -  {ex.Message}");
                return new { message = "Failed to activate request", error = ex.Message };
            }
        }
    }
}
