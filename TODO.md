# TODO 
tldr; exploring sql code gen.

## publish function to APIM [COMPLETE; for now]
- [x] Generate swagger.json at build time
  there is a need to publish generated to APIM

 - [x] make package to generate swagger.json
   outcome: https://github.com/djseng/Microsoft.Azure.Functions.Worker.Extensions.OpenAPI.CLI 
     artifact: nuget Microsoft.Azure.Functions.Worker.Extensions.OpenAPI.CLI v1.0.6

   ! potential future: deploy package to some nuget repository
     atm: nuget Microsoft.Azure.Functions.Worker.Extensions.OpenAPI.CLI is committed to repo explicitly with custom `nuget.config`

- [x] Publish function to azure
  there is need to automate publishing a function to azure automatically

  outcome: https://github.com/djseng/PocApiDescFunction/blob/master/.github/workflows/cd.yaml

  ! potential future: Validate the updated `swagger.json` before publishing
    atm: revision is pushed and activated as long as there is a successful build
    poc
    ```bash
    currentActiveJson="ACTIVE APIM swagger.json"
    proposedJson="REPO swagger.json"
    docker run --rm -t -v $(pwd):/specs:ro   openapitools/openapi-diff:latest /specs/$currentActionJson /specs/$proposedJson --state
    # when no_changes
    #   ==> do not import revision to APIM
    # when incompatible
    #   ==> fail ci, requires manual intervention, do not publish function
    # when compatible
    #   ==> proceed as normal
    ```

  - [x] setup service 
    outcome: poc: https://github.com/djseng/PocApiDescFunction

  - [x] setup service principal
    ```bash
    cispname="NAME OF THE SERVICE PRINCIPAL"
    sub="SUBSCRIPTION ID"
    repo="NAME OF THE REPOSITORY"
    functionrg="NAME OF THE RESOURCE GROUP THE FUNCTION APP RESOURCE RESIDES IN"
    functionapp="NAME OF THE FUNCTION APP RESOURCE"
    apimrg="NAME OF THE RESOURCE GROUP THE APIM RESOURCE RESIDES IN"
    apim="NAME OF THE APIM RESOURCE"

    # create a service principal (sp) has "Contributor" role to scope of the $functionapp
    secretJson=$(az ad sp create-for-rbac --name "$cispname" --role contributor --scopes /subscriptions/$sub/resourceGroups/$functionrg/providers/Microsoft.Web/sites/$functionapp --sdk-auth)
    clientId=$(echo $secretJson | jq .clientId)

    # save the resultant `AZURE_CREDENTIALS` json as a secret in the repo for use with the `azure/login@v1` action
    gh secret set AZURE_CREDENTIALS -a actions -b $secretJson -r $repo

    # add "Contributor" role to scope of the $apim
    az role assignment create --assignee $clientId --role "Contributor" --scope "/subscriptions/$sub/resourceGroups/$apimrg/providers/Microsoft.ApiManagement/service/$apim"
    ```

  - [x] generate creds secrets for ci usage
    outcome: completed in outcome above

  - [x] ~~validate swagger.json(openapi spec v3) on build~~
    outcome: BOM causing `az apim api import` to fail
      issue: https://github.com/Azure/azure-cli/issues/29405
      artifact: nuget Microsoft.Azure.Functions.Worker.Extensions.OpenAPI.CLI v1.0.7

- [x] ~~Generating valid swagger.json (openapi spec v3) (pre req to import apim) using `OpenApi*` attribute from the `Microsoft.Azure.WebJobs.Extensions.OpenApi` nuget.~~
  outcome: BOM issue noted above, gave perception that `Microsoft.Azure.WebJobs.Extensions.OpenApi` was generating invalid OpenApi spec (big rabbit hole)

! API Managment
  - stumbled upon https://learn.microsoft.com/en-us/azure/api-center/ which appears to handle coordination between multiple APIM instances (dev, test, production)
  - mention of Azure APIops, is this `https://github.com/Azure/apiops` the same?
  - How do APICenter and APIOps interact or overlap?

! need to include as a task when generating function endpoints with appropriate `OpenApi*` attributes

! need to backfill already implemented endpoints with correct `OpenApi*` endpoints

## sproc codegen
- [*] SQL Stored Procedure Client CodeGen (wip in private djseng repo, should move to 3cloud org potentially)
 - [ ] finish wip
 - [ ] get pattern used to execute sprocs in the streamline code


# KEY

=> `##` is a story

=> `- [ ]` is a task TODO

=> `- [x]` is a task COMPLETED

=> `- [*]` is a task INPROGRESS

=> poc is Proof of Concept

=> wip is Work in Progress

=> ! is future work