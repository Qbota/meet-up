*** Settings ***
Documentation     A test suite with a single test for valid login.

Resource          resource.robot

*** Test Cases ***
Test register
    Open Browser To Main Page
    Go to Register Page
    Input Username    testUser
    Input Login       testLogin
    Input Password    password@123
    Input Password Confirmation   password@123
    Submit Credentials
    [Teardown]    Close Browser
                    