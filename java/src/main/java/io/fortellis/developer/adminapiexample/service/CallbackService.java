package io.fortellis.developer.adminapiexample.service;

import org.springframework.stereotype.Service;

import io.fortellis.developer.adminapiexample.models.CallbackRequest;

@Service
public class CallbackService {
    final String CONNECTION_SERVICE_URL = "https://subscriptions-dev.fortellis.io/connections";

    // Additional processing with the activate request can also be done before the connection callback
    public void connectionCallback (String connectionId) {
        System.out.println("Starting connection callback");
        String connectionCallbackUrl = String.format("%s/%s/callback", CONNECTION_SERVICE_URL, connectionId);

        System.out.println("Connection callback url: " + connectionCallbackUrl);

        // Call connection callback with oauth2 token here
        CallbackRequest acceptedConnection = new CallbackRequest(CallbackRequest.ACCEPTED);
        System.out.println("Simulating call to connection callback with body: " + acceptedConnection);
        // TODO: Make call to connection callback with authorization

        System.out.println("Connection callback successful");
    }
}