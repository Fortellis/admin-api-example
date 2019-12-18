package io.fortellis.developer.adminapiexample.controller;

import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.servlet.HandlerInterceptor;
import org.springframework.web.servlet.handler.MappedInterceptor;
import org.springframework.web.bind.annotation.RequestBody;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Bean;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;

import io.fortellis.developer.adminapiexample.models.ActivateRequest;
import io.fortellis.developer.adminapiexample.models.ActivateResponse;
import io.fortellis.developer.adminapiexample.models.DeactivateResponse;
import io.fortellis.developer.adminapiexample.service.CallbackService;
import io.fortellis.developer.adminapiexample.service.ConnectionService;

@RestController
public class AdminApiController {

    private static final ThreadLocal<String> connectionIdLocal = new ThreadLocal<String>();

    @Autowired
    private ConnectionService connectionService;

    @Autowired
    private CallbackService callbackService;

    @PostMapping("/activate")
    public ActivateResponse activateConnection(@RequestBody final ActivateRequest activateRequest) {
        System.out.println("Entered activate connection route");
        connectionIdLocal.set(activateRequest.getConnectionId());

        ActivateResponse activateResponse = connectionService.activateConnection(activateRequest);
        System.out.println("Returning activate response: " + activateResponse);
        return activateResponse;
    }

    @PostMapping("/deactivate/{connectionId}")
    public DeactivateResponse deactivateConnection(@PathVariable final String connectionId) {
        System.out.println("Entered deactivate connection route");

        DeactivateResponse deactivateResponse = connectionService.deactivateConnection(connectionId);
        System.out.println("Returning deactivate response: " + deactivateResponse);
        return deactivateResponse;
    }

    // After the /activate response, the connection callback can be done asynchronously in case of any slow additional processing required,
    // i.e. associating a Fortellis organization to an internal domain entity such as a dealership.
    // This is an example of immediately calling the connection callback after the response, but the call can be made later or even manually.
    @Bean
    public MappedInterceptor interceptor() {
        String[] paths = new String[]{"/activate"};
        return new MappedInterceptor(paths, new HandlerInterceptor() {
            @Override
            public void afterCompletion(HttpServletRequest request, HttpServletResponse response, Object handler, Exception ex) throws Exception {
                String connectionId = connectionIdLocal.get();
                connectionIdLocal.set(null);

                // A connection is not active until the connection callback is made with an accepted status
                callbackService.connectionCallback(connectionId);
            }
        });
    }
}