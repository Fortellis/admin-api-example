package io.fortellis.developer.adminapiexample.models;

import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonInclude.Include;

import lombok.Data;

@Data
public class ActionLink {
    String href; // The target URI

    String rel; // The link relation type to the resource

    @JsonInclude(Include.NON_NULL)
    String method; // The HTTP verb that MUST be used to make a request to the target of the link

    @JsonInclude(Include.NON_NULL)
    String title; // A title for the link to facilitate understanding by the end clients
}