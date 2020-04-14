# Overview
## Info
This is an example starter project for  Node.js implementation of the Admin API defined in module-admin-v2.yaml
> For more info, please see https://docs.fortellis.io/docs/tutorials/admin-api/overview

All of the method described are implemented to return static values

## Purpose
The Admin API is used as a bridge between Fortellis subscriptions and a Provider's domain.

> A subscription is made when a user purchases a Fortellis solution and is used when solutions make calls to Fortellis APIs. The subscription carries implicit information about the user's organization and the solution's info. A subscription is made of several connections, which link a Fortellis API to an API Implementation.

In order to store the information tied with the subscription id, a provider can implement an Admin API according to this [specification](https://docs.fortellis.io/docs/tutorials/admin-api/admin-api-specs/). Once completed and configured in an API Implementation, Fortellis will send the Admin API information about a subscription whenever a new subscription is made. The Admin API can then perform any necessary processes such as storing, mapping, or acquiring authorization before activating the connection by calling the connection callback endpoint.

Simalar to activation, a subscription might be deactivated, and Fortellis will notify the Admin API that a subscription should no longer be useable.

# Security
When Fortellis calls the Admin API, an OAuth 2.0 JWT token will be sent in the `Authorization` header of the request. This token should be validated against the Fortellis authorization server by the Admin API to ensure that the request was made by Fortellis.

> For more information on token verification, please see https://docs.fortellis.io/docs/tutorials/integration/verifying-JSON-tokens/

# Routes

## Activate
POST `/activate`

Example request body
```
{
	"entityInfo": {
    	"id": "2529a384-aee3-4b9a-af35-ed08e77dee15",
    	"name": "Super Car Dealership",
    	"address": "1234 Some St. Columbus, OH 43235",
    	"countryCode": "US",
	    "phoneNumber": "(614) 555-5555"
	},
	"solutionInfo": {
    	"id": "f83eaff0-3f88-4ebd-9fc8-25f051632968",
    	"name": "Awesome Application",
    	"developer": "Awesome App Developer",
    	"contactEmail": "contact@example.com"
	},
	"userInfo": {
    	"fortellisId": "user@example.com"
	},
	"apiInfo": {
    	"id": "v1-appointments",
    	"name": "Appointments"
	},
	"subscriptionId": "0f5b7a0c-8c59-4f4e-a9b8-9086b49f97e2",
	"connectionId": "7b8d5510-9bf7-4339-906f-32b8bdf31bc6"
}
```

## Deactivate
POST `/deactivate/{connectionId}`

# Connection callback
POST `https://subscriptions.fortellis.io/connections/{connectionId}/callback`

A connection is not activated until a connection callback is made to Fortellis after the `/activate` response. This allows a Provider as much time as needed to perform any actions necessary before accepting the responsibility of appropriately fulfilling the corresponding requests.

The callback endpoint is similarly protected. In order to call the callback endpoint, the request must use an appropriate OAuth 2.0 token from the Fortellis authorization server.

> For more information about getting an access token, please see https://docs.fortellis.io/docs/tutorials/integration/getting-your-access-token/.

The body of the POST call must be JSON containing a `status` field with a value of either `accepted` or `rejected`.
```
{
    "status": "accepted"
}
```

# Alternative to Admin API
As an alternative to implementing the Admin API, Providers may look up information about the subscription on every request, but this will cause increased latency and is not preferred.

> For more information, please see https://docs.fortellis.io/docs/tutorials/integration/no-admin-api/

# Running

Prerequisites: Node.js 8+

1. `npm install`
2. `npm start`

# Testing
The following should return a `401` because there is no authorization:
```
curl -vX POST \
  http://localhost:3000/activate \
  -d '{
  "entityInfo": {
    "id": "63153c90-6f97-4cd7-b721-dd3ac8b8083d",
    "name": "Super Car Dealership",
    "address": "1234 Some St. Columbus, OH 43235",
    "countryCode": "US",
    "phoneNumber": "(614) 555-5555"
  },
  "solutionInfo": {
    "id": "9e592d11-738c-4d5c-9508-859d391f7192",
    "name": "Awesome Application",
    "developer": "Awesome App Developer",
    "contactEmail": "contact@example.com"
  },
  "userInfo": { "fortellisId": "user@example.com" },
  "apiInfo": { "id": "v1-appointments", "name": "Appointments" },
  "subscriptionId": "0f5b7a0c-8c59-4f4e-a9b8-9086b49f97e2",
  "connectionId": "7b8d5510-9bf7-4339-906f-32b8bdf31bc6"
}'
```

Use the following to get an access token:
> `client_id` and `client_secret` can be found under each API Implementation in Developer Account
```
 echo "<client_id>:<client_secret>" | base64
```
```
curl -X POST \
  https://identity.fortellis.io/oauth2/aus1p1ixy7YL8cMq02p7/v1/token \
  -H 'Authorization: Basic <base64 encoded client_id:client_secret>' \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -d 'grant_type=client_credentials&scope=anonymous'
```
The token will be found in the JSON response in the field `access_token`.

Use that token in the following requests:

`/activate`
```
curl -vX POST \
  http://localhost:3000/activate \
  -H 'Authorization: Bearer <access_token>' \
  -d '{
  "entityInfo": {
    "id": "63153c90-6f97-4cd7-b721-dd3ac8b8083d",
    "name": "Super Car Dealership",
    "address": "1234 Some St. Columbus, OH 43235",
    "countryCode": "US",
    "phoneNumber": "(614) 555-5555"
  },
  "solutionInfo": {
    "id": "9e592d11-738c-4d5c-9508-859d391f7192",
    "name": "Awesome Application",
    "developer": "Awesome App Developer",
    "contactEmail": "contact@example.com"
  },
  "userInfo": { "fortellisId": "user@example.com" },
  "apiInfo": { "id": "v1-appointments", "name": "Appointments" },
  "subscriptionId": "0f5b7a0c-8c59-4f4e-a9b8-9086b49f97e2",
  "connectionId": "7b8d5510-9bf7-4339-906f-32b8bdf31bc6"
}'
```
`/deactivate`
```
curl -X POST \
  http://localhost:3000/deactivate/7b8d5510-9bf7-4339-906f-32b8bdf31bc6 \
  -H 'Authorization: Bearer <access_token>'
```
Make sure to verify the issuer of the token and the audience for the token at each of these endpoints.

NOTE: Use the `api_provider` audience in your token to develop your Admin API. Once you are receiving tokens from Fortellis, change the audience to `fortellis`.

Additionally, each API Implementation in Developer Account contains a `Postman Collection Test`, which can be imported to Postman for example calls with autofilled information.
