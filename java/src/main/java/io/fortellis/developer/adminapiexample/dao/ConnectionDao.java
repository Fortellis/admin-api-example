package io.fortellis.developer.adminapiexample.dao;

import org.springframework.stereotype.Repository;

import io.fortellis.developer.adminapiexample.models.ActivateRequest;

@Repository
public class ConnectionDao {
    public void addConnection(final ActivateRequest activateRequest) {
        // TODO: Add logic for adding connection to database
        System.out.println("Simulating added connection " + activateRequest.getConnectionId() + " to database");
    }

    public void deleteConnection(final String connectionId) {
        // TODO: Add logic for deleting or deactivating connection in database
        System.out.println("Simulating deleted connection id: " + connectionId + " from database");
    }
}