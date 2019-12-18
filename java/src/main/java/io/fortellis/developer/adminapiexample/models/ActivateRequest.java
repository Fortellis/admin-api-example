package io.fortellis.developer.adminapiexample.models;

import lombok.Data;


@Data
public class ActivateRequest {
    EntityInfo entityInfo;
    SolutionInfo solutionInfo;
    UserInfo userInfo;
    ApiInfo apiInfo;
    String subscriptionId;
    String connectionId;
}