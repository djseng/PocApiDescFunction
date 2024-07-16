# publish function to APIM
- [x] Generate swagger.json at build time
 - [x] make package to generate swagger.json
 - [ ] deploy package to some nuget repository (TBD)
- [x] Publish function to azure
  - [x] setup service 
  - [x] setup service principal
  - [x] generate creds secrets for ci usage
  - [ ] validate swagger.json(openapi spec v3) on build
- [*] Generating valid swagger.json (openapi spec v3) (pre req to import apim) using `OpenApi*` attribute from the `Microsoft.Azure.WebJobs.Extensions.OpenApi.Core` nuget.
 - [ ] need to include as a task when generating function endpoints with appropriate `OpenApi*` attributes
 - [ ] need to backfill already implemented endpoints with correct `OpenApi*` endpoints
 - [ ] need to ensure generated swagger.json is valid, possibly a build task

# sproc codegen
- [ ] SQL Stored Procedure Client CodeGen (wip in private djseng repo, should move to 3cloud org potentially)
 - [ ] finish wip
 - [ ] get pattern used to execute sprocs in the streamline code
