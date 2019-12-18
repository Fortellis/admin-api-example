package io.fortellis.developer.adminapiexample.models;

import java.util.List;

import lombok.Data;

@Data
public class DeactivateResponse {
    List<ActionLink> links;
}