package io.fortellis.developer.adminapiexample.models;

import lombok.Data;

@Data
public class CallbackRequest {
    final static public String ACCEPTED = "accepted";
    final static public String REJECTED = "rejected";

    private String status;

    public CallbackRequest(String status) {
        this.status = status;
    }
}