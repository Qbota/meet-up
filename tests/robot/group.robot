*** Settings ***
Documentation     A test suite with a single test for valid group creation

Resource          resource.robot

*** Test Cases ***
Test register
    Open Browser To Main Page
    Go to Login Page
    Input Username       testUser
    Input Password    Test123!
    Submit Credentials
    Go To Group Page
    Click create group
    Input group name    testGroup
    Input group descritpion  testGroupDescription
    Select icon
    Click members list
    Create group
    Go To Group Page


