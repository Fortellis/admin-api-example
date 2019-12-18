package io.fortellis.developer.adminapiexample.service;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import io.fortellis.developer.adminapiexample.dao.ConnectionDao;
import io.fortellis.developer.adminapiexample.models.ActionLink;
import io.fortellis.developer.adminapiexample.models.ActivateRequest;
import io.fortellis.developer.adminapiexample.models.ActivateResponse;
import io.fortellis.developer.adminapiexample.models.DeactivateResponse;

@Service
public class ConnectionService {

    @Autowired
    private ConnectionDao connectionDao;

    public ActivateResponse activateConnection(final ActivateRequest activateRequest) {
        System.out.println("Activating request: " + activateRequest);

        connectionDao.addConnection(activateRequest);

        ActivateResponse activateResponse = new ActivateResponse();

        List<ActionLink> links = new ArrayList<ActionLink>();
        ActionLink link = new ActionLink();
        link.setHref("https://example.com/deactivate");
        link.setRel("deactivate");
        link.setMethod("POST");
        links.add(link);

        activateResponse.setLinks(links);
        return activateResponse;
    }

    public DeactivateResponse deactivateConnection(final String connectionId) {
        System.out.println("Deactiving connection: " + connectionId);

        connectionDao.deleteConnection(connectionId);

        DeactivateResponse deactivateResponse = new DeactivateResponse();

        List<ActionLink> links = new ArrayList<ActionLink>();
        ActionLink link = new ActionLink();
        link.setHref("https://example.com/activate");
        link.setRel("activate");
        link.setMethod("POST");
        links.add(link);

        deactivateResponse.setLinks(links);
        return deactivateResponse;
    }
}